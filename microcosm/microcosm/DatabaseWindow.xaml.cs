﻿using System;
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
using microcosm.Planet;

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

        // イベントリスト右クリック→削除
        public void deleteEvent_Click(object sender, EventArgs e)
        {
            if (UserEvent.SelectedItem == null)
            {
                return;
            }
            if (UserEvent.SelectedIndex == 0)
            {
                MessageBox.Show("出生時刻は削除できません。");
            }

            UserData udata = (UserData)UserEvent.Tag;
            udata.userevent.RemoveAt(UserEvent.SelectedIndex - 1);

            UserEvent.Items.RemoveAt(UserEvent.SelectedIndex);
            XmlSerializer serializer = new XmlSerializer(typeof(UserData));
            File.Delete(udata.filename);
            FileStream fs = new FileStream(udata.filename, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            serializer.Serialize(sw, udata);
            sw.Close();
            fs.Close();
        }

        // イベントリスト右クリック→回帰計算
        public void returnEvent_Click(object sender, EventArgs e)
        {
            // ソーラーリターン
            // 誕生日1日前から1時間ごとに計算
            // 原始的だけどとりあえずいいや
            if (UserEvent.SelectedItem == null)
            {
                return;
            }
            if (UserEvent.SelectedItem is UserData)
            {
                UserData data = (UserData)UserEvent.SelectedItem;
                DateTime currentDate = new DateTime(
                    data.birth_year,
                    data.birth_month,
                    data.birth_day,
                    data.birth_hour,
                    data.birth_minute,
                    data.birth_second
                    );
                List<PlanetData> currentPlanet = mainwindow.calc.PositionCalc(data.birth_year,
                    data.birth_month,
                    data.birth_day,
                    data.birth_hour,
                    data.birth_minute,
                    data.birth_second,
                    data.lat,
                    data.lng,
                    (int)mainwindow.config.houseCalc, 0);

                DateTime calcDate = currentDate.AddDays(364.5);
                for (int i = 0; i < 40; i++)
                {
                    calcDate = calcDate.AddHours(1);
                    List<PlanetData> planet = mainwindow.calc.PositionCalc(calcDate.Year,
                        calcDate.Month,
                        calcDate.Day,
                        calcDate.Hour,
                        calcDate.Minute,
                        calcDate.Day,
                        data.lat,
                        data.lng,
                        (int)mainwindow.config.houseCalc, 0);
                    if (Math.Abs(currentPlanet[0].absolute_position - planet[0].absolute_position) < 0.01)
                    {
                        Console.WriteLine(currentPlanet[0].absolute_position.ToString());
                        Console.WriteLine(planet[0].absolute_position.ToString());
                        break;
                    }
                }
                MessageBox.Show(calcDate.ToString());
            }


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
            TreeViewItem parentItem;
            TreeViewItem item = (TreeViewItem)UserDirTree.SelectedItem;
            if (item == null)
            {
                item = (TreeViewItem)UserDirTree.Items[0];
            }
            if (item.Parent is TreeViewItem)
            {
                parentItem = (TreeViewItem)item.Parent;
                if (parentItem.Tag == null)
                {
                    item = (TreeViewItem)UserDirTree.Items[0];
                }
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
                parentItem = (TreeViewItem)item.Parent;
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
            string evTxt = "";
            if (eventName.IndexOf("- ") == 0)
            {
                evTxt = eventName;
            }
            else
            {
                evTxt = "- " + eventName;
            }
            UserEvent uevent = new UserEvent()
            {
                event_name = evTxt,
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
                name = evTxt,
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
            UserEvent.Tag = udata;

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
            string evTxt = "";
            if (eventName.IndexOf("- ") == 0)
            {
                evTxt = eventName;
            }
            else
            {
                evTxt = "- " + eventName;
            }
            UserEvent uevent = new UserEvent()
            {
                event_name = evTxt,
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
                name = evTxt,
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
            if (index > 0)
            {
                udata.userevent[index - 1] = uevent;
            }
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
            if (iteminfo == null)
            {
                return;
            }
            if (Directory.Exists(iteminfo.fileName))
            {
                return;
            }
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
                int success = 0;
                int err = 0;
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
                                double.Parse(data[9]), double.Parse(data[10]), data[8], data[6], data[11]);
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
                                success++;
                            }
                            catch (IOException)
                            {
                                err++;
                            }

                        }
                        else
                        {
                            continue;
                        }
                    }
                    window.CreateTree();
                }
                if (err == 0)
                {
                    MessageBox.Show("完了しました。");
                }
                else
                {
                    MessageBox.Show("完了しました。(エラー" + err.ToString() + "件発生");
                }
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
                int success = 0;
                int err = 0;
                using (Stream fileStream = oFD.OpenFile())
                {
                    StreamReader sr = new StreamReader(fileStream, Encoding.GetEncoding("shift-jis"), true);
                    while (sr.Peek() >= 0)
                    {
                        string line = sr.ReadLine();
                        if (line.IndexOf(",") > 0)
                        {
                            string trimdata = line.Replace("  ", " ");
                            string[] data = trimdata.Split(' ');
                            // data[0] ymd
                            // data[1] his
                            // data[2] lat
                            // data[3] lng
                            // data[4] other

                            int year = int.Parse(data[0].Substring(0, 4));
                            int month = int.Parse(data[0].Substring(4, 2));
                            int day = int.Parse(data[0].Substring(6, 2));

                            int hour = int.Parse(data[1].Substring(0, 2));
                            int minute = int.Parse(data[1].Substring(2, 2));
                            int second = int.Parse(data[1].Substring(4, 2));

                            string[] name = data[4].Split(',');
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
                                success++;
                            } 
                            catch (IOException)
                            {
                                err++;
                            }

                        }
                        else
                        {
                            continue;
                        }
                    }
                    window.CreateTree();
                }
                if (err == 0)
                {
                    MessageBox.Show("完了しました。");
                }
                else
                {
                    MessageBox.Show("完了しました。(エラー" + err.ToString() + "件発生");
                }
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
                int success = 0;
                int err = 0;
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
                        double dlat = 35.679567;
                        if (data[5].IndexOf('n') > 0)
                        {
                            string[] lats = data[5].Split('n');
                            dlat = double.Parse(lats[0]) + double.Parse(lats[1]) / 60;
                        }
                        else
                        {
                            string[] lats = data[5].Split('s');
                            dlat = double.Parse(lats[0]) + double.Parse(lats[1]) / 60;
                        }
                        double dlng = 139.772003;
                        if (data[6].IndexOf('w') > 0)
                        {
                            string[] lngs = data[6].Split('w');
                            dlng = double.Parse(lngs[0]) + double.Parse(lngs[1]) / 60;
                        }
                        else
                        {
                            string[] lngs = data[6].Split('e');
                            dlng = double.Parse(lngs[0]) + double.Parse(lngs[1]) / 60;
                        }

                        UserData udata = new UserData(data[0], "",
                            int.Parse(days[2]), int.Parse(days[1]), int.Parse(days[0]),
                            int.Parse(hours[0]), int.Parse(hours[1]), 0,
                            dlat, dlng, data[4], data[8], timezone);
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
                            success++;
                        }
                        catch (IOException)
                        {
                            err++;
                        }
                    }
                    window.CreateTree();
                }
                if (err == 0)
                {
                    MessageBox.Show("完了しました。");
                }
                else
                {
                    MessageBox.Show("完了しました。(エラー" + err.ToString() + "件発生");
                }
            }
        }

        private void TimePassages_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog oFD = new OpenFileDialog();
            oFD.FilterIndex = 1;
            oFD.Filter = "TimePassager Chart Files|*.chw";
            oFD.Title = "チャートファイルを選択してください";
            bool? result = oFD.ShowDialog();
            if (result == true)
            {
                string fileName = oFD.FileName;
                int success = 0;
                int err = 0;
                using (Stream fileStream = oFD.OpenFile())
                {
                    StreamReader sr = new StreamReader(fileStream, Encoding.GetEncoding("shift-jis"), true);
                    int i = 0;
                    string name = "";
                    int year = 2000;
                    int month = 1;
                    int day = 1;
                    int hour = 12;
                    int minute = 0;
                    string timezone = "JST";
                    string place = "";
                    string lat = "35.670587";
                    string lng = "139.772003";
                    string memo = "";
                    while (sr.Peek() >= 0)
                    {
                        string line = sr.ReadLine();
                        i++;
                        if (i == 1)
                        {
                            continue;
                        }
                        switch (i)
                        {
                            case 2:
                                name = line;
                                break;
                            case 3:
                                string[] dt = line.Split('/');
                                year = int.Parse(dt[2]);
                                month = int.Parse(dt[0]);
                                day = int.Parse(dt[1]);
                                break;
                            case 4:
                                string[] tm = line.Split(':');
                                hour = int.Parse(tm[0]);
                                minute = int.Parse(tm[1]);
                                break;
                            case 5:
                                if (line == "-9.0")
                                {
                                    timezone = "JST";
                                }
                                else
                                {
                                    timezone = "UTC";
                                }
                                break;
                            case 6:
                                // daylight設定、インポートしない
                            case 7:
                                place = line;
                                break;
                            case 8:
                                place += "," + line;
                                break;
                            case 9:
                                lat = line;
                                break;
                            case 10:
                                lng = line;
                                break;
                            case 33:
                                memo = line;
                                break;
                            default:
                                break;
                        }
                        if (i >= 33)
                        {
                            UserData udata = new UserData(name, "",
                                year, month, day,
                                hour, minute, 0,
                                double.Parse(lat), double.Parse(lng), place, memo, timezone);
                            string filename = name + ".csm";
                            Assembly myAssembly = Assembly.GetEntryAssembly();
                            string path = System.IO.Path.GetDirectoryName(myAssembly.Location) + @"\data\TimePassages\" + filename;

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
                                success++;
                            }
                            catch (IOException)
                            {
                                err++;
                            }
                            break;
                        }

                    }
                    window.CreateTree();
                }
                if (err == 0)
                {
                    MessageBox.Show("完了しました。");
                }
                else
                {
                    MessageBox.Show("完了しました。(エラー" + err.ToString() + "件発生");
                }
            }
        }

        private void Astrolog_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog oFD = new OpenFileDialog();
            oFD.FilterIndex = 1;
            oFD.Filter = "Astrolog DAT Files|*.dat";
            oFD.Title = "チャートファイルを選択してください";
            bool? result = oFD.ShowDialog();
            if (result == true)
            {
                string fileName = oFD.FileName;
                int success = 0;
                int err = 0;
                using (Stream fileStream = oFD.OpenFile())
                {
                    string name = "";
                    int year = 2000;
                    int month = 1;
                    int day = 1;
                    int hour = 12;
                    int minute = 0;
                    int second = 0;
                    string timezone = "JST";
                    string place = "";
                    string lat = "35.670587";
                    double dlat = 35.679567;
                    string lng = "139.772003";
                    double dlng = 139.772003;
                    string memo = "";
                    bool dataSet = false;
                    StreamReader sr = new StreamReader(fileStream, Encoding.GetEncoding("shift-jis"), true);
                    while (sr.Peek() >= 0)
                    {
                        string line = sr.ReadLine();
                        string[] data = line.Split(' ');
                        if (data[0].IndexOf("/qb") == 0)
                        {
                            month = int.Parse(data[1]);
                            day = int.Parse(data[2]);
                            year = int.Parse(data[3]);
                            string[] hours = data[4].Split(':');
                            hour = int.Parse(hours[0]);
                            minute = int.Parse(hours[1]);
                            second = int.Parse(hours[2]);
                            if (data[6] == "-9:00")
                            {
                                timezone = "JST";
                            }
                            else
                            {
                                timezone = "UTC";
                            }
                            // Astrologは時分秒表記
                            string[] lngs = data[7].Split('\'');
                            string[] deglngs = lngs[0].Split(':');
                            lng = deglngs[0];
                            if (lngs[1].IndexOf('W') > 0)
                            {
                                dlng = double.Parse(lng) * -1 + double.Parse(deglngs[1]) / 60;
                            }
                            else
                            {
                                dlng = double.Parse(lng) + double.Parse(deglngs[1]) / 60;
                            }
                            string[] lats = data[8].Split('\'');
                            string[] deglats = lats[0].Split(':');
                            lat = deglats[0];
                            if (lats[1].IndexOf('S') > 0)
                            {
                                dlat = double.Parse(lat) * -1 + double.Parse(deglats[1]) / 60;
                            }
                            else
                            {
                                dlat = double.Parse(lat) + double.Parse(deglats[1]) / 60;
                            }
                        }
                        else if (data[0].IndexOf("/zi") == 0)
                        {
                            data = line.Split('\"');
                            name = data[1];
                            place = data[3];
                            dataSet = true;
                        }
                        else
                        {
                            continue;
                        }

                        if (!dataSet) continue;

                        UserData udata = new UserData(name, "",
                            year, month, day,
                            hour, minute, second,
                            dlat, dlng, place, memo, timezone);
                        string filename = name + ".csm";
                        Assembly myAssembly = Assembly.GetEntryAssembly();
                        string path = System.IO.Path.GetDirectoryName(myAssembly.Location) + @"\data\Astrolog32\" + filename;

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
                            success++;
                        }
                        catch (IOException)
                        {
                            err++;
                        }
                    }
                    window.CreateTree();
                }
                if (err == 0)
                {
                    MessageBox.Show("完了しました。");
                }
                else
                {
                    MessageBox.Show("完了しました。(エラー" + err.ToString() + "件発生");
                }
            }
        }

        private void StarFisher_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog oFD = new OpenFileDialog();
            oFD.FilterIndex = 1;
            oFD.Filter = "StarFisher Script Files|*.sfs";
            oFD.Title = "チャートファイルを選択してください";
            bool? result = oFD.ShowDialog();
            if (result == true)
            {
                string fileName = oFD.FileName;
                int err = 0;
                int success = 0;
                using (Stream fileStream = oFD.OpenFile())
                {
                    string name = "";
                    int year = 2000;
                    int month = 1;
                    int day = 1;
                    int hour = 12;
                    int minute = 0;
                    int second = 0;
                    string place = "";
                    double dlat = 35.670587;
                    double dlng = 139.772003;
                    string timezone = "JST";
                    string memo = "";
                    bool dataSet = false;
                    StreamReader sr = new StreamReader(fileStream, true);
                    while (sr.Peek() >= 0)
                    {
                        string line = sr.ReadLine();
                        if (line.IndexOf("Latitude") > 0)
                        {
                            string[] datainfo = line.Split('\"');
                            string[] lats = datainfo[1].Split('\'');
                            if (lats[0].IndexOf("S") > 0)
                            {
                                string[] dlats = lats[0].Split('S');
                                dlat = double.Parse(dlats[0]) * -1 + double.Parse(dlats[1]) / 60;
                            }
                            else
                            {
                                string[] dlats = lats[0].Split('N');
                                dlat = double.Parse(dlats[0]) + double.Parse(dlats[1]) / 60;
                            }
                        }
                        if (line.IndexOf("Longitude") > 0)
                        {
                            string[] datainfo = line.Split('\"');
                            string[] lngs = datainfo[1].Split('\'');
                            if (lngs[0].IndexOf("W") > 0)
                            {
                                string[] dlngs = lngs[0].Split('W');
                                dlng = double.Parse(dlngs[0]) * -1 + double.Parse(dlngs[1]) / 60;
                            }
                            else
                            {
                                string[] dlngs = lngs[0].Split('E');
                                dlng = double.Parse(dlngs[0]) + double.Parse(dlngs[1]) / 60;
                            }
                        }
                        if (line.IndexOf("Date") > 0)
                        {
                            string[] datainfo = line.Split('\"');
                            string[] dateinfo = datainfo[1].Split(' ');
                            year = int.Parse(dateinfo[0].Split('-')[0]);
                            month = int.Parse(dateinfo[0].Split('-')[1]);
                            day = int.Parse(dateinfo[0].Split('-')[2]);
                            hour = int.Parse(dateinfo[1].Split(':')[0]);
                            minute = int.Parse(dateinfo[1].Split(':')[1]);
                            second = int.Parse(dateinfo[1].Split(':')[2]);
                            timezone = dateinfo[2];
                        }
                        if (line.IndexOf("Caption") > 0)
                        {
                            name = line.Split('\"')[1];
                        }
                        if (line.IndexOf("Location") > 0)
                        {
                            place = line.Split('\"')[1];
                        }
                        if (line.IndexOf("Note") > 0)
                        {
                            memo = line.Split('\"')[1];
                            dataSet = true;
                        }


                        if (!dataSet) continue;

                        UserData udata = new UserData(name, "",
                            year, month, day,
                            hour, minute, second,
                            dlat, dlng, place, memo, timezone);
                        string filename = name + ".csm";
                        Assembly myAssembly = Assembly.GetEntryAssembly();
                        string path = System.IO.Path.GetDirectoryName(myAssembly.Location) + @"\data\StarFisher\" + filename;

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
                            success++;
                        }
                        catch (IOException)
                        {
                            err++;
                        }
                    }
                    window.CreateTree();
                }
                if (err == 0)
                {
                    MessageBox.Show("完了しました。");
                }
                else
                {
                    MessageBox.Show("完了しました。(エラー" + err.ToString() + "件発生");
                }
            }
        }

        private void Morinus_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog oFD = new OpenFileDialog();
            oFD.FilterIndex = 1;
            oFD.Filter = "Morinus Horoscope Files|*.hor";
            oFD.Title = "チャートファイルを選択してください";
            bool? result = oFD.ShowDialog();
            if (result == true)
            {
                string fileName = oFD.FileName;
                int success = 0;
                int err = 0;
                using (Stream fileStream = oFD.OpenFile())
                {
                    int i = 0;
                    string name = "";
                    int year = 2000;
                    int month = 1;
                    int day = 1;
                    int hour = 12;
                    int minute = 0;
                    int second = 0;
                    string place = "";
                    double dlat = 35.670587;
                    double dlng = 139.772003;
                    string timezone = "JST";
                    string memo = "";
                    StreamReader sr = new StreamReader(fileStream, true);
                    while (sr.Peek() >= 0)
                    {
                        string line = sr.ReadLine();
                        i++;
                        switch (i)
                        {
                            case 1:
                                name = line.Substring(1);
                                break;
                            case 6:
                                year = int.Parse(line.Substring(2));
                                break;
                            case 7:
                                month = int.Parse(line.Substring(2));
                                break;
                            case 8:
                                day = int.Parse(line.Substring(2));
                                break;
                            case 9:
                                hour = int.Parse(line.Substring(2));
                                break;
                            case 10:
                                minute = int.Parse(line.Substring(2));
                                break;
                            case 11:
                                second = int.Parse(line.Substring(2));
                                break;
                            case 18:
                                place = line.Substring(1);
                                break;
                            case 20:
                                dlat = double.Parse(line.Substring(2));
                                break;
                            case 21:
                                dlat += double.Parse(line.Substring(2)) / 100;
                                break;
                            case 23:
                                string lat = line.Substring(2);
                                if (lat == "00")
                                {
                                    dlat *= -1;
                                }
                                break;
                            case 24:
                                dlng = double.Parse(line.Substring(2));
                                break;
                            case 25:
                                dlng += double.Parse(line.Substring(2)) / 100;
                                break;
                            case 27:
                                string lng = line.Substring(2);
                                if (lng == "00")
                                {
                                    dlng *= -1;
                                }
                                break;

                        }

                    }

                    UserData udata = new UserData(name, "",
                        year, month, day,
                        hour, minute, second,
                        dlat, dlng, place, memo, timezone);
                    string filename = name + ".csm";
                    Assembly myAssembly = Assembly.GetEntryAssembly();
                    string path = System.IO.Path.GetDirectoryName(myAssembly.Location) + @"\data\Morinus\" + filename;

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
                        success++;
                    }
                    catch (IOException)
                    {
                        err++;
                    }
                    window.CreateTree();

                }
                if (err == 0)
                {
                    MessageBox.Show("完了しました。");
                } else
                {
                    MessageBox.Show("完了しました。(エラー" + err.ToString() + "件発生");
                }

            }
        }

        private void Amateru_Export(object sender, RoutedEventArgs e)
        {
        }

        private void NewMenuItem_Click(object sender, RoutedEventArgs e)
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
    }
}
