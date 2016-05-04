﻿using microcosm.Config;
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
        private double _natalSunx;
        public double natalSunx
        {
            get
            {
                return _natalSunx;
            }
            set
            {
                _natalSunx = value;
                OnPropertyChanged("natalSunx");
            }
        }

        private double _natalSuny;
        public double natalSuny
        {
            get
            {
                return _natalSuny;
            }
            set
            {
                _natalSuny = value;
                OnPropertyChanged("natalSuny");
            }
        }
        private string _natalSuntxt;
        public string natalSuntxt
        {
            get
            {
                return _natalSuntxt;
            }
            set
            {
                _natalSuntxt = value;
                OnPropertyChanged("natalSuntxt");
            }
        }
        private double _natalSundegreex;
        public double natalSundegreex
        {
            get
            {
                return _natalSundegreex;
            }
            set
            {
                _natalSundegreex = value;
                OnPropertyChanged("natalSundegreex");
            }
        }

        private double _natalSundegreey;
        public double natalSundegreey
        {
            get
            {
                return _natalSundegreey;
            }
            set
            {
                _natalSundegreey = value;
                OnPropertyChanged("natalSundegreey");
            }
        }
        private string _natalSundegreetxt;
        public string natalSundegreetxt
        {
            get
            {
                return _natalSundegreetxt;
            }
            set
            {
                _natalSundegreetxt = value;
                OnPropertyChanged("natalSundegreetxt");
            }
        }
        private double _natalSunsignx;
        public double natalSunsignx
        {
            get
            {
                return _natalSunsignx;
            }
            set
            {
                _natalSunsignx = value;
                OnPropertyChanged("natalSunsignx");
            }
        }

        private double _natalSunsigny;
        public double natalSunsigny
        {
            get
            {
                return _natalSunsigny;
            }
            set
            {
                _natalSunsigny = value;
                OnPropertyChanged("natalSunsigny");
            }
        }
        private string _natalSunsigntxt;
        public string natalSunsigntxt
        {
            get
            {
                return _natalSunsigntxt;
            }
            set
            {
                _natalSunsigntxt = value;
                OnPropertyChanged("natalSunsigntxt");
            }
        }
        private double _natalSunMinutex;
        public double natalSunMinutex
        {
            get
            {
                return _natalSunMinutex;
            }
            set
            {
                _natalSunMinutex = value;
                OnPropertyChanged("natalSunMinutex");
            }
        }

        private double _natalSunMinutey;
        public double natalSunMinutey
        {
            get
            {
                return _natalSunMinutey;
            }
            set
            {
                _natalSunMinutey = value;
                OnPropertyChanged("natalSunMinutey");
            }
        }
        private string _natalSunMinutetxt;
        public string natalSunMinutetxt
        {
            get
            {
                return _natalSunMinutetxt;
            }
            set
            {
                _natalSunMinutetxt = value;
                OnPropertyChanged("natalSunMinutetxt");
            }
        }
        private double _natalSunangle;
        public double natalSunangle
        {
            get
            {
                return _natalSunangle;
            }
            set
            {
                _natalSunangle = value;
                OnPropertyChanged("natalSunangle");
            }
        }

        #endregion

        // 月
        #region
        private double _natalMoonx;
        public double natalMoonx
        {
            get
            {
                return _natalMoonx;
            }
            set
            {
                _natalMoonx = value;
                OnPropertyChanged("natalMoonx");
            }
        }

        private double _natalMoony;
        public double natalMoony
        {
            get
            {
                return _natalMoony;
            }
            set
            {
                _natalMoony = value;
                OnPropertyChanged("natalMoony");
            }
        }
        private string _natalMoontxt;
        public string natalMoontxt
        {
            get
            {
                return _natalMoontxt;
            }
            set
            {
                _natalMoontxt = value;
                OnPropertyChanged("natalMoontxt");
            }
        }
        private double _natalMoondegreex;
        public double natalMoondegreex
        {
            get
            {
                return _natalMoondegreex;
            }
            set
            {
                _natalMoondegreex = value;
                OnPropertyChanged("natalMoondegreex");
            }
        }

        private double _natalMoondegreey;
        public double natalMoondegreey
        {
            get
            {
                return _natalMoondegreey;
            }
            set
            {
                _natalMoondegreey = value;
                OnPropertyChanged("natalMoondegreey");
            }
        }
        private string _natalMoondegreetxt;
        public string natalMoondegreetxt
        {
            get
            {
                return _natalMoondegreetxt;
            }
            set
            {
                _natalMoondegreetxt = value;
                OnPropertyChanged("natalMoondegreetxt");
            }
        }
        private double _natalMoonsignx;
        public double natalMoonsignx
        {
            get
            {
                return _natalMoonsignx;
            }
            set
            {
                _natalMoonsignx = value;
                OnPropertyChanged("natalMoonsignx");
            }
        }

        private double _natalMoonsigny;
        public double natalMoonsigny
        {
            get
            {
                return _natalMoonsigny;
            }
            set
            {
                _natalMoonsigny = value;
                OnPropertyChanged("natalMoonsigny");
            }
        }
        private string _natalMoonsigntxt;
        public string natalMoonsigntxt
        {
            get
            {
                return _natalMoonsigntxt;
            }
            set
            {
                _natalMoonsigntxt = value;
                OnPropertyChanged("natalMoonsigntxt");
            }
        }
        private double _natalMoonMinutex;
        public double natalMoonMinutex
        {
            get
            {
                return _natalMoonMinutex;
            }
            set
            {
                _natalMoonMinutex = value;
                OnPropertyChanged("natalMoonMinutex");
            }
        }

        private double _natalMoonMinutey;
        public double natalMoonMinutey
        {
            get
            {
                return _natalMoonMinutey;
            }
            set
            {
                _natalMoonMinutey = value;
                OnPropertyChanged("natalMoonMinutey");
            }
        }
        private string _natalMoonMinutetxt;
        public string natalMoonMinutetxt
        {
            get
            {
                return _natalMoonMinutetxt;
            }
            set
            {
                _natalMoonMinutetxt = value;
                OnPropertyChanged("natalMoonMinutetxt");
            }
        }
        private double _natalMoonangle;
        public double natalMoonangle
        {
            get
            {
                return _natalMoonangle;
            }
            set
            {
                _natalMoonangle = value;
                OnPropertyChanged("natalMoonangle");
            }
        }

        #endregion

        // 水星
        #region
        private double _natalMercuryx;
        public double natalMercuryx
        {
            get
            {
                return _natalMercuryx;
            }
            set
            {
                _natalMercuryx = value;
                OnPropertyChanged("natalMercuryx");
            }
        }

        private double _natalMercuryy;
        public double natalMercuryy
        {
            get
            {
                return _natalMercuryy;
            }
            set
            {
                _natalMercuryy = value;
                OnPropertyChanged("natalMercuryy");
            }
        }
        private string _natalMercurytxt;
        public string natalMercurytxt
        {
            get
            {
                return _natalMercurytxt;
            }
            set
            {
                _natalMercurytxt = value;
                OnPropertyChanged("natalMercurytxt");
            }
        }
        private double _natalMercurydegreex;
        public double natalMercurydegreex
        {
            get
            {
                return _natalMercurydegreex;
            }
            set
            {
                _natalMercurydegreex = value;
                OnPropertyChanged("natalMercurydegreex");
            }
        }

        private double _natalMercurydegreey;
        public double natalMercurydegreey
        {
            get
            {
                return _natalMercurydegreey;
            }
            set
            {
                _natalMercurydegreey = value;
                OnPropertyChanged("natalMercurydegreey");
            }
        }
        private string _natalMercurydegreetxt;
        public string natalMercurydegreetxt
        {
            get
            {
                return _natalMercurydegreetxt;
            }
            set
            {
                _natalMercurydegreetxt = value;
                OnPropertyChanged("natalMercurydegreetxt");
            }
        }
        private double _natalMercurysignx;
        public double natalMercurysignx
        {
            get
            {
                return _natalMercurysignx;
            }
            set
            {
                _natalMercurysignx = value;
                OnPropertyChanged("natalMercurysignx");
            }
        }

        private double _natalMercurysigny;
        public double natalMercurysigny
        {
            get
            {
                return _natalMercurysigny;
            }
            set
            {
                _natalMercurysigny = value;
                OnPropertyChanged("natalMercurysigny");
            }
        }
        private string _natalMercurysigntxt;
        public string natalMercurysigntxt
        {
            get
            {
                return _natalMercurysigntxt;
            }
            set
            {
                _natalMercurysigntxt = value;
                OnPropertyChanged("natalMercurysigntxt");
            }
        }
        private double _natalMercuryMinutex;
        public double natalMercuryMinutex
        {
            get
            {
                return _natalMercuryMinutex;
            }
            set
            {
                _natalMercuryMinutex = value;
                OnPropertyChanged("natalMercuryMinutex");
            }
        }

        private double _natalMercuryMinutey;
        public double natalMercuryMinutey
        {
            get
            {
                return _natalMercuryMinutey;
            }
            set
            {
                _natalMercuryMinutey = value;
                OnPropertyChanged("natalMercuryMinutey");
            }
        }
        private string _natalMercuryMinutetxt;
        public string natalMercuryMinutetxt
        {
            get
            {
                return _natalMercuryMinutetxt;
            }
            set
            {
                _natalMercuryMinutetxt = value;
                OnPropertyChanged("natalMercuryMinutetxt");
            }
        }
        private double _natalMercuryangle;
        public double natalMercuryangle
        {
            get
            {
                return _natalMercuryangle;
            }
            set
            {
                _natalMercuryangle = value;
                OnPropertyChanged("natalMercuryangle");
            }
        }

        #endregion

        // 金星
        #region
        private double _natalVenusx;
        public double natalVenusx
        {
            get
            {
                return _natalVenusx;
            }
            set
            {
                _natalVenusx = value;
                OnPropertyChanged("natalVenusx");
            }
        }

        private double _natalVenusy;
        public double natalVenusy
        {
            get
            {
                return _natalVenusy;
            }
            set
            {
                _natalVenusy = value;
                OnPropertyChanged("natalVenusy");
            }
        }
        private string _natalVenustxt;
        public string natalVenustxt
        {
            get
            {
                return _natalVenustxt;
            }
            set
            {
                _natalVenustxt = value;
                OnPropertyChanged("natalVenustxt");
            }
        }
        private double _natalVenusdegreex;
        public double natalVenusdegreex
        {
            get
            {
                return _natalVenusdegreex;
            }
            set
            {
                _natalVenusdegreex = value;
                OnPropertyChanged("natalVenusdegreex");
            }
        }

        private double _natalVenusdegreey;
        public double natalVenusdegreey
        {
            get
            {
                return _natalVenusdegreey;
            }
            set
            {
                _natalVenusdegreey = value;
                OnPropertyChanged("natalVenusdegreey");
            }
        }
        private string _natalVenusdegreetxt;
        public string natalVenusdegreetxt
        {
            get
            {
                return _natalVenusdegreetxt;
            }
            set
            {
                _natalVenusdegreetxt = value;
                OnPropertyChanged("natalVenusdegreetxt");
            }
        }
        private double _natalVenussignx;
        public double natalVenussignx
        {
            get
            {
                return _natalVenussignx;
            }
            set
            {
                _natalVenussignx = value;
                OnPropertyChanged("natalVenussignx");
            }
        }

        private double _natalVenussigny;
        public double natalVenussigny
        {
            get
            {
                return _natalVenussigny;
            }
            set
            {
                _natalVenussigny = value;
                OnPropertyChanged("natalVenussigny");
            }
        }
        private string _natalVenussigntxt;
        public string natalVenussigntxt
        {
            get
            {
                return _natalVenussigntxt;
            }
            set
            {
                _natalVenussigntxt = value;
                OnPropertyChanged("natalVenussigntxt");
            }
        }
        private double _natalVenusMinutex;
        public double natalVenusMinutex
        {
            get
            {
                return _natalVenusMinutex;
            }
            set
            {
                _natalVenusMinutex = value;
                OnPropertyChanged("natalVenusMinutex");
            }
        }

        private double _natalVenusMinutey;
        public double natalVenusMinutey
        {
            get
            {
                return _natalVenusMinutey;
            }
            set
            {
                _natalVenusMinutey = value;
                OnPropertyChanged("natalVenusMinutey");
            }
        }
        private string _natalVenusMinutetxt;
        public string natalVenusMinutetxt
        {
            get
            {
                return _natalVenusMinutetxt;
            }
            set
            {
                _natalVenusMinutetxt = value;
                OnPropertyChanged("natalVenusMinutetxt");
            }
        }
        private double _natalVenusangle;
        public double natalVenusangle
        {
            get
            {
                return _natalVenusangle;
            }
            set
            {
                _natalVenusangle = value;
                OnPropertyChanged("natalVenusangle");
            }
        }

        #endregion

        // 火星
        #region
        private double _natalMarsx;
        public double natalMarsx
        {
            get
            {
                return _natalMarsx;
            }
            set
            {
                _natalMarsx = value;
                OnPropertyChanged("natalMarsx");
            }
        }

        private double _natalMarsy;
        public double natalMarsy
        {
            get
            {
                return _natalMarsy;
            }
            set
            {
                _natalMarsy = value;
                OnPropertyChanged("natalMarsy");
            }
        }
        private string _natalMarstxt;
        public string natalMarstxt
        {
            get
            {
                return _natalMarstxt;
            }
            set
            {
                _natalMarstxt = value;
                OnPropertyChanged("natalMarstxt");
            }
        }
        private double _natalMarsdegreex;
        public double natalMarsdegreex
        {
            get
            {
                return _natalMarsdegreex;
            }
            set
            {
                _natalMarsdegreex = value;
                OnPropertyChanged("natalMarsdegreex");
            }
        }

        private double _natalMarsdegreey;
        public double natalMarsdegreey
        {
            get
            {
                return _natalMarsdegreey;
            }
            set
            {
                _natalMarsdegreey = value;
                OnPropertyChanged("natalMarsdegreey");
            }
        }
        private string _natalMarsdegreetxt;
        public string natalMarsdegreetxt
        {
            get
            {
                return _natalMarsdegreetxt;
            }
            set
            {
                _natalMarsdegreetxt = value;
                OnPropertyChanged("natalMarsdegreetxt");
            }
        }
        private double _natalMarssignx;
        public double natalMarssignx
        {
            get
            {
                return _natalMarssignx;
            }
            set
            {
                _natalMarssignx = value;
                OnPropertyChanged("natalMarssignx");
            }
        }

        private double _natalMarssigny;
        public double natalMarssigny
        {
            get
            {
                return _natalMarssigny;
            }
            set
            {
                _natalMarssigny = value;
                OnPropertyChanged("natalMarssigny");
            }
        }
        private string _natalMarssigntxt;
        public string natalMarssigntxt
        {
            get
            {
                return _natalMarssigntxt;
            }
            set
            {
                _natalMarssigntxt = value;
                OnPropertyChanged("natalMarssigntxt");
            }
        }
        private double _natalMarsMinutex;
        public double natalMarsMinutex
        {
            get
            {
                return _natalMarsMinutex;
            }
            set
            {
                _natalMarsMinutex = value;
                OnPropertyChanged("natalMarsMinutex");
            }
        }

        private double _natalMarsMinutey;
        public double natalMarsMinutey
        {
            get
            {
                return _natalMarsMinutey;
            }
            set
            {
                _natalMarsMinutey = value;
                OnPropertyChanged("natalMarsMinutey");
            }
        }
        private string _natalMarsMinutetxt;
        public string natalMarsMinutetxt
        {
            get
            {
                return _natalMarsMinutetxt;
            }
            set
            {
                _natalMarsMinutetxt = value;
                OnPropertyChanged("natalMarsMinutetxt");
            }
        }
        private double _natalMarsangle;
        public double natalMarsangle
        {
            get
            {
                return _natalMarsangle;
            }
            set
            {
                _natalMarsangle = value;
                OnPropertyChanged("natalMarsangle");
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
