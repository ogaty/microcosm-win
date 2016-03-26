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
        public ConfigData config;
        public double year_days = 365.2424;
        public SwissEph s;

        public AstroCalc(ConfigData config)
        {
            this.config = config;
            s = new SwissEph();
            // http://www.astro.com/ftp/swisseph/ephe/archive_zip/ からDL
            s.swe_set_ephe_path(config.ephepath);
            s.OnLoadFile += (sender, ev) => {
                if (File.Exists(ev.FileName))
                    ev.File = new FileStream(ev.FileName, FileMode.Open);
            };
        }

        // 天体の位置を計算する
        public List<PlanetData> PositionCalc(int year, int month, int day, int hour, int min, double sec, double lat, double lng)
        {
            List<PlanetData> planetdata = new List<PlanetData>(); ;

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
            s.swe_utc_time_zone(year, month, day, hour, min, sec, 9.0, ref utc_year, ref utc_month, ref utc_day, ref utc_hour, ref utc_minute, ref utc_second);
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
                    // tropicalからayanamsaをマイナス
                    if (x[0] > daya)
                    {
                        x[0] -= daya;
                    }
                    else
                    {
                        x[0] = x[0] - daya + 360;
                    }
                }
                else
                {
                    // Universal Timeで計算, 結果はxに入る
                    s.swe_calc_ut(dret[1], i, flag, x, ref serr);
                }


                PlanetData p = new PlanetData() { no = i, absolute_position = x[0], speed = x[3], aspects = new List<AspectInfo>(),
                    secondAspects = new List<AspectInfo>(),
                    thirdAspects = new List<AspectInfo>(),
                    sensitive = false, isDisp = true, isAspectDisp = true };
                if (config.centric == ECentric.HELIO_CENTRIC && i == 0)
                {
                    // ヘリオセントリック太陽
                    p.isDisp = false;
                    p.isAspectDisp = false;
                }
                if (i >= 10)
                {
                    p.isDisp = false;
                    p.isAspectDisp = false;
                }
                if (i == 11)
                {
                    // ヘッド
                    p.isDisp = true;
                    p.isAspectDisp = true;
                }
                if (config.centric == ECentric.HELIO_CENTRIC && i == 14)
                {
                    // ヘリオセントリック地球
                    p.isDisp = true;
                    p.isAspectDisp = true;
                }
                planetdata.Add(p);
            });

            s.swe_close();
            return planetdata;
        }

        // カスプを計算
        public double[] CuspCalc(int year, int month, int day, int hour, int min, double sec, double lat, double lng, int house)
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
            s.swe_utc_time_zone(year, month, day, hour, min, sec, 9.0, ref utc_year, ref utc_month, ref utc_day, ref utc_hour, ref utc_minute, ref utc_second);
            s.swe_utc_to_jd(utc_year, utc_month, utc_day, utc_hour, utc_minute, utc_second, 1, dret, ref serr);

            double[] cusps = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            double[] ascmc = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            if (house == 0)
            {
                s.swe_houses(dret[1], lat, lng, 'P', cusps, ascmc);
            }
            else if (house == 1)
            {
                s.swe_houses(dret[1], lat, lng, 'K', cusps, ascmc);
            }
            else if (house == 2)
            {
                s.swe_houses(dret[1], lat, lng, 'C', cusps, ascmc);
            }
            else
            {
                s.swe_houses(dret[1], lat, lng, 'E', cusps, ascmc);
            }
            s.swe_close();

            return cusps;
        }

        // 一度一年法
        // 一年を365.24日で計算、当然ずれが生じる
        // 面倒なのでとりあえず放置
        public List<PlanetData> PrimaryProgressionCalc(List<PlanetData> natallist, DateTime natalTime, DateTime transitTime)
        {
            List<PlanetData> progresslist = new List<PlanetData>();
            TimeSpan ts = transitTime - natalTime;
            double years = ts.TotalDays / year_days;
            natallist.ForEach(data =>
            {
                PlanetData progressdata = new PlanetData() { absolute_position = data.absolute_position, no = data.no, sensitive = data.sensitive, speed = data.speed,
                    aspects = new List<AspectInfo>(),
                    secondAspects = new List<AspectInfo>(),
                    thirdAspects= new List<AspectInfo>() };
                progressdata.absolute_position += years;
                progressdata.absolute_position %= 365;
                progresslist.Add(progressdata);
            });

            return progresslist;
        }

        // 一日一年法
        // 一年を365.24日で計算、当然ずれが生じる
        // 面倒なのでとりあえず放置
        public List<PlanetData> SecondaryProgressionCalc(List<PlanetData> natallist, DateTime natalTime, DateTime transitTime)
        {
            List<PlanetData> progresslist = new List<PlanetData>();
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
            s.swe_utc_time_zone(newTime.Year, newTime.Month, newTime.Day, newTime.Hour, newTime.Minute, newTime.Second, 9.0, ref utc_year, ref utc_month, ref utc_day, ref utc_hour, ref utc_minute, ref utc_second);
            s.swe_utc_to_jd(utc_year, utc_month, utc_day, utc_hour, utc_minute, utc_second, 1, dret, ref serr);

            natallist.ForEach(data =>
            {
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
                    s.swe_calc_ut(dret[1], data.no, flag, x, ref serr);
                    // tropicalからayanamsaをマイナス
                    x[0] = x[0] > daya ? x[0] - daya : x[0] - daya + 360;
                }
                else
                {
                    // Universal Timeで計算, 結果はxに入る
                    s.swe_calc_ut(dret[1], data.no, flag, x, ref serr);
                }

                PlanetData progressdata = new PlanetData() { absolute_position = x[0], no = data.no, sensitive = data.sensitive, speed = data.speed,
                    aspects = new List<AspectInfo>(),
                    secondAspects = new List<AspectInfo>(),
                    thirdAspects = new List<AspectInfo>()
                };
                progresslist.Add(progressdata);

            });

            return progresslist;
        }

        // CPS
        // 月、水星、金星、太陽は一日一年法
        // 火星以降および感受点は一度一年法
        // 一年を365.24日で計算、当然ずれが生じる
        // 面倒なのでとりあえず放置
        public List<PlanetData> CompositProgressionCalc(List<PlanetData> natallist, DateTime natalTime, DateTime transitTime)
        {
            List<PlanetData> progresslist = new List<PlanetData>();
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
            s.swe_utc_time_zone(newTime.Year, newTime.Month, newTime.Day, newTime.Hour, newTime.Minute, newTime.Second, 9.0, ref utc_year, ref utc_month, ref utc_day, ref utc_hour, ref utc_minute, ref utc_second);
            s.swe_utc_to_jd(utc_year, utc_month, utc_day, utc_hour, utc_minute, utc_second, 1, dret, ref serr);

            natallist.ForEach(data =>
            {
                PlanetData progressdata;
                if ((data.no != CommonData.ZODIAC_MOON) && 
                    (data.no != CommonData.ZODIAC_MERCURY) && 
                    (data.no != CommonData.ZODIAC_VENUS) && 
                    (data.no != CommonData.ZODIAC_SUN))
                {
                    progressdata = new PlanetData() { absolute_position = data.absolute_position, no = data.no, sensitive = data.sensitive, speed = data.speed,
                        aspects = new List<AspectInfo>(), secondAspects = new List<AspectInfo>(), thirdAspects = new List<AspectInfo>() };
                    progressdata.absolute_position += years;
                    progressdata.absolute_position %= 365;
                    progresslist.Add(progressdata);
                    return;
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
                    s.swe_calc_ut(dret[1], data.no, flag, x, ref serr);
                    // tropicalからayanamsaをマイナス
                    x[0] = x[0] > daya ? x[0] - daya : x[0] - daya + 360;
                }
                else
                {
                    // Universal Timeで計算, 結果はxに入る
                    s.swe_calc_ut(dret[1], data.no, flag, x, ref serr);
                }

                progressdata = new PlanetData() { absolute_position = x[0], no = data.no, sensitive = data.sensitive, speed = data.speed, aspects = new List<AspectInfo>(),
                    secondAspects = new List<AspectInfo>(), thirdAspects = new List<AspectInfo>() };
                progresslist.Add(progressdata);

            });


            return progresslist;
        }

        // 同じリストのアスペクトを計算する
        public List<PlanetData> AspectCalcSame(AspectSetting a_setting, List<PlanetData> list)
        {
            // if (natal-natal)
            for (int i = 0; i < list.Count - 2; i++)
            {
                if (!list[i].isAspectDisp)
                {
                    continue;
                }
                for (int j = i + 1; j < list.Count - 1; j++)
                {
                    if (!list[j].isAspectDisp)
                    {
                        continue;
                    }

                    // 90.0 と　300.0では210度ではなく150度にならなければいけない
                    double aspect_degree = list[i].absolute_position - list[j].absolute_position;

                    if (aspect_degree > 180)
                    {
                        aspect_degree = list[j].absolute_position + 360 - list[i].absolute_position;
                    }
                    if (aspect_degree < 0)
                    {
                        aspect_degree = Math.Abs(aspect_degree);
                    }

                    foreach (AspectKind kind in Enum.GetValues(typeof(AspectKind)))
                    {
                        if (i == CommonData.ZODIAC_SUN)
                        {
                            if (kind == AspectKind.CONJUNCTION ||
                                kind == AspectKind.OPPOSITION ||
                                kind == AspectKind.TRINE ||
                                kind == AspectKind.SQUARE ||
                                kind == AspectKind.SESQUIQUADRATE)
                            {
                                if (aspect_degree < getDegree(kind) + a_setting.orb_nn_sun_soft_1st &&
                                    aspect_degree > getDegree(kind) - a_setting.orb_nn_sun_soft_1st)
                                {
                                    list[i].aspects.Add(new AspectInfo() { targetPosition = list[j].absolute_position, aspectKind = kind, softHard = 2, targetPlanetNo = list[j].no });
                                    break;
                                }
                                else if (aspect_degree < getDegree(kind) + a_setting.orb_nn_sun_hard_1st &&
                                  aspect_degree > getDegree(kind) - a_setting.orb_nn_sun_hard_1st)
                                {
                                    list[i].aspects.Add(new AspectInfo() { targetPosition = list[j].absolute_position, aspectKind = kind, softHard = 1, targetPlanetNo = list[j].no });
                                    break;
                                }
                            }
                            else if (kind == AspectKind.INCONJUNCT)
                            {
                                if (aspect_degree < getDegree(kind) + a_setting.orb_nn_sun_soft_150 &&
                                    aspect_degree > getDegree(kind) - a_setting.orb_nn_sun_soft_150)
                                {
                                    list[i].aspects.Add(new AspectInfo() { targetPosition = list[j].absolute_position, aspectKind = kind, softHard = 2, targetPlanetNo = list[j].no });
                                    break;
                                }
                                else if (aspect_degree < getDegree(kind) + a_setting.orb_nn_sun_hard_150 &&
                                  aspect_degree > getDegree(kind) - a_setting.orb_nn_sun_hard_150)
                                {
                                    list[i].aspects.Add(new AspectInfo() { targetPosition = list[j].absolute_position, aspectKind = kind, softHard = 1, targetPlanetNo = list[j].no });
                                    break;
                                }
                            }
                            else
                            {
                                if (aspect_degree < getDegree(kind) + a_setting.orb_nn_sun_soft_2nd &&
                                    aspect_degree > getDegree(kind) - a_setting.orb_nn_sun_soft_2nd)
                                {
                                    list[i].aspects.Add(new AspectInfo() { targetPosition = list[j].absolute_position,
                                        aspectKind = kind, softHard = 2, targetPlanetNo = list[j].no });
                                    break;
                                }
                                else if (aspect_degree < getDegree(kind) + a_setting.orb_nn_sun_hard_2nd &&
                                  aspect_degree > getDegree(kind) - a_setting.orb_nn_sun_hard_2nd)
                                {
                                    list[i].aspects.Add(new AspectInfo() { targetPosition = list[j].absolute_position,
                                        aspectKind = kind, softHard = 1, targetPlanetNo = list[j].no });
                                    break;
                                }
                            }

                        }
                        else if (i == CommonData.ZODIAC_MOON)
                        {
                            if (kind == AspectKind.CONJUNCTION ||
                                kind == AspectKind.OPPOSITION ||
                                kind == AspectKind.TRINE ||
                                kind == AspectKind.SQUARE ||
                                kind == AspectKind.SESQUIQUADRATE)
                            {
                                if (aspect_degree < getDegree(kind) + a_setting.orb_nn_moon_hard_1st &&
                                    aspect_degree > getDegree(kind) - a_setting.orb_nn_moon_hard_1st)
                                {
                                    list[i].aspects.Add(new AspectInfo() { targetPosition = list[j].absolute_position,
                                        aspectKind = kind, softHard = 2, targetPlanetNo = list[j].no });
                                    break;
                                }
                                else if (aspect_degree < getDegree(kind) + a_setting.orb_nn_moon_hard_1st &&
                                    aspect_degree > getDegree(kind) - a_setting.orb_nn_moon_hard_1st)
                                {
                                    list[i].aspects.Add(new AspectInfo() { targetPosition = list[j].absolute_position,
                                        aspectKind = kind, softHard = 1, targetPlanetNo = list[j].no });
                                    break;
                                }
                            }
                            else if (kind == AspectKind.INCONJUNCT)
                            {
                                if (aspect_degree < getDegree(kind) + a_setting.orb_nn_moon_hard_150 &&
                                    aspect_degree > getDegree(kind) - a_setting.orb_nn_moon_hard_150)
                                {
                                    list[i].aspects.Add(new AspectInfo() { targetPosition = list[j].absolute_position,
                                        aspectKind = kind,
                                        softHard = 2, targetPlanetNo = list[j].no });
                                    break;
                                }
                                else if (aspect_degree < getDegree(kind) + a_setting.orb_nn_moon_hard_150 &&
                                    aspect_degree > getDegree(kind) - a_setting.orb_nn_moon_hard_150)
                                {
                                    list[i].aspects.Add(new AspectInfo() { targetPosition = list[j].absolute_position,
                                        aspectKind = kind,
                                        softHard = 1,
                                        targetPlanetNo = list[j].no });
                                    break;
                                }
                            }
                            else
                            {
                                if (aspect_degree < getDegree(kind) + a_setting.orb_nn_moon_hard_2nd &&
                                    aspect_degree > getDegree(kind) - a_setting.orb_nn_moon_hard_2nd)
                                {
                                    list[i].aspects.Add(new AspectInfo() { targetPosition = list[j].absolute_position,
                                        aspectKind = kind,
                                        softHard = 2,
                                        targetPlanetNo = list[j].no });
                                    break;
                                }
                                else if (aspect_degree < getDegree(kind) + a_setting.orb_nn_moon_hard_2nd &&
                                    aspect_degree > getDegree(kind) - a_setting.orb_nn_moon_hard_2nd)
                                {
                                    list[i].aspects.Add(new AspectInfo() { targetPosition = list[j].absolute_position,
                                        aspectKind = kind, softHard = 1,
                                        targetPlanetNo = list[j].no });
                                    break;
                                }
                            }

                        }
                        else
                        {
                            if (kind == AspectKind.CONJUNCTION ||
                                kind == AspectKind.OPPOSITION ||
                                kind == AspectKind.TRINE ||
                                kind == AspectKind.SQUARE ||
                                kind == AspectKind.SESQUIQUADRATE)
                            {
                                if (aspect_degree < getDegree(kind) + a_setting.orb_nn_other_soft_1st &&
                                    aspect_degree > getDegree(kind) - a_setting.orb_nn_other_soft_1st)
                                {
                                    list[i].aspects.Add(new AspectInfo() { targetPosition = list[j].absolute_position,
                                        aspectKind = kind,
                                        softHard = 2,
                                        targetPlanetNo = list[j].no });
                                    break;
                                }
                                else if (aspect_degree < getDegree(kind) + a_setting.orb_nn_other_hard_1st &&
                                    aspect_degree > getDegree(kind) - a_setting.orb_nn_other_hard_1st)
                                {
                                    list[i].aspects.Add(new AspectInfo() { targetPosition = list[j].absolute_position,
                                        aspectKind = kind,
                                        softHard = 1,
                                        targetPlanetNo = list[j].no });
                                    break;
                                }
                            }
                            else if (kind == AspectKind.INCONJUNCT)
                            {
                                if (aspect_degree < getDegree(kind) + a_setting.orb_nn_other_soft_150 &&
                                    aspect_degree > getDegree(kind) - a_setting.orb_nn_other_soft_150)
                                {
                                    list[i].aspects.Add(new AspectInfo() { targetPosition = list[j].absolute_position,
                                        aspectKind = kind, softHard = 2,
                                        targetPlanetNo = list[j].no });
                                    break;
                                }
                                else if (aspect_degree < getDegree(kind) + a_setting.orb_nn_other_hard_150 &&
                                    aspect_degree > getDegree(kind) - a_setting.orb_nn_other_hard_150)
                                {
                                    list[i].aspects.Add(new AspectInfo() { targetPosition = list[j].absolute_position,
                                        aspectKind = kind, softHard = 1, targetPlanetNo = list[j].no });
                                    break;
                                }
                            }
                            else
                            {
                                if (aspect_degree < getDegree(kind) + a_setting.orb_nn_other_soft_2nd &&
                                    aspect_degree > getDegree(kind) - a_setting.orb_nn_other_soft_2nd)
                                {
                                    list[i].aspects.Add(new AspectInfo() { targetPosition = list[j].absolute_position,
                                        aspectKind = kind, softHard = 2, targetPlanetNo = list[j].no });
                                    break;
                                }
                                else if (aspect_degree < getDegree(kind) + a_setting.orb_nn_other_hard_2nd &&
                                    aspect_degree > getDegree(kind) - a_setting.orb_nn_other_hard_2nd)
                                {
                                    list[i].aspects.Add(new AspectInfo() { targetPosition = list[j].absolute_position,
                                        aspectKind = kind, softHard = 1, targetPlanetNo = list[j].no });
                                    break;
                                }
                            }
                        }
                    }

                }
            }

            return list;
        }

        // 違うリストのアスペクトを計算する
        public List<PlanetData> AspectCalcOther(AspectSetting a_setting, List<PlanetData> fromList, List<PlanetData> toList, int listKind)
        {
            // if (natal-natal)
            for (int i = 0; i < fromList.Count - 1; i++)
            {
                for (int j = 0; j < toList.Count - 1; j++)
                {
                    // 90.0 と　300.0では210度ではなく150度にならなければいけない
                    double aspect_degree = fromList[i].absolute_position - toList[j].absolute_position;

                    if (aspect_degree > 180)
                    {
                        aspect_degree = fromList[j].absolute_position + 360 - toList[i].absolute_position;
                    }
                    if (aspect_degree < 0)
                    {
                        aspect_degree = Math.Abs(aspect_degree);
                    }


                    foreach (AspectKind kind in Enum.GetValues(typeof(AspectKind)))
                    {
                        if (listKind == 1)
                        {
                            // progress
                            if (i == CommonData.ZODIAC_SUN)
                            {
                                if (aspect_degree < getDegree(kind) + a_setting.orb_nn_sun_soft_1st &&
                                    aspect_degree > getDegree(kind) - a_setting.orb_nn_sun_soft_1st)
                                {
                                    fromList[i].secondAspects.Add(new AspectInfo() { targetPosition = toList[j].absolute_position,
                                        aspectKind = kind,
                                        softHard = 2,
                                        targetPlanetNo = toList[j].no });
                                    break;
                                }
                                else if (aspect_degree < getDegree(kind) + a_setting.orb_nn_sun_hard_1st &&
                                  aspect_degree > getDegree(kind) - a_setting.orb_nn_sun_hard_1st)
                                {
                                    fromList[i].secondAspects.Add(new AspectInfo() { targetPosition = toList[j].absolute_position,
                                        aspectKind = kind,
                                        softHard = 1, targetPlanetNo = toList[j].no });
                                    break;
                                }
                            }
                            else if (i == CommonData.ZODIAC_MOON)
                            {
                                if (aspect_degree < getDegree(kind) + a_setting.orb_nn_moon_soft_1st &&
                                    aspect_degree > getDegree(kind) - a_setting.orb_nn_moon_soft_1st)
                                {
                                    fromList[i].secondAspects.Add(new AspectInfo() { targetPosition = toList[j].absolute_position,
                                        aspectKind = kind, softHard = 2,
                                        targetPlanetNo = toList[j].no });
                                    break;
                                }
                                else if (aspect_degree < getDegree(kind) + a_setting.orb_nn_moon_hard_1st &&
                                  aspect_degree > getDegree(kind) - a_setting.orb_nn_moon_hard_1st)
                                {
                                    fromList[i].secondAspects.Add(new AspectInfo() { targetPosition = toList[j].absolute_position,
                                        aspectKind = kind,
                                        softHard = 1,
                                        targetPlanetNo = toList[j].no });
                                    break;
                                }

                            }
                            else
                            {
                                if (aspect_degree < getDegree(kind) + a_setting.orb_nn_other_soft_1st &&
                                    aspect_degree > getDegree(kind) - a_setting.orb_nn_other_soft_1st)
                                {
                                    fromList[i].secondAspects.Add(new AspectInfo() { targetPosition = toList[j].absolute_position,
                                        aspectKind = kind,
                                        softHard = 2,
                                        targetPlanetNo = toList[j].no });
                                    break;
                                }
                                else if (aspect_degree < getDegree(kind) + a_setting.orb_nn_other_hard_1st &&
                                     aspect_degree > getDegree(kind) - a_setting.orb_nn_other_hard_1st)
                                {
                                    fromList[i].secondAspects.Add(new AspectInfo() { targetPosition = toList[j].absolute_position,
                                        aspectKind = kind,
                                        softHard = 1, targetPlanetNo = toList[j].no });
                                    break;
                                }

                            }
                        }
                        else
                        {
                            // transit
                            if (i == CommonData.ZODIAC_SUN)
                            {
                                if (aspect_degree < getDegree(kind) + a_setting.orb_nn_sun_soft_1st &&
                                    aspect_degree > getDegree(kind) - a_setting.orb_nn_sun_soft_1st)
                                {
                                    fromList[i].thirdAspects.Add(new AspectInfo() { targetPosition = toList[j].absolute_position,
                                        aspectKind = kind, softHard = 2, targetPlanetNo = toList[j].no });
                                    break;
                                }
                                else if (aspect_degree < getDegree(kind) + a_setting.orb_nn_sun_hard_1st &&
                                    aspect_degree > getDegree(kind) - a_setting.orb_nn_sun_hard_1st)
                                {
                                    fromList[i].thirdAspects.Add(new AspectInfo() { targetPosition = toList[j].absolute_position,
                                        aspectKind = kind, softHard = 1, targetPlanetNo = toList[j].no });
                                    break;
                                }
                            }
                            else if (i == CommonData.ZODIAC_MOON)
                            {
                                if (aspect_degree < getDegree(kind) + a_setting.orb_nn_moon_soft_1st &&
                                    aspect_degree > getDegree(kind) - a_setting.orb_nn_moon_soft_1st)
                                {
                                    fromList[i].thirdAspects.Add(new AspectInfo() { targetPosition = toList[j].absolute_position,
                                        aspectKind = kind, softHard = 2, targetPlanetNo = toList[j].no });
                                    break;
                                }
                                else if (aspect_degree < getDegree(kind) + a_setting.orb_nn_moon_hard_1st &&
                                    aspect_degree > getDegree(kind) - a_setting.orb_nn_moon_hard_1st)
                                {
                                    fromList[i].thirdAspects.Add(new AspectInfo() { targetPosition = toList[j].absolute_position,
                                        aspectKind = kind, softHard = 1, targetPlanetNo = toList[j].no });
                                    break;
                                }

                            }
                            else
                            {
                                if (aspect_degree < getDegree(kind) + a_setting.orb_nn_other_soft_1st &&
                                    aspect_degree > getDegree(kind) - a_setting.orb_nn_other_soft_1st)
                                {
                                    fromList[i].thirdAspects.Add(new AspectInfo() { targetPosition = toList[j].absolute_position,
                                        aspectKind = kind, softHard = 2, targetPlanetNo = toList[j].no });
                                    break;
                                }
                                else if (aspect_degree < getDegree(kind) + a_setting.orb_nn_other_hard_1st &&
                                    aspect_degree > getDegree(kind) - a_setting.orb_nn_other_hard_1st)
                                {
                                    fromList[i].thirdAspects.Add(new AspectInfo() { targetPosition = toList[j].absolute_position,
                                        aspectKind = kind,
                                        softHard = 1,
                                        targetPlanetNo = toList[j].no });
                                    break;
                                }

                            }
                        }
                    }

                }
            }

            return fromList;
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
