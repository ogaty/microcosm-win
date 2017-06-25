using SwissEphNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using microcosm.Config;
using microcosm.Planet;
using microcosm.Common;
using microcosm.Aspect;
using microcosm.Setting;

namespace microcosm.Calc
{
    // 天体およびハウスの計算をする
    public class AstroCalc
    {
        public MainWindow main;
        public ConfigData config;
        public double year_days = 365.2424;
        public SwissEph s;

        public AstroCalc(MainWindow main, ConfigData config)
        {
            this.main = main;
            this.config = config;
            s = new SwissEph();
            // http://www.astro.com/ftp/swisseph/ephe/archive_zip/ からDL
            s.swe_set_ephe_path(config.ephepath);
            s.OnLoadFile += (sender, ev) => {
                if (File.Exists(ev.FileName))
                    ev.File = new FileStream(ev.FileName, FileMode.Open);
            };
        }

        /// <summary>
        /// for unit test
        /// </summary>
        /// <param name="config"></param>
        public AstroCalc(ConfigData config)
        {
            this.config = config;
            s = new SwissEph();
            s.swe_set_ephe_path(config.ephepath);
            s.OnLoadFile += (sender, ev) => {
                if (File.Exists(ev.FileName))
                    ev.File = new FileStream(ev.FileName, FileMode.Open);
            };
        }

        // 天体の位置を計算する
        public Dictionary<int, PlanetData> PositionCalc(DateTime d, double lat, double lng, int houseKind, int subIndex)
        {
            Dictionary<int, PlanetData> planetdata = new Dictionary<int, PlanetData>(); ;

            // absolute position
            double[] x = { 0, 0, 0, 0, 0, 0 };
            string serr = "";

            int utc_year = 0;
            int utc_month = 0;
            int utc_day = 0;
            int utc_hour = 0;
            int utc_minute = 0;
            double utc_second = 0;

            // [0]:Ephemeris Time [1]:Universal Time
            double[] dret = { 0.0, 0.0 };

            int ii = 0;

            // utcに変換
            s.swe_utc_time_zone(d.Year, d.Month, d.Day, d.Hour, d.Minute, d.Second, 0.0, ref utc_year, ref utc_month, ref utc_day, ref utc_hour, ref utc_minute, ref utc_second);
            s.swe_utc_to_jd(utc_year, utc_month, utc_day, utc_hour, utc_minute, utc_second, 1, dret, ref serr);

            // 10天体ループ
            // 11(DH/TrueNode)、14(Earth)、15(Chiron)もついでに計算
            Enumerable.Range(0, 16).ToList().ForEach(i =>
            {
                int flag = SwissEph.SEFLG_SWIEPH | SwissEph.SEFLG_SPEED;
                if (config.centric == ECentric.HELIO_CENTRIC)
                    flag |= SwissEph.SEFLG_HELCTR;
                if (config.sidereal == Esidereal.SIDEREAL)
                {
                    flag |= SwissEph.SEFLG_SIDEREAL;
                    s.swe_set_sid_mode(SwissEph.SE_SIDM_LAHIRI, 0, 0);
                    // ayanamsa計算
                    double daya = 0.0;
                    double ut = s.swe_get_ayanamsa_ex_ut(dret[1], SwissEph.SEFLG_SWIEPH, out daya, ref serr);

                    // Ephemeris Timeで計算, 結果はxに入る
                    s.swe_calc_ut(dret[1], i, flag, x, ref serr);
                }
                else
                {
                    // Universal Timeで計算, 結果はxに入る
                    s.swe_calc_ut(dret[1], i, flag, x, ref serr);
                }


                PlanetData p = new PlanetData() { no = i, absolute_position = x[0], speed = x[3], aspects = new List<AspectInfo>(),
                    secondAspects = new List<AspectInfo>(),
                    thirdAspects = new List<AspectInfo>(),
                    sensitive = false };
                if (i < 10 && subIndex >= 0)
                {
                    if (main.currentSetting.dispPlanet[subIndex][main.dispListMap[i]])
                    {
                        p.isDisp = true;
                    }
                    else
                    {
                        p.isDisp = false;
                    }
                    if (main.currentSetting.dispAspectPlanet[subIndex][main.dispListMap[i]])
                    {
                        p.isAspectDisp = true;
                    }
                    else
                    {
                        p.isAspectDisp = false;
                    }
                }
                if (config.centric == ECentric.HELIO_CENTRIC && i == 0)
                {
                    // ヘリオセントリック太陽
                    p.isDisp = false;
                    p.isAspectDisp = false;
                }
                if (i == 10)
                {
                    // MEAN NODE
                    p.isDisp = false;
                    p.isAspectDisp = false;
                }
                if (i == 11 && subIndex >= 0 && config.centric == ECentric.GEO_CENTRIC)
                {
                    // TRUE NODE ヘッド
                    if (main.currentSetting.dispPlanet[subIndex][11])
                    {
                        p.isDisp = true;
                    }
                    else
                    {
                        p.isDisp = false;
                    }
                    if (main.currentSetting.dispAspectPlanet[subIndex][11])
                    {
                        p.isAspectDisp = true;
                    }
                    else
                    {
                        p.isAspectDisp = false;
                    }
                }
                if (i == 12)
                {
                    // mean apogee、どうでもいい
                    p.isDisp = false;
                    p.isAspectDisp = false;
                }
                if (i == 13 && subIndex >= 0)
                {
                    // true apogee、要はリリス
                    if (main.currentSetting.dispPlanet[subIndex][CommonData.ZODIAC_LILITH])
                    {
                        p.isDisp = true;
                    }
                    else
                    {
                        p.isDisp = false;
                    }
                    if (main.currentSetting.dispAspectPlanet[subIndex][CommonData.ZODIAC_LILITH])
                    {
                        p.isAspectDisp = true;
                    }
                    else
                    {
                        p.isAspectDisp = false;
                    }
                }
                if (config.centric == ECentric.HELIO_CENTRIC && i == 14 && subIndex >= 0)
                {
                    // ヘリオセントリック地球
                    if (main.currentSetting.dispPlanet[subIndex][main.dispListMap[i]])
                    {
                        p.isDisp = true;
                    }
                    else
                    {
                        p.isDisp = false;
                    }
                    if (main.currentSetting.dispAspectPlanet[subIndex][main.dispListMap[i]])
                    {
                        p.isAspectDisp = true;
                    }
                    else
                    {
                        p.isAspectDisp = false;
                    }
                }
                if (i == 15 && subIndex >= 0)
                {
                    if (main.currentSetting.dispPlanet[subIndex][CommonData.ZODIAC_CHIRON])
                    {
                        p.isDisp = true;
                    }
                    else
                    {
                        p.isDisp = false;
                    }
                    if (main.currentSetting.dispAspectPlanet[subIndex][CommonData.ZODIAC_CHIRON])
                    {
                        p.isAspectDisp = true;
                    }
                    else
                    {
                        p.isAspectDisp = false;
                    }
                }
                ii = i;
                planetdata[i] = p;
            });

            s.swe_close();
            // ハウスを後ろにくっつける
            double[] houses = CuspCalc(d, lat, lng, houseKind);
            planetdata = setHouse(planetdata, houses, main.currentSetting, subIndex);

            return planetdata;
        }

