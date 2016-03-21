using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace microcosm.Config
{
    public enum ECentric
    {
        GEO_CENTRIC = 0,
        HELIO_CENTRIC = 1
    }
    public enum Esidereal
    {
        TROPICAL = 0,
        SIDEREAL = 1
    }
    public class ConfigData
    {
        // 天文データパス
        [XmlElement("ephepath")]
        public string ephepath;

        // GEO or HERIO
        [XmlElement("centric")]
        public ECentric centric { get; set; }

        // TROPICAL or SIDEREAL
        [XmlElement("sidereal")]
        public Esidereal sidereal { get; set; }

        // 現在地
        [XmlElement("defaultPlace")]
        public string defaultPlace { get; set; }

        // 緯度
        [XmlElement("lat")]
        public double lat { get; set; }

        // 経度
        [XmlElement("lng")]
        public double lng { get; set; }

        // プログレス計算方法
        [XmlElement("progression")]
        public int progression { get; set; }

        // ハウス
        [XmlElement("house")]
        public int houseCalc { get; set; }

        // SolarFireっぽく表示orAMATERUっぽく表示
        [XmlElement("dispPattern")]
        public int dispPattern { get; set; }

        public ConfigData()
        {
            centric = ECentric.GEO_CENTRIC;
            sidereal = Esidereal.TROPICAL;
        }

    }
}
