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
        private Dictionary<String, Station> stationsList;
        private List<String> stationNames;
        private List<Stop> stopsList;


        public Zone(String name, int number, String area)
        {
            this.name = name;
            this.number = number;
            this.perimeter = new Polygon(area, name);
            stopsList = new List<Stop>();
            stationsList = new Dictionary<string, Station>();
            stationNames = new List<String>();

        }

        public void addStop(Stop stop) {

            stopsList.Add(stop);

        }

        public void addStation(String key, Station value) {

            stationsList.Add(key, value);
            stationNames.Add(key);

        }

        public void addStopToStation(String key, Stop value) {

            stationsList[key].addStopToStation(value);

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

        public int getNumber() {
            return number;
        }

        public Station getStation(String key) {
            return stationsList[key];
        }

        public List<String> getStationsNames() {
            return stationNames;
        }

        public Dictionary<String, Station> getStations()
        {
            return stationsList;
        }

    }
}
