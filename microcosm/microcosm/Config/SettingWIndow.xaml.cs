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
        public bool[,] planetDispSun = new bool[10, 15];
        public bool[,] planetDispMoon = new bool[10, 15];
        public bool[,] planetDispMercury = new bool[10, 15];
        public bool[,] planetDispVenus = new bool[10, 15];
        public bool[,] planetDispMars = new bool[10, 15];
        public bool[,] planetDispJupiter = new bool[10, 15];
        public bool[,] planetDispSaturn = new bool[10, 15];
        public bool[,] planetDispUranus = new bool[10, 15];
        public bool[,] planetDispNeptune = new bool[10, 15];
        public bool[,] planetDispPluto = new bool[10, 15];
        public bool[,] planetDispDh = new bool[10, 15];
        public bool[,] planetDispChiron = new bool[10, 15];
        public bool[,] planetDispAsc = new bool[10, 15];
        public bool[,] planetDispMc = new bool[10, 15];
        public bool[,] planetDispEarth = new bool[10, 15];
        public bool[,] planetDispLilith = new bool[10, 15];

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
        public bool[,] aspectEarth = new bool[10, 15];
        public bool[,] aspectLilith = new bool[10, 15];

        public bool[,] aspectConjunction = new bool[10, 15];
        public bool[,] aspectOpposition = new bool[10, 15];
        public bool[,] aspectTrine = new bool[10, 15];
        public bool[,] aspectSquare = new bool[10, 15];
        public bool[,] aspectSextile = new bool[10, 15];
        public bool[,] aspectInconjunct = new bool[10, 15];
        public bool[,] aspectSesquiquadrate = new bool[10, 15];

        public bool[,] aspectDispChecked = new bool[10, 15];

        public double[,] orbSunHard1st = new double[10, 15];
        public double[,] orbSunHard2nd = new double[10, 15];
        public double[,] orbSunHard150 = new double[10, 15];
        public double[,] orbSunSoft1st = new double[10, 15];
        public double[,] orbSunSoft2nd = new double[10, 15];
        public double[,] orbSunSoft150 = new double[10, 15];
        public double[,] orbMoonHard1st = new double[10, 15];
        public double[,] orbMoonHard2nd = new double[10, 15];
        public double[,] orbMoonHard150 = new double[10, 15];
        public double[,] orbMoonSoft1st = new double[10, 15];
        public double[,] orbMoonSoft2nd = new double[10, 15];
        public double[,] orbMoonSoft150 = new double[10, 15];
        public double[,] orbOtherHard1st = new double[10, 15];
        public double[,] orbOtherHard2nd = new double[10, 15];
        public double[,] orbOtherHard150 = new double[10, 15];
        public double[,] orbOtherSoft1st = new double[10, 15];
        public double[,] orbOtherSoft2nd = new double[10, 15];
        public double[,] orbOtherSoft150 = new double[10, 15];

        public string[] tempDispName = new string[10];

        // アスペクト表示（１）
        public Dictionary<string, AspectControlTable> aspectControlTable = new Dictionary<string, AspectControlTable>();
        public List<string> aspectTargetNames = new List<string>();

        // アスペクト表示（２）
        public Dictionary<string, AspectControlTable> controlTable = new Dictionary<string, AspectControlTable>();
        public List<string> targetNames = new List<string>();

        // 天体表示
        public Dictionary<string, AspectControlTable> planetDispControlTable = new Dictionary<string, AspectControlTable>();
        public List<string> planetTargetNames = new List<string>();

        string[] strNumbers = { "11", "22", "33", "12", "13", "23" };
        string[] strNumbers2 = { "11", "22", "33" };


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
                targetNames.Add("aspectEarthOn" + s);
                targetNames.Add("aspectLilithOn" + s);

                aspectTargetNames.Add("aspectConjunctionOn" + s);
                aspectTargetNames.Add("aspectOppositionOn" + s);
                aspectTargetNames.Add("aspectTrineOn" + s);
                aspectTargetNames.Add("aspectSquareOn" + s);
                aspectTargetNames.Add("aspectSextileOn" + s);
                aspectTargetNames.Add("aspectInconjunctOn" + s);
                aspectTargetNames.Add("aspectSesquiquadrateOn" + s);

            }
            foreach (var s in strNumbers2)
            {
                planetTargetNames.Add("planetSunOn" + s);
                planetTargetNames.Add("planetMoonOn" + s);
                planetTargetNames.Add("planetMercuryOn" + s);
                planetTargetNames.Add("planetVenusOn" + s);
                planetTargetNames.Add("planetMarsOn" + s);
                planetTargetNames.Add("planetJupiterOn" + s);
                planetTargetNames.Add("planetSaturnOn" + s);
                planetTargetNames.Add("planetUranusOn" + s);
                planetTargetNames.Add("planetNeptuneOn" + s);
                planetTargetNames.Add("planetPlutoOn" + s);
                planetTargetNames.Add("planetDhOn" + s);
                planetTargetNames.Add("planetChironOn" + s);
                planetTargetNames.Add("planetAscOn" + s);
                planetTargetNames.Add("planetMcOn" + s);
                planetTargetNames.Add("planetEarthOn" + s);
                planetTargetNames.Add("planetLilithOn" + s);
            }
            createControlTable();

            dispList.ItemsSource = new ObservableCollection<SettingData>(main.settings);
            dispList.SelectedIndex = 0;
            settingVM = new SettingWindowViewModel(main);
            leftPane.DataContext = settingVM;

            settingVM.dispName = main.currentSetting.dispName;
            for (int i = 0; i < 10; i++)
            {
                tempDispName[i] = main.settings[i].dispName;
            }
            setAspect();

            // 天体表示初期化
            for (int i = 0; i < planetTargetNames.Count; i++)
            {
                int subIndex = planetDispControlTable[planetTargetNames[i]].subIndex;
                for (int j = 0; j < 10; j++)
                {
                    // commonNo: ipl
                    // tempArray: 設定切り替え時に一時的に保存しておく場所
                    int commonDataNo = planetDispControlTable[planetTargetNames[i]].commonDataNo;
                    planetDispControlTable[planetTargetNames[i]].tempArray[j, subIndex] = main.settings[j].dispPlanet[subIndex][commonDataNo];
                }
            }
            // アスペクト（１）初期化
            for (int i = 0; i < aspectTargetNames.Count; i++)
            {
                int subIndex = aspectControlTable[aspectTargetNames[i]].subIndex;
                for (int j = 0; j < 10; j++)
                {
                    AspectKind aspectKindNo = aspectControlTable[aspectTargetNames[i]].aspectKindNo;
                    aspectControlTable[aspectTargetNames[i]].tempArray[j, subIndex] = main.settings[j].dispAspectCategory[subIndex][aspectKindNo];
                }
            }

            // アスペクト（２）初期化
            for (int i = 0; i < targetNames.Count; i++)
            {
                int subIndex = controlTable[targetNames[i]].subIndex;
                for (int j = 0; j < 10; j++)
                {
                    // commonNo: ipl
                    // tempArray: 設定切り替え時に一時的に保存しておく場所
                    int commonDataNo = controlTable[targetNames[i]].commonDataNo;
                    controlTable[targetNames[i]].tempArray[j, subIndex] = main.settings[j].dispAspectPlanet[subIndex][commonDataNo];
                }
            }

            setOrb();
            ReRender(dispList);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Hidden;
        }

        private void dispList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox list = (ListBox)sender;
            ReRender(list);
        }

        private void ReRender(ListBox list)
        {
            settingName.Text = main.settings[list.SelectedIndex].dispName;

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
                if (controlTable[targetNames[i]].tempArray[list.SelectedIndex, subIndex])
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
                if (aspectControlTable[aspectTargetNames[i]].tempArray[list.SelectedIndex, subIndex])
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

            // 天体表示設定
            for (int i = 0; i < planetTargetNames.Count; i++)
            {
                int subIndex = planetDispControlTable[planetTargetNames[i]].subIndex;
                if (planetDispControlTable[planetTargetNames[i]].tempArray[list.SelectedIndex, subIndex])
                {
                    planetDispControlTable[planetTargetNames[i]].selfElement.Visibility = Visibility.Visible;
                    planetDispControlTable[planetTargetNames[i]].selfElement.Height = 24;
                    planetDispControlTable[planetTargetNames[i]].anotherElement.Visibility = Visibility.Hidden;
                    planetDispControlTable[planetTargetNames[i]].anotherElement.Height = 0;
                }
                else
                {
                    planetDispControlTable[planetTargetNames[i]].selfElement.Visibility = Visibility.Hidden;
                    planetDispControlTable[planetTargetNames[i]].selfElement.Height = 0;
                    planetDispControlTable[planetTargetNames[i]].anotherElement.Visibility = Visibility.Visible;
                    planetDispControlTable[planetTargetNames[i]].anotherElement.Height = 24;
                }
            }

            // アスペクト表示（１）のチェック
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

            reOrb();
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
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    orbSunSoft1st[i, j] = main.settings[i].orbs[j][OrbKind.SUN_SOFT_1ST];
                    orbSunHard1st[i, j] = main.settings[i].orbs[j][OrbKind.SUN_HARD_1ST];
                    orbSunSoft2nd[i, j] = main.settings[i].orbs[j][OrbKind.SUN_SOFT_2ND];
                    orbSunHard2nd[i, j] = main.settings[i].orbs[j][OrbKind.SUN_HARD_2ND];
                    orbSunSoft150[i, j] = main.settings[i].orbs[j][OrbKind.SUN_SOFT_150];
                    orbSunHard150[i, j] = main.settings[i].orbs[j][OrbKind.SUN_HARD_150];
                    orbMoonSoft1st[i, j] = main.settings[i].orbs[j][OrbKind.MOON_SOFT_1ST];
                    orbMoonHard1st[i, j] = main.settings[i].orbs[j][OrbKind.MOON_HARD_1ST];
                    orbMoonSoft2nd[i, j] = main.settings[i].orbs[j][OrbKind.MOON_SOFT_2ND];
                    orbMoonHard2nd[i, j] = main.settings[i].orbs[j][OrbKind.MOON_HARD_2ND];
                    orbMoonSoft150[i, j] = main.settings[i].orbs[j][OrbKind.MOON_SOFT_150];
                    orbMoonHard150[i, j] = main.settings[i].orbs[j][OrbKind.MOON_HARD_150];
                    orbOtherSoft1st[i, j] = main.settings[i].orbs[j][OrbKind.OTHER_SOFT_1ST];
                    orbOtherHard1st[i, j] = main.settings[i].orbs[j][OrbKind.OTHER_HARD_1ST];
                    orbOtherSoft2nd[i, j] = main.settings[i].orbs[j][OrbKind.OTHER_SOFT_2ND];
                    orbOtherHard2nd[i, j] = main.settings[i].orbs[j][OrbKind.OTHER_HARD_2ND];
                    orbOtherSoft150[i, j] = main.settings[i].orbs[j][OrbKind.OTHER_SOFT_150];
                    orbOtherHard150[i, j] = main.settings[i].orbs[j][OrbKind.OTHER_HARD_150];
                }
            }

            reOrb();
        }

        private void reOrb()
        {
            if (sunSoft1st == null)
            {
                return;
            }

            int index = dispList.SelectedIndex;
            int orbIndex = orbRing.SelectedIndex;

            sunSoft1st.Text = orbSunSoft1st[index, orbIndex].ToString();
            sunHard1st.Text = orbSunHard1st[index, orbIndex].ToString();
            sunSoft2nd.Text = orbSunSoft2nd[index, orbIndex].ToString();
            sunHard2nd.Text = orbSunHard2nd[index, orbIndex].ToString();
            sunSoft150.Text = orbSunSoft150[index, orbIndex].ToString();
            sunHard150.Text = orbSunHard150[index, orbIndex].ToString();
            moonSoft1st.Text = orbMoonSoft1st[index, orbIndex].ToString();
            moonHard1st.Text = orbMoonSoft1st[index, orbIndex].ToString();
            moonSoft2nd.Text = orbMoonSoft2nd[index, orbIndex].ToString();
            moonHard2nd.Text = orbMoonHard2nd[index, orbIndex].ToString();
            moonSoft150.Text = orbMoonSoft150[index, orbIndex].ToString();
            moonHard150.Text = orbMoonHard150[index, orbIndex].ToString();
            otherSoft1st.Text = orbOtherSoft1st[index, orbIndex].ToString();
            otherHard1st.Text = orbOtherHard1st[index, orbIndex].ToString();
            otherSoft2nd.Text = orbOtherSoft2nd[index, orbIndex].ToString();
            otherHard2nd.Text = orbOtherHard2nd[index, orbIndex].ToString();
            otherSoft150.Text = orbOtherSoft150[index, orbIndex].ToString();
            otherHard150.Text = orbOtherHard150[index, orbIndex].ToString();
        }

        private void orbRing_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sunSoft1st != null)
            {
                reOrb();
            }
        }

        // アスペクト（２）のマウスダウン
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

            controlTable[img.Name].tempArray[index, subindex] = controlTable[img.Name].targetBoolean;

            /* リアルタイムはややこしくなるから一度コメントアウト
            if (subindex == 0)
            {
                for (int i = 0; i < main.list1.Count(); i++)
                {
                    if (main.list1[i].no == controlTable[img.Name].commonDataNo)
                    {
                        if (index == main.dispSettingBox.SelectedIndex)
                        {
                            main.list1[i].isAspectDisp = controlTable[img.Name].targetBoolean;
                        }
                        break;
                    }

                }
            }
            else if (subindex == 1)
            {
                for (int i = 0; i < main.list1.Count(); i++)
                {
                    if (main.list2[i].no == controlTable[img.Name].commonDataNo)
                    {
                        if (index == main.dispSettingBox.SelectedIndex)
                        {
                            main.list2[i].isAspectDisp = controlTable[img.Name].targetBoolean;
                        }
                        break;
                    }

                }
            }
            else if (subindex == 2)
            {
                for (int i = 0; i < main.list1.Count(); i++)
                {
                    if (main.list3[i].no == controlTable[img.Name].commonDataNo)
                    {
                        if (index == main.dispSettingBox.SelectedIndex)
                        {
                            main.list3[i].isAspectDisp = controlTable[img.Name].targetBoolean;
                        }
                        break;
                    }

                }
            }
            */
            main.ReRender();
        }

        // 天体表示のマウスクリック
        private void planetDispMouseDownCommon(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            img.Visibility = Visibility.Hidden;
            img.Height = 0;

            Image ctl = (Image)(planetDispControlTable[img.Name].anotherElement);
            ctl.Visibility = Visibility.Visible;
            ctl.Height = 24;

            int index = dispList.SelectedIndex;
            int subindex = planetDispControlTable[img.Name].subIndex;

            planetDispControlTable[img.Name].tempArray[index, subindex] = planetDispControlTable[img.Name].targetBoolean;


            /* リアルタイムはややこしくなるから一度コメントアウト
            switch (subindex)
            {
                case 0:
                    // natalの場合
                    for (int i = 0; i < main.list1.Count(); i++)
                    {
                        if (main.list1[i].no == planetDispControlTable[img.Name].commonDataNo)
                        {
                            if (index == main.dispSettingBox.SelectedIndex)
                            {
                                main.list1[i].isDisp = planetDispControlTable[img.Name].targetBoolean;
                            }
                            break;
                        }
                    }
                    break;
                case 1:
                    // progressの場合
                    for (int i = 0; i < main.list2.Count(); i++)
                    {
                        if (main.list2[i].no == planetDispControlTable[img.Name].commonDataNo)
                        {
                            if (index == main.dispSettingBox.SelectedIndex)
                            {
                                main.list2[i].isDisp = planetDispControlTable[img.Name].targetBoolean;
                            }
                            break;
                        }
                    }
                    break;
                case 2:
                    // transitの場合
                    for (int i = 0; i < main.list3.Count(); i++)
                    {
                        if (main.list3[i].no == planetDispControlTable[img.Name].commonDataNo)
                        {
                            if (index == main.dispSettingBox.SelectedIndex)
                            {
                                main.list3[i].isDisp = planetDispControlTable[img.Name].targetBoolean;
                            }
                            break;
                        }
                    }
                    break;
            }
            */

            main.ReRender();
        }

        // アスペクト（１）のマウスダウン
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
                    targetBoolean = false,
                    subIndex = subIndexNo,
                    commonDataNo = (int)CommonData.ZODIAC_SUN
                });
                controlTable.Add("aspectMoonOn" + n, new AspectControlTable()
                {
                    selfElement = (FrameworkElement)FindName("aspectMoonOn" + n),
                    anotherElement = (FrameworkElement)FindName("aspectMoonOff" + n),
                    tempArray = aspectMoon,
                    targetBoolean = false,
                    subIndex = subIndexNo,
                    commonDataNo = (int)CommonData.ZODIAC_MOON
                });
                controlTable.Add("aspectMercuryOn" + n, new AspectControlTable()
                {
                    selfElement = (FrameworkElement)FindName("aspectMercuryOn" + n),
                    anotherElement = (FrameworkElement)FindName("aspectMercuryOff" + n),
                    tempArray = aspectMercury,
                    targetBoolean = false,
                    subIndex = subIndexNo,
                    commonDataNo = (int)CommonData.ZODIAC_MERCURY
                });
                controlTable.Add("aspectVenusOn" + n, new AspectControlTable()
                {
                    selfElement = (FrameworkElement)FindName("aspectVenusOn" + n),
                    anotherElement = (FrameworkElement)FindName("aspectVenusOff" + n),
                    tempArray = aspectVenus,
                    targetBoolean = false,
                    subIndex = subIndexNo,
                    commonDataNo = (int)CommonData.ZODIAC_VENUS
                });
                controlTable.Add("aspectMarsOn" + n, new AspectControlTable()
                {
                    selfElement = (FrameworkElement)FindName("aspectMarsOn" + n),
                    anotherElement = (FrameworkElement)FindName("aspectMarsOff" + n),
                    tempArray = aspectMars,
                    targetBoolean = false,
                    subIndex = subIndexNo,
                    commonDataNo = (int)CommonData.ZODIAC_MARS
                });
                controlTable.Add("aspectJupiterOn" + n, new AspectControlTable()
                {
                    selfElement = (FrameworkElement)FindName("aspectJupiterOn" + n),
                    anotherElement = (FrameworkElement)FindName("aspectJupiterOff" + n),
                    tempArray = aspectJupiter,
                    targetBoolean = false,
                    subIndex = subIndexNo,
                    commonDataNo = (int)CommonData.ZODIAC_JUPITER
                });
                controlTable.Add("aspectSaturnOn" + n, new AspectControlTable()
                {
                    selfElement = (FrameworkElement)FindName("aspectSaturnOn" + n),
                    anotherElement = (FrameworkElement)FindName("aspectSaturnOff" + n),
                    tempArray = aspectSaturn,
                    targetBoolean = false,
                    subIndex = subIndexNo,
                    commonDataNo = (int)CommonData.ZODIAC_SATURN
                });
                controlTable.Add("aspectUranusOn" + n, new AspectControlTable()
                {
                    selfElement = (FrameworkElement)FindName("aspectUranusOn" + n),
                    anotherElement = (FrameworkElement)FindName("aspectUranusOff" + n),
                    tempArray = aspectUranus,
                    targetBoolean = false,
                    subIndex = subIndexNo,
                    commonDataNo = (int)CommonData.ZODIAC_URANUS
                });
                controlTable.Add("aspectNeptuneOn" + n, new AspectControlTable()
                {
                    selfElement = (FrameworkElement)FindName("aspectNeptuneOn" + n),
                    anotherElement = (FrameworkElement)FindName("aspectNeptuneOff" + n),
                    tempArray = aspectNeptune,
                    targetBoolean = false,
                    subIndex = subIndexNo,
                    commonDataNo = (int)CommonData.ZODIAC_NEPTUNE
                });
                controlTable.Add("aspectPlutoOn" + n, new AspectControlTable()
                {
                    selfElement = (FrameworkElement)FindName("aspectPlutoOn" + n),
                    anotherElement = (FrameworkElement)FindName("aspectPlutoOff" + n),
                    tempArray = aspectPluto,
                    targetBoolean = false,
                    subIndex = subIndexNo,
                    commonDataNo = (int)CommonData.ZODIAC_PLUTO
                });
                controlTable.Add("aspectDhOn" + n, new AspectControlTable()
                {
                    selfElement = (FrameworkElement)FindName("aspectDhOn" + n),
                    anotherElement = (FrameworkElement)FindName("aspectDhOff" + n),
                    tempArray = aspectDh,
                    targetBoolean = false,
                    subIndex = subIndexNo,
                    commonDataNo = (int)CommonData.ZODIAC_DH_TRUENODE
                });
                controlTable.Add("aspectChironOn" + n, new AspectControlTable()
                {
                    selfElement = (FrameworkElement)FindName("aspectChironOn" + n),
                    anotherElement = (FrameworkElement)FindName("aspectChironOff" + n),
                    tempArray = aspectChiron,
                    targetBoolean = false,
                    subIndex = subIndexNo,
                    commonDataNo = (int)CommonData.ZODIAC_CHIRON
                });
                controlTable.Add("aspectAscOn" + n, new AspectControlTable()
                {
                    selfElement = (FrameworkElement)FindName("aspectAscOn" + n),
                    anotherElement = (FrameworkElement)FindName("aspectAscOff" + n),
                    tempArray = aspectAsc,
                    targetBoolean = false,
                    subIndex = subIndexNo,
                    commonDataNo = (int)CommonData.ZODIAC_ASC
                });
                controlTable.Add("aspectMcOn" + n, new AspectControlTable()
                {
                    selfElement = (FrameworkElement)FindName("aspectMcOn" + n),
                    anotherElement = (FrameworkElement)FindName("aspectMcOff" + n),
                    tempArray = aspectMc,
                    targetBoolean = false,
                    subIndex = subIndexNo,
                    commonDataNo = (int)CommonData.ZODIAC_MC
                });
                controlTable.Add("aspectEarthOn" + n, new AspectControlTable()
                {
                    selfElement = (FrameworkElement)FindName("aspectEarthOn" + n),
                    anotherElement = (FrameworkElement)FindName("aspectEarthOff" + n),
                    tempArray = aspectEarth,
                    targetBoolean = false,
                    subIndex = subIndexNo,
                    commonDataNo = (int)CommonData.ZODIAC_EARTH
                });
                controlTable.Add("aspectLilithOn" + n, new AspectControlTable()
                {
                    selfElement = (FrameworkElement)FindName("aspectLilithOn" + n),
                    anotherElement = (FrameworkElement)FindName("aspectLilithOff" + n),
                    tempArray = aspectLilith,
                    targetBoolean = false,
                    subIndex = subIndexNo,
                    commonDataNo = (int)CommonData.ZODIAC_LILITH
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
                controlTable.Add("aspectEarthOff" + n, new AspectControlTable()
                {
                    selfElement = (FrameworkElement)FindName("aspectEarthOff" + n),
                    anotherElement = (FrameworkElement)FindName("aspectEarthOn" + n),
                    tempArray = aspectEarth,
                    targetBoolean = true,
                    subIndex = subIndexNo,
                    commonDataNo = (int)CommonData.ZODIAC_EARTH
                });
                controlTable.Add("aspectLilithOff" + n, new AspectControlTable()
                {
                    selfElement = (FrameworkElement)FindName("aspectLilithOff" + n),
                    anotherElement = (FrameworkElement)FindName("aspectLilithOn" + n),
                    tempArray = aspectLilith,
                    targetBoolean = true,
                    subIndex = subIndexNo,
                    commonDataNo = (int)CommonData.ZODIAC_LILITH
                });


                aspectControlTable.Add("aspectConjunctionOn" + n, new AspectControlTable()
                {
                    aspectSelfElement = (FrameworkElement)FindName("aspectConjunctionOn" + n),
                    aspectAnotherElement = (FrameworkElement)FindName("aspectConjunctionOff" + n),
                    tempArray = aspectConjunction,
                    targetBoolean = false,
                    subIndex = subIndexNo,
                    aspectKindNo = AspectKind.CONJUNCTION
                });
                aspectControlTable.Add("aspectOppositionOn" + n, new AspectControlTable()
                {
                    aspectSelfElement = (FrameworkElement)FindName("aspectOppositionOn" + n),
                    aspectAnotherElement = (FrameworkElement)FindName("aspectOppositionOff" + n),
                    tempArray = aspectOpposition,
                    targetBoolean = false,
                    subIndex = subIndexNo,
                    aspectKindNo = AspectKind.OPPOSITION
                });
                aspectControlTable.Add("aspectSquareOn" + n, new AspectControlTable()
                {
                    aspectSelfElement = (FrameworkElement)FindName("aspectSquareOn" + n),
                    aspectAnotherElement = (FrameworkElement)FindName("aspectSquareOff" + n),
                    tempArray = aspectSquare,
                    targetBoolean = false,
                    subIndex = subIndexNo,
                    aspectKindNo = AspectKind.SQUARE
                });
                aspectControlTable.Add("aspectTrineOn" + n, new AspectControlTable()
                {
                    aspectSelfElement = (FrameworkElement)FindName("aspectTrineOn" + n),
                    aspectAnotherElement = (FrameworkElement)FindName("aspectTrineOff" + n),
                    tempArray = aspectTrine,
                    targetBoolean = false,
                    subIndex = subIndexNo,
                    aspectKindNo = AspectKind.TRINE
                });
                aspectControlTable.Add("aspectSextileOn" + n, new AspectControlTable()
                {
                    aspectSelfElement = (FrameworkElement)FindName("aspectSextileOn" + n),
                    aspectAnotherElement = (FrameworkElement)FindName("aspectSextileOff" + n),
                    tempArray = aspectSextile,
                    targetBoolean = false,
                    subIndex = subIndexNo,
                    aspectKindNo = AspectKind.SEXTILE
                });
                aspectControlTable.Add("aspectInconjunctOn" + n, new AspectControlTable()
                {
                    aspectSelfElement = (FrameworkElement)FindName("aspectInconjunctOn" + n),
                    aspectAnotherElement = (FrameworkElement)FindName("aspectInconjunctOff" + n),
                    tempArray = aspectInconjunct,
                    targetBoolean = false,
                    subIndex = subIndexNo,
                    aspectKindNo = AspectKind.INCONJUNCT
                });
                aspectControlTable.Add("aspectSesquiquadrateOn" + n, new AspectControlTable()
                {
                    aspectSelfElement = (FrameworkElement)FindName("aspectSesquiquadrateOn" + n),
                    aspectAnotherElement = (FrameworkElement)FindName("aspectSesquiquadrateOff" + n),
                    tempArray = aspectSesquiquadrate,
                    targetBoolean = false,
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
            foreach (string n in strNumbers2)
            {
                if (n == "11") subIndexNo = 0;
                else if (n == "22") subIndexNo = 1;
                else if (n == "33") subIndexNo = 2;

                planetDispControlTable.Add("planetSunOn" + n, new AspectControlTable()
                {
                    selfElement = (FrameworkElement)FindName("planetSunOn" + n),
                    anotherElement = (FrameworkElement)FindName("planetSunOff" + n),
                    tempArray = planetDispSun,
                    targetBoolean = false,
                    subIndex = subIndexNo,
                    commonDataNo = (int)CommonData.ZODIAC_SUN
                });
                planetDispControlTable.Add("planetMoonOn" + n, new AspectControlTable()
                {
                    selfElement = (FrameworkElement)FindName("planetMoonOn" + n),
                    anotherElement = (FrameworkElement)FindName("planetMoonOff" + n),
                    tempArray = planetDispMoon,
                    targetBoolean = false,
                    subIndex = subIndexNo,
                    commonDataNo = (int)CommonData.ZODIAC_MOON
                });
                planetDispControlTable.Add("planetMercuryOn" + n, new AspectControlTable()
                {
                    selfElement = (FrameworkElement)FindName("planetMercuryOn" + n),
                    anotherElement = (FrameworkElement)FindName("planetMercuryOff" + n),
                    tempArray = planetDispMercury,
                    targetBoolean = false,
                    subIndex = subIndexNo,
                    commonDataNo = (int)CommonData.ZODIAC_MERCURY
                });
                planetDispControlTable.Add("planetVenusOn" + n, new AspectControlTable()
                {
                    selfElement = (FrameworkElement)FindName("planetVenusOn" + n),
                    anotherElement = (FrameworkElement)FindName("planetVenusOff" + n),
                    tempArray = planetDispVenus,
                    targetBoolean = false,
                    subIndex = subIndexNo,
                    commonDataNo = (int)CommonData.ZODIAC_VENUS
                });
                planetDispControlTable.Add("planetMarsOn" + n, new AspectControlTable()
                {
                    selfElement = (FrameworkElement)FindName("planetMarsOn" + n),
                    anotherElement = (FrameworkElement)FindName("planetMarsOff" + n),
                    tempArray = planetDispMars,
                    targetBoolean = false,
                    subIndex = subIndexNo,
                    commonDataNo = (int)CommonData.ZODIAC_MARS
                });
                planetDispControlTable.Add("planetJupiterOn" + n, new AspectControlTable()
                {
                    selfElement = (FrameworkElement)FindName("planetJupiterOn" + n),
                    anotherElement = (FrameworkElement)FindName("planetJupiterOff" + n),
                    tempArray = planetDispJupiter,
                    targetBoolean = false,
                    subIndex = subIndexNo,
                    commonDataNo = (int)CommonData.ZODIAC_JUPITER
                });
                planetDispControlTable.Add("planetSaturnOn" + n, new AspectControlTable()
                {
                    selfElement = (FrameworkElement)FindName("planetSaturnOn" + n),
                    anotherElement = (FrameworkElement)FindName("planetSaturnOff" + n),
                    tempArray = planetDispSaturn,
                    targetBoolean = false,
                    subIndex = subIndexNo,
                    commonDataNo = (int)CommonData.ZODIAC_SATURN
                });
                planetDispControlTable.Add("planetUranusOn" + n, new AspectControlTable()
                {
                    selfElement = (FrameworkElement)FindName("planetUranusOn" + n),
                    anotherElement = (FrameworkElement)FindName("planetUranusOff" + n),
                    tempArray = planetDispUranus,
                    targetBoolean = false,
                    subIndex = subIndexNo,
                    commonDataNo = (int)CommonData.ZODIAC_URANUS
                });
                planetDispControlTable.Add("planetNeptuneOn" + n, new AspectControlTable()
                {
                    selfElement = (FrameworkElement)FindName("planetNeptuneOn" + n),
                    anotherElement = (FrameworkElement)FindName("planetNeptuneOff" + n),
                    tempArray = planetDispNeptune,
                    targetBoolean = false,
                    subIndex = subIndexNo,
                    commonDataNo = (int)CommonData.ZODIAC_NEPTUNE
                });
                planetDispControlTable.Add("planetPlutoOn" + n, new AspectControlTable()
                {
                    selfElement = (FrameworkElement)FindName("planetPlutoOn" + n),
                    anotherElement = (FrameworkElement)FindName("planetPlutoOff" + n),
                    tempArray = planetDispPluto,
                    targetBoolean = false,
                    subIndex = subIndexNo,
                    commonDataNo = (int)CommonData.ZODIAC_PLUTO
                });
                planetDispControlTable.Add("planetDhOn" + n, new AspectControlTable()
                {
                    selfElement = (FrameworkElement)FindName("planetDhOn" + n),
                    anotherElement = (FrameworkElement)FindName("planetDhOff" + n),
                    tempArray = planetDispDh,
                    targetBoolean = false,
                    subIndex = subIndexNo,
                    commonDataNo = (int)CommonData.ZODIAC_DH_TRUENODE
                });
                planetDispControlTable.Add("planetChironOn" + n, new AspectControlTable()
                {
                    selfElement = (FrameworkElement)FindName("planetChironOn" + n),
                    anotherElement = (FrameworkElement)FindName("planetChironOff" + n),
                    tempArray = planetDispChiron,
                    targetBoolean = false,
                    subIndex = subIndexNo,
                    commonDataNo = (int)CommonData.ZODIAC_CHIRON
                });
                planetDispControlTable.Add("planetAscOn" + n, new AspectControlTable()
                {
                    selfElement = (FrameworkElement)FindName("planetAscOn" + n),
                    anotherElement = (FrameworkElement)FindName("planetAscOff" + n),
                    tempArray = planetDispAsc,
                    targetBoolean = false,
                    subIndex = subIndexNo,
                    commonDataNo = (int)CommonData.ZODIAC_ASC
                });
                planetDispControlTable.Add("planetMcOn" + n, new AspectControlTable()
                {
                    selfElement = (FrameworkElement)FindName("planetMcOn" + n),
                    anotherElement = (FrameworkElement)FindName("planetMcOff" + n),
                    tempArray = planetDispMc,
                    targetBoolean = false,
                    subIndex = subIndexNo,
                    commonDataNo = (int)CommonData.ZODIAC_MC
                });
                planetDispControlTable.Add("planetEarthOn" + n, new AspectControlTable()
                {
                    selfElement = (FrameworkElement)FindName("planetEarthOn" + n),
                    anotherElement = (FrameworkElement)FindName("planetEarthOff" + n),
                    tempArray = planetDispEarth,
                    targetBoolean = false,
                    subIndex = subIndexNo,
                    commonDataNo = (int)CommonData.ZODIAC_EARTH
                });
                planetDispControlTable.Add("planetLilithOn" + n, new AspectControlTable()
                {
                    selfElement = (FrameworkElement)FindName("planetLilithOn" + n),
                    anotherElement = (FrameworkElement)FindName("planetLilithOff" + n),
                    tempArray = planetDispLilith,
                    targetBoolean = false,
                    subIndex = subIndexNo,
                    commonDataNo = (int)CommonData.ZODIAC_LILITH
                });
                planetDispControlTable.Add("planetSunOff" + n, new AspectControlTable()
                {
                    selfElement = (FrameworkElement)FindName("planetSunOff" + n),
                    anotherElement = (FrameworkElement)FindName("planetSunOn" + n),
                    tempArray = planetDispSun,
                    targetBoolean = true,
                    subIndex = subIndexNo,
                    commonDataNo = (int)CommonData.ZODIAC_SUN
                });
                planetDispControlTable.Add("planetMoonOff" + n, new AspectControlTable()
                {
                    selfElement = (FrameworkElement)FindName("planetMoonOff" + n),
                    anotherElement = (FrameworkElement)FindName("planetMoonOn" + n),
                    tempArray = planetDispMoon,
                    targetBoolean = true,
                    subIndex = subIndexNo,
                    commonDataNo = (int)CommonData.ZODIAC_MOON
                });
                planetDispControlTable.Add("planetMercuryOff" + n, new AspectControlTable()
                {
                    selfElement = (FrameworkElement)FindName("planetMercuryOff" + n),
                    anotherElement = (FrameworkElement)FindName("planetMercuryOn" + n),
                    tempArray = planetDispMercury,
                    targetBoolean = true,
                    subIndex = subIndexNo,
                    commonDataNo = (int)CommonData.ZODIAC_MERCURY
                });
                planetDispControlTable.Add("planetVenusOff" + n, new AspectControlTable()
                {
                    selfElement = (FrameworkElement)FindName("planetVenusOff" + n),
                    anotherElement = (FrameworkElement)FindName("planetVenusOn" + n),
                    tempArray = planetDispVenus,
                    targetBoolean = true,
                    subIndex = subIndexNo,
                    commonDataNo = (int)CommonData.ZODIAC_VENUS
                });
                planetDispControlTable.Add("planetMarsOff" + n, new AspectControlTable()
                {
                    selfElement = (FrameworkElement)FindName("planetMarsOff" + n),
                    anotherElement = (FrameworkElement)FindName("planetMarsOn" + n),
                    tempArray = planetDispMars,
                    targetBoolean = true,
                    subIndex = subIndexNo,
                    commonDataNo = (int)CommonData.ZODIAC_MARS
                });
                planetDispControlTable.Add("planetJupiterOff" + n, new AspectControlTable()
                {
                    selfElement = (FrameworkElement)FindName("planetJupiterOff" + n),
                    anotherElement = (FrameworkElement)FindName("planetJupiterOn" + n),
                    tempArray = planetDispJupiter,
                    targetBoolean = true,
                    subIndex = subIndexNo,
                    commonDataNo = (int)CommonData.ZODIAC_JUPITER
                });
                planetDispControlTable.Add("planetSaturnOff" + n, new AspectControlTable()
                {
                    selfElement = (FrameworkElement)FindName("planetSaturnOff" + n),
                    anotherElement = (FrameworkElement)FindName("planetSaturnOn" + n),
                    tempArray = planetDispSaturn,
                    targetBoolean = true,
                    subIndex = subIndexNo,
                    commonDataNo = (int)CommonData.ZODIAC_SATURN
                });
                planetDispControlTable.Add("planetUranusOff" + n, new AspectControlTable()
                {
                    selfElement = (FrameworkElement)FindName("planetUranusOff" + n),
                    anotherElement = (FrameworkElement)FindName("planetUranusOn" + n),
                    tempArray = planetDispUranus,
                    targetBoolean = true,
                    subIndex = subIndexNo,
                    commonDataNo = (int)CommonData.ZODIAC_URANUS
                });
                planetDispControlTable.Add("planetNeptuneOff" + n, new AspectControlTable()
                {
                    selfElement = (FrameworkElement)FindName("planetNeptuneOff" + n),
                    anotherElement = (FrameworkElement)FindName("planetNeptuneOn" + n),
                    tempArray = planetDispNeptune,
                    targetBoolean = true,
                    subIndex = subIndexNo,
                    commonDataNo = (int)CommonData.ZODIAC_NEPTUNE
                });
                planetDispControlTable.Add("planetPlutoOff" + n, new AspectControlTable()
                {
                    selfElement = (FrameworkElement)FindName("planetPlutoOff" + n),
                    anotherElement = (FrameworkElement)FindName("planetPlutoOn" + n),
                    tempArray = planetDispPluto,
                    targetBoolean = true,
                    subIndex = subIndexNo,
                    commonDataNo = (int)CommonData.ZODIAC_PLUTO
                });
                planetDispControlTable.Add("planetDhOff" + n, new AspectControlTable()
                {
                    selfElement = (FrameworkElement)FindName("planetDhOff" + n),
                    anotherElement = (FrameworkElement)FindName("planetDhOn" + n),
                    tempArray = planetDispDh,
                    targetBoolean = true,
                    subIndex = subIndexNo,
                    commonDataNo = (int)CommonData.ZODIAC_DH_TRUENODE
                });
                planetDispControlTable.Add("planetChironOff" + n, new AspectControlTable()
                {
                    selfElement = (FrameworkElement)FindName("planetChironOff" + n),
                    anotherElement = (FrameworkElement)FindName("planetChironOn" + n),
                    tempArray = planetDispChiron,
                    targetBoolean = true,
                    subIndex = subIndexNo,
                    commonDataNo = (int)CommonData.ZODIAC_CHIRON
                });
                planetDispControlTable.Add("planetAscOff" + n, new AspectControlTable()
                {
                    selfElement = (FrameworkElement)FindName("planetAscOff" + n),
                    anotherElement = (FrameworkElement)FindName("planetAscOn" + n),
                    tempArray = planetDispAsc,
                    targetBoolean = true,
                    subIndex = subIndexNo,
                    commonDataNo = (int)CommonData.ZODIAC_ASC
                });
                planetDispControlTable.Add("planetMcOff" + n, new AspectControlTable()
                {
                    selfElement = (FrameworkElement)FindName("planetMcOff" + n),
                    anotherElement = (FrameworkElement)FindName("planetMcOn" + n),
                    tempArray = planetDispMc,
                    targetBoolean = true,
                    subIndex = subIndexNo,
                    commonDataNo = (int)CommonData.ZODIAC_MC
                });
                planetDispControlTable.Add("planetEarthOff" + n, new AspectControlTable()
                {
                    selfElement = (FrameworkElement)FindName("planetEarthOff" + n),
                    anotherElement = (FrameworkElement)FindName("planetEarthOn" + n),
                    tempArray = planetDispEarth,
                    targetBoolean = true,
                    subIndex = subIndexNo,
                    commonDataNo = (int)CommonData.ZODIAC_EARTH
                });
                planetDispControlTable.Add("planetLilithOff" + n, new AspectControlTable()
                {
                    selfElement = (FrameworkElement)FindName("planetLilithOff" + n),
                    anotherElement = (FrameworkElement)FindName("planetLilithOn" + n),
                    tempArray = planetDispLilith,
                    targetBoolean = true,
                    subIndex = subIndexNo,
                    commonDataNo = (int)CommonData.ZODIAC_LILITH
                });

            }


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
            if (disp22.IsChecked == true)
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
            if (disp33.IsChecked == true)
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
            if (disp12.IsChecked == true)
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
            if (disp13.IsChecked == true)
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
            if (disp23.IsChecked == true)
            {
                aspectDispChecked[index, 5] = true;
            }
            else
            {
                aspectDispChecked[index, 5] = false;
            }
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {

            for (int index = 0; index < 10; index++)
            {
                SettingXml xmldata = new SettingXml();
                #region xmldata-aspectPlanet
                xmldata.aspectSun11 = aspectSun[index, 0];
                xmldata.aspectSun22 = aspectSun[index, 1];
                xmldata.aspectSun33 = aspectSun[index, 2];
                xmldata.aspectSun12 = aspectSun[index, 3];
                xmldata.aspectSun13 = aspectSun[index, 4];
                xmldata.aspectSun23 = aspectSun[index, 5];
                xmldata.aspectMoon11 = aspectMoon[index, 0];
                xmldata.aspectMoon22 = aspectMoon[index, 1];
                xmldata.aspectMoon33 = aspectMoon[index, 2];
                xmldata.aspectMoon12 = aspectMoon[index, 3];
                xmldata.aspectMoon13 = aspectMoon[index, 4];
                xmldata.aspectMoon23 = aspectMoon[index, 5];
                xmldata.aspectMercury11 = aspectMercury[index, 0];
                xmldata.aspectMercury22 = aspectMercury[index, 1];
                xmldata.aspectMercury33 = aspectMercury[index, 2];
                xmldata.aspectMercury12 = aspectMercury[index, 3];
                xmldata.aspectMercury13 = aspectMercury[index, 4];
                xmldata.aspectMercury23 = aspectMercury[index, 5];
                xmldata.aspectVenus11 = aspectVenus[index, 0];
                xmldata.aspectVenus22 = aspectVenus[index, 1];
                xmldata.aspectVenus33 = aspectVenus[index, 2];
                xmldata.aspectVenus12 = aspectVenus[index, 3];
                xmldata.aspectVenus13 = aspectVenus[index, 4];
                xmldata.aspectVenus23 = aspectVenus[index, 5];
                xmldata.aspectMars11 = aspectMars[index, 0];
                xmldata.aspectMars22 = aspectMars[index, 1];
                xmldata.aspectMars33 = aspectMars[index, 2];
                xmldata.aspectMars12 = aspectMars[index, 3];
                xmldata.aspectMars13 = aspectMars[index, 4];
                xmldata.aspectMars23 = aspectMars[index, 5];
                xmldata.aspectJupiter11 = aspectJupiter[index, 0];
                xmldata.aspectJupiter22 = aspectJupiter[index, 1];
                xmldata.aspectJupiter33 = aspectJupiter[index, 2];
                xmldata.aspectJupiter12 = aspectJupiter[index, 3];
                xmldata.aspectJupiter13 = aspectJupiter[index, 4];
                xmldata.aspectJupiter23 = aspectJupiter[index, 5];
                xmldata.aspectSaturn11 = aspectSaturn[index, 0];
                xmldata.aspectSaturn22 = aspectSaturn[index, 1];
                xmldata.aspectSaturn33 = aspectSaturn[index, 2];
                xmldata.aspectSaturn12 = aspectSaturn[index, 3];
                xmldata.aspectSaturn13 = aspectSaturn[index, 4];
                xmldata.aspectSaturn23 = aspectSaturn[index, 5];
                xmldata.aspectUranus11 = aspectUranus[index, 0];
                xmldata.aspectUranus22 = aspectUranus[index, 1];
                xmldata.aspectUranus33 = aspectUranus[index, 2];
                xmldata.aspectUranus12 = aspectUranus[index, 3];
                xmldata.aspectUranus13 = aspectUranus[index, 4];
                xmldata.aspectUranus23 = aspectUranus[index, 5];
                xmldata.aspectNeptune11 = aspectNeptune[index, 0];
                xmldata.aspectNeptune22 = aspectNeptune[index, 1];
                xmldata.aspectNeptune33 = aspectNeptune[index, 2];
                xmldata.aspectNeptune12 = aspectNeptune[index, 3];
                xmldata.aspectNeptune13 = aspectNeptune[index, 4];
                xmldata.aspectNeptune23 = aspectNeptune[index, 5];
                xmldata.aspectPluto11 = aspectPluto[index, 0];
                xmldata.aspectPluto22 = aspectPluto[index, 1];
                xmldata.aspectPluto33 = aspectPluto[index, 2];
                xmldata.aspectPluto12 = aspectPluto[index, 3];
                xmldata.aspectPluto13 = aspectPluto[index, 4];
                xmldata.aspectPluto23 = aspectPluto[index, 5];
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
                xmldata.aspectEarth11 = aspectEarth[index, 0];
                xmldata.aspectEarth22 = aspectEarth[index, 1];
                xmldata.aspectEarth33 = aspectEarth[index, 2];
                xmldata.aspectEarth12 = aspectEarth[index, 3];
                xmldata.aspectEarth13 = aspectEarth[index, 4];
                xmldata.aspectEarth23 = aspectEarth[index, 5];
                xmldata.aspectLilith11 = aspectLilith[index, 0];
                xmldata.aspectLilith22 = aspectLilith[index, 1];
                xmldata.aspectLilith33 = aspectLilith[index, 2];
                xmldata.aspectLilith12 = aspectLilith[index, 3];
                xmldata.aspectLilith13 = aspectLilith[index, 4];
                xmldata.aspectLilith23 = aspectLilith[index, 5];
                #endregion

                #region xmldata-aspectAspect
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
                #endregion

                xmldata.dispAspect = new bool[6];
                xmldata.dispAspect[0] = aspectDispChecked[index, 0];
                xmldata.dispAspect[1] = aspectDispChecked[index, 1];
                xmldata.dispAspect[2] = aspectDispChecked[index, 2];
                xmldata.dispAspect[3] = aspectDispChecked[index, 3];
                xmldata.dispAspect[4] = aspectDispChecked[index, 4];
                xmldata.dispAspect[5] = aspectDispChecked[index, 5];

                xmldata.dispPlanetSun11 = planetDispSun[index, 0];
                xmldata.dispPlanetSun22 = planetDispSun[index, 1];
                xmldata.dispPlanetSun33 = planetDispSun[index, 2];
                xmldata.dispPlanetMoon11 = planetDispMoon[index, 0];
                xmldata.dispPlanetMoon22 = planetDispMoon[index, 1];
                xmldata.dispPlanetMoon33 = planetDispMoon[index, 2];
                xmldata.dispPlanetMercury11 = planetDispMercury[index, 0];
                xmldata.dispPlanetMercury22 = planetDispMercury[index, 1];
                xmldata.dispPlanetMercury33 = planetDispMercury[index, 2];
                xmldata.dispPlanetVenus11 = planetDispVenus[index, 0];
                xmldata.dispPlanetVenus22 = planetDispVenus[index, 1];
                xmldata.dispPlanetVenus33 = planetDispVenus[index, 2];
                xmldata.dispPlanetMars11 = planetDispMars[index, 0];
                xmldata.dispPlanetMars22 = planetDispMars[index, 1];
                xmldata.dispPlanetMars33 = planetDispMars[index, 2];
                xmldata.dispPlanetJupiter11 = planetDispJupiter[index, 0];
                xmldata.dispPlanetJupiter22 = planetDispJupiter[index, 1];
                xmldata.dispPlanetJupiter33 = planetDispJupiter[index, 2];
                xmldata.dispPlanetSaturn11 = planetDispSaturn[index, 0];
                xmldata.dispPlanetSaturn22 = planetDispSaturn[index, 1];
                xmldata.dispPlanetSaturn33 = planetDispSaturn[index, 2];
                xmldata.dispPlanetUranus11 = planetDispUranus[index, 0];
                xmldata.dispPlanetUranus22 = planetDispUranus[index, 1];
                xmldata.dispPlanetUranus33 = planetDispUranus[index, 2];
                xmldata.dispPlanetNeptune11 = planetDispNeptune[index, 0];
                xmldata.dispPlanetNeptune22 = planetDispNeptune[index, 1];
                xmldata.dispPlanetNeptune33 = planetDispNeptune[index, 2];
                xmldata.dispPlanetPluto11 = planetDispPluto[index, 0];
                xmldata.dispPlanetPluto22 = planetDispPluto[index, 1];
                xmldata.dispPlanetPluto33 = planetDispPluto[index, 2];
                xmldata.dispPlanetDh11 = planetDispDh[index, 0];
                xmldata.dispPlanetDh22 = planetDispDh[index, 1];
                xmldata.dispPlanetDh33 = planetDispDh[index, 2];
                xmldata.dispPlanetChiron11 = planetDispChiron[index, 0];
                xmldata.dispPlanetChiron22 = planetDispChiron[index, 1];
                xmldata.dispPlanetChiron33 = planetDispChiron[index, 2];
                xmldata.dispPlanetAsc11 = planetDispAsc[index, 0];
                xmldata.dispPlanetAsc22 = planetDispAsc[index, 1];
                xmldata.dispPlanetAsc33 = planetDispAsc[index, 2];
                xmldata.dispPlanetMc11 = planetDispMc[index, 0];
                xmldata.dispPlanetMc22 = planetDispMc[index, 1];
                xmldata.dispPlanetMc33 = planetDispMc[index, 2];
                xmldata.dispPlanetEarth11 = planetDispEarth[index, 0];
                xmldata.dispPlanetEarth22 = planetDispEarth[index, 1];
                xmldata.dispPlanetEarth33 = planetDispEarth[index, 2];
                xmldata.dispPlanetLilith11 = planetDispLilith[index, 0];
                xmldata.dispPlanetLilith22 = planetDispLilith[index, 1];
                xmldata.dispPlanetLilith33 = planetDispLilith[index, 2];

                #region xmldata-orb
                xmldata.orb_sun_hard_1st_0 = orbSunHard1st[index, 0];
                xmldata.orb_sun_hard_2nd_0 = orbSunHard2nd[index, 0];
                xmldata.orb_sun_hard_150_0 = orbSunHard150[index, 0];
                xmldata.orb_sun_soft_1st_0 = orbSunSoft1st[index, 0];
                xmldata.orb_sun_soft_2nd_0 = orbSunSoft2nd[index, 0];
                xmldata.orb_sun_soft_150_0 = orbSunSoft150[index, 0];
                xmldata.orb_moon_hard_1st_0 = orbMoonHard1st[index, 0];
                xmldata.orb_moon_hard_2nd_0 = orbMoonHard2nd[index, 0];
                xmldata.orb_moon_hard_150_0 = orbMoonHard150[index, 0];
                xmldata.orb_moon_soft_1st_0 = orbMoonSoft1st[index, 0];
                xmldata.orb_moon_soft_2nd_0 = orbMoonSoft2nd[index, 0];
                xmldata.orb_moon_soft_150_0 = orbMoonSoft150[index, 0];
                xmldata.orb_other_hard_1st_0 = orbOtherHard1st[index, 0];
                xmldata.orb_other_hard_2nd_0 = orbOtherHard2nd[index, 0];
                xmldata.orb_other_hard_150_0 = orbOtherHard150[index, 0];
                xmldata.orb_other_soft_1st_0 = orbOtherSoft1st[index, 0];
                xmldata.orb_other_soft_2nd_0 = orbOtherSoft2nd[index, 0];
                xmldata.orb_other_soft_150_0 = orbOtherSoft150[index, 0];
                xmldata.orb_sun_hard_1st_1 = orbSunHard1st[index, 1];
                xmldata.orb_sun_hard_2nd_1 = orbSunHard2nd[index, 1];
                xmldata.orb_sun_hard_150_1 = orbSunHard150[index, 1];
                xmldata.orb_sun_soft_1st_1 = orbSunSoft1st[index, 1];
                xmldata.orb_sun_soft_2nd_1 = orbSunSoft2nd[index, 1];
                xmldata.orb_sun_soft_150_1 = orbSunSoft150[index, 1];
                xmldata.orb_moon_hard_1st_1 = orbMoonHard1st[index, 1];
                xmldata.orb_moon_hard_2nd_1 = orbMoonHard2nd[index, 1];
                xmldata.orb_moon_hard_150_1 = orbMoonHard150[index, 1];
                xmldata.orb_moon_soft_1st_1 = orbMoonSoft1st[index, 1];
                xmldata.orb_moon_soft_2nd_1 = orbMoonSoft2nd[index, 1];
                xmldata.orb_moon_soft_150_1 = orbMoonSoft150[index, 1];
                xmldata.orb_other_hard_1st_1 = orbOtherHard1st[index, 1];
                xmldata.orb_other_hard_2nd_1 = orbOtherHard2nd[index, 1];
                xmldata.orb_other_hard_150_1 = orbOtherHard150[index, 1];
                xmldata.orb_other_soft_1st_1 = orbOtherSoft1st[index, 1];
                xmldata.orb_other_soft_2nd_1 = orbOtherSoft2nd[index, 1];
                xmldata.orb_other_soft_150_1 = orbOtherSoft150[index, 1];
                xmldata.orb_sun_hard_1st_2 = orbSunHard1st[index, 2];
                xmldata.orb_sun_hard_2nd_2 = orbSunHard2nd[index, 2];
                xmldata.orb_sun_hard_150_2 = orbSunHard150[index, 2];
                xmldata.orb_sun_soft_1st_2 = orbSunSoft1st[index, 2];
                xmldata.orb_sun_soft_2nd_2 = orbSunSoft2nd[index, 2];
                xmldata.orb_sun_soft_150_2 = orbSunSoft150[index, 2];
                xmldata.orb_moon_hard_1st_2 = orbMoonHard1st[index, 2];
                xmldata.orb_moon_hard_2nd_2 = orbMoonHard2nd[index, 2];
                xmldata.orb_moon_hard_150_2 = orbMoonHard150[index, 2];
                xmldata.orb_moon_soft_1st_2 = orbMoonSoft1st[index, 2];
                xmldata.orb_moon_soft_2nd_2 = orbMoonSoft2nd[index, 2];
                xmldata.orb_moon_soft_150_2 = orbMoonSoft150[index, 2];
                xmldata.orb_other_hard_1st_2 = orbOtherHard1st[index, 2];
                xmldata.orb_other_hard_2nd_2 = orbOtherHard2nd[index, 2];
                xmldata.orb_other_hard_150_2 = orbOtherHard150[index, 2];
                xmldata.orb_other_soft_1st_2 = orbOtherSoft1st[index, 2];
                xmldata.orb_other_soft_2nd_2 = orbOtherSoft2nd[index, 2];
                xmldata.orb_other_soft_150_2 = orbOtherSoft150[index, 2];
                xmldata.orb_sun_hard_1st_3 = orbSunHard1st[index, 3];
                xmldata.orb_sun_hard_2nd_3 = orbSunHard2nd[index, 3];
                xmldata.orb_sun_hard_150_3 = orbSunHard150[index, 3];
                xmldata.orb_sun_soft_1st_3 = orbSunSoft1st[index, 3];
                xmldata.orb_sun_soft_2nd_3 = orbSunSoft2nd[index, 3];
                xmldata.orb_sun_soft_150_3 = orbSunSoft150[index, 3];
                xmldata.orb_moon_hard_1st_3 = orbMoonHard1st[index, 3];
                xmldata.orb_moon_hard_2nd_3 = orbMoonHard2nd[index, 3];
                xmldata.orb_moon_hard_150_3 = orbMoonHard150[index, 3];
                xmldata.orb_moon_soft_1st_3 = orbMoonSoft1st[index, 3];
                xmldata.orb_moon_soft_2nd_3 = orbMoonSoft2nd[index, 3];
                xmldata.orb_moon_soft_150_3 = orbMoonSoft150[index, 3];
                xmldata.orb_other_hard_1st_3 = orbOtherHard1st[index, 3];
                xmldata.orb_other_hard_2nd_3 = orbOtherHard2nd[index, 3];
                xmldata.orb_other_hard_150_3 = orbOtherHard150[index, 3];
                xmldata.orb_other_soft_1st_3 = orbOtherSoft1st[index, 3];
                xmldata.orb_other_soft_2nd_3 = orbOtherSoft2nd[index, 3];
                xmldata.orb_other_soft_150_3 = orbOtherSoft150[index, 3];
                xmldata.orb_sun_hard_1st_4 = orbSunHard1st[index, 4];
                xmldata.orb_sun_hard_2nd_4 = orbSunHard2nd[index, 4];
                xmldata.orb_sun_hard_150_4 = orbSunHard150[index, 4];
                xmldata.orb_sun_soft_1st_4 = orbSunSoft1st[index, 4];
                xmldata.orb_sun_soft_2nd_4 = orbSunSoft2nd[index, 4];
                xmldata.orb_sun_soft_150_4 = orbSunSoft150[index, 4];
                xmldata.orb_moon_hard_1st_4 = orbMoonHard1st[index, 4];
                xmldata.orb_moon_hard_2nd_4 = orbMoonHard2nd[index, 4];
                xmldata.orb_moon_hard_150_4 = orbMoonHard150[index, 4];
                xmldata.orb_moon_soft_1st_4 = orbMoonSoft1st[index, 4];
                xmldata.orb_moon_soft_2nd_4 = orbMoonSoft2nd[index, 4];
                xmldata.orb_moon_soft_150_4 = orbMoonSoft150[index, 4];
                xmldata.orb_other_hard_1st_4 = orbOtherHard1st[index, 4];
                xmldata.orb_other_hard_2nd_4 = orbOtherHard2nd[index, 4];
                xmldata.orb_other_hard_150_4 = orbOtherHard150[index, 4];
                xmldata.orb_other_soft_1st_4 = orbOtherSoft1st[index, 4];
                xmldata.orb_other_soft_2nd_4 = orbOtherSoft2nd[index, 4];
                xmldata.orb_other_soft_150_4 = orbOtherSoft150[index, 4];
                xmldata.orb_sun_hard_1st_5 = orbSunHard1st[index, 5];
                xmldata.orb_sun_hard_2nd_5 = orbSunHard2nd[index, 5];
                xmldata.orb_sun_hard_150_5 = orbSunHard150[index, 5];
                xmldata.orb_sun_soft_1st_5 = orbSunSoft1st[index, 5];
                xmldata.orb_sun_soft_2nd_5 = orbSunSoft2nd[index, 5];
                xmldata.orb_sun_soft_150_5 = orbSunSoft150[index, 5];
                xmldata.orb_moon_hard_1st_5 = orbMoonHard1st[index, 5];
                xmldata.orb_moon_hard_2nd_5 = orbMoonHard2nd[index, 5];
                xmldata.orb_moon_hard_150_5 = orbMoonHard150[index, 5];
                xmldata.orb_moon_soft_1st_5 = orbMoonSoft1st[index, 5];
                xmldata.orb_moon_soft_2nd_5 = orbMoonSoft2nd[index, 5];
                xmldata.orb_moon_soft_150_5 = orbMoonSoft150[index, 5];
                xmldata.orb_other_hard_1st_5 = orbOtherHard1st[index, 5];
                xmldata.orb_other_hard_2nd_5 = orbOtherHard2nd[index, 5];
                xmldata.orb_other_hard_150_5 = orbOtherHard150[index, 5];
                xmldata.orb_other_soft_1st_5 = orbOtherSoft1st[index, 5];
                xmldata.orb_other_soft_2nd_5 = orbOtherSoft2nd[index, 5];
                xmldata.orb_other_soft_150_5 = orbOtherSoft150[index, 5];
                #endregion
                xmldata.dispname = tempDispName[index];

                string filename = @"system\setting" + index + ".csm";
                XmlSerializer serializer = new XmlSerializer(typeof(SettingXml));
                FileStream fs = new FileStream(filename, FileMode.Create);
                StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
                serializer.Serialize(sw, xmldata);
                sw.Close();
                fs.Close();

                main.settings[index].xmlData = xmldata;
            }

            int currentIndex = main.dispSettingBox.SelectedIndex;

            // settingへのコピー
            for (int i = 0; i < 10; i++)
            {
                main.settings[i].dispPlanet.Clear();
                Dictionary<int, bool> dp11 = new Dictionary<int, bool>();
                dp11.Add(CommonData.ZODIAC_SUN, main.settings[i].xmlData.dispPlanetSun11);
                dp11.Add(CommonData.ZODIAC_MOON, main.settings[i].xmlData.dispPlanetMoon11);
                dp11.Add(CommonData.ZODIAC_MERCURY, main.settings[i].xmlData.dispPlanetMercury11);
                dp11.Add(CommonData.ZODIAC_VENUS, main.settings[i].xmlData.dispPlanetVenus11);
                dp11.Add(CommonData.ZODIAC_MARS, main.settings[i].xmlData.dispPlanetMars11);
                dp11.Add(CommonData.ZODIAC_JUPITER, main.settings[i].xmlData.dispPlanetJupiter11);
                dp11.Add(CommonData.ZODIAC_SATURN, main.settings[i].xmlData.dispPlanetSaturn11);
                dp11.Add(CommonData.ZODIAC_URANUS, main.settings[i].xmlData.dispPlanetUranus11);
                dp11.Add(CommonData.ZODIAC_NEPTUNE, main.settings[i].xmlData.dispPlanetNeptune11);
                dp11.Add(CommonData.ZODIAC_PLUTO, main.settings[i].xmlData.dispPlanetPluto11);
                dp11.Add(CommonData.ZODIAC_DH_TRUENODE, main.settings[i].xmlData.dispPlanetDh11);
                dp11.Add(CommonData.ZODIAC_CHIRON, main.settings[i].xmlData.dispPlanetChiron11);
                dp11.Add(CommonData.ZODIAC_ASC, main.settings[i].xmlData.dispPlanetAsc11);
                dp11.Add(CommonData.ZODIAC_MC, main.settings[i].xmlData.dispPlanetMc11);
                dp11.Add(CommonData.ZODIAC_EARTH, main.settings[i].xmlData.dispPlanetEarth11);
                dp11.Add(CommonData.ZODIAC_LILITH, main.settings[i].xmlData.dispPlanetLilith11);
                dp11.Add(CommonData.ZODIAC_CELES, false);
                dp11.Add(CommonData.ZODIAC_PARAS, false);
                dp11.Add(CommonData.ZODIAC_JUNO, false);
                dp11.Add(CommonData.ZODIAC_VESTA, false);
                main.settings[i].dispPlanet.Add(dp11);
                Dictionary<int, bool> dp22 = new Dictionary<int, bool>();
                dp22.Add(CommonData.ZODIAC_SUN, main.settings[i].xmlData.dispPlanetSun22);
                dp22.Add(CommonData.ZODIAC_MOON, main.settings[i].xmlData.dispPlanetMoon22);
                dp22.Add(CommonData.ZODIAC_MERCURY, main.settings[i].xmlData.dispPlanetMercury22);
                dp22.Add(CommonData.ZODIAC_VENUS, main.settings[i].xmlData.dispPlanetVenus22);
                dp22.Add(CommonData.ZODIAC_MARS, main.settings[i].xmlData.dispPlanetMars22);
                dp22.Add(CommonData.ZODIAC_JUPITER, main.settings[i].xmlData.dispPlanetJupiter22);
                dp22.Add(CommonData.ZODIAC_SATURN, main.settings[i].xmlData.dispPlanetSaturn22);
                dp22.Add(CommonData.ZODIAC_URANUS, main.settings[i].xmlData.dispPlanetUranus22);
                dp22.Add(CommonData.ZODIAC_NEPTUNE, main.settings[i].xmlData.dispPlanetNeptune22);
                dp22.Add(CommonData.ZODIAC_PLUTO, main.settings[i].xmlData.dispPlanetPluto22);
                dp22.Add(CommonData.ZODIAC_DH_TRUENODE, main.settings[i].xmlData.dispPlanetDh22);
                dp22.Add(CommonData.ZODIAC_CHIRON, main.settings[i].xmlData.dispPlanetChiron22);
                dp22.Add(CommonData.ZODIAC_ASC, main.settings[i].xmlData.dispPlanetAsc22);
                dp22.Add(CommonData.ZODIAC_MC, main.settings[i].xmlData.dispPlanetMc22);
                dp22.Add(CommonData.ZODIAC_EARTH, main.settings[i].xmlData.dispPlanetEarth22);
                dp22.Add(CommonData.ZODIAC_LILITH, main.settings[i].xmlData.dispPlanetLilith22);
                dp22.Add(CommonData.ZODIAC_CELES, false);
                dp22.Add(CommonData.ZODIAC_PARAS, false);
                dp22.Add(CommonData.ZODIAC_JUNO, false);
                dp22.Add(CommonData.ZODIAC_VESTA, false);
                main.settings[i].dispPlanet.Add(dp22);
                Dictionary<int, bool> dp33 = new Dictionary<int, bool>();
                dp33.Add(CommonData.ZODIAC_SUN, main.settings[i].xmlData.dispPlanetSun33);
                dp33.Add(CommonData.ZODIAC_MOON, main.settings[i].xmlData.dispPlanetMoon33);
                dp33.Add(CommonData.ZODIAC_MERCURY, main.settings[i].xmlData.dispPlanetMercury33);
                dp33.Add(CommonData.ZODIAC_VENUS, main.settings[i].xmlData.dispPlanetVenus33);
                dp33.Add(CommonData.ZODIAC_MARS, main.settings[i].xmlData.dispPlanetMars33);
                dp33.Add(CommonData.ZODIAC_JUPITER, main.settings[i].xmlData.dispPlanetJupiter33);
                dp33.Add(CommonData.ZODIAC_SATURN, main.settings[i].xmlData.dispPlanetSaturn33);
                dp33.Add(CommonData.ZODIAC_URANUS, main.settings[i].xmlData.dispPlanetUranus33);
                dp33.Add(CommonData.ZODIAC_NEPTUNE, main.settings[i].xmlData.dispPlanetNeptune33);
                dp33.Add(CommonData.ZODIAC_PLUTO, main.settings[i].xmlData.dispPlanetPluto33);
                dp33.Add(CommonData.ZODIAC_DH_TRUENODE, main.settings[i].xmlData.dispPlanetDh33);
                dp33.Add(CommonData.ZODIAC_CHIRON, main.settings[i].xmlData.dispPlanetChiron33);
                dp33.Add(CommonData.ZODIAC_ASC, main.settings[i].xmlData.dispPlanetAsc33);
                dp33.Add(CommonData.ZODIAC_MC, main.settings[i].xmlData.dispPlanetMc33);
                dp33.Add(CommonData.ZODIAC_EARTH, main.settings[i].xmlData.dispPlanetEarth33);
                dp33.Add(CommonData.ZODIAC_LILITH, main.settings[i].xmlData.dispPlanetLilith33);
                dp33.Add(CommonData.ZODIAC_CELES, false);
                dp33.Add(CommonData.ZODIAC_PARAS, false);
                dp33.Add(CommonData.ZODIAC_JUNO, false);
                dp33.Add(CommonData.ZODIAC_VESTA, false);
                main.settings[i].dispPlanet.Add(dp33);

                main.settings[i].dispAspectPlanet.Clear();
                Dictionary<int, bool> d11 = new Dictionary<int, bool>();
                d11.Add(CommonData.ZODIAC_SUN, main.settings[i].xmlData.aspectSun11);
                d11.Add(CommonData.ZODIAC_MOON, main.settings[i].xmlData.aspectMoon11);
                d11.Add(CommonData.ZODIAC_MERCURY, main.settings[i].xmlData.aspectMercury11);
                d11.Add(CommonData.ZODIAC_VENUS, main.settings[i].xmlData.aspectVenus11);
                d11.Add(CommonData.ZODIAC_MARS, main.settings[i].xmlData.aspectMars11);
                d11.Add(CommonData.ZODIAC_JUPITER, main.settings[i].xmlData.aspectJupiter11);
                d11.Add(CommonData.ZODIAC_SATURN, main.settings[i].xmlData.aspectSaturn11);
                d11.Add(CommonData.ZODIAC_URANUS, main.settings[i].xmlData.aspectUranus11);
                d11.Add(CommonData.ZODIAC_NEPTUNE, main.settings[i].xmlData.aspectNeptune11);
                d11.Add(CommonData.ZODIAC_PLUTO, main.settings[i].xmlData.aspectPluto11);
                d11.Add(CommonData.ZODIAC_DH_TRUENODE, main.settings[i].xmlData.aspectDh11);
                d11.Add(CommonData.ZODIAC_CHIRON, main.settings[i].xmlData.aspectChiron11);
                d11.Add(CommonData.ZODIAC_ASC, main.settings[i].xmlData.aspectAsc11);
                d11.Add(CommonData.ZODIAC_MC, main.settings[i].xmlData.aspectMc11);
                d11.Add(CommonData.ZODIAC_EARTH, main.settings[i].xmlData.aspectEarth11);
                d11.Add(CommonData.ZODIAC_LILITH, main.settings[i].xmlData.dispPlanetLilith11);
                d11.Add(CommonData.ZODIAC_CELES, false);
                d11.Add(CommonData.ZODIAC_PARAS, false);
                d11.Add(CommonData.ZODIAC_JUNO, false);
                d11.Add(CommonData.ZODIAC_VESTA, false);
                main.settings[i].dispAspectPlanet.Add(d11);
                Dictionary<int, bool> d22 = new Dictionary<int, bool>();
                d22.Add(CommonData.ZODIAC_SUN, main.settings[i].xmlData.aspectSun22);
                d22.Add(CommonData.ZODIAC_MOON, main.settings[i].xmlData.aspectMoon22);
                d22.Add(CommonData.ZODIAC_MERCURY, main.settings[i].xmlData.aspectMercury22);
                d22.Add(CommonData.ZODIAC_VENUS, main.settings[i].xmlData.aspectVenus22);
                d22.Add(CommonData.ZODIAC_MARS, main.settings[i].xmlData.aspectMars22);
                d22.Add(CommonData.ZODIAC_JUPITER, main.settings[i].xmlData.aspectJupiter22);
                d22.Add(CommonData.ZODIAC_SATURN, main.settings[i].xmlData.aspectSaturn22);
                d22.Add(CommonData.ZODIAC_URANUS, main.settings[i].xmlData.aspectUranus22);
                d22.Add(CommonData.ZODIAC_NEPTUNE, main.settings[i].xmlData.aspectNeptune22);
                d22.Add(CommonData.ZODIAC_PLUTO, main.settings[i].xmlData.aspectPluto22);
                d22.Add(CommonData.ZODIAC_DH_TRUENODE, main.settings[i].xmlData.aspectDh22);
                d22.Add(CommonData.ZODIAC_CHIRON, main.settings[i].xmlData.aspectChiron22);
                d22.Add(CommonData.ZODIAC_ASC, main.settings[i].xmlData.aspectAsc22);
                d22.Add(CommonData.ZODIAC_MC, main.settings[i].xmlData.aspectMc22);
                d22.Add(CommonData.ZODIAC_EARTH, main.settings[i].xmlData.aspectEarth22);
                d22.Add(CommonData.ZODIAC_LILITH, main.settings[i].xmlData.dispPlanetLilith22);
                d22.Add(CommonData.ZODIAC_CELES, false);
                d22.Add(CommonData.ZODIAC_PARAS, false);
                d22.Add(CommonData.ZODIAC_JUNO, false);
                d22.Add(CommonData.ZODIAC_VESTA, false);
                main.settings[i].dispAspectPlanet.Add(d22);
                Dictionary<int, bool> d33 = new Dictionary<int, bool>();
                d33.Add(CommonData.ZODIAC_SUN, main.settings[i].xmlData.aspectSun33);
                d33.Add(CommonData.ZODIAC_MOON, main.settings[i].xmlData.aspectMoon33);
                d33.Add(CommonData.ZODIAC_MERCURY, main.settings[i].xmlData.aspectMercury33);
                d33.Add(CommonData.ZODIAC_VENUS, main.settings[i].xmlData.aspectVenus33);
                d33.Add(CommonData.ZODIAC_MARS, main.settings[i].xmlData.aspectMars33);
                d33.Add(CommonData.ZODIAC_JUPITER, main.settings[i].xmlData.aspectJupiter33);
                d33.Add(CommonData.ZODIAC_SATURN, main.settings[i].xmlData.aspectSaturn33);
                d33.Add(CommonData.ZODIAC_URANUS, main.settings[i].xmlData.aspectUranus33);
                d33.Add(CommonData.ZODIAC_NEPTUNE, main.settings[i].xmlData.aspectNeptune33);
                d33.Add(CommonData.ZODIAC_PLUTO, main.settings[i].xmlData.aspectPluto33);
                d33.Add(CommonData.ZODIAC_DH_TRUENODE, main.settings[i].xmlData.aspectDh33);
                d33.Add(CommonData.ZODIAC_CHIRON, main.settings[i].xmlData.aspectChiron33);
                d33.Add(CommonData.ZODIAC_ASC, main.settings[i].xmlData.aspectAsc33);
                d33.Add(CommonData.ZODIAC_MC, main.settings[i].xmlData.aspectMc33);
                d33.Add(CommonData.ZODIAC_EARTH, main.settings[i].xmlData.aspectEarth33);
                d33.Add(CommonData.ZODIAC_LILITH, main.settings[i].xmlData.dispPlanetLilith33);
                d33.Add(CommonData.ZODIAC_CELES, false);
                d33.Add(CommonData.ZODIAC_PARAS, false);
                d33.Add(CommonData.ZODIAC_JUNO, false);
                d33.Add(CommonData.ZODIAC_VESTA, false);
                main.settings[i].dispAspectPlanet.Add(d33);
                Dictionary<int, bool> d12 = new Dictionary<int, bool>();
                d12.Add(CommonData.ZODIAC_SUN, main.settings[i].xmlData.aspectSun12);
                d12.Add(CommonData.ZODIAC_MOON, main.settings[i].xmlData.aspectMoon12);
                d12.Add(CommonData.ZODIAC_MERCURY, main.settings[i].xmlData.aspectMercury12);
                d12.Add(CommonData.ZODIAC_VENUS, main.settings[i].xmlData.aspectVenus12);
                d12.Add(CommonData.ZODIAC_MARS, main.settings[i].xmlData.aspectMars12);
                d12.Add(CommonData.ZODIAC_JUPITER, main.settings[i].xmlData.aspectJupiter12);
                d12.Add(CommonData.ZODIAC_SATURN, main.settings[i].xmlData.aspectSaturn12);
                d12.Add(CommonData.ZODIAC_URANUS, main.settings[i].xmlData.aspectUranus12);
                d12.Add(CommonData.ZODIAC_NEPTUNE, main.settings[i].xmlData.aspectNeptune12);
                d12.Add(CommonData.ZODIAC_PLUTO, main.settings[i].xmlData.aspectPluto12);
                d12.Add(CommonData.ZODIAC_DH_TRUENODE, main.settings[i].xmlData.aspectDh12);
                d12.Add(CommonData.ZODIAC_CHIRON, main.settings[i].xmlData.aspectChiron12);
                d12.Add(CommonData.ZODIAC_ASC, main.settings[i].xmlData.aspectAsc12);
                d12.Add(CommonData.ZODIAC_MC, main.settings[i].xmlData.aspectMc12);
                d12.Add(CommonData.ZODIAC_EARTH, main.settings[i].xmlData.aspectEarth12);
                d12.Add(CommonData.ZODIAC_LILITH, main.settings[i].xmlData.dispPlanetLilith12);
                d12.Add(CommonData.ZODIAC_CELES, false);
                d12.Add(CommonData.ZODIAC_PARAS, false);
                d12.Add(CommonData.ZODIAC_JUNO, false);
                d12.Add(CommonData.ZODIAC_VESTA, false);
                main.settings[i].dispAspectPlanet.Add(d12);
                Dictionary<int, bool> d13 = new Dictionary<int, bool>();
                d13.Add(CommonData.ZODIAC_SUN, main.settings[i].xmlData.aspectSun13);
                d13.Add(CommonData.ZODIAC_MOON, main.settings[i].xmlData.aspectMoon13);
                d13.Add(CommonData.ZODIAC_MERCURY, main.settings[i].xmlData.aspectMercury13);
                d13.Add(CommonData.ZODIAC_VENUS, main.settings[i].xmlData.aspectVenus13);
                d13.Add(CommonData.ZODIAC_MARS, main.settings[i].xmlData.aspectMars13);
                d13.Add(CommonData.ZODIAC_JUPITER, main.settings[i].xmlData.aspectJupiter13);
                d13.Add(CommonData.ZODIAC_SATURN, main.settings[i].xmlData.aspectSaturn13);
                d13.Add(CommonData.ZODIAC_URANUS, main.settings[i].xmlData.aspectUranus13);
                d13.Add(CommonData.ZODIAC_NEPTUNE, main.settings[i].xmlData.aspectNeptune13);
                d13.Add(CommonData.ZODIAC_PLUTO, main.settings[i].xmlData.aspectPluto13);
                d13.Add(CommonData.ZODIAC_DH_TRUENODE, main.settings[i].xmlData.aspectDh13);
                d13.Add(CommonData.ZODIAC_ASC, main.settings[i].xmlData.aspectAsc13);
                d13.Add(CommonData.ZODIAC_MC, main.settings[i].xmlData.aspectMc13);
                d13.Add(CommonData.ZODIAC_CHIRON, main.settings[i].xmlData.aspectChiron13);
                d13.Add(CommonData.ZODIAC_EARTH, main.settings[i].xmlData.aspectEarth13);
                d13.Add(CommonData.ZODIAC_LILITH, main.settings[i].xmlData.dispPlanetLilith13);
                d13.Add(CommonData.ZODIAC_CELES, false);
                d13.Add(CommonData.ZODIAC_PARAS, false);
                d13.Add(CommonData.ZODIAC_JUNO, false);
                d13.Add(CommonData.ZODIAC_VESTA, false);
                main.settings[i].dispAspectPlanet.Add(d13);
                Dictionary<int, bool> d23 = new Dictionary<int, bool>();
                d23.Add(CommonData.ZODIAC_SUN, main.settings[i].xmlData.aspectSun23);
                d23.Add(CommonData.ZODIAC_MOON, main.settings[i].xmlData.aspectMoon23);
                d23.Add(CommonData.ZODIAC_MERCURY, main.settings[i].xmlData.aspectMercury23);
                d23.Add(CommonData.ZODIAC_VENUS, main.settings[i].xmlData.aspectVenus23);
                d23.Add(CommonData.ZODIAC_MARS, main.settings[i].xmlData.aspectMars23);
                d23.Add(CommonData.ZODIAC_JUPITER, main.settings[i].xmlData.aspectJupiter23);
                d23.Add(CommonData.ZODIAC_SATURN, main.settings[i].xmlData.aspectSaturn23);
                d23.Add(CommonData.ZODIAC_URANUS, main.settings[i].xmlData.aspectUranus23);
                d23.Add(CommonData.ZODIAC_NEPTUNE, main.settings[i].xmlData.aspectNeptune23);
                d23.Add(CommonData.ZODIAC_PLUTO, main.settings[i].xmlData.aspectPluto23);
                d23.Add(CommonData.ZODIAC_DH_TRUENODE, main.settings[i].xmlData.aspectDh23);
                d23.Add(CommonData.ZODIAC_CHIRON, main.settings[i].xmlData.aspectChiron23);
                d23.Add(CommonData.ZODIAC_ASC, main.settings[i].xmlData.aspectAsc23);
                d23.Add(CommonData.ZODIAC_MC, main.settings[i].xmlData.aspectMc23);
                d23.Add(CommonData.ZODIAC_EARTH, main.settings[i].xmlData.aspectEarth23);
                d23.Add(CommonData.ZODIAC_LILITH, main.settings[i].xmlData.dispPlanetLilith23);
                d23.Add(CommonData.ZODIAC_CELES, false);
                d23.Add(CommonData.ZODIAC_PARAS, false);
                d23.Add(CommonData.ZODIAC_JUNO, false);
                d23.Add(CommonData.ZODIAC_VESTA, false);
                main.settings[i].dispAspectPlanet.Add(d23);

                main.settings[i].dispAspectCategory.Clear();
                #region settings-dispAspectCategory
                Dictionary<AspectKind, bool> a11 = new Dictionary<AspectKind, bool>();
                a11.Add(AspectKind.CONJUNCTION, main.settings[i].xmlData.aspectConjunction11);
                a11.Add(AspectKind.OPPOSITION, main.settings[i].xmlData.aspectOpposition11);
                a11.Add(AspectKind.TRINE, main.settings[i].xmlData.aspectTrine11);
                a11.Add(AspectKind.SQUARE, main.settings[i].xmlData.aspectSquare11);
                a11.Add(AspectKind.SEXTILE, main.settings[i].xmlData.aspectSextile11);
                a11.Add(AspectKind.INCONJUNCT, main.settings[i].xmlData.aspectInconjunct11);
                a11.Add(AspectKind.SESQUIQUADRATE, main.settings[i].xmlData.aspectSesquiquadrate11);
                main.settings[i].dispAspectCategory.Add(a11);
                Dictionary<AspectKind, bool> a22 = new Dictionary<AspectKind, bool>();
                a22.Add(AspectKind.CONJUNCTION, main.settings[i].xmlData.aspectConjunction22);
                a22.Add(AspectKind.OPPOSITION, main.settings[i].xmlData.aspectOpposition22);
                a22.Add(AspectKind.TRINE, main.settings[i].xmlData.aspectTrine22);
                a22.Add(AspectKind.SQUARE, main.settings[i].xmlData.aspectSquare22);
                a22.Add(AspectKind.SEXTILE, main.settings[i].xmlData.aspectSextile22);
                a22.Add(AspectKind.INCONJUNCT, main.settings[i].xmlData.aspectInconjunct22);
                a22.Add(AspectKind.SESQUIQUADRATE, main.settings[i].xmlData.aspectSesquiquadrate22);
                main.settings[i].dispAspectCategory.Add(a22);
                Dictionary<AspectKind, bool> a33 = new Dictionary<AspectKind, bool>();
                a33.Add(AspectKind.CONJUNCTION, main.settings[i].xmlData.aspectConjunction33);
                a33.Add(AspectKind.OPPOSITION, main.settings[i].xmlData.aspectOpposition33);
                a33.Add(AspectKind.TRINE, main.settings[i].xmlData.aspectTrine33);
                a33.Add(AspectKind.SQUARE, main.settings[i].xmlData.aspectSquare33);
                a33.Add(AspectKind.SEXTILE, main.settings[i].xmlData.aspectSextile33);
                a33.Add(AspectKind.INCONJUNCT, main.settings[i].xmlData.aspectInconjunct33);
                a33.Add(AspectKind.SESQUIQUADRATE, main.settings[i].xmlData.aspectSesquiquadrate33);
                main.settings[i].dispAspectCategory.Add(a33);
                Dictionary<AspectKind, bool> a12 = new Dictionary<AspectKind, bool>();
                a12.Add(AspectKind.CONJUNCTION, main.settings[i].xmlData.aspectConjunction12);
                a12.Add(AspectKind.OPPOSITION, main.settings[i].xmlData.aspectOpposition12);
                a12.Add(AspectKind.TRINE, main.settings[i].xmlData.aspectTrine12);
                a12.Add(AspectKind.SQUARE, main.settings[i].xmlData.aspectSquare12);
                a12.Add(AspectKind.SEXTILE, main.settings[i].xmlData.aspectSextile12);
                a12.Add(AspectKind.INCONJUNCT, main.settings[i].xmlData.aspectInconjunct12);
                a12.Add(AspectKind.SESQUIQUADRATE, main.settings[i].xmlData.aspectSesquiquadrate12);
                main.settings[i].dispAspectCategory.Add(a12);
                Dictionary<AspectKind, bool> a13 = new Dictionary<AspectKind, bool>();
                a13.Add(AspectKind.CONJUNCTION, main.settings[i].xmlData.aspectConjunction13);
                a13.Add(AspectKind.OPPOSITION, main.settings[i].xmlData.aspectOpposition13);
                a13.Add(AspectKind.TRINE, main.settings[i].xmlData.aspectTrine13);
                a13.Add(AspectKind.SQUARE, main.settings[i].xmlData.aspectSquare13);
                a13.Add(AspectKind.SEXTILE, main.settings[i].xmlData.aspectSextile13);
                a13.Add(AspectKind.INCONJUNCT, main.settings[i].xmlData.aspectInconjunct13);
                a13.Add(AspectKind.SESQUIQUADRATE, main.settings[i].xmlData.aspectSesquiquadrate13);
                main.settings[i].dispAspectCategory.Add(a13);
                Dictionary<AspectKind, bool> a23 = new Dictionary<AspectKind, bool>();
                a23.Add(AspectKind.CONJUNCTION, main.settings[i].xmlData.aspectConjunction23);
                a23.Add(AspectKind.OPPOSITION, main.settings[i].xmlData.aspectOpposition23);
                a23.Add(AspectKind.TRINE, main.settings[i].xmlData.aspectTrine23);
                a23.Add(AspectKind.SQUARE, main.settings[i].xmlData.aspectSquare23);
                a23.Add(AspectKind.SEXTILE, main.settings[i].xmlData.aspectSextile23);
                a23.Add(AspectKind.INCONJUNCT, main.settings[i].xmlData.aspectInconjunct23);
                a23.Add(AspectKind.SESQUIQUADRATE, main.settings[i].xmlData.aspectSesquiquadrate23);
                main.settings[i].dispAspectCategory.Add(a23);
                #endregion

                main.settings[i].orbs.Clear();
                Dictionary<OrbKind, double> o11 = new Dictionary<OrbKind, double>();
                #region o11
                o11.Add(OrbKind.SUN_HARD_1ST, main.settings[i].xmlData.orb_sun_hard_1st_0);
                o11.Add(OrbKind.SUN_SOFT_1ST, main.settings[i].xmlData.orb_sun_soft_1st_0);
                o11.Add(OrbKind.SUN_HARD_2ND, main.settings[i].xmlData.orb_sun_hard_2nd_0);
                o11.Add(OrbKind.SUN_SOFT_2ND, main.settings[i].xmlData.orb_sun_soft_2nd_0);
                o11.Add(OrbKind.SUN_HARD_150, main.settings[i].xmlData.orb_sun_hard_150_0);
                o11.Add(OrbKind.SUN_SOFT_150, main.settings[i].xmlData.orb_sun_soft_150_0);
                o11.Add(OrbKind.MOON_HARD_1ST, main.settings[i].xmlData.orb_moon_hard_1st_0);
                o11.Add(OrbKind.MOON_SOFT_1ST, main.settings[i].xmlData.orb_moon_soft_1st_0);
                o11.Add(OrbKind.MOON_HARD_2ND, main.settings[i].xmlData.orb_moon_hard_2nd_0);
                o11.Add(OrbKind.MOON_SOFT_2ND, main.settings[i].xmlData.orb_moon_soft_2nd_0);
                o11.Add(OrbKind.MOON_HARD_150, main.settings[i].xmlData.orb_moon_hard_150_0);
                o11.Add(OrbKind.MOON_SOFT_150, main.settings[i].xmlData.orb_moon_soft_150_0);
                o11.Add(OrbKind.OTHER_HARD_1ST, main.settings[i].xmlData.orb_other_hard_1st_0);
                o11.Add(OrbKind.OTHER_SOFT_1ST, main.settings[i].xmlData.orb_other_soft_1st_0);
                o11.Add(OrbKind.OTHER_HARD_2ND, main.settings[i].xmlData.orb_other_hard_2nd_0);
                o11.Add(OrbKind.OTHER_SOFT_2ND, main.settings[i].xmlData.orb_other_soft_2nd_0);
                o11.Add(OrbKind.OTHER_HARD_150, main.settings[i].xmlData.orb_other_hard_150_0);
                o11.Add(OrbKind.OTHER_SOFT_150, main.settings[i].xmlData.orb_other_soft_150_0);
                main.settings[i].orbs.Add(o11);
                #endregion
                Dictionary<OrbKind, double> o22 = new Dictionary<OrbKind, double>();
                #region o22
                o22.Add(OrbKind.SUN_HARD_1ST, main.settings[i].xmlData.orb_sun_hard_1st_1);
                o22.Add(OrbKind.SUN_SOFT_1ST, main.settings[i].xmlData.orb_sun_soft_1st_1);
                o22.Add(OrbKind.SUN_HARD_2ND, main.settings[i].xmlData.orb_sun_hard_2nd_1);
                o22.Add(OrbKind.SUN_SOFT_2ND, main.settings[i].xmlData.orb_sun_soft_2nd_1);
                o22.Add(OrbKind.SUN_HARD_150, main.settings[i].xmlData.orb_sun_hard_150_1);
                o22.Add(OrbKind.SUN_SOFT_150, main.settings[i].xmlData.orb_sun_soft_150_1);
                o22.Add(OrbKind.MOON_HARD_1ST, main.settings[i].xmlData.orb_moon_hard_1st_1);
                o22.Add(OrbKind.MOON_SOFT_1ST, main.settings[i].xmlData.orb_moon_soft_1st_1);
                o22.Add(OrbKind.MOON_HARD_2ND, main.settings[i].xmlData.orb_moon_hard_2nd_1);
                o22.Add(OrbKind.MOON_SOFT_2ND, main.settings[i].xmlData.orb_moon_soft_2nd_1);
                o22.Add(OrbKind.MOON_HARD_150, main.settings[i].xmlData.orb_moon_hard_150_1);
                o22.Add(OrbKind.MOON_SOFT_150, main.settings[i].xmlData.orb_moon_soft_150_1);
                o22.Add(OrbKind.OTHER_HARD_1ST, main.settings[i].xmlData.orb_other_hard_1st_1);
                o22.Add(OrbKind.OTHER_SOFT_1ST, main.settings[i].xmlData.orb_other_soft_1st_1);
                o22.Add(OrbKind.OTHER_HARD_2ND, main.settings[i].xmlData.orb_other_hard_2nd_1);
                o22.Add(OrbKind.OTHER_SOFT_2ND, main.settings[i].xmlData.orb_other_soft_2nd_1);
                o22.Add(OrbKind.OTHER_HARD_150, main.settings[i].xmlData.orb_other_hard_150_1);
                o22.Add(OrbKind.OTHER_SOFT_150, main.settings[i].xmlData.orb_other_soft_150_1);
                main.settings[i].orbs.Add(o22);
                #endregion
                Dictionary<OrbKind, double> o33 = new Dictionary<OrbKind, double>();
                #region o33
                o33.Add(OrbKind.SUN_HARD_1ST, main.settings[i].xmlData.orb_sun_hard_1st_2);
                o33.Add(OrbKind.SUN_SOFT_1ST, main.settings[i].xmlData.orb_sun_soft_1st_2);
                o33.Add(OrbKind.SUN_HARD_2ND, main.settings[i].xmlData.orb_sun_hard_2nd_2);
                o33.Add(OrbKind.SUN_SOFT_2ND, main.settings[i].xmlData.orb_sun_soft_2nd_2);
                o33.Add(OrbKind.SUN_HARD_150, main.settings[i].xmlData.orb_sun_hard_150_2);
                o33.Add(OrbKind.SUN_SOFT_150, main.settings[i].xmlData.orb_sun_soft_150_2);
                o33.Add(OrbKind.MOON_HARD_1ST, main.settings[i].xmlData.orb_moon_hard_1st_2);
                o33.Add(OrbKind.MOON_SOFT_1ST, main.settings[i].xmlData.orb_moon_soft_1st_2);
                o33.Add(OrbKind.MOON_HARD_2ND, main.settings[i].xmlData.orb_moon_hard_2nd_2);
                o33.Add(OrbKind.MOON_SOFT_2ND, main.settings[i].xmlData.orb_moon_soft_2nd_2);
                o33.Add(OrbKind.MOON_HARD_150, main.settings[i].xmlData.orb_moon_hard_150_2);
                o33.Add(OrbKind.MOON_SOFT_150, main.settings[i].xmlData.orb_moon_soft_150_2);
                o33.Add(OrbKind.OTHER_HARD_1ST, main.settings[i].xmlData.orb_other_hard_1st_2);
                o33.Add(OrbKind.OTHER_SOFT_1ST, main.settings[i].xmlData.orb_other_soft_1st_2);
                o33.Add(OrbKind.OTHER_HARD_2ND, main.settings[i].xmlData.orb_other_hard_2nd_2);
                o33.Add(OrbKind.OTHER_SOFT_2ND, main.settings[i].xmlData.orb_other_soft_2nd_2);
                o33.Add(OrbKind.OTHER_HARD_150, main.settings[i].xmlData.orb_other_hard_150_2);
                o33.Add(OrbKind.OTHER_SOFT_150, main.settings[i].xmlData.orb_other_soft_150_2);
                main.settings[i].orbs.Add(o33);
                #endregion
                Dictionary<OrbKind, double> o12 = new Dictionary<OrbKind, double>();
                #region o12
                o12.Add(OrbKind.SUN_HARD_1ST, main.settings[i].xmlData.orb_sun_hard_1st_3);
                o12.Add(OrbKind.SUN_SOFT_1ST, main.settings[i].xmlData.orb_sun_soft_1st_3);
                o12.Add(OrbKind.SUN_HARD_2ND, main.settings[i].xmlData.orb_sun_hard_2nd_3);
                o12.Add(OrbKind.SUN_SOFT_2ND, main.settings[i].xmlData.orb_sun_soft_2nd_3);
                o12.Add(OrbKind.SUN_HARD_150, main.settings[i].xmlData.orb_sun_hard_150_3);
                o12.Add(OrbKind.SUN_SOFT_150, main.settings[i].xmlData.orb_sun_soft_150_3);
                o12.Add(OrbKind.MOON_HARD_1ST, main.settings[i].xmlData.orb_moon_hard_1st_3);
                o12.Add(OrbKind.MOON_SOFT_1ST, main.settings[i].xmlData.orb_moon_soft_1st_3);
                o12.Add(OrbKind.MOON_HARD_2ND, main.settings[i].xmlData.orb_moon_hard_2nd_3);
                o12.Add(OrbKind.MOON_SOFT_2ND, main.settings[i].xmlData.orb_moon_soft_2nd_3);
                o12.Add(OrbKind.MOON_HARD_150, main.settings[i].xmlData.orb_moon_hard_150_3);
                o12.Add(OrbKind.MOON_SOFT_150, main.settings[i].xmlData.orb_moon_soft_150_3);
                o12.Add(OrbKind.OTHER_HARD_1ST, main.settings[i].xmlData.orb_other_hard_1st_3);
                o12.Add(OrbKind.OTHER_SOFT_1ST, main.settings[i].xmlData.orb_other_soft_1st_3);
                o12.Add(OrbKind.OTHER_HARD_2ND, main.settings[i].xmlData.orb_other_hard_2nd_3);
                o12.Add(OrbKind.OTHER_SOFT_2ND, main.settings[i].xmlData.orb_other_soft_2nd_3);
                o12.Add(OrbKind.OTHER_HARD_150, main.settings[i].xmlData.orb_other_hard_150_3);
                o12.Add(OrbKind.OTHER_SOFT_150, main.settings[i].xmlData.orb_other_soft_150_3);
                main.settings[i].orbs.Add(o12);
                #endregion
                Dictionary<OrbKind, double> o13 = new Dictionary<OrbKind, double>();
                #region o13
                o13.Add(OrbKind.SUN_HARD_1ST, main.settings[i].xmlData.orb_sun_hard_1st_4);
                o13.Add(OrbKind.SUN_SOFT_1ST, main.settings[i].xmlData.orb_sun_soft_1st_4);
                o13.Add(OrbKind.SUN_HARD_2ND, main.settings[i].xmlData.orb_sun_hard_2nd_4);
                o13.Add(OrbKind.SUN_SOFT_2ND, main.settings[i].xmlData.orb_sun_soft_2nd_4);
                o13.Add(OrbKind.SUN_HARD_150, main.settings[i].xmlData.orb_sun_hard_150_4);
                o13.Add(OrbKind.SUN_SOFT_150, main.settings[i].xmlData.orb_sun_soft_150_4);
                o13.Add(OrbKind.MOON_HARD_1ST, main.settings[i].xmlData.orb_moon_hard_1st_4);
                o13.Add(OrbKind.MOON_SOFT_1ST, main.settings[i].xmlData.orb_moon_soft_1st_4);
                o13.Add(OrbKind.MOON_HARD_2ND, main.settings[i].xmlData.orb_moon_hard_2nd_4);
                o13.Add(OrbKind.MOON_SOFT_2ND, main.settings[i].xmlData.orb_moon_soft_2nd_4);
                o13.Add(OrbKind.MOON_HARD_150, main.settings[i].xmlData.orb_moon_hard_150_4);
                o13.Add(OrbKind.MOON_SOFT_150, main.settings[i].xmlData.orb_moon_soft_150_4);
                o13.Add(OrbKind.OTHER_HARD_1ST, main.settings[i].xmlData.orb_other_hard_1st_4);
                o13.Add(OrbKind.OTHER_SOFT_1ST, main.settings[i].xmlData.orb_other_soft_1st_4);
                o13.Add(OrbKind.OTHER_HARD_2ND, main.settings[i].xmlData.orb_other_hard_2nd_4);
                o13.Add(OrbKind.OTHER_SOFT_2ND, main.settings[i].xmlData.orb_other_soft_2nd_4);
                o13.Add(OrbKind.OTHER_HARD_150, main.settings[i].xmlData.orb_other_hard_150_4);
                o13.Add(OrbKind.OTHER_SOFT_150, main.settings[i].xmlData.orb_other_soft_150_4);
                main.settings[i].orbs.Add(o13);
                #endregion
                Dictionary<OrbKind, double> o23 = new Dictionary<OrbKind, double>();
                #region o23
                o23.Add(OrbKind.SUN_HARD_1ST, main.settings[i].xmlData.orb_sun_hard_1st_5);
                o23.Add(OrbKind.SUN_SOFT_1ST, main.settings[i].xmlData.orb_sun_soft_1st_5);
                o23.Add(OrbKind.SUN_HARD_2ND, main.settings[i].xmlData.orb_sun_hard_2nd_5);
                o23.Add(OrbKind.SUN_SOFT_2ND, main.settings[i].xmlData.orb_sun_soft_2nd_5);
                o23.Add(OrbKind.SUN_HARD_150, main.settings[i].xmlData.orb_sun_hard_150_5);
                o23.Add(OrbKind.SUN_SOFT_150, main.settings[i].xmlData.orb_sun_soft_150_5);
                o23.Add(OrbKind.MOON_HARD_1ST, main.settings[i].xmlData.orb_moon_hard_1st_5);
                o23.Add(OrbKind.MOON_SOFT_1ST, main.settings[i].xmlData.orb_moon_soft_1st_5);
                o23.Add(OrbKind.MOON_HARD_2ND, main.settings[i].xmlData.orb_moon_hard_2nd_5);
                o23.Add(OrbKind.MOON_SOFT_2ND, main.settings[i].xmlData.orb_moon_soft_2nd_5);
                o23.Add(OrbKind.MOON_HARD_150, main.settings[i].xmlData.orb_moon_hard_150_5);
                o23.Add(OrbKind.MOON_SOFT_150, main.settings[i].xmlData.orb_moon_soft_150_5);
                o23.Add(OrbKind.OTHER_HARD_1ST, main.settings[i].xmlData.orb_other_hard_1st_5);
                o23.Add(OrbKind.OTHER_SOFT_1ST, main.settings[i].xmlData.orb_other_soft_1st_5);
                o23.Add(OrbKind.OTHER_HARD_2ND, main.settings[i].xmlData.orb_other_hard_2nd_5);
                o23.Add(OrbKind.OTHER_SOFT_2ND, main.settings[i].xmlData.orb_other_soft_2nd_5);
                o23.Add(OrbKind.OTHER_HARD_150, main.settings[i].xmlData.orb_other_hard_150_5);
                o23.Add(OrbKind.OTHER_SOFT_150, main.settings[i].xmlData.orb_other_soft_150_5);
                main.settings[i].orbs.Add(o23);
                #endregion

                main.settings[i].dispAspect[0, 0] = aspectDispChecked[currentIndex, 0];
                main.settings[i].dispAspect[1, 1] = aspectDispChecked[currentIndex, 1];
                main.settings[i].dispAspect[2, 2] = aspectDispChecked[currentIndex, 2];
                main.settings[i].dispAspect[0, 1] = aspectDispChecked[currentIndex, 3];
                main.settings[i].dispAspect[0, 2] = aspectDispChecked[currentIndex, 4];
                main.settings[i].dispAspect[1, 2] = aspectDispChecked[currentIndex, 5];

            }


            main.currentSetting = main.settings[currentIndex];


            main.ReCalc();
            main.ReRender();

            this.Visibility = Visibility.Hidden;
        }




        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }

        private void sunSoft1st_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            TextBox box = (TextBox)sender;
            int index = dispList.SelectedIndex;
            int orbIndex = orbRing.SelectedIndex;
            try
            {
                orbSunSoft1st[index, orbIndex] = double.Parse(box.Text);
            }
            catch (FormatException except)
            {
                Console.WriteLine(except.Message);
            }
        }

        private void sunHard1st_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            TextBox box = (TextBox)sender;
            int index = dispList.SelectedIndex;
            int orbIndex = orbRing.SelectedIndex;
            try
            {
                orbSunHard1st[index, orbIndex] = double.Parse(box.Text);
            }
            catch (FormatException except)
            {
                Console.WriteLine(except.Message);
            }
        }

        private void sunSoft150_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            TextBox box = (TextBox)sender;
            int index = dispList.SelectedIndex;
            int orbIndex = orbRing.SelectedIndex;
            try
            {
                orbSunSoft150[index, orbIndex] = double.Parse(box.Text);
            }
            catch (FormatException except)
            {
                Console.WriteLine(except.Message);
            }
        }

        private void sunHard150_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            TextBox box = (TextBox)sender;
            int index = dispList.SelectedIndex;
            int orbIndex = orbRing.SelectedIndex;
            try
            {
                orbSunHard150[index, orbIndex] = double.Parse(box.Text);
            }
            catch (FormatException except)
            {
                Console.WriteLine(except.Message);
            }
        }

        private void sunSoft2nd_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            TextBox box = (TextBox)sender;
            int index = dispList.SelectedIndex;
            int orbIndex = orbRing.SelectedIndex;
            try
            {
                orbSunSoft2nd[index, orbIndex] = double.Parse(box.Text);
            }
            catch (FormatException except)
            {
                Console.WriteLine(except.Message);
            }
        }

        private void sunHard2nd_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            TextBox box = (TextBox)sender;
            int index = dispList.SelectedIndex;
            int orbIndex = orbRing.SelectedIndex;
            try
            {
                orbSunHard2nd[index, orbIndex] = double.Parse(box.Text);
            }
            catch (FormatException except)
            {
                Console.WriteLine(except.Message);
            }
        }

        private void moonSoft1st_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            TextBox box = (TextBox)sender;
            int index = dispList.SelectedIndex;
            int orbIndex = orbRing.SelectedIndex;
            try
            {
                orbMoonSoft1st[index, orbIndex] = double.Parse(box.Text);
            }
            catch (FormatException except)
            {
                Console.WriteLine(except.Message);
            }
        }

        private void moonSoft150_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            TextBox box = (TextBox)sender;
            int index = dispList.SelectedIndex;
            int orbIndex = orbRing.SelectedIndex;
            try
            {
                orbMoonSoft150[index, orbIndex] = double.Parse(box.Text);
            }
            catch (FormatException except)
            {
                Console.WriteLine(except.Message);
            }
        }

        private void moonSoft2nd_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            TextBox box = (TextBox)sender;
            int index = dispList.SelectedIndex;
            int orbIndex = orbRing.SelectedIndex;
            try
            {
                orbMoonSoft2nd[index, orbIndex] = double.Parse(box.Text);
            }
            catch (FormatException except)
            {
                Console.WriteLine(except.Message);
            }
        }

        private void moonHard1st_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            TextBox box = (TextBox)sender;
            int index = dispList.SelectedIndex;
            int orbIndex = orbRing.SelectedIndex;
            try
            {
                orbMoonHard1st[index, orbIndex] = double.Parse(box.Text);
            }
            catch (FormatException except)
            {
                Console.WriteLine(except.Message);
            }
        }

        private void moonHard150_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            TextBox box = (TextBox)sender;
            int index = dispList.SelectedIndex;
            int orbIndex = orbRing.SelectedIndex;
            try
            {
                orbMoonHard150[index, orbIndex] = double.Parse(box.Text);
            }
            catch (FormatException except)
            {
                Console.WriteLine(except.Message);
            }
        }

        private void moonHard2nd_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            TextBox box = (TextBox)sender;
            int index = dispList.SelectedIndex;
            int orbIndex = orbRing.SelectedIndex;
            try
            {
                orbMoonHard2nd[index, orbIndex] = double.Parse(box.Text);
            }
            catch (FormatException except)
            {
                Console.WriteLine(except.Message);
            }
        }

        private void otherSoft1st_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            TextBox box = (TextBox)sender;
            int index = dispList.SelectedIndex;
            int orbIndex = orbRing.SelectedIndex;
            try
            {
                orbOtherSoft1st[index, orbIndex] = double.Parse(box.Text);
            }
            catch (FormatException except)
            {
                Console.WriteLine(except.Message);
            }
        }

        private void otherSoft150_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            TextBox box = (TextBox)sender;
            int index = dispList.SelectedIndex;
            int orbIndex = orbRing.SelectedIndex;
            try
            {
                orbOtherSoft150[index, orbIndex] = double.Parse(box.Text);
            }
            catch (FormatException except)
            {
                Console.WriteLine(except.Message);
            }
        }

        private void otherSoft2nd_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            TextBox box = (TextBox)sender;
            int index = dispList.SelectedIndex;
            int orbIndex = orbRing.SelectedIndex;
            try
            {
                orbOtherSoft2nd[index, orbIndex] = double.Parse(box.Text);
            }
            catch (FormatException except)
            {
                Console.WriteLine(except.Message);
            }
        }

        private void otherHard1st_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            TextBox box = (TextBox)sender;
            int index = dispList.SelectedIndex;
            int orbIndex = orbRing.SelectedIndex;
            try
            {
                orbOtherHard1st[index, orbIndex] = double.Parse(box.Text);
            }
            catch (FormatException except)
            {
                Console.WriteLine(except.Message);
            }
        }

        private void otherHard150_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            TextBox box = (TextBox)sender;
            int index = dispList.SelectedIndex;
            int orbIndex = orbRing.SelectedIndex;
            try
            {
                orbOtherHard150[index, orbIndex] = double.Parse(box.Text);
            }
            catch (FormatException except)
            {
                Console.WriteLine(except.Message);
            }
        }

        private void otherHard2nd_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            TextBox box = (TextBox)sender;
            int index = dispList.SelectedIndex;
            int orbIndex = orbRing.SelectedIndex;
            try
            {
                orbOtherHard2nd[index, orbIndex] = double.Parse(box.Text);
            }
            catch (FormatException except)
            {
                Console.WriteLine(except.Message);
            }
        }

        private void settingName_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            int index = dispList.SelectedIndex;
            tempDispName[index] = settingName.Text;
        }

        private void resetButton_Click(object sender, RoutedEventArgs e)
        {

            // 天体表示
            for (int i = 0; i < planetTargetNames.Count; i++)
            {
                // subIndex: 11～23を表す値
                int subIndex = planetDispControlTable[planetTargetNames[i]].subIndex;

                // dispList Counter
                for (int j = 0; j < 10; j++)
                {
                    int commonDataNo = planetDispControlTable[planetTargetNames[i]].commonDataNo;
                    if ((13 < i && i < 16) ||
                        (29 < i && i < 32) ||
                        (45 < i && i < 48))
                    {
                        planetDispControlTable[planetTargetNames[i]].tempArray[j, subIndex] = false;
                        // planetサポートで変わるよ
                        int index = i % 16;

                        // こういう書き方バグを生む
                        if (index == 10)
                        {
                            // true node
                            index = 11;
                        }
                        else if (index == 11)
                        {
                            // chiron
                            index = 15;
                        }
                        else if (index == 12)
                        {
                            // ASC
                            index = 10000;
                        }
                        else if (index == 13)
                        {
                            // MC
                            index = 10001;
                        }
                        else if (index == 14)
                        {
                            // Earth
                            index = 14;
                        }
                        else if (index == 15)
                        {
                            // Lilith
                            index = 13;
                        }

                        if (i < 16)
                        {
                            if (main.list1.ContainsKey(index)) 
                            {
                                main.list1[index].isDisp = false;
                            }
                        }
                        else if (i < 32)
                        {
                            if (main.list2.ContainsKey(index))
                            {
                                main.list2[index].isDisp = false;
                            }
                        }
                        else if (i < 48)
                        {
                            if (main.list3.ContainsKey(index))
                            {
                                main.list3[index].isDisp = false;
                            }
                        }
                    }
                    else
                    {
                        planetDispControlTable[planetTargetNames[i]].tempArray[j, subIndex] = true;
                        // planetサポートで変わるよ
                        int index = i % 16;
                        if (index == 10)
                        {
                            // true node
                            index = 11;
                        }
                        else if (index == 11)
                        {
                            // chiron
                            index = 15;
                        }
                        else if (index == 12)
                        {
                            // ASC
                            index = 10000;
                        }
                        else if (index == 13)
                        {
                            // MC
                            index = 10001;
                        }
                        else if (index == 14)
                        {
                            // Earth
                            index = 14;
                        }
                        else if (index == 15)
                        {
                            // Lilith
                            index = 13;
                        }
                        if (i < 16)
                        {
                            if (main.list1.ContainsKey(index))
                            {
                                main.list1[index].isDisp = false;
                            }
                        }
                        else if (i < 32)
                        {
                            if (main.list2.ContainsKey(index))
                            {
                                main.list2[index].isDisp = false;
                            }
                        }
                        else if (i < 48)
                        {
                            if (main.list3.ContainsKey(index))
                            {
                                main.list3[index].isDisp = false;
                            }
                        }
                    }
                }

            }

            // アスペクト設定（２）
            for (int i = 0; i < targetNames.Count; i++)
            {
                // subIndex: 11～23を表す値
                int subIndex = controlTable[targetNames[i]].subIndex;
                for (int j = 0; j < 10; j++)
                {
                    // commonNo: ipl
                    // tempArray: 設定切り替え時に一時的に保存しておく場所
                    int commonDataNo = controlTable[targetNames[i]].commonDataNo;
                    if (targetNames[i].IndexOf("aspectEarthOn") == 0 ||
                        targetNames[i].IndexOf("aspectLilithOn") == 0)
                    {
                        controlTable[targetNames[i]].tempArray[j, subIndex] = false;
                    }
                    else
                    {
                        controlTable[targetNames[i]].tempArray[j, subIndex] = true;
                    }
                }

            }

            // アスペクト設定（１）
            for (int i = 0; i < aspectTargetNames.Count; i++)
            {
                // subIndex: 11～23を表す値
                int subIndex = aspectControlTable[aspectTargetNames[i]].subIndex;
                for (int j = 0; j < 10; j++)
                {
                    // commonNo: ipl
                    // tempArray: 設定切り替え時に一時的に保存しておく場所
                    int commonDataNo = aspectControlTable[aspectTargetNames[i]].commonDataNo;
                    aspectControlTable[aspectTargetNames[i]].tempArray[j, subIndex] = true;
                }

            }

            for (int index = 0; index < 10; index++)
            {
                for (int orbIndex = 0; orbIndex < 6; orbIndex++)
                {
                    orbSunSoft1st[index, orbIndex] = 8;
                    orbSunHard1st[index, orbIndex] = 4;
                    orbSunSoft2nd[index, orbIndex] = 2;
                    orbSunHard2nd[index, orbIndex] = 1;
                    orbSunSoft150[index, orbIndex] = 3;
                    orbSunHard150[index, orbIndex] = 1.5;
                    orbMoonSoft1st[index, orbIndex] = 8;
                    orbMoonSoft1st[index, orbIndex] = 4;
                    orbMoonSoft2nd[index, orbIndex] = 3;
                    orbMoonHard2nd[index, orbIndex] = 1.5;
                    orbMoonSoft150[index, orbIndex] = 2;
                    orbMoonHard150[index, orbIndex] = 1;
                    orbOtherSoft1st[index, orbIndex] = 6;
                    orbOtherHard1st[index, orbIndex] = 4;
                    orbOtherSoft2nd[index, orbIndex] = 2;
                    orbOtherHard2nd[index, orbIndex] = 1;
                    orbOtherSoft150[index, orbIndex] = 1.5;
                    orbOtherHard150[index, orbIndex] = 1;
                }
            }

            ReRender(dispList);
            main.ReRender();

        }
    }
}
