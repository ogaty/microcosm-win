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
        public PlanetListData(int i)
        {
            pName = CommonData.getPlanetSymbol(i);
            firstData = "aaa";
            secondData = "bbb";
            thirdData = "ccc";
            fourthData = "ddd";
            fifthData = "eee";
            sixthData = "eee";
        }
    }
    public class PlanetListViewModel
    {
        public ObservableCollection<PlanetListData> pList { get; set; }

        public PlanetListViewModel()
        {
            pList = new ObservableCollection<PlanetListData>();
            Enumerable.Range(0, 10).ToList().ForEach(i => {
                pList.Add(new PlanetListData(i));
            });
            
        }
    }
}
