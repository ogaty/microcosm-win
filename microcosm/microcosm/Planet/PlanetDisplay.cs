using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace microcosm.Planet
{
    delegate void RingDisplay(
            string planetTxt, PointF planet,
            string degTxt, PointF degree,
            string signTxt, PointF sign,
            string minuteTxt, PointF minute,
            string retrogradeTxt, PointF retrograde
        );

    public class PlanetDisplay
    {
        public int planetNo { get; set; }
        public bool isDisp { get; set; }

        public string planetTxt { get; set; }
        public PointF planetPt { get; set; }

        public string degreeTxt { get; set; }
        public PointF degreePt { get; set; }

        public string symbolTxt { get; set; }
        public PointF symbolPt { get; set; }

        public string minuteTxt { get; set; }
        public PointF minutePt { get; set; }

        public string retrogradeTxt { get; set; }
        public PointF retrogradePt { get; set; }
    }
}
