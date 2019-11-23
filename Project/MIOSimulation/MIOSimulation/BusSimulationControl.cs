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
        private string referenceYear;
        private string referenceMonth;
        private Dictionary<String, Bus> busReference;
        private Dictionary<String, int> busMatch;
        Dictionary<String, Int32> memo;
        public int interval = 60;

        public int Interval { get => interval; set => interval = value; }

        public BusSimulationControl(long start, long finish)
        {
            this.start = start;
            this.finish = finish;
            movingTo = -1;
            referenceMonth = "";
            referenceYear = "";
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

        // Date;IdBus;IdStop;Odometer;Lng;Lat;TaskId;LineId;TripId
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
                PointLatLng location = new PointLatLng(Double.Parse(Lat, CultureInfo.InvariantCulture.NumberFormat) / 10000000, Double.Parse(Lng, CultureInfo.InvariantCulture.NumberFormat) / 10000000);
                BusLocation temp;
                switch (Convert.ToInt32(stopId))
                {
                    case -1:
                        temp = new BusLocation(location, busId, Int32.Parse(stopId), true);
                        break;
                    default:
                        temp = new BusLocation(location, busId, Int32.Parse(stopId), false);
                        break;
                }
                if (!busReference.ContainsKey(busId))
                {
                    busReference.Add(busId, new Bus(busId, splitData[7]));
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
                        String []values = date.Split('-');
                        referenceMonth = values[1];
                        referenceYear = values[0];
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

        // 2019-06-20 18:00:17

        public long createNumber(string date)
        {
            String[] fecha = date.Split(' ')[0].Split('-');
            String[] data = date.Split(' ')[1].Split(':');

            long result = (long.Parse(fecha[2]) * 86400) + (long.Parse(data[0]) * 3600) + (long.Parse(data[1]) * 60) + (long.Parse(data[2]));
            return result;
        }

        public string creatDate(long number) {
            number += offset;
            long day = (number % 2073600)/ 86400;
            long hour = (number % 86400)/3600;
            long minutes = (number % 3600)/60;
            long seconds = number % 60;
            String date = day + " " + hour+ " "+ minutes+ " "+ seconds;
            return date;
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
            while (movingTo <= finish && steps <= interval)
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
                            if (location.IsIddle)
                            {
                                busReference[location.BusName].IsIddle = true;
                            }
                            else
                            {
                                busReference[location.BusName].IsIddle = false;
                            }
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
            int reference = interval;
            if (interval < 30) {
                reference = 61 - interval;
            }
            while (movingTo <= finish && steps <= reference)
            {
                foreach (BusLocation location in timeLine[movingTo])
                {
                    if (busReference.ContainsKey(location.BusName))
                    {
                        Bus actualBus = busReference[location.BusName];
                        if (actualBus.IsIddle) {
                            actualBus.IsIddle = true;
                        }
                        actualBus.ActualStop = location.StopId;
                        actualBus.NextPosition = location.Postion;
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

                if (item.Value >= 0)
                {

                    if (actualBus.PreviousStop != actualBus.ActualStop || actualBus.ActualStop == -1 || actualBus.PreviousStop == -1)
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