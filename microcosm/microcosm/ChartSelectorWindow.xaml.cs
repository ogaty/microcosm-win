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
            if (natalTime.IsChecked == true)
            {
                DateTime dt = new DateTime(int.Parse(setYear.Text),
                    int.Parse(setMonth.Text),
                    int.Parse(setDay.Text),
                    int.Parse(setHour.Text),
                    int.Parse(setMinute.Text),
                    int.Parse(setSecond.Text));
                DateTime newDt = dt.AddYears(-1 * count);
                setDay.Text = newDt.Day.ToString();
                setMonth.Text = newDt.Month.ToString();
                setYear.Text = newDt.Year.ToString();
                setHour.Text = newDt.Hour.ToString();
                setMinute.Text = newDt.Minute.ToString();
                setSecond.Text = newDt.Second.ToString();
                main.targetUser.birth_day = newDt.Day;
                main.targetUser.birth_month = newDt.Month;
                main.targetUser.birth_year = newDt.Year;
                main.mainWindowVM.userBirthStr = setYear.Text + "年" + setMonth.Text + "月" + setDay.Text + "日 " +
                    setHour.Text + ":" + setMinute.Text + ":" + setSecond.Text;
            }
            else
            {
                DateTime dt = new DateTime(int.Parse(setYear.Text),
                    int.Parse(setMonth.Text),
                    int.Parse(setDay.Text),
                    int.Parse(setHour.Text),
                    int.Parse(setMinute.Text),
                    int.Parse(setSecond.Text));
                DateTime newDt = dt.AddYears(-1 * count);
                setDay.Text = newDt.Day.ToString();
                setMonth.Text = newDt.Month.ToString();
                setYear.Text = newDt.Year.ToString();
                setHour.Text = newDt.Hour.ToString();
                setMinute.Text = newDt.Minute.ToString();
                setSecond.Text = newDt.Second.ToString();
                main.userdata.birth_day = newDt.Day;
                main.userdata.birth_month = newDt.Month;
                main.userdata.birth_year = newDt.Year;
                main.mainWindowVM.transitBirthStr = setYear.Text + "年" + setMonth.Text + "月" + setDay.Text + "日 " +
                    setHour.Text + ":" + setMinute.Text + ":" + setSecond.Text;
            }
            main.ReCalc();
            main.ReRender();

        }

        private void RightYear_Click(object sender, RoutedEventArgs e)
        {
            int count = int.Parse(unitYear.Text);
            int year = int.Parse(setYear.Text);
            if (natalTime.IsChecked == true)
            {
                DateTime dt = new DateTime(int.Parse(setYear.Text),
                    int.Parse(setMonth.Text),
                    int.Parse(setDay.Text),
                    int.Parse(setHour.Text),
                    int.Parse(setMinute.Text),
                    int.Parse(setSecond.Text));
                DateTime newDt = dt.AddYears(count);
                setDay.Text = newDt.Day.ToString();
                setMonth.Text = newDt.Month.ToString();
                setYear.Text = newDt.Year.ToString();
                setHour.Text = newDt.Hour.ToString();
                setMinute.Text = newDt.Minute.ToString();
                setSecond.Text = newDt.Second.ToString();
                main.targetUser.birth_day = newDt.Day;
                main.targetUser.birth_month = newDt.Month;
                main.targetUser.birth_year = newDt.Year;

                main.mainWindowVM.userBirthStr = setYear.Text + "年" + setMonth.Text + "月" + setDay.Text + "日 " +
                    setHour.Text + ":" + setMinute.Text + ":" + setSecond.Text;
            }
            else
            {
                DateTime dt = new DateTime(int.Parse(setYear.Text),
                    int.Parse(setMonth.Text),
                    int.Parse(setDay.Text),
                    int.Parse(setHour.Text),
                    int.Parse(setMinute.Text),
                    int.Parse(setSecond.Text));
                DateTime newDt = dt.AddYears(count);
                setDay.Text = newDt.Day.ToString();
                setMonth.Text = newDt.Month.ToString();
                setYear.Text = newDt.Year.ToString();
                setHour.Text = newDt.Hour.ToString();
                setMinute.Text = newDt.Minute.ToString();
                setSecond.Text = newDt.Second.ToString();
                main.userdata.birth_day = newDt.Day;
                main.userdata.birth_month = newDt.Month;
                main.userdata.birth_year = newDt.Year;

                main.mainWindowVM.transitBirthStr = setYear.Text + "年" + setMonth.Text + "月" + setDay.Text + "日 " +
                    setHour.Text + ":" + setMinute.Text + ":" + setSecond.Text;
            }
            main.ReCalc();
            main.ReRender();
        }

        private void natalTime_Checked(object sender, RoutedEventArgs e)
        {
            if (setYear == null)
            {
                // initialize前に呼ばれてしまうのでリターン
                return;
            }
            setYear.Text = main.targetUser.birth_year.ToString();
            setMonth.Text = main.targetUser.birth_month.ToString();
            setDay.Text = main.targetUser.birth_day.ToString();
            setHour.Text = main.targetUser.birth_hour.ToString();
            setMinute.Text = main.targetUser.birth_minute.ToString();
            setSecond.Text = main.targetUser.birth_second.ToString();
        }

        private void transitTime_Checked(object sender, RoutedEventArgs e)
        {
            setYear.Text = main.userdata.birth_year.ToString();
            setMonth.Text = main.userdata.birth_month.ToString();
            setDay.Text = main.userdata.birth_day.ToString();
            setHour.Text = main.userdata.birth_hour.ToString();
            setMinute.Text = main.userdata.birth_minute.ToString();
            setSecond.Text = main.userdata.birth_second.ToString();

        }

        private void timeSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (setYear == null)
            {
                // initialize前に呼ばれてしまうのでリターン
                return;
            }
            ComboBox item = (ComboBox)sender;
            int index = item.SelectedIndex;
            switch (index)
            {
                case 0:
                    // 1時間
                    unitYear.Text = "0";
                    unitMonth.Text = "0";
                    unitDay.Text = "0";
                    unitHour.Text = "1";
                    unitMinute.Text = "0";
                    unitSecond.Text = "0";
                    break;
                case 1:
                    // 1日
                    unitYear.Text = "0";
                    unitMonth.Text = "0";
                    unitDay.Text = "1";
                    unitHour.Text = "0";
                    unitMinute.Text = "0";
                    unitSecond.Text = "0";
                    break;
                case 2:
                    // 7日
                    unitYear.Text = "0";
                    unitMonth.Text = "0";
                    unitDay.Text = "7";
                    unitHour.Text = "0";
                    unitMinute.Text = "0";
                    unitSecond.Text = "0";
                    break;
                case 3:
                    // 30日
                    unitYear.Text = "0";
                    unitMonth.Text = "0";
                    unitDay.Text = "30";
                    unitHour.Text = "0";
                    unitMinute.Text = "0";
                    unitSecond.Text = "0";
                    break;
                case 4:
                    // 365日
                    unitYear.Text = "0";
                    unitMonth.Text = "0";
                    unitDay.Text = "365";
                    unitHour.Text = "0";
                    unitMinute.Text = "0";
                    unitSecond.Text = "0";
                    break;
                default:
                    break;
            }
        }

        private void LeftMonth_Click(object sender, RoutedEventArgs e)
        {
            int count = int.Parse(unitMonth.Text);
            int month = int.Parse(setMonth.Text);
            if (natalTime.IsChecked == true)
            {
                DateTime dt = new DateTime(int.Parse(setYear.Text),
                    int.Parse(setMonth.Text),
                    int.Parse(setDay.Text),
                    int.Parse(setHour.Text),
                    int.Parse(setMinute.Text),
                    int.Parse(setSecond.Text));
                DateTime newDt = dt.AddMonths(-1 * count);
                setDay.Text = newDt.Day.ToString();
                setMonth.Text = newDt.Month.ToString();
                setYear.Text = newDt.Year.ToString();
                main.targetUser.birth_day = newDt.Day;
                main.targetUser.birth_month = newDt.Month;
                main.targetUser.birth_year = newDt.Year;
                main.mainWindowVM.userBirthStr = setYear.Text + "年" + setMonth.Text + "月" + setDay.Text + "日 " +
                    setHour.Text + ":" + setMinute.Text + ":" + setSecond.Text;
            }
            else
            {
                DateTime dt = new DateTime(int.Parse(setYear.Text),
                    int.Parse(setMonth.Text),
                    int.Parse(setDay.Text),
                    int.Parse(setHour.Text),
                    int.Parse(setMinute.Text),
                    int.Parse(setSecond.Text));
                DateTime newDt = dt.AddMonths(-1 * count);
                setDay.Text = newDt.Day.ToString();
                setMonth.Text = newDt.Month.ToString();
                setYear.Text = newDt.Year.ToString();
                main.userdata.birth_day = newDt.Day;
                main.userdata.birth_month = newDt.Month;
                main.userdata.birth_year = newDt.Year;
                main.mainWindowVM.transitBirthStr = setYear.Text + "年" + setMonth.Text + "月" + setDay.Text + "日 " +
                    setHour.Text + ":" + setMinute.Text + ":" + setSecond.Text;
            }
            main.ReCalc();
            main.ReRender();
        }

        private void RightMonth_Click(object sender, RoutedEventArgs e)
        {
            int count = int.Parse(unitMonth.Text);
            if (natalTime.IsChecked == true)
            {
                DateTime dt = new DateTime(int.Parse(setYear.Text),
                    int.Parse(setMonth.Text),
                    int.Parse(setDay.Text),
                    int.Parse(setHour.Text),
                    int.Parse(setMinute.Text),
                    int.Parse(setSecond.Text));
                DateTime newDt = dt.AddMonths(count);
                setMonth.Text = newDt.Month.ToString();
                setYear.Text = newDt.Year.ToString();
                main.targetUser.birth_month = newDt.Month;
                main.targetUser.birth_year = newDt.Year;
                main.mainWindowVM.userBirthStr = setYear.Text + "年" + setMonth.Text + "月" + setDay.Text + "日 " +
                    setHour.Text + ":" + setMinute.Text + ":" + setSecond.Text;
            }
            else
            {
                DateTime dt = new DateTime(int.Parse(setYear.Text),
                    int.Parse(setMonth.Text),
                    int.Parse(setDay.Text),
                    int.Parse(setHour.Text),
                    int.Parse(setMinute.Text),
                    int.Parse(setSecond.Text));
                DateTime newDt = dt.AddMonths(count);
                setMonth.Text = newDt.Month.ToString();
                setYear.Text = newDt.Year.ToString();
                main.userdata.birth_month = newDt.Month;
                main.userdata.birth_year = newDt.Year;
                main.mainWindowVM.transitBirthStr = setYear.Text + "年" + setMonth.Text + "月" + setDay.Text + "日 " +
                    setHour.Text + ":" + setMinute.Text + ":" + setSecond.Text;
            }
            main.ReCalc();
            main.ReRender();
        }

        private void LeftDay_Click(object sender, RoutedEventArgs e)
        {
            int count = int.Parse(unitDay.Text);
            int month = int.Parse(setDay.Text);
            if (natalTime.IsChecked == true)
            {
                DateTime dt = new DateTime(int.Parse(setYear.Text),
                    int.Parse(setMonth.Text),
                    int.Parse(setDay.Text),
                    int.Parse(setHour.Text),
                    int.Parse(setMinute.Text),
                    int.Parse(setSecond.Text));
                DateTime newDt = dt.AddDays(-1 * count);
                setDay.Text = newDt.Day.ToString();
                setMonth.Text = newDt.Month.ToString();
                setYear.Text = newDt.Year.ToString();
                main.targetUser.birth_day = newDt.Day;
                main.targetUser.birth_month = newDt.Month;
                main.targetUser.birth_year = newDt.Year;
                main.mainWindowVM.userBirthStr = setYear.Text + "年" + setMonth.Text + "月" + setDay.Text + "日 " +
                    setHour.Text + ":" + setMinute.Text + ":" + setSecond.Text;
            }
            else
            {
                DateTime dt = new DateTime(int.Parse(setYear.Text),
                    int.Parse(setMonth.Text),
                    int.Parse(setDay.Text),
                    int.Parse(setHour.Text),
                    int.Parse(setMinute.Text),
                    int.Parse(setSecond.Text));
                DateTime newDt = dt.AddDays(-1 * count);
                setDay.Text = newDt.Day.ToString();
                setMonth.Text = newDt.Month.ToString();
                setYear.Text = newDt.Year.ToString();
                main.userdata.birth_day = newDt.Day;
                main.userdata.birth_month = newDt.Month;
                main.userdata.birth_year = newDt.Year;
                main.mainWindowVM.transitBirthStr = setYear.Text + "年" + setMonth.Text + "月" + setDay.Text + "日 " +
                    setHour.Text + ":" + setMinute.Text + ":" + setSecond.Text;
            }
            main.ReCalc();
            main.ReRender();
        }

        private void RightDay_Click(object sender, RoutedEventArgs e)
        {
            int count = int.Parse(unitDay.Text);
            int day = int.Parse(setDay.Text);
            if (natalTime.IsChecked == true)
            {
                DateTime dt = new DateTime(int.Parse(setYear.Text),
                    int.Parse(setMonth.Text),
                    int.Parse(setDay.Text),
                    int.Parse(setHour.Text),
                    int.Parse(setMinute.Text),
                    int.Parse(setSecond.Text));
                DateTime newDt = dt.AddDays(count);
                setDay.Text = newDt.Day.ToString();
                setMonth.Text = newDt.Month.ToString();
                setYear.Text = newDt.Year.ToString();
                main.targetUser.birth_day = newDt.Day;
                main.targetUser.birth_month = newDt.Month;
                main.targetUser.birth_year = newDt.Year;
                main.mainWindowVM.userBirthStr = setYear.Text + "年" + setMonth.Text + "月" + setDay.Text + "日 " +
                    setHour.Text + ":" + setMinute.Text + ":" + setSecond.Text;
            }
            else
            {
                DateTime dt = new DateTime(int.Parse(setYear.Text),
                    int.Parse(setMonth.Text),
                    int.Parse(setDay.Text),
                    int.Parse(setHour.Text),
                    int.Parse(setMinute.Text),
                    int.Parse(setSecond.Text));
                DateTime newDt = dt.AddDays(count);
                setDay.Text = newDt.Day.ToString();
                setMonth.Text = newDt.Month.ToString();
                setYear.Text = newDt.Year.ToString();
                main.userdata.birth_day = newDt.Day;
                main.userdata.birth_month = newDt.Month;
                main.userdata.birth_year = newDt.Year;
                main.mainWindowVM.transitBirthStr = setYear.Text + "年" + setMonth.Text + "月" + setDay.Text + "日 " +
                    setHour.Text + ":" + setMinute.Text + ":" + setSecond.Text;
            }
            main.ReCalc();
            main.ReRender();
        }

        private void LeftHour_Click(object sender, RoutedEventArgs e)
        {
            int count = int.Parse(unitHour.Text);
            int hour = int.Parse(setHour.Text);
            if (natalTime.IsChecked == true)
            {
                DateTime dt = new DateTime(int.Parse(setYear.Text),
                    int.Parse(setMonth.Text),
                    int.Parse(setDay.Text),
                    int.Parse(setHour.Text),
                    int.Parse(setMinute.Text),
                    int.Parse(setSecond.Text));
                DateTime newDt = dt.AddHours(-1 * count);
                setDay.Text = newDt.Day.ToString();
                setMonth.Text = newDt.Month.ToString();
                setYear.Text = newDt.Year.ToString();
                setHour.Text = newDt.Hour.ToString();
                main.targetUser.birth_day = newDt.Day;
                main.targetUser.birth_month = newDt.Month;
                main.targetUser.birth_year = newDt.Year;
                main.targetUser.birth_hour = newDt.Hour;
                main.mainWindowVM.userBirthStr = setYear.Text + "年" + setMonth.Text + "月" + setDay.Text + "日 " +
                    setHour.Text + ":" + setMinute.Text + ":" + setSecond.Text;
            }
            else
            {
                DateTime dt = new DateTime(int.Parse(setYear.Text),
                    int.Parse(setMonth.Text),
                    int.Parse(setDay.Text),
                    int.Parse(setHour.Text),
                    int.Parse(setMinute.Text),
                    int.Parse(setSecond.Text));
                DateTime newDt = dt.AddHours(-1 * count);
                setDay.Text = newDt.Day.ToString();
                setMonth.Text = newDt.Month.ToString();
                setYear.Text = newDt.Year.ToString();
                setHour.Text = newDt.Hour.ToString();
                main.userdata.birth_day = newDt.Day;
                main.userdata.birth_month = newDt.Month;
                main.userdata.birth_year = newDt.Year;
                main.userdata.birth_hour = newDt.Hour;
                main.mainWindowVM.transitBirthStr = setYear.Text + "年" + setMonth.Text + "月" + setDay.Text + "日 " +
                    setHour.Text + ":" + setMinute.Text + ":" + setSecond.Text;
            }
            main.ReCalc();
            main.ReRender();
        }

        private void RightHour_Click(object sender, RoutedEventArgs e)
        {
            int count = int.Parse(unitHour.Text);
            int hour = int.Parse(setHour.Text);
            if (natalTime.IsChecked == true)
            {
                DateTime dt = new DateTime(int.Parse(setYear.Text),
                    int.Parse(setMonth.Text),
                    int.Parse(setDay.Text),
                    int.Parse(setHour.Text),
                    int.Parse(setMinute.Text),
                    int.Parse(setSecond.Text));
                DateTime newDt = dt.AddHours(count);
                setDay.Text = newDt.Day.ToString();
                setMonth.Text = newDt.Month.ToString();
                setYear.Text = newDt.Year.ToString();
                setHour.Text = newDt.Hour.ToString();
                setMinute.Text = newDt.Minute.ToString();
                setSecond.Text = newDt.Second.ToString();
                main.targetUser.birth_day = newDt.Day;
                main.targetUser.birth_month = newDt.Month;
                main.targetUser.birth_year = newDt.Year;
                main.mainWindowVM.userBirthStr = setYear.Text + "年" + setMonth.Text + "月" + setDay.Text + "日 " +
                    setHour.Text + ":" + setMinute.Text + ":" + setSecond.Text;
            }
            else
            {
                DateTime dt = new DateTime(int.Parse(setYear.Text),
                    int.Parse(setMonth.Text),
                    int.Parse(setDay.Text),
                    int.Parse(setHour.Text),
                    int.Parse(setMinute.Text),
                    int.Parse(setSecond.Text));
                DateTime newDt = dt.AddHours(count);
                setDay.Text = newDt.Day.ToString();
                setMonth.Text = newDt.Month.ToString();
                setYear.Text = newDt.Year.ToString();
                setHour.Text = newDt.Hour.ToString();
                setMinute.Text = newDt.Minute.ToString();
                setSecond.Text = newDt.Second.ToString();
                main.userdata.birth_day = newDt.Day;
                main.userdata.birth_month = newDt.Month;
                main.userdata.birth_year = newDt.Year;
                main.mainWindowVM.transitBirthStr = setYear.Text + "年" + setMonth.Text + "月" + setDay.Text + "日 " +
                    setHour.Text + ":" + setMinute.Text + ":" + setSecond.Text;
            }
            main.ReCalc();
            main.ReRender();
        }

        private void LeftMinute_Click(object sender, RoutedEventArgs e)
        {
            int count = int.Parse(unitMinute.Text);
            int minute = int.Parse(setMinute.Text);
            if (natalTime.IsChecked == true)
            {
                DateTime dt = new DateTime(int.Parse(setYear.Text),
                    int.Parse(setMonth.Text),
                    int.Parse(setDay.Text),
                    int.Parse(setHour.Text),
                    int.Parse(setMinute.Text),
                    int.Parse(setSecond.Text));
                DateTime newDt = dt.AddMinutes(-1 * count);
                setDay.Text = newDt.Day.ToString();
                setMonth.Text = newDt.Month.ToString();
                setYear.Text = newDt.Year.ToString();
                setHour.Text = newDt.Hour.ToString();
                setMinute.Text = newDt.Minute.ToString();
                setSecond.Text = newDt.Second.ToString();
                main.targetUser.birth_day = newDt.Day;
                main.targetUser.birth_month = newDt.Month;
                main.targetUser.birth_year = newDt.Year;
                main.mainWindowVM.userBirthStr = setYear.Text + "年" + setMonth.Text + "月" + setDay.Text + "日 " +
                    setHour.Text + ":" + setMinute.Text + ":" + setSecond.Text;
            }
            else
            {
                DateTime dt = new DateTime(int.Parse(setYear.Text),
                    int.Parse(setMonth.Text),
                    int.Parse(setDay.Text),
                    int.Parse(setHour.Text),
                    int.Parse(setMinute.Text),
                    int.Parse(setSecond.Text));
                DateTime newDt = dt.AddMinutes(-1 * count);
                setDay.Text = newDt.Day.ToString();
                setMonth.Text = newDt.Month.ToString();
                setYear.Text = newDt.Year.ToString();
                setHour.Text = newDt.Hour.ToString();
                setMinute.Text = newDt.Minute.ToString();
                setSecond.Text = newDt.Second.ToString();
                main.userdata.birth_day = newDt.Day;
                main.userdata.birth_month = newDt.Month;
                main.userdata.birth_year = newDt.Year;
                main.mainWindowVM.transitBirthStr = setYear.Text + "年" + setMonth.Text + "月" + setDay.Text + "日 " +
                    setHour.Text + ":" + setMinute.Text + ":" + setSecond.Text;
            }
            main.ReCalc();
            main.ReRender();
        }

        private void RightMinute_Click(object sender, RoutedEventArgs e)
        {
            int count = int.Parse(unitMinute.Text);
            int minute = int.Parse(setMinute.Text);
            if (natalTime.IsChecked == true)
            {
                DateTime dt = new DateTime(int.Parse(setYear.Text),
                    int.Parse(setMonth.Text),
                    int.Parse(setDay.Text),
                    int.Parse(setHour.Text),
                    int.Parse(setMinute.Text),
                    int.Parse(setSecond.Text));
                DateTime newDt = dt.AddMinutes(count);
                setDay.Text = newDt.Day.ToString();
                setMonth.Text = newDt.Month.ToString();
                setYear.Text = newDt.Year.ToString();
                setHour.Text = newDt.Hour.ToString();
                setMinute.Text = newDt.Minute.ToString();
                setSecond.Text = newDt.Second.ToString();
                main.targetUser.birth_day = newDt.Day;
                main.targetUser.birth_month = newDt.Month;
                main.targetUser.birth_year = newDt.Year;
                main.mainWindowVM.userBirthStr = setYear.Text + "年" + setMonth.Text + "月" + setDay.Text + "日 " +
                    setHour.Text + ":" + setMinute.Text + ":" + setSecond.Text;
            }
            else
            {
                DateTime dt = new DateTime(int.Parse(setYear.Text),
                    int.Parse(setMonth.Text),
                    int.Parse(setDay.Text),
                    int.Parse(setHour.Text),
                    int.Parse(setMinute.Text),
                    int.Parse(setSecond.Text));
                DateTime newDt = dt.AddMinutes(count);
                setDay.Text = newDt.Day.ToString();
                setMonth.Text = newDt.Month.ToString();
                setYear.Text = newDt.Year.ToString();
                setHour.Text = newDt.Hour.ToString();
                setMinute.Text = newDt.Minute.ToString();
                setSecond.Text = newDt.Second.ToString();
                main.userdata.birth_day = newDt.Day;
                main.userdata.birth_month = newDt.Month;
                main.userdata.birth_year = newDt.Year;
                main.mainWindowVM.transitBirthStr = setYear.Text + "年" + setMonth.Text + "月" + setDay.Text + "日 " +
                    setHour.Text + ":" + setMinute.Text + ":" + setSecond.Text;
            }
            main.ReCalc();
            main.ReRender();
        }

        private void LeftSecond_Click(object sender, RoutedEventArgs e)
        {
            int count = int.Parse(unitSecond.Text);
            int second = int.Parse(setSecond.Text);
            if (natalTime.IsChecked == true)
            {
                DateTime dt = new DateTime(int.Parse(setYear.Text),
                    int.Parse(setMonth.Text),
                    int.Parse(setDay.Text),
                    int.Parse(setHour.Text),
                    int.Parse(setMinute.Text),
                    int.Parse(setSecond.Text));
                DateTime newDt = dt.AddSeconds(-1 * count);
                setDay.Text = newDt.Day.ToString();
                setMonth.Text = newDt.Month.ToString();
                setYear.Text = newDt.Year.ToString();
                setHour.Text = newDt.Hour.ToString();
                setMinute.Text = newDt.Minute.ToString();
                setSecond.Text = newDt.Second.ToString();
                main.targetUser.birth_day = newDt.Day;
                main.targetUser.birth_month = newDt.Month;
                main.targetUser.birth_year = newDt.Year;
                main.mainWindowVM.userBirthStr = setYear.Text + "年" + setMonth.Text + "月" + setDay.Text + "日 " +
                    setHour.Text + ":" + setMinute.Text + ":" + setSecond.Text;
            }
            else
            {
                DateTime dt = new DateTime(int.Parse(setYear.Text),
                    int.Parse(setMonth.Text),
                    int.Parse(setDay.Text),
                    int.Parse(setHour.Text),
                    int.Parse(setMinute.Text),
                    int.Parse(setSecond.Text));
                DateTime newDt = dt.AddSeconds(-1 * count);
                setDay.Text = newDt.Day.ToString();
                setMonth.Text = newDt.Month.ToString();
                setYear.Text = newDt.Year.ToString();
                setHour.Text = newDt.Hour.ToString();
                setMinute.Text = newDt.Minute.ToString();
                setSecond.Text = newDt.Second.ToString();
                main.userdata.birth_day = newDt.Day;
                main.userdata.birth_month = newDt.Month;
                main.userdata.birth_year = newDt.Year;
                main.mainWindowVM.transitBirthStr = setYear.Text + "年" + setMonth.Text + "月" + setDay.Text + "日 " +
                    setHour.Text + ":" + setMinute.Text + ":" + setSecond.Text;
            }
            main.ReCalc();
            main.ReRender();
        }

        private void RightSecond_Click(object sender, RoutedEventArgs e)
        {
            int count = int.Parse(unitSecond.Text);
            int second = int.Parse(setSecond.Text);
            if (natalTime.IsChecked == true)
            {
                DateTime dt = new DateTime(int.Parse(setYear.Text),
                    int.Parse(setMonth.Text),
                    int.Parse(setDay.Text),
                    int.Parse(setHour.Text),
                    int.Parse(setMinute.Text),
                    int.Parse(setSecond.Text));
                DateTime newDt = dt.AddSeconds(count);
                setDay.Text = newDt.Day.ToString();
                setMonth.Text = newDt.Month.ToString();
                setYear.Text = newDt.Year.ToString();
                setHour.Text = newDt.Hour.ToString();
                setMinute.Text = newDt.Minute.ToString();
                setSecond.Text = newDt.Second.ToString();
                main.targetUser.birth_day = newDt.Day;
                main.targetUser.birth_month = newDt.Month;
                main.targetUser.birth_year = newDt.Year;
                main.mainWindowVM.userBirthStr = setYear.Text + "年" + setMonth.Text + "月" + setDay.Text + "日 " +
                    setHour.Text + ":" + setMinute.Text + ":" + setSecond.Text;
            }
            else
            {
                DateTime dt = new DateTime(int.Parse(setYear.Text),
                    int.Parse(setMonth.Text),
                    int.Parse(setDay.Text),
                    int.Parse(setHour.Text),
                    int.Parse(setMinute.Text),
                    int.Parse(setSecond.Text));
                DateTime newDt = dt.AddSeconds(count);
                setDay.Text = newDt.Day.ToString();
                setMonth.Text = newDt.Month.ToString();
                setYear.Text = newDt.Year.ToString();
                setHour.Text = newDt.Hour.ToString();
                setMinute.Text = newDt.Minute.ToString();
                setSecond.Text = newDt.Second.ToString();
                main.userdata.birth_day = newDt.Day;
                main.userdata.birth_month = newDt.Month;
                main.userdata.birth_year = newDt.Year;
                main.mainWindowVM.transitBirthStr = setYear.Text + "年" + setMonth.Text + "月" + setDay.Text + "日 " +
                    setHour.Text + ":" + setMinute.Text + ":" + setSecond.Text;
            }
            main.ReCalc();
            main.ReRender();
        }

        private void LeftChange_Click(object sender, RoutedEventArgs e)
        {
            int yearCount = int.Parse(unitYear.Text);
            int monthCount = int.Parse(unitMonth.Text);
            int dayCount = int.Parse(unitDay.Text);
            int hourCount = int.Parse(unitHour.Text);
            int minuteCount = int.Parse(unitMinute.Text);
            int secondCount = int.Parse(unitSecond.Text);
            int year = int.Parse(setYear.Text);
            int month = int.Parse(setMonth.Text);
            int day = int.Parse(setDay.Text);
            int hour = int.Parse(setHour.Text);
            int minute = int.Parse(setMinute.Text);
            int second = int.Parse(setSecond.Text);
            DateTime dt = new DateTime(int.Parse(unitYear.Text),
                int.Parse(unitMonth.Text),
                int.Parse(unitDay.Text),
                int.Parse(unitHour.Text),
                int.Parse(unitMinute.Text),
                int.Parse(unitSecond.Text));
            DateTime newDt = dt.AddSeconds(-1 * secondCount);
            newDt = newDt.AddMinutes(-1 * minuteCount);
            newDt = newDt.AddHours(-1 * hourCount);
            newDt = newDt.AddDays(-1 * dayCount);
            newDt = newDt.AddMonths(-1 * monthCount);
            newDt = newDt.AddYears(-1 * yearCount);
            setDay.Text = newDt.Day.ToString();
            setMonth.Text = newDt.Month.ToString();
            setYear.Text = newDt.Year.ToString();
            setHour.Text = newDt.Hour.ToString();
            setMinute.Text = newDt.Minute.ToString();
            setSecond.Text = newDt.Second.ToString();

            if (natalTime.IsChecked == true)
            {
                main.targetUser.birth_year = newDt.Year;
                main.targetUser.birth_month = newDt.Month;
                main.targetUser.birth_day = newDt.Day;
                main.targetUser.birth_hour = newDt.Month;
                main.targetUser.birth_minute = newDt.Minute;
                main.targetUser.birth_second = newDt.Second;
                main.mainWindowVM.userBirthStr = setYear.Text + "年" + setMonth.Text + "月" + setDay.Text + "日 " +
                    setHour.Text + ":" + setMinute.Text + ":" + setSecond.Text;
            }
            else
            {
                main.userdata.birth_year = newDt.Year;
                main.userdata.birth_month = newDt.Month;
                main.userdata.birth_day = newDt.Day;
                main.userdata.birth_hour = newDt.Month;
                main.userdata.birth_minute = newDt.Minute;
                main.userdata.birth_second = newDt.Second;

                main.mainWindowVM.transitBirthStr = setYear.Text + "年" + setMonth.Text + "月" + setDay.Text + "日 " +
                    setHour.Text + ":" + setMinute.Text + ":" + setSecond.Text;
            }
            main.ReCalc();
            main.ReRender();

        }

        private void RightChange_Click(object sender, RoutedEventArgs e)
        {
            int yearCount = int.Parse(unitYear.Text);
            int monthCount = int.Parse(unitMonth.Text);
            int dayCount = int.Parse(unitDay.Text);
            int hourCount = int.Parse(unitHour.Text);
            int minuteCount = int.Parse(unitMinute.Text);
            int secondCount = int.Parse(unitSecond.Text);
            int year = int.Parse(setYear.Text);
            int month = int.Parse(setMonth.Text);
            int day = int.Parse(setDay.Text);
            int hour = int.Parse(setHour.Text);
            int minute = int.Parse(setMinute.Text);
            int second = int.Parse(setSecond.Text);
            DateTime dt = new DateTime(int.Parse(unitYear.Text),
                int.Parse(unitMonth.Text),
                int.Parse(unitDay.Text),
                int.Parse(unitHour.Text),
                int.Parse(unitMinute.Text),
                int.Parse(unitSecond.Text));
            DateTime newDt = dt.AddSeconds(secondCount);
            newDt = newDt.AddMinutes(minuteCount);
            newDt = newDt.AddHours(hourCount);
            newDt = newDt.AddDays(dayCount);
            newDt = newDt.AddMonths(monthCount);
            newDt = newDt.AddYears(yearCount);
            setDay.Text = newDt.Day.ToString();
            setMonth.Text = newDt.Month.ToString();
            setYear.Text = newDt.Year.ToString();
            setHour.Text = newDt.Hour.ToString();
            setMinute.Text = newDt.Minute.ToString();
            setSecond.Text = newDt.Second.ToString();


            if (natalTime.IsChecked == true)
            {
                main.targetUser.birth_year = newDt.Year;
                main.targetUser.birth_month = newDt.Month;
                main.targetUser.birth_day = newDt.Day;
                main.targetUser.birth_hour = newDt.Month;
                main.targetUser.birth_minute = newDt.Minute;
                main.targetUser.birth_second = newDt.Second;
                main.mainWindowVM.userBirthStr = setYear.Text + "年" + setMonth.Text + "月" + setDay.Text + "日 " +
                    setHour.Text + ":" + setMinute.Text + ":" + setSecond.Text;
            }
            else
            {
                main.userdata.birth_year = newDt.Year;
                main.userdata.birth_month = newDt.Month;
                main.userdata.birth_day = newDt.Day;
                main.userdata.birth_hour = newDt.Month;
                main.userdata.birth_minute = newDt.Minute;
                main.userdata.birth_second = newDt.Second;
                
                main.mainWindowVM.transitBirthStr = setYear.Text + "年" + setMonth.Text + "月" + setDay.Text + "日 " +
                    setHour.Text + ":" + setMinute.Text + ":" + setSecond.Text;
            }
            main.ReCalc();
            main.ReRender();
        }

        private void setButton_Click(object sender, RoutedEventArgs e)
        {
            if (natalTime.IsChecked == true)
            {
                main.mainWindowVM.userBirthStr = string.Format("{0:D4}", int.Parse(setYear.Text)) + "/" + 
                    string.Format("{0:D2}", int.Parse(setMonth.Text)) + "/" + 
                    string.Format("{0:D2}", int.Parse(setDay.Text)) + " " +
                    setHour.Text + ":" + setMinute.Text + ":" + setSecond.Text + " " + main.userdata.timezone;
            }
            else
            {
                main.mainWindowVM.transitBirthStr = setYear.Text + "/" + setMonth.Text + "/" + setDay.Text + " " +
                    setHour.Text + ":" + setMinute.Text + ":" + setSecond.Text;
            }
            main.ReCalc();
            main.ReRender();
        }
    }
}
