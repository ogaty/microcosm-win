using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using microcosm.Aspect;
using microcosm.Planet;
using microcosm.Setting;
using microcosm.Common;
using microcosm.Config;

namespace microcosm.Calc
{
    public class AspectCalc
    {
        // 同じリストのアスペクトを計算する
        public List<PlanetData> AspectCalcSame(SettingData a_setting, List<PlanetData> list)
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
                    // アスペクトは180°以上にはならない
                    double from;
                    double to;
                    if (list[i].absolute_position - list[j].absolute_position < 0) {
                        to = list[j].absolute_position;
                        from = list[i].absolute_position;
                    }
                    else
                    {
                        to = list[i].absolute_position;
                        from = list[j].absolute_position;
                    }
                    double aspect_degree = to - from;
                    if (aspect_degree > 180)
                    {
                        aspect_degree = 360 + from - to;
                    }

                    foreach (AspectKind kind in Enum.GetValues(typeof(AspectKind)))
                    {
                        if (i == CommonData.ZODIAC_SUN)
                        {
                            if (kind == AspectKind.CONJUNCTION ||
                                kind == AspectKind.OPPOSITION ||
                                kind == AspectKind.TRINE ||
                                kind == AspectKind.SQUARE ||
                                kind == AspectKind.SEXTILE)
                            {
                                if (aspect_degree < getDegree(kind) + a_setting.orb_sun_hard_1st[0, 0] &&
                                    aspect_degree > getDegree(kind) - a_setting.orb_sun_hard_1st[0, 0])
                                {
                                    list[i].aspects.Add(new AspectInfo()
                                    {
                                        targetPosition = list[j].absolute_position,
                                        aspectKind = kind,
                                        softHard = SoftHard.HARD,
                                        targetPlanetNo = list[j].no
                                    });
                                    break;
                                }
                                else if (aspect_degree < getDegree(kind) + a_setting.orb_sun_soft_1st[0, 0] &&
                                  aspect_degree > getDegree(kind) - a_setting.orb_sun_soft_1st[0, 0])
                                {
                                    list[i].aspects.Add(new AspectInfo()
                                    {
                                        targetPosition = list[j].absolute_position,
                                        aspectKind = kind,
                                        softHard = SoftHard.SOFT,
                                        targetPlanetNo = list[j].no
                                    });
                                    break;
                                }
                            }
                            else if (kind == AspectKind.INCONJUNCT ||
                                kind == AspectKind.SESQUIQUADRATE)
                            {
                                if (aspect_degree < getDegree(kind) + a_setting.orb_sun_hard_150[0, 0] &&
                                    aspect_degree > getDegree(kind) - a_setting.orb_sun_hard_150[0, 0])
                                {
                                    list[i].aspects.Add(new AspectInfo()
                                    {
                                        targetPosition = list[j].absolute_position,
                                        aspectKind = kind,
                                        softHard = SoftHard.HARD,
                                        targetPlanetNo = list[j].no
                                    });
                                    break;
                                }
                                else if (aspect_degree < getDegree(kind) + a_setting.orb_sun_soft_150[0, 0] &&
                                  aspect_degree > getDegree(kind) - a_setting.orb_sun_soft_150[0, 0])
                                {
                                    list[i].aspects.Add(new AspectInfo()
                                    {
                                        targetPosition = list[j].absolute_position,
                                        aspectKind = kind,
                                        softHard = SoftHard.SOFT,
                                        targetPlanetNo = list[j].no
                                    });
                                    break;
                                }
                            }
                            else
                            {
                                if (aspect_degree < getDegree(kind) + a_setting.orb_sun_hard_2nd[0, 0] &&
                                    aspect_degree > getDegree(kind) - a_setting.orb_sun_hard_2nd[0, 0])
                                {
                                    list[i].aspects.Add(new AspectInfo()
                                    {
                                        targetPosition = list[j].absolute_position,
                                        aspectKind = kind,
                                        softHard = SoftHard.HARD,
                                        targetPlanetNo = list[j].no
                                    });
                                    break;
                                }
                                else if (aspect_degree < getDegree(kind) + a_setting.orb_sun_soft_2nd[0, 0] &&
                                  aspect_degree > getDegree(kind) - a_setting.orb_sun_soft_2nd[0, 0])
                                {
                                    list[i].aspects.Add(new AspectInfo()
                                    {
                                        targetPosition = list[j].absolute_position,
                                        aspectKind = kind,
                                        softHard = SoftHard.SOFT,
                                        targetPlanetNo = list[j].no
                                    });
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
                                kind == AspectKind.SEXTILE)
                            {
                                if (aspect_degree < getDegree(kind) + a_setting.orb_moon_hard_1st[0, 0] &&
                                    aspect_degree > getDegree(kind) - a_setting.orb_moon_hard_1st[0, 0])
                                {
                                    list[i].aspects.Add(new AspectInfo()
                                    {
                                        targetPosition = list[j].absolute_position,
                                        aspectKind = kind,
                                        softHard = SoftHard.HARD,
                                        targetPlanetNo = list[j].no
                                    });
                                    break;
                                }
                                else if (aspect_degree < getDegree(kind) + a_setting.orb_moon_soft_1st[0, 0] &&
                                    aspect_degree > getDegree(kind) - a_setting.orb_moon_soft_1st[0, 0])
                                {
                                    list[i].aspects.Add(new AspectInfo()
                                    {
                                        targetPosition = list[j].absolute_position,
                                        aspectKind = kind,
                                        softHard = SoftHard.SOFT,
                                        targetPlanetNo = list[j].no
                                    });
                                    break;
                                }
                            }
                            else if (kind == AspectKind.INCONJUNCT ||
                                kind == AspectKind.SESQUIQUADRATE)
                            {
                                Console.WriteLine(aspect_degree);
                                Console.WriteLine(getDegree(kind) + a_setting.orb_moon_soft_150[0, 0]);
                                Console.WriteLine(getDegree(kind) - a_setting.orb_moon_soft_150[0, 0]);
                                Console.WriteLine("");
                                if (aspect_degree < getDegree(kind) + a_setting.orb_moon_hard_150[0, 0] &&
                                    aspect_degree > getDegree(kind) - a_setting.orb_moon_hard_150[0, 0])
                                {
                                    list[i].aspects.Add(new AspectInfo()
                                    {
                                        targetPosition = list[j].absolute_position,
                                        aspectKind = kind,
                                        softHard = SoftHard.HARD,
                                        targetPlanetNo = list[j].no
                                    });
                                    break;
                                }
                                else if (aspect_degree < getDegree(kind) + a_setting.orb_moon_soft_150[0, 0] &&
                                    aspect_degree > getDegree(kind) - a_setting.orb_moon_soft_150[0, 0])
                                {
                                    list[i].aspects.Add(new AspectInfo()
                                    {
                                        targetPosition = list[j].absolute_position,
                                        aspectKind = kind,
                                        softHard = SoftHard.SOFT,
                                        targetPlanetNo = list[j].no
                                    });
                                    break;
                                }
                            }
                            else
                            {
                                if (aspect_degree < getDegree(kind) + a_setting.orb_moon_hard_2nd[0, 0] &&
                                    aspect_degree > getDegree(kind) - a_setting.orb_moon_hard_2nd[0, 0])
                                {
                                    list[i].aspects.Add(new AspectInfo()
                                    {
                                        targetPosition = list[j].absolute_position,
                                        aspectKind = kind,
                                        softHard = SoftHard.HARD,
                                        targetPlanetNo = list[j].no
                                    });
                                    break;
                                }
                                else if (aspect_degree < getDegree(kind) + a_setting.orb_moon_soft_2nd[0, 0] &&
                                    aspect_degree > getDegree(kind) - a_setting.orb_moon_soft_2nd[0, 0])
                                {
                                    list[i].aspects.Add(new AspectInfo()
                                    {
                                        targetPosition = list[j].absolute_position,
                                        aspectKind = kind,
                                        softHard = SoftHard.SOFT,
                                        targetPlanetNo = list[j].no
                                    });
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
                                kind == AspectKind.SEXTILE)
                            {
                                if (aspect_degree < getDegree(kind) + a_setting.orb_other_hard_1st[0, 0] &&
                                    aspect_degree > getDegree(kind) - a_setting.orb_other_hard_1st[0, 0])
                                {
                                    list[i].aspects.Add(new AspectInfo()
                                    {
                                        targetPosition = list[j].absolute_position,
                                        aspectKind = kind,
                                        softHard = SoftHard.HARD,
                                        targetPlanetNo = list[j].no
                                    });
                                    break;
                                }
                                else if (aspect_degree < getDegree(kind) + a_setting.orb_other_soft_1st[0, 0] &&
                                    aspect_degree > getDegree(kind) - a_setting.orb_other_soft_1st[0, 0])
                                {
                                    list[i].aspects.Add(new AspectInfo()
                                    {
                                        targetPosition = list[j].absolute_position,
                                        aspectKind = kind,
                                        softHard = SoftHard.SOFT,
                                        targetPlanetNo = list[j].no
                                    });
                                    break;
                                }
                            }
                            else if (kind == AspectKind.INCONJUNCT ||
                                kind == AspectKind.SESQUIQUADRATE)
                            {
                                if (aspect_degree < getDegree(kind) + a_setting.orb_other_hard_150[0, 0] &&
                                    aspect_degree > getDegree(kind) - a_setting.orb_other_hard_150[0, 0])
                                {
                                    list[i].aspects.Add(new AspectInfo()
                                    {
                                        targetPosition = list[j].absolute_position,
                                        aspectKind = kind,
                                        softHard = SoftHard.HARD,
                                        targetPlanetNo = list[j].no
                                    });
                                    break;
                                }
                                else if (aspect_degree < getDegree(kind) + a_setting.orb_other_soft_150[0, 0] &&
                                    aspect_degree > getDegree(kind) - a_setting.orb_other_soft_150[0, 0])
                                {
                                    list[i].aspects.Add(new AspectInfo()
                                    {
                                        targetPosition = list[j].absolute_position,
                                        aspectKind = kind,
                                        softHard = SoftHard.SOFT,
                                        targetPlanetNo = list[j].no
                                    });
                                    break;
                                }
                            }
                            else
                            {
                                if (aspect_degree < getDegree(kind) + a_setting.orb_other_hard_2nd[0, 0] &&
                                    aspect_degree > getDegree(kind) - a_setting.orb_other_hard_2nd[0, 0])
                                {
                                    list[i].aspects.Add(new AspectInfo()
                                    {
                                        targetPosition = list[j].absolute_position,
                                        aspectKind = kind,
                                        softHard = SoftHard.HARD,
                                        targetPlanetNo = list[j].no
                                    });
                                    break;
                                }
                                else if (aspect_degree < getDegree(kind) + a_setting.orb_other_soft_2nd[0, 0] &&
                                    aspect_degree > getDegree(kind) - a_setting.orb_other_soft_2nd[0, 0])
                                {
                                    list[i].aspects.Add(new AspectInfo()
                                    {
                                        targetPosition = list[j].absolute_position,
                                        aspectKind = kind,
                                        softHard = SoftHard.SOFT,
                                        targetPlanetNo = list[j].no
                                    });
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
        // fromListにアスペクトを追加して返却
        public List<PlanetData> AspectCalcOther(SettingData a_setting, List<PlanetData> fromList, List<PlanetData> toList, 
            int listKind)
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
                                if (aspect_degree < getDegree(kind) + a_setting.orb_sun_hard_1st[0, 0] &&
                                    aspect_degree > getDegree(kind) - a_setting.orb_sun_hard_1st[0, 0])
                                {
                                    fromList[i].secondAspects.Add(new AspectInfo() { targetPosition = toList[j].absolute_position, aspectKind = kind,
                                        softHard = SoftHard.SOFT, targetPlanetNo = toList[j].no });
                                    break;
                                }
                                else if (aspect_degree < getDegree(kind) + a_setting.orb_sun_soft_1st[0, 0] &&
                                  aspect_degree > getDegree(kind) - a_setting.orb_sun_soft_1st[0, 0])
                                {
                                    fromList[i].secondAspects.Add(new AspectInfo() { targetPosition = toList[j].absolute_position, aspectKind = kind,
                                        softHard = SoftHard.HARD, targetPlanetNo = toList[j].no });
                                    break;
                                }
                            }
                            else if (i == CommonData.ZODIAC_MOON)
                            {
                                if (aspect_degree < getDegree(kind) + a_setting.orb_moon_hard_1st[0, 0] &&
                                    aspect_degree > getDegree(kind) - a_setting.orb_moon_hard_1st[0, 0])
                                {
                                    fromList[i].secondAspects.Add(new AspectInfo() { targetPosition = toList[j].absolute_position, aspectKind = kind,
                                        softHard = SoftHard.SOFT, targetPlanetNo = toList[j].no });
                                    break;
                                }
                                else if (aspect_degree < getDegree(kind) + a_setting.orb_moon_soft_1st[0, 0] &&
                                  aspect_degree > getDegree(kind) - a_setting.orb_moon_soft_1st[0, 0])
                                {
                                    fromList[i].secondAspects.Add(new AspectInfo() { targetPosition = toList[j].absolute_position, aspectKind = kind,
                                        softHard = SoftHard.HARD, targetPlanetNo = toList[j].no });
                                    break;
                                }

                            }
                            else
                            {
                                if (aspect_degree < getDegree(kind) + a_setting.orb_other_hard_1st[0, 0] &&
                                    aspect_degree > getDegree(kind) - a_setting.orb_other_hard_1st[0, 0])
                                {
                                    fromList[i].secondAspects.Add(new AspectInfo() { targetPosition = toList[j].absolute_position, aspectKind = kind,
                                        softHard = SoftHard.SOFT, targetPlanetNo = toList[j].no });
                                    break;
                                }
                                else if (aspect_degree < getDegree(kind) + a_setting.orb_other_soft_1st[0, 0] &&
                                     aspect_degree > getDegree(kind) - a_setting.orb_other_soft_1st[0, 0])
                                {
                                    fromList[i].secondAspects.Add(new AspectInfo() { targetPosition = toList[j].absolute_position, aspectKind = kind,
                                        softHard = SoftHard.HARD, targetPlanetNo = toList[j].no });
                                    break;
                                }

                            }
                        }
                        else
                        {
                            // transit
                            if (i == CommonData.ZODIAC_SUN)
                            {
                                if (aspect_degree < getDegree(kind) + a_setting.orb_sun_hard_1st[0, 0] &&
                                    aspect_degree > getDegree(kind) - a_setting.orb_sun_hard_1st[0, 0])
                                {
                                    fromList[i].thirdAspects.Add(new AspectInfo()
                                    {
                                        targetPosition = toList[j].absolute_position,
                                        aspectKind = kind,
                                        softHard = SoftHard.SOFT,
                                        targetPlanetNo = toList[j].no
                                    });
                                    break;
                                }
                                else if (aspect_degree < getDegree(kind) + a_setting.orb_sun_soft_1st[0, 0] &&
                                    aspect_degree > getDegree(kind) - a_setting.orb_sun_soft_1st[0, 0])
                                {
                                    fromList[i].thirdAspects.Add(new AspectInfo()
                                    {
                                        targetPosition = toList[j].absolute_position,
                                        aspectKind = kind,
                                        softHard = SoftHard.HARD,
                                        targetPlanetNo = toList[j].no
                                    });
                                    break;
                                }
                            }
                            else if (i == CommonData.ZODIAC_MOON)
                            {
                                if (aspect_degree < getDegree(kind) + a_setting.orb_moon_hard_1st[0, 0] &&
                                    aspect_degree > getDegree(kind) - a_setting.orb_moon_hard_1st[0, 0])
                                {
                                    fromList[i].thirdAspects.Add(new AspectInfo()
                                    {
                                        targetPosition = toList[j].absolute_position,
                                        aspectKind = kind,
                                        softHard = SoftHard.SOFT,
                                        targetPlanetNo = toList[j].no
                                    });
                                    break;
                                }
                                else if (aspect_degree < getDegree(kind) + a_setting.orb_sun_soft_1st[0, 0] &&
                                    aspect_degree > getDegree(kind) - a_setting.orb_sun_soft_1st[0, 0])
                                {
                                    fromList[i].thirdAspects.Add(new AspectInfo()
                                    {
                                        targetPosition = toList[j].absolute_position,
                                        aspectKind = kind,
                                        softHard = SoftHard.HARD,
                                        targetPlanetNo = toList[j].no
                                    });
                                    break;
                                }

                            }
                            else
                            {
                                if (aspect_degree < getDegree(kind) + a_setting.orb_other_hard_1st[0, 0] &&
                                    aspect_degree > getDegree(kind) - a_setting.orb_other_hard_1st[0, 0])
                                {
                                    fromList[i].thirdAspects.Add(new AspectInfo()
                                    {
                                        targetPosition = toList[j].absolute_position,
                                        aspectKind = kind,
                                        softHard = SoftHard.SOFT,
                                        targetPlanetNo = toList[j].no
                                    });
                                    break;
                                }
                                else if (aspect_degree < getDegree(kind) + a_setting.orb_other_soft_1st[0, 0] &&
                                    aspect_degree > getDegree(kind) - a_setting.orb_other_soft_1st[0, 0])
                                {
                                    fromList[i].thirdAspects.Add(new AspectInfo()
                                    {
                                        targetPosition = toList[j].absolute_position,
                                        aspectKind = kind,
                                        softHard = SoftHard.HARD,
                                        targetPlanetNo = toList[j].no
                                    });
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
