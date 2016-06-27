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

        private void aspectMercuryOn11_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectMoonOff11.Visibility = Visibility.Visible;
            aspectMoonOff11.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_MERCURY)
                {
                    data.isAspectDisp = false;
                    break;
                }
            }
            main.ReRender();

        }

        private void aspectMercuryOff11_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectMoonOn11.Visibility = Visibility.Visible;
            aspectMoonOn11.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_MERCURY)
                {
                    data.isAspectDisp = true;
                    break;
                }
            }
            main.ReRender();

        }

        private void aspectVenusOn11_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectMoonOff11.Visibility = Visibility.Visible;
            aspectMoonOff11.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_VENUS)
                {
                    data.isAspectDisp = false;
                    break;
                }
            }
            main.ReRender();

        }

        private void aspectVenusOff11_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectMoonOn11.Visibility = Visibility.Visible;
            aspectMoonOn11.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_VENUS)
                {
                    data.isAspectDisp = true;
                    break;
                }
            }
            main.ReRender();

        }

        private void aspectMarsOn11_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectMoonOff11.Visibility = Visibility.Visible;
            aspectMoonOff11.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_MARS)
                {
                    data.isAspectDisp = false;
                    break;
                }
            }
            main.ReRender();

        }

        private void aspectMarsOff11_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectMoonOn11.Visibility = Visibility.Visible;
            aspectMoonOn11.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_MARS)
                {
                    data.isAspectDisp = true;
                    break;
                }
            }
            main.ReRender();

        }

        private void aspectJupiterOn11_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectMoonOff11.Visibility = Visibility.Visible;
            aspectMoonOff11.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_JUPITER)
                {
                    data.isAspectDisp = false;
                    break;
                }
            }
            main.ReRender();

        }

        private void aspectJupiterOff11_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectMoonOn11.Visibility = Visibility.Visible;
            aspectMoonOn11.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_JUPITER)
                {
                    data.isAspectDisp = true;
                    break;
                }
            }
            main.ReRender();

        }

        private void aspectSaturnOn11_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectMoonOff11.Visibility = Visibility.Visible;
            aspectMoonOff11.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_SATURN)
                {
                    data.isAspectDisp = false;
                    break;
                }
            }
            main.ReRender();

        }

        private void aspectSaturnOff11_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectMoonOn11.Visibility = Visibility.Visible;
            aspectMoonOn11.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_SATURN)
                {
                    data.isAspectDisp = true;
                    break;
                }
            }
            main.ReRender();

        }

        private void aspectUranusOn11_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectMoonOff11.Visibility = Visibility.Visible;
            aspectMoonOff11.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_URANUS)
                {
                    data.isAspectDisp = false;
                    break;
                }
            }
            main.ReRender();

        }

        private void aspectUranusOff11_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectMoonOn11.Visibility = Visibility.Visible;
            aspectMoonOn11.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_URANUS)
                {
                    data.isAspectDisp = true;
                    break;
                }
            }
            main.ReRender();

        }

        private void aspectNeptuneOn11_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectMoonOff11.Visibility = Visibility.Visible;
            aspectMoonOff11.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_NEPTUNE)
                {
                    data.isAspectDisp = false;
                    break;
                }
            }
            main.ReRender();

        }

        private void aspectNeptuneOff11_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectMoonOn11.Visibility = Visibility.Visible;
            aspectMoonOn11.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_NEPTUNE)
                {
                    data.isAspectDisp = true;
                    break;
                }
            }
            main.ReRender();

        }

        private void aspectPlutoOn11_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectPlutoOff11.Visibility = Visibility.Visible;
            aspectPlutoOff11.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_PLUTO)
                {
                    data.isAspectDisp = false;
                    break;
                }
            }
            main.ReRender();

        }

        private void aspectPlutoOff11_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectPlutoOn11.Visibility = Visibility.Visible;
            aspectPlutoOn11.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_PLUTO)
                {
                    data.isAspectDisp = true;
                    break;
                }
            }
            main.ReRender();

        }

        private void aspectDhOn11_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectDhOff11.Visibility = Visibility.Visible;
            aspectDhOff11.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_DH_TRUENODE)
                {
                    data.isAspectDisp = false;
                    break;
                }
            }
            main.ReRender();

        }

        private void aspectDhOff11_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectDhOn11.Visibility = Visibility.Visible;
            aspectDhOn11.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_DH_TRUENODE)
                {
                    data.isAspectDisp = true;
                    break;
                }
            }
            main.ReRender();

        }

        private void aspectChironOn11_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectChironOff11.Visibility = Visibility.Visible;
            aspectChironOff11.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_CHIRON)
                {
                    data.isAspectDisp = false;
                    break;
                }
            }
            main.ReRender();

        }

        private void aspectChironOff11_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectChironOn11.Visibility = Visibility.Visible;
            aspectChironOn11.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_CHIRON)
                {
                    data.isAspectDisp = true;
                    break;
                }
            }
            main.ReRender();

        }




        private void aspectSunOn23_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectSunOff23.Visibility = Visibility.Visible;
            aspectSunOff23.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_SUN)
                {
                    data.isAspectDisp = true;
                    break;
                }
            }
            main.ReRender();
        }

        private void aspectSunOff23_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectSunOn23.Visibility = Visibility.Visible;
            aspectSunOn23.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_SUN)
                {
                    data.isAspectDisp = true;
                    break;
                }
            }
            main.ReRender();

        }

        private void aspectMoonOn23_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectMoonOff23.Visibility = Visibility.Visible;
            aspectMoonOff23.Height = 24;

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

        private void aspectMoonOff23_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectMoonOn23.Visibility = Visibility.Visible;
            aspectMoonOn23.Height = 24;

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

        private void aspectMercuryOn23_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectMercuryOff23.Visibility = Visibility.Visible;
            aspectMercuryOff23.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_MERCURY)
                {
                    data.isAspectDisp = true;
                    break;
                }
            }
            main.ReRender();

        }

        private void aspectMercuryOff23_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectMercuryOn23.Visibility = Visibility.Visible;
            aspectMercuryOn23.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_MERCURY)
                {
                    data.isAspectDisp = true;
                    break;
                }
            }
            main.ReRender();

        }

        private void aspectVenusOn23_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectVenusOff23.Visibility = Visibility.Visible;
            aspectVenusOff23.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_VENUS)
                {
                    data.isAspectDisp = true;
                    break;
                }
            }
            main.ReRender();
        }

        private void aspectVenusOff23_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectVenusOn23.Visibility = Visibility.Visible;
            aspectVenusOn23.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_VENUS)
                {
                    data.isAspectDisp = true;
                    break;
                }
            }
            main.ReRender();
        }

        private void aspectMarsOn23_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectMarsOff23.Visibility = Visibility.Visible;
            aspectMarsOff23.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_MARS)
                {
                    data.isAspectDisp = true;
                    break;
                }
            }
            main.ReRender();
        }

        private void aspectMarsOff23_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectMarsOn23.Visibility = Visibility.Visible;
            aspectMarsOn23.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_MARS)
                {
                    data.isAspectDisp = true;
                    break;
                }
            }
            main.ReRender();
        }

        private void aspectJupiterOn23_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectJupiterOff23.Visibility = Visibility.Visible;
            aspectJupiterOff23.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_JUPITER)
                {
                    data.isAspectDisp = true;
                    break;
                }
            }
            main.ReRender();
        }

        private void aspectJupiterOff23_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectJupiterOn23.Visibility = Visibility.Visible;
            aspectJupiterOn23.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_JUPITER)
                {
                    data.isAspectDisp = true;
                    break;
                }
            }
            main.ReRender();
        }

        private void aspectSaturnOn23_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectSaturnOff23.Visibility = Visibility.Visible;
            aspectSaturnOff23.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_SATURN)
                {
                    data.isAspectDisp = true;
                    break;
                }
            }
            main.ReRender();
        }

        private void aspectSaturnOff23_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectSaturnOn23.Visibility = Visibility.Visible;
            aspectSaturnOn23.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_SATURN)
                {
                    data.isAspectDisp = true;
                    break;
                }
            }
            main.ReRender();
        }

        private void aspectUranusOn23_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectUranusOff23.Visibility = Visibility.Visible;
            aspectUranusOff23.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_URANUS)
                {
                    data.isAspectDisp = true;
                    break;
                }
            }
            main.ReRender();
        }

        private void aspectUranusOff23_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectUranusOn23.Visibility = Visibility.Visible;
            aspectUranusOn23.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_URANUS)
                {
                    data.isAspectDisp = true;
                    break;
                }
            }
            main.ReRender();
        }

        private void aspectNeptuneOn23_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectNeptuneOff23.Visibility = Visibility.Visible;
            aspectNeptuneOff23.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_NEPTUNE)
                {
                    data.isAspectDisp = true;
                    break;
                }
            }
            main.ReRender();
        }

        private void aspectNeptuneOff23_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectNeptuneOn23.Visibility = Visibility.Visible;
            aspectNeptuneOn23.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_NEPTUNE)
                {
                    data.isAspectDisp = true;
                    break;
                }
            }
            main.ReRender();
        }

        private void aspectPlutoOn23_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectPlutoOff23.Visibility = Visibility.Visible;
            aspectPlutoOff23.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_PLUTO)
                {
                    data.isAspectDisp = true;
                    break;
                }
            }
            main.ReRender();
        }

        private void aspectPlutoOff23_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectPlutoOn23.Visibility = Visibility.Visible;
            aspectPlutoOn23.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_PLUTO)
                {
                    data.isAspectDisp = true;
                    break;
                }
            }
            main.ReRender();
        }

        private void aspectDhOn23_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectDhOff23.Visibility = Visibility.Visible;
            aspectDhOff23.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_DH_TRUENODE)
                {
                    data.isAspectDisp = true;
                    break;
                }
            }
            main.ReRender();
        }

        private void aspectDhOff23_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectDhOn23.Visibility = Visibility.Visible;
            aspectDhOn23.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_DH_TRUENODE)
                {
                    data.isAspectDisp = true;
                    break;
                }
            }
            main.ReRender();
        }

        private void aspectChironOn23_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectChironOff23.Visibility = Visibility.Visible;
            aspectChironOff23.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_CHIRON)
                {
                    data.isAspectDisp = true;
                    break;
                }
            }
            main.ReRender();
        }

        private void aspectChironOff23_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectChironOn23.Visibility = Visibility.Visible;
            aspectChironOn23.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_CHIRON)
                {
                    data.isAspectDisp = true;
                    break;
                }
            }
            main.ReRender();
        }

        private void aspectSunOn13_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectSunOff23.Visibility = Visibility.Visible;
            aspectSunOff23.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_SUN)
                {
                    data.isAspectDisp = true;
                    break;
                }
            }
            main.ReRender();
        }

        private void aspectSunOff13_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectSunOn23.Visibility = Visibility.Visible;
            aspectSunOn23.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_SUN)
                {
                    data.isAspectDisp = true;
                    break;
                }
            }
            main.ReRender();
        }

        private void aspectMoonOn13_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectMoonOff23.Visibility = Visibility.Visible;
            aspectMoonOff23.Height = 24;

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

        private void aspectMoonOff13_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectMoonOn23.Visibility = Visibility.Visible;
            aspectMoonOn23.Height = 24;

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

        private void aspectMercuryOn13_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void aspectMercuryOff13_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void aspectVenusOn13_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void aspectVenusOff13_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void aspectMarsOn13_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void aspectMarsOff13_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void aspectJupiterOn13_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void aspectJupiterOff13_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void aspectSaturnOn13_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void aspectSaturnOff13_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void aspectUranusOn13_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void aspectUranusOff13_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void aspectNeptuneOn13_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void aspectNeptuneOff13_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void aspectPlutoOn13_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void aspectPlutoOff13_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void aspectDhOn13_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void aspectDhOff13_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void aspectChironOn13_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void aspectChironOff13_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void aspectSunOn12_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectSunOff12.Visibility = Visibility.Visible;
            aspectSunOff12.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_SUN)
                {
                    data.isAspectDisp = true;
                    break;
                }
            }
            main.ReRender();
        }

        private void aspectSunOff12_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectChironOn12.Visibility = Visibility.Visible;
            aspectChironOn12.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_SUN)
                {
                    data.isAspectDisp = true;
                    break;
                }
            }
            main.ReRender();
        }

        private void aspectMoonOn12_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectMoonOff12.Visibility = Visibility.Visible;
            aspectMoonOff12.Height = 24;

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

        private void aspectMoonOff12_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectMoonOn12.Visibility = Visibility.Visible;
            aspectMoonOn12.Height = 24;

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

        private void aspectMercuryOn12_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectMercuryOff12.Visibility = Visibility.Visible;
            aspectMercuryOff12.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_MERCURY)
                {
                    data.isAspectDisp = true;
                    break;
                }
            }
            main.ReRender();
        }

        private void aspectMercuryOff12_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectMercuryOn12.Visibility = Visibility.Visible;
            aspectMercuryOn12.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_MERCURY)
                {
                    data.isAspectDisp = true;
                    break;
                }
            }
            main.ReRender();
        }

        private void aspectVenusOn12_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectVenusOff12.Visibility = Visibility.Visible;
            aspectVenusOff12.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_VENUS)
                {
                    data.isAspectDisp = true;
                    break;
                }
            }
            main.ReRender();
        }

        private void aspectVenusOff12_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectVenusOn12.Visibility = Visibility.Visible;
            aspectVenusOn12.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_VENUS)
                {
                    data.isAspectDisp = true;
                    break;
                }
            }
            main.ReRender();
        }

        private void aspectMarsOn12_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectMarsOff12.Visibility = Visibility.Visible;
            aspectMarsOff12.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_MARS)
                {
                    data.isAspectDisp = true;
                    break;
                }
            }
            main.ReRender();
        }

        private void aspectMarsOff12_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectMarsOn12.Visibility = Visibility.Visible;
            aspectMarsOn12.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_MARS)
                {
                    data.isAspectDisp = true;
                    break;
                }
            }
            main.ReRender();
        }

        private void aspectJupiterOn12_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectJupiterOff12.Visibility = Visibility.Visible;
            aspectJupiterOff12.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_JUPITER)
                {
                    data.isAspectDisp = true;
                    break;
                }
            }
            main.ReRender();
        }

        private void aspectJupiterOff12_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectJupiterOn12.Visibility = Visibility.Visible;
            aspectJupiterOn12.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_JUPITER)
                {
                    data.isAspectDisp = true;
                    break;
                }
            }
            main.ReRender();
        }

        private void aspectSaturnOn12_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectSaturnOff12.Visibility = Visibility.Visible;
            aspectSaturnOff12.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_SATURN)
                {
                    data.isAspectDisp = true;
                    break;
                }
            }
            main.ReRender();
        }

        private void aspectSaturnOff12_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectSaturnOn12.Visibility = Visibility.Visible;
            aspectSaturnOn12.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_SATURN)
                {
                    data.isAspectDisp = true;
                    break;
                }
            }
            main.ReRender();
        }

        private void aspectUranusOn12_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectUranusOff12.Visibility = Visibility.Visible;
            aspectUranusOff12.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_URANUS)
                {
                    data.isAspectDisp = true;
                    break;
                }
            }
            main.ReRender();
        }

        private void aspectUranusOff12_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectUranusOn12.Visibility = Visibility.Visible;
            aspectUranusOn12.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_URANUS)
                {
                    data.isAspectDisp = true;
                    break;
                }
            }
            main.ReRender();
        }

        private void aspectNeptuneOn12_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void aspectNeptuneOff12_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void aspectPlutoOn12_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void aspectPlutoOff12_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void aspectDhOn12_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void aspectDhOff12_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void aspectChironOn12_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void aspectChironOff12_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void aspectSunOn33_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectSunOff33.Visibility = Visibility.Visible;
            aspectSunOff33.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_SUN)
                {
                    data.isAspectDisp = false;
                    break;
                }
            }
            main.ReRender();

        }

        private void aspectSunOff33_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectSunOn33.Visibility = Visibility.Visible;
            aspectSunOn33.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_SUN)
                {
                    data.isAspectDisp = true;
                    break;
                }
            }
            main.ReRender();

        }

        private void aspectMoonOn33_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectMoonOff33.Visibility = Visibility.Visible;
            aspectMoonOff33.Height = 24;

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

        private void aspectMoonOff33_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectMoonOn33.Visibility = Visibility.Visible;
            aspectMoonOn33.Height = 24;

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

        private void aspectMercuryOn33_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectMercuryOff33.Visibility = Visibility.Visible;
            aspectMercuryOff33.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_MERCURY)
                {
                    data.isAspectDisp = false;
                    break;
                }
            }
            main.ReRender();

        }

        private void aspectMercuryOff33_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectMercuryOn33.Visibility = Visibility.Visible;
            aspectMercuryOn33.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_MERCURY)
                {
                    data.isAspectDisp = true;
                    break;
                }
            }
            main.ReRender();

        }

        private void aspectVenusOn33_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectVenusOff33.Visibility = Visibility.Visible;
            aspectVenusOff33.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_VENUS)
                {
                    data.isAspectDisp = false;
                    break;
                }
            }
            main.ReRender();

        }

        private void aspectVenusOff33_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectVenusOn33.Visibility = Visibility.Visible;
            aspectVenusOn33.Height = 24;

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

        private void aspectMarsOn33_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectMarsOff33.Visibility = Visibility.Visible;
            aspectMarsOff33.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_MARS)
                {
                    data.isAspectDisp = false;
                    break;
                }
            }
            main.ReRender();

        }

        private void aspectMarsOff33_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectMarsOn33.Visibility = Visibility.Visible;
            aspectMarsOn33.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_MARS)
                {
                    data.isAspectDisp = true;
                    break;
                }
            }
            main.ReRender();

        }

        private void aspectJupiterOn33_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectJupiterOff33.Visibility = Visibility.Visible;
            aspectJupiterOff33.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_JUPITER)
                {
                    data.isAspectDisp = false;
                    break;
                }
            }
            main.ReRender();


        }

        private void aspectJupiterOff33_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectJupiterOn33.Visibility = Visibility.Visible;
            aspectJupiterOn33.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_JUPITER)
                {
                    data.isAspectDisp = true;
                    break;
                }
            }
            main.ReRender();

        }

        private void aspectSaturnOn33_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectSaturnOff33.Visibility = Visibility.Visible;
            aspectSaturnOff33.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_SATURN)
                {
                    data.isAspectDisp = false;
                    break;
                }
            }
            main.ReRender();

        }

        private void aspectSaturnOff33_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectSaturnOn33.Visibility = Visibility.Visible;
            aspectSaturnOn33.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_SATURN)
                {
                    data.isAspectDisp = true;
                    break;
                }
            }
            main.ReRender();

        }

        private void aspectUranusOn33_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectUranusOff33.Visibility = Visibility.Visible;
            aspectUranusOff33.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_URANUS)
                {
                    data.isAspectDisp = false;
                    break;
                }
            }
            main.ReRender();

        }

        private void aspectUranusOff33_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectUranusOn33.Visibility = Visibility.Visible;
            aspectUranusOn33.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_URANUS)
                {
                    data.isAspectDisp = true;
                    break;
                }
            }
            main.ReRender();

        }

        private void aspectNeptuneOn33_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectNeptuneOff33.Visibility = Visibility.Visible;
            aspectNeptuneOff33.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_NEPTUNE)
                {
                    data.isAspectDisp = false;
                    break;
                }
            }
            main.ReRender();

        }

        private void aspectNeptuneOff33_MouseDown(object sender, MouseButtonEventArgs e)
        {

            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectNeptuneOn33.Visibility = Visibility.Visible;
            aspectNeptuneOn33.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_NEPTUNE)
                {
                    data.isAspectDisp = true;
                    break;
                }
            }
            main.ReRender();
        }

        private void aspectPlutoOn33_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectPlutoOff33.Visibility = Visibility.Visible;
            aspectPlutoOff33.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_PLUTO)
                {
                    data.isAspectDisp = false;
                    break;
                }
            }
            main.ReRender();

        }

        private void aspectPlutoOff33_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectPlutoOn33.Visibility = Visibility.Visible;
            aspectPlutoOn33.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_PLUTO)
                {
                    data.isAspectDisp = true;
                    break;
                }
            }
            main.ReRender();

        }

        private void aspectDhOn33_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectDhOff33.Visibility = Visibility.Visible;
            aspectDhOff33.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_DH_TRUENODE)
                {
                    data.isAspectDisp = false;
                    break;
                }
            }
            main.ReRender();

        }

        private void aspectDhOff33_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectDhOn33.Visibility = Visibility.Visible;
            aspectDhOn33.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_DH_TRUENODE)
                {
                    data.isAspectDisp = true;
                    break;
                }
            }
            main.ReRender();

        }

        private void aspectChironOn33_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectChironOff33.Visibility = Visibility.Visible;
            aspectChironOff33.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_CHIRON)
                {
                    data.isAspectDisp = false;
                    break;
                }
            }
            main.ReRender();

        }

        private void aspectChironOff33_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectChironOn33.Visibility = Visibility.Visible;
            aspectChironOn33.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_CHIRON)
                {
                    data.isAspectDisp = true;
                    break;
                }
            }
            main.ReRender();
        }

        private void aspectSunOn22_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectSunOff22.Visibility = Visibility.Visible;
            aspectSunOff22.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_SUN)
                {
                    data.isAspectDisp = false;
                    break;
                }
            }
            main.ReRender();

        }

        private void aspectSunOff22_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectSunOn22.Visibility = Visibility.Visible;
            aspectSunOn22.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_SUN)
                {
                    data.isAspectDisp = true;
                    break;
                }
            }
            main.ReRender();

        }

        private void aspectMoonOn22_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectMoonOff22.Visibility = Visibility.Visible;
            aspectMoonOff22.Height = 24;

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

        private void aspectMoonOff22_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectMoonOn22.Visibility = Visibility.Visible;
            aspectMoonOn22.Height = 24;

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

        private void aspectMercuryOn22_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectMercuryOff22.Visibility = Visibility.Visible;
            aspectMercuryOff22.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_MERCURY)
                {
                    data.isAspectDisp = false;
                    break;
                }
            }
            main.ReRender();

        }

        private void aspectMercuryOff22_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectMercuryOn22.Visibility = Visibility.Visible;
            aspectMercuryOn22.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_MERCURY)
                {
                    data.isAspectDisp = true;
                    break;
                }
            }
            main.ReRender();

        }

        private void aspectVenusOn22_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectVenusOff22.Visibility = Visibility.Visible;
            aspectVenusOff22.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_VENUS)
                {
                    data.isAspectDisp = false;
                    break;
                }
            }
            main.ReRender();

        }

        private void aspectVenusOff22_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectVenusOn22.Visibility = Visibility.Visible;
            aspectVenusOn22.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_VENUS)
                {
                    data.isAspectDisp = true;
                    break;
                }
            }
            main.ReRender();

        }

        private void aspectMarsOn22_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectMarsOff22.Visibility = Visibility.Visible;
            aspectMarsOff22.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_MARS)
                {
                    data.isAspectDisp = false;
                    break;
                }
            }
            main.ReRender();

        }

        private void aspectMarsOff22_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectMarsOn22.Visibility = Visibility.Visible;
            aspectMarsOn22.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_MARS)
                {
                    data.isAspectDisp = true;
                    break;
                }
            }
            main.ReRender();

        }

        private void aspectJupiterOn22_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectJupiterOff22.Visibility = Visibility.Visible;
            aspectJupiterOff22.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_JUPITER)
                {
                    data.isAspectDisp = false;
                    break;
                }
            }
            main.ReRender();

        }

        private void aspectJupiterOff22_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectJupiterOn22.Visibility = Visibility.Visible;
            aspectJupiterOn22.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_JUPITER)
                {
                    data.isAspectDisp = true;
                    break;
                }
            }
            main.ReRender();

        }

        private void aspectSaturnOn22_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectSaturnOff22.Visibility = Visibility.Visible;
            aspectSaturnOff22.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_SATURN)
                {
                    data.isAspectDisp = false;
                    break;
                }
            }
            main.ReRender();

        }

        private void aspectSaturnOff22_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectSaturnOn22.Visibility = Visibility.Visible;
            aspectSaturnOn22.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_SATURN)
                {
                    data.isAspectDisp = true;
                    break;
                }
            }
            main.ReRender();

        }

        private void aspectUranusOn22_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectUranusOff22.Visibility = Visibility.Visible;
            aspectUranusOff22.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_URANUS)
                {
                    data.isAspectDisp = false;
                    break;
                }
            }
            main.ReRender();

        }

        private void aspectUranusOff22_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectUranusOn22.Visibility = Visibility.Visible;
            aspectUranusOn22.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_URANUS)
                {
                    data.isAspectDisp = true;
                    break;
                }
            }
            main.ReRender();

        }

        private void aspectNeptuneOn22_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectNeptuneOff22.Visibility = Visibility.Visible;
            aspectNeptuneOff22.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_NEPTUNE)
                {
                    data.isAspectDisp = false;
                    break;
                }
            }
            main.ReRender();

        }

        private void aspectNeptuneOff22_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectNeptuneOn22.Visibility = Visibility.Visible;
            aspectNeptuneOn22.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_NEPTUNE)
                {
                    data.isAspectDisp = true;
                    break;
                }
            }
            main.ReRender();

        }

        private void aspectPlutoOn22_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectPlutoOff22.Visibility = Visibility.Visible;
            aspectPlutoOff22.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_PLUTO)
                {
                    data.isAspectDisp = false;
                    break;
                }
            }
            main.ReRender();

        }

        private void aspectPlutoOff22_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectPlutoOn22.Visibility = Visibility.Visible;
            aspectPlutoOn22.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_PLUTO)
                {
                    data.isAspectDisp = true;
                    break;
                }
            }
            main.ReRender();

        }

        private void aspectDhOn22_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectDhOff22.Visibility = Visibility.Visible;
            aspectDhOff22.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_DH_TRUENODE)
                {
                    data.isAspectDisp = false;
                    break;
                }
            }
            main.ReRender();

        }

        private void aspectDhOff22_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectDhOn22.Visibility = Visibility.Visible;
            aspectDhOn22.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_DH_TRUENODE)
                {
                    data.isAspectDisp = true;
                    break;
                }
            }
            main.ReRender();

        }

        private void aspectChironOn22_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectChironOff22.Visibility = Visibility.Visible;
            aspectChironOff22.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_CHIRON)
                {
                    data.isAspectDisp = false;
                    break;
                }
            }
            main.ReRender();

        }

        private void aspectChironOff22_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;
            aspectChironOn22.Visibility = Visibility.Visible;
            aspectChironOn22.Height = 24;

            foreach (var data in main.list1)
            {
                if (data.no == (int)CommonData.ZODIAC_CHIRON)
                {
                    data.isAspectDisp = true;
                    break;
                }
            }
            main.ReRender();

        }
    }
}
