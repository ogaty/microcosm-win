using microcosm.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace microcosm.ViewModel
{
    /// <summary>
    /// UserEventにくっつくタグ
    /// csmの場合はユーザーデータが入る
    /// ecsmの場合はユーザーデータを入れない
    /// </summary>
    public class UserEventTag
    {
        public bool ecsm = false;
        public UserData udata;
    }
}
