using Avans.StatisticalRobot;
using BatterijPercentageSpace;
using IngenomenMedicijnSpace;
using NoodKnopSpace;
using RobotRijdenSpace;
using MedicijnenInnemenSpace;
using SimpleMqtt;
using BerichtOntvangenSpace;
using SQL;
internal class Program
{
   private static void Main(string[] args)
   {
      var client = SimpleMqttClient.CreateSimpleMqttClientForHiveMQ("3a59955c");
      client.SubscribeToTopic("NoodKnop_Web");
      client.SubscribeToTopic("LCD");

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
      PeriodTimer timerBatterij = new PeriodTimer(60000);
      PeriodTimer tijdsWaardeTimer = new PeriodTimer(20000);
      PeriodTimer timerNaBericht = null;

      SqlRepository sqlRepository = new SqlRepository("Server=aei-sql2.avans.nl,1443;Database=DB2228443;UID=ITI2228443;password=H1aiW6L4;TrustServerCertificate=true");
      Batterij batterij = new Batterij(timerBatterij); // dit zijn de constructors
      NoodKnop noodKnop = new NoodKnop(button6WasPressed, button6, led5);
      RobotRijden robotRijden = new RobotRijden(afstandsensor, gaDraaien, gaRijden);
      IngenomenMedicijn ingenomenMedicijn = new IngenomenMedicijn(lcd, button23WasPressed, timerMedicijn, aantalIngenomenMedicijnen, button23);
      MedicijnenInnemen medicijnenInnemen = new MedicijnenInnemen(lcd, geefBericht, timerMedicijnInname, timerNaBericht, gaDraaien, gaRijden, ingenomenMedicijn, noodKnop, robotRijden, sqlRepository);
      BerichtOntvangen berichtOntvangen = new BerichtOntvangen(client, led5, lcd);

      robotRijden.RobotRij(); // robot startup
      led5.SetOn();
      led22.SetOn();
      lcd.SetText($"Medicijnen \ningenomen: {aantalIngenomenMedicijnen}");
      Console.WriteLine($"De afstand tot een object is: {afstandsensor.GetUltrasoneDistance()} cm");
      medicijnenInnemen.TijdsWaardeLijst();
      while (true)
      {
         batterij.GetMeasurementTotal(client); // batterijpercentage

         robotRijden.RobotBeweging(); // al het rijden in 1 methode

         noodKnop.GetNoodKnopTotaal(client); // noodknop in 1 methode

         if (tijdsWaardeTimer.Check()) // Timer verloopt elke 20 seconden
         {
            var nieuweTijdsWaardeLijst = medicijnenInnemen.TijdsWaardeLijst();
            Console.WriteLine("TijdsWaardeLijst geüpdatet:");
            foreach (var tijd in nieuweTijdsWaardeLijst)
            {
               Console.WriteLine(tijd); // Debug: print de nieuwe tijdswaarden
            }
         }
         
         medicijnenInnemen.MedicijnInnemen1(client); // medicijn innemen op bepaalde tijd methode op een dag

         medicijnenInnemen.MedicijnInnemen2(client); // medicijn innemen op bepaalde tijd methode op een dag

         medicijnenInnemen.MedicijnInnemen3(client); // medicijn innemen op bepaalde tijd methode op een dag

         medicijnenInnemen.MedicijnInnemen4(client); // medicijn innemen op bepaalde tijd methode op een dag

         medicijnenInnemen.MedicijnInnemen5(client); // medicijn innemen op bepaalde tijd methode op een dag

         medicijnenInnemen.MedicijnInnemen6(client); // medicijn innemen op bepaalde tijd methode op een dag

         medicijnenInnemen.MedicijnInnemen7(client); // medicijn innemen op bepaalde tijd methode op een dag

         ingenomenMedicijn.AantalIngenomenMedicijnenXTotaal(client); // telt aantal ingenomen medicijnen methode

         berichtOntvangen.OnlineBerichten();

         Robot.Wait(100); // wachten om te snelle while-loop te verkomen
      }
   }
}