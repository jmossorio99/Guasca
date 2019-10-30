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

        public Station(String name)
        {

            this.name = name;
            stationStops = new List<Stop>();

        }

        public void addStopToStation(Stop newStop)
        {
            stationStops.Add(newStop);
        }

        public String Name { get; set; }

        public List<Stop> getStationStops()
        {
            return stationStops;
        }
    }
}
