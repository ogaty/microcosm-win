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
using Microsoft.Win32;
using System.Reflection;

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
        public UserEventEditWindow eventEditWindow;
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
            if (item.SelectedItem == null) {
                return;
            }
            if (item.SelectedItem is UserEventData)
            {
                UserEventData data = (UserEventData)item.SelectedItem;
                if (data != null)
                {
                    window.Memo = data.memo;
                }
            }
            else
            {
                UserData data = (UserData)item.SelectedItem;
                if (data != null)
                {
                    window.Memo = data.memo;
                }

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
            UserEventData edata;
            mainwindow.targetUser = udata;
            if (UserEvent.SelectedItem is UserData)
            {
                edata = new UserEventData()
                {
                    name = udata.name,
                    birth_year = udata.birth_year,
                    birth_month = udata.birth_month,
                    birth_day = udata.birth_day,
                    birth_hour = udata.birth_hour,
                    birth_minute = udata.birth_minute,
                    birth_second = udata.birth_second,
                    birth_place = udata.birth_place,
                    lat = udata.lat,
                    lng = udata.lng,
                    timezone = udata.timezone,
                    memo = udata.memo,
                    birth_str = String.Format("{0}年{1}月{2}日 {3:00}:{4:00}:{5:00}",
                        udata.birth_year,
                        udata.birth_month,
                        udata.birth_day,
                        udata.birth_hour,
                        udata.birth_minute,
                        udata.birth_second
                    ),
                    lat_lng = String.Format("{0:00.000}/{1:000.000}", udata.lat, udata.lng),
                    fullpath = udata.filename
                };
                mainwindow.userdata = edata;
            }
            else
            {
                mainwindow.userdata = (UserEventData)UserEvent.SelectedItem;
                edata = (UserEventData)UserEvent.SelectedItem;
            }
            mainwindow.userdata = edata;
            mainwindow.mainWindowVM.ReSet(udata.name, udata.birth_str, udata.birth_place, udata.lat.ToString(), udata.lng.ToString(),
                edata.name, edata.birth_str, edata.birth_place, edata.lat.ToString(), edata.lng.ToString());
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

        // イベントリスト右クリック→編集
        public void editEvent_Click(object sender, EventArgs e)
        {
            editEventData();
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

        // 新規作成(イベント)コールバック
        public void newEvent_Click_CB(
            string eventName,
            DateTime eventBirth,
            int eventHour,
            int eventMinute,
            int eventSecond,
            string eventPlace,
            double eventLat,
            double eventLng,
            string eventMemo,
            string eventTimezone
        )
        {
            UserData udata = (UserData)UserEvent.Tag;
            UserEvent uevent = new UserEvent()
            {
                event_name = eventName,
                event_year = eventBirth.Year,
                event_month = eventBirth.Month,
                event_day = eventBirth.Day,
                event_hour = eventHour,
                event_minute = eventMinute,
                event_second = eventSecond,
                event_place = eventPlace,
                event_lat = eventLat,
                event_lng = eventLng,
                event_memo = eventMemo,
                event_timezone = eventTimezone,
            };
            UserEventData ueventdata = new UserEventData()
            {
                name = eventName,
                birth_year = eventBirth.Year,
                birth_month = eventBirth.Month,
                birth_day = eventBirth.Day,
                birth_hour = eventHour,
                birth_minute = eventMinute,
                birth_second = eventSecond,
                birth_place = eventPlace,
                lat = eventLat,
                lng = eventLng,
                memo = eventMemo,
                timezone = eventTimezone,
                birth_str = String.Format("{0}年{1}月{2}日 {3:00}:{4:00}:{5:00}",
                        eventBirth.Year,
                        eventBirth.Month,
                        eventBirth.Day,
                        eventHour,
                        eventMinute,
                        eventSecond
                    ),
                fullpath = udata.filename,
                lat_lng = String.Format("{0:00.000}/{1:000.000}", eventLat, eventLng)
            };
            udata.userevent.Add(uevent);

            UserEvent.Items.Add(ueventdata);
            XmlSerializer serializer = new XmlSerializer(typeof(UserData));
            FileStream fs = new FileStream(udata.filename, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            serializer.Serialize(sw, udata);
            sw.Close();
            fs.Close();

        }

        // イベント編集コールバック
        public void editEvent_Click_CB(
            int index,
            string eventName,
            DateTime eventBirth,
            int eventHour,
            int eventMinute,
            int eventSecond,
            string eventPlace,
            double eventLat,
            double eventLng,
            string eventMemo,
            string eventTimezone
            )
        {
            UserData udata = (UserData)UserEvent.Tag;
            UserEvent uevent = new UserEvent()
            {
                event_name = eventName,
                event_year = eventBirth.Year,
                event_month = eventBirth.Month,
                event_day = eventBirth.Day,
                event_hour = eventHour,
                event_minute = eventMinute,
                event_second = eventSecond,
                event_place = eventPlace,
                event_lat = eventLat,
                event_lng = eventLng,
                event_memo = eventMemo,
                event_timezone = eventTimezone,
            };
            UserEventData ueventdata = new UserEventData()
            {
                name = eventName,
                birth_year = eventBirth.Year,
                birth_month = eventBirth.Month,
                birth_day = eventBirth.Day,
                birth_hour = eventHour,
                birth_minute = eventMinute,
                birth_second = eventSecond,
                birth_place = eventPlace,
                lat = eventLat,
                lng = eventLng,
                memo = eventMemo,
                timezone = eventTimezone,
                birth_str = String.Format("{0}年{1}月{2}日 {3:00}:{4:00}:{5:00}",
                        eventBirth.Year,
                        eventBirth.Month,
                        eventBirth.Day,
                        eventHour,
                        eventMinute,
                        eventSecond
                    ),
                fullpath = udata.filename,
                lat_lng = String.Format("{0:00.000}/{1:000.000}", eventLat, eventLng)
            };
            udata.userevent[index] = uevent;
            UserEvent.Items[index] = ueventdata;
            XmlSerializer serializer = new XmlSerializer(typeof(UserData));
            FileStream fs = new FileStream(udata.filename, FileMode.Create);
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

        // 右クリックイベントデータ追加
        public void newEventData()
        {
            setDisable();
            DbItem item = new DbItem()
            {
                userName = "新規イベント",
                userBirth = DateTime.Today,
                userHour = "12",
                userMinute = "0",
                userSecond = "0",
                userPlace = "東京都中央区",
                userLat = "35.685175",
                userLng = "139.7528",
                userTimezone = "JST",
                memo = ""
            };
            if (eventEditWindow == null)
            {
                eventEditWindow = new UserEventEditWindow(this, item);
            }
            else
            {
                eventEditWindow.UserEditRefresh(item);
            }
            eventEditWindow.Visibility = Visibility.Visible;
        }

        // 右クリックイベントデータ編集
        public void editEventData()
        {
            if (UserEvent.SelectedItem == null)
            {
                return;
            }
            DbItem item;
            if (UserEvent.SelectedItem is UserData)
            {
                UserData data = (UserData)UserEvent.SelectedItem;
                item = new DbItem()
                {
                    userName = data.name,
                    userBirth = new DateTime(data.birth_year, data.birth_month, data.birth_day,
                    data.birth_hour, data.birth_minute, data.birth_second),
                    userHour = data.birth_hour.ToString(),
                    userMinute = data.birth_minute.ToString(),
                    userSecond = data.birth_second.ToString(),
                    userPlace = data.birth_place,
                    userLat = data.lat.ToString(),
                    userLng = data.lng.ToString(),
                    userTimezone = data.timezone,
                    memo = data.memo
                };
            }
            else
            {
                UserEventData data = (UserEventData)UserEvent.SelectedItem;
                item = new DbItem()
                {
                    userName = data.name,
                    userBirth = new DateTime(data.birth_year, data.birth_month, data.birth_day,
                    data.birth_hour, data.birth_minute, data.birth_second),
                    userHour = data.birth_hour.ToString(),
                    userMinute = data.birth_minute.ToString(),
                    userSecond = data.birth_second.ToString(),
                    userPlace = data.birth_place,
                    userLat = data.lat.ToString(),
                    userLng = data.lng.ToString(),
                    userTimezone = data.timezone,
                    memo = data.memo
                };
            }
            if (eventEditWindow == null)
            {
                eventEditWindow = new UserEventEditWindow(this, item);
            }
            else
            {
                eventEditWindow.UserEditRefresh(item);
            }
            eventEditWindow.isEdit = true;
            eventEditWindow.index = UserEvent.SelectedIndex;
            setDisable();
            eventEditWindow.Visibility = Visibility.Visible;
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

        private void Import_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Amateru_Import(object sender, RoutedEventArgs e)
        {
            OpenFileDialog oFD = new OpenFileDialog();
            oFD.FilterIndex = 1;
            oFD.Filter = "AMATERU Export Files|*.csv";
            oFD.Title = "エクスポートしたファイルを選択してください";
            bool? result = oFD.ShowDialog();
            if (result == true)
            {
                string fileName = oFD.FileName;
                using (Stream fileStream = oFD.OpenFile())
                {
                    StreamReader sr = new StreamReader(fileStream, true);
                    while (sr.Peek() >= 0)
                    {
                        string line = sr.ReadLine();
                        if (line.IndexOf("NATAL") == 0)
                        {
                            string[] data = line.Split('\t');
                            string[] days = data[6].Split('-');
                            string[] hours = data[7].Split(':');
                            UserData udata = new UserData(data[1], data[2], 
                                int.Parse(days[0]), int.Parse(days[1]), int.Parse(days[2]), 
                                int.Parse(hours[0]), int.Parse(hours[1]), int.Parse(hours[2]), 
                                double.Parse(data[9]), double.Parse(data[10]), data[9], data[6], data[11]);
                            string filename = data[1] + ".csm";
                            Assembly myAssembly = Assembly.GetEntryAssembly();
                            string path =  System.IO.Path.GetDirectoryName(myAssembly.Location) + @"\data\AMATERU\" + filename;

                            try
                            {
                                if (!Directory.Exists(System.IO.Path.GetDirectoryName(path)))
                                {
                                    Directory.CreateDirectory(System.IO.Path.GetDirectoryName(path));
                                }
                                XmlSerializer serializer = new XmlSerializer(typeof(UserData));
                                FileStream fs = new FileStream(path, FileMode.Create);
                                StreamWriter sw = new StreamWriter(fs);
                                serializer.Serialize(sw, udata);
                                sw.Close();
                                fs.Close();
                            }
                            catch (IOException)
                            {

                            }

                        }
                        else
                        {
                            continue;
                        }
                    }
                    window.CreateTree();
                }
                MessageBox.Show("完了しました。");
            }

        }

        private void Stargazer_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog oFD = new OpenFileDialog();
            oFD.FilterIndex = 1;
            oFD.Title = "ファイルを選択してください";
            bool? result = oFD.ShowDialog();
            if (result == true)
            {
                string fileName = oFD.FileName;
                using (Stream fileStream = oFD.OpenFile())
                {
                    StreamReader sr = new StreamReader(fileStream, Encoding.GetEncoding("shift-jis"), true);
                    while (sr.Peek() >= 0)
                    {
                        string line = sr.ReadLine();
                        if (line.IndexOf(",") > 0)
                        {
                            string[] data = line.Split(' ');
                            int year = int.Parse(data[0].Substring(0, 4));
                            int month = int.Parse(data[0].Substring(4, 2));
                            int day = int.Parse(data[0].Substring(6, 2));

                            int hour = int.Parse(data[1].Substring(0, 2));
                            int minute = int.Parse(data[1].Substring(2, 2));
                            int second = int.Parse(data[1].Substring(4, 2));

                            string[] name = data[6].Split(',');
                            name[0] = name[0].Replace("\"", "");
                            name[1] = name[1].Replace("\"", "");

                            UserData udata = new UserData(name[1], "",
                                year, month, day,
                                hour, minute, second,
                                double.Parse(data[3]), double.Parse(data[5]), name[0], "", "JST");
                            string filename = name[1] + ".csm";
                            Assembly myAssembly = Assembly.GetEntryAssembly();
                            string path = System.IO.Path.GetDirectoryName(myAssembly.Location) + @"\data\Stargazer\" + filename;

                            try
                            {
                                if (!Directory.Exists(System.IO.Path.GetDirectoryName(path)))
                                {
                                    Directory.CreateDirectory(System.IO.Path.GetDirectoryName(path));
                                }
                                XmlSerializer serializer = new XmlSerializer(typeof(UserData));
                                FileStream fs = new FileStream(path, FileMode.Create);
                                StreamWriter sw = new StreamWriter(fs);
                                serializer.Serialize(sw, udata);
                                sw.Close();
                                fs.Close();
                            } 
                            catch (IOException)
                            {

                            }

                        }
                        else
                        {
                            continue;
                        }
                    }
                    window.CreateTree();
                }
                MessageBox.Show("完了しました。");
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Hidden;
        }

        private void Zet_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog oFD = new OpenFileDialog();
            oFD.FilterIndex = 1;
            oFD.Filter = "ZET Chart Files|*.zbs";
            oFD.Title = "チャートファイルを選択してください";
            bool? result = oFD.ShowDialog();
            if (result == true)
            {
                string fileName = oFD.FileName;
                using (Stream fileStream = oFD.OpenFile())
                {
                    StreamReader sr = new StreamReader(fileStream, Encoding.GetEncoding("shift-jis"), true);
                    while (sr.Peek() >= 0)
                    {
                        string line = sr.ReadLine();
                        string[] data = line.Split(';');
                        if (data[0] == "") continue;
                        if (data[0].IndexOf('-') == 0)
                        {
                            data[0] = data[0].Substring(2);
                        }
                        string[] days = data[1].Split('.');
                        string[] hours = data[2].Split(':');
                        string timezone = "UTC";
                        if (data[3] == " +9")
                        {
                            timezone = "JST";
                        }
                        string lat = data[5].Replace('n', '.');
                        string lng = data[6].Replace('e', '.');
                        UserData udata = new UserData(data[0], "",
                            int.Parse(days[2]), int.Parse(days[1]), int.Parse(days[0]),
                            int.Parse(hours[0]), int.Parse(hours[1]), 0,
                            double.Parse(lat), double.Parse(lng), data[4], data[8], timezone);
                        string filename = data[0] + ".csm";
                        Assembly myAssembly = Assembly.GetEntryAssembly();
                        string path = System.IO.Path.GetDirectoryName(myAssembly.Location) + @"\data\ZET\" + filename;

                        try
                        {
                            if (!Directory.Exists(System.IO.Path.GetDirectoryName(path)))
                            {
                                Directory.CreateDirectory(System.IO.Path.GetDirectoryName(path));
                            }
                            XmlSerializer serializer = new XmlSerializer(typeof(UserData));
                            FileStream fs = new FileStream(path, FileMode.Create);
                            StreamWriter sw = new StreamWriter(fs);
                            serializer.Serialize(sw, udata);
                            sw.Close();
                            fs.Close();
                        }
                        catch (IOException)
                        {

                        }
                    }
                    window.CreateTree();
                }
                MessageBox.Show("完了しました。");
            }
        }
    }
}
