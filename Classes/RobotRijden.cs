namespace RobotRijdenSpace;
using Avans.StatisticalRobot;
public class RobotRijden
{
    Ultrasonic _afstandsensor;
    bool _gaDraaien;
    bool _gaRijden;
    public RobotRijden(Ultrasonic afstandsensor, bool gaDraaien, bool gaRijden)
    {
        _afstandsensor = afstandsensor;
        _gaDraaien = gaDraaien;
        _gaRijden = gaRijden;
    }
    public void RobotRij()
    {
        Robot.Motors(100, 90);
    }

    public void RobotDraai()
    {
        Robot.Motors(180, -180);
    }

    public void RobotBeweging()
    {
        Console.WriteLine($"De afstand tot een object is: {_afstandsensor.GetUltrasoneDistance()} cm");

         if (_afstandsensor.GetUltrasoneDistance() < 10 && _gaDraaien == true) // robot draaien
         {
            this.RobotDraai();
            _gaRijden = true;
            _gaDraaien = false;
         }

         if (_afstandsensor.GetUltrasoneDistance() > 10 && _gaRijden == true) // robot verder laten rijden
         {
            this.RobotRij();
            _gaRijden = false;
            _gaDraaien = true;
         }
    }
}