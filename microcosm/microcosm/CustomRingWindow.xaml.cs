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

using microcosm.Config;

namespace microcosm
{
    /// <summary>
    /// CustomRing.xaml の相互作用ロジック
    /// </summary>
    public partial class CustomRingWindow : Window
    {
        public MainWindow main;
        public CustomRingWindow(MainWindow main)
        {
            this.main = main;
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Hidden;
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            this.main.tempSettings.bands = ringSelector.SelectedIndex + 1;
            if (ringSelector.SelectedIndex == 0)
            {
                // 一重円
                switch (ring1.SelectedIndex)
                {
                    case 0:
                        main.tempSettings.firstBand = TempSetting.BandKind.NATAL;
                        break;
                    case 1:
                        main.tempSettings.firstBand = TempSetting.BandKind.PROGRESS;
                        break;
                    case 2:
                        main.tempSettings.firstBand = TempSetting.BandKind.TRANSIT;
                        break;
                    default:
                        main.tempSettings.firstBand = TempSetting.BandKind.TRANSIT;
                        break;
                }
                switch (house1.SelectedIndex)
                {
                    case 0:
                        main.tempSettings.firstHouseDiv = TempSetting.HouseDivide.USER1;
                        break;
                    case 1:
                        main.tempSettings.firstHouseDiv = TempSetting.HouseDivide.EVENT1;
                        break;
                    case 2:
                        main.tempSettings.firstHouseDiv = TempSetting.HouseDivide.PROGRESS;
                        break;
                    default:
                        main.tempSettings.firstHouseDiv = TempSetting.HouseDivide.USER1;
                        break;
                }
            }
            else if (ringSelector.SelectedIndex == 1)
            {
                // 二重円
                switch (ring1.SelectedIndex)
                {
                    case 0:
                        main.tempSettings.firstBand = TempSetting.BandKind.NATAL;
                        break;
                    case 1:
                        main.tempSettings.firstBand = TempSetting.BandKind.PROGRESS;
                        break;
                    case 2:
                        main.tempSettings.firstBand = TempSetting.BandKind.TRANSIT;
                        break;
                    default:
                        main.tempSettings.firstBand = TempSetting.BandKind.TRANSIT;
                        break;
                }
                switch (house1.SelectedIndex)
                {
                    case 0:
                        main.tempSettings.firstHouseDiv = TempSetting.HouseDivide.USER1;
                        break;
                    case 1:
                        main.tempSettings.firstHouseDiv = TempSetting.HouseDivide.EVENT1;
                        break;
                    case 2:
                        main.tempSettings.firstHouseDiv = TempSetting.HouseDivide.PROGRESS;
                        break;
                    default:
                        main.tempSettings.firstHouseDiv = TempSetting.HouseDivide.USER1;
                        break;
                }

                switch (ring2.SelectedIndex)
                {
                    case 0:
                        main.tempSettings.secondBand = TempSetting.BandKind.NATAL;
                        break;
                    case 1:
                        main.tempSettings.secondBand = TempSetting.BandKind.PROGRESS;
                        break;
                    case 2:
                        main.tempSettings.secondBand = TempSetting.BandKind.TRANSIT;
                        break;
                    default:
                        main.tempSettings.secondBand = TempSetting.BandKind.TRANSIT;
                        break;
                }
                switch (house2.SelectedIndex)
                {
                    case 0:
                        main.tempSettings.secondHouseDiv = TempSetting.HouseDivide.USER1;
                        break;
                    case 1:
                        main.tempSettings.secondHouseDiv = TempSetting.HouseDivide.EVENT1;
                        break;
                    case 2:
                        main.tempSettings.secondHouseDiv = TempSetting.HouseDivide.PROGRESS;
                        break;
                    default:
                        main.tempSettings.secondHouseDiv = TempSetting.HouseDivide.USER1;
                        break;
                }
            }

            else if (ringSelector.SelectedIndex == 2)
            {
                // 三重円
                switch (ring1.SelectedIndex)
                {
                    case 0:
                        main.tempSettings.firstBand = TempSetting.BandKind.NATAL;
                        break;
                    case 1:
                        main.tempSettings.firstBand = TempSetting.BandKind.PROGRESS;
                        break;
                    case 2:
                        main.tempSettings.firstBand = TempSetting.BandKind.TRANSIT;
                        break;
                    default:
                        main.tempSettings.firstBand = TempSetting.BandKind.TRANSIT;
                        break;
                }
                switch (house1.SelectedIndex)
                {
                    case 0:
                        main.tempSettings.firstHouseDiv = TempSetting.HouseDivide.USER1;
                        break;
                    case 1:
                        main.tempSettings.firstHouseDiv = TempSetting.HouseDivide.EVENT1;
                        break;
                    case 2:
                        main.tempSettings.firstHouseDiv = TempSetting.HouseDivide.PROGRESS;
                        break;
                    default:
                        main.tempSettings.firstHouseDiv = TempSetting.HouseDivide.USER1;
                        break;
                }

                switch (ring2.SelectedIndex)
                {
                    case 0:
                        main.tempSettings.secondBand = TempSetting.BandKind.NATAL;
                        break;
                    case 1:
                        main.tempSettings.secondBand = TempSetting.BandKind.PROGRESS;
                        break;
                    case 2:
                        main.tempSettings.secondBand = TempSetting.BandKind.TRANSIT;
                        break;
                    default:
                        main.tempSettings.secondBand = TempSetting.BandKind.TRANSIT;
                        break;
                }
                switch (house2.SelectedIndex)
                {
                    case 0:
                        main.tempSettings.secondHouseDiv = TempSetting.HouseDivide.USER1;
                        break;
                    case 1:
                        main.tempSettings.secondHouseDiv = TempSetting.HouseDivide.EVENT1;
                        break;
                    case 2:
                        main.tempSettings.secondHouseDiv = TempSetting.HouseDivide.PROGRESS;
                        break;
                    default:
                        main.tempSettings.secondHouseDiv = TempSetting.HouseDivide.USER1;
                        break;
                }

                switch (ring3.SelectedIndex)
                {
                    case 0:
                        main.tempSettings.thirdBand = TempSetting.BandKind.NATAL;
                        break;
                    case 1:
                        main.tempSettings.thirdBand = TempSetting.BandKind.PROGRESS;
                        break;
                    case 2:
                        main.tempSettings.thirdBand = TempSetting.BandKind.TRANSIT;
                        break;
                    default:
                        main.tempSettings.thirdBand = TempSetting.BandKind.TRANSIT;
                        break;
                }
                switch (house3.SelectedIndex)
                {
                    case 0:
                        main.tempSettings.thirdHouseDiv = TempSetting.HouseDivide.USER1;
                        break;
                    case 1:
                        main.tempSettings.thirdHouseDiv = TempSetting.HouseDivide.EVENT1;
                        break;
                    case 2:
                        main.tempSettings.thirdHouseDiv = TempSetting.HouseDivide.PROGRESS;
                        break;
                    default:
                        main.tempSettings.thirdHouseDiv = TempSetting.HouseDivide.USER1;
                        break;
                }
            }
            main.ReCalc();
            main.ReRender();
            this.Visibility = Visibility.Hidden;

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;

        }

        private void ringSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ring1 == null)
            {
                return;
            }
            int index = ringSelector.SelectedIndex;
            switch (index)
            {
                case 0:
                    no2.Visibility = Visibility.Hidden;
                    no3.Visibility = Visibility.Hidden;
                    break;
                case 1:
                    no2.Visibility = Visibility.Visible;
                    no3.Visibility = Visibility.Hidden;
                    break;
                case 2:
                    no2.Visibility = Visibility.Visible;
                    no3.Visibility = Visibility.Visible;
                    break;
                default:
                    break;
            }
        }
    }
}
