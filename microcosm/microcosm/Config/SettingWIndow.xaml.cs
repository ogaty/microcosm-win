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
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using System.IO;

using microcosm.ViewModel;
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
        // settings * 10, 1-1,1-2…5-5で15個
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
        public bool[,] aspectMc = new bool[10, 15];

        public bool[,] aspectConjunction = new bool[10, 15];
        public bool[,] aspectOpposition = new bool[10, 15];
        public bool[,] aspectTrine = new bool[10, 15];
        public bool[,] aspectSquare = new bool[10, 15];
        public bool[,] aspectSextile = new bool[10, 15];
        public bool[,] aspectInconjunct = new bool[10, 15];
        public bool[,] aspectSesquiquadrate = new bool[10, 15];

        public bool[,] aspectDispChecked = new bool[10, 15];

        public Dictionary<string, AspectControlTable> controlTable = new Dictionary<string, AspectControlTable>();
        public Dictionary<string, AspectControlTable> aspectControlTable = new Dictionary<string, AspectControlTable>();

        string[] strNumbers = { "11", "22", "33", "12", "13", "23" };

        public List<string> targetNames = new List<string>();
        public List<string> aspectTargetNames = new List<string>();

        public SettingWIndow(MainWindow main)
        {
            this.main = main;
            InitializeComponent();

            foreach (var s in strNumbers)
            {
                targetNames.Add("aspectSunOn" + s);
                targetNames.Add("aspectMoonOn" + s);
                targetNames.Add("aspectMercuryOn" + s);
                targetNames.Add("aspectVenusOn" + s);
                targetNames.Add("aspectMarsOn" + s);
                targetNames.Add("aspectJupiterOn" + s);
                targetNames.Add("aspectSaturnOn" + s);
                targetNames.Add("aspectUranusOn" + s);
                targetNames.Add("aspectNeptuneOn" + s);
                targetNames.Add("aspectPlutoOn" + s);
                targetNames.Add("aspectDhOn" + s);
                targetNames.Add("aspectChironOn" + s);
                targetNames.Add("aspectAscOn" + s);
                targetNames.Add("aspectMcOn" + s);

                aspectTargetNames.Add("aspectConjunctionOn" + s);
                aspectTargetNames.Add("aspectOppositionOn" + s);
                aspectTargetNames.Add("aspectTrineOn" + s);
                aspectTargetNames.Add("aspectSquareOn" + s);
                aspectTargetNames.Add("aspectSextileOn" + s);
                aspectTargetNames.Add("aspectInconjunctOn" + s);
                aspectTargetNames.Add("aspectSesquiquadrateOn" + s);
            }
            createControlTable();

            dispList.ItemsSource = new ObservableCollection<SettingData>(main.settings);
            dispList.SelectedIndex = 0;
            settingVM = new SettingWindowViewModel(main);
            leftPane.DataContext = settingVM;

            settingVM.dispName = main.currentSetting.dispName;
            setAspect();
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
            if (!controlTable.ContainsKey("aspectSunOn11"))
            {
                return;
            }
            // アスペクト設定
            for (int i = 0; i < targetNames.Count; i++)
            {
                int subIndex = controlTable[targetNames[i]].subIndex;
                for (int j = 0; j < 10; j++)
                {
                    int commonDataNo = controlTable[targetNames[i]].commonDataNo;
                    controlTable[targetNames[i]].tempArray[j, subIndex] = main.settings[j].dispPlanet[subIndex][commonDataNo];
                }
                if (controlTable[targetNames[i]].tempArray[main.dispSettingBox.SelectedIndex, subIndex])
                {
                    controlTable[targetNames[i]].selfElement.Visibility = Visibility.Visible;
                    controlTable[targetNames[i]].selfElement.Height = 24;
                    controlTable[targetNames[i]].anotherElement.Visibility = Visibility.Hidden;
                    controlTable[targetNames[i]].anotherElement.Height = 0;
                }
                else
                {
                    controlTable[targetNames[i]].selfElement.Visibility = Visibility.Hidden;
                    controlTable[targetNames[i]].selfElement.Height = 0;
                    controlTable[targetNames[i]].anotherElement.Visibility = Visibility.Visible;
                    controlTable[targetNames[i]].anotherElement.Height = 24;
                }
            }
            // アスペクト設定
            for (int i = 0; i < aspectTargetNames.Count; i++)
            {
                int subIndex = aspectControlTable[aspectTargetNames[i]].subIndex;
                for (int j = 0; j < 10; j++)
                {
                    AspectKind aspectKindNo = aspectControlTable[aspectTargetNames[i]].aspectKindNo;
                    aspectControlTable[aspectTargetNames[i]].tempArray[j, subIndex] = main.settings[j].dispAspectCategory[subIndex][aspectKindNo];
                }
                if (aspectControlTable[aspectTargetNames[i]].tempArray[main.dispSettingBox.SelectedIndex, subIndex])
                {
                    aspectControlTable[aspectTargetNames[i]].aspectSelfElement.Visibility = Visibility.Visible;
                    aspectControlTable[aspectTargetNames[i]].aspectSelfElement.Height = 24;
                    aspectControlTable[aspectTargetNames[i]].aspectAnotherElement.Visibility = Visibility.Hidden;
                    aspectControlTable[aspectTargetNames[i]].aspectAnotherElement.Height = 0;
                }
                else
                {
                    aspectControlTable[aspectTargetNames[i]].aspectSelfElement.Visibility = Visibility.Hidden;
                    aspectControlTable[aspectTargetNames[i]].aspectSelfElement.Height = 0;
                    aspectControlTable[aspectTargetNames[i]].aspectAnotherElement.Visibility = Visibility.Visible;
                    aspectControlTable[aspectTargetNames[i]].aspectAnotherElement.Height = 24;
                }
            }

            disp11.IsChecked = main.settings[list.SelectedIndex].dispAspect[0, 0];
            disp22.IsChecked = main.settings[list.SelectedIndex].dispAspect[1, 1];
            disp33.IsChecked = main.settings[list.SelectedIndex].dispAspect[2, 2];
            disp12.IsChecked = main.settings[list.SelectedIndex].dispAspect[0, 1];
            disp13.IsChecked = main.settings[list.SelectedIndex].dispAspect[0, 2];
            disp23.IsChecked = main.settings[list.SelectedIndex].dispAspect[1, 2];
            for (int i = 0; i < 10; i++)
            {
                aspectDispChecked[i, 0] = main.settings[i].dispAspect[0, 0];
                aspectDispChecked[i, 1] = main.settings[i].dispAspect[1, 1];
                aspectDispChecked[i, 2] = main.settings[i].dispAspect[2, 2];
                aspectDispChecked[i, 3] = main.settings[i].dispAspect[0, 1];
                aspectDispChecked[i, 4] = main.settings[i].dispAspect[0, 2];
                aspectDispChecked[i, 5] = main.settings[i].dispAspect[1, 2];
            }

            setOrb();
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
            ringSubIndex subidx = CommonData.getRingSubIndex(index);
            int from = subidx.from;
            int to = subidx.to;

            sunSoft1st.Text = main.settings[dispList.SelectedIndex].orbs[index][OrbKind.SUN_SOFT_1ST].ToString();
            sunHard1st.Text = main.settings[dispList.SelectedIndex].orbs[index][OrbKind.SUN_HARD_1ST].ToString();
            sunSoft2nd.Text = main.settings[dispList.SelectedIndex].orbs[index][OrbKind.SUN_SOFT_2ND].ToString();
            sunHard2nd.Text = main.settings[dispList.SelectedIndex].orbs[index][OrbKind.SUN_HARD_2ND].ToString();
            sunSoft150.Text = main.settings[dispList.SelectedIndex].orbs[index][OrbKind.SUN_SOFT_150].ToString();
            sunHard150.Text = main.settings[dispList.SelectedIndex].orbs[index][OrbKind.SUN_HARD_150].ToString();
            moonSoft1st.Text = main.settings[dispList.SelectedIndex].orbs[index][OrbKind.MOON_SOFT_1ST].ToString();
            moonHard1st.Text = main.settings[dispList.SelectedIndex].orbs[index][OrbKind.MOON_HARD_1ST].ToString();
            moonSoft2nd.Text = main.settings[dispList.SelectedIndex].orbs[index][OrbKind.MOON_SOFT_2ND].ToString();
            moonHard2nd.Text = main.settings[dispList.SelectedIndex].orbs[index][OrbKind.MOON_HARD_2ND].ToString();
            moonSoft150.Text = main.settings[dispList.SelectedIndex].orbs[index][OrbKind.MOON_SOFT_150].ToString();
            moonHard150.Text = main.settings[dispList.SelectedIndex].orbs[index][OrbKind.MOON_HARD_150].ToString();
            otherSoft1st.Text = main.settings[dispList.SelectedIndex].orbs[index][OrbKind.OTHER_SOFT_1ST].ToString();
            otherHard1st.Text = main.settings[dispList.SelectedIndex].orbs[index][OrbKind.OTHER_HARD_1ST].ToString();
            otherSoft2nd.Text = main.settings[dispList.SelectedIndex].orbs[index][OrbKind.OTHER_SOFT_2ND].ToString();
            otherHard2nd.Text = main.settings[dispList.SelectedIndex].orbs[index][OrbKind.OTHER_HARD_2ND].ToString();
            otherSoft150.Text = main.settings[dispList.SelectedIndex].orbs[index][OrbKind.OTHER_SOFT_150].ToString();
            otherHard150.Text = main.settings[dispList.SelectedIndex].orbs[index][OrbKind.OTHER_HARD_150].ToString();
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

        private void aspectMouseDownCommon(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;

            Image ctl = (Image)(controlTable[img.Name].anotherElement);
            ctl.Visibility = Visibility.Visible;
            ctl.Height = 24;

            int index = dispList.SelectedIndex;
            int subindex = controlTable[img.Name].subIndex;

            controlTable[img.Name].targetBoolean = !controlTable[img.Name].targetBoolean;
            controlTable[img.Name].tempArray[index, subindex] = controlTable[img.Name].targetBoolean;

            foreach (var data in main.list1)
            {
                if (data.no == controlTable[img.Name].commonDataNo)
                {
                    data.isAspectDisp = true;
                    break;
                }
            }
            main.ReRender();
        }

        private void createControlTable()
        {
            int subIndexNo = 0;
            foreach (string n in strNumbers)
            {
                if (n == "11") subIndexNo = 0;
                else if (n == "22") subIndexNo = 1;
                else if (n == "33") subIndexNo = 2;
                else if (n == "12") subIndexNo = 3;
                else if (n == "13") subIndexNo = 4;
                else if (n == "23") subIndexNo = 5;
                controlTable.Add("aspectSunOn" + n, new AspectControlTable()
                {
                    selfElement = (FrameworkElement)FindName("aspectSunOn" + n),
                    anotherElement = (FrameworkElement)FindName("aspectSunOff" + n),
                    tempArray = aspectSun,
                    targetBoolean = true,
                    subIndex = subIndexNo,
                    commonDataNo = (int)CommonData.ZODIAC_SUN
                });
                controlTable.Add("aspectMoonOn" + n, new AspectControlTable()
                {
                    selfElement = (FrameworkElement)FindName("aspectMoonOn" + n),
                    anotherElement = (FrameworkElement)FindName("aspectMoonOff" + n),
                    tempArray = aspectMoon,
                    targetBoolean = true,
                    subIndex = subIndexNo,
                    commonDataNo = (int)CommonData.ZODIAC_MOON
                });
                controlTable.Add("aspectMercuryOn" + n, new AspectControlTable()
                {
                    selfElement = (FrameworkElement)FindName("aspectMercuryOn" + n),
                    anotherElement = (FrameworkElement)FindName("aspectMercuryOff" + n),
                    tempArray = aspectMercury,
                    targetBoolean = true,
                    subIndex = subIndexNo,
                    commonDataNo = (int)CommonData.ZODIAC_MERCURY
                });
                controlTable.Add("aspectVenusOn" + n, new AspectControlTable()
                {
                    selfElement = (FrameworkElement)FindName("aspectVenusOn" + n),
                    anotherElement = (FrameworkElement)FindName("aspectVenusOff" + n),
                    tempArray = aspectVenus,
                    targetBoolean = true,
                    subIndex = subIndexNo,
                    commonDataNo = (int)CommonData.ZODIAC_VENUS
                });
                controlTable.Add("aspectMarsOn" + n, new AspectControlTable()
                {
                    selfElement = (FrameworkElement)FindName("aspectMarsOn" + n),
                    anotherElement = (FrameworkElement)FindName("aspectMarsOff" + n),
                    tempArray = aspectMars,
                    targetBoolean = true,
                    subIndex = subIndexNo,
                    commonDataNo = (int)CommonData.ZODIAC_MARS
                });
                controlTable.Add("aspectJupiterOn" + n, new AspectControlTable()
                {
                    selfElement = (FrameworkElement)FindName("aspectJupiterOn" + n),
                    anotherElement = (FrameworkElement)FindName("aspectJupiterOff" + n),
                    tempArray = aspectJupiter,
                    targetBoolean = true,
                    subIndex = subIndexNo,
                    commonDataNo = (int)CommonData.ZODIAC_JUPITER
                });
                controlTable.Add("aspectSaturnOn" + n, new AspectControlTable()
                {
                    selfElement = (FrameworkElement)FindName("aspectSaturnOn" + n),
                    anotherElement = (FrameworkElement)FindName("aspectSaturnOff" + n),
                    tempArray = aspectSaturn,
                    targetBoolean = true,
                    subIndex = subIndexNo,
                    commonDataNo = (int)CommonData.ZODIAC_SATURN
                });
                controlTable.Add("aspectUranusOn" + n, new AspectControlTable()
                {
                    selfElement = (FrameworkElement)FindName("aspectUranusOn" + n),
                    anotherElement = (FrameworkElement)FindName("aspectUranusOff" + n),
                    tempArray = aspectUranus,
                    targetBoolean = true,
                    subIndex = subIndexNo,
                    commonDataNo = (int)CommonData.ZODIAC_URANUS
                });
                controlTable.Add("aspectNeptuneOn" + n, new AspectControlTable()
                {
                    selfElement = (FrameworkElement)FindName("aspectNeptuneOn" + n),
                    anotherElement = (FrameworkElement)FindName("aspectNeptuneOff" + n),
                    tempArray = aspectNeptune,
                    targetBoolean = true,
                    subIndex = subIndexNo,
                    commonDataNo = (int)CommonData.ZODIAC_NEPTUNE
                });
                controlTable.Add("aspectPlutoOn" + n, new AspectControlTable()
                {
                    selfElement = (FrameworkElement)FindName("aspectPlutoOn" + n),
                    anotherElement = (FrameworkElement)FindName("aspectPlutoOff" + n),
                    tempArray = aspectPluto,
                    targetBoolean = true,
                    subIndex = subIndexNo,
                    commonDataNo = (int)CommonData.ZODIAC_PLUTO
                });
                controlTable.Add("aspectDhOn" + n, new AspectControlTable()
                {
                    selfElement = (FrameworkElement)FindName("aspectDhOn" + n),
                    anotherElement = (FrameworkElement)FindName("aspectDhOff" + n),
                    tempArray = aspectDh,
                    targetBoolean = true,
                    subIndex = subIndexNo,
                    commonDataNo = (int)CommonData.ZODIAC_DH_TRUENODE
                });
                controlTable.Add("aspectChironOn" + n, new AspectControlTable()
                {
                    selfElement = (FrameworkElement)FindName("aspectChironOn" + n),
                    anotherElement = (FrameworkElement)FindName("aspectChironOff" + n),
                    tempArray = aspectChiron,
                    targetBoolean = true,
                    subIndex = subIndexNo,
                    commonDataNo = (int)CommonData.ZODIAC_CHIRON
                });
                controlTable.Add("aspectAscOn" + n, new AspectControlTable()
                {
                    selfElement = (FrameworkElement)FindName("aspectAscOn" + n),
                    anotherElement = (FrameworkElement)FindName("aspectAscOff" + n),
                    tempArray = aspectAsc,
                    targetBoolean = true,
                    subIndex = subIndexNo,
                    commonDataNo = (int)CommonData.ZODIAC_ASC
                });
                controlTable.Add("aspectMcOn" + n, new AspectControlTable()
                {
                    selfElement = (FrameworkElement)FindName("aspectMcOn" + n),
                    anotherElement = (FrameworkElement)FindName("aspectMcOff" + n),
                    tempArray = aspectMc,
                    targetBoolean = true,
                    subIndex = subIndexNo,
                    commonDataNo = (int)CommonData.ZODIAC_MC
                });
                controlTable.Add("aspectSunOff" + n, new AspectControlTable()
                {
                    selfElement = (FrameworkElement)FindName("aspectSunOff" + n),
                    anotherElement = (FrameworkElement)FindName("aspectSunOn" + n),
                    tempArray = aspectSun,
                    targetBoolean = true,
                    subIndex = subIndexNo,
                    commonDataNo = (int)CommonData.ZODIAC_SUN
                });
                controlTable.Add("aspectMoonOff" + n, new AspectControlTable()
                {
                    selfElement = (FrameworkElement)FindName("aspectMoonOff" + n),
                    anotherElement = (FrameworkElement)FindName("aspectMoonOn" + n),
                    tempArray = aspectMoon,
                    targetBoolean = true,
                    subIndex = subIndexNo,
                    commonDataNo = (int)CommonData.ZODIAC_MOON
                });
                controlTable.Add("aspectMercuryOff" + n, new AspectControlTable()
                {
                    selfElement = (FrameworkElement)FindName("aspectMercuryOff" + n),
                    anotherElement = (FrameworkElement)FindName("aspectMercuryOn" + n),
                    tempArray = aspectMercury,
                    targetBoolean = true,
                    subIndex = subIndexNo,
                    commonDataNo = (int)CommonData.ZODIAC_MERCURY
                });
                controlTable.Add("aspectVenusOff" + n, new AspectControlTable()
                {
                    selfElement = (FrameworkElement)FindName("aspectVenusOff" + n),
                    anotherElement = (FrameworkElement)FindName("aspectVenusOn" + n),
                    tempArray = aspectVenus,
                    targetBoolean = true,
                    subIndex = subIndexNo,
                    commonDataNo = (int)CommonData.ZODIAC_VENUS
                });
                controlTable.Add("aspectMarsOff" + n, new AspectControlTable()
                {
                    selfElement = (FrameworkElement)FindName("aspectMarsOff" + n),
                    anotherElement = (FrameworkElement)FindName("aspectMarsOn" + n),
                    tempArray = aspectMars,
                    targetBoolean = true,
                    subIndex = subIndexNo,
                    commonDataNo = (int)CommonData.ZODIAC_MARS
                });
                controlTable.Add("aspectJupiterOff" + n, new AspectControlTable()
                {
                    selfElement = (FrameworkElement)FindName("aspectJupiterOff" + n),
                    anotherElement = (FrameworkElement)FindName("aspectJupiterOn" + n),
                    tempArray = aspectJupiter,
                    targetBoolean = true,
                    subIndex = subIndexNo,
                    commonDataNo = (int)CommonData.ZODIAC_JUPITER
                });
                controlTable.Add("aspectSaturnOff" + n, new AspectControlTable()
                {
                    selfElement = (FrameworkElement)FindName("aspectSaturnOff" + n),
                    anotherElement = (FrameworkElement)FindName("aspectSaturnOn" + n),
                    tempArray = aspectSaturn,
                    targetBoolean = true,
                    subIndex = subIndexNo,
                    commonDataNo = (int)CommonData.ZODIAC_SATURN
                });
                controlTable.Add("aspectUranusOff" + n, new AspectControlTable()
                {
                    selfElement = (FrameworkElement)FindName("aspectUranusOff" + n),
                    anotherElement = (FrameworkElement)FindName("aspectUranusOn" + n),
                    tempArray = aspectUranus,
                    targetBoolean = true,
                    subIndex = subIndexNo,
                    commonDataNo = (int)CommonData.ZODIAC_URANUS
                });
                controlTable.Add("aspectNeptuneOff" + n, new AspectControlTable()
                {
                    selfElement = (FrameworkElement)FindName("aspectNeptuneOff" + n),
                    anotherElement = (FrameworkElement)FindName("aspectNeptuneOn" + n),
                    tempArray = aspectNeptune,
                    targetBoolean = true,
                    subIndex = subIndexNo,
                    commonDataNo = (int)CommonData.ZODIAC_NEPTUNE
                });
                controlTable.Add("aspectPlutoOff" + n, new AspectControlTable()
                {
                    selfElement = (FrameworkElement)FindName("aspectPlutoOff" + n),
                    anotherElement = (FrameworkElement)FindName("aspectPlutoOn" + n),
                    tempArray = aspectPluto,
                    targetBoolean = true,
                    subIndex = subIndexNo,
                    commonDataNo = (int)CommonData.ZODIAC_PLUTO
                });
                controlTable.Add("aspectDhOff" + n, new AspectControlTable()
                {
                    selfElement = (FrameworkElement)FindName("aspectDhOff" + n),
                    anotherElement = (FrameworkElement)FindName("aspectDhOn" + n),
                    tempArray = aspectDh,
                    targetBoolean = true,
                    subIndex = subIndexNo,
                    commonDataNo = (int)CommonData.ZODIAC_DH_TRUENODE
                });
                controlTable.Add("aspectChironOff" + n, new AspectControlTable()
                {
                    selfElement = (FrameworkElement)FindName("aspectChironOff" + n),
                    anotherElement = (FrameworkElement)FindName("aspectChironOn" + n),
                    tempArray = aspectChiron,
                    targetBoolean = true,
                    subIndex = subIndexNo,
                    commonDataNo = (int)CommonData.ZODIAC_CHIRON
                });
                controlTable.Add("aspectAscOff" + n, new AspectControlTable()
                {
                    selfElement = (FrameworkElement)FindName("aspectAscOff" + n),
                    anotherElement = (FrameworkElement)FindName("aspectAscOn" + n),
                    tempArray = aspectAsc,
                    targetBoolean = true,
                    subIndex = subIndexNo,
                    commonDataNo = (int)CommonData.ZODIAC_ASC
                });
                controlTable.Add("aspectMcOff" + n, new AspectControlTable()
                {
                    selfElement = (FrameworkElement)FindName("aspectMcOff" + n),
                    anotherElement = (FrameworkElement)FindName("aspectMcOn" + n),
                    tempArray = aspectMc,
                    targetBoolean = true,
                    subIndex = subIndexNo,
                    commonDataNo = (int)CommonData.ZODIAC_MC
                });

                aspectControlTable.Add("aspectConjunctionOn" + n, new AspectControlTable()
                {
                    aspectSelfElement = (FrameworkElement)FindName("aspectConjunctionOn" + n),
                    aspectAnotherElement = (FrameworkElement)FindName("aspectConjunctionOff" + n),
                    tempArray = aspectConjunction,
                    targetBoolean = true,
                    subIndex = subIndexNo,
                    aspectKindNo = AspectKind.CONJUNCTION
                });
                aspectControlTable.Add("aspectOppositionOn" + n, new AspectControlTable()
                {
                    aspectSelfElement = (FrameworkElement)FindName("aspectOppositionOn" + n),
                    aspectAnotherElement = (FrameworkElement)FindName("aspectOppositionOff" + n),
                    tempArray = aspectOpposition,
                    targetBoolean = true,
                    subIndex = subIndexNo,
                    aspectKindNo = AspectKind.OPPOSITION
                });
                aspectControlTable.Add("aspectSquareOn" + n, new AspectControlTable()
                {
                    aspectSelfElement = (FrameworkElement)FindName("aspectSquareOn" + n),
                    aspectAnotherElement = (FrameworkElement)FindName("aspectSquareOff" + n),
                    tempArray = aspectSquare,
                    targetBoolean = true,
                    subIndex = subIndexNo,
                    aspectKindNo = AspectKind.SQUARE
                });
                aspectControlTable.Add("aspectTrineOn" + n, new AspectControlTable()
                {
                    aspectSelfElement = (FrameworkElement)FindName("aspectTrineOn" + n),
                    aspectAnotherElement = (FrameworkElement)FindName("aspectTrineOff" + n),
                    tempArray = aspectTrine,
                    targetBoolean = true,
                    subIndex = subIndexNo,
                    aspectKindNo = AspectKind.TRINE
                });
                aspectControlTable.Add("aspectSextileOn" + n, new AspectControlTable()
                {
                    aspectSelfElement = (FrameworkElement)FindName("aspectSextileOn" + n),
                    aspectAnotherElement = (FrameworkElement)FindName("aspectSextileOff" + n),
                    tempArray = aspectSextile,
                    targetBoolean = true,
                    subIndex = subIndexNo,
                    aspectKindNo = AspectKind.SEXTILE
                });
                aspectControlTable.Add("aspectInconjunctOn" + n, new AspectControlTable()
                {
                    aspectSelfElement = (FrameworkElement)FindName("aspectInconjunctOn" + n),
                    aspectAnotherElement = (FrameworkElement)FindName("aspectInconjunctOff" + n),
                    tempArray = aspectInconjunct,
                    targetBoolean = true,
                    subIndex = subIndexNo,
                    aspectKindNo = AspectKind.INCONJUNCT
                });
                aspectControlTable.Add("aspectSesquiquadrateOn" + n, new AspectControlTable()
                {
                    aspectSelfElement = (FrameworkElement)FindName("aspectSesquiquadrateOn" + n),
                    aspectAnotherElement = (FrameworkElement)FindName("aspectSesquiquadrateOff" + n),
                    tempArray = aspectSesquiquadrate,
                    targetBoolean = true,
                    subIndex = subIndexNo,
                    aspectKindNo = AspectKind.SESQUIQUADRATE
                });
                aspectControlTable.Add("aspectConjunctionOff" + n, new AspectControlTable()
                {
                    aspectSelfElement = (FrameworkElement)FindName("aspectConjunctionOff" + n),
                    aspectAnotherElement = (FrameworkElement)FindName("aspectConjunctionOn" + n),
                    tempArray = aspectConjunction,
                    targetBoolean = true,
                    subIndex = subIndexNo,
                    aspectKindNo = AspectKind.CONJUNCTION
                });
                aspectControlTable.Add("aspectOppositionOff" + n, new AspectControlTable()
                {
                    aspectSelfElement = (FrameworkElement)FindName("aspectOppositionOff" + n),
                    aspectAnotherElement = (FrameworkElement)FindName("aspectOppositionOn" + n),
                    tempArray = aspectOpposition,
                    targetBoolean = true,
                    subIndex = subIndexNo,
                    aspectKindNo = AspectKind.OPPOSITION
                });
                aspectControlTable.Add("aspectSquareOff" + n, new AspectControlTable()
                {
                    aspectSelfElement = (FrameworkElement)FindName("aspectSquareOff" + n),
                    aspectAnotherElement = (FrameworkElement)FindName("aspectSquareOn" + n),
                    tempArray = aspectSquare,
                    targetBoolean = true,
                    subIndex = subIndexNo,
                    aspectKindNo = AspectKind.SQUARE
                });
                aspectControlTable.Add("aspectTrineOff" + n, new AspectControlTable()
                {
                    aspectSelfElement = (FrameworkElement)FindName("aspectTrineOff" + n),
                    aspectAnotherElement = (FrameworkElement)FindName("aspectTrineOn" + n),
                    tempArray = aspectTrine,
                    targetBoolean = true,
                    subIndex = subIndexNo,
                    aspectKindNo = AspectKind.TRINE
                });
                aspectControlTable.Add("aspectSextileOff" + n, new AspectControlTable()
                {
                    aspectSelfElement = (FrameworkElement)FindName("aspectSextileOff" + n),
                    aspectAnotherElement = (FrameworkElement)FindName("aspectSextileOn" + n),
                    tempArray = aspectSextile,
                    targetBoolean = true,
                    subIndex = subIndexNo,
                    aspectKindNo = AspectKind.SEXTILE
                });
                aspectControlTable.Add("aspectInconjunctOff" + n, new AspectControlTable()
                {
                    aspectSelfElement = (FrameworkElement)FindName("aspectInconjunctOff" + n),
                    aspectAnotherElement = (FrameworkElement)FindName("aspectInconjunctOn" + n),
                    tempArray = aspectInconjunct,
                    targetBoolean = true,
                    subIndex = subIndexNo,
                    aspectKindNo = AspectKind.INCONJUNCT
                });
                aspectControlTable.Add("aspectSesquiquadrateOff" + n, new AspectControlTable()
                {
                    aspectSelfElement = (FrameworkElement)FindName("aspectSesquiquadrateOff" + n),
                    aspectAnotherElement = (FrameworkElement)FindName("aspectSesquiquadrateOn" + n),
                    tempArray = aspectSesquiquadrate,
                    targetBoolean = true,
                    subIndex = subIndexNo,
                    aspectKindNo = AspectKind.SESQUIQUADRATE
                });

            }


        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            SettingXml xmldata = new SettingXml();
            if (aspectSunOn11.Visibility == Visibility.Visible)
            {
                xmldata.aspectSun11 = true;
            }
            else
            {
                xmldata.aspectSun11 = false;
            }
            if (aspectMoonOn11.Visibility == Visibility.Visible)
            {
                xmldata.aspectMoon11 = true;
            }
            else
            {
                xmldata.aspectMoon11 = false;
            }
            if (aspectMercuryOn11.Visibility == Visibility.Visible)
            {
                xmldata.aspectMercury11 = true;
            }
            else
            {
                xmldata.aspectMercury11 = false;
            }
            if (aspectVenusOn11.Visibility == Visibility.Visible)
            {
                xmldata.aspectVenus11 = true;
            }
            else
            {
                xmldata.aspectVenus11 = false;
            }
            if (aspectMarsOn11.Visibility == Visibility.Visible)
            {
                xmldata.aspectMars11 = true;
            }
            else
            {
                xmldata.aspectMars11 = false;
            }
            if (aspectJupiterOn11.Visibility == Visibility.Visible)
            {
                xmldata.aspectJupiter11 = true;
            }
            else
            {
                xmldata.aspectJupiter11 = false;
            }
            if (aspectSaturnOn11.Visibility == Visibility.Visible)
            {
                xmldata.aspectSaturn11 = true;
            }
            else
            {
                xmldata.aspectSaturn11 = false;
            }
            if (aspectUranusOn11.Visibility == Visibility.Visible)
            {
                xmldata.aspectUranus11 = true;
            }
            else
            {
                xmldata.aspectUranus11 = false;
            }
            if (aspectNeptuneOn11.Visibility == Visibility.Visible)
            {
                xmldata.aspectNeptune11 = true;
            }
            else
            {
                xmldata.aspectNeptune11 = false;
            }
            if (aspectPlutoOn11.Visibility == Visibility.Visible)
            {
                xmldata.aspectPluto11 = true;
            }
            else
            {
                xmldata.aspectPluto11 = false;
            }
            if (aspectSunOn22.Visibility == Visibility.Visible)
            {
                xmldata.aspectSun22 = true;
            }
            else
            {
                xmldata.aspectSun22 = false;
            }
            if (aspectMoonOn22.Visibility == Visibility.Visible)
            {
                xmldata.aspectMoon22 = true;
            }
            else
            {
                xmldata.aspectMoon22 = false;
            }
            if (aspectMercuryOn22.Visibility == Visibility.Visible)
            {
                xmldata.aspectMercury22 = true;
            }
            else
            {
                xmldata.aspectMercury22 = false;
            }
            if (aspectVenusOn22.Visibility == Visibility.Visible)
            {
                xmldata.aspectVenus22 = true;
            }
            else
            {
                xmldata.aspectVenus22 = false;
            }
            if (aspectMarsOn22.Visibility == Visibility.Visible)
            {
                xmldata.aspectMars22 = true;
            }
            else
            {
                xmldata.aspectMars22 = false;
            }
            if (aspectJupiterOn22.Visibility == Visibility.Visible)
            {
                xmldata.aspectJupiter22 = true;
            }
            else
            {
                xmldata.aspectJupiter22 = false;
            }
            if (aspectSaturnOn22.Visibility == Visibility.Visible)
            {
                xmldata.aspectSaturn22 = true;
            }
            else
            {
                xmldata.aspectSaturn22 = false;
            }
            if (aspectUranusOn22.Visibility == Visibility.Visible)
            {
                xmldata.aspectUranus22 = true;
            }
            else
            {
                xmldata.aspectUranus22 = false;
            }
            if (aspectNeptuneOn22.Visibility == Visibility.Visible)
            {
                xmldata.aspectNeptune22 = true;
            }
            else
            {
                xmldata.aspectNeptune22 = false;
            }
            if (aspectPlutoOn22.Visibility == Visibility.Visible)
            {
                xmldata.aspectPluto22 = true;
            }
            else
            {
                xmldata.aspectPluto22 = false;
            }
            if (aspectSunOn33.Visibility == Visibility.Visible)
            {
                xmldata.aspectSun33 = true;
            }
            else
            {
                xmldata.aspectSun33 = false;
            }
            if (aspectMoonOn33.Visibility == Visibility.Visible)
            {
                xmldata.aspectMoon33 = true;
            }
            else
            {
                xmldata.aspectMoon33 = false;
            }
            if (aspectMercuryOn33.Visibility == Visibility.Visible)
            {
                xmldata.aspectMercury33 = true;
            }
            else
            {
                xmldata.aspectMercury33 = false;
            }
            if (aspectVenusOn33.Visibility == Visibility.Visible)
            {
                xmldata.aspectVenus33 = true;
            }
            else
            {
                xmldata.aspectVenus33 = false;
            }
            if (aspectMarsOn33.Visibility == Visibility.Visible)
            {
                xmldata.aspectMars33 = true;
            }
            else
            {
                xmldata.aspectMars33 = false;
            }
            if (aspectJupiterOn33.Visibility == Visibility.Visible)
            {
                xmldata.aspectJupiter33 = true;
            }
            else
            {
                xmldata.aspectJupiter33 = false;
            }
            if (aspectSaturnOn33.Visibility == Visibility.Visible)
            {
                xmldata.aspectSaturn33 = true;
            }
            else
            {
                xmldata.aspectSaturn33 = false;
            }
            if (aspectUranusOn33.Visibility == Visibility.Visible)
            {
                xmldata.aspectUranus33 = true;
            }
            else
            {
                xmldata.aspectUranus33 = false;
            }
            if (aspectNeptuneOn33.Visibility == Visibility.Visible)
            {
                xmldata.aspectNeptune33 = true;
            }
            else
            {
                xmldata.aspectNeptune33 = false;
            }
            if (aspectPlutoOn33.Visibility == Visibility.Visible)
            {
                xmldata.aspectPluto33 = true;
            }
            else
            {
                xmldata.aspectPluto33 = false;
            }
            if (aspectSunOn12.Visibility == Visibility.Visible)
            {
                xmldata.aspectSun12 = true;
            }
            else
            {
                xmldata.aspectSun12 = false;
            }
            if (aspectMoonOn12.Visibility == Visibility.Visible)
            {
                xmldata.aspectMoon12 = true;
            }
            else
            {
                xmldata.aspectMoon12 = false;
            }
            if (aspectMercuryOn12.Visibility == Visibility.Visible)
            {
                xmldata.aspectMercury12 = true;
            }
            else
            {
                xmldata.aspectMercury12 = false;
            }
            if (aspectVenusOn12.Visibility == Visibility.Visible)
            {
                xmldata.aspectVenus12 = true;
            }
            else
            {
                xmldata.aspectVenus12 = false;
            }
            if (aspectMarsOn12.Visibility == Visibility.Visible)
            {
                xmldata.aspectMars12 = true;
            }
            else
            {
                xmldata.aspectMars12 = false;
            }
            if (aspectJupiterOn12.Visibility == Visibility.Visible)
            {
                xmldata.aspectJupiter12 = true;
            }
            else
            {
                xmldata.aspectJupiter12 = false;
            }
            if (aspectSaturnOn12.Visibility == Visibility.Visible)
            {
                xmldata.aspectSaturn12 = true;
            }
            else
            {
                xmldata.aspectSaturn12 = false;
            }
            if (aspectUranusOn12.Visibility == Visibility.Visible)
            {
                xmldata.aspectUranus12 = true;
            }
            else
            {
                xmldata.aspectUranus12 = false;
            }
            if (aspectNeptuneOn12.Visibility == Visibility.Visible)
            {
                xmldata.aspectNeptune12 = true;
            }
            else
            {
                xmldata.aspectNeptune12 = false;
            }
            if (aspectPlutoOn12.Visibility == Visibility.Visible)
            {
                xmldata.aspectPluto12 = true;
            }
            else
            {
                xmldata.aspectPluto12 = false;
            }
            if (aspectSunOn13.Visibility == Visibility.Visible)
            {
                xmldata.aspectSun13 = true;
            }
            else
            {
                xmldata.aspectSun13 = false;
            }
            if (aspectMoonOn13.Visibility == Visibility.Visible)
            {
                xmldata.aspectMoon13 = true;
            }
            else
            {
                xmldata.aspectMoon13 = false;
            }
            if (aspectMercuryOn13.Visibility == Visibility.Visible)
            {
                xmldata.aspectMercury13 = true;
            }
            else
            {
                xmldata.aspectMercury13 = false;
            }
            if (aspectVenusOn13.Visibility == Visibility.Visible)
            {
                xmldata.aspectVenus13 = true;
            }
            else
            {
                xmldata.aspectVenus13 = false;
            }
            if (aspectMarsOn13.Visibility == Visibility.Visible)
            {
                xmldata.aspectMars13 = true;
            }
            else
            {
                xmldata.aspectMars13 = false;
            }
            if (aspectJupiterOn13.Visibility == Visibility.Visible)
            {
                xmldata.aspectJupiter13 = true;
            }
            else
            {
                xmldata.aspectJupiter13 = false;
            }
            if (aspectSaturnOn13.Visibility == Visibility.Visible)
            {
                xmldata.aspectSaturn13 = true;
            }
            else
            {
                xmldata.aspectSaturn13 = false;
            }
            if (aspectUranusOn13.Visibility == Visibility.Visible)
            {
                xmldata.aspectUranus13 = true;
            }
            else
            {
                xmldata.aspectUranus13 = false;
            }
            if (aspectNeptuneOn13.Visibility == Visibility.Visible)
            {
                xmldata.aspectNeptune13 = true;
            }
            else
            {
                xmldata.aspectNeptune13 = false;
            }
            if (aspectPlutoOn13.Visibility == Visibility.Visible)
            {
                xmldata.aspectPluto13 = true;
            }
            else
            {
                xmldata.aspectPluto13 = false;
            }
            if (aspectSunOn23.Visibility == Visibility.Visible)
            {
                xmldata.aspectSun23 = true;
            }
            else
            {
                xmldata.aspectSun23 = false;
            }
            if (aspectMoonOn23.Visibility == Visibility.Visible)
            {
                xmldata.aspectMoon23 = true;
            }
            else
            {
                xmldata.aspectMoon23 = false;
            }
            if (aspectMercuryOn23.Visibility == Visibility.Visible)
            {
                xmldata.aspectMercury23 = true;
            }
            else
            {
                xmldata.aspectMercury23 = false;
            }
            if (aspectVenusOn23.Visibility == Visibility.Visible)
            {
                xmldata.aspectVenus23 = true;
            }
            else
            {
                xmldata.aspectVenus23 = false;
            }
            if (aspectMarsOn23.Visibility == Visibility.Visible)
            {
                xmldata.aspectMars23 = true;
            }
            else
            {
                xmldata.aspectMars23 = false;
            }
            if (aspectJupiterOn23.Visibility == Visibility.Visible)
            {
                xmldata.aspectJupiter23 = true;
            }
            else
            {
                xmldata.aspectJupiter23 = false;
            }
            if (aspectSaturnOn23.Visibility == Visibility.Visible)
            {
                xmldata.aspectSaturn23 = true;
            }
            else
            {
                xmldata.aspectSaturn23 = false;
            }
            if (aspectUranusOn23.Visibility == Visibility.Visible)
            {
                xmldata.aspectUranus23 = true;
            }
            else
            {
                xmldata.aspectUranus23 = false;
            }
            if (aspectNeptuneOn23.Visibility == Visibility.Visible)
            {
                xmldata.aspectNeptune23 = true;
            }
            else
            {
                xmldata.aspectNeptune23 = false;
            }
            if (aspectPlutoOn23.Visibility == Visibility.Visible)
            {
                xmldata.aspectPluto23 = true;
            }
            else
            {
                xmldata.aspectPluto23 = false;
            }

            for (int index = 0; index < 10; index++)
            {
                xmldata.aspectDh11 = aspectDh[index, 0];
                xmldata.aspectDh22 = aspectDh[index, 1];
                xmldata.aspectDh33 = aspectDh[index, 2];
                xmldata.aspectDh12 = aspectDh[index, 3];
                xmldata.aspectDh13 = aspectDh[index, 4];
                xmldata.aspectDh23 = aspectDh[index, 5];
                xmldata.aspectChiron11 = aspectChiron[index, 0];
                xmldata.aspectChiron22 = aspectChiron[index, 1];
                xmldata.aspectChiron33 = aspectChiron[index, 2];
                xmldata.aspectChiron12 = aspectChiron[index, 3];
                xmldata.aspectChiron13 = aspectChiron[index, 4];
                xmldata.aspectChiron23 = aspectChiron[index, 5];
                xmldata.aspectAsc11 = aspectAsc[index, 0];
                xmldata.aspectAsc22 = aspectAsc[index, 1];
                xmldata.aspectAsc33 = aspectAsc[index, 2];
                xmldata.aspectAsc12 = aspectAsc[index, 3];
                xmldata.aspectAsc13 = aspectAsc[index, 4];
                xmldata.aspectAsc23 = aspectAsc[index, 5];
                xmldata.aspectMc11 = aspectMc[index, 0];
                xmldata.aspectMc22 = aspectMc[index, 1];
                xmldata.aspectMc33 = aspectMc[index, 2];
                xmldata.aspectMc12 = aspectMc[index, 3];
                xmldata.aspectMc13 = aspectMc[index, 4];
                xmldata.aspectMc23 = aspectMc[index, 5];
                xmldata.aspectConjunction11 = aspectConjunction[index, 0];
                xmldata.aspectConjunction22 = aspectConjunction[index, 1];
                xmldata.aspectConjunction33 = aspectConjunction[index, 2];
                xmldata.aspectConjunction12 = aspectConjunction[index, 3];
                xmldata.aspectConjunction13 = aspectConjunction[index, 4];
                xmldata.aspectConjunction23 = aspectConjunction[index, 5];
                xmldata.aspectOpposition11 = aspectOpposition[index, 0];
                xmldata.aspectOpposition22 = aspectOpposition[index, 1];
                xmldata.aspectOpposition33 = aspectOpposition[index, 2];
                xmldata.aspectOpposition12 = aspectOpposition[index, 3];
                xmldata.aspectOpposition13 = aspectOpposition[index, 4];
                xmldata.aspectOpposition23 = aspectOpposition[index, 5];
                xmldata.aspectTrine11 = aspectTrine[index, 0];
                xmldata.aspectTrine22 = aspectTrine[index, 1];
                xmldata.aspectTrine33 = aspectTrine[index, 2];
                xmldata.aspectTrine12 = aspectTrine[index, 3];
                xmldata.aspectTrine13 = aspectTrine[index, 4];
                xmldata.aspectTrine23 = aspectTrine[index, 5];
                xmldata.aspectSquare11 = aspectSquare[index, 0];
                xmldata.aspectSquare22 = aspectSquare[index, 1];
                xmldata.aspectSquare33 = aspectSquare[index, 2];
                xmldata.aspectSquare12 = aspectSquare[index, 3];
                xmldata.aspectSquare13 = aspectSquare[index, 4];
                xmldata.aspectSquare23 = aspectSquare[index, 5];
                xmldata.aspectSextile11 = aspectSextile[index, 0];
                xmldata.aspectSextile22 = aspectSextile[index, 1];
                xmldata.aspectSextile33 = aspectSextile[index, 2];
                xmldata.aspectSextile12 = aspectSextile[index, 3];
                xmldata.aspectSextile13 = aspectSextile[index, 4];
                xmldata.aspectSextile23 = aspectSextile[index, 5];

                xmldata.aspectInconjunct11 = aspectInconjunct[index, 0];
                xmldata.aspectInconjunct22 = aspectInconjunct[index, 1];
                xmldata.aspectInconjunct33 = aspectInconjunct[index, 2];
                xmldata.aspectInconjunct12 = aspectInconjunct[index, 3];
                xmldata.aspectInconjunct13 = aspectInconjunct[index, 4];
                xmldata.aspectInconjunct23 = aspectInconjunct[index, 5];
                xmldata.aspectSesquiquadrate11 = aspectSesquiquadrate[index, 0];
                xmldata.aspectSesquiquadrate22 = aspectSesquiquadrate[index, 1];
                xmldata.aspectSesquiquadrate33 = aspectSesquiquadrate[index, 2];
                xmldata.aspectSesquiquadrate12 = aspectSesquiquadrate[index, 3];
                xmldata.aspectSesquiquadrate13 = aspectSesquiquadrate[index, 4];
                xmldata.aspectSesquiquadrate23 = aspectSesquiquadrate[index, 5];

                xmldata.dispAspect = new bool[6];
                xmldata.dispAspect[0] = aspectDispChecked[index, 0];
                xmldata.dispAspect[1] = aspectDispChecked[index, 1];
                xmldata.dispAspect[2] = aspectDispChecked[index, 2];
                xmldata.dispAspect[3] = aspectDispChecked[index, 3];
                xmldata.dispAspect[4] = aspectDispChecked[index, 4];
                xmldata.dispAspect[5] = aspectDispChecked[index, 5];

                string filename = @"system\setting" + index + ".csm";
                XmlSerializer serializer = new XmlSerializer(typeof(SettingXml));
                FileStream fs = new FileStream(filename, FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                serializer.Serialize(sw, xmldata);
                sw.Close();
                fs.Close();

                main.settings[index].xmlData = xmldata;
            }

            int currentIndex = main.dispSettingBox.SelectedIndex;
            main.currentSetting.aspectConjunction[0, 0] = aspectConjunction[currentIndex, 0];
            main.currentSetting.aspectConjunction[1, 1] = aspectConjunction[currentIndex, 1];
            main.currentSetting.aspectConjunction[2, 2] = aspectConjunction[currentIndex, 2];
            main.currentSetting.aspectConjunction[0, 1] = aspectConjunction[currentIndex, 3];
            main.currentSetting.aspectConjunction[0, 2] = aspectConjunction[currentIndex, 4];
            main.currentSetting.aspectConjunction[1, 2] = aspectConjunction[currentIndex, 5];
            main.currentSetting.aspectOpposition[0, 0] = aspectOpposition[currentIndex, 0];
            main.currentSetting.aspectOpposition[1, 1] = aspectOpposition[currentIndex, 1];
            main.currentSetting.aspectOpposition[2, 2] = aspectOpposition[currentIndex, 2];
            main.currentSetting.aspectOpposition[0, 1] = aspectOpposition[currentIndex, 3];
            main.currentSetting.aspectOpposition[0, 2] = aspectOpposition[currentIndex, 4];
            main.currentSetting.aspectOpposition[1, 2] = aspectOpposition[currentIndex, 5];
            main.currentSetting.aspectTrine[0, 0] = aspectTrine[currentIndex, 0];
            main.currentSetting.aspectTrine[1, 1] = aspectTrine[currentIndex, 1];
            main.currentSetting.aspectTrine[2, 2] = aspectTrine[currentIndex, 2];
            main.currentSetting.aspectTrine[0, 1] = aspectTrine[currentIndex, 3];
            main.currentSetting.aspectTrine[0, 2] = aspectTrine[currentIndex, 4];
            main.currentSetting.aspectTrine[1, 2] = aspectTrine[currentIndex, 5];
            main.currentSetting.aspectSquare[0, 0] = aspectSquare[currentIndex, 0];
            main.currentSetting.aspectSquare[1, 1] = aspectSquare[currentIndex, 1];
            main.currentSetting.aspectSquare[2, 2] = aspectSquare[currentIndex, 2];
            main.currentSetting.aspectSquare[0, 1] = aspectSquare[currentIndex, 3];
            main.currentSetting.aspectSquare[0, 2] = aspectSquare[currentIndex, 4];
            main.currentSetting.aspectSquare[1, 2] = aspectSquare[currentIndex, 5];
            main.currentSetting.aspectSextile[0, 0] = aspectSextile[currentIndex, 0];
            main.currentSetting.aspectSextile[1, 1] = aspectSextile[currentIndex, 1];
            main.currentSetting.aspectSextile[2, 2] = aspectSextile[currentIndex, 2];
            main.currentSetting.aspectSextile[0, 1] = aspectSextile[currentIndex, 3];
            main.currentSetting.aspectSextile[0, 2] = aspectSextile[currentIndex, 4];
            main.currentSetting.aspectSextile[1, 2] = aspectSextile[currentIndex, 5];
            main.currentSetting.aspectInconjunct[0, 0] = aspectInconjunct[currentIndex, 0];
            main.currentSetting.aspectInconjunct[1, 1] = aspectInconjunct[currentIndex, 1];
            main.currentSetting.aspectInconjunct[2, 2] = aspectInconjunct[currentIndex, 2];
            main.currentSetting.aspectInconjunct[0, 1] = aspectInconjunct[currentIndex, 3];
            main.currentSetting.aspectInconjunct[0, 2] = aspectInconjunct[currentIndex, 4];
            main.currentSetting.aspectInconjunct[1, 2] = aspectInconjunct[currentIndex, 5];
            main.currentSetting.aspectSesquiquadrate[0, 0] = aspectSesquiquadrate[currentIndex, 0];
            main.currentSetting.aspectSesquiquadrate[1, 1] = aspectSesquiquadrate[currentIndex, 1];
            main.currentSetting.aspectSesquiquadrate[2, 2] = aspectSesquiquadrate[currentIndex, 2];
            main.currentSetting.aspectSesquiquadrate[0, 1] = aspectSesquiquadrate[currentIndex, 3];
            main.currentSetting.aspectSesquiquadrate[0, 2] = aspectSesquiquadrate[currentIndex, 4];
            main.currentSetting.aspectSesquiquadrate[1, 2] = aspectSesquiquadrate[currentIndex, 5];

            main.currentSetting.dispAspect[0, 0] = aspectDispChecked[currentIndex, 0];
            main.currentSetting.dispAspect[1, 1] = aspectDispChecked[currentIndex, 1];
            main.currentSetting.dispAspect[2, 2] = aspectDispChecked[currentIndex, 2];
            main.currentSetting.dispAspect[0, 1] = aspectDispChecked[currentIndex, 3];
            main.currentSetting.dispAspect[0, 2] = aspectDispChecked[currentIndex, 4];
            main.currentSetting.dispAspect[1, 2] = aspectDispChecked[currentIndex, 5];


            main.ReCalc();
            main.ReRender();

            this.Visibility = Visibility.Hidden;
        }

        private void aspect2MouseDownCommon(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;

            Image ctl = (Image)aspectControlTable[img.Name].aspectAnotherElement;
            ctl.Visibility = Visibility.Visible;
            ctl.Height = 24;

            int index = dispList.SelectedIndex;
            int subIndex = aspectControlTable[img.Name].subIndex;

            aspectControlTable[img.Name].tempArray[index, subIndex] = aspectControlTable[img.Name].targetBoolean;

            main.ReRender();
        }

        private void disp11_Click(object sender, RoutedEventArgs e)
        {
            int index = dispList.SelectedIndex;
            if (disp11.IsChecked == true)
            {
                aspectDispChecked[index, 0] = true;
            }
            else
            {
                aspectDispChecked[index, 0] = false;
            }
        }

        private void disp22_Click(object sender, RoutedEventArgs e)
        {
            int index = dispList.SelectedIndex;
            if (disp11.IsChecked == true)
            {
                aspectDispChecked[index, 1] = true;
            }
            else
            {
                aspectDispChecked[index, 1] = false;
            }
        }

        private void disp33_Click(object sender, RoutedEventArgs e)
        {
            int index = dispList.SelectedIndex;
            if (disp11.IsChecked == true)
            {
                aspectDispChecked[index, 2] = true;
            }
            else
            {
                aspectDispChecked[index, 2] = false;
            }
        }

        private void disp12_Click(object sender, RoutedEventArgs e)
        {
            int index = dispList.SelectedIndex;
            if (disp11.IsChecked == true)
            {
                aspectDispChecked[index, 3] = true;
            }
            else
            {
                aspectDispChecked[index, 3] = false;
            }
        }

        private void disp13_Click(object sender, RoutedEventArgs e)
        {
            int index = dispList.SelectedIndex;
            if (disp11.IsChecked == true)
            {
                aspectDispChecked[index, 4] = true;
            }
            else
            {
                aspectDispChecked[index, 4] = false;
            }
        }

        private void disp23_Click(object sender, RoutedEventArgs e)
        {
            int index = dispList.SelectedIndex;
            if (disp11.IsChecked == true)
            {
                aspectDispChecked[index, 5] = true;
            }
            else
            {
                aspectDispChecked[index, 5] = false;
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }
    }
}
