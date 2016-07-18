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
                                if (kind == AspectKind.CONJUNCTION && !a_setting.dispAspectCategory[0][AspectKind.CONJUNCTION])
                                {
                                    continue;
                                }
                                if (kind == AspectKind.OPPOSITION && !a_setting.dispAspectCategory[0][AspectKind.OPPOSITION])
                                {
                                    continue;
                                }
                                if (kind == AspectKind.TRINE && !a_setting.dispAspectCategory[0][AspectKind.TRINE])
                                {
                                    continue;
                                }
                                if (kind == AspectKind.SQUARE && !a_setting.dispAspectCategory[0][AspectKind.SQUARE])
                                {
                                    continue;
                                }
                                if (kind == AspectKind.SEXTILE && !a_setting.dispAspectCategory[0][AspectKind.SEXTILE])
                                {
                                    continue;
                                }
                                if (aspect_degree < getDegree(kind) + a_setting.orbs[0][OrbKind.SUN_HARD_1ST] &&
                                    aspect_degree > getDegree(kind) - a_setting.orbs[0][OrbKind.SUN_HARD_1ST])
                                {
                                    list[i].aspects.Add(new AspectInfo()
                                    {
                                        targetPosition = list[j].absolute_position,
                                        aspectKind = kind,
                                        softHard = SoftHard.HARD,
                                        srcPlanetNo = list[i].no,
                                        targetPlanetNo = list[j].no,
                                        absoluteDegree = aspect_degree
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
                                        srcPlanetNo = list[i].no,
                                        targetPlanetNo = list[j].no,
                                        absoluteDegree = aspect_degree
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
                                    if (kind == AspectKind.INCONJUNCT && !a_setting.dispAspectCategory[0][AspectKind.INCONJUNCT])
                                    {
                                        continue;
                                    }
                                    if (kind == AspectKind.SESQUIQUADRATE && !a_setting.dispAspectCategory[0][AspectKind.SESQUIQUADRATE])
                                    {
                                        continue;
                                    }
                                    list[i].aspects.Add(new AspectInfo()
                                    {
                                        targetPosition = list[j].absolute_position,
                                        aspectKind = kind,
                                        softHard = SoftHard.HARD,
                                        srcPlanetNo = list[i].no,
                                        targetPlanetNo = list[j].no,
                                        absoluteDegree = aspect_degree
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
                                        srcPlanetNo = list[i].no,
                                        targetPlanetNo = list[j].no,
                                        absoluteDegree = aspect_degree
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
                                        srcPlanetNo = list[i].no,
                                        targetPlanetNo = list[j].no,
                                        absoluteDegree = aspect_degree
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
                                        srcPlanetNo = list[i].no,
                                        targetPlanetNo = list[j].no,
                                        absoluteDegree = aspect_degree
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
                                if (kind == AspectKind.CONJUNCTION && !a_setting.dispAspectCategory[0][AspectKind.CONJUNCTION])
                                {
                                    continue;
                                }
                                if (kind == AspectKind.OPPOSITION && !a_setting.dispAspectCategory[0][AspectKind.OPPOSITION])
                                {
                                    continue;
                                }
                                if (kind == AspectKind.TRINE && !a_setting.dispAspectCategory[0][AspectKind.TRINE])
                                {
                                    continue;
                                }
                                if (kind == AspectKind.SQUARE && !a_setting.dispAspectCategory[0][AspectKind.SQUARE])
                                {
                                    continue;
                                }
                                if (kind == AspectKind.SEXTILE && !a_setting.dispAspectCategory[0][AspectKind.SEXTILE])
                                {
                                    continue;
                                }

                                if (aspect_degree < getDegree(kind) + a_setting.orbs[0][OrbKind.MOON_HARD_1ST] &&
                                    aspect_degree > getDegree(kind) - a_setting.orbs[0][OrbKind.MOON_HARD_1ST])
                                {
                                    list[i].aspects.Add(new AspectInfo()
                                    {
                                        targetPosition = list[j].absolute_position,
                                        aspectKind = kind,
                                        softHard = SoftHard.HARD,
                                        srcPlanetNo = list[i].no,
                                        targetPlanetNo = list[j].no,
                                        absoluteDegree = aspect_degree
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
                                        srcPlanetNo = list[i].no,
                                        targetPlanetNo = list[j].no,
                                        absoluteDegree = aspect_degree
                                    });
                                    break;
                                }
                            }
                            else if (kind == AspectKind.INCONJUNCT ||
                                kind == AspectKind.SESQUIQUADRATE)
                            {
                                if (kind == AspectKind.INCONJUNCT && !a_setting.dispAspectCategory[0][AspectKind.INCONJUNCT])
                                {
                                    continue;
                                }
                                if (kind == AspectKind.SESQUIQUADRATE && !a_setting.dispAspectCategory[0][AspectKind.SESQUIQUADRATE])
                                {
                                    continue;
                                }

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
                                        srcPlanetNo = list[i].no,
                                        targetPlanetNo = list[j].no,
                                        absoluteDegree = aspect_degree
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
                                        srcPlanetNo = list[i].no,
                                        targetPlanetNo = list[j].no,
                                        absoluteDegree = aspect_degree
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
                                        srcPlanetNo = list[i].no,
                                        targetPlanetNo = list[j].no,
                                        absoluteDegree = aspect_degree
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
                                        srcPlanetNo = list[i].no,
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
                                if (kind == AspectKind.CONJUNCTION && !a_setting.dispAspectCategory[0][AspectKind.CONJUNCTION])
                                {
                                    continue;
                                }
                                if (kind == AspectKind.OPPOSITION && !a_setting.dispAspectCategory[0][AspectKind.OPPOSITION])
                                {
                                    continue;
                                }
                                if (kind == AspectKind.TRINE && !a_setting.dispAspectCategory[0][AspectKind.TRINE])
                                {
                                    continue;
                                }
                                if (kind == AspectKind.SQUARE && !a_setting.dispAspectCategory[0][AspectKind.SQUARE])
                                {
                                    continue;
                                }
                                if (kind == AspectKind.SEXTILE && !a_setting.dispAspectCategory[0][AspectKind.SEXTILE])
                                {
                                    continue;
                                }

                                if (aspect_degree < getDegree(kind) + a_setting.orbs[0][OrbKind.OTHER_HARD_1ST] &&
                                    aspect_degree > getDegree(kind) - a_setting.orbs[0][OrbKind.OTHER_HARD_1ST])
                                {
                                    list[i].aspects.Add(new AspectInfo()
                                    {
                                        targetPosition = list[j].absolute_position,
                                        aspectKind = kind,
                                        softHard = SoftHard.HARD,
                                        srcPlanetNo = list[i].no,
                                        targetPlanetNo = list[j].no,
                                        absoluteDegree = aspect_degree
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
                                        srcPlanetNo = list[i].no,
                                        targetPlanetNo = list[j].no,
                                        absoluteDegree = aspect_degree
                                    });
                                    break;
                                }
                            }
                            else if (kind == AspectKind.INCONJUNCT ||
                                kind == AspectKind.SESQUIQUADRATE)
                            {
                                if (kind == AspectKind.INCONJUNCT && !a_setting.dispAspectCategory[0][AspectKind.INCONJUNCT])
                                {
                                    continue;
                                }
                                if (kind == AspectKind.SESQUIQUADRATE && !a_setting.dispAspectCategory[0][AspectKind.SESQUIQUADRATE])
                                {
                                    continue;
                                }

                                if (aspect_degree < getDegree(kind) + a_setting.orbs[0][OrbKind.OTHER_HARD_150] &&
                                    aspect_degree > getDegree(kind) - a_setting.orbs[0][OrbKind.OTHER_HARD_150])
                                {
                                    list[i].aspects.Add(new AspectInfo()
                                    {
                                        targetPosition = list[j].absolute_position,
                                        aspectKind = kind,
                                        softHard = SoftHard.HARD,
                                        srcPlanetNo = list[i].no,
                                        targetPlanetNo = list[j].no,
                                        absoluteDegree = aspect_degree
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
                                        srcPlanetNo = list[i].no,
                                        targetPlanetNo = list[j].no,
                                        absoluteDegree = aspect_degree
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
                                        srcPlanetNo = list[i].no,
                                        targetPlanetNo = list[j].no,
                                        absoluteDegree = aspect_degree
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
                                        srcPlanetNo = list[i].no,
                                        targetPlanetNo = list[j].no,
                                        absoluteDegree = aspect_degree
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
        // 0: from1 to1
        // 1: from2 to2
        // 2: from3 to3
        // 3: from1 to2
        // 4: from1 to3
        // 5: from2 to3
        // 6: from4 to4
        // 7: from5 to5
        // 8: from1 to4
        // 9: from1 to5
        // 10: from2 to4
        // 11: from2 to5
        // 12: from3 to4
        // 13: from3 to5
        // 14: from4 to5
        public List<PlanetData> AspectCalcOther(SettingData a_setting, 
            List<PlanetData> fromList, List<PlanetData> toList, 
            int listKind)
        {
            for (int i = 0; i < fromList.Count - 1; i++)
            {
                for (int j = 0; j < toList.Count - 1; j++)
                {
                    // 90.0 と　300.0では210度ではなく150度にならなければいけない
                    double aspect_degree = fromList[i].absolute_position - toList[j].absolute_position;

                    if (aspect_degree > 180)
                    {
                        aspect_degree = fromList[i].absolute_position + 360 - toList[j].absolute_position;
                    }
                    if (aspect_degree < 0)
                    {
                        aspect_degree = Math.Abs(aspect_degree);
                    }


                    foreach (AspectKind kind in Enum.GetValues(typeof(AspectKind)))
                    {
                        if (listKind == 3)
                        {
                            // 1-2
                            if (i == CommonData.ZODIAC_SUN)
                            {
                                if (kind == AspectKind.CONJUNCTION ||
                                    kind == AspectKind.OPPOSITION ||
                                    kind == AspectKind.TRINE ||
                                    kind == AspectKind.SQUARE ||
                                    kind == AspectKind.SEXTILE)
                                {
                                    if (kind == AspectKind.CONJUNCTION && !a_setting.dispAspectCategory[listKind][AspectKind.CONJUNCTION])
                                    {
                                        continue;
                                    }
                                    if (kind == AspectKind.OPPOSITION && !a_setting.dispAspectCategory[listKind][AspectKind.OPPOSITION])
                                    {
                                        continue;
                                    }
                                    if (kind == AspectKind.TRINE && !a_setting.dispAspectCategory[listKind][AspectKind.TRINE])
                                    {
                                        continue;
                                    }
                                    if (kind == AspectKind.SQUARE && !a_setting.dispAspectCategory[listKind][AspectKind.SQUARE])
                                    {
                                        continue;
                                    }
                                    if (kind == AspectKind.SEXTILE && !a_setting.dispAspectCategory[listKind][AspectKind.SEXTILE])
                                    {
                                        continue;
                                    }
                                    if (aspect_degree < getDegree(kind) + a_setting.orbs[listKind][OrbKind.SUN_HARD_1ST] &&
                                        aspect_degree > getDegree(kind) - a_setting.orbs[listKind][OrbKind.SUN_HARD_1ST])
                                    {

                                        fromList[i].secondAspects.Add(new AspectInfo()
                                        {
                                            targetPosition = toList[j].absolute_position,
                                            aspectKind = kind,
                                            softHard = SoftHard.SOFT,
                                            targetPlanetNo = toList[j].no,
                                            srcPlanetNo = fromList[i].no,
                                            absoluteDegree = aspect_degree
                                        });
                                        break;
                                    }
                                    else if (aspect_degree < getDegree(kind) + a_setting.orbs[listKind][OrbKind.SUN_SOFT_1ST] &&
                                      aspect_degree > getDegree(kind) - a_setting.orbs[listKind][OrbKind.SUN_SOFT_1ST])
                                    {
                                        fromList[i].secondAspects.Add(new AspectInfo()
                                        {
                                            targetPosition = toList[j].absolute_position,
                                            aspectKind = kind,
                                            softHard = SoftHard.HARD,
                                            targetPlanetNo = toList[j].no,
                                            srcPlanetNo = fromList[i].no,
                                            absoluteDegree = aspect_degree
                                        });
                                        break;
                                    }
                                }
                                else if (kind == AspectKind.INCONJUNCT ||
                                         kind == AspectKind.SESQUIQUADRATE)
                                {
                                    if (aspect_degree < getDegree(kind) + a_setting.orbs[listKind][OrbKind.SUN_HARD_150] &&
                                        aspect_degree > getDegree(kind) - a_setting.orbs[listKind][OrbKind.SUN_HARD_150])
                                    {

                                        fromList[i].secondAspects.Add(new AspectInfo()
                                        {
                                            targetPosition = toList[j].absolute_position,
                                            aspectKind = kind,
                                            softHard = SoftHard.SOFT,
                                            targetPlanetNo = toList[j].no,
                                            srcPlanetNo = fromList[i].no,
                                            absoluteDegree = aspect_degree
                                        });
                                        break;
                                    }
                                    else if (aspect_degree < getDegree(kind) + a_setting.orbs[listKind][OrbKind.SUN_SOFT_150] &&
                                      aspect_degree > getDegree(kind) - a_setting.orbs[listKind][OrbKind.SUN_SOFT_150])
                                    {
                                        fromList[i].secondAspects.Add(new AspectInfo()
                                        {
                                            targetPosition = toList[j].absolute_position,
                                            aspectKind = kind,
                                            softHard = SoftHard.HARD,
                                            targetPlanetNo = toList[j].no,
                                            srcPlanetNo = fromList[i].no,
                                            absoluteDegree = aspect_degree
                                        });
                                        break;
                                    }
                                }
                                else
                                {
                                    if (aspect_degree < getDegree(kind) + a_setting.orbs[listKind][OrbKind.SUN_HARD_2ND] &&
                                        aspect_degree > getDegree(kind) - a_setting.orbs[listKind][OrbKind.SUN_HARD_2ND])
                                    {

                                        fromList[i].secondAspects.Add(new AspectInfo()
                                        {
                                            targetPosition = toList[j].absolute_position,
                                            aspectKind = kind,
                                            softHard = SoftHard.SOFT,
                                            targetPlanetNo = toList[j].no,
                                            srcPlanetNo = fromList[i].no,
                                            absoluteDegree = aspect_degree
                                        });
                                        break;
                                    }
                                    else if (aspect_degree < getDegree(kind) + a_setting.orbs[listKind][OrbKind.SUN_SOFT_2ND] &&
                                      aspect_degree > getDegree(kind) - a_setting.orbs[listKind][OrbKind.SUN_SOFT_2ND])
                                    {
                                        fromList[i].secondAspects.Add(new AspectInfo()
                                        {
                                            targetPosition = toList[j].absolute_position,
                                            aspectKind = kind,
                                            softHard = SoftHard.HARD,
                                            targetPlanetNo = toList[j].no,
                                            srcPlanetNo = fromList[i].no,
                                            absoluteDegree = aspect_degree
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
                                    if (kind == AspectKind.CONJUNCTION && !a_setting.dispAspectCategory[listKind][AspectKind.CONJUNCTION])
                                    {
                                        continue;
                                    }
                                    if (kind == AspectKind.OPPOSITION && !a_setting.dispAspectCategory[listKind][AspectKind.OPPOSITION])
                                    {
                                        continue;
                                    }
                                    if (kind == AspectKind.TRINE && !a_setting.dispAspectCategory[listKind][AspectKind.TRINE])
                                    {
                                        continue;
                                    }
                                    if (kind == AspectKind.SQUARE && !a_setting.dispAspectCategory[listKind][AspectKind.SQUARE])
                                    {
                                        continue;
                                    }
                                    if (kind == AspectKind.SEXTILE && !a_setting.dispAspectCategory[listKind][AspectKind.SEXTILE])
                                    {
                                        continue;
                                    }

                                    if (aspect_degree < getDegree(kind) + a_setting.orbs[listKind][OrbKind.MOON_HARD_1ST] &&
                                    aspect_degree > getDegree(kind) - a_setting.orbs[listKind][OrbKind.MOON_HARD_1ST])
                                    {
                                        fromList[i].secondAspects.Add(new AspectInfo()
                                        {
                                            targetPosition = toList[j].absolute_position,
                                            aspectKind = kind,
                                            softHard = SoftHard.SOFT,
                                            targetPlanetNo = toList[j].no,
                                            srcPlanetNo = fromList[i].no,
                                            absoluteDegree = aspect_degree
                                        });
                                        break;
                                    }
                                    else if (aspect_degree < getDegree(kind) + a_setting.orbs[listKind][OrbKind.MOON_HARD_1ST] &&
                                             aspect_degree > getDegree(kind) - a_setting.orbs[listKind][OrbKind.MOON_HARD_1ST])
                                    {
                                        fromList[i].secondAspects.Add(new AspectInfo()
                                        {
                                            targetPosition = toList[j].absolute_position,
                                            aspectKind = kind,
                                            softHard = SoftHard.HARD,
                                            targetPlanetNo = toList[j].no,
                                            srcPlanetNo = fromList[i].no,
                                            absoluteDegree = aspect_degree
                                        });
                                        break;
                                    }
                                }
                                else if (kind == AspectKind.INCONJUNCT ||
                                         kind == AspectKind.SESQUIQUADRATE)
                                {
                                    if (aspect_degree < getDegree(kind) + a_setting.orbs[listKind][OrbKind.MOON_HARD_150] &&
                                        aspect_degree > getDegree(kind) - a_setting.orbs[listKind][OrbKind.MOON_HARD_150])
                                    {

                                        fromList[i].secondAspects.Add(new AspectInfo()
                                        {
                                            targetPosition = toList[j].absolute_position,
                                            aspectKind = kind,
                                            softHard = SoftHard.SOFT,
                                            targetPlanetNo = toList[j].no,
                                            srcPlanetNo = fromList[i].no,
                                            absoluteDegree = aspect_degree
                                        });
                                        break;
                                    }
                                    else if (aspect_degree < getDegree(kind) + a_setting.orbs[listKind][OrbKind.MOON_SOFT_150] &&
                                      aspect_degree > getDegree(kind) - a_setting.orbs[listKind][OrbKind.MOON_SOFT_150])
                                    {
                                        fromList[i].secondAspects.Add(new AspectInfo()
                                        {
                                            targetPosition = toList[j].absolute_position,
                                            aspectKind = kind,
                                            softHard = SoftHard.HARD,
                                            targetPlanetNo = toList[j].no,
                                            srcPlanetNo = fromList[i].no,
                                            absoluteDegree = aspect_degree
                                        });
                                        break;
                                    }
                                }
                                else
                                {
                                    if (aspect_degree < getDegree(kind) + a_setting.orbs[listKind][OrbKind.MOON_HARD_2ND] &&
                                        aspect_degree > getDegree(kind) - a_setting.orbs[listKind][OrbKind.MOON_HARD_2ND])
                                    {

                                        fromList[i].secondAspects.Add(new AspectInfo()
                                        {
                                            targetPosition = toList[j].absolute_position,
                                            aspectKind = kind,
                                            softHard = SoftHard.SOFT,
                                            targetPlanetNo = toList[j].no,
                                            srcPlanetNo = fromList[i].no,
                                            absoluteDegree = aspect_degree
                                        });
                                        break;
                                    }
                                    else if (aspect_degree < getDegree(kind) + a_setting.orbs[listKind][OrbKind.MOON_SOFT_2ND] &&
                                      aspect_degree > getDegree(kind) - a_setting.orbs[listKind][OrbKind.MOON_SOFT_2ND])
                                    {
                                        fromList[i].secondAspects.Add(new AspectInfo()
                                        {
                                            targetPosition = toList[j].absolute_position,
                                            aspectKind = kind,
                                            softHard = SoftHard.HARD,
                                            targetPlanetNo = toList[j].no,
                                            srcPlanetNo = fromList[i].no,
                                            absoluteDegree = aspect_degree
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
                                    if (kind == AspectKind.CONJUNCTION && !a_setting.dispAspectCategory[listKind][AspectKind.CONJUNCTION])
                                    {
                                        continue;
                                    }
                                    if (kind == AspectKind.OPPOSITION && !a_setting.dispAspectCategory[listKind][AspectKind.OPPOSITION])
                                    {
                                        continue;
                                    }
                                    if (kind == AspectKind.TRINE && !a_setting.dispAspectCategory[listKind][AspectKind.TRINE])
                                    {
                                        continue;
                                    }
                                    if (kind == AspectKind.SQUARE && !a_setting.dispAspectCategory[listKind][AspectKind.SQUARE])
                                    {
                                        continue;
                                    }
                                    if (kind == AspectKind.SEXTILE && !a_setting.dispAspectCategory[listKind][AspectKind.SEXTILE])
                                    {
                                        continue;
                                    }
                                    if (aspect_degree < getDegree(kind) + a_setting.orbs[listKind][OrbKind.OTHER_HARD_1ST] &&
                                        aspect_degree > getDegree(kind) - a_setting.orbs[listKind][OrbKind.OTHER_HARD_1ST])
                                    {
                                        fromList[i].secondAspects.Add(new AspectInfo()
                                        {
                                            targetPosition = toList[j].absolute_position,
                                            aspectKind = kind,
                                            softHard = SoftHard.SOFT,
                                            targetPlanetNo = toList[j].no,
                                            srcPlanetNo = fromList[i].no,
                                            absoluteDegree = aspect_degree
                                        });
                                        break;
                                    }
                                    else if (aspect_degree < getDegree(kind) + a_setting.orbs[listKind][OrbKind.OTHER_SOFT_1ST] &&
                                         aspect_degree > getDegree(kind) - a_setting.orbs[listKind][OrbKind.OTHER_SOFT_1ST])
                                    {
                                        fromList[i].secondAspects.Add(new AspectInfo()
                                        {
                                            targetPosition = toList[j].absolute_position,
                                            aspectKind = kind,
                                            softHard = SoftHard.HARD,
                                            targetPlanetNo = toList[j].no,
                                            srcPlanetNo = fromList[i].no,
                                            absoluteDegree = aspect_degree
                                        });
                                        break;
                                    }
                                }
                                else if (kind == AspectKind.INCONJUNCT ||
                                         kind == AspectKind.SESQUIQUADRATE)
                                {
                                    if (aspect_degree < getDegree(kind) + a_setting.orbs[listKind][OrbKind.OTHER_HARD_150] &&
                                        aspect_degree > getDegree(kind) - a_setting.orbs[listKind][OrbKind.OTHER_HARD_150])
                                    {

                                        fromList[i].secondAspects.Add(new AspectInfo()
                                        {
                                            targetPosition = toList[j].absolute_position,
                                            aspectKind = kind,
                                            softHard = SoftHard.SOFT,
                                            targetPlanetNo = toList[j].no,
                                            srcPlanetNo = fromList[i].no,
                                            absoluteDegree = aspect_degree
                                        });
                                        break;
                                    }
                                    else if (aspect_degree < getDegree(kind) + a_setting.orbs[listKind][OrbKind.OTHER_SOFT_150] &&
                                      aspect_degree > getDegree(kind) - a_setting.orbs[listKind][OrbKind.OTHER_SOFT_150])
                                    {
                                        fromList[i].secondAspects.Add(new AspectInfo()
                                        {
                                            targetPosition = toList[j].absolute_position,
                                            aspectKind = kind,
                                            softHard = SoftHard.HARD,
                                            targetPlanetNo = toList[j].no,
                                            srcPlanetNo = fromList[i].no,
                                            absoluteDegree = aspect_degree
                                        });
                                        break;
                                    }
                                }
                                else
                                {
                                    if (aspect_degree < getDegree(kind) + a_setting.orbs[listKind][OrbKind.OTHER_HARD_2ND] &&
                                        aspect_degree > getDegree(kind) - a_setting.orbs[listKind][OrbKind.OTHER_HARD_2ND])
                                    {

                                        fromList[i].secondAspects.Add(new AspectInfo()
                                        {
                                            targetPosition = toList[j].absolute_position,
                                            aspectKind = kind,
                                            softHard = SoftHard.SOFT,
                                            targetPlanetNo = toList[j].no,
                                            srcPlanetNo = fromList[i].no,
                                            absoluteDegree = aspect_degree
                                        });
                                        break;
                                    }
                                    else if (aspect_degree < getDegree(kind) + a_setting.orbs[listKind][OrbKind.OTHER_SOFT_2ND] &&
                                      aspect_degree > getDegree(kind) - a_setting.orbs[listKind][OrbKind.OTHER_SOFT_2ND])
                                    {
                                        fromList[i].secondAspects.Add(new AspectInfo()
                                        {
                                            targetPosition = toList[j].absolute_position,
                                            aspectKind = kind,
                                            softHard = SoftHard.HARD,
                                            targetPlanetNo = toList[j].no,
                                            srcPlanetNo = fromList[i].no,
                                            absoluteDegree = aspect_degree
                                        });
                                        break;
                                    }
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
                                if (kind == AspectKind.CONJUNCTION ||
                                    kind == AspectKind.OPPOSITION ||
                                    kind == AspectKind.TRINE ||
                                    kind == AspectKind.SQUARE ||
                                    kind == AspectKind.SEXTILE)
                                {
                                    if (kind == AspectKind.CONJUNCTION && !a_setting.dispAspectCategory[listKind][AspectKind.CONJUNCTION])
                                    {
                                        continue;
                                    }
                                    if (kind == AspectKind.OPPOSITION && !a_setting.dispAspectCategory[listKind][AspectKind.OPPOSITION])
                                    {
                                        continue;
                                    }
                                    if (kind == AspectKind.TRINE && !a_setting.dispAspectCategory[listKind][AspectKind.TRINE])
                                    {
                                        continue;
                                    }
                                    if (kind == AspectKind.SQUARE && !a_setting.dispAspectCategory[listKind][AspectKind.SQUARE])
                                    {
                                        continue;
                                    }
                                    if (kind == AspectKind.SEXTILE && !a_setting.dispAspectCategory[listKind][AspectKind.SEXTILE])
                                    {
                                        continue;
                                    }
                                    if (aspect_degree < getDegree(kind) + a_setting.orbs[listKind][OrbKind.SUN_HARD_1ST] &&
                                        aspect_degree > getDegree(kind) - a_setting.orbs[listKind][OrbKind.SUN_HARD_1ST])
                                    {

                                        fromList[i].thirdAspects.Add(new AspectInfo()
                                        {
                                            targetPosition = toList[j].absolute_position,
                                            aspectKind = kind,
                                            softHard = SoftHard.SOFT,
                                            srcPlanetNo = fromList[i].no,
                                            targetPlanetNo = toList[j].no,
                                            absoluteDegree = aspect_degree
                                        });
                                        break;
                                    }
                                    else if (aspect_degree < getDegree(kind) + a_setting.orbs[listKind][OrbKind.SUN_SOFT_1ST] &&
                                        aspect_degree > getDegree(kind) - a_setting.orbs[listKind][OrbKind.SUN_SOFT_1ST])
                                    {
                                        fromList[i].thirdAspects.Add(new AspectInfo()
                                        {
                                            targetPosition = toList[j].absolute_position,
                                            aspectKind = kind,
                                            softHard = SoftHard.HARD,
                                            srcPlanetNo = fromList[i].no,
                                            targetPlanetNo = toList[j].no,
                                            absoluteDegree = aspect_degree
                                        });
                                        break;
                                    }
                                }
                                else if (kind == AspectKind.INCONJUNCT ||
                                         kind == AspectKind.SESQUIQUADRATE)
                                {
                                    if (aspect_degree < getDegree(kind) + a_setting.orbs[listKind][OrbKind.MOON_HARD_150] &&
                                        aspect_degree > getDegree(kind) - a_setting.orbs[listKind][OrbKind.MOON_HARD_150])
                                    {

                                        fromList[i].thirdAspects.Add(new AspectInfo()
                                        {
                                            targetPosition = toList[j].absolute_position,
                                            aspectKind = kind,
                                            softHard = SoftHard.SOFT,
                                            targetPlanetNo = toList[j].no,
                                            srcPlanetNo = fromList[i].no,
                                            absoluteDegree = aspect_degree
                                        });
                                        break;
                                    }
                                    else if (aspect_degree < getDegree(kind) + a_setting.orbs[listKind][OrbKind.MOON_SOFT_150] &&
                                      aspect_degree > getDegree(kind) - a_setting.orbs[listKind][OrbKind.MOON_SOFT_150])
                                    {
                                        fromList[i].thirdAspects.Add(new AspectInfo()
                                        {
                                            targetPosition = toList[j].absolute_position,
                                            aspectKind = kind,
                                            softHard = SoftHard.HARD,
                                            targetPlanetNo = toList[j].no,
                                            srcPlanetNo = fromList[i].no,
                                            absoluteDegree = aspect_degree
                                        });
                                        break;
                                    }
                                }
                                else
                                {
                                    if (aspect_degree < getDegree(kind) + a_setting.orbs[listKind][OrbKind.MOON_HARD_2ND] &&
                                        aspect_degree > getDegree(kind) - a_setting.orbs[listKind][OrbKind.MOON_HARD_2ND])
                                    {

                                        fromList[i].thirdAspects.Add(new AspectInfo()
                                        {
                                            targetPosition = toList[j].absolute_position,
                                            aspectKind = kind,
                                            softHard = SoftHard.SOFT,
                                            targetPlanetNo = toList[j].no,
                                            srcPlanetNo = fromList[i].no,
                                            absoluteDegree = aspect_degree
                                        });
                                        break;
                                    }
                                    else if (aspect_degree < getDegree(kind) + a_setting.orbs[listKind][OrbKind.MOON_SOFT_2ND] &&
                                      aspect_degree > getDegree(kind) - a_setting.orbs[listKind][OrbKind.MOON_SOFT_2ND])
                                    {
                                        fromList[i].thirdAspects.Add(new AspectInfo()
                                        {
                                            targetPosition = toList[j].absolute_position,
                                            aspectKind = kind,
                                            softHard = SoftHard.HARD,
                                            targetPlanetNo = toList[j].no,
                                            srcPlanetNo = fromList[i].no,
                                            absoluteDegree = aspect_degree
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
                                    if (kind == AspectKind.CONJUNCTION && !a_setting.dispAspectCategory[listKind][AspectKind.CONJUNCTION])
                                    {
                                        continue;
                                    }
                                    if (kind == AspectKind.OPPOSITION && !a_setting.dispAspectCategory[listKind][AspectKind.OPPOSITION])
                                    {
                                        continue;
                                    }
                                    if (kind == AspectKind.TRINE && !a_setting.dispAspectCategory[listKind][AspectKind.TRINE])
                                    {
                                        continue;
                                    }
                                    if (kind == AspectKind.SQUARE && !a_setting.dispAspectCategory[listKind][AspectKind.SQUARE])
                                    {
                                        continue;
                                    }
                                    if (kind == AspectKind.SEXTILE && !a_setting.dispAspectCategory[listKind][AspectKind.SEXTILE])
                                    {
                                        continue;
                                    }

                                    if (aspect_degree < getDegree(kind) + a_setting.orbs[listKind][OrbKind.MOON_HARD_1ST] &&
                                        aspect_degree > getDegree(kind) - a_setting.orbs[listKind][OrbKind.MOON_HARD_1ST])
                                    {
                                        fromList[i].thirdAspects.Add(new AspectInfo()
                                        {
                                            targetPosition = toList[j].absolute_position,
                                            aspectKind = kind,
                                            softHard = SoftHard.SOFT,
                                            srcPlanetNo = fromList[i].no,
                                            targetPlanetNo = toList[j].no,
                                            absoluteDegree = aspect_degree
                                        });
                                        break;
                                    }
                                    else if (aspect_degree < getDegree(kind) + a_setting.orbs[listKind][OrbKind.OTHER_HARD_1ST] &&
                                        aspect_degree > getDegree(kind) - a_setting.orbs[listKind][OrbKind.OTHER_HARD_1ST])
                                    {
                                        fromList[i].thirdAspects.Add(new AspectInfo()
                                        {
                                            targetPosition = toList[j].absolute_position,
                                            aspectKind = kind,
                                            softHard = SoftHard.HARD,
                                            srcPlanetNo = fromList[i].no,
                                            targetPlanetNo = toList[j].no,
                                            absoluteDegree = aspect_degree
                                        });
                                        break;
                                    }
                                }
                                else if (kind == AspectKind.INCONJUNCT ||
                                         kind == AspectKind.SESQUIQUADRATE)
                                {
                                    if (aspect_degree < getDegree(kind) + a_setting.orbs[listKind][OrbKind.MOON_HARD_150] &&
                                        aspect_degree > getDegree(kind) - a_setting.orbs[listKind][OrbKind.MOON_HARD_150])
                                    {

                                        fromList[i].thirdAspects.Add(new AspectInfo()
                                        {
                                            targetPosition = toList[j].absolute_position,
                                            aspectKind = kind,
                                            softHard = SoftHard.SOFT,
                                            targetPlanetNo = toList[j].no,
                                            srcPlanetNo = fromList[i].no,
                                            absoluteDegree = aspect_degree
                                        });
                                        break;
                                    }
                                    else if (aspect_degree < getDegree(kind) + a_setting.orbs[listKind][OrbKind.MOON_SOFT_150] &&
                                      aspect_degree > getDegree(kind) - a_setting.orbs[listKind][OrbKind.MOON_SOFT_150])
                                    {
                                        fromList[i].thirdAspects.Add(new AspectInfo()
                                        {
                                            targetPosition = toList[j].absolute_position,
                                            aspectKind = kind,
                                            softHard = SoftHard.HARD,
                                            targetPlanetNo = toList[j].no,
                                            srcPlanetNo = fromList[i].no,
                                            absoluteDegree = aspect_degree
                                        });
                                        break;
                                    }
                                }
                                else
                                {
                                    if (aspect_degree < getDegree(kind) + a_setting.orbs[listKind][OrbKind.MOON_HARD_2ND] &&
                                        aspect_degree > getDegree(kind) - a_setting.orbs[listKind][OrbKind.MOON_HARD_2ND])
                                    {

                                        fromList[i].thirdAspects.Add(new AspectInfo()
                                        {
                                            targetPosition = toList[j].absolute_position,
                                            aspectKind = kind,
                                            softHard = SoftHard.SOFT,
                                            targetPlanetNo = toList[j].no,
                                            srcPlanetNo = fromList[i].no,
                                            absoluteDegree = aspect_degree
                                        });
                                        break;
                                    }
                                    else if (aspect_degree < getDegree(kind) + a_setting.orbs[listKind][OrbKind.MOON_SOFT_2ND] &&
                                      aspect_degree > getDegree(kind) - a_setting.orbs[listKind][OrbKind.MOON_SOFT_2ND])
                                    {
                                        fromList[i].thirdAspects.Add(new AspectInfo()
                                        {
                                            targetPosition = toList[j].absolute_position,
                                            aspectKind = kind,
                                            softHard = SoftHard.HARD,
                                            targetPlanetNo = toList[j].no,
                                            srcPlanetNo = fromList[i].no,
                                            absoluteDegree = aspect_degree
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
                                    if (kind == AspectKind.CONJUNCTION && !a_setting.dispAspectCategory[listKind][AspectKind.CONJUNCTION])
                                    {
                                        continue;
                                    }
                                    if (kind == AspectKind.OPPOSITION && !a_setting.dispAspectCategory[listKind][AspectKind.OPPOSITION])
                                    {
                                        continue;
                                    }
                                    if (kind == AspectKind.TRINE && !a_setting.dispAspectCategory[listKind][AspectKind.TRINE])
                                    {
                                        continue;
                                    }
                                    if (kind == AspectKind.SQUARE && !a_setting.dispAspectCategory[listKind][AspectKind.SQUARE])
                                    {
                                        continue;
                                    }
                                    if (kind == AspectKind.SEXTILE && !a_setting.dispAspectCategory[listKind][AspectKind.SEXTILE])
                                    {
                                        continue;
                                    }

                                    if (aspect_degree < getDegree(kind) + a_setting.orbs[listKind][OrbKind.OTHER_HARD_1ST] &&
                                        aspect_degree > getDegree(kind) - a_setting.orbs[listKind][OrbKind.OTHER_HARD_1ST])
                                    {
                                        fromList[i].thirdAspects.Add(new AspectInfo()
                                        {
                                            targetPosition = toList[j].absolute_position,
                                            aspectKind = kind,
                                            softHard = SoftHard.SOFT,
                                            srcPlanetNo = fromList[i].no,
                                            targetPlanetNo = toList[j].no,
                                            absoluteDegree = aspect_degree
                                        });
                                        break;
                                    }
                                    else if (aspect_degree < getDegree(kind) + a_setting.orbs[listKind][OrbKind.OTHER_SOFT_1ST] &&
                                        aspect_degree > getDegree(kind) - a_setting.orbs[listKind][OrbKind.OTHER_SOFT_1ST])
                                    {
                                        fromList[i].thirdAspects.Add(new AspectInfo()
                                        {
                                            targetPosition = toList[j].absolute_position,
                                            aspectKind = kind,
                                            softHard = SoftHard.HARD,
                                            srcPlanetNo = fromList[i].no,
                                            targetPlanetNo = toList[j].no,
                                            absoluteDegree = aspect_degree
                                        });
                                        break;
                                    }
                                }


                                else if (kind == AspectKind.INCONJUNCT ||
                                         kind == AspectKind.SESQUIQUADRATE)
                                {
                                    if (aspect_degree < getDegree(kind) + a_setting.orbs[listKind][OrbKind.MOON_HARD_150] &&
                                        aspect_degree > getDegree(kind) - a_setting.orbs[listKind][OrbKind.MOON_HARD_150])
                                    {

                                        fromList[i].thirdAspects.Add(new AspectInfo()
                                        {
                                            targetPosition = toList[j].absolute_position,
                                            aspectKind = kind,
                                            softHard = SoftHard.SOFT,
                                            targetPlanetNo = toList[j].no,
                                            srcPlanetNo = fromList[i].no,
                                            absoluteDegree = aspect_degree
                                        });
                                        break;
                                    }
                                    else if (aspect_degree < getDegree(kind) + a_setting.orbs[listKind][OrbKind.MOON_SOFT_150] &&
                                      aspect_degree > getDegree(kind) - a_setting.orbs[listKind][OrbKind.MOON_SOFT_150])
                                    {
                                        fromList[i].thirdAspects.Add(new AspectInfo()
                                        {
                                            targetPosition = toList[j].absolute_position,
                                            aspectKind = kind,
                                            softHard = SoftHard.HARD,
                                            targetPlanetNo = toList[j].no,
                                            srcPlanetNo = fromList[i].no,
                                            absoluteDegree = aspect_degree
                                        });
                                        break;
                                    }
                                }
                                else
                                {
                                    if (aspect_degree < getDegree(kind) + a_setting.orbs[listKind][OrbKind.MOON_HARD_2ND] &&
                                        aspect_degree > getDegree(kind) - a_setting.orbs[listKind][OrbKind.MOON_HARD_2ND])
                                    {

                                        fromList[i].thirdAspects.Add(new AspectInfo()
                                        {
                                            targetPosition = toList[j].absolute_position,
                                            aspectKind = kind,
                                            softHard = SoftHard.SOFT,
                                            targetPlanetNo = toList[j].no,
                                            srcPlanetNo = fromList[i].no,
                                            absoluteDegree = aspect_degree
                                        });
                                        break;
                                    }
                                    else if (aspect_degree < getDegree(kind) + a_setting.orbs[listKind][OrbKind.MOON_SOFT_2ND] &&
                                      aspect_degree > getDegree(kind) - a_setting.orbs[listKind][OrbKind.MOON_SOFT_2ND])
                                    {
                                        fromList[i].thirdAspects.Add(new AspectInfo()
                                        {
                                            targetPosition = toList[j].absolute_position,
                                            aspectKind = kind,
                                            softHard = SoftHard.HARD,
                                            targetPlanetNo = toList[j].no,
                                            srcPlanetNo = fromList[i].no,
                                            absoluteDegree = aspect_degree
                                        });
                                        break;
                                    }
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
