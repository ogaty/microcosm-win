using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace microcosm.DB
{
    public class UserBinding
    {
        public string birthStr;
        public UserBinding()
        {

        }
        public UserBinding(UserData data)
        {
            birthStr = String.Format("{0}/{1}/{2} {3}:{4}:{5} {6}", data.birth_year,
                data.birth_month, data.birth_day, data.birth_hour, data.birth_minute, data.birth_second, data.timezone);

        }
    }
}
