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

using microcosm.ViewModel;
using microcosm.DB;

namespace microcosm
{
    /// <summary>
    /// DatabaseWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class DatabaseWindow : Window
    {
        public MainWindow mainwindow;
        public DatabaseWindowViewModel window;
        public DatabaseWindow(MainWindow mainwindow)
        {
            InitializeComponent();

            window = new DatabaseWindowViewModel(this);
            this.DataContext = window;
        }

        private void UserTree_MouseDown(object sender, MouseButtonEventArgs e)
        {
            UserTree usertree = (UserTree)sender;
            MessageBox.Show(usertree.dirpath);
        }

        private void UserEvent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView item = (ListView)sender;
            UserEventData data = (UserEventData)item.SelectedItem;
            window.Memo = data.memo;
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (UserEvent.SelectedItem == null)
            {
                return;
            }
            mainwindow.userdata = (UserEventData)UserEvent.SelectedItem;

            this.Close();
        }
    }
}
