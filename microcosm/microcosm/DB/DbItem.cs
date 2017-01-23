using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace microcosm.DB
{
    public class DbItem
    {
        public string fileName;
        public string fileNameNoExt;
        public bool isDir;

        public string userName;
        public string userFurigana;
        public DateTime userBirth;
        public string userHour;
        public string userMinute;
        public string userSecond;
        public string userPlace;
        public string userLat;
        public string userLng;
        public string userTimezone;
        public string memo;

        public bool ecsm = false;

        // コンストラクタは２パターンある
        public DbItem()
        {

        }

        public UserData getUserdata()
        {
            XMLDBManager DBMgr = new XMLDBManager(fileName);
            UserData udata = DBMgr.getObject();
            udata.filename = fileName;
            return udata;
        }

        /// <summary>
        /// ecsm(csv)をUserDataのリスト形式に変換
        /// </summary>
        /// <returns></returns>
        public List<UserData> getUserlist()
        {
            List<UserData> ulist = new List<UserData>();

            using (Stream fileStream = new FileStream(this.fileName, FileMode.Open))
            {
                StreamReader sr = new StreamReader(fileStream, Encoding.GetEncoding("utf-8"), true);
                int error = 0;
                while (sr.Peek() >= 0)
                {
                    string line = sr.ReadLine();
                    if (line.IndexOf(",") > 0)
                    {
                        try
                        {
                            string[] data = line.Split(',');
                            // data[0] name
                            // data[1] furigana
                            // data[2] y
                            // data[3] m
                            // data[4] d
                            // data[5] h
                            // data[6] i
                            // data[7] s
                            // data[8] lat
                            // data[9] lng
                            // data[10] place
                            // data[11] memo
                            // data[12] timezone
                            UserData udata = new UserData();
                            udata.name = data[0];
                            udata.furigana = data[1];
                            udata.birth_year = int.Parse(data[2]);
                            udata.birth_month = int.Parse(data[3]);
                            udata.birth_day = int.Parse(data[4]);
                            udata.birth_hour = int.Parse(data[5]);
                            udata.birth_minute = int.Parse(data[6]);
                            udata.birth_second = int.Parse(data[7]);
                            udata.lat = double.Parse(data[8]);
                            udata.lng = double.Parse(data[9]);
                            udata.birth_place = data[10];
                            udata.memo = data[11];
                            udata.timezone = data[12];

                            ulist.Add(udata);
                        }
                        catch
                        {
                            error++;
                        }
                    }
                }
            }
            return ulist;
        }
    }
}
