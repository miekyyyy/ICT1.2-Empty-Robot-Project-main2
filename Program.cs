using System.Device.Gpio;
using System.Security.Cryptography.X509Certificates;
using Avans.StatisticalRobot;
using BatterijPercentageSpace;
using IngenomenMedicijnSpace;
using NoodKnopSpace;
using RobotRijdenSpace;
using MedicijnenInnemenSpace;
using SimpleMqtt;
using BerichtOntvangenSpace;
internal class Program 
{  
   private static async void Main(string[] args)
   {
      var client = SimpleMqttClient.CreateSimpleMqttClientForHiveMQ("3a59955c");
      await client.SubscribeToTopic("NoodKnop_Web");

      LCD16x2 lcd = new LCD16x2(0x3E); // sensoren, knoppen etc.
      Led led22 = new Led(22);
      Button button23 = new Button(23);
      Led led5 = new Led(5);
      Button button6 = new Button(6);
      Ultrasonic afstandsensor = new Ultrasonic(16);

      bool geefBericht = true;   // benodigde variabelen
      bool button23WasPressed = false;
      int aantalIngenomenMedicijnen = 0;
      bool gaRijden = false;
      bool gaDraaien = true;
      bool button6WasPressed = false;
      
      PeriodTimer timerMedicijnInname = new PeriodTimer(31000); // interne timers
      PeriodTimer timerMedicijn = new PeriodTimer(500); 
      PeriodTimer timerBatterij = new PeriodTimer(30000); 
      PeriodTimer timerNaBericht = null;

      Batterij batterij = new Batterij(timerBatterij); // dit zijn de constructors
      NoodKnop noodKnop = new NoodKnop(button6WasPressed, button6, led5); 
      RobotRijden robotRijden = new RobotRijden(afstandsensor, gaDraaien, gaRijden);
      IngenomenMedicijn ingenomenMedicijn = new IngenomenMedicijn(lcd, button23WasPressed, timerMedicijn, aantalIngenomenMedicijnen, button23);
      MedicijnenInnemen medicijnenInnemen = new MedicijnenInnemen(lcd, geefBericht, timerMedicijnInname, timerNaBericht, gaDraaien, gaRijden, ingenomenMedicijn, noodKnop, robotRijden);
      BerichtOntvangen berichtOntvangen = new BerichtOntvangen(client, led5);

      robotRijden.RobotRij(); // robot startup
      led5.SetOn();
      led22.SetOn();
      lcd.SetText($"Medicijnen \ningenomen: {aantalIngenomenMedicijnen}");
      Console.WriteLine($"De afstand tot een object is: {afstandsensor.GetUltrasoneDistance()} cm");
   
      while (true)
      {
         
         batterij.GetMeasurementTotal(client); // batterijpercentage

         robotRijden.RobotBeweging(); // al het rijden in 1 methode

         noodKnop.GetNoodKnopTotaal(client); // noodknop in 1 methode

         medicijnenInnemen.MedicijnInnemen(client); // medicijn innemen op bepaalde tijd methode

         ingenomenMedicijn.AantalIngenomenMedicijnenXTotaal(client); // telt aantal ingenomen medicijnen methode

         berichtOntvangen.OnlineNoodKnop();
         
         Robot.Wait(100); // wachten om te snelle while-loop te verkomen
      }
   }
}