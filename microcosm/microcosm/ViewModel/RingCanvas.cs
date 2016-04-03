using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace microcosm.ViewModel
{
    public class RingCanvas
    {
        // 獣帯外側
        public double outerWidth { get; set; }
        public double outerHeight { get; set; }
        // 獣帯内側
        public double innerWidth { get; set; }
        public double innerHeight { get; set; }

        public RingCanvas()
        {
            outerWidth = 370;
            outerHeight = 370;
            innerWidth = 330;
            innerHeight = 330;
        }
    }
}
