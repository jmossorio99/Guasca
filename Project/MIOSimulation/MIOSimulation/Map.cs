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
using System.Globalization;
using System.Diagnostics;

namespace MIOSimulation
{
    public partial class SimulacionMetroCali : Form
    {

        //Overlays to store markers and routes
        private GMapOverlay routes = new GMapOverlay("routes");
        private GMapOverlay simulation = new GMapOverlay("routes");
        private List<String> dataSimulation = new List<string>();
        private List<PointLatLng> points = new List<PointLatLng>();
        private readonly string stationNamesFilePath = "./stationNames.txt";
        private List<String> stationNames;
        private Boolean filterSelected = false;
        private List<int> zonesChecked;
        private List<GMapOverlay> stationsZoomedOutList = new List<GMapOverlay>();
        private List<GMapOverlay> fullStationsOverlays = new List<GMapOverlay>();
        private List<GMapOverlay> stopsListOverlays = new List<GMapOverlay>();
        private List<GMapOverlay> polygonsList = new List<GMapOverlay>();
        private List<GMapOverlay> zonesOverlays = new List<GMapOverlay>();

        //Everything new is down here
        private List<Zone> zones = new List<Zone>();

        StreamWriter sw = new StreamWriter("stationsData.txt");

        BusSimulationControl busSimulation;

        public SimulacionMetroCali()
        {
            InitializeComponent();
        }

