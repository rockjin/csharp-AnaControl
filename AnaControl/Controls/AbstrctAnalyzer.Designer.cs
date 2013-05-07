namespace AnaControl.Controls
{
    partial class AbstrctAnalyzer
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbDrawChart = new System.Windows.Forms.ToolStripButton();
            this.tsbSaveChart = new System.Windows.Forms.ToolStripButton();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.contextMenuStrip_msChart = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem_Save = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_3d = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemChartTypes = new System.Windows.Forms.ToolStripMenuItem();
            this.TsbShowValueLabel = new System.Windows.Forms.ToolStripMenuItem();
            this.miSaveData = new System.Windows.Forms.ToolStripMenuItem();
            this.miCopyData = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.contextMenuStrip_msChart.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbDrawChart,
            this.tsbSaveChart});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(492, 39);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbDrawChart
            // 
            this.tsbDrawChart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbDrawChart.Image = global::AnaControl.Properties.Resources.refresh;
            this.tsbDrawChart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDrawChart.Name = "tsbDrawChart";
            this.tsbDrawChart.Size = new System.Drawing.Size(36, 36);
            this.tsbDrawChart.Text = "toolStripButton1";
            this.tsbDrawChart.Click += new System.EventHandler(this.tsbDrawChart_Click);
            // 
            // tsbSaveChart
            // 
            this.tsbSaveChart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSaveChart.Image = global::AnaControl.Properties.Resources.save;
            this.tsbSaveChart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSaveChart.Name = "tsbSaveChart";
            this.tsbSaveChart.Size = new System.Drawing.Size(36, 36);
            this.tsbSaveChart.Text = "toolStripButton2";
            this.tsbSaveChart.Click += new System.EventHandler(this.tsbSaveChart_Click);
            // 
            // chart1
            // 
            this.chart1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            chartArea1.AlignmentOrientation = ((System.Windows.Forms.DataVisualization.Charting.AreaAlignmentOrientations)((System.Windows.Forms.DataVisualization.Charting.AreaAlignmentOrientations.Vertical | System.Windows.Forms.DataVisualization.Charting.AreaAlignmentOrientations.Horizontal)));
            chartArea1.AxisX.TextOrientation = System.Windows.Forms.DataVisualization.Charting.TextOrientation.Horizontal;
            chartArea1.AxisX.Title = "tyytyt";
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.ContextMenuStrip = this.contextMenuStrip_msChart;
            this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.IsTextAutoFit = false;
            legend1.LegendItemOrder = System.Windows.Forms.DataVisualization.Charting.LegendItemOrder.SameAsSeriesOrder;
            legend1.Name = "Legend1";
            legend1.TextWrapThreshold = 0;
            legend1.Title = "yuuy";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(0, 39);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(492, 273);
            this.chart1.TabIndex = 1;
            this.chart1.Text = "chart1";
            // 
            // contextMenuStrip_msChart
            // 
            this.contextMenuStrip_msChart.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_Save,
            this.ToolStripMenuItem_3d,
            this.menuItemChartTypes,
            this.TsbShowValueLabel,
            this.miSaveData,
            this.miCopyData});
            this.contextMenuStrip_msChart.Name = "contextMenuStrip_msChart";
            this.contextMenuStrip_msChart.Size = new System.Drawing.Size(153, 158);
            this.contextMenuStrip_msChart.Text = "Save";
            // 
            // toolStripMenuItem_Save
            // 
            this.toolStripMenuItem_Save.Name = "toolStripMenuItem_Save";
            this.toolStripMenuItem_Save.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem_Save.Text = "Save";
            this.toolStripMenuItem_Save.Click += new System.EventHandler(this.toolStripMenuItem_Save_Click);
            // 
            // ToolStripMenuItem_3d
            // 
            this.ToolStripMenuItem_3d.Name = "ToolStripMenuItem_3d";
            this.ToolStripMenuItem_3d.Size = new System.Drawing.Size(152, 22);
            this.ToolStripMenuItem_3d.Text = "3D";
            this.ToolStripMenuItem_3d.Click += new System.EventHandler(this.ToolStripMenuItem_3d_Click);
            // 
            // menuItemChartTypes
            // 
            this.menuItemChartTypes.Name = "menuItemChartTypes";
            this.menuItemChartTypes.Size = new System.Drawing.Size(152, 22);
            this.menuItemChartTypes.Text = "曲线类型";
            // 
            // TsbShowValueLabel
            // 
            this.TsbShowValueLabel.Name = "TsbShowValueLabel";
            this.TsbShowValueLabel.Size = new System.Drawing.Size(152, 22);
            this.TsbShowValueLabel.Text = "显示标签值";
            this.TsbShowValueLabel.Click += new System.EventHandler(this.TsbShowValueLabel_Click);
            // 
            // miSaveData
            // 
            this.miSaveData.Name = "miSaveData";
            this.miSaveData.Size = new System.Drawing.Size(152, 22);
            this.miSaveData.Text = "Save Data";
            this.miSaveData.Click += new System.EventHandler(this.miSaveData_Click);
            // 
            // miCopyData
            // 
            this.miCopyData.Name = "miCopyData";
            this.miCopyData.Size = new System.Drawing.Size(152, 22);
            this.miCopyData.Text = "Copy Data";
            this.miCopyData.Click += new System.EventHandler(this.miCopyData_Click);
            // 
            // AbstrctAnalyzer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "AbstrctAnalyzer";
            this.Size = new System.Drawing.Size(492, 312);
            this.Load += new System.EventHandler(this.AbstrctAnalyzer_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.contextMenuStrip_msChart.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbDrawChart;
        private System.Windows.Forms.ToolStripButton tsbSaveChart;
        protected System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        protected System.Windows.Forms.ContextMenuStrip contextMenuStrip_msChart;
        protected System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Save;
        protected System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_3d;
        protected System.Windows.Forms.ToolStripMenuItem menuItemChartTypes;
        private System.Windows.Forms.ToolStripMenuItem TsbShowValueLabel;
        private System.Windows.Forms.ToolStripMenuItem miSaveData;
        private System.Windows.Forms.ToolStripMenuItem miCopyData;
    }
}
