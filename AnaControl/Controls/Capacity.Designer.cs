namespace AnaControl
{
    public partial class Capacity
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint3 = new System.Windows.Forms.DataVisualization.Charting.DataPoint();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint4 = new System.Windows.Forms.DataVisualization.Charting.DataPoint();
            this.toolStripMenuItem_RemoveRepeats = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripToolbar = new System.Windows.Forms.ToolStrip();
            this.tsbSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripDropDownButton_ChartType = new System.Windows.Forms.ToolStripDropDownButton();
            this.ToolStripMenuItem_3d = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Save = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip_msChart = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.toolStripMenuItemFirstPassYieldRate = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripToolbar.SuspendLayout();
            this.contextMenuStrip_msChart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStripMenuItem_RemoveRepeats
            // 
            this.toolStripMenuItem_RemoveRepeats.Name = "toolStripMenuItem_RemoveRepeats";
            this.toolStripMenuItem_RemoveRepeats.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem_RemoveRepeats.Text = "去除重复项";
            this.toolStripMenuItem_RemoveRepeats.Click += new System.EventHandler(this.toolStripMenuItem_RemoveRepeats_Click);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_RemoveRepeats,
            this.toolStripMenuItemFirstPassYieldRate});
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(93, 22);
            this.toolStripDropDownButton1.Text = "查询参数设置";
            // 
            // toolStripToolbar
            // 
            this.toolStripToolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbSave,
            this.toolStripDropDownButton1,
            this.toolStripDropDownButton_ChartType});
            this.toolStripToolbar.Location = new System.Drawing.Point(0, 0);
            this.toolStripToolbar.Name = "toolStripToolbar";
            this.toolStripToolbar.Size = new System.Drawing.Size(364, 25);
            this.toolStripToolbar.TabIndex = 10;
            this.toolStripToolbar.Text = "toolStrip1";
            // 
            // tsbSave
            // 
            this.tsbSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSave.Image = global::AnaControl.Properties.Resources.save;
            this.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSave.Name = "tsbSave";
            this.tsbSave.Size = new System.Drawing.Size(23, 22);
            this.tsbSave.Click += new System.EventHandler(this.tsbSave_Click);
            // 
            // toolStripDropDownButton_ChartType
            // 
            this.toolStripDropDownButton_ChartType.Name = "toolStripDropDownButton_ChartType";
            this.toolStripDropDownButton_ChartType.Size = new System.Drawing.Size(69, 22);
            this.toolStripDropDownButton_ChartType.Text = "图表类型";
            // 
            // ToolStripMenuItem_3d
            // 
            this.ToolStripMenuItem_3d.Name = "ToolStripMenuItem_3d";
            this.ToolStripMenuItem_3d.Size = new System.Drawing.Size(103, 22);
            this.ToolStripMenuItem_3d.Text = "3D";
            // 
            // toolStripMenuItem_Save
            // 
            this.toolStripMenuItem_Save.Name = "toolStripMenuItem_Save";
            this.toolStripMenuItem_Save.Size = new System.Drawing.Size(103, 22);
            this.toolStripMenuItem_Save.Text = "Save";
            // 
            // contextMenuStrip_msChart
            // 
            this.contextMenuStrip_msChart.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_Save,
            this.ToolStripMenuItem_3d});
            this.contextMenuStrip_msChart.Name = "contextMenuStrip_msChart";
            this.contextMenuStrip_msChart.Size = new System.Drawing.Size(104, 48);
            this.contextMenuStrip_msChart.Text = "Save";
            // 
            // chart1
            // 
            this.chart1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            chartArea2.AlignmentOrientation = System.Windows.Forms.DataVisualization.Charting.AreaAlignmentOrientations.Horizontal;
            chartArea2.AxisX.Interval = 1D;
            chartArea2.AxisX.MajorGrid.Enabled = false;
            chartArea2.AxisX.MajorTickMark.Enabled = false;
            chartArea2.AxisX.ScaleBreakStyle.BreakLineStyle = System.Windows.Forms.DataVisualization.Charting.BreakLineStyle.Straight;
            chartArea2.AxisX.ScaleBreakStyle.Enabled = true;
            chartArea2.AxisX.ScaleBreakStyle.StartFromZero = System.Windows.Forms.DataVisualization.Charting.StartFromZero.Yes;
            chartArea2.AxisX2.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.False;
            chartArea2.AxisX2.MajorGrid.Enabled = false;
            chartArea2.AxisX2.MajorTickMark.Enabled = false;
            chartArea2.AxisY.MajorGrid.Enabled = false;
            chartArea2.AxisY2.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.False;
            chartArea2.AxisY2.MajorGrid.Enabled = false;
            chartArea2.AxisY2.MajorTickMark.Enabled = false;
            chartArea2.BackColor = System.Drawing.SystemColors.AppWorkspace;
            chartArea2.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea2);
            this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.Name = "Legend1";
            this.chart1.Legends.Add(legend2);
            this.chart1.Location = new System.Drawing.Point(0, 25);
            this.chart1.Name = "chart1";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            series2.Points.Add(dataPoint3);
            series2.Points.Add(dataPoint4);
            this.chart1.Series.Add(series2);
            this.chart1.Size = new System.Drawing.Size(364, 323);
            this.chart1.TabIndex = 0;
            // 
            // toolStripMenuItemFirstPassYieldRate
            // 
            this.toolStripMenuItemFirstPassYieldRate.Name = "toolStripMenuItemFirstPassYieldRate";
            this.toolStripMenuItemFirstPassYieldRate.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItemFirstPassYieldRate.Text = "一次通过率";
            this.toolStripMenuItemFirstPassYieldRate.Click += new System.EventHandler(this.toolStripMenuItemFirstPassYieldRate_Click);
            // 
            // Capacity
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.toolStripToolbar);
            this.Name = "Capacity";
            this.Size = new System.Drawing.Size(364, 348);
            this.Load += new System.EventHandler(this.Capacity_Load);
            this.toolStripToolbar.ResumeLayout(false);
            this.toolStripToolbar.PerformLayout();
            this.contextMenuStrip_msChart.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_RemoveRepeats;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStrip toolStripToolbar;
        private System.Windows.Forms.ToolStripButton tsbSave;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_3d;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Save;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_msChart;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton_ChartType;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFirstPassYieldRate;
    }
}
