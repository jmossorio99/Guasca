namespace MIOSimulation
{
    partial class Map
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Map));
            this.gmap = new GMap.NET.WindowsForms.GMapControl();
            this.StationStop_CB = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.zoom = new System.Windows.Forms.Button();
            this.zoomplus = new System.Windows.Forms.Button();
            this.slower = new System.Windows.Forms.Button();
            this.faster = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.goSimulation = new System.Windows.Forms.Button();
            this.prueba = new System.Windows.Forms.Label();
            this.startSimulation = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            //
            // gmap
            //
            this.gmap.Bearing = 0F;
            this.gmap.CanDragMap = true;
            this.gmap.EmptyTileColor = System.Drawing.Color.Navy;
            this.gmap.GrayScaleMode = false;
            this.gmap.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.gmap.LevelsKeepInMemmory = 5;
            this.gmap.Location = new System.Drawing.Point(1, -2);
            this.gmap.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gmap.MarkersEnabled = true;
            this.gmap.MaxZoom = 18;
            this.gmap.MinZoom = 0;
            this.gmap.MouseWheelZoomEnabled = true;
            this.gmap.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.gmap.Name = "gmap";
            this.gmap.NegativeMode = false;
            this.gmap.PolygonsEnabled = true;
            this.gmap.RetryLoadTile = 0;
            this.gmap.RoutesEnabled = true;
            this.gmap.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.gmap.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.gmap.ShowTileGridLines = false;
            this.gmap.Size = new System.Drawing.Size(665, 402);
            this.gmap.TabIndex = 0;
            this.gmap.Zoom = 14D;
            //
            // StationStop_CB
            //
            this.StationStop_CB.FormattingEnabled = true;
            this.StationStop_CB.Location = new System.Drawing.Point(56, 57);
            this.StationStop_CB.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.StationStop_CB.Name = "StationStop_CB";
            this.StationStop_CB.Size = new System.Drawing.Size(98, 21);
            this.StationStop_CB.TabIndex = 1;
            this.StationStop_CB.Text = "Estaciones y paradas";
            //
            // label1
            //
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Palatino Linotype", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(86, 38);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 31);
            this.label1.TabIndex = 2;
            this.label1.Text = "Filtro";
            //
            // panel1
            //
            this.panel1.BackColor = System.Drawing.Color.SteelBlue;
            this.panel1.Controls.Add(this.zoom);
            this.panel1.Controls.Add(this.zoomplus);
            this.panel1.Controls.Add(this.slower);
            this.panel1.Controls.Add(this.faster);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.goSimulation);
            this.panel1.Controls.Add(this.prueba);
            this.panel1.Controls.Add(this.startSimulation);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.StationStop_CB);
            this.panel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.panel1.Location = new System.Drawing.Point(1005, -2);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(294, 619);
            this.panel1.TabIndex = 3;
            //
            // zoom
            //
            this.zoom.ForeColor = System.Drawing.Color.Black;
            this.zoom.Location = new System.Drawing.Point(146, 285);
            this.zoom.Name = "zoom";
            this.zoom.Size = new System.Drawing.Size(79, 31);
            this.zoom.TabIndex = 10;
            this.zoom.Text = "- Zoom";
            this.zoom.UseVisualStyleBackColor = true;
            this.zoom.Click += new System.EventHandler(this.Zoom_Click);
            //
            // zoomplus
            //
            this.zoomplus.ForeColor = System.Drawing.Color.Black;
            this.zoomplus.Location = new System.Drawing.Point(145, 246);
            this.zoomplus.Name = "zoomplus";
            this.zoomplus.Size = new System.Drawing.Size(80, 31);
            this.zoomplus.TabIndex = 9;
            this.zoomplus.Text = "+ Zoom";
            this.zoomplus.UseVisualStyleBackColor = true;
            this.zoomplus.Click += new System.EventHandler(this.Zoomplus_Click);
            //
            // slower
            //
            this.slower.ForeColor = System.Drawing.Color.Black;
            this.slower.Location = new System.Drawing.Point(231, 283);
            this.slower.Name = "slower";
            this.slower.Size = new System.Drawing.Size(29, 35);
            this.slower.TabIndex = 8;
            this.slower.Text = "-";
            this.slower.UseVisualStyleBackColor = true;
            this.slower.Click += new System.EventHandler(this.Slower_Click);
            //
            // faster
            //
            this.faster.ForeColor = System.Drawing.Color.Black;
            this.faster.Location = new System.Drawing.Point(231, 242);
            this.faster.Name = "faster";
            this.faster.Size = new System.Drawing.Size(29, 35);
            this.faster.TabIndex = 7;
            this.faster.Text = "+";
            this.faster.UseVisualStyleBackColor = true;
            this.faster.Click += new System.EventHandler(this.Faster_Click);
            //
            // button2
            //
            this.button2.ForeColor = System.Drawing.Color.Black;
            this.button2.Location = new System.Drawing.Point(46, 285);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(94, 30);
            this.button2.TabIndex = 6;
            this.button2.Text = "Pausar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            //
            // goSimulation
            //
            this.goSimulation.ForeColor = System.Drawing.Color.Black;
            this.goSimulation.Location = new System.Drawing.Point(46, 247);
            this.goSimulation.Name = "goSimulation";
            this.goSimulation.Size = new System.Drawing.Size(93, 30);
            this.goSimulation.TabIndex = 5;
            this.goSimulation.Text = "Reanudar";
            this.goSimulation.UseVisualStyleBackColor = true;
            this.goSimulation.Click += new System.EventHandler(this.GoSimulation_Click);
            //
            // prueba
            //
            this.prueba.AutoSize = true;
            this.prueba.Location = new System.Drawing.Point(71, 201);
            this.prueba.Name = "prueba";
            this.prueba.Size = new System.Drawing.Size(163, 20);
            this.prueba.TabIndex = 4;
            this.prueba.Text = "Informacion de la ruta";
            //
            // startSimulation
            //
            this.startSimulation.ForeColor = System.Drawing.Color.Black;
            this.startSimulation.Location = new System.Drawing.Point(110, 148);
            this.startSimulation.Name = "startSimulation";
            this.startSimulation.Size = new System.Drawing.Size(80, 30);
            this.startSimulation.TabIndex = 3;
            this.startSimulation.Text = "Simular movimiento";
            this.startSimulation.UseVisualStyleBackColor = true;
            this.startSimulation.Click += new System.EventHandler(this.StartSimulation_Click);
            //
            // timer1
            //
            this.timer1.Interval = 300;
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            //
            // Map
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1298, 616);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.gmap);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Map";
            this.Text = "Map";
            this.Load += new System.EventHandler(this.Map_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private GMap.NET.WindowsForms.GMapControl gmap;
        private System.Windows.Forms.ComboBox StationStop_CB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button startSimulation;
        private System.Windows.Forms.Label prueba;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button goSimulation;
        private System.Windows.Forms.Button slower;
        private System.Windows.Forms.Button faster;
        private System.Windows.Forms.Button zoom;
        private System.Windows.Forms.Button zoomplus;
    }
}
