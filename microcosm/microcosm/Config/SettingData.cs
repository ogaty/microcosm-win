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
        [XmlElement("dispname")]
        public string dispname;

        [XmlElement("orb_sun_soft_1st")]
        public double[,] orb_sun_soft_1st;

        [XmlElement("orb_sun_hard_1st")]
        public double[,] orb_sun_hard_1st;

        [XmlElement("orb_moon_soft_1st")]
        public double[,] orb_moon_soft_1st;

        [XmlElement("orb_moon_hard_1st")]
        public double[,] orb_moon_hard_1st;

        [XmlElement("orb_other_soft_1st")]
        public double[,] orb_other_soft_1st;

        [XmlElement("orb_other_hard_1st")]
        public double[,] orb_other_hard_1st;

        [XmlElement("orb_other_soft_2nd")]
        public double[,] orb_sun_soft_2nd;

        [XmlElement("orb_other_hard_2nd")]
        public double[,] orb_sun_hard_2nd;

        [XmlElement("orb_moon_soft_2nd")]
        public double[,] orb_moon_soft_2nd;

        [XmlElement("orb_moon_hard_2nd")]
        public double[,] orb_moon_hard_2nd;

        [XmlElement("orb_other_soft_2nd")]
        public double[,] orb_other_soft_2nd;

        [XmlElement("orb_other_hard_2nd")]
        public double[,] orb_other_hard_2nd;

        [XmlElement("orb_sun_soft_150")]
        public double[,] orb_sun_soft_150;

        [XmlElement("orb_sun_hard_150")]
        public double[,] orb_sun_hard_150;

        [XmlElement("orb_moon_soft_150")]
        public double[,] orb_moon_soft_150;

        [XmlElement("orb_moon_hard_150")]
        public double[,] orb_moon_hard_150;

        [XmlElement("orb_other_soft_150")]
        public double[,] orb_other_soft_150;

        [XmlElement("orb_other_hard_150")]
        public double[,] orb_other_hard_150;

        [XmlElement("dispCircle")]
        public bool[] dispCircle = new bool[] {
            true, false, false, false, false, false
        };
        [XmlElement("dispPlanet")]
        public List<Dictionary<int, bool>> dispPlanet;

        [XmlElement("dispAspect")]
        public bool[,] dispAspect;

        public SettingData()
        {
            init();
        }
        public SettingData(string name)
        {
            init(name);
        }
        public void init(string name = "表示設定")
        {
            this.dispname = name;
            orb_sun_soft_1st = new double[6, 6] {
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 }
            };
            orb_sun_soft_2nd = new double[6, 6] {
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 }
            };
            orb_sun_soft_150 = new double[6, 6] {
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 }
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
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 }
            };
            orb_moon_soft_150 = new double[6, 6] {
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 }
            };
            orb_other_soft_1st = new double[6, 6] {
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 }
            };
            orb_other_soft_2nd = new double[6, 6] {
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 }
            };
            orb_other_soft_150 = new double[6, 6] {
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 }
            };
            orb_sun_hard_1st = new double[6, 6] {
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 }
            };
            orb_sun_hard_2nd = new double[6, 6] {
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 }
            };
            orb_sun_hard_150 = new double[6, 6] {
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 }
            };
            orb_moon_hard_1st = new double[6, 6] {
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 }
            };
            orb_moon_hard_2nd = new double[6, 6] {
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 }
            };
            orb_moon_hard_150 = new double[6, 6] {
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 }
            };
            orb_other_hard_1st = new double[6, 6] {
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 }
            };
            orb_other_hard_2nd = new double[6, 6] {
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 }
            };
            orb_other_hard_150 = new double[6, 6] {
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 },
                { 8.0, 8.0, 8.0, 8.0, 8.0, 8.0 }
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
            dispPlanet.Add(d);
            dispPlanet.Add(d);
            dispPlanet.Add(d);
            dispPlanet.Add(d);
            dispPlanet.Add(d);
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
