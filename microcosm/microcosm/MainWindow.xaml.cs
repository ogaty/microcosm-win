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
                outerWidth = 368,
                outerHeight = 368
            };


            firstPList = new PlanetListViewModel();

            //viewmodel設定
            planetList.DataContext = firstPList;

            houseList = new HouseListViewModel();
            cuspList.DataContext = houseList;

        }

        private void Ellipse_MouseEnter(object sender, MouseEventArgs e)
        {
        }
    }
}
