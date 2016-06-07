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
    /// ChartSelectorWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class ChartSelectorWindow : Window
    {
        public MainWindow main;
        public ChartSelectorWindow(MainWindow main)
        {
            this.main = main;
            InitializeComponent();
            setYear.Text = DateTime.Now.Year.ToString();
            setMonth.Text = DateTime.Now.Month.ToString();
            setDay.Text = DateTime.Now.Day.ToString();
            setHour.Text = DateTime.Now.Hour.ToString();
            setMinute.Text = DateTime.Now.Minute.ToString();
            setSecond.Text = DateTime.Now.Second.ToString();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Hidden;
        }

        private void LeftYear_Click(object sender, RoutedEventArgs e)
        {
            int count = int.Parse(unitYear.Text);
            int year = int.Parse(setYear.Text);
            setYear.Text = (year - count).ToString();
            main.targetUser.birth_year -= count;
            main.mainWindowVM.userBirthStr = setYear.Text + "年" + setMonth.Text + "月" + setDay.Text + "日 " + 
                setHour.Text + ":" + setMinute.Text + ":" + setSecond.Text;
            main.ReCalc();
            main.ReRender();
        }

        private void RightYear_Click(object sender, RoutedEventArgs e)
        {
            int count = int.Parse(unitYear.Text);
            int year = int.Parse(setYear.Text);
            setYear.Text = (year + count).ToString();
            main.targetUser.birth_year += count;
            main.mainWindowVM.userBirthStr = setYear.Text + "年" + setMonth.Text + "月" + setDay.Text + "日 " +
                setHour.Text + ":" + setMinute.Text + ":" + setSecond.Text;
            main.ReCalc();
            main.ReRender();
        }
    }
}
