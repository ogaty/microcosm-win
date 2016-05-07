using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace microcosm.Planet
{
    public class PlanetDisplay
    {
        public int planetNo { get; set; }

        public string planetTxt { get; set; }
        public PointF planetPt { get; set; }

        public string degreeTxt { get; set; }
        public PointF degreePt { get; set; }
    }
}
