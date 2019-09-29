using GMap.NET.WindowsForms;
using System;
using System.Windows.Forms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.MapProviders;
using GMap.NET;
using System.Collections.Generic;
using System.IO;

namespace MIOSimulation
{
    public partial class Map : Form
    {

        //Overlays to store markers and routes
        private GMapOverlay stops = new GMapOverlay("stops");
        private GMapOverlay stations = new GMapOverlay("stops");
        private GMapOverlay routes = new GMapOverlay();
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
            stationNames.Sort();
            addAllStopsAndStations();

            gmap.MapProvider = GoogleMapProvider.Instance;
            GMaps.Instance.Mode = AccessMode.ServerOnly;
            gmap.Position = new PointLatLng(3.442275, -76.515539);
            gmap.ShowCenter = false;

            gmap.Overlays.Add(stops);
            gmap.Overlays.Add(stations);
            gmap.Overlays.Add(routes);

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
                marker = new GMarkerGoogle(new PointLatLng(Double.Parse(tempSplit[7]), Double.Parse(tempSplit[6])), GMarkerGoogleType.green_dot);
                marker.ToolTipText = tempSplit[3];
                stations.Markers.Add(marker);
            }
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
