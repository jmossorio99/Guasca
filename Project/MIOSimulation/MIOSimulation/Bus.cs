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

        public Bus(string identifier, string name)
        {
            this.identifier = identifier;
            this.Name = name;
            ActualPosition = new PointLatLng(0, 0);

            NextPosition = new PointLatLng(0, 0);
        }

        public String Identifier { get; set; }
        public String Name { get; set; }
        public PointLatLng ActualPosition { get; set; }
        public PointLatLng NextPosition { get; set; }
    }
}
