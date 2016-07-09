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

        #region orb 1st
        // 第一種アスペクト、一重～六重円
        [XmlElement("orb_sun_soft_1st_0")]
        public double orb_sun_soft_1st_0;
        [XmlElement("orb_sun_soft_1st_1")]
        public double orb_sun_soft_1st_1;
        [XmlElement("orb_sun_soft_1st_2")]
        public double orb_sun_soft_1st_2;
        [XmlElement("orb_sun_soft_1st_3")]
        public double orb_sun_soft_1st_3;
        [XmlElement("orb_sun_soft_1st_4")]
        public double orb_sun_soft_1st_4;
        [XmlElement("orb_sun_soft_1st_5")]
        public double orb_sun_soft_1st_5;


        [XmlElement("orb_sun_hard_1st_0")]
        public double orb_sun_hard_1st_0;
        [XmlElement("orb_sun_hard_1st_1")]
        public double orb_sun_hard_1st_1;
        [XmlElement("orb_sun_hard_1st_2")]
        public double orb_sun_hard_1st_2;
        [XmlElement("orb_sun_hard_1st_3")]
        public double orb_sun_hard_1st_3;
        [XmlElement("orb_sun_hard_1st_4")]
        public double orb_sun_hard_1st_4;
        [XmlElement("orb_sun_hard_1st_5")]
        public double orb_sun_hard_1st_5;


        [XmlElement("orb_moon_soft_1st_0")]
        public double orb_moon_soft_1st_0;
        [XmlElement("orb_moon_soft_1st_1")]
        public double orb_moon_soft_1st_1;
        [XmlElement("orb_moon_soft_1st_2")]
        public double orb_moon_soft_1st_2;
        [XmlElement("orb_moon_soft_1st_3")]
        public double orb_moon_soft_1st_3;
        [XmlElement("orb_moon_soft_1st_4")]
        public double orb_moon_soft_1st_4;
        [XmlElement("orb_moon_soft_1st_5")]
        public double orb_moon_soft_1st_5;


        [XmlElement("orb_moon_hard_1st_0")]
        public double orb_moon_hard_1st_0;
        [XmlElement("orb_moon_hard_1st_1")]
        public double orb_moon_hard_1st_1;
        [XmlElement("orb_moon_hard_1st_2")]
        public double orb_moon_hard_1st_2;
        [XmlElement("orb_moon_hard_1st_3")]
        public double orb_moon_hard_1st_3;
        [XmlElement("orb_moon_hard_1st_4")]
        public double orb_moon_hard_1st_4;
        [XmlElement("orb_moon_hard_1st_5")]
        public double orb_moon_hard_1st_5;


        [XmlElement("orb_other_soft_1st_0")]
        public double orb_other_soft_1st_0;
        [XmlElement("orb_other_soft_1st_1")]
        public double orb_other_soft_1st_1;
        [XmlElement("orb_other_soft_1st_2")]
        public double orb_other_soft_1st_2;
        [XmlElement("orb_other_soft_1st_3")]
        public double orb_other_soft_1st_3;
        [XmlElement("orb_other_soft_1st_4")]
        public double orb_other_soft_1st_4;
        [XmlElement("orb_other_soft_1st_5")]
        public double orb_other_soft_1st_5;


        [XmlElement("orb_other_hard_1st_0")]
        public double orb_other_hard_1st_0;
        [XmlElement("orb_other_hard_1st_1")]
        public double orb_other_hard_1st_1;
        [XmlElement("orb_other_hard_1st_2")]
        public double orb_other_hard_1st_2;
        [XmlElement("orb_other_hard_1st_3")]
        public double orb_other_hard_1st_3;
        [XmlElement("orb_other_hard_1st_4")]
        public double orb_other_hard_1st_4;
        [XmlElement("orb_other_hard_1st_5")]
        public double orb_other_hard_1st_5;
        #endregion

        // 第二種アスペクト、一重～五重円
        #region orb 2nd
        [XmlElement("orb_sun_soft_2nd_0")]
        public double orb_sun_soft_2nd_0;
        [XmlElement("orb_sun_soft_2nd_1")]
        public double orb_sun_soft_2nd_1;
        [XmlElement("orb_sun_soft_2nd_2")]
        public double orb_sun_soft_2nd_2;
        [XmlElement("orb_sun_soft_2nd_3")]
        public double orb_sun_soft_2nd_3;
        [XmlElement("orb_sun_soft_2nd_4")]
        public double orb_sun_soft_2nd_4;
        [XmlElement("orb_sun_soft_2nd_5")]
        public double orb_sun_soft_2nd_5;


        [XmlElement("orb_sun_hard_2nd_0")]
        public double orb_sun_hard_2nd_0;
        [XmlElement("orb_sun_hard_2nd_1")]
        public double orb_sun_hard_2nd_1;
        [XmlElement("orb_sun_hard_2nd_2")]
        public double orb_sun_hard_2nd_2;
        [XmlElement("orb_sun_hard_2nd_3")]
        public double orb_sun_hard_2nd_3;
        [XmlElement("orb_sun_hard_2nd_4")]
        public double orb_sun_hard_2nd_4;
        [XmlElement("orb_sun_hard_2nd_5")]
        public double orb_sun_hard_2nd_5;


        [XmlElement("orb_moon_soft_2nd_0")]
        public double orb_moon_soft_2nd_0;
        [XmlElement("orb_moon_soft_2nd_1")]
        public double orb_moon_soft_2nd_1;
        [XmlElement("orb_moon_soft_2nd_2")]
        public double orb_moon_soft_2nd_2;
        [XmlElement("orb_moon_soft_2nd_3")]
        public double orb_moon_soft_2nd_3;
        [XmlElement("orb_moon_soft_2nd_4")]
        public double orb_moon_soft_2nd_4;
        [XmlElement("orb_moon_soft_2nd_5")]
        public double orb_moon_soft_2nd_5;


        [XmlElement("orb_moon_hard_2nd_0")]
        public double orb_moon_hard_2nd_0;
        [XmlElement("orb_moon_hard_2nd_1")]
        public double orb_moon_hard_2nd_1;
        [XmlElement("orb_moon_hard_2nd_2")]
        public double orb_moon_hard_2nd_2;
        [XmlElement("orb_moon_hard_2nd_3")]
        public double orb_moon_hard_2nd_3;
        [XmlElement("orb_moon_hard_2nd_4")]
        public double orb_moon_hard_2nd_4;
        [XmlElement("orb_moon_hard_2nd_5")]
        public double orb_moon_hard_2nd_5;


        [XmlElement("orb_other_soft_2nd_0")]
        public double orb_other_soft_2nd_0;
        [XmlElement("orb_other_soft_2nd_1")]
        public double orb_other_soft_2nd_1;
        [XmlElement("orb_other_soft_2nd_2")]
        public double orb_other_soft_2nd_2;
        [XmlElement("orb_other_soft_2nd_3")]
        public double orb_other_soft_2nd_3;
        [XmlElement("orb_other_soft_2nd_4")]
        public double orb_other_soft_2nd_4;
        [XmlElement("orb_other_soft_2nd_5")]
        public double orb_other_soft_2nd_5;


        [XmlElement("orb_other_hard_2nd_0")]
        public double orb_other_hard_2nd_0;
        [XmlElement("orb_other_hard_2nd_1")]
        public double orb_other_hard_2nd_1;
        [XmlElement("orb_other_hard_2nd_2")]
        public double orb_other_hard_2nd_2;
        [XmlElement("orb_other_hard_2nd_3")]
        public double orb_other_hard_2nd_3;
        [XmlElement("orb_other_hard_2nd_4")]
        public double orb_other_hard_2nd_4;
        [XmlElement("orb_other_hard_2nd_5")]
        public double orb_other_hard_2nd_5;
        #endregion

        // 135/150
        #region orb 150
        [XmlElement("orb_sun_soft_150_0")]
        public double orb_sun_soft_150_0;
        [XmlElement("orb_sun_soft_150_1")]
        public double orb_sun_soft_150_1;
        [XmlElement("orb_sun_soft_150_2")]
        public double orb_sun_soft_150_2;
        [XmlElement("orb_sun_soft_150_3")]
        public double orb_sun_soft_150_3;
        [XmlElement("orb_sun_soft_150_4")]
        public double orb_sun_soft_150_4;
        [XmlElement("orb_sun_soft_150_5")]
        public double orb_sun_soft_150_5;


        [XmlElement("orb_sun_hard_150_0")]
        public double orb_sun_hard_150_0;
        [XmlElement("orb_sun_hard_150_1")]
        public double orb_sun_hard_150_1;
        [XmlElement("orb_sun_hard_150_2")]
        public double orb_sun_hard_150_2;
        [XmlElement("orb_sun_hard_150_3")]
        public double orb_sun_hard_150_3;
        [XmlElement("orb_sun_hard_150_4")]
        public double orb_sun_hard_150_4;
        [XmlElement("orb_sun_hard_150_5")]
        public double orb_sun_hard_150_5;


        [XmlElement("orb_moon_soft_150_0")]
        public double orb_moon_soft_150_0;
        [XmlElement("orb_moon_soft_150_1")]
        public double orb_moon_soft_150_1;
        [XmlElement("orb_moon_soft_150_2")]
        public double orb_moon_soft_150_2;
        [XmlElement("orb_moon_soft_150_3")]
        public double orb_moon_soft_150_3;
        [XmlElement("orb_moon_soft_150_4")]
        public double orb_moon_soft_150_4;
        [XmlElement("orb_moon_soft_150_5")]
        public double orb_moon_soft_150_5;


        [XmlElement("orb_moon_hard_150_0")]
        public double orb_moon_hard_150_0;
        [XmlElement("orb_moon_hard_150_1")]
        public double orb_moon_hard_150_1;
        [XmlElement("orb_moon_hard_150_2")]
        public double orb_moon_hard_150_2;
        [XmlElement("orb_moon_hard_150_3")]
        public double orb_moon_hard_150_3;
        [XmlElement("orb_moon_hard_150_4")]
        public double orb_moon_hard_150_4;
        [XmlElement("orb_moon_hard_150_5")]
        public double orb_moon_hard_150_5;


        [XmlElement("orb_other_soft_150_0")]
        public double orb_other_soft_150_0;
        [XmlElement("orb_other_soft_150_1")]
        public double orb_other_soft_150_1;
        [XmlElement("orb_other_soft_150_2")]
        public double orb_other_soft_150_2;
        [XmlElement("orb_other_soft_150_3")]
        public double orb_other_soft_150_3;
        [XmlElement("orb_other_soft_150_4")]
        public double orb_other_soft_150_4;
        [XmlElement("orb_other_soft_150_5")]
        public double orb_other_soft_150_5;


        [XmlElement("orb_other_hard_150_0")]
        public double orb_other_hard_150_0;
        [XmlElement("orb_other_hard_150_1")]
        public double orb_other_hard_150_1;
        [XmlElement("orb_other_hard_150_2")]
        public double orb_other_hard_150_2;
        [XmlElement("orb_other_hard_150_3")]
        public double orb_other_hard_150_3;
        [XmlElement("orb_other_hard_150_4")]
        public double orb_other_hard_150_4;
        [XmlElement("orb_other_hard_150_5")]
        public double orb_other_hard_150_5;
        #endregion

        [XmlElement("dispCircle")]
        public bool[] dispCircle;

        #region dispPlanet
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
        #endregion

        [XmlElement("dispAspect")]
        public bool[] dispAspect;

        #region aspect Planet
        [XmlElement("aspectSun11")]
        public bool aspectSun11;
        [XmlElement("aspectMoon11")]
        public bool aspectMoon11;
        [XmlElement("aspectMercury11")]
        public bool aspectMercury11;
        [XmlElement("aspectVenus11")]
        public bool aspectVenus11;
        [XmlElement("aspectMars11")]
        public bool aspectMars11;
        [XmlElement("aspectJupiter11")]
        public bool aspectJupiter11;
        [XmlElement("aspectSaturn11")]
        public bool aspectSaturn11;
        [XmlElement("aspectUranus11")]
        public bool aspectUranus11;
        [XmlElement("aspectNeptune11")]
        public bool aspectNeptune11;
        [XmlElement("aspectPluto11")]
        public bool aspectPluto11;
        [XmlElement("aspectDh11")]
        public bool aspectDh11;
        [XmlElement("aspectChiron11")]
        public bool aspectChiron11;
        [XmlElement("aspectAsc11")]
        public bool aspectAsc11;
        [XmlElement("aspectMc11")]
        public bool aspectMc11;
        [XmlElement("aspectSun22")]
        public bool aspectSun22;
        [XmlElement("aspectMoon22")]
        public bool aspectMoon22;
        [XmlElement("aspectMercury22")]
        public bool aspectMercury22;
        [XmlElement("aspectVenus22")]
        public bool aspectVenus22;
        [XmlElement("aspectMars22")]
        public bool aspectMars22;
        [XmlElement("aspectJupiter22")]
        public bool aspectJupiter22;
        [XmlElement("aspectSaturn22")]
        public bool aspectSaturn22;
        [XmlElement("aspectUranus22")]
        public bool aspectUranus22;
        [XmlElement("aspectNeptune22")]
        public bool aspectNeptune22;
        [XmlElement("aspectPluto22")]
        public bool aspectPluto22;
        [XmlElement("aspectDh22")]
        public bool aspectDh22;
        [XmlElement("aspectChiron22")]
        public bool aspectChiron22;
        [XmlElement("aspectAsc22")]
        public bool aspectAsc22;
        [XmlElement("aspectMc22")]
        public bool aspectMc22;
        [XmlElement("aspectSun33")]
        public bool aspectSun33;
        [XmlElement("aspectMoon33")]
        public bool aspectMoon33;
        [XmlElement("aspectMercury33")]
        public bool aspectMercury33;
        [XmlElement("aspectVenus33")]
        public bool aspectVenus33;
        [XmlElement("aspectMars33")]
        public bool aspectMars33;
        [XmlElement("aspectJupiter33")]
        public bool aspectJupiter33;
        [XmlElement("aspectSaturn33")]
        public bool aspectSaturn33;
        [XmlElement("aspectUranus33")]
        public bool aspectUranus33;
        [XmlElement("aspectNeptune33")]
        public bool aspectNeptune33;
        [XmlElement("aspectPluto33")]
        public bool aspectPluto33;
        [XmlElement("aspectDh33")]
        public bool aspectDh33;
        [XmlElement("aspectChiron33")]
        public bool aspectChiron33;
        [XmlElement("aspectAsc33")]
        public bool aspectAsc33;
        [XmlElement("aspectMc33")]
        public bool aspectMc33;
        [XmlElement("aspectSun12")]
        public bool aspectSun12;
        [XmlElement("aspectMoon12")]
        public bool aspectMoon12;
        [XmlElement("aspectMercury12")]
        public bool aspectMercury12;
        [XmlElement("aspectVenus12")]
        public bool aspectVenus12;
        [XmlElement("aspectMars12")]
        public bool aspectMars12;
        [XmlElement("aspectJupiter12")]
        public bool aspectJupiter12;
        [XmlElement("aspectSaturn12")]
        public bool aspectSaturn12;
        [XmlElement("aspectUranus12")]
        public bool aspectUranus12;
        [XmlElement("aspectNeptune12")]
        public bool aspectNeptune12;
        [XmlElement("aspectPluto12")]
        public bool aspectPluto12;
        [XmlElement("aspectDh12")]
        public bool aspectDh12;
        [XmlElement("aspectChiron12")]
        public bool aspectChiron12;
        [XmlElement("aspectAsc12")]
        public bool aspectAsc12;
        [XmlElement("aspectMc12")]
        public bool aspectMc12;
        [XmlElement("aspectSun13")]
        public bool aspectSun13;
        [XmlElement("aspectMoon13")]
        public bool aspectMoon13;
        [XmlElement("aspectMercury13")]
        public bool aspectMercury13;
        [XmlElement("aspectVenus13")]
        public bool aspectVenus13;
        [XmlElement("aspectMars13")]
        public bool aspectMars13;
        [XmlElement("aspectJupiter13")]
        public bool aspectJupiter13;
        [XmlElement("aspectSaturn13")]
        public bool aspectSaturn13;
        [XmlElement("aspectUranus13")]
        public bool aspectUranus13;
        [XmlElement("aspectNeptune13")]
        public bool aspectNeptune13;
        [XmlElement("aspectPluto13")]
        public bool aspectPluto13;
        [XmlElement("aspectDh13")]
        public bool aspectDh13;
        [XmlElement("aspectChiron13")]
        public bool aspectChiron13;
        [XmlElement("aspectAsc13")]
        public bool aspectAsc13;
        [XmlElement("aspectMc13")]
        public bool aspectMc13;
        [XmlElement("aspectSun23")]
        public bool aspectSun23;
        [XmlElement("aspectMoon23")]
        public bool aspectMoon23;
        [XmlElement("aspectMercury23")]
        public bool aspectMercury23;
        [XmlElement("aspectVenus23")]
        public bool aspectVenus23;
        [XmlElement("aspectMars23")]
        public bool aspectMars23;
        [XmlElement("aspectJupiter23")]
        public bool aspectJupiter23;
        [XmlElement("aspectSaturn23")]
        public bool aspectSaturn23;
        [XmlElement("aspectUranus23")]
        public bool aspectUranus23;
        [XmlElement("aspectNeptune23")]
        public bool aspectNeptune23;
        [XmlElement("aspectPluto23")]
        public bool aspectPluto23;
        [XmlElement("aspectDh23")]
        public bool aspectDh23;
        [XmlElement("aspectChiron23")]
        public bool aspectChiron23;
        [XmlElement("aspectAsc23")]
        public bool aspectAsc23;
        [XmlElement("aspectMc23")]
        public bool aspectMc23;
        #endregion

        #region aspect Aspect
        [XmlElement("aspectConjunction11")]
        public bool aspectConjunction11;
        [XmlElement("aspectOpposition11")]
        public bool aspectOpposition11;
        [XmlElement("aspectTrine11")]
        public bool aspectTrine11;
        [XmlElement("aspectSquare11")]
        public bool aspectSquare11;
        [XmlElement("aspectSextile11")]
        public bool aspectSextile11;
        [XmlElement("aspectInconjunct11")]
        public bool aspectInconjunct11;
        [XmlElement("aspectSesquiquadrate11")]
        public bool aspectSesquiquadrate11;
        [XmlElement("aspectConjunction22")]
        public bool aspectConjunction22;
        [XmlElement("aspectOpposition22")]
        public bool aspectOpposition22;
        [XmlElement("aspectTrine22")]
        public bool aspectTrine22;
        [XmlElement("aspectSquare22")]
        public bool aspectSquare22;
        [XmlElement("aspectSextile22")]
        public bool aspectSextile22;
        [XmlElement("aspectInconjunct22")]
        public bool aspectInconjunct22;
        [XmlElement("aspectSesquiquadrate22")]
        public bool aspectSesquiquadrate22;
        [XmlElement("aspectConjunction33")]
        public bool aspectConjunction33;
        [XmlElement("aspectOpposition33")]
        public bool aspectOpposition33;
        [XmlElement("aspectTrine33")]
        public bool aspectTrine33;
        [XmlElement("aspectSquare33")]
        public bool aspectSquare33;
        [XmlElement("aspectSextile33")]
        public bool aspectSextile33;
        [XmlElement("aspectInconjunct33")]
        public bool aspectInconjunct33;
        [XmlElement("aspectSesquiquadrate33")]
        public bool aspectSesquiquadrate33;
        [XmlElement("aspectConjunction12")]
        public bool aspectConjunction12;
        [XmlElement("aspectOpposition12")]
        public bool aspectOpposition12;
        [XmlElement("aspectTrine12")]
        public bool aspectTrine12;
        [XmlElement("aspectSquare12")]
        public bool aspectSquare12;
        [XmlElement("aspectSextile12")]
        public bool aspectSextile12;
        [XmlElement("aspectInconjunct12")]
        public bool aspectInconjunct12;
        [XmlElement("aspectSesquiquadrate12")]
        public bool aspectSesquiquadrate12;
        [XmlElement("aspectConjunction13")]
        public bool aspectConjunction13;
        [XmlElement("aspectOpposition13")]
        public bool aspectOpposition13;
        [XmlElement("aspectTrine13")]
        public bool aspectTrine13;
        [XmlElement("aspectSquare13")]
        public bool aspectSquare13;
        [XmlElement("aspectSextile13")]
        public bool aspectSextile13;
        [XmlElement("aspectInconjunct13")]
        public bool aspectInconjunct13;
        [XmlElement("aspectSesquiquadrate13")]
        public bool aspectSesquiquadrate13;
        [XmlElement("aspectConjunction23")]
        public bool aspectConjunction23;
        [XmlElement("aspectOpposition23")]
        public bool aspectOpposition23;
        [XmlElement("aspectTrine23")]
        public bool aspectTrine23;
        [XmlElement("aspectSquare23")]
        public bool aspectSquare23;
        [XmlElement("aspectSextile23")]
        public bool aspectSextile23;
        [XmlElement("aspectInconjunct23")]
        public bool aspectInconjunct23;
        [XmlElement("aspectSesquiquadrate23")]
        public bool aspectSesquiquadrate23;
        #endregion

    }
}
