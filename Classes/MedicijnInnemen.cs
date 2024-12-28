using Avans.StatisticalRobot;
using NoodKnopSpace;
using RobotRijdenSpace;
using IngenomenMedicijnSpace;
using SQL;
using Microsoft.VisualBasic;
namespace MedicijnenInnemenSpace;


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
    SqlRepository _sqlRepository;
    public MedicijnenInnemen(LCD16x2 lcd, bool geefBericht, PeriodTimer timerMedicijnInname, PeriodTimer timerNaBericht, bool gaDraaien, bool gaRijden, IngenomenMedicijn ingenomenMedicijn, NoodKnop noodKnop, RobotRijden robotRijden, SqlRepository sqlRepository)
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
        _sqlRepository = sqlRepository;
    }
    public List<string> TijdsWaardeLijst()
    {
        // Haal de lijst van Schema-objecten op
        var schemaList = _sqlRepository.SqlRetrieveMethodeSchema();

        // Converteer naar een lijst van strings (alleen TijdsWaarde)
        var tijdsWaardeLijst = schemaList.Select(schema => schema.tijdsWaarde).ToList();

        return tijdsWaardeLijst;
    }
    public void MedicijnInnemen1(SimpleMqtt.SimpleMqttClient client)
    {
        string huidigeDag = DateTime.Now.ToString("ddd");
        if (huidigeDag == "Mon")
        {
            var tijdsWaardeLijst = TijdsWaardeLijst();
            string huidigeTijd = DateTime.Now.ToString("h:mm tt"); // van internet, geeft huidige tijd
            if (_timerMedicijnInname.Check())
            {
                if (huidigeTijd == DateTime.Parse(tijdsWaardeLijst[0]).ToString("h:mm tt"))
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
                        _geefBericht = true;
                        _timerNaBericht = null;

                    }

                }
            }
        }

    }

    public void MedicijnInnemen2(SimpleMqtt.SimpleMqttClient client)
    {
        string huidigeDag = DateTime.Now.ToString("ddd");
        if (huidigeDag == "Tue")
        {
            var tijdsWaardeLijst = TijdsWaardeLijst();
            string huidigeTijd = DateTime.Now.ToString("h:mm tt"); // van internet, geeft huidige tijd
            if (_timerMedicijnInname.Check())
            {
                if (huidigeTijd == DateTime.Parse(tijdsWaardeLijst[1]).ToString("h:mm tt"))
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
                        _geefBericht = true;
                        _timerNaBericht = null;
                    }

                }
            }
        }

    }

    public void MedicijnInnemen3(SimpleMqtt.SimpleMqttClient client)
    {
        string huidigeDag = DateTime.Now.ToString("ddd");
        if (huidigeDag == "Wed")
        {
            var tijdsWaardeLijst = TijdsWaardeLijst();
            string huidigeTijd = DateTime.Now.ToString("h:mm tt"); // van internet, geeft huidige tijd
            if (_timerMedicijnInname.Check())
            {
                if (huidigeTijd == DateTime.Parse(tijdsWaardeLijst[2]).ToString("h:mm tt"))
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
                        _geefBericht = true;
                        _timerNaBericht = null;
                    }

                }
            }
        }

    }

    public void MedicijnInnemen4(SimpleMqtt.SimpleMqttClient client)
    {
        string huidigeDag = DateTime.Now.ToString("ddd");
        if (huidigeDag == "Thu")
        {
            var tijdsWaardeLijst = TijdsWaardeLijst();
            string huidigeTijd = DateTime.Now.ToString("h:mm tt"); // van internet, geeft huidige tijd
            if (_timerMedicijnInname.Check())
            {
                if (huidigeTijd == DateTime.Parse(tijdsWaardeLijst[3]).ToString("h:mm tt"))
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
                        _geefBericht = true;
                        _timerNaBericht = null;
                    }

                }
            }
        }

    }

    public void MedicijnInnemen5(SimpleMqtt.SimpleMqttClient client)
    {
        string huidigeDag = DateTime.Now.ToString("ddd");
        if (huidigeDag == "Fri")
        {
            var tijdsWaardeLijst = TijdsWaardeLijst();
            string huidigeTijd = DateTime.Now.ToString("h:mm tt"); // van internet, geeft huidige tijd
            if (_timerMedicijnInname.Check())
            {
                if (huidigeTijd == DateTime.Parse(tijdsWaardeLijst[4]).ToString("h:mm tt"))
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
                        _geefBericht = true;
                        _timerNaBericht = null;
                    }

                }
            }
        }

    }

    public void MedicijnInnemen6(SimpleMqtt.SimpleMqttClient client)
    {
        string huidigeDag = DateTime.Now.ToString("ddd");
        if (huidigeDag == "Sat")
        {
            var tijdsWaardeLijst = TijdsWaardeLijst();
            string huidigeTijd = DateTime.Now.ToString("h:mm tt"); // van internet, geeft huidige tijd
            if (_timerMedicijnInname.Check())
            {
                if (huidigeTijd == DateTime.Parse(tijdsWaardeLijst[5]).ToString("h:mm tt"))
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
                        _geefBericht = true;
                        _timerNaBericht = null;
                    }

                }
            }
        }

    }

    public void MedicijnInnemen7(SimpleMqtt.SimpleMqttClient client)
    {
        string huidigeDag = DateTime.Now.ToString("ddd");
        if (huidigeDag == "Sun")
        {
            var tijdsWaardeLijst = TijdsWaardeLijst();
            string huidigeTijd = DateTime.Now.ToString("h:mm tt"); // van internet, geeft huidige tijd
            if (_timerMedicijnInname.Check())
            {
                if (huidigeTijd == DateTime.Parse(tijdsWaardeLijst[6]).ToString("h:mm tt"))
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
                        _geefBericht = true;
                        _timerNaBericht = null;
                    }

                }
            }
        }

    }
}

