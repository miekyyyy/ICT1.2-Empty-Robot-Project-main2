using SimpleMqtt;
using Avans.StatisticalRobot;
namespace BerichtOntvangenSpace;
public class BerichtOntvangen
{
    SimpleMqttClient _client;
    Led _led5;
    public BerichtOntvangen(SimpleMqttClient client, Led led5)
    {
        _client = client;
        _led5 = led5;
    }

    public void OnlineNoodKnop()
    {
        _client.OnMessageReceived += (sender, args) =>
        {
            if(args.Topic == "NoodKnop_Web")
            {
                Robot.Motors(0,0);
                _led5.SetOff();
                Robot.Wait(100);
                Environment.Exit(0);
            }
        };
    }

}