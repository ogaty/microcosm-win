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
    public partial class UserSearchWindow : Window
    {
        public UserEditWindow editWindow;
//        public ObservableCollection<AddrSearchResult> searchResultList { get; set; }
        public UserSearchWindowViewModel searchResultList { get; set; }

        public UserSearchWindow(UserEditWindow editWindow, string searchStr)
        {
            this.editWindow = editWindow;
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
    }
}
