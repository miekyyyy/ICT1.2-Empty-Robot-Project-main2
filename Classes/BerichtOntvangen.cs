using SimpleMqtt;
using Avans.StatisticalRobot;
namespace BerichtOntvangenSpace;
public class BerichtOntvangen
{
    SimpleMqttClient _client;
    Led _led5;
    LCD16x2 _lcd;
    public BerichtOntvangen(SimpleMqttClient client, Led led5, LCD16x2 lcd)
    {
        _client = client;
        _led5 = led5;
        _lcd = lcd;
    }

    public void OnlineBerichten()
    {
        _client.OnMessageReceived += (sender, args) =>
        {
            if(args.Topic == "NoodKnop_Web")
            {
                Console.WriteLine("Er is op noodknop_web gedrukt");
                Robot.Motors(0,0);
                _led5.SetOff();
                Robot.Wait(100);
                Environment.Exit(0);
            }

            else if(args.Topic == "LCD")
            {
                Console.WriteLine("LCD tekst is veranderd");
                _lcd.SetText(args.Message);
                Robot.PlayNotes("fgfg");
            }
        };
    }

}