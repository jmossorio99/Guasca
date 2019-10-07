﻿using GMap.NET.WindowsForms;
using System;
using System.Windows.Forms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.MapProviders;
using GMap.NET;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Collections;
using System.Globalization;

namespace MIOSimulation
{
    public partial class SimulacionMetroCali : Form
    {

        //Overlays to store markers and routes
        private GMapOverlay stops = new GMapOverlay("stops");
        private GMapOverlay fullStations = new GMapOverlay();
        private GMapOverlay stationsZoomedOut = new GMapOverlay();
        private GMapOverlay routes = new GMapOverlay("routes");
        private GMapOverlay simulation = new GMapOverlay("routes");
        private List<String> dataSimulation = new List<string>();
        private List<PointLatLng> points = new List<PointLatLng>();
        private GMapOverlay polygons = new GMapOverlay();
        private GMapOverlay Zones = new GMapOverlay();
        private List<Polygon> pZones = new List<Polygon>();
        private int number=0;
        private Double zoom1 = 15;
        private readonly string stationNamesFilePath = "./stationNames.txt";
        private List<String> stationNames;
        Dictionary<String, List<String>> stationsTable = new Dictionary<String, List<String>>();
        private Boolean filterSelected = false;
        

        public SimulacionMetroCali()
        {
            InitializeComponent();
        }

        private void Map_Load(object sender, EventArgs e)
        {
            //setting up the map
            addZones();
            stationNames = readStationNames();
            addListsToHashset();
            addAllStopsAndStations();
            
            gmap.MapProvider = GoogleMapProvider.Instance;
            GMaps.Instance.Mode = AccessMode.ServerOnly;
            gmap.Position = new PointLatLng(3.436440, -76.515270);
            gmap.ShowCenter = false;
            gmap.Zoom = 13;
            gmap.Overlays.Add(Zones);

            StationStop_CB.Items.Add("Estaciones y paradas");
            StationStop_CB.Items.Add("Estaciones");
            StationStop_CB.Items.Add("Paradas");
            StationStop_CB.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void addZones()
        {
            FileReader frZones = new FileReader("CoordenatesPolygons.txt");
            List<String> zonesData = frZones.readFile();
            int size = zonesData.Count;
            List<int> toSee = new List<int>{0,1,2,3,4,5,6,7,8};
            foreach (var i in toSee) {
                String elem = zonesData[i];
                Polygon example = new Polygon(elem, "Zone "+i);
                Zones.Polygons.Add(new GMapPolygon(example.getPolygon(), "Zone"+ i));
                polygons.Polygons.Add(new GMapPolygon(example.getPolygon(), "Zone" + i));
                pZones.Add(example);
            }
            
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
                PointLatLng location = new PointLatLng(Double.Parse(tempSplit[7], CultureInfo.InvariantCulture.NumberFormat), Double.Parse(tempSplit[6], CultureInfo.InvariantCulture.NumberFormat)); 
                marker = new GMarkerGoogle(location , GMarkerGoogleType.blue_small);
                marker.ToolTipText = tempSplit[3];
                String zone = "";
                foreach (var pol in pZones) {
                    if (pol.isInside(location)) {
                        zone = pol.getName();
                        break;
                    }
                }
                marker.ToolTipText += " En " + zone;

                stops.Markers.Add(marker);
            }
            foreach (var item in stationData)
            {
                String[] tempSplit = item.Split(',');
                GMapMarker marker;
                PointLatLng location = new PointLatLng(Double.Parse(tempSplit[7], CultureInfo.InvariantCulture.NumberFormat), Double.Parse(tempSplit[6], CultureInfo.InvariantCulture.NumberFormat));
                marker = new GMarkerGoogle(location, new Bitmap("./img/station.png"));
                marker.ToolTipText = tempSplit[3];

                String zone = "";

                String name = isStation(tempSplit[3]);
                List<String> temp = stationsTable[name];

                foreach (var pol in pZones)
                {
                    if (pol.isInside(location))
                    {
                        zone = pol.getName();
                        break;
                    }
                }
                marker.ToolTipText += " En " + zone;

                temp.Add(item);
                fullStations.Markers.Add(marker);

            }
            setStationsPolygons();

        }

