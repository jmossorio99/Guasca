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

namespace MIOSimulation
{
    public partial class MapApp : Form
    {

        private List<int> zonesChecked;

        public MapApp()
        {
            InitializeComponent();
            toShowChoiceBox.Items.Add("Estaciones y paradas");
            toShowChoiceBox.Items.Add("Estaciones");
            toShowChoiceBox.Items.Add("Paradas");
            toShowChoiceBox.DropDownStyle = ComboBoxStyle.DropDownList;

            zonesChecked = new List<int>();
        }

        private void mapLoad(object sender, EventArgs e)
        {
            gMap.MapProvider = GoogleMapProvider.Instance;
            GMaps.Instance.Mode = AccessMode.ServerOnly;
            gMap.Position = new PointLatLng(3.436440, -76.515270);
            gMap.ShowCenter = false;
            gMap.Zoom = 13;
            zoomBar.Value = Convert.ToInt32(gMap.Zoom);
        }

        private void ZoomInBtn_Click(object sender, EventArgs e)
        {
            gMap.Zoom += 1;
            zoomBar.Value = Convert.ToInt32(gMap.Zoom);
        }

        private void ZoomOutBtn_Click(object sender, EventArgs e)
        {
            gMap.Zoom -= 1;
            zoomBar.Value = Convert.ToInt32(gMap.Zoom);
        }

        private void ZoomBar_ValueChanged(object sender, EventArgs e)
        {
            gMap.Zoom = zoomBar.Value;
        }
    }
}
