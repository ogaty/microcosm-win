using System;
using System.Collections.Generic;
using System.IO;
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
using System.Xml.Serialization;

namespace microcosm.Config
{
    /// <summary>
    /// CommonConfigWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class CommonConfigWindow : Window
    {
        public MainWindow main;
        public CommonConfigWindow(MainWindow main)
        {
            this.main = main;
            InitializeComponent();

            if (main.config.centric == ECentric.GEO_CENTRIC)
            {
                geoCentric.IsChecked = true;
            } else
            {
                helioCentric.IsChecked = true;
            }
            if (main.config.sidereal == Esidereal.TROPICAL)
            {
                tropical.IsChecked = true;
            }
            else
            {
                sidereal.IsChecked = true;
            }
            if (main.config.progression == EProgression.PRIMARY)
            {
                primaryProgression.IsChecked = true;
                secondaryProgression.IsChecked = false;
                compositProgression.IsChecked = false;
            }
            else if (main.config.progression == EProgression.SECONDARY)
            {
                primaryProgression.IsChecked = false;
                secondaryProgression.IsChecked = true;
                compositProgression.IsChecked = false;
            }
            else
            {
                primaryProgression.IsChecked = false;
                secondaryProgression.IsChecked = false;
                compositProgression.IsChecked = true;
            }
            if (main.config.decimalDisp == (int)EDecimalDisp.DECIMAL)
            {
                decimalDisp.IsChecked = true;
                degreeDisp.IsChecked = false;
            }
            else
            {
                decimalDisp.IsChecked = false;
                degreeDisp.IsChecked = true;
            }
            if (main.config.dispPattern == 0)
            {
                fullDisp.IsChecked = true;
                miniDisp.IsChecked = false;
            }
            else
            {
                fullDisp.IsChecked = false;
                miniDisp.IsChecked = true;
            }
        }

        private void Centric_Checked(object sender, RoutedEventArgs e)
        {
        }

        // 保存
        private void OkClick(object sender, RoutedEventArgs e)
        {
            if (geoCentric.IsChecked == true)
            {
                main.config.centric = ECentric.GEO_CENTRIC;
                main.mainWindowVM.centricMode = "GeoCentric";
            }
            else
            {
                main.config.centric = ECentric.HELIO_CENTRIC;
                main.mainWindowVM.centricMode = "HelioCentric";
            }
            if (tropical.IsChecked == true)
            {
                main.config.sidereal = Esidereal.TROPICAL;
                main.mainWindowVM.siderealStr = "TROPICAL";
            }
            else
            {
                main.config.sidereal = Esidereal.SIDEREAL;
                main.mainWindowVM.siderealStr = "SIDEREAL";
            }
            if (primaryProgression.IsChecked == true)
            {
                main.config.progression = EProgression.PRIMARY;
                main.mainWindowVM.progressionCalc = "一度一年法";
            }
            else if (secondaryProgression.IsChecked == true)
            {
                main.config.progression = EProgression.SECONDARY;
                main.mainWindowVM.progressionCalc = "一日一年法";
            }
            else
            {
                main.config.progression = EProgression.CPS;
                main.mainWindowVM.progressionCalc = "CPS";
            }
            if (decimalDisp.IsChecked == true)
            {
                main.config.decimalDisp = EDecimalDisp.DECIMAL;
            }
            else
            {
                main.config.decimalDisp = EDecimalDisp.DEGREE;
            }
            if (fullDisp.IsChecked == true)
            {
                main.config.dispPattern = (int)DispPetern.FULL;
                main.tempSettings.centerPattern = (int)DispPetern.FULL;
            }
            else
            {
                main.config.dispPattern = (int)DispPetern.MINI;
                main.tempSettings.centerPattern = (int)DispPetern.MINI;
            }
            if (placidus.IsChecked == true)
            {
                main.config.houseCalc = EHouseCalc.PLACIDUS;
                main.houseCalc.Text = "PLACIDUS";
            }
            else if (koch.IsChecked == true)
            {
                main.config.houseCalc = EHouseCalc.KOCH;
                main.houseCalc.Text = "KOCH";
            }
            else if (campanus.IsChecked == true)
            {
                main.config.houseCalc = EHouseCalc.CAMPANUS;
                main.houseCalc.Text = "CAMPANUS";
            }
            else if (equal.IsChecked == true)
            {
                main.config.houseCalc = EHouseCalc.EQUAL;
                main.houseCalc.Text = "EQUAL";
            }

            string filename = @"system\config.csm";
            XmlSerializer serializer = new XmlSerializer(typeof(ConfigData));
            FileStream fs = new FileStream(filename, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            serializer.Serialize(sw, main.config);
            sw.Close();
            fs.Close();

            main.ReCalc();
            main.ReRender();

            this.Visibility = Visibility.Hidden;
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Hidden;
        }

        private void Equinox_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
