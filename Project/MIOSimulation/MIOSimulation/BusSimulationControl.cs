using GMap.NET;
using System;
using System.Collections.Generic;
using System.Globalization;
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

        public BusSimulationControl(long start, long finish)
        {

            this.start = start;
            this.finish = finish;
            movingTo = -1;
            busReference = new Dictionary<string, Bus>();
            busMatch = new Dictionary<string, int>();
            busesInSimulation = new List<Bus>();
            timeLine = new List<List<BusLocation>>();
            InitializeSimulation();
        }

        // Date;IdBus;IdStop;Odometer;TaskId;LineId;TripId;Lng;Lat
        // 2019-06-20 18:00:17
        private void InitializeSimulation()
        {
            FileReader br = new FileReader("busSimulationl.txt");
            List<String> data = br.readFile();
            String lastDate = "";
            int position = -1;
            foreach (String line in data)
            {
                string[] splitData = line.Split(';');
                String date = splitData[0];
                String busId = splitData[1];
                String Lng = splitData[7];
                String Lat = splitData[8];
                PointLatLng location = new PointLatLng(Double.Parse(Lat, CultureInfo.InvariantCulture.NumberFormat), Double.Parse(Lng, CultureInfo.InvariantCulture.NumberFormat));
                BusLocation temp = new BusLocation(location, busId);
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
                    if (lastDate.Equals("")) { lastDate = date; offset = createNumber(lastDate); }
                    long start = createNumber(lastDate);
                    long finish = createNumber(date);
                    for (long i = start + 1; i <= finish; i++)
                    {
                        position++;
                        timeLine.Add(new List<BusLocation>());
                    }
                    timeLine[position].Add(temp);
                    lastDate = date;
                }
            }

        }

        private long createNumber(string date)
        {
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
            int steps = 0;
            while (movingTo <= finish && steps <= 30)
            {
                foreach (BusLocation location in timeLine[movingTo])
                {
                    if (busReference.ContainsKey(location.BusName))
                    {
                        busReference[location.BusName].ActualPostion = location.Postion;
                        busMatch.Add(location.BusName, 1);
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
                        busReference[location.BusName].NextPostion = location.Postion;
                        if (busMatch.ContainsKey(location.BusName)) { busMatch.Add(location.BusName, 2); }
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
                if (item.Value == 2)
                {
                    busesInSimulation.Add(busReference[item.Key]);
                }
            }

        }
    }
}