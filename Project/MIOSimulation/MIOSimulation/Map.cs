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
            addAllStops();
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
            foreach (var item in stopData)
            {
                String[] tempSplit = item.Split(',');
                markers.Markers.Add(new GMarkerGoogle(new PointLatLng(Double.Parse(tempSplit[7]), Double.Parse(tempSplit[6])), GMarkerGoogleType.blue_small));
            }
        }
    }
}
