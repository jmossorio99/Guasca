using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIOSimulation
{   

    class Station
    {
        private String name;
        private Polygon perimeter;
        private List<Stop> stationStops;
        private Zone zone;

        public Station(String name, Zone zone)
        {

            this.name = name;
            this.zone = zone;
            stationStops = new List<Stop>();

        }

        public void addStopToStation(Stop newStop)
        {
            stationStops.Add(newStop);
        }

        public String getName() {
            return name;
        }

        public Zone getZone() {
            return zone;
        }

        public void setPerimeter(Polygon perimeter) {
            this.perimeter = perimeter;
        }

        public List<Stop> getStationStops()
        {
            return stationStops;
        }

        public Polygon getPerimeter()
        {
            return perimeter;
        }
    }
}
