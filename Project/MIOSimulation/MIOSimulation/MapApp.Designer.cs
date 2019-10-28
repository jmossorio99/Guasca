namespace MIOSimulation
{
    partial class MapApp
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
            this.gMap = new GMap.NET.WindowsForms.GMapControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.mostrarLbl = new System.Windows.Forms.Label();
            this.toShowChoiceBox = new System.Windows.Forms.ComboBox();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.zoomOutBtn = new System.Windows.Forms.Button();
            this.zoomInBtn = new System.Windows.Forms.Button();
            this.zoomLbl = new System.Windows.Forms.Label();
            this.zoomBar = new System.Windows.Forms.TrackBar();
            this.simulacionLbl = new System.Windows.Forms.Label();
            this.horaInicioLbl = new System.Windows.Forms.Label();
            this.horaInicioTxt = new System.Windows.Forms.TextBox();
            this.horaFinLbl = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.horaFinTxt = new System.Windows.Forms.TextBox();
            this.startSimBtn = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.zoomBar)).BeginInit();
            this.SuspendLayout();
            // 
            // gMap
            // 
            this.gMap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gMap.Bearing = 0F;
            this.gMap.CanDragMap = true;
            this.gMap.EmptyTileColor = System.Drawing.Color.Navy;
            this.gMap.GrayScaleMode = false;
            this.gMap.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.gMap.LevelsKeepInMemmory = 5;
            this.gMap.Location = new System.Drawing.Point(0, 0);
            this.gMap.MarkersEnabled = true;
            this.gMap.MaxZoom = 18;
            this.gMap.MinZoom = 10;
            this.gMap.MouseWheelZoomEnabled = true;
            this.gMap.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.gMap.Name = "gMap";
            this.gMap.NegativeMode = false;
            this.gMap.PolygonsEnabled = true;
            this.gMap.RetryLoadTile = 0;
            this.gMap.RoutesEnabled = true;
            this.gMap.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.gMap.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.gMap.ShowTileGridLines = false;
            this.gMap.Size = new System.Drawing.Size(1078, 647);
            this.gMap.TabIndex = 0;
            this.gMap.Zoom = 0D;
            this.gMap.Load += new System.EventHandler(this.mapLoad);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Salmon;
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.startSimBtn);
            this.panel1.Controls.Add(this.horaFinTxt);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.horaFinLbl);
            this.panel1.Controls.Add(this.horaInicioTxt);
            this.panel1.Controls.Add(this.horaInicioLbl);
            this.panel1.Controls.Add(this.simulacionLbl);
            this.panel1.Controls.Add(this.zoomBar);
            this.panel1.Controls.Add(this.zoomLbl);
            this.panel1.Controls.Add(this.zoomInBtn);
            this.panel1.Controls.Add(this.zoomOutBtn);
            this.panel1.Controls.Add(this.checkedListBox1);
            this.panel1.Controls.Add(this.toShowChoiceBox);
            this.panel1.Controls.Add(this.mostrarLbl);
            this.panel1.Location = new System.Drawing.Point(1084, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(328, 647);
            this.panel1.TabIndex = 1;
            // 
            // mostrarLbl
            // 
            this.mostrarLbl.AutoSize = true;
            this.mostrarLbl.Font = new System.Drawing.Font("Nirmala UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mostrarLbl.Location = new System.Drawing.Point(131, 9);
            this.mostrarLbl.Name = "mostrarLbl";
            this.mostrarLbl.Size = new System.Drawing.Size(65, 20);
            this.mostrarLbl.TabIndex = 2;
            this.mostrarLbl.Text = "Mostrar";
            // 
            // toShowChoiceBox
            // 
            this.toShowChoiceBox.FormattingEnabled = true;
            this.toShowChoiceBox.Location = new System.Drawing.Point(57, 34);
            this.toShowChoiceBox.Name = "toShowChoiceBox";
            this.toShowChoiceBox.Size = new System.Drawing.Size(217, 21);
            this.toShowChoiceBox.TabIndex = 3;
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Items.AddRange(new object[] {
            "Zone 0 - Centro",
            "Zone 1 - Universidades",
            "Zone 2 - Menga",
            "Zone 3 - Paso del Comercio",
            "Zone 4 - Sanín",
            "Zone 5 - Nuevo Latir",
            "Zone 6 - Simón Bolivar",
            "Zone 7 - Cañaveralejo",
            "Zone 8 - Calipso"});
            this.checkedListBox1.Location = new System.Drawing.Point(57, 58);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(217, 139);
            this.checkedListBox1.TabIndex = 4;
            // 
            // zoomOutBtn
            // 
            this.zoomOutBtn.Location = new System.Drawing.Point(25, 590);
            this.zoomOutBtn.Name = "zoomOutBtn";
            this.zoomOutBtn.Size = new System.Drawing.Size(27, 23);
            this.zoomOutBtn.TabIndex = 5;
            this.zoomOutBtn.Text = "-";
            this.zoomOutBtn.UseVisualStyleBackColor = true;
            this.zoomOutBtn.Click += new System.EventHandler(this.ZoomOutBtn_Click);
            // 
            // zoomInBtn
            // 
            this.zoomInBtn.Location = new System.Drawing.Point(282, 591);
            this.zoomInBtn.Name = "zoomInBtn";
            this.zoomInBtn.Size = new System.Drawing.Size(27, 23);
            this.zoomInBtn.TabIndex = 6;
            this.zoomInBtn.Text = "+";
            this.zoomInBtn.UseVisualStyleBackColor = true;
            this.zoomInBtn.Click += new System.EventHandler(this.ZoomInBtn_Click);
            // 
            // zoomLbl
            // 
            this.zoomLbl.AutoSize = true;
            this.zoomLbl.Font = new System.Drawing.Font("Nirmala UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.zoomLbl.Location = new System.Drawing.Point(143, 567);
            this.zoomLbl.Name = "zoomLbl";
            this.zoomLbl.Size = new System.Drawing.Size(50, 20);
            this.zoomLbl.TabIndex = 7;
            this.zoomLbl.Text = "Zoom";
            // 
            // zoomBar
            // 
            this.zoomBar.LargeChange = 3;
            this.zoomBar.Location = new System.Drawing.Point(58, 590);
            this.zoomBar.Maximum = 18;
            this.zoomBar.Minimum = 10;
            this.zoomBar.Name = "zoomBar";
            this.zoomBar.Size = new System.Drawing.Size(216, 45);
            this.zoomBar.TabIndex = 11;
            this.zoomBar.Value = 13;
            this.zoomBar.ValueChanged += new System.EventHandler(this.ZoomBar_ValueChanged);
            // 
            // simulacionLbl
            // 
            this.simulacionLbl.AutoSize = true;
            this.simulacionLbl.Font = new System.Drawing.Font("Nirmala UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simulacionLbl.Location = new System.Drawing.Point(124, 210);
            this.simulacionLbl.Name = "simulacionLbl";
            this.simulacionLbl.Size = new System.Drawing.Size(85, 20);
            this.simulacionLbl.TabIndex = 13;
            this.simulacionLbl.Text = "Simulacion";
            // 
            // horaInicioLbl
            // 
            this.horaInicioLbl.AutoSize = true;
            this.horaInicioLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.horaInicioLbl.Location = new System.Drawing.Point(31, 249);
            this.horaInicioLbl.Name = "horaInicioLbl";
            this.horaInicioLbl.Size = new System.Drawing.Size(72, 16);
            this.horaInicioLbl.TabIndex = 14;
            this.horaInicioLbl.Text = "Hora inicio";
            // 
            // horaInicioTxt
            // 
            this.horaInicioTxt.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.horaInicioTxt.Location = new System.Drawing.Point(29, 267);
            this.horaInicioTxt.MaxLength = 5;
            this.horaInicioTxt.Name = "horaInicioTxt";
            this.horaInicioTxt.Size = new System.Drawing.Size(76, 25);
            this.horaInicioTxt.TabIndex = 15;
            // 
            // horaFinLbl
            // 
            this.horaFinLbl.AutoSize = true;
            this.horaFinLbl.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.horaFinLbl.Location = new System.Drawing.Point(138, 245);
            this.horaFinLbl.Name = "horaFinLbl";
            this.horaFinLbl.Size = new System.Drawing.Size(55, 17);
            this.horaFinLbl.TabIndex = 16;
            this.horaFinLbl.Text = "Hora fin";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(142, 232);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "(hh:mm)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(45, 235);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "(hh:mm)";
            // 
            // horaFinTxt
            // 
            this.horaFinTxt.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.horaFinTxt.Location = new System.Drawing.Point(129, 267);
            this.horaFinTxt.Name = "horaFinTxt";
            this.horaFinTxt.Size = new System.Drawing.Size(72, 25);
            this.horaFinTxt.TabIndex = 19;
            // 
            // startSimBtn
            // 
            this.startSimBtn.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startSimBtn.Location = new System.Drawing.Point(222, 267);
            this.startSimBtn.Name = "startSimBtn";
            this.startSimBtn.Size = new System.Drawing.Size(75, 25);
            this.startSimBtn.TabIndex = 20;
            this.startSimBtn.Text = "Iniciar";
            this.startSimBtn.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(282, 382);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(27, 23);
            this.button1.TabIndex = 21;
            this.button1.Text = "+";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(25, 382);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(27, 23);
            this.button2.TabIndex = 22;
            this.button2.Text = "-";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Nirmala UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(93, 385);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(155, 20);
            this.label3.TabIndex = 23;
            this.label3.Text = "Velocidad simulacion";
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Nirmala UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(77, 330);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(76, 26);
            this.button3.TabIndex = 24;
            this.button3.Text = "Pausar";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("Nirmala UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.Location = new System.Drawing.Point(173, 330);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(86, 26);
            this.button4.TabIndex = 25;
            this.button4.Text = "Reanudar";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // MapApp
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(1412, 648);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.gMap);
            this.Name = "MapApp";
            this.Text = "SimulacionMetroCali";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.zoomBar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private GMap.NET.WindowsForms.GMapControl gMap;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox toShowChoiceBox;
        private System.Windows.Forms.Label mostrarLbl;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.Button zoomInBtn;
        private System.Windows.Forms.Button zoomOutBtn;
        private System.Windows.Forms.Label zoomLbl;
        private System.Windows.Forms.TrackBar zoomBar;
        private System.Windows.Forms.Label simulacionLbl;
        private System.Windows.Forms.Label horaInicioLbl;
        private System.Windows.Forms.TextBox horaInicioTxt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label horaFinLbl;
        private System.Windows.Forms.TextBox horaFinTxt;
        private System.Windows.Forms.Button startSimBtn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
    }
}