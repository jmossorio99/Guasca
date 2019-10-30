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
        private PointLatLng nextPostion;

        public Bus(string identifier, string name)
        {
            this.identifier = identifier;
            this.Name = name;
            ActualPostion = new PointLatLng(0, 0);

            NextPostion = new PointLatLng(0, 0);
        }

        public String Identifier { get; set; }
        public String Name { get; set; }
        public PointLatLng ActualPostion { get; set; }
        public PointLatLng NextPostion { get; set; }
    }
}
