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

        public Map()
        {
            InitializeComponent();
        }

        private void Map_Load(object sender, EventArgs e)
        {
            //setting up the map
            markers.Markers.Add(new GMarkerGoogle(new PointLatLng(3.44327333, -76.54844833), GMarkerGoogleType.lightblue_pushpin));
            markers.Markers.Add(new GMarkerGoogle(new PointLatLng(3.440465, -76.54748), GMarkerGoogleType.lightblue_pushpin));
            //addAllStops();
            gmap.MapProvider = GoogleMapProvider.Instance;
            GMaps.Instance.Mode = AccessMode.ServerOnly;
            gmap.Position = new PointLatLng(3.442275, -76.515539);
            gmap.ShowCenter = false;
            gmap.Overlays.Add(markers);
            gmap.Overlays.Add(routes);
            
        }

        private void addAllStops()
        {
            FileReader fr = new FileReader("stops.csv");
            List<String> stopData = fr.readFile();
            List<Double> coords = new List<Double>();
            foreach (var item in stopData)
            {
                String[] tempSplit = item.Split(',');
                coords.Add(Double.Parse(tempSplit[6]));
                coords.Add(Double.Parse(tempSplit[5]));
            }
            for (int i = 0; i < coords.Count; i += 2)
            {
                markers.Markers.Add(new GMarkerGoogle(new PointLatLng(coords[i], coords[i + 1]), GMarkerGoogleType.lightblue_pushpin));
            }
        }
    }
}
