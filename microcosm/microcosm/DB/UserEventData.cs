using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace microcosm.DB
{
    // DatabaseWindowにバインドするリスト
    public class UserEventData
    {
        public string name { get; set; }
        public string birth_str { get; set; }
        public int birth_year { get; set; }
        public int birth_month { get; set; }
        public int birth_day { get; set; }
        public int birth_hour { get; set; }
        public int birth_minute { get; set; }
        public int birth_second { get; set; }
        public string birth_place { get; set; }
        public double lat { get; set; }
        public double lng { get; set; }
        public string lat_lng { get; set; }
        public string timezone { get; set; }
        public string memo { get; set; }
        public string fullpath { get; set; }
    }
}
