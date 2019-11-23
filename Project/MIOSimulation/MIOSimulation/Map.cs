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
        private int second;
        private Dictionary<String, String> lines = new Dictionary<String, String>();
        private Dictionary<String, String> reverseLines = new Dictionary<String, String>();

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
            addLines();
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

        private void addLines()
        {
            FileReader frLines = new FileReader("lines.txt");
            List<String> linesData = frLines.readFile();
            List<String> linesD = new List<string>();
            foreach (String line in linesData)
            {
                String[] a = line.Split(',');
                if (!lines.ContainsKey(a[1]))
                {
                    lines.Add(a[1], a[0]);
                    reverseLines.Add(a[0], a[1]);
                    linesD.Add(a[1]);
                }
            }
            foreach (String line in lines.Keys)
            {
                listLines.Items.Add(line);
            }
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
            updateCheckdZones();
            if (StationStop_CB.SelectedIndex == 0)
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
        int error = 0;
        private void StartSimulation_Click(object sender, EventArgs e)
        {
            
            if (verifyFormatDate(horaInicioTxt.Text, horaFinTxt.Text))
            {

                if (verifyDateOrder(horaInicioTxt.Text, horaFinTxt.Text))
                {
                    try
                    {
                        //FileReader frUbication = new FileReader("datagramList.txt");
                        //dataSimulation = frUbication.readFile();
                        gmap.Overlays.Clear();
                        routes.Routes.Clear();
                        //busSimulation.setInterval("20-06-2019 11:16:47", "20-06-2019 11:46:49");
                        busSimulation.setInterval(horaInicioTxt.Text, horaFinTxt.Text);
                        updateCheckdZones();
                        if (zonesChecked.Count == 0)
                        {
                            MessageBox.Show("Seleccione las zonas a visualizar");
                        }
                        int val = 0;
                        if (!int.TryParse(timeInterval.Text, out val)) {
                            MessageBox.Show("Formato incorrecto para el intervalo de tiempo");
                        }

                        if(zonesChecked.Count != 0 && !timeInterval.Text.Equals("")){
                            busSimulation.Interval = val;
                            second = 0;
                            timer1.Start();
                        }
                    }
                    catch (Exception t)
                    {
                        
                    }


                }
                else
                {
                    MessageBox.Show("Orden de fechas incorrecto, la fecha de inicio debe ser menor a la de fin");
                }
            }
            else
            {
                MessageBox.Show("Formato de fecha incorrecto, debe tener el siguiente formato dd-mm-aa hh:mm:ss");
            }

        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            
                List<Bus> inSimulation = busSimulation.Next30();

                gmap.Overlays.Clear();
                routes.Routes.Clear();
                if (inSimulation.Count != 0)
                {
                    drawBuses(inSimulation, second);
                    second += busSimulation.Interval;
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
                    if (listLines.Text.Equals("") || listLines.Text.Equals("N/A"))
                    {
                        if (zones[a].isInside(bus.ActualPosition))
                        {
                            bus.Zone = zones[a].getName();
                            readyToUse.Add(bus);
                            break;
                        }
                    }
                    else
                    {
                        if (zones[a].isInside(bus.ActualPosition) && bus.Name == lines[listLines.Text])
                        {
                            bus.Zone = zones[a].getName();
                            readyToUse.Add(bus);
                            break;
                        }
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
                prueba.Text = "En la ruta a las: " + busSimulation.creatDate(second);
                string rute = "Fuera de servicio";
                if(reverseLines.ContainsKey(bus.Name))
                    rute = reverseLines[bus.Name];
                marker.ToolTipText = "Tiempo perdido: " + bus.TimeLocation + "\n Linea: " +rute;

                //marker.ToolTipText = "En ruta a las " + tempSplit[0];


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
        }

        private Boolean verifyFormatDate(String start,String end)
        {
            Boolean correct = true;

            try
            {

                String[] firstSplitStart = start.Split(' ');
                String[] firstSplitEnd = end.Split(' ');

                String[] secondSplitStartDate = firstSplitStart[0].Split('-');
                String[] secondSplitEndDate = firstSplitEnd[0].Split('-');

                String[] secondSplitStartHour = firstSplitStart[1].Split(':');
                String[] secondSplitEndHour = firstSplitEnd[1].Split(':');

                if (firstSplitStart.Length != 2 || firstSplitEnd.Length != 2 || secondSplitStartDate.Length != 3 || secondSplitStartHour.Length != 3 || secondSplitEndDate.Length != 3 || secondSplitEndHour.Length != 3)
                {
                    correct = false;
                }
                else
                {
                    Boolean t1 = canAllListToInt(secondSplitStartDate);
                    Boolean t2 = canAllListToInt(secondSplitStartHour);
                    Boolean t3 = canAllListToInt(secondSplitEndDate);
                    Boolean t4 = canAllListToInt(secondSplitEndHour);

                    if (t1 == false || t2 == false || t3 == false || t4 == false)
                    {
                        correct = false;
                    }
                }

            }
            catch (Exception e)
            {
                correct = false;
            }

            return correct;
        }

        private Boolean canAllListToInt(String[] toTest)
        {
            Boolean can = true;

            foreach (var temp in toTest)
            {
                int size = temp.Length;

                for (int i =0;i<size && can;i++)
                {
                    int charTemp =(int) temp[i];

                    if (!(47<charTemp&&charTemp<58))
                    {
                        can = false;
                    }
                }

            }

            return can;
        }

        private Boolean verifyDateOrder(String startDate, String endDate)
        {
            Boolean correct=true;

            String[] firstSplitStart = startDate.Split(' ');
            String[] firstSplitEnd = endDate.Split(' ');

            String[] secondSplitStartDate = firstSplitStart[0].Split('-');
            String[] secondSplitEndDate = firstSplitEnd[0].Split('-');

            String[] secondSplitStartHour = firstSplitStart[1].Split(':');
            String[] secondSplitEndHour = firstSplitEnd[1].Split(':');

            int startYear = Int32.Parse(secondSplitStartDate[0]);
            int startMonth = Int32.Parse(secondSplitStartDate[1]);
            int startDay = Int32.Parse(secondSplitStartDate[2]);
            int startHour = Int32.Parse(secondSplitStartHour[0]);
            int startminute = Int32.Parse(secondSplitStartHour[1]);
            int startSecond = Int32.Parse(secondSplitStartHour[2]);

            int endYear = Int32.Parse(secondSplitEndDate[0]);
            int endMonth = Int32.Parse(secondSplitEndDate[1]);
            int endDay = Int32.Parse(secondSplitEndDate[2]);
            int endHour = Int32.Parse(secondSplitEndHour[0]);
            int endMinute = Int32.Parse(secondSplitEndHour[1]);
            int endSecond = Int32.Parse(secondSplitEndHour[2]);

            DateTime start = new DateTime(startYear,startMonth,startDay,startHour,startminute,startSecond);
            DateTime end = new DateTime(endYear,endMonth,endDay,endHour,endMinute,endSecond);

            if (start>end)
            {
                correct = false;
            }

            return correct;
        }

        private void Label10_Click(object sender, EventArgs e)
        {

        }

        private void Label10_Click_1(object sender, EventArgs e)
        {

        }
    }
}


    // Bus
    // BusLocation
    // BusSimulationControl
    // DataProcessor
    // DBConnection
    // FileReader
    // Polygon
    // Program
    // Station
    // Stop
    // Zone