        public PlanetData PositionCalcSingle(int no, DateTime d)
        {
            // absolute position
            double[] x = { 0, 0, 0, 0, 0, 0 };
            string serr = "";

            int utc_year = 0;
            int utc_month = 0;
            int utc_day = 0;
            int utc_hour = 0;
            int utc_minute = 0;
            double utc_second = 0;

            // [0]:Ephemeris Time [1]:Universal Time
            double[] dret = { 0.0, 0.0 };

            // utcに変換
            s.swe_utc_time_zone(d.Year, d.Month, d.Day, d.Hour, d.Minute, d.Second, 0.0, ref utc_year, ref utc_month, ref utc_day, ref utc_hour, ref utc_minute, ref utc_second);
            s.swe_utc_to_jd(utc_year, utc_month, utc_day, utc_hour, utc_minute, utc_second, 1, dret, ref serr);


            int flag = SwissEph.SEFLG_SWIEPH | SwissEph.SEFLG_SPEED;
            if (config.centric == ECentric.HELIO_CENTRIC)
                flag |= SwissEph.SEFLG_HELCTR;
            if (config.sidereal == Esidereal.SIDEREAL)
            {
                flag |= SwissEph.SEFLG_SIDEREAL;
                s.swe_set_sid_mode(SwissEph.SE_SIDM_LAHIRI, 0, 0);
                // ayanamsa計算
                double daya = 0.0;
                double ut = s.swe_get_ayanamsa_ex_ut(dret[1], SwissEph.SEFLG_SWIEPH, out daya, ref serr);

                // Ephemeris Timeで計算, 結果はxに入る
                s.swe_calc_ut(dret[1], no, flag, x, ref serr);
            }
            else
            {
                // Universal Timeで計算, 結果はxに入る
                s.swe_calc_ut(dret[1], no, flag, x, ref serr);
            }


            PlanetData planetData = new PlanetData();
            planetData.no = no;
            planetData.absolute_position = x[0];

            return planetData;
        }

