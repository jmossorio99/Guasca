using GMap.NET;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIOSimulation
{
    class BusSimulationControl
    {
        private List<List<BusLocation>> timeLine;
        private List<Bus> busesInSimulation;
        private long start;
        private long finish;
        private long offset;
        private int movingTo;
        private Dictionary<String, Bus> busReference;
        private Dictionary<String, int> busMatch;
        Dictionary<String, Int32> memo;

        public BusSimulationControl(long start, long finish)
        {
            this.start = start;
            this.finish = finish;
            movingTo = -1;
            busReference = new Dictionary<string, Bus>();
            busMatch = new Dictionary<string, int>();
            busesInSimulation = new List<Bus>();
            timeLine = new List<List<BusLocation>>();
            memo = new Dictionary<string, int>();
            InitializeSimulation();
            LoadArcs();
        }

        private void LoadArcs()
        {
            FileReader br = new FileReader("arcs.txt");
            List<String> data = br.readFile();

            foreach (string line in data) {
                string[] splitData = line.Split('	');
                String query = splitData[1] + " " + splitData[2];
                if (!memo.ContainsKey(query)) {
                    if (!splitData[3].Equals(""))
                    {
                        memo.Add(query, Int32.Parse(splitData[3]));
                    }
                    else {
                        memo.Add(query, 0);
                    }
                    
                }
                
            }

        }

        // Date;IdBus;IdStop;Odometer;TaskId;LineId;TripId;Lng;Lat
        // 2019-06-20 18:00:17
        private void InitializeSimulation()
        {
            FileReader br = new FileReader("dataSimulation1.txt");
            List<String> data = br.readFile();
            String lastDate = "";
            int position = -1;
            foreach (String line in data)
            {
                string[] splitData = line.Split(',');
                String date = splitData[0];
                String busId = splitData[1];
                String stopId = splitData[2];
                String Lng = splitData[4];
                String Lat = splitData[5];
                PointLatLng location = new PointLatLng(Double.Parse(Lat, CultureInfo.InvariantCulture.NumberFormat)/10000000, Double.Parse(Lng, CultureInfo.InvariantCulture.NumberFormat)/ 10000000);
                BusLocation temp = new BusLocation(location, busId,  Int32.Parse(stopId));
                if (!busReference.ContainsKey(busId))
                {
                    busReference.Add(busId, new Bus(busId, ""));
                }

                if (date.CompareTo(lastDate) == 0)
                {
                    timeLine[position].Add(temp);
                }
                else
                {
                    if (lastDate.Equals(""))
                    {
                        lastDate = date;
                        offset = createNumber(lastDate);
                        position++;
                        timeLine.Add(new List<BusLocation>());
                    }
                    else {
                        long start = createNumber(lastDate);
                        long finish = createNumber(date);
                        for (long i = start + 1; i <= finish; i++)
                        {
                            position++;
                            timeLine.Add(new List<BusLocation>());
                        }
                    }
                    
                    timeLine[position].Add(temp);
                    lastDate = date;
                }
            }

        }

        public long createNumber(string date)
        {
            String[] fecha = date.Split(' ')[1].Split(':');
            String[] data = date.Split(' ')[1].Split(':');

            long result = long.Parse(data[0]) * 3600 + long.Parse(data[1]) * 60 + long.Parse(data[2]);
            return result;
        }

        public void setInterval(String s, String f)
        {
            start = createNumber(s) - offset;
            finish = createNumber(f) - offset;
            movingTo = (int)start;
        }

        public List<Bus> Next30()
        {
            busesInSimulation = new List<Bus>();
            busMatch = new Dictionary<string, int>();
            int steps = 0;
            while (movingTo <= finish && steps <= 30)
            {
                foreach (BusLocation location in timeLine[movingTo])
                {
                    if (busReference.ContainsKey(location.BusName))
                    {
                        if (!busMatch.ContainsKey(location.BusName))
                        {
                            Bus actualBus = busReference[location.BusName];
                            actualBus.ActualPosition = location.Postion;
                            actualBus.PreviousStop = location.StopId;
                            actualBus.TimeElapse = 0;
                            actualBus.TimeElapse -= movingTo;
                            busMatch.Add(location.BusName, -1);
                        }
                    }
                }
                movingTo++;
                steps++;
            }
            checkNext30();
            checkValid();
            return busesInSimulation;
        }


        private void checkNext30()
        {
            int steps = 0;
            while (movingTo <= finish && steps <= 30)
            {
                foreach (BusLocation location in timeLine[movingTo])
                {
                    if (busReference.ContainsKey(location.BusName))
                    {
                        Bus actualBus = busReference[location.BusName];
                        actualBus.NextPosition = location.Postion;
                        actualBus.ActualStop = location.StopId;
                        if (busMatch.ContainsKey(location.BusName)) {
                            int solution = movingTo + (int)actualBus.TimeElapse;
                            busMatch.Remove(location.BusName);
                            busMatch.Add(location.BusName, solution);
                        }
                    }
                }
                movingTo++;
                steps++;
            }
            movingTo -= steps;
        }

        private void checkValid()
        {
            foreach (var item in busMatch)
            {
                Bus actualBus = busReference[item.Key];

                if (item.Value >= 0 && actualBus.ActualStop != -1 && actualBus.PreviousStop != -1)
                {
                    
                    if (actualBus.PreviousStop != actualBus.ActualStop)
                    {
                        actualBus.TimeLocation = 0;
                        actualBus.TimeLocation -= item.Value;
                        if (memo.ContainsKey(actualBus.PreviousStop + " " + actualBus.ActualStop)) {
                            actualBus.TimeLocation += memo[actualBus.PreviousStop + " " + actualBus.ActualStop];
                        }
                        else {
                            actualBus.TimeLocation = -1;
                        }
                        
                        actualBus.TimeElapse = 0;
                        
                    }
                    busesInSimulation.Add(actualBus);
                }
            }

        }
    }
}