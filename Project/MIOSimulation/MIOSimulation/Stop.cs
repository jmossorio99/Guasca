using GMap.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIOSimulation
{
    class Stop
    { 
        private String name;
        private PointLatLng position;
        private Zone zone;


        public Stop(String name, PointLatLng position, Zone zone)
        {
            this.name = name;
            this.position = position;
            this.zone = zone;
        }

        public PointLatLng Position { get; set; }

        public String Name { get; set; }

        public Zone Zone { get; set; }
    }

    
}
