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

        public UserData targetUser;
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
            targetUser = new UserData(config);
            UserBinding ub = new UserBinding(targetUser);
            TransitBinding tb = new TransitBinding(targetUser);
            this.DataContext = new
            {
                userName = targetUser.name,
                userBirthStr = ub.birthStr,
                userTimezone = targetUser.timezone,
                userBirthPlace = targetUser.birth_place,
                userLat = String.Format("{0:f4}", targetUser.lat),
                userLng = String.Format("{0:f4}", targetUser.lng),
                transitName = "イベント未設定",
                transitBirthStr = tb.birthStr,
                transitTimezone = targetUser.timezone,
                transitPlace = targetUser.birth_place,
                transitLat = String.Format("{0:f4}", targetUser.lat),
                transitLng = String.Format("{0:f4}", targetUser.lng),
            };

            ReCalc();

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
            natalEarthSymbol.DataContext = rcanvas;
            natalEarthDegree.DataContext = rcanvas;
            natalEarthSign.DataContext = rcanvas;
            natalEarthMinute.DataContext = rcanvas;
            natalEarthRetrograde.DataContext = rcanvas;

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

        public void ReCalc()
        {
            list1 = calc.PositionCalc(targetUser.birth_year, targetUser.birth_month, targetUser.birth_day, targetUser.birth_hour, targetUser.birth_minute, targetUser.birth_second, targetUser.lat, targetUser.lng);
            list2 = calc.PositionCalc(targetUser.birth_year, targetUser.birth_month, targetUser.birth_day, targetUser.birth_hour, targetUser.birth_minute, targetUser.birth_second, targetUser.lat, targetUser.lng);
            list3 = calc.PositionCalc(targetUser.birth_year, targetUser.birth_month, targetUser.birth_day, targetUser.birth_hour, targetUser.birth_minute, targetUser.birth_second, targetUser.lat, targetUser.lng);
            list4 = calc.PositionCalc(targetUser.birth_year, targetUser.birth_month, targetUser.birth_day, targetUser.birth_hour, targetUser.birth_minute, targetUser.birth_second, targetUser.lat, targetUser.lng);
            list5 = calc.PositionCalc(targetUser.birth_year, targetUser.birth_month, targetUser.birth_day, targetUser.birth_hour, targetUser.birth_minute, targetUser.birth_second, targetUser.lat, targetUser.lng);
            list6 = calc.PositionCalc(targetUser.birth_year, targetUser.birth_month, targetUser.birth_day, targetUser.birth_hour, targetUser.birth_minute, targetUser.birth_second, targetUser.lat, targetUser.lng);

            houseList1 = calc.CuspCalc(targetUser.birth_year, targetUser.birth_month, targetUser.birth_day, targetUser.birth_hour, targetUser.birth_minute, targetUser.birth_second, targetUser.lat, targetUser.lng, config.houseCalc);
            houseList2 = calc.CuspCalc(targetUser.birth_year, targetUser.birth_month, targetUser.birth_day, targetUser.birth_hour, targetUser.birth_minute, targetUser.birth_second, targetUser.lat, targetUser.lng, config.houseCalc);
            houseList3 = calc.CuspCalc(targetUser.birth_year, targetUser.birth_month, targetUser.birth_day, targetUser.birth_hour, targetUser.birth_minute, targetUser.birth_second, targetUser.lat, targetUser.lng, config.houseCalc);
            houseList4 = calc.CuspCalc(targetUser.birth_year, targetUser.birth_month, targetUser.birth_day, targetUser.birth_hour, targetUser.birth_minute, targetUser.birth_second, targetUser.lat, targetUser.lng, config.houseCalc);
            houseList5 = calc.CuspCalc(targetUser.birth_year, targetUser.birth_month, targetUser.birth_day, targetUser.birth_hour, targetUser.birth_minute, targetUser.birth_second, targetUser.lat, targetUser.lng, config.houseCalc);
            houseList6 = calc.CuspCalc(targetUser.birth_year, targetUser.birth_month, targetUser.birth_day, targetUser.birth_hour, targetUser.birth_minute, targetUser.birth_second, targetUser.lat, targetUser.lng, config.houseCalc);
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


            Console.WriteLine(ringCanvas.ActualWidth.ToString() + "," + ringStack.ActualHeight.ToString());

            ReRender();
        }

        // レンダリングメイン
        public void ReRender()
        {
            firstPList.ReRender(list1, list2, list3, list4, list5, list6);
            houseList.ReRender(houseList1, houseList2, houseList3, houseList4, houseList5, houseList6);

            houseCuspRender(houseList1);
            signCuspRender(houseList1[1]);
            zodiacRender(houseList1[1]);
            planetRender(houseList1[1], list1, list2, list3);
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
            List<bool> dispList = new List<bool>();
            List<PlanetDisplay> pDisplayList = new List<PlanetDisplay>();

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

                    degreeList.Add(planet.absolute_position);
                    dispList.Add(planet.isDisp);

                    PlanetDisplay display = new PlanetDisplay()
                    {
                        planetNo = planet.no,
                        isDisp = planet.isDisp,
                        planetPt = point,
                        planetTxt = CommonData.getPlanetSymbol(planet.no),
                        degreePt = pointdegree,
                        degreeTxt = (planet.absolute_position % 30).ToString("00°"),
                        symbolPt = pointsymbol,
                        symbolTxt = CommonData.getSignText(planet.absolute_position),
                        minutePt = pointminute,
                        minuteTxt = ((planet.absolute_position % 1) / 100 * 60 * 100).ToString("00") + "'",
                        retrogradePt = pointretrograde,
                        retrogradeTxt = CommonData.getRetrograde(planet.speed)
                    };
                    pDisplayList.Add(display);

                    //                    g.DrawString(CommonData.getPlanetSymbol(planet.no), fnt, brush, point.X, point.Y);
                    Console.WriteLine(planet.absolute_position - startdegree);
                });

                AllClear();
                pDisplayList.ForEach(displayData => {
                    RingDisplay display;
                    if (displayData.planetNo == (int)CommonData.ZODIAC_SUN)
                    {
                        if (!displayData.isDisp)
                        {
                            return;
                        }
                        rcanvas.natalSunTxt = displayData.planetTxt;
                        rcanvas.natalSunX = displayData.planetPt.X;
                        rcanvas.natalSunY = displayData.planetPt.Y;
                        rcanvas.natalSunDegreeTxt = displayData.degreeTxt;
                        rcanvas.natalSunDegreeX = displayData.degreePt.X;
                        rcanvas.natalSunDegreeY = displayData.degreePt.Y;
                        rcanvas.natalSunSignTxt = displayData.symbolTxt;
                        rcanvas.natalSunSignX = displayData.symbolPt.X;
                        rcanvas.natalSunSignY = displayData.symbolPt.Y;
                        rcanvas.natalSunMinuteTxt = displayData.minuteTxt;
                        rcanvas.natalSunMinuteX = displayData.minutePt.X;
                        rcanvas.natalSunMinuteY = displayData.minutePt.Y;
                        rcanvas.natalSunRetrogradeTxt = displayData.retrogradeTxt;
                        rcanvas.natalSunRetrogradeX = displayData.retrogradePt.X;
                        rcanvas.natalSunRetrogradeY = displayData.retrogradePt.Y;
                    }
                    else if (displayData.planetNo == (int)CommonData.ZODIAC_MOON)
                    {
                        if (!displayData.isDisp)
                        {
                            return;
                        }
                        rcanvas.natalMoonTxt = displayData.planetTxt;
                        rcanvas.natalMoonX = displayData.planetPt.X;
                        rcanvas.natalMoonY = displayData.planetPt.Y;
                        rcanvas.natalMoonDegreeTxt = displayData.degreeTxt;
                        rcanvas.natalMoonDegreeX = displayData.degreePt.X;
                        rcanvas.natalMoonDegreeY = displayData.degreePt.Y;
                        rcanvas.natalMoonSignTxt = displayData.symbolTxt;
                        rcanvas.natalMoonSignX = displayData.symbolPt.X;
                        rcanvas.natalMoonSignY = displayData.symbolPt.Y;
                        rcanvas.natalMoonMinuteTxt = displayData.minuteTxt;
                        rcanvas.natalMoonMinuteX = displayData.minutePt.X;
                        rcanvas.natalMoonMinuteY = displayData.minutePt.Y;
                        rcanvas.natalMoonRetrogradeTxt = displayData.retrogradeTxt;
                        rcanvas.natalMoonRetrogradeX = displayData.retrogradePt.X;
                        rcanvas.natalMoonRetrogradeY = displayData.retrogradePt.Y;
                    }
                    else if (displayData.planetNo == (int)CommonData.ZODIAC_MERCURY)
                    {
                        if (!displayData.isDisp)
                        {
                            return;
                        }
                        rcanvas.natalMercuryTxt = displayData.planetTxt;
                        rcanvas.natalMercuryX = displayData.planetPt.X;
                        rcanvas.natalMercuryY = displayData.planetPt.Y;
                        rcanvas.natalMercuryDegreeTxt = displayData.degreeTxt;
                        rcanvas.natalMercuryDegreeX = displayData.degreePt.X;
                        rcanvas.natalMercuryDegreeY = displayData.degreePt.Y;
                        rcanvas.natalMercurySignTxt = displayData.symbolTxt;
                        rcanvas.natalMercurySignX = displayData.symbolPt.X;
                        rcanvas.natalMercurySignY = displayData.symbolPt.Y;
                        rcanvas.natalMercuryMinuteTxt = displayData.minuteTxt;
                        rcanvas.natalMercuryMinuteX = displayData.minutePt.X;
                        rcanvas.natalMercuryMinuteY = displayData.minutePt.Y;
                        rcanvas.natalMercuryRetrogradeTxt = displayData.retrogradeTxt;
                        rcanvas.natalMercuryRetrogradeX = displayData.retrogradePt.X;
                        rcanvas.natalMercuryRetrogradeY = displayData.retrogradePt.Y;
                    }
                    else if (displayData.planetNo == (int)CommonData.ZODIAC_VENUS)
                    {
                        if (!displayData.isDisp)
                        {
                            return;
                        }
                        rcanvas.natalVenusTxt = displayData.planetTxt;
                        rcanvas.natalVenusX = displayData.planetPt.X;
                        rcanvas.natalVenusY = displayData.planetPt.Y;
                        rcanvas.natalVenusDegreeTxt = displayData.degreeTxt;
                        rcanvas.natalVenusDegreeX = displayData.degreePt.X;
                        rcanvas.natalVenusDegreeY = displayData.degreePt.Y;
                        rcanvas.natalVenusSignTxt = displayData.symbolTxt;
                        rcanvas.natalVenusSignX = displayData.symbolPt.X;
                        rcanvas.natalVenusSignY = displayData.symbolPt.Y;
                        rcanvas.natalVenusMinuteTxt = displayData.minuteTxt;
                        rcanvas.natalVenusMinuteX = displayData.minutePt.X;
                        rcanvas.natalVenusMinuteY = displayData.minutePt.Y;
                        rcanvas.natalVenusRetrogradeTxt = displayData.retrogradeTxt;
                        rcanvas.natalVenusRetrogradeX = displayData.retrogradePt.X;
                        rcanvas.natalVenusRetrogradeY = displayData.retrogradePt.Y;
                    }
                    else if (displayData.planetNo == (int)CommonData.ZODIAC_MARS)
                    {
                        if (!displayData.isDisp)
                        {
                            return;
                        }
                        rcanvas.natalMarsTxt = displayData.planetTxt;
                        rcanvas.natalMarsX = displayData.planetPt.X;
                        rcanvas.natalMarsY = displayData.planetPt.Y;
                        rcanvas.natalMarsDegreeTxt = displayData.degreeTxt;
                        rcanvas.natalMarsDegreeX = displayData.degreePt.X;
                        rcanvas.natalMarsDegreeY = displayData.degreePt.Y;
                        rcanvas.natalMarsSignTxt = displayData.symbolTxt;
                        rcanvas.natalMarsSignX = displayData.symbolPt.X;
                        rcanvas.natalMarsSignY = displayData.symbolPt.Y;
                        rcanvas.natalMarsMinuteTxt = displayData.minuteTxt;
                        rcanvas.natalMarsMinuteX = displayData.minutePt.X;
                        rcanvas.natalMarsMinuteY = displayData.minutePt.Y;
                        rcanvas.natalMarsRetrogradeTxt = displayData.retrogradeTxt;
                        rcanvas.natalMarsRetrogradeX = displayData.retrogradePt.X;
                        rcanvas.natalMarsRetrogradeY = displayData.retrogradePt.Y;
                    }
                    else if (displayData.planetNo == (int)CommonData.ZODIAC_JUPITER)
                    {
                        if (!displayData.isDisp)
                        {
                            return;
                        }
                        SetJupiter(
                            displayData.planetTxt,
                            displayData.planetPt,
                            displayData.degreeTxt,
                            displayData.degreePt,
                            displayData.symbolTxt,
                            displayData.symbolPt,
                            displayData.minuteTxt,
                            displayData.minutePt,
                            displayData.retrogradeTxt,
                            displayData.retrogradePt
                        );
                    }
                    else if (displayData.planetNo == (int)CommonData.ZODIAC_SATURN)
                    {
                        if (!displayData.isDisp)
                        {
                            return;
                        }
                        SetSaturn(
                            displayData.planetTxt,
                            displayData.planetPt,
                            displayData.degreeTxt,
                            displayData.degreePt,
                            displayData.symbolTxt,
                            displayData.symbolPt,
                            displayData.minuteTxt,
                            displayData.minutePt,
                            displayData.retrogradeTxt,
                            displayData.retrogradePt
                        );
                    }
                    else if (displayData.planetNo == (int)CommonData.ZODIAC_URANUS)
                    {
                        if (!displayData.isDisp)
                        {
                            return;
                        }
                        SetUranus(
                            displayData.planetTxt,
                            displayData.planetPt,
                            displayData.degreeTxt,
                            displayData.degreePt,
                            displayData.symbolTxt,
                            displayData.symbolPt,
                            displayData.minuteTxt,
                            displayData.minutePt,
                            displayData.retrogradeTxt,
                            displayData.retrogradePt
                        );
                    }
                    else if (displayData.planetNo == (int)CommonData.ZODIAC_NEPTUNE)
                    {
                        if (!displayData.isDisp)
                        {
                            return;
                        }
                        SetNeptune(
                            displayData.planetTxt,
                            displayData.planetPt,
                            displayData.degreeTxt,
                            displayData.degreePt,
                            displayData.symbolTxt,
                            displayData.symbolPt,
                            displayData.minuteTxt,
                            displayData.minutePt,
                            displayData.retrogradeTxt,
                            displayData.retrogradePt
                        );
                    }
                    else if (displayData.planetNo == (int)CommonData.ZODIAC_PLUTO)
                    {
                        if (!displayData.isDisp)
                        {
                            return;
                        }
                        SetPluto(
                            displayData.planetTxt,
                            displayData.planetPt,
                            displayData.degreeTxt,
                            displayData.degreePt,
                            displayData.symbolTxt,
                            displayData.symbolPt,
                            displayData.minuteTxt,
                            displayData.minutePt,
                            displayData.retrogradeTxt,
                            displayData.retrogradePt
                        );
                    }
                    else if (displayData.planetNo == (int)CommonData.ZODIAC_EARTH)
                    {
                        if (!displayData.isDisp)
                        {
                            return;
                        }
                        rcanvas.natalEarthTxt = displayData.planetTxt;
                        rcanvas.natalEarthX = displayData.planetPt.X;
                        rcanvas.natalEarthY = displayData.planetPt.Y;
                        rcanvas.natalEarthDegreeTxt = displayData.degreeTxt;
                        rcanvas.natalEarthDegreeX = displayData.degreePt.X;
                        rcanvas.natalEarthDegreeY = displayData.degreePt.Y;
                        rcanvas.natalEarthSignTxt = displayData.symbolTxt;
                        rcanvas.natalEarthSignX = displayData.symbolPt.X;
                        rcanvas.natalEarthSignY = displayData.symbolPt.Y;
                        rcanvas.natalEarthMinuteTxt = displayData.minuteTxt;
                        rcanvas.natalEarthMinuteX = displayData.minutePt.X;
                        rcanvas.natalEarthMinuteY = displayData.minutePt.Y;
                        rcanvas.natalEarthRetrogradeTxt = displayData.retrogradeTxt;
                        rcanvas.natalEarthRetrogradeX = displayData.retrogradePt.X;
                        rcanvas.natalEarthRetrogradeY = displayData.retrogradePt.Y;
                    }
                    else if (displayData.planetNo == (int)CommonData.ZODIAC_DH_TRUENODE)
                    {
                    }
//                    display();
                });

            }
            else if (tempsettings.bands == 2)
            {
            }
            else if (tempsettings.bands == 3)
            {
            }
        }

        public void AllClear()
        {
            rcanvas.natalSunTxt = "";
            rcanvas.natalSunDegreeTxt = "";
            rcanvas.natalSunSignTxt = "";
            rcanvas.natalSunMinuteTxt = "";
            rcanvas.natalSunRetrogradeTxt = "";
            rcanvas.natalEarthTxt = "";
            rcanvas.natalEarthDegreeTxt = "";
            rcanvas.natalEarthSignTxt = "";
            rcanvas.natalEarthMinuteTxt = "";
            rcanvas.natalEarthRetrogradeTxt = "";
        }

        public void SetJupiter(
            string planetTxt, PointF planet,
            string degTxt, PointF degree,
            string signTxt, PointF sign,
            string minuteTxt, PointF minute,
            string retrogradeTxt, PointF retrograde)
        {
            rcanvas.natalJupiterTxt = planetTxt;
            rcanvas.natalJupiterX = planet.X;
            rcanvas.natalJupiterY = planet.Y;
            rcanvas.natalJupiterDegreeTxt = degTxt;
            rcanvas.natalJupiterDegreeX = degree.X;
            rcanvas.natalJupiterDegreeY = degree.Y;
            rcanvas.natalJupiterSignTxt = signTxt;
            rcanvas.natalJupiterSignX = sign.X;
            rcanvas.natalJupiterSignY = sign.Y;
            rcanvas.natalJupiterMinuteTxt = minuteTxt;
            rcanvas.natalJupiterMinuteX = minute.X;
            rcanvas.natalJupiterMinuteY = minute.Y;
            rcanvas.natalJupiterRetrogradeTxt = retrogradeTxt;
            rcanvas.natalJupiterRetrogradeX = retrograde.X;
            rcanvas.natalJupiterRetrogradeY = retrograde.Y;
        }

        public void SetSaturn(
            string planetTxt, PointF planet,
            string degTxt, PointF degree,
            string signTxt, PointF sign,
            string minuteTxt, PointF minute,
            string retrogradeTxt, PointF retrograde)
        {
            rcanvas.natalSaturnTxt = planetTxt;
            rcanvas.natalSaturnX = planet.X;
            rcanvas.natalSaturnY = planet.Y;
            rcanvas.natalSaturnDegreeTxt = degTxt;
            rcanvas.natalSaturnDegreeX = degree.X;
            rcanvas.natalSaturnDegreeY = degree.Y;
            rcanvas.natalSaturnSignTxt = signTxt;
            rcanvas.natalSaturnSignX = sign.X;
            rcanvas.natalSaturnSignY = sign.Y;
            rcanvas.natalSaturnMinuteTxt = minuteTxt;
            rcanvas.natalSaturnMinuteX = minute.X;
            rcanvas.natalSaturnMinuteY = minute.Y;
            rcanvas.natalSaturnRetrogradeTxt = retrogradeTxt;
            rcanvas.natalSaturnRetrogradeX = retrograde.X;
            rcanvas.natalSaturnRetrogradeY = retrograde.Y;
        }

        public void SetUranus(
            string planetTxt, PointF planet,
            string degTxt, PointF degree,
            string signTxt, PointF sign,
            string minuteTxt, PointF minute,
            string retrogradeTxt, PointF retrograde)
        {
            rcanvas.natalUranusTxt = planetTxt;
            rcanvas.natalUranusX = planet.X;
            rcanvas.natalUranusY = planet.Y;
            rcanvas.natalUranusDegreeTxt = degTxt;
            rcanvas.natalUranusDegreeX = degree.X;
            rcanvas.natalUranusDegreeY = degree.Y;
            rcanvas.natalUranusSignTxt = signTxt;
            rcanvas.natalUranusSignX = sign.X;
            rcanvas.natalUranusSignY = sign.Y;
            rcanvas.natalUranusMinuteTxt = minuteTxt;
            rcanvas.natalUranusMinuteX = minute.X;
            rcanvas.natalUranusMinuteY = minute.Y;
            rcanvas.natalUranusRetrogradeTxt = retrogradeTxt;
            rcanvas.natalUranusRetrogradeX = retrograde.X;
            rcanvas.natalUranusRetrogradeY = retrograde.Y;
        }

        public void SetNeptune(
            string planetTxt, PointF planet,
            string degTxt, PointF degree,
            string signTxt, PointF sign,
            string minuteTxt, PointF minute,
            string retrogradeTxt, PointF retrograde)
        {
            rcanvas.natalNeptuneTxt = planetTxt;
            rcanvas.natalNeptuneX = planet.X;
            rcanvas.natalNeptuneY = planet.Y;
            rcanvas.natalNeptuneDegreeTxt = degTxt;
            rcanvas.natalNeptuneDegreeX = degree.X;
            rcanvas.natalNeptuneDegreeY = degree.Y;
            rcanvas.natalNeptuneSignTxt = signTxt;
            rcanvas.natalNeptuneSignX = sign.X;
            rcanvas.natalNeptuneSignY = sign.Y;
            rcanvas.natalNeptuneMinuteTxt = minuteTxt;
            rcanvas.natalNeptuneMinuteX = minute.X;
            rcanvas.natalNeptuneMinuteY = minute.Y;
            rcanvas.natalNeptuneRetrogradeTxt = retrogradeTxt;
            rcanvas.natalNeptuneRetrogradeX = retrograde.X;
            rcanvas.natalNeptuneRetrogradeY = retrograde.Y;
        }

        public void SetPluto(
            string planetTxt, PointF planet, 
            string degTxt, PointF degree, 
            string signTxt, PointF sign, 
            string minuteTxt, PointF minute, 
            string retrogradeTxt, PointF retrograde) 
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

        private void SingleRing_Click(object sender, RoutedEventArgs e)
        {
            tempsettings.bands = 1;
            ReRender();
        }

        private void TripleRing_Click(object sender, RoutedEventArgs e)
        {
            tempsettings.bands = 3;
            ReRender();
        }

        private void MultipleRing_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
