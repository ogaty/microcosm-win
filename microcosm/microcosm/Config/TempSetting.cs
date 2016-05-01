using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace microcosm.Config
{
    public class TempSetting
    {
        // 一時的な設定
        // 保存しない

        public int bands = 1;

        public TempSetting(ConfigData config)
        {
            if (config.defaultBands < 1 || config.defaultBands > 7)
            {
                bands = 1;
            } else
            {
                bands = config.defaultBands;
            }
        }
    }
}
