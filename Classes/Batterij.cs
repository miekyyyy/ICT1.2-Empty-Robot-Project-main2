namespace BatterijPercentageSpace;
using System.Device.Gpio;
using Avans.StatisticalRobot;
using HiveMQtt.MQTT5.Types;
using SimpleMqtt;
public class Batterij
{
    PeriodTimer _timerBatterij;
    public Batterij(PeriodTimer timerBatterij)
    {
        _timerBatterij = timerBatterij;
    }
    public string GetMeasurement()
    {
        double maxBatterij = 8400;

        double batterijPercentage = Robot.ReadBatteryMillivolts() / maxBatterij * 100;
        batterijPercentage = Math.Round(batterijPercentage, 0);
        return $"{batterijPercentage} %";
    }

    public void GetMeasurementTotal(SimpleMqtt.SimpleMqttClient client)
    {
        if (_timerBatterij.Check()) // battrijpercentage weergeven na bepaalde tijd
         {
            Console.WriteLine(GetMeasurement());
            client.PublishMessage($"{GetMeasurement()}","Batterij");
         }
    }
}