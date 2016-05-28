using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace microcosm.DB
{
    // メインウィンドウ用
    // UserDataとUserEventを両方管理
    public class User
    {
        public UserData udata { get; set; }
        public UserEvent uevent { get; set; }
        public string filename { get; set; }
        public int index;

        public User(UserData d, UserEvent e, string filename, int index)
        {
            udata = d;
            uevent = e;
            this.filename = filename;
            this.index = index;
        }
    }
}
