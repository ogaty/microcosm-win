using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Controls;
using System.Windows;
using microcosm.DB;
using System.Xml.Serialization;
using System.Windows.Input;

namespace microcosm.ViewModel
{
    public class UserTree
    {
        public string name { get; set; }
        public List<UserTree> items { get; set; }
        public string dirpath { get; set; }

        public UserTree(string _name)
        {
            this.name = _name;
        }
    }

    public class DatabaseWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string datadir { get; set; }
        public DatabaseWindow dbwindow { get; set; }

        public TreeViewItem _UserDir;
        public TreeViewItem UserDir
        {
            get
            {
                return _UserDir;
            }
            set
            {
                _UserDir = value;
                // とりあえずBinding無しで
                // OnPropertyChanged("UserDir");
            }
        }

        public ItemCollection _UItems;
        public ItemCollection UItems
        {
            get
            {
                return _UItems;
            }
            set
            {
                _UItems = value;
                OnPropertyChanged("Items");
            }
        }

        public List<UserEventData> _UserEventList;
        public List<UserEventData> UserEventList
        {
            get
            {
                return _UserEventList;
            }
            set
            {
                _UserEventList = value;
                OnPropertyChanged("UserEventList");
            }
        }

        public string _Memo;
        public string Memo
        {
            get
            {
                return _Memo;
            }
            set
            {
                _Memo = value;
                OnPropertyChanged("Memo");
            }
        }

