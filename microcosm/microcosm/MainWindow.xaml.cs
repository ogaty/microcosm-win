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

            rcanvas.ariesTxt = CommonData.getSignSymbol(0);
            rcanvas.ariesX = pList[0].X;
            rcanvas.ariesY = pList[0].Y;
            rcanvas.taurusTxt = CommonData.getSignSymbol(1);
            rcanvas.taurusX = pList[1].X;
            rcanvas.taurusY = pList[1].Y;
            rcanvas.geminiTxt = CommonData.getSignSymbol(2);
            rcanvas.geminiX = pList[2].X;
            rcanvas.geminiY = pList[2].Y;
            rcanvas.cancerTxt = CommonData.getSignSymbol(3);
            rcanvas.cancerX = pList[3].X;
            rcanvas.cancerY = pList[3].Y;
            rcanvas.leoTxt = CommonData.getSignSymbol(4);
            rcanvas.leoX = pList[4].X;
            rcanvas.leoY = pList[4].Y;
            rcanvas.virgoTxt = CommonData.getSignSymbol(5);
            rcanvas.virgoX = pList[5].X;
            rcanvas.virgoY = pList[5].Y;
            rcanvas.libraTxt = CommonData.getSignSymbol(6);
            rcanvas.libraX = pList[6].X;
            rcanvas.libraY = pList[6].Y;
            rcanvas.scorpionTxt = CommonData.getSignSymbol(7);
            rcanvas.scorpionX = pList[7].X;
            rcanvas.scorpionY = pList[7].Y;
            rcanvas.sagittariusTxt = CommonData.getSignSymbol(8);
            rcanvas.sagittariusX = pList[8].X;
            rcanvas.sagittariusY = pList[8].Y;
            rcanvas.capricornTxt = CommonData.getSignSymbol(9);
            rcanvas.capricornX = pList[9].X;
            rcanvas.capricornY = pList[9].Y;
            rcanvas.aquariusTxt = CommonData.getSignSymbol(10);
            rcanvas.aquariusX = pList[10].X;
            rcanvas.aquariusY = pList[10].Y;
            rcanvas.piscesTxt = CommonData.getSignSymbol(11);
            rcanvas.piscesX = pList[11].X;
            rcanvas.piscesY = pList[11].Y;
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

                rcanvas.natalSunTxt = CommonData.getPlanetSymbol(0);
                rcanvas.natalSunX = pList[0].X;
                rcanvas.natalSunY = pList[0].Y;
                rcanvas.natalSunDegreeTxt = (degreeList[0] % 30).ToString("00°");
                rcanvas.natalSunDegreeX = pDegList[0].X;
                rcanvas.natalSunDegreeY = pDegList[0].Y;
                rcanvas.natalSunSignTxt = CommonData.getSignText(degreeList[0]);
                rcanvas.natalSunSignX = pSymbolList[0].X;
                rcanvas.natalSunSignY = pSymbolList[0].Y;
                rcanvas.natalSunMinuteYxt = ((degreeList[0] % 1) / 100 * 60 * 100).ToString("00") + "'";
                rcanvas.natalSunMinuteX = pMinuteList[0].X;
                rcanvas.natalSunMinuteY = pMinuteList[0].Y;
                rcanvas.natalSunRetrogradeTxt = CommonData.getRetrograde(retrogradeList[0]);
                rcanvas.natalSunRetrogradeX = pRetrogradeList[1].X;
                rcanvas.natalSunRetrogradeY = pRetrogradeList[1].Y;

                rcanvas.natalMoonTxt = CommonData.getPlanetSymbol(1);
                rcanvas.natalMoonX = pList[1].X;
                rcanvas.natalMoonY = pList[1].Y;
                rcanvas.natalMoonDegreeTxt = (degreeList[1] % 30).ToString("00°");
                rcanvas.natalMoonDegreeX = pDegList[1].X;
                rcanvas.natalMoonDegreeY = pDegList[1].Y;
                rcanvas.natalMoonSignTxt = CommonData.getSignText(degreeList[1]);
                rcanvas.natalMoonSignX = pSymbolList[1].X;
                rcanvas.natalMoonSignY = pSymbolList[1].Y;
                rcanvas.natalMoonMinuteTxt = ((degreeList[1] % 1) / 100 * 60 * 100).ToString("00") + "'";
                rcanvas.natalMoonMinuteX = pMinuteList[1].X;
                rcanvas.natalMoonMinuteY = pMinuteList[1].Y;
                rcanvas.natalMoonRetrogradeTxt = CommonData.getRetrograde(retrogradeList[1]);
                rcanvas.natalMoonRetrogradeX = pRetrogradeList[1].X;
                rcanvas.natalMoonRetrogradeY = pRetrogradeList[1].Y;

                rcanvas.natalMercuryTxt = CommonData.getPlanetSymbol(2);
                rcanvas.natalMercuryX = pList[2].X;
                rcanvas.natalMercuryY = pList[2].Y;
                rcanvas.natalMercuryDegreeTxt = (degreeList[2] % 30).ToString("00°");
                rcanvas.natalMercuryDegreeX = pDegList[2].X;
                rcanvas.natalMercuryDegreeY = pDegList[2].Y;
                rcanvas.natalMercurySignTxt = CommonData.getSignText(degreeList[2]);
                rcanvas.natalMercurySignX = pSymbolList[2].X;
                rcanvas.natalMercurySignY = pSymbolList[2].Y;
                rcanvas.natalMercuryMinuteTxt = ((degreeList[2] % 1) / 100 * 60 * 100).ToString("00") + "'";
                rcanvas.natalMercuryMinuteX = pMinuteList[2].X;
                rcanvas.natalMercuryMinuteY = pMinuteList[2].Y;
                rcanvas.natalMercuryRetrogradeTxt = CommonData.getRetrograde(retrogradeList[2]);
                rcanvas.natalMercuryRetrogradeX = pRetrogradeList[2].X;
                rcanvas.natalMercuryRetrogradeY = pRetrogradeList[2].Y;

                rcanvas.natalVenusTxt = CommonData.getPlanetSymbol(3);
                rcanvas.natalVenusX = pList[3].X;
                rcanvas.natalVenusY = pList[3].Y;
                rcanvas.natalVenusDegreeTxt = (degreeList[3] % 30).ToString("00°");
                rcanvas.natalVenusDegreeX = pDegList[3].X;
                rcanvas.natalVenusDegreeY = pDegList[3].Y;
                rcanvas.natalVenusSignTxt = CommonData.getSignText(degreeList[3]);
                rcanvas.natalVenusSignX = pSymbolList[3].X;
                rcanvas.natalVenusSignY = pSymbolList[3].Y;
                rcanvas.natalVenusMinuteTxt = ((degreeList[3] % 1) / 100 * 60 * 100).ToString("00") + "'";
                rcanvas.natalVenusMinuteX = pMinuteList[3].X;
                rcanvas.natalVenusMinuteY = pMinuteList[3].Y;
                rcanvas.natalVenusRetrogradeTxt = CommonData.getRetrograde(retrogradeList[3]);
                rcanvas.natalVenusRetrogradeX = pRetrogradeList[3].X;
                rcanvas.natalVenusRetrogradeY = pRetrogradeList[3].Y;

                rcanvas.natalMarsTxt = CommonData.getPlanetSymbol(4);
                rcanvas.natalMarsX = pList[4].X;
                rcanvas.natalMarsY = pList[4].Y;
                rcanvas.natalMarsDegreeTxt = (degreeList[4] % 30).ToString("00°");
                rcanvas.natalMarsDegreeX = pDegList[4].X;
                rcanvas.natalMarsDegreeY = pDegList[4].Y;
                rcanvas.natalMarsSignTxt = CommonData.getSignText(degreeList[4]);
                rcanvas.natalMarsSignX = pSymbolList[4].X;
                rcanvas.natalMarsSignY = pSymbolList[4].Y;
                rcanvas.natalMarsMinuteTxt = ((degreeList[4] % 1) / 100 * 60 * 100).ToString("00") + "'";
                rcanvas.natalMarsMinuteX = pMinuteList[4].X;
                rcanvas.natalMarsMinuteY = pMinuteList[4].Y;
                rcanvas.natalMarsRetrogradeTxt = CommonData.getRetrograde(retrogradeList[4]);
                rcanvas.natalMarsRetrogradeX = pRetrogradeList[4].X;
                rcanvas.natalMarsRetrogradeY = pRetrogradeList[4].Y;

                rcanvas.natalJupiterTxt = CommonData.getPlanetSymbol(5);
                rcanvas.natalJupiterX = pList[5].X;
                rcanvas.natalJupiterY = pList[5].Y;
                rcanvas.natalJupiterDegreeTxt = (degreeList[5] % 30).ToString("00°");
                rcanvas.natalJupiterDegreeX = pDegList[5].X;
                rcanvas.natalJupiterDegreeY = pDegList[5].Y;
                rcanvas.natalJupiterSignTxt = CommonData.getSignText(degreeList[5]);
                rcanvas.natalJupiterSignX = pSymbolList[5].X;
                rcanvas.natalJupiterSignY = pSymbolList[5].Y;
                rcanvas.natalJupiterMinuteTxt = ((degreeList[5] % 1) / 100 * 60 * 100).ToString("00") + "'";
                rcanvas.natalJupiterMinuteX = pMinuteList[5].X;
                rcanvas.natalJupiterMinuteY = pMinuteList[5].Y;
                rcanvas.natalJupiterRetrogradeTxt = CommonData.getRetrograde(retrogradeList[5]);
                rcanvas.natalJupiterRetrogradeX = pRetrogradeList[5].X;
                rcanvas.natalJupiterRetrogradeY = pRetrogradeList[5].Y;

                rcanvas.natalSaturnTxt = CommonData.getPlanetSymbol(6);
                rcanvas.natalSaturnX = pList[6].X;
                rcanvas.natalSaturnY = pList[6].Y;
                rcanvas.natalSaturnDegreeTxt = (degreeList[6] % 30).ToString("00°");
                rcanvas.natalSaturnDegreeX = pDegList[6].X;
                rcanvas.natalSaturnDegreeY = pDegList[6].Y;
                rcanvas.natalSaturnSignTxt = CommonData.getSignText(degreeList[6]);
                rcanvas.natalSaturnSignX = pSymbolList[6].X;
                rcanvas.natalSaturnSignY = pSymbolList[6].Y;
                rcanvas.natalSaturnMinuteTxt = ((degreeList[6] % 1) / 100 * 60 * 100).ToString("00") + "'";
                rcanvas.natalSaturnMinuteX = pMinuteList[6].X;
                rcanvas.natalSaturnMinuteY = pMinuteList[6].Y;
                rcanvas.natalSaturnRetrogradeTxt = CommonData.getRetrograde(retrogradeList[6]);
                rcanvas.natalSaturnRetrogradeX = pRetrogradeList[6].X;
                rcanvas.natalSaturnRetrogradeY = pRetrogradeList[6].Y;

                rcanvas.natalUranusTxt = CommonData.getPlanetSymbol(7);
                rcanvas.natalUranusX = pList[7].X;
                rcanvas.natalUranusY = pList[7].Y;
                rcanvas.natalUranusDegreeTxt = (degreeList[7] % 30).ToString("00°");
                rcanvas.natalUranusDegreeX = pDegList[7].X;
                rcanvas.natalUranusDegreeY = pDegList[7].Y;
                rcanvas.natalUranusSignTxt = CommonData.getSignText(degreeList[7]);
                rcanvas.natalUranusSignX = pSymbolList[7].X;
                rcanvas.natalUranusSignY = pSymbolList[7].Y;
                rcanvas.natalUranusMinuteTxt = ((degreeList[7] % 1) / 100 * 60 * 100).ToString("00") + "'";
                rcanvas.natalUranusMinuteX = pMinuteList[7].X;
                rcanvas.natalUranusMinuteY = pMinuteList[7].Y;
                rcanvas.natalUranusRetrogradeTxt = CommonData.getRetrograde(retrogradeList[7]);
                rcanvas.natalUranusRetrogradeX = pRetrogradeList[7].X;
                rcanvas.natalUranusRetrogradeY = pRetrogradeList[7].Y;

                rcanvas.natalNeptuneTxt = CommonData.getPlanetSymbol(8);
                rcanvas.natalNeptuneX = pList[8].X;
                rcanvas.natalNeptuneY = pList[8].Y;
                rcanvas.natalNeptuneDegreeTxt = (degreeList[8] % 30).ToString("00°");
                rcanvas.natalNeptuneDegreeX = pDegList[8].X;
                rcanvas.natalNeptuneDegreeY = pDegList[8].Y;
                rcanvas.natalNeptuneSignTxt = CommonData.getSignText(degreeList[8]);
                rcanvas.natalNeptuneSignX = pSymbolList[8].X;
                rcanvas.natalNeptuneSignY = pSymbolList[8].Y;
                rcanvas.natalNeptuneMinuteTxt = ((degreeList[8] % 1) / 100 * 60 * 100).ToString("00") + "'";
                rcanvas.natalNeptuneMinuteX = pMinuteList[8].X;
                rcanvas.natalNeptuneMinuteY = pMinuteList[8].Y;
                rcanvas.natalNeptuneRetrogradeTxt = CommonData.getRetrograde(retrogradeList[8]);
                rcanvas.natalNeptuneRetrogradeX = pRetrogradeList[8].X;
                rcanvas.natalNeptuneRetrogradeY = pRetrogradeList[8].Y;

                SetPluto(
                    CommonData.getPlanetSymbol(9),
                    pList[9],
                    (degreeList[9] % 30).ToString("00°"),
                    pDegList[9],
                    CommonData.getSignText(degreeList[9]),
                    pSymbolList[9],
                    ((degreeList[9] % 1) / 100 * 60 * 100).ToString("00") + "'",
                    pMinuteList[9],
                    CommonData.getRetrograde(retrogradeList[9]),
                    pRetrogradeList[9]
                );

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
        
        public void SetPluto(
            string planetTxt, pointF planet, 
            string degTxt, pointF degree, 
            string signTxt, pointF sign, 
            string minuteTxt, pointF minute, 
            string retrogradeTxt, pointF retrograde) 
        {
            rcanvas.natalPlutoTxt = planetTxt;
            rcanvas.natalPlutoX = planet.X;
            rcanvas.natalPlutoY = planet.Y;
            rcanvas.natalPlutoDegreeTxt = degTxt;
            rcanvas.natalPlutoDegreeX = degree.X;
            rcanvas.natalPlutoDegreeY = degree.Y;
            rcanvas.natalPlutoSignTxt = signTxt;
            rcanvas.natalPlutoSignX = sign.X;
            rcanvas.natalPlutoSignY = sign.Y;
            rcanvas.natalPlutoMinuteTxt = minuteTxt;
            rcanvas.natalPlutoMinuteX = minute.X;
            rcanvas.natalPlutoMinuteY = minute.Y;
            rcanvas.natalPlutoRetrogradeTxt = retrogradeTxt;
            rcanvas.natalPlutoRetrogradeX = retrograde.X;
            rcanvas.natalPlutoRetrogradeY = retrograde.Y;
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
