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
using System.Windows.Shapes;

using microcosm.ViewModel;
using System.Collections.ObjectModel;
using microcosm.Common;
using microcosm.Aspect;

namespace microcosm.Config
{
    /// <summary>
    /// SettingWIndow.xaml の相互作用ロジック
    /// </summary>
    public partial class SettingWIndow : Window
    {
        public MainWindow main;
        public SettingWindowViewModel settingVM;

        // Aspect 一時保存用
        public bool[,] aspectSun = new bool[10, 15];
        public bool[,] aspectMoon = new bool[10, 15];
        public bool[,] aspectMercury = new bool[10, 15];
        public bool[,] aspectVenus = new bool[10, 15];
        public bool[,] aspectMars = new bool[10, 15];
        public bool[,] aspectJupiter = new bool[10, 15];
        public bool[,] aspectSaturn = new bool[10, 15];
        public bool[,] aspectUranus = new bool[10, 15];
        public bool[,] aspectNeptune = new bool[10, 15];
        public bool[,] aspectPluto = new bool[10, 15];
        public bool[,] aspectDh = new bool[10, 15];
        public bool[,] aspectChiron = new bool[10, 15];
        public bool[,] aspectAsc = new bool[10, 15];
        public bool[,] aspectMc = new bool[10,15];

        public Dictionary<string, bool[,]> aspectTempArray = new Dictionary<string, bool[,]>();
        public Dictionary<string, bool> aspectBoolean = new Dictionary<string, bool>();
        public Dictionary<string, int> aspectSubindex = new Dictionary<string, int>();
        public Dictionary<string, int> aspectCommonDataNo = new Dictionary<string, int>();

        // control dictionary
        public Dictionary<string, FrameworkElement> anotherControl = new Dictionary<string, FrameworkElement>();

        public List<string> aspectControlList = new List<string>();
        public List<Image> aspectControlNameList = new List<Image>();

        public SettingWIndow(MainWindow main)
        {
            this.main = main;
            InitializeComponent();

            createAspectAnotherControlName();
            createAspectTempArray();
            createAspectBoolean();
            createAspectSubindex();
            createAspectCommonDataNo();
            createAspectControlList();

            dispList.ItemsSource = new ObservableCollection<SettingData>(main.settings);
            dispList.SelectedIndex = 0;
            settingVM = new SettingWindowViewModel(main);
            leftPane.DataContext = settingVM;

            settingVM.dispName = main.currentSetting.dispName;
            setAspect();
            setOrb();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Hidden;
        }

        private void dispList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox list = (ListBox)sender;
            settingName.Text = main.settings[list.SelectedIndex].dispName;
            firstSun.IsChecked = main.settings[list.SelectedIndex].dispPlanet[0][CommonData.ZODIAC_SUN];
            secondSun.IsChecked = main.settings[list.SelectedIndex].dispPlanet[1][CommonData.ZODIAC_SUN];
            thirdSun.IsChecked = main.settings[list.SelectedIndex].dispPlanet[2][CommonData.ZODIAC_SUN];
            fourthSun.IsChecked = main.settings[list.SelectedIndex].dispPlanet[3][CommonData.ZODIAC_SUN];
            fifthSun.IsChecked = main.settings[list.SelectedIndex].dispPlanet[4][CommonData.ZODIAC_SUN];

            firstMoon.IsChecked = main.settings[list.SelectedIndex].dispPlanet[0][CommonData.ZODIAC_MOON];
            secondMoon.IsChecked = main.settings[list.SelectedIndex].dispPlanet[1][CommonData.ZODIAC_MOON];
            thirdMoon.IsChecked = main.settings[list.SelectedIndex].dispPlanet[2][CommonData.ZODIAC_MOON];
            fourthMoon.IsChecked = main.settings[list.SelectedIndex].dispPlanet[3][CommonData.ZODIAC_MOON];
            fifthMoon.IsChecked = main.settings[list.SelectedIndex].dispPlanet[4][CommonData.ZODIAC_MOON];

            firstMercury.IsChecked = main.settings[list.SelectedIndex].dispPlanet[0][CommonData.ZODIAC_MERCURY];
            secondMercury.IsChecked = main.settings[list.SelectedIndex].dispPlanet[1][CommonData.ZODIAC_MERCURY];
            thirdMercury.IsChecked = main.settings[list.SelectedIndex].dispPlanet[2][CommonData.ZODIAC_MERCURY];
            fourthMercury.IsChecked = main.settings[list.SelectedIndex].dispPlanet[3][CommonData.ZODIAC_MERCURY];
            fifthMercury.IsChecked = main.settings[list.SelectedIndex].dispPlanet[4][CommonData.ZODIAC_MERCURY];

            firstVenus.IsChecked = main.settings[list.SelectedIndex].dispPlanet[0][CommonData.ZODIAC_VENUS];
            secondVenus.IsChecked = main.settings[list.SelectedIndex].dispPlanet[1][CommonData.ZODIAC_VENUS];
            thirdVenus.IsChecked = main.settings[list.SelectedIndex].dispPlanet[2][CommonData.ZODIAC_VENUS];
            fourthVenus.IsChecked = main.settings[list.SelectedIndex].dispPlanet[3][CommonData.ZODIAC_VENUS];
            fifthVenus.IsChecked = main.settings[list.SelectedIndex].dispPlanet[4][CommonData.ZODIAC_VENUS];

            firstMars.IsChecked = main.settings[list.SelectedIndex].dispPlanet[0][CommonData.ZODIAC_MARS];
            secondMars.IsChecked = main.settings[list.SelectedIndex].dispPlanet[1][CommonData.ZODIAC_MARS];
            thirdMars.IsChecked = main.settings[list.SelectedIndex].dispPlanet[2][CommonData.ZODIAC_MARS];
            fourthMars.IsChecked = main.settings[list.SelectedIndex].dispPlanet[3][CommonData.ZODIAC_MARS];
            fifthMars.IsChecked = main.settings[list.SelectedIndex].dispPlanet[4][CommonData.ZODIAC_MARS];

            firstJupiter.IsChecked = main.settings[list.SelectedIndex].dispPlanet[0][CommonData.ZODIAC_JUPITER];
            secondJupiter.IsChecked = main.settings[list.SelectedIndex].dispPlanet[1][CommonData.ZODIAC_JUPITER];
            thirdJupiter.IsChecked = main.settings[list.SelectedIndex].dispPlanet[2][CommonData.ZODIAC_JUPITER];
            fourthJupiter.IsChecked = main.settings[list.SelectedIndex].dispPlanet[3][CommonData.ZODIAC_JUPITER];
            fifthJupiter.IsChecked = main.settings[list.SelectedIndex].dispPlanet[4][CommonData.ZODIAC_JUPITER];

            firstSaturn.IsChecked = main.settings[list.SelectedIndex].dispPlanet[0][CommonData.ZODIAC_SATURN];
            secondSaturn.IsChecked = main.settings[list.SelectedIndex].dispPlanet[1][CommonData.ZODIAC_SATURN];
            thirdSaturn.IsChecked = main.settings[list.SelectedIndex].dispPlanet[2][CommonData.ZODIAC_SATURN];
            fourthSaturn.IsChecked = main.settings[list.SelectedIndex].dispPlanet[3][CommonData.ZODIAC_SATURN];
            fifthSaturn.IsChecked = main.settings[list.SelectedIndex].dispPlanet[4][CommonData.ZODIAC_SATURN];

            firstUranus.IsChecked = main.settings[list.SelectedIndex].dispPlanet[0][CommonData.ZODIAC_URANUS];
            secondUranus.IsChecked = main.settings[list.SelectedIndex].dispPlanet[1][CommonData.ZODIAC_URANUS];
            thirdUranus.IsChecked = main.settings[list.SelectedIndex].dispPlanet[2][CommonData.ZODIAC_URANUS];
            fourthUranus.IsChecked = main.settings[list.SelectedIndex].dispPlanet[3][CommonData.ZODIAC_URANUS];
            fifthUranus.IsChecked = main.settings[list.SelectedIndex].dispPlanet[4][CommonData.ZODIAC_URANUS];

            firstNeptune.IsChecked = main.settings[list.SelectedIndex].dispPlanet[0][CommonData.ZODIAC_NEPTUNE];
            secondNeptune.IsChecked = main.settings[list.SelectedIndex].dispPlanet[1][CommonData.ZODIAC_NEPTUNE];
            thirdNeptune.IsChecked = main.settings[list.SelectedIndex].dispPlanet[2][CommonData.ZODIAC_NEPTUNE];
            fourthNeptune.IsChecked = main.settings[list.SelectedIndex].dispPlanet[3][CommonData.ZODIAC_NEPTUNE];
            fifthNeptune.IsChecked = main.settings[list.SelectedIndex].dispPlanet[4][CommonData.ZODIAC_NEPTUNE];

            firstPluto.IsChecked = main.settings[list.SelectedIndex].dispPlanet[0][CommonData.ZODIAC_PLUTO];
            secondPluto.IsChecked = main.settings[list.SelectedIndex].dispPlanet[1][CommonData.ZODIAC_PLUTO];
            thirdPluto.IsChecked = main.settings[list.SelectedIndex].dispPlanet[2][CommonData.ZODIAC_PLUTO];
            fourthPluto.IsChecked = main.settings[list.SelectedIndex].dispPlanet[3][CommonData.ZODIAC_PLUTO];
            fifthPluto.IsChecked = main.settings[list.SelectedIndex].dispPlanet[4][CommonData.ZODIAC_PLUTO];

            firstDH.IsChecked = main.settings[list.SelectedIndex].dispPlanet[0][CommonData.ZODIAC_DH_TRUENODE];
            secondDH.IsChecked = main.settings[list.SelectedIndex].dispPlanet[1][CommonData.ZODIAC_DH_TRUENODE];
            thirdDH.IsChecked = main.settings[list.SelectedIndex].dispPlanet[2][CommonData.ZODIAC_DH_TRUENODE];
            fourthDH.IsChecked = main.settings[list.SelectedIndex].dispPlanet[3][CommonData.ZODIAC_DH_TRUENODE];
            fifthDH.IsChecked = main.settings[list.SelectedIndex].dispPlanet[4][CommonData.ZODIAC_DH_TRUENODE];

            firstChiron.IsChecked = main.settings[list.SelectedIndex].dispPlanet[0][CommonData.ZODIAC_CHIRON];
            secondChiron.IsChecked = main.settings[list.SelectedIndex].dispPlanet[1][CommonData.ZODIAC_CHIRON];
            thirdChiron.IsChecked = main.settings[list.SelectedIndex].dispPlanet[2][CommonData.ZODIAC_CHIRON];
            fourthChiron.IsChecked = main.settings[list.SelectedIndex].dispPlanet[3][CommonData.ZODIAC_CHIRON];
            fifthChiron.IsChecked = main.settings[list.SelectedIndex].dispPlanet[4][CommonData.ZODIAC_CHIRON];

            firstEarth.IsChecked = main.settings[list.SelectedIndex].dispPlanet[0][CommonData.ZODIAC_EARTH];
            secondEarth.IsChecked = main.settings[list.SelectedIndex].dispPlanet[1][CommonData.ZODIAC_EARTH];
            thirdEarth.IsChecked = main.settings[list.SelectedIndex].dispPlanet[2][CommonData.ZODIAC_EARTH];
            fourthEarth.IsChecked = main.settings[list.SelectedIndex].dispPlanet[3][CommonData.ZODIAC_EARTH];
            fifthEarth.IsChecked = main.settings[list.SelectedIndex].dispPlanet[4][CommonData.ZODIAC_EARTH];

            firstLilith.IsChecked = main.settings[list.SelectedIndex].dispPlanet[0][CommonData.ZODIAC_LILITH];
            secondLilith.IsChecked = main.settings[list.SelectedIndex].dispPlanet[1][CommonData.ZODIAC_LILITH];
            thirdLilith.IsChecked = main.settings[list.SelectedIndex].dispPlanet[2][CommonData.ZODIAC_LILITH];
            fourthLilith.IsChecked = main.settings[list.SelectedIndex].dispPlanet[3][CommonData.ZODIAC_LILITH];
            fifthLilith.IsChecked = main.settings[list.SelectedIndex].dispPlanet[4][CommonData.ZODIAC_LILITH];

            firstCeles.IsChecked = main.settings[list.SelectedIndex].dispPlanet[0][CommonData.ZODIAC_CELES];
            secondCeles.IsChecked = main.settings[list.SelectedIndex].dispPlanet[1][CommonData.ZODIAC_CELES];
            thirdCeles.IsChecked = main.settings[list.SelectedIndex].dispPlanet[2][CommonData.ZODIAC_CELES];
            fourthCeles.IsChecked = main.settings[list.SelectedIndex].dispPlanet[3][CommonData.ZODIAC_CELES];
            fifthCeles.IsChecked = main.settings[list.SelectedIndex].dispPlanet[4][CommonData.ZODIAC_CELES];

            firstParas.IsChecked = main.settings[list.SelectedIndex].dispPlanet[0][CommonData.ZODIAC_PARAS];
            secondParas.IsChecked = main.settings[list.SelectedIndex].dispPlanet[1][CommonData.ZODIAC_PARAS];
            thirdParas.IsChecked = main.settings[list.SelectedIndex].dispPlanet[2][CommonData.ZODIAC_PARAS];
            fourthParas.IsChecked = main.settings[list.SelectedIndex].dispPlanet[3][CommonData.ZODIAC_PARAS];
            fifthParas.IsChecked = main.settings[list.SelectedIndex].dispPlanet[4][CommonData.ZODIAC_PARAS];

            firstJuno.IsChecked = main.settings[list.SelectedIndex].dispPlanet[0][CommonData.ZODIAC_JUNO];
            secondJuno.IsChecked = main.settings[list.SelectedIndex].dispPlanet[1][CommonData.ZODIAC_JUNO];
            thirdJuno.IsChecked = main.settings[list.SelectedIndex].dispPlanet[2][CommonData.ZODIAC_JUNO];
            fourthJuno.IsChecked = main.settings[list.SelectedIndex].dispPlanet[3][CommonData.ZODIAC_JUNO];
            fifthJuno.IsChecked = main.settings[list.SelectedIndex].dispPlanet[4][CommonData.ZODIAC_JUNO];

            firstVesta.IsChecked = main.settings[list.SelectedIndex].dispPlanet[0][CommonData.ZODIAC_VESTA];
            secondVesta.IsChecked = main.settings[list.SelectedIndex].dispPlanet[1][CommonData.ZODIAC_VESTA];
            thirdVesta.IsChecked = main.settings[list.SelectedIndex].dispPlanet[2][CommonData.ZODIAC_VESTA];
            fourthVesta.IsChecked = main.settings[list.SelectedIndex].dispPlanet[3][CommonData.ZODIAC_VESTA];
            fifthVesta.IsChecked = main.settings[list.SelectedIndex].dispPlanet[4][CommonData.ZODIAC_VESTA];

            firstDT.IsChecked = main.settings[list.SelectedIndex].dispPlanet[0][CommonData.ZODIAC_DT_OSCULATE_APOGEE];
            secondDT.IsChecked = main.settings[list.SelectedIndex].dispPlanet[1][CommonData.ZODIAC_DT_OSCULATE_APOGEE];
            thirdDT.IsChecked = main.settings[list.SelectedIndex].dispPlanet[2][CommonData.ZODIAC_DT_OSCULATE_APOGEE];
            fourthDT.IsChecked = main.settings[list.SelectedIndex].dispPlanet[3][CommonData.ZODIAC_DT_OSCULATE_APOGEE];
            fifthDT.IsChecked = main.settings[list.SelectedIndex].dispPlanet[4][CommonData.ZODIAC_DT_OSCULATE_APOGEE];

            if (aspectSunOn11 == null)
            {
                return;
            }

            for (int i = 0; i < aspectControlList.Count; i++)
            {
                if ((aspectTempArray[aspectControlList[i]])[list.SelectedIndex, aspectSubindex[aspectControlList[i]]])
                {
                    aspectControlNameList[i].Visibility = Visibility.Visible;
                    aspectControlNameList[i].Height = 24;
                    anotherControl[aspectControlList[i]].Visibility = Visibility.Hidden;
                    anotherControl[aspectControlList[i]].Height = 0;
                }
                else
                {
                    aspectControlNameList[i].Visibility = Visibility.Hidden;
                    aspectControlNameList[i].Height = 0;
                    anotherControl[aspectControlList[i]].Visibility = Visibility.Visible;
                    anotherControl[aspectControlList[i]].Height = 24;
                }
            }
        }

        private void setAspect()
        {
            aspectSunOn11.Tag = 1;
            aspectSunOff11.Tag = 0;

            int index = orbRing.SelectedIndex;
            switch (index)
            {
                case 0:
                    break;
            }
        }

