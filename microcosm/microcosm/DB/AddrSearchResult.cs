using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace microcosm.DB
{
    public class AddrSearchResult
    {
        public string resultPlace { get; set; }
        public double resultLat { get; set; }
        public double resultLng { get; set; }

        public AddrSearchResult(
            string resultPlace,
            double resultLat,
            double resultLng)
        {
            this.resultPlace = resultPlace;
            this.resultLat = resultLat;
            this.resultLng = resultLng;
        }
    }
}
