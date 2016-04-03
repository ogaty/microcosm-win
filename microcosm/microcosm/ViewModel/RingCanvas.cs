using microcosm.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace microcosm.ViewModel
{
    public class RingCanvas : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public RingCanvas(ConfigData config)
        {
            outerWidth = config.zodiacOuterWidth;
            outerHeight = config.zodiacOuterWidth;
            innerWidth = outerWidth - config.zodiacWidth;
            innerHeight = outerHeight - config.zodiacWidth;
            innerLeft = config.zodiacWidth / 2;
            innerTop = config.zodiacWidth / 2;
            centerWidth = config.zodiacCenter;
            centerHeight = config.zodiacCenter;
            centerLeft = config.zodiacOuterWidth / 2 - config.zodiacCenter / 2;
            centerTop = config.zodiacOuterWidth / 2 - config.zodiacCenter / 2;
        }

        // 獣帯外側
        private double _outerWidth;
        public double outerWidth
        {
            get
            {
                return _outerWidth;
            }
            set
            {
                _outerWidth = value;
                OnPropertyChanged("outerWidth");
            }
        }
        private double _outerHeight;
        public double outerHeight
        {
            get
            {
                return _outerHeight;
            }
            set
            {
                _outerHeight = value;
                OnPropertyChanged("outerHeight");
            }
        }
        // 獣帯内側
        private double _innerWidth;
        public double innerWidth
        {
            get
            {
                return _innerWidth;
            }
            set
            {
                _innerWidth = value;
                OnPropertyChanged("innerWidth");
            }
        }
        private double _innerHeight;
        public double innerHeight
        {
            get
            {
                return _innerHeight;
            }
            set
            {
                _innerHeight = value;
                OnPropertyChanged("innerHeight");
            }
        }
        private double _innerLeft;
        public double innerLeft
        {
            get
            {
                return _innerLeft;
            }
            set
            {
                _innerLeft = value;
                OnPropertyChanged("innerLeft");
            }
        }
        private double _innerTop;
        public double innerTop
        {
            get
            {
                return _innerTop;
            }
            set
            {
                _innerTop = value;
                OnPropertyChanged("innerTop");
            }
        }

        // 中心
        private double _centerWidth;
        public double centerWidth
        {
            get
            {
                return _centerWidth;
            }
            set
            {
                _centerWidth = value;
                OnPropertyChanged("centerWidth");
            }
        }

        private double _centerHeight;
        public double centerHeight
        {
            get
            {
                return _centerHeight;
            }
            set
            {
                _centerHeight = value;
                OnPropertyChanged("centerHeight");
            }
        }
        private double _centerLeft;
        public double centerLeft
        {
            get
            {
                return _centerLeft;
            }
            set
            {
                _centerLeft = value;
                OnPropertyChanged("centerLeft");
            }
        }
        private double _centerTop;
        public double centerTop
        {
            get
            {
                return _centerTop;
            }
            set
            {
                _centerTop = value;
                OnPropertyChanged("centerTop");
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