        /// <summary>
        /// unittest用
        /// </summary>
        /// <param name="subIndex">targetNoList決定に使用</param>
        /// <returns></returns>
        public Dictionary<int, PlanetData> setHouse(Dictionary<int, PlanetData> pdata, double[] houses, SettingData setting, int subIndex)
        {
            PlanetData pAsc = new PlanetData()
            {
                absolute_position = houses[1],
                no = CommonData.ZODIAC_ASC,
                sensitive = true,
                speed = 1,
                aspects = new List<AspectInfo>(),
                secondAspects = new List<AspectInfo>(),
                thirdAspects = new List<AspectInfo>()
            };
            if (subIndex >= 0)
            {
                if (setting.dispPlanet[subIndex][CommonData.ZODIAC_ASC])
                {
                    pAsc.isDisp = true;
                }
                else
                {
                    pAsc.isDisp = false;
                }
                if (setting.dispAspectPlanet[subIndex][CommonData.ZODIAC_ASC])
                {
                    pAsc.isAspectDisp = true;
                }
                else
                {
                    pAsc.isAspectDisp = false;
                }
            }

            pdata[CommonData.ZODIAC_ASC] = pAsc;


            PlanetData pMc = new PlanetData()
            {
                isAspectDisp = true,
                isDisp = true,
                absolute_position = houses[10],
                no = CommonData.ZODIAC_MC,
                sensitive = true,
                speed = 1,
                aspects = new List<AspectInfo>(),
                secondAspects = new List<AspectInfo>(),
                thirdAspects = new List<AspectInfo>()
            };
            if (subIndex >= 0)
            {
                if (setting.dispPlanet[subIndex][CommonData.ZODIAC_MC])
                {
                    pMc.isDisp = true;
                }
                else
                {
                    pMc.isDisp = false;
                }
                if (setting.dispAspectPlanet[subIndex][CommonData.ZODIAC_MC])
                {
                    pMc.isAspectDisp = true;
                }
                else
                {
                    pMc.isAspectDisp = false;
                }
            }

            pdata[CommonData.ZODIAC_MC] = pMc;
            return pdata;
        }

        // カスプを計算
        public double[] CuspCalc(DateTime d, double lat, double lng, int houseKind)
        {
            int utc_year = 0;
            int utc_month = 0;
            int utc_day = 0;
            int utc_hour = 0;
            int utc_minute = 0;
            double utc_second = 0;
            string serr = "";
            // [0]:Ephemeris Time [1]:Universal Time
            double[] dret = { 0.0, 0.0 };

            SwissEph s = new SwissEph();

            // utcに変換
            s.swe_utc_time_zone(d.Year, d.Month, d.Day, d.Hour, d.Minute, d.Second, 0.0, ref utc_year, ref utc_month, ref utc_day, ref utc_hour, ref utc_minute, ref utc_second);
            s.swe_utc_to_jd(utc_year, utc_month, utc_day, utc_hour, utc_minute, utc_second, 1, dret, ref serr);

            double[] cusps = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            double[] ascmc = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            if (houseKind == 0)
            {
                // Placidas
                s.swe_houses(dret[1], lat, lng, 'P', cusps, ascmc);
            }
            else if (houseKind == 1)
            {
                // Koch
                s.swe_houses(dret[1], lat, lng, 'K', cusps, ascmc);
            }
            else if (houseKind == 2)
            {
                // Campanus
                s.swe_houses(dret[1], lat, lng, 'C', cusps, ascmc);
            }
            else
            {
                // Equal
                s.swe_houses(dret[1], lat, lng, 'E', cusps, ascmc);
            }
            s.swe_close();

            return cusps;
        }

