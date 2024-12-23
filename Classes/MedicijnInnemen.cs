using Avans.StatisticalRobot;
using NoodKnopSpace;
using RobotRijdenSpace;
using IngenomenMedicijnSpace;
using Microsoft.VisualBasic;
namespace MedicijnenInnemenSpace;
using SimpleMqtt;

public class MedicijnenInnemen
{       
    LCD16x2 _lcd;
    bool _geefBericht;
    PeriodTimer _timerMedicijnInname;
    PeriodTimer _timerNaBericht;
    bool _gaDraaien;
    bool _gaRijden;
    IngenomenMedicijn _ingenomenMedicijn;
    NoodKnop _noodKnop;
    RobotRijden _robotRijden;
    public MedicijnenInnemen(LCD16x2 lcd, bool geefBericht, PeriodTimer timerMedicijnInname, PeriodTimer timerNaBericht, bool gaDraaien, bool gaRijden, IngenomenMedicijn ingenomenMedicijn, NoodKnop noodKnop, RobotRijden robotRijden)
    {
        _lcd = lcd;
        _geefBericht = geefBericht;
        _timerMedicijnInname = timerMedicijnInname;
        _timerNaBericht = timerNaBericht;
        _gaDraaien = gaDraaien;
        _gaRijden = gaDraaien;
        _ingenomenMedicijn = ingenomenMedicijn;
        _noodKnop = noodKnop;
        _robotRijden = robotRijden;
    }

    public void MedicijnInnemen(SimpleMqtt.SimpleMqttClient client)
    {
        string huidigeTijd = DateTime.Now.ToString("h:mm tt"); // van internet, geeft huidige tijd
        if (_timerMedicijnInname.Check())
        {
            if (huidigeTijd == "3:13 PM")
            {
                if (_geefBericht == true)
                {
                    _lcd.SetText("Neem je medicijnen in!");     // geeft op lcd weer dat je medicijnen ingenomen moeten worden op een bepaalde tijd
                    Robot.PlayNotes("baba");
                    if (_timerNaBericht == null)
                    {
                        _timerNaBericht = new PeriodTimer(10000);
                    }
                    _gaRijden = false;
                    _gaDraaien = false;
                    Robot.Motors(0, 0);
                    while (_geefBericht == true)
                    {
                        _noodKnop.GetNoodKnopTotaal(client);
                        _ingenomenMedicijn.AantalIngenomenMedicijnenXTotaal(client);
                        if (_timerNaBericht.Check())
                        {
                            _robotRijden.RobotRij();
                            _gaDraaien = true;
                            _geefBericht = false;
                        }
                    }

                }

            }
        }

    }
}

