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
using microcosm.Common;

namespace microcosm.DB
{
    /// <summary>
    /// UserEventEditWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class UserEventEditWindow : Window
    {
        public DatabaseWindow dbwindow;
        public PlaceSearchWindow searchWindow;
        public GoogleSearchWindow googleSearchWindow;
        public DateTime defaultDate { get; } = new DateTime(2000, 1, 1, 12, 0, 0);
        public bool isEdit = false;
        public int index;
        public UserEventEditWindow(DatabaseWindow dbwindow, DbItem item)
        {
            this.dbwindow = dbwindow;
            InitializeComponent();
            UserEditSet(item);
        }

        // 表示文字リフレッシュ
        public void UserEditRefresh(DbItem item)
        {
            UserEditClear();
            UserEditSet(item);
        }

        // 表示文字設定
        public void UserEditSet(DbItem item)
        {
            eventName.Text = item.userName;
            eventBirth.SelectedDate = item.userBirth;
            eventHour.Text = item.userHour;
            eventMinute.Text = item.userMinute;
            eventSecond.Text = item.userSecond;
            eventPlace.Text = item.userPlace;
            eventLat.Text = item.userLat;
            eventLng.Text = item.userLng;
            eventTimezone.SelectedIndex = CommonData.getTimezoneIndex(item.userTimezone);
            eventMemo.Text = item.memo;
        }


        // 表示文字設定(CB)
        public void UserEditSet(string place, string lat, string lng)
        {
            eventPlace.Text = place;
            eventLat.Text = lat;
            eventLng.Text = lng;
        }


        // 表示文字クリア
        public void UserEditClear()
        {
            eventName.Text = "";
            eventBirth.SelectedDate = defaultDate;
            eventHour.Text = "";
            eventMinute.Text = "";
            eventSecond.Text = "";
            eventPlace.Text = "";
            eventLat.Text = "";
            eventLng.Text = "";
            eventTimezone.SelectedIndex = CommonData.getTimezoneIndex("JST");
            eventMemo.Text = "";
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            if (isEdit)
            {
                dbwindow.editEvent_Click_CB(
                    index,
                    eventName.Text,
                    eventBirth.DisplayDate,
                    int.Parse(eventHour.Text),
                    int.Parse(eventMinute.Text),
                    int.Parse(eventSecond.Text),
                    eventPlace.Text,
                    double.Parse(eventLat.Text),
                    double.Parse(eventLng.Text),
                    eventMemo.Text,
                    eventTimezone.Text
                );
            }
            else
            {
                dbwindow.newEvent_Click_CB(
                    eventName.Text,
                    eventBirth.DisplayDate,
                    int.Parse(eventHour.Text),
                    int.Parse(eventMinute.Text),
                    int.Parse(eventSecond.Text),
                    eventPlace.Text,
                    double.Parse(eventLat.Text),
                    double.Parse(eventLng.Text),
                    eventMemo.Text,
                    eventTimezone.Text
                );
            }
            dbwindow.setEnable();
            isEdit = false;
            this.Visibility = Visibility.Hidden;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            dbwindow.setEnable();
            this.Visibility = Visibility.Hidden;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            dbwindow.setEnable();
            this.Visibility = Visibility.Hidden;
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            if (searchWindow == null)
            {
                searchWindow = new PlaceSearchWindow(this, eventPlace.Text);
            }
            searchWindow.Visibility = Visibility.Visible;
        }

        private void GoogleSearch_Click(object sender, RoutedEventArgs e)
        {
            if (googleSearchWindow == null)
            {
                googleSearchWindow = new GoogleSearchWindow(this, eventPlace.Text);
            }
            googleSearchWindow.Visibility = Visibility.Visible;

        }

        private void Import_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