        private void setStationsPolygons() {

            foreach (var item in stationNames)
            {
                // each station name is the key for the dictionary to access that station's list of full stops
                String key = item;
                List<String> lines = stationsTable[key];
                List<PointLatLng> points = new List<PointLatLng>();

                Boolean addedFirst = false;
                foreach (var line in lines)
                {
                    String[] temp = line.Split(',');
                    points.Add(new PointLatLng(Double.Parse(temp[7], CultureInfo.InvariantCulture.NumberFormat), Double.Parse(temp[6], CultureInfo.InvariantCulture.NumberFormat)));
                    if (!addedFirst)
                    {
                        stationsZoomedOut.Markers.Add(new GMarkerGoogle(new PointLatLng(Double.Parse(temp[7], CultureInfo.InvariantCulture.NumberFormat), Double.Parse(temp[6], CultureInfo.InvariantCulture.NumberFormat)), new Bitmap("./img/station.png")));
                        addedFirst = true;
                    }
                }
                paintPolygon(points, key);
            }

        }

        private void paintPolygon(List<PointLatLng> points, String stationName) {

            if (points.Count > 2)
            {
                GMapPolygon polygon = new GMapPolygon(convexHull(points), stationName);
                polygons.Polygons.Add(polygon);
            }
            else
            {
                GMapPolygon polygon = new GMapPolygon(points, stationName);
                polygons.Polygons.Add(polygon);
            }

        }

        private List<PointLatLng> convexHull(List<PointLatLng> points) {

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

        private int orientation(PointLatLng p, PointLatLng q, PointLatLng r) {

            //Lat = y, Lng = x
            Double val = (q.Lat - p.Lat) * (r.Lng - q.Lng) - (q.Lng - p.Lng) * (r.Lat - q.Lat);
            if (val == 0) return 0;
            return (val > 0) ? 1 : 2;

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

            filterSelected = true;
            
            if(StationStop_CB.SelectedIndex == 0)
            {
                gmap.Overlays.Clear();
                gmap.Overlays.Add(stops);
                gmap.Overlays.Add(polygons);
                gmap.Zoom = 12.5;
                addStationsOverlay();
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
                gmap.Overlays.Add(polygons);
                gmap.Zoom = 12.5;
                addStationsOverlay();
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
                Double lat1 = Double.Parse(tempSplit[4], CultureInfo.InvariantCulture.NumberFormat);
                Double lng1 = Double.Parse(tempSplit[3], CultureInfo.InvariantCulture.NumberFormat);
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

        private void addStationsOverlay() {

            if (filterSelected)
            {

                Double zoom = gmap.Zoom;
                zoomLbl.Text = zoom + "";
                if (StationStop_CB.SelectedIndex == 0)
                {
                    if (Math.Floor(zoom) > 16)
                    {
                        gmap.Overlays.Clear();
                        gmap.Overlays.Add(fullStations);
                        gmap.Overlays.Add(stops);
                        gmap.Overlays.Add(polygons);
                    }
                    else
                    {
                        gmap.Overlays.Clear();
                        gmap.Overlays.Add(stationsZoomedOut);
                        gmap.Overlays.Add(stops);
                        gmap.Overlays.Add(polygons);
                    }
                }
                else if(StationStop_CB.SelectedIndex == 1)
                {
                    if (Math.Floor(zoom) > 16)
                    {
                        gmap.Overlays.Clear();
                        gmap.Overlays.Add(fullStations);
                        gmap.Overlays.Add(polygons);
                    }
                    else
                    {
                        gmap.Overlays.Clear();
                        gmap.Overlays.Add(stationsZoomedOut);
                        gmap.Overlays.Add(polygons);
                    }
                }
                
            }

        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
