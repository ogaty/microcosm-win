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
            this.main.tempSettings.bands = ringSelector.SelectedIndex + 2;
            main.ReCalc();
            main.ReRender();
            this.Visibility = Visibility.Hidden;

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;

        }
    }
}
