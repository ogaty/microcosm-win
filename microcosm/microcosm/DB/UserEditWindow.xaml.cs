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

namespace microcosm.DB
{
    /// <summary>
    /// UserEditWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class UserEditWindow : Window
    {
        public DatabaseWindow dbwindow;
        public UserEditWindow(DatabaseWindow dbwindow)
        {
            this.dbwindow = dbwindow;
            InitializeComponent();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            dbwindow.setEnable();
            this.Visibility = Visibility.Hidden;
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            dbwindow.newUser_Click_CB(
                fileName.Text,
                userName.Text,
                userFurigana.Text,
                userBirth.DisplayDate,
                int.Parse(userHour.Text),
                int.Parse(userMinute.Text),
                int.Parse(userSecond.Text),
                userPlace.Text,
                double.Parse(userLat.Text),
                double.Parse(userLng.Text),
                userMemo.Text,
                userTimezone.Text
                );
            dbwindow.setEnable();
            this.Visibility = Visibility.Hidden;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Hidden;
        }
    }
}
