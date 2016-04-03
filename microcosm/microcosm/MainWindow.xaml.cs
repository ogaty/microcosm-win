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
        public AstroCalc calc;
        public RingCanvas rcanvas;
        public ReportViewModel reportVM;

        public MainWindow()
        {
            InitializeComponent();

            DataInit();
            DataCalc();
            SetViewModel();
            
        }

        private void DataInit()
        {
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

            rcanvas = new RingCanvas(config);
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

            List<PlanetData> list1 = calc.PositionCalc(initUser.birth_year, initUser.birth_month, initUser.birth_day, initUser.birth_hour, initUser.birth_minute, initUser.birth_second, initUser.lat, initUser.lng);
            List<PlanetData> list2 = calc.PositionCalc(initUser.birth_year, initUser.birth_month, initUser.birth_day, initUser.birth_hour, initUser.birth_minute, initUser.birth_second, initUser.lat, initUser.lng);
            List<PlanetData> list3 = calc.PositionCalc(initUser.birth_year, initUser.birth_month, initUser.birth_day, initUser.birth_hour, initUser.birth_minute, initUser.birth_second, initUser.lat, initUser.lng);
            List<PlanetData> list4 = calc.PositionCalc(initUser.birth_year, initUser.birth_month, initUser.birth_day, initUser.birth_hour, initUser.birth_minute, initUser.birth_second, initUser.lat, initUser.lng);
            List<PlanetData> list5 = calc.PositionCalc(initUser.birth_year, initUser.birth_month, initUser.birth_day, initUser.birth_hour, initUser.birth_minute, initUser.birth_second, initUser.lat, initUser.lng);
            List<PlanetData> list6 = calc.PositionCalc(initUser.birth_year, initUser.birth_month, initUser.birth_day, initUser.birth_hour, initUser.birth_minute, initUser.birth_second, initUser.lat, initUser.lng);

            double[] houseList1 = calc.CuspCalc(initUser.birth_year, initUser.birth_month, initUser.birth_day, initUser.birth_hour, initUser.birth_minute, initUser.birth_second, initUser.lat, initUser.lng, config.houseCalc);
            double[] houseList2 = calc.CuspCalc(initUser.birth_year, initUser.birth_month, initUser.birth_day, initUser.birth_hour, initUser.birth_minute, initUser.birth_second, initUser.lat, initUser.lng, config.houseCalc);
            double[] houseList3 = calc.CuspCalc(initUser.birth_year, initUser.birth_month, initUser.birth_day, initUser.birth_hour, initUser.birth_minute, initUser.birth_second, initUser.lat, initUser.lng, config.houseCalc);
            double[] houseList4 = calc.CuspCalc(initUser.birth_year, initUser.birth_month, initUser.birth_day, initUser.birth_hour, initUser.birth_minute, initUser.birth_second, initUser.lat, initUser.lng, config.houseCalc);
            double[] houseList5 = calc.CuspCalc(initUser.birth_year, initUser.birth_month, initUser.birth_day, initUser.birth_hour, initUser.birth_minute, initUser.birth_second, initUser.lat, initUser.lng, config.houseCalc);
            double[] houseList6 = calc.CuspCalc(initUser.birth_year, initUser.birth_month, initUser.birth_day, initUser.birth_hour, initUser.birth_minute, initUser.birth_second, initUser.lat, initUser.lng, config.houseCalc);

            //viewmodel設定
            firstPList = new PlanetListViewModel(list1, list2, list3, list4, list5, list6);
            planetList.DataContext = firstPList;

            houseList = new HouseListViewModel(houseList1, houseList2, houseList3, houseList4, houseList5, houseList6);
            cuspList.DataContext = houseList;

            outerRing.DataContext = rcanvas;
            innerRing.DataContext = rcanvas;
            centerRing.DataContext = rcanvas;

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


            Console.WriteLine(ringCanvas.ActualWidth.ToString() + "," + ringStack.ActualHeight.ToString());
        }
    }
}
