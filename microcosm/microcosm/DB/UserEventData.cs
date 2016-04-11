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
        public string birth_place { get; set; }
        public string lat_lng { get; set; }
        public string memo { get; set; }
        public string fullpath { get; set; }
    }
}
