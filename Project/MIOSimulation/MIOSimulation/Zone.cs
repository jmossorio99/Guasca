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
        private Polygon area;
        private Dictionary<String,Station> stationsDictionary;
        private List<Stop> stopsList;
        private List<String> stationsAdded;
        

        public Zone(String name,int number,Polygon area)
        {
            this.name = name;
            this.number = number;
            this.area = area;
            stopsList = new List<Stop>();
            stationsAdded = new List<string>();

        }

        public void addAnStationPointToThisZone(String stationName,Stop stationStop)
        {

            if (area.isInside(stationStop.Position))
            {

                Station stationTemp = stationsDictionary[stationName];

                if (stationTemp != null)
                {
                    stationTemp.addStopToStation(stationStop);
                    stationsAdded.Add(stationName);
                }
                else
                {
                    stationTemp = new Station(stationName);
                    stationTemp.addStopToStation(stationStop);
                    stationsAdded.Add(stationName);
                }

            }
            else
            {
                // no yet implemented
            }          
        }

        private void addStopToThisZone(Stop newStop)
        {

            if (area.isInside(newStop.Position))
            {
                stopsList.Add(newStop);
            }
            else
            {
                // no  yet implemented
            }

        }        
        public List<Stop> getAllStops()
        {
        return stopsList;
        }

        public List<Station> getStations()
        {
            List<Station> zoneStations = new List<Station>();

            for (int i = 0; i < stationsAdded.Count; i++)
            {

                String stationNameTemp = stationsAdded[i];

               zoneStations.Add(stationsDictionary[stationNameTemp]);

            }

            return zoneStations;
        }

        
    }
}
