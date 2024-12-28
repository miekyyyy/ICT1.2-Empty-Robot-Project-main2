namespace IngenomenMedicijnSpace;
using System.Device.Gpio;
using Avans.StatisticalRobot;
using SimpleMqtt;
public class IngenomenMedicijn
{
   LCD16x2 _lcd;
   bool _button23WasPressed;
   PeriodTimer _timerMedicijn;
   int _aantalIngenomenMedicijnen;
   Button _button23;
   public IngenomenMedicijn(LCD16x2 lcd, bool button23WasPressed, PeriodTimer timerMedicijn, int aantalIngenomenMedicijnen, Button button23)
   {
      _lcd = lcd;
      _button23WasPressed = button23WasPressed;
      _timerMedicijn = timerMedicijn;
      _aantalIngenomenMedicijnen = aantalIngenomenMedicijnen;
      _button23 = button23;

   }
    public void AantalIngenomenMedicijnenX()
    {

        if (!_button23WasPressed)
        {
            _aantalIngenomenMedicijnen += 1;
            Console.WriteLine($"aantal keer dat medicijnen zijn ingenomen is: {_aantalIngenomenMedicijnen}");
            _lcd.SetText($"Medicijnen \ningenomen: {_aantalIngenomenMedicijnen}");
        }
   } 

   public void AantalIngenomenMedicijnenXTotaal(SimpleMqtt.SimpleMqttClient client)
   {
    if (_button23.GetState() == "Pressed")  // als de knop gedrukt is
         {
            if (_timerMedicijn.Check()) // als de interne klok is afgelopen
            {
               AantalIngenomenMedicijnenX(); // medicijn tel methode
               client.PublishMessage("Er is gedrukt ", "Medicijn_Inname");
            }
         }
         else
         {
            _button23WasPressed = false;
         }
   }
}