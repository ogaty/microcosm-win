using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using microcosm.Planet;
using microcosm.Common;

namespace microcosm.ViewModel
{
    public class PlanetListData
    {
        public string pName { get; set; }
        public string firstData { get; set; }
        public string secondData { get; set; }
        public string thirdData { get; set; }
        public string fourthData { get; set; }
        public string fifthData { get; set; }
        public string sixthData { get; set; }

        public PlanetListData()
        {

        }
        public PlanetListData(
            int i, 
            PlanetData data1, 
            PlanetData data2,
            PlanetData data3,
            PlanetData data4,
            PlanetData data5,
            PlanetData data6
            )
        {
            pName = CommonData.getPlanetSymbol(i);
            firstData = getTxt(data1.absolute_position);
            secondData = getTxt(data2.absolute_position);
            thirdData = getTxt(data3.absolute_position);
            fourthData = getTxt(data4.absolute_position);
            fifthData = getTxt(data5.absolute_position);
            sixthData = getTxt(data6.absolute_position);
        }

        private string getTxt(double absolute_position)
        {
            string dataTxt = CommonData.getSignText(absolute_position);
            dataTxt += string.Format("{0,00:F3}", CommonData.getDeg(absolute_position));
            return dataTxt;
        }
    }
    public class PlanetListViewModel
    {
        public ObservableCollection<PlanetListData> pList { get; set; }

        public PlanetListViewModel(
            List<PlanetData> list1,
            List<PlanetData> list2,
            List<PlanetData> list3,
            List<PlanetData> list4,
            List<PlanetData> list5,
            List<PlanetData> list6
            )
        {
            pList = new ObservableCollection<PlanetListData>();
            Enumerable.Range(0, 10).ToList().ForEach(i => {
                pList.Add(new PlanetListData(i, 
                    list1[i], 
                    list2[i],
                    list3[i],
                    list4[i],
                    list5[i],
                    list6[i]
                    ));
            });
            
        }
    }
}
