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
using microcosm.Planet;
using microcosm.Common;

namespace microcosm
{
    /// <summary>
    /// DatabaseWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class DatabaseWindow : Window
    {
        public MainWindow mainwindow;
        public DatabaseWindowViewModel vm;
        public UserEditWindow editwindow;
        public UserEventEditWindow eventEditWindow;
        public DatabaseProcessWindow processWindow;
        public KaikiWindow kaikiwindow;

        public DatabaseWindow(MainWindow mainwindow)
        {
            this.mainwindow = mainwindow;
            InitializeComponent();

            vm = new DatabaseWindowViewModel(this);
            this.DataContext = vm;
            u1.Tag = mainwindow.targetUser;
            u2.Tag = mainwindow.targetUser2;
            t1.Tag = mainwindow.userdata;
            t2.Tag = mainwindow.userdata2;
            u1.Text = mainwindow.targetUser.name + "\n" + mainwindow.targetUser.birth_str_ymd + "\n" + mainwindow.targetUser.birth_str_his;
            u2.Text = mainwindow.targetUser2.name + "\n" + mainwindow.targetUser2.birth_str_ymd + "\n" + mainwindow.targetUser2.birth_str_his;
            t1.Text = mainwindow.userdata.name + "\n" + mainwindow.userdata.birth_str_ymd + "\n" + mainwindow.userdata.birth_str_his;
            t2.Text = mainwindow.userdata2.name + "\n" + mainwindow.userdata2.birth_str_ymd + "\n" + mainwindow.userdata2.birth_str_his;
        }

        private void UserTree_MouseDown(object sender, MouseButtonEventArgs e)
        {
            UserTree usertree = (UserTree)sender;
        }

        // イベントリストの選択
        private void UserEvent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            vm.SelectionChanged((ListView)sender);
        }

        /// <summary>
        /// 決定ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            mainwindow.targetUser = (UserData)u1.Tag;
            mainwindow.targetUser2 = (UserData)u2.Tag;
            mainwindow.userdata = (UserEventData)t1.Tag;
            mainwindow.userdata2 = (UserEventData)t2.Tag;
            UserData udata = (UserData)u1.Tag;
            UserEventData edata = (UserEventData)t1.Tag;

            mainwindow.mainWindowVM.ReSet(udata.name, udata.birth_str, udata.birth_place, udata.lat.ToString(), udata.lng.ToString(),
            edata.name, edata.birth_str, edata.birth_place, edata.lat.ToString(), edata.lng.ToString(), udata.timezone, edata.timezone);
            mainwindow.setYear.Text = ((UserData)u1.Tag).birth_year.ToString();
            mainwindow.setMonth.Text = ((UserData)u1.Tag).birth_month.ToString();
            mainwindow.setDay.Text = ((UserData)u1.Tag).birth_day.ToString();
            mainwindow.setHour.Text = ((UserData)u1.Tag).birth_hour.ToString();
            mainwindow.setMinute.Text = ((UserData)u1.Tag).birth_minute.ToString();
            mainwindow.setSecond.Text = ((UserData)u1.Tag).birth_second.ToString();
            mainwindow.ReCalc();
            mainwindow.ReRender();

