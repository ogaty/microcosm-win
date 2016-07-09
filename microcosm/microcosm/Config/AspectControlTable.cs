using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using microcosm.Aspect;

namespace microcosm.Config
{
    public class AspectControlTable
    {
        public FrameworkElement selfElement;
        public FrameworkElement anotherElement;
        public FrameworkElement aspectSelfElement;
        public FrameworkElement aspectAnotherElement;
        public bool[,] tempArray;
        public bool targetBoolean;
        public int commonDataNo;
        public AspectKind aspectKindNo;

        // 0～15
        // 0: 1-1
        // 1: 2-2
        public int subIndex;
    }
}
