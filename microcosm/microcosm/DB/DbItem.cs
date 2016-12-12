using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace microcosm.DB
{
    public class DbItem
    {
        public string fileName;
        public string fileNameNoExt;
        public bool isDir;

        public string userName;
        public string userFurigana;
        public DateTime userBirth;
        public string userHour;
        public string userMinute;
        public string userSecond;
        public string userPlace;
        public string userLat;
        public string userLng;
        public string userTimezone;
        public string memo;
    }
}
