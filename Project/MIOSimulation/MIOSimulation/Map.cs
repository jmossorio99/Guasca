using GMap.NET.WindowsForms;
using System;
using System.Windows.Forms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.MapProviders;
using GMap.NET;

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
            gmap.MapProvider = GoogleMapProvider.Instance;
            GMaps.Instance.Mode = AccessMode.ServerOnly;
            gmap.Position = new PointLatLng(3.442275, -76.515539);
            gmap.ShowCenter = false;
            gmap.Overlays.Add(markers);
            gmap.Overlays.Add(routes);
        }
    }
}