            this.Visibility = Visibility.Hidden;
        }

        // 新規作成(何もないところ右クリック)
        private void NewDataContext_Click(object sender, RoutedEventArgs e)
        {
            newData();
        }

        // 新規作成(ファイル)
        // 選択状態の場合こちらが呼ばれる
        public void newItem_Click(object sender, EventArgs e)
        {
            newData();
        }

        // メニュー新規作成
        private void NewMenuItem_Click(object sender, RoutedEventArgs e)
        {
            newData();
        }


        /// <summary>
        /// イベントリスト右クリック→表示
        /// 決定と同様の動作
        /// </summary>
        public void disp_Click(object sender, EventArgs e)
        {
            if (UserEvent.SelectedItem == null)
            {
                return;
            }

            mainwindow.targetUser = (UserData)UserEvent.Items[0];
            mainwindow.mainWindowVM.userName = mainwindow.targetUser.name;
            mainwindow.mainWindowVM.userBirthStr = mainwindow.targetUser.birth_str;
            mainwindow.mainWindowVM.userBirthPlace = mainwindow.targetUser.birth_place;
            mainwindow.mainWindowVM.userLat = mainwindow.targetUser.lat.ToString("00.0000");
            mainwindow.mainWindowVM.userLng = mainwindow.targetUser.lng.ToString("000.0000");


            mainwindow.userdata = (UserEventData)UserEvent.SelectedItem;
            mainwindow.mainWindowVM.transitName = mainwindow.userdata.name.Replace("- ", "");
            mainwindow.mainWindowVM.transitBirthStr = mainwindow.userdata.birth_str;
            mainwindow.mainWindowVM.transitPlace = mainwindow.userdata.birth_place;
            mainwindow.mainWindowVM.transitLat = mainwindow.userdata.lat.ToString("00.0000");
            mainwindow.mainWindowVM.transitLng = mainwindow.userdata.lng.ToString("000.0000");


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

            UserEventTag tag = (UserEventTag)UserEvent.Tag;
            UserData udata = tag.udata;
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
            /*
            if (kaikiwindow == null)
            {
                kaikiwindow = new KaikiWindow();
            }
            kaikiwindow.Visibility = Visibility.Visible;
            */

            // 誕生日1日前から1時間ごとに計算
            // 原始的だけどとりあえずいいや
            if (UserEvent.SelectedItem == null)
            {
                return;
            }
            if (UserEvent.SelectedItem is UserData)
            {
                UserData data = (UserData)UserEvent.SelectedItem;
                DateTime currentDate = mainwindow.udataTime(data);
                Dictionary<int, PlanetData> currentPlanet = mainwindow.calc.PositionCalc(currentDate,
                    data.lat,
                    data.lng,
                    (int)mainwindow.config.houseCalc, 0);

                DateTime calcDate = currentDate.AddDays(364.5);
                int addOver = 0;
                for (int i = 0; i < 1000; i++)
                {
                    if (addOver == 3)
                    {
                        calcDate = calcDate.AddDays(0.5);
                    }
                    else if (addOver == 2)
                    {
                        calcDate = calcDate.AddDays(0.1);
                    }
                    else if (addOver == 1)
                    {
                        calcDate = calcDate.AddDays(0.01);
                    }
                    else
                    {
                        calcDate = calcDate.AddDays(0.001);
                    }
                    Dictionary<int, PlanetData> planet = mainwindow.calc.PositionCalc(calcDate,
                        data.lat,
                        data.lng,
                        (int)mainwindow.config.houseCalc, 0);
                    double diff = Math.Abs(currentPlanet[0].absolute_position - planet[0].absolute_position);
                    if (diff < 0.001)
                    {
//                        Console.WriteLine(currentPlanet[0].absolute_position.ToString());
//                        Console.WriteLine(planet[0].absolute_position.ToString());
//                        Console.WriteLine(i.ToString());
                        break;
                    }
                    else if (diff > 0.5)
                    {
                        addOver = 3;
                    }
                    else if (diff > 0.1)
                    {
                        addOver = 2;
                    }
                    else if (diff > 0.01)
                    {
                        addOver = 1;
                    }
                    else
                    {
                        addOver = 0;
//                        Console.WriteLine(Math.Abs(currentPlanet[0].absolute_position - planet[0].absolute_position));
                    }
                }
                //                MessageBox.Show(calcDate.ToString());
                newEvent_Click_CB("太陽回帰",
                    calcDate,
                    calcDate.Hour,
                    calcDate.Minute,
                    calcDate.Second,
                    data.birth_place,
                    data.lat,
                    data.lng,
                    "",
                    data.timezone);
                    
            }


        }

        /// <summary>
        /// イベントリストダブルクリック
        /// OK押下と同様
        /// </summary>
        public void userEvent_DoubleClick(object sender, EventArgs e)
        {
            if (UserEvent.SelectedItem == null)
            {
                return;
            }
            UserData udata;
            UserEventData edata;
            if (UserEvent.SelectedItem is UserData)
            {
                udata = (UserData)UserEvent.SelectedItem;
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
                    lat_lng = String.Format("{0:00.000}/{1:000.000}", udata.lat, udata.lng),
                    fullpath = udata.filename
                };
                mainwindow.userdata = edata;
                mainwindow.targetUser = udata;
                u1.Tag = udata;
                u1.Text = udata.name + "\n" + udata.birth_str_ymd + "\n" + udata.birth_str_his;
            }
            else
            {
                mainwindow.userdata = (UserEventData)UserEvent.SelectedItem;
                edata = (UserEventData)UserEvent.SelectedItem;
                udata = new UserData()
                {
                    name = edata.name,
                    birth_year = edata.birth_year,
                    birth_month = edata.birth_month,
                    birth_day = edata.birth_day,
                    birth_hour = edata.birth_hour,
                    birth_minute = edata.birth_minute,
                    birth_second = edata.birth_second,
                    birth_place = edata.birth_place,
                    lat = edata.lat,
                    lng = edata.lng,
                    timezone = edata.timezone,
                    memo = edata.memo,
                };
                mainwindow.targetUser = udata;
                t1.Tag = edata;
                t1.Text = edata.name.Replace("- ", "") + "\n" + edata.birth_str_ymd + "\n" + edata.birth_str_his;
            }
            mainwindow.userdata = edata;
            mainwindow.mainWindowVM.ReSet(udata.name, udata.birth_str, udata.birth_place, udata.lat.ToString(), udata.lng.ToString(),
                edata.name, edata.birth_str, edata.birth_place, edata.lat.ToString(), edata.lng.ToString(), udata.timezone, edata.timezone);
            mainwindow.ReCalc();
            mainwindow.ReRender();

            this.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// 新規作成(ファイル)コールバック
        /// 編集の場合もここが呼ばれる
        /// ここに来るfileNameはNoExt、拡張子なし
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="userName"></param>
        /// <param name="userFurigana"></param>
        /// <param name="userBirth"></param>
        /// <param name="userHour"></param>
        /// <param name="userMinute"></param>
        /// <param name="userSecond"></param>
        /// <param name="userPlace"></param>
        /// <param name="userLat"></param>
        /// <param name="userLng"></param>
        /// <param name="userMemo"></param>
        /// <param name="userTimezone"></param>
        /// <param name="isEdit">新規 or 編集</param>
        /// <param name="selectedItem">選択していたアイテム</param>
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
            string userTimezone,
            bool isEdit,
            TreeViewItem selectedItem
        )
        {
            // 可能性があるのは
            // 新規作成＋未選択
            // 新規作成＋ディレクトリ選択
            // 新規作成＋ファイル選択
            // 編集＋ファイル選択
            // 編集の場合、必ずファイル選択のはず
            // 欲しいのはパスだけ
            string newDir;
            string newPath;

            if (isEdit)
            {
                // 必ず存在、無ければバグ
                DbItem item0 = (DbItem)selectedItem.Tag;
                newDir = System.IO.Path.GetDirectoryName(item0.fileName);
                // 編集はファイル名も変えられる
                newPath = newDir + @"\" + fileName + ".csm";

                // 前のファイルを削除
                File.Delete(item0.fileName);
                // オブジェクトは消さない
                selectedItem.Header = fileName;
                item0.fileName = newPath;
                item0.userName = userName;
                item0.userFurigana = userFurigana;
            }
            else
            {
                // 新規作成の場合TreeViewにaddする
                TreeViewItem parentItem;

                if (selectedItem == null)
                {
                    // 未選択の場合data直下に生成
                    newDir = @"data";
                    parentItem = (TreeViewItem)UserDirTree.Items[0];
                }
                else
                {
                    // ファイル or ディレクトリ選択状態で新規作成
                    DbItem item0 = (DbItem)selectedItem.Tag;
                    if (item0.isDir)
                    {
                        // ディレクトリ選択時はその配下に作成
                        newDir = item0.fileName;
                        parentItem = (TreeViewItem)selectedItem;
                    }
                    else
                    {
                        // ファイル選択時は親のディレクトリ配下にぶら下げる
                        newDir = System.IO.Path.GetDirectoryName(item0.fileName);
                        parentItem = (TreeViewItem)selectedItem.Parent;
                    }
                }
                newPath = newDir + @"\" + fileName + ".csm";

                // 新規作成時なのでオブジェクトも作成
                TreeViewItem newItem = new TreeViewItem { Header = fileName };
                newItem.Tag = new DbItem
                {
                    fileName = newPath,
                    isDir = false,
                    userName = userName,
                    userFurigana = userFurigana
                };
                newItem.Selected += vm.UserItem_Selected;
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
            UserEventTag tag = (UserEventTag)UserEvent.Tag;
            UserData udata = tag.udata;
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
                fullpath = udata.filename,
                lat_lng = String.Format("{0:00.000}/{1:000.000}", eventLat, eventLng)
            };
            udata.userevent.Add(uevent);
            tag = new UserEventTag()
            {
                ecsm = false,
                udata = (UserData)uevent
            };
            UserEvent.Tag = tag;

            UserEvent.Items.Add(ueventdata);
            XmlSerializer serializer = new XmlSerializer(typeof(UserData));
            FileStream fs = new FileStream(udata.filename, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            serializer.Serialize(sw, udata);
            sw.Close();
            fs.Close();

        }

        /// <summary>
        /// イベント編集コールバック
        /// 今のところecsmの編集はできない
        /// </summary>
        /// <param name="index"></param>
        /// <param name="eventName"></param>
        /// <param name="eventBirth"></param>
        /// <param name="eventHour"></param>
        /// <param name="eventMinute"></param>
        /// <param name="eventSecond"></param>
        /// <param name="eventPlace"></param>
        /// <param name="eventLat"></param>
        /// <param name="eventLng"></param>
        /// <param name="eventMemo"></param>
        /// <param name="eventTimezone"></param>
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
            UserEventTag tag = (UserEventTag)UserEvent.Tag;
            UserData udata = tag.udata;
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
            // TODO DbItemコンストラクタ
            AddUserEditWindow(new DbItem
            {
                fileNameNoExt = "新規データ"
                + DateTime.Now.Year
                + DateTime.Now.Month.ToString("00")
                + DateTime.Now.Day.ToString("00")
                + DateTime.Now.Hour.ToString("00")
                + DateTime.Now.Minute.ToString("00")
                + DateTime.Now.Second.ToString("00"),
                fileName = "新規データ"
                + DateTime.Now.Year
                + DateTime.Now.Month.ToString("00")
                + DateTime.Now.Day.ToString("00")
                + DateTime.Now.Hour.ToString("00")
                + DateTime.Now.Minute.ToString("00")
                + DateTime.Now.Second.ToString("00")
                + ".csm",
                isDir = false,
                userName = "新規データ",
                userFurigana = "しんきでーた",
                userBirth = DateTime.Today,
                userHour = "12",
                userMinute = "0",
                userSecond = "0",
                userPlace = "東京都千代田区",
                userLat = "35.685175",
                userLng = "139.7528",
                userTimezone = "JST",
                memo = ""
            }, false);
        }

        // 右クリックイベントデータ追加
        public void newEventData()
        {
            UserEventTag tag = (UserEventTag)UserEvent.Tag;
            if (tag.ecsm)
            {
                MessageBox.Show("現在のバージョンではインポートフォルダ内のデータは編集できません。");
                return;
            }

            setDisable();
            DbItem item = new DbItem()
            {
                userName = "新規イベント",
                userBirth = DateTime.Today,
                userHour = "12",
                userMinute = "0",
                userSecond = "0",
                userPlace = "東京都千代田区",
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
            UserEventTag tag = (UserEventTag)UserEvent.Tag;
            if (tag.ecsm)
            {
                MessageBox.Show("現在のバージョンではインポートフォルダ内のデータは編集できません。");
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
                // ディレクトリを選択していた場合はその下に
                string dirName = iteminfo.fileName + @"\" + newDir;
                newItem.Tag = new DbItem
                {
                    fileName = dirName,
                    fileNameNoExt = dirName,
                    isDir = true
                };
                item.Items.Add(newItem);
                Directory.CreateDirectory(dirName);
            }
            else
            {
                // ファイルを選択していた場合は並列に
                if (item.Parent is TreeView)
                {
                    // 親が存在する
                    string dirName = @"data\" + newDir;
                    newItem.Tag = new DbItem
                    {
                        fileName = dirName,
                        fileNameNoExt = dirName,
                        isDir = true
                    };
                    item.Items.Add(newItem);
                    Directory.CreateDirectory(dirName);
                }
                else
                {
                    // 親が存在しない
                    TreeViewItem parentItem = (TreeViewItem)item.Parent;
                    DbItem parentIteminfo = (DbItem)parentItem.Tag;
                    if (parentIteminfo == null)
                    {
                        // dataを選択している
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
                        fileNameNoExt = dirName,
                        isDir = true
                    };
                    parentItem.Items.Add(newItem);
                    Directory.CreateDirectory(dirName);
                }
            }
        }

        // ツリー右クリック編集
        public void edit_Click(object sender, EventArgs e)
        {
            TreeViewItem item = (TreeViewItem)UserDirTree.SelectedItem;
            DbItem iteminfo = vm.createItem((DbItem)item.Tag);
            if (iteminfo == null)
            {
                return;
            }

            setDisable();
            AddUserEditWindow(iteminfo, true);
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
            vm.CreateTree();
        }

        // 編集ウィンドウ表示
        public void AddUserEditWindow(DbItem item, bool isEdit)
        {
            if (editwindow == null)
            {
                editwindow = new UserEditWindow(this, item);
            }
            else
            {
                editwindow.UserEditRefresh(item);
            }
            editwindow.isEdit = isEdit;
            editwindow.selected = (TreeViewItem)UserDirTree.SelectedItem;
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
                if (processWindow == null)
                {
                    processWindow = new DatabaseProcessWindow(this);
                }
                processWindow.Visibility = Visibility.Visible;
                processWindow.Focus();
                processWindow.startAmateru(oFD);
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
                if (processWindow == null)
                {
                    processWindow = new DatabaseProcessWindow(this);
                }
                processWindow.Visibility = Visibility.Visible;
                processWindow.Focus();
                processWindow.startSG(oFD);
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
                    vm.CreateTree();
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
                    vm.CreateTree();
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
                    vm.CreateTree();
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
                    vm.CreateTree();
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
                    vm.CreateTree();

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

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// ユーザー１
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void data1Button_Click(object sender, RoutedEventArgs e)
        {
            if (UserEvent.SelectedItem == null)
            {
                return;
            }
            UserEventTag utag = (UserEventTag)UserEvent.Tag;
            UserData udata;
            if (utag.ecsm)
            {
                udata = (UserData)UserEvent.SelectedItem;
            }
            else
            {
                if (UserEvent.SelectedItem is UserData)
                {
                    udata = (UserData)utag.udata;
                }
                else
                {
                    udata = (UserData)((UserEventData)UserEvent.SelectedItem);
                }
            }
            udata.timezone = CommonData.getTimezoneShortText(udata.timezone);

            u1.Tag = udata;
            u1.Text = udata.name + "\n" + udata.birth_str_ymd + "\n" + udata.birth_str_his;
        }

        /// <summary>
        /// ユーザー２
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void data2Button_Click(object sender, RoutedEventArgs e)
        {
            if (UserEvent.SelectedItem == null)
            {
                return;
            }
            UserEventTag utag = (UserEventTag)UserEvent.Tag;
            UserData udata;
            if (utag.ecsm)
            {
                udata = (UserData)UserEvent.SelectedItem;
            }
            else
            {
                if (UserEvent.SelectedItem is UserData)
                {
                    udata = (UserData)utag.udata;
                }
                else
                {
                    udata = (UserData)((UserEventData)UserEvent.SelectedItem);
                }
            }
            udata.timezone = CommonData.getTimezoneShortText(udata.timezone);

            u2.Tag = udata;
            u2.Text = udata.name + "\n" + udata.birth_str_ymd + "\n" + udata.birth_str_his;
        }

        private void data3Button_Click(object sender, RoutedEventArgs e)
        {
            if (UserEvent.SelectedItem == null)
            {
                return;
            }
            UserEventData edata;
            if (UserEvent.SelectedItem is UserData)
            {
                // udataは変更しない（でいいよね）
                edata = (UserEventData)((UserData)UserEvent.SelectedItem);
            }
            else
            {
                edata = (UserEventData)UserEvent.SelectedItem;
            }
            edata.timezone = CommonData.getTimezoneShortText(edata.timezone);
            t1.Tag = edata;
            t1.Text = edata.name + "\n" + edata.birth_str_ymd + "\n" + edata.birth_str_his;
        }

        private void data4Button_Click(object sender, RoutedEventArgs e)
        {
            if (UserEvent.SelectedItem == null)
            {
                return;
            }
            UserEventData edata;
            if (UserEvent.SelectedItem is UserData)
            {
                // udataは変更しない（でいいよね）
                edata = (UserEventData)((UserData)UserEvent.SelectedItem);
            }
            else
            {
                edata = (UserEventData)UserEvent.SelectedItem;
            }
            edata.timezone = CommonData.getTimezoneShortText(edata.timezone);
            t2.Text = edata.name + "\n" + edata.birth_str_ymd + "\n" + edata.birth_str_his;
            t2.Tag = edata;
        }
    }
}