        private void setOrb()
        {
            int index = orbRing.SelectedIndex;
            switch (index)
            {
                case 0:
                    // N
                    sunSoft1st.Text = main.currentSetting.orb_sun_soft_1st[0, 0].ToString();
                    sunHard1st.Text = main.currentSetting.orb_sun_hard_1st[0, 0].ToString();
                    sunSoft2nd.Text = main.currentSetting.orb_sun_soft_2nd[0, 0].ToString();
                    sunHard2nd.Text = main.currentSetting.orb_sun_hard_2nd[0, 0].ToString();
                    sunSoft150.Text = main.currentSetting.orb_sun_soft_150[0, 0].ToString();
                    sunHard150.Text = main.currentSetting.orb_sun_hard_150[0, 0].ToString();
                    moonSoft1st.Text = main.currentSetting.orb_moon_soft_1st[0, 0].ToString();
                    moonHard1st.Text = main.currentSetting.orb_moon_hard_1st[0, 0].ToString();
                    moonSoft2nd.Text = main.currentSetting.orb_moon_soft_2nd[0, 0].ToString();
                    moonHard2nd.Text = main.currentSetting.orb_moon_hard_2nd[0, 0].ToString();
                    moonSoft150.Text = main.currentSetting.orb_moon_soft_150[0, 0].ToString();
                    moonHard150.Text = main.currentSetting.orb_moon_hard_150[0, 0].ToString();
                    otherSoft1st.Text = main.currentSetting.orb_other_soft_1st[0, 0].ToString();
                    otherHard1st.Text = main.currentSetting.orb_other_hard_1st[0, 0].ToString();
                    otherSoft2nd.Text = main.currentSetting.orb_other_soft_2nd[0, 0].ToString();
                    otherHard2nd.Text = main.currentSetting.orb_other_hard_2nd[0, 0].ToString();
                    otherSoft150.Text = main.currentSetting.orb_other_soft_150[0, 0].ToString();
                    otherHard150.Text = main.currentSetting.orb_other_hard_150[0, 0].ToString();
                    break;
                case 1:
                    // P
                    sunSoft1st.Text = main.currentSetting.orb_sun_soft_1st[1, 1].ToString();
                    sunHard1st.Text = main.currentSetting.orb_sun_hard_1st[1, 1].ToString();
                    sunSoft2nd.Text = main.currentSetting.orb_sun_soft_2nd[1, 1].ToString();
                    sunHard2nd.Text = main.currentSetting.orb_sun_hard_2nd[1, 1].ToString();
                    sunSoft150.Text = main.currentSetting.orb_sun_soft_150[1, 1].ToString();
                    sunHard150.Text = main.currentSetting.orb_sun_hard_150[1, 1].ToString();
                    moonSoft1st.Text = main.currentSetting.orb_moon_soft_1st[1, 1].ToString();
                    moonHard1st.Text = main.currentSetting.orb_moon_hard_1st[1, 1].ToString();
                    moonSoft2nd.Text = main.currentSetting.orb_moon_soft_2nd[1, 1].ToString();
                    moonHard2nd.Text = main.currentSetting.orb_moon_hard_2nd[1, 1].ToString();
                    moonSoft150.Text = main.currentSetting.orb_moon_soft_150[1, 1].ToString();
                    moonHard150.Text = main.currentSetting.orb_moon_hard_150[1, 1].ToString();
                    otherSoft1st.Text = main.currentSetting.orb_other_soft_1st[1, 1].ToString();
                    otherHard1st.Text = main.currentSetting.orb_other_hard_1st[1, 1].ToString();
                    otherSoft2nd.Text = main.currentSetting.orb_other_soft_2nd[1, 1].ToString();
                    otherHard2nd.Text = main.currentSetting.orb_other_hard_2nd[1, 1].ToString();
                    otherSoft150.Text = main.currentSetting.orb_other_soft_150[1, 1].ToString();
                    otherHard150.Text = main.currentSetting.orb_other_hard_150[1, 1].ToString();
                    break;
                case 2:
                    // T
                    sunSoft1st.Text = main.currentSetting.orb_sun_soft_1st[2, 2].ToString();
                    sunHard1st.Text = main.currentSetting.orb_sun_hard_1st[2, 2].ToString();
                    sunSoft2nd.Text = main.currentSetting.orb_sun_soft_2nd[2, 2].ToString();
                    sunHard2nd.Text = main.currentSetting.orb_sun_hard_2nd[2, 2].ToString();
                    sunSoft150.Text = main.currentSetting.orb_sun_soft_150[2, 2].ToString();
                    sunHard150.Text = main.currentSetting.orb_sun_hard_150[2, 2].ToString();
                    moonSoft1st.Text = main.currentSetting.orb_moon_soft_1st[2, 2].ToString();
                    moonHard1st.Text = main.currentSetting.orb_moon_hard_1st[2, 2].ToString();
                    moonSoft2nd.Text = main.currentSetting.orb_moon_soft_2nd[2, 2].ToString();
                    moonHard2nd.Text = main.currentSetting.orb_moon_hard_2nd[2, 2].ToString();
                    moonSoft150.Text = main.currentSetting.orb_moon_soft_150[2, 2].ToString();
                    moonHard150.Text = main.currentSetting.orb_moon_hard_150[2, 2].ToString();
                    otherSoft1st.Text = main.currentSetting.orb_other_soft_1st[2, 2].ToString();
                    otherHard1st.Text = main.currentSetting.orb_other_hard_1st[2, 2].ToString();
                    otherSoft2nd.Text = main.currentSetting.orb_other_soft_2nd[2, 2].ToString();
                    otherHard2nd.Text = main.currentSetting.orb_other_hard_2nd[2, 2].ToString();
                    otherSoft150.Text = main.currentSetting.orb_other_soft_150[2, 2].ToString();
                    otherHard150.Text = main.currentSetting.orb_other_hard_150[2, 2].ToString();
                    break;
                case 3:
                    // N-P
                    sunSoft1st.Text = main.currentSetting.orb_sun_soft_1st[0, 1].ToString();
                    sunHard1st.Text = main.currentSetting.orb_sun_hard_1st[0, 1].ToString();
                    sunSoft2nd.Text = main.currentSetting.orb_sun_soft_2nd[0, 1].ToString();
                    sunHard2nd.Text = main.currentSetting.orb_sun_hard_2nd[0, 1].ToString();
                    sunSoft150.Text = main.currentSetting.orb_sun_soft_150[0, 1].ToString();
                    sunHard150.Text = main.currentSetting.orb_sun_hard_150[0, 1].ToString();
                    moonSoft1st.Text = main.currentSetting.orb_moon_soft_1st[0, 1].ToString();
                    moonHard1st.Text = main.currentSetting.orb_moon_hard_1st[0, 1].ToString();
                    moonSoft2nd.Text = main.currentSetting.orb_moon_soft_2nd[0, 1].ToString();
                    moonHard2nd.Text = main.currentSetting.orb_moon_hard_2nd[0, 1].ToString();
                    moonSoft150.Text = main.currentSetting.orb_moon_soft_150[0, 1].ToString();
                    moonHard150.Text = main.currentSetting.orb_moon_hard_150[0, 1].ToString();
                    otherSoft1st.Text = main.currentSetting.orb_other_soft_1st[0, 1].ToString();
                    otherHard1st.Text = main.currentSetting.orb_other_hard_1st[0, 1].ToString();
                    otherSoft2nd.Text = main.currentSetting.orb_other_soft_2nd[0, 1].ToString();
                    otherHard2nd.Text = main.currentSetting.orb_other_hard_2nd[0, 1].ToString();
                    otherSoft150.Text = main.currentSetting.orb_other_soft_150[0, 1].ToString();
                    otherHard150.Text = main.currentSetting.orb_other_hard_150[0, 1].ToString();
                    break;
                case 4:
                    // N-T
                    sunSoft1st.Text = main.currentSetting.orb_sun_soft_1st[0, 2].ToString();
                    sunHard1st.Text = main.currentSetting.orb_sun_hard_1st[0, 2].ToString();
                    sunSoft2nd.Text = main.currentSetting.orb_sun_soft_2nd[0, 2].ToString();
                    sunHard2nd.Text = main.currentSetting.orb_sun_hard_2nd[0, 2].ToString();
                    sunSoft150.Text = main.currentSetting.orb_sun_soft_150[0, 2].ToString();
                    sunHard150.Text = main.currentSetting.orb_sun_hard_150[0, 2].ToString();
                    moonSoft1st.Text = main.currentSetting.orb_moon_soft_1st[0, 2].ToString();
                    moonHard1st.Text = main.currentSetting.orb_moon_hard_1st[0, 2].ToString();
                    moonSoft2nd.Text = main.currentSetting.orb_moon_soft_2nd[0, 2].ToString();
                    moonHard2nd.Text = main.currentSetting.orb_moon_hard_2nd[0, 2].ToString();
                    moonSoft150.Text = main.currentSetting.orb_moon_soft_150[0, 2].ToString();
                    moonHard150.Text = main.currentSetting.orb_moon_hard_150[0, 2].ToString();
                    otherSoft1st.Text = main.currentSetting.orb_other_soft_1st[0, 2].ToString();
                    otherHard1st.Text = main.currentSetting.orb_other_hard_1st[0, 2].ToString();
                    otherSoft2nd.Text = main.currentSetting.orb_other_soft_2nd[0, 2].ToString();
                    otherHard2nd.Text = main.currentSetting.orb_other_hard_2nd[0, 2].ToString();
                    otherSoft150.Text = main.currentSetting.orb_other_soft_150[0, 2].ToString();
                    otherHard150.Text = main.currentSetting.orb_other_hard_150[0, 2].ToString();
                    break;
                case 5:
                    // P-T
                    sunSoft1st.Text = main.currentSetting.orb_sun_soft_1st[1, 2].ToString();
                    sunHard1st.Text = main.currentSetting.orb_sun_hard_1st[1, 2].ToString();
                    sunSoft2nd.Text = main.currentSetting.orb_sun_soft_2nd[1, 2].ToString();
                    sunHard2nd.Text = main.currentSetting.orb_sun_hard_2nd[1, 2].ToString();
                    sunSoft150.Text = main.currentSetting.orb_sun_soft_150[1, 2].ToString();
                    sunHard150.Text = main.currentSetting.orb_sun_hard_150[1, 2].ToString();
                    moonSoft1st.Text = main.currentSetting.orb_moon_soft_1st[1, 2].ToString();
                    moonHard1st.Text = main.currentSetting.orb_moon_hard_1st[1, 2].ToString();
                    moonSoft2nd.Text = main.currentSetting.orb_moon_soft_2nd[1, 2].ToString();
                    moonHard2nd.Text = main.currentSetting.orb_moon_hard_2nd[1, 2].ToString();
                    moonSoft150.Text = main.currentSetting.orb_moon_soft_150[1, 2].ToString();
                    moonHard150.Text = main.currentSetting.orb_moon_hard_150[1, 2].ToString();
                    otherSoft1st.Text = main.currentSetting.orb_other_soft_1st[1, 2].ToString();
                    otherHard1st.Text = main.currentSetting.orb_other_hard_1st[1, 2].ToString();
                    otherSoft2nd.Text = main.currentSetting.orb_other_soft_2nd[1, 2].ToString();
                    otherHard2nd.Text = main.currentSetting.orb_other_hard_2nd[1, 2].ToString();
                    otherSoft150.Text = main.currentSetting.orb_other_soft_150[1, 2].ToString();
                    otherHard150.Text = main.currentSetting.orb_other_hard_150[1, 2].ToString();
                    break;
                case 6:
                    // N-4
                    sunSoft1st.Text = main.currentSetting.orb_sun_soft_1st[0, 3].ToString();
                    sunHard1st.Text = main.currentSetting.orb_sun_hard_1st[0, 3].ToString();
                    sunSoft2nd.Text = main.currentSetting.orb_sun_soft_2nd[0, 3].ToString();
                    sunHard2nd.Text = main.currentSetting.orb_sun_hard_2nd[0, 3].ToString();
                    sunSoft150.Text = main.currentSetting.orb_sun_soft_150[0, 3].ToString();
                    sunHard150.Text = main.currentSetting.orb_sun_hard_150[0, 3].ToString();
                    moonSoft1st.Text = main.currentSetting.orb_moon_soft_1st[0, 3].ToString();
                    moonHard1st.Text = main.currentSetting.orb_moon_hard_1st[0, 3].ToString();
                    moonSoft2nd.Text = main.currentSetting.orb_moon_soft_2nd[0, 3].ToString();
                    moonHard2nd.Text = main.currentSetting.orb_moon_hard_2nd[0, 3].ToString();
                    moonSoft150.Text = main.currentSetting.orb_moon_soft_150[0, 3].ToString();
                    moonHard150.Text = main.currentSetting.orb_moon_hard_150[0, 3].ToString();
                    otherSoft1st.Text = main.currentSetting.orb_other_soft_1st[0, 3].ToString();
                    otherHard1st.Text = main.currentSetting.orb_other_hard_1st[0, 3].ToString();
                    otherSoft2nd.Text = main.currentSetting.orb_other_soft_2nd[0, 3].ToString();
                    otherHard2nd.Text = main.currentSetting.orb_other_hard_2nd[0, 3].ToString();
                    otherSoft150.Text = main.currentSetting.orb_other_soft_150[0, 3].ToString();
                    otherHard150.Text = main.currentSetting.orb_other_hard_150[0, 3].ToString();
                    break;
                case 7:
                    // N-5
                    sunSoft1st.Text = main.currentSetting.orb_sun_soft_1st[0, 4].ToString();
                    sunHard1st.Text = main.currentSetting.orb_sun_hard_1st[0, 4].ToString();
                    sunSoft2nd.Text = main.currentSetting.orb_sun_soft_2nd[0, 4].ToString();
                    sunHard2nd.Text = main.currentSetting.orb_sun_hard_2nd[0, 4].ToString();
                    sunSoft150.Text = main.currentSetting.orb_sun_soft_150[0, 4].ToString();
                    sunHard150.Text = main.currentSetting.orb_sun_hard_150[0, 4].ToString();
                    moonSoft1st.Text = main.currentSetting.orb_moon_soft_1st[0, 4].ToString();
                    moonHard1st.Text = main.currentSetting.orb_moon_hard_1st[0, 4].ToString();
                    moonSoft2nd.Text = main.currentSetting.orb_moon_soft_2nd[0, 4].ToString();
                    moonHard2nd.Text = main.currentSetting.orb_moon_hard_2nd[0, 4].ToString();
                    moonSoft150.Text = main.currentSetting.orb_moon_soft_150[0, 4].ToString();
                    moonHard150.Text = main.currentSetting.orb_moon_hard_150[0, 4].ToString();
                    otherSoft1st.Text = main.currentSetting.orb_other_soft_1st[0, 4].ToString();
                    otherHard1st.Text = main.currentSetting.orb_other_hard_1st[0, 4].ToString();
                    otherSoft2nd.Text = main.currentSetting.orb_other_soft_2nd[0, 4].ToString();
                    otherHard2nd.Text = main.currentSetting.orb_other_hard_2nd[0, 4].ToString();
                    otherSoft150.Text = main.currentSetting.orb_other_soft_150[0, 4].ToString();
                    otherHard150.Text = main.currentSetting.orb_other_hard_150[0, 4].ToString();
                    break;
                case 8:
                    // P-4
                    sunSoft1st.Text = main.currentSetting.orb_sun_soft_1st[1, 3].ToString();
                    sunHard1st.Text = main.currentSetting.orb_sun_hard_1st[1, 3].ToString();
                    sunSoft2nd.Text = main.currentSetting.orb_sun_soft_2nd[1, 3].ToString();
                    sunHard2nd.Text = main.currentSetting.orb_sun_hard_2nd[1, 3].ToString();
                    sunSoft150.Text = main.currentSetting.orb_sun_soft_150[1, 3].ToString();
                    sunHard150.Text = main.currentSetting.orb_sun_hard_150[1, 3].ToString();
                    moonSoft1st.Text = main.currentSetting.orb_moon_soft_1st[1, 3].ToString();
                    moonHard1st.Text = main.currentSetting.orb_moon_hard_1st[1, 3].ToString();
                    moonSoft2nd.Text = main.currentSetting.orb_moon_soft_2nd[1, 3].ToString();
                    moonHard2nd.Text = main.currentSetting.orb_moon_hard_2nd[1, 3].ToString();
                    moonSoft150.Text = main.currentSetting.orb_moon_soft_150[1, 3].ToString();
                    moonHard150.Text = main.currentSetting.orb_moon_hard_150[1, 3].ToString();
                    otherSoft1st.Text = main.currentSetting.orb_other_soft_1st[1, 3].ToString();
                    otherHard1st.Text = main.currentSetting.orb_other_hard_1st[1, 3].ToString();
                    otherSoft2nd.Text = main.currentSetting.orb_other_soft_2nd[1, 3].ToString();
                    otherHard2nd.Text = main.currentSetting.orb_other_hard_2nd[1, 3].ToString();
                    otherSoft150.Text = main.currentSetting.orb_other_soft_150[1, 3].ToString();
                    otherHard150.Text = main.currentSetting.orb_other_hard_150[1, 3].ToString();
                    break;
                case 9:
                    // P-5
                    sunSoft1st.Text = main.currentSetting.orb_sun_soft_1st[1, 4].ToString();
                    sunHard1st.Text = main.currentSetting.orb_sun_hard_1st[1, 4].ToString();
                    sunSoft2nd.Text = main.currentSetting.orb_sun_soft_2nd[1, 4].ToString();
                    sunHard2nd.Text = main.currentSetting.orb_sun_hard_2nd[1, 4].ToString();
                    sunSoft150.Text = main.currentSetting.orb_sun_soft_150[1, 4].ToString();
                    sunHard150.Text = main.currentSetting.orb_sun_hard_150[1, 4].ToString();
                    moonSoft1st.Text = main.currentSetting.orb_moon_soft_1st[1, 4].ToString();
                    moonHard1st.Text = main.currentSetting.orb_moon_hard_1st[1, 4].ToString();
                    moonSoft2nd.Text = main.currentSetting.orb_moon_soft_2nd[1, 4].ToString();
                    moonHard2nd.Text = main.currentSetting.orb_moon_hard_2nd[1, 4].ToString();
                    moonSoft150.Text = main.currentSetting.orb_moon_soft_150[1, 4].ToString();
                    moonHard150.Text = main.currentSetting.orb_moon_hard_150[1, 4].ToString();
                    otherSoft1st.Text = main.currentSetting.orb_other_soft_1st[1, 4].ToString();
                    otherHard1st.Text = main.currentSetting.orb_other_hard_1st[1, 4].ToString();
                    otherSoft2nd.Text = main.currentSetting.orb_other_soft_2nd[1, 4].ToString();
                    otherHard2nd.Text = main.currentSetting.orb_other_hard_2nd[1, 4].ToString();
                    otherSoft150.Text = main.currentSetting.orb_other_soft_150[1, 4].ToString();
                    otherHard150.Text = main.currentSetting.orb_other_hard_150[1, 4].ToString();
                    break;
                case 10:
                    // T-4
                    sunSoft1st.Text = main.currentSetting.orb_sun_soft_1st[2, 3].ToString();
                    sunHard1st.Text = main.currentSetting.orb_sun_hard_1st[2, 3].ToString();
                    sunSoft2nd.Text = main.currentSetting.orb_sun_soft_2nd[2, 3].ToString();
                    sunHard2nd.Text = main.currentSetting.orb_sun_hard_2nd[2, 3].ToString();
                    sunSoft150.Text = main.currentSetting.orb_sun_soft_150[2, 3].ToString();
                    sunHard150.Text = main.currentSetting.orb_sun_hard_150[2, 3].ToString();
                    moonSoft1st.Text = main.currentSetting.orb_moon_soft_1st[2, 3].ToString();
                    moonHard1st.Text = main.currentSetting.orb_moon_hard_1st[2, 3].ToString();
                    moonSoft2nd.Text = main.currentSetting.orb_moon_soft_2nd[2, 3].ToString();
                    moonHard2nd.Text = main.currentSetting.orb_moon_hard_2nd[2, 3].ToString();
                    moonSoft150.Text = main.currentSetting.orb_moon_soft_150[2, 3].ToString();
                    moonHard150.Text = main.currentSetting.orb_moon_hard_150[2, 3].ToString();
                    otherSoft1st.Text = main.currentSetting.orb_other_soft_1st[2, 3].ToString();
                    otherHard1st.Text = main.currentSetting.orb_other_hard_1st[2, 3].ToString();
                    otherSoft2nd.Text = main.currentSetting.orb_other_soft_2nd[2, 3].ToString();
                    otherHard2nd.Text = main.currentSetting.orb_other_hard_2nd[2, 3].ToString();
                    otherSoft150.Text = main.currentSetting.orb_other_soft_150[2, 3].ToString();
                    otherHard150.Text = main.currentSetting.orb_other_hard_150[2, 3].ToString();
                    break;
                case 11:
                    // T-5
                    sunSoft1st.Text = main.currentSetting.orb_sun_soft_1st[2, 4].ToString();
                    sunHard1st.Text = main.currentSetting.orb_sun_hard_1st[2, 4].ToString();
                    sunSoft2nd.Text = main.currentSetting.orb_sun_soft_2nd[2, 4].ToString();
                    sunHard2nd.Text = main.currentSetting.orb_sun_hard_2nd[2, 4].ToString();
                    sunSoft150.Text = main.currentSetting.orb_sun_soft_150[2, 4].ToString();
                    sunHard150.Text = main.currentSetting.orb_sun_hard_150[2, 4].ToString();
                    moonSoft1st.Text = main.currentSetting.orb_moon_soft_1st[2, 4].ToString();
                    moonHard1st.Text = main.currentSetting.orb_moon_hard_1st[2, 4].ToString();
                    moonSoft2nd.Text = main.currentSetting.orb_moon_soft_2nd[2, 4].ToString();
                    moonHard2nd.Text = main.currentSetting.orb_moon_hard_2nd[2, 4].ToString();
                    moonSoft150.Text = main.currentSetting.orb_moon_soft_150[2, 4].ToString();
                    moonHard150.Text = main.currentSetting.orb_moon_hard_150[2, 4].ToString();
                    otherSoft1st.Text = main.currentSetting.orb_other_soft_1st[2, 4].ToString();
                    otherHard1st.Text = main.currentSetting.orb_other_hard_1st[2, 4].ToString();
                    otherSoft2nd.Text = main.currentSetting.orb_other_soft_2nd[2, 4].ToString();
                    otherHard2nd.Text = main.currentSetting.orb_other_hard_2nd[2, 4].ToString();
                    otherSoft150.Text = main.currentSetting.orb_other_soft_150[2, 4].ToString();
                    otherHard150.Text = main.currentSetting.orb_other_hard_150[2, 4].ToString();
                    break;
                case 12:
                    // 4-4
                    sunSoft1st.Text = main.currentSetting.orb_sun_soft_1st[3, 3].ToString();
                    sunHard1st.Text = main.currentSetting.orb_sun_hard_1st[3, 3].ToString();
                    sunSoft2nd.Text = main.currentSetting.orb_sun_soft_2nd[3, 3].ToString();
                    sunHard2nd.Text = main.currentSetting.orb_sun_hard_2nd[3, 3].ToString();
                    sunSoft150.Text = main.currentSetting.orb_sun_soft_150[3, 3].ToString();
                    sunHard150.Text = main.currentSetting.orb_sun_hard_150[3, 3].ToString();
                    moonSoft1st.Text = main.currentSetting.orb_moon_soft_1st[3, 3].ToString();
                    moonHard1st.Text = main.currentSetting.orb_moon_hard_1st[3, 3].ToString();
                    moonSoft2nd.Text = main.currentSetting.orb_moon_soft_2nd[3, 3].ToString();
                    moonHard2nd.Text = main.currentSetting.orb_moon_hard_2nd[3, 3].ToString();
                    moonSoft150.Text = main.currentSetting.orb_moon_soft_150[3, 3].ToString();
                    moonHard150.Text = main.currentSetting.orb_moon_hard_150[3, 3].ToString();
                    otherSoft1st.Text = main.currentSetting.orb_other_soft_1st[3, 3].ToString();
                    otherHard1st.Text = main.currentSetting.orb_other_hard_1st[3, 3].ToString();
                    otherSoft2nd.Text = main.currentSetting.orb_other_soft_2nd[3, 3].ToString();
                    otherHard2nd.Text = main.currentSetting.orb_other_hard_2nd[3, 3].ToString();
                    otherSoft150.Text = main.currentSetting.orb_other_soft_150[3, 3].ToString();
                    otherHard150.Text = main.currentSetting.orb_other_hard_150[3, 3].ToString();
                    break;
                case 13:
                    // 4-5
                    sunSoft1st.Text = main.currentSetting.orb_sun_soft_1st[3, 4].ToString();
                    sunHard1st.Text = main.currentSetting.orb_sun_hard_1st[3, 4].ToString();
                    sunSoft2nd.Text = main.currentSetting.orb_sun_soft_2nd[3, 4].ToString();
                    sunHard2nd.Text = main.currentSetting.orb_sun_hard_2nd[3, 4].ToString();
                    sunSoft150.Text = main.currentSetting.orb_sun_soft_150[3, 4].ToString();
                    sunHard150.Text = main.currentSetting.orb_sun_hard_150[3, 4].ToString();
                    moonSoft1st.Text = main.currentSetting.orb_moon_soft_1st[3, 4].ToString();
                    moonHard1st.Text = main.currentSetting.orb_moon_hard_1st[3, 4].ToString();
                    moonSoft2nd.Text = main.currentSetting.orb_moon_soft_2nd[3, 4].ToString();
                    moonHard2nd.Text = main.currentSetting.orb_moon_hard_2nd[3, 4].ToString();
                    moonSoft150.Text = main.currentSetting.orb_moon_soft_150[3, 4].ToString();
                    moonHard150.Text = main.currentSetting.orb_moon_hard_150[3, 4].ToString();
                    otherSoft1st.Text = main.currentSetting.orb_other_soft_1st[3, 4].ToString();
                    otherHard1st.Text = main.currentSetting.orb_other_hard_1st[3, 4].ToString();
                    otherSoft2nd.Text = main.currentSetting.orb_other_soft_2nd[3, 4].ToString();
                    otherHard2nd.Text = main.currentSetting.orb_other_hard_2nd[3, 4].ToString();
                    otherSoft150.Text = main.currentSetting.orb_other_soft_150[3, 4].ToString();
                    otherHard150.Text = main.currentSetting.orb_other_hard_150[3, 4].ToString();
                    break;
                case 14:
                    // 5-5
                    sunSoft1st.Text = main.currentSetting.orb_sun_soft_1st[4, 4].ToString();
                    sunHard1st.Text = main.currentSetting.orb_sun_hard_1st[4, 4].ToString();
                    sunSoft2nd.Text = main.currentSetting.orb_sun_soft_2nd[4, 4].ToString();
                    sunHard2nd.Text = main.currentSetting.orb_sun_hard_2nd[4, 4].ToString();
                    sunSoft150.Text = main.currentSetting.orb_sun_soft_150[4, 4].ToString();
                    sunHard150.Text = main.currentSetting.orb_sun_hard_150[4, 4].ToString();
                    moonSoft1st.Text = main.currentSetting.orb_moon_soft_1st[4, 4].ToString();
                    moonHard1st.Text = main.currentSetting.orb_moon_hard_1st[4, 4].ToString();
                    moonSoft2nd.Text = main.currentSetting.orb_moon_soft_2nd[4, 4].ToString();
                    moonHard2nd.Text = main.currentSetting.orb_moon_hard_2nd[4, 4].ToString();
                    moonSoft150.Text = main.currentSetting.orb_moon_soft_150[4, 4].ToString();
                    moonHard150.Text = main.currentSetting.orb_moon_hard_150[4, 4].ToString();
                    otherSoft1st.Text = main.currentSetting.orb_other_soft_1st[4, 4].ToString();
                    otherHard1st.Text = main.currentSetting.orb_other_hard_1st[4, 4].ToString();
                    otherSoft2nd.Text = main.currentSetting.orb_other_soft_2nd[4, 4].ToString();
                    otherHard2nd.Text = main.currentSetting.orb_other_hard_2nd[4, 4].ToString();
                    otherSoft150.Text = main.currentSetting.orb_other_soft_150[4, 4].ToString();
                    otherHard150.Text = main.currentSetting.orb_other_hard_150[4, 4].ToString();
                    break;
                default:
                    break;
            }
        }

