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
        private List<Zone> zones;
        private List<GMapOverlay> zonesList = new List<GMapOverlay>();

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

        private void addZones()
        {

            FileReader zonesReader = new FileReader("CoordenatesPolygons.txt");
            List<String> zonesData = zonesReader.readFile();
            List<int> toSee = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
            foreach (var i in toSee)
            {
                String elem = zonesData[i];
                Polygon example = new Polygon(elem, "Zone " + i);
                Zone newZone = new Zone(elem, i, example);
                zones.Add(newZone);
                GMapPolygon polygonToAdd = new GMapPolygon(newZone.Area.getPolygon(),"Zone" +i);
                polygonToAdd.Fill = new SolidBrush(Color.Transparent);
                polygonToAdd.Stroke = new Pen(Color.Red, 1);

                (zonesList[i]).Polygons.Add(polygonToAdd);
                //pZones.Add(example);
                zones.Add(newZone);
            }

        }

        private Boolean CorrectFormat(String line)
        {
            Boolean correct = false;

            String[] firstSplit = line.Split(' ');
            if (firstSplit.Length==2)
            {

                String[] secondSplit = firstSplit[0].Split('-');
                String[] thirdSplit = firstSplit[1].Split(':');

                if (secondSplit.Length==3 && thirdSplit.Length==3)
                {

                    try
                    {

                        int temp1 = Int32.Parse(secondSplit[0]);
                        int temp2 = Int32.Parse(secondSplit[1]);
                        int temp3 = Int32.Parse(secondSplit[2]);
                        int temp4 = Int32.Parse(thirdSplit[0]);
                        int temp5 = Int32.Parse(thirdSplit[1]);
                        int temp6 = Int32.Parse(thirdSplit[2]);

                        correct = true;
                    }
                    catch (Exception e)
                    {
                        
                    }

                }
            }

            return correct;
        }
    }
}
