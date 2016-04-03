using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.ObjectModel;
using microcosm.Common;

namespace microcosm.ViewModel
{
    public class HouseListData
    {
        public string hName { get; set; }
        public string firstData { get; set; }
        public string secondData { get; set; }
        public string thirdData { get; set; }
        public string fourthData { get; set; }
        public string fifthData { get; set; }
        public string sixthData { get; set; }

        public HouseListData()
        {

        }
        public HouseListData(int i)
        {
            hName = CommonData.getSignSymbol(i);
            firstData = "aaa";
            secondData = "bbb";
            thirdData = "ccc";
            fourthData = "ddd";
            fifthData = "eee";
            sixthData = "eee";
        }
    }

    public class HouseListViewModel
    {
        public ObservableCollection<HouseListData> hList { get; set; }

        public HouseListViewModel()
        {
            hList = new ObservableCollection<HouseListData>();
            Enumerable.Range(0, 12).ToList().ForEach(i => {
                hList.Add(new HouseListData(i));
            });

        }
    }

}
