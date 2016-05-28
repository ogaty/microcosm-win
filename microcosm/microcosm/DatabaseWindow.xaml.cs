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
using System.Xml.Serialization;
using System.IO;

namespace microcosm
{
    /// <summary>
    /// DatabaseWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class DatabaseWindow : Window
    {
        public MainWindow mainwindow;
        public DatabaseWindowViewModel window;
        public UserEditWindow editwindow;
        public DatabaseWindow(MainWindow mainwindow)
        {
            this.mainwindow = mainwindow;
            InitializeComponent();

            window = new DatabaseWindowViewModel(this);
            this.DataContext = window;
        }

        private void UserTree_MouseDown(object sender, MouseButtonEventArgs e)
        {
            UserTree usertree = (UserTree)sender;
        }

        // イベントリストの選択
        private void UserEvent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView item = (ListView)sender;
            UserEventData data = (UserEventData)item.SelectedItem;
            window.Memo = data.memo;
        }

        // 決定ボタン
        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (UserEvent.SelectedItem == null)
            {
                return;
            }
            mainwindow.userdata = (UserEventData)UserEvent.SelectedItem;

            this.Close();
        }

        // 新規作成(ファイル)
        public void newItem_Click(object sender, EventArgs e)
        {
            setDisable();
            AddUserEditWindow();
        }

        // 新規作成(ファイル)コールバック
        public void newUser_Click_CB(
            string fileName,
            string userName,
            string userFurigana,
            DateTime userBirth,
            int userHour,
            int userMinute,
            int userSecond,
            string userPlace,
            double userLat,
            double userLng,
            string userMemo,
            string userTimezone
        )
        {
            TreeViewItem item = (TreeViewItem)UserDirTree.SelectedItem;
            if (item == null)
            {
                item = (TreeViewItem)UserDirTree.Items[0];
            }
            DbItem iteminfo = (DbItem)item.Tag;
            string newDir;
            if (iteminfo == null)
            {
                iteminfo = new DbItem()
                {
                    fileName = "data",
                    isDir = true
                };
                newDir = @"data\";
            } else
            {
                newDir = System.IO.Path.GetDirectoryName(iteminfo.fileName);
            }
            string newPath;
            TreeViewItem newItem = new TreeViewItem { Header = fileName };
            if (iteminfo.isDir)
            {
                newPath = newDir + @"\" + fileName + ".csm";
                newItem.Tag = new DbItem
                {
                    fileName = newPath,
                    isDir = false
                };
                newItem.Selected += window.UserItem_Selected;
                item.Items.Add(newItem);
            }
            else
            {
                TreeViewItem parentItem = (TreeViewItem)item.Parent;
                DbItem parentIteminfo = (DbItem)parentItem.Tag;
                string parentDir = System.IO.Path.GetDirectoryName(parentIteminfo.fileName);
                newPath = parentDir + @"\" + fileName + ".csm";
                newItem.Tag = new DbItem
                {
                    fileName = newPath,
                    isDir = false
                };
                newItem.Selected += window.UserItem_Selected;
                parentItem.Items.Add(newItem);
            }
            UserData udata = new UserData()
            {
                name = userName,
                furigana = userFurigana,
                birth_year = userBirth.Year,
                birth_month = userBirth.Month,
                birth_day = userBirth.Day,
                birth_hour = userHour,
                birth_minute = userMinute,
                birth_second = userSecond,
                birth_place = userPlace,
                lat = userLat,
                lng = userLng,
                memo = userMemo,
                timezone = userTimezone,
                userevent = null
            };
            XmlSerializer serializer = new XmlSerializer(typeof(UserData));
            FileStream fs = new FileStream(newPath, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            serializer.Serialize(sw, udata);
            sw.Close();
            fs.Close();
        }

        // 新規作成(ディレクトリ)
        public void newDir_Click(object sender, EventArgs e)
        {
            TreeViewItem item = (TreeViewItem)UserDirTree.SelectedItem;
            DbItem iteminfo = (DbItem)item.Tag;
            TreeViewItem newItem = new TreeViewItem { Header = "新規ディレクトリ" };
            TreeViewItem parentItem = (TreeViewItem)item.Parent;
            DbItem parentIteminfo = (DbItem)parentItem.Tag;
            string parentPath = System.IO.Path.GetDirectoryName(parentIteminfo.fileName);
            newItem.Tag = new DbItem
            {
                fileName = parentPath + "新規ディレクトリ",
                isDir = true
            };
            parentItem.Items.Add(newItem);
        }

        // 編集
        public void edit_Click(object sender, EventArgs e)
        {
            TreeViewItem item = (TreeViewItem)UserDirTree.SelectedItem;
            DbItem iteminfo = (DbItem)item.Tag;
            TreeViewItem newItem = new TreeViewItem { Header = "新規ディレクトリ" };
            TreeViewItem parentItem = (TreeViewItem)item.Parent;
            DbItem parentIteminfo = (DbItem)parentItem.Tag;
            string parentPath = System.IO.Path.GetDirectoryName(parentIteminfo.fileName);
            newItem.Tag = new DbItem
            {
                fileName = parentPath + "新規ディレクトリ",
                isDir = true
            };
            parentItem.Items.Add(newItem);
        }

        // 削除
        public void deleteItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("click");
        }

        public void AddUserEditWindow()
        {
            if (editwindow == null)
            {
                editwindow = new UserEditWindow(this);
            }
            editwindow.Visibility = Visibility.Visible;
        }

        public void setEnable()
        {
            this.IsEnabled = true;
        }
        public void setDisable()
        {
            this.IsEnabled = false;
        }
    }
}
