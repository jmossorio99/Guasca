using GMap.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIOSimulation
{
    class BusLocation
    {
        private PointLatLng postion;
        private string busName;

        public BusLocation(PointLatLng postion, string busName)
        {
            Postion = postion;
            BusName = busName;
        }

        public PointLatLng Postion { get; set; }
        public string BusName { get; set; }
    }
}
