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

        protected string[] houses = { "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX", "X", "XI", "XII" };

        public HouseListData()
        {

        }
        public HouseListData(int i,
            double data1,
            double data2,
            double data3,
            double data4,
            double data5,
            double data6
            )
        {
            hName = houses[i];
            firstData = getTxt(data1);
            secondData = getTxt(data2);
            thirdData = getTxt(data3);
            fourthData = getTxt(data4);
            fifthData = getTxt(data5);
            sixthData = getTxt(data6);
        }

        private string getTxt(double absolute_position)
        {
            string dataTxt = CommonData.getSignText(absolute_position);
            dataTxt += string.Format("{0,00:F3}", CommonData.getDeg(absolute_position));
            return dataTxt;
        }
    }

    public class HouseListViewModel
    {
        public ObservableCollection<HouseListData> hList { get; set; }

        public HouseListViewModel(
            double[] list1,
            double[] list2,
            double[] list3,
            double[] list4,
            double[] list5,
            double[] list6
            )
        {
            hList = new ObservableCollection<HouseListData>();
            Enumerable.Range(0, 12).ToList().ForEach(i => {
                hList.Add(new HouseListData(i,
                    list1[i + 1],
                    list2[i + 1],
                    list3[i + 1],
                    list4[i + 1],
                    list5[i + 1],
                    list6[i + 1]
                    ));
            });

        }
    }

}
