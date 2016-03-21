using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace microcosm.Aspect
{
    public enum AspectKind
    {
        CONJUNCTION = 1,
        OPPOSITION = 2,
        INCONJUNCT = 3,
        SESQUIQUADRATE = 4,
        TRINE = 5,
        SQUARE = 6,
        SEXTILE = 7
    };
    public class AspectInfo
    {
        public double targetPosition; // 絶対位置
        public AspectKind aspectKind; // アスペクト種別
        public int softHard; // ソフトorハード
        public int targetPlanetNo; // ターゲット番号
    }
}
