using GMap.NET.WindowsForms;
using System;
using System.Windows.Forms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.MapProviders;
using GMap.NET;
using System.Collections.Generic;
using System.IO;
using System.Drawing;

namespace MIOSimulation
{
    public partial class Map : Form
    {

        //Overlays to store markers and routes
        private GMapOverlay stops = new GMapOverlay("stops");
        private GMapOverlay stations = new GMapOverlay("stops");
        private GMapOverlay routes = new GMapOverlay();
        private GMapOverlay centerOfStationMark = new GMapOverlay();
        private GMapOverlay polygons = new GMapOverlay();
        private readonly string stationNamesFilePath = "./stationNames.txt";
        private List<String> stationNames;

        public Map()
        {
            InitializeComponent();
        }

        private void Map_Load(object sender, EventArgs e)
        {
            //setting up the map
            stationNames = readStationNames();
            //stationNames.Sort();
            addAllStopsAndStations();

            gmap.MapProvider = GoogleMapProvider.Instance;
            GMaps.Instance.Mode = AccessMode.ServerOnly;
            gmap.Position = new PointLatLng(3.442275, -76.515539);
            gmap.ShowCenter = false;

            gmap.Overlays.Add(stops);
            gmap.Overlays.Add(stations);
            gmap.Overlays.Add(routes);
            gmap.Overlays.Add(polygons);
            gmap.Overlays.Add(centerOfStationMark);

            StationStop_CB.Items.Add("Estaciones y paradas");
            StationStop_CB.Items.Add("Estaciones");
            StationStop_CB.Items.Add("Paradas");
        }

        private void addAllStopsAndStations()
        {
            FileReader frStops = new FileReader("stops.txt");
            FileReader frStations = new FileReader("stations.txt");
            List<String> stopData = frStops.readFile();
            List<String> stationData = frStations.readFile();
            foreach (var item in stopData)
            {
                String[] tempSplit = item.Split(',');
                GMapMarker marker;
                marker = new GMarkerGoogle(new PointLatLng(Double.Parse(tempSplit[7]), Double.Parse(tempSplit[6])), GMarkerGoogleType.blue_small);
                marker.ToolTipText = tempSplit[3];
                stops.Markers.Add(marker);
            }
            foreach (var item in stationData)
            {
                String[] tempSplit = item.Split(',');
                GMapMarker marker;
                marker = new GMarkerGoogle(new PointLatLng(Double.Parse(tempSplit[7]), Double.Parse(tempSplit[6])), new Bitmap("./img/station.png"));
                marker.ToolTipText = tempSplit[3];
                stations.Markers.Add(marker);
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

            GMapPolygon polygon = new GMapPolygon(points, stationName + " ");

            polygons.Polygons.Add(polygon);

            PointLatLng center = getCenterPoint(points);
            GMarkerGoogle marker1 = new GMarkerGoogle(center,GMarkerGoogleType.blue);
            marker1.ToolTipText = ""+stationName;

            centerOfStationMark.Markers.Add(marker1);

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

        private List<String> readStationNames()
        {

            FileReader fr = new FileReader(stationNamesFilePath);
            return fr.readFile();

        }

        private Boolean isStation(String name)
        {

            foreach (var item in stationNames)
            {
                String[] temp = name.Split(' ');
                String last = temp[temp.Length - 1];
                if (name.Contains(item) && last.Length == 2)
                {
                    try
                    {
                        int peti = Convert.ToInt32(last[1]);
                       return true;
                    }
                    catch (Exception)
                    {
                        return false;
                    }

                }
            }
            return false;

        }


    }
}
