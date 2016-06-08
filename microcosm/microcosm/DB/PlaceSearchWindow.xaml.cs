using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace microcosm.DB
{
    /// <summary>
    /// UserSearchWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class PlaceSearchWindow : Window
    {
        public UserEditWindow editWindow;
        public UserEventEditWindow eventEditWindow;
        //        public ObservableCollection<AddrSearchResult> searchResultList { get; set; }
        public UserSearchWindowViewModel searchResultList { get; set; }

        public PlaceSearchWindow(UserEditWindow editWindow, string searchStr)
        {
            this.editWindow = editWindow;
            InitializeComponent();

            searchPlace.Text = searchStr;
            searchResultList = new UserSearchWindowViewModel();
            searchResultList.resultList = new List<AddrSearchResult>();
            resultBox.DataContext = searchResultList;
        }

        public PlaceSearchWindow(UserEventEditWindow editWindow, string searchStr)
        {
            this.eventEditWindow = editWindow;
            InitializeComponent();

            searchPlace.Text = searchStr;
            searchResultList = new UserSearchWindowViewModel();
            searchResultList.resultList = new List<AddrSearchResult>();
            resultBox.DataContext = searchResultList;
        }

        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            string filename = @"system\addr.csv";
            if (!File.Exists(filename))
            {
                System.Windows.MessageBox.Show("住所ファイルの読み込みに失敗しました。");
            }
            else
            {
                // 読み込み
                FileStream fs = new FileStream(filename, FileMode.Open);
                StreamReader reader = new StreamReader(fs);
                string line;
                char[] split = { ',' };
                searchResultList.resultList.Clear();
                List<AddrSearchResult> list = new List<AddrSearchResult>();
                while ((line = reader.ReadLine()) != null)
                {
                    string[] data = line.Split(split);
                    if (data[0].IndexOf(searchPlace.Text) >= 0)
                    {
                        AddrSearchResult r = new AddrSearchResult
                            (
                            data[0],
                            double.Parse(data[1]),
                            double.Parse(data[2])
                            );
                        list.Add(r);
                    }
                }
                searchResultList.resultList = list;
                fs.Close();
            }

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Hidden;
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            AddrSearchResult searchItem = (AddrSearchResult)resultBox.SelectedItem;
            if (searchItem == null)
            {
                return;
            }
            if (editWindow != null)
            {
                editWindow.UserEditSet(searchItem.resultPlace, searchItem.resultLat.ToString(), searchItem.resultLng.ToString());
            }
            else
            {
                eventEditWindow.UserEditSet(searchItem.resultPlace, searchItem.resultLat.ToString(), searchItem.resultLng.ToString());
            }
            this.Visibility = Visibility.Hidden;
        }
    }
}
