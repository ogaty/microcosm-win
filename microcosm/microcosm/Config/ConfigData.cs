using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace microcosm.Config
{
    // 共通設定
    // クラスは一つだけ

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
    public enum EProgression
    {
        PRIMARY = 0,
        SECONDARY = 1,
        CPS = 2
    }
    public enum EHouseCalc
    {
        PLACIDUS = 0,
        KOCH  = 1,
        CAMPANUS = 2,
        EQUAL = 3
    }
    public enum EDecimalDisp
    {
        DECIMAL = 0,
        DEGREE = 1
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

        // デフォルトタイムゾーン
        [XmlElement("defaultTimezone")]
        public string defaultTimezone { get; set; }

        // プログレス計算方法
        [XmlElement("progression")]
        public EProgression progression { get; set; }

        // デフォルト表示
        [XmlElement("defaultbands")]
        public int defaultBands { get; set; }

        // ハウス
        [XmlElement("house")]
        public int houseCalc { get; set; }

        // 獣帯外側幅
        [XmlElement("zodiacOuterWidth")]
        public int zodiacOuterWidth { get; set; }

        // 獣帯幅
        [XmlElement("zodiacWidth")]
        public int zodiacWidth { get; set; }

        // 中心円
        [XmlElement("zodiacCenter")]
        public int zodiacCenter { get; set; }

        // 10進、60進
        [XmlElement("decimalDisp")]
        public int decimalDisp { get; set; }

        // SolarFireっぽく表示orAMATERUっぽく表示
        [XmlElement("dispPattern")]
        public int dispPattern { get; set; }

        public ConfigData()
        {
            ephepath = @"\ephe";
            centric = ECentric.GEO_CENTRIC;
            sidereal = Esidereal.TROPICAL;
            defaultPlace = "東京都中央区";
            lat = 35.670587;
            lng = 139.772003;
            houseCalc = (int)EHouseCalc.PLACIDUS;
            zodiacOuterWidth = 470;
            zodiacWidth = 60;
            zodiacCenter = 250;
            defaultTimezone = "JST";
            dispPattern = 0;
            progression = EProgression.PRIMARY;
        }

    }
}
