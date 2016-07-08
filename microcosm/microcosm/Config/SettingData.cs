using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using microcosm.Common;

namespace microcosm.Config
{
    // 複数クラスが存在（0～9）

    public class SettingData
    {
        public SettingXml xmlData;
        public string dispName { get; set; }

        // nn np nt n4 n5 n6
        // pn pp pt p4 p5 p6
        // tn tp tt t4 t5 t6 
        // 4n 4p 4t 44 45 46
        // 5n 5p 5t 54 55 56
        // 6n 6p 6t 64 65 66
        // 7重円サポートはいらないだろ
        // いやどうせなら？
        public double[,] orb_sun_soft_1st;
        public double[,] orb_sun_hard_1st;
        public double[,] orb_moon_soft_1st;
        public double[,] orb_moon_hard_1st;
        public double[,] orb_other_soft_1st;
        public double[,] orb_other_hard_1st;
        public double[,] orb_sun_soft_2nd;
        public double[,] orb_sun_hard_2nd;
        public double[,] orb_moon_soft_2nd;
        public double[,] orb_moon_hard_2nd;
        public double[,] orb_other_soft_2nd;
        public double[,] orb_other_hard_2nd;
        public double[,] orb_sun_soft_150;
        public double[,] orb_sun_hard_150;
        public double[,] orb_moon_soft_150;
        public double[,] orb_moon_hard_150;
        public double[,] orb_other_soft_150;
        public double[,] orb_other_hard_150;

        public bool[] dispCircle = new bool[] {
            true, false, false, false, false, false
        };
        // 0:11～15:45
        public List<Dictionary<int, bool>> dispPlanet;

        // [from, to]
        public bool[,] aspectConjunction;
        public bool[,] aspectOpposition;
        public bool[,] aspectSquare;
        public bool[,] aspectTrine;
        public bool[,] aspectSextile;
        public bool[,] aspectInconjunct;
        public bool[,] aspectSesquiquadrate;
        // [from, to]
        public bool[,] dispAspect;

