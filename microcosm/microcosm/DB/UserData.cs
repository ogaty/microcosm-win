using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

using microcosm.Config;

namespace microcosm.DB
{
    /// <summary>
    /// csmファイル格納クラス
    /// </summary>
    [XmlRoot("userdata")]
    public class UserData
    {
        [XmlElement("name")]
        public string name { get; set; }
        [XmlElement("furigana")]
        public string furigana { get; set; }
        [XmlElement("birth_year")]
        public int birth_year { get; set; }
        [XmlElement("birth_month")]
        public int birth_month { get; set; }
        [XmlElement("birth_day")]
        public int birth_day { get; set; }
        [XmlElement("birth_hour")]
        public int birth_hour { get; set; }
        [XmlElement("birth_minute")]
        public int birth_minute { get; set; }
        [XmlElement("birth_second")]
        public int birth_second { get; set; }
        [XmlElement("lat")]
        public double lat { get; set; }
        [XmlElement("lng")]
        public double lng { get; set; }
        [XmlElement("birth_place")]
        public string birth_place { get; set; }
        [XmlElement("memo")]
        public string memo { get; set; }
        [XmlElement("timezone")]
        public string timezone { get; set; }
        [XmlArray("eventlist")]
        [XmlArrayItem("event")]
        public List<UserEvent> userevent { get; set; }

        public string filename;

        public string birth_str
        {
            get
            {
                return birth_year.ToString("0000") + "/" + birth_month.ToString("00") + "/" + birth_day.ToString("00") + " " +
                    birth_hour.ToString("00") + ":" + birth_minute.ToString("00") + ":" + birth_second.ToString("00");
            }
        }
        public string birth_str_ymd
        {
            get
            {
                return birth_year.ToString("0000") + "/" + birth_month.ToString("00") + "/" + birth_day.ToString("00") + " ";
            }
        }
        public string birth_str_his
        {
            get
            {
                return birth_hour.ToString("00") + ":" + birth_minute.ToString("00") + ":" + birth_second.ToString("00");
            }
        }


        public string lat_lng
        {
            get
            {
                return lat.ToString("00.000") + "/" + lng.ToString("000.000");
            }
        }

        public UserData()
        {
            this.name = "現在時刻";
            this.furigana = "";
            this.birth_year = DateTime.Now.Year;
            this.birth_month = DateTime.Now.Month;
            this.birth_day = DateTime.Now.Day;
            this.birth_hour = DateTime.Now.Hour;
            this.birth_minute = DateTime.Now.Minute;
            this.birth_second = DateTime.Now.Second;
            this.birth_place = "東京都千代田区";
            this.lat = 35.685175;
            this.lng = 139.7528;
            this.memo = "";
            this.timezone = "JST";
        }

        public UserData(ConfigData config)
        {
            this.name = "現在時刻";
            this.furigana = "";
            this.birth_year = DateTime.Now.Year;
            this.birth_month = DateTime.Now.Month;
            this.birth_day = DateTime.Now.Day;
            this.birth_hour = DateTime.Now.Hour;
            this.birth_minute = DateTime.Now.Minute;
            this.birth_second = DateTime.Now.Second;
            this.birth_place = config.defaultPlace;
            this.lat = config.lat;
            this.lng = config.lng;
            this.memo = "";
            this.timezone = "JST";
        }

        public UserData(
            string name,
            string furigana,
            int birth_year,
            int birth_month,
            int birth_day,
            int birth_hour,
            int birth_minute,
            int birth_second,
            double lat,
            double lng,
            string birth_place,
            string memo,
            string timezone
            )
        {
            this.name = name;
            this.furigana = furigana;
            this.birth_year = birth_year;
            this.birth_month = birth_month;
            this.birth_day = birth_day;
            this.birth_hour = birth_hour;
            this.birth_minute = birth_minute;
            this.birth_second = birth_second;
            this.lat = lat;
            this.lng = lng;
            this.birth_place = birth_place;
            this.memo = memo;
            if (timezone == "JST(日本標準")
            {
                this.timezone = "JST";
            }
            else
            {
                this.timezone = timezone;
            }
        }

        public void setData(
            string name,
            string furigana,
            int birth_year,
            int birth_month,
            int birth_day,
            int birth_hour,
            int birth_minute,
            int birth_second,
            double lat,
            double lng,
            string birth_place,
            string memo,
            string timezone
            )
        {
            this.name = name;
            this.furigana = furigana;
            this.birth_year = birth_year;
            this.birth_month = birth_month;
            this.birth_day = birth_day;
            this.birth_hour = birth_hour;
            this.birth_minute = birth_minute;
            this.birth_second = birth_second;
            this.lat = lat;
            this.lng = lng;
            this.birth_place = birth_place;
            this.memo = memo;
            if (timezone == "JST(日本標準")
            {
                this.timezone = "JST";
            }
            else
            {
                this.timezone = timezone;
            }
        }

        public static explicit operator UserEvent(UserData val)
        {
            return new UserEvent(val.name, val.birth_year, val.birth_month, val.birth_day,
                val.birth_hour, val.birth_minute, val.birth_second, val.birth_place,
                val.lat, val.lng, val.timezone, val.memo);
        }

        public static explicit operator UserEventData(UserData val)
        {
            return new UserEventData(val.name, val.birth_year, val.birth_month, val.birth_day,
                val.birth_hour, val.birth_minute, val.birth_second, 
                val.lat, val.lng, val.birth_place, val.timezone, val.memo);
        }

        public override string ToString()
        {
            return this.name;
        }
    }
}