        // 一度一年法
        // 一度ずらすので再計算不要
        // 一年を365.24日で計算、当然ずれが生じる
        // 面倒なのでとりあえず放置
        public Dictionary<int, PlanetData> PrimaryProgressionCalc(Dictionary<int, PlanetData> natallist, DateTime natalTime, DateTime transitTime)
        {
            Dictionary<int, PlanetData> progresslist = new Dictionary<int, PlanetData>();
            TimeSpan ts = transitTime - natalTime;
            double years = ts.TotalDays / year_days;
            foreach (KeyValuePair<int, PlanetData> pair in natallist)
            {
                PlanetData progressdata = new PlanetData()
                {
                    absolute_position = pair.Value.absolute_position,
                    no = pair.Key,
                    sensitive = pair.Value.sensitive,
                    speed = pair.Value.speed,
                    aspects = new List<AspectInfo>(),
                    secondAspects = new List<AspectInfo>(),
                    thirdAspects = new List<AspectInfo>(),
                    fourthAspects = new List<AspectInfo>(),
                    fifthAspects = new List<AspectInfo>(),
                    isDisp = true,
                    isAspectDisp = true
                };
                if (config.centric == ECentric.HELIO_CENTRIC && pair.Key == 0)
                {
                    // ヘリオセントリック太陽
                    progressdata.isDisp = false;
                    progressdata.isAspectDisp = false;
                }
                if (pair.Key == 10)
                {
                    // MEAN NODE
                    progressdata.isDisp = false;
                    progressdata.isAspectDisp = false;
                }
                if (pair.Key == 12)
                {
                    // mean apogee、どうでもいい
                    progressdata.isDisp = false;
                    progressdata.isAspectDisp = false;
                }
                if (pair.Key == 13)
                {
                    // true apogee、リリス
                    progressdata.isDisp = false;
                    progressdata.isAspectDisp = false;
                }
                if (config.centric == ECentric.GEO_CENTRIC && pair.Key == 14)
                {
                    // ヘリオセントリック地球
                    progressdata.isDisp = false;
                    progressdata.isAspectDisp = false;
                }

                progressdata.absolute_position += years;
                progressdata.absolute_position %= 365;
                progresslist[pair.Key] = progressdata;
            }

            return progresslist;
        }
        public double[] PrimaryProgressionHouseCalc(double[] houseList, DateTime natalTime, DateTime transitTime)
        {
            double[] retHouse = new double[13];
            houseList.CopyTo(retHouse, 0);

            List<PlanetData> progresslist = new List<PlanetData>();
            TimeSpan ts = transitTime - natalTime;
            double years = ts.TotalDays / year_days;
            for (int i = 0; i < houseList.Count(); i++)
            {
                retHouse[i] += years;
            }

            return retHouse;
        }


