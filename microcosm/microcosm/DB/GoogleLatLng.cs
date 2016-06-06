using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace microcosm.DB
{
    public class AddressComponent
    {
        public string long_name { get; set; }
        public string short_name { get; set; }
        public List<string> types { get; set; }
    }

    public class Location
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class Southwest
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class Northeast
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class Viewport
    {
        public Southwest southwest { get; set; }
        public Northeast northeast { get; set; }
    }

    public class Southwest2
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class Northeast2
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class Bounds
    {
        public Southwest2 southwest { get; set; }
        public Northeast2 northeast { get; set; }
    }

    public class Geometry
    {
        public Location location { get; set; }
        public string location_type { get; set; }
        public Viewport viewport { get; set; }
        public Bounds bounds { get; set; }
    }

    public class Result
    {
        public List<string> types { get; set; }
        public string formatted_address { get; set; }
        public List<AddressComponent> address_components { get; set; }
        public Geometry geometry { get; set; }
    }

    public class GoogleLatLng
    {
        public string status { get; set; }
        public List<Result> results { get; set; }
    }

}
