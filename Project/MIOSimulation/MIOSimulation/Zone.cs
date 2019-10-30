using GMap.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIOSimulation
{
    class Zone
    {
        private String name;
        private int number;
        private Polygon perimeter;
        private List<Station> stations;
        private List<Stop> stopsList;
        private List<String> stationsAdded;
        

        public Zone(String name, int number, String area)
        {
            this.name = name;
            this.number = number;
            this.perimeter = new Polygon(area, name);
            stopsList = new List<Stop>();
            stationsAdded = new List<string>();

        }

        public bool isInside(PointLatLng p) {

            return perimeter.isInside(p);

        }

        public Polygon getPerimeter() {
            return perimeter;
        }

        public String getName() {
            return name;
        }

    }
}