        // no: 設定番号
        public SettingData(int no)
        {
            init(no);
        }
        public void init(int no)
        {
            xmlData = new SettingXml();

            this.dispName = "表示設定" + no.ToString();
            orb_sun_soft_1st = new double[6, 6] {
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 }
            };
            orb_sun_soft_2nd = new double[6, 6] {
                { 6.0, 6.0, 6.0, 6.0, 6.0, 6.0 },
                { 6.0, 6.0, 6.0, 6.0, 6.0, 6.0 },
                { 6.0, 6.0, 6.0, 6.0, 6.0, 6.0 },
                { 6.0, 6.0, 6.0, 6.0, 6.0, 6.0 },
                { 6.0, 6.0, 6.0, 6.0, 6.0, 6.0 },
                { 6.0, 6.0, 6.0, 6.0, 6.0, 6.0 }
            };
            orb_sun_soft_150 = new double[6, 6] {
                { 3.0, 3.0, 3.0, 3.0, 3.0, 3.0 },
                { 3.0, 3.0, 3.0, 3.0, 3.0, 3.0 },
                { 3.0, 3.0, 3.0, 3.0, 3.0, 3.0 },
                { 3.0, 3.0, 3.0, 3.0, 3.0, 3.0 },
                { 3.0, 3.0, 3.0, 3.0, 3.0, 3.0 },
                { 3.0, 3.0, 3.0, 3.0, 3.0, 3.0 }
            };
            orb_moon_soft_1st = new double[6, 6] {
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 }
            };
            orb_moon_soft_2nd = new double[6, 6] {
                { 6.0, 6.0, 6.0, 6.0, 6.0, 6.0 },
                { 6.0, 6.0, 6.0, 6.0, 6.0, 6.0 },
                { 6.0, 6.0, 6.0, 6.0, 6.0, 6.0 },
                { 6.0, 6.0, 6.0, 6.0, 6.0, 6.0 },
                { 6.0, 6.0, 6.0, 6.0, 6.0, 6.0 },
                { 6.0, 6.0, 6.0, 6.0, 6.0, 6.0 }
            };
            orb_moon_soft_150 = new double[6, 6] {
                { 3.0, 3.0, 3.0, 3.0, 3.0, 3.0 },
                { 3.0, 3.0, 3.0, 3.0, 3.0, 3.0 },
                { 3.0, 3.0, 3.0, 3.0, 3.0, 3.0 },
                { 3.0, 3.0, 3.0, 3.0, 3.0, 3.0 },
                { 3.0, 3.0, 3.0, 3.0, 3.0, 3.0 },
                { 3.0, 3.0, 3.0, 3.0, 3.0, 3.0 }
            };
            orb_other_soft_1st = new double[6, 6] {
                { 6.0, 6.0, 6.0, 6.0, 6.0, 6.0 },
                { 6.0, 6.0, 6.0, 6.0, 6.0, 6.0 },
                { 6.0, 6.0, 6.0, 6.0, 6.0, 6.0 },
                { 6.0, 6.0, 6.0, 6.0, 6.0, 6.0 },
                { 6.0, 6.0, 6.0, 6.0, 6.0, 6.0 },
                { 6.0, 6.0, 6.0, 6.0, 6.0, 6.0 }
            };
            orb_other_soft_2nd = new double[6, 6] {
                { 3.0, 3.0, 3.0, 3.0, 3.0, 3.0 },
                { 3.0, 3.0, 3.0, 3.0, 3.0, 3.0 },
                { 3.0, 3.0, 3.0, 3.0, 3.0, 3.0 },
                { 3.0, 3.0, 3.0, 3.0, 3.0, 3.0 },
                { 3.0, 3.0, 3.0, 3.0, 3.0, 3.0 },
                { 3.0, 3.0, 3.0, 3.0, 3.0, 3.0 }
            };
            orb_other_soft_150 = new double[6, 6] {
                { 1.5, 1.5, 1.5, 1.5, 1.5, 1.5 },
                { 1.5, 1.5, 1.5, 1.5, 1.5, 1.5 },
                { 1.5, 1.5, 1.5, 1.5, 1.5, 1.5 },
                { 1.5, 1.5, 1.5, 1.5, 1.5, 1.5 },
                { 1.5, 1.5, 1.5, 1.5, 1.5, 1.5 },
                { 1.5, 1.5, 1.5, 1.5, 1.5, 1.5 }
            };
            orb_sun_hard_1st = new double[6, 6] {
                { 4.0, 4.0, 4.0, 4.0, 4.0, 4.0 },
                { 4.0, 4.0, 4.0, 4.0, 4.0, 4.0 },
                { 4.0, 4.0, 4.0, 4.0, 4.0, 4.0 },
                { 4.0, 4.0, 4.0, 4.0, 4.0, 4.0 },
                { 4.0, 4.0, 4.0, 4.0, 4.0, 4.0 },
                { 4.0, 4.0, 4.0, 4.0, 4.0, 4.0 }
            };
            orb_sun_hard_2nd = new double[6, 6] {
                { 1.5, 1.5, 1.5, 1.5, 1.5, 1.5 },
                { 1.5, 1.5, 1.5, 1.5, 1.5, 1.5 },
                { 1.5, 1.5, 1.5, 1.5, 1.5, 1.5 },
                { 1.5, 1.5, 1.5, 1.5, 1.5, 1.5 },
                { 1.5, 1.5, 1.5, 1.5, 1.5, 1.5 },
                { 1.5, 1.5, 1.5, 1.5, 1.5, 1.5 }
            };
            orb_sun_hard_150 = new double[6, 6] {
                { 2.0, 2.0, 2.0, 2.0, 2.0, 2.0 },
                { 2.0, 2.0, 2.0, 2.0, 2.0, 2.0 },
                { 2.0, 2.0, 2.0, 2.0, 2.0, 2.0 },
                { 2.0, 2.0, 2.0, 2.0, 2.0, 2.0 },
                { 2.0, 2.0, 2.0, 2.0, 2.0, 2.0 },
                { 2.0, 2.0, 2.0, 2.0, 2.0, 2.0 }
            };
            orb_moon_hard_1st = new double[6, 6] {
                { 4.0, 4.0, 4.0, 4.0, 4.0, 4.0 },
                { 4.0, 4.0, 4.0, 4.0, 4.0, 4.0 },
                { 4.0, 4.0, 4.0, 4.0, 4.0, 4.0 },
                { 4.0, 4.0, 4.0, 4.0, 4.0, 4.0 },
                { 4.0, 4.0, 4.0, 4.0, 4.0, 4.0 },
                { 4.0, 4.0, 4.0, 4.0, 4.0, 4.0 }
            };
            orb_moon_hard_2nd = new double[6, 6] {
                { 2.0, 2.0, 2.0, 2.0, 2.0, 2.0 },
                { 2.0, 2.0, 2.0, 2.0, 2.0, 2.0 },
                { 2.0, 2.0, 2.0, 2.0, 2.0, 2.0 },
                { 2.0, 2.0, 2.0, 2.0, 2.0, 2.0 },
                { 2.0, 2.0, 2.0, 2.0, 2.0, 2.0 },
                { 2.0, 2.0, 2.0, 2.0, 2.0, 2.0 }
            };
            orb_moon_hard_150 = new double[6, 6] {
                { 2.0, 2.0, 2.0, 2.0, 2.0, 2.0 },
                { 2.0, 2.0, 2.0, 2.0, 2.0, 2.0 },
                { 2.0, 2.0, 2.0, 2.0, 2.0, 2.0 },
                { 2.0, 2.0, 2.0, 2.0, 2.0, 2.0 },
                { 2.0, 2.0, 2.0, 2.0, 2.0, 2.0 },
                { 2.0, 2.0, 2.0, 2.0, 2.0, 2.0 }
            };
            orb_other_hard_1st = new double[6, 6] {
                { 6.0, 6.0, 6.0, 6.0, 6.0, 6.0 },
                { 6.0, 6.0, 6.0, 6.0, 6.0, 6.0 },
                { 6.0, 6.0, 6.0, 6.0, 6.0, 6.0 },
                { 6.0, 6.0, 6.0, 6.0, 6.0, 6.0 },
                { 6.0, 6.0, 6.0, 6.0, 6.0, 6.0 },
                { 6.0, 6.0, 6.0, 6.0, 6.0, 6.0 }
            };
            orb_other_hard_2nd = new double[6, 6] {
                { 4.0, 4.0, 4.0, 4.0, 4.0, 4.0 },
                { 4.0, 4.0, 4.0, 4.0, 4.0, 4.0 },
                { 4.0, 4.0, 4.0, 4.0, 4.0, 4.0 },
                { 4.0, 4.0, 4.0, 4.0, 4.0, 4.0 },
                { 4.0, 4.0, 4.0, 4.0, 4.0, 4.0 },
                { 4.0, 4.0, 4.0, 4.0, 4.0, 4.0 }
            };
            orb_other_hard_150 = new double[6, 6] {
                { 1.0, 1.0, 1.0, 1.0, 1.0, 1.0 },
                { 1.0, 1.0, 1.0, 1.0, 1.0, 1.0 },
                { 1.0, 1.0, 1.0, 1.0, 1.0, 1.0 },
                { 1.0, 1.0, 1.0, 1.0, 1.0, 1.0 },
                { 1.0, 1.0, 1.0, 1.0, 1.0, 1.0 },
                { 1.0, 1.0, 1.0, 1.0, 1.0, 1.0 }
            };
            dispCircle = new bool[6] { true, false, false, false, false, false };

