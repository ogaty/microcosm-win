﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

using System.Xml.Serialization;
using microcosm.DB;
using microcosm.ViewModel;
using microcosm.Config;
using microcosm.Calc;
using microcosm.Planet;
using System.Drawing;
using microcosm.Common;

namespace microcosm
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public PlanetListViewModel firstPList;
        public HouseListViewModel houseList;

        public SettingData[] settings = new SettingData[10];
        public ConfigData config = new ConfigData();
        public TempSetting tempsettings;

        public AstroCalc calc;
        public RingCanvasViewModel rcanvas;
        public ReportViewModel reportVM;

        public UserEventData userdata;
        public Line[] cusps = new Line[12];

        List<PlanetData> list1;
        List<PlanetData> list2;
        List<PlanetData> list3;
        List<PlanetData> list4;
        List<PlanetData> list5;
        List<PlanetData> list6;

        public double[] houseList1;
        public double[] houseList2;
        public double[] houseList3;
        public double[] houseList4;
        public double[] houseList5;
        public double[] houseList6;

        public MainWindow()
        {
            InitializeComponent();

            DataInit();
            DataCalc();
            SetViewModel();
            
        }

        // データ初期化
        private void DataInit()
        {
            tempsettings = new TempSetting(config);
            Enumerable.Range(0, 10).ToList().ForEach(i =>
            {
                string filename = "setting" + i + ".csm";
                settings[i] = new SettingData(i);

                if (!File.Exists(filename))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(SettingXml));
                    FileStream fs = new FileStream(filename, FileMode.Create);
                    StreamWriter sw = new StreamWriter(fs);
                    serializer.Serialize(sw, settings[i].xmlData);
                    sw.Close();
                    fs.Close();
                } else
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(SettingXml));
                    FileStream fs = new FileStream(filename, FileMode.Open);
                    settings[i].xmlData = (SettingXml)serializer.Deserialize(fs);
                    fs.Close();
                }

            });

            {
                string filename = "config.csm";
                if (!File.Exists(filename))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(ConfigData));
                    FileStream fs = new FileStream(filename, FileMode.Create);
                    StreamWriter sw = new StreamWriter(fs);
                    serializer.Serialize(sw, config);
                    sw.Close();
                    fs.Close();
                } else
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(ConfigData));
                    FileStream fs = new FileStream(filename, FileMode.Open);
                    config = (ConfigData)serializer.Deserialize(fs);
                    fs.Close();
                }
            }

            rcanvas = new RingCanvasViewModel(config);
            cusps[0] = cusp1;
            cusps[1] = cusp2;
            cusps[2] = cusp3;
            cusps[3] = cusp4;
            cusps[4] = cusp5;
            cusps[5] = cusp6;
            cusps[6] = cusp7;
            cusps[7] = cusp8;
            cusps[8] = cusp9;
            cusps[9] = cusp10;
            cusps[10] = cusp11;
            cusps[11] = cusp12;

        }

        private void DataCalc()
        {
            calc = new AstroCalc(config);

        }

        private void SetViewModel()
        {
            UserData initUser = new UserData(config);
            UserBinding ub = new UserBinding(initUser);
            TransitBinding tb = new TransitBinding(initUser);
            this.DataContext = new
            {
                userName = initUser.name,
                userBirthStr = ub.birthStr,
                userTimezone = initUser.timezone,
                userBirthPlace = initUser.birth_place,
                userLat = String.Format("{0:f4}", initUser.lat),
                userLng = String.Format("{0:f4}", initUser.lng),
                transitName = "イベント未設定",
                transitBirthStr = tb.birthStr,
                transitTimezone = initUser.timezone,
                transitPlace = initUser.birth_place,
                transitLat = String.Format("{0:f4}", initUser.lat),
                transitLng = String.Format("{0:f4}", initUser.lng),
            };

            list1 = calc.PositionCalc(initUser.birth_year, initUser.birth_month, initUser.birth_day, initUser.birth_hour, initUser.birth_minute, initUser.birth_second, initUser.lat, initUser.lng);
            list2 = calc.PositionCalc(initUser.birth_year, initUser.birth_month, initUser.birth_day, initUser.birth_hour, initUser.birth_minute, initUser.birth_second, initUser.lat, initUser.lng);
            list3 = calc.PositionCalc(initUser.birth_year, initUser.birth_month, initUser.birth_day, initUser.birth_hour, initUser.birth_minute, initUser.birth_second, initUser.lat, initUser.lng);
            list4 = calc.PositionCalc(initUser.birth_year, initUser.birth_month, initUser.birth_day, initUser.birth_hour, initUser.birth_minute, initUser.birth_second, initUser.lat, initUser.lng);
            list5 = calc.PositionCalc(initUser.birth_year, initUser.birth_month, initUser.birth_day, initUser.birth_hour, initUser.birth_minute, initUser.birth_second, initUser.lat, initUser.lng);
            list6 = calc.PositionCalc(initUser.birth_year, initUser.birth_month, initUser.birth_day, initUser.birth_hour, initUser.birth_minute, initUser.birth_second, initUser.lat, initUser.lng);

            houseList1 = calc.CuspCalc(initUser.birth_year, initUser.birth_month, initUser.birth_day, initUser.birth_hour, initUser.birth_minute, initUser.birth_second, initUser.lat, initUser.lng, config.houseCalc);
            houseList2 = calc.CuspCalc(initUser.birth_year, initUser.birth_month, initUser.birth_day, initUser.birth_hour, initUser.birth_minute, initUser.birth_second, initUser.lat, initUser.lng, config.houseCalc);
            houseList3 = calc.CuspCalc(initUser.birth_year, initUser.birth_month, initUser.birth_day, initUser.birth_hour, initUser.birth_minute, initUser.birth_second, initUser.lat, initUser.lng, config.houseCalc);
            houseList4 = calc.CuspCalc(initUser.birth_year, initUser.birth_month, initUser.birth_day, initUser.birth_hour, initUser.birth_minute, initUser.birth_second, initUser.lat, initUser.lng, config.houseCalc);
            houseList5 = calc.CuspCalc(initUser.birth_year, initUser.birth_month, initUser.birth_day, initUser.birth_hour, initUser.birth_minute, initUser.birth_second, initUser.lat, initUser.lng, config.houseCalc);
            houseList6 = calc.CuspCalc(initUser.birth_year, initUser.birth_month, initUser.birth_day, initUser.birth_hour, initUser.birth_minute, initUser.birth_second, initUser.lat, initUser.lng, config.houseCalc);

            //viewmodel設定
            firstPList = new PlanetListViewModel(list1, list2, list3, list4, list5, list6);
            planetList.DataContext = firstPList;

            houseList = new HouseListViewModel(houseList1, houseList2, houseList3, houseList4, houseList5, houseList6);
            cuspList.DataContext = houseList;

            outerRing.DataContext = rcanvas;
            innerRing.DataContext = rcanvas;
            centerRing.DataContext = rcanvas;
            cusps[0].DataContext = rcanvas;
            cusps[1].DataContext = rcanvas;
            cusps[2].DataContext = rcanvas;
            cusps[3].DataContext = rcanvas;
            cusps[4].DataContext = rcanvas;
            cusps[5].DataContext = rcanvas;
            cusps[6].DataContext = rcanvas;
            cusps[7].DataContext = rcanvas;
            cusps[8].DataContext = rcanvas;
            cusps[9].DataContext = rcanvas;
            cusps[10].DataContext = rcanvas;
            cusps[11].DataContext = rcanvas;
            ariesSymbol.DataContext = rcanvas;

            reportVM = new ReportViewModel(
                list1,
                list2, 
                list3,
                list4,
                list5,
                list6,
                houseList1, 
                houseList2, 
                houseList3, 
                houseList4, 
                houseList5, 
                houseList6);
            houseDown.DataContext = reportVM;
            houseRight.DataContext = reportVM;
            houseUp.DataContext = reportVM;
            houseLeft.DataContext = reportVM;
            signFire.DataContext = reportVM;
            signEarth.DataContext = reportVM;
            signAir.DataContext = reportVM;
            signWater.DataContext = reportVM;
            signCardinal.DataContext = reportVM;
            signFixed.DataContext = reportVM;
            signMutable.DataContext = reportVM;
            houseAngular.DataContext = reportVM;
            houseSuccedent.DataContext = reportVM;
            houseCadent.DataContext = reportVM;

        }

        private void Ellipse_MouseEnter(object sender, MouseEventArgs e)
        {
        }

        private void mainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            rcanvas.innerLeft = config.zodiacWidth / 2;
            rcanvas.innerTop = config.zodiacWidth / 2;
            if (ringCanvas.ActualWidth > ringStack.ActualHeight)
            {
                rcanvas.outerWidth = ringStack.ActualHeight;
                rcanvas.outerHeight = ringStack.ActualHeight;
                rcanvas.innerWidth = ringStack.ActualHeight - config.zodiacWidth;
                rcanvas.innerHeight = ringStack.ActualHeight - config.zodiacWidth;
                rcanvas.centerLeft = ringStack.ActualHeight / 2 - config.zodiacCenter / 2;
                rcanvas.centerTop = ringStack.ActualHeight / 2 - config.zodiacCenter / 2;
            }
            else
            {
                rcanvas.outerWidth = ringCanvas.ActualWidth;
                rcanvas.outerHeight = ringCanvas.ActualWidth;
                rcanvas.innerWidth = ringCanvas.ActualWidth - config.zodiacWidth;
                rcanvas.innerHeight = ringCanvas.ActualWidth - config.zodiacWidth;
                rcanvas.centerLeft = ringCanvas.ActualWidth / 2 - config.zodiacCenter / 2;
                rcanvas.centerTop = ringCanvas.ActualWidth / 2 - config.zodiacCenter / 2;
            }

            houseCuspRender(houseList1);
            zodiacRender(houseList1[0]);
            planetRender(houseList1[0], list1, list2, list3);

            Console.WriteLine(ringCanvas.ActualWidth.ToString() + "," + ringStack.ActualHeight.ToString());
        }

        // ハウスカスプレンダリング
        private void houseCuspRender(double[] natalcusp)
        {
            //内側がstart, 外側がend
            double startX = config.zodiacCenter / 2;
            double endX = rcanvas.outerWidth / 2;

            double startY = 0;
            double endY = 0;
            List<PointF[]> pList = new List<PointF[]>();
            Enumerable.Range(1, 12).ToList().ForEach(i =>
            {
                double degree = natalcusp[i] - natalcusp[1];

                PointF newStart = rotate(startX, startY, degree);
                newStart.X += (float)rcanvas.outerWidth / 2;
                // Formの座標は下がプラス、数学では上がマイナス
                newStart.Y = newStart.Y * -1;
                newStart.Y += (float)rcanvas.outerHeight / 2;

                PointF newEnd = rotate(endX, endY, degree);
                newEnd.X += (float)rcanvas.outerWidth / 2;
                // Formの座標は下がプラス、数学では上がマイナス
                newEnd.Y = newEnd.Y * -1;
                newEnd.Y += (float)rcanvas.outerHeight / 2;

                PointF[] pointList = new PointF[2];
                pointList[0] = newStart;
                pointList[1] = newEnd;
                pList.Add(pointList);

            });

            rcanvas.cusp1x1 = pList[0][0].X;
            rcanvas.cusp1y1 = pList[0][0].Y;
            rcanvas.cusp1x2 = pList[0][1].X;
            rcanvas.cusp1y2 = pList[0][1].Y;

            rcanvas.cusp2x1 = pList[1][0].X;
            rcanvas.cusp2y1 = pList[1][0].Y;
            rcanvas.cusp2x2 = pList[1][1].X;
            rcanvas.cusp2y2 = pList[1][1].Y;

            rcanvas.cusp3x1 = pList[2][0].X;
            rcanvas.cusp3y1 = pList[2][0].Y;
            rcanvas.cusp3x2 = pList[2][1].X;
            rcanvas.cusp3y2 = pList[2][1].Y;

            rcanvas.cusp4x1 = pList[3][0].X;
            rcanvas.cusp4y1 = pList[3][0].Y;
            rcanvas.cusp4x2 = pList[3][1].X;
            rcanvas.cusp4y2 = pList[3][1].Y;

            rcanvas.cusp5x1 = pList[4][0].X;
            rcanvas.cusp5y1 = pList[4][0].Y;
            rcanvas.cusp5x2 = pList[4][1].X;
            rcanvas.cusp5y2 = pList[4][1].Y;

            rcanvas.cusp6x1 = pList[5][0].X;
            rcanvas.cusp6y1 = pList[5][0].Y;
            rcanvas.cusp6x2 = pList[5][1].X;
            rcanvas.cusp6y2 = pList[5][1].Y;

            rcanvas.cusp7x1 = pList[6][0].X;
            rcanvas.cusp7y1 = pList[6][0].Y;
            rcanvas.cusp7x2 = pList[6][1].X;
            rcanvas.cusp7y2 = pList[6][1].Y;

            rcanvas.cusp8x1 = pList[7][0].X;
            rcanvas.cusp8y1 = pList[7][0].Y;
            rcanvas.cusp8x2 = pList[7][1].X;
            rcanvas.cusp8y2 = pList[7][1].Y;

            rcanvas.cusp9x1 = pList[8][0].X;
            rcanvas.cusp9y1 = pList[8][0].Y;
            rcanvas.cusp9x2 = pList[8][1].X;
            rcanvas.cusp9y2 = pList[8][1].Y;

            rcanvas.cusp10x1 = pList[9][0].X;
            rcanvas.cusp10y1 = pList[9][0].Y;
            rcanvas.cusp10x2 = pList[9][1].X;
            rcanvas.cusp10y2 = pList[9][1].Y;

            rcanvas.cusp11x1 = pList[10][0].X;
            rcanvas.cusp11y1 = pList[10][0].Y;
            rcanvas.cusp11x2 = pList[10][1].X;
            rcanvas.cusp11y2 = pList[10][1].Y;

            rcanvas.cusp12x1 = pList[11][0].X;
            rcanvas.cusp12y1 = pList[11][0].Y;
            rcanvas.cusp12x2 = pList[11][1].X;
            rcanvas.cusp12y2 = pList[11][1].Y;

        }

        // サインカスプレンダリング
        private void signCuspRender(double startdegree)
        {
            double startX = config.zodiacCenter / 2;
            double endX = rcanvas.outerWidth / 2;

            double startY = 0;
            double endY = 0;
            List<PointF[]> pList = new List<PointF[]>();
            
            Enumerable.Range(1, 12).ToList().ForEach(i =>
            {
                double degree = (30.0 * i) - startdegree;

                PointF newStart = rotate(startX, startY, degree);
                newStart.X += (float)rcanvas.outerWidth / 2;
                // Formの座標は下がプラス、数学では上がマイナス
                newStart.Y = newStart.Y * -1;
                newStart.Y += (float)rcanvas.outerHeight / 2;

                PointF newEnd = rotate(endX, endY, degree);
                newEnd.X += (float)rcanvas.outerWidth / 2;
                // Formの座標は下がプラス、数学では上がマイナス
                newEnd.Y = newEnd.Y * -1;
                newEnd.Y += (float)rcanvas.outerHeight / 2;

                PointF[] pointList = new PointF[2];
                pointList[0] = newStart;
                pointList[1] = newEnd;
                pList.Add(pointList);
            });

            rcanvas.cusp1x1 = pList[0][0].X;
            rcanvas.cusp1y1 = pList[0][0].Y;
            rcanvas.cusp1x2 = pList[0][1].X;
            rcanvas.cusp1y2 = pList[0][1].Y;

            rcanvas.cusp2x1 = pList[1][0].X;
            rcanvas.cusp2y1 = pList[1][0].Y;
            rcanvas.cusp2x2 = pList[1][1].X;
            rcanvas.cusp2y2 = pList[1][1].Y;

            rcanvas.cusp3x1 = pList[2][0].X;
            rcanvas.cusp3y1 = pList[2][0].Y;
            rcanvas.cusp3x2 = pList[2][1].X;
            rcanvas.cusp3y2 = pList[2][1].Y;

            rcanvas.cusp4x1 = pList[3][0].X;
            rcanvas.cusp4y1 = pList[3][0].Y;
            rcanvas.cusp4x2 = pList[3][1].X;
            rcanvas.cusp4y2 = pList[3][1].Y;

            rcanvas.cusp5x1 = pList[4][0].X;
            rcanvas.cusp5y1 = pList[4][0].Y;
            rcanvas.cusp5x2 = pList[4][1].X;
            rcanvas.cusp5y2 = pList[4][1].Y;

            rcanvas.cusp6x1 = pList[5][0].X;
            rcanvas.cusp6y1 = pList[5][0].Y;
            rcanvas.cusp6x2 = pList[5][1].X;
            rcanvas.cusp6y2 = pList[5][1].Y;

            rcanvas.cusp7x1 = pList[6][0].X;
            rcanvas.cusp7y1 = pList[6][0].Y;
            rcanvas.cusp7x2 = pList[6][1].X;
            rcanvas.cusp7y2 = pList[6][1].Y;

            rcanvas.cusp8x1 = pList[7][0].X;
            rcanvas.cusp8y1 = pList[7][0].Y;
            rcanvas.cusp8x2 = pList[7][1].X;
            rcanvas.cusp8y2 = pList[7][1].Y;

            rcanvas.cusp9x1 = pList[8][0].X;
            rcanvas.cusp9y1 = pList[8][0].Y;
            rcanvas.cusp9x2 = pList[8][1].X;
            rcanvas.cusp9y2 = pList[8][1].Y;

            rcanvas.cusp10x1 = pList[9][0].X;
            rcanvas.cusp10y1 = pList[9][0].Y;
            rcanvas.cusp10x2 = pList[9][1].X;
            rcanvas.cusp10y2 = pList[9][1].Y;

            rcanvas.cusp11x1 = pList[10][0].X;
            rcanvas.cusp11y1 = pList[10][0].Y;
            rcanvas.cusp11x2 = pList[10][1].X;
            rcanvas.cusp11y2 = pList[10][1].Y;

            rcanvas.cusp12x1 = pList[11][0].X;
            rcanvas.cusp12y1 = pList[11][0].Y;
            rcanvas.cusp12x2 = pList[11][1].X;
            rcanvas.cusp12y2 = pList[11][1].Y;

        }

        // zodiac文字列描画
        private void zodiacRender(double startdegree)
        {
            List<PointF> pList = new List<PointF>();
            Enumerable.Range(0, 12).ToList().ForEach(i =>
            {
                PointF point = rotate(rcanvas.outerWidth / 2 - 20, 0, (30 * (i + 1)) - startdegree - 15.0);
                point.X += (float)rcanvas.outerWidth / 2;
//                point.X -= (float)rcanvas.outerWidth - (float)rcanvas.innerWidth;
                point.Y *= -1;
                point.Y += (float)rcanvas.outerHeight / 2;
//                point.Y -= (float)rcanvas.outerHeight - (float)rcanvas.innerHeight;
                pList.Add(point);
            });

            rcanvas.ariestxt = CommonData.getSignText(0);
            rcanvas.ariesx = pList[0].X;
            rcanvas.ariesy = pList[0].Y;
        }

        // 天体の表示
        private void planetRender(double startdegree, List<PlanetData> natallist,
            List<PlanetData> progresslist,
            List<PlanetData> transitlist)
        {
            List<double> degreeList = new List<double>();
            List<PointF> pList = new List<PointF>();

            if (tempsettings.bands == 1)
            {
                natallist.ForEach(planet =>
                {
                    // 天体表示させない
                    if (!planet.isDisp)
                    {
                        return;
                    }

                    int offset = 0;
                    if (0 < degreeList.Find(p => p < planet.absolute_position + 4 && p > planet.absolute_position - 4))
                    {
                        offset = 15;
                    }
                    PointF point;
                    if (tempsettings.bands == 1)
                    {
                        point = rotate(rcanvas.outerWidth - offset, 0, planet.absolute_position - startdegree);
                    }
                    else if (tempsettings.bands == 2)
                    {
                        point = rotate(rcanvas.outerWidth - offset, 0, planet.absolute_position - startdegree);
                    }
                    else
                    {
                        point = rotate(rcanvas.outerWidth - offset, 0, planet.absolute_position - startdegree);
                    }
                    degreeList.Add(planet.absolute_position);
                    point.X += (float)rcanvas.outerWidth / 2;
                    point.X -= 8;
                    point.Y *= -1;
                    point.Y += (float)rcanvas.outerHeight / 2;
                    point.Y -= 15;
                    pList.Add(point);
//                    g.DrawString(CommonData.getPlanetSymbol(planet.no), fnt, brush, point.X, point.Y);
                    Console.WriteLine(planet.absolute_position - startdegree);
                });

                rcanvas.ariestxt = CommonData.getSignText(0);
                rcanvas.ariesx = pList[0].X;
                rcanvas.ariesy = pList[0].Y;
                rcanvas.taurusx = pList[1].X;
                rcanvas.taurusy = pList[1].Y;
            }
            else if (tempsettings.bands == 2)
            {
            }
            else if (tempsettings.bands == 3)
            {
            }
        }


        public void UsernameRefresh()
        {

        }

        private void OpenDatabase_Click(object sender, RoutedEventArgs e)
        {
            DatabaseWindow dbwin = new DatabaseWindow(this);
            dbwin.Show();
        }

        private void mainWindow_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        // ポイントの回転
        // 左サイドバーのwidthマイナスして呼び、後で足すこと
        protected PointF rotate(double x, double y, double degree)
        {
            // ホロスコープは180°から始まる
            degree += 180.0;

            double rad = (degree / 180.0) * Math.PI;
            double newX = x * Math.Cos(rad) - y * Math.Sin(rad);
            double newY = x * Math.Sin(rad) + y * Math.Cos(rad);


            return new PointF((float)newX, (float)newY);
        }

    }
}
