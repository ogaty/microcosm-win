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
        public SettingWIndow(MainWindow main)
        {
            this.main = main;
            InitializeComponent();

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

        private void aspectSunOn11_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectSunOff11.Visibility = Visibility.Visible;
            aspectSunOff11.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_SUN)
                {
                    // ちょっと違う？？
                    data.isAspectDisp = false;
                    break;
                }
            }
            main.ReRender();
        }

        private void aspectSunOff11_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectSunOn11.Visibility = Visibility.Visible;
            aspectSunOn11.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_SUN)
                {
                    // ちょっと違う？？
                    data.isAspectDisp = true;
                    break;
                }
            }
            main.ReRender();
        }

        private void aspectMoonOn11_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectMoonOff11.Visibility = Visibility.Visible;
            aspectMoonOff11.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_MOON)
                {
                    data.isAspectDisp = false;
                    break;
                }
            }
            main.ReRender();
        }

        private void aspectMoonOff11_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectMoonOn11.Visibility = Visibility.Visible;
            aspectMoonOn11.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_MOON)
                {
                    data.isAspectDisp = true;
                    break;
                }
            }
            main.ReRender();
        }
    }
}
