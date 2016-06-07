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
            if (data != null)
            {
                window.Memo = data.memo;
            }
        }

        // 決定ボタン
        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (UserEvent.SelectedItem == null)
            {
                return;
            }
            UserData udata = (UserData)UserEvent.Tag;
            mainwindow.targetUser = udata;
            UserEventData edata = (UserEventData)UserEvent.SelectedItem;
            mainwindow.userdata = edata;
            mainwindow.mainWindowVM.ReSet(udata.name, udata.birth_str, udata.birth_place, udata.lat.ToString(), udata.lng.ToString(),
                edata.name, edata.birth_str, edata.birth_place, edata.lat, edata.lng);
            mainwindow.ReCalc();
            mainwindow.ReRender();

            this.Visibility = Visibility.Hidden;
        }

        // 新規作成(ファイル)
        public void newItem_Click(object sender, EventArgs e)
        {
            newData();
        }

        // イベントリスト右クリック→表示
        public void disp_Click(object sender, EventArgs e)
        {
            if (UserEvent.SelectedItem == null)
            {
                return;
            }
            mainwindow.userdata = (UserEventData)UserEvent.SelectedItem;

            this.Visibility = Visibility.Hidden;
        }

        // イベントリスト右クリック→新規追加
        public void addEvent_Click(object sender, EventArgs e)
        {
            newEventData();
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
                    isDir = false,
                    userName = userName,
                    userFurigana = userFurigana
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
                    isDir = false,
                    userName = userName,
                    userFurigana = userFurigana
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

        // ユーザーデータ追加
        public void newData()
        {
            setDisable();
            AddUserEditWindow(new DbItem
            {
                fileName = "新規データ"
                + DateTime.Now.Year
                + DateTime.Now.Month.ToString("00")
                + DateTime.Now.Day.ToString("00")
                + DateTime.Now.Hour.ToString("00")
                + DateTime.Now.Minute.ToString("00")
                + DateTime.Now.Second.ToString("00"),
                isDir = false,
                userName = "新規データ",
                userFurigana = "しんきでーた",
                userBirth = DateTime.Today,
                userHour = "12",
                userMinute = "0",
                userSecond = "0",
                userPlace = "東京都中央区",
                userLat = "35.685175",
                userLng = "139.7528",
                userTimezone = "JST",
                memo = ""
            });
        }

        // イベントデータ追加
        // fileNameとかが不要になる
        public void newEventData()
        {
            setDisable();
            // TODO
        }

        // 新規作成(ディレクトリ)
        public void newDir_Click(object sender, EventArgs e)
        {
            TreeViewItem item = (TreeViewItem)UserDirTree.SelectedItem;
            DbItem iteminfo = (DbItem)item.Tag;
            string newDir = "新規ディレクトリ" +
                DateTime.Now.Year.ToString() +
                DateTime.Now.Month.ToString("00") +
                DateTime.Now.Day.ToString("00") +
                DateTime.Now.Hour.ToString("00") +
                DateTime.Now.Minute.ToString("00") +
                DateTime.Now.Second.ToString("00");
            TreeViewItem newItem = new TreeViewItem { Header = newDir };
            string parentPath;
            if (iteminfo.isDir)
            {
                string dirName = iteminfo.fileName + @"\" + newDir;
                newItem.Tag = new DbItem
                {
                    fileName = dirName,
                    isDir = true
                };
                item.Items.Add(newItem);
                Directory.CreateDirectory(dirName);
            }
            else
            {
                if (item.Parent is TreeView)
                {
                    string dirName = @"data\" + newDir;
                    newItem.Tag = new DbItem
                    {
                        fileName = dirName,
                        isDir = true
                    };
                    item.Items.Add(newItem);
                    Directory.CreateDirectory(dirName);
                }
                else
                {
                    TreeViewItem parentItem = (TreeViewItem)item.Parent;
                    DbItem parentIteminfo = (DbItem)parentItem.Tag;
                    if (parentIteminfo == null)
                    {
                        parentPath = "data";
                    }
                    else
                    {
                        parentPath = System.IO.Path.GetDirectoryName(parentIteminfo.fileName);
                    }
                    string dirName = parentPath + @"\" + newDir;
                    newItem.Tag = new DbItem
                    {
                        fileName = dirName,
                        isDir = true
                    };
                    parentItem.Items.Add(newItem);
                    Directory.CreateDirectory(dirName);
                }
            }
        }

        // 編集
        public void edit_Click(object sender, EventArgs e)
        {
            TreeViewItem item = (TreeViewItem)UserDirTree.SelectedItem;
            DbItem iteminfo = (DbItem)item.Tag;
            XMLDBManager DBMgr = new XMLDBManager(iteminfo.fileName);
            UserData data = DBMgr.getObject();
            iteminfo.fileName = System.IO.Path.GetFileNameWithoutExtension(iteminfo.fileName);
            iteminfo.userName = data.name;
            iteminfo.userFurigana = data.furigana;
            iteminfo.userBirth = new DateTime(data.birth_year, data.birth_month, data.birth_day, data.birth_hour, data.birth_minute, data.birth_second);
            iteminfo.userHour = data.birth_hour.ToString();
            iteminfo.userMinute = data.birth_minute.ToString();
            iteminfo.userSecond = data.birth_second.ToString();
            iteminfo.userPlace = data.birth_place;
            iteminfo.userLat = data.lat.ToString("00.000");
            iteminfo.userLng = data.lng.ToString("000.000");
            iteminfo.userTimezone = data.timezone;
            iteminfo.memo = data.memo;

            setDisable();
            AddUserEditWindow(iteminfo);
        }

        // 削除
        public void deleteItem_Click(object sender, EventArgs e)
        {
            TreeViewItem item = (TreeViewItem)UserDirTree.SelectedItem;
            if (item == null)
            {
                return;
            }
            DbItem iteminfo = (DbItem)item.Tag;
            if (iteminfo.isDir)
            {
                if (Directory.Exists(iteminfo.fileName))
                {
                    try
                    {
                        Directory.Delete(iteminfo.fileName, true);
                    }
                    catch (IOException)
                    {
                        MessageBox.Show("ディレクトリの削除に失敗しました。");
                    }
                }
            }
            else 
            {
                if (File.Exists(iteminfo.fileName))
                {
                    try
                    {
                        File.Delete(iteminfo.fileName);
                    }
                    catch (IOException)
                    {
                        MessageBox.Show("ファイルの削除に失敗しました。");
                    }
                }
            }
            window.CreateTree();
        }

        public void AddUserEditWindow(DbItem item)
        {
            if (editwindow == null)
            {
                editwindow = new UserEditWindow(this, item);
            }
            else
            {
                editwindow.UserEditRefresh(item);
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

        // 新規作成(何もないところ右クリック)
        private void NewDataContext_Click(object sender, RoutedEventArgs e)
        {
            newData();
        }
    }
}