        // 一日一年法
        // 一年を365.24日で計算、当然ずれが生じる
        // 面倒なのでとりあえず放置
        public Dictionary<int, PlanetData> SecondaryProgressionCalc(Dictionary<int, PlanetData> natallist, DateTime natalTime, DateTime transitTime)
        {
            Dictionary<int, PlanetData> progresslist = new Dictionary<int, PlanetData>();
            TimeSpan ts = transitTime - natalTime;
            double years = ts.TotalDays / year_days;

            // 日付を秒数に変換する
            int seconds = (int)(years * 86400);

            TimeSpan add = TimeSpan.FromSeconds(seconds);

            DateTime newTime = natalTime + add;

            // absolute position
            double[] x = { 0, 0, 0, 0, 0, 0 };
            string serr = "";

            int utc_year = 0;
            int utc_month = 0;
            int utc_day = 0;
            int utc_hour = 0;
            int utc_minute = 0;
            double utc_second = 0;

            // [0]:Ephemeris Time [1]:Universal Time
            double[] dret = { 0.0, 0.0 };

            // utcに変換
            s.swe_utc_time_zone(newTime.Year, newTime.Month, newTime.Day, newTime.Hour, newTime.Minute, newTime.Second, 0.0, ref utc_year, ref utc_month, ref utc_day, ref utc_hour, ref utc_minute, ref utc_second);
            s.swe_utc_to_jd(utc_year, utc_month, utc_day, utc_hour, utc_minute, utc_second, 1, dret, ref serr);

            foreach (KeyValuePair<int, PlanetData> pair in natallist)
            {
                if (config.centric == ECentric.HELIO_CENTRIC && pair.Key == CommonData.ZODIAC_SUN)
                {
                    // ヘリオセントリック太陽
                    continue;
                }
                if (pair.Key == 10)
                {
                    // MEAN NODE
                    continue;
                }
                if (pair.Key == 12)
                {
                    // mean apogee、どうでもいい
                    continue;
                }
                if (pair.Key == 13)
                {
                    // true apogee、リリス
                    continue;
                }
                if (config.centric == ECentric.GEO_CENTRIC && pair.Key == CommonData.ZODIAC_EARTH)
                {
                    // ジオセントリック地球
                    continue;
                }
                int flag = SwissEph.SEFLG_SWIEPH | SwissEph.SEFLG_SPEED;
                if (config.centric == ECentric.HELIO_CENTRIC) flag |= SwissEph.SEFLG_HELCTR;
                if (config.sidereal == Esidereal.SIDEREAL)
                {
                    flag |= SwissEph.SEFLG_SIDEREAL;
                    s.swe_set_sid_mode(SwissEph.SE_SIDM_LAHIRI, 0, 0);
                    // ayanamsa計算
                    double daya = 0.0;
                    double ut = s.swe_get_ayanamsa_ex_ut(dret[1], SwissEph.SEFLG_SWIEPH, out daya, ref serr);

                    // Ephemeris Timeで計算, 結果はxに入る
                    s.swe_calc_ut(dret[1], pair.Key, flag, x, ref serr);
                    // tropicalからayanamsaをマイナス
                    x[0] = x[0] > daya ? x[0] - daya : x[0] - daya + 360;
                }
                else
                {
                    // Universal Timeで計算, 結果はxに入る
                    s.swe_calc_ut(dret[1], pair.Key, flag, x, ref serr);
                }

                PlanetData progressdata = new PlanetData()
                {
                    absolute_position = x[0],
                    no = pair.Key,
                    sensitive = pair.Value.sensitive,
                    speed = pair.Value.speed,
                    aspects = new List<AspectInfo>(),
                    secondAspects = new List<AspectInfo>(),
                    thirdAspects = new List<AspectInfo>(),
                    fourthAspects = new List<AspectInfo>(),
                    fifthAspects = new List<AspectInfo>(),
                    isDisp = true,
                    isAspectDisp = true
                };
                progresslist[pair.Key] = progressdata;
            }

            return progresslist;
        }
        public double[] SecondaryProgressionHouseCalc(double[] houseList, Dictionary<int, PlanetData> natallist, DateTime natalTime, DateTime transitTime, double lat, double lng, string timezone)
        {
            List<PlanetData> progresslist = new List<PlanetData>();
            TimeSpan ts = transitTime - natalTime;
            double years = ts.TotalDays / year_days;

            // 日付を秒数に変換する
            int seconds = (int)(years * 86400);

            TimeSpan add = TimeSpan.FromSeconds(seconds);

            DateTime newTime = natalTime + add;


            double[] retHouse = CuspCalc(newTime, lat, lng, (int)main.config.houseCalc);

            return retHouse;
        }