        private void Map_Load(object sender, EventArgs e)
        {
            //setting up the map
            for (int i = 0; i < 9; i++) {
                stationsZoomedOutList.Add(new GMapOverlay());
                stopsListOverlays.Add(new GMapOverlay());
                fullStationsOverlays.Add(new GMapOverlay());
                polygonsList.Add(new GMapOverlay());
                zonesOverlays.Add(new GMapOverlay());
            }
            zonesChecked = new List<int>();
            addZones();
            stationNames = readStationNames();
            addAllStopsAndStations();

            string url = "dataSimulation1.txt";
            if (MessageBox.Show("Desea usar un datagram diferente?", "Datagram de simulacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                OpenFileDialog open = new OpenFileDialog();
                if (open.ShowDialog() == DialogResult.OK)
                {
                    url = open.FileName;
                    DataProcessor p = new DataProcessor();
                    p.reading(url);
                }
            }
            
            busSimulation = new BusSimulationControl(0,0);
            
            gmap.MapProvider = GoogleMapProvider.Instance;
            GMaps.Instance.Mode = AccessMode.ServerOnly;
            gmap.Position = new PointLatLng(3.436440, -76.515270);
            gmap.ShowCenter = false;
            gmap.Zoom = 13;
            StationStop_CB.Items.Add("Estaciones y paradas");
            StationStop_CB.Items.Add("Estaciones");
            StationStop_CB.Items.Add("Paradas");
            StationStop_CB.DropDownStyle = ComboBoxStyle.DropDownList;

            
            trackBar1.Value = Convert.ToInt32(gmap.Zoom);
        }

        private void addZones()
        {

            FileReader frZones = new FileReader("CoordenatesPolygons.txt");
            List<String> zonesData = frZones.readFile();

            for (int i = 0; i < 9; i++)
            {
                String zonePerimeter = zonesData[i];
                Zone newZone = new Zone("Zona " + i, i, zonePerimeter);
                zones.Add(newZone);
                GMapPolygon newPolygon = new GMapPolygon(newZone.getPerimeter().getPolygon(), newZone.getName());
                newPolygon.Fill = new SolidBrush(Color.Transparent);
                newPolygon.Stroke = new Pen(Color.Red, 1);
                zonesOverlays[i].Polygons.Add(newPolygon);
                zonesChecked.Add(i);
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
                String[] tempStopData = item.Split(',');
                GMapMarker marker;
                PointLatLng location = new PointLatLng(Double.Parse(tempStopData[7], CultureInfo.InvariantCulture.NumberFormat), Double.Parse(tempStopData[6], CultureInfo.InvariantCulture.NumberFormat)); 
                marker = new GMarkerGoogle(location , GMarkerGoogleType.blue_small);
                marker.ToolTipText = tempStopData[3];
                String zoneName = "";
                int zoneNum = -1;
                Stop newStop;
                foreach (var zone in zones) {
                    if (zone.isInside(location)) {
                        zoneName = zone.getName();
                        zoneNum = zone.getNumber();
                        newStop = new Stop(marker.ToolTipText, location, zone);
                        zone.addStop(newStop);
                        break;
                    }
                }
                marker.ToolTipText += " En " + zoneName;
                (stopsListOverlays[zoneNum]).Markers.Add(marker);
            }

            foreach (var item in stationData)
            {
                String[] tempStationData = item.Split(',');
                GMapMarker marker;
                PointLatLng location = new PointLatLng(Double.Parse(tempStationData[7], CultureInfo.InvariantCulture.NumberFormat), Double.Parse(tempStationData[6], CultureInfo.InvariantCulture.NumberFormat));
                marker = new GMarkerGoogle(location, new Bitmap("./img/station.png"));
                marker.ToolTipText = tempStationData[3];

                String zoneName = "";
                int zoneNum = -1;
                String stationName = isStation(tempStationData[3]);
                Station newStation;

                foreach (var zone in zones)
                {
                    if (zone.isInside(location))
                    {
                        zoneName = zone.getName();
                        zoneNum = zone.getNumber();
                        Stop newStop = new Stop(marker.ToolTipText, location, zone);
                        try
                        {
                            newStation = zone.getStation(stationName);
                            zone.addStopToStation(stationName, newStop);
                        }
                        catch (Exception)
                        {
                            newStation = new Station(stationName, zone);
                            zone.addStation(stationName, newStation);
                            zone.addStopToStation(stationName, newStop);
                        }
                        
                        break;
                    }
                }
                marker.ToolTipText += " En " + zoneName;
                (fullStationsOverlays[zoneNum]).Markers.Add(marker);
            }

            //Adding the perimeter polygon for each station
            foreach (var zone in zones)
            {

                List<String> stationNamesInZone = zone.getStationsNames();
                foreach (var key in stationNamesInZone)
                {
                    Station station = zone.getStation(key);
                    List<Stop> stopsInStation = station.getStationStops();
                    List<PointLatLng> perimeterPoints = new List<PointLatLng>();
                    Boolean addedFirst = false;
                    foreach (var stop in stopsInStation)
                    {
                        perimeterPoints.Add(stop.getPosition());
                        if (!addedFirst)
                        {

                            GMapMarker markerStation = new GMarkerGoogle(stop.getPosition(), new Bitmap("./img/station.png"));
                            markerStation.ToolTipText = stop.getName() + "En " + zone.getName();
                            stationsZoomedOutList[zone.getNumber()].Markers.Add(markerStation);
                            addedFirst = true;

                        }
                    }
                    station.setPerimeter(new Polygon(perimeterPoints, station.getName()));
                }

            }
            //Painting all the station polygons
            setStationsPolygons();

        }

        public void writeStop(String item, String zone)
        {
            String o = item.Replace(',', ';');
            sw.WriteLine(o+';'+zone.Split(' ')[1]);
        }

        private void setStationsPolygons() {

            foreach (var zone in zones)
            {
                List<String> stationNamesInZone = zone.getStationsNames();
                foreach (var key in stationNamesInZone)
                {
                    Station station = zone.getStation(key);
                    Polygon stationPolygon = station.getPerimeter();
                    paintPolygon(stationPolygon, station.getName(), station.getZone().getNumber());
                }
            }

        }

        private void paintPolygon(Polygon stationPolygon, String stationName, int zoneNumber) {

            if (stationPolygon.getPolygon().Count > 2)
            {
                GMapPolygon polygon = new GMapPolygon(stationPolygon.getHull(), stationName);
                ((GMapOverlay)polygonsList[zoneNumber]).Polygons.Add(polygon);
            }
            else
            {
                GMapPolygon polygon = new GMapPolygon(stationPolygon.getPolygon(), stationName);
                ((GMapOverlay)polygonsList[zoneNumber]).Polygons.Add(polygon);
            }

        }

        private List<String> readStationNames()
        {

            FileReader fr = new FileReader(stationNamesFilePath);
            return fr.readFile();

        }

        private void StationStop_CB_SelectedIndexChanged(object sender, EventArgs e)
        {
            filterSelected = true;
            
            if(StationStop_CB.SelectedIndex == 0)
            {
                addStationsOverlay();
                gmap.Zoom = 12.5;
            }
            else if(StationStop_CB.SelectedIndex == 2)
            {
                gmap.Overlays.Clear();
                foreach (int a in zonesChecked)
                {
                    gmap.Overlays.Add(stopsListOverlays[a]);
                    gmap.Overlays.Add(zonesOverlays[a]);
                }
                gmap.Zoom = 12.5;
            }
            else
            {
                addStationsOverlay();
                gmap.Zoom = 12.5;
            }

        }

        private void addStationsOverlay()
        {

            trackBar1.Value = Convert.ToInt32(gmap.Zoom);
            if (filterSelected)
            {

                Double zoom = gmap.Zoom;
                if (StationStop_CB.SelectedIndex == 0)
                {
                    if (Math.Floor(zoom) > 16)
                    {
                        gmap.Overlays.Clear();
                        foreach (int a in zonesChecked) {
                            gmap.Overlays.Add(fullStationsOverlays[a]);
                            gmap.Overlays.Add(stopsListOverlays[a]);
                            gmap.Overlays.Add(polygonsList[a]);
                            gmap.Overlays.Add(zonesOverlays[a]);
                        }
                    }
                    else
                    {
                        gmap.Overlays.Clear();
                        foreach (int a in zonesChecked)
                        {
                            gmap.Overlays.Add(stationsZoomedOutList[a]);
                            gmap.Overlays.Add(stopsListOverlays[a]);
                            gmap.Overlays.Add(zonesOverlays[a]);
                        }
                    }
                }
                else if (StationStop_CB.SelectedIndex == 1)
                {
                    if (Math.Floor(zoom) > 16)
                    {
                        gmap.Overlays.Clear();
                        foreach (int a in zonesChecked)
                        {
                            gmap.Overlays.Add(fullStationsOverlays[a]);
                            gmap.Overlays.Add(polygonsList[a]);
                            gmap.Overlays.Add(zonesOverlays[a]);
                        }
                    }
                    else
                    {
                        gmap.Overlays.Clear();
                        foreach (int a in zonesChecked)
                        {
                            gmap.Overlays.Add(stationsZoomedOutList[a]);
                            gmap.Overlays.Add(zonesOverlays[a]);
                        }
                    }
                }

            }

        }

        private void StartSimulation_Click(object sender, EventArgs e)
        {
            //FileReader frUbication = new FileReader("datagramList.txt");
            //dataSimulation = frUbication.readFile();
            gmap.Overlays.Clear();
            routes.Routes.Clear();
            busSimulation.setInterval("20/06/2019 11:16:47", "20/06/2019 11:36:49");
            timer1.Start();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {

            List<Bus> inSimulation = busSimulation.Next30();
            int second = 1;
            gmap.Overlays.Clear();
            routes.Routes.Clear();
            if (inSimulation.Count != 0)
            {
                drawBuses(inSimulation,second);
                second += 30;
            }
            else
            {
                timer1.Stop();
            }
        }

        private void drawBuses(List<Bus> buses,int second) {
            List<Bus> readyToUse = specifiedBuses(buses);
            smothTrasition(readyToUse,second);
        }


        private List<Bus> specifiedBuses(List<Bus> buses)
        {
            List<Bus> readyToUse = new List<Bus>();
            foreach (Bus bus in buses) {
                foreach (int a in zonesChecked)
                {
                    if (zones[a].isInside(bus.ActualPosition))
                    {
                        bus.Zone = zones[a].getName();
                        readyToUse.Add(bus);
                        break;
                    }
                }
            }
            return readyToUse;
        }

        private void smothTrasition(List<Bus> readyToUse,int second)
        {
            gmap.Overlays.Clear();
            routes.Routes.Clear();
            simulation.Clear();
            foreach (Bus bus in readyToUse)
            {
                GMapMarker marker;
                if (bus.IsIddle)
                {
                    marker = new GMarkerGoogle(bus.ActualPosition, new Bitmap("./img/redBus.png"));
                }
                else
                {
                    marker = new GMarkerGoogle(bus.ActualPosition, new Bitmap("./img/greenBus.png"));
                }
                points.Add(bus.ActualPosition);
                marker.ToolTipText = "Time Lost " + bus.TimeLocation;

                //marker.ToolTipText = "En ruta a las " + tempSplit[0];
                prueba.Text = "On route at " + second;

                simulation.Markers.Add(marker);
            }

            gmap.Overlays.Add(simulation);
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
            gmap.Zoom = gmap.Zoom + 1;
            trackBar1.Value = Convert.ToInt32(gmap.Zoom);
        }

        private void Zoom_Click(object sender, EventArgs e)
        {
            gmap.Zoom = gmap.Zoom - 1;
            trackBar1.Value = Convert.ToInt32(gmap.Zoom);
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

        private void updateCheckdZones() {

            zonesChecked = new List<int>();
            foreach (int s in zonesCheckedList.CheckedIndices) {
                zonesChecked.Add(s);
            }
        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {

        }

        private void TrackBar1_ValueChanged(object sender, EventArgs e)
        {
            gmap.Zoom = trackBar1.Value;
        }

        private void Gmap_Load_1(object sender, EventArgs e)
        {

        }

        private void ZonesCheckedList_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateCheckdZones();
        }
    }
}

/*
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
*/
