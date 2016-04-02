using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace microcosm.Config
{
    // ２次元配列がシリアライズできないのでコンバーターを用意
    public class SettingXml
    {
        [XmlElement("dispname")]
        public string dispname;

        // 第一種アスペクト、一重～六重円
        [XmlElement("orb_sun_soft_1st_0")]
        public double[] orb_sun_soft_1st_0;
        [XmlElement("orb_sun_soft_1st_1")]
        public double[] orb_sun_soft_1st_1;
        [XmlElement("orb_sun_soft_1st_2")]
        public double[] orb_sun_soft_1st_2;
        [XmlElement("orb_sun_soft_1st_3")]
        public double[] orb_sun_soft_1st_3;
        [XmlElement("orb_sun_soft_1st_4")]
        public double[] orb_sun_soft_1st_4;
        [XmlElement("orb_sun_soft_1st_5")]
        public double[] orb_sun_soft_1st_5;


        [XmlElement("orb_sun_hard_1st_0")]
        public double[] orb_sun_hard_1st_0;
        [XmlElement("orb_sun_hard_1st_1")]
        public double[] orb_sun_hard_1st_1;
        [XmlElement("orb_sun_hard_1st_2")]
        public double[] orb_sun_hard_1st_2;
        [XmlElement("orb_sun_hard_1st_3")]
        public double[] orb_sun_hard_1st_3;
        [XmlElement("orb_sun_hard_1st_4")]
        public double[] orb_sun_hard_1st_4;
        [XmlElement("orb_sun_hard_1st_5")]
        public double[] orb_sun_hard_1st_5;


        [XmlElement("orb_moon_soft_1st_0")]
        public double[] orb_moon_soft_1st_0;
        [XmlElement("orb_moon_soft_1st_1")]
        public double[] orb_moon_soft_1st_1;
        [XmlElement("orb_moon_soft_1st_2")]
        public double[] orb_moon_soft_1st_2;
        [XmlElement("orb_moon_soft_1st_3")]
        public double[] orb_moon_soft_1st_3;
        [XmlElement("orb_moon_soft_1st_4")]
        public double[] orb_moon_soft_1st_4;
        [XmlElement("orb_moon_soft_1st_5")]
        public double[] orb_moon_soft_1st_5;


        [XmlElement("orb_moon_hard_1st_0")]
        public double[] orb_moon_hard_1st_0;
        [XmlElement("orb_moon_hard_1st_1")]
        public double[] orb_moon_hard_1st_1;
        [XmlElement("orb_moon_hard_1st_2")]
        public double[] orb_moon_hard_1st_2;
        [XmlElement("orb_moon_hard_1st_3")]
        public double[] orb_moon_hard_1st_3;
        [XmlElement("orb_moon_hard_1st_4")]
        public double[] orb_moon_hard_1st_4;
        [XmlElement("orb_moon_hard_1st_5")]
        public double[] orb_moon_hard_1st_5;


        [XmlElement("orb_other_soft_1st_0")]
        public double[] orb_other_soft_1st_0;
        [XmlElement("orb_other_soft_1st_1")]
        public double[] orb_other_soft_1st_1;
        [XmlElement("orb_other_soft_1st_2")]
        public double[] orb_other_soft_1st_2;
        [XmlElement("orb_other_soft_1st_3")]
        public double[] orb_other_soft_1st_3;
        [XmlElement("orb_other_soft_1st_4")]
        public double[] orb_other_soft_1st_4;
        [XmlElement("orb_other_soft_1st_5")]
        public double[] orb_other_soft_1st_5;


        [XmlElement("orb_other_hard_1st_0")]
        public double[] orb_other_hard_1st_0;
        [XmlElement("orb_other_hard_1st_1")]
        public double[] orb_other_hard_1st_1;
        [XmlElement("orb_other_hard_1st_2")]
        public double[] orb_other_hard_1st_2;
        [XmlElement("orb_other_hard_1st_3")]
        public double[] orb_other_hard_1st_3;
        [XmlElement("orb_other_hard_1st_4")]
        public double[] orb_other_hard_1st_4;
        [XmlElement("orb_other_hard_1st_5")]
        public double[] orb_other_hard_1st_5;


        // 第二種アスペクト、一重～五重円
        [XmlElement("orb_sun_soft_2nd_0")]
        public double[] orb_sun_soft_2nd_0;
        [XmlElement("orb_sun_soft_2nd_1")]
        public double[] orb_sun_soft_2nd_1;
        [XmlElement("orb_sun_soft_2nd_2")]
        public double[] orb_sun_soft_2nd_2;
        [XmlElement("orb_sun_soft_2nd_3")]
        public double[] orb_sun_soft_2nd_3;
        [XmlElement("orb_sun_soft_2nd_4")]
        public double[] orb_sun_soft_2nd_4;
        [XmlElement("orb_sun_soft_2nd_5")]
        public double[] orb_sun_soft_2nd_5;


        [XmlElement("orb_sun_hard_2nd_0")]
        public double[] orb_sun_hard_2nd_0;
        [XmlElement("orb_sun_hard_2nd_1")]
        public double[] orb_sun_hard_2nd_1;
        [XmlElement("orb_sun_hard_2nd_2")]
        public double[] orb_sun_hard_2nd_2;
        [XmlElement("orb_sun_hard_2nd_3")]
        public double[] orb_sun_hard_2nd_3;
        [XmlElement("orb_sun_hard_2nd_4")]
        public double[] orb_sun_hard_2nd_4;
        [XmlElement("orb_sun_hard_2nd_5")]
        public double[] orb_sun_hard_2nd_5;


        [XmlElement("orb_moon_soft_2nd_0")]
        public double[] orb_moon_soft_2nd_0;
        [XmlElement("orb_moon_soft_2nd_1")]
        public double[] orb_moon_soft_2nd_1;
        [XmlElement("orb_moon_soft_2nd_2")]
        public double[] orb_moon_soft_2nd_2;
        [XmlElement("orb_moon_soft_2nd_3")]
        public double[] orb_moon_soft_2nd_3;
        [XmlElement("orb_moon_soft_2nd_4")]
        public double[] orb_moon_soft_2nd_4;
        [XmlElement("orb_moon_soft_2nd_5")]
        public double[] orb_moon_soft_2nd_5;


        [XmlElement("orb_moon_hard_2nd_0")]
        public double[] orb_moon_hard_2nd_0;
        [XmlElement("orb_moon_hard_2nd_1")]
        public double[] orb_moon_hard_2nd_1;
        [XmlElement("orb_moon_hard_2nd_2")]
        public double[] orb_moon_hard_2nd_2;
        [XmlElement("orb_moon_hard_2nd_3")]
        public double[] orb_moon_hard_2nd_3;
        [XmlElement("orb_moon_hard_2nd_4")]
        public double[] orb_moon_hard_2nd_4;
        [XmlElement("orb_moon_hard_2nd_5")]
        public double[] orb_moon_hard_2nd_5;


        [XmlElement("orb_other_soft_2nd_0")]
        public double[] orb_other_soft_2nd_0;
        [XmlElement("orb_other_soft_2nd_1")]
        public double[] orb_other_soft_2nd_1;
        [XmlElement("orb_other_soft_2nd_2")]
        public double[] orb_other_soft_2nd_2;
        [XmlElement("orb_other_soft_2nd_3")]
        public double[] orb_other_soft_2nd_3;
        [XmlElement("orb_other_soft_2nd_4")]
        public double[] orb_other_soft_2nd_4;
        [XmlElement("orb_other_soft_2nd_5")]
        public double[] orb_other_soft_2nd_5;


        [XmlElement("orb_other_hard_2nd_0")]
        public double[] orb_other_hard_2nd_0;
        [XmlElement("orb_other_hard_2nd_1")]
        public double[] orb_other_hard_2nd_1;
        [XmlElement("orb_other_hard_2nd_2")]
        public double[] orb_other_hard_2nd_2;
        [XmlElement("orb_other_hard_2nd_3")]
        public double[] orb_other_hard_2nd_3;
        [XmlElement("orb_other_hard_2nd_4")]
        public double[] orb_other_hard_2nd_4;
        [XmlElement("orb_other_hard_2nd_5")]
        public double[] orb_other_hard_2nd_5;


        [XmlElement("orb_sun_soft_150_0")]
        public double[] orb_sun_soft_150_0;
        [XmlElement("orb_sun_soft_150_1")]
        public double[] orb_sun_soft_150_1;
        [XmlElement("orb_sun_soft_150_2")]
        public double[] orb_sun_soft_150_2;
        [XmlElement("orb_sun_soft_150_3")]
        public double[] orb_sun_soft_150_3;
        [XmlElement("orb_sun_soft_150_4")]
        public double[] orb_sun_soft_150_4;
        [XmlElement("orb_sun_soft_150_5")]
        public double[] orb_sun_soft_150_5;


        [XmlElement("orb_sun_hard_150_0")]
        public double[] orb_sun_hard_150_0;
        [XmlElement("orb_sun_hard_150_1")]
        public double[] orb_sun_hard_150_1;
        [XmlElement("orb_sun_hard_150_2")]
        public double[] orb_sun_hard_150_2;
        [XmlElement("orb_sun_hard_150_3")]
        public double[] orb_sun_hard_150_3;
        [XmlElement("orb_sun_hard_150_4")]
        public double[] orb_sun_hard_150_4;
        [XmlElement("orb_sun_hard_150_5")]
        public double[] orb_sun_hard_150_5;


        [XmlElement("orb_moon_soft_150_0")]
        public double[] orb_moon_soft_150_0;
        [XmlElement("orb_moon_soft_150_1")]
        public double[] orb_moon_soft_150_1;
        [XmlElement("orb_moon_soft_150_2")]
        public double[] orb_moon_soft_150_2;
        [XmlElement("orb_moon_soft_150_3")]
        public double[] orb_moon_soft_150_3;
        [XmlElement("orb_moon_soft_150_4")]
        public double[] orb_moon_soft_150_4;
        [XmlElement("orb_moon_soft_150_5")]
        public double[] orb_moon_soft_150_5;


        [XmlElement("orb_moon_hard_150_0")]
        public double[] orb_moon_hard_150_0;
        [XmlElement("orb_moon_hard_150_1")]
        public double[] orb_moon_hard_150_1;
        [XmlElement("orb_moon_hard_150_2")]
        public double[] orb_moon_hard_150_2;
        [XmlElement("orb_moon_hard_150_3")]
        public double[] orb_moon_hard_150_3;
        [XmlElement("orb_moon_hard_150_4")]
        public double[] orb_moon_hard_150_4;
        [XmlElement("orb_moon_hard_150_5")]
        public double[] orb_moon_hard_150_5;


        [XmlElement("orb_other_soft_150_0")]
        public double[] orb_other_soft_150_0;
        [XmlElement("orb_other_soft_150_1")]
        public double[] orb_other_soft_150_1;
        [XmlElement("orb_other_soft_150_2")]
        public double[] orb_other_soft_150_2;
        [XmlElement("orb_other_soft_150_3")]
        public double[] orb_other_soft_150_3;
        [XmlElement("orb_other_soft_150_4")]
        public double[] orb_other_soft_150_4;
        [XmlElement("orb_other_soft_150_5")]
        public double[] orb_other_soft_150_5;


        [XmlElement("orb_other_hard_150_0")]
        public double[] orb_other_hard_150_0;
        [XmlElement("orb_other_hard_150_1")]
        public double[] orb_other_hard_150_1;
        [XmlElement("orb_other_hard_150_2")]
        public double[] orb_other_hard_150_2;
        [XmlElement("orb_other_hard_150_3")]
        public double[] orb_other_hard_150_3;
        [XmlElement("orb_other_hard_150_4")]
        public double[] orb_other_hard_150_4;
        [XmlElement("orb_other_hard_150_5")]
        public double[] orb_other_hard_150_5;

        [XmlElement("dispCircle")]
        public bool[] dispCircle;

        [XmlElement("dispPlanet_sun")]
        public bool dispPlanet_sun;
        [XmlElement("dispPlanet_moon")]
        public bool dispPlanet_moon;
        [XmlElement("dispPlanet_mercury")]
        public bool dispPlanet_mercury;
        [XmlElement("dispPlanet_venus")]
        public bool dispPlanet_venus;
        [XmlElement("dispPlanet_mars")]
        public bool dispPlanet_mars;
        [XmlElement("dispPlanet_jupiter")]
        public bool dispPlanet_jupiter;
        [XmlElement("dispPlanet_saturn")]
        public bool dispPlanet_saturn;
        [XmlElement("dispPlanet_uranus")]
        public bool dispPlanet_uranus;
        [XmlElement("dispPlanet_neptune")]
        public bool dispPlanet_neptune;
        [XmlElement("dispPlanet_pluto")]
        public bool dispPlanet_pluto;
        [XmlElement("dispPlanet_truenode")]
        public bool dispPlanet_truenode;
        [XmlElement("dispPlanet_earth")]
        public bool dispPlanet_earth;

    }
}
