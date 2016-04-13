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
        public string _transitName;
        public string transitName;
        public string _transitBirthStr;
        public string transitBirthStr;
        public string _transitTimezone;
        public string transitTimezone;
        public string _transitPlace;
        public string transitPlace;
        public string _transitLat;
        public string transitLat;
        public string _transitLng;
        public string transitLng;

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
