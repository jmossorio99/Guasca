using GMap.NET.WindowsForms;
using System;
using System.Windows.Forms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.MapProviders;
using GMap.NET;
using System.Collections.Generic;

namespace MIOSimulation
{
    public partial class Map : Form
    {

        //Overlays to store markers and routes
        private GMapOverlay markers = new GMapOverlay();
        private GMapOverlay routes = new GMapOverlay();
        private int numberOfStations;

        public Map()
        {
            InitializeComponent();
        }

        private void Map_Load(object sender, EventArgs e)
        {
            //setting up the map
            addAllStops();
            gmap.MapProvider = GoogleMapProvider.Instance;
            GMaps.Instance.Mode = AccessMode.ServerOnly;
            gmap.Position = new PointLatLng(3.442275, -76.515539);
            gmap.ShowCenter = false;
            gmap.Overlays.Add(markers);
            gmap.Overlays.Add(routes);
            StationStop_CB.Items.Add("Estaciones y paradas");
            StationStop_CB.Items.Add("Estaciones");
            StationStop_CB.Items.Add("Paradas");
            numberOfStations = 0;
        }

        private void addAllStops()
        {
            FileReader fr = new FileReader("stops.csv");
            List<String> stopData = fr.readFile();
            foreach (var item in stopData)
            {
                String[] tempSplit = item.Split(',');
                markers.Markers.Add(new GMarkerGoogle(new PointLatLng(Double.Parse(tempSplit[7]), Double.Parse(tempSplit[6])), GMarkerGoogleType.blue_small));
            }

            List<PointLatLng> points = new List<PointLatLng>();
            PointLatLng p1 = new PointLatLng(3.41454278, -76.54957278);
            PointLatLng p2 = new PointLatLng(3.41476833, -76.54962139);
           // PointLatLng p3 = new PointLatLng(-76.89785083,3.487897899107);
            points.Add(p1);
            points.Add(p2);
           // points.Add(p3);
            paintPolygon(points,"Terminal cañaveralejo");

        }

        private void paintPolygon(List<PointLatLng> points, String stationName) {

            GMapPolygon polygon = new GMapPolygon(points, stationName + " " + numberOfStations);
            
            GMapOverlay polygons = new GMapOverlay("station " + numberOfStations);
            
            polygons.Polygons.Add(polygon);

            //update gmap

            gmap.Overlays.Add(polygons);

            PointLatLng center = getCenterPoint(points);
            GMarkerGoogle marker1 = new GMarkerGoogle(center,GMarkerGoogleType.blue);
            marker1.ToolTipText = ""+stationName;

            markers.Markers.Add(marker1);
            gmap.Overlays.Add(markers);

            numberOfStations++;
        }

        private PointLatLng getCenterPoint(List<PointLatLng> vertexes) {

            PointLatLng centerPoint = new PointLatLng();

            int sum = 0;
            double lat = 0;
            double lng = 0;

            foreach (var point in vertexes) {
                sum++;
                lat += point.Lat;
                lng += point.Lng;
            }

            lat = lat / sum;
            lng = lng / sum;
            centerPoint.Lat = lat;
            centerPoint.Lng = lng;

            return centerPoint;
        }
    }
}
