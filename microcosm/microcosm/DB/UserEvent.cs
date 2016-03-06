using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace microcosm.DB
{
    public class UserEvent
    {
        [XmlElement("event_name")]
        public string event_name;
        [XmlElement("event_year")]
        public int event_year;
        [XmlElement("event_month")]
        public int event_month;
        [XmlElement("event_day")]
        public int event_day;
        [XmlElement("event_hour")]
        public int event_hour;
        [XmlElement("event_minute")]
        public int event_minute;
        [XmlElement("event_second")]
        public int event_second;
        [XmlElement("event_place")]
        public string event_place;
        [XmlElement("event_lat")]
        public double event_lat;
        [XmlElement("event_lng")]
        public double event_lng;
        [XmlElement("event_timezone")]
        public string event_timezone;
        [XmlElement("event_memo")]
        public string event_memo;

        public UserEvent()
        {

        }

        public UserEvent(string event_name, int event_year, int event_month, int event_day,
            int event_hour, int event_minute, int event_second, string event_place,
            double event_lat, double event_lng, string event_timezone, string event_memo)
        {
            this.event_name = event_name;
            this.event_year = event_year;
            this.event_month = event_month;
            this.event_day = event_day;
            this.event_hour = event_hour;
            this.event_minute = event_minute;
            this.event_second = event_second;
            this.event_place = event_place;
            this.event_lat = event_lat;
            this.event_lng = event_lng;
            this.event_timezone = event_timezone;
            this.event_memo = event_memo;
        }

        public static explicit operator UserData(UserEvent val)
        {
            return new UserData(val.event_name, "", val.event_year, val.event_month, val.event_day,
                val.event_hour, val.event_minute, val.event_second,
                val.event_lat, val.event_lng, val.event_place, val.event_memo, val.event_timezone);
        }
    }
}
