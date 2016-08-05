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
using microcosm.Aspect;
using Microsoft.Win32;

namespace microcosm
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindowViewModel mainWindowVM;
        public PlanetListViewModel firstPList;
        public HouseListViewModel houseList;

        public SettingData[] settings = new SettingData[10];
        public SettingData currentSetting;
        public ConfigData config;
        public TempSetting tempSettings;

        public AstroCalc calc;
        public RingCanvasViewModel rcanvas;
        public ReportViewModel reportVM;

        public UserData targetUser;
        public UserEventData userdata;

        public Dictionary<int, int> dispListMap;

        public List<PlanetData> list1;
        public List<PlanetData> list2;
        public List<PlanetData> list3;
        public List<PlanetData> list4;
        public List<PlanetData> list5;
        public List<PlanetData> list6;
        public List<PlanetData> list7;

        public double[] houseList1;
        public double[] houseList2;
        public double[] houseList3;
        public double[] houseList4;
        public double[] houseList5;
        public double[] houseList6;
        public double[] houseList7;

        public CommonConfigWindow configWindow;
        public SettingWIndow setWindow;
        public ChartSelectorWindow chartSelecterWindow;
        public DatabaseWindow dbWindow;
        public CustomRingWindow ringWindow;
        public VersionWindow versionWindow;

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

            {
                string exePath = Environment.GetCommandLineArgs()[0];
                string exeDir = System.IO.Path.GetDirectoryName(exePath);
                string filename = @"system\config.csm";
                if (!File.Exists(filename))
                {
                    // 生成も
                    config = new ConfigData(exeDir + @"\ephe");
                    XmlSerializer serializer = new XmlSerializer(typeof(ConfigData));
                    FileStream fs = new FileStream(filename, FileMode.Create);
                    StreamWriter sw = new StreamWriter(fs);
                    serializer.Serialize(sw, config);
                    sw.Close();
                    fs.Close();
                }
                else
                {
                    // 読み込み
                    try
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(ConfigData));
                        FileStream fs = new FileStream(filename, FileMode.Open);
                        config = (ConfigData)serializer.Deserialize(fs);
                        fs.Close();
                    }
                    catch (IOException)
                    {
                        MessageBox.Show("設定ファイル読み込みに失敗しました。再作成します。");
                        XmlSerializer serializer = new XmlSerializer(typeof(ConfigData));
                        FileStream fs = new FileStream(filename, FileMode.Create);
                        StreamWriter sw = new StreamWriter(fs);
                        serializer.Serialize(sw, config);
                        sw.Close();
                        fs.Close();
                    }
                }
            }

            // 個別設定ファイル読み込み
            tempSettings = new TempSetting(config);
            Enumerable.Range(0, 10).ToList().ForEach(i =>
            {
                string filename = @"system\setting" + i + ".csm";
                settings[i] = new SettingData(i);

                if (!File.Exists(filename))
                {
                    // 生成
                    XmlSerializer serializer = new XmlSerializer(typeof(SettingXml));
                    FileStream fs = new FileStream(filename, FileMode.Create);
                    StreamWriter sw = new StreamWriter(fs);
                    serializer.Serialize(sw, settings[i].xmlData);
                    sw.Close();
                    fs.Close();
                } else
                {
                    // 読み込み
                    try
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(SettingXml));
                        FileStream fs = new FileStream(filename, FileMode.Open);
                        settings[i].xmlData = (SettingXml)serializer.Deserialize(fs);
                        fs.Close();
                    }
                    catch (IOException)
                    {
                        MessageBox.Show("設定ファイル読み込みに失敗しました。再作成します。");
                        XmlSerializer serializer = new XmlSerializer(typeof(SettingXml));
                        FileStream fs = new FileStream(filename, FileMode.Create);
                        StreamWriter sw = new StreamWriter(fs);
                        serializer.Serialize(sw, settings[i].xmlData);
                        sw.Close();
                        fs.Close();
                    }
                }
            });

            for (int i = 0; i < 10; i++)
            {
                settings[i].dispAspect[0, 0] = settings[i].xmlData.dispAspect[0];
                settings[i].dispAspect[1, 1] = settings[i].xmlData.dispAspect[1];
                settings[i].dispAspect[2, 2] = settings[i].xmlData.dispAspect[2];
                settings[i].dispAspect[0, 1] = settings[i].xmlData.dispAspect[3];
                settings[i].dispAspect[0, 2] = settings[i].xmlData.dispAspect[4];
                settings[i].dispAspect[1, 2] = settings[i].xmlData.dispAspect[5];
                settings[i].dispName = settings[i].xmlData.dispname;
                Dictionary<int, bool> dp11 = new Dictionary<int, bool>();
                dp11.Add(CommonData.ZODIAC_SUN, settings[i].xmlData.dispPlanetSun11);
                dp11.Add(CommonData.ZODIAC_MOON, settings[i].xmlData.dispPlanetMoon11);
                dp11.Add(CommonData.ZODIAC_MERCURY, settings[i].xmlData.dispPlanetMercury11);
                dp11.Add(CommonData.ZODIAC_VENUS, settings[i].xmlData.dispPlanetVenus11);
                dp11.Add(CommonData.ZODIAC_MARS, settings[i].xmlData.dispPlanetMars11);
                dp11.Add(CommonData.ZODIAC_JUPITER, settings[i].xmlData.dispPlanetJupiter11);
                dp11.Add(CommonData.ZODIAC_SATURN, settings[i].xmlData.dispPlanetSaturn11);
                dp11.Add(CommonData.ZODIAC_URANUS, settings[i].xmlData.dispPlanetUranus11);
                dp11.Add(CommonData.ZODIAC_NEPTUNE, settings[i].xmlData.dispPlanetNeptune11);
                dp11.Add(CommonData.ZODIAC_PLUTO, settings[i].xmlData.dispPlanetPluto11);
                dp11.Add(CommonData.ZODIAC_DH_TRUENODE, settings[i].xmlData.dispPlanetDh11);
                dp11.Add(CommonData.ZODIAC_ASC, settings[i].xmlData.dispPlanetAsc11);
                dp11.Add(CommonData.ZODIAC_MC, settings[i].xmlData.dispPlanetMc11);
                dp11.Add(CommonData.ZODIAC_CHIRON, settings[i].xmlData.dispPlanetChiron11);
                dp11.Add(CommonData.ZODIAC_EARTH, false);
                dp11.Add(CommonData.ZODIAC_LILITH, false);
                dp11.Add(CommonData.ZODIAC_CELES, false);
                dp11.Add(CommonData.ZODIAC_PARAS, false);
                dp11.Add(CommonData.ZODIAC_JUNO, false);
                dp11.Add(CommonData.ZODIAC_VESTA, false);
                dp11.Add(CommonData.ZODIAC_DT_OSCULATE_APOGEE, false);
                settings[i].dispPlanet.Add(dp11);
                Dictionary<int, bool> dp22 = new Dictionary<int, bool>();
                dp22.Add(CommonData.ZODIAC_SUN, settings[i].xmlData.dispPlanetSun22);
                dp22.Add(CommonData.ZODIAC_MOON, settings[i].xmlData.dispPlanetMoon22);
                dp22.Add(CommonData.ZODIAC_MERCURY, settings[i].xmlData.dispPlanetMercury22);
                dp22.Add(CommonData.ZODIAC_VENUS, settings[i].xmlData.dispPlanetVenus22);
                dp22.Add(CommonData.ZODIAC_MARS, settings[i].xmlData.dispPlanetMars22);
                dp22.Add(CommonData.ZODIAC_JUPITER, settings[i].xmlData.dispPlanetJupiter22);
                dp22.Add(CommonData.ZODIAC_SATURN, settings[i].xmlData.dispPlanetSaturn22);
                dp22.Add(CommonData.ZODIAC_URANUS, settings[i].xmlData.dispPlanetUranus22);
                dp22.Add(CommonData.ZODIAC_NEPTUNE, settings[i].xmlData.dispPlanetNeptune22);
                dp22.Add(CommonData.ZODIAC_PLUTO, settings[i].xmlData.dispPlanetPluto22);
                dp22.Add(CommonData.ZODIAC_DH_TRUENODE, settings[i].xmlData.dispPlanetDh22);
                dp22.Add(CommonData.ZODIAC_ASC, settings[i].xmlData.dispPlanetAsc22);
                dp22.Add(CommonData.ZODIAC_MC, settings[i].xmlData.dispPlanetMc22);
                dp22.Add(CommonData.ZODIAC_CHIRON, settings[i].xmlData.dispPlanetChiron22);
                dp22.Add(CommonData.ZODIAC_EARTH, false);
                dp22.Add(CommonData.ZODIAC_LILITH, false);
                dp22.Add(CommonData.ZODIAC_CELES, false);
                dp22.Add(CommonData.ZODIAC_PARAS, false);
                dp22.Add(CommonData.ZODIAC_JUNO, false);
                dp22.Add(CommonData.ZODIAC_VESTA, false);
                dp22.Add(CommonData.ZODIAC_DT_OSCULATE_APOGEE, false);
                settings[i].dispPlanet.Add(dp22);
                Dictionary<int, bool> dp33 = new Dictionary<int, bool>();
                dp33.Add(CommonData.ZODIAC_SUN, settings[i].xmlData.dispPlanetSun33);
                dp33.Add(CommonData.ZODIAC_MOON, settings[i].xmlData.dispPlanetMoon33);
                dp33.Add(CommonData.ZODIAC_MERCURY, settings[i].xmlData.dispPlanetMercury33);
                dp33.Add(CommonData.ZODIAC_VENUS, settings[i].xmlData.dispPlanetVenus33);
                dp33.Add(CommonData.ZODIAC_MARS, settings[i].xmlData.dispPlanetMars33);
                dp33.Add(CommonData.ZODIAC_JUPITER, settings[i].xmlData.dispPlanetJupiter33);
                dp33.Add(CommonData.ZODIAC_SATURN, settings[i].xmlData.dispPlanetSaturn33);
                dp33.Add(CommonData.ZODIAC_URANUS, settings[i].xmlData.dispPlanetUranus33);
                dp33.Add(CommonData.ZODIAC_NEPTUNE, settings[i].xmlData.dispPlanetNeptune33);
                dp33.Add(CommonData.ZODIAC_PLUTO, settings[i].xmlData.dispPlanetPluto33);
                dp33.Add(CommonData.ZODIAC_DH_TRUENODE, settings[i].xmlData.dispPlanetDh33);
                dp33.Add(CommonData.ZODIAC_ASC, settings[i].xmlData.dispPlanetAsc33);
                dp33.Add(CommonData.ZODIAC_MC, settings[i].xmlData.dispPlanetMc33);
                dp33.Add(CommonData.ZODIAC_CHIRON, settings[i].xmlData.dispPlanetChiron33);
                dp33.Add(CommonData.ZODIAC_EARTH, false);
                dp33.Add(CommonData.ZODIAC_LILITH, false);
                dp33.Add(CommonData.ZODIAC_CELES, false);
                dp33.Add(CommonData.ZODIAC_PARAS, false);
                dp33.Add(CommonData.ZODIAC_JUNO, false);
                dp33.Add(CommonData.ZODIAC_VESTA, false);
                dp33.Add(CommonData.ZODIAC_DT_OSCULATE_APOGEE, false);
                settings[i].dispPlanet.Add(dp33);
                Dictionary<int, bool> dp12 = new Dictionary<int, bool>();
                dp12.Add(CommonData.ZODIAC_SUN, settings[i].xmlData.dispPlanetSun12);
                dp12.Add(CommonData.ZODIAC_MOON, settings[i].xmlData.dispPlanetMoon12);
                dp12.Add(CommonData.ZODIAC_MERCURY, settings[i].xmlData.dispPlanetMercury12);
                dp12.Add(CommonData.ZODIAC_VENUS, settings[i].xmlData.dispPlanetVenus12);
                dp12.Add(CommonData.ZODIAC_MARS, settings[i].xmlData.dispPlanetMars12);
                dp12.Add(CommonData.ZODIAC_JUPITER, settings[i].xmlData.dispPlanetJupiter12);
                dp12.Add(CommonData.ZODIAC_SATURN, settings[i].xmlData.dispPlanetSaturn12);
                dp12.Add(CommonData.ZODIAC_URANUS, settings[i].xmlData.dispPlanetUranus12);
                dp12.Add(CommonData.ZODIAC_NEPTUNE, settings[i].xmlData.dispPlanetNeptune12);
                dp12.Add(CommonData.ZODIAC_PLUTO, settings[i].xmlData.dispPlanetPluto12);
                dp12.Add(CommonData.ZODIAC_DH_TRUENODE, settings[i].xmlData.dispPlanetDh12);
                dp12.Add(CommonData.ZODIAC_ASC, settings[i].xmlData.dispPlanetAsc12);
                dp12.Add(CommonData.ZODIAC_MC, settings[i].xmlData.dispPlanetMc12);
                dp12.Add(CommonData.ZODIAC_CHIRON, settings[i].xmlData.dispPlanetChiron12);
                dp12.Add(CommonData.ZODIAC_EARTH, false);
                dp12.Add(CommonData.ZODIAC_LILITH, false);
                dp12.Add(CommonData.ZODIAC_CELES, false);
                dp12.Add(CommonData.ZODIAC_PARAS, false);
                dp12.Add(CommonData.ZODIAC_JUNO, false);
                dp12.Add(CommonData.ZODIAC_VESTA, false);
                dp12.Add(CommonData.ZODIAC_DT_OSCULATE_APOGEE, false);
                settings[i].dispPlanet.Add(dp12);
                Dictionary<int, bool> dp13 = new Dictionary<int, bool>();
                dp13.Add(CommonData.ZODIAC_SUN, settings[i].xmlData.dispPlanetSun13);
                dp13.Add(CommonData.ZODIAC_MOON, settings[i].xmlData.dispPlanetMoon13);
                dp13.Add(CommonData.ZODIAC_MERCURY, settings[i].xmlData.dispPlanetMercury13);
                dp13.Add(CommonData.ZODIAC_VENUS, settings[i].xmlData.dispPlanetVenus13);
                dp13.Add(CommonData.ZODIAC_MARS, settings[i].xmlData.dispPlanetMars13);
                dp13.Add(CommonData.ZODIAC_JUPITER, settings[i].xmlData.dispPlanetJupiter13);
                dp13.Add(CommonData.ZODIAC_SATURN, settings[i].xmlData.dispPlanetSaturn13);
                dp13.Add(CommonData.ZODIAC_URANUS, settings[i].xmlData.dispPlanetUranus13);
                dp13.Add(CommonData.ZODIAC_NEPTUNE, settings[i].xmlData.dispPlanetNeptune13);
                dp13.Add(CommonData.ZODIAC_PLUTO, settings[i].xmlData.dispPlanetPluto13);
                dp13.Add(CommonData.ZODIAC_DH_TRUENODE, settings[i].xmlData.dispPlanetDh13);
                dp13.Add(CommonData.ZODIAC_ASC, settings[i].xmlData.dispPlanetAsc13);
                dp13.Add(CommonData.ZODIAC_MC, settings[i].xmlData.dispPlanetMc13);
                dp13.Add(CommonData.ZODIAC_CHIRON, settings[i].xmlData.dispPlanetChiron13);
                dp13.Add(CommonData.ZODIAC_EARTH, false);
                dp13.Add(CommonData.ZODIAC_LILITH, false);
                dp13.Add(CommonData.ZODIAC_CELES, false);
                dp13.Add(CommonData.ZODIAC_PARAS, false);
                dp13.Add(CommonData.ZODIAC_JUNO, false);
                dp13.Add(CommonData.ZODIAC_VESTA, false);
                dp13.Add(CommonData.ZODIAC_DT_OSCULATE_APOGEE, false);
                settings[i].dispPlanet.Add(dp13);
                Dictionary<int, bool> dp23 = new Dictionary<int, bool>();
                dp23.Add(CommonData.ZODIAC_SUN, settings[i].xmlData.dispPlanetSun23);
                dp23.Add(CommonData.ZODIAC_MOON, settings[i].xmlData.dispPlanetMoon23);
                dp23.Add(CommonData.ZODIAC_MERCURY, settings[i].xmlData.dispPlanetMercury23);
                dp23.Add(CommonData.ZODIAC_VENUS, settings[i].xmlData.dispPlanetVenus23);
                dp23.Add(CommonData.ZODIAC_MARS, settings[i].xmlData.dispPlanetMars23);
                dp23.Add(CommonData.ZODIAC_JUPITER, settings[i].xmlData.dispPlanetJupiter23);
                dp23.Add(CommonData.ZODIAC_SATURN, settings[i].xmlData.dispPlanetSaturn23);
                dp23.Add(CommonData.ZODIAC_URANUS, settings[i].xmlData.dispPlanetUranus23);
                dp23.Add(CommonData.ZODIAC_NEPTUNE, settings[i].xmlData.dispPlanetNeptune23);
                dp23.Add(CommonData.ZODIAC_PLUTO, settings[i].xmlData.dispPlanetPluto23);
                dp23.Add(CommonData.ZODIAC_DH_TRUENODE, settings[i].xmlData.dispPlanetDh23);
                dp23.Add(CommonData.ZODIAC_ASC, settings[i].xmlData.dispPlanetAsc23);
                dp23.Add(CommonData.ZODIAC_MC, settings[i].xmlData.dispPlanetMc23);
                dp23.Add(CommonData.ZODIAC_CHIRON, settings[i].xmlData.dispPlanetChiron23);
                dp23.Add(CommonData.ZODIAC_EARTH, false);
                dp23.Add(CommonData.ZODIAC_LILITH, false);
                dp23.Add(CommonData.ZODIAC_CELES, false);
                dp23.Add(CommonData.ZODIAC_PARAS, false);
                dp23.Add(CommonData.ZODIAC_JUNO, false);
                dp23.Add(CommonData.ZODIAC_VESTA, false);
                dp23.Add(CommonData.ZODIAC_DT_OSCULATE_APOGEE, false);
                settings[i].dispPlanet.Add(dp23);
                Dictionary<int, bool> d11 = new Dictionary<int, bool>();
                d11.Add(CommonData.ZODIAC_SUN, settings[i].xmlData.aspectSun11);
                d11.Add(CommonData.ZODIAC_MOON, settings[i].xmlData.aspectMoon11);
                d11.Add(CommonData.ZODIAC_MERCURY, settings[i].xmlData.aspectMercury11);
                d11.Add(CommonData.ZODIAC_VENUS, settings[i].xmlData.aspectVenus11);
                d11.Add(CommonData.ZODIAC_MARS, settings[i].xmlData.aspectMars11);
                d11.Add(CommonData.ZODIAC_JUPITER, settings[i].xmlData.aspectJupiter11);
                d11.Add(CommonData.ZODIAC_SATURN, settings[i].xmlData.aspectSaturn11);
                d11.Add(CommonData.ZODIAC_URANUS, settings[i].xmlData.aspectUranus11);
                d11.Add(CommonData.ZODIAC_NEPTUNE, settings[i].xmlData.aspectNeptune11);
                d11.Add(CommonData.ZODIAC_PLUTO, settings[i].xmlData.aspectPluto11);
                d11.Add(CommonData.ZODIAC_DH_TRUENODE, settings[i].xmlData.aspectDh11);
                d11.Add(CommonData.ZODIAC_ASC, settings[i].xmlData.aspectAsc11);
                d11.Add(CommonData.ZODIAC_MC, settings[i].xmlData.aspectMc11);
                d11.Add(CommonData.ZODIAC_CHIRON, settings[i].xmlData.aspectChiron11);
                d11.Add(CommonData.ZODIAC_EARTH, false);
                d11.Add(CommonData.ZODIAC_LILITH, false);
                d11.Add(CommonData.ZODIAC_CELES, false);
                d11.Add(CommonData.ZODIAC_PARAS, false);
                d11.Add(CommonData.ZODIAC_JUNO, false);
                d11.Add(CommonData.ZODIAC_VESTA, false);
                d11.Add(CommonData.ZODIAC_DT_OSCULATE_APOGEE, false);
                settings[i].dispAspectPlanet.Add(d11);
                Dictionary<int, bool> d22 = new Dictionary<int, bool>();
                d22.Add(CommonData.ZODIAC_SUN, settings[i].xmlData.aspectSun22);
                d22.Add(CommonData.ZODIAC_MOON, settings[i].xmlData.aspectMoon22);
                d22.Add(CommonData.ZODIAC_MERCURY, settings[i].xmlData.aspectMercury22);
                d22.Add(CommonData.ZODIAC_VENUS, settings[i].xmlData.aspectVenus22);
                d22.Add(CommonData.ZODIAC_MARS, settings[i].xmlData.aspectMars22);
                d22.Add(CommonData.ZODIAC_JUPITER, settings[i].xmlData.aspectJupiter22);
                d22.Add(CommonData.ZODIAC_SATURN, settings[i].xmlData.aspectSaturn22);
                d22.Add(CommonData.ZODIAC_URANUS, settings[i].xmlData.aspectUranus22);
                d22.Add(CommonData.ZODIAC_NEPTUNE, settings[i].xmlData.aspectNeptune22);
                d22.Add(CommonData.ZODIAC_PLUTO, settings[i].xmlData.aspectPluto22);
                d22.Add(CommonData.ZODIAC_DH_TRUENODE, settings[i].xmlData.aspectDh22);
                d22.Add(CommonData.ZODIAC_ASC, settings[i].xmlData.aspectAsc22);
                d22.Add(CommonData.ZODIAC_MC, settings[i].xmlData.aspectMc22);
                d22.Add(CommonData.ZODIAC_CHIRON, settings[i].xmlData.aspectChiron22);
                d22.Add(CommonData.ZODIAC_EARTH, false);
                d22.Add(CommonData.ZODIAC_LILITH, false);
                d22.Add(CommonData.ZODIAC_CELES, false);
                d22.Add(CommonData.ZODIAC_PARAS, false);
                d22.Add(CommonData.ZODIAC_JUNO, false);
                d22.Add(CommonData.ZODIAC_VESTA, false);
                d22.Add(CommonData.ZODIAC_DT_OSCULATE_APOGEE, false);
                settings[i].dispAspectPlanet.Add(d22);
                Dictionary<int, bool> d33 = new Dictionary<int, bool>();
                d33.Add(CommonData.ZODIAC_SUN, settings[i].xmlData.aspectSun33);
                d33.Add(CommonData.ZODIAC_MOON, settings[i].xmlData.aspectMoon33);
                d33.Add(CommonData.ZODIAC_MERCURY, settings[i].xmlData.aspectMercury33);
                d33.Add(CommonData.ZODIAC_VENUS, settings[i].xmlData.aspectVenus33);
                d33.Add(CommonData.ZODIAC_MARS, settings[i].xmlData.aspectMars33);
                d33.Add(CommonData.ZODIAC_JUPITER, settings[i].xmlData.aspectJupiter33);
                d33.Add(CommonData.ZODIAC_SATURN, settings[i].xmlData.aspectSaturn33);
                d33.Add(CommonData.ZODIAC_URANUS, settings[i].xmlData.aspectUranus33);
                d33.Add(CommonData.ZODIAC_NEPTUNE, settings[i].xmlData.aspectNeptune33);
                d33.Add(CommonData.ZODIAC_PLUTO, settings[i].xmlData.aspectPluto33);
                d33.Add(CommonData.ZODIAC_DH_TRUENODE, settings[i].xmlData.aspectDh33);
                d33.Add(CommonData.ZODIAC_ASC, settings[i].xmlData.aspectAsc33);
                d33.Add(CommonData.ZODIAC_MC, settings[i].xmlData.aspectMc33);
                d33.Add(CommonData.ZODIAC_CHIRON, settings[i].xmlData.aspectChiron33);
                d33.Add(CommonData.ZODIAC_EARTH, false);
                d33.Add(CommonData.ZODIAC_LILITH, false);
                d33.Add(CommonData.ZODIAC_CELES, false);
                d33.Add(CommonData.ZODIAC_PARAS, false);
                d33.Add(CommonData.ZODIAC_JUNO, false);
                d33.Add(CommonData.ZODIAC_VESTA, false);
                d33.Add(CommonData.ZODIAC_DT_OSCULATE_APOGEE, false);
                settings[i].dispAspectPlanet.Add(d33);
                Dictionary<int, bool> d12 = new Dictionary<int, bool>();
                d12.Add(CommonData.ZODIAC_SUN, settings[i].xmlData.aspectSun12);
                d12.Add(CommonData.ZODIAC_MOON, settings[i].xmlData.aspectMoon12);
                d12.Add(CommonData.ZODIAC_MERCURY, settings[i].xmlData.aspectMercury12);
                d12.Add(CommonData.ZODIAC_VENUS, settings[i].xmlData.aspectVenus12);
                d12.Add(CommonData.ZODIAC_MARS, settings[i].xmlData.aspectMars12);
                d12.Add(CommonData.ZODIAC_JUPITER, settings[i].xmlData.aspectJupiter12);
                d12.Add(CommonData.ZODIAC_SATURN, settings[i].xmlData.aspectSaturn12);
                d12.Add(CommonData.ZODIAC_URANUS, settings[i].xmlData.aspectUranus12);
                d12.Add(CommonData.ZODIAC_NEPTUNE, settings[i].xmlData.aspectNeptune12);
                d12.Add(CommonData.ZODIAC_PLUTO, settings[i].xmlData.aspectPluto12);
                d12.Add(CommonData.ZODIAC_DH_TRUENODE, settings[i].xmlData.aspectDh12);
                d12.Add(CommonData.ZODIAC_ASC, settings[i].xmlData.aspectAsc12);
                d12.Add(CommonData.ZODIAC_MC, settings[i].xmlData.aspectMc12);
                d12.Add(CommonData.ZODIAC_CHIRON, settings[i].xmlData.aspectChiron12);
                d12.Add(CommonData.ZODIAC_EARTH, false);
                d12.Add(CommonData.ZODIAC_LILITH, false);
                d12.Add(CommonData.ZODIAC_CELES, false);
                d12.Add(CommonData.ZODIAC_PARAS, false);
                d12.Add(CommonData.ZODIAC_JUNO, false);
                d12.Add(CommonData.ZODIAC_VESTA, false);
                d12.Add(CommonData.ZODIAC_DT_OSCULATE_APOGEE, false);
                settings[i].dispAspectPlanet.Add(d12);
                Dictionary<int, bool> d13 = new Dictionary<int, bool>();
                d13.Add(CommonData.ZODIAC_SUN, settings[i].xmlData.aspectSun13);
                d13.Add(CommonData.ZODIAC_MOON, settings[i].xmlData.aspectMoon13);
                d13.Add(CommonData.ZODIAC_MERCURY, settings[i].xmlData.aspectMercury13);
                d13.Add(CommonData.ZODIAC_VENUS, settings[i].xmlData.aspectVenus13);
                d13.Add(CommonData.ZODIAC_MARS, settings[i].xmlData.aspectMars13);
                d13.Add(CommonData.ZODIAC_JUPITER, settings[i].xmlData.aspectJupiter13);
                d13.Add(CommonData.ZODIAC_SATURN, settings[i].xmlData.aspectSaturn13);
                d13.Add(CommonData.ZODIAC_URANUS, settings[i].xmlData.aspectUranus13);
                d13.Add(CommonData.ZODIAC_NEPTUNE, settings[i].xmlData.aspectNeptune13);
                d13.Add(CommonData.ZODIAC_PLUTO, settings[i].xmlData.aspectPluto13);
                d13.Add(CommonData.ZODIAC_DH_TRUENODE, settings[i].xmlData.aspectDh13);
                d13.Add(CommonData.ZODIAC_ASC, settings[i].xmlData.aspectAsc13);
                d13.Add(CommonData.ZODIAC_MC, settings[i].xmlData.aspectMc13);
                d13.Add(CommonData.ZODIAC_CHIRON, settings[i].xmlData.aspectChiron13);
                d13.Add(CommonData.ZODIAC_EARTH, false);
                d13.Add(CommonData.ZODIAC_LILITH, false);
                d13.Add(CommonData.ZODIAC_CELES, false);
                d13.Add(CommonData.ZODIAC_PARAS, false);
                d13.Add(CommonData.ZODIAC_JUNO, false);
                d13.Add(CommonData.ZODIAC_VESTA, false);
                d13.Add(CommonData.ZODIAC_DT_OSCULATE_APOGEE, false);
                settings[i].dispAspectPlanet.Add(d13);
                Dictionary<int, bool> d23 = new Dictionary<int, bool>();
                d23.Add(CommonData.ZODIAC_SUN, settings[i].xmlData.aspectSun23);
                d23.Add(CommonData.ZODIAC_MOON, settings[i].xmlData.aspectMoon23);
                d23.Add(CommonData.ZODIAC_MERCURY, settings[i].xmlData.aspectMercury23);
                d23.Add(CommonData.ZODIAC_VENUS, settings[i].xmlData.aspectVenus23);
                d23.Add(CommonData.ZODIAC_MARS, settings[i].xmlData.aspectMars23);
                d23.Add(CommonData.ZODIAC_JUPITER, settings[i].xmlData.aspectJupiter23);
                d23.Add(CommonData.ZODIAC_SATURN, settings[i].xmlData.aspectSaturn23);
                d23.Add(CommonData.ZODIAC_URANUS, settings[i].xmlData.aspectUranus23);
                d23.Add(CommonData.ZODIAC_NEPTUNE, settings[i].xmlData.aspectNeptune23);
                d23.Add(CommonData.ZODIAC_PLUTO, settings[i].xmlData.aspectPluto23);
                d23.Add(CommonData.ZODIAC_DH_TRUENODE, settings[i].xmlData.aspectDh23);
                d23.Add(CommonData.ZODIAC_ASC, settings[i].xmlData.aspectAsc23);
                d23.Add(CommonData.ZODIAC_MC, settings[i].xmlData.aspectMc23);
                d23.Add(CommonData.ZODIAC_CHIRON, settings[i].xmlData.aspectChiron23);
                d23.Add(CommonData.ZODIAC_EARTH, false);
                d23.Add(CommonData.ZODIAC_LILITH, false);
                d23.Add(CommonData.ZODIAC_CELES, false);
                d23.Add(CommonData.ZODIAC_PARAS, false);
                d23.Add(CommonData.ZODIAC_JUNO, false);
                d23.Add(CommonData.ZODIAC_VESTA, false);
                d23.Add(CommonData.ZODIAC_DT_OSCULATE_APOGEE, false);
                settings[i].dispAspectPlanet.Add(d23);

                Dictionary<AspectKind, bool> a11 = new Dictionary<AspectKind, bool>();
                a11.Add(AspectKind.CONJUNCTION, settings[i].xmlData.aspectConjunction11);
                a11.Add(AspectKind.OPPOSITION, settings[i].xmlData.aspectOpposition11);
                a11.Add(AspectKind.TRINE, settings[i].xmlData.aspectTrine11);
                a11.Add(AspectKind.SQUARE, settings[i].xmlData.aspectSquare11);
                a11.Add(AspectKind.SEXTILE, settings[i].xmlData.aspectSextile11);
                a11.Add(AspectKind.INCONJUNCT, settings[i].xmlData.aspectInconjunct11);
                a11.Add(AspectKind.SESQUIQUADRATE, settings[i].xmlData.aspectSesquiquadrate11);
                settings[i].dispAspectCategory.Add(a11);
                Dictionary<AspectKind, bool> a22 = new Dictionary<AspectKind, bool>();
                a22.Add(AspectKind.CONJUNCTION, settings[i].xmlData.aspectConjunction22);
                a22.Add(AspectKind.OPPOSITION, settings[i].xmlData.aspectOpposition22);
                a22.Add(AspectKind.TRINE, settings[i].xmlData.aspectTrine22);
                a22.Add(AspectKind.SQUARE, settings[i].xmlData.aspectSquare22);
                a22.Add(AspectKind.SEXTILE, settings[i].xmlData.aspectSextile22);
                a22.Add(AspectKind.INCONJUNCT, settings[i].xmlData.aspectInconjunct22);
                a22.Add(AspectKind.SESQUIQUADRATE, settings[i].xmlData.aspectSesquiquadrate22);
                settings[i].dispAspectCategory.Add(a22);
                Dictionary<AspectKind, bool> a33 = new Dictionary<AspectKind, bool>();
                a33.Add(AspectKind.CONJUNCTION, settings[i].xmlData.aspectConjunction33);
                a33.Add(AspectKind.OPPOSITION, settings[i].xmlData.aspectOpposition33);
                a33.Add(AspectKind.TRINE, settings[i].xmlData.aspectTrine33);
                a33.Add(AspectKind.SQUARE, settings[i].xmlData.aspectSquare33);
                a33.Add(AspectKind.SEXTILE, settings[i].xmlData.aspectSextile33);
                a33.Add(AspectKind.INCONJUNCT, settings[i].xmlData.aspectInconjunct33);
                a33.Add(AspectKind.SESQUIQUADRATE, settings[i].xmlData.aspectSesquiquadrate33);
                settings[i].dispAspectCategory.Add(a33);
                Dictionary<AspectKind, bool> a12 = new Dictionary<AspectKind, bool>();
                a12.Add(AspectKind.CONJUNCTION, settings[i].xmlData.aspectConjunction12);
                a12.Add(AspectKind.OPPOSITION, settings[i].xmlData.aspectOpposition12);
                a12.Add(AspectKind.TRINE, settings[i].xmlData.aspectTrine12);
                a12.Add(AspectKind.SQUARE, settings[i].xmlData.aspectSquare12);
                a12.Add(AspectKind.SEXTILE, settings[i].xmlData.aspectSextile12);
                a12.Add(AspectKind.INCONJUNCT, settings[i].xmlData.aspectInconjunct12);
                a12.Add(AspectKind.SESQUIQUADRATE, settings[i].xmlData.aspectSesquiquadrate12);
                settings[i].dispAspectCategory.Add(a12);
                Dictionary<AspectKind, bool> a13 = new Dictionary<AspectKind, bool>();
                a13.Add(AspectKind.CONJUNCTION, settings[i].xmlData.aspectConjunction13);
                a13.Add(AspectKind.OPPOSITION, settings[i].xmlData.aspectOpposition13);
                a13.Add(AspectKind.TRINE, settings[i].xmlData.aspectTrine13);
                a13.Add(AspectKind.SQUARE, settings[i].xmlData.aspectSquare13);
                a13.Add(AspectKind.SEXTILE, settings[i].xmlData.aspectSextile13);
                a13.Add(AspectKind.INCONJUNCT, settings[i].xmlData.aspectInconjunct13);
                a13.Add(AspectKind.SESQUIQUADRATE, settings[i].xmlData.aspectSesquiquadrate13);
                settings[i].dispAspectCategory.Add(a13);
                Dictionary<AspectKind, bool> a23 = new Dictionary<AspectKind, bool>();
                a23.Add(AspectKind.CONJUNCTION, settings[i].xmlData.aspectConjunction23);
                a23.Add(AspectKind.OPPOSITION, settings[i].xmlData.aspectOpposition23);
                a23.Add(AspectKind.TRINE, settings[i].xmlData.aspectTrine23);
                a23.Add(AspectKind.SQUARE, settings[i].xmlData.aspectSquare23);
                a23.Add(AspectKind.SEXTILE, settings[i].xmlData.aspectSextile23);
                a23.Add(AspectKind.INCONJUNCT, settings[i].xmlData.aspectInconjunct23);
                a23.Add(AspectKind.SESQUIQUADRATE, settings[i].xmlData.aspectSesquiquadrate23);
                settings[i].dispAspectCategory.Add(a23);
                Dictionary<OrbKind, double> o11 = new Dictionary<OrbKind, double>();
                o11.Add(OrbKind.SUN_HARD_1ST, settings[i].xmlData.orb_sun_hard_1st_0);
                o11.Add(OrbKind.SUN_SOFT_1ST, settings[i].xmlData.orb_sun_soft_1st_0);
                o11.Add(OrbKind.SUN_HARD_2ND, settings[i].xmlData.orb_sun_hard_2nd_0);
                o11.Add(OrbKind.SUN_SOFT_2ND, settings[i].xmlData.orb_sun_soft_2nd_0);
                o11.Add(OrbKind.SUN_HARD_150, settings[i].xmlData.orb_sun_hard_150_0);
                o11.Add(OrbKind.SUN_SOFT_150, settings[i].xmlData.orb_sun_soft_150_0);
                o11.Add(OrbKind.MOON_HARD_1ST, settings[i].xmlData.orb_moon_hard_1st_0);
                o11.Add(OrbKind.MOON_SOFT_1ST, settings[i].xmlData.orb_moon_soft_1st_0);
                o11.Add(OrbKind.MOON_HARD_2ND, settings[i].xmlData.orb_moon_hard_2nd_0);
                o11.Add(OrbKind.MOON_SOFT_2ND, settings[i].xmlData.orb_moon_soft_2nd_0);
                o11.Add(OrbKind.MOON_HARD_150, settings[i].xmlData.orb_moon_hard_150_0);
                o11.Add(OrbKind.MOON_SOFT_150, settings[i].xmlData.orb_moon_soft_150_0);
                o11.Add(OrbKind.OTHER_HARD_1ST, settings[i].xmlData.orb_other_hard_1st_0);
                o11.Add(OrbKind.OTHER_SOFT_1ST, settings[i].xmlData.orb_other_soft_1st_0);
                o11.Add(OrbKind.OTHER_HARD_2ND, settings[i].xmlData.orb_other_hard_2nd_0);
                o11.Add(OrbKind.OTHER_SOFT_2ND, settings[i].xmlData.orb_other_soft_2nd_0);
                o11.Add(OrbKind.OTHER_HARD_150, settings[i].xmlData.orb_other_hard_150_0);
                o11.Add(OrbKind.OTHER_SOFT_150, settings[i].xmlData.orb_other_soft_150_0);
                settings[i].orbs.Add(o11);
                Dictionary<OrbKind, double> o22 = new Dictionary<OrbKind, double>();
                o22.Add(OrbKind.SUN_HARD_1ST, settings[i].xmlData.orb_sun_hard_1st_1);
                o22.Add(OrbKind.SUN_SOFT_1ST, settings[i].xmlData.orb_sun_soft_1st_1);
                o22.Add(OrbKind.SUN_HARD_2ND, settings[i].xmlData.orb_sun_hard_2nd_1);
                o22.Add(OrbKind.SUN_SOFT_2ND, settings[i].xmlData.orb_sun_soft_2nd_1);
                o22.Add(OrbKind.SUN_HARD_150, settings[i].xmlData.orb_sun_hard_150_1);
                o22.Add(OrbKind.SUN_SOFT_150, settings[i].xmlData.orb_sun_soft_150_1);
                o22.Add(OrbKind.MOON_HARD_1ST, settings[i].xmlData.orb_moon_hard_1st_1);
                o22.Add(OrbKind.MOON_SOFT_1ST, settings[i].xmlData.orb_moon_soft_1st_1);
                o22.Add(OrbKind.MOON_HARD_2ND, settings[i].xmlData.orb_moon_hard_2nd_1);
                o22.Add(OrbKind.MOON_SOFT_2ND, settings[i].xmlData.orb_moon_soft_2nd_1);
                o22.Add(OrbKind.MOON_HARD_150, settings[i].xmlData.orb_moon_hard_150_1);
                o22.Add(OrbKind.MOON_SOFT_150, settings[i].xmlData.orb_moon_soft_150_1);
                o22.Add(OrbKind.OTHER_HARD_1ST, settings[i].xmlData.orb_other_hard_1st_1);
                o22.Add(OrbKind.OTHER_SOFT_1ST, settings[i].xmlData.orb_other_soft_1st_1);
                o22.Add(OrbKind.OTHER_HARD_2ND, settings[i].xmlData.orb_other_hard_2nd_1);
                o22.Add(OrbKind.OTHER_SOFT_2ND, settings[i].xmlData.orb_other_soft_2nd_1);
                o22.Add(OrbKind.OTHER_HARD_150, settings[i].xmlData.orb_other_hard_150_1);
                o22.Add(OrbKind.OTHER_SOFT_150, settings[i].xmlData.orb_other_soft_150_1);
                settings[i].orbs.Add(o22);
                Dictionary<OrbKind, double> o33 = new Dictionary<OrbKind, double>();
                o33.Add(OrbKind.SUN_HARD_1ST, settings[i].xmlData.orb_sun_hard_1st_2);
                o33.Add(OrbKind.SUN_SOFT_1ST, settings[i].xmlData.orb_sun_soft_1st_2);
                o33.Add(OrbKind.SUN_HARD_2ND, settings[i].xmlData.orb_sun_hard_2nd_2);
                o33.Add(OrbKind.SUN_SOFT_2ND, settings[i].xmlData.orb_sun_soft_2nd_2);
                o33.Add(OrbKind.SUN_HARD_150, settings[i].xmlData.orb_sun_hard_150_2);
                o33.Add(OrbKind.SUN_SOFT_150, settings[i].xmlData.orb_sun_soft_150_2);
                o33.Add(OrbKind.MOON_HARD_1ST, settings[i].xmlData.orb_moon_hard_1st_2);
                o33.Add(OrbKind.MOON_SOFT_1ST, settings[i].xmlData.orb_moon_soft_1st_2);
                o33.Add(OrbKind.MOON_HARD_2ND, settings[i].xmlData.orb_moon_hard_2nd_2);
                o33.Add(OrbKind.MOON_SOFT_2ND, settings[i].xmlData.orb_moon_soft_2nd_2);
                o33.Add(OrbKind.MOON_HARD_150, settings[i].xmlData.orb_moon_hard_150_2);
                o33.Add(OrbKind.MOON_SOFT_150, settings[i].xmlData.orb_moon_soft_150_2);
                o33.Add(OrbKind.OTHER_HARD_1ST, settings[i].xmlData.orb_other_hard_1st_2);
                o33.Add(OrbKind.OTHER_SOFT_1ST, settings[i].xmlData.orb_other_soft_1st_2);
                o33.Add(OrbKind.OTHER_HARD_2ND, settings[i].xmlData.orb_other_hard_2nd_2);
                o33.Add(OrbKind.OTHER_SOFT_2ND, settings[i].xmlData.orb_other_soft_2nd_2);
                o33.Add(OrbKind.OTHER_HARD_150, settings[i].xmlData.orb_other_hard_150_2);
                o33.Add(OrbKind.OTHER_SOFT_150, settings[i].xmlData.orb_other_soft_150_2);
                settings[i].orbs.Add(o33);
                Dictionary<OrbKind, double> o12 = new Dictionary<OrbKind, double>();
                o12.Add(OrbKind.SUN_HARD_1ST, settings[i].xmlData.orb_sun_hard_1st_3);
                o12.Add(OrbKind.SUN_SOFT_1ST, settings[i].xmlData.orb_sun_soft_1st_3);
                o12.Add(OrbKind.SUN_HARD_2ND, settings[i].xmlData.orb_sun_hard_2nd_3);
                o12.Add(OrbKind.SUN_SOFT_2ND, settings[i].xmlData.orb_sun_soft_2nd_3);
                o12.Add(OrbKind.SUN_HARD_150, settings[i].xmlData.orb_sun_hard_150_3);
                o12.Add(OrbKind.SUN_SOFT_150, settings[i].xmlData.orb_sun_soft_150_3);
                o12.Add(OrbKind.MOON_HARD_1ST, settings[i].xmlData.orb_moon_hard_1st_3);
                o12.Add(OrbKind.MOON_SOFT_1ST, settings[i].xmlData.orb_moon_soft_1st_3);
                o12.Add(OrbKind.MOON_HARD_2ND, settings[i].xmlData.orb_moon_hard_2nd_3);
                o12.Add(OrbKind.MOON_SOFT_2ND, settings[i].xmlData.orb_moon_soft_2nd_3);
                o12.Add(OrbKind.MOON_HARD_150, settings[i].xmlData.orb_moon_hard_150_3);
                o12.Add(OrbKind.MOON_SOFT_150, settings[i].xmlData.orb_moon_soft_150_3);
                o12.Add(OrbKind.OTHER_HARD_1ST, settings[i].xmlData.orb_other_hard_1st_3);
                o12.Add(OrbKind.OTHER_SOFT_1ST, settings[i].xmlData.orb_other_soft_1st_3);
                o12.Add(OrbKind.OTHER_HARD_2ND, settings[i].xmlData.orb_other_hard_2nd_3);
                o12.Add(OrbKind.OTHER_SOFT_2ND, settings[i].xmlData.orb_other_soft_2nd_3);
                o12.Add(OrbKind.OTHER_HARD_150, settings[i].xmlData.orb_other_hard_150_3);
                o12.Add(OrbKind.OTHER_SOFT_150, settings[i].xmlData.orb_other_soft_150_3);
                settings[i].orbs.Add(o12);
                Dictionary<OrbKind, double> o13 = new Dictionary<OrbKind, double>();
                o13.Add(OrbKind.SUN_HARD_1ST, settings[i].xmlData.orb_sun_hard_1st_4);
                o13.Add(OrbKind.SUN_SOFT_1ST, settings[i].xmlData.orb_sun_soft_1st_4);
                o13.Add(OrbKind.SUN_HARD_2ND, settings[i].xmlData.orb_sun_hard_2nd_4);
                o13.Add(OrbKind.SUN_SOFT_2ND, settings[i].xmlData.orb_sun_soft_2nd_4);
                o13.Add(OrbKind.SUN_HARD_150, settings[i].xmlData.orb_sun_hard_150_4);
                o13.Add(OrbKind.SUN_SOFT_150, settings[i].xmlData.orb_sun_soft_150_4);
                o13.Add(OrbKind.MOON_HARD_1ST, settings[i].xmlData.orb_moon_hard_1st_4);
                o13.Add(OrbKind.MOON_SOFT_1ST, settings[i].xmlData.orb_moon_soft_1st_4);
                o13.Add(OrbKind.MOON_HARD_2ND, settings[i].xmlData.orb_moon_hard_2nd_4);
                o13.Add(OrbKind.MOON_SOFT_2ND, settings[i].xmlData.orb_moon_soft_2nd_4);
                o13.Add(OrbKind.MOON_HARD_150, settings[i].xmlData.orb_moon_hard_150_4);
                o13.Add(OrbKind.MOON_SOFT_150, settings[i].xmlData.orb_moon_soft_150_4);
                o13.Add(OrbKind.OTHER_HARD_1ST, settings[i].xmlData.orb_other_hard_1st_4);
                o13.Add(OrbKind.OTHER_SOFT_1ST, settings[i].xmlData.orb_other_soft_1st_4);
                o13.Add(OrbKind.OTHER_HARD_2ND, settings[i].xmlData.orb_other_hard_2nd_4);
                o13.Add(OrbKind.OTHER_SOFT_2ND, settings[i].xmlData.orb_other_soft_2nd_4);
                o13.Add(OrbKind.OTHER_HARD_150, settings[i].xmlData.orb_other_hard_150_4);
                o13.Add(OrbKind.OTHER_SOFT_150, settings[i].xmlData.orb_other_soft_150_4);
                settings[i].orbs.Add(o13);
                Dictionary<OrbKind, double> o23 = new Dictionary<OrbKind, double>();
                o23.Add(OrbKind.SUN_HARD_1ST, settings[i].xmlData.orb_sun_hard_1st_5);
                o23.Add(OrbKind.SUN_SOFT_1ST, settings[i].xmlData.orb_sun_soft_1st_5);
                o23.Add(OrbKind.SUN_HARD_2ND, settings[i].xmlData.orb_sun_hard_2nd_5);
                o23.Add(OrbKind.SUN_SOFT_2ND, settings[i].xmlData.orb_sun_soft_2nd_5);
                o23.Add(OrbKind.SUN_HARD_150, settings[i].xmlData.orb_sun_hard_150_5);
                o23.Add(OrbKind.SUN_SOFT_150, settings[i].xmlData.orb_sun_soft_150_5);
                o23.Add(OrbKind.MOON_HARD_1ST, settings[i].xmlData.orb_moon_hard_1st_5);
                o23.Add(OrbKind.MOON_SOFT_1ST, settings[i].xmlData.orb_moon_soft_1st_5);
                o23.Add(OrbKind.MOON_HARD_2ND, settings[i].xmlData.orb_moon_hard_2nd_5);
                o23.Add(OrbKind.MOON_SOFT_2ND, settings[i].xmlData.orb_moon_soft_2nd_5);
                o23.Add(OrbKind.MOON_HARD_150, settings[i].xmlData.orb_moon_hard_150_5);
                o23.Add(OrbKind.MOON_SOFT_150, settings[i].xmlData.orb_moon_soft_150_5);
                o23.Add(OrbKind.OTHER_HARD_1ST, settings[i].xmlData.orb_other_hard_1st_5);
                o23.Add(OrbKind.OTHER_SOFT_1ST, settings[i].xmlData.orb_other_soft_1st_5);
                o23.Add(OrbKind.OTHER_HARD_2ND, settings[i].xmlData.orb_other_hard_2nd_5);
                o23.Add(OrbKind.OTHER_SOFT_2ND, settings[i].xmlData.orb_other_soft_2nd_5);
                o23.Add(OrbKind.OTHER_HARD_150, settings[i].xmlData.orb_other_hard_150_5);
                o23.Add(OrbKind.OTHER_SOFT_150, settings[i].xmlData.orb_other_soft_150_5);
                settings[i].orbs.Add(o23);
            }
            currentSetting = settings[0];


            dispListMap = new Dictionary<int, int>();
            dispListMap.Add(CommonData.ZODIAC_SUN, 0);
            dispListMap.Add(CommonData.ZODIAC_MOON, 1);
            dispListMap.Add(CommonData.ZODIAC_MERCURY, 2);
            dispListMap.Add(CommonData.ZODIAC_VENUS, 3);
            dispListMap.Add(CommonData.ZODIAC_MARS, 4);
            dispListMap.Add(CommonData.ZODIAC_JUPITER, 5);
            dispListMap.Add(CommonData.ZODIAC_SATURN, 6);
            dispListMap.Add(CommonData.ZODIAC_URANUS, 7);
            dispListMap.Add(CommonData.ZODIAC_NEPTUNE, 8);
            dispListMap.Add(CommonData.ZODIAC_PLUTO, 9);
            dispListMap.Add(CommonData.ZODIAC_DH_TRUENODE, 10);
            dispListMap.Add(CommonData.ZODIAC_CHIRON, 11);
            dispListMap.Add(CommonData.ZODIAC_ASC, 12);
            dispListMap.Add(CommonData.ZODIAC_MC, 13);
            dispListMap.Add(CommonData.ZODIAC_EARTH, 14);

            rcanvas = new RingCanvasViewModel(config);
            ringStack.Background = System.Windows.Media.Brushes.GhostWhite;

        }

        // AstroCalcインスタンス
        private void DataCalc()
        {
            calc = new AstroCalc(this, config);
        }

        private void SetViewModel()
        {
            targetUser = new UserData(config);
            userdata = new UserEventData()
            {
                name = targetUser.name,
                birth_year = targetUser.birth_year,
                birth_month = targetUser.birth_month,
                birth_day = targetUser.birth_day,
                birth_hour = targetUser.birth_hour,
                birth_minute = targetUser.birth_minute,
                birth_second = targetUser.birth_second,
                birth_place = targetUser.birth_place,
                birth_str = targetUser.birth_str,
                lat = targetUser.lat,
                lng = targetUser.lng,
                lat_lng = targetUser.lat_lng,
                timezone = targetUser.timezone,
                memo = targetUser.memo,
                fullpath = targetUser.filename
            };
            UserBinding ub = new UserBinding(targetUser);
            TransitBinding tb = new TransitBinding(targetUser);
            mainWindowVM = new MainWindowViewModel()
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
            this.DataContext = mainWindowVM;
            if (config.progression == EProgression.PRIMARY)
            {
                mainWindowVM.progressionCalc = "一度一年法";
            }
            else if (config.progression == EProgression.SECONDARY)
            {
                mainWindowVM.progressionCalc = "一日一年法";
            }
            else
            {
                mainWindowVM.progressionCalc = "CPS";
            }
            if (config.sidereal == Esidereal.SIDEREAL)
            {
                mainWindowVM.siderealStr = "SIDEREAL";
            }
            else
            {
                mainWindowVM.siderealStr = "TROPICAL";
            }
            if (config.houseCalc == EHouseCalc.CAMPANUS)
            {
                mainWindowVM.houseDivide = "CAMPANUS";
            }
            else if (config.houseCalc == EHouseCalc.EQUAL)
            {
                mainWindowVM.houseDivide = "EQUAL";
            }
            else if (config.houseCalc == EHouseCalc.KOCH)
            {
                mainWindowVM.houseDivide = "KOCH";
            }
            else if (config.houseCalc == EHouseCalc.PLACIDUS)
            {
                mainWindowVM.houseDivide = "PLACIDUS";
            }

            if (config.centric == ECentric.GEO_CENTRIC)
            {
                mainWindowVM.centricMode = "GeoCentric";
            }
            else
            {
                mainWindowVM.centricMode = "HelioCentric";
            }

            UserEventData edata = CommonData.udata2event(targetUser);
            ReCalc(edata, edata, edata, edata, edata, edata, edata);

            //viewmodel設定
            firstPList = new PlanetListViewModel(this, list1, list2, list3, list4, list5, list6);
            planetList.DataContext = firstPList;

            houseList = new HouseListViewModel(houseList1, houseList2, houseList3, houseList4, houseList5, houseList6);
            cuspList.DataContext = houseList;

            explanation.DataContext = mainWindowVM;

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
            if (tempSettings.firstHouseDiv == TempSetting.HouseDivide.USER1)
            {
                UserEventData edata = CommonData.udata2event(targetUser);
                ReCalc(edata, userdata, userdata, userdata, userdata, userdata, userdata);
            } else
            {
                ReCalc(userdata, userdata, userdata, userdata, userdata, userdata, userdata);
            }
        }

        // 再計算
        // 表示可否の時はここは呼ばない
        public void ReCalc(
                UserEventData list1Data,
                UserEventData list2Data,
                UserEventData list3Data,
                UserEventData list4Data,
                UserEventData list5Data,
                UserEventData list6Data,
                UserEventData list7Data
            )
        {
            if (list1Data != null)
            {
                if (tempSettings.firstBand == TempSetting.BandKind.PROGRESS)
                {
                    // プログレスの場合（まず使わないけど）
                    List<PlanetData> tempList;
                    switch (config.progression)
                    {
                        case EProgression.PRIMARY:
                            tempList = calc.PositionCalc(list1Data.birth_year, list1Data.birth_month, list1Data.birth_day,
                                list1Data.birth_hour, list1Data.birth_minute, list1Data.birth_second,
                                list1Data.lat, list1Data.lng, (int)config.houseCalc, 0);

                            list1 = calc.PrimaryProgressionCalc(tempList,
                                new DateTime(targetUser.birth_year,
                                    targetUser.birth_month,
                                    targetUser.birth_day,
                                    targetUser.birth_hour,
                                    targetUser.birth_minute,
                                    targetUser.birth_second
                                ),
                                new DateTime(userdata.birth_year,
                                    userdata.birth_month,
                                    userdata.birth_day,
                                    userdata.birth_hour,
                                    userdata.birth_minute,
                                    userdata.birth_second
                                )
                            );
                            if (tempSettings.firstHouseDiv == TempSetting.HouseDivide.PROGRESS)
                            {
                                houseList1 = calc.PrimaryProgressionHouseCalc(houseList1,
                                    new DateTime(targetUser.birth_year,
                                        targetUser.birth_month,
                                        targetUser.birth_day,
                                        targetUser.birth_hour,
                                        targetUser.birth_minute,
                                        targetUser.birth_second
                                    ),
                                    new DateTime(userdata.birth_year,
                                        userdata.birth_month,
                                        userdata.birth_day,
                                        userdata.birth_hour,
                                        userdata.birth_minute,
                                        userdata.birth_second
                                    )
                                );
                            }

                            break;

                        case EProgression.SECONDARY:
                            tempList = calc.PositionCalc(list1Data.birth_year, list1Data.birth_month, list1Data.birth_day,
                                list1Data.birth_hour, list1Data.birth_minute, list1Data.birth_second,
                                list1Data.lat, list1Data.lng, (int)config.houseCalc, 0);

                            list1 = calc.SecondaryProgressionCalc(tempList,
                                new DateTime(targetUser.birth_year,
                                    targetUser.birth_month,
                                    targetUser.birth_day,
                                    targetUser.birth_hour,
                                    targetUser.birth_minute,
                                    targetUser.birth_second
                                ),
                                new DateTime(userdata.birth_year,
                                    userdata.birth_month,
                                    userdata.birth_day,
                                    userdata.birth_hour,
                                    userdata.birth_minute,
                                    userdata.birth_second
                                )
                            );
                            if (tempSettings.firstHouseDiv == TempSetting.HouseDivide.PROGRESS)
                            {
                                houseList1 = calc.SecondaryProgressionHouseCalc(houseList1,
                                    tempList,
                                    new DateTime(targetUser.birth_year,
                                        targetUser.birth_month,
                                        targetUser.birth_day,
                                        targetUser.birth_hour,
                                        targetUser.birth_minute,
                                        targetUser.birth_second
                                    ),
                                    new DateTime(userdata.birth_year,
                                        userdata.birth_month,
                                        userdata.birth_day,
                                        userdata.birth_hour,
                                        userdata.birth_minute,
                                        userdata.birth_second
                                    ),
                                    targetUser.lat,
                                    targetUser.lng,
                                    targetUser.timezone
                                );

                            }

                            break;

                        case EProgression.CPS:
                            tempList = calc.PositionCalc(list1Data.birth_year, list1Data.birth_month, list1Data.birth_day,
                                list1Data.birth_hour, list1Data.birth_minute, list1Data.birth_second,
                                list1Data.lat, list1Data.lng, (int)config.houseCalc, 0);

                            list1 = calc.CompositProgressionCalc(tempList,
                                new DateTime(targetUser.birth_year,
                                    targetUser.birth_month,
                                    targetUser.birth_day,
                                    targetUser.birth_hour,
                                    targetUser.birth_minute,
                                    targetUser.birth_second
                                ),
                                new DateTime(userdata.birth_year,
                                    userdata.birth_month,
                                    userdata.birth_day,
                                    userdata.birth_hour,
                                    userdata.birth_minute,
                                    userdata.birth_second
                                )
                            );
                            if (tempSettings.firstHouseDiv == TempSetting.HouseDivide.PROGRESS)
                            {
                                houseList1 = calc.CompositProgressionHouseCalc(houseList1,
                                    tempList,
                                    new DateTime(targetUser.birth_year,
                                        targetUser.birth_month,
                                        targetUser.birth_day,
                                        targetUser.birth_hour,
                                        targetUser.birth_minute,
                                        targetUser.birth_second
                                    ),
                                    new DateTime(userdata.birth_year,
                                        userdata.birth_month,
                                        userdata.birth_day,
                                        userdata.birth_hour,
                                        userdata.birth_minute,
                                        userdata.birth_second
                                    ),
                                    targetUser.lat,
                                    targetUser.lng,
                                    targetUser.timezone
                                );

                            }

                            break;
                    }
                }
                else
                {
                    list1 = calc.PositionCalc(list1Data.birth_year, list1Data.birth_month, list1Data.birth_day,
                        list1Data.birth_hour, list1Data.birth_minute, list1Data.birth_second,
                        list1Data.lat, list1Data.lng, (int)config.houseCalc, 0);
                }
                if (tempSettings.firstHouseDiv == TempSetting.HouseDivide.USER1)
                {
                    houseList1 = calc.CuspCalc(targetUser.birth_year, targetUser.birth_month, targetUser.birth_day,
                        targetUser.birth_hour, targetUser.birth_minute, targetUser.birth_second,
                        targetUser.lat, targetUser.lng, (int)config.houseCalc);
                }
                else if (tempSettings.firstHouseDiv == TempSetting.HouseDivide.EVENT1)
                {
                    houseList1 = calc.CuspCalc(userdata.birth_year, userdata.birth_month, userdata.birth_day,
                        userdata.birth_hour, userdata.birth_minute, userdata.birth_second,
                        userdata.lat, userdata.lng, (int)config.houseCalc);
                }
            }
            if (list2Data != null)
            {
                if (tempSettings.secondBand == TempSetting.BandKind.PROGRESS)
                {
                    switch (config.progression)
                    {
                        case EProgression.PRIMARY:
                            list2 = calc.PrimaryProgressionCalc(list1,
                                new DateTime(targetUser.birth_year,
                                    targetUser.birth_month,
                                    targetUser.birth_day,
                                    targetUser.birth_hour,
                                    targetUser.birth_minute,
                                    targetUser.birth_second
                                ),
                                new DateTime(userdata.birth_year,
                                    userdata.birth_month,
                                    userdata.birth_day,
                                    userdata.birth_hour,
                                    userdata.birth_minute,
                                    userdata.birth_second
                                )
                            );
                            if (tempSettings.secondHouseDiv == TempSetting.HouseDivide.PROGRESS)
                            {
                                houseList2 = calc.PrimaryProgressionHouseCalc(houseList1,
                                    new DateTime(targetUser.birth_year,
                                        targetUser.birth_month,
                                        targetUser.birth_day,
                                        targetUser.birth_hour,
                                        targetUser.birth_minute,
                                        targetUser.birth_second
                                    ),
                                    new DateTime(userdata.birth_year,
                                        userdata.birth_month,
                                        userdata.birth_day,
                                        userdata.birth_hour,
                                        userdata.birth_minute,
                                        userdata.birth_second
                                    )
                                );
                            }

                            break;

                        case EProgression.SECONDARY:
                            list2 = calc.SecondaryProgressionCalc(list1,
                                new DateTime(targetUser.birth_year,
                                    targetUser.birth_month,
                                    targetUser.birth_day,
                                    targetUser.birth_hour,
                                    targetUser.birth_minute,
                                    targetUser.birth_second
                                ),
                                new DateTime(userdata.birth_year,
                                    userdata.birth_month,
                                    userdata.birth_day,
                                    userdata.birth_hour,
                                    userdata.birth_minute,
                                    userdata.birth_second
                                )
                            );
                            if (tempSettings.secondHouseDiv == TempSetting.HouseDivide.PROGRESS)
                            {
                                houseList2 = calc.SecondaryProgressionHouseCalc(houseList1,
                                    list1,
                                    new DateTime(targetUser.birth_year,
                                        targetUser.birth_month,
                                        targetUser.birth_day,
                                        targetUser.birth_hour,
                                        targetUser.birth_minute,
                                        targetUser.birth_second
                                    ),
                                    new DateTime(userdata.birth_year,
                                        userdata.birth_month,
                                        userdata.birth_day,
                                        userdata.birth_hour,
                                        userdata.birth_minute,
                                        userdata.birth_second
                                    ),
                                    targetUser.lat,
                                    targetUser.lng,
                                    targetUser.timezone
                                );

                            }

                            break;

                        case EProgression.CPS:
                            list2 = calc.CompositProgressionCalc(list1,
                                new DateTime(targetUser.birth_year,
                                    targetUser.birth_month,
                                    targetUser.birth_day,
                                    targetUser.birth_hour,
                                    targetUser.birth_minute,
                                    targetUser.birth_second
                                ),
                                new DateTime(userdata.birth_year,
                                    userdata.birth_month,
                                    userdata.birth_day,
                                    userdata.birth_hour,
                                    userdata.birth_minute,
                                    userdata.birth_second
                                )
                            );
                            if (tempSettings.secondHouseDiv == TempSetting.HouseDivide.PROGRESS)
                            {
                                houseList2 = calc.CompositProgressionHouseCalc(houseList1,
                                    list1,
                                    new DateTime(targetUser.birth_year,
                                        targetUser.birth_month,
                                        targetUser.birth_day,
                                        targetUser.birth_hour,
                                        targetUser.birth_minute,
                                        targetUser.birth_second
                                    ),
                                    new DateTime(userdata.birth_year,
                                        userdata.birth_month,
                                        userdata.birth_day,
                                        userdata.birth_hour,
                                        userdata.birth_minute,
                                        userdata.birth_second
                                    ),
                                    targetUser.lat,
                                    targetUser.lng,
                                    targetUser.timezone
                                );

                            }

                            break;
                    }
                }
                else
                {
                    list2 = calc.PositionCalc(list2Data.birth_year, list2Data.birth_month, list2Data.birth_day,
                        list2Data.birth_hour, list2Data.birth_minute, list2Data.birth_second,
                        list2Data.lat, list2Data.lng, (int)config.houseCalc, 1);
                }
                if (tempSettings.secondHouseDiv == TempSetting.HouseDivide.USER1)
                {
                    houseList2 = calc.CuspCalc(targetUser.birth_year, targetUser.birth_month, targetUser.birth_day,
                        targetUser.birth_hour, targetUser.birth_minute, targetUser.birth_second,
                        targetUser.lat, targetUser.lng, (int)config.houseCalc);
                }
                else if (tempSettings.secondHouseDiv == TempSetting.HouseDivide.EVENT1)
                {
                    houseList2 = calc.CuspCalc(userdata.birth_year, userdata.birth_month, userdata.birth_day,
                        userdata.birth_hour, userdata.birth_minute, userdata.birth_second,
                        userdata.lat, userdata.lng, (int)config.houseCalc);
                }
            }
            if (list3Data != null)
            {
                if (tempSettings.thirdBand == TempSetting.BandKind.PROGRESS)
                {
                    switch (config.progression)
                    {
                        case EProgression.PRIMARY:
                            list3 = calc.PrimaryProgressionCalc(list1,
                                new DateTime(targetUser.birth_year,
                                    targetUser.birth_month,
                                    targetUser.birth_day,
                                    targetUser.birth_hour,
                                    targetUser.birth_minute,
                                    targetUser.birth_second
                                ),
                                new DateTime(userdata.birth_year,
                                    userdata.birth_month,
                                    userdata.birth_day,
                                    userdata.birth_hour,
                                    userdata.birth_minute,
                                    userdata.birth_second
                                )
                            );
                            if (tempSettings.thirdHouseDiv == TempSetting.HouseDivide.PROGRESS)
                            {
                                houseList3 = calc.PrimaryProgressionHouseCalc(houseList3,
                                    new DateTime(targetUser.birth_year,
                                        targetUser.birth_month,
                                        targetUser.birth_day,
                                        targetUser.birth_hour,
                                        targetUser.birth_minute,
                                        targetUser.birth_second
                                    ),
                                    new DateTime(userdata.birth_year,
                                        userdata.birth_month,
                                        userdata.birth_day,
                                        userdata.birth_hour,
                                        userdata.birth_minute,
                                        userdata.birth_second
                                    )
                                );
                            }

                            break;

                        case EProgression.SECONDARY:
                            list3 = calc.SecondaryProgressionCalc(list1,
                                new DateTime(targetUser.birth_year,
                                    targetUser.birth_month,
                                    targetUser.birth_day,
                                    targetUser.birth_hour,
                                    targetUser.birth_minute,
                                    targetUser.birth_second
                                ),
                                new DateTime(userdata.birth_year,
                                    userdata.birth_month,
                                    userdata.birth_day,
                                    userdata.birth_hour,
                                    userdata.birth_minute,
                                    userdata.birth_second
                                )
                            );
                            if (tempSettings.thirdHouseDiv == TempSetting.HouseDivide.PROGRESS)
                            {
                                houseList3 = calc.SecondaryProgressionHouseCalc(houseList3,
                                    list1,
                                    new DateTime(targetUser.birth_year,
                                        targetUser.birth_month,
                                        targetUser.birth_day,
                                        targetUser.birth_hour,
                                        targetUser.birth_minute,
                                        targetUser.birth_second
                                    ),
                                    new DateTime(userdata.birth_year,
                                        userdata.birth_month,
                                        userdata.birth_day,
                                        userdata.birth_hour,
                                        userdata.birth_minute,
                                        userdata.birth_second
                                    ),
                                    targetUser.lat,
                                    targetUser.lng,
                                    targetUser.timezone
                                );

                            }

                            break;

                        case EProgression.CPS:
                            list3 = calc.CompositProgressionCalc(list1,
                                new DateTime(targetUser.birth_year,
                                    targetUser.birth_month,
                                    targetUser.birth_day,
                                    targetUser.birth_hour,
                                    targetUser.birth_minute,
                                    targetUser.birth_second
                                ),
                                new DateTime(userdata.birth_year,
                                    userdata.birth_month,
                                    userdata.birth_day,
                                    userdata.birth_hour,
                                    userdata.birth_minute,
                                    userdata.birth_second
                                )
                            );
                            if (tempSettings.thirdHouseDiv == TempSetting.HouseDivide.PROGRESS)
                            {
                                houseList3 = calc.CompositProgressionHouseCalc(houseList3,
                                    list1,
                                    new DateTime(targetUser.birth_year,
                                        targetUser.birth_month,
                                        targetUser.birth_day,
                                        targetUser.birth_hour,
                                        targetUser.birth_minute,
                                        targetUser.birth_second
                                    ),
                                    new DateTime(userdata.birth_year,
                                        userdata.birth_month,
                                        userdata.birth_day,
                                        userdata.birth_hour,
                                        userdata.birth_minute,
                                        userdata.birth_second
                                    ),
                                    targetUser.lat,
                                    targetUser.lng,
                                    targetUser.timezone
                                );

                            }

                            break;
                    }
                }
                else
                {
                    list3 = calc.PositionCalc(list3Data.birth_year, list3Data.birth_month, list3Data.birth_day,
                        list3Data.birth_hour, list3Data.birth_minute, list3Data.birth_second,
                        list3Data.lat, list3Data.lng, (int)config.houseCalc, 2);
                }
                if (tempSettings.thirdHouseDiv == TempSetting.HouseDivide.USER1)
                {
                    houseList3 = calc.CuspCalc(targetUser.birth_year, targetUser.birth_month, targetUser.birth_day,
                        targetUser.birth_hour, targetUser.birth_minute, targetUser.birth_second,
                        targetUser.lat, targetUser.lng, (int)config.houseCalc);
                }
                else if (tempSettings.thirdHouseDiv == TempSetting.HouseDivide.EVENT1)
                {
                    houseList3 = calc.CuspCalc(userdata.birth_year, userdata.birth_month, userdata.birth_day,
                        userdata.birth_hour, userdata.birth_minute, userdata.birth_second,
                        userdata.lat, userdata.lng, (int)config.houseCalc);
                }
            }
            if (list4Data != null)
            {
                if (tempSettings.fourthBand == TempSetting.BandKind.PROGRESS)
                {
                    list4 = calc.PositionCalc(list4Data.birth_year, list4Data.birth_month, list4Data.birth_day,
                        list4Data.birth_hour, list4Data.birth_minute, list4Data.birth_second,
                        list4Data.lat, list4Data.lng, (int)config.houseCalc, -1);
                }
                else
                {
                    list4 = calc.PositionCalc(list4Data.birth_year, list4Data.birth_month, list4Data.birth_day,
                        list4Data.birth_hour, list4Data.birth_minute, list4Data.birth_second,
                        list4Data.lat, list4Data.lng, (int)config.houseCalc, -1);
                }
                houseList4 = calc.CuspCalc(list4Data.birth_year, list4Data.birth_month, list4Data.birth_day,
                    list4Data.birth_hour, list4Data.birth_minute, list4Data.birth_second,
                    list4Data.lat, list4Data.lng, (int)config.houseCalc);
            }
            if (list5Data != null)
            {
                if (tempSettings.fifthBand == TempSetting.BandKind.PROGRESS)
                {
                    list5 = calc.PositionCalc(list5Data.birth_year, list5Data.birth_month, list5Data.birth_day,
                        list5Data.birth_hour, list5Data.birth_minute, list5Data.birth_second,
                        list5Data.lat, list5Data.lng, (int)config.houseCalc, -1);
                }
                else
                {
                    list5 = calc.PositionCalc(list5Data.birth_year, list5Data.birth_month, list5Data.birth_day,
                        list5Data.birth_hour, list5Data.birth_minute, list5Data.birth_second,
                        list5Data.lat, list5Data.lng, (int)config.houseCalc, -1);
                }
                houseList5 = calc.CuspCalc(list5Data.birth_year, list5Data.birth_month, list5Data.birth_day,
                    list5Data.birth_hour, list5Data.birth_minute, list5Data.birth_second,
                    list5Data.lat, list5Data.lng, (int)config.houseCalc);
            }
            if (list6Data != null)
            {
                if (tempSettings.sixthBand == TempSetting.BandKind.PROGRESS)
                {
                    list6 = calc.PositionCalc(list6Data.birth_year, list6Data.birth_month, list6Data.birth_day,
                        list6Data.birth_hour, list6Data.birth_minute, list6Data.birth_second,
                        list6Data.lat, list6Data.lng, (int)config.houseCalc, -1);
                }
                else
                {
                    list6 = calc.PositionCalc(list6Data.birth_year, list6Data.birth_month, list6Data.birth_day,
                        list6Data.birth_hour, list6Data.birth_minute, list6Data.birth_second,
                        list6Data.lat, list6Data.lng, (int)config.houseCalc, -1);
                }
                houseList6 = calc.CuspCalc(list6Data.birth_year, list6Data.birth_month, list6Data.birth_day,
                    list6Data.birth_hour, list6Data.birth_minute, list6Data.birth_second,
                    list6Data.lat, list6Data.lng, (int)config.houseCalc);
            }
            if (list7Data != null)
            {
                if (tempSettings.secondBand == TempSetting.BandKind.PROGRESS)
                {
                    list7 = calc.PositionCalc(list7Data.birth_year, list7Data.birth_month, list7Data.birth_day,
                        list7Data.birth_hour, list7Data.birth_minute, list7Data.birth_second,
                        list7Data.lat, list7Data.lng, (int)config.houseCalc, -1);
                }
                else
                {
                    list7 = calc.PositionCalc(list7Data.birth_year, list7Data.birth_month, list7Data.birth_day,
                        list7Data.birth_hour, list7Data.birth_minute, list7Data.birth_second,
                        list7Data.lat, list7Data.lng, (int)config.houseCalc, -1);
                }
                houseList7 = calc.CuspCalc(list7Data.birth_year, list7Data.birth_month, list7Data.birth_day,
                    list7Data.birth_hour, list7Data.birth_minute, list7Data.birth_second,
                    list7Data.lat, list7Data.lng, (int)config.houseCalc);
            }


            AspectCalc aspect = new AspectCalc(this);
            list1 = aspect.AspectCalcSame(currentSetting, list1);
            list1 = aspect.AspectCalcOther(currentSetting, list1, list2, 3);
            list1 = aspect.AspectCalcOther(currentSetting, list1, list3, 4);
//            list1 = aspect.AspectCalcOther(currentSetting, list1, list4, 9);
//            list1 = aspect.AspectCalcOther(currentSetting, list1, list5, 10);
//            list1 = aspect.AspectCalcOther(currentSetting, list1, list6, 20);
            list2 = aspect.AspectCalcSame(currentSetting, list2);
            list2 = aspect.AspectCalcOther(currentSetting, list2, list3, 5);
//            list2 = aspect.AspectCalcOther(currentSetting, list2, list4, 11);
//            list2 = aspect.AspectCalcOther(currentSetting, list2, list5, 12);
//            list2 = aspect.AspectCalcOther(currentSetting, list2, list6, 20);
            list3 = aspect.AspectCalcSame(currentSetting, list3);
//            list3 = aspect.AspectCalcOther(currentSetting, list3, list4, 13);
//            list3 = aspect.AspectCalcOther(currentSetting, list3, list5, 14);
//            list3 = aspect.AspectCalcOther(currentSetting, list3, list6, 20);
//            list4 = aspect.AspectCalcSame(currentSetting, list4);
//            list4 = aspect.AspectCalcOther(currentSetting, list4, list5, 15);
//            list4 = aspect.AspectCalcOther(currentSetting, list4, list6, 20);
//            list5 = aspect.AspectCalcSame(currentSetting, list5);
//            list5 = aspect.AspectCalcOther(currentSetting, list5, list6, 20);
//            list6 = aspect.AspectCalcSame(currentSetting, list6);
        }

        private void mainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ReRender();
        }

        // レンダリングメイン
        // disp変更の場合はこれだけ呼ぶ
        public void ReRender()
        {
            AllClear();
            rcanvas.innerLeft = config.zodiacWidth / 2;
            rcanvas.innerTop = config.zodiacWidth / 2;
            if (ringCanvas.ActualWidth > ringStack.ActualHeight)
            {
                if (tempSettings.centerPattern == 0 || tempSettings.bands > 1)
                {
                    tempSettings.zodiacCenter = (int)(ringStack.ActualHeight * 0.7 / 2);
                }
                else
                {
                    tempSettings.zodiacCenter = (int)(ringStack.ActualHeight * 0.6);
                }

                rcanvas.outerWidth = ringStack.ActualHeight;
                rcanvas.outerHeight = ringStack.ActualHeight;
                rcanvas.innerWidth = ringStack.ActualHeight - config.zodiacWidth;
                rcanvas.innerHeight = ringStack.ActualHeight - config.zodiacWidth;
                rcanvas.centerLeft = ringStack.ActualHeight / 2 - tempSettings.zodiacCenter / 2;
                rcanvas.centerTop = ringStack.ActualHeight / 2 - tempSettings.zodiacCenter / 2;
            }
            else
            {
                if (tempSettings.centerPattern == 0 || tempSettings.bands > 1)
                {
                    tempSettings.zodiacCenter = (int)(ringCanvas.ActualWidth * 0.7 / 2);
                }
                else
                {
                    tempSettings.zodiacCenter = (int)(ringCanvas.ActualWidth * 0.6);
                }

                rcanvas.outerWidth = ringCanvas.ActualWidth;
                rcanvas.outerHeight = ringCanvas.ActualWidth;
                rcanvas.innerWidth = ringCanvas.ActualWidth - config.zodiacWidth;
                rcanvas.innerHeight = ringCanvas.ActualWidth - config.zodiacWidth;
                rcanvas.centerLeft = ringCanvas.ActualWidth / 2 - tempSettings.zodiacCenter / 2;
                rcanvas.centerTop = ringCanvas.ActualWidth / 2 - tempSettings.zodiacCenter / 2;
            }


            // Console.WriteLine(ringCanvas.ActualWidth.ToString() + "," + ringStack.ActualHeight.ToString());

            firstPList.ReRender(list1, list2, list3, list4, list5, list6);
            houseList.ReRender(houseList1, houseList2, houseList3, houseList4, houseList5, houseList6);

            circleRender();
            houseCuspRender(houseList1, houseList2, houseList3, houseList4, houseList5);

            // houseCuspRender2(houseList1);
            signCuspRender(houseList1[1]);
            zodiacRender(houseList1[1]);
            list1.Sort((a, b) => (int)(a.absolute_position - b.absolute_position));
            list2.Sort((a, b) => (int)(a.absolute_position - b.absolute_position));
            list3.Sort((a, b) => (int)(a.absolute_position - b.absolute_position));
            list4.Sort((a, b) => (int)(a.absolute_position - b.absolute_position));
            list5.Sort((a, b) => (int)(a.absolute_position - b.absolute_position));
            planetRender(houseList1[1], list1, list2, list3, list4, list5);
            planetLine(houseList1[1], list1, list2, list3, list4, list5);
            aspectsRendering(houseList1[1], list1, list2, list3, list4, list5);

            // 大丈夫だと思うけど戻しておく
            list1.Sort((a, b) => (int)(a.no - b.no));
            list2.Sort((a, b) => (int)(a.no - b.no));
            list3.Sort((a, b) => (int)(a.no - b.no));
            list4.Sort((a, b) => (int)(a.no - b.no));
            list5.Sort((a, b) => (int)(a.no - b.no));

            Label copy = new Label();
            copy.Content = "microcosm";
            copy.Margin = new Thickness(ringCanvas.ActualWidth - 70, ringStack.ActualHeight - 45, 0, 0);
            Label url = new Label();
            url.Content = "http://ogatism.jp/";
            url.Margin = new Thickness(ringCanvas.ActualWidth - 105, ringStack.ActualHeight - 30, 0, 0);

            ringCanvas.Children.Add(copy);
            ringCanvas.Children.Add(url);

        }

        // 円レンダリング
        private void circleRender()
        {
            // 獣帯外側
            Ellipse outerEllipse = new Ellipse()
            {
                StrokeThickness = 3,
                Margin = new Thickness(15, 15, 15, 15),
                Stroke = System.Windows.SystemColors.WindowTextBrush
            };
            if (ringCanvas.ActualWidth > ringStack.ActualHeight)
            {
                // 横長(Heightのほうが短い)
                outerEllipse.Width = ringStack.ActualHeight - 30;
                outerEllipse.Height = ringStack.ActualHeight - 30;

            }
            else
            {
                // 縦長(Widthのほうが短い)
                outerEllipse.Width = ringStack.ActualWidth - 30;
                outerEllipse.Height = ringStack.ActualWidth - 30;
            }
            ringCanvas.Children.Add(outerEllipse);

            // 獣帯内側
            Ellipse innerEllipse = new Ellipse()
            {
                StrokeThickness = 3,
                Margin = new Thickness(45, 45, 45, 45),
                Stroke = System.Windows.SystemColors.WindowTextBrush
            };
            if (ringCanvas.ActualWidth > ringStack.ActualHeight)
            {
                // 横長(Heightのほうが短い)
                innerEllipse.Width = ringStack.ActualHeight - 90;
                innerEllipse.Height = ringStack.ActualHeight - 90;

            }
            else
            {
                // 縦長(Widthのほうが短い)
                innerEllipse.Width = ringCanvas.ActualWidth - 90;
                innerEllipse.Height = ringCanvas.ActualWidth - 90;
            }
            ringCanvas.Children.Add(innerEllipse);

            // 中心
            int marginSize;
            if (ringCanvas.ActualWidth > ringStack.ActualHeight)
            {
                // 横長
                marginSize = (int)((ringStack.ActualHeight - tempSettings.zodiacCenter) / 2);
            }
            else
            {
                // 縦長
                marginSize = (int)((ringCanvas.ActualWidth - tempSettings.zodiacCenter) / 2);
            }
            Ellipse centerEllipse = new Ellipse()
            {
                StrokeThickness = 3,
                Stroke = System.Windows.SystemColors.WindowTextBrush,
                Width = tempSettings.zodiacCenter,
                Height = tempSettings.zodiacCenter
            };
            centerEllipse.Margin = new Thickness(marginSize, marginSize, marginSize, marginSize);
            ringCanvas.Children.Add(centerEllipse);

            // 二重円
            if (tempSettings.bands == 2)
            {
                int margin2Size;
                Ellipse ring2Ellipse = new Ellipse()
                {
                    StrokeThickness = 1,
                    Stroke = System.Windows.Media.Brushes.Gray,
                };
                if (ringCanvas.ActualWidth > ringStack.ActualHeight)
                {
                    // 横長
                    margin2Size = (int)(ringStack.ActualHeight - 90 - tempSettings.zodiacCenter) / 4 + 45;
                    ring2Ellipse.Width = (int)(ringStack.ActualHeight + tempSettings.zodiacCenter - 90) / 2;
                    ring2Ellipse.Height = (int)(ringStack.ActualHeight + tempSettings.zodiacCenter - 90) / 2;
                }
                else
                {
                    // 縦長
                    margin2Size = (int)(ringStack.ActualWidth - 90 - tempSettings.zodiacCenter) / 4 + 45;
                    ring2Ellipse.Width = (int)(ringStack.ActualWidth + tempSettings.zodiacCenter - 90) / 2;
                    ring2Ellipse.Height = (int)(ringStack.ActualWidth + tempSettings.zodiacCenter - 90) / 2;
                }
                ring2Ellipse.Margin = new Thickness(margin2Size, margin2Size, margin2Size, margin2Size);
                ringCanvas.Children.Add(ring2Ellipse);
            }

            // 三重円
            if (tempSettings.bands == 3)
            {
                int margin2Size;
                Ellipse ring2Ellipse = new Ellipse()
                {
                    StrokeThickness = 1,
                    Stroke = System.Windows.Media.Brushes.Gray,
                };
                if (ringCanvas.ActualWidth > ringStack.ActualHeight)
                {
                    // 横長
                    margin2Size = (int)(ringStack.ActualHeight - 90 - tempSettings.zodiacCenter) / 3 + 45;
                    ring2Ellipse.Width = (int)(ringStack.ActualHeight - 90 - tempSettings.zodiacCenter) / 3 + tempSettings.zodiacCenter;
                    ring2Ellipse.Height = (int)(ringStack.ActualHeight - 90 - tempSettings.zodiacCenter) / 3 + tempSettings.zodiacCenter;
                }
                else
                {
                    // 縦長
                    margin2Size = (int)(ringStack.ActualWidth - 90 - tempSettings.zodiacCenter) / 3 + 45;
                    ring2Ellipse.Width = (int)(ringStack.ActualWidth - 90 - tempSettings.zodiacCenter) / 3 + tempSettings.zodiacCenter;
                    ring2Ellipse.Height = (int)(ringStack.ActualWidth - 90 - tempSettings.zodiacCenter) / 3 + tempSettings.zodiacCenter;
                }
                ring2Ellipse.Margin = new Thickness(margin2Size, margin2Size, margin2Size, margin2Size);
                ringCanvas.Children.Add(ring2Ellipse);

                int margin3Size;
                Ellipse ring3Ellipse = new Ellipse()
                {
                    StrokeThickness = 1,
                    Stroke = System.Windows.Media.Brushes.Gray,
                };
                if (ringCanvas.ActualWidth > ringStack.ActualHeight)
                {
                    // 横長
                    margin3Size = (int)(ringStack.ActualHeight - 90 - tempSettings.zodiacCenter) / 6 + 45;
                    ring3Ellipse.Width = (int)(ringStack.ActualHeight - 90 - tempSettings.zodiacCenter) * 2 / 3 + tempSettings.zodiacCenter;
                    ring3Ellipse.Height = (int)(ringStack.ActualHeight - 90 - tempSettings.zodiacCenter) * 2 / 3 + tempSettings.zodiacCenter;
                }
                else
                {
                    // 縦長
                    margin3Size = (int)(ringStack.ActualWidth - 90 - tempSettings.zodiacCenter) / 6 + 45;
                    ring3Ellipse.Width = (int)(ringStack.ActualWidth - 90 - tempSettings.zodiacCenter) * 2 / 3 + tempSettings.zodiacCenter;
                    ring3Ellipse.Height = (int)(ringStack.ActualWidth - 90 - tempSettings.zodiacCenter) * 2 / 3 + tempSettings.zodiacCenter;
                }
                ring3Ellipse.Margin = new Thickness(margin3Size, margin3Size, margin3Size, margin3Size);
                ringCanvas.Children.Add(ring3Ellipse);

            }

            // 四重円
            if (tempSettings.bands == 4)
            {
                int margin2Size;
                Ellipse ring2Ellipse = new Ellipse()
                {
                    StrokeThickness = 1,
                    Stroke = System.Windows.Media.Brushes.Gray,
                };
                if (ringCanvas.ActualWidth > ringStack.ActualHeight)
                {
                    // 横長
                    margin2Size = (int)(ringStack.ActualHeight - 90 - tempSettings.zodiacCenter) * 3 / 8 + 45;
                    ring2Ellipse.Width = (int)(ringStack.ActualHeight - 90 - tempSettings.zodiacCenter) / 4 + tempSettings.zodiacCenter;
                    ring2Ellipse.Height = (int)(ringStack.ActualHeight - 90 - tempSettings.zodiacCenter) / 4 + tempSettings.zodiacCenter;
                }
                else
                {
                    // 縦長
                    margin2Size = (int)(ringStack.ActualWidth - 90 - tempSettings.zodiacCenter) * 3 / 8 + 45;
                    ring2Ellipse.Width = (int)(ringStack.ActualWidth - 90 - tempSettings.zodiacCenter) / 4 + tempSettings.zodiacCenter;
                    ring2Ellipse.Height = (int)(ringStack.ActualWidth - 90 - tempSettings.zodiacCenter) / 4 + tempSettings.zodiacCenter;
                }
                ring2Ellipse.Margin = new Thickness(margin2Size, margin2Size, margin2Size, margin2Size);
                ringCanvas.Children.Add(ring2Ellipse);

                int margin3Size;
                Ellipse ring3Ellipse = new Ellipse()
                {
                    StrokeThickness = 1,
                    Stroke = System.Windows.Media.Brushes.Gray,
                };
                if (ringCanvas.ActualWidth > ringStack.ActualHeight)
                {
                    // 横長
                    margin3Size = (int)(ringStack.ActualHeight - 90 - tempSettings.zodiacCenter) / 4 + 45;
                    ring3Ellipse.Width = (int)(ringStack.ActualHeight - 90 - tempSettings.zodiacCenter) / 2 + tempSettings.zodiacCenter;
                    ring3Ellipse.Height = (int)(ringStack.ActualHeight - 90 - tempSettings.zodiacCenter) / 2 + tempSettings.zodiacCenter;
                }
                else
                {
                    // 縦長
                    margin3Size = (int)(ringStack.ActualWidth - 90 - tempSettings.zodiacCenter) / 4 + 45;
                    ring3Ellipse.Width = (int)(ringStack.ActualWidth - 90 - tempSettings.zodiacCenter) / 2 + tempSettings.zodiacCenter;
                    ring3Ellipse.Height = (int)(ringStack.ActualWidth - 90 - tempSettings.zodiacCenter) / 2 + tempSettings.zodiacCenter;
                }
                ring3Ellipse.Margin = new Thickness(margin3Size, margin3Size, margin3Size, margin3Size);
                ringCanvas.Children.Add(ring3Ellipse);

                int margin4Size;
                Ellipse ring4Ellipse = new Ellipse()
                {
                    StrokeThickness = 1,
                    Stroke = System.Windows.Media.Brushes.Gray,
                };
                if (ringCanvas.ActualWidth > ringStack.ActualHeight)
                {
                    // 横長
                    margin4Size = (int)(ringStack.ActualHeight - 90 - tempSettings.zodiacCenter) / 8 + 45;
                    ring4Ellipse.Width = (int)(ringStack.ActualHeight - 90 - tempSettings.zodiacCenter) * 3 / 4 + tempSettings.zodiacCenter;
                    ring4Ellipse.Height = (int)(ringStack.ActualHeight - 90 - tempSettings.zodiacCenter) * 3 / 4 + tempSettings.zodiacCenter;
                }
                else
                {
                    // 縦長
                    margin4Size = (int)(ringStack.ActualWidth - 90 - tempSettings.zodiacCenter) / 8 + 45;
                    ring4Ellipse.Width = (int)(ringStack.ActualWidth - 90 - tempSettings.zodiacCenter) * 3 / 4 + tempSettings.zodiacCenter;
                    ring4Ellipse.Height = (int)(ringStack.ActualWidth - 90 - tempSettings.zodiacCenter) * 3 / 4 + tempSettings.zodiacCenter;
                }
                ring4Ellipse.Margin = new Thickness(margin4Size, margin4Size, margin4Size, margin4Size);
                ringCanvas.Children.Add(ring4Ellipse);
            }

            // 五重円
            if (tempSettings.bands == 5)
            {
                int margin2Size;
                Ellipse ring2Ellipse = new Ellipse()
                {
                    StrokeThickness = 1,
                    Stroke = System.Windows.Media.Brushes.Gray,
                };
                if (ringCanvas.ActualWidth > ringStack.ActualHeight)
                {
                    // 横長
                    margin2Size = (int)(ringStack.ActualHeight - 90 - tempSettings.zodiacCenter) * 2 / 5 + 45;
                    ring2Ellipse.Width = (int)(ringStack.ActualHeight - 90 - tempSettings.zodiacCenter) / 5 + tempSettings.zodiacCenter;
                    ring2Ellipse.Height = (int)(ringStack.ActualHeight - 90 - tempSettings.zodiacCenter) / 5 + tempSettings.zodiacCenter;
                }
                else
                {
                    // 縦長
                    margin2Size = (int)(ringStack.ActualWidth - 90 - tempSettings.zodiacCenter) * 2 / 5 + 45;
                    ring2Ellipse.Width = (int)(ringStack.ActualWidth - 90 - tempSettings.zodiacCenter) / 5 + tempSettings.zodiacCenter;
                    ring2Ellipse.Height = (int)(ringStack.ActualWidth - 90 - tempSettings.zodiacCenter) / 5 + tempSettings.zodiacCenter;
                }
                ring2Ellipse.Margin = new Thickness(margin2Size, margin2Size, margin2Size, margin2Size);
                ringCanvas.Children.Add(ring2Ellipse);

                int margin3Size;
                Ellipse ring3Ellipse = new Ellipse()
                {
                    StrokeThickness = 1,
                    Stroke = System.Windows.Media.Brushes.Gray,
                };
                if (ringCanvas.ActualWidth > ringStack.ActualHeight)
                {
                    // 横長
                    margin3Size = (int)(ringStack.ActualHeight - 90 - tempSettings.zodiacCenter) * 3 / 10 + 45;
                    ring3Ellipse.Width = (int)(ringStack.ActualHeight - 90 - tempSettings.zodiacCenter) * 2 / 5 + tempSettings.zodiacCenter;
                    ring3Ellipse.Height = (int)(ringStack.ActualHeight - 90 - tempSettings.zodiacCenter) * 2 / 5 + tempSettings.zodiacCenter;
                }
                else
                {
                    // 縦長
                    margin3Size = (int)(ringStack.ActualWidth - 90 - tempSettings.zodiacCenter) * 3 / 10 + 45;
                    ring3Ellipse.Width = (int)(ringStack.ActualWidth - 90 - tempSettings.zodiacCenter) * 2 / 5 + tempSettings.zodiacCenter;
                    ring3Ellipse.Height = (int)(ringStack.ActualWidth - 90 - tempSettings.zodiacCenter) * 2 / 5 + tempSettings.zodiacCenter;
                }
                ring3Ellipse.Margin = new Thickness(margin3Size, margin3Size, margin3Size, margin3Size);
                ringCanvas.Children.Add(ring3Ellipse);

                int margin4Size;
                Ellipse ring4Ellipse = new Ellipse()
                {
                    StrokeThickness = 1,
                    Stroke = System.Windows.Media.Brushes.Gray,
                };
                if (ringCanvas.ActualWidth > ringStack.ActualHeight)
                {
                    // 横長
                    margin4Size = (int)(ringStack.ActualHeight - 90 - tempSettings.zodiacCenter) * 1 / 5 + 45;
                    ring4Ellipse.Width = (int)(ringStack.ActualHeight - 90 - tempSettings.zodiacCenter) * 3 / 5 + tempSettings.zodiacCenter;
                    ring4Ellipse.Height = (int)(ringStack.ActualHeight - 90 - tempSettings.zodiacCenter) * 3 / 5 + tempSettings.zodiacCenter;
                }
                else
                {
                    // 縦長
                    margin4Size = (int)(ringStack.ActualWidth - 90 - tempSettings.zodiacCenter) * 1 / 5 + 45;
                    ring4Ellipse.Width = (int)(ringStack.ActualWidth - 90 - tempSettings.zodiacCenter) * 3 / 5 + tempSettings.zodiacCenter;
                    ring4Ellipse.Height = (int)(ringStack.ActualWidth - 90 - tempSettings.zodiacCenter) * 3 / 5 + tempSettings.zodiacCenter;
                }
                ring4Ellipse.Margin = new Thickness(margin4Size, margin4Size, margin4Size, margin4Size);
                ringCanvas.Children.Add(ring4Ellipse);

                int margin5Size;
                Ellipse ring5Ellipse = new Ellipse()
                {
                    StrokeThickness = 1,
                    Stroke = System.Windows.Media.Brushes.Gray,
                };
                if (ringCanvas.ActualWidth > ringStack.ActualHeight)
                {
                    // 横長
                    margin5Size = (int)(ringStack.ActualHeight - 90 - tempSettings.zodiacCenter) * 1 / 10 + 45;
                    ring5Ellipse.Width = (int)(ringStack.ActualHeight - 90 - tempSettings.zodiacCenter) * 4 / 5 + tempSettings.zodiacCenter;
                    ring5Ellipse.Height = (int)(ringStack.ActualHeight - 90 - tempSettings.zodiacCenter) * 4 / 5 + tempSettings.zodiacCenter;
                }
                else
                {
                    // 縦長
                    margin5Size = (int)(ringStack.ActualWidth - 90 - tempSettings.zodiacCenter) * 1 / 10 + 45;
                    ring5Ellipse.Width = (int)(ringStack.ActualWidth - 90 - tempSettings.zodiacCenter) * 4 / 5 + tempSettings.zodiacCenter;
                    ring5Ellipse.Height = (int)(ringStack.ActualWidth - 90 - tempSettings.zodiacCenter) * 4 / 5 + tempSettings.zodiacCenter;
                }
                ring5Ellipse.Margin = new Thickness(margin5Size, margin5Size, margin5Size, margin5Size);
                ringCanvas.Children.Add(ring5Ellipse);
            }
        }
        // a = (x1 - x2) / 2
        // b = (x2 + a) / 2 = (x2 + (x1 - x2) / 2 ) / 2
        // = (x2 /2 + x1 / 2) / 2

        // ハウスカスプレンダリング
        private void houseCuspRender(double[] natalcusp,
            double[] cusp2,
            double[] cusp3,
            double[] cusp4,
            double[] cusp5)
        {
            //内側がstart, 外側がend
            double startX = tempSettings.zodiacCenter / 2;
            double endX;
            if (ringStack.ActualHeight < ringStack.ActualWidth)
            {
                endX = (ringStack.ActualHeight - 90) / 2;
            }
            else
            {
                endX = (ringCanvas.ActualWidth - 90) / 2;
            }

            double startY = 0;
            double endY = 0;
            List<PointF[]> pList = new List<PointF[]>();
            List<PointF[]> pListSecond = new List<PointF[]>();
            List<PointF[]> pListThird = new List<PointF[]>();

            // 最適化と五重円は後で
            if (tempSettings.bands == 1)
            {
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
            }
            else if (tempSettings.bands == 2)
            {
                // end座標が変わる
                Enumerable.Range(1, 12).ToList().ForEach(i =>
                {
                    double degree = natalcusp[i] - natalcusp[1];

                    PointF newStart = rotate(startX, startY, degree);
                    newStart.X += (float)rcanvas.outerWidth / 2;
                    // Formの座標は下がプラス、数学では上がマイナス
                    newStart.Y = newStart.Y * -1;
                    newStart.Y += (float)rcanvas.outerHeight / 2;

                    endX = (rcanvas.outerWidth - 90) / 2;
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
                // start座標が変わる
                Enumerable.Range(1, 12).ToList().ForEach(i =>
                {
                    double degree = cusp2[i] - natalcusp[1];

                    startX = (rcanvas.outerWidth - 90) / 2;
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
                    pListSecond.Add(pointList);

                });
            }
            if (tempSettings.bands == 3)
            {
                // end座標が変わる
                Enumerable.Range(1, 12).ToList().ForEach(i =>
                {
                    double degree = natalcusp[i] - natalcusp[1];

                    PointF newStart = rotate(startX, startY, degree);
                    newStart.X += (float)rcanvas.outerWidth / 2;
                    // Formの座標は下がプラス、数学では上がマイナス
                    newStart.Y = newStart.Y * -1;
                    newStart.Y += (float)rcanvas.outerHeight / 2;

                    endX = (rcanvas.outerWidth + 2 * tempSettings.zodiacCenter - 90) / 6;
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
                // start座標、end座標が変わる
                Enumerable.Range(1, 12).ToList().ForEach(i =>
                {
                    double degree = cusp2[i] - natalcusp[1];

                    startX = (rcanvas.outerWidth + 2 * tempSettings.zodiacCenter - 90) / 6;
                    PointF newStart = rotate(startX, startY, degree);
                    newStart.X += (float)rcanvas.outerWidth / 2;
                    // Formの座標は下がプラス、数学では上がマイナス
                    newStart.Y = newStart.Y * -1;
                    newStart.Y += (float)rcanvas.outerHeight / 2;

                    endX = (2 * rcanvas.outerWidth + tempSettings.zodiacCenter - 180) / 6;
                    PointF newEnd = rotate(endX, endY, degree);
                    newEnd.X += (float)rcanvas.outerWidth / 2;
                    // Formの座標は下がプラス、数学では上がマイナス
                    newEnd.Y = newEnd.Y * -1;
                    newEnd.Y += (float)rcanvas.outerHeight / 2;

                    PointF[] pointList = new PointF[2];
                    pointList[0] = newStart;
                    pointList[1] = newEnd;
                    pListSecond.Add(pointList);

                });
                Enumerable.Range(1, 12).ToList().ForEach(i =>
                {
                    double degree = cusp3[i] - natalcusp[1];

                    startX = (2 * rcanvas.outerWidth + tempSettings.zodiacCenter - 180) / 6;
                    PointF newStart = rotate(startX, startY, degree);
                    newStart.X += (float)rcanvas.outerWidth / 2;
                    // Formの座標は下がプラス、数学では上がマイナス
                    newStart.Y = newStart.Y * -1;
                    newStart.Y += (float)rcanvas.outerHeight / 2;

                    endX = (ringCanvas.ActualWidth - 90) / 2;
                    PointF newEnd = rotate(endX, endY, degree);
                    newEnd.X += (float)rcanvas.outerWidth / 2;
                    // Formの座標は下がプラス、数学では上がマイナス
                    newEnd.Y = newEnd.Y * -1;
                    newEnd.Y += (float)rcanvas.outerHeight / 2;

                    PointF[] pointList = new PointF[2];
                    pointList[0] = newStart;
                    pointList[1] = newEnd;
                    pListThird.Add(pointList);

                });
            }

            Enumerable.Range(0, 12).ToList().ForEach(i =>
            {
                Line l = new Line();
                l.X1 = pList[i][0].X;
                l.Y1 = pList[i][0].Y;
                l.X2 = pList[i][1].X;
                l.Y2 = pList[i][1].Y;
                if (i % 3 == 0)
                {
                    l.Stroke = System.Windows.Media.Brushes.Gray;
                }
                else
                {
                    l.Stroke = System.Windows.Media.Brushes.LightGray;
                    l.StrokeDashArray = new DoubleCollection();
                    l.StrokeDashArray.Add(4.0);
                    l.StrokeDashArray.Add(4.0);
                }
                l.StrokeThickness = 2.0;
                l.Tag = new Explanation()
                {
                    before = (i + 1).ToString() + "ハウス　",
                    sign = CommonData.getSignTextJp(natalcusp[i + 1]),
                    degree = DecimalToHex((natalcusp[i + 1] % 30).ToString())
                };
                l.MouseEnter += houseCuspMouseEnter;
                if (i == 0)
                {
                    l.ToolTip = "ASC " + CommonData.getSignTextJp(natalcusp[i + 1]) + (natalcusp[i + 1] % 30).ToString("0") + "度";
                }
                else if (i == 3)
                {
                    l.ToolTip = "IC " + CommonData.getSignTextJp(natalcusp[i + 1]) + (natalcusp[i + 1] % 30).ToString("0") + "度";
                }
                else if (i == 6)
                {
                    l.ToolTip = "DSC " + CommonData.getSignTextJp(natalcusp[i + 1]) + (natalcusp[i + 1] % 30).ToString("0") + "度";
                }
                else if (i == 9)
                {
                    l.ToolTip = "MC " + CommonData.getSignTextJp(natalcusp[i + 1]) + (natalcusp[i + 1] % 30).ToString("0") + "度";
                }
                else
                {
                    l.ToolTip = (i + 1).ToString() + "ハウスカスプ " + CommonData.getSignTextJp(natalcusp[i + 1]) + (natalcusp[i + 1] % 30).ToString("0") + "度";
                }
                ringCanvas.Children.Add(l);
            });
            if (tempSettings.bands >= 2)
            {
                Enumerable.Range(0, 12).ToList().ForEach(i =>
                {
                    Line l = new Line();
                    l.X1 = pListSecond[i][0].X;
                    l.Y1 = pListSecond[i][0].Y;
                    l.X2 = pListSecond[i][1].X;
                    l.Y2 = pListSecond[i][1].Y;
                    if (i % 3 == 0)
                    {
                        l.Stroke = System.Windows.Media.Brushes.Gray;
                    }
                    else
                    {
                        l.Stroke = System.Windows.Media.Brushes.LightGray;
                        l.StrokeDashArray = new DoubleCollection();
                        l.StrokeDashArray.Add(4.0);
                        l.StrokeDashArray.Add(4.0);
                    }
                    l.StrokeThickness = 2.0;
                    l.Tag = new Explanation()
                    {
                        before = (i + 1).ToString() + "ハウス　",
                        sign = CommonData.getSignTextJp(natalcusp[i + 1]),
                        degree = DecimalToHex((natalcusp[i + 1] % 30).ToString())
                    };
                    l.MouseEnter += houseCuspMouseEnter;
                    if (i == 0)
                    {
                        l.ToolTip = "ASC " + CommonData.getSignTextJp(natalcusp[i + 1]) + (natalcusp[i + 1] % 30).ToString("0") + "度";
                    }
                    else if (i == 3)
                    {
                        l.ToolTip = "IC " + CommonData.getSignTextJp(natalcusp[i + 1]) + (natalcusp[i + 1] % 30).ToString("0") + "度";
                    }
                    else if (i == 6)
                    {
                        l.ToolTip = "DSC " + CommonData.getSignTextJp(natalcusp[i + 1]) + (natalcusp[i + 1] % 30).ToString("0") + "度";
                    }
                    else if (i == 9)
                    {
                        l.ToolTip = "MC " + CommonData.getSignTextJp(natalcusp[i + 1]) + (natalcusp[i + 1] % 30).ToString("0") + "度";
                    }
                    else
                    {
                        l.ToolTip = (i + 1).ToString() + "ハウスカスプ " + CommonData.getSignTextJp(natalcusp[i + 1]) + (natalcusp[i + 1] % 30).ToString("0") + "度";
                    }
                    ringCanvas.Children.Add(l);
                });
            }
            if (tempSettings.bands >= 3)
            {
                Enumerable.Range(0, 12).ToList().ForEach(i =>
                {
                    Line l = new Line();
                    l.X1 = pListThird[i][0].X;
                    l.Y1 = pListThird[i][0].Y;
                    l.X2 = pListThird[i][1].X;
                    l.Y2 = pListThird[i][1].Y;
                    if (i % 3 == 0)
                    {
                        l.Stroke = System.Windows.Media.Brushes.Gray;
                    }
                    else
                    {
                        l.Stroke = System.Windows.Media.Brushes.LightGray;
                        l.StrokeDashArray = new DoubleCollection();
                        l.StrokeDashArray.Add(4.0);
                        l.StrokeDashArray.Add(4.0);
                    }
                    l.StrokeThickness = 2.0;
                    l.Tag = new Explanation()
                    {
                        before = (i + 1).ToString() + "ハウス　",
                        sign = CommonData.getSignTextJp(natalcusp[i + 1]),
                        degree = DecimalToHex((natalcusp[i + 1] % 30).ToString())
                    };
                    l.MouseEnter += houseCuspMouseEnter;
                    if (i == 0)
                    {
                        l.ToolTip = "ASC " + CommonData.getSignTextJp(natalcusp[i + 1]) + (natalcusp[i + 1] % 30).ToString("0") + "度";
                    }
                    else if (i == 3)
                    {
                        l.ToolTip = "IC " + CommonData.getSignTextJp(natalcusp[i + 1]) + (natalcusp[i + 1] % 30).ToString("0") + "度";
                    }
                    else if (i == 6)
                    {
                        l.ToolTip = "DSC " + CommonData.getSignTextJp(natalcusp[i + 1]) + (natalcusp[i + 1] % 30).ToString("0") + "度";
                    }
                    else if (i == 9)
                    {
                        l.ToolTip = "MC " + CommonData.getSignTextJp(natalcusp[i + 1]) + (natalcusp[i + 1] % 30).ToString("0") + "度";
                    }
                    else
                    {
                        l.ToolTip = (i + 1).ToString() + "ハウスカスプ " + CommonData.getSignTextJp(natalcusp[i + 1]) + (natalcusp[i + 1] % 30).ToString("0") + "度";
                    }
                    ringCanvas.Children.Add(l);
                });
            }
        }

        // サインカスプレンダリング
        private void signCuspRender(double startdegree)
        {
            // 内側がstart、外側がend
            // margin + thickness * 3だけ実際のリングの幅と差がある
            double startX = (rcanvas.innerWidth - 29) / 2;
            double endX = (rcanvas.outerWidth - 29)/ 2;

            double startY = 0;
            double endY = 0;
            List<PointF[]> pList = new List<PointF[]>();
            
            Enumerable.Range(1, 12).ToList().ForEach(i =>
            {
                double degree = (30.0 * i) - startdegree;

                PointF newStart = rotate(startX, startY, degree);
                newStart.X += (float)(rcanvas.outerWidth) / 2;
                // Formの座標は下がプラス、数学では上がマイナス
                newStart.Y = newStart.Y * -1;
                newStart.Y += (float)(rcanvas.outerHeight) / 2;

                PointF newEnd = rotate(endX, endY, degree);
                newEnd.X += (float)(rcanvas.outerWidth) / 2;
                // Formの座標は下がプラス、数学では上がマイナス
                newEnd.Y = newEnd.Y * -1;
                newEnd.Y += (float)(rcanvas.outerHeight) / 2;

                PointF[] pointList = new PointF[2];
                pointList[0] = newStart;
                pointList[1] = newEnd;
                pList.Add(pointList);
            });

            Enumerable.Range(0, 12).ToList().ForEach(i =>
            {
                Line l = new Line();
                l.X1 = pList[i][0].X;
                l.Y1 = pList[i][0].Y;
                l.X2 = pList[i][1].X;
                l.Y2 = pList[i][1].Y;
                l.Stroke = System.Windows.Media.Brushes.Black;
                l.StrokeThickness = 1.0;
                ringCanvas.Children.Add(l);
            });
        }

        // zodiac文字列描画
        // 獣帯に乗る文字ね
        private void zodiacRender(double startdegree)
        {
            List<PointF> pList = new List<PointF>();
            Enumerable.Range(0, 12).ToList().ForEach(i =>
            {
                PointF point = rotate(rcanvas.outerWidth / 2 - 33, 0, (30 * (i + 1)) - startdegree - 15.0);
                point.X += (float)rcanvas.outerWidth / 2 - 10;
//                point.X -= (float)rcanvas.outerWidth - (float)rcanvas.innerWidth;
                point.Y *= -1;
                point.Y += (float)rcanvas.outerHeight / 2 - 12;
//                point.Y -= (float)rcanvas.outerHeight - (float)rcanvas.innerHeight;
                pList.Add(point);
            });

            Enumerable.Range(0, 12).ToList().ForEach(i =>
            {
                Label zodiacLabel = new Label();
                zodiacLabel.Content = CommonData.getSignSymbol(i);
                zodiacLabel.Margin = new Thickness(pList[i].X, pList[i].Y, 0, 0);
                zodiacLabel.Foreground = CommonData.getSignColor(i * 30);
                zodiacLabel.ToolTip = CommonData.getSignTextJp(i * 30);
                ringCanvas.Children.Add(zodiacLabel);
            });
        }

        // 天体から中心円への線
        private void planetLine(double startdegree, 
            List<PlanetData> list1,
            List<PlanetData> list2,
            List<PlanetData> list3,
            List<PlanetData> list4,
            List<PlanetData> list5
            )
        {
            List<bool> dispList = new List<bool>();
            List<PlanetDisplay> pDisplayList = new List<PlanetDisplay>();

            if (tempSettings.centerPattern == 1)
            {
                return;
            }

            if (tempSettings.bands == 1)
            {
                int[] box = new int[72];
                list1.ForEach(planet =>
                {
                    if (planet.isDisp == false)
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
                    // 重ならないようにずらしを入れる
                    // 1サインに6度単位5個までデータが入る
                    int index = (int)(planet.absolute_position / 5);
                    if (box[index] == 1)
                    {
                        while (box[index] == 1)
                        {
                            index++;
                            if (index == 72)
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

                    PointF pointPlanet;
                    PointF pointRing;
                    pointPlanet = rotate(rcanvas.outerWidth / 3 - 65, 0, 5 * index - startdegree + 3);
                    pointRing = rotate(tempSettings.zodiacCenter / 2, 0, planet.absolute_position - startdegree);

                    pointPlanet.X += (float)(rcanvas.outerWidth / 2);
                    pointPlanet.Y *= -1;
                    pointPlanet.Y += (float)(rcanvas.outerHeight / 2);
                    pointRing.X += (float)(rcanvas.outerWidth / 2);
                    pointRing.Y *= -1;
                    pointRing.Y += (float)(rcanvas.outerHeight / 2);

                    Line l = new Line();
                    l.X1 = pointPlanet.X;
                    l.Y1 = pointPlanet.Y;
                    l.X2 = pointRing.X;
                    l.Y2 = pointRing.Y;
                    l.Stroke = System.Windows.Media.Brushes.Gray;
                    l.StrokeThickness = 1.0;
                    ringCanvas.Children.Add(l);
                });
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
            // 最終的に
            ringCanvas.Children.Clear();
        }

        // 天体表示
        public void SetSign(PlanetDisplay displayData)
        {
            Label txtLbl = new Label();
            txtLbl.Content = displayData.planetTxt;
            txtLbl.Margin = new Thickness(displayData.planetPt.X, displayData.planetPt.Y, 0, 0);
            txtLbl.Foreground = displayData.planetColor;
            txtLbl.Tag = displayData.explanation;
            txtLbl.FontSize = 16;
            txtLbl.MouseEnter += planetMouseEnter;
            ringCanvas.Children.Add(txtLbl);

            Label degreeLbl = new Label();
            degreeLbl.Content = displayData.degreeTxt;
            degreeLbl.Margin = new Thickness(displayData.degreePt.X, displayData.degreePt.Y, 0, 0);
            degreeLbl.Tag = displayData.explanation;
            degreeLbl.MouseEnter += planetMouseEnter;
            ringCanvas.Children.Add(degreeLbl);

            Label signLbl = new Label();
            signLbl.Content = displayData.symbolTxt;
            signLbl.Margin = new Thickness(displayData.symbolPt.X, displayData.symbolPt.Y, 0, 0);
            signLbl.Foreground = displayData.symbolColor;
            signLbl.Tag = displayData.explanation;
            signLbl.MouseEnter += planetMouseEnter;
            ringCanvas.Children.Add(signLbl);

            Label minuteLbl = new Label();
            minuteLbl.Content = displayData.minuteTxt;
            minuteLbl.Margin = new Thickness(displayData.minutePt.X, displayData.minutePt.Y, 0, 0);
            minuteLbl.Tag = displayData.explanation;
            minuteLbl.MouseEnter += planetMouseEnter;
            ringCanvas.Children.Add(minuteLbl);

            Label retrogradeLbel = new Label();
            retrogradeLbel.Content = displayData.retrogradeTxt;
            retrogradeLbel.Margin = new Thickness(displayData.retrogradePt.X, displayData.retrogradePt.Y, 0, 0);
            retrogradeLbel.Tag = displayData.explanation;
            retrogradeLbel.MouseEnter += planetMouseEnter;
            ringCanvas.Children.Add(retrogradeLbel);
        }

        // 天体表示
        // 天体だけ
        public void SetOnlySign(PlanetDisplay displayData)
        {
            Label txtLbl = new Label();
            txtLbl.Content = displayData.planetTxt;
            txtLbl.Margin = new Thickness(displayData.planetPt.X, displayData.planetPt.Y, 0, 0);
            txtLbl.Foreground = displayData.planetColor;
            txtLbl.Tag = displayData.explanation;
            txtLbl.FontSize = 16;
            txtLbl.MouseEnter += planetMouseEnter;
            ringCanvas.Children.Add(txtLbl);
        }

        // 天体表示
        // 天体、度数だけ
        public void SetOnlySignDegree(PlanetDisplay displayData)
        {
            Label txtLbl = new Label();
            txtLbl.Content = displayData.planetTxt;
            txtLbl.Margin = new Thickness(displayData.planetPt.X, displayData.planetPt.Y, 0, 0);
            txtLbl.Foreground = displayData.planetColor;
            txtLbl.Tag = displayData.explanation;
            txtLbl.FontSize = 16;
            txtLbl.MouseEnter += planetMouseEnter;
            ringCanvas.Children.Add(txtLbl);

            Label degreeLbl = new Label();
            degreeLbl.Content = displayData.degreeTxt;
            if (displayData.retrogradeTxt == "")
            {
                // todo
            }
            degreeLbl.Margin = new Thickness(displayData.degreePt.X, displayData.degreePt.Y, 0, 0);
            degreeLbl.Tag = displayData.explanation;
            degreeLbl.MouseEnter += planetMouseEnter;
            ringCanvas.Children.Add(degreeLbl);
        }

        // アスペクト表示
        public void aspectsRendering(
                double startDegree, 
                List<PlanetData> list1, 
                List<PlanetData> list2, 
                List<PlanetData> list3,
                List<PlanetData> list4,
                List<PlanetData> list5
            )
        {
            if (currentSetting.dispAspect[0, 0])
            {
                aspectRender(startDegree, list1, 1, 1, 1, 1);
            }
            if (tempSettings.bands == 2)
            {
                if (currentSetting.dispAspect[0, 0])
                {
                    aspectRender(startDegree, list1, 1, 1, 2, 1);
                }
                if (currentSetting.dispAspect[1, 1])
                {
                    aspectRender(startDegree, list2, 2, 2, 2, 1);
                }
                if (currentSetting.dispAspect[0, 1])
                {
                    aspectRender(startDegree, list1, 1, 2, 2, 2);
                }
            }
            if (tempSettings.bands == 3)
            {
                if (currentSetting.dispAspect[0, 0])
                {
                    aspectRender(startDegree, list1, 1, 1, 3, 1);
                }
                if (currentSetting.dispAspect[1, 1])
                {
                    aspectRender(startDegree, list2, 2, 2, 3, 1);
                }
                if (currentSetting.dispAspect[2, 2])
                {
                    aspectRender(startDegree, list3, 3, 3, 3, 1);
                }
                if (currentSetting.dispAspect[0, 1])
                {
                    aspectRender(startDegree, list1, 1, 2, 3, 2);
                }
                if (currentSetting.dispAspect[0, 2])
                {
                    aspectRender(startDegree, list1, 1, 3, 3, 3);
                }
                if (currentSetting.dispAspect[1, 2])
                {
                    aspectRender(startDegree, list2, 2, 3, 3, 3);
                }
            }
            /*
            if (aspectSetting.n_n)
            {
                aspectRender(startDegree, natallist, 1, 1, 1);
            }
            if (aspectSetting.p_p && setting.bands > 1)
            {
                aspectRender(startDegree, progresslist, 2, 2, 1);
            }
            if (aspectSetting.t_t && setting.bands > 2)
            {
                aspectRender(startDegree, transitlist, 3, 3, 1);
            }
            if (aspectSetting.n_p && setting.bands > 1)
            {
                aspectRender(startDegree, natallist, 1, 2, 2);
            }
            if (aspectSetting.n_t && setting.bands > 2)
            {
                aspectRender(startDegree, natallist, 1, 3, 3);
            }
            if (aspectSetting.p_t && setting.bands > 2)
            {
                aspectRender(startDegree, progresslist, 2, 3, 3);
            }
            */

        }

        // startPosition、endPosition : n-pの線は1-2となる
        // aspectKind1 : aspectを使う 2: secondAspectを使う
        private void aspectRender(double startDegree, List<PlanetData> list, 
            int startPosition, int endPosition, int aspectRings,
            int aspectKind)
        {
            if (list == null)
            {
                return;
            }
            double startRingX = tempSettings.zodiacCenter / 2;
            double endRingX = tempSettings.zodiacCenter / 2;
            if (aspectRings == 1)
            {
                // 一重円
                startRingX = tempSettings.zodiacCenter / 2;
                endRingX = tempSettings.zodiacCenter / 2;
            }
            else if (aspectRings == 2)
            {
                // 二重円
                if (startPosition == 1)
                {
                    // 内側
                    startRingX = tempSettings.zodiacCenter / 2;
                }
                else
                {
                    // 外側
                    if (ringCanvas.ActualWidth > ringStack.ActualHeight)
                    {
                        // 横長
                        startRingX = (ringStack.ActualHeight + tempSettings.zodiacCenter - 90) / 4;
                    }
                    else
                    {
                        // 縦長
                        startRingX = (ringStack.ActualWidth + tempSettings.zodiacCenter - 90) / 4;
                    }
                }
                if (endPosition == 1)
                {
                    // 内側
                    endRingX = tempSettings.zodiacCenter / 2;
                }
                else
                {
                    // 外側
                    if (ringCanvas.ActualWidth > ringStack.ActualHeight)
                    {
                        // 横長
                        endRingX = (ringStack.ActualHeight + tempSettings.zodiacCenter - 90) / 4;
                    }
                    else
                    {
                        // 縦長
                        endRingX = (ringStack.ActualWidth + tempSettings.zodiacCenter - 90) / 4;
                    }
                }
            }
            else if (aspectRings == 3)
            {
                // 三重円
                if (startPosition == 1)
                {
                    // 1
                    startRingX = tempSettings.zodiacCenter / 2;
                }
                else if (startPosition == 2)
                {
                    // 2
                    if (ringCanvas.ActualWidth > ringStack.ActualHeight)
                    {
                        // 横長
                        startRingX = (ringStack.ActualHeight + 2 * tempSettings.zodiacCenter - 90) / 6;
                    }
                    else
                    {
                        // 縦長
                        startRingX = (ringStack.ActualWidth + 2 * tempSettings.zodiacCenter - 90) / 6;
                    }
                }
                else
                {
                    // 3
                    if (ringCanvas.ActualWidth > ringStack.ActualHeight)
                    {
                        // 横長
                        startRingX = (2 * ringStack.ActualHeight + tempSettings.zodiacCenter - 180) / 6;
                    }
                    else
                    {
                        // 縦長
                        startRingX = (2 * ringStack.ActualWidth + tempSettings.zodiacCenter - 180) / 6;
                    }
                }
                if (endPosition == 1)
                {
                    // 1
                    endRingX = tempSettings.zodiacCenter / 2;
                }
                else if (endPosition == 2)
                {
                    // 2
                    if (ringCanvas.ActualWidth > ringStack.ActualHeight)
                    {
                        // 横長
                        endRingX = (ringStack.ActualHeight + 2 * tempSettings.zodiacCenter - 90) / 6;
                    }
                    else
                    {
                        // 縦長
                        endRingX = (ringStack.ActualWidth + 2 * tempSettings.zodiacCenter - 90) / 6;
                    }
                }
                else
                {
                    // 3
                    if (ringCanvas.ActualWidth > ringStack.ActualHeight)
                    {
                        // 横長
                        endRingX = (2 * ringStack.ActualHeight + tempSettings.zodiacCenter - 180) / 6;
                    }
                    else
                    {
                        // 縦長
                        endRingX = (2 * ringStack.ActualWidth + tempSettings.zodiacCenter - 180) / 6;
                    }
                }

            }

            for (int i = 0; i < list.Count; i++)
            {
                if (!list[i].isAspectDisp)
                {
                    // 表示対象外
                    continue;
                }
                PointF startPoint;
                startPoint = rotate(startRingX, 0, list[i].absolute_position - startDegree);
                startPoint.X += (float)((rcanvas.outerWidth) / 2);
                startPoint.Y *= -1;
                startPoint.Y += (float)((rcanvas.outerHeight) / 2);
                if (aspectKind == 1)
                {
                    aspectListRender(startDegree, list, list[i].aspects, startPoint, endRingX);
                }
                else if (aspectKind == 2)
                {
                    aspectListRender(startDegree, list, list[i].secondAspects, startPoint, endRingX);
                }
                else if (aspectKind == 3)
                {
                    aspectListRender(startDegree, list, list[i].thirdAspects, startPoint, endRingX);
                }
            }
        }

        // aspectRender サブ関数
        private void aspectListRender(double startDegree, List<PlanetData> list, List<AspectInfo> aspects, PointF startPoint, double endRingX)
        {
            for (int j = 0; j < aspects.Count; j++)
            {
                int tNo = calc.targetNoList[aspects[j].targetPlanetNo];
                if (!list[tNo].isAspectDisp)
                {
                    continue;
                }
                PointF endPoint;

                endPoint = rotate(endRingX, 0, aspects[j].targetPosition - startDegree);
                endPoint.X += (float)((rcanvas.outerWidth) / 2);
                endPoint.Y *= -1;
                endPoint.Y += (float)((rcanvas.outerHeight) / 2);

                Line aspectLine = new Line()
                {
                    X1 = startPoint.X,
                    Y1 = startPoint.Y,
                    X2 = endPoint.X,
                    Y2 = endPoint.Y
                };
                if (aspects[j].softHard == SoftHard.SOFT)
                {
                    aspectLine.StrokeDashArray = new DoubleCollection();
                    aspectLine.StrokeDashArray.Add(4.0);
                    aspectLine.StrokeDashArray.Add(4.0);
                }
                TextBlock aspectLbl = new TextBlock();
                aspectLbl.Margin = new Thickness(Math.Abs(startPoint.X + endPoint.X) / 2 - 5, Math.Abs(endPoint.Y + startPoint.Y) / 2 - 8, 0, 0);
                if (aspects[j].aspectKind == Aspect.AspectKind.CONJUNCTION)
                {
                    // 描画しない
                }
                else if (aspects[j].aspectKind == Aspect.AspectKind.OPPOSITION)
                {
                    aspectLine.Stroke = System.Windows.Media.Brushes.Red;
                    aspectLbl.Foreground = System.Windows.Media.Brushes.Red;
                    aspectLbl.Text = "☍";
                    aspectLbl.HorizontalAlignment = HorizontalAlignment.Left;
                    aspectLbl.TextAlignment = TextAlignment.Left;
                    aspectLbl.VerticalAlignment = VerticalAlignment.Top;
                }
                else if (aspects[j].aspectKind == Aspect.AspectKind.TRINE)
                {
                    aspectLine.Stroke = System.Windows.Media.Brushes.Orange;
                    aspectLbl.Foreground = System.Windows.Media.Brushes.Orange;
                    aspectLbl.Text = "△";
                    aspectLbl.HorizontalAlignment = HorizontalAlignment.Left;
                    aspectLbl.TextAlignment = TextAlignment.Left;
                    aspectLbl.VerticalAlignment = VerticalAlignment.Top;
                }
                else if (aspects[j].aspectKind == Aspect.AspectKind.SQUARE)
                {
                    aspectLine.Stroke = System.Windows.Media.Brushes.Purple;
                    aspectLbl.Foreground = System.Windows.Media.Brushes.Purple;
                    aspectLbl.Text = "□";
                    aspectLbl.HorizontalAlignment = HorizontalAlignment.Left;
                    aspectLbl.TextAlignment = TextAlignment.Left;
                    aspectLbl.VerticalAlignment = VerticalAlignment.Top;

                }
                else if (aspects[j].aspectKind == Aspect.AspectKind.SEXTILE)
                {
                    aspectLine.Stroke = System.Windows.Media.Brushes.Green;
                    aspectLbl.Foreground = System.Windows.Media.Brushes.Green;
                    aspectLbl.Text = "⚹";
                    aspectLbl.HorizontalAlignment = HorizontalAlignment.Left;
                    aspectLbl.TextAlignment = TextAlignment.Left;
                    aspectLbl.VerticalAlignment = VerticalAlignment.Top;
                }
                else if (aspects[j].aspectKind == Aspect.AspectKind.INCONJUNCT)
                {
                    aspectLine.Stroke = System.Windows.Media.Brushes.Gray;
                    aspectLbl.Foreground = System.Windows.Media.Brushes.Gray;
                    aspectLbl.Text = "⚻";
                    aspectLbl.HorizontalAlignment = HorizontalAlignment.Left;
                    aspectLbl.TextAlignment = TextAlignment.Left;
                    aspectLbl.VerticalAlignment = VerticalAlignment.Top;
                }
                else
                {
                    aspectLine.Stroke = System.Windows.Media.Brushes.Black;
                    aspectLbl.Foreground = System.Windows.Media.Brushes.Black;
                    aspectLbl.Text = "⚼";
                    aspectLbl.HorizontalAlignment = HorizontalAlignment.Left;
                    aspectLbl.TextAlignment = TextAlignment.Left;
                    aspectLbl.VerticalAlignment = VerticalAlignment.Top;
                }
                aspectLine.MouseEnter += new MouseEventHandler(aspectMouseEnter);
                aspectLine.Tag = aspects[j];
                ringCanvas.Children.Add(aspectLine);
                ringCanvas.Children.Add(aspectLbl);

            }

        }

        private void houseCuspMouseEnter(object sender, System.EventArgs e)
        {
            Line l = (Line)sender;
            Explanation data = (Explanation)l.Tag;
            mainWindowVM.explanationTxt = data.before + data.sign + DecimalToHex(data.degree.ToString("0.000")).ToString("0.000") + "\'";
        }

        private void planetMouseEnter(object sender, System.EventArgs e)
        {
            Label l = (Label)sender;
            Explanation data = (Explanation)l.Tag;
            string retro;
            if (data.retrograde)
            {
                retro = "(逆行)";
            }
            else
            {
                retro = "";
            }
            mainWindowVM.explanationTxt = data.planet + " " + data.sign + DecimalToHex(data.degree.ToString("0.000")).ToString("0.000") + "\' " + retro;
        }
        private void aspectMouseEnter(object sender, System.EventArgs e)
        {
            Line l = (Line)sender;
            AspectInfo info = (AspectInfo)l.Tag;
            mainWindowVM.explanationTxt = CommonData.getPlanetText(info.srcPlanetNo) + "-" +
                CommonData.getPlanetText(info.targetPlanetNo) + " " + 
                info.aspectKind.ToString() + " " + info.absoluteDegree.ToString("0.00") + "°";
        }
        public void explanationClear(object sender, System.EventArgs e)
        {
            mainWindowVM.explanationTxt = "";
        }

        public void UsernameRefresh()
        {

        }

        private void OpenDatabase_Click(object sender, RoutedEventArgs e)
        {
            if (dbWindow == null)
            {
                dbWindow = new DatabaseWindow(this);
            }
            dbWindow.Visibility = Visibility.Visible;
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

        public double DecimalToHex(string decimalStr)
        {
            double tmp = double.Parse(decimalStr);
            double ftmp = tmp - (int)tmp;
            ftmp = ftmp / 100 * 60;
            int itmp = (int)tmp;
            return itmp + ftmp;
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
            tempSettings.bands = 1;
            tempSettings.firstHouseDiv = TempSetting.HouseDivide.USER1;
            ReCalc();
            ReRender();
        }

        private void TripleRing_Click(object sender, RoutedEventArgs e)
        {
            tempSettings.bands = 3;
            ReRender();
        }

        private void MultipleRing_Click(object sender, RoutedEventArgs e)
        {
            if (ringWindow == null)
            {
                ringWindow = new CustomRingWindow(this);
            }
            ringWindow.Visibility = Visibility.Visible;
        }

        private void OpenDisplayConfig_Click(object sender, RoutedEventArgs e)
        {
            if (setWindow == null)
            {
                setWindow = new SettingWIndow(this);
            }
            setWindow.Visibility = Visibility.Visible;
        }

        private void ChartSelector_Click(object sender, RoutedEventArgs e)
        {
            if (chartSelecterWindow == null)
            {
                chartSelecterWindow = new ChartSelectorWindow(this);
            }
            chartSelecterWindow.Visibility = Visibility.Visible;

        }

        private void mainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyboardDevice.IsKeyDown(Key.LeftCtrl) || e.KeyboardDevice.IsKeyDown(Key.RightCtrl))
            {

                if (e.Key == Key.A)
                {
                    // MessageBox.Show("Ctrl + A");
                }

                if (e.Key == Key.T)
                {
                    // メニュー
                    if (chartSelecterWindow == null)
                    {
                        chartSelecterWindow = new ChartSelectorWindow(this);
                    }
                    chartSelecterWindow.Visibility = Visibility.Visible;
                }
            }
            if (e.Key == Key.D0)
            {
                dispSettingBox.SelectedIndex = 0;
            }
            if (e.Key == Key.D1)
            {
                dispSettingBox.SelectedIndex = 1;
            }
            if (e.Key == Key.D2)
            {
                dispSettingBox.SelectedIndex = 2;
            }
            if (e.Key == Key.D3)
            {
                dispSettingBox.SelectedIndex = 3;
            }
            if (e.Key == Key.D4)
            {
                dispSettingBox.SelectedIndex = 4;
            }
            if (e.Key == Key.D5)
            {
                dispSettingBox.SelectedIndex = 5;
            }
            if (e.Key == Key.D6)
            {
                dispSettingBox.SelectedIndex = 6;
            }
            if (e.Key == Key.D7)
            {
                dispSettingBox.SelectedIndex = 7;
            }
            if (e.Key == Key.D8)
            {
                dispSettingBox.SelectedIndex = 8;
            }
            if (e.Key == Key.D9)
            {
                dispSettingBox.SelectedIndex = 9;
            }

        }

        private void Natal_Current_Click(object sender, RoutedEventArgs e)
        {
            natalSet();
            UserEventData edata = CommonData.udata2event(targetUser);
            ReCalc(edata, null, null, null, null, null, null);
            ReRender();
        }

        private void Transit_Current_Click(object sender, RoutedEventArgs e)
        {
            transitSet();
            ReCalc(null, userdata, null, null, null, null, null);
            ReRender();
        }

        private void Both_Current_Click(object sender, RoutedEventArgs e)
        {
            natalSet();
            transitSet();
            UserEventData edata = CommonData.udata2event(targetUser);
            ReCalc(edata, userdata, null, null, null, null, null);
            ReRender();
        }

        public void natalSet()
        {
            mainWindowVM.userName = "現在時刻";
            mainWindowVM.userBirthStr =
                DateTime.Now.Year
                + "/"
                + DateTime.Now.Month.ToString("00")
                + "/"
                + DateTime.Now.Day.ToString("00")
                + " "
                + DateTime.Now.Hour.ToString("00")
                + ":"
                + DateTime.Now.Minute.ToString("00")
                + ":"
                + DateTime.Now.Second.ToString("00")
                + " "
                + config.defaultTimezone;
            mainWindowVM.userBirthPlace = config.defaultPlace;
            mainWindowVM.userLat = config.lat.ToString("00.0000");
            mainWindowVM.userLng = config.lng.ToString("000.0000");

            targetUser.name = "現在時刻";
            targetUser.birth_year = DateTime.Now.Year;
            targetUser.birth_month = DateTime.Now.Month;
            targetUser.birth_day = DateTime.Now.Day;
            targetUser.birth_hour = DateTime.Now.Hour;
            targetUser.birth_minute = DateTime.Now.Minute;
            targetUser.birth_second = DateTime.Now.Second;
            targetUser.birth_place = config.defaultPlace;
            targetUser.lat = config.lat;
            targetUser.lng = config.lng;
            targetUser.timezone = config.defaultTimezone;
        }

        public void transitSet()
        {
            mainWindowVM.transitName = "現在時刻";
            mainWindowVM.transitBirthStr =
                DateTime.Now.Year
                + "/"
                + DateTime.Now.Month.ToString("00")
                + "/"
                + DateTime.Now.Day.ToString("00")
                + " "
                + DateTime.Now.Hour.ToString("00")
                + ":"
                + DateTime.Now.Minute.ToString("00")
                + ":"
                + DateTime.Now.Second.ToString("00")
                + " "
                + config.defaultTimezone;
            mainWindowVM.transitPlace = config.defaultPlace;
            mainWindowVM.transitLat = config.lat.ToString("00.0000");
            mainWindowVM.transitLng = config.lng.ToString("000.0000");

            userdata.name = "現在時刻";
            userdata.birth_year = DateTime.Now.Year;
            userdata.birth_month = DateTime.Now.Month;
            userdata.birth_day = DateTime.Now.Day;
            userdata.birth_hour = DateTime.Now.Hour;
            userdata.birth_minute = DateTime.Now.Minute;
            userdata.birth_second = DateTime.Now.Second;
            userdata.birth_place = config.defaultPlace;
            userdata.lat = config.lat;
            userdata.lng = config.lng;
            userdata.timezone = config.defaultTimezone;

        }

        private void Png_Export(object sender, RoutedEventArgs e)
        {
            DrawingVisual dv = new DrawingVisual();
            DrawingContext dc = dv.RenderOpen();
            Canvas cnvs = ringCanvas;
            cnvs.Background = System.Windows.Media.Brushes.White;
            cnvs.LayoutTransform = null;
            System.Windows.Size oldSize = new System.Windows.Size(ringCanvas.ActualWidth, ringCanvas.ActualHeight);
            System.Windows.Size renderSize;
            if (cnvs.ActualHeight > cnvs.ActualWidth)
            {
                renderSize = new System.Windows.Size(cnvs.ActualHeight, cnvs.ActualHeight);
            }
            else
            {
                renderSize = new System.Windows.Size(cnvs.ActualWidth, ringStack.ActualHeight);
            }
            cnvs.Measure(renderSize);
            cnvs.Arrange(new Rect(renderSize));
            RenderTargetBitmap render = new RenderTargetBitmap((Int32)renderSize.Width, (Int32)renderSize.Height, 96, 96, PixelFormats.Default);
            render.Render(cnvs);

            var enc = new PngBitmapEncoder();
            enc.Frames.Add(BitmapFrame.Create(render));

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = "horoscope.png";
            sfd.Filter = "pngファイル(*.png)|*.png|すべてのファイル(*.*)|*.*";
            sfd.Title = "画像ファイル名を選択してください";

            string pngFile;
            sfd.ShowDialog();
            if (sfd.FileName != "")
            {
                pngFile = sfd.FileName;
                using (FileStream fs = new FileStream(pngFile, FileMode.Create))
                {
                    enc.Save(fs);
                    fs.Close();
                }
            }


            ringCanvas.Measure(oldSize);
            ringCanvas.Arrange(new Rect(oldSize));

        }

        private void VersionWindow_Click(object sender, RoutedEventArgs e)
        {
            if (versionWindow == null)
            {
                versionWindow = new VersionWindow();
            }
            versionWindow.Visibility = Visibility.Visible;

        }

        private void dispSettingBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (userdata == null || targetUser == null)
            {
                return;
            }
            int index = dispSettingBox.SelectedIndex;
            currentSetting = settings[index];
            ReCalc();
            ReRender();
        }

        private void SingleRingEvent_Click(object sender, RoutedEventArgs e)
        {
            tempSettings.bands = 1;
            tempSettings.firstHouseDiv = TempSetting.HouseDivide.EVENT1;
            ReCalc();
            ReRender();
        }
    }
}
