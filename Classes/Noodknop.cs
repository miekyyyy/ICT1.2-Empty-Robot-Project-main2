namespace NoodKnopSpace;
using Avans.StatisticalRobot;
using HiveMQtt;

public class NoodKnop
{
   bool _button6WasPressed;
   Button _button6;
   Led _led5;
   public NoodKnop(bool button6WasPressed, Button button6, Led led5)
   {
      _button6 = button6;
      _button6WasPressed = button6WasPressed;
      _led5 = led5;
   }
    public void NoodKnopDruk()
    {
          if (!_button6WasPressed)
            {
               Robot.Motors(0, 0);
               _led5.SetOff();
            }
    }

    public void GetNoodKnopTotaal(SimpleMqtt.SimpleMqttClient client)
    {
      if (_button6.GetState() == "Pressed") // als de knop gedrukt is
         {
            NoodKnopDruk(); //noodstop methode
            client.PublishMessage("Er is gedrukt", "NoodKnop");
            Robot.Wait(100);
            Environment.Exit(0);
         }
      else
      {
         _button6WasPressed = false; //zorgt ervoor dat de input maar per klik is         }
      }
    }
}