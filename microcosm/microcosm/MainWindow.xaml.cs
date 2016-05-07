using System;
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
        public Line[] scusps = new Line[12];

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

        public CommonConfigWindow configWindow;

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
                string filename = @"system\setting" + i + ".csm";
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
                string filename = @"system\config.csm";
                if (!File.Exists(filename))
                {
                    // 生成も
                    XmlSerializer serializer = new XmlSerializer(typeof(ConfigData));
                    FileStream fs = new FileStream(filename, FileMode.Create);
                    StreamWriter sw = new StreamWriter(fs);
                    serializer.Serialize(sw, config);
                    sw.Close();
                    fs.Close();
                } else
                {
                    // 読み込み
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
            scusps[0] = scusp1;
            scusps[1] = scusp2;
            scusps[2] = scusp3;
            scusps[3] = scusp4;
            scusps[4] = scusp5;
            scusps[5] = scusp6;
            scusps[6] = scusp7;
            scusps[7] = scusp8;
            scusps[8] = scusp9;
            scusps[9] = scusp10;
            scusps[10] = scusp11;
            scusps[11] = scusp12;

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
            scusps[0].DataContext = rcanvas;
            scusps[1].DataContext = rcanvas;
            scusps[2].DataContext = rcanvas;
            scusps[3].DataContext = rcanvas;
            scusps[4].DataContext = rcanvas;
            scusps[5].DataContext = rcanvas;
            scusps[6].DataContext = rcanvas;
            scusps[7].DataContext = rcanvas;
            scusps[8].DataContext = rcanvas;
            scusps[9].DataContext = rcanvas;
            scusps[10].DataContext = rcanvas;
            scusps[11].DataContext = rcanvas;
            ariesSymbol.DataContext = rcanvas;
            taurusSymbol.DataContext = rcanvas;
            geminiSymbol.DataContext = rcanvas;
            cancerSymbol.DataContext = rcanvas;
            leoSymbol.DataContext = rcanvas;
            virgoSymbol.DataContext = rcanvas;
            libraSymbol.DataContext = rcanvas;
            scorpionSymbol.DataContext = rcanvas;
            sagittariusSymbol.DataContext = rcanvas;
            capricornSymbol.DataContext = rcanvas;
            aquariusSymbol.DataContext = rcanvas;
            piscesSymbol.DataContext = rcanvas;
            natalSunSymbol.DataContext = rcanvas;
            natalSunDegree.DataContext = rcanvas;
            natalSunSign.DataContext = rcanvas;
            natalSunMinute.DataContext = rcanvas;
            natalSunRetrograde.DataContext = rcanvas;
            natalMoonSymbol.DataContext = rcanvas;
            natalMoonDegree.DataContext = rcanvas;
            natalMoonSign.DataContext = rcanvas;
            natalMoonMinute.DataContext = rcanvas;
            natalMoonRetrograde.DataContext = rcanvas;
            natalMercurySymbol.DataContext = rcanvas;
            natalMercuryDegree.DataContext = rcanvas;
            natalMercurySign.DataContext = rcanvas;
            natalMercuryMinute.DataContext = rcanvas;
            natalMercuryRetrograde.DataContext = rcanvas;
            natalVenusSymbol.DataContext = rcanvas;
            natalVenusDegree.DataContext = rcanvas;
            natalVenusSign.DataContext = rcanvas;
            natalVenusMinute.DataContext = rcanvas;
            natalVenusRetrograde.DataContext = rcanvas;
            natalMarsSymbol.DataContext = rcanvas;
            natalMarsDegree.DataContext = rcanvas;
            natalMarsSign.DataContext = rcanvas;
            natalMarsMinute.DataContext = rcanvas;
            natalMarsRetrograde.DataContext = rcanvas;
            natalJupiterSymbol.DataContext = rcanvas;
            natalJupiterDegree.DataContext = rcanvas;
            natalJupiterSign.DataContext = rcanvas;
            natalJupiterMinute.DataContext = rcanvas;
            natalJupiterRetrograde.DataContext = rcanvas;
            natalSaturnSymbol.DataContext = rcanvas;
            natalSaturnDegree.DataContext = rcanvas;
            natalSaturnSign.DataContext = rcanvas;
            natalSaturnMinute.DataContext = rcanvas;
            natalSaturnRetrograde.DataContext = rcanvas;
            natalUranusSymbol.DataContext = rcanvas;
            natalUranusDegree.DataContext = rcanvas;
            natalUranusSign.DataContext = rcanvas;
            natalUranusMinute.DataContext = rcanvas;
            natalUranusRetrograde.DataContext = rcanvas;
            natalNeptuneSymbol.DataContext = rcanvas;
            natalNeptuneDegree.DataContext = rcanvas;
            natalNeptuneSign.DataContext = rcanvas;
            natalNeptuneMinute.DataContext = rcanvas;
            natalNeptuneRetrograde.DataContext = rcanvas;
            natalPlutoSymbol.DataContext = rcanvas;
            natalPlutoDegree.DataContext = rcanvas;
            natalPlutoSign.DataContext = rcanvas;
            natalPlutoMinute.DataContext = rcanvas;
            natalPlutoRetrograde.DataContext = rcanvas;

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
            signCuspRender(houseList1[1]);
            zodiacRender(houseList1[1]);
            planetRender(houseList1[1], list1, list2, list3);

            Console.WriteLine(ringCanvas.ActualWidth.ToString() + "," + ringStack.ActualHeight.ToString());
        }

        // ハウスカスプレンダリング
        private void houseCuspRender(double[] natalcusp)
        {
            //内側がstart, 外側がend
            double startX = config.zodiacCenter / 2;
            double endX = rcanvas.innerWidth / 2;

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
            double startX = rcanvas.innerWidth / 2;
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

            rcanvas.scusp1x1 = pList[0][0].X;
            rcanvas.scusp1y1 = pList[0][0].Y;
            rcanvas.scusp1x2 = pList[0][1].X;
            rcanvas.scusp1y2 = pList[0][1].Y;

            rcanvas.scusp2x1 = pList[1][0].X;
            rcanvas.scusp2y1 = pList[1][0].Y;
            rcanvas.scusp2x2 = pList[1][1].X;
            rcanvas.scusp2y2 = pList[1][1].Y;

            rcanvas.scusp3x1 = pList[2][0].X;
            rcanvas.scusp3y1 = pList[2][0].Y;
            rcanvas.scusp3x2 = pList[2][1].X;
            rcanvas.scusp3y2 = pList[2][1].Y;

            rcanvas.scusp4x1 = pList[3][0].X;
            rcanvas.scusp4y1 = pList[3][0].Y;
            rcanvas.scusp4x2 = pList[3][1].X;
            rcanvas.scusp4y2 = pList[3][1].Y;

            rcanvas.scusp5x1 = pList[4][0].X;
            rcanvas.scusp5y1 = pList[4][0].Y;
            rcanvas.scusp5x2 = pList[4][1].X;
            rcanvas.scusp5y2 = pList[4][1].Y;

            rcanvas.scusp6x1 = pList[5][0].X;
            rcanvas.scusp6y1 = pList[5][0].Y;
            rcanvas.scusp6x2 = pList[5][1].X;
            rcanvas.scusp6y2 = pList[5][1].Y;

            rcanvas.scusp7x1 = pList[6][0].X;
            rcanvas.scusp7y1 = pList[6][0].Y;
            rcanvas.scusp7x2 = pList[6][1].X;
            rcanvas.scusp7y2 = pList[6][1].Y;

            rcanvas.scusp8x1 = pList[7][0].X;
            rcanvas.scusp8y1 = pList[7][0].Y;
            rcanvas.scusp8x2 = pList[7][1].X;
            rcanvas.scusp8y2 = pList[7][1].Y;

            rcanvas.scusp9x1 = pList[8][0].X;
            rcanvas.scusp9y1 = pList[8][0].Y;
            rcanvas.scusp9x2 = pList[8][1].X;
            rcanvas.scusp9y2 = pList[8][1].Y;

            rcanvas.scusp10x1 = pList[9][0].X;
            rcanvas.scusp10y1 = pList[9][0].Y;
            rcanvas.scusp10x2 = pList[9][1].X;
            rcanvas.scusp10y2 = pList[9][1].Y;

            rcanvas.scusp11x1 = pList[10][0].X;
            rcanvas.scusp11y1 = pList[10][0].Y;
            rcanvas.scusp11x2 = pList[10][1].X;
            rcanvas.scusp11y2 = pList[10][1].Y;

            rcanvas.scusp12x1 = pList[11][0].X;
            rcanvas.scusp12y1 = pList[11][0].Y;
            rcanvas.scusp12x2 = pList[11][1].X;
            rcanvas.scusp12y2 = pList[11][1].Y;

        }

        // zodiac文字列描画
        private void zodiacRender(double startdegree)
        {
            List<PointF> pList = new List<PointF>();
            Enumerable.Range(0, 12).ToList().ForEach(i =>
            {
                PointF point = rotate(rcanvas.outerWidth / 2 - 18, 0, (30 * (i + 1)) - startdegree - 15.0);
                point.X += (float)rcanvas.outerWidth / 2 - 10;
//                point.X -= (float)rcanvas.outerWidth - (float)rcanvas.innerWidth;
                point.Y *= -1;
                point.Y += (float)rcanvas.outerHeight / 2 - 12;
//                point.Y -= (float)rcanvas.outerHeight - (float)rcanvas.innerHeight;
                pList.Add(point);
            });

            rcanvas.ariestxt = CommonData.getSignSymbol(0);
            rcanvas.ariesx = pList[0].X;
            rcanvas.ariesy = pList[0].Y;
            rcanvas.taurustxt = CommonData.getSignSymbol(1);
            rcanvas.taurusx = pList[1].X;
            rcanvas.taurusy = pList[1].Y;
            rcanvas.geminitxt = CommonData.getSignSymbol(2);
            rcanvas.geminix = pList[2].X;
            rcanvas.geminiy = pList[2].Y;
            rcanvas.cancertxt = CommonData.getSignSymbol(3);
            rcanvas.cancerx = pList[3].X;
            rcanvas.cancery = pList[3].Y;
            rcanvas.leotxt = CommonData.getSignSymbol(4);
            rcanvas.leox = pList[4].X;
            rcanvas.leoy = pList[4].Y;
            rcanvas.virgotxt = CommonData.getSignSymbol(5);
            rcanvas.virgox = pList[5].X;
            rcanvas.virgoy = pList[5].Y;
            rcanvas.libratxt = CommonData.getSignSymbol(6);
            rcanvas.librax = pList[6].X;
            rcanvas.libray = pList[6].Y;
            rcanvas.scorpiontxt = CommonData.getSignSymbol(7);
            rcanvas.scorpionx = pList[7].X;
            rcanvas.scorpiony = pList[7].Y;
            rcanvas.sagittariustxt = CommonData.getSignSymbol(8);
            rcanvas.sagittariusx = pList[8].X;
            rcanvas.sagittariusy = pList[8].Y;
            rcanvas.capricorntxt = CommonData.getSignSymbol(9);
            rcanvas.capricornx = pList[9].X;
            rcanvas.capricorny = pList[9].Y;
            rcanvas.aquariustxt = CommonData.getSignSymbol(10);
            rcanvas.aquariusx = pList[10].X;
            rcanvas.aquariusy = pList[10].Y;
            rcanvas.piscestxt = CommonData.getSignSymbol(11);
            rcanvas.piscesx = pList[11].X;
            rcanvas.piscesy = pList[11].Y;
        }

        // 天体の表示
        private void planetRender(double startdegree, List<PlanetData> natallist,
            List<PlanetData> progresslist,
            List<PlanetData> transitlist)
        {
            List<double> degreeList = new List<double>();
            List<double> retrogradeList = new List<double>();
            List<bool> dispList = new List<bool>();
            List<PointF> pList = new List<PointF>();
            List<PointF> pDegList = new List<PointF>();
            List<PointF> pSymbolList = new List<PointF>();
            List<PointF> pMinuteList = new List<PointF>();
            List<PointF> pRetrogradeList = new List<PointF>();

            if (tempsettings.bands == 1)
            {
                natallist.ForEach(planet =>
                {
                    // 天体表示させない
                    if (!planet.isDisp)
                    {
                        return;
                    }

                    PointF point;
                    PointF pointdegree;
                    PointF pointsymbol;
                    PointF pointminute;
                    PointF pointretrograde;
                    if (tempsettings.bands == 1)
                    {
                        point = rotate(rcanvas.outerWidth / 3 + 20, 0, planet.absolute_position - startdegree);
                        pointdegree = rotate(rcanvas.outerWidth / 3, 0, planet.absolute_position - startdegree);
                        pointsymbol = rotate(rcanvas.outerWidth / 3 - 20, 0, planet.absolute_position - startdegree);
                        pointminute = rotate(rcanvas.outerWidth / 3 - 40, 0, planet.absolute_position - startdegree);
                        pointretrograde = rotate(rcanvas.outerWidth / 3 - 60, 0, planet.absolute_position - startdegree);
                    }
                    else if (tempsettings.bands == 2)
                    {
                        point = rotate(rcanvas.outerWidth / 2 + 20 - rcanvas.innerWidth / 2, 0, planet.absolute_position - startdegree);
                        pointdegree = rotate(rcanvas.outerWidth / 3, 0, planet.absolute_position - startdegree);
                        pointsymbol = rotate(rcanvas.outerWidth / 3 - 20, 0, planet.absolute_position - startdegree);
                        pointminute = rotate(rcanvas.outerWidth / 3 - 40, 0, planet.absolute_position - startdegree);
                        pointretrograde = rotate(rcanvas.outerWidth / 3 - 60, 0, planet.absolute_position - startdegree);
                    }
                    else
                    {
                        point = rotate(rcanvas.outerWidth / 2 + 20 - rcanvas.innerWidth / 2, 0, planet.absolute_position - startdegree);
                        pointdegree = rotate(rcanvas.outerWidth / 3, 0, planet.absolute_position - startdegree);
                        pointsymbol = rotate(rcanvas.outerWidth / 3 - 20, 0, planet.absolute_position - startdegree);
                        pointminute = rotate(rcanvas.outerWidth / 3 - 40, 0, planet.absolute_position - startdegree);
                        pointretrograde = rotate(rcanvas.outerWidth / 3 - 60, 0, planet.absolute_position - startdegree);
                    }
                    degreeList.Add(planet.absolute_position);
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
                    point.Y -= 15;
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

                    retrogradeList.Add(planet.speed);
                    dispList.Add(planet.isDisp);
                    pList.Add(point);
                    pDegList.Add(pointdegree);
                    pSymbolList.Add(pointsymbol);
                    pMinuteList.Add(pointminute);
                    pRetrogradeList.Add(pointretrograde);
                    //                    g.DrawString(CommonData.getPlanetSymbol(planet.no), fnt, brush, point.X, point.Y);
                    Console.WriteLine(planet.absolute_position - startdegree);
                });

                if (dispList.Count < 1 && dispList[0])
                {
                    rcanvas.natalSuntxt = CommonData.getPlanetSymbol(0);
                    rcanvas.natalSunx = pList[0].X;
                    rcanvas.natalSuny = pList[0].Y;
                    rcanvas.natalSundegreetxt = (degreeList[0] % 30).ToString("00°");
                    rcanvas.natalSundegreex = pDegList[0].X;
                    rcanvas.natalSundegreey = pDegList[0].Y;
                    rcanvas.natalSunsigntxt = CommonData.getSignText(degreeList[0]);
                    rcanvas.natalSunsignx = pSymbolList[0].X;
                    rcanvas.natalSunsigny = pSymbolList[0].Y;
                    rcanvas.natalSunMinutetxt = ((degreeList[0] % 1) / 100 * 60 * 100).ToString("00") + "'";
                    rcanvas.natalSunMinutex = pMinuteList[0].X;
                    rcanvas.natalSunMinutey = pMinuteList[0].Y;
                    rcanvas.natalSunRetrogradetxt = CommonData.getRetrograde(retrogradeList[0]);
                    rcanvas.natalSunRetrogradex = pRetrogradeList[1].X;
                    rcanvas.natalSunRetrogradey = pRetrogradeList[1].Y;
                }

                if (dispList[1])
                {
                    rcanvas.natalMoontxt = CommonData.getPlanetSymbol(1);
                    rcanvas.natalMoonx = pList[1].X;
                    rcanvas.natalMoony = pList[1].Y;
                    rcanvas.natalMoondegreetxt = (degreeList[1] % 30).ToString("00°");
                    rcanvas.natalMoondegreex = pDegList[1].X;
                    rcanvas.natalMoondegreey = pDegList[1].Y;
                    rcanvas.natalMoonsigntxt = CommonData.getSignText(degreeList[1]);
                    rcanvas.natalMoonsignx = pSymbolList[1].X;
                    rcanvas.natalMoonsigny = pSymbolList[1].Y;
                    rcanvas.natalMoonMinutetxt = ((degreeList[1] % 1) / 100 * 60 * 100).ToString("00") + "'";
                    rcanvas.natalMoonMinutex = pMinuteList[1].X;
                    rcanvas.natalMoonMinutey = pMinuteList[1].Y;
                    rcanvas.natalMoonRetrogradetxt = CommonData.getRetrograde(retrogradeList[1]);
                    rcanvas.natalMoonRetrogradex = pRetrogradeList[1].X;
                    rcanvas.natalMoonRetrogradey = pRetrogradeList[1].Y;
                }

                if (dispList[2])
                {
                    rcanvas.natalMercurytxt = CommonData.getPlanetSymbol(2);
                    rcanvas.natalMercuryx = pList[2].X;
                    rcanvas.natalMercuryy = pList[2].Y;
                    rcanvas.natalMercurydegreetxt = (degreeList[2] % 30).ToString("00°");
                    rcanvas.natalMercurydegreex = pDegList[2].X;
                    rcanvas.natalMercurydegreey = pDegList[2].Y;
                    rcanvas.natalMercurysigntxt = CommonData.getSignText(degreeList[2]);
                    rcanvas.natalMercurysignx = pSymbolList[2].X;
                    rcanvas.natalMercurysigny = pSymbolList[2].Y;
                    rcanvas.natalMercuryMinutetxt = ((degreeList[2] % 1) / 100 * 60 * 100).ToString("00") + "'";
                    rcanvas.natalMercuryMinutex = pMinuteList[2].X;
                    rcanvas.natalMercuryMinutey = pMinuteList[2].Y;
                    rcanvas.natalMercuryRetrogradetxt = CommonData.getRetrograde(retrogradeList[2]);
                    rcanvas.natalMercuryRetrogradex = pRetrogradeList[2].X;
                    rcanvas.natalMercuryRetrogradey = pRetrogradeList[2].Y;
                }

                if (dispList[3])
                {
                    rcanvas.natalVenustxt = CommonData.getPlanetSymbol(3);
                    rcanvas.natalVenusx = pList[3].X;
                    rcanvas.natalVenusy = pList[3].Y;
                    rcanvas.natalVenusdegreetxt = (degreeList[3] % 30).ToString("00°");
                    rcanvas.natalVenusdegreex = pDegList[3].X;
                    rcanvas.natalVenusdegreey = pDegList[3].Y;
                    rcanvas.natalVenussigntxt = CommonData.getSignText(degreeList[3]);
                    rcanvas.natalVenussignx = pSymbolList[3].X;
                    rcanvas.natalVenussigny = pSymbolList[3].Y;
                    rcanvas.natalVenusMinutetxt = ((degreeList[3] % 1) / 100 * 60 * 100).ToString("00") + "'";
                    rcanvas.natalVenusMinutex = pMinuteList[3].X;
                    rcanvas.natalVenusMinutey = pMinuteList[3].Y;
                    rcanvas.natalVenusRetrogradetxt = CommonData.getRetrograde(retrogradeList[3]);
                    rcanvas.natalVenusRetrogradex = pRetrogradeList[3].X;
                    rcanvas.natalVenusRetrogradey = pRetrogradeList[3].Y;
                }

                if (dispList[4])
                {
                    rcanvas.natalMarstxt = CommonData.getPlanetSymbol(4);
                    rcanvas.natalMarsx = pList[4].X;
                    rcanvas.natalMarsy = pList[4].Y;
                    rcanvas.natalMarsdegreetxt = (degreeList[4] % 30).ToString("00°");
                    rcanvas.natalMarsdegreex = pDegList[4].X;
                    rcanvas.natalMarsdegreey = pDegList[4].Y;
                    rcanvas.natalMarssigntxt = CommonData.getSignText(degreeList[4]);
                    rcanvas.natalMarssignx = pSymbolList[4].X;
                    rcanvas.natalMarssigny = pSymbolList[4].Y;
                    rcanvas.natalMarsMinutetxt = ((degreeList[4] % 1) / 100 * 60 * 100).ToString("00") + "'";
                    rcanvas.natalMarsMinutex = pMinuteList[4].X;
                    rcanvas.natalMarsMinutey = pMinuteList[4].Y;
                    rcanvas.natalMarsRetrogradetxt = CommonData.getRetrograde(retrogradeList[4]);
                    rcanvas.natalMarsRetrogradex = pRetrogradeList[4].X;
                    rcanvas.natalMarsRetrogradey = pRetrogradeList[4].Y;
                }

                if (dispList[5])
                {
                    rcanvas.natalJupitertxt = CommonData.getPlanetSymbol(5);
                    rcanvas.natalJupiterx = pList[5].X;
                    rcanvas.natalJupitery = pList[5].Y;
                    rcanvas.natalJupiterdegreetxt = (degreeList[5] % 30).ToString("00°");
                    rcanvas.natalJupiterdegreex = pDegList[5].X;
                    rcanvas.natalJupiterdegreey = pDegList[5].Y;
                    rcanvas.natalJupitersigntxt = CommonData.getSignText(degreeList[5]);
                    rcanvas.natalJupitersignx = pSymbolList[5].X;
                    rcanvas.natalJupitersigny = pSymbolList[5].Y;
                    rcanvas.natalJupiterMinutetxt = ((degreeList[5] % 1) / 100 * 60 * 100).ToString("00") + "'";
                    rcanvas.natalJupiterMinutex = pMinuteList[5].X;
                    rcanvas.natalJupiterMinutey = pMinuteList[5].Y;
                    rcanvas.natalJupiterRetrogradetxt = CommonData.getRetrograde(retrogradeList[5]);
                    rcanvas.natalJupiterRetrogradex = pRetrogradeList[5].X;
                    rcanvas.natalJupiterRetrogradey = pRetrogradeList[5].Y;
                }

                if (dispList[6])
                {
                    rcanvas.natalSaturntxt = CommonData.getPlanetSymbol(6);
                    rcanvas.natalSaturnx = pList[6].X;
                    rcanvas.natalSaturny = pList[6].Y;
                    rcanvas.natalSaturndegreetxt = (degreeList[6] % 30).ToString("00°");
                    rcanvas.natalSaturndegreex = pDegList[6].X;
                    rcanvas.natalSaturndegreey = pDegList[6].Y;
                    rcanvas.natalSaturnsigntxt = CommonData.getSignText(degreeList[6]);
                    rcanvas.natalSaturnsignx = pSymbolList[6].X;
                    rcanvas.natalSaturnsigny = pSymbolList[6].Y;
                    rcanvas.natalSaturnMinutetxt = ((degreeList[6] % 1) / 100 * 60 * 100).ToString("00") + "'";
                    rcanvas.natalSaturnMinutex = pMinuteList[6].X;
                    rcanvas.natalSaturnMinutey = pMinuteList[6].Y;
                    rcanvas.natalSaturnRetrogradetxt = CommonData.getRetrograde(retrogradeList[6]);
                    rcanvas.natalSaturnRetrogradex = pRetrogradeList[6].X;
                    rcanvas.natalSaturnRetrogradey = pRetrogradeList[6].Y;
                }

                if (dispList[7])
                {
                    rcanvas.natalUranustxt = CommonData.getPlanetSymbol(7);
                    rcanvas.natalUranusx = pList[7].X;
                    rcanvas.natalUranusy = pList[7].Y;
                    rcanvas.natalUranusdegreetxt = (degreeList[7] % 30).ToString("00°");
                    rcanvas.natalUranusdegreex = pDegList[7].X;
                    rcanvas.natalUranusdegreey = pDegList[7].Y;
                    rcanvas.natalUranussigntxt = CommonData.getSignText(degreeList[7]);
                    rcanvas.natalUranussignx = pSymbolList[7].X;
                    rcanvas.natalUranussigny = pSymbolList[7].Y;
                    rcanvas.natalUranusMinutetxt = ((degreeList[7] % 1) / 100 * 60 * 100).ToString("00") + "'";
                    rcanvas.natalUranusMinutex = pMinuteList[7].X;
                    rcanvas.natalUranusMinutey = pMinuteList[7].Y;
                    rcanvas.natalUranusRetrogradetxt = CommonData.getRetrograde(retrogradeList[7]);
                    rcanvas.natalUranusRetrogradex = pRetrogradeList[7].X;
                    rcanvas.natalUranusRetrogradey = pRetrogradeList[7].Y;
                }

                if (dispList[8])
                {
                    rcanvas.natalNeptunetxt = CommonData.getPlanetSymbol(8);
                    rcanvas.natalNeptunex = pList[8].X;
                    rcanvas.natalNeptuney = pList[8].Y;
                    rcanvas.natalNeptunedegreetxt = (degreeList[8] % 30).ToString("00°");
                    rcanvas.natalNeptunedegreex = pDegList[8].X;
                    rcanvas.natalNeptunedegreey = pDegList[8].Y;
                    rcanvas.natalNeptunesigntxt = CommonData.getSignText(degreeList[8]);
                    rcanvas.natalNeptunesignx = pSymbolList[8].X;
                    rcanvas.natalNeptunesigny = pSymbolList[8].Y;
                    rcanvas.natalNeptuneMinutetxt = ((degreeList[8] % 1) / 100 * 60 * 100).ToString("00") + "'";
                    rcanvas.natalNeptuneMinutex = pMinuteList[8].X;
                    rcanvas.natalNeptuneMinutey = pMinuteList[8].Y;
                    rcanvas.natalNeptuneRetrogradetxt = CommonData.getRetrograde(retrogradeList[8]);
                    rcanvas.natalNeptuneRetrogradex = pRetrogradeList[8].X;
                    rcanvas.natalNeptuneRetrogradey = pRetrogradeList[8].Y;
                }

                if (dispList[9])
                {
                    rcanvas.natalPlutotxt = CommonData.getPlanetSymbol(9);
                    rcanvas.natalPlutox = pList[9].X;
                    rcanvas.natalPlutoy = pList[9].Y;
                    rcanvas.natalPlutodegreetxt = (degreeList[9] % 30).ToString("00°");
                    rcanvas.natalPlutodegreex = pDegList[9].X;
                    rcanvas.natalPlutodegreey = pDegList[9].Y;
                    rcanvas.natalPlutosigntxt = CommonData.getSignText(degreeList[9]);
                    rcanvas.natalPlutosignx = pSymbolList[9].X;
                    rcanvas.natalPlutosigny = pSymbolList[9].Y;
                    rcanvas.natalPlutoMinutetxt = ((degreeList[9] % 1) / 100 * 60 * 100).ToString("00") + "'";
                    rcanvas.natalPlutoMinutex = pMinuteList[9].X;
                    rcanvas.natalPlutoMinutey = pMinuteList[9].Y;
                    rcanvas.natalPlutoRetrogradetxt = CommonData.getRetrograde(retrogradeList[9]);
                    rcanvas.natalPlutoRetrogradex = pRetrogradeList[9].X;
                    rcanvas.natalPlutoRetrogradey = pRetrogradeList[9].Y;
                }

                if (dispList[14])
                {
                    rcanvas.natalEarthtxt = CommonData.getPlanetSymbol(14);
                    rcanvas.natalEarthx = pList[14].X;
                    rcanvas.natalEarthy = pList[14].Y;
                    rcanvas.natalEarthdegreetxt = (degreeList[14] % 30).ToString("00°");
                    rcanvas.natalEarthdegreex = pDegList[14].X;
                    rcanvas.natalEarthdegreey = pDegList[14].Y;
                    rcanvas.natalEarthsigntxt = CommonData.getSignText(degreeList[14]);
                    rcanvas.natalEarthsignx = pSymbolList[14].X;
                    rcanvas.natalEarthsigny = pSymbolList[14].Y;
                    rcanvas.natalEarthMinutetxt = ((degreeList[14] % 1) / 100 * 60 * 100).ToString("00") + "'";
                    rcanvas.natalEarthMinutex = pMinuteList[14].X;
                    rcanvas.natalEarthMinutey = pMinuteList[14].Y;
                    rcanvas.natalEarthRetrogradetxt = CommonData.getRetrograde(retrogradeList[14]);
                    rcanvas.natalEarthRetrogradex = pRetrogradeList[14].X;
                    rcanvas.natalEarthRetrogradey = pRetrogradeList[14].Y;
                }
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

        private void OpenCommonConfig_Click(object sender, RoutedEventArgs e)
        {
            if (configWindow == null)
            {
                configWindow = new CommonConfigWindow(this);
            }
            configWindow.Visibility = Visibility.Visible;
        }
    }
}
