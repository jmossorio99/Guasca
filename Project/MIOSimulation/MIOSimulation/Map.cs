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
        private GMapOverlay routes = new GMapOverlay("routes");
        private GMapOverlay simulation = new GMapOverlay("routes");
        private List<String> dataSimulation = new List<string>();
        private List<PointLatLng> points = new List<PointLatLng>();
        private int number=0;
        private Double zoom1 = 15;
        private readonly string stationNamesFilePath = "./stationNames.txt";
        //private List<String> stationNames;

        public Map()
        {
            InitializeComponent();
        }

        private void Map_Load(object sender, EventArgs e)
        {
            //setting up the map
            //stationNames = readStationNames();
            //stationNames.Sort();
            addAllStopsAndStations();

            gmap.MapProvider = GoogleMapProvider.Instance;
            GMaps.Instance.Mode = AccessMode.ServerOnly;
            gmap.Position = new PointLatLng(3.436440, -76.515270);
            gmap.ShowCenter = false;
            gmap.Zoom = 13;

            //gmap.Overlays.Add(stops);
           // gmap.Overlays.Add(stations);
            //gmap.Overlays.Add(routes);
            

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
        }

        private List<String> readStationNames()
        {

            FileReader fr = new FileReader(stationNamesFilePath);
            return fr.readFile();

        }

        private void Gmap_Load(object sender, EventArgs e)
        {

        }

        private void StationStop_CB_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if(StationStop_CB.SelectedIndex == 0)
            {
                gmap.Overlays.Clear();
                gmap.Overlays.Add(stops);
                gmap.Overlays.Add(stations);
                gmap.Zoom = 12.5;
            }
            else if(StationStop_CB.SelectedIndex == 2)
            {
                gmap.Overlays.Clear();
                gmap.Overlays.Add(stops);
                gmap.Zoom = 12.5;
            }
            else
            {
                gmap.Overlays.Clear();
                gmap.Overlays.Add(stations);
                gmap.Zoom = 12.5;
            }
        }

        private void StartSimulation_Click(object sender, EventArgs e)
        {
            FileReader frUbication = new FileReader("datagramList.txt");
            dataSimulation = frUbication.readFile();
            timer1.Start();

        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if(number< dataSimulation.Count)
            {
                gmap.Overlays.Clear();
                routes.Routes.Clear();
                simulation.Markers.Clear();
                String[] tempSplit = dataSimulation[number].Split(';');
                GMapMarker marker;
                Double lat1 = Double.Parse(tempSplit[4]);
                Double lng1 = Double.Parse(tempSplit[3]);
                marker = new GMarkerGoogle(new PointLatLng(lat1, lng1), new Bitmap("./img/bus.png"));
                points.Add(new PointLatLng(lat1, lng1));
                if (Double.Parse(tempSplit[2]) != -1)
                {
                    marker.ToolTipText = "En ruta a las " + tempSplit[0];
                    prueba.Text = "En ruta a las " + tempSplit[0];
                }
                else
                {
                    marker.ToolTipText = "Tiempo muerto a las " + tempSplit[0];
                    prueba.Text= "Tiempo muerto a las " + tempSplit[0];
                }
                simulation.Markers.Add(marker);
                gmap.Overlays.Add(simulation);
                gmap.Position = new PointLatLng(lat1, lng1);
                gmap.Zoom = zoom1;
                GMapRoute pointsRoutes = new GMapRoute(points, "Ruta");
                routes.Routes.Add(pointsRoutes);
                gmap.Overlays.Add(routes);
                number++;
            }
            else
            {
                timer1.Stop();
            }
        }

        private void GoSimulation_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }

        private void Faster_Click(object sender, EventArgs e)
        {
            if (timer1.Interval > 50)
            {
                timer1.Interval = timer1.Interval - 50;
            }
        }

        private void Slower_Click(object sender, EventArgs e)
        {
            if (timer1.Interval < 1000)
            {
                timer1.Interval = timer1.Interval+50;
            }
        }

        private void Zoomplus_Click(object sender, EventArgs e)
        {
            if (zoom1 < 16)
            {
                zoom1 = zoom1 + 0.5;
            }
        }

        private void Zoom_Click(object sender, EventArgs e)
        {
            if (zoom1 > 6)
            {
                zoom1 = zoom1 - 0.5;
            }
        }

        //private Boolean isStation(String name)
        //{

        //    foreach (var item in stationNames)
        //    {
        //        String[] temp = name.Split(' ');
        //        String last = temp[temp.Length - 1];
        //        if (name.Contains(item) && last.Length == 2)
        //        {
        //            try
        //            {
        //                int peti = Convert.ToInt32(last[1]);
        //                return true;
        //            }
        //            catch (Exception)
        //            {
        //                return false;
        //            }

        //        }
        //    }
        //    return false;

        //}


    }
}
