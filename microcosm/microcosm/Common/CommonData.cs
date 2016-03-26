using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace microcosm.Common
{
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
        public const int ZODIAC_EARTH = 14;
        public const int ZODIAC_CHIRON = 15;
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
        // 番号を引数に天体のシンボルを返す
        public static string getPlanetSymbol(int number)
        {
            switch (number)
            {
                case ZODIAC_SUN:
                    return "\u2609";
                // return "A";
                case ZODIAC_MOON:
                    return "\u263d";
                // return "B";
                case ZODIAC_MERCURY:
                    return "\u263f";
                // return "C";
                case ZODIAC_VENUS:
                    return "\u2640";
                // return "D";
                case ZODIAC_MARS:
                    return "\u2642";
                // return "E";
                case ZODIAC_JUPITER:
                    return "\u2643";
                // return "F";
                case ZODIAC_SATURN:
                    return "\u2644";
                // return "G";
                case ZODIAC_URANUS:
                    return "\u2645";
                // return "H";
                case ZODIAC_NEPTUNE:
                    return "\u2646";
                // return "I";
                case ZODIAC_PLUTO:
                    return "\u2647";
                // 外部フォントだと天文学用のPLUTOになっているのが困りどころ
                // return "J";
                case ZODIAC_DH_TRUENODE:
                    return "\u260a";
                case ZODIAC_EARTH:
                    return "u2641";
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

        // サイン度数を返す(0～29.9)
        public static double getDeg(double absolute_position)
        {
            return absolute_position % 30;
        }
    }
}
