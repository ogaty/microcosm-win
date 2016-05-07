﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Controls;
using microcosm.DB;

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
            if (!Directory.Exists(rootDirectoryInfo.FullName))
            {
                Directory.CreateDirectory(rootDirectoryInfo.FullName);
            }

            dbwindow.UserDirTree.Items.Add(CreateDirectoryNode(rootDirectoryInfo));
        }

        // ツリー選択
        private void UserItem_Selected(object sender, System.Windows.RoutedEventArgs e)
        {
            TreeViewItem item = (TreeViewItem)sender;
            XMLDBManager DBMgr = new XMLDBManager(item.Tag.ToString());
            UserData data = DBMgr.getObject();
            UserEventData udata = new UserEventData()
            {
                name = data.name,
                birth_str = data.birth_str,
                birth_place = data.birth_place,
                lat_lng = data.lat_lng,
                memo = data.memo,
                fullpath = item.Tag.ToString()
            };

            dbwindow.UserEvent.Items.Clear();
            dbwindow.UserEvent.Items.Add(udata);

            if (data.userevent == null)
            {
                return;
            }

            int i = 0;
            data.userevent.ForEach(ev =>
            {
                dbwindow.UserEvent.Items.Add(createEventData(ev, item.Tag.ToString(), i));
                i++;
            });
        }

        private void UserEvent_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            UserEventData udata = (UserEventData)sender;
            Memo = udata.memo;
            Console.WriteLine("memo");
        }

        private UserEventData createEventData(UserEvent uevent, string filename, int index)
        {
            return new UserEventData()
            {
                name = "- " + uevent.event_name,
                birth_str = String.Format("{0}年{1}月{2}日 {3:00}:{4:00}:{5:00}",
                        uevent.event_year,
                        uevent.event_month,
                        uevent.event_day,
                        uevent.event_hour,
                        uevent.event_minute,
                        uevent.event_second
                    ),
                birth_place = uevent.event_place,
                lat_lng = String.Format("({0},{1})", uevent.event_lat, uevent.event_lng),
                memo = uevent.event_memo,
                fullpath = filename
            };
        }


        private void UserDir_Selected(object sender, System.Windows.RoutedEventArgs e)
        {
            Console.WriteLine("dir");
        }

        // ディレクトリ再帰的呼び出し
        private TreeViewItem CreateDirectoryNode(DirectoryInfo directoryInfo)
        {
            if (!Directory.Exists(directoryInfo.FullName))
            {
                Directory.CreateDirectory(directoryInfo.FullName);
            }

            var directoryNode = new TreeViewItem { Header = directoryInfo.Name };
            foreach (var directory in directoryInfo.GetDirectories())
            {
                TreeViewItem item = CreateDirectoryNode(directory);
                item.Tag = directory.FullName;
                item.Selected += UserDir_Selected;
                directoryNode.Items.Add(item);
            }

            foreach (var file in directoryInfo.GetFiles())
            {
                TreeViewItem item = new TreeViewItem { Header = file.Name };
                item.Tag = file.FullName;
                item.Selected += UserItem_Selected;
                directoryNode.Items.Add(item);
            }

            return directoryNode;
        }
    }
}