using GMap.NET.WindowsForms;
using System;
using System.Windows.Forms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.MapProviders;
using GMap.NET;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Collections;

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
        Dictionary<String, List<String>> stationsTable = new Dictionary<String, List<String>>();
        

        public Map()
        {
            InitializeComponent();
        }

        private void Map_Load(object sender, EventArgs e)
        {
            //setting up the map
            stationNames = readStationNames();
            //stationNames.Sort();
            addListsToHashset();
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

                String name = isStation(tempSplit[3]);
                List<String> temp = stationsTable[name];
                temp.Add(item);
                
            }
            setStationsPolygons();

        }

        private void setStationsPolygons() {

            foreach (var item in stationNames)
            {
                String key = item;
                List<String> lines = stationsTable[key];
                List<PointLatLng> points = new List<PointLatLng>();
                foreach (var line in lines)
                {
                    String[] temp = line.Split(',');
                    points.Add(new PointLatLng(Double.Parse(temp[7]), Double.Parse(temp[6])));
                }
                paintPolygon(points, key);
            }

        }

        private void paintPolygon(List<PointLatLng> points, String stationName) {

            GMapPolygon polygon = new GMapPolygon(points, stationName + " ");
            polygons.Polygons.Add(polygon);

        }

        private List<String> readStationNames()
        {

            FileReader fr = new FileReader(stationNamesFilePath);
            return fr.readFile();

        }

        private String isStation(String name)
        {
            String ret = "";

            foreach (var item in stationNames)
            {
                if (name.Contains(item))
                {
                    ret = item;
                    break;
                }
            }
            return ret;

        }

        private void addListsToHashset()
        {

            foreach (var item in stationNames)
            {
                stationsTable.Add(item, new List<string>());
            }

        }


    }
}
