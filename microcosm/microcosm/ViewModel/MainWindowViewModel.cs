using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace microcosm.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        // 左上ユーザー
        public string _userName;
        public string userName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
                OnPropertyChanged("userName");
            }
        }
        public string _userBirthStr;
        public string userBirthStr
        {
            get
            {
                return _userBirthStr;
            }
            set
            {
                _userBirthStr = value;
                OnPropertyChanged("userBirthStr");
            }
        }
        public string _userTimezone;
        public string userTimezone
        {
            get
            {
                return _userTimezone;
            }
            set
            {
                _userTimezone = value;
                OnPropertyChanged("userTimezone");
            }
        }
        public string _userBirthPlace;
        public string userBirthPlace
        {
            get
            {
                return _userBirthPlace;
            }
            set
            {
                _userBirthPlace = value;
                OnPropertyChanged("userBirthPlace");
            }
        }
        public string _userLat;
        public string userLat
        {
            get
            {
                return _userLat;
            }
            set
            {
                _userLat = value;
                OnPropertyChanged("userLat");
            }
        }
        public string _userLng;
        public string userLng
        {
            get
            {
                return _userLng;
            }
            set
            {
                _userLng = value;
                OnPropertyChanged("userLng");
            }
        }

        // 右上イベント
        public string _transitName;
        public string transitName
        {
            get
            {
                return _transitName;
            }
            set
            {
                _transitName = value;
                OnPropertyChanged("transitName");
            }
        }
        public string _transitBirthStr;
        public string transitBirthStr
        {
            get
            {
                return _transitBirthStr;
            }
            set
            {
                _transitBirthStr = value;
                OnPropertyChanged("transitBirthStr");
            }
        }
        public string _transitTimezone;
        public string transitTimezone
        {
            get
            {
                    return _transitTimezone;
            }
            set
            {
                _transitTimezone = value;
                OnPropertyChanged("transitTimezone");
            }
        }
        public string _transitPlace;
        public string transitPlace
        {
            get
            {
                return _transitPlace;
            }
            set
            {
                _transitPlace = value;
                OnPropertyChanged("transitPlace");
            }
        }
        public string _transitLat;
        public string transitLat
        {
            get
            {
                return _transitLat;
            }
            set
            {
                _transitLat = value;
                OnPropertyChanged("transitLat");
            }
        }
        public string _transitLng;
        public string transitLng
        {
            get
            {
                return _transitLng;
            }
            set
            {
                _transitLng = value;
                OnPropertyChanged("transitLng");
            }
        }
        public string _explanationTxt;
        public string explanationTxt
        {
            get
            {
                return _explanationTxt;
            }
            set
            {
                _explanationTxt = value;
                OnPropertyChanged("explanationTxt");
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
    }
}
