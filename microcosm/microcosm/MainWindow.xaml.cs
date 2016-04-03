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

namespace microcosm
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public PlanetListViewModel firstPList;
        public SettingData[] settings = new SettingData[10];
        public RingCanvas rcanvas = new RingCanvas();

        public MainWindow()
        {
            InitializeComponent();

            UserData initUser = new UserData();
            UserBinding ub = new UserBinding(initUser);

            this.DataContext = new
            {
                userName = initUser.name,
                userBirthStr = ub.birthStr,
                userBirthPlace = initUser.birth_place,
                userLat = String.Format("{0:f4}", initUser.lat),
                userLng = String.Format("{0:f4}", initUser.lng)
            };

            DataInit();
        }

        private void DataInit()
        {
            firstPList = new PlanetListViewModel();
            planetList.DataContext = firstPList;

            Enumerable.Range(0, 10).ToList().ForEach(i =>
            {
                string filename = "setting" + i + ".csm";
                settings[i] = new SettingData(i);
                XmlSerializer serializer = new XmlSerializer(typeof(SettingXml));
                FileStream fs = new FileStream(filename, FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                serializer.Serialize(sw, settings[i].xmlData);
                sw.Close();
                fs.Close();
            });

            outerRing.DataContext = rcanvas;
        }

        private void Ellipse_MouseEnter(object sender, MouseEventArgs e)
        {
        }

        private void mainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            rcanvas.outerWidth = ringCanvas.ActualWidth;
            rcanvas.outerHeight = ringCanvas.ActualHeight;
            
            Console.WriteLine(ringCanvas.ActualWidth.ToString());
        }
    }
}