        protected void OnPropertyChanged(string propertyname)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }

        }

        // constructor
        public DatabaseWindowViewModel(DatabaseWindow dbwindow)
        {
            this.dbwindow = dbwindow;
            string exePath = Environment.GetCommandLineArgs()[0];
            string exeFullPath = System.IO.Path.GetFullPath(exePath);
            datadir = System.IO.Path.GetDirectoryName(exeFullPath) + @"\data";

            _UserDir = new TreeViewItem();
            /*
            UserDir.Add(new UserTree("test1"));
            UserDir.Add(new UserTree("test2"));
            UserDir.Add(new UserTree("test3"));
            UserDir[0].items = new List<UserTree>();
            UserDir[0].items.Add(new UserTree("testA"));
            */

            CreateTree();
        }

        // ツリー作成
        public void CreateTree()
        {
            var rootDirectoryInfo = new DirectoryInfo(datadir);
            dbwindow.UserDirTree.Items.Clear();
            dbwindow.UserEvent.Items.Clear();
            if (!Directory.Exists(rootDirectoryInfo.FullName))
            {
                Directory.CreateDirectory(rootDirectoryInfo.FullName);
            }

            dbwindow.UserDirTree.Items.Add(CreateDirectoryNode(rootDirectoryInfo));
            TreeViewItem treeItem = (TreeViewItem)dbwindow.UserDirTree.Items[0];
            treeItem.IsExpanded = true;
        }

        // ツリー選択
        public void UserItem_Selected(object sender, System.Windows.RoutedEventArgs e)
        {
            TreeViewItem item = (TreeViewItem)sender;
            DbItem iteminfo = (DbItem)item.Tag;
            UserData udata = iteminfo.getUserdata();

            dbwindow.UserEvent.Items.Clear();
            dbwindow.UserEvent.Items.Add(udata);
            dbwindow.UserEvent.Tag = udata;

            if (udata.userevent == null)
            {
                return;
            }

            int i = 0;
            udata.userevent.ForEach(ev =>
            {
                dbwindow.UserEvent.Items.Add(createEventData(ev, iteminfo.fileName, i));
                i++;
            });
            ContextMenu context = new ContextMenu();
            MenuItem newItem = new MenuItem { Header = "表示" };
            newItem.Click += dbwindow.disp_Click;
            context.Items.Add(newItem);
            MenuItem addEventItem = new MenuItem { Header = "イベント追加" };
            addEventItem.Click += dbwindow.addEvent_Click;
            context.Items.Add(addEventItem);
            MenuItem editEventItem = new MenuItem { Header = "イベント編集" };
            editEventItem.Click += dbwindow.editEvent_Click;
            context.Items.Add(editEventItem);
            MenuItem deleteEventItem = new MenuItem { Header = "イベント削除" };
            deleteEventItem.Click += dbwindow.deleteEvent_Click;
            context.Items.Add(deleteEventItem);
            //            MenuItem returnEventItem = new MenuItem { Header = "回帰計算" };
            //            returnEventItem.Click += dbwindow.returnEvent_Click;
            //            context.Items.Add(returnEventItem);

            dbwindow.UserEvent.ContextMenu = context;
            dbwindow.UserEvent.MouseDoubleClick += dbwindow.userEvent_DoubleClick;
            dbwindow.UserEvent.SelectedIndex = 0;
            UserData data = (UserData)dbwindow.UserEvent.Items[0];
            Memo = data.memo;
        }

        // リスト上に表示されるデータを作成
        private UserEventData createEventData(UserEvent uevent, string filename, int index)
        {
            return new UserEventData()
            {
                name = "- " + uevent.event_name,
                birth_str = String.Format("{0}/{1:00}/{2:00} {3:00}:{4:00}:{5:00}",
                        uevent.event_year,
                        uevent.event_month,
                        uevent.event_day,
                        uevent.event_hour,
                        uevent.event_minute,
                        uevent.event_second
                    ),
                birth_year = uevent.event_year,
                birth_month = uevent.event_month,
                birth_minute = uevent.event_day,
                birth_hour = uevent.event_hour,
                birth_day = uevent.event_day,
                birth_second = uevent.event_second,
                birth_place = uevent.event_place,
                lat = uevent.event_lat,
                lng = uevent.event_lng,
                lat_lng = String.Format("{0:00.000}/{1:000.000}", uevent.event_lat, uevent.event_lng),
                memo = uevent.event_memo,
                timezone = uevent.event_timezone,
                fullpath = filename
            };
        }


        private void UserDir_Selected(object sender, System.Windows.RoutedEventArgs e)
        {
        }

        /// <summary>
        /// ディレクトリ再帰的呼び出し
        /// </summary>
        private TreeViewItem CreateDirectoryNode(DirectoryInfo directoryInfo)
        {
            if (!Directory.Exists(directoryInfo.FullName))
            {
                Directory.CreateDirectory(directoryInfo.FullName);
            }

            ContextMenu context = new ContextMenu();
            MenuItem newItem = new MenuItem { Header = "新規データ作成" };
            newItem.Click += dbwindow.newItem_Click;
            context.Items.Add(newItem);

            MenuItem newDirItem = new MenuItem { Header = "新規ディレクトリ作成" };
            newDirItem.Click += dbwindow.newDir_Click;
            context.Items.Add(newDirItem);

            MenuItem editItem = new MenuItem { Header = "編集" };
            editItem.Click += dbwindow.edit_Click;
            context.Items.Add(editItem);

            MenuItem deleteItem = new MenuItem { Header = "削除" };
            deleteItem.Click += dbwindow.deleteItem_Click;
            context.Items.Add(deleteItem);

            var directoryNode = new TreeViewItem { Header = directoryInfo.Name };
            directoryNode.ContextMenu = context;

            foreach (var directory in directoryInfo.GetDirectories())
            {
                // ディレクトリ
                TreeViewItem item = CreateDirectoryNode(directory);
                item.Tag = new DbItem
                {
                    fileName = directory.FullName,
                    isDir = true
                };
                item.ContextMenu = context;
                item.Selected += UserDir_Selected;
                item.MouseDoubleClick += UserDir_MouseDoubleClick;
                directoryNode.Items.Add(item);
            }

            foreach (var file in directoryInfo.GetFiles())
            {
                // ファイル
                string trimName = System.IO.Path.GetFileNameWithoutExtension(file.Name);
                TreeViewItem item = new TreeViewItem { Header = trimName };
                item.Tag = new DbItem
                {
                    fileName = file.FullName,
                    isDir = false
                };
                item.ContextMenu = context;
                item.Selected += UserItem_Selected;
                item.MouseDoubleClick += UserItem_MouseDoubleClick;
                directoryNode.Items.Add(item);
            }

            return directoryNode;
        }

        private void UserItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            TreeViewItem item = (TreeViewItem)sender;
            DbItem iteminfo = (DbItem)item.Tag;
            XMLDBManager DBMgr = new XMLDBManager(iteminfo.fileName);
            UserData data = DBMgr.getObject();
            UserEventData edata = new UserEventData()
            {
                name = data.name,
                birth_str = data.birth_str,
                birth_place = data.birth_place,
                birth_year = data.birth_year,
                birth_month = data.birth_month,
                birth_day = data.birth_day,
                birth_hour = data.birth_hour,
                birth_minute = data.birth_minute,
                birth_second = data.birth_second,
                lat = data.lat,
                lng = data.lng,
                lat_lng = data.lat_lng,
                memo = data.memo,
                timezone = data.timezone,
                fullpath = iteminfo.fileName
            };

            dbwindow.mainwindow.userdata = edata;
            dbwindow.mainwindow.mainWindowVM.ReSet(data.name, data.birth_str, data.birth_place, data.lat.ToString(), data.lng.ToString(),
                edata.name, edata.birth_str, edata.birth_place, edata.lat.ToString(), edata.lng.ToString(), data.timezone, edata.timezone);
            dbwindow.mainwindow.ReCalc();
            dbwindow.mainwindow.ReRender();

            dbwindow.Visibility = Visibility.Hidden;
        }

        private void UserDir_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            TreeViewItem item = (TreeViewItem)sender;
        }

        public DbItem createItem(DbItem item)
        {
            if (item == null)
            {
                return null;
            }
            if (Directory.Exists(item.fileName))
            {
                return null;
            }
            // xmlパースして編集ウィンドウを表示
            XMLDBManager DBMgr = new XMLDBManager(item.fileName);
            UserData data = DBMgr.getObject();

            item.fileNameNoExt = System.IO.Path.GetFileNameWithoutExtension(item.fileName);
            item.userName = data.name;
            item.userFurigana = data.furigana;
            item.userBirth = new DateTime(data.birth_year, data.birth_month, data.birth_day, data.birth_hour, data.birth_minute, data.birth_second);
            item.userHour = data.birth_hour.ToString();
            item.userMinute = data.birth_minute.ToString();
            item.userSecond = data.birth_second.ToString();
            item.userPlace = data.birth_place;
            item.userLat = data.lat.ToString("00.000");
            item.userLng = data.lng.ToString("000.000");
            item.userTimezone = data.timezone;
            item.memo = data.memo;

            return item;
        }


        // イベントリストの選択
        public void SelectionChanged(ListView item)
        {
            if (item.SelectedItem == null)
            {
                return;
            }
            if (item.SelectedItem is UserEventData)
            {
                UserEventData data = (UserEventData)item.SelectedItem;
                if (data != null)
                {
                    Memo = data.memo;
                }
            }
            else
            {
                UserData data = (UserData)item.SelectedItem;
                if (data != null)
                {
                    Memo = data.memo;
                }
            }
        }



    }
}
