using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

using microcosm.Common;
using microcosm.Planet;
using microcosm.Aspect;

namespace microcosm
{
    partial class MainWindow
    {
        // 天体の表示
        private void planetRender(double startdegree,
            List<PlanetData> list1,
            List<PlanetData> list2,
            List<PlanetData> list3,
            List<PlanetData> list4,
            List<PlanetData> list5
            )
        {
            List<bool> dispList = new List<bool>();
            List<PlanetDisplay> pDisplayList = new List<PlanetDisplay>();

            if (tempSettings.bands == 1)
            {
                int[] box = new int[60];
                for (int i = 0; i < 60; i++)
                {
                    box[i] = 0;
                }
                list1.ForEach(planet =>
                {
                    // 天体表示させない
                    if (!planet.isDisp)
                    {
                        return;
                    }
                    if (planet.no == 10000)
                    {
                        return;
                    }
                    if (planet.no == 10001)
                    {
                        return;
                    }

                    PointF point;
                    PointF pointdegree;
                    PointF pointsymbol;
                    PointF pointminute;
                    PointF pointretrograde;
                    // 重ならないようにずらしを入れる
                    // 1サインに6度単位5個までデータが入る
                    int index = 0;
                    int absolute_position = 0;
                    if (planet.absolute_position < 0)
                    {
                        absolute_position = (int)planet.absolute_position + 360;
                    }
                    else
                    {
                        absolute_position = (int)planet.absolute_position;
                    }
                    index = (int)(absolute_position / 6);
                    if (box[index] == 1)
                    {
                        while (box[index] == 1)
                        {
                            index++;
                            if (index == 60)
                            {
                                index = 0;
                            }
                        }
                        box[index] = 1;
                    }
                    else
                    {
                        box[index] = 1;
                    }

                    if (ringCanvas.ActualWidth < 470)
                    {
                        point = rotate(rcanvas.outerWidth / 3 - 20, 0, 6 * index - startdegree);
                        pointdegree = rotate(rcanvas.outerWidth / 3 - 40, 0, 6 * index - startdegree);
                    }
                    else
                    {
                        point = rotate(rcanvas.outerWidth / 3 + 20, 0, 6 * index - startdegree);
                        pointdegree = rotate(rcanvas.outerWidth / 3, 0, 6 * index - startdegree);
                    }
                    pointsymbol = rotate(rcanvas.outerWidth / 3 - 20, 0, 6 * index - startdegree);
                    pointminute = rotate(rcanvas.outerWidth / 3 - 40, 0, 6 * index - startdegree);
                    pointretrograde = rotate(rcanvas.outerWidth / 3 - 60, 0, 6 * index - startdegree);
                    point.X += (float)rcanvas.outerWidth / 2;
                    point.X -= 8;
                    pointdegree.X += (float)rcanvas.outerWidth / 2;
                    pointdegree.X -= 8;
                    pointsymbol.X += (float)rcanvas.outerWidth / 2;
                    pointsymbol.X -= 8;
                    pointminute.X += (float)rcanvas.outerWidth / 2;
                    pointminute.X -= 8;
                    pointretrograde.X += (float)rcanvas.outerWidth / 2;
                    pointretrograde.X -= 8;

                    point.Y *= -1;
                    point.Y += (float)rcanvas.outerHeight / 2;
                    point.Y -= 18;
                    pointdegree.Y *= -1;
                    pointdegree.Y += (float)rcanvas.outerHeight / 2;
                    pointdegree.Y -= 15;
                    pointsymbol.Y *= -1;
                    pointsymbol.Y += (float)rcanvas.outerHeight / 2;
                    pointsymbol.Y -= 15;
                    pointminute.Y *= -1;
                    pointminute.Y += (float)rcanvas.outerHeight / 2;
                    pointminute.Y -= 15;
                    pointretrograde.Y *= -1;
                    pointretrograde.Y += (float)rcanvas.outerHeight / 2;
                    pointretrograde.Y -= 15;

                    dispList.Add(planet.isDisp);
                    bool retrograde;
                    if (planet.speed < 0)
                    {
                        retrograde = true;
                    }
                    else
                    {
                        retrograde = false;
                    }

                    Explanation exp = new Explanation()
                    {
                        planet = CommonData.getPlanetText(planet.no),
                        degree = planet.absolute_position % 30,
                        sign = CommonData.getSignTextJp(planet.absolute_position),
                        retrograde = retrograde
                    };

                    PlanetDisplay display = new PlanetDisplay()
                    {
                        planetNo = planet.no,
                        isDisp = planet.isDisp,
                        explanation = exp,
                        planetPt = point,
                        planetTxt = CommonData.getPlanetSymbol(planet.no),
                        planetColor = CommonData.getPlanetColor(planet.no),
                        degreePt = pointdegree,
                        degreeTxt = ((planet.absolute_position) % 30 - 0.5).ToString("00°"),
                        symbolPt = pointsymbol,
                        symbolTxt = CommonData.getSignText(planet.absolute_position),
                        minutePt = pointminute,
                        // 小数点以下切り捨て 59.9->59
                        minuteTxt = ((planet.absolute_position % 1) * 60 - 0.5).ToString("00") + "'",
                        retrogradePt = pointretrograde,
                        retrogradeTxt = CommonData.getRetrograde(planet.speed),
                        symbolColor = CommonData.getSignColor(planet.absolute_position)
                    };
                    pDisplayList.Add(display);

                });

                pDisplayList.ForEach(displayData =>
                {
                    if (!displayData.isDisp)
                    {
                        return;
                    }
                    if (ringCanvas.ActualWidth < 470)
                    {
                        SetOnlySignDegree(displayData);
                    }
                    else
                    {
                        SetSign(displayData);
                    }

                });
            }
            // 二重円
            else if (tempSettings.bands == 2)
            {
                int[] box = new int[60];
                for (int i = 0; i < 60; i++)
                {
                    box[i] = 0;
                }
                list1.ForEach(planet =>
                {
                    // 天体表示させない
                    if (!planet.isDisp)
                    {
                        return;
                    }

                    PointF point;
                    // 重ならないようにずらしを入れる
                    // 1サインに6度単位5個までデータが入る
                    int index = 0;
                    int absolute_position = 0;
                    if (planet.absolute_position < 0)
                    {
                        absolute_position = (int)planet.absolute_position + 360;
                    }
                    else
                    {
                        absolute_position = (int)planet.absolute_position;
                    }
                    index = (int)(absolute_position / 6);
                    if (box[index] == 1)
                    {
                        while (box[index] == 1)
                        {
                            index++;
                            if (index == 60)
                            {
                                index = 0;
                            }
                        }
                        box[index] = 1;
                    }
                    else
                    {
                        box[index] = 1;
                    }

                    point = rotate(rcanvas.outerWidth / 5 + 20, 0, 6 * index - startdegree);
                    point.X += (float)rcanvas.outerWidth / 2;
                    point.X -= 8;

                    point.Y *= -1;
                    point.Y += (float)rcanvas.outerHeight / 2;
                    point.Y -= 18;

                    dispList.Add(planet.isDisp);

                    Explanation exp = new Explanation()
                    {
                        degree = planet.absolute_position % 30,
                        sign = CommonData.getSignTextJp(planet.absolute_position)
                    };

                    PlanetDisplay display = new PlanetDisplay()
                    {
                        planetNo = planet.no,
                        isDisp = planet.isDisp,
                        explanation = exp,
                        planetPt = point,
                        planetTxt = CommonData.getPlanetSymbol(planet.no),
                        planetColor = CommonData.getPlanetColor(planet.no)
                    };
                    pDisplayList.Add(display);
                });
                for (int i = 0; i < 60; i++)
                {
                    box[i] = 0;
                }

                list2.ForEach(planet =>
                {
                    // 天体表示させない
                    if (!planet.isDisp)
                    {
                        return;
                    }

                    PointF point;
                    // 重ならないようにずらしを入れる
                    // 1サインに6度単位5個までデータが入る
                    int index = 0;
                    int absolute_position = 0;
                    if (planet.absolute_position < 0)
                    {
                        absolute_position = (int)planet.absolute_position + 360;
                    }
                    else
                    {
                        absolute_position = (int)planet.absolute_position;
                    }
                    index = (int)(absolute_position / 6);
                    if (box[index] == 1)
                    {
                        while (box[index] == 1)
                        {
                            index++;
                            if (index == 60)
                            {
                                index = 0;
                            }
                        }
                        box[index] = 1;
                    }
                    else
                    {
                        box[index] = 1;
                    }

                    point = rotate(rcanvas.outerWidth / 3 + 20, 0, 6 * index - startdegree);
                    point.X += (float)rcanvas.outerWidth / 2;
                    point.X -= 8;

                    point.Y *= -1;
                    point.Y += (float)rcanvas.outerHeight / 2;
                    point.Y -= 18;

                    dispList.Add(planet.isDisp);

                    Explanation exp = new Explanation()
                    {
                        degree = planet.absolute_position % 30,
                        sign = CommonData.getSignTextJp(planet.absolute_position)
                    };

                    PlanetDisplay display = new PlanetDisplay()
                    {
                        planetNo = planet.no,
                        isDisp = planet.isDisp,
                        explanation = exp,
                        planetPt = point,
                        planetTxt = CommonData.getPlanetSymbol(planet.no),
                        planetColor = CommonData.getPlanetColor(planet.no)
                    };
                    pDisplayList.Add(display);

                });

                pDisplayList.ForEach(displayData =>
                {
                    if (!displayData.isDisp)
                    {
                        return;
                    }
                    SetOnlySign(displayData);
                });

            }

            // 三重円
            else if (tempSettings.bands == 3)
            {
                int[] box = new int[60];
                for (int i = 0; i < 60; i++)
                {
                    box[i] = 0;
                }
                list1.ForEach(planet =>
                {
                    // 天体表示させない
                    if (!planet.isDisp)
                    {
                        return;
                    }

                    PointF point;
                    // 重ならないようにずらしを入れる
                    // 1サインに6度単位5個までデータが入る
                    int index = 0;
                    int absolute_position = 0;
                    if (planet.absolute_position < 0)
                    {
                        absolute_position = (int)planet.absolute_position + 360;
                    }
                    else
                    {
                        absolute_position = (int)planet.absolute_position;
                    }
                    index = (int)(absolute_position / 6);
                    if (box[index] == 1)
                    {
                        while (box[index] == 1)
                        {
                            index++;
                            if (index == 60)
                            {
                                index = 0;
                            }
                        }
                        box[index] = 1;
                    }
                    else
                    {
                        box[index] = 1;
                    }

                    point = rotate(rcanvas.outerWidth / 5, 0, 6 * index - startdegree);
                    point.X += (float)rcanvas.outerWidth / 2;
                    point.X -= 8;

                    point.Y *= -1;
                    point.Y += (float)rcanvas.outerHeight / 2;
                    point.Y -= 18;

                    dispList.Add(planet.isDisp);

                    Explanation exp = new Explanation()
                    {
                        degree = planet.absolute_position % 30,
                        sign = CommonData.getSignTextJp(planet.absolute_position)
                    };

                    PlanetDisplay display = new PlanetDisplay()
                    {
                        planetNo = planet.no,
                        isDisp = planet.isDisp,
                        explanation = exp,
                        planetPt = point,
                        planetTxt = CommonData.getPlanetSymbol(planet.no),
                        planetColor = CommonData.getPlanetColor(planet.no)
                    };
                    pDisplayList.Add(display);
                });
                for (int i = 0; i < 60; i++)
                {
                    box[i] = 0;
                }

                list2.ForEach(planet =>
                {
                    // 天体表示させない
                    if (!planet.isDisp)
                    {
                        return;
                    }

                    PointF point;
                    // 重ならないようにずらしを入れる
                    // 1サインに6度単位5個までデータが入る
                    int index = 0;
                    int absolute_position = 0;
                    if (planet.absolute_position < 0)
                    {
                        absolute_position = (int)planet.absolute_position + 360;
                    }
                    else
                    {
                        absolute_position = (int)planet.absolute_position;
                    }
                    index = (int)(absolute_position / 6);
                    if (box[index] == 1)
                    {
                        while (box[index] == 1)
                        {
                            index++;
                            if (index == 60)
                            {
                                index = 0;
                            }
                        }
                        box[index] = 1;
                    }
                    else
                    {
                        box[index] = 1;
                    }

                    point = rotate(rcanvas.outerWidth / 4 + 20, 0, 6 * index - startdegree);
                    point.X += (float)rcanvas.outerWidth / 2;
                    point.X -= 8;

                    point.Y *= -1;
                    point.Y += (float)rcanvas.outerHeight / 2;
                    point.Y -= 18;

                    dispList.Add(planet.isDisp);

                    Explanation exp = new Explanation()
                    {
                        degree = planet.absolute_position % 30,
                        sign = CommonData.getSignTextJp(planet.absolute_position)
                    };

                    PlanetDisplay display = new PlanetDisplay()
                    {
                        planetNo = planet.no,
                        isDisp = planet.isDisp,
                        explanation = exp,
                        planetPt = point,
                        planetTxt = CommonData.getPlanetSymbol(planet.no),
                        planetColor = CommonData.getPlanetColor(planet.no)
                    };
                    pDisplayList.Add(display);

                });

                pDisplayList.ForEach(displayData =>
                {
                    if (!displayData.isDisp)
                    {
                        return;
                    }
                    SetOnlySign(displayData);
                });

                for (int i = 0; i < 60; i++)
                {
                    box[i] = 0;
                }

                list3.ForEach(planet =>
                {
                    // 天体表示させない
                    if (!planet.isDisp)
                    {
                        return;
                    }

                    PointF point;
                    // 重ならないようにずらしを入れる
                    // 1サインに6度単位5個までデータが入る
                    int index = 0;
                    int absolute_position = 0;
                    if (planet.absolute_position < 0)
                    {
                        absolute_position = (int)planet.absolute_position + 360;
                    }
                    else
                    {
                        absolute_position = (int)planet.absolute_position;
                    }
                    index = (int)(absolute_position / 6);
                    if (box[index] == 1)
                    {
                        while (box[index] == 1)
                        {
                            index++;
                            if (index == 60)
                            {
                                index = 0;
                            }
                        }
                        box[index] = 1;
                    }
                    else
                    {
                        box[index] = 1;
                    }

                    point = rotate(rcanvas.outerWidth / 3 + 20, 0, 6 * index - startdegree);
                    point.X += (float)rcanvas.outerWidth / 2;
                    point.X -= 8;

                    point.Y *= -1;
                    point.Y += (float)rcanvas.outerHeight / 2;
                    point.Y -= 18;

                    dispList.Add(planet.isDisp);

                    Explanation exp = new Explanation()
                    {
                        degree = planet.absolute_position % 30,
                        sign = CommonData.getSignTextJp(planet.absolute_position)
                    };

                    PlanetDisplay display = new PlanetDisplay()
                    {
                        planetNo = planet.no,
                        isDisp = planet.isDisp,
                        explanation = exp,
                        planetPt = point,
                        planetTxt = CommonData.getPlanetSymbol(planet.no),
                        planetColor = CommonData.getPlanetColor(planet.no)
                    };
                    pDisplayList.Add(display);

                });

                pDisplayList.ForEach(displayData =>
                {
                    if (!displayData.isDisp)
                    {
                        return;
                    }
                    SetOnlySign(displayData);
                });

            }

            // 四重円
            else if (tempSettings.bands == 4)
            {
                int[] box = new int[60];
                for (int i = 0; i < 60; i++)
                {
                    box[i] = 0;
                }
                list1.ForEach(planet =>
                {
                    // 天体表示させない
                    if (!planet.isDisp)
                    {
                        return;
                    }

                    PointF point;
                    // 重ならないようにずらしを入れる
                    // 1サインに6度単位5個までデータが入る
                    int index = (int)(planet.absolute_position / 6);
                    if (box[index] == 1)
                    {
                        while (box[index] == 1)
                        {
                            index++;
                            if (index == 60)
                            {
                                index = 0;
                            }
                        }
                        box[index] = 1;
                    }
                    else
                    {
                        box[index] = 1;
                    }

                    point = rotate(rcanvas.outerWidth / 5, 0, 6 * index - startdegree);
                    point.X += (float)rcanvas.outerWidth / 2;
                    point.X -= 8;

                    point.Y *= -1;
                    point.Y += (float)rcanvas.outerHeight / 2;
                    point.Y -= 18;

                    dispList.Add(planet.isDisp);

                    Explanation exp = new Explanation()
                    {
                        degree = planet.absolute_position % 30,
                        sign = CommonData.getSignTextJp(planet.absolute_position)
                    };

                    PlanetDisplay display = new PlanetDisplay()
                    {
                        planetNo = planet.no,
                        isDisp = planet.isDisp,
                        explanation = exp,
                        planetPt = point,
                        planetTxt = CommonData.getPlanetSymbol(planet.no),
                        planetColor = CommonData.getPlanetColor(planet.no)
                    };
                    pDisplayList.Add(display);
                });
                for (int i = 0; i < 60; i++)
                {
                    box[i] = 0;
                }

                list2.ForEach(planet =>
                {
                    // 天体表示させない
                    if (!planet.isDisp)
                    {
                        return;
                    }

                    PointF point;
                    // 重ならないようにずらしを入れる
                    // 1サインに6度単位5個までデータが入る
                    int index = (int)(planet.absolute_position / 6);
                    if (box[index] == 1)
                    {
                        while (box[index] == 1)
                        {
                            index++;
                            if (index == 60)
                            {
                                index = 0;
                            }
                        }
                        box[index] = 1;
                    }
                    else
                    {
                        box[index] = 1;
                    }

                    point = rotate(rcanvas.outerWidth / 4, 0, 6 * index - startdegree);
                    point.X += (float)rcanvas.outerWidth / 2;
                    point.X -= 8;

                    point.Y *= -1;
                    point.Y += (float)rcanvas.outerHeight / 2;
                    point.Y -= 18;

                    dispList.Add(planet.isDisp);

                    Explanation exp = new Explanation()
                    {
                        degree = planet.absolute_position % 30,
                        sign = CommonData.getSignTextJp(planet.absolute_position)
                    };

                    PlanetDisplay display = new PlanetDisplay()
                    {
                        planetNo = planet.no,
                        isDisp = planet.isDisp,
                        explanation = exp,
                        planetPt = point,
                        planetTxt = CommonData.getPlanetSymbol(planet.no),
                        planetColor = CommonData.getPlanetColor(planet.no)
                    };
                    pDisplayList.Add(display);

                });

                pDisplayList.ForEach(displayData =>
                {
                    if (!displayData.isDisp)
                    {
                        return;
                    }
                    SetOnlySign(displayData);
                });

                for (int i = 0; i < 60; i++)
                {
                    box[i] = 0;
                }

                list3.ForEach(planet =>
                {
                    // 天体表示させない
                    if (!planet.isDisp)
                    {
                        return;
                    }

                    PointF point;
                    // 重ならないようにずらしを入れる
                    // 1サインに6度単位5個までデータが入る
                    int index = (int)(planet.absolute_position / 6);
                    if (box[index] == 1)
                    {
                        while (box[index] == 1)
                        {
                            index++;
                            if (index == 60)
                            {
                                index = 0;
                            }
                        }
                        box[index] = 1;
                    }
                    else
                    {
                        box[index] = 1;
                    }

                    point = rotate(rcanvas.outerWidth / 3 - 5, 0, 6 * index - startdegree);
                    point.X += (float)rcanvas.outerWidth / 2;
                    point.X -= 8;

                    point.Y *= -1;
                    point.Y += (float)rcanvas.outerHeight / 2;
                    point.Y -= 18;

                    dispList.Add(planet.isDisp);

                    Explanation exp = new Explanation()
                    {
                        degree = planet.absolute_position % 30,
                        sign = CommonData.getSignTextJp(planet.absolute_position)
                    };

                    PlanetDisplay display = new PlanetDisplay()
                    {
                        planetNo = planet.no,
                        isDisp = planet.isDisp,
                        explanation = exp,
                        planetPt = point,
                        planetTxt = CommonData.getPlanetSymbol(planet.no),
                        planetColor = CommonData.getPlanetColor(planet.no)
                    };
                    pDisplayList.Add(display);

                });

                pDisplayList.ForEach(displayData =>
                {
                    if (!displayData.isDisp)
                    {
                        return;
                    }
                    SetOnlySign(displayData);
                });

                for (int i = 0; i < 60; i++)
                {
                    box[i] = 0;
                }

                list4.ForEach(planet =>
                {
                    // 天体表示させない
                    if (!planet.isDisp)
                    {
                        return;
                    }

                    PointF point;
                    // 重ならないようにずらしを入れる
                    // 1サインに6度単位5個までデータが入る
                    int index = (int)(planet.absolute_position / 6);
                    if (box[index] == 1)
                    {
                        while (box[index] == 1)
                        {
                            index++;
                            if (index == 60)
                            {
                                index = 0;
                            }
                        }
                        box[index] = 1;
                    }
                    else
                    {
                        box[index] = 1;
                    }

                    point = rotate(rcanvas.outerWidth / 3 + 20, 0, 6 * index - startdegree);
                    point.X += (float)rcanvas.outerWidth / 2;
                    point.X -= 8;

                    point.Y *= -1;
                    point.Y += (float)rcanvas.outerHeight / 2;
                    point.Y -= 18;

                    dispList.Add(planet.isDisp);

                    Explanation exp = new Explanation()
                    {
                        degree = planet.absolute_position % 30,
                        sign = CommonData.getSignTextJp(planet.absolute_position)
                    };

                    PlanetDisplay display = new PlanetDisplay()
                    {
                        planetNo = planet.no,
                        isDisp = planet.isDisp,
                        explanation = exp,
                        planetPt = point,
                        planetTxt = CommonData.getPlanetSymbol(planet.no),
                        planetColor = CommonData.getPlanetColor(planet.no)
                    };
                    pDisplayList.Add(display);

                });

                pDisplayList.ForEach(displayData =>
                {
                    if (!displayData.isDisp)
                    {
                        return;
                    }
                    SetOnlySign(displayData);
                });

            }

            // 五重円
            else if (tempSettings.bands == 5)
            {
                int[] box = new int[60];
                for (int i = 0; i < 60; i++)
                {
                    box[i] = 0;
                }
                list1.ForEach(planet =>
                {
                    // 天体表示させない
                    if (!planet.isDisp)
                    {
                        return;
                    }

                    PointF point;
                    // 重ならないようにずらしを入れる
                    // 1サインに6度単位5個までデータが入る
                    int index = (int)(planet.absolute_position / 6);
                    if (box[index] == 1)
                    {
                        while (box[index] == 1)
                        {
                            index++;
                            if (index == 60)
                            {
                                index = 0;
                            }
                        }
                        box[index] = 1;
                    }
                    else
                    {
                        box[index] = 1;
                    }

                    point = rotate(rcanvas.outerWidth / 5, 0, 6 * index - startdegree);
                    point.X += (float)rcanvas.outerWidth / 2;
                    point.X -= 8;

                    point.Y *= -1;
                    point.Y += (float)rcanvas.outerHeight / 2;
                    point.Y -= 18;

                    dispList.Add(planet.isDisp);

                    Explanation exp = new Explanation()
                    {
                        degree = planet.absolute_position % 30,
                        sign = CommonData.getSignTextJp(planet.absolute_position)
                    };

                    PlanetDisplay display = new PlanetDisplay()
                    {
                        planetNo = planet.no,
                        isDisp = planet.isDisp,
                        explanation = exp,
                        planetPt = point,
                        planetTxt = CommonData.getPlanetSymbol(planet.no),
                        planetColor = CommonData.getPlanetColor(planet.no)
                    };
                    pDisplayList.Add(display);
                });
                for (int i = 0; i < 60; i++)
                {
                    box[i] = 0;
                }

                list2.ForEach(planet =>
                {
                    // 天体表示させない
                    if (!planet.isDisp)
                    {
                        return;
                    }

                    PointF point;
                    // 重ならないようにずらしを入れる
                    // 1サインに6度単位5個までデータが入る
                    int index = (int)(planet.absolute_position / 6);
                    if (box[index] == 1)
                    {
                        while (box[index] == 1)
                        {
                            index++;
                            if (index == 60)
                            {
                                index = 0;
                            }
                        }
                        box[index] = 1;
                    }
                    else
                    {
                        box[index] = 1;
                    }

                    point = rotate(rcanvas.outerWidth / 4, 0, 6 * index - startdegree);
                    point.X += (float)rcanvas.outerWidth / 2;
                    point.X -= 8;

                    point.Y *= -1;
                    point.Y += (float)rcanvas.outerHeight / 2;
                    point.Y -= 18;

                    dispList.Add(planet.isDisp);

                    Explanation exp = new Explanation()
                    {
                        degree = planet.absolute_position % 30,
                        sign = CommonData.getSignTextJp(planet.absolute_position)
                    };

                    PlanetDisplay display = new PlanetDisplay()
                    {
                        planetNo = planet.no,
                        isDisp = planet.isDisp,
                        explanation = exp,
                        planetPt = point,
                        planetTxt = CommonData.getPlanetSymbol(planet.no),
                        planetColor = CommonData.getPlanetColor(planet.no)
                    };
                    pDisplayList.Add(display);

                });

                pDisplayList.ForEach(displayData =>
                {
                    if (!displayData.isDisp)
                    {
                        return;
                    }
                    SetOnlySign(displayData);
                });

                for (int i = 0; i < 60; i++)
                {
                    box[i] = 0;
                }

                list3.ForEach(planet =>
                {
                    // 天体表示させない
                    if (!planet.isDisp)
                    {
                        return;
                    }

                    PointF point;
                    // 重ならないようにずらしを入れる
                    // 1サインに6度単位5個までデータが入る
                    int index = (int)(planet.absolute_position / 6);
                    if (box[index] == 1)
                    {
                        while (box[index] == 1)
                        {
                            index++;
                            if (index == 60)
                            {
                                index = 0;
                            }
                        }
                        box[index] = 1;
                    }
                    else
                    {
                        box[index] = 1;
                    }

                    point = rotate(rcanvas.outerWidth / 4 + 20, 0, 6 * index - startdegree);
                    point.X += (float)rcanvas.outerWidth / 2;
                    point.X -= 8;

                    point.Y *= -1;
                    point.Y += (float)rcanvas.outerHeight / 2;
                    point.Y -= 18;

                    dispList.Add(planet.isDisp);

                    Explanation exp = new Explanation()
                    {
                        degree = planet.absolute_position % 30,
                        sign = CommonData.getSignTextJp(planet.absolute_position)
                    };

                    PlanetDisplay display = new PlanetDisplay()
                    {
                        planetNo = planet.no,
                        isDisp = planet.isDisp,
                        explanation = exp,
                        planetPt = point,
                        planetTxt = CommonData.getPlanetSymbol(planet.no),
                        planetColor = CommonData.getPlanetColor(planet.no)
                    };
                    pDisplayList.Add(display);

                });

                pDisplayList.ForEach(displayData =>
                {
                    if (!displayData.isDisp)
                    {
                        return;
                    }
                    SetOnlySign(displayData);
                });

                for (int i = 0; i < 60; i++)
                {
                    box[i] = 0;
                }

                list4.ForEach(planet =>
                {
                    // 天体表示させない
                    if (!planet.isDisp)
                    {
                        return;
                    }

                    PointF point;
                    // 重ならないようにずらしを入れる
                    // 1サインに6度単位5個までデータが入る
                    int index = (int)(planet.absolute_position / 6);
                    if (box[index] == 1)
                    {
                        while (box[index] == 1)
                        {
                            index++;
                            if (index == 60)
                            {
                                index = 0;
                            }
                        }
                        box[index] = 1;
                    }
                    else
                    {
                        box[index] = 1;
                    }

                    point = rotate(rcanvas.outerWidth / 3, 0, 6 * index - startdegree);
                    point.X += (float)rcanvas.outerWidth / 2;
                    point.X -= 8;

                    point.Y *= -1;
                    point.Y += (float)rcanvas.outerHeight / 2;
                    point.Y -= 18;

                    dispList.Add(planet.isDisp);

                    Explanation exp = new Explanation()
                    {
                        degree = planet.absolute_position % 30,
                        sign = CommonData.getSignTextJp(planet.absolute_position)
                    };

                    PlanetDisplay display = new PlanetDisplay()
                    {
                        planetNo = planet.no,
                        isDisp = planet.isDisp,
                        explanation = exp,
                        planetPt = point,
                        planetTxt = CommonData.getPlanetSymbol(planet.no),
                        planetColor = CommonData.getPlanetColor(planet.no)
                    };
                    pDisplayList.Add(display);

                });

                pDisplayList.ForEach(displayData =>
                {
                    if (!displayData.isDisp)
                    {
                        return;
                    }
                    SetOnlySign(displayData);
                });

                for (int i = 0; i < 60; i++)
                {
                    box[i] = 0;
                }

                list5.ForEach(planet =>
                {
                    // 天体表示させない
                    if (!planet.isDisp)
                    {
                        return;
                    }

                    PointF point;
                    // 重ならないようにずらしを入れる
                    // 1サインに6度単位5個までデータが入る
                    int index = (int)(planet.absolute_position / 6);
                    if (box[index] == 1)
                    {
                        while (box[index] == 1)
                        {
                            index++;
                            if (index == 60)
                            {
                                index = 0;
                            }
                        }
                        box[index] = 1;
                    }
                    else
                    {
                        box[index] = 1;
                    }

                    point = rotate(rcanvas.outerWidth / 3 + 20, 0, 6 * index - startdegree);
                    point.X += (float)rcanvas.outerWidth / 2;
                    point.X -= 8;

                    point.Y *= -1;
                    point.Y += (float)rcanvas.outerHeight / 2;
                    point.Y -= 18;

                    dispList.Add(planet.isDisp);

                    Explanation exp = new Explanation()
                    {
                        degree = planet.absolute_position % 30,
                        sign = CommonData.getSignTextJp(planet.absolute_position)
                    };

                    PlanetDisplay display = new PlanetDisplay()
                    {
                        planetNo = planet.no,
                        isDisp = planet.isDisp,
                        explanation = exp,
                        planetPt = point,
                        planetTxt = CommonData.getPlanetSymbol(planet.no),
                        planetColor = CommonData.getPlanetColor(planet.no)
                    };
                    pDisplayList.Add(display);

                });

                pDisplayList.ForEach(displayData =>
                {
                    if (!displayData.isDisp)
                    {
                        return;
                    }
                    SetOnlySign(displayData);
                });

            }

        }


    }
}
