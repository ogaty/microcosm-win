using microcosm.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace microcosm.ViewModel
{
    public class RingCanvasViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public RingCanvasViewModel(ConfigData config)
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
        #region
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
        #endregion

        // 獣帯内側
        #region
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
        #endregion

        // 中心
        #region
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
        #endregion

        // カスプ1
        #region
        private double _cusp1x1;
        public double cusp1x1
        {
            get
            {
                return _cusp1x1;
            }
            set
            {
                _cusp1x1 = value;
                OnPropertyChanged("cusp1x1");
            }
        }

        private double _cusp1y1;
        public double cusp1y1
        {
            get
            {
                return _cusp1y1;
            }
            set
            {
                _cusp1y1 = value;
                OnPropertyChanged("cusp1y1");
            }
        }

        private double _cusp1x2;
        public double cusp1x2
        {
            get
            {
                return _cusp1x2;
            }
            set
            {
                _cusp1x2 = value;
                OnPropertyChanged("cusp1x2");
            }
        }

        private double _cusp1y2;
        public double cusp1y2
        {
            get
            {
                return _cusp1y2;
            }
            set
            {
                _cusp1y2 = value;
                OnPropertyChanged("cusp1y2");
            }
        }

        #endregion

        // カスプ2
        #region
        private double _cusp2x1;
        public double cusp2x1
        {
            get
            {
                return _cusp2x1;
            }
            set
            {
                _cusp2x1 = value;
                OnPropertyChanged("cusp2x1");
            }
        }

        private double _cusp2y1;
        public double cusp2y1
        {
            get
            {
                return _cusp2y1;
            }
            set
            {
                _cusp2y1 = value;
                OnPropertyChanged("cusp2y1");
            }
        }

        private double _cusp2x2;
        public double cusp2x2
        {
            get
            {
                return _cusp2x2;
            }
            set
            {
                _cusp2x2 = value;
                OnPropertyChanged("cusp2x2");
            }
        }

        private double _cusp2y2;
        public double cusp2y2
        {
            get
            {
                return _cusp2y2;
            }
            set
            {
                _cusp2y2 = value;
                OnPropertyChanged("cusp2y2");
            }
        }

        #endregion

        // カスプ3
        #region
        private double _cusp3x1;
        public double cusp3x1
        {
            get
            {
                return _cusp3x1;
            }
            set
            {
                _cusp3x1 = value;
                OnPropertyChanged("cusp3x1");
            }
        }

        private double _cusp3y1;
        public double cusp3y1
        {
            get
            {
                return _cusp3y1;
            }
            set
            {
                _cusp3y1 = value;
                OnPropertyChanged("cusp3y1");
            }
        }

        private double _cusp3x2;
        public double cusp3x2
        {
            get
            {
                return _cusp3x2;
            }
            set
            {
                _cusp3x2 = value;
                OnPropertyChanged("cusp3x2");
            }
        }

        private double _cusp3y2;
        public double cusp3y2
        {
            get
            {
                return _cusp3y2;
            }
            set
            {
                _cusp3y2 = value;
                OnPropertyChanged("cusp3y2");
            }
        }

        #endregion

        // カスプ4
        #region
        private double _cusp4x1;
        public double cusp4x1
        {
            get
            {
                return _cusp4x1;
            }
            set
            {
                _cusp4x1 = value;
                OnPropertyChanged("cusp4x1");
            }
        }

        private double _cusp4y1;
        public double cusp4y1
        {
            get
            {
                return _cusp4y1;
            }
            set
            {
                _cusp4y1 = value;
                OnPropertyChanged("cusp4y1");
            }
        }

        private double _cusp4x2;
        public double cusp4x2
        {
            get
            {
                return _cusp4x2;
            }
            set
            {
                _cusp4x2 = value;
                OnPropertyChanged("cusp4x2");
            }
        }

        private double _cusp4y2;
        public double cusp4y2
        {
            get
            {
                return _cusp4y2;
            }
            set
            {
                _cusp4y2 = value;
                OnPropertyChanged("cusp4y2");
            }
        }

        #endregion

        // カスプ5
        #region
        private double _cusp5x1;
        public double cusp5x1
        {
            get
            {
                return _cusp5x1;
            }
            set
            {
                _cusp5x1 = value;
                OnPropertyChanged("cusp5x1");
            }
        }

        private double _cusp5y1;
        public double cusp5y1
        {
            get
            {
                return _cusp5y1;
            }
            set
            {
                _cusp5y1 = value;
                OnPropertyChanged("cusp5y1");
            }
        }

        private double _cusp5x2;
        public double cusp5x2
        {
            get
            {
                return _cusp5x2;
            }
            set
            {
                _cusp5x2 = value;
                OnPropertyChanged("cusp5x2");
            }
        }

        private double _cusp5y2;
        public double cusp5y2
        {
            get
            {
                return _cusp5y2;
            }
            set
            {
                _cusp5y2 = value;
                OnPropertyChanged("cusp5y2");
            }
        }

        #endregion

        // カスプ6
        #region
        private double _cusp6x1;
        public double cusp6x1
        {
            get
            {
                return _cusp6x1;
            }
            set
            {
                _cusp6x1 = value;
                OnPropertyChanged("cusp6x1");
            }
        }

        private double _cusp6y1;
        public double cusp6y1
        {
            get
            {
                return _cusp6y1;
            }
            set
            {
                _cusp6y1 = value;
                OnPropertyChanged("cusp6y1");
            }
        }

        private double _cusp6x2;
        public double cusp6x2
        {
            get
            {
                return _cusp6x2;
            }
            set
            {
                _cusp6x2 = value;
                OnPropertyChanged("cusp6x2");
            }
        }

        private double _cusp6y2;
        public double cusp6y2
        {
            get
            {
                return _cusp6y2;
            }
            set
            {
                _cusp6y2 = value;
                OnPropertyChanged("cusp6y2");
            }
        }

        #endregion

        // カスプ7
        #region
        private double _cusp7x1;
        public double cusp7x1
        {
            get
            {
                return _cusp7x1;
            }
            set
            {
                _cusp7x1 = value;
                OnPropertyChanged("cusp7x1");
            }
        }

        private double _cusp7y1;
        public double cusp7y1
        {
            get
            {
                return _cusp7y1;
            }
            set
            {
                _cusp7y1 = value;
                OnPropertyChanged("cusp7y1");
            }
        }

        private double _cusp7x2;
        public double cusp7x2
        {
            get
            {
                return _cusp7x2;
            }
            set
            {
                _cusp7x2 = value;
                OnPropertyChanged("cusp7x2");
            }
        }

        private double _cusp7y2;
        public double cusp7y2
        {
            get
            {
                return _cusp7y2;
            }
            set
            {
                _cusp7y2 = value;
                OnPropertyChanged("cusp7y2");
            }
        }

        #endregion

        // カスプ8
        #region
        private double _cusp8x1;
        public double cusp8x1
        {
            get
            {
                return _cusp8x1;
            }
            set
            {
                _cusp8x1 = value;
                OnPropertyChanged("cusp8x1");
            }
        }

        private double _cusp8y1;
        public double cusp8y1
        {
            get
            {
                return _cusp8y1;
            }
            set
            {
                _cusp8y1 = value;
                OnPropertyChanged("cusp8y1");
            }
        }

        private double _cusp8x2;
        public double cusp8x2
        {
            get
            {
                return _cusp8x2;
            }
            set
            {
                _cusp8x2 = value;
                OnPropertyChanged("cusp8x2");
            }
        }

        private double _cusp8y2;
        public double cusp8y2
        {
            get
            {
                return _cusp8y2;
            }
            set
            {
                _cusp8y2 = value;
                OnPropertyChanged("cusp8y2");
            }
        }

        #endregion

        // カスプ9
        #region
        private double _cusp9x1;
        public double cusp9x1
        {
            get
            {
                return _cusp9x1;
            }
            set
            {
                _cusp9x1 = value;
                OnPropertyChanged("cusp9x1");
            }
        }

        private double _cusp9y1;
        public double cusp9y1
        {
            get
            {
                return _cusp9y1;
            }
            set
            {
                _cusp9y1 = value;
                OnPropertyChanged("cusp9y1");
            }
        }

        private double _cusp9x2;
        public double cusp9x2
        {
            get
            {
                return _cusp9x2;
            }
            set
            {
                _cusp9x2 = value;
                OnPropertyChanged("cusp9x2");
            }
        }

        private double _cusp9y2;
        public double cusp9y2
        {
            get
            {
                return _cusp9y2;
            }
            set
            {
                _cusp9y2 = value;
                OnPropertyChanged("cusp9y2");
            }
        }

        #endregion

        // カスプ10
        #region
        private double _cusp10x1;
        public double cusp10x1
        {
            get
            {
                return _cusp10x1;
            }
            set
            {
                _cusp10x1 = value;
                OnPropertyChanged("cusp10x1");
            }
        }

        private double _cusp10y1;
        public double cusp10y1
        {
            get
            {
                return _cusp10y1;
            }
            set
            {
                _cusp10y1 = value;
                OnPropertyChanged("cusp10y1");
            }
        }

        private double _cusp10x2;
        public double cusp10x2
        {
            get
            {
                return _cusp10x2;
            }
            set
            {
                _cusp10x2 = value;
                OnPropertyChanged("cusp10x2");
            }
        }

        private double _cusp10y2;
        public double cusp10y2
        {
            get
            {
                return _cusp10y2;
            }
            set
            {
                _cusp10y2 = value;
                OnPropertyChanged("cusp10y2");
            }
        }

        #endregion

        // カスプ11
        #region
        private double _cusp11x1;
        public double cusp11x1
        {
            get
            {
                return _cusp11x1;
            }
            set
            {
                _cusp11x1 = value;
                OnPropertyChanged("cusp11x1");
            }
        }

        private double _cusp11y1;
        public double cusp11y1
        {
            get
            {
                return _cusp11y1;
            }
            set
            {
                _cusp11y1 = value;
                OnPropertyChanged("cusp11y1");
            }
        }

        private double _cusp11x2;
        public double cusp11x2
        {
            get
            {
                return _cusp11x2;
            }
            set
            {
                _cusp11x2 = value;
                OnPropertyChanged("cusp11x2");
            }
        }

        private double _cusp11y2;
        public double cusp11y2
        {
            get
            {
                return _cusp11y2;
            }
            set
            {
                _cusp11y2 = value;
                OnPropertyChanged("cusp11y2");
            }
        }

        #endregion

        // カスプ12
        #region
        private double _cusp12x1;
        public double cusp12x1
        {
            get
            {
                return _cusp12x1;
            }
            set
            {
                _cusp12x1 = value;
                OnPropertyChanged("cusp12x1");
            }
        }

        private double _cusp12y1;
        public double cusp12y1
        {
            get
            {
                return _cusp12y1;
            }
            set
            {
                _cusp12y1 = value;
                OnPropertyChanged("cusp12y1");
            }
        }

        private double _cusp12x2;
        public double cusp12x2
        {
            get
            {
                return _cusp12x2;
            }
            set
            {
                _cusp12x2 = value;
                OnPropertyChanged("cusp12x2");
            }
        }

        private double _cusp12y2;
        public double cusp12y2
        {
            get
            {
                return _cusp12y2;
            }
            set
            {
                _cusp12y2 = value;
                OnPropertyChanged("cusp12y2");
            }
        }

        #endregion

        // カスプ1
        #region
        private double _scusp1x1;
        public double scusp1x1
        {
            get
            {
                return _scusp1x1;
            }
            set
            {
                _scusp1x1 = value;
                OnPropertyChanged("scusp1x1");
            }
        }

        private double _scusp1y1;
        public double scusp1y1
        {
            get
            {
                return _scusp1y1;
            }
            set
            {
                _scusp1y1 = value;
                OnPropertyChanged("scusp1y1");
            }
        }

        private double _scusp1x2;
        public double scusp1x2
        {
            get
            {
                return _scusp1x2;
            }
            set
            {
                _scusp1x2 = value;
                OnPropertyChanged("scusp1x2");
            }
        }

        private double _scusp1y2;
        public double scusp1y2
        {
            get
            {
                return _scusp1y2;
            }
            set
            {
                _scusp1y2 = value;
                OnPropertyChanged("scusp1y2");
            }
        }

        #endregion

        // カスプ2
        #region
        private double _scusp2x1;
        public double scusp2x1
        {
            get
            {
                return _scusp2x1;
            }
            set
            {
                _scusp2x1 = value;
                OnPropertyChanged("scusp2x1");
            }
        }

        private double _scusp2y1;
        public double scusp2y1
        {
            get
            {
                return _scusp2y1;
            }
            set
            {
                _scusp2y1 = value;
                OnPropertyChanged("scusp2y1");
            }
        }

        private double _scusp2x2;
        public double scusp2x2
        {
            get
            {
                return _scusp2x2;
            }
            set
            {
                _scusp2x2 = value;
                OnPropertyChanged("scusp2x2");
            }
        }

        private double _scusp2y2;
        public double scusp2y2
        {
            get
            {
                return _scusp2y2;
            }
            set
            {
                _scusp2y2 = value;
                OnPropertyChanged("scusp2y2");
            }
        }

        #endregion

        // カスプ3
        #region
        private double _scusp3x1;
        public double scusp3x1
        {
            get
            {
                return _scusp3x1;
            }
            set
            {
                _scusp3x1 = value;
                OnPropertyChanged("scusp3x1");
            }
        }

        private double _scusp3y1;
        public double scusp3y1
        {
            get
            {
                return _scusp3y1;
            }
            set
            {
                _scusp3y1 = value;
                OnPropertyChanged("scusp3y1");
            }
        }

        private double _scusp3x2;
        public double scusp3x2
        {
            get
            {
                return _scusp3x2;
            }
            set
            {
                _scusp3x2 = value;
                OnPropertyChanged("scusp3x2");
            }
        }

        private double _scusp3y2;
        public double scusp3y2
        {
            get
            {
                return _scusp3y2;
            }
            set
            {
                _scusp3y2 = value;
                OnPropertyChanged("scusp3y2");
            }
        }

        #endregion

        // カスプ4
        #region
        private double _scusp4x1;
        public double scusp4x1
        {
            get
            {
                return _scusp4x1;
            }
            set
            {
                _scusp4x1 = value;
                OnPropertyChanged("scusp4x1");
            }
        }

        private double _scusp4y1;
        public double scusp4y1
        {
            get
            {
                return _scusp4y1;
            }
            set
            {
                _scusp4y1 = value;
                OnPropertyChanged("scusp4y1");
            }
        }

        private double _scusp4x2;
        public double scusp4x2
        {
            get
            {
                return _scusp4x2;
            }
            set
            {
                _scusp4x2 = value;
                OnPropertyChanged("scusp4x2");
            }
        }

        private double _scusp4y2;
        public double scusp4y2
        {
            get
            {
                return _scusp4y2;
            }
            set
            {
                _scusp4y2 = value;
                OnPropertyChanged("scusp4y2");
            }
        }

        #endregion

        // カスプ5
        #region
        private double _scusp5x1;
        public double scusp5x1
        {
            get
            {
                return _scusp5x1;
            }
            set
            {
                _scusp5x1 = value;
                OnPropertyChanged("scusp5x1");
            }
        }

        private double _scusp5y1;
        public double scusp5y1
        {
            get
            {
                return _scusp5y1;
            }
            set
            {
                _scusp5y1 = value;
                OnPropertyChanged("scusp5y1");
            }
        }

        private double _scusp5x2;
        public double scusp5x2
        {
            get
            {
                return _scusp5x2;
            }
            set
            {
                _scusp5x2 = value;
                OnPropertyChanged("scusp5x2");
            }
        }

        private double _scusp5y2;
        public double scusp5y2
        {
            get
            {
                return _scusp5y2;
            }
            set
            {
                _scusp5y2 = value;
                OnPropertyChanged("scusp5y2");
            }
        }

        #endregion

        // カスプ6
        #region
        private double _scusp6x1;
        public double scusp6x1
        {
            get
            {
                return _scusp6x1;
            }
            set
            {
                _scusp6x1 = value;
                OnPropertyChanged("scusp6x1");
            }
        }

        private double _scusp6y1;
        public double scusp6y1
        {
            get
            {
                return _scusp6y1;
            }
            set
            {
                _scusp6y1 = value;
                OnPropertyChanged("scusp6y1");
            }
        }

        private double _scusp6x2;
        public double scusp6x2
        {
            get
            {
                return _scusp6x2;
            }
            set
            {
                _scusp6x2 = value;
                OnPropertyChanged("scusp6x2");
            }
        }

        private double _scusp6y2;
        public double scusp6y2
        {
            get
            {
                return _scusp6y2;
            }
            set
            {
                _scusp6y2 = value;
                OnPropertyChanged("scusp6y2");
            }
        }

        #endregion

        // カスプ7
        #region
        private double _scusp7x1;
        public double scusp7x1
        {
            get
            {
                return _scusp7x1;
            }
            set
            {
                _scusp7x1 = value;
                OnPropertyChanged("scusp7x1");
            }
        }

        private double _scusp7y1;
        public double scusp7y1
        {
            get
            {
                return _scusp7y1;
            }
            set
            {
                _scusp7y1 = value;
                OnPropertyChanged("scusp7y1");
            }
        }

        private double _scusp7x2;
        public double scusp7x2
        {
            get
            {
                return _scusp7x2;
            }
            set
            {
                _scusp7x2 = value;
                OnPropertyChanged("scusp7x2");
            }
        }

        private double _scusp7y2;
        public double scusp7y2
        {
            get
            {
                return _scusp7y2;
            }
            set
            {
                _scusp7y2 = value;
                OnPropertyChanged("scusp7y2");
            }
        }

        #endregion

        // カスプ8
        #region
        private double _scusp8x1;
        public double scusp8x1
        {
            get
            {
                return _scusp8x1;
            }
            set
            {
                _scusp8x1 = value;
                OnPropertyChanged("scusp8x1");
            }
        }

        private double _scusp8y1;
        public double scusp8y1
        {
            get
            {
                return _scusp8y1;
            }
            set
            {
                _scusp8y1 = value;
                OnPropertyChanged("scusp8y1");
            }
        }

        private double _scusp8x2;
        public double scusp8x2
        {
            get
            {
                return _scusp8x2;
            }
            set
            {
                _scusp8x2 = value;
                OnPropertyChanged("scusp8x2");
            }
        }

        private double _scusp8y2;
        public double scusp8y2
        {
            get
            {
                return _scusp8y2;
            }
            set
            {
                _scusp8y2 = value;
                OnPropertyChanged("scusp8y2");
            }
        }

        #endregion

        // カスプ9
        #region
        private double _scusp9x1;
        public double scusp9x1
        {
            get
            {
                return _scusp9x1;
            }
            set
            {
                _scusp9x1 = value;
                OnPropertyChanged("scusp9x1");
            }
        }

        private double _scusp9y1;
        public double scusp9y1
        {
            get
            {
                return _scusp9y1;
            }
            set
            {
                _scusp9y1 = value;
                OnPropertyChanged("scusp9y1");
            }
        }

        private double _scusp9x2;
        public double scusp9x2
        {
            get
            {
                return _scusp9x2;
            }
            set
            {
                _scusp9x2 = value;
                OnPropertyChanged("scusp9x2");
            }
        }

        private double _scusp9y2;
        public double scusp9y2
        {
            get
            {
                return _scusp9y2;
            }
            set
            {
                _scusp9y2 = value;
                OnPropertyChanged("scusp9y2");
            }
        }

        #endregion

        // カスプ10
        #region
        private double _scusp10x1;
        public double scusp10x1
        {
            get
            {
                return _scusp10x1;
            }
            set
            {
                _scusp10x1 = value;
                OnPropertyChanged("scusp10x1");
            }
        }

        private double _scusp10y1;
        public double scusp10y1
        {
            get
            {
                return _scusp10y1;
            }
            set
            {
                _scusp10y1 = value;
                OnPropertyChanged("scusp10y1");
            }
        }

        private double _scusp10x2;
        public double scusp10x2
        {
            get
            {
                return _scusp10x2;
            }
            set
            {
                _scusp10x2 = value;
                OnPropertyChanged("scusp10x2");
            }
        }

        private double _scusp10y2;
        public double scusp10y2
        {
            get
            {
                return _scusp10y2;
            }
            set
            {
                _scusp10y2 = value;
                OnPropertyChanged("scusp10y2");
            }
        }

        #endregion

        // カスプ11
        #region
        private double _scusp11x1;
        public double scusp11x1
        {
            get
            {
                return _scusp11x1;
            }
            set
            {
                _scusp11x1 = value;
                OnPropertyChanged("scusp11x1");
            }
        }

        private double _scusp11y1;
        public double scusp11y1
        {
            get
            {
                return _scusp11y1;
            }
            set
            {
                _scusp11y1 = value;
                OnPropertyChanged("scusp11y1");
            }
        }

        private double _scusp11x2;
        public double scusp11x2
        {
            get
            {
                return _scusp11x2;
            }
            set
            {
                _scusp11x2 = value;
                OnPropertyChanged("scusp11x2");
            }
        }

        private double _scusp11y2;
        public double scusp11y2
        {
            get
            {
                return _scusp11y2;
            }
            set
            {
                _scusp11y2 = value;
                OnPropertyChanged("scusp11y2");
            }
        }

        #endregion

        // カスプ12
        #region
        private double _scusp12x1;
        public double scusp12x1
        {
            get
            {
                return _scusp12x1;
            }
            set
            {
                _scusp12x1 = value;
                OnPropertyChanged("scusp12x1");
            }
        }

        private double _scusp12y1;
        public double scusp12y1
        {
            get
            {
                return _scusp12y1;
            }
            set
            {
                _scusp12y1 = value;
                OnPropertyChanged("scusp12y1");
            }
        }

        private double _scusp12x2;
        public double scusp12x2
        {
            get
            {
                return _scusp12x2;
            }
            set
            {
                _scusp12x2 = value;
                OnPropertyChanged("scusp12x2");
            }
        }

        private double _scusp12y2;
        public double scusp12y2
        {
            get
            {
                return _scusp12y2;
            }
            set
            {
                _scusp12y2 = value;
                OnPropertyChanged("scusp12y2");
            }
        }

        #endregion

        // 牡羊座
        #region
        private double _ariesx;
        public double ariesx
        {
            get
            {
                return _ariesx;
            }
            set
            {
                _ariesx = value;
                OnPropertyChanged("ariesx");
            }
        }

        private double _ariesy;
        public double ariesy
        {
            get
            {
                return _ariesy;
            }
            set
            {
                _ariesy = value;
                OnPropertyChanged("ariesy");
            }
        }

        private string _ariestxt;
        public string ariestxt
        {
            get
            {
                return _ariestxt;
            }
            set
            {
                _ariestxt = value;
                OnPropertyChanged("ariestxt");
            }
        }

        #endregion

        // 牡牛座
        #region
        private double _taurusx;
        public double taurusx
        {
            get
            {
                return _taurusx;
            }
            set
            {
                _taurusx = value;
                OnPropertyChanged("taurusx");
            }
        }

        private double _taurusy;
        public double taurusy
        {
            get
            {
                return _taurusy;
            }
            set
            {
                _taurusy = value;
                OnPropertyChanged("taurusy");
            }
        }
        private string _taurustxt;
        public string taurustxt
        {
            get
            {
                return _taurustxt;
            }
            set
            {
                _taurustxt = value;
                OnPropertyChanged("taurustxt");
            }
        }

        #endregion

        // 双子座
        #region
        private double _geminix;
        public double geminix
        {
            get
            {
                return _geminix;
            }
            set
            {
                _geminix = value;
                OnPropertyChanged("geminix");
            }
        }

        private double _geminiy;
        public double geminiy
        {
            get
            {
                return _geminiy;
            }
            set
            {
                _geminiy = value;
                OnPropertyChanged("geminiy");
            }
        }
        private string _geminitxt;
        public string geminitxt
        {
            get
            {
                return _geminitxt;
            }
            set
            {
                _geminitxt = value;
                OnPropertyChanged("geminitxt");
            }
        }


        #endregion

        // 蟹座
        #region
        private double _cancerx;
        public double cancerx
        {
            get
            {
                return _cancerx;
            }
            set
            {
                _cancerx = value;
                OnPropertyChanged("cancerx");
            }
        }

        private double _cancery;
        public double cancery
        {
            get
            {
                return _cancery;
            }
            set
            {
                _cancery = value;
                OnPropertyChanged("cancery");
            }
        }

        private string _cancertxt;
        public string cancertxt
        {
            get
            {
                return _cancertxt;
            }
            set
            {
                _cancertxt = value;
                OnPropertyChanged("cancertxt");
            }
        }

        #endregion

        // 獅子座
        #region
        private double _leox;
        public double leox
        {
            get
            {
                return _leox;
            }
            set
            {
                _leox = value;
                OnPropertyChanged("leox");
            }
        }

        private double _leoy;
        public double leoy
        {
            get
            {
                return _leoy;
            }
            set
            {
                _leoy = value;
                OnPropertyChanged("leoy");
            }
        }

        private string _leotxt;
        public string leotxt
        {
            get
            {
                return _leotxt;
            }
            set
            {
                _leotxt = value;
                OnPropertyChanged("leotxt");
            }
        }

        #endregion

        // 乙女座
        #region
        private double _virgox;
        public double virgox
        {
            get
            {
                return _virgox;
            }
            set
            {
                _virgox = value;
                OnPropertyChanged("virgox");
            }
        }

        private double _virgoy;
        public double virgoy
        {
            get
            {
                return _virgoy;
            }
            set
            {
                _virgoy = value;
                OnPropertyChanged("virgoy");
            }
        }

        private string _virgotxt;
        public string virgotxt
        {
            get
            {
                return _virgotxt;
            }
            set
            {
                _virgotxt = value;
                OnPropertyChanged("virgotxt");
            }
        }

        #endregion

        // 天秤座
        #region
        private double _librax;
        public double librax
        {
            get
            {
                return _librax;
            }
            set
            {
                _librax = value;
                OnPropertyChanged("librax");
            }
        }

        private double _libray;
        public double libray
        {
            get
            {
                return _libray;
            }
            set
            {
                _libray = value;
                OnPropertyChanged("libray");
            }
        }

        private string _libratxt;
        public string libratxt
        {
            get
            {
                return _libratxt;
            }
            set
            {
                _libratxt = value;
                OnPropertyChanged("libratxt");
            }
        }

        #endregion

        // 蠍座
        #region
        private double _scorpionx;
        public double scorpionx
        {
            get
            {
                return _scorpionx;
            }
            set
            {
                _scorpionx = value;
                OnPropertyChanged("scorpionx");
            }
        }

        private double _scorpiony;
        public double scorpiony
        {
            get
            {
                return _scorpiony;
            }
            set
            {
                _scorpiony = value;
                OnPropertyChanged("scorpiony");
            }
        }

        private string _scorpiontxt;
        public string scorpiontxt
        {
            get
            {
                return _scorpiontxt;
            }
            set
            {
                _scorpiontxt = value;
                OnPropertyChanged("scorpiontxt");
            }
        }
        #endregion

        // 射手座
        #region
        private double _sagittariusx;
        public double sagittariusx
        {
            get
            {
                return _sagittariusx;
            }
            set
            {
                _sagittariusx = value;
                OnPropertyChanged("sagittariusx");
            }
        }

        private double _sagittariusy;
        public double sagittariusy
        {
            get
            {
                return _sagittariusy;
            }
            set
            {
                _sagittariusy = value;
                OnPropertyChanged("sagittariusy");
            }
        }

        private string _sagittariustxt;
        public string sagittariustxt
        {
            get
            {
                return _sagittariustxt;
            }
            set
            {
                _sagittariustxt = value;
                OnPropertyChanged("sagittariustxt");
            }
        }
        #endregion

        // 山羊座
        #region
        private double _capricornx;
        public double capricornx
        {
            get
            {
                return _capricornx;
            }
            set
            {
                _capricornx = value;
                OnPropertyChanged("capricornx");
            }
        }

        private double _capricorny;
        public double capricorny
        {
            get
            {
                return _capricorny;
            }
            set
            {
                _capricorny = value;
                OnPropertyChanged("capricorny");
            }
        }
        private string _capricorntxt;
        public string capricorntxt
        {
            get
            {
                return _capricorntxt;
            }
            set
            {
                _capricorntxt = value;
                OnPropertyChanged("capricorntxt");
            }
        }

        #endregion

        // 水瓶座
        #region
        private double _aquariusx;
        public double aquariusx
        {
            get
            {
                return _aquariusx;
            }
            set
            {
                _aquariusx = value;
                OnPropertyChanged("aquariusx");
            }
        }

        private double _aquariusy;
        public double aquariusy
        {
            get
            {
                return _aquariusy;
            }
            set
            {
                _aquariusy = value;
                OnPropertyChanged("aquariusy");
            }
        }
        private string _aquariustxt;
        public string aquariustxt
        {
            get
            {
                return _aquariustxt;
            }
            set
            {
                _aquariustxt = value;
                OnPropertyChanged("aquariustxt");
            }
        }

        #endregion

        // 魚座
        #region
        private double _piscesx;
        public double piscesx
        {
            get
            {
                return _piscesx;
            }
            set
            {
                _piscesx = value;
                OnPropertyChanged("piscesx");
            }
        }

        private double _piscesy;
        public double piscesy
        {
            get
            {
                return _piscesy;
            }
            set
            {
                _piscesy = value;
                OnPropertyChanged("piscesy");
            }
        }
        private string _piscestxt;
        public string piscestxt
        {
            get
            {
                return _piscestxt;
            }
            set
            {
                _piscestxt = value;
                OnPropertyChanged("piscestxt");
            }
        }

        #endregion

        // 太陽
        #region
        private double _natalsunx;
        public double natalsunx
        {
            get
            {
                return _natalsunx;
            }
            set
            {
                _natalsunx = value;
                OnPropertyChanged("natalsunx");
            }
        }

        private double _natalsuny;
        public double natalsuny
        {
            get
            {
                return _natalsuny;
            }
            set
            {
                _natalsuny = value;
                OnPropertyChanged("natalsuny");
            }
        }
        private string _natalsuntxt;
        public string natalsuntxt
        {
            get
            {
                return _natalsuntxt;
            }
            set
            {
                _natalsuntxt = value;
                OnPropertyChanged("natalsuntxt");
            }
        }

        #endregion

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