        // CPS
        // 月、水星、金星、太陽は一日一年法
        // 火星以降および感受点は一度一年法
        // 一年を365.24日で計算、当然ずれが生じる
        // 面倒なのでとりあえず放置
        public Dictionary<int, PlanetData> CompositProgressionCalc(Dictionary<int, PlanetData> natallist, DateTime natalTime, DateTime transitTime)
        {
            Dictionary<int, PlanetData> progresslist = new Dictionary<int, PlanetData>();
            TimeSpan ts = transitTime - natalTime;
            double years = ts.TotalDays / year_days;

            // 日付を秒数に変換する
            int seconds = (int)(years * 86400);

            TimeSpan add = TimeSpan.FromSeconds(seconds);

            DateTime newTime = natalTime + add;

            // absolute position
            double[] x = { 0, 0, 0, 0, 0, 0 };
            string serr = "";

            int utc_year = 0;
            int utc_month = 0;
            int utc_day = 0;
            int utc_hour = 0;
            int utc_minute = 0;
            double utc_second = 0;

            // [0]:Ephemeris Time [1]:Universal Time
            double[] dret = { 0.0, 0.0 };

            // utcに変換
            s.swe_utc_time_zone(newTime.Year, newTime.Month, newTime.Day, newTime.Hour, newTime.Minute, newTime.Second, 0.0, ref utc_year, ref utc_month, ref utc_day, ref utc_hour, ref utc_minute, ref utc_second);
            s.swe_utc_to_jd(utc_year, utc_month, utc_day, utc_hour, utc_minute, utc_second, 1, dret, ref serr);

            foreach (KeyValuePair<int, PlanetData> pair in natallist)
            {
                PlanetData progressdata;
                if (config.centric == ECentric.HELIO_CENTRIC && pair.Key == CommonData.ZODIAC_SUN)
                {
                    // ヘリオセントリック太陽
                    continue;
                }
                if (pair.Key == 10)
                {
                    // MEAN NODE
                    continue;
                }
                if (pair.Key == 12)
                {
                    // mean apogee、どうでもいい
                    continue;
                }
                if (pair.Key == 13)
                {
                    // true apogee、リリス
                    continue;
                }
                if (config.centric == ECentric.GEO_CENTRIC && pair.Key == CommonData.ZODIAC_EARTH)
                {
                    // ジオセントリック地球
                    continue;
                }

                if ((pair.Key != CommonData.ZODIAC_MOON) &&
                    (pair.Key != CommonData.ZODIAC_MERCURY) &&
                    (pair.Key != CommonData.ZODIAC_VENUS) &&
                    (pair.Key != CommonData.ZODIAC_SUN))
                {
                    progressdata = new PlanetData()
                    {
                        absolute_position = pair.Value.absolute_position,
                        no = pair.Key,
                        sensitive = pair.Value.sensitive,
                        speed = pair.Value.speed,
                        aspects = new List<AspectInfo>(),
                        secondAspects = new List<AspectInfo>(),
                        thirdAspects = new List<AspectInfo>(),
                        fourthAspects = null,
                        fifthAspects = null,
                        sixthAspects = null,
                        seventhAspects = null,
                        isDisp = true,
                    };
                    progressdata.absolute_position += years;
                    progressdata.absolute_position %= 365;
                    progresslist[pair.Key] = progressdata;
                    continue;
                }
                int flag = SwissEph.SEFLG_SWIEPH | SwissEph.SEFLG_SPEED;
                if (config.centric == ECentric.HELIO_CENTRIC)
                    flag |= SwissEph.SEFLG_HELCTR;
                if (config.sidereal == Esidereal.SIDEREAL)
                {
                    flag |= SwissEph.SEFLG_SIDEREAL;
                    s.swe_set_sid_mode(SwissEph.SE_SIDM_LAHIRI, 0, 0);
                    // ayanamsa計算
                    double daya = 0.0;
                    double ut = s.swe_get_ayanamsa_ex_ut(dret[1], SwissEph.SEFLG_SWIEPH, out daya, ref serr);

                    // Ephemeris Timeで計算, 結果はxに入る
                    s.swe_calc_ut(dret[1], pair.Key, flag, x, ref serr);
                    // tropicalからayanamsaをマイナス
                    x[0] = x[0] > daya ? x[0] - daya : x[0] - daya + 360;
                }
                else
                {
                    // Universal Timeで計算, 結果はxに入る
                    s.swe_calc_ut(dret[1], pair.Key, flag, x, ref serr);
                }

                progressdata = new PlanetData()
                {
                    absolute_position = x[0],
                    no = pair.Key,
                    sensitive = pair.Value.sensitive,
                    speed = pair.Value.speed,
                    aspects = new List<AspectInfo>(),
                    secondAspects = new List<AspectInfo>(),
                    thirdAspects = new List<AspectInfo>(),
                    fourthAspects = new List<AspectInfo>(),
                    fifthAspects = new List<AspectInfo>(),
                    isDisp = true,
                    isAspectDisp = true
                };
                progresslist[pair.Key] = progressdata;
            }



            return progresslist;
        }
        public double[] CompositProgressionHouseCalc(double[] houseList, Dictionary<int, PlanetData> natallist, DateTime natalTime, DateTime transitTime, double lat, double lng, string timezone)
        {
            // AMATERU、SG共にSecondaryで計算されてた
            return SecondaryProgressionHouseCalc(houseList, natallist, natalTime, transitTime, lat, lng, timezone);
        }

        private double getDegree(AspectKind kind)
        {
            switch (kind)
            {
                case AspectKind.CONJUNCTION:
                    return 0.0;
                case AspectKind.OPPOSITION:
                    return 180.0;
                case AspectKind.TRINE:
                    return 120.0;
                case AspectKind.SQUARE:
                    return 90.0;
                case AspectKind.SEXTILE:
                    return 60.0;
                case AspectKind.INCONJUNCT:
                    return 150.0;
                case AspectKind.SESQUIQUADRATE:
                    return 135.0;
                default:
                    break;
            }

            return 0.0;
        }
    }
}
