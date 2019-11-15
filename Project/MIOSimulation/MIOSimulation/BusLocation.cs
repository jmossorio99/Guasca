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
        private int stopId;
        private Boolean isIddle;

        public BusLocation(PointLatLng postion, string busName,int stopId, Boolean isIddle)
        {
            Postion = postion;
            BusName = busName;
            StopId = stopId;
            IsIddle = isIddle;
        }

        public PointLatLng Postion { get; set; }
        public string BusName { get; set; }

        public int StopId { get; set; }
        public bool IsIddle { get; set; }
    }
}
