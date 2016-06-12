﻿using System;
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
        public const int ZODIAC_DT_OSCULATE_APOGEE = 13;
        public const int ZODIAC_EARTH = 14;
        public const int ZODIAC_CHIRON = 15;
        public const int ZODIAC_CELES = 17;
        public const int ZODIAC_PARAS = 18;
        public const int ZODIAC_JUNO = 19;
        public const int ZODIAC_VESTA = 20;
        public const int ZODIAC_LILITH = 1181;
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
                    return "\u2641";
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
    }
}
