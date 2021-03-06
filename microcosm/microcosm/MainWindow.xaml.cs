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
        public UserData targetUser2;
        public UserEventData userdata;
        public UserEventData userdata2;

        public Dictionary<int, int> dispListMap;

        public Dictionary<int, PlanetData> list1;
        public Dictionary<int, PlanetData> list2;
        public Dictionary<int, PlanetData> list3;
        public Dictionary<int, PlanetData> list4;
        public Dictionary<int, PlanetData> list5;
        public Dictionary<int, PlanetData> list6;
        public Dictionary<int, PlanetData> list7;

        public double[] houseList1;
        public double[] houseList2;
        public double[] houseList3;
        public double[] houseList4;
        public double[] houseList5;
        public double[] houseList6;
        public double[] houseList7;

        public Dictionary<int, string> dispositorTxt = new Dictionary<int, string>();

        public Dispositor[] dispositorList = new Dispositor[10];
        public Dictionary<int, bool> checkList = new Dictionary<int, bool>();

        public bool ctrl_a = false;

        public int currentTarget = 1;

        public int[] calcTargetUser = { 1, 1, 1, 1, 1, 1, 1 };
        public int[] calcTargetEvent = { 1, 1, 1, 1, 1, 1, 1 };

        public CommonConfigWindow configWindow;
        public SettingWIndow setWindow;
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
                string dirName = @"system";
                if (!Directory.Exists(dirName))
                {
                    Directory.CreateDirectory(dirName);
                }
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
                dp11.Add(CommonData.ZODIAC_EARTH, settings[i].xmlData.dispPlanetEarth11);
                dp11.Add(CommonData.ZODIAC_LILITH, settings[i].xmlData.dispPlanetLilith11);
                dp11.Add(CommonData.ZODIAC_CELES, false);
                dp11.Add(CommonData.ZODIAC_PARAS, false);
                dp11.Add(CommonData.ZODIAC_JUNO, false);
                dp11.Add(CommonData.ZODIAC_VESTA, false);
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
                dp22.Add(CommonData.ZODIAC_EARTH, settings[i].xmlData.dispPlanetEarth22);
                dp22.Add(CommonData.ZODIAC_LILITH, settings[i].xmlData.dispPlanetLilith22);
                dp22.Add(CommonData.ZODIAC_CELES, false);
                dp22.Add(CommonData.ZODIAC_PARAS, false);
                dp22.Add(CommonData.ZODIAC_JUNO, false);
                dp22.Add(CommonData.ZODIAC_VESTA, false);
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
                dp33.Add(CommonData.ZODIAC_EARTH, settings[i].xmlData.dispPlanetEarth33);
                dp33.Add(CommonData.ZODIAC_LILITH, settings[i].xmlData.dispPlanetLilith33);
                dp33.Add(CommonData.ZODIAC_CELES, false);
                dp33.Add(CommonData.ZODIAC_PARAS, false);
                dp33.Add(CommonData.ZODIAC_JUNO, false);
                dp33.Add(CommonData.ZODIAC_VESTA, false);
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
                dp12.Add(CommonData.ZODIAC_EARTH, settings[i].xmlData.dispPlanetEarth12);
                dp12.Add(CommonData.ZODIAC_LILITH, settings[i].xmlData.dispPlanetLilith12);
                dp12.Add(CommonData.ZODIAC_CELES, false);
                dp12.Add(CommonData.ZODIAC_PARAS, false);
                dp12.Add(CommonData.ZODIAC_JUNO, false);
                dp12.Add(CommonData.ZODIAC_VESTA, false);
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
                dp13.Add(CommonData.ZODIAC_EARTH, settings[i].xmlData.dispPlanetEarth13);
                dp13.Add(CommonData.ZODIAC_LILITH, settings[i].xmlData.dispPlanetLilith13);
                dp13.Add(CommonData.ZODIAC_CELES, false);
                dp13.Add(CommonData.ZODIAC_PARAS, false);
                dp13.Add(CommonData.ZODIAC_JUNO, false);
                dp13.Add(CommonData.ZODIAC_VESTA, false);
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
                dp23.Add(CommonData.ZODIAC_EARTH, settings[i].xmlData.dispPlanetEarth23);
                dp23.Add(CommonData.ZODIAC_LILITH, settings[i].xmlData.dispPlanetLilith23);
                dp23.Add(CommonData.ZODIAC_CELES, false);
                dp23.Add(CommonData.ZODIAC_PARAS, false);
                dp23.Add(CommonData.ZODIAC_JUNO, false);
                dp23.Add(CommonData.ZODIAC_VESTA, false);
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
                d11.Add(CommonData.ZODIAC_EARTH, settings[i].xmlData.aspectEarth11);
                d11.Add(CommonData.ZODIAC_LILITH, settings[i].xmlData.aspectLilith11);
                d11.Add(CommonData.ZODIAC_CELES, false);
                d11.Add(CommonData.ZODIAC_PARAS, false);
                d11.Add(CommonData.ZODIAC_JUNO, false);
                d11.Add(CommonData.ZODIAC_VESTA, false);
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
                d22.Add(CommonData.ZODIAC_EARTH, settings[i].xmlData.aspectEarth22);
                d22.Add(CommonData.ZODIAC_LILITH, settings[i].xmlData.aspectLilith22);
                d22.Add(CommonData.ZODIAC_CELES, false);
                d22.Add(CommonData.ZODIAC_PARAS, false);
                d22.Add(CommonData.ZODIAC_JUNO, false);
                d22.Add(CommonData.ZODIAC_VESTA, false);
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
                d33.Add(CommonData.ZODIAC_EARTH, settings[i].xmlData.aspectEarth33);
                d33.Add(CommonData.ZODIAC_LILITH, settings[i].xmlData.aspectLilith33);
                d33.Add(CommonData.ZODIAC_CELES, false);
                d33.Add(CommonData.ZODIAC_PARAS, false);
                d33.Add(CommonData.ZODIAC_JUNO, false);
                d33.Add(CommonData.ZODIAC_VESTA, false);
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
                d12.Add(CommonData.ZODIAC_EARTH, settings[i].xmlData.aspectEarth12);
                d12.Add(CommonData.ZODIAC_LILITH, settings[i].xmlData.aspectLilith12);
                d12.Add(CommonData.ZODIAC_CELES, false);
                d12.Add(CommonData.ZODIAC_PARAS, false);
                d12.Add(CommonData.ZODIAC_JUNO, false);
                d12.Add(CommonData.ZODIAC_VESTA, false);
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
                d13.Add(CommonData.ZODIAC_EARTH, settings[i].xmlData.aspectEarth13);
                d13.Add(CommonData.ZODIAC_LILITH, settings[i].xmlData.aspectLilith13);
                d13.Add(CommonData.ZODIAC_CELES, false);
                d13.Add(CommonData.ZODIAC_PARAS, false);
                d13.Add(CommonData.ZODIAC_JUNO, false);
                d13.Add(CommonData.ZODIAC_VESTA, false);
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
                d23.Add(CommonData.ZODIAC_EARTH, settings[i].xmlData.aspectEarth23);
                d23.Add(CommonData.ZODIAC_LILITH, settings[i].xmlData.aspectLilith23);
                d23.Add(CommonData.ZODIAC_CELES, false);
                d23.Add(CommonData.ZODIAC_PARAS, false);
                d23.Add(CommonData.ZODIAC_JUNO, false);
                d23.Add(CommonData.ZODIAC_VESTA, false);
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
            dispListMap.Add(CommonData.ZODIAC_LILITH, 15);

            rcanvas = new RingCanvasViewModel(config);
            ringStack.Background = System.Windows.Media.Brushes.GhostWhite;
            ContextMenu context = new ContextMenu();
            MenuItem fullAspectItem = new MenuItem { Header = "全ての天体のアスペクトを表示" };
            fullAspectItem.Click += FullAspect_Click;
            context.Items.Add(fullAspectItem);
            ringStack.ContextMenu = context;

        }

        /// <summary>
        /// AstroCalcインスタンス
        /// </summary>
        private void DataCalc()
        {
            calc = new AstroCalc(this, config);
        }

        private void SetViewModel()
        {
            targetUser = new UserData(config);
            targetUser2 = new UserData(config);
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
                lat = targetUser.lat,
                lng = targetUser.lng,
                lat_lng = targetUser.lat_lng,
                timezone = targetUser.timezone,
                memo = targetUser.memo,
                fullpath = targetUser.filename
            };
            userdata2 = new UserEventData()
            {
                name = targetUser.name,
                birth_year = targetUser.birth_year,
                birth_month = targetUser.birth_month,
                birth_day = targetUser.birth_day,
                birth_hour = targetUser.birth_hour,
                birth_minute = targetUser.birth_minute,
                birth_second = targetUser.birth_second,
                birth_place = targetUser.birth_place,
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
                userLat = String.Format("{0:f6}", targetUser.lat),
                userLng = String.Format("{0:f6}", targetUser.lng),
                transitName = "現在時刻",
                transitBirthStr = tb.birthStr,
                transitTimezone = targetUser.timezone,
                transitPlace = targetUser.birth_place,
                transitLat = String.Format("{0:f6}", targetUser.lat),
                transitLng = String.Format("{0:f6}", targetUser.lng),
            };
            // 左上、右上表示
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
            List<UserEventData> listEventData = new List<UserEventData>();
            listEventData.Add(edata);
            listEventData.Add(edata);
            listEventData.Add(edata);
            listEventData.Add(edata);
            listEventData.Add(edata);
            listEventData.Add(edata);
            listEventData.Add(edata);
            // 一番最初のReCalc
            ReCalc(listEventData);

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

            setYear.Text = targetUser.birth_year.ToString();
            setMonth.Text = targetUser.birth_month.ToString();
            setDay.Text = targetUser.birth_day.ToString();
            setHour.Text = targetUser.birth_hour.ToString();
            setMinute.Text = targetUser.birth_minute.ToString();
            setSecond.Text = targetUser.birth_second.ToString();
            setLat.Text = String.Format("{0:f4}", targetUser.lat);
            setLng.Text = String.Format("{0:f4}", targetUser.lng);
        }

        /// <summary>
        /// 再計算
        /// </summary>
        public void ReCalc()
        {
#if DEBUG
            DateTime startDt = DateTime.Now;
#endif

            if (tempSettings.firstBand == TempSetting.BandKind.NATAL)
            {
                UserEventData edata;
                // 1番目の円がnatalの場合、userDataをuserEventDataにする
                if (calcTargetUser[0] == 1)
                {
                    edata = CommonData.udata2event(targetUser);
                }
                else
                {
                    edata = CommonData.udata2event(targetUser2);
                }

                List<UserEventData> listEventData = new List<UserEventData>();
                listEventData.Add(edata);
                for (int i = 1; i < 7; i++)
                {
                    UserEventData tempEdata;
                    if (tempSettings.secondBand == TempSetting.BandKind.NATAL)
                    {
                        if (calcTargetUser[1] == 1)
                        {
                            tempEdata = CommonData.udata2event(targetUser);
                            listEventData.Add(tempEdata);
                        }
                        else
                        {
                            tempEdata = CommonData.udata2event(targetUser2);
                            listEventData.Add(tempEdata);
                        }
                    }
                    else
                    {
                        if (calcTargetEvent[1] == 1)
                        {
                            listEventData.Add(userdata);
                        }
                        else
                        {
                            listEventData.Add(userdata2);
                        }
                    }
                }
                ReCalc(listEventData);
            }
            else
            {
                // 1番目の円がnatalじゃない場合
                List<UserEventData> listEventData = new List<UserEventData>();
                for (int i = 0; i < 7; i++)
                {
                    if (calcTargetEvent[0] == 1)
                    {
                        listEventData.Add(userdata);
                    }
                    else
                    {
                        listEventData.Add(userdata2);
                    }
                }
                ReCalc(listEventData);
            }

            reportVM.ReCalcReport(list1, list2, list3, list4, list5, list6,
                houseList1, houseList2, houseList3, houseList4, houseList5, houseList6);
#if DEBUG
            DateTime endDt = DateTime.Now;
            TimeSpan ts = endDt - startDt; // 時間の差分を取得
            Console.WriteLine("Recalc " + ts.TotalSeconds + " sec"); // 経過時間（秒）
#endif

        }

        // 再計算
        // 表示可否の時はここは呼ばない
        public void ReCalc(
                List<UserEventData> listEventData
            )
        {
            // 次のリファクタリングできれいにしよう
            UserEventData list1Data = null;
            UserEventData list2Data = null;
            UserEventData list3Data = null;
            UserEventData list4Data = null;
            UserEventData list5Data = null;
            UserEventData list6Data = null;
            UserEventData list7Data = null;
            DateTime list1Time = new DateTime();
            DateTime list2Time = new DateTime();
            DateTime list3Time = new DateTime();
            DateTime list4Time = new DateTime();
            DateTime list5Time = new DateTime();
            DateTime list6Time = new DateTime();
            DateTime list7Time = new DateTime();
            if (listEventData.Count > 0)
            {
                list1Data = listEventData[0];
                list1Time = edataTime(list1Data);
            }
            if (listEventData.Count > 1)
            {
                list2Data = listEventData[1];
                list2Time = edataTime(list2Data);
            }
            if (listEventData.Count > 2)
            {
                list3Data = listEventData[2];
                list3Time = edataTime(list3Data);
            }
            if (listEventData.Count > 3)
            {
                list4Data = listEventData[3];
                list4Time = edataTime(list4Data);
            }
            if (listEventData.Count > 4)
            {
                list5Data = listEventData[4];
                list5Time = edataTime(list5Data);
            }
            if (listEventData.Count > 5)
            {
                list6Data = listEventData[5];
                list6Time = edataTime(list6Data);
            }
            if (listEventData.Count > 6)
            {
                list7Data = listEventData[6];
                list7Time = edataTime(list7Data);
            }

            DateTime udata1;
            DateTime edata1;
            if (calcTargetUser[0] == 1)
            {
                udata1 = udataTime(targetUser);
                if (calcTargetEvent[0] == 1)
                {
                    edata1 = edataTime(userdata);
                }
                else
                {
                    edata1 = edataTime(userdata2);
                }
            }
            else
            {
                udata1 = udataTime(targetUser2);
                if (calcTargetEvent[0] == 1)
                {
                    edata1 = edataTime(userdata);
                }
                else
                {
                    edata1 = edataTime(userdata2);
                }
            }

            if (list1Data != null)
            {
                if (tempSettings.firstBand == TempSetting.BandKind.PROGRESS)
                {
                    // プログレスの場合（まず使わないけど）
                    Dictionary<int, PlanetData> tempList;

                    switch (config.progression)
                    {
                        case EProgression.PRIMARY:
                            tempList = calc.PositionCalc(list1Time,
                                list1Data.lat, list1Data.lng, (int)config.houseCalc, 0);

                            list1 = calc.PrimaryProgressionCalc(tempList, udata1, edata1);
                            if (tempSettings.firstHouseDiv == TempSetting.HouseDivide.PROGRESS)
                            {
                                houseList1 = calc.PrimaryProgressionHouseCalc(houseList1, udata1, edata1);

                            }

                            break;

                        case EProgression.SECONDARY:
                            tempList = calc.PositionCalc(list1Time,
                                list1Data.lat, list1Data.lng, (int)config.houseCalc, 0);


                            list1 = calc.SecondaryProgressionCalc(tempList, udata1, edata1);
                            if (tempSettings.firstHouseDiv == TempSetting.HouseDivide.PROGRESS)
                            {
                                houseList1 = calc.SecondaryProgressionHouseCalc(houseList1,
                                    tempList, udata1, edata1,
                                    targetUser.lat,
                                    targetUser.lng,
                                    targetUser.timezone
                                );

                            }

                            break;

                        case EProgression.CPS:
                            tempList = calc.PositionCalc(list1Time,
                                list1Data.lat, list1Data.lng, (int)config.houseCalc, 0);

                            list1 = calc.CompositProgressionCalc(tempList,
                                udata1, edata1
                            );
                            if (tempSettings.firstHouseDiv == TempSetting.HouseDivide.PROGRESS)
                            {
                                houseList1 = calc.CompositProgressionHouseCalc(houseList1,
                                    tempList,
                                    udata1, edata1,
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
                    // natal or transit
                    list1 = calc.PositionCalc(list1Time,
                        list1Data.lat, list1Data.lng, (int)config.houseCalc, 0);
                }
                if (tempSettings.firstHouseDiv == TempSetting.HouseDivide.USER1)
                {
                    if (calcTargetUser[0] == 1)
                    {
                        houseList1 = calc.CuspCalc(udata1,
                        targetUser.lat, targetUser.lng, (int)config.houseCalc);
                    }
                    else
                    {
                        houseList1 = calc.CuspCalc(udata1,
                        targetUser2.lat, targetUser2.lng, (int)config.houseCalc);
                    }
                }
                else if (tempSettings.firstHouseDiv == TempSetting.HouseDivide.EVENT1)
                {
                    if (calcTargetEvent[1] == 1)
                    {
                        houseList1 = calc.CuspCalc(edata1,
                            userdata.lat, userdata.lng, (int)config.houseCalc);
                    }
                    else
                    {
                        houseList1 = calc.CuspCalc(edata1,
                            userdata2.lat, userdata2.lng, (int)config.houseCalc);
                    }
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
                                udata1, edata1
                            );
                            if (tempSettings.secondHouseDiv == TempSetting.HouseDivide.PROGRESS)
                            {
                                houseList2 = calc.PrimaryProgressionHouseCalc(houseList1,
                                    udata1, edata1
                                );
                            }

                            break;

                        case EProgression.SECONDARY:
                            list2 = calc.SecondaryProgressionCalc(list1,
                                udata1, edata1
                            );
                            if (tempSettings.secondHouseDiv == TempSetting.HouseDivide.PROGRESS)
                            {
                                houseList2 = calc.SecondaryProgressionHouseCalc(houseList1,
                                    list1, udata1, edata1,
                                    targetUser.lat,
                                    targetUser.lng,
                                    targetUser.timezone
                                );

                            }

                            break;

                        case EProgression.CPS:
                            list2 = calc.CompositProgressionCalc(list1,
                                udata1, edata1
                            );
                            if (tempSettings.secondHouseDiv == TempSetting.HouseDivide.PROGRESS)
                            {
                                houseList2 = calc.CompositProgressionHouseCalc(houseList1,
                                    list1,
                                    udata1,
                                    edata1,
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
                    // natal or transit
                    list2 = calc.PositionCalc(list2Time,
                        list2Data.lat, list2Data.lng, (int)config.houseCalc, 1);
                }
                if (tempSettings.secondHouseDiv == TempSetting.HouseDivide.USER1)
                {
                    if (calcTargetUser[1] == 1)
                    {
                        houseList2 = calc.CuspCalc(udata1,
                        targetUser.lat, targetUser.lng, (int)config.houseCalc);
                    }
                    else
                    {
                        houseList2 = calc.CuspCalc(udata1,
                        targetUser2.lat, targetUser2.lng, (int)config.houseCalc);
                    }
                }
                else if (tempSettings.secondHouseDiv == TempSetting.HouseDivide.EVENT1)
                {
                    if (calcTargetEvent[1] == 1)
                    {
                        houseList2 = calc.CuspCalc(edata1,
                            userdata.lat, userdata.lng, (int)config.houseCalc);
                    }
                    else
                    {
                        houseList2 = calc.CuspCalc(edata1,
                            userdata2.lat, userdata2.lng, (int)config.houseCalc);
                    }
                }
            }
            if (list3Data != null)
            {
                if (tempSettings.thirdBand == TempSetting.BandKind.PROGRESS)
                {
                    switch (config.progression)
                    {
                        case EProgression.PRIMARY:
                            list3 = calc.PrimaryProgressionCalc(list1, udata1, edata1
                            );
                            if (tempSettings.thirdHouseDiv == TempSetting.HouseDivide.PROGRESS)
                            {
                                houseList3 = calc.PrimaryProgressionHouseCalc(houseList3, udata1, edata1
                                );
                            }

                            break;

                        case EProgression.SECONDARY:
                            list3 = calc.SecondaryProgressionCalc(list1, udata1, edata1
                            );
                            if (tempSettings.thirdHouseDiv == TempSetting.HouseDivide.PROGRESS)
                            {
                                houseList3 = calc.SecondaryProgressionHouseCalc(houseList3,
                                    list1, udata1, edata1,
                                    targetUser.lat,
                                    targetUser.lng,
                                    targetUser.timezone
                                );

                            }

                            break;

                        case EProgression.CPS:
                            list3 = calc.CompositProgressionCalc(list1, udata1, edata1
                            );
                            if (tempSettings.thirdHouseDiv == TempSetting.HouseDivide.PROGRESS)
                            {
                                houseList3 = calc.CompositProgressionHouseCalc(houseList3,
                                    list1, udata1, edata1,
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
                    // natal or transit
                    list3 = calc.PositionCalc(list3Time,
                        list3Data.lat, list3Data.lng, (int)config.houseCalc, 2);
                }
                if (tempSettings.thirdHouseDiv == TempSetting.HouseDivide.USER1)
                {
                    if (calcTargetUser[2] == 1)
                    {
                        houseList3 = calc.CuspCalc(udata1,
                        targetUser.lat, targetUser.lng, (int)config.houseCalc);
                    }
                    else
                    {
                        houseList3 = calc.CuspCalc(udata1,
                        targetUser2.lat, targetUser2.lng, (int)config.houseCalc);
                    }
                }
                else if (tempSettings.thirdHouseDiv == TempSetting.HouseDivide.EVENT1)
                {
                    if (calcTargetUser[2] == 1)
                    {
                        houseList3 = calc.CuspCalc(edata1,
                            userdata.lat, userdata.lng, (int)config.houseCalc);
                    }
                    else
                    {
                        houseList3 = calc.CuspCalc(edata1,
                            userdata2.lat, userdata2.lng, (int)config.houseCalc);
                    }
                }
            }
            // 以下未実装
            if (list4Data != null)
            {
                if (tempSettings.fourthBand == TempSetting.BandKind.PROGRESS)
                {
                    list4 = calc.PositionCalc(list4Time,
                        list4Data.lat, list4Data.lng, (int)config.houseCalc, -1);
                }
                else
                {
                    list4 = calc.PositionCalc(list4Time,
                        list4Data.lat, list4Data.lng, (int)config.houseCalc, -1);
                }
                houseList4 = calc.CuspCalc(edata1,
                    list4Data.lat, list4Data.lng, (int)config.houseCalc);
            }
            if (list5Data != null)
            {
                if (tempSettings.fifthBand == TempSetting.BandKind.PROGRESS)
                {
                    list5 = calc.PositionCalc(list5Time,
                        list5Data.lat, list5Data.lng, (int)config.houseCalc, -1);
                }
                else
                {
                    list5 = calc.PositionCalc(list5Time,
                        list5Data.lat, list5Data.lng, (int)config.houseCalc, -1);
                }
                houseList5 = calc.CuspCalc(edata1,
                    list5Data.lat, list5Data.lng, (int)config.houseCalc);
            }
            if (list6Data != null)
            {
                if (tempSettings.sixthBand == TempSetting.BandKind.PROGRESS)
                {
                    list6 = calc.PositionCalc(list6Time,
                        list6Data.lat, list6Data.lng, (int)config.houseCalc, -1);
                }
                else
                {
                    list6 = calc.PositionCalc(list6Time,
                        list6Data.lat, list6Data.lng, (int)config.houseCalc, -1);
                }
                houseList6 = calc.CuspCalc(edata1,
                    list6Data.lat, list6Data.lng, (int)config.houseCalc);
            }
            if (list7Data != null)
            {
                if (tempSettings.secondBand == TempSetting.BandKind.PROGRESS)
                {
                    list7 = calc.PositionCalc(list7Time,
                        list7Data.lat, list7Data.lng, (int)config.houseCalc, -1);
                }
                else
                {
                    list7 = calc.PositionCalc(list7Time,
                        list7Data.lat, list7Data.lng, (int)config.houseCalc, -1);
                }
                houseList7 = calc.CuspCalc(edata1,
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

            // ディスポジター
            // 初期化
            for (int i = 0; i < 10; i++)
            {
                dispositorList[i] = new Dispositor();
                dispositorList[i].prev = new List<int>();
            }

            for (int i = 0; i < 10; i++)
            {
                dispositorList[i].next = CommonData.getSignRulersNo(CommonData.getSign(list1[i].absolute_position));
                if (i != dispositorList[i].next)
                {
                    dispositorList[dispositorList[i].next].prev.Add(i);
                }
                else
                {
                    dispositorList[i].final = true;
                }
            }
            calcDispositor();

        }

        private void calcDispositor()
        {
            dispositor1.Text = "";
            for (int i = 0; i < 10; i++)
            {
                if (i > 0)
                {
                    dispositor1.Text = dispositor1.Text + Environment.NewLine +
                        CommonData.getPlanetSymbol(list1[i].no) +
                        CommonData.getSignText(list1[i].absolute_position) +
                        ">" + CommonData.getPlanetSymbol(dispositorList[i].next) +
                        CommonData.getSignText(list1[dispositorList[i].next].absolute_position);
                }
                else
                {
                    dispositor1.Text = 
                        CommonData.getPlanetSymbol(list1[i].no) +
                        CommonData.getSignText(list1[i].absolute_position) +
                        ">" + CommonData.getPlanetSymbol(dispositorList[i].next) +
                        CommonData.getSignText(list1[dispositorList[i].next].absolute_position);
                }
            }
        }

        private void mainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ReRender();
        }

        /// <summary>
        /// レンダリングメイン
        /// disp変更の場合はこれだけ呼ぶ
        /// </summary>
        public void ReRender()
        {
#if DEBUG
            DateTime startDt = DateTime.Now;
#endif
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
            List<PlanetData> newList1 = new List<PlanetData>();
            foreach (var keys in list1.Keys)
            {
                newList1.Add(list1[keys]);
            }
            List<PlanetData> newList2 = new List<PlanetData>();
            foreach (var keys in list2.Keys)
            {
                newList2.Add(list2[keys]);
            }
            List<PlanetData> newList3 = new List<PlanetData>();
            foreach (var keys in list3.Keys)
            {
                newList3.Add(list3[keys]);
            }
            List<PlanetData> newList4 = new List<PlanetData>();
            foreach (var keys in list4.Keys)
            {
                newList4.Add(list4[keys]);
            }
            List<PlanetData> newList5 = new List<PlanetData>();
            foreach (var keys in list5.Keys)
            {
                newList4.Add(list5[keys]);
            }
            newList1.Sort((a, b) => (int)(a.absolute_position * 100 - b.absolute_position * 100));
            newList2.Sort((a, b) => (int)(a.absolute_position * 100 - b.absolute_position * 100));
            newList3.Sort((a, b) => (int)(a.absolute_position * 100 - b.absolute_position * 100));
            newList4.Sort((a, b) => (int)(a.absolute_position * 100 - b.absolute_position * 100));
            newList5.Sort((a, b) => (int)(a.absolute_position * 100 - b.absolute_position * 100));
            planetRender(houseList1[1], newList1, newList2, newList3, newList4, newList5);
            planetLine(houseList1[1], newList1, newList2, newList3, newList4, newList5);

            // アスペクト描画がおかしくなるので戻しておく
            newList1.Sort((a, b) => (int)(a.no - b.no));
            newList2.Sort((a, b) => (int)(a.no - b.no));
            newList3.Sort((a, b) => (int)(a.no - b.no));
            newList4.Sort((a, b) => (int)(a.no - b.no));
            newList5.Sort((a, b) => (int)(a.no - b.no));
            aspectsRendering(houseList1[1], list1, list2, list3, list4, list5);

            Label copy = new Label();
            copy.Content = "microcosm";
            copy.Margin = new Thickness(ringCanvas.ActualWidth - 70, ringStack.ActualHeight - 45, 0, 0);
            Label url = new Label();
            url.Content = "http://microcosm.ogatism.com/";
            url.Margin = new Thickness(ringCanvas.ActualWidth - 180, ringStack.ActualHeight - 30, 0, 0);

            ringCanvas.Children.Add(copy);
            ringCanvas.Children.Add(url);
#if DEBUG
            DateTime endDt = DateTime.Now;
            TimeSpan ts = endDt - startDt; // 時間の差分を取得
            Console.WriteLine("ReRender " + ts.TotalSeconds + " sec"); // 経過時間（秒）
#endif
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
                PointF point = rotate(rcanvas.outerWidth / 2 - 31, 0, (30 * (i + 1)) - startdegree - 15.0);
                point.X += (float)rcanvas.outerWidth / 2 - 12;
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
                zodiacLabel.ToolTip = CommonData.getSignTextJp(i * 30) + " (ルーラー：" + CommonData.getSignRulersSymbol(i) + ")";
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
            if (config.color29 == Config.Color29.CHANGE &&
                (
                    displayData.degreeTxt == "29°" ||
                    displayData.degreeTxt == "00°" ||
                    displayData.degreeTxt == "29" ||
                    displayData.degreeTxt == "00"
                    )
                )
            {
                degreeLbl.Foreground = new SolidColorBrush(Colors.Red);
                degreeLbl.FontWeight = FontWeights.Bold;
            }
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

            ContextMenu context = new ContextMenu();
            MenuItem onlyAspectItem = new MenuItem { Header = "この天体のアスペクトだけ表示" };
            onlyAspectItem.Click += OnlyAspect_Click;
            context.Items.Add(onlyAspectItem);
            MenuItem fullAspectItem = new MenuItem { Header = "全ての天体のアスペクトだけ表示" };
            fullAspectItem.Click += FullAspect_Click;
            context.Items.Add(fullAspectItem);

            txtLbl.ContextMenu = context;

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
            ContextMenu context = new ContextMenu();
            MenuItem newItem = new MenuItem { Header = "この天体のアスペクトだけ表示" };
            newItem.Click += OnlyAspect_Click;
            context.Items.Add(newItem);
            MenuItem fullAspectItem = new MenuItem { Header = "全ての天体のアスペクトだけ表示" };
            fullAspectItem.Click += FullAspect_Click;
            context.Items.Add(fullAspectItem);

            txtLbl.ContextMenu = context;
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

            TextBlock degreeLbl = new TextBlock();
            degreeLbl.Text = displayData.degreeTxt;
            if (displayData.retrogradeTxt != "")
            {
                degreeLbl.TextDecorations = TextDecorations.Underline;
            }
            if (config.color29 == Config.Color29.CHANGE && 
                    (
                    displayData.degreeTxt == "29°" || 
                    displayData.degreeTxt == "00°" ||
                    displayData.degreeTxt == "29" ||
                    displayData.degreeTxt == "00"
                    )
                )
            {
                degreeLbl.Foreground = new SolidColorBrush(Colors.Red);
                degreeLbl.FontWeight = FontWeights.Bold;
            }

            degreeLbl.Margin = new Thickness(displayData.degreePt.X, displayData.degreePt.Y, 0, 0);
            degreeLbl.Tag = displayData.explanation;
            degreeLbl.MouseEnter += planetMouseEnter;

            ContextMenu context = new ContextMenu();
            MenuItem newItem = new MenuItem { Header = "この天体のアスペクトだけ表示" };
            newItem.Click += OnlyAspect_Click;
            context.Items.Add(newItem);
            MenuItem fullAspectItem = new MenuItem { Header = "全ての天体のアスペクトだけ表示" };
            fullAspectItem.Click += FullAspect_Click;
            context.Items.Add(fullAspectItem);

            txtLbl.ContextMenu = context;

            ringCanvas.Children.Add(degreeLbl);
        }

        public void OnlyAspect_Click(object sender, EventArgs e)
        {
            Label l;
            TextBlock t;
            Explanation data;
            MenuItem s = (MenuItem)sender;
            ContextMenu m = (ContextMenu)s.Parent;
            if (m.PlacementTarget is Label)
            {
                l = (Label)m.PlacementTarget;
                data = (Explanation)l.Tag;
            }
            else if (m.PlacementTarget is TextBlock)
            {
                t = (TextBlock)m.PlacementTarget;
                data = (Explanation)t.Tag;
            }
            else
            {
                return;
            }

            foreach (object planet in ringCanvas.Children)
            {
                if (planet is Line)
                {
                    FrameworkElement elem = (FrameworkElement)planet;
                    if (elem.Tag is AspectInfo)
                    {
                        AspectInfo info = (AspectInfo)elem.Tag;
                        if (data.planetNo != -1 &&
                            data.planetNo != info.srcPlanetNo &&
                            data.planetNo != info.targetPlanetNo)
                        {
                            elem.Visibility = Visibility.Hidden;
                        } else
                        {
                            elem.Visibility = Visibility.Visible;
                        }
                    }
                }
                else if (planet is TextBlock)
                {
                    TextBlock aspectTxt = (TextBlock)planet;
                    if (aspectTxt.Text == "☍" ||
                        aspectTxt.Text == "△" ||
                        aspectTxt.Text == "□" ||
                        aspectTxt.Text == "⚹" ||
                        aspectTxt.Text == "⚻" ||
                        aspectTxt.Text == "⚼")
                    {
                        AspectInfo info = (AspectInfo)aspectTxt.Tag;
                        if (data.planetNo != -1 &&
                            data.planetNo != info.srcPlanetNo &&
                            data.planetNo != info.targetPlanetNo)
                        {
                            aspectTxt.Visibility = Visibility.Hidden;
                        }
                        else
                        {
                            aspectTxt.Visibility = Visibility.Visible;
                        }
                    }
                }
            }
        }

        public void FullAspect_Click(object sender, EventArgs e)
        {
            foreach (object planet in ringCanvas.Children)
            {
                FrameworkElement elem = (FrameworkElement)planet;
                elem.Visibility = Visibility.Visible;
            }
        }

        // アスペクト表示
        public void aspectsRendering(
                double startDegree, 
                Dictionary<int, PlanetData> list1,
                Dictionary<int, PlanetData> list2,
                Dictionary<int, PlanetData> list3,
                Dictionary<int, PlanetData> list4,
                Dictionary<int, PlanetData> list5
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
        private void aspectRender(double startDegree, Dictionary<int, PlanetData> list, 
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

            List<PlanetData> newList = new List<PlanetData>();

            foreach (KeyValuePair<int, PlanetData> pair in list)
            {
                newList.Add(pair.Value);
            }

            for (int i = 0; i < newList.Count; i++)
            {
                if (!newList[i].isAspectDisp)
                {
                    // 表示対象外
                    continue;
                }
                PointF startPoint;
                startPoint = rotate(startRingX, 0, newList[i].absolute_position - startDegree);
                startPoint.X += (float)((rcanvas.outerWidth) / 2);
                startPoint.Y *= -1;
                startPoint.Y += (float)((rcanvas.outerHeight) / 2);
                if (aspectKind == 1)
                {
                    aspectListRender(startDegree, list, newList[i].aspects, startPoint, endRingX, endPosition);
                }
                else if (aspectKind == 2)
                {
                    aspectListRender(startDegree, list, newList[i].secondAspects, startPoint, endRingX, endPosition);
                }
                else if (aspectKind == 3)
                {
                    aspectListRender(startDegree, list, newList[i].thirdAspects, startPoint, endRingX, endPosition);
                }
            }
        }

        // aspectRender サブ関数
        private void aspectListRender(double startDegree, Dictionary<int, PlanetData> list, List<AspectInfo> aspects, PointF startPoint, double endRingX, int endPosition)
        {
            for (int j = 0; j < aspects.Count; j++)
            {
                if (!list[aspects[j].targetPlanetNo].isAspectDisp)
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
                    aspectLine.Stroke = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 255, 0, 0));
//                    aspectLine.Stroke = System.Windows.Media.Brushes.Red;
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
                aspectLbl.Tag = aspects[j];
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
            Label l;
            TextBlock t;
            Explanation data;
            if (sender is Label)
            {
                l = (Label)sender;
                data = (Explanation)l.Tag;
            }
            else if (sender is TextBlock)
            {
                t = (TextBlock)sender;
                data = (Explanation)t.Tag;
            }
            else
            {
                return;
            }
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

        // 一重円
        private void SingleRing_Click(object sender, RoutedEventArgs e)
        {
            tempSettings.bands = 1;
            tempSettings.firstHouseDiv = TempSetting.HouseDivide.USER1;
            ReCalc();
            ReRender();
        }
        // 一重円イベント
        private void SingleRingEvent_Click(object sender, RoutedEventArgs e)
        {
            tempSettings.bands = 1;
            tempSettings.firstHouseDiv = TempSetting.HouseDivide.EVENT1;
            ReCalc();
            ReRender();
        }


        // 三重円
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

        /// <summary>
        /// 右クリック関連
        /// </summary>
        private void mainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyboardDevice.IsKeyDown(Key.LeftCtrl) || e.KeyboardDevice.IsKeyDown(Key.RightCtrl))
            {

                if (e.Key == Key.A)
                {
                    noAspectClick();
                }

                else if (e.Key == Key.N)
                {
                    // メニュー
                    natalTime.IsChecked = true;
                    transitTime.IsChecked = false;
                    setYear.Focus();
                }
                else if (e.Key == Key.T)
                {
                    // メニュー
                    natalTime.IsChecked = false;
                    transitTime.IsChecked = true;
                    setYear.Focus();
                }
                else if (e.Key == Key.D0)
                {
                    dispSettingBox.SelectedIndex = 0;
                }
                else if (e.Key == Key.D1)
                {
                    dispSettingBox.SelectedIndex = 1;
                }
                else if (e.Key == Key.D2)
                {
                    dispSettingBox.SelectedIndex = 2;
                }
                else if (e.Key == Key.D3)
                {
                    dispSettingBox.SelectedIndex = 3;
                }
                else if (e.Key == Key.D4)
                {
                    dispSettingBox.SelectedIndex = 4;
                }
                else if (e.Key == Key.D5)
                {
                    dispSettingBox.SelectedIndex = 5;
                }
                else if (e.Key == Key.D6)
                {
                    dispSettingBox.SelectedIndex = 6;
                }
                else if (e.Key == Key.D7)
                {
                    dispSettingBox.SelectedIndex = 7;
                }
                else if (e.Key == Key.D8)
                {
                    dispSettingBox.SelectedIndex = 8;
                }
                else if (e.Key == Key.D9)
                {
                    dispSettingBox.SelectedIndex = 9;
                }

            }
            else if (e.KeyboardDevice.IsKeyDown(Key.LeftShift) || e.KeyboardDevice.IsKeyDown(Key.RightShift))
            {
                if (e.Key == Key.F6)
                {
                    // 一重円
                    tempSettings.bands = 1;
                    tempSettings.firstHouseDiv = TempSetting.HouseDivide.EVENT1;
                    ReCalc();
                    ReRender();
                }
            }
            else
            {
                if (e.Key == Key.F4)
                {
                    // 三重円
                    tempSettings.bands = 3;
                    ReRender();
                }
                else if (e.Key == Key.F6)
                {
                    // 一重円
                    tempSettings.bands = 1;
                    tempSettings.firstHouseDiv = TempSetting.HouseDivide.USER1;
                    ReCalc();
                    ReRender();
                }
            }


        }

        /// <summary>
        /// 現在時刻を開く
        /// targetUserを現在時刻にする
        /// targetUser2は不要
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Natal_Current_Click(object sender, RoutedEventArgs e)
        {
            natalSet();
            UserEventData edata = CommonData.udata2event(targetUser);

            List<UserEventData> listEventData = new List<UserEventData>();
            listEventData.Add(edata);
            ReCalc(listEventData);
            ReRender();
        }

        /// <summary>
        /// 現在時刻を開く
        /// userdataを現在時刻にする
        /// userdata2は不要
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Transit_Current_Click(object sender, RoutedEventArgs e)
        {
            transitSet();
            UserEventData edata = CommonData.udata2event(targetUser);
            List<UserEventData> listEventData = new List<UserEventData>();
            listEventData.Add(edata);
            listEventData.Add(userdata);
            ReCalc(listEventData);
            ReRender();
        }

        /// <summary>
        /// 現在時刻を開く
        /// 両方とも現在時刻にする
        /// userdata2は不要
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Both_Current_Click(object sender, RoutedEventArgs e)
        {
            natalSet();
            transitSet();
            UserEventData edata = CommonData.udata2event(targetUser);

            List<UserEventData> listEventData = new List<UserEventData>();
            listEventData.Add(edata);
            listEventData.Add(userdata);

            ReCalc(listEventData);
            ReRender();
        }

        /// <summary>
        /// 現在時刻セット
        /// </summary>
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

        /// <summary>
        /// 現在時刻セット
        /// </summary>
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

        /// <summary>
        /// コールバック
        /// 設定xのコンボボックス
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        private void noAspect_Click(object sender, RoutedEventArgs e)
        {
            noAspectClick();
        }

        private void noAspectClick()
        {
            if (ctrl_a)
            {
                foreach (object planet in ringCanvas.Children)
                {
                    FrameworkElement elem = (FrameworkElement)planet;
                    elem.Visibility = Visibility.Visible;
                }
                ctrl_a = false;
            }
            else
            {
                foreach (object planet in ringCanvas.Children)
                {
                    FrameworkElement elem = (FrameworkElement)planet;
                    if (elem.Tag is AspectInfo)
                    {
                        elem.Visibility = Visibility.Hidden;
                    }
                }
                ctrl_a = true;
            }
        }

        /// <summary>
        /// タイムセレクターコールバック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void natalTime_Checked(object sender, RoutedEventArgs e)
        {
            if (setYear == null)
            {
                // initialize前に呼ばれてしまうのでリターン
                return;
            }
            setYear.Text = targetUser.birth_year.ToString();
            setMonth.Text = targetUser.birth_month.ToString();
            setDay.Text = targetUser.birth_day.ToString();
            setHour.Text = targetUser.birth_hour.ToString();
            setMinute.Text = targetUser.birth_minute.ToString();
            setSecond.Text = targetUser.birth_second.ToString();
        }

        private void transitTime_Checked(object sender, RoutedEventArgs e)
        {
            setYear.Text = userdata.birth_year.ToString();
            setMonth.Text = userdata.birth_month.ToString();
            setDay.Text = userdata.birth_day.ToString();
            setHour.Text = userdata.birth_hour.ToString();
            setMinute.Text = userdata.birth_minute.ToString();
            setSecond.Text = userdata.birth_second.ToString();
        }

        private void timeSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (setYear == null)
            {
                // initialize前に呼ばれてしまうのでリターン
                return;
            }
            ComboBox item = (ComboBox)sender;
            int index = item.SelectedIndex;
            switch (index)
            {
                case 0:
                    // 1時間
                    unitYear.Text = "0";
                    unitMonth.Text = "0";
                    unitDay.Text = "0";
                    unitHour.Text = "1";
                    unitMinute.Text = "0";
                    unitSecond.Text = "0";
                    break;
                case 1:
                    // 1日
                    unitYear.Text = "0";
                    unitMonth.Text = "0";
                    unitDay.Text = "1";
                    unitHour.Text = "0";
                    unitMinute.Text = "0";
                    unitSecond.Text = "0";
                    break;
                case 2:
                    // 7日
                    unitYear.Text = "0";
                    unitMonth.Text = "0";
                    unitDay.Text = "7";
                    unitHour.Text = "0";
                    unitMinute.Text = "0";
                    unitSecond.Text = "0";
                    break;
                case 3:
                    // 30日
                    unitYear.Text = "0";
                    unitMonth.Text = "0";
                    unitDay.Text = "30";
                    unitHour.Text = "0";
                    unitMinute.Text = "0";
                    unitSecond.Text = "0";
                    break;
                case 4:
                    // 365日
                    unitYear.Text = "0";
                    unitMonth.Text = "0";
                    unitDay.Text = "365";
                    unitHour.Text = "0";
                    unitMinute.Text = "0";
                    unitSecond.Text = "0";
                    break;
                default:
                    break;
            }
        }

        private void nowButton_Click(object sender, RoutedEventArgs e)
        {
            setYear.Text = DateTime.Now.Year.ToString();
            setMonth.Text = DateTime.Now.Month.ToString();
            setDay.Text = DateTime.Now.Day.ToString();
            setHour.Text = DateTime.Now.Hour.ToString();
            setMinute.Text = DateTime.Now.Minute.ToString();
            setSecond.Text = DateTime.Now.Second.ToString();
        }

        private void setButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime dt = new DateTime(int.Parse(setYear.Text),
                int.Parse(setMonth.Text),
                int.Parse(setDay.Text),
                int.Parse(setHour.Text),
                int.Parse(setMinute.Text),
                int.Parse(setSecond.Text));

            if (natalTime.IsChecked == true)
            {
                targetUser.birth_year = dt.Year;
                targetUser.birth_month = dt.Month;
                targetUser.birth_day = dt.Day;
                targetUser.birth_hour = dt.Hour;
                targetUser.birth_minute = dt.Minute;
                targetUser.birth_second = dt.Second;
                targetUser.lat = double.Parse(setLat.Text);
                targetUser.lng = double.Parse(setLng.Text);
                mainWindowVM.userBirthStr = String.Format("{0:D4}", int.Parse(setYear.Text)) + "/" + String.Format("{0:D2}", int.Parse(setMonth.Text)) + "/" + String.Format("{0:D2}", int.Parse(setDay.Text)) + " " +
                    String.Format("{0:D2}", int.Parse(setHour.Text)) + ":" + String.Format("{0:D2}", int.Parse(setMinute.Text)) + ":" + String.Format("{0:D2}", int.Parse(setSecond.Text));
                mainWindowVM.userLat = setLat.Text;
                mainWindowVM.userLng = setLng.Text;
            }
            else
            {
                userdata.birth_year = dt.Year;
                userdata.birth_month = dt.Month;
                userdata.birth_day = dt.Day;
                userdata.birth_hour = dt.Hour;
                userdata.birth_minute = dt.Minute;
                userdata.birth_second = dt.Second;
                userdata.lat = double.Parse(setLat.Text);
                userdata.lng = double.Parse(setLng.Text);
                mainWindowVM.transitBirthStr = String.Format("{0:D4}", int.Parse(setYear.Text)) + "/" + String.Format("{0:D2}", int.Parse(setMonth.Text)) + "/" + String.Format("{0:D2}", int.Parse(setDay.Text)) + " " +
                    String.Format("{0:D2}", int.Parse(setHour.Text)) + ":" + String.Format("{0:D2}", int.Parse(setMinute.Text)) + ":" + String.Format("{0:D2}", int.Parse(setSecond.Text));
                mainWindowVM.transitLat = setLat.Text;
                mainWindowVM.transitLng = setLng.Text;
            }
            ReCalc();
            ReRender();
        }

        private void LeftYear_Click(object sender, RoutedEventArgs e)
        {
            int count = int.Parse(unitYear.Text);
            int year = int.Parse(setYear.Text);
            if (natalTime.IsChecked == true)
            {
                DateTime dt = new DateTime(int.Parse(setYear.Text),
                    int.Parse(setMonth.Text),
                    int.Parse(setDay.Text),
                    int.Parse(setHour.Text),
                    int.Parse(setMinute.Text),
                    int.Parse(setSecond.Text));
                DateTime newDt = dt.AddYears(-1 * count);
                setDay.Text = newDt.Day.ToString();
                setMonth.Text = newDt.Month.ToString();
                setYear.Text = newDt.Year.ToString();
                setHour.Text = newDt.Hour.ToString();
                setMinute.Text = newDt.Minute.ToString();
                setSecond.Text = newDt.Second.ToString();
                targetUser.birth_day = newDt.Day;
                targetUser.birth_month = newDt.Month;
                targetUser.birth_year = newDt.Year;
                mainWindowVM.userBirthStr = String.Format("{0:D4}", int.Parse(setYear.Text)) + "/" + String.Format("{0:D2}", int.Parse(setMonth.Text)) + "/" + String.Format("{0:D2}", int.Parse(setDay.Text)) + " " +
                    String.Format("{0:D2}", int.Parse(setHour.Text)) + ":" + String.Format("{0:D2}", int.Parse(setMinute.Text)) + ":" + String.Format("{0:D2}", int.Parse(setSecond.Text));
            }
            else
            {
                DateTime dt = new DateTime(int.Parse(setYear.Text),
                    int.Parse(setMonth.Text),
                    int.Parse(setDay.Text),
                    int.Parse(setHour.Text),
                    int.Parse(setMinute.Text),
                    int.Parse(setSecond.Text));
                DateTime newDt = dt.AddYears(-1 * count);
                setDay.Text = newDt.Day.ToString();
                setMonth.Text = newDt.Month.ToString();
                setYear.Text = newDt.Year.ToString();
                setHour.Text = newDt.Hour.ToString();
                setMinute.Text = newDt.Minute.ToString();
                setSecond.Text = newDt.Second.ToString();
                userdata.birth_day = newDt.Day;
                userdata.birth_month = newDt.Month;
                userdata.birth_year = newDt.Year;
                mainWindowVM.transitBirthStr = String.Format("{0:D4}", int.Parse(setYear.Text)) + "/" + String.Format("{0:D2}", int.Parse(setMonth.Text)) + "/" + String.Format("{0:D2}", int.Parse(setDay.Text)) + " " +
                    String.Format("{0:D2}", int.Parse(setHour.Text)) + ":" + String.Format("{0:D2}", int.Parse(setMinute.Text)) + ":" + String.Format("{0:D2}", int.Parse(setSecond.Text));
            }
            ReCalc();
            ReRender();
        }

        private void RightYear_Click(object sender, RoutedEventArgs e)
        {
            int count = int.Parse(unitYear.Text);
            int year = int.Parse(setYear.Text);
            if (natalTime.IsChecked == true)
            {
                DateTime dt = new DateTime(int.Parse(setYear.Text),
                    int.Parse(setMonth.Text),
                    int.Parse(setDay.Text),
                    int.Parse(setHour.Text),
                    int.Parse(setMinute.Text),
                    int.Parse(setSecond.Text));
                DateTime newDt = dt.AddYears(count);
                setDay.Text = newDt.Day.ToString();
                setMonth.Text = newDt.Month.ToString();
                setYear.Text = newDt.Year.ToString();
                setHour.Text = newDt.Hour.ToString();
                setMinute.Text = newDt.Minute.ToString();
                setSecond.Text = newDt.Second.ToString();
                targetUser.birth_day = newDt.Day;
                targetUser.birth_month = newDt.Month;
                targetUser.birth_year = newDt.Year;

                mainWindowVM.userBirthStr = String.Format("{0:D4}", int.Parse(setYear.Text)) + "/" + String.Format("{0:D2}", int.Parse(setMonth.Text)) + "/" + String.Format("{0:D2}", int.Parse(setDay.Text)) + " " +
                    String.Format("{0:D2}", int.Parse(setHour.Text)) + ":" + String.Format("{0:D2}", int.Parse(setMinute.Text)) + ":" + String.Format("{0:D2}", int.Parse(setSecond.Text));
            }
            else
            {
                DateTime dt = new DateTime(int.Parse(setYear.Text),
                    int.Parse(setMonth.Text),
                    int.Parse(setDay.Text),
                    int.Parse(setHour.Text),
                    int.Parse(setMinute.Text),
                    int.Parse(setSecond.Text));
                DateTime newDt = dt.AddYears(count);
                setDay.Text = newDt.Day.ToString();
                setMonth.Text = newDt.Month.ToString();
                setYear.Text = newDt.Year.ToString();
                setHour.Text = newDt.Hour.ToString();
                setMinute.Text = newDt.Minute.ToString();
                setSecond.Text = newDt.Second.ToString();
                userdata.birth_day = newDt.Day;
                userdata.birth_month = newDt.Month;
                userdata.birth_year = newDt.Year;

                mainWindowVM.transitBirthStr = String.Format("{0:D4}", int.Parse(setYear.Text)) + "/" + String.Format("{0:D2}", int.Parse(setMonth.Text)) + "/" + String.Format("{0:D2}", int.Parse(setDay.Text)) + " " +
                    String.Format("{0:D2}", int.Parse(setHour.Text)) + ":" + String.Format("{0:D2}", int.Parse(setMinute.Text)) + ":" + String.Format("{0:D2}", int.Parse(setSecond.Text));
            }
            ReCalc();
            ReRender();
        }

        private void LeftMonth_Click(object sender, RoutedEventArgs e)
        {
            int count = int.Parse(unitMonth.Text);
            int month = int.Parse(setMonth.Text);
            if (natalTime.IsChecked == true)
            {
                DateTime dt = new DateTime(int.Parse(setYear.Text),
                    int.Parse(setMonth.Text),
                    int.Parse(setDay.Text),
                    int.Parse(setHour.Text),
                    int.Parse(setMinute.Text),
                    int.Parse(setSecond.Text));
                DateTime newDt = dt.AddMonths(-1 * count);
                setDay.Text = newDt.Day.ToString();
                setMonth.Text = newDt.Month.ToString();
                setYear.Text = newDt.Year.ToString();
                targetUser.birth_day = newDt.Day;
                targetUser.birth_month = newDt.Month;
                targetUser.birth_year = newDt.Year;
                mainWindowVM.userBirthStr = String.Format("{0:D4}", int.Parse(setYear.Text)) + "/" + String.Format("{0:D2}", int.Parse(setMonth.Text)) + "/" + String.Format("{0:D2}", int.Parse(setDay.Text)) + " " +
                    String.Format("{0:D2}", int.Parse(setHour.Text)) + ":" + String.Format("{0:D2}", int.Parse(setMinute.Text)) + ":" + String.Format("{0:D2}", int.Parse(setSecond.Text));
            }
            else
            {
                DateTime dt = new DateTime(int.Parse(setYear.Text),
                    int.Parse(setMonth.Text),
                    int.Parse(setDay.Text),
                    int.Parse(setHour.Text),
                    int.Parse(setMinute.Text),
                    int.Parse(setSecond.Text));
                DateTime newDt = dt.AddMonths(-1 * count);
                setDay.Text = newDt.Day.ToString();
                setMonth.Text = newDt.Month.ToString();
                setYear.Text = newDt.Year.ToString();
                userdata.birth_day = newDt.Day;
                userdata.birth_month = newDt.Month;
                userdata.birth_year = newDt.Year;
                mainWindowVM.transitBirthStr = String.Format("{0:D4}", int.Parse(setYear.Text)) + "/" + String.Format("{0:D2}", int.Parse(setMonth.Text)) + "/" + String.Format("{0:D2}", int.Parse(setDay.Text)) + " " +
                    String.Format("{0:D2}", int.Parse(setHour.Text)) + ":" + String.Format("{0:D2}", int.Parse(setMinute.Text)) + ":" + String.Format("{0:D2}", int.Parse(setSecond.Text));
            }
            ReCalc();
            ReRender();
        }

        private void RightMonth_Click(object sender, RoutedEventArgs e)
        {
            int count = int.Parse(unitMonth.Text);
            if (natalTime.IsChecked == true)
            {
                DateTime dt = new DateTime(int.Parse(setYear.Text),
                    int.Parse(setMonth.Text),
                    int.Parse(setDay.Text),
                    int.Parse(setHour.Text),
                    int.Parse(setMinute.Text),
                    int.Parse(setSecond.Text));
                DateTime newDt = dt.AddMonths(count);
                setMonth.Text = newDt.Month.ToString();
                setYear.Text = newDt.Year.ToString();
                targetUser.birth_month = newDt.Month;
                targetUser.birth_year = newDt.Year;
                mainWindowVM.userBirthStr = String.Format("{0:D4}", int.Parse(setYear.Text)) + "/" + String.Format("{0:D2}", int.Parse(setMonth.Text)) + "/" + String.Format("{0:D2}", int.Parse(setDay.Text)) + " " +
                    String.Format("{0:D2}", int.Parse(setHour.Text)) + ":" + String.Format("{0:D2}", int.Parse(setMinute.Text)) + ":" + String.Format("{0:D2}", int.Parse(setSecond.Text));
            }
            else
            {
                DateTime dt = new DateTime(int.Parse(setYear.Text),
                    int.Parse(setMonth.Text),
                    int.Parse(setDay.Text),
                    int.Parse(setHour.Text),
                    int.Parse(setMinute.Text),
                    int.Parse(setSecond.Text));
                DateTime newDt = dt.AddMonths(count);
                setMonth.Text = newDt.Month.ToString();
                setYear.Text = newDt.Year.ToString();
                userdata.birth_month = newDt.Month;
                userdata.birth_year = newDt.Year;
                mainWindowVM.transitBirthStr = String.Format("{0:D4}", int.Parse(setYear.Text)) + "/" + String.Format("{0:D2}", int.Parse(setMonth.Text)) + "/" + String.Format("{0:D2}", int.Parse(setDay.Text)) + " " +
                    String.Format("{0:D2}", int.Parse(setHour.Text)) + ":" + String.Format("{0:D2}", int.Parse(setMinute.Text)) + ":" + String.Format("{0:D2}", int.Parse(setSecond.Text));
            }
            ReCalc();
            ReRender();
        }

        private void LeftDay_Click(object sender, RoutedEventArgs e)
        {
            int count = int.Parse(unitDay.Text);
            int month = int.Parse(setDay.Text);
            if (natalTime.IsChecked == true)
            {
                DateTime dt = new DateTime(int.Parse(setYear.Text),
                    int.Parse(setMonth.Text),
                    int.Parse(setDay.Text),
                    int.Parse(setHour.Text),
                    int.Parse(setMinute.Text),
                    int.Parse(setSecond.Text));
                DateTime newDt = dt.AddDays(-1 * count);
                setDay.Text = newDt.Day.ToString();
                setMonth.Text = newDt.Month.ToString();
                setYear.Text = newDt.Year.ToString();
                targetUser.birth_day = newDt.Day;
                targetUser.birth_month = newDt.Month;
                targetUser.birth_year = newDt.Year;
                mainWindowVM.userBirthStr = String.Format("{0:D4}", int.Parse(setYear.Text)) + "/" + String.Format("{0:D2}", int.Parse(setMonth.Text)) + "/" + String.Format("{0:D2}", int.Parse(setDay.Text)) + " " +
                    String.Format("{0:D2}", int.Parse(setHour.Text)) + ":" + String.Format("{0:D2}", int.Parse(setMinute.Text)) + ":" + String.Format("{0:D2}", int.Parse(setSecond.Text));
            }
            else
            {
                DateTime dt = new DateTime(int.Parse(setYear.Text),
                    int.Parse(setMonth.Text),
                    int.Parse(setDay.Text),
                    int.Parse(setHour.Text),
                    int.Parse(setMinute.Text),
                    int.Parse(setSecond.Text));
                DateTime newDt = dt.AddDays(-1 * count);
                setDay.Text = newDt.Day.ToString();
                setMonth.Text = newDt.Month.ToString();
                setYear.Text = newDt.Year.ToString();
                userdata.birth_day = newDt.Day;
                userdata.birth_month = newDt.Month;
                userdata.birth_year = newDt.Year;
                mainWindowVM.transitBirthStr = String.Format("{0:D4}", int.Parse(setYear.Text)) + "/" + String.Format("{0:D2}", int.Parse(setMonth.Text)) + "/" + String.Format("{0:D2}", int.Parse(setDay.Text)) + " " +
                    String.Format("{0:D2}", int.Parse(setHour.Text)) + ":" + String.Format("{0:D2}", int.Parse(setMinute.Text)) + ":" + String.Format("{0:D2}", int.Parse(setSecond.Text));
            }
            ReCalc();
            ReRender();
        }

        private void RightDay_Click(object sender, RoutedEventArgs e)
        {
            int count = int.Parse(unitDay.Text);
            int day = int.Parse(setDay.Text);
            if (natalTime.IsChecked == true)
            {
                DateTime dt = new DateTime(int.Parse(setYear.Text),
                    int.Parse(setMonth.Text),
                    int.Parse(setDay.Text),
                    int.Parse(setHour.Text),
                    int.Parse(setMinute.Text),
                    int.Parse(setSecond.Text));
                DateTime newDt = dt.AddDays(count);
                setDay.Text = newDt.Day.ToString();
                setMonth.Text = newDt.Month.ToString();
                setYear.Text = newDt.Year.ToString();
                targetUser.birth_day = newDt.Day;
                targetUser.birth_month = newDt.Month;
                targetUser.birth_year = newDt.Year;
                mainWindowVM.userBirthStr = String.Format("{0:D4}", int.Parse(setYear.Text)) + "/" + String.Format("{0:D2}", int.Parse(setMonth.Text)) + "/" + String.Format("{0:D2}", int.Parse(setDay.Text)) + " " +
                    String.Format("{0:D2}", int.Parse(setHour.Text)) + ":" + String.Format("{0:D2}", int.Parse(setMinute.Text)) + ":" + String.Format("{0:D2}", int.Parse(setSecond.Text));
            }
            else
            {
                DateTime dt = new DateTime(int.Parse(setYear.Text),
                    int.Parse(setMonth.Text),
                    int.Parse(setDay.Text),
                    int.Parse(setHour.Text),
                    int.Parse(setMinute.Text),
                    int.Parse(setSecond.Text));
                DateTime newDt = dt.AddDays(count);
                setDay.Text = newDt.Day.ToString();
                setMonth.Text = newDt.Month.ToString();
                setYear.Text = newDt.Year.ToString();
                userdata.birth_day = newDt.Day;
                userdata.birth_month = newDt.Month;
                userdata.birth_year = newDt.Year;
                mainWindowVM.transitBirthStr = String.Format("{0:D4}", int.Parse(setYear.Text)) + "/" + String.Format("{0:D2}", int.Parse(setMonth.Text)) + "/" + String.Format("{0:D2}", int.Parse(setDay.Text)) + " " +
                    String.Format("{0:D2}", int.Parse(setHour.Text)) + ":" + String.Format("{0:D2}", int.Parse(setMinute.Text)) + ":" + String.Format("{0:D2}", int.Parse(setSecond.Text));
            }
            ReCalc();
            ReRender();
        }

        private void LeftHour_Click(object sender, RoutedEventArgs e)
        {
            int count = int.Parse(unitHour.Text);
            int hour = int.Parse(setHour.Text);
            if (natalTime.IsChecked == true)
            {
                DateTime dt = new DateTime(int.Parse(setYear.Text),
                    int.Parse(setMonth.Text),
                    int.Parse(setDay.Text),
                    int.Parse(setHour.Text),
                    int.Parse(setMinute.Text),
                    int.Parse(setSecond.Text));
                DateTime newDt = dt.AddHours(-1 * count);
                setDay.Text = newDt.Day.ToString();
                setMonth.Text = newDt.Month.ToString();
                setYear.Text = newDt.Year.ToString();
                setHour.Text = newDt.Hour.ToString();
                targetUser.birth_day = newDt.Day;
                targetUser.birth_month = newDt.Month;
                targetUser.birth_year = newDt.Year;
                targetUser.birth_hour = newDt.Hour;
                mainWindowVM.userBirthStr = String.Format("{0:D4}", int.Parse(setYear.Text)) + "/" + String.Format("{0:D2}", int.Parse(setMonth.Text)) + "/" + String.Format("{0:D2}", int.Parse(setDay.Text)) + " " +
                    String.Format("{0:D2}", int.Parse(setHour.Text)) + ":" + String.Format("{0:D2}", int.Parse(setMinute.Text)) + ":" + String.Format("{0:D2}", int.Parse(setSecond.Text));
            }
            else
            {
                DateTime dt = new DateTime(int.Parse(setYear.Text),
                    int.Parse(setMonth.Text),
                    int.Parse(setDay.Text),
                    int.Parse(setHour.Text),
                    int.Parse(setMinute.Text),
                    int.Parse(setSecond.Text));
                DateTime newDt = dt.AddHours(-1 * count);
                setDay.Text = newDt.Day.ToString();
                setMonth.Text = newDt.Month.ToString();
                setYear.Text = newDt.Year.ToString();
                setHour.Text = newDt.Hour.ToString();
                userdata.birth_day = newDt.Day;
                userdata.birth_month = newDt.Month;
                userdata.birth_year = newDt.Year;
                userdata.birth_hour = newDt.Hour;
                mainWindowVM.transitBirthStr = String.Format("{0:D4}", int.Parse(setYear.Text)) + "/" + String.Format("{0:D2}", int.Parse(setMonth.Text)) + "/" + String.Format("{0:D2}", int.Parse(setDay.Text)) + " " +
                    String.Format("{0:D2}", int.Parse(setHour.Text)) + ":" + String.Format("{0:D2}", int.Parse(setMinute.Text)) + ":" + String.Format("{0:D2}", int.Parse(setSecond.Text));
            }
            ReCalc();
            ReRender();
        }

        private void RightHour_Click(object sender, RoutedEventArgs e)
        {
            int count = int.Parse(unitHour.Text);
            int hour = int.Parse(setHour.Text);
            if (natalTime.IsChecked == true)
            {
                DateTime dt = new DateTime(int.Parse(setYear.Text),
                    int.Parse(setMonth.Text),
                    int.Parse(setDay.Text),
                    int.Parse(setHour.Text),
                    int.Parse(setMinute.Text),
                    int.Parse(setSecond.Text));
                DateTime newDt = dt.AddHours(count);
                setDay.Text = newDt.Day.ToString();
                setMonth.Text = newDt.Month.ToString();
                setYear.Text = newDt.Year.ToString();
                setHour.Text = newDt.Hour.ToString();
                setMinute.Text = newDt.Minute.ToString();
                setSecond.Text = newDt.Second.ToString();
                targetUser.birth_second = newDt.Second;
                targetUser.birth_minute = newDt.Minute;
                targetUser.birth_hour = newDt.Hour;
                targetUser.birth_day = newDt.Day;
                targetUser.birth_month = newDt.Month;
                targetUser.birth_year = newDt.Year;
                mainWindowVM.userBirthStr = String.Format("{0:D4}", int.Parse(setYear.Text)) + "/" + String.Format("{0:D2}", int.Parse(setMonth.Text)) + "/" + String.Format("{0:D2}", int.Parse(setDay.Text)) + " " +
                    String.Format("{0:D2}", int.Parse(setHour.Text)) + ":" + String.Format("{0:D2}", int.Parse(setMinute.Text)) + ":" + String.Format("{0:D2}", int.Parse(setSecond.Text));
            }
            else
            {
                DateTime dt = new DateTime(int.Parse(setYear.Text),
                    int.Parse(setMonth.Text),
                    int.Parse(setDay.Text),
                    int.Parse(setHour.Text),
                    int.Parse(setMinute.Text),
                    int.Parse(setSecond.Text));
                DateTime newDt = dt.AddHours(count);
                setDay.Text = newDt.Day.ToString();
                setMonth.Text = newDt.Month.ToString();
                setYear.Text = newDt.Year.ToString();
                setHour.Text = newDt.Hour.ToString();
                setMinute.Text = newDt.Minute.ToString();
                setSecond.Text = newDt.Second.ToString();
                userdata.birth_second = newDt.Second;
                userdata.birth_minute = newDt.Minute;
                userdata.birth_hour = newDt.Hour;
                userdata.birth_day = newDt.Day;
                userdata.birth_month = newDt.Month;
                userdata.birth_year = newDt.Year;
                mainWindowVM.transitBirthStr = String.Format("{0:D4}", int.Parse(setYear.Text)) + "/" + String.Format("{0:D2}", int.Parse(setMonth.Text)) + "/" + String.Format("{0:D2}", int.Parse(setDay.Text)) + " " +
                    String.Format("{0:D2}", int.Parse(setHour.Text)) + ":" + String.Format("{0:D2}", int.Parse(setMinute.Text)) + ":" + String.Format("{0:D2}", int.Parse(setSecond.Text));
            }
            ReCalc();
            ReRender();
        }

        private void LeftMinute_Click(object sender, RoutedEventArgs e)
        {
            int count = int.Parse(unitMinute.Text);
            int minute = int.Parse(setMinute.Text);
            if (natalTime.IsChecked == true)
            {
                DateTime dt = new DateTime(int.Parse(setYear.Text),
                    int.Parse(setMonth.Text),
                    int.Parse(setDay.Text),
                    int.Parse(setHour.Text),
                    int.Parse(setMinute.Text),
                    int.Parse(setSecond.Text));
                DateTime newDt = dt.AddMinutes(-1 * count);
                setDay.Text = newDt.Day.ToString();
                setMonth.Text = newDt.Month.ToString();
                setYear.Text = newDt.Year.ToString();
                setHour.Text = newDt.Hour.ToString();
                setMinute.Text = newDt.Minute.ToString();
                setSecond.Text = newDt.Second.ToString();
                targetUser.birth_second = newDt.Second;
                targetUser.birth_minute = newDt.Minute;
                targetUser.birth_hour = newDt.Hour;
                targetUser.birth_day = newDt.Day;
                targetUser.birth_month = newDt.Month;
                targetUser.birth_year = newDt.Year;
                mainWindowVM.userBirthStr = String.Format("{0:D4}", int.Parse(setYear.Text)) + "/" + String.Format("{0:D2}", int.Parse(setMonth.Text)) + "/" + String.Format("{0:D2}", int.Parse(setDay.Text)) + " " +
                    String.Format("{0:D2}", int.Parse(setHour.Text)) + ":" + String.Format("{0:D2}", int.Parse(setMinute.Text)) + ":" + String.Format("{0:D2}", int.Parse(setSecond.Text));
            }
            else
            {
                DateTime dt = new DateTime(int.Parse(setYear.Text),
                    int.Parse(setMonth.Text),
                    int.Parse(setDay.Text),
                    int.Parse(setHour.Text),
                    int.Parse(setMinute.Text),
                    int.Parse(setSecond.Text));
                DateTime newDt = dt.AddMinutes(-1 * count);
                setDay.Text = newDt.Day.ToString();
                setMonth.Text = newDt.Month.ToString();
                setYear.Text = newDt.Year.ToString();
                setHour.Text = newDt.Hour.ToString();
                setMinute.Text = newDt.Minute.ToString();
                setSecond.Text = newDt.Second.ToString();
                userdata.birth_second = newDt.Second;
                userdata.birth_minute = newDt.Minute;
                userdata.birth_hour = newDt.Hour;
                userdata.birth_day = newDt.Day;
                userdata.birth_month = newDt.Month;
                userdata.birth_year = newDt.Year;
                mainWindowVM.transitBirthStr = String.Format("{0:D4}", int.Parse(setYear.Text)) + "/" + String.Format("{0:D2}", int.Parse(setMonth.Text)) + "/" + String.Format("{0:D2}", int.Parse(setDay.Text)) + " " +
                    String.Format("{0:D2}", int.Parse(setHour.Text)) + ":" + String.Format("{0:D2}", int.Parse(setMinute.Text)) + ":" + String.Format("{0:D2}", int.Parse(setSecond.Text));
            }
            ReCalc();
            ReRender();
        }

        private void RightMinute_Click(object sender, RoutedEventArgs e)
        {
            int count = int.Parse(unitMinute.Text);
            int minute = int.Parse(setMinute.Text);
            if (natalTime.IsChecked == true)
            {
                DateTime dt = new DateTime(int.Parse(setYear.Text),
                    int.Parse(setMonth.Text),
                    int.Parse(setDay.Text),
                    int.Parse(setHour.Text),
                    int.Parse(setMinute.Text),
                    int.Parse(setSecond.Text));
                DateTime newDt = dt.AddMinutes(count);
                setDay.Text = newDt.Day.ToString();
                setMonth.Text = newDt.Month.ToString();
                setYear.Text = newDt.Year.ToString();
                setHour.Text = newDt.Hour.ToString();
                setMinute.Text = newDt.Minute.ToString();
                setSecond.Text = newDt.Second.ToString();
                targetUser.birth_second = newDt.Second;
                targetUser.birth_minute = newDt.Minute;
                targetUser.birth_hour = newDt.Hour;
                targetUser.birth_day = newDt.Day;
                targetUser.birth_month = newDt.Month;
                targetUser.birth_year = newDt.Year;
                mainWindowVM.userBirthStr = String.Format("{0:D4}", int.Parse(setYear.Text)) + "/" + String.Format("{0:D2}", int.Parse(setMonth.Text)) + "/" + String.Format("{0:D2}", int.Parse(setDay.Text)) + " " +
                    String.Format("{0:D2}", int.Parse(setHour.Text)) + ":" + String.Format("{0:D2}", int.Parse(setMinute.Text)) + ":" + String.Format("{0:D2}", int.Parse(setSecond.Text));
            }
            else
            {
                DateTime dt = new DateTime(int.Parse(setYear.Text),
                    int.Parse(setMonth.Text),
                    int.Parse(setDay.Text),
                    int.Parse(setHour.Text),
                    int.Parse(setMinute.Text),
                    int.Parse(setSecond.Text));
                DateTime newDt = dt.AddMinutes(count);
                setDay.Text = newDt.Day.ToString();
                setMonth.Text = newDt.Month.ToString();
                setYear.Text = newDt.Year.ToString();
                setHour.Text = newDt.Hour.ToString();
                setMinute.Text = newDt.Minute.ToString();
                setSecond.Text = newDt.Second.ToString();
                userdata.birth_second = newDt.Second;
                userdata.birth_minute = newDt.Minute;
                userdata.birth_hour = newDt.Hour;
                userdata.birth_day = newDt.Day;
                userdata.birth_month = newDt.Month;
                userdata.birth_year = newDt.Year;
                mainWindowVM.transitBirthStr = String.Format("{0:D4}", int.Parse(setYear.Text)) + "/" + String.Format("{0:D2}", int.Parse(setMonth.Text)) + "/" + String.Format("{0:D2}", int.Parse(setDay.Text)) + " " +
                    String.Format("{0:D2}", int.Parse(setHour.Text)) + ":" + String.Format("{0:D2}", int.Parse(setMinute.Text)) + ":" + String.Format("{0:D2}", int.Parse(setSecond.Text));
            }
            ReCalc();
            ReRender();
        }

        private void LeftSecond_Click(object sender, RoutedEventArgs e)
        {
            int count = int.Parse(unitSecond.Text);
            int second = int.Parse(setSecond.Text);
            if (natalTime.IsChecked == true)
            {
                DateTime dt = new DateTime(int.Parse(setYear.Text),
                    int.Parse(setMonth.Text),
                    int.Parse(setDay.Text),
                    int.Parse(setHour.Text),
                    int.Parse(setMinute.Text),
                    int.Parse(setSecond.Text));
                DateTime newDt = dt.AddSeconds(-1 * count);
                setDay.Text = newDt.Day.ToString();
                setMonth.Text = newDt.Month.ToString();
                setYear.Text = newDt.Year.ToString();
                setHour.Text = newDt.Hour.ToString();
                setMinute.Text = newDt.Minute.ToString();
                setSecond.Text = newDt.Second.ToString();
                targetUser.birth_second = newDt.Second;
                targetUser.birth_minute = newDt.Minute;
                targetUser.birth_hour = newDt.Hour;
                targetUser.birth_day = newDt.Day;
                targetUser.birth_month = newDt.Month;
                targetUser.birth_year = newDt.Year;
                mainWindowVM.userBirthStr = String.Format("{0:D4}", int.Parse(setYear.Text)) + "/" + String.Format("{0:D2}", int.Parse(setMonth.Text)) + "/" + String.Format("{0:D2}", int.Parse(setDay.Text)) + " " +
                    String.Format("{0:D2}", int.Parse(setHour.Text)) + ":" + String.Format("{0:D2}", int.Parse(setMinute.Text)) + ":" + String.Format("{0:D2}", int.Parse(setSecond.Text));
            }
            else
            {
                DateTime dt = new DateTime(int.Parse(setYear.Text),
                    int.Parse(setMonth.Text),
                    int.Parse(setDay.Text),
                    int.Parse(setHour.Text),
                    int.Parse(setMinute.Text),
                    int.Parse(setSecond.Text));
                DateTime newDt = dt.AddSeconds(-1 * count);
                setDay.Text = newDt.Day.ToString();
                setMonth.Text = newDt.Month.ToString();
                setYear.Text = newDt.Year.ToString();
                setHour.Text = newDt.Hour.ToString();
                setMinute.Text = newDt.Minute.ToString();
                setSecond.Text = newDt.Second.ToString();
                userdata.birth_second = newDt.Second;
                userdata.birth_minute = newDt.Minute;
                userdata.birth_hour = newDt.Hour;
                userdata.birth_day = newDt.Day;
                userdata.birth_month = newDt.Month;
                userdata.birth_year = newDt.Year;
                mainWindowVM.transitBirthStr = String.Format("{0:D4}", int.Parse(setYear.Text)) + "/" + String.Format("{0:D2}", int.Parse(setMonth.Text)) + "/" + String.Format("{0:D2}", int.Parse(setDay.Text)) + " " +
                    String.Format("{0:D2}", int.Parse(setHour.Text)) + ":" + String.Format("{0:D2}", int.Parse(setMinute.Text)) + ":" + String.Format("{0:D2}", int.Parse(setSecond.Text));
            }
            ReCalc();
            ReRender();
        }

        private void RightSecond_Click(object sender, RoutedEventArgs e)
        {
            int count = int.Parse(unitSecond.Text);
            int second = int.Parse(setSecond.Text);
            if (natalTime.IsChecked == true)
            {
                DateTime dt = new DateTime(int.Parse(setYear.Text),
                    int.Parse(setMonth.Text),
                    int.Parse(setDay.Text),
                    int.Parse(setHour.Text),
                    int.Parse(setMinute.Text),
                    int.Parse(setSecond.Text));
                DateTime newDt = dt.AddSeconds(count);
                setDay.Text = newDt.Day.ToString();
                setMonth.Text = newDt.Month.ToString();
                setYear.Text = newDt.Year.ToString();
                setHour.Text = newDt.Hour.ToString();
                setMinute.Text = newDt.Minute.ToString();
                setSecond.Text = newDt.Second.ToString();
                targetUser.birth_second = newDt.Second;
                targetUser.birth_minute = newDt.Minute;
                targetUser.birth_hour = newDt.Hour;
                targetUser.birth_day = newDt.Day;
                targetUser.birth_month = newDt.Month;
                targetUser.birth_year = newDt.Year;
                mainWindowVM.userBirthStr = String.Format("{0:D4}", int.Parse(setYear.Text)) + "/" + String.Format("{0:D2}", int.Parse(setMonth.Text)) + "/" + String.Format("{0:D2}", int.Parse(setDay.Text)) + " " +
                    String.Format("{0:D2}", int.Parse(setHour.Text)) + ":" + String.Format("{0:D2}", int.Parse(setMinute.Text)) + ":" + String.Format("{0:D2}", int.Parse(setSecond.Text));
            }
            else
            {
                DateTime dt = new DateTime(int.Parse(setYear.Text),
                    int.Parse(setMonth.Text),
                    int.Parse(setDay.Text),
                    int.Parse(setHour.Text),
                    int.Parse(setMinute.Text),
                    int.Parse(setSecond.Text));
                DateTime newDt = dt.AddSeconds(count);
                setDay.Text = newDt.Day.ToString();
                setMonth.Text = newDt.Month.ToString();
                setYear.Text = newDt.Year.ToString();
                setHour.Text = newDt.Hour.ToString();
                setMinute.Text = newDt.Minute.ToString();
                setSecond.Text = newDt.Second.ToString();
                userdata.birth_second = newDt.Second;
                userdata.birth_minute = newDt.Minute;
                userdata.birth_hour = newDt.Hour;
                userdata.birth_day = newDt.Day;
                userdata.birth_month = newDt.Month;
                userdata.birth_year = newDt.Year;
                mainWindowVM.transitBirthStr = String.Format("{0:D4}", int.Parse(setYear.Text)) + "/" + String.Format("{0:D2}", int.Parse(setMonth.Text)) + "/" + String.Format("{0:D2}", int.Parse(setDay.Text)) + " " +
                    String.Format("{0:D2}", int.Parse(setHour.Text)) + ":" + String.Format("{0:D2}", int.Parse(setMinute.Text)) + ":" + String.Format("{0:D2}", int.Parse(setSecond.Text));
            }
            ReCalc();
            ReRender();
        }

        private void GotKeyboardFocusCommon(object sender, KeyboardFocusChangedEventArgs e)
        {
            TextBox item = (TextBox)sender;
            item.SelectAll();
        }

        private void LeftChange_Click(object sender, RoutedEventArgs e)
        {
            int yearCount = int.Parse(unitYear.Text);
            int monthCount = int.Parse(unitMonth.Text);
            int dayCount = int.Parse(unitDay.Text);
            int hourCount = int.Parse(unitHour.Text);
            int minuteCount = int.Parse(unitMinute.Text);
            int secondCount = int.Parse(unitSecond.Text);
            int year = int.Parse(setYear.Text);
            int month = int.Parse(setMonth.Text);
            int day = int.Parse(setDay.Text);
            int hour = int.Parse(setHour.Text);
            int minute = int.Parse(setMinute.Text);
            int second = int.Parse(setSecond.Text);
            DateTime dt = new DateTime(int.Parse(setYear.Text),
                int.Parse(setMonth.Text),
                int.Parse(setDay.Text),
                int.Parse(setHour.Text),
                int.Parse(setMinute.Text),
                int.Parse(setSecond.Text));
            DateTime newDt = dt.AddSeconds(-1 * secondCount);
            newDt = newDt.AddMinutes(-1 * minuteCount);
            newDt = newDt.AddHours(-1 * hourCount);
            newDt = newDt.AddDays(-1 * dayCount);
            newDt = newDt.AddMonths(-1 * monthCount);
            newDt = newDt.AddYears(-1 * yearCount);
            setDay.Text = newDt.Day.ToString();
            setMonth.Text = newDt.Month.ToString();
            setYear.Text = newDt.Year.ToString();
            setHour.Text = newDt.Hour.ToString();
            setMinute.Text = newDt.Minute.ToString();
            setSecond.Text = newDt.Second.ToString();

            if (natalTime.IsChecked == true)
            {
                targetUser.birth_year = newDt.Year;
                targetUser.birth_month = newDt.Month;
                targetUser.birth_day = newDt.Day;
                targetUser.birth_hour = newDt.Month;
                targetUser.birth_minute = newDt.Minute;
                targetUser.birth_second = newDt.Second;
                mainWindowVM.userBirthStr = String.Format("{0:D4}", int.Parse(setYear.Text)) + "/" + String.Format("{0:D2}", int.Parse(setMonth.Text)) + "/" + String.Format("{0:D2}", int.Parse(setDay.Text)) + " " +
                    String.Format("{0:D2}", int.Parse(setHour.Text)) + ":" + String.Format("{0:D2}", int.Parse(setMinute.Text)) + ":" + String.Format("{0:D2}", int.Parse(setSecond.Text));
            }
            else
            {
                userdata.birth_year = newDt.Year;
                userdata.birth_month = newDt.Month;
                userdata.birth_day = newDt.Day;
                userdata.birth_hour = newDt.Month;
                userdata.birth_minute = newDt.Minute;
                userdata.birth_second = newDt.Second;

                mainWindowVM.transitBirthStr = String.Format("{0:D4}", int.Parse(setYear.Text)) + "/" + String.Format("{0:D2}", int.Parse(setMonth.Text)) + "/" + String.Format("{0:D2}", int.Parse(setDay.Text)) + " " +
                    String.Format("{0:D2}", int.Parse(setHour.Text)) + ":" + String.Format("{0:D2}", int.Parse(setMinute.Text)) + ":" + String.Format("{0:D2}", int.Parse(setSecond.Text));
            }
            ReCalc();
            ReRender();
        }

        private void RightChange_Click(object sender, RoutedEventArgs e)
        {
            int yearCount = int.Parse(unitYear.Text);
            int monthCount = int.Parse(unitMonth.Text);
            int dayCount = int.Parse(unitDay.Text);
            int hourCount = int.Parse(unitHour.Text);
            int minuteCount = int.Parse(unitMinute.Text);
            int secondCount = int.Parse(unitSecond.Text);
            int year = int.Parse(setYear.Text);
            int month = int.Parse(setMonth.Text);
            int day = int.Parse(setDay.Text);
            int hour = int.Parse(setHour.Text);
            int minute = int.Parse(setMinute.Text);
            int second = int.Parse(setSecond.Text);
            DateTime dt = new DateTime(int.Parse(setYear.Text),
                int.Parse(setMonth.Text),
                int.Parse(setDay.Text),
                int.Parse(setHour.Text),
                int.Parse(setMinute.Text),
                int.Parse(setSecond.Text));
            DateTime newDt = dt.AddSeconds(secondCount);
            newDt = newDt.AddMinutes(minuteCount);
            newDt = newDt.AddHours(hourCount);
            newDt = newDt.AddDays(dayCount);
            newDt = newDt.AddMonths(monthCount);
            newDt = newDt.AddYears(yearCount);
            setDay.Text = newDt.Day.ToString();
            setMonth.Text = newDt.Month.ToString();
            setYear.Text = newDt.Year.ToString();
            setHour.Text = newDt.Hour.ToString();
            setMinute.Text = newDt.Minute.ToString();
            setSecond.Text = newDt.Second.ToString();


            if (natalTime.IsChecked == true)
            {
                targetUser.birth_year = newDt.Year;
                targetUser.birth_month = newDt.Month;
                targetUser.birth_day = newDt.Day;
                targetUser.birth_hour = newDt.Month;
                targetUser.birth_minute = newDt.Minute;
                targetUser.birth_second = newDt.Second;
                mainWindowVM.userBirthStr = String.Format("{0:D4}", int.Parse(setYear.Text)) + "/" + String.Format("{0:D2}", int.Parse(setMonth.Text)) + "/" + String.Format("{0:D2}", int.Parse(setDay.Text)) + " " +
                    String.Format("{0:D2}", int.Parse(setHour.Text)) + ":" + String.Format("{0:D2}", int.Parse(setMinute.Text)) + ":" + String.Format("{0:D2}", int.Parse(setSecond.Text));
            }
            else
            {
                userdata.birth_year = newDt.Year;
                userdata.birth_month = newDt.Month;
                userdata.birth_day = newDt.Day;
                userdata.birth_hour = newDt.Month;
                userdata.birth_minute = newDt.Minute;
                userdata.birth_second = newDt.Second;

                mainWindowVM.transitBirthStr = String.Format("{0:D4}", int.Parse(setYear.Text)) + "/" + String.Format("{0:D2}", int.Parse(setMonth.Text)) + "/" + String.Format("{0:D2}", int.Parse(setDay.Text)) + " " +
                    String.Format("{0:D2}", int.Parse(setHour.Text)) + ":" + String.Format("{0:D2}", int.Parse(setMinute.Text)) + ":" + String.Format("{0:D2}", int.Parse(setSecond.Text));
            }
            ReCalc();
            ReRender();
        }

        /// <summary>
        /// イベントインフォ(右上枠)　イベント１クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void changeButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindowVM.transitName = userdata.name;
            mainWindowVM.transitBirthStr = String.Format("{0}/{1:D2}/{2:D2} {3:D2}:{4:D2}:{5:D2}", userdata.birth_year,
                userdata.birth_month, userdata.birth_day, userdata.birth_hour, userdata.birth_minute, userdata.birth_second);
            mainWindowVM.transitTimezone = userdata.timezone;
            mainWindowVM.transitPlace = userdata.birth_place;
            mainWindowVM.transitLat = String.Format("{0:f6}", userdata.lat);
            mainWindowVM.transitLng = String.Format("{0:f6}", userdata.lng);
            currentTarget = 1;
        }

        /// <summary>
        /// イベントインフォ(右上枠)　イベント２クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void event2Button_Click(object sender, RoutedEventArgs e)
        {
            mainWindowVM.transitName = userdata2.name;
            mainWindowVM.transitBirthStr = String.Format("{0}/{1:D2}/{2:D2} {3:D2}:{4:D2}:{5:D2}", userdata2.birth_year,
                userdata2.birth_month, userdata2.birth_day, userdata2.birth_hour, userdata2.birth_minute, userdata2.birth_second);
            mainWindowVM.transitTimezone = userdata2.timezone;
            mainWindowVM.transitPlace = userdata2.birth_place;
            mainWindowVM.transitLat = String.Format("{0:f6}", userdata2.lat);
            mainWindowVM.transitLng = String.Format("{0:f6}", userdata2.lng);
            currentTarget = 2;
        }

        /// <summary>
        /// ユーザーインフォ(左上枠)　ユーザー１クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void changeUserButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindowVM.userName = targetUser.name;
            mainWindowVM.userBirthStr = String.Format("{0}/{1:D2}/{2:D2} {3:D2}:{4:D2}:{5:D2}", targetUser.birth_year,
                targetUser.birth_month, targetUser.birth_day, targetUser.birth_hour, targetUser.birth_minute, targetUser.birth_second);
            mainWindowVM.userTimezone = targetUser.timezone;
            mainWindowVM.userBirthPlace = targetUser.birth_place;
            mainWindowVM.userLat = String.Format("{0:f6}", targetUser.lat);
            mainWindowVM.userLng = String.Format("{0:f6}", targetUser.lng);
            currentTarget = 1;
        }

        /// <summary>
        /// ユーザーインフォ(左上枠)　ユーザー２クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void user2Button_Click(object sender, RoutedEventArgs e)
        {
            mainWindowVM.userName = targetUser2.name;
            mainWindowVM.userBirthStr = String.Format("{0}/{1:D2}/{2:D2} {3:D2}:{4:D2}:{5:D2}", targetUser2.birth_year,
                targetUser2.birth_month, targetUser2.birth_day, targetUser2.birth_hour, targetUser2.birth_minute, targetUser2.birth_second);
            mainWindowVM.userTimezone = targetUser2.timezone;
            mainWindowVM.userBirthPlace = targetUser2.birth_place;
            mainWindowVM.userLat = String.Format("{0:f6}", targetUser2.lat);
            mainWindowVM.userLng = String.Format("{0:f6}", targetUser2.lng);
            currentTarget = 2;
        }

        public UserData getUserData(int n)
        {
            if (calcTargetUser[n] == 1)
            {
                return targetUser;
            }
            return targetUser2;
        }

        public UserEventData getUserEvent(int n)
        {
            if (calcTargetEvent[n] == 1)
            {
                return userdata;
            }
            return userdata2;
        }

        /*
        private void color_Click(object sender, RoutedEventArgs e)
        {
            ColorPick c = new ColorPick();
            c.Show();
        }
        */

        /// <summary>
        /// DateTime型にして変換(UTCで)
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        public DateTime udataTime(UserData u)
        {
            if (CommonData.getTimezoneIndex(u.timezone) == 0)
            {
                // JSTね
                DateTime d = new DateTime(u.birth_year,
                    u.birth_month,
                    u.birth_day,
                    u.birth_hour,
                    u.birth_minute,
                    u.birth_second);
                return d.AddHours(-9.0);
            }
            return new DateTime(u.birth_year,
                u.birth_month,
                u.birth_day,
                u.birth_hour,
                u.birth_minute,
                u.birth_second);
        }

        public DateTime edataTime(UserEventData e)
        {
            if (CommonData.getTimezoneIndex(e.timezone) == 0)
            {
                DateTime d = new DateTime(e.birth_year,
                    e.birth_month,
                    e.birth_day,
                    e.birth_hour,
                    e.birth_minute,
                    e.birth_second);
                return d.AddHours(-9.0);
            }
            return new DateTime(e.birth_year,
                e.birth_month,
                e.birth_day,
                e.birth_hour,
                e.birth_minute,
                e.birth_second);
        }
    }
}
