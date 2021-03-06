﻿using System;
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
        int[] box = new int[72];
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
                boxReset();
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
                    if (currentSetting.dispPlanet[0][planet.no] == false)
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
                    int absolute_position = getNewAbsPosition(planet);
                    int index = boxSet(absolute_position);

                    if (ringCanvas.ActualWidth < 470)
                    {
                        point = rotate(rcanvas.outerWidth / 3 - 20, 0, 5 * index - startdegree + 3);
                        pointdegree = rotate(rcanvas.outerWidth / 3 - 40, 0, 5 * index - startdegree + 3);
                    }
                    else
                    {
                        point = rotate(rcanvas.outerWidth / 3 + 20, 0, 5 * index - startdegree + 3);
                        pointdegree = rotate(rcanvas.outerWidth / 3, 0, 5 * index - startdegree + 3);
                    }
                    pointsymbol = rotate(rcanvas.outerWidth / 3 - 20, 0, 5 * index - startdegree + 3);
                    pointminute = rotate(rcanvas.outerWidth / 3 - 40, 0, 5 * index - startdegree + 3);
                    pointretrograde = rotate(rcanvas.outerWidth / 3 - 60, 0, 5 * index - startdegree + 3);
                    point = getNewPoint(point);

                    pointdegree.X += (float)rcanvas.outerWidth / 2;
                    pointdegree.X -= 8;
                    pointsymbol.X += (float)rcanvas.outerWidth / 2;
                    pointsymbol.X -= 8;
                    pointminute.X += (float)rcanvas.outerWidth / 2;
                    pointminute.X -= 8;
                    pointretrograde.X += (float)rcanvas.outerWidth / 2;
                    pointretrograde.X -= 8;

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
                        retrograde = retrograde,
                        planetNo = planet.no
                    };

                    string degreeTxt;
                    if (mainWindow.config.decimalDisp == Config.EDecimalDisp.DECIMAL)
                    {
                        degreeTxt = ((planet.absolute_position) % 30 - 0.5).ToString("00");
                    }
                    else
                    {
                        degreeTxt = ((planet.absolute_position) % 30 - 0.5).ToString("00°");
                    }
                    string minuteTxt;
                    if (mainWindow.config.decimalDisp == Config.EDecimalDisp.DECIMAL)
                    {
                        minuteTxt = ((planet.absolute_position % 1 * 100) - 0.5).ToString("00");
                    }
                    else
                    {
                        minuteTxt = ((planet.absolute_position % 1) * 60 - 0.5).ToString("00") + "'";
                    }
                    PlanetDisplay display = new PlanetDisplay()
                    {
                        planetNo = planet.no,
                        isDisp = planet.isDisp,
                        explanation = exp,
                        planetPt = point,
                        planetTxt = CommonData.getPlanetSymbol(planet.no),
                        planetColor = CommonData.getPlanetColor(planet.no),
                        degreePt = pointdegree,
                        degreeTxt = degreeTxt,
                        symbolPt = pointsymbol,
                        symbolTxt = CommonData.getSignText(planet.absolute_position),
                        minutePt = pointminute,
                        // 小数点以下切り捨て 59.9->59
                        minuteTxt = minuteTxt,
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
                    if (ringCanvas.ActualWidth < 470 || tempSettings.centerPattern == 1)
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
                boxReset();
                list1.ForEach(planet =>
                {
                    // 天体表示させない
                    if (!planet.isDisp)
                    {
                        return;
                    }
                    if (currentSetting.dispPlanet[0][planet.no] == false)
                    {
                        return;
                    }

                    PointF point;
                    int absolute_position = getNewAbsPosition(planet);
                    int index = boxSet(absolute_position);

                    point = rotate(rcanvas.outerWidth / 5 + 20, 0, 5 * index - startdegree);
                    point = getNewPoint(point);

                    dispList.Add(planet.isDisp);

                    Explanation exp = getExp(planet);
                    PlanetDisplay display = createPlanetDisplay(planet, exp, point);
                    pDisplayList.Add(display);
                });
                boxReset();

                list2.ForEach(planet =>
                {
                    // 天体表示させない
                    if (!planet.isDisp)
                    {
                        return;
                    }
                    if (currentSetting.dispPlanet[1][planet.no] == false)
                    {
                        return;
                    }

                    PointF point;
                    int absolute_position = getNewAbsPosition(planet);
                    int index = boxSet(absolute_position);

                    point = rotate(rcanvas.outerWidth / 3 + 20, 0, 5 * index - startdegree);
                    point = getNewPoint(point);

                    dispList.Add(planet.isDisp);

                    Explanation exp = getExp(planet);
                    PlanetDisplay display = createPlanetDisplay(planet, exp, point);
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
                boxReset();
                list1.ForEach(planet =>
                {
                    // 天体表示させない
                    if (!planet.isDisp)
                    {
                        return;
                    }

                    if (currentSetting.dispPlanet[0][planet.no] == false)
                    {
                        return;
                    }

                    PointF point;
                    int absolute_position = getNewAbsPosition(planet);
                    int index = boxSet(absolute_position);

                    point = rotate(rcanvas.outerWidth / 5, 0, 5 * index - startdegree);
                    point = getNewPoint(point);

                    dispList.Add(planet.isDisp);

                    Explanation exp = getExp(planet);
                    PlanetDisplay display = createPlanetDisplay(planet, exp, point);
                    pDisplayList.Add(display);
                });

                boxReset();

                list2.ForEach(planet =>
                {
                    // 天体表示させない
                    if (!planet.isDisp)
                    {
                        return;
                    }

                    if (currentSetting.dispPlanet[1][planet.no] == false)
                    {
                        return;
                    }

                    if (planet.sensitive)
                    {
                        return;
                    }

                    PointF point;
                    int absolute_position = getNewAbsPosition(planet);
                    int index = boxSet(absolute_position);

                    point = rotate(rcanvas.outerWidth / 4 + 20, 0, 5 * index - startdegree);
                    point = getNewPoint(point);

                    dispList.Add(planet.isDisp);

                    Explanation exp = getExp(planet);
                    PlanetDisplay display = createPlanetDisplay(planet, exp, point);
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

                boxReset();

                list3.ForEach(planet =>
                {
                    // 天体表示させない
                    if (!planet.isDisp)
                    {
                        return;
                    }

                    if (currentSetting.dispPlanet[2][planet.no] == false)
                    {
                        return;
                    }

                    if (planet.sensitive)
                    {
                        return;
                    }


                    PointF point;
                    int absolute_position = getNewAbsPosition(planet);
                    int index = boxSet(absolute_position);

                    point = rotate(rcanvas.outerWidth / 3 + 20, 0, 5 * index - startdegree);
                    point = getNewPoint(point);

                    dispList.Add(planet.isDisp);

                    Explanation exp = getExp(planet);
                    PlanetDisplay display = createPlanetDisplay(planet, exp, point);
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
                boxReset();
                list1.ForEach(planet =>
                {
                    // 天体表示させない
                    if (!planet.isDisp)
                    {
                        return;
                    }

                    PointF point;
                    int index = boxSet(planet.absolute_position);

                    point = rotate(rcanvas.outerWidth / 5, 0, 5 * index - startdegree);
                    point = getNewPoint(point);

                    dispList.Add(planet.isDisp);

                    Explanation exp = getExp(planet);

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

                boxReset();

                list2.ForEach(planet =>
                {
                    // 天体表示させない
                    if (!planet.isDisp)
                    {
                        return;
                    }

                    PointF point;
                    int index = boxSet(planet.absolute_position);

                    point = rotate(rcanvas.outerWidth / 4, 0, 5 * index - startdegree);
                    point = getNewPoint(point);

                    dispList.Add(planet.isDisp);

                    Explanation exp = getExp(planet);
                    PlanetDisplay display = createPlanetDisplay(planet, exp, point);
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

                boxReset();

                list3.ForEach(planet =>
                {
                    // 天体表示させない
                    if (!planet.isDisp)
                    {
                        return;
                    }

                    PointF point;
                    int index = boxSet(planet.absolute_position);

                    point = rotate(rcanvas.outerWidth / 3 - 5, 0, 5 * index - startdegree);
                    point = getNewPoint(point);

                    dispList.Add(planet.isDisp);

                    Explanation exp = getExp(planet);
                    PlanetDisplay display = createPlanetDisplay(planet, exp, point);
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

                boxReset();

                list4.ForEach(planet =>
                {
                    // 天体表示させない
                    if (!planet.isDisp)
                    {
                        return;
                    }

                    PointF point;
                    int index = boxSet(planet.absolute_position);

                    point = rotate(rcanvas.outerWidth / 3 + 20, 0, 5 * index - startdegree);
                    point = getNewPoint(point);

                    dispList.Add(planet.isDisp);

                    Explanation exp = getExp(planet);
                    PlanetDisplay display = createPlanetDisplay(planet, exp, point);
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
                boxReset();
                list1.ForEach(planet =>
                {
                    // 天体表示させない
                    if (!planet.isDisp)
                    {
                        return;
                    }

                    PointF point;
                    int index = boxSet(planet.absolute_position);

                    point = rotate(rcanvas.outerWidth / 5, 0, 5 * index - startdegree);
                    point.X += (float)rcanvas.outerWidth / 2;
                    point.X -= 8;

                    point.Y *= -1;
                    point.Y += (float)rcanvas.outerHeight / 2;
                    point.Y -= 18;

                    dispList.Add(planet.isDisp);

                    Explanation exp = getExp(planet);
                    PlanetDisplay display = createPlanetDisplay(planet, exp, point);
                    pDisplayList.Add(display);
                });

                boxReset();

                list2.ForEach(planet =>
                {
                    // 天体表示させない
                    if (!planet.isDisp)
                    {
                        return;
                    }

                    PointF point;
                    int index = boxSet(planet.absolute_position);

                    point = rotate(rcanvas.outerWidth / 4, 0, 5 * index - startdegree);
                    point = getNewPoint(point);

                    dispList.Add(planet.isDisp);

                    Explanation exp = getExp(planet);
                    PlanetDisplay display = createPlanetDisplay(planet, exp, point);
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

                boxReset();

                list3.ForEach(planet =>
                {
                    // 天体表示させない
                    if (!planet.isDisp)
                    {
                        return;
                    }

                    PointF point;
                    int index = boxSet(planet.absolute_position);

                    point = rotate(rcanvas.outerWidth / 4 + 20, 0, 5 * index - startdegree);
                    point = getNewPoint(point);

                    dispList.Add(planet.isDisp);

                    Explanation exp = getExp(planet);
                    PlanetDisplay display = createPlanetDisplay(planet, exp, point);
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

                boxReset();

                list4.ForEach(planet =>
                {
                    // 天体表示させない
                    if (!planet.isDisp)
                    {
                        return;
                    }

                    PointF point;
                    int index = boxSet(planet.absolute_position);

                    point = rotate(rcanvas.outerWidth / 3, 0, 5 * index - startdegree);
                    point = getNewPoint(point);

                    dispList.Add(planet.isDisp);

                    Explanation exp = getExp(planet);
                    PlanetDisplay display = createPlanetDisplay(planet, exp, point);
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

                boxReset();

                list5.ForEach(planet =>
                {
                    // 天体表示させない
                    if (!planet.isDisp)
                    {
                        return;
                    }

                    PointF point;
                    int index = boxSet(planet.absolute_position);

                    point = rotate(rcanvas.outerWidth / 3 + 20, 0, 5 * index - startdegree);
                    point = getNewPoint(point);

                    dispList.Add(planet.isDisp);

                    Explanation exp = getExp(planet);

                    PlanetDisplay display = createPlanetDisplay(planet, exp, point);
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

        private int boxSet(double absolute_position)
        {
            // 重ならないようにずらしを入れる
            // 1サインに6度単位5個までデータが入る
            int index = (int)(absolute_position / 5) % 72;
            if (box[index] == 1)
            {
                while (box[index] == 1)
                {
                    index++;
                    index = boxMax(index);
                }
                box[index] = 1;
            }
            else
            {
                box[index] = 1;
            }
            return index;
        }

        private void boxReset()
        {
            for (int i = 0; i < 72; i++)
            {
                box[i] = 0;
            }
        }

        private int boxMax(int index)
        {
            if (index == 72)
            {
                return 0;
            }
            return index;
        }

        private int getNewAbsPosition(PlanetData planet)
        {
            int absolute_position = 0;
            if (planet.absolute_position < 0)
            {
                absolute_position = (int)planet.absolute_position + 360;
                if (absolute_position == 360)
                {
                    absolute_position = 0;
                }
            }
            else
            {
                absolute_position = (int)planet.absolute_position;
            }

            return absolute_position;
        }


        private PointF getNewPoint(PointF point)
        {
            point.X += (float)rcanvas.outerWidth / 2;
            point.X -= 8;

            point.Y *= -1;
            point.Y += (float)rcanvas.outerHeight / 2;
            point.Y -= 18;
            return point;
        }

        private Explanation getExp(PlanetData planet)
        {
            bool retrograde;
            if (planet.speed < 0)
            {
                retrograde = true;
            }
            else
            {
                retrograde = false;
            }

            return new Explanation()
            {
                degree = planet.absolute_position % 30,
                sign = CommonData.getSignTextJp(planet.absolute_position),
                planetNo = planet.no,
                planet = CommonData.getPlanetText(planet.no),
                retrograde = retrograde,

            };
        }

        private PlanetDisplay createPlanetDisplay(PlanetData planet, Explanation exp, PointF point)
        {
            return new PlanetDisplay()
            {
                planetNo = planet.no,
                isDisp = planet.isDisp,
                explanation = exp,
                planetPt = point,
                planetTxt = CommonData.getPlanetSymbol(planet.no),
                planetColor = CommonData.getPlanetColor(planet.no)
            };
        }

    }
}
