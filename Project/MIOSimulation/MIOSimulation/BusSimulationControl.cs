using System;
using System.Collections.Generic;
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
        private Dictionary<String, int> dateReference;

        public BusSimulationControl(long start, long finish) {
            this.start = start;
            this.finish = finish;
            dateReference = new Dictionary<string, int>();
            busesInSimulation = new List<Bus>();
            timeLine = new List<List<BusLocation>>();
            InitializeSimulation();
        }

        // Date;IdBus;IdStop;Odometer;TaskId;LineId;TripId;Lng;Lat
        // 2019-06-20 18:00:17
        private void InitializeSimulation() {
            FileReader br = new FileReader("busTravel");
            List<String> data = br.readFile();
            String lastDate = "";
            int position = 0;
            foreach (String line in data) {
                string[] splitData = line.Split(';');
                String date = splitData[0];
                String Lng = splitData[7];
                String Lat = splitData[8];
                if (date.CompareTo(lastDate) == 0)
                {
                    
                }
                else {
                    position++;
                    timeLine.Add(new List<BusLocation>());
                    timeLine[position].Add(new BusLocation());
                }
            }
            
        }

        public void setInterval(String start, String finish) {

        }

        public List<Bus> Next30() {

            return null;
        }
    }
}
