using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMap.NET.WindowsForms;
using System.Windows.Forms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.MapProviders;
using GMap.NET;
using System.IO;
using System.Drawing;
using System.Collections;
using System.Globalization;

namespace MIOSimulation
{
    class Polygon
    {
        private List<PointLatLng> polygon;
        private String name;

        public List<PointLatLng> getPolygon() {
            return polygon;
        }

        public String getName() {
            return name;
        }

        public Polygon(String data,String name) {
            this.name = name;
            polygon = new List<PointLatLng>();
            String[] sData = data.Split('#');

            foreach (var location in sData)
            {
                if (location.Contains(',')) {
                    String[] pData = location.Split(':');
                    String[] lData = pData[1].Split(',');
                    double lat = Double.Parse(lData[0], CultureInfo.InvariantCulture.NumberFormat);
                    double lng = Double.Parse(lData[1], CultureInfo.InvariantCulture.NumberFormat);

                    polygon.Add(new PointLatLng(lat, lng));
                }
            }
            
        }

        //Lat = y, Lng = x
        public Boolean onSegment(PointLatLng p, PointLatLng q, PointLatLng r) {
            if (q.Lat <= Math.Max(p.Lat, r.Lat) && q.Lat >= Math.Min(p.Lat, r.Lat)
                && q.Lng <= Math.Max(p.Lng, r.Lng) && q.Lng >= Math.Min(p.Lng, r.Lng)) { return true; }

            return false;
        }

        public int orientation(PointLatLng p, PointLatLng q, PointLatLng r)
        {
            double val = (q.Lat - p.Lat) * (r.Lng - q.Lng) -
                (q.Lng - p.Lng) * (r.Lat - q.Lat);

            if (val == 0) return 0;
            return (val > 0) ? 1 : 2;
        }

        public bool doIntersect(PointLatLng p1, PointLatLng q1, PointLatLng p2, PointLatLng q2) {
            int o1 = orientation(p1, q1, p2);
            int o2 = orientation(p1, q1, q2);
            int o3 = orientation(p2, q2, p1);
            int o4 = orientation(p2, q2, q1);

            if (o1 != o2 && o3 != o4)
                return true;

            // p1, q1 and p2 are colinear and p2 lies on segment p1q1 
            if (o1 == 0 && onSegment(p1, p2, q1)) return true;

            // p1, q1 and p2 are colinear and q2 lies on segment p1q1 
            if (o2 == 0 && onSegment(p1, q2, q1)) return true;

            // p2, q2 and p1 are colinear and p1 lies on segment p2q2 
            if (o3 == 0 && onSegment(p2, p1, q2)) return true;

            // p2, q2 and q1 are colinear and q1 lies on segment p2q2 
            if (o4 == 0 && onSegment(p2, q1, q2)) return true;

            return false;

        }

        public bool isInside(PointLatLng p) {
            int n = polygon.Count;

            if (n < 3) return false;

            PointLatLng extreme = new PointLatLng(10000000,p.Lat);

            int count = 0, i = 0;

            do
            {
                int next = (i + 1) % n;

                if (doIntersect(polygon[i], polygon[next], p, extreme)) {
                    if (orientation(polygon[i], p, polygon[next]) == 0)
                    {
                        return onSegment(polygon[i], p, polygon[next]);
                    }

                    count++;
                }

                i = next;
            } while (i != 0);

            return count % 2 == 1 ? true : false;
        }
    }

    
}
