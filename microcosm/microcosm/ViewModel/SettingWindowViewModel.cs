using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using microcosm.Config;

namespace microcosm.ViewModel
{
    public class SettingWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindow main;
        // public List<SettingData> sList { get; set; }
        private string _dispName;
        public string dispName
        {
            get
            {
                return _dispName;
            }
            set
            {
                _dispName = value;
                OnPropertyChanged("dispName");
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

        public SettingWindowViewModel(MainWindow main)
        {
            this.main = main;

            //sList = new List<SettingData>();
            //for (int i = 0; i < main.settings.Count(); i++)
            //{
            //    sList.Add(main.settings[i]);
            //}
        }
    }
}