            dispPlanet = new List<Dictionary<int, bool>>();
            Dictionary<int, bool> d = new Dictionary<int, bool>();
            d.Add(CommonData.ZODIAC_SUN, true);
            d.Add(CommonData.ZODIAC_MOON, true);
            d.Add(CommonData.ZODIAC_MERCURY, true);
            d.Add(CommonData.ZODIAC_VENUS, true);
            d.Add(CommonData.ZODIAC_MARS, true);
            d.Add(CommonData.ZODIAC_JUPITER, true);
            d.Add(CommonData.ZODIAC_SATURN, true);
            d.Add(CommonData.ZODIAC_URANUS, true);
            d.Add(CommonData.ZODIAC_NEPTUNE, true);
            d.Add(CommonData.ZODIAC_PLUTO, true);
            d.Add(CommonData.ZODIAC_DH_TRUENODE, true);
            d.Add(CommonData.ZODIAC_ASC, true);
            d.Add(CommonData.ZODIAC_MC, true);
            d.Add(CommonData.ZODIAC_CHIRON, false);
            d.Add(CommonData.ZODIAC_EARTH, false);
            d.Add(CommonData.ZODIAC_LILITH, false);
            d.Add(CommonData.ZODIAC_CELES, false);
            d.Add(CommonData.ZODIAC_PARAS, false);
            d.Add(CommonData.ZODIAC_JUNO, false);
            d.Add(CommonData.ZODIAC_VESTA, false);
            d.Add(CommonData.ZODIAC_DT_OSCULATE_APOGEE, false);
            dispPlanet.Add(d);
            dispPlanet.Add(d);
            dispPlanet.Add(d);
            dispPlanet.Add(d);
            dispPlanet.Add(d);
            aspectConjunction = new bool[6, 6] {
                { true, true, true, true, true, true },
                { true, true, true, true, true, true },
                { true, true, true, true, true, true },
                { true, true, true, true, true, true },
                { true, true, true, true, true, true },
                { true, true, true, true, true, true }
            };
            aspectOpposition = new bool[6, 6] {
                { true, true, true, true, true, true },
                { true, true, true, true, true, true },
                { true, true, true, true, true, true },
                { true, true, true, true, true, true },
                { true, true, true, true, true, true },
                { true, true, true, true, true, true }
            };
            aspectTrine = new bool[6, 6] {
                { true, true, true, true, true, true },
                { true, true, true, true, true, true },
                { true, true, true, true, true, true },
                { true, true, true, true, true, true },
                { true, true, true, true, true, true },
                { true, true, true, true, true, true }
            };
            aspectSquare = new bool[6, 6] {
                { true, true, true, true, true, true },
                { true, true, true, true, true, true },
                { true, true, true, true, true, true },
                { true, true, true, true, true, true },
                { true, true, true, true, true, true },
                { true, true, true, true, true, true }
            };
            aspectSextile = new bool[6, 6] {
                { true, true, true, true, true, true },
                { true, true, true, true, true, true },
                { true, true, true, true, true, true },
                { true, true, true, true, true, true },
                { true, true, true, true, true, true },
                { true, true, true, true, true, true }
            };
            aspectInconjunct = new bool[6, 6] {
                { true, true, true, true, true, true },
                { true, true, true, true, true, true },
                { true, true, true, true, true, true },
                { true, true, true, true, true, true },
                { true, true, true, true, true, true },
                { true, true, true, true, true, true }
            };
            aspectSesquiquadrate = new bool[6, 6] {
                { true, true, true, true, true, true },
                { true, true, true, true, true, true },
                { true, true, true, true, true, true },
                { true, true, true, true, true, true },
                { true, true, true, true, true, true },
                { true, true, true, true, true, true }
            };
            dispAspect = new bool[6, 6] {
                { true, true, true, true, true, true },
                { true, true, true, true, true, true },
                { true, true, true, true, true, true },
                { true, true, true, true, true, true },
                { true, true, true, true, true, true },
                { true, true, true, true, true, true }
            };

        }
    }
}
