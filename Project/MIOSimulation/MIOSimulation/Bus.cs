using GMap.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMap.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIOSimulation
{
    class Bus
    {
        String identifier;
        String name;
        private PointLatLng actualPostion;
        private PointLatLng nextPosition;
        private int previousLocation;
        private int actualLocation;
        private long timeLocation;

        public Bus(string identifier, string name)
        {
            this.identifier = identifier;
            this.Name = name;
            ActualPosition = new PointLatLng(0, 0);
            NextPosition = new PointLatLng(0, 0);

            previousLocation = -1;
            actualLocation = -1;
            timeLocation = 0;
        }

        public String Identifier { get; set; }
        public String Name { get; set; }
        public PointLatLng ActualPosition { get; set; }
        public PointLatLng NextPosition { get; set; }
        public int PreviousLocation{ get; set; }
        public int ActualLocation  { get; set; }

        public long TimeLocatino { get; set; }


       


    }
}