        private void conjunction11_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            if ((int)img.Tag == 1)
            {
                // on->off
                // main.list1['sun'] = false;
            } else
            {
            }

        }

        private void opposition11_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            if ((int)img.Tag == 1)
            {
                // on->off
            }
            else
            {
            }
        }

        private void trine11_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            if ((int)img.Tag == 1)
            {
                // on->off
            }
            else
            {
            }
        }

        private void square11_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            if ((int)img.Tag == 1)
            {
                // on->off
            }
            else
            {
            }
        }

        private void sextile11_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            if ((int)img.Tag == 1)
            {
                // on->off
            }
            else
            {
            }
        }

        private void inconjunct11_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            if ((int)img.Tag == 1)
            {
                // on->off
            }
            else
            {
            }
        }

        private void sesquiquadrate11_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            if ((int)img.Tag == 1)
            {
                // on->off
            }
            else
            {
            }
        }

        private void orbRing_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sunSoft1st != null)
            {
                setOrb();
            }
        }

        // aspect
        private void aspectSunOn11_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectSunOff11_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectMoonOn11_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectMoonOff11_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectMercuryOn11_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectMercuryOff11_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectVenusOn11_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectVenusOff11_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectMarsOn11_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectMarsOff11_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectJupiterOn11_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectJupiterOff11_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectSaturnOn11_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectSaturnOff11_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectUranusOn11_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectUranusOff11_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectNeptuneOn11_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectNeptuneOff11_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectPlutoOn11_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectPlutoOff11_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectDhOn11_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectDhOff11_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectChironOn11_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectChironOff11_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectAscOn11_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectAscOff11_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectMcOn11_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectMcOff11_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }


        private void aspectSunOn23_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectSunOff23_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectMoonOn23_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectMoonOff23_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectMercuryOn23_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectMercuryOff23_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectVenusOn23_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectVenusOff23_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectMarsOn23_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectMarsOff23_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectJupiterOn23_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectJupiterOff23_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectSaturnOn23_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectSaturnOff23_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectUranusOn23_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectUranusOff23_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectNeptuneOn23_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectNeptuneOff23_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectPlutoOn23_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectPlutoOff23_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectDhOn23_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectDhOff23_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectChironOn23_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectChironOff23_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectAscOn23_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectAscOff23_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectMcOn23_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectMcOff23_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectSunOn13_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectSunOff13_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectMoonOn13_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectMoonOff13_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectMercuryOn13_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectMercuryOff13_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectVenusOn13_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectVenusOff13_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectMarsOn13_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectMarsOff13_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectJupiterOn13_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectJupiterOff13_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectSaturnOn13_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectSaturnOff13_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectUranusOn13_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectUranusOff13_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectNeptuneOn13_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectNeptuneOff13_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectPlutoOn13_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectPlutoOff13_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectDhOn13_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectDhOff13_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectChironOn13_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectChironOff13_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectAscOn13_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectAscOff13_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectMcOn13_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectMcOff13_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectSunOn12_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectSunOff12_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectMoonOn12_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectMoonOff12_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectMercuryOn12_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectMercuryOff12_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectVenusOn12_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectVenusOff12_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectMarsOn12_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectMarsOff12_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectJupiterOn12_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectJupiterOff12_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectSaturnOn12_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectSaturnOff12_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectUranusOn12_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectUranusOff12_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectNeptuneOn12_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectNeptuneOff12_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectPlutoOn12_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectPlutoOff12_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectDhOn12_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectDhOff12_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectChironOn12_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectChironOff12_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectAscOn12_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectAscOff12_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectMcOn12_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectMcOff12_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectSunOn33_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectSunOff33_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectMoonOn33_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectMoonOff33_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectMercuryOn33_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectMercuryOff33_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectVenusOn33_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectVenusOff33_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectMarsOn33_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectMarsOff33_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectJupiterOn33_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectJupiterOff33_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectSaturnOn33_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectSaturnOff33_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectUranusOn33_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectUranusOff33_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectNeptuneOn33_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectNeptuneOff33_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectPlutoOn33_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectPlutoOff33_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectDhOn33_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectDhOff33_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectChironOn33_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectChironOff33_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectAscOn33_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectAscOff33_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectMcOn33_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectMcOff33_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectSunOn22_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectSunOff22_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectMoonOn22_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectMoonOff22_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectMercuryOn22_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectMercuryOff22_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectVenusOn22_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectVenusOff22_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectMarsOn22_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectMarsOff22_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectJupiterOn22_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectJupiterOff22_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectSaturnOn22_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectSaturnOff22_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectUranusOn22_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectUranusOff22_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectNeptuneOn22_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectNeptuneOff22_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectPlutoOn22_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectPlutoOff22_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectDhOn22_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectDhOff22_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectChironOn22_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectChironOff22_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectAscOn22_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectAscOff22_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectMcOn22_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectMcOff22_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aspectMouseDownCommon(sender, e);
        }

        private void aspectMouseDownCommon(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;

            Image ctl = (Image)anotherControl[img.Name];
            ctl.Visibility = Visibility.Visible;
            ctl.Height = 24;

            int index = dispList.SelectedIndex;
            
            (aspectTempArray[img.Name])[index, aspectSubindex[img.Name]] = aspectBoolean[img.Name];

            foreach (var data in main.list1)
            {
                if (data.no == aspectCommonDataNo[img.Name])
                {
                    data.isAspectDisp = true;
                    break;
                }
            }
            main.ReRender();
        }

        private void createAspectAnotherControlName()
        {
            anotherControl.Add("aspectSunOn11", aspectSunOff11);
            anotherControl.Add("aspectMoonOn11", aspectMoonOff11);
            anotherControl.Add("aspectMercuryOn11", aspectMercuryOff11);
            anotherControl.Add("aspectVenusOn11", aspectVenusOff11);
            anotherControl.Add("aspectMarsOn11", aspectMarsOff11);
            anotherControl.Add("aspectJupiterOn11", aspectJupiterOff11);
            anotherControl.Add("aspectSaturnOn11", aspectSaturnOff11);
            anotherControl.Add("aspectUranusOn11", aspectUranusOff11);
            anotherControl.Add("aspectNeptuneOn11", aspectNeptuneOff11);
            anotherControl.Add("aspectPlutoOn11", aspectPlutoOff11);
            anotherControl.Add("aspectDhOn11", aspectDhOff11);
            anotherControl.Add("aspectChironOn11", aspectChironOff11);
            anotherControl.Add("aspectAscOn11", aspectAscOff11);
            anotherControl.Add("aspectMcOn11", aspectMcOff11);
            anotherControl.Add("aspectSunOn22", aspectSunOff22);
            anotherControl.Add("aspectMoonOn22", aspectMoonOff22);
            anotherControl.Add("aspectMercuryOn22", aspectMercuryOff22);
            anotherControl.Add("aspectVenusOn22", aspectVenusOff22);
            anotherControl.Add("aspectMarsOn22", aspectMarsOff22);
            anotherControl.Add("aspectJupiterOn22", aspectJupiterOff22);
            anotherControl.Add("aspectSaturnOn22", aspectSaturnOff22);
            anotherControl.Add("aspectUranusOn22", aspectUranusOff22);
            anotherControl.Add("aspectNeptuneOn22", aspectNeptuneOff22);
            anotherControl.Add("aspectPlutoOn22", aspectPlutoOff22);
            anotherControl.Add("aspectDhOn22", aspectDhOff22);
            anotherControl.Add("aspectChironOn22", aspectChironOff22);
            anotherControl.Add("aspectAscOn22", aspectAscOff22);
            anotherControl.Add("aspectMcOn22", aspectMcOff22);
            anotherControl.Add("aspectSunOn33", aspectSunOff33);
            anotherControl.Add("aspectMoonOn33", aspectMoonOff33);
            anotherControl.Add("aspectMercuryOn33", aspectMercuryOff33);
            anotherControl.Add("aspectVenusOn33", aspectVenusOff33);
            anotherControl.Add("aspectMarsOn33", aspectMarsOff33);
            anotherControl.Add("aspectJupiterOn33", aspectJupiterOff33);
            anotherControl.Add("aspectSaturnOn33", aspectSaturnOff33);
            anotherControl.Add("aspectUranusOn33", aspectUranusOff33);
            anotherControl.Add("aspectNeptuneOn33", aspectNeptuneOff33);
            anotherControl.Add("aspectPlutoOn33", aspectPlutoOff33);
            anotherControl.Add("aspectDhOn33", aspectDhOff33);
            anotherControl.Add("aspectChironOn33", aspectChironOff33);
            anotherControl.Add("aspectAscOn33", aspectAscOff33);
            anotherControl.Add("aspectMcOn33", aspectMcOff33);
            anotherControl.Add("aspectSunOn12", aspectSunOff12);
            anotherControl.Add("aspectMoonOn12", aspectMoonOff12);
            anotherControl.Add("aspectMercuryOn12", aspectMercuryOff12);
            anotherControl.Add("aspectVenusOn12", aspectVenusOff12);
            anotherControl.Add("aspectMarsOn12", aspectMarsOff12);
            anotherControl.Add("aspectJupiterOn12", aspectJupiterOff12);
            anotherControl.Add("aspectSaturnOn12", aspectSaturnOff12);
            anotherControl.Add("aspectUranusOn12", aspectUranusOff12);
            anotherControl.Add("aspectNeptuneOn12", aspectNeptuneOff12);
            anotherControl.Add("aspectPlutoOn12", aspectPlutoOff12);
            anotherControl.Add("aspectDhOn12", aspectDhOff12);
            anotherControl.Add("aspectChironOn12", aspectChironOff12);
            anotherControl.Add("aspectAscOn12", aspectAscOff12);
            anotherControl.Add("aspectMcOn12", aspectMcOff12);
            anotherControl.Add("aspectSunOn13", aspectSunOff13);
            anotherControl.Add("aspectMoonOn13", aspectMoonOff13);
            anotherControl.Add("aspectMercuryOn13", aspectMercuryOff13);
            anotherControl.Add("aspectVenusOn13", aspectVenusOff13);
            anotherControl.Add("aspectMarsOn13", aspectMarsOff13);
            anotherControl.Add("aspectJupiterOn13", aspectJupiterOff13);
            anotherControl.Add("aspectSaturnOn13", aspectSaturnOff13);
            anotherControl.Add("aspectUranusOn13", aspectUranusOff13);
            anotherControl.Add("aspectNeptuneOn13", aspectNeptuneOff13);
            anotherControl.Add("aspectPlutoOn13", aspectPlutoOff13);
            anotherControl.Add("aspectDhOn13", aspectDhOff13);
            anotherControl.Add("aspectChironOn13", aspectChironOff13);
            anotherControl.Add("aspectAscOn13", aspectAscOff13);
            anotherControl.Add("aspectMcOn13", aspectMcOff13);
            anotherControl.Add("aspectSunOn23", aspectSunOff23);
            anotherControl.Add("aspectMoonOn23", aspectMoonOff23);
            anotherControl.Add("aspectMercuryOn23", aspectMercuryOff23);
            anotherControl.Add("aspectVenusOn23", aspectVenusOff23);
            anotherControl.Add("aspectMarsOn23", aspectMarsOff23);
            anotherControl.Add("aspectJupiterOn23", aspectJupiterOff23);
            anotherControl.Add("aspectSaturnOn23", aspectSaturnOff23);
            anotherControl.Add("aspectUranusOn23", aspectUranusOff23);
            anotherControl.Add("aspectNeptuneOn23", aspectNeptuneOff23);
            anotherControl.Add("aspectPlutoOn23", aspectPlutoOff23);
            anotherControl.Add("aspectDhOn23", aspectDhOff23);
            anotherControl.Add("aspectChironOn23", aspectChironOff23);
            anotherControl.Add("aspectAscOn23", aspectAscOff23);
            anotherControl.Add("aspectMcOn23", aspectMcOff23);
            anotherControl.Add("aspectSunOff11", aspectSunOn11);
            anotherControl.Add("aspectMoonOff11", aspectMoonOn11);
            anotherControl.Add("aspectMercuryOff11", aspectMercuryOn11);
            anotherControl.Add("aspectVenusOff11", aspectVenusOn11);
            anotherControl.Add("aspectMarsOff11", aspectMarsOn11);
            anotherControl.Add("aspectJupiterOff11", aspectJupiterOn11);
            anotherControl.Add("aspectSaturnOff11", aspectSaturnOn11);
            anotherControl.Add("aspectUranusOff11", aspectUranusOn11);
            anotherControl.Add("aspectNeptuneOff11", aspectNeptuneOn11);
            anotherControl.Add("aspectPlutoOff11", aspectPlutoOn11);
            anotherControl.Add("aspectDhOff11", aspectDhOn11);
            anotherControl.Add("aspectChironOff11", aspectChironOn11);
            anotherControl.Add("aspectAscOff11", aspectAscOn11);
            anotherControl.Add("aspectMcOff11", aspectMcOn11);
            anotherControl.Add("aspectSunOff22", aspectSunOn22);
            anotherControl.Add("aspectMoonOff22", aspectMoonOn22);
            anotherControl.Add("aspectMercuryOff22", aspectMercuryOn22);
            anotherControl.Add("aspectVenusOff22", aspectVenusOn22);
            anotherControl.Add("aspectMarsOff22", aspectMarsOn22);
            anotherControl.Add("aspectJupiterOff22", aspectJupiterOn22);
            anotherControl.Add("aspectSaturnOff22", aspectSaturnOn22);
            anotherControl.Add("aspectUranusOff22", aspectUranusOn22);
            anotherControl.Add("aspectNeptuneOff22", aspectNeptuneOn22);
            anotherControl.Add("aspectPlutoOff22", aspectPlutoOn22);
            anotherControl.Add("aspectDhOff22", aspectDhOn22);
            anotherControl.Add("aspectChironOff22", aspectChironOn22);
            anotherControl.Add("aspectAscOff22", aspectAscOn22);
            anotherControl.Add("aspectMcOff22", aspectMcOn22);
            anotherControl.Add("aspectSunOff33", aspectSunOn33);
            anotherControl.Add("aspectMoonOff33", aspectMoonOn33);
            anotherControl.Add("aspectMercuryOff33", aspectMercuryOn33);
            anotherControl.Add("aspectVenusOff33", aspectVenusOn33);
            anotherControl.Add("aspectMarsOff33", aspectMarsOn33);
            anotherControl.Add("aspectJupiterOff33", aspectJupiterOn33);
            anotherControl.Add("aspectSaturnOff33", aspectSaturnOn33);
            anotherControl.Add("aspectUranusOff33", aspectUranusOn33);
            anotherControl.Add("aspectNeptuneOff33", aspectNeptuneOn33);
            anotherControl.Add("aspectPlutoOff33", aspectPlutoOn33);
            anotherControl.Add("aspectDhOff33", aspectDhOn33);
            anotherControl.Add("aspectChironOff33", aspectChironOn33);
            anotherControl.Add("aspectAscOff33", aspectAscOn33);
            anotherControl.Add("aspectMcOff33", aspectMcOn33);
            anotherControl.Add("aspectSunOff12", aspectSunOn12);
            anotherControl.Add("aspectMoonOff12", aspectMoonOn12);
            anotherControl.Add("aspectMercuryOff12", aspectMercuryOn12);
            anotherControl.Add("aspectVenusOff12", aspectVenusOn12);
            anotherControl.Add("aspectMarsOff12", aspectMarsOn12);
            anotherControl.Add("aspectJupiterOff12", aspectJupiterOn12);
            anotherControl.Add("aspectSaturnOff12", aspectSaturnOn12);
            anotherControl.Add("aspectUranusOff12", aspectUranusOn12);
            anotherControl.Add("aspectNeptuneOff12", aspectNeptuneOn12);
            anotherControl.Add("aspectPlutoOff12", aspectPlutoOn12);
            anotherControl.Add("aspectDhOff12", aspectDhOn12);
            anotherControl.Add("aspectChironOff12", aspectChironOn12);
            anotherControl.Add("aspectAscOff12", aspectAscOn12);
            anotherControl.Add("aspectMcOff12", aspectMcOn12);
            anotherControl.Add("aspectSunOff13", aspectSunOn13);
            anotherControl.Add("aspectMoonOff13", aspectMoonOn13);
            anotherControl.Add("aspectMercuryOff13", aspectMercuryOn13);
            anotherControl.Add("aspectVenusOff13", aspectVenusOn13);
            anotherControl.Add("aspectMarsOff13", aspectMarsOn13);
            anotherControl.Add("aspectJupiterOff13", aspectJupiterOn13);
            anotherControl.Add("aspectSaturnOff13", aspectSaturnOn13);
            anotherControl.Add("aspectUranusOff13", aspectUranusOn13);
            anotherControl.Add("aspectNeptuneOff13", aspectNeptuneOn13);
            anotherControl.Add("aspectPlutoOff13", aspectPlutoOn13);
            anotherControl.Add("aspectDhOff13", aspectDhOn13);
            anotherControl.Add("aspectChironOff13", aspectChironOn13);
            anotherControl.Add("aspectAscOff13", aspectAscOn13);
            anotherControl.Add("aspectMcOff13", aspectMcOn13);
            anotherControl.Add("aspectSunOff23", aspectSunOn23);
            anotherControl.Add("aspectMoonOff23", aspectMoonOn23);
            anotherControl.Add("aspectMercuryOff23", aspectMercuryOn23);
            anotherControl.Add("aspectVenusOff23", aspectVenusOn23);
            anotherControl.Add("aspectMarsOff23", aspectMarsOn23);
            anotherControl.Add("aspectJupiterOff23", aspectJupiterOn23);
            anotherControl.Add("aspectSaturnOff23", aspectSaturnOn23);
            anotherControl.Add("aspectUranusOff23", aspectUranusOn23);
            anotherControl.Add("aspectNeptuneOff23", aspectNeptuneOn23);
            anotherControl.Add("aspectPlutoOff23", aspectPlutoOn23);
            anotherControl.Add("aspectDhOff23", aspectDhOn23);
            anotherControl.Add("aspectChironOff23", aspectChironOn23);
            anotherControl.Add("aspectAscOff23", aspectAscOn23);
            anotherControl.Add("aspectMcOff23", aspectMcOn23);

        }

        private void createAspectTempArray()
        {
            aspectTempArray.Add("aspectSunOn11", aspectSun);
            aspectTempArray.Add("aspectMoonOn11", aspectMoon);
            aspectTempArray.Add("aspectMercuryOn11", aspectMercury);
            aspectTempArray.Add("aspectVenusOn11", aspectVenus);
            aspectTempArray.Add("aspectMarsOn11", aspectMars);
            aspectTempArray.Add("aspectJupiterOn11", aspectJupiter);
            aspectTempArray.Add("aspectSaturnOn11", aspectSaturn);
            aspectTempArray.Add("aspectUranusOn11", aspectUranus);
            aspectTempArray.Add("aspectNeptuneOn11", aspectNeptune);
            aspectTempArray.Add("aspectPlutoOn11", aspectPluto);
            aspectTempArray.Add("aspectDhOn11", aspectDh);
            aspectTempArray.Add("aspectChironOn11", aspectChiron);
            aspectTempArray.Add("aspectAscOn11", aspectAsc);
            aspectTempArray.Add("aspectMcOn11", aspectMc);
            aspectTempArray.Add("aspectSunOn22", aspectSun);
            aspectTempArray.Add("aspectMoonOn22", aspectMoon);
            aspectTempArray.Add("aspectMercuryOn22", aspectMercury);
            aspectTempArray.Add("aspectVenusOn22", aspectVenus);
            aspectTempArray.Add("aspectMarsOn22", aspectMars);
            aspectTempArray.Add("aspectJupiterOn22", aspectJupiter);
            aspectTempArray.Add("aspectSaturnOn22", aspectSaturn);
            aspectTempArray.Add("aspectUranusOn22", aspectUranus);
            aspectTempArray.Add("aspectNeptuneOn22", aspectNeptune);
            aspectTempArray.Add("aspectPlutoOn22", aspectPluto);
            aspectTempArray.Add("aspectDhOn22", aspectDh);
            aspectTempArray.Add("aspectChironOn22", aspectChiron);
            aspectTempArray.Add("aspectAscOn22", aspectAsc);
            aspectTempArray.Add("aspectMcOn22", aspectMc);
            aspectTempArray.Add("aspectSunOn33", aspectSun);
            aspectTempArray.Add("aspectMoonOn33", aspectMoon);
            aspectTempArray.Add("aspectMercuryOn33", aspectMercury);
            aspectTempArray.Add("aspectVenusOn33", aspectVenus);
            aspectTempArray.Add("aspectMarsOn33", aspectMars);
            aspectTempArray.Add("aspectJupiterOn33", aspectJupiter);
            aspectTempArray.Add("aspectSaturnOn33", aspectSaturn);
            aspectTempArray.Add("aspectUranusOn33", aspectUranus);
            aspectTempArray.Add("aspectNeptuneOn33", aspectNeptune);
            aspectTempArray.Add("aspectPlutoOn33", aspectPluto);
            aspectTempArray.Add("aspectDhOn33", aspectDh);
            aspectTempArray.Add("aspectChironOn33", aspectChiron);
            aspectTempArray.Add("aspectAscOn33", aspectAsc);
            aspectTempArray.Add("aspectMcOn33", aspectMc);
            aspectTempArray.Add("aspectSunOn12", aspectSun);
            aspectTempArray.Add("aspectMoonOn12", aspectMoon);
            aspectTempArray.Add("aspectMercuryOn12", aspectMercury);
            aspectTempArray.Add("aspectVenusOn12", aspectVenus);
            aspectTempArray.Add("aspectMarsOn12", aspectMars);
            aspectTempArray.Add("aspectJupiterOn12", aspectJupiter);
            aspectTempArray.Add("aspectSaturnOn12", aspectSaturn);
            aspectTempArray.Add("aspectUranusOn12", aspectUranus);
            aspectTempArray.Add("aspectNeptuneOn12", aspectNeptune);
            aspectTempArray.Add("aspectPlutoOn12", aspectPluto);
            aspectTempArray.Add("aspectDhOn12", aspectDh);
            aspectTempArray.Add("aspectChironOn12", aspectChiron);
            aspectTempArray.Add("aspectAscOn12", aspectAsc);
            aspectTempArray.Add("aspectMcOn12", aspectMc);
            aspectTempArray.Add("aspectSunOn13", aspectSun);
            aspectTempArray.Add("aspectMoonOn13", aspectMoon);
            aspectTempArray.Add("aspectMercuryOn13", aspectMercury);
            aspectTempArray.Add("aspectVenusOn13", aspectVenus);
            aspectTempArray.Add("aspectMarsOn13", aspectMars);
            aspectTempArray.Add("aspectJupiterOn13", aspectJupiter);
            aspectTempArray.Add("aspectSaturnOn13", aspectSaturn);
            aspectTempArray.Add("aspectUranusOn13", aspectUranus);
            aspectTempArray.Add("aspectNeptuneOn13", aspectNeptune);
            aspectTempArray.Add("aspectPlutoOn13", aspectPluto);
            aspectTempArray.Add("aspectDhOn13", aspectDh);
            aspectTempArray.Add("aspectChironOn13", aspectChiron);
            aspectTempArray.Add("aspectAscOn13", aspectAsc);
            aspectTempArray.Add("aspectMcOn13", aspectMc);
            aspectTempArray.Add("aspectSunOn23", aspectSun);
            aspectTempArray.Add("aspectMoonOn23", aspectMoon);
            aspectTempArray.Add("aspectMercuryOn23", aspectMercury);
            aspectTempArray.Add("aspectVenusOn23", aspectVenus);
            aspectTempArray.Add("aspectMarsOn23", aspectMars);
            aspectTempArray.Add("aspectJupiterOn23", aspectJupiter);
            aspectTempArray.Add("aspectSaturnOn23", aspectSaturn);
            aspectTempArray.Add("aspectUranusOn23", aspectUranus);
            aspectTempArray.Add("aspectNeptuneOn23", aspectNeptune);
            aspectTempArray.Add("aspectPlutoOn23", aspectPluto);
            aspectTempArray.Add("aspectDhOn23", aspectDh);
            aspectTempArray.Add("aspectChironOn23", aspectChiron);
            aspectTempArray.Add("aspectAscOn23", aspectAsc);
            aspectTempArray.Add("aspectMcOn23", aspectMc);
            aspectTempArray.Add("aspectSunOff11", aspectSun);
            aspectTempArray.Add("aspectMoonOff11", aspectMoon);
            aspectTempArray.Add("aspectMercuryOff11", aspectMercury);
            aspectTempArray.Add("aspectVenusOff11", aspectVenus);
            aspectTempArray.Add("aspectMarsOff11", aspectMars);
            aspectTempArray.Add("aspectJupiterOff11", aspectJupiter);
            aspectTempArray.Add("aspectSaturnOff11", aspectSaturn);
            aspectTempArray.Add("aspectUranusOff11", aspectUranus);
            aspectTempArray.Add("aspectNeptuneOff11", aspectNeptune);
            aspectTempArray.Add("aspectPlutoOff11", aspectPluto);
            aspectTempArray.Add("aspectDhOff11", aspectDh);
            aspectTempArray.Add("aspectChironOff11", aspectChiron);
            aspectTempArray.Add("aspectAscOff11", aspectAsc);
            aspectTempArray.Add("aspectMcOff11", aspectMc);
            aspectTempArray.Add("aspectSunOff22", aspectSun);
            aspectTempArray.Add("aspectMoonOff22", aspectMoon);
            aspectTempArray.Add("aspectMercuryOff22", aspectMercury);
            aspectTempArray.Add("aspectVenusOff22", aspectVenus);
            aspectTempArray.Add("aspectMarsOff22", aspectMars);
            aspectTempArray.Add("aspectJupiterOff22", aspectJupiter);
            aspectTempArray.Add("aspectSaturnOff22", aspectSaturn);
            aspectTempArray.Add("aspectUranusOff22", aspectUranus);
            aspectTempArray.Add("aspectNeptuneOff22", aspectNeptune);
            aspectTempArray.Add("aspectPlutoOff22", aspectPluto);
            aspectTempArray.Add("aspectDhOff22", aspectDh);
            aspectTempArray.Add("aspectChironOff22", aspectChiron);
            aspectTempArray.Add("aspectAscOff22", aspectAsc);
            aspectTempArray.Add("aspectMcOff22", aspectMc);
            aspectTempArray.Add("aspectSunOff33", aspectSun);
            aspectTempArray.Add("aspectMoonOff33", aspectMoon);
            aspectTempArray.Add("aspectMercuryOff33", aspectMercury);
            aspectTempArray.Add("aspectVenusOff33", aspectVenus);
            aspectTempArray.Add("aspectMarsOff33", aspectMars);
            aspectTempArray.Add("aspectJupiterOff33", aspectJupiter);
            aspectTempArray.Add("aspectSaturnOff33", aspectSaturn);
            aspectTempArray.Add("aspectUranusOff33", aspectUranus);
            aspectTempArray.Add("aspectNeptuneOff33", aspectNeptune);
            aspectTempArray.Add("aspectPlutoOff33", aspectPluto);
            aspectTempArray.Add("aspectDhOff33", aspectDh);
            aspectTempArray.Add("aspectChironOff33", aspectChiron);
            aspectTempArray.Add("aspectAscOff33", aspectAsc);
            aspectTempArray.Add("aspectMcOff33", aspectMc);
            aspectTempArray.Add("aspectSunOff12", aspectSun);
            aspectTempArray.Add("aspectMoonOff12", aspectMoon);
            aspectTempArray.Add("aspectMercuryOff12", aspectMercury);
            aspectTempArray.Add("aspectVenusOff12", aspectVenus);
            aspectTempArray.Add("aspectMarsOff12", aspectMars);
            aspectTempArray.Add("aspectJupiterOff12", aspectJupiter);
            aspectTempArray.Add("aspectSaturnOff12", aspectSaturn);
            aspectTempArray.Add("aspectUranusOff12", aspectUranus);
            aspectTempArray.Add("aspectNeptuneOff12", aspectNeptune);
            aspectTempArray.Add("aspectPlutoOff12", aspectPluto);
            aspectTempArray.Add("aspectDhOff12", aspectDh);
            aspectTempArray.Add("aspectChironOff12", aspectChiron);
            aspectTempArray.Add("aspectAscOff12", aspectAsc);
            aspectTempArray.Add("aspectMcOff12", aspectMc);
            aspectTempArray.Add("aspectSunOff13", aspectSun);
            aspectTempArray.Add("aspectMoonOff13", aspectMoon);
            aspectTempArray.Add("aspectMercuryOff13", aspectMercury);
            aspectTempArray.Add("aspectVenusOff13", aspectVenus);
            aspectTempArray.Add("aspectMarsOff13", aspectMars);
            aspectTempArray.Add("aspectJupiterOff13", aspectJupiter);
            aspectTempArray.Add("aspectSaturnOff13", aspectSaturn);
            aspectTempArray.Add("aspectUranusOff13", aspectUranus);
            aspectTempArray.Add("aspectNeptuneOff13", aspectNeptune);
            aspectTempArray.Add("aspectPlutoOff13", aspectPluto);
            aspectTempArray.Add("aspectDhOff13", aspectDh);
            aspectTempArray.Add("aspectChironOff13", aspectChiron);
            aspectTempArray.Add("aspectAscOff13", aspectAsc);
            aspectTempArray.Add("aspectMcOff13", aspectMc);
            aspectTempArray.Add("aspectSunOff23", aspectSun);
            aspectTempArray.Add("aspectMoonOff23", aspectMoon);
            aspectTempArray.Add("aspectMercuryOff23", aspectMercury);
            aspectTempArray.Add("aspectVenusOff23", aspectVenus);
            aspectTempArray.Add("aspectMarsOff23", aspectMars);
            aspectTempArray.Add("aspectJupiterOff23", aspectJupiter);
            aspectTempArray.Add("aspectSaturnOff23", aspectSaturn);
            aspectTempArray.Add("aspectUranusOff23", aspectUranus);
            aspectTempArray.Add("aspectNeptuneOff23", aspectNeptune);
            aspectTempArray.Add("aspectPlutoOff23", aspectPluto);
            aspectTempArray.Add("aspectDhOff23", aspectDh);
            aspectTempArray.Add("aspectChironOff23", aspectChiron);
            aspectTempArray.Add("aspectAscOff23", aspectAsc);
            aspectTempArray.Add("aspectMcOff23", aspectMc);
        }

        private void createAspectBoolean()
        {
            aspectBoolean.Add("aspectSunOn11", true);
            aspectBoolean.Add("aspectMoonOn11", true);
            aspectBoolean.Add("aspectMercuryOn11", true);
            aspectBoolean.Add("aspectVenusOn11", true);
            aspectBoolean.Add("aspectMarsOn11", true);
            aspectBoolean.Add("aspectJupiterOn11", true);
            aspectBoolean.Add("aspectSaturnOn11", true);
            aspectBoolean.Add("aspectUranusOn11", true);
            aspectBoolean.Add("aspectNeptuneOn11", true);
            aspectBoolean.Add("aspectPlutoOn11", true);
            aspectBoolean.Add("aspectDhOn11", true);
            aspectBoolean.Add("aspectChironOn11", true);
            aspectBoolean.Add("aspectAscOn11", true);
            aspectBoolean.Add("aspectMcOn11", true);
            aspectBoolean.Add("aspectSunOn22", true);
            aspectBoolean.Add("aspectMoonOn22", true);
            aspectBoolean.Add("aspectMercuryOn22", true);
            aspectBoolean.Add("aspectVenusOn22", true);
            aspectBoolean.Add("aspectMarsOn22", true);
            aspectBoolean.Add("aspectJupiterOn22", true);
            aspectBoolean.Add("aspectSaturnOn22", true);
            aspectBoolean.Add("aspectUranusOn22", true);
            aspectBoolean.Add("aspectNeptuneOn22", true);
            aspectBoolean.Add("aspectPlutoOn22", true);
            aspectBoolean.Add("aspectDhOn22", true);
            aspectBoolean.Add("aspectChironOn22", true);
            aspectBoolean.Add("aspectAscOn22", true);
            aspectBoolean.Add("aspectMcOn22", true);
            aspectBoolean.Add("aspectSunOn33", true);
            aspectBoolean.Add("aspectMoonOn33", true);
            aspectBoolean.Add("aspectMercuryOn33", true);
            aspectBoolean.Add("aspectVenusOn33", true);
            aspectBoolean.Add("aspectMarsOn33", true);
            aspectBoolean.Add("aspectJupiterOn33", true);
            aspectBoolean.Add("aspectSaturnOn33", true);
            aspectBoolean.Add("aspectUranusOn33", true);
            aspectBoolean.Add("aspectNeptuneOn33", true);
            aspectBoolean.Add("aspectPlutoOn33", true);
            aspectBoolean.Add("aspectDhOn33", true);
            aspectBoolean.Add("aspectChironOn33", true);
            aspectBoolean.Add("aspectAscOn33", true);
            aspectBoolean.Add("aspectMcOn33", true);
            aspectBoolean.Add("aspectSunOn12", true);
            aspectBoolean.Add("aspectMoonOn12", true);
            aspectBoolean.Add("aspectMercuryOn12", true);
            aspectBoolean.Add("aspectVenusOn12", true);
            aspectBoolean.Add("aspectMarsOn12", true);
            aspectBoolean.Add("aspectJupiterOn12", true);
            aspectBoolean.Add("aspectSaturnOn12", true);
            aspectBoolean.Add("aspectUranusOn12", true);
            aspectBoolean.Add("aspectNeptuneOn12", true);
            aspectBoolean.Add("aspectPlutoOn12", true);
            aspectBoolean.Add("aspectDhOn12", true);
            aspectBoolean.Add("aspectChironOn12", true);
            aspectBoolean.Add("aspectAscOn12", true);
            aspectBoolean.Add("aspectMcOn12", true);
            aspectBoolean.Add("aspectSunOn13", true);
            aspectBoolean.Add("aspectMoonOn13", true);
            aspectBoolean.Add("aspectMercuryOn13", true);
            aspectBoolean.Add("aspectVenusOn13", true);
            aspectBoolean.Add("aspectMarsOn13", true);
            aspectBoolean.Add("aspectJupiterOn13", true);
            aspectBoolean.Add("aspectSaturnOn13", true);
            aspectBoolean.Add("aspectUranusOn13", true);
            aspectBoolean.Add("aspectNeptuneOn13", true);
            aspectBoolean.Add("aspectPlutoOn13", true);
            aspectBoolean.Add("aspectDhOn13", true);
            aspectBoolean.Add("aspectChironOn13", true);
            aspectBoolean.Add("aspectAscOn13", true);
            aspectBoolean.Add("aspectMcOn13", true);
            aspectBoolean.Add("aspectSunOn23", true);
            aspectBoolean.Add("aspectMoonOn23", true);
            aspectBoolean.Add("aspectMercuryOn23", true);
            aspectBoolean.Add("aspectVenusOn23", true);
            aspectBoolean.Add("aspectMarsOn23", true);
            aspectBoolean.Add("aspectJupiterOn23", true);
            aspectBoolean.Add("aspectSaturnOn23", true);
            aspectBoolean.Add("aspectUranusOn23", true);
            aspectBoolean.Add("aspectNeptuneOn23", true);
            aspectBoolean.Add("aspectPlutoOn23", true);
            aspectBoolean.Add("aspectDhOn23", true);
            aspectBoolean.Add("aspectChironOn23", true);
            aspectBoolean.Add("aspectAscOn23", true);
            aspectBoolean.Add("aspectMcOn23", true);
            aspectBoolean.Add("aspectSunOff11", false);
            aspectBoolean.Add("aspectMoonOff11", false);
            aspectBoolean.Add("aspectMercuryOff11", false);
            aspectBoolean.Add("aspectVenusOff11", false);
            aspectBoolean.Add("aspectMarsOff11", false);
            aspectBoolean.Add("aspectJupiterOff11", false);
            aspectBoolean.Add("aspectSaturnOff11", false);
            aspectBoolean.Add("aspectUranusOff11", false);
            aspectBoolean.Add("aspectNeptuneOff11", false);
            aspectBoolean.Add("aspectPlutoOff11", false);
            aspectBoolean.Add("aspectDhOff11", false);
            aspectBoolean.Add("aspectChironOff11", false);
            aspectBoolean.Add("aspectAscOff11", false);
            aspectBoolean.Add("aspectMcOff11", false);
            aspectBoolean.Add("aspectSunOff22", false);
            aspectBoolean.Add("aspectMoonOff22", false);
            aspectBoolean.Add("aspectMercuryOff22", false);
            aspectBoolean.Add("aspectVenusOff22", false);
            aspectBoolean.Add("aspectMarsOff22", false);
            aspectBoolean.Add("aspectJupiterOff22", false);
            aspectBoolean.Add("aspectSaturnOff22", false);
            aspectBoolean.Add("aspectUranusOff22", false);
            aspectBoolean.Add("aspectNeptuneOff22", false);
            aspectBoolean.Add("aspectPlutoOff22", false);
            aspectBoolean.Add("aspectDhOff22", false);
            aspectBoolean.Add("aspectChironOff22", false);
            aspectBoolean.Add("aspectAscOff22", false);
            aspectBoolean.Add("aspectMcOff22", false);
            aspectBoolean.Add("aspectSunOff33", false);
            aspectBoolean.Add("aspectMoonOff33", false);
            aspectBoolean.Add("aspectMercuryOff33", false);
            aspectBoolean.Add("aspectVenusOff33", false);
            aspectBoolean.Add("aspectMarsOff33", false);
            aspectBoolean.Add("aspectJupiterOff33", false);
            aspectBoolean.Add("aspectSaturnOff33", false);
            aspectBoolean.Add("aspectUranusOff33", false);
            aspectBoolean.Add("aspectNeptuneOff33", false);
            aspectBoolean.Add("aspectPlutoOff33", false);
            aspectBoolean.Add("aspectDhOff33", false);
            aspectBoolean.Add("aspectChironOff33", false);
            aspectBoolean.Add("aspectAscOff33", false);
            aspectBoolean.Add("aspectMcOff33", false);
            aspectBoolean.Add("aspectSunOff12", false);
            aspectBoolean.Add("aspectMoonOff12", false);
            aspectBoolean.Add("aspectMercuryOff12", false);
            aspectBoolean.Add("aspectVenusOff12", false);
            aspectBoolean.Add("aspectMarsOff12", false);
            aspectBoolean.Add("aspectJupiterOff12", false);
            aspectBoolean.Add("aspectSaturnOff12", false);
            aspectBoolean.Add("aspectUranusOff12", false);
            aspectBoolean.Add("aspectNeptuneOff12", false);
            aspectBoolean.Add("aspectPlutoOff12", false);
            aspectBoolean.Add("aspectDhOff12", false);
            aspectBoolean.Add("aspectChironOff12", false);
            aspectBoolean.Add("aspectAscOff12", false);
            aspectBoolean.Add("aspectMcOff12", false);
            aspectBoolean.Add("aspectSunOff13", false);
            aspectBoolean.Add("aspectMoonOff13", false);
            aspectBoolean.Add("aspectMercuryOff13", false);
            aspectBoolean.Add("aspectVenusOff13", false);
            aspectBoolean.Add("aspectMarsOff13", false);
            aspectBoolean.Add("aspectJupiterOff13", false);
            aspectBoolean.Add("aspectSaturnOff13", false);
            aspectBoolean.Add("aspectUranusOff13", false);
            aspectBoolean.Add("aspectNeptuneOff13", false);
            aspectBoolean.Add("aspectPlutoOff13", false);
            aspectBoolean.Add("aspectDhOff13", false);
            aspectBoolean.Add("aspectChironOff13", false);
            aspectBoolean.Add("aspectAscOff13", false);
            aspectBoolean.Add("aspectMcOff13", false);
            aspectBoolean.Add("aspectSunOff23", false);
            aspectBoolean.Add("aspectMoonOff23", false);
            aspectBoolean.Add("aspectMercuryOff23", false);
            aspectBoolean.Add("aspectVenusOff23", false);
            aspectBoolean.Add("aspectMarsOff23", false);
            aspectBoolean.Add("aspectJupiterOff23", false);
            aspectBoolean.Add("aspectSaturnOff23", false);
            aspectBoolean.Add("aspectUranusOff23", false);
            aspectBoolean.Add("aspectNeptuneOff23", false);
            aspectBoolean.Add("aspectPlutoOff23", false);
            aspectBoolean.Add("aspectDhOff23", false);
            aspectBoolean.Add("aspectChironOff23", false);
            aspectBoolean.Add("aspectAscOff23", false);
            aspectBoolean.Add("aspectMcOff23", false);
        }

        private void createAspectSubindex()
        {
            aspectSubindex.Add("aspectSunOn11", 0);
            aspectSubindex.Add("aspectMoonOn11", 0);
            aspectSubindex.Add("aspectMercuryOn11", 0);
            aspectSubindex.Add("aspectVenusOn11", 0);
            aspectSubindex.Add("aspectMarsOn11", 0);
            aspectSubindex.Add("aspectJupiterOn11", 0);
            aspectSubindex.Add("aspectSaturnOn11", 0);
            aspectSubindex.Add("aspectUranusOn11", 0);
            aspectSubindex.Add("aspectNeptuneOn11", 0);
            aspectSubindex.Add("aspectPlutoOn11", 0);
            aspectSubindex.Add("aspectDhOn11", 0);
            aspectSubindex.Add("aspectChironOn11", 0);
            aspectSubindex.Add("aspectAscOn11", 0);
            aspectSubindex.Add("aspectMcOn11", 0);
            aspectSubindex.Add("aspectSunOn22", 1);
            aspectSubindex.Add("aspectMoonOn22", 1);
            aspectSubindex.Add("aspectMercuryOn22", 1);
            aspectSubindex.Add("aspectVenusOn22", 1);
            aspectSubindex.Add("aspectMarsOn22", 1);
            aspectSubindex.Add("aspectJupiterOn22", 1);
            aspectSubindex.Add("aspectSaturnOn22", 1);
            aspectSubindex.Add("aspectUranusOn22", 1);
            aspectSubindex.Add("aspectNeptuneOn22", 1);
            aspectSubindex.Add("aspectPlutoOn22", 1);
            aspectSubindex.Add("aspectDhOn22", 1);
            aspectSubindex.Add("aspectChironOn22", 1);
            aspectSubindex.Add("aspectAscOn22", 1);
            aspectSubindex.Add("aspectMcOn22", 1);
            aspectSubindex.Add("aspectSunOn33", 2);
            aspectSubindex.Add("aspectMoonOn33", 2);
            aspectSubindex.Add("aspectMercuryOn33", 2);
            aspectSubindex.Add("aspectVenusOn33", 2);
            aspectSubindex.Add("aspectMarsOn33", 2);
            aspectSubindex.Add("aspectJupiterOn33", 2);
            aspectSubindex.Add("aspectSaturnOn33", 2);
            aspectSubindex.Add("aspectUranusOn33", 2);
            aspectSubindex.Add("aspectNeptuneOn33", 2);
            aspectSubindex.Add("aspectPlutoOn33", 2);
            aspectSubindex.Add("aspectDhOn33", 2);
            aspectSubindex.Add("aspectChironOn33", 2);
            aspectSubindex.Add("aspectAscOn33", 2);
            aspectSubindex.Add("aspectMcOn33", 2);
            aspectSubindex.Add("aspectSunOn12", 3);
            aspectSubindex.Add("aspectMoonOn12", 3);
            aspectSubindex.Add("aspectMercuryOn12", 3);
            aspectSubindex.Add("aspectVenusOn12", 3);
            aspectSubindex.Add("aspectMarsOn12", 3);
            aspectSubindex.Add("aspectJupiterOn12", 3);
            aspectSubindex.Add("aspectSaturnOn12", 3);
            aspectSubindex.Add("aspectUranusOn12", 3);
            aspectSubindex.Add("aspectNeptuneOn12", 3);
            aspectSubindex.Add("aspectPlutoOn12", 3);
            aspectSubindex.Add("aspectDhOn12", 3);
            aspectSubindex.Add("aspectChironOn12", 3);
            aspectSubindex.Add("aspectAscOn12", 3);
            aspectSubindex.Add("aspectMcOn12", 3);
            aspectSubindex.Add("aspectSunOn13", 4);
            aspectSubindex.Add("aspectMoonOn13", 4);
            aspectSubindex.Add("aspectMercuryOn13", 4);
            aspectSubindex.Add("aspectVenusOn13", 4);
            aspectSubindex.Add("aspectMarsOn13", 4);
            aspectSubindex.Add("aspectJupiterOn13", 4);
            aspectSubindex.Add("aspectSaturnOn13", 4);
            aspectSubindex.Add("aspectUranusOn13", 4);
            aspectSubindex.Add("aspectNeptuneOn13", 4);
            aspectSubindex.Add("aspectPlutoOn13", 4);
            aspectSubindex.Add("aspectDhOn13", 4);
            aspectSubindex.Add("aspectChironOn13", 4);
            aspectSubindex.Add("aspectAscOn13", 4);
            aspectSubindex.Add("aspectMcOn13", 4);
            aspectSubindex.Add("aspectSunOn23", 5);
            aspectSubindex.Add("aspectMoonOn23", 5);
            aspectSubindex.Add("aspectMercuryOn23", 5);
            aspectSubindex.Add("aspectVenusOn23", 5);
            aspectSubindex.Add("aspectMarsOn23", 5);
            aspectSubindex.Add("aspectJupiterOn23", 5);
            aspectSubindex.Add("aspectSaturnOn23", 5);
            aspectSubindex.Add("aspectUranusOn23", 5);
            aspectSubindex.Add("aspectNeptuneOn23", 5);
            aspectSubindex.Add("aspectPlutoOn23", 5);
            aspectSubindex.Add("aspectDhOn23", 5);
            aspectSubindex.Add("aspectChironOn23", 5);
            aspectSubindex.Add("aspectAscOn23", 5);
            aspectSubindex.Add("aspectMcOn23", 5);
            aspectSubindex.Add("aspectSunOff11", 0);
            aspectSubindex.Add("aspectMoonOff11", 0);
            aspectSubindex.Add("aspectMercuryOff11", 0);
            aspectSubindex.Add("aspectVenusOff11", 0);
            aspectSubindex.Add("aspectMarsOff11", 0);
            aspectSubindex.Add("aspectJupiterOff11", 0);
            aspectSubindex.Add("aspectSaturnOff11", 0);
            aspectSubindex.Add("aspectUranusOff11", 0);
            aspectSubindex.Add("aspectNeptuneOff11", 0);
            aspectSubindex.Add("aspectPlutoOff11", 0);
            aspectSubindex.Add("aspectDhOff11", 0);
            aspectSubindex.Add("aspectChironOff11", 0);
            aspectSubindex.Add("aspectAscOff11", 0);
            aspectSubindex.Add("aspectMcOff11", 0);
            aspectSubindex.Add("aspectSunOff22", 1);
            aspectSubindex.Add("aspectMoonOff22", 1);
            aspectSubindex.Add("aspectMercuryOff22", 1);
            aspectSubindex.Add("aspectVenusOff22", 1);
            aspectSubindex.Add("aspectMarsOff22", 1);
            aspectSubindex.Add("aspectJupiterOff22", 1);
            aspectSubindex.Add("aspectSaturnOff22", 1);
            aspectSubindex.Add("aspectUranusOff22", 1);
            aspectSubindex.Add("aspectNeptuneOff22", 1);
            aspectSubindex.Add("aspectPlutoOff22", 1);
            aspectSubindex.Add("aspectDhOff22", 1);
            aspectSubindex.Add("aspectChironOff22", 1);
            aspectSubindex.Add("aspectAscOff22", 1);
            aspectSubindex.Add("aspectMcOff22", 1);
            aspectSubindex.Add("aspectSunOff33", 2);
            aspectSubindex.Add("aspectMoonOff33", 2);
            aspectSubindex.Add("aspectMercuryOff33", 2);
            aspectSubindex.Add("aspectVenusOff33", 2);
            aspectSubindex.Add("aspectMarsOff33", 2);
            aspectSubindex.Add("aspectJupiterOff33", 2);
            aspectSubindex.Add("aspectSaturnOff33", 2);
            aspectSubindex.Add("aspectUranusOff33", 2);
            aspectSubindex.Add("aspectNeptuneOff33", 2);
            aspectSubindex.Add("aspectPlutoOff33", 2);
            aspectSubindex.Add("aspectDhOff33", 2);
            aspectSubindex.Add("aspectChironOff33", 2);
            aspectSubindex.Add("aspectAscOff33", 2);
            aspectSubindex.Add("aspectMcOff33", 2);
            aspectSubindex.Add("aspectSunOff12", 3);
            aspectSubindex.Add("aspectMoonOff12", 3);
            aspectSubindex.Add("aspectMercuryOff12", 3);
            aspectSubindex.Add("aspectVenusOff12", 3);
            aspectSubindex.Add("aspectMarsOff12", 3);
            aspectSubindex.Add("aspectJupiterOff12", 3);
            aspectSubindex.Add("aspectSaturnOff12", 3);
            aspectSubindex.Add("aspectUranusOff12", 3);
            aspectSubindex.Add("aspectNeptuneOff12", 3);
            aspectSubindex.Add("aspectPlutoOff12", 3);
            aspectSubindex.Add("aspectDhOff12", 3);
            aspectSubindex.Add("aspectChironOff12", 3);
            aspectSubindex.Add("aspectAscOff12", 3);
            aspectSubindex.Add("aspectMcOff12", 3);
            aspectSubindex.Add("aspectSunOff13", 4);
            aspectSubindex.Add("aspectMoonOff13", 4);
            aspectSubindex.Add("aspectMercuryOff13", 4);
            aspectSubindex.Add("aspectVenusOff13", 4);
            aspectSubindex.Add("aspectMarsOff13", 4);
            aspectSubindex.Add("aspectJupiterOff13", 4);
            aspectSubindex.Add("aspectSaturnOff13", 4);
            aspectSubindex.Add("aspectUranusOff13", 4);
            aspectSubindex.Add("aspectNeptuneOff13", 4);
            aspectSubindex.Add("aspectPlutoOff13", 4);
            aspectSubindex.Add("aspectDhOff13", 4);
            aspectSubindex.Add("aspectChironOff13", 4);
            aspectSubindex.Add("aspectAscOff13", 4);
            aspectSubindex.Add("aspectMcOff13", 4);
            aspectSubindex.Add("aspectSunOff23", 5);
            aspectSubindex.Add("aspectMoonOff23", 5);
            aspectSubindex.Add("aspectMercuryOff23", 5);
            aspectSubindex.Add("aspectVenusOff23", 5);
            aspectSubindex.Add("aspectMarsOff23", 5);
            aspectSubindex.Add("aspectJupiterOff23", 5);
            aspectSubindex.Add("aspectSaturnOff23", 5);
            aspectSubindex.Add("aspectUranusOff23", 5);
            aspectSubindex.Add("aspectNeptuneOff23", 5);
            aspectSubindex.Add("aspectPlutoOff23", 5);
            aspectSubindex.Add("aspectDhOff23", 5);
            aspectSubindex.Add("aspectChironOff23", 5);
            aspectSubindex.Add("aspectAscOff23", 5);
            aspectSubindex.Add("aspectMcOff23", 5);
        }

        private void createAspectCommonDataNo()
        {
            aspectCommonDataNo.Add("aspectSunOff11", (int)CommonData.ZODIAC_SUN);
            aspectCommonDataNo.Add("aspectMoonOff11", (int)CommonData.ZODIAC_MOON);
            aspectCommonDataNo.Add("aspectMercuryOff11", (int)CommonData.ZODIAC_MERCURY);
            aspectCommonDataNo.Add("aspectVenusOff11", (int)CommonData.ZODIAC_VENUS);
            aspectCommonDataNo.Add("aspectMarsOff11", (int)CommonData.ZODIAC_MARS);
            aspectCommonDataNo.Add("aspectJupiterOff11", (int)CommonData.ZODIAC_JUPITER);
            aspectCommonDataNo.Add("aspectSaturnOff11", (int)CommonData.ZODIAC_SATURN);
            aspectCommonDataNo.Add("aspectUranusOff11", (int)CommonData.ZODIAC_URANUS);
            aspectCommonDataNo.Add("aspectNeptuneOff11", (int)CommonData.ZODIAC_NEPTUNE);
            aspectCommonDataNo.Add("aspectPlutoOff11", (int)CommonData.ZODIAC_PLUTO);
            aspectCommonDataNo.Add("aspectDhOff11", (int)CommonData.ZODIAC_DH_TRUENODE);
            aspectCommonDataNo.Add("aspectChironOff11", (int)CommonData.ZODIAC_CHIRON);
            aspectCommonDataNo.Add("aspectAscOff11", (int)CommonData.ZODIAC_ASC);
            aspectCommonDataNo.Add("aspectMcOff11", (int)CommonData.ZODIAC_MC);
            aspectCommonDataNo.Add("aspectSunOff22", (int)CommonData.ZODIAC_SUN);
            aspectCommonDataNo.Add("aspectMoonOff22", (int)CommonData.ZODIAC_MOON);
            aspectCommonDataNo.Add("aspectMercuryOff22", (int)CommonData.ZODIAC_MERCURY);
            aspectCommonDataNo.Add("aspectVenusOff22", (int)CommonData.ZODIAC_VENUS);
            aspectCommonDataNo.Add("aspectMarsOff22", (int)CommonData.ZODIAC_MARS);
            aspectCommonDataNo.Add("aspectJupiterOff22", (int)CommonData.ZODIAC_JUPITER);
            aspectCommonDataNo.Add("aspectSaturnOff22", (int)CommonData.ZODIAC_SATURN);
            aspectCommonDataNo.Add("aspectUranusOff22", (int)CommonData.ZODIAC_URANUS);
            aspectCommonDataNo.Add("aspectNeptuneOff22", (int)CommonData.ZODIAC_NEPTUNE);
            aspectCommonDataNo.Add("aspectPlutoOff22", (int)CommonData.ZODIAC_PLUTO);
            aspectCommonDataNo.Add("aspectDhOff22", (int)CommonData.ZODIAC_DH_TRUENODE);
            aspectCommonDataNo.Add("aspectChironOff22", (int)CommonData.ZODIAC_CHIRON);
            aspectCommonDataNo.Add("aspectAscOff22", (int)CommonData.ZODIAC_ASC);
            aspectCommonDataNo.Add("aspectMcOff22", (int)CommonData.ZODIAC_MC);
            aspectCommonDataNo.Add("aspectSunOff33", (int)CommonData.ZODIAC_SUN);
            aspectCommonDataNo.Add("aspectMoonOff33", (int)CommonData.ZODIAC_MOON);
            aspectCommonDataNo.Add("aspectMercuryOff33", (int)CommonData.ZODIAC_MERCURY);
            aspectCommonDataNo.Add("aspectVenusOff33", (int)CommonData.ZODIAC_VENUS);
            aspectCommonDataNo.Add("aspectMarsOff33", (int)CommonData.ZODIAC_MARS);
            aspectCommonDataNo.Add("aspectJupiterOff33", (int)CommonData.ZODIAC_JUPITER);
            aspectCommonDataNo.Add("aspectSaturnOff33", (int)CommonData.ZODIAC_SATURN);
            aspectCommonDataNo.Add("aspectUranusOff33", (int)CommonData.ZODIAC_URANUS);
            aspectCommonDataNo.Add("aspectNeptuneOff33", (int)CommonData.ZODIAC_NEPTUNE);
            aspectCommonDataNo.Add("aspectPlutoOff33", (int)CommonData.ZODIAC_PLUTO);
            aspectCommonDataNo.Add("aspectDhOff33", (int)CommonData.ZODIAC_DH_TRUENODE);
            aspectCommonDataNo.Add("aspectChironOff33", (int)CommonData.ZODIAC_CHIRON);
            aspectCommonDataNo.Add("aspectAscOff33", (int)CommonData.ZODIAC_ASC);
            aspectCommonDataNo.Add("aspectMcOff33", (int)CommonData.ZODIAC_MC);
            aspectCommonDataNo.Add("aspectSunOff12", (int)CommonData.ZODIAC_SUN);
            aspectCommonDataNo.Add("aspectMoonOff12", (int)CommonData.ZODIAC_MOON);
            aspectCommonDataNo.Add("aspectMercuryOff12", (int)CommonData.ZODIAC_MERCURY);
            aspectCommonDataNo.Add("aspectVenusOff12", (int)CommonData.ZODIAC_VENUS);
            aspectCommonDataNo.Add("aspectMarsOff12", (int)CommonData.ZODIAC_MARS);
            aspectCommonDataNo.Add("aspectJupiterOff12", (int)CommonData.ZODIAC_JUPITER);
            aspectCommonDataNo.Add("aspectSaturnOff12", (int)CommonData.ZODIAC_SATURN);
            aspectCommonDataNo.Add("aspectUranusOff12", (int)CommonData.ZODIAC_URANUS);
            aspectCommonDataNo.Add("aspectNeptuneOff12", (int)CommonData.ZODIAC_NEPTUNE);
            aspectCommonDataNo.Add("aspectPlutoOff12", (int)CommonData.ZODIAC_PLUTO);
            aspectCommonDataNo.Add("aspectDhOff12", (int)CommonData.ZODIAC_DH_TRUENODE);
            aspectCommonDataNo.Add("aspectChironOff12", (int)CommonData.ZODIAC_CHIRON);
            aspectCommonDataNo.Add("aspectAscOff12", (int)CommonData.ZODIAC_ASC);
            aspectCommonDataNo.Add("aspectMcOff12", (int)CommonData.ZODIAC_MC);
            aspectCommonDataNo.Add("aspectSunOff13", (int)CommonData.ZODIAC_SUN);
            aspectCommonDataNo.Add("aspectMoonOff13", (int)CommonData.ZODIAC_MOON);
            aspectCommonDataNo.Add("aspectMercuryOff13", (int)CommonData.ZODIAC_MERCURY);
            aspectCommonDataNo.Add("aspectVenusOff13", (int)CommonData.ZODIAC_VENUS);
            aspectCommonDataNo.Add("aspectMarsOff13", (int)CommonData.ZODIAC_MARS);
            aspectCommonDataNo.Add("aspectJupiterOff13", (int)CommonData.ZODIAC_JUPITER);
            aspectCommonDataNo.Add("aspectSaturnOff13", (int)CommonData.ZODIAC_SATURN);
            aspectCommonDataNo.Add("aspectUranusOff13", (int)CommonData.ZODIAC_URANUS);
            aspectCommonDataNo.Add("aspectNeptuneOff13", (int)CommonData.ZODIAC_NEPTUNE);
            aspectCommonDataNo.Add("aspectPlutoOff13", (int)CommonData.ZODIAC_PLUTO);
            aspectCommonDataNo.Add("aspectDhOff13", (int)CommonData.ZODIAC_DH_TRUENODE);
            aspectCommonDataNo.Add("aspectChironOff13", (int)CommonData.ZODIAC_CHIRON);
            aspectCommonDataNo.Add("aspectAscOff13", (int)CommonData.ZODIAC_ASC);
            aspectCommonDataNo.Add("aspectMcOff13", (int)CommonData.ZODIAC_MC);
            aspectCommonDataNo.Add("aspectSunOff23", (int)CommonData.ZODIAC_SUN);
            aspectCommonDataNo.Add("aspectMoonOff23", (int)CommonData.ZODIAC_MOON);
            aspectCommonDataNo.Add("aspectMercuryOff23", (int)CommonData.ZODIAC_MERCURY);
            aspectCommonDataNo.Add("aspectVenusOff23", (int)CommonData.ZODIAC_VENUS);
            aspectCommonDataNo.Add("aspectMarsOff23", (int)CommonData.ZODIAC_MARS);
            aspectCommonDataNo.Add("aspectJupiterOff23", (int)CommonData.ZODIAC_JUPITER);
            aspectCommonDataNo.Add("aspectSaturnOff23", (int)CommonData.ZODIAC_SATURN);
            aspectCommonDataNo.Add("aspectUranusOff23", (int)CommonData.ZODIAC_URANUS);
            aspectCommonDataNo.Add("aspectNeptuneOff23", (int)CommonData.ZODIAC_NEPTUNE);
            aspectCommonDataNo.Add("aspectPlutoOff23", (int)CommonData.ZODIAC_PLUTO);
            aspectCommonDataNo.Add("aspectDhOff23", (int)CommonData.ZODIAC_DH_TRUENODE);
            aspectCommonDataNo.Add("aspectChironOff23", (int)CommonData.ZODIAC_CHIRON);
            aspectCommonDataNo.Add("aspectAscOff23", (int)CommonData.ZODIAC_ASC);
            aspectCommonDataNo.Add("aspectMcOff23", (int)CommonData.ZODIAC_MC);
            aspectCommonDataNo.Add("aspectSunOn11", (int)CommonData.ZODIAC_SUN);
            aspectCommonDataNo.Add("aspectMoonOn11", (int)CommonData.ZODIAC_MOON);
            aspectCommonDataNo.Add("aspectMercuryOn11", (int)CommonData.ZODIAC_MERCURY);
            aspectCommonDataNo.Add("aspectVenusOn11", (int)CommonData.ZODIAC_VENUS);
            aspectCommonDataNo.Add("aspectMarsOn11", (int)CommonData.ZODIAC_MARS);
            aspectCommonDataNo.Add("aspectJupiterOn11", (int)CommonData.ZODIAC_JUPITER);
            aspectCommonDataNo.Add("aspectSaturnOn11", (int)CommonData.ZODIAC_SATURN);
            aspectCommonDataNo.Add("aspectUranusOn11", (int)CommonData.ZODIAC_URANUS);
            aspectCommonDataNo.Add("aspectNeptuneOn11", (int)CommonData.ZODIAC_NEPTUNE);
            aspectCommonDataNo.Add("aspectPlutoOn11", (int)CommonData.ZODIAC_PLUTO);
            aspectCommonDataNo.Add("aspectDhOn11", (int)CommonData.ZODIAC_DH_TRUENODE);
            aspectCommonDataNo.Add("aspectChironOn11", (int)CommonData.ZODIAC_CHIRON);
            aspectCommonDataNo.Add("aspectAscOn11", (int)CommonData.ZODIAC_ASC);
            aspectCommonDataNo.Add("aspectMcOn11", (int)CommonData.ZODIAC_MC);
            aspectCommonDataNo.Add("aspectSunOn22", (int)CommonData.ZODIAC_SUN);
            aspectCommonDataNo.Add("aspectMoonOn22", (int)CommonData.ZODIAC_MOON);
            aspectCommonDataNo.Add("aspectMercuryOn22", (int)CommonData.ZODIAC_MERCURY);
            aspectCommonDataNo.Add("aspectVenusOn22", (int)CommonData.ZODIAC_VENUS);
            aspectCommonDataNo.Add("aspectMarsOn22", (int)CommonData.ZODIAC_MARS);
            aspectCommonDataNo.Add("aspectJupiterOn22", (int)CommonData.ZODIAC_JUPITER);
            aspectCommonDataNo.Add("aspectSaturnOn22", (int)CommonData.ZODIAC_SATURN);
            aspectCommonDataNo.Add("aspectUranusOn22", (int)CommonData.ZODIAC_URANUS);
            aspectCommonDataNo.Add("aspectNeptuneOn22", (int)CommonData.ZODIAC_NEPTUNE);
            aspectCommonDataNo.Add("aspectPlutoOn22", (int)CommonData.ZODIAC_PLUTO);
            aspectCommonDataNo.Add("aspectDhOn22", (int)CommonData.ZODIAC_DH_TRUENODE);
            aspectCommonDataNo.Add("aspectChironOn22", (int)CommonData.ZODIAC_CHIRON);
            aspectCommonDataNo.Add("aspectAscOn22", (int)CommonData.ZODIAC_ASC);
            aspectCommonDataNo.Add("aspectMcOn22", (int)CommonData.ZODIAC_MC);
            aspectCommonDataNo.Add("aspectSunOn33", (int)CommonData.ZODIAC_SUN);
            aspectCommonDataNo.Add("aspectMoonOn33", (int)CommonData.ZODIAC_MOON);
            aspectCommonDataNo.Add("aspectMercuryOn33", (int)CommonData.ZODIAC_MERCURY);
            aspectCommonDataNo.Add("aspectVenusOn33", (int)CommonData.ZODIAC_VENUS);
            aspectCommonDataNo.Add("aspectMarsOn33", (int)CommonData.ZODIAC_MARS);
            aspectCommonDataNo.Add("aspectJupiterOn33", (int)CommonData.ZODIAC_JUPITER);
            aspectCommonDataNo.Add("aspectSaturnOn33", (int)CommonData.ZODIAC_SATURN);
            aspectCommonDataNo.Add("aspectUranusOn33", (int)CommonData.ZODIAC_URANUS);
            aspectCommonDataNo.Add("aspectNeptuneOn33", (int)CommonData.ZODIAC_NEPTUNE);
            aspectCommonDataNo.Add("aspectPlutoOn33", (int)CommonData.ZODIAC_PLUTO);
            aspectCommonDataNo.Add("aspectDhOn33", (int)CommonData.ZODIAC_DH_TRUENODE);
            aspectCommonDataNo.Add("aspectChironOn33", (int)CommonData.ZODIAC_CHIRON);
            aspectCommonDataNo.Add("aspectAscOn33", (int)CommonData.ZODIAC_ASC);
            aspectCommonDataNo.Add("aspectMcOn33", (int)CommonData.ZODIAC_MC);
            aspectCommonDataNo.Add("aspectSunOn12", (int)CommonData.ZODIAC_SUN);
            aspectCommonDataNo.Add("aspectMoonOn12", (int)CommonData.ZODIAC_MOON);
            aspectCommonDataNo.Add("aspectMercuryOn12", (int)CommonData.ZODIAC_MERCURY);
            aspectCommonDataNo.Add("aspectVenusOn12", (int)CommonData.ZODIAC_VENUS);
            aspectCommonDataNo.Add("aspectMarsOn12", (int)CommonData.ZODIAC_MARS);
            aspectCommonDataNo.Add("aspectJupiterOn12", (int)CommonData.ZODIAC_JUPITER);
            aspectCommonDataNo.Add("aspectSaturnOn12", (int)CommonData.ZODIAC_SATURN);
            aspectCommonDataNo.Add("aspectUranusOn12", (int)CommonData.ZODIAC_URANUS);
            aspectCommonDataNo.Add("aspectNeptuneOn12", (int)CommonData.ZODIAC_NEPTUNE);
            aspectCommonDataNo.Add("aspectPlutoOn12", (int)CommonData.ZODIAC_PLUTO);
            aspectCommonDataNo.Add("aspectDhOn12", (int)CommonData.ZODIAC_DH_TRUENODE);
            aspectCommonDataNo.Add("aspectChironOn12", (int)CommonData.ZODIAC_CHIRON);
            aspectCommonDataNo.Add("aspectAscOn12", (int)CommonData.ZODIAC_ASC);
            aspectCommonDataNo.Add("aspectMcOn12", (int)CommonData.ZODIAC_MC);
            aspectCommonDataNo.Add("aspectSunOn13", (int)CommonData.ZODIAC_SUN);
            aspectCommonDataNo.Add("aspectMoonOn13", (int)CommonData.ZODIAC_MOON);
            aspectCommonDataNo.Add("aspectMercuryOn13", (int)CommonData.ZODIAC_MERCURY);
            aspectCommonDataNo.Add("aspectVenusOn13", (int)CommonData.ZODIAC_VENUS);
            aspectCommonDataNo.Add("aspectMarsOn13", (int)CommonData.ZODIAC_MARS);
            aspectCommonDataNo.Add("aspectJupiterOn13", (int)CommonData.ZODIAC_JUPITER);
            aspectCommonDataNo.Add("aspectSaturnOn13", (int)CommonData.ZODIAC_SATURN);
            aspectCommonDataNo.Add("aspectUranusOn13", (int)CommonData.ZODIAC_URANUS);
            aspectCommonDataNo.Add("aspectNeptuneOn13", (int)CommonData.ZODIAC_NEPTUNE);
            aspectCommonDataNo.Add("aspectPlutoOn13", (int)CommonData.ZODIAC_PLUTO);
            aspectCommonDataNo.Add("aspectDhOn13", (int)CommonData.ZODIAC_DH_TRUENODE);
            aspectCommonDataNo.Add("aspectChironOn13", (int)CommonData.ZODIAC_CHIRON);
            aspectCommonDataNo.Add("aspectAscOn13", (int)CommonData.ZODIAC_ASC);
            aspectCommonDataNo.Add("aspectMcOn13", (int)CommonData.ZODIAC_MC);
            aspectCommonDataNo.Add("aspectSunOn23", (int)CommonData.ZODIAC_SUN);
            aspectCommonDataNo.Add("aspectMoonOn23", (int)CommonData.ZODIAC_MOON);
            aspectCommonDataNo.Add("aspectMercuryOn23", (int)CommonData.ZODIAC_MERCURY);
            aspectCommonDataNo.Add("aspectVenusOn23", (int)CommonData.ZODIAC_VENUS);
            aspectCommonDataNo.Add("aspectMarsOn23", (int)CommonData.ZODIAC_MARS);
            aspectCommonDataNo.Add("aspectJupiterOn23", (int)CommonData.ZODIAC_JUPITER);
            aspectCommonDataNo.Add("aspectSaturnOn23", (int)CommonData.ZODIAC_SATURN);
            aspectCommonDataNo.Add("aspectUranusOn23", (int)CommonData.ZODIAC_URANUS);
            aspectCommonDataNo.Add("aspectNeptuneOn23", (int)CommonData.ZODIAC_NEPTUNE);
            aspectCommonDataNo.Add("aspectPlutoOn23", (int)CommonData.ZODIAC_PLUTO);
            aspectCommonDataNo.Add("aspectDhOn23", (int)CommonData.ZODIAC_DH_TRUENODE);
            aspectCommonDataNo.Add("aspectChironOn23", (int)CommonData.ZODIAC_CHIRON);
            aspectCommonDataNo.Add("aspectAscOn23", (int)CommonData.ZODIAC_ASC);
            aspectCommonDataNo.Add("aspectMcOn23", (int)CommonData.ZODIAC_MC);

        }

        private void createAspectControlList()
        {
            aspectControlList.Add("aspectSunOn11");
            aspectControlList.Add("aspectMoonOn11");
            aspectControlList.Add("aspectMercuryOn11");
            aspectControlList.Add("aspectVenusOn11");
            aspectControlList.Add("aspectMarsOn11");
            aspectControlList.Add("aspectJupiterOn11");
            aspectControlList.Add("aspectSaturnOn11");
            aspectControlList.Add("aspectUranusOn11");
            aspectControlList.Add("aspectNeptuneOn11");
            aspectControlList.Add("aspectPlutoOn11");
            aspectControlList.Add("aspectDhOn11");
            aspectControlList.Add("aspectChironOn11");
            aspectControlList.Add("aspectAscOn11");
            aspectControlList.Add("aspectMcOn11");
            aspectControlList.Add("aspectSunOff11");
            aspectControlList.Add("aspectMoonOff11");
            aspectControlList.Add("aspectMercuryOff11");
            aspectControlList.Add("aspectVenusOff11");
            aspectControlList.Add("aspectMarsOff11");
            aspectControlList.Add("aspectJupiterOff11");
            aspectControlList.Add("aspectSaturnOff11");
            aspectControlList.Add("aspectUranusOff11");
            aspectControlList.Add("aspectNeptuneOff11");
            aspectControlList.Add("aspectPlutoOff11");
            aspectControlList.Add("aspectDhOff11");
            aspectControlList.Add("aspectChironOff11");
            aspectControlList.Add("aspectAscOff11");
            aspectControlList.Add("aspectMcOff11");
            aspectControlList.Add("aspectSunOn22");
            aspectControlList.Add("aspectMoonOn22");
            aspectControlList.Add("aspectMercuryOn22");
            aspectControlList.Add("aspectVenusOn22");
            aspectControlList.Add("aspectMarsOn22");
            aspectControlList.Add("aspectJupiterOn22");
            aspectControlList.Add("aspectSaturnOn22");
            aspectControlList.Add("aspectUranusOn22");
            aspectControlList.Add("aspectNeptuneOn22");
            aspectControlList.Add("aspectPlutoOn22");
            aspectControlList.Add("aspectDhOn22");
            aspectControlList.Add("aspectChironOn22");
            aspectControlList.Add("aspectAscOn22");
            aspectControlList.Add("aspectMcOn22");
            aspectControlList.Add("aspectSunOff22");
            aspectControlList.Add("aspectMoonOff22");
            aspectControlList.Add("aspectMercuryOff22");
            aspectControlList.Add("aspectVenusOff22");
            aspectControlList.Add("aspectMarsOff22");
            aspectControlList.Add("aspectJupiterOff22");
            aspectControlList.Add("aspectSaturnOff22");
            aspectControlList.Add("aspectUranusOff22");
            aspectControlList.Add("aspectNeptuneOff22");
            aspectControlList.Add("aspectPlutoOff22");
            aspectControlList.Add("aspectDhOff22");
            aspectControlList.Add("aspectChironOff22");
            aspectControlList.Add("aspectAscOff22");
            aspectControlList.Add("aspectMcOff22");
            aspectControlList.Add("aspectSunOn33");
            aspectControlList.Add("aspectMoonOn33");
            aspectControlList.Add("aspectMercuryOn33");
            aspectControlList.Add("aspectVenusOn33");
            aspectControlList.Add("aspectMarsOn33");
            aspectControlList.Add("aspectJupiterOn33");
            aspectControlList.Add("aspectSaturnOn33");
            aspectControlList.Add("aspectUranusOn33");
            aspectControlList.Add("aspectNeptuneOn33");
            aspectControlList.Add("aspectPlutoOn33");
            aspectControlList.Add("aspectDhOn33");
            aspectControlList.Add("aspectChironOn33");
            aspectControlList.Add("aspectAscOn33");
            aspectControlList.Add("aspectMcOn33");
            aspectControlList.Add("aspectSunOff33");
            aspectControlList.Add("aspectMoonOff33");
            aspectControlList.Add("aspectMercuryOff33");
            aspectControlList.Add("aspectVenusOff33");
            aspectControlList.Add("aspectMarsOff33");
            aspectControlList.Add("aspectJupiterOff33");
            aspectControlList.Add("aspectSaturnOff33");
            aspectControlList.Add("aspectUranusOff33");
            aspectControlList.Add("aspectNeptuneOff33");
            aspectControlList.Add("aspectPlutoOff33");
            aspectControlList.Add("aspectDhOff33");
            aspectControlList.Add("aspectChironOff33");
            aspectControlList.Add("aspectAscOff33");
            aspectControlList.Add("aspectMcOff33");
            aspectControlList.Add("aspectSunOn12");
            aspectControlList.Add("aspectMoonOn12");
            aspectControlList.Add("aspectMercuryOn12");
            aspectControlList.Add("aspectVenusOn12");
            aspectControlList.Add("aspectMarsOn12");
            aspectControlList.Add("aspectJupiterOn12");
            aspectControlList.Add("aspectSaturnOn12");
            aspectControlList.Add("aspectUranusOn12");
            aspectControlList.Add("aspectNeptuneOn12");
            aspectControlList.Add("aspectPlutoOn12");
            aspectControlList.Add("aspectDhOn12");
            aspectControlList.Add("aspectChironOn12");
            aspectControlList.Add("aspectAscOn12");
            aspectControlList.Add("aspectMcOn12");
            aspectControlList.Add("aspectSunOff12");
            aspectControlList.Add("aspectMoonOff12");
            aspectControlList.Add("aspectMercuryOff12");
            aspectControlList.Add("aspectVenusOff12");
            aspectControlList.Add("aspectMarsOff12");
            aspectControlList.Add("aspectJupiterOff12");
            aspectControlList.Add("aspectSaturnOff12");
            aspectControlList.Add("aspectUranusOff12");
            aspectControlList.Add("aspectNeptuneOff12");
            aspectControlList.Add("aspectPlutoOff12");
            aspectControlList.Add("aspectDhOff12");
            aspectControlList.Add("aspectChironOff12");
            aspectControlList.Add("aspectAscOff12");
            aspectControlList.Add("aspectMcOff12");
            aspectControlList.Add("aspectSunOn13");
            aspectControlList.Add("aspectMoonOn13");
            aspectControlList.Add("aspectMercuryOn13");
            aspectControlList.Add("aspectVenusOn13");
            aspectControlList.Add("aspectMarsOn13");
            aspectControlList.Add("aspectJupiterOn13");
            aspectControlList.Add("aspectSaturnOn13");
            aspectControlList.Add("aspectUranusOn13");
            aspectControlList.Add("aspectNeptuneOn13");
            aspectControlList.Add("aspectPlutoOn13");
            aspectControlList.Add("aspectDhOn13");
            aspectControlList.Add("aspectChironOn13");
            aspectControlList.Add("aspectAscOn13");
            aspectControlList.Add("aspectMcOn13");
            aspectControlList.Add("aspectSunOff13");
            aspectControlList.Add("aspectMoonOff13");
            aspectControlList.Add("aspectMercuryOff13");
            aspectControlList.Add("aspectVenusOff13");
            aspectControlList.Add("aspectMarsOff13");
            aspectControlList.Add("aspectJupiterOff13");
            aspectControlList.Add("aspectSaturnOff13");
            aspectControlList.Add("aspectUranusOff13");
            aspectControlList.Add("aspectNeptuneOff13");
            aspectControlList.Add("aspectPlutoOff13");
            aspectControlList.Add("aspectDhOff13");
            aspectControlList.Add("aspectChironOff13");
            aspectControlList.Add("aspectAscOff13");
            aspectControlList.Add("aspectMcOff13");
            aspectControlList.Add("aspectSunOn23");
            aspectControlList.Add("aspectMoonOn23");
            aspectControlList.Add("aspectMercuryOn23");
            aspectControlList.Add("aspectVenusOn23");
            aspectControlList.Add("aspectMarsOn23");
            aspectControlList.Add("aspectJupiterOn23");
            aspectControlList.Add("aspectSaturnOn23");
            aspectControlList.Add("aspectUranusOn23");
            aspectControlList.Add("aspectNeptuneOn23");
            aspectControlList.Add("aspectPlutoOn23");
            aspectControlList.Add("aspectDhOn23");
            aspectControlList.Add("aspectChironOn23");
            aspectControlList.Add("aspectAscOn23");
            aspectControlList.Add("aspectMcOn23");
            aspectControlList.Add("aspectSunOff23");
            aspectControlList.Add("aspectMoonOff23");
            aspectControlList.Add("aspectMercuryOff23");
            aspectControlList.Add("aspectVenusOff23");
            aspectControlList.Add("aspectMarsOff23");
            aspectControlList.Add("aspectJupiterOff23");
            aspectControlList.Add("aspectSaturnOff23");
            aspectControlList.Add("aspectUranusOff23");
            aspectControlList.Add("aspectNeptuneOff23");
            aspectControlList.Add("aspectPlutoOff23");
            aspectControlList.Add("aspectDhOff23");
            aspectControlList.Add("aspectChironOff23");
            aspectControlList.Add("aspectAscOff23");
            aspectControlList.Add("aspectMcOff23");
            aspectControlNameList.Add(aspectSunOn11);
            aspectControlNameList.Add(aspectMoonOn11);
            aspectControlNameList.Add(aspectMercuryOn11);
            aspectControlNameList.Add(aspectVenusOn11);
            aspectControlNameList.Add(aspectMarsOn11);
            aspectControlNameList.Add(aspectJupiterOn11);
            aspectControlNameList.Add(aspectSaturnOn11);
            aspectControlNameList.Add(aspectUranusOn11);
            aspectControlNameList.Add(aspectNeptuneOn11);
            aspectControlNameList.Add(aspectPlutoOn11);
            aspectControlNameList.Add(aspectDhOn11);
            aspectControlNameList.Add(aspectChironOn11);
            aspectControlNameList.Add(aspectAscOn11);
            aspectControlNameList.Add(aspectMcOn11);
            aspectControlNameList.Add(aspectSunOff11);
            aspectControlNameList.Add(aspectMoonOff11);
            aspectControlNameList.Add(aspectMercuryOff11);
            aspectControlNameList.Add(aspectVenusOff11);
            aspectControlNameList.Add(aspectMarsOff11);
            aspectControlNameList.Add(aspectJupiterOff11);
            aspectControlNameList.Add(aspectSaturnOff11);
            aspectControlNameList.Add(aspectUranusOff11);
            aspectControlNameList.Add(aspectNeptuneOff11);
            aspectControlNameList.Add(aspectPlutoOff11);
            aspectControlNameList.Add(aspectDhOff11);
            aspectControlNameList.Add(aspectChironOff11);
            aspectControlNameList.Add(aspectAscOff11);
            aspectControlNameList.Add(aspectMcOff11);
            aspectControlNameList.Add(aspectSunOn22);
            aspectControlNameList.Add(aspectMoonOn22);
            aspectControlNameList.Add(aspectMercuryOn22);
            aspectControlNameList.Add(aspectVenusOn22);
            aspectControlNameList.Add(aspectMarsOn22);
            aspectControlNameList.Add(aspectJupiterOn22);
            aspectControlNameList.Add(aspectSaturnOn22);
            aspectControlNameList.Add(aspectUranusOn22);
            aspectControlNameList.Add(aspectNeptuneOn22);
            aspectControlNameList.Add(aspectPlutoOn22);
            aspectControlNameList.Add(aspectDhOn22);
            aspectControlNameList.Add(aspectChironOn22);
            aspectControlNameList.Add(aspectAscOn22);
            aspectControlNameList.Add(aspectMcOn22);
            aspectControlNameList.Add(aspectSunOff22);
            aspectControlNameList.Add(aspectMoonOff22);
            aspectControlNameList.Add(aspectMercuryOff22);
            aspectControlNameList.Add(aspectVenusOff22);
            aspectControlNameList.Add(aspectMarsOff22);
            aspectControlNameList.Add(aspectJupiterOff22);
            aspectControlNameList.Add(aspectSaturnOff22);
            aspectControlNameList.Add(aspectUranusOff22);
            aspectControlNameList.Add(aspectNeptuneOff22);
            aspectControlNameList.Add(aspectPlutoOff22);
            aspectControlNameList.Add(aspectDhOff22);
            aspectControlNameList.Add(aspectChironOff22);
            aspectControlNameList.Add(aspectAscOff22);
            aspectControlNameList.Add(aspectMcOff22);
            aspectControlNameList.Add(aspectSunOn33);
            aspectControlNameList.Add(aspectMoonOn33);
            aspectControlNameList.Add(aspectMercuryOn33);
            aspectControlNameList.Add(aspectVenusOn33);
            aspectControlNameList.Add(aspectMarsOn33);
            aspectControlNameList.Add(aspectJupiterOn33);
            aspectControlNameList.Add(aspectSaturnOn33);
            aspectControlNameList.Add(aspectUranusOn33);
            aspectControlNameList.Add(aspectNeptuneOn33);
            aspectControlNameList.Add(aspectPlutoOn33);
            aspectControlNameList.Add(aspectDhOn33);
            aspectControlNameList.Add(aspectChironOn33);
            aspectControlNameList.Add(aspectAscOn33);
            aspectControlNameList.Add(aspectMcOn33);
            aspectControlNameList.Add(aspectSunOff33);
            aspectControlNameList.Add(aspectMoonOff33);
            aspectControlNameList.Add(aspectMercuryOff33);
            aspectControlNameList.Add(aspectVenusOff33);
            aspectControlNameList.Add(aspectMarsOff33);
            aspectControlNameList.Add(aspectJupiterOff33);
            aspectControlNameList.Add(aspectSaturnOff33);
            aspectControlNameList.Add(aspectUranusOff33);
            aspectControlNameList.Add(aspectNeptuneOff33);
            aspectControlNameList.Add(aspectPlutoOff33);
            aspectControlNameList.Add(aspectDhOff33);
            aspectControlNameList.Add(aspectChironOff33);
            aspectControlNameList.Add(aspectAscOff33);
            aspectControlNameList.Add(aspectMcOff33);
            aspectControlNameList.Add(aspectSunOn12);
            aspectControlNameList.Add(aspectMoonOn12);
            aspectControlNameList.Add(aspectMercuryOn12);
            aspectControlNameList.Add(aspectVenusOn12);
            aspectControlNameList.Add(aspectMarsOn12);
            aspectControlNameList.Add(aspectJupiterOn12);
            aspectControlNameList.Add(aspectSaturnOn12);
            aspectControlNameList.Add(aspectUranusOn12);
            aspectControlNameList.Add(aspectNeptuneOn12);
            aspectControlNameList.Add(aspectPlutoOn12);
            aspectControlNameList.Add(aspectDhOn12);
            aspectControlNameList.Add(aspectChironOn12);
            aspectControlNameList.Add(aspectAscOn12);
            aspectControlNameList.Add(aspectMcOn12);
            aspectControlNameList.Add(aspectSunOff12);
            aspectControlNameList.Add(aspectMoonOff12);
            aspectControlNameList.Add(aspectMercuryOff12);
            aspectControlNameList.Add(aspectVenusOff12);
            aspectControlNameList.Add(aspectMarsOff12);
            aspectControlNameList.Add(aspectJupiterOff12);
            aspectControlNameList.Add(aspectSaturnOff12);
            aspectControlNameList.Add(aspectUranusOff12);
            aspectControlNameList.Add(aspectNeptuneOff12);
            aspectControlNameList.Add(aspectPlutoOff12);
            aspectControlNameList.Add(aspectDhOff12);
            aspectControlNameList.Add(aspectChironOff12);
            aspectControlNameList.Add(aspectAscOff12);
            aspectControlNameList.Add(aspectMcOff12);
            aspectControlNameList.Add(aspectSunOn13);
            aspectControlNameList.Add(aspectMoonOn13);
            aspectControlNameList.Add(aspectMercuryOn13);
            aspectControlNameList.Add(aspectVenusOn13);
            aspectControlNameList.Add(aspectMarsOn13);
            aspectControlNameList.Add(aspectJupiterOn13);
            aspectControlNameList.Add(aspectSaturnOn13);
            aspectControlNameList.Add(aspectUranusOn13);
            aspectControlNameList.Add(aspectNeptuneOn13);
            aspectControlNameList.Add(aspectPlutoOn13);
            aspectControlNameList.Add(aspectDhOn13);
            aspectControlNameList.Add(aspectChironOn13);
            aspectControlNameList.Add(aspectAscOn13);
            aspectControlNameList.Add(aspectMcOn13);
            aspectControlNameList.Add(aspectSunOff13);
            aspectControlNameList.Add(aspectMoonOff13);
            aspectControlNameList.Add(aspectMercuryOff13);
            aspectControlNameList.Add(aspectVenusOff13);
            aspectControlNameList.Add(aspectMarsOff13);
            aspectControlNameList.Add(aspectJupiterOff13);
            aspectControlNameList.Add(aspectSaturnOff13);
            aspectControlNameList.Add(aspectUranusOff13);
            aspectControlNameList.Add(aspectNeptuneOff13);
            aspectControlNameList.Add(aspectPlutoOff13);
            aspectControlNameList.Add(aspectDhOff13);
            aspectControlNameList.Add(aspectChironOff13);
            aspectControlNameList.Add(aspectAscOff13);
            aspectControlNameList.Add(aspectMcOff13);
            aspectControlNameList.Add(aspectSunOn23);
            aspectControlNameList.Add(aspectMoonOn23);
            aspectControlNameList.Add(aspectMercuryOn23);
            aspectControlNameList.Add(aspectVenusOn23);
            aspectControlNameList.Add(aspectMarsOn23);
            aspectControlNameList.Add(aspectJupiterOn23);
            aspectControlNameList.Add(aspectSaturnOn23);
            aspectControlNameList.Add(aspectUranusOn23);
            aspectControlNameList.Add(aspectNeptuneOn23);
            aspectControlNameList.Add(aspectPlutoOn23);
            aspectControlNameList.Add(aspectDhOn23);
            aspectControlNameList.Add(aspectChironOn23);
            aspectControlNameList.Add(aspectAscOn23);
            aspectControlNameList.Add(aspectMcOn23);
            aspectControlNameList.Add(aspectSunOff23);
            aspectControlNameList.Add(aspectMoonOff23);
            aspectControlNameList.Add(aspectMercuryOff23);
            aspectControlNameList.Add(aspectVenusOff23);
            aspectControlNameList.Add(aspectMarsOff23);
            aspectControlNameList.Add(aspectJupiterOff23);
            aspectControlNameList.Add(aspectSaturnOff23);
            aspectControlNameList.Add(aspectUranusOff23);
            aspectControlNameList.Add(aspectNeptuneOff23);
            aspectControlNameList.Add(aspectPlutoOff23);
            aspectControlNameList.Add(aspectDhOff23);
            aspectControlNameList.Add(aspectChironOff23);
            aspectControlNameList.Add(aspectAscOff23);
            aspectControlNameList.Add(aspectMcOff23);

        }

    }
}
