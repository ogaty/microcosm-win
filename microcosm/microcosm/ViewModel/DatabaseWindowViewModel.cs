using System;
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

        public List<UserData> _UserEventList;
        public List<UserData> UserEventList
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

        private void UserItem_Selected(object sender, System.Windows.RoutedEventArgs e)
        {
            TreeViewItem item = (TreeViewItem)sender;
            XMLDBManager DBMgr = new XMLDBManager(item.Tag.ToString());
            UserData data = DBMgr.getObject();

            dbwindow.UserEvent.Items.Clear();
            dbwindow.UserEvent.Items.Add(data);
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
