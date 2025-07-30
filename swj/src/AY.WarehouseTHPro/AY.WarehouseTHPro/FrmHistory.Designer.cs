namespace AY.WarehouseTHPro
{
    partial class FrmHistory
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
            SeeSharpTools.JY.GUI.StripChartXSeries stripChartXSeries1 = new SeeSharpTools.JY.GUI.StripChartXSeries();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.button1 = new System.Windows.Forms.Button();
            this.chartTrend = new SeeSharpTools.JY.GUI.StripChartX();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.button1);
            this.splitContainer1.Panel1.Controls.Add(this.dateTimePicker2);
            this.splitContainer1.Panel1.Controls.Add(this.dateTimePicker1);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.btnCancel);
            this.splitContainer1.Panel1.Controls.Add(this.btnOk);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.chartTrend);
            this.splitContainer1.Size = new System.Drawing.Size(1089, 587);
            this.splitContainer1.SplitterDistance = 91;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.BackgroundImage = global::AY.WarehouseTHPro.Properties.Resources.Pink;
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(959, 26);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(84, 39);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "导出";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.BackgroundImage = global::AY.WarehouseTHPro.Properties.Resources.Green;
            this.btnOk.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOk.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnOk.FlatAppearance.BorderSize = 0;
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.Location = new System.Drawing.Point(46, 26);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(116, 39);
            this.btnOk.TabIndex = 6;
            this.btnOk.Text = "变量选择";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(188, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 20);
            this.label1.TabIndex = 7;
            this.label1.Text = "开始时间：";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "yyyy-MM-dd HHJ:mm:ss";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(274, 34);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 26);
            this.dateTimePicker1.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(519, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "结束时间：";
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.CustomFormat = "yyyy-MM-dd HHJ:mm:ss";
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker2.Location = new System.Drawing.Point(605, 33);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(200, 26);
            this.dateTimePicker2.TabIndex = 8;
            // 
            // button1
            // 
            this.button1.BackgroundImage = global::AY.WarehouseTHPro.Properties.Resources.btnbg01;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(842, 26);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(85, 39);
            this.button1.TabIndex = 9;
            this.button1.Text = "查询";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // chartTrend
            // 
            this.chartTrend.AxisX.AutoScale = false;
            this.chartTrend.AxisX.AutoZoomReset = false;
            this.chartTrend.AxisX.Color = System.Drawing.Color.White;
            this.chartTrend.AxisX.InitWithScaleView = false;
            this.chartTrend.AxisX.IsLogarithmic = false;
            this.chartTrend.AxisX.LabelAngle = 0;
            this.chartTrend.AxisX.LabelEnabled = true;
            this.chartTrend.AxisX.LabelFormat = null;
            this.chartTrend.AxisX.MajorGridColor = System.Drawing.Color.White;
            this.chartTrend.AxisX.MajorGridCount = 6;
            this.chartTrend.AxisX.MajorGridEnabled = true;
            this.chartTrend.AxisX.MajorGridType = SeeSharpTools.JY.GUI.StripChartXAxis.GridStyle.Dash;
            this.chartTrend.AxisX.Maximum = 1000D;
            this.chartTrend.AxisX.Minimum = 0D;
            this.chartTrend.AxisX.MinorGridColor = System.Drawing.Color.Black;
            this.chartTrend.AxisX.MinorGridEnabled = false;
            this.chartTrend.AxisX.MinorGridType = SeeSharpTools.JY.GUI.StripChartXAxis.GridStyle.DashDot;
            this.chartTrend.AxisX.TickWidth = 1F;
            this.chartTrend.AxisX.Title = "";
            this.chartTrend.AxisX.TitleOrientation = SeeSharpTools.JY.GUI.StripChartXAxis.AxisTextOrientation.Auto;
            this.chartTrend.AxisX.TitlePosition = SeeSharpTools.JY.GUI.StripChartXAxis.AxisTextPosition.Center;
            this.chartTrend.AxisX.ViewMaximum = 1000D;
            this.chartTrend.AxisX.ViewMinimum = 0D;
            this.chartTrend.AxisX2.AutoScale = false;
            this.chartTrend.AxisX2.AutoZoomReset = false;
            this.chartTrend.AxisX2.Color = System.Drawing.Color.Black;
            this.chartTrend.AxisX2.InitWithScaleView = false;
            this.chartTrend.AxisX2.IsLogarithmic = false;
            this.chartTrend.AxisX2.LabelAngle = 0;
            this.chartTrend.AxisX2.LabelEnabled = true;
            this.chartTrend.AxisX2.LabelFormat = null;
            this.chartTrend.AxisX2.MajorGridColor = System.Drawing.Color.Black;
            this.chartTrend.AxisX2.MajorGridCount = 6;
            this.chartTrend.AxisX2.MajorGridEnabled = true;
            this.chartTrend.AxisX2.MajorGridType = SeeSharpTools.JY.GUI.StripChartXAxis.GridStyle.Dash;
            this.chartTrend.AxisX2.Maximum = 1000D;
            this.chartTrend.AxisX2.Minimum = 0D;
            this.chartTrend.AxisX2.MinorGridColor = System.Drawing.Color.Black;
            this.chartTrend.AxisX2.MinorGridEnabled = false;
            this.chartTrend.AxisX2.MinorGridType = SeeSharpTools.JY.GUI.StripChartXAxis.GridStyle.DashDot;
            this.chartTrend.AxisX2.TickWidth = 1F;
            this.chartTrend.AxisX2.Title = "";
            this.chartTrend.AxisX2.TitleOrientation = SeeSharpTools.JY.GUI.StripChartXAxis.AxisTextOrientation.Auto;
            this.chartTrend.AxisX2.TitlePosition = SeeSharpTools.JY.GUI.StripChartXAxis.AxisTextPosition.Center;
            this.chartTrend.AxisX2.ViewMaximum = 1000D;
            this.chartTrend.AxisX2.ViewMinimum = 0D;
            this.chartTrend.AxisY.AutoScale = true;
            this.chartTrend.AxisY.AutoZoomReset = false;
            this.chartTrend.AxisY.Color = System.Drawing.Color.White;
            this.chartTrend.AxisY.InitWithScaleView = false;
            this.chartTrend.AxisY.IsLogarithmic = false;
            this.chartTrend.AxisY.LabelAngle = 0;
            this.chartTrend.AxisY.LabelEnabled = true;
            this.chartTrend.AxisY.LabelFormat = null;
            this.chartTrend.AxisY.MajorGridColor = System.Drawing.Color.White;
            this.chartTrend.AxisY.MajorGridCount = 10;
            this.chartTrend.AxisY.MajorGridEnabled = true;
            this.chartTrend.AxisY.MajorGridType = SeeSharpTools.JY.GUI.StripChartXAxis.GridStyle.Dash;
            this.chartTrend.AxisY.Maximum = 100D;
            this.chartTrend.AxisY.Minimum = 0D;
            this.chartTrend.AxisY.MinorGridColor = System.Drawing.Color.Black;
            this.chartTrend.AxisY.MinorGridEnabled = false;
            this.chartTrend.AxisY.MinorGridType = SeeSharpTools.JY.GUI.StripChartXAxis.GridStyle.DashDot;
            this.chartTrend.AxisY.TickWidth = 1F;
            this.chartTrend.AxisY.Title = "";
            this.chartTrend.AxisY.TitleOrientation = SeeSharpTools.JY.GUI.StripChartXAxis.AxisTextOrientation.Auto;
            this.chartTrend.AxisY.TitlePosition = SeeSharpTools.JY.GUI.StripChartXAxis.AxisTextPosition.Center;
            this.chartTrend.AxisY.ViewMaximum = 3.5D;
            this.chartTrend.AxisY.ViewMinimum = 0.5D;
            this.chartTrend.AxisY2.AutoScale = true;
            this.chartTrend.AxisY2.AutoZoomReset = false;
            this.chartTrend.AxisY2.Color = System.Drawing.Color.Black;
            this.chartTrend.AxisY2.InitWithScaleView = false;
            this.chartTrend.AxisY2.IsLogarithmic = false;
            this.chartTrend.AxisY2.LabelAngle = 0;
            this.chartTrend.AxisY2.LabelEnabled = true;
            this.chartTrend.AxisY2.LabelFormat = null;
            this.chartTrend.AxisY2.MajorGridColor = System.Drawing.Color.Black;
            this.chartTrend.AxisY2.MajorGridCount = 6;
            this.chartTrend.AxisY2.MajorGridEnabled = true;
            this.chartTrend.AxisY2.MajorGridType = SeeSharpTools.JY.GUI.StripChartXAxis.GridStyle.Dash;
            this.chartTrend.AxisY2.Maximum = 3.5D;
            this.chartTrend.AxisY2.Minimum = 0.5D;
            this.chartTrend.AxisY2.MinorGridColor = System.Drawing.Color.Black;
            this.chartTrend.AxisY2.MinorGridEnabled = false;
            this.chartTrend.AxisY2.MinorGridType = SeeSharpTools.JY.GUI.StripChartXAxis.GridStyle.DashDot;
            this.chartTrend.AxisY2.TickWidth = 1F;
            this.chartTrend.AxisY2.Title = "";
            this.chartTrend.AxisY2.TitleOrientation = SeeSharpTools.JY.GUI.StripChartXAxis.AxisTextOrientation.Auto;
            this.chartTrend.AxisY2.TitlePosition = SeeSharpTools.JY.GUI.StripChartXAxis.AxisTextPosition.Center;
            this.chartTrend.AxisY2.ViewMaximum = 3.5D;
            this.chartTrend.AxisY2.ViewMinimum = 0.5D;
            this.chartTrend.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.chartTrend.ChartAreaBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.chartTrend.Direction = SeeSharpTools.JY.GUI.StripChartX.ScrollDirection.LeftToRight;
            this.chartTrend.DisplayPoints = 4000;
            this.chartTrend.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.chartTrend.ForeColor = System.Drawing.Color.White;
            this.chartTrend.GradientStyle = SeeSharpTools.JY.GUI.StripChartX.ChartGradientStyle.None;
            this.chartTrend.LegendBackColor = System.Drawing.Color.Transparent;
            this.chartTrend.LegendFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chartTrend.LegendForeColor = System.Drawing.Color.White;
            this.chartTrend.LegendVisible = true;
            stripChartXSeries1.Color = System.Drawing.Color.Red;
            stripChartXSeries1.Marker = SeeSharpTools.JY.GUI.StripChartXSeries.MarkerType.None;
            stripChartXSeries1.Name = "Series1";
            stripChartXSeries1.Type = SeeSharpTools.JY.GUI.StripChartXSeries.LineType.FastLine;
            stripChartXSeries1.Visible = true;
            stripChartXSeries1.Width = SeeSharpTools.JY.GUI.StripChartXSeries.LineWidth.Thin;
            stripChartXSeries1.XPlotAxis = SeeSharpTools.JY.GUI.StripChartXAxis.PlotAxis.Primary;
            stripChartXSeries1.YPlotAxis = SeeSharpTools.JY.GUI.StripChartXAxis.PlotAxis.Primary;
            this.chartTrend.LineSeries.Add(stripChartXSeries1);
            this.chartTrend.Location = new System.Drawing.Point(13, 5);
            this.chartTrend.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chartTrend.Miscellaneous.CheckInfinity = false;
            this.chartTrend.Miscellaneous.CheckNaN = false;
            this.chartTrend.Miscellaneous.CheckNegtiveOrZero = false;
            this.chartTrend.Miscellaneous.DirectionChartCount = 3;
            this.chartTrend.Miscellaneous.Fitting = SeeSharpTools.JY.GUI.StripChartX.FitType.Range;
            this.chartTrend.Miscellaneous.MaxSeriesCount = 32;
            this.chartTrend.Miscellaneous.MaxSeriesPointCount = 4000;
            this.chartTrend.Miscellaneous.SplitLayoutColumnInterval = 0F;
            this.chartTrend.Miscellaneous.SplitLayoutDirection = SeeSharpTools.JY.GUI.StripChartXUtility.LayoutDirection.LeftToRight;
            this.chartTrend.Miscellaneous.SplitLayoutRowInterval = 0F;
            this.chartTrend.Miscellaneous.SplitViewAutoLayout = true;
            this.chartTrend.Name = "chartTrend";
            this.chartTrend.NextTimeStamp = new System.DateTime(((long)(0)));
            this.chartTrend.ScrollType = SeeSharpTools.JY.GUI.StripChartX.StripScrollType.Cumulation;
            this.chartTrend.SeriesCount = 1;
            this.chartTrend.Size = new System.Drawing.Size(1063, 479);
            this.chartTrend.SplitView = false;
            this.chartTrend.StartIndex = 0;
            this.chartTrend.TabIndex = 1;
            this.chartTrend.TimeInterval = System.TimeSpan.Parse("00:00:00");
            this.chartTrend.TimeStampFormat = null;
            this.chartTrend.XCursor.AutoInterval = true;
            this.chartTrend.XCursor.Color = System.Drawing.Color.DeepSkyBlue;
            this.chartTrend.XCursor.Interval = 0.001D;
            this.chartTrend.XCursor.Mode = SeeSharpTools.JY.GUI.StripChartXCursor.CursorMode.Zoom;
            this.chartTrend.XCursor.SelectionColor = System.Drawing.Color.LightGray;
            this.chartTrend.XCursor.Value = double.NaN;
            this.chartTrend.XDataType = SeeSharpTools.JY.GUI.StripChartX.XAxisDataType.Index;
            this.chartTrend.YCursor.AutoInterval = true;
            this.chartTrend.YCursor.Color = System.Drawing.Color.DeepSkyBlue;
            this.chartTrend.YCursor.Interval = 0.001D;
            this.chartTrend.YCursor.Mode = SeeSharpTools.JY.GUI.StripChartXCursor.CursorMode.Disabled;
            this.chartTrend.YCursor.SelectionColor = System.Drawing.Color.LightGray;
            this.chartTrend.YCursor.Value = double.NaN;
            // 
            // FrmHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.ClientSize = new System.Drawing.Size(1089, 587);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FrmHistory";
            this.Text = "历史趋势";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Label label2;
        private SeeSharpTools.JY.GUI.StripChartX chartTrend;
    }
}