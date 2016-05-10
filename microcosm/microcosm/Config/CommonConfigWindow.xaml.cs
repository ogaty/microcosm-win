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
            if (main.config.progression == Progression.PRIMARY)
            {
                primaryProgression.IsChecked = true;
                secondaryProgression.IsChecked = false;
                compositProgression.IsChecked = false;
            }
            else if (main.config.progression == Progression.SECONDARY)
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
        }

        private void Centric_Checked(object sender, RoutedEventArgs e)
        {
        }

        private void OkClick(object sender, RoutedEventArgs e)
        {
            if (geoCentric.IsChecked == true)
            {
                main.config.centric = ECentric.GEO_CENTRIC;
            }
            else
            {
                main.config.centric = ECentric.HELIO_CENTRIC;
            }
            if (tropical.IsChecked == true)
            {
                main.config.sidereal = Esidereal.TROPICAL;
            }
            else
            {
                main.config.sidereal = Esidereal.SIDEREAL;
            }
            if (primaryProgression.IsChecked == true)
            {
                main.config.progression = Progression.PRIMARY;
            }
            else if (secondaryProgression.IsChecked == true)
            {
                main.config.progression = Progression.SECONDARY;
            }
            else
            {
                main.config.progression = Progression.CPS;
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
