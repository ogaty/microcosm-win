using microcosm.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace microcosm.Common
{
    public class ringSubIndex
    {
        public int from;
        public int to;
    }
    public enum OrbKind
    {
        SUN_HARD_1ST = 0,
        SUN_SOFT_1ST = 1,
        SUN_HARD_2ND = 2,
        SUN_SOFT_2ND = 3,
        SUN_HARD_150 = 4,
        SUN_SOFT_150 = 5,
        MOON_HARD_1ST = 6,
        MOON_SOFT_1ST = 7,
        MOON_HARD_2ND = 8,
        MOON_SOFT_2ND = 9,
        MOON_HARD_150 = 10,
        MOON_SOFT_150 = 11,
        OTHER_HARD_1ST = 12,
        OTHER_SOFT_1ST = 13,
        OTHER_HARD_2ND = 14,
        OTHER_SOFT_2ND = 15,
        OTHER_HARD_150 = 16,
        OTHER_SOFT_150 = 17
    }


    public class CommonData
    {
        const double TIMEZONE_JST = 9.0;
        const double TIMEZONE_GMT = 0.0;

        public const int ZODIAC_SUN = 0;
        public const int ZODIAC_MOON = 1;
        public const int ZODIAC_MERCURY = 2;
        public const int ZODIAC_VENUS = 3;
        public const int ZODIAC_MARS = 4;
        public const int ZODIAC_JUPITER = 5;
        public const int ZODIAC_SATURN = 6;
        public const int ZODIAC_URANUS = 7;
        public const int ZODIAC_NEPTUNE = 8;
        public const int ZODIAC_PLUTO = 9;
        public const int ZODIAC_DH_TRUENODE = 11;
        public const int ZODIAC_DT_OSCULATE_APOGEE = 13;
        public const int ZODIAC_LILITH = 13; // 小惑星のリリス(1181)と混同しないこと
        public const int ZODIAC_EARTH = 14;
        public const int ZODIAC_CHIRON = 15;
        public const int ZODIAC_CELES = 17;
        public const int ZODIAC_PARAS = 18;
        public const int ZODIAC_JUNO = 19;
        public const int ZODIAC_VESTA = 20;
        public const int ZODIAC_ASC = 10000;
        public const int ZODIAC_MC = 10001;

        public const int SIGN_ARIES = 0;
        public const int SIGN_TAURUS = 1;
        public const int SIGN_GEMINI = 2;
        public const int SIGN_CANCER = 3;
        public const int SIGN_LEO = 4;
        public const int SIGN_VIRGO = 5;
        public const int SIGN_LIBRA = 6;
        public const int SIGN_SCORPIO = 7;
        public const int SIGN_SAGITTARIUS = 8;
        public const int SIGN_CAPRICORN = 9;
        public const int SIGN_AQUARIUS = 10;
        public const int SIGN_PISCES = 11;

        // タイムゾーンを返す
        public static double getTimezone(string timezone)
        {
            switch (timezone)
            {
                case "JST":
                    return TIMEZONE_JST;
                default:
                    break;
            }
            return TIMEZONE_GMT;
        }
        // (日本標準)とかの文字列を返す
        public static string getTimezoneLongText(string timezone)
        {
            switch (timezone)
            {
                case "JST":
                    return Properties.Resources.TIMEZONE_JST_STR_LONG;
                default:
                    break;
            }
            return Properties.Resources.TIMEZONE_UTC_STR_LONG;
        }
        // JSTのみを返す
        public static string getTimezoneShortText(int index)
        {
            switch (index)
            {
                case 0:
                    return Properties.Resources.TIMEZONE_JST_STR_SHORT;
                default:
                    break;
            }
            return Properties.Resources.TIMEZONE_UTC_STR_SHORT;
        }
        // SelectBoxのIndexを返す
        public static int getTimezoneIndex(string timezone)
        {
            switch (timezone)
            {
                case "JST":
                    return 0;
                case "UTC":
                    return 1;
            }
            return 0;
        }
        // 番号を引数に天体のシンボルを返す
        public static string getPlanetSymbol(int number)
        {
            switch (number)
            {
                case ZODIAC_SUN:
                    return "\u2609";
                case ZODIAC_MOON:
                    return "\u263d";
                case ZODIAC_MERCURY:
                    return "\u263f";
                case ZODIAC_VENUS:
                    return "\u2640";
                case ZODIAC_MARS:
                    return "\u2642";
                case ZODIAC_JUPITER:
                    return "\u2643";
                case ZODIAC_SATURN:
                    return "\u2644";
                case ZODIAC_URANUS:
                    return "\u2645";
                case ZODIAC_NEPTUNE:
                    return "\u2646";
                case ZODIAC_PLUTO:
                    return "\u2647";
                // 外部フォントだと天文学用のPLUTOになっているのが困りどころ
                case ZODIAC_DH_TRUENODE:
                    return "\u260a";
                case ZODIAC_EARTH:
                    return "\u2641";
                case ZODIAC_CHIRON:
                    return "\u26b7";
                case ZODIAC_LILITH:
                    return "\u26b8";
            }
            return "";
        }

        // 番号を引数に天体の文字列を返す
        public static string getPlanetText(int number)
        {
            switch (number)
            {
                case ZODIAC_SUN:
                    return "太陽";
                case ZODIAC_MOON:
                    return "月";
                case ZODIAC_MERCURY:
                    return "水星";
                case ZODIAC_VENUS:
                    return "金星";
                case ZODIAC_MARS:
                    return "火星";
                case ZODIAC_JUPITER:
                    return "木星";
                case ZODIAC_SATURN:
                    return "土星";
                case ZODIAC_URANUS:
                    return "天王星";
                case ZODIAC_NEPTUNE:
                    return "海王星";
                case ZODIAC_PLUTO:
                    return "冥王星";
                case ZODIAC_DH_TRUENODE:
                    return "ヘッド";
                case ZODIAC_CHIRON:
                    return "キロン";
                case ZODIAC_ASC:
                    return "ASC";
                case ZODIAC_MC:
                    return "MC";
                case ZODIAC_EARTH:
                    return "地球";
                case ZODIAC_LILITH:
                    return "リリス";
            }
            return "";
        }

        // 番号を引数にサインのシンボルを返す
        public static string getSignSymbol(int number)
        {
            switch (number)
            {
                case SIGN_ARIES:
                    return "\u2648";
                case SIGN_TAURUS:
                    return "\u2649";
                case SIGN_GEMINI:
                    return "\u264a";
                case SIGN_CANCER:
                    return "\u264b";
                case SIGN_LEO:
                    return "\u264c";
                case SIGN_VIRGO:
                    return "\u264d";
                case SIGN_LIBRA:
                    return "\u264e";
                case SIGN_SCORPIO:
                    return "\u264f";
                case SIGN_SAGITTARIUS:
                    return "\u2650";
                case SIGN_CAPRICORN:
                    return "\u2651";
                case SIGN_AQUARIUS:
                    return "\u2652";
                case SIGN_PISCES:
                    return "\u2653";
            }
            return "";
        }

        // 番号を引数にサインルーラーのシンボルを返す
        public static string getSignRulersSymbol(int number)
        {
            switch (number)
            {
                case SIGN_ARIES:
                    // 火星
                    return "\u2642";
                case SIGN_TAURUS:
                    // 金星
                    return "\u2640";
                case SIGN_GEMINI:
                    // 水星
                    return "\u263f";
                case SIGN_CANCER:
                    // 月
                    return "\u263d";
                case SIGN_LEO:
                    // 太陽
                    return "\u2609";
                case SIGN_VIRGO:
                    // 水星
                    return "\u263f";
                case SIGN_LIBRA:
                    // 金星
                    return "\u2640";
                case SIGN_SCORPIO:
                    // 冥王星
                    return "\u2647";
                case SIGN_SAGITTARIUS:
                    // 木星
                    return "\u2643";
                case SIGN_CAPRICORN:
                    // 土星
                    return "\u2644";
                case SIGN_AQUARIUS:
                    // 天王星
                    return "\u2645";
                case SIGN_PISCES:
                    // 海王星
                    return "\u2646";
            }
            return "";
        }

        // planetNoを引数に所属するサインルーラーの番号を返す
        public static int getSignRulersNo(int planetNo)
        {
            switch (planetNo)
            {
                case SIGN_ARIES:
                    // 火星
                    return 4;
                case SIGN_TAURUS:
                    // 金星
                    return 3;
                case SIGN_GEMINI:
                    // 水星
                    return 2;
                case SIGN_CANCER:
                    // 月
                    return 1;
                case SIGN_LEO:
                    // 太陽
                    return 0;
                case SIGN_VIRGO:
                    // 水星
                    return 2;
                case SIGN_LIBRA:
                    // 金星
                    return 3;
                case SIGN_SCORPIO:
                    // 冥王星
                    return 9;
                case SIGN_SAGITTARIUS:
                    // 木星
                    return 5;
                case SIGN_CAPRICORN:
                    // 土星
                    return 6;
                case SIGN_AQUARIUS:
                    // 天王星
                    return 7;
                case SIGN_PISCES:
                    // 海王星
                    return 8;
            }
            return 0;
        }

        // 番号を引数に感受点のシンボルを返す
        public static string getSensitiveSymbol(int number)
        {
            switch (number)
            {
                // UNICODEが無い！
                case ZODIAC_ASC:
                    return "Ac";
                // return "K";
                // UNICODEが無い！
                case ZODIAC_MC:
                    return "Mc";
                // return "L";
                case ZODIAC_DH_TRUENODE:
                    return "\u260a";
                    // return "M";
            }
            return "";
        }

        // 番号を引数に感受点の文字列を返す
        public static string getSensitiveText(int number)
        {
            switch (number)
            {
                case ZODIAC_ASC:
                    return "ASC";
                case ZODIAC_MC:
                    return "MC";
                case ZODIAC_DH_TRUENODE:
                    return "D.H.";
            }
            return "";
        }

        public static System.Windows.Media.Brush getPlanetColor(int number)
        {
            if (number == (int)CommonData.ZODIAC_SUN)
            {
                return System.Windows.Media.Brushes.Olive;
            }
            else if (number == (int)CommonData.ZODIAC_MOON)
            {
                return System.Windows.Media.Brushes.DarkGoldenrod;
            }
            else if (number == (int)CommonData.ZODIAC_MERCURY)
            {
                return System.Windows.Media.Brushes.Purple;
            }
            else if (number == (int)CommonData.ZODIAC_VENUS)
            {
                return System.Windows.Media.Brushes.Green;
            }
            else if (number == (int)CommonData.ZODIAC_MARS)
            {
                return System.Windows.Media.Brushes.Red;
            }
            else if (number == (int)CommonData.ZODIAC_JUPITER)
            {
                return System.Windows.Media.Brushes.Maroon;
            }
            else if (number == (int)CommonData.ZODIAC_SATURN)
            {
                return System.Windows.Media.Brushes.DimGray;
            }
            else if (number == (int)CommonData.ZODIAC_URANUS)
            {
                return System.Windows.Media.Brushes.DarkTurquoise;
            }
            else if (number == (int)CommonData.ZODIAC_NEPTUNE)
            {
                return System.Windows.Media.Brushes.DodgerBlue;
            }
            else if (number == (int)CommonData.ZODIAC_PLUTO)
            {
                return System.Windows.Media.Brushes.DeepPink;
            }
            else if (number == (int)CommonData.ZODIAC_EARTH)
            {
                return System.Windows.Media.Brushes.SkyBlue;
            }
            else if (number == (int)CommonData.ZODIAC_DH_TRUENODE)
            {
                return System.Windows.Media.Brushes.DarkCyan;
            }
            else if (number == (int)CommonData.ZODIAC_LILITH)
            {
                return System.Windows.Media.Brushes.MediumSeaGreen;
            }
            return System.Windows.Media.Brushes.Black;
        }


        // サイン番号を返す(0:牡羊座、11:魚座)
        public static int getSign(double absolute_position)
        {
            return (int)absolute_position / 30;
        }

        // サインテキストを返す(0:♈、11:♓)
        public static string getSignText(double absolute_position)
        {
            return getSignSymbol((int)absolute_position / 30);
        }

        // サインテキストを返す(0:♈、11:♓)
        public static string getSignTextJp(double absolute_position)
        {
            switch((int)absolute_position / 30)
            {
                case 0:
                    return "牡羊座";
                case 1:
                    return "牡牛座";
                case 2:
                    return "双子座";
                case 3:
                    return "蟹座";
                case 4:
                    return "獅子座";
                case 5:
                    return "乙女座";
                case 6:
                    return "天秤座";
                case 7:
                    return "蠍座";
                case 8:
                    return "射手座";
                case 9:
                    return "山羊座";
                case 10:
                    return "水瓶座";
                case 11:
                    return "魚座";
                default:
                    break;
            }
            return "";
        }

        // サイン色を返す
        public static System.Windows.Media.Brush getSignColor(double absolute_position)
        {
            switch((int)absolute_position / 30)
            {
                case 0:
                    // 牡羊座
                    return System.Windows.Media.Brushes.OrangeRed;
                case 1:
                    // 牡牛座
                    return System.Windows.Media.Brushes.Goldenrod;
                case 2:
                    // 双子座
                    return System.Windows.Media.Brushes.MediumSeaGreen;
                case 3:
                    // 蟹座
                    return System.Windows.Media.Brushes.SteelBlue;
                case 4:
                    // 獅子座
                    return System.Windows.Media.Brushes.Crimson;
                case 5:
                    // 乙女座
                    return System.Windows.Media.Brushes.Maroon;
                case 6:
                    // 天秤座
                    return System.Windows.Media.Brushes.Teal;
                case 7:
                    // 蠍座
                    return System.Windows.Media.Brushes.CornflowerBlue;
                case 8:
                    // 射手座
                    return System.Windows.Media.Brushes.DeepPink;
                case 9:
                    // 山羊座
                    return System.Windows.Media.Brushes.SaddleBrown;
                case 10:
                    // 水瓶座
                    return System.Windows.Media.Brushes.CadetBlue;
                case 11:
                    // 魚座
                    return System.Windows.Media.Brushes.DodgerBlue;
                default:
                    break;
            }
            return System.Windows.Media.Brushes.Black;
        }

        // サイン度数を返す(0～29.9)
        public static double getDeg(double absolute_position)
        {
            return absolute_position % 30;
        }

        public static string getRetrograde(double speed)
        {
            if (speed < 0)
            {
                return "\u211e";
            }
            return "";
        }

        public static ringSubIndex getRingSubIndex(int subindex)
        {
            ringSubIndex ret = new ringSubIndex()
            {
                from = 0,
                to = 0
            };
            switch (subindex)
            {
                case 0:
                    // N
                    ret.from = 0;
                    ret.to = 0;
                    break;
                case 1:
                    // P
                    ret.from = 1;
                    ret.to = 1;
                    break;
                case 2:
                    // T
                    ret.from = 2;
                    ret.to = 2;
                    break;
                case 3:
                    // N-P
                    ret.from = 0;
                    ret.to = 1;
                    break;
                case 4:
                    // N-T
                    ret.from = 0;
                    ret.to = 2;
                    break;
                case 5:
                    // P-T
                    ret.from = 1;
                    ret.to = 2;
                    break;
                case 6:
                    // N-4
                    ret.from = 0;
                    ret.to = 3;
                    break;
                case 7:
                    // N-5
                    ret.from = 0;
                    ret.to = 4;
                    break;
                case 8:
                    // P-4
                    ret.from = 1;
                    ret.to = 3;
                    break;
                case 9:
                    // P-5
                    ret.from = 1;
                    ret.to = 4;
                    break;
                case 10:
                    // T-4
                    ret.from = 2;
                    ret.to = 3;
                    break;
                case 11:
                    // T-5
                    ret.from = 2;
                    ret.to = 4;
                    break;
                case 12:
                    // 4-4
                    ret.from = 3;
                    ret.to = 3;
                    break;
                case 13:
                    // 4-5
                    ret.from = 3;
                    ret.to = 4;
                    break;
                case 14:
                    // 5-5
                    ret.from = 4;
                    ret.to = 4;
                    break;
                default:
                    break;
            }

            return ret;
        }

        public static UserEventData udata2event(UserData udata)
        {
            return new UserEventData()
            {
                name = udata.name,
                birth_year = udata.birth_year,
                birth_month = udata.birth_month,
                birth_day = udata.birth_day,
                birth_hour = udata.birth_hour,
                birth_minute = udata.birth_minute,
                birth_second = udata.birth_second,
                birth_place = udata.birth_place,
                birth_str = udata.birth_str,
                lat = udata.lat,
                lng = udata.lng,
                lat_lng = udata.lat_lng,
                timezone = udata.timezone,
                memo = udata.memo
            };
        }
    }
}
