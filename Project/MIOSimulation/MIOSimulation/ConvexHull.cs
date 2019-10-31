using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMap.NET;

namespace MIOSimulation
{
    class ConvexHull
    {

        public List<PointLatLng> getHull(List<PointLatLng> points)
        {

            List<PointLatLng> hull = new List<PointLatLng>();
            int n = points.Count;

            int l = 0;
            for (int i = 1; i < n; i++)
            {
                if (points[i].Lng < points[l].Lng)
                {
                    l = i;
                }
            }

            int p = l;
            int q;
            do
            {

                hull.Add(points[p]);
                q = (p + 1) % n;
                for (int i = 0; i < n; i++)
                {
                    if (orientation(points[p], points[i], points[q]) == 2)
                    {
                        q = i;
                    }
                }
                p = q;

            } while (p != l);

            return hull;

        }

        private int orientation(PointLatLng p, PointLatLng q, PointLatLng r)
        {

            //Lat = y, Lng = x
            Double val = (q.Lat - p.Lat) * (r.Lng - q.Lng) - (q.Lng - p.Lng) * (r.Lat - q.Lat);
            if (val == 0) return 0;
            return (val > 0) ? 1 : 2;

        }

    }
}
