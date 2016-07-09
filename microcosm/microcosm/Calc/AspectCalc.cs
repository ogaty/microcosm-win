﻿using System;
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
        public MainWindow main;

        public AspectCalc(MainWindow main)
        {
            this.main = main;
        }

        // 同じリストのアスペクトを計算する
        public List<PlanetData> AspectCalcSame(SettingData a_setting, List<PlanetData> list)
        {
            // if (natal-natal)
            for (int i = 0; i < list.Count - 1; i++)
            {
                if (!list[i].isAspectDisp)
                {
                    continue;
                }
                for (int j = i + 1; j < list.Count; j++)
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
                                if (aspect_degree < getDegree(kind) + a_setting.orbs[0][OrbKind.SUN_HARD_1ST] &&
                                    aspect_degree > getDegree(kind) - a_setting.orbs[0][OrbKind.SUN_HARD_1ST])
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
                                else if (aspect_degree < getDegree(kind) + a_setting.orbs[0][OrbKind.SUN_SOFT_1ST] &&
                                  aspect_degree > getDegree(kind) - a_setting.orbs[0][OrbKind.SUN_SOFT_1ST])
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
                                if (aspect_degree < getDegree(kind) + a_setting.orbs[0][OrbKind.SUN_HARD_150] &&
                                    aspect_degree > getDegree(kind) - a_setting.orbs[0][OrbKind.SUN_HARD_150])
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
                                else if (aspect_degree < getDegree(kind) + a_setting.orbs[0][OrbKind.SUN_SOFT_150] &&
                                  aspect_degree > getDegree(kind) - a_setting.orbs[0][OrbKind.SUN_SOFT_150])
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
                                if (aspect_degree < getDegree(kind) + a_setting.orbs[0][OrbKind.SUN_HARD_2ND] &&
                                    aspect_degree > getDegree(kind) - a_setting.orbs[0][OrbKind.SUN_HARD_2ND])
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
                                else if (aspect_degree < getDegree(kind) + a_setting.orbs[0][OrbKind.SUN_SOFT_2ND] &&
                                  aspect_degree > getDegree(kind) - a_setting.orbs[0][OrbKind.SUN_SOFT_2ND])
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
                                if (aspect_degree < getDegree(kind) + a_setting.orbs[0][OrbKind.MOON_HARD_1ST] &&
                                    aspect_degree > getDegree(kind) - a_setting.orbs[0][OrbKind.MOON_HARD_1ST])
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
                                else if (aspect_degree < getDegree(kind) + a_setting.orbs[0][OrbKind.MOON_SOFT_1ST] &&
                                    aspect_degree > getDegree(kind) - a_setting.orbs[0][OrbKind.MOON_SOFT_1ST])
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
                                // Console.WriteLine(aspect_degree);
                                // Console.WriteLine(getDegree(kind) + a_setting.orbs[0][OrbKind.MOON_HARD_150]);
                                // Console.WriteLine(getDegree(kind) - a_setting.orbs[0][OrbKind.MOON_HARD_150]);
                                // Console.WriteLine("");
                                if (aspect_degree < getDegree(kind) + a_setting.orbs[0][OrbKind.MOON_HARD_150] &&
                                    aspect_degree > getDegree(kind) - a_setting.orbs[0][OrbKind.MOON_HARD_150])
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
                                else if (aspect_degree < getDegree(kind) + a_setting.orbs[0][OrbKind.MOON_SOFT_150] &&
                                    aspect_degree > getDegree(kind) - a_setting.orbs[0][OrbKind.MOON_SOFT_150])
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
                                if (aspect_degree < getDegree(kind) + a_setting.orbs[0][OrbKind.MOON_HARD_2ND] &&
                                    aspect_degree > getDegree(kind) - a_setting.orbs[0][OrbKind.MOON_HARD_2ND])
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
                                else if (aspect_degree < getDegree(kind) + a_setting.orbs[0][OrbKind.MOON_SOFT_2ND] &&
                                    aspect_degree > getDegree(kind) - a_setting.orbs[0][OrbKind.MOON_SOFT_2ND])
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
                                if (aspect_degree < getDegree(kind) + a_setting.orbs[0][OrbKind.OTHER_HARD_1ST] &&
                                    aspect_degree > getDegree(kind) - a_setting.orbs[0][OrbKind.OTHER_HARD_1ST])
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
                                else if (aspect_degree < getDegree(kind) + a_setting.orbs[0][OrbKind.OTHER_SOFT_1ST] &&
                                    aspect_degree > getDegree(kind) - a_setting.orbs[0][OrbKind.OTHER_SOFT_1ST])
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
                                if (aspect_degree < getDegree(kind) + a_setting.orbs[0][OrbKind.OTHER_HARD_150] &&
                                    aspect_degree > getDegree(kind) - a_setting.orbs[0][OrbKind.OTHER_HARD_150])
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
                                else if (aspect_degree < getDegree(kind) + a_setting.orbs[0][OrbKind.OTHER_SOFT_150] &&
                                    aspect_degree > getDegree(kind) - a_setting.orbs[0][OrbKind.OTHER_SOFT_150])
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
                                if (aspect_degree < getDegree(kind) + a_setting.orbs[0][OrbKind.OTHER_HARD_2ND] &&
                                    aspect_degree > getDegree(kind) - a_setting.orbs[0][OrbKind.OTHER_HARD_2ND])
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
                                else if (aspect_degree < getDegree(kind) + a_setting.orbs[0][OrbKind.OTHER_SOFT_2ND] &&
                                    aspect_degree > getDegree(kind) - a_setting.orbs[0][OrbKind.OTHER_SOFT_2ND])
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
        // listKindはpositionを決める
        // 1: from1 to1
        // 2: from2 to2
        // 3: from3 to3
        // 4: from1 to2
        // 5: from1 to3
        // 6: from2 to3
        // 7: from4 to4
        // 8: from5 to5
        // 9: from1 to4
        // 10: from1 to5
        // 11: from2 to4
        // 12: from2 to5
        // 13: from3 to4
        // 14: from3 to5
        // 15: from4 to5
        public List<PlanetData> AspectCalcOther(SettingData a_setting, 
            List<PlanetData> fromList, List<PlanetData> toList, 
            int listKind)
        {
            int n = 0; // TODO
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
                        if (listKind == 4)
                        {
                            // 1-2
                            if (i == CommonData.ZODIAC_SUN)
                            {
                                if (aspect_degree < getDegree(kind) + a_setting.orbs[n][OrbKind.SUN_HARD_1ST] &&
                                    aspect_degree > getDegree(kind) - a_setting.orbs[n][OrbKind.SUN_HARD_1ST])
                                {
                                    fromList[i].secondAspects.Add(new AspectInfo() { targetPosition = toList[j].absolute_position, aspectKind = kind,
                                        softHard = SoftHard.SOFT, targetPlanetNo = toList[j].no });
                                    break;
                                }
                                else if (aspect_degree < getDegree(kind) + a_setting.orbs[n][OrbKind.SUN_SOFT_1ST] &&
                                  aspect_degree > getDegree(kind) - a_setting.orbs[n][OrbKind.SUN_SOFT_1ST])
                                {
                                    fromList[i].secondAspects.Add(new AspectInfo() { targetPosition = toList[j].absolute_position, aspectKind = kind,
                                        softHard = SoftHard.HARD, targetPlanetNo = toList[j].no });
                                    break;
                                }
                            }
                            else if (i == CommonData.ZODIAC_MOON)
                            {
                                if (aspect_degree < getDegree(kind) + a_setting.orbs[n][OrbKind.MOON_HARD_1ST] &&
                                    aspect_degree > getDegree(kind) - a_setting.orbs[n][OrbKind.MOON_HARD_1ST])
                                {
                                    fromList[i].secondAspects.Add(new AspectInfo() { targetPosition = toList[j].absolute_position, aspectKind = kind,
                                        softHard = SoftHard.SOFT, targetPlanetNo = toList[j].no });
                                    break;
                                }
                                else if (aspect_degree < getDegree(kind) + a_setting.orbs[n][OrbKind.MOON_HARD_1ST] &&
                                  aspect_degree > getDegree(kind) - a_setting.orbs[n][OrbKind.MOON_HARD_1ST])
                                {
                                    fromList[i].secondAspects.Add(new AspectInfo() { targetPosition = toList[j].absolute_position, aspectKind = kind,
                                        softHard = SoftHard.HARD, targetPlanetNo = toList[j].no });
                                    break;
                                }

                            }
                            else
                            {
                                if (aspect_degree < getDegree(kind) + a_setting.orbs[n][OrbKind.OTHER_HARD_1ST] &&
                                    aspect_degree > getDegree(kind) - a_setting.orbs[n][OrbKind.OTHER_HARD_1ST])
                                {
                                    fromList[i].secondAspects.Add(new AspectInfo() { targetPosition = toList[j].absolute_position, aspectKind = kind,
                                        softHard = SoftHard.SOFT, targetPlanetNo = toList[j].no });
                                    break;
                                }
                                else if (aspect_degree < getDegree(kind) + a_setting.orbs[n][OrbKind.OTHER_SOFT_1ST] &&
                                     aspect_degree > getDegree(kind) - a_setting.orbs[n][OrbKind.OTHER_SOFT_1ST])
                                {
                                    fromList[i].secondAspects.Add(new AspectInfo() { targetPosition = toList[j].absolute_position, aspectKind = kind,
                                        softHard = SoftHard.HARD, targetPlanetNo = toList[j].no });
                                    break;
                                }

                            }
                        }
                        else
                        {
                            // 今はこれで
                            // 1-3
                            // 2-3
                            if (i == CommonData.ZODIAC_SUN)
                            {
                                if (aspect_degree < getDegree(kind) + a_setting.orbs[n][OrbKind.SUN_HARD_1ST] &&
                                    aspect_degree > getDegree(kind) - a_setting.orbs[n][OrbKind.SUN_HARD_1ST])
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
                                else if (aspect_degree < getDegree(kind) + a_setting.orbs[n][OrbKind.SUN_SOFT_1ST] &&
                                    aspect_degree > getDegree(kind) - a_setting.orbs[n][OrbKind.SUN_SOFT_1ST])
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
                                if (aspect_degree < getDegree(kind) + a_setting.orbs[n][OrbKind.MOON_HARD_1ST] &&
                                    aspect_degree > getDegree(kind) - a_setting.orbs[n][OrbKind.MOON_HARD_1ST])
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
                                else if (aspect_degree < getDegree(kind) + a_setting.orbs[n][OrbKind.OTHER_HARD_1ST] &&
                                    aspect_degree > getDegree(kind) - a_setting.orbs[n][OrbKind.OTHER_HARD_1ST])
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
                                if (aspect_degree < getDegree(kind) + a_setting.orbs[n][OrbKind.OTHER_HARD_2ND] &&
                                    aspect_degree > getDegree(kind) - a_setting.orbs[n][OrbKind.OTHER_HARD_2ND])
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
                                else if (aspect_degree < getDegree(kind) + a_setting.orbs[n][OrbKind.OTHER_SOFT_2ND] &&
                                    aspect_degree > getDegree(kind) - a_setting.orbs[n][OrbKind.OTHER_SOFT_2ND])
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
