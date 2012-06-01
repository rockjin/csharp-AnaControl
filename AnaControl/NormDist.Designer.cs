using System.Data;

namespace AnaControl
{
    partial class NormDist
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
            if(_db.Conn!=null && _db.Conn.State!=ConnectionState.Closed)
            {
                _db.DisConnect();
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
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint1 = new System.Windows.Forms.DataVisualization.Charting.DataPoint();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint2 = new System.Windows.Forms.DataVisualization.Charting.DataPoint();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint3 = new System.Windows.Forms.DataVisualization.Charting.DataPoint();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint4 = new System.Windows.Forms.DataVisualization.Charting.DataPoint();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint5 = new System.Windows.Forms.DataVisualization.Charting.DataPoint();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint6 = new System.Windows.Forms.DataVisualization.Charting.DataPoint();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint7 = new System.Windows.Forms.DataVisualization.Charting.DataPoint();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint8 = new System.Windows.Forms.DataVisualization.Charting.DataPoint();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint9 = new System.Windows.Forms.DataVisualization.Charting.DataPoint();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint10 = new System.Windows.Forms.DataVisualization.Charting.DataPoint();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint11 = new System.Windows.Forms.DataVisualization.Charting.DataPoint();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint12 = new System.Windows.Forms.DataVisualization.Charting.DataPoint();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint13 = new System.Windows.Forms.DataVisualization.Charting.DataPoint();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint14 = new System.Windows.Forms.DataVisualization.Charting.DataPoint();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint15 = new System.Windows.Forms.DataVisualization.Charting.DataPoint();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint16 = new System.Windows.Forms.DataVisualization.Charting.DataPoint();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint17 = new System.Windows.Forms.DataVisualization.Charting.DataPoint();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint18 = new System.Windows.Forms.DataVisualization.Charting.DataPoint();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint19 = new System.Windows.Forms.DataVisualization.Charting.DataPoint();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint20 = new System.Windows.Forms.DataVisualization.Charting.DataPoint();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint21 = new System.Windows.Forms.DataVisualization.Charting.DataPoint();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint22 = new System.Windows.Forms.DataVisualization.Charting.DataPoint();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint23 = new System.Windows.Forms.DataVisualization.Charting.DataPoint();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint24 = new System.Windows.Forms.DataVisualization.Charting.DataPoint();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint25 = new System.Windows.Forms.DataVisualization.Charting.DataPoint();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint26 = new System.Windows.Forms.DataVisualization.Charting.DataPoint();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint27 = new System.Windows.Forms.DataVisualization.Charting.DataPoint();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint28 = new System.Windows.Forms.DataVisualization.Charting.DataPoint();
            this.ToolStripMenuItem_RemoveFail = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripMenuItem_RemoveRepeats = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_RemovePass = new System.Windows.Forms.ToolStripMenuItem();
            this.去除异常数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.产品类型选择ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePicker_End = new System.Windows.Forms.DateTimePicker();
            this.label17 = new System.Windows.Forms.Label();
            this.dateTimePicker_Start = new System.Windows.Forms.DateTimePicker();
            this.label16 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.tb_Max = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.tb_Min = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.tb_TotalCount = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.tb_CPK = new System.Windows.Forms.TextBox();
            this.tb_Sigma = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_CP = new System.Windows.Forms.TextBox();
            this.tb_Average = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tb_M3Sigma = new System.Windows.Forms.TextBox();
            this.tb_CPU = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tb_P3Sigma = new System.Windows.Forms.TextBox();
            this.tb_CPL = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tb_CA = new System.Windows.Forms.TextBox();
            this.textBox_LSL = new System.Windows.Forms.TextBox();
            this.textBox_USL = new System.Windows.Forms.TextBox();
            this.textBox_Target = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.textBoxDefect = new System.Windows.Forms.TextBox();
            this.toolStripToolbar = new System.Windows.Forms.ToolStrip();
            this.tsbSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripComboBox_ItemSel = new System.Windows.Forms.ToolStripComboBox();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.contextMenuStrip_msChart = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem_Save = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_CopyBmp = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_CopyData = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.checkBoxAutoWildcard = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.toolStripToolbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.contextMenuStrip_msChart.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ToolStripMenuItem_RemoveFail
            // 
            this.ToolStripMenuItem_RemoveFail.Name = "ToolStripMenuItem_RemoveFail";
            this.ToolStripMenuItem_RemoveFail.Size = new System.Drawing.Size(153, 22);
            this.ToolStripMenuItem_RemoveFail.Text = "去除FAIL数据";
            this.ToolStripMenuItem_RemoveFail.Click += new System.EventHandler(this.toolStripMenuItem_RemoveRepeats_Click);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_RemoveRepeats,
            this.ToolStripMenuItem_RemovePass,
            this.ToolStripMenuItem_RemoveFail,
            this.去除异常数据ToolStripMenuItem,
            this.产品类型选择ToolStripMenuItem});
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(69, 22);
            this.toolStripDropDownButton1.Text = "过滤选项";
            // 
            // toolStripMenuItem_RemoveRepeats
            // 
            this.toolStripMenuItem_RemoveRepeats.Name = "toolStripMenuItem_RemoveRepeats";
            this.toolStripMenuItem_RemoveRepeats.Size = new System.Drawing.Size(153, 22);
            this.toolStripMenuItem_RemoveRepeats.Text = "去除重复项";
            this.toolStripMenuItem_RemoveRepeats.Click += new System.EventHandler(this.toolStripMenuItem_RemoveRepeats_Click);
            // 
            // ToolStripMenuItem_RemovePass
            // 
            this.ToolStripMenuItem_RemovePass.Name = "ToolStripMenuItem_RemovePass";
            this.ToolStripMenuItem_RemovePass.Size = new System.Drawing.Size(153, 22);
            this.ToolStripMenuItem_RemovePass.Text = "去除PASS数据";
            this.ToolStripMenuItem_RemovePass.Click += new System.EventHandler(this.toolStripMenuItem_RemoveRepeats_Click);
            // 
            // 去除异常数据ToolStripMenuItem
            // 
            this.去除异常数据ToolStripMenuItem.Name = "去除异常数据ToolStripMenuItem";
            this.去除异常数据ToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.去除异常数据ToolStripMenuItem.Text = "去除异常数据";
            this.去除异常数据ToolStripMenuItem.Click += new System.EventHandler(this.去除异常数据ToolStripMenuItem_Click);
            // 
            // 产品类型选择ToolStripMenuItem
            // 
            this.产品类型选择ToolStripMenuItem.Name = "产品类型选择ToolStripMenuItem";
            this.产品类型选择ToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.产品类型选择ToolStripMenuItem.Text = "产品类型选择";
            this.产品类型选择ToolStripMenuItem.Click += new System.EventHandler(this.产品类型选择ToolStripMenuItem_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 3F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.dateTimePicker_End, 1, 22);
            this.tableLayoutPanel1.Controls.Add(this.label17, 0, 22);
            this.tableLayoutPanel1.Controls.Add(this.dateTimePicker_Start, 1, 21);
            this.tableLayoutPanel1.Controls.Add(this.label16, 0, 21);
            this.tableLayoutPanel1.Controls.Add(this.label2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label11, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label12, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.tb_Max, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.label13, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.tb_Min, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.label14, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.tb_TotalCount, 2, 5);
            this.tableLayoutPanel1.Controls.Add(this.label15, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.tb_CPK, 2, 15);
            this.tableLayoutPanel1.Controls.Add(this.tb_Sigma, 2, 6);
            this.tableLayoutPanel1.Controls.Add(this.label10, 1, 15);
            this.tableLayoutPanel1.Controls.Add(this.label3, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.tb_CP, 2, 14);
            this.tableLayoutPanel1.Controls.Add(this.tb_Average, 2, 7);
            this.tableLayoutPanel1.Controls.Add(this.label9, 1, 14);
            this.tableLayoutPanel1.Controls.Add(this.label4, 1, 8);
            this.tableLayoutPanel1.Controls.Add(this.tb_M3Sigma, 2, 13);
            this.tableLayoutPanel1.Controls.Add(this.tb_CPU, 2, 8);
            this.tableLayoutPanel1.Controls.Add(this.label8, 1, 13);
            this.tableLayoutPanel1.Controls.Add(this.label5, 1, 9);
            this.tableLayoutPanel1.Controls.Add(this.tb_P3Sigma, 2, 12);
            this.tableLayoutPanel1.Controls.Add(this.tb_CPL, 2, 9);
            this.tableLayoutPanel1.Controls.Add(this.label7, 1, 12);
            this.tableLayoutPanel1.Controls.Add(this.label6, 1, 11);
            this.tableLayoutPanel1.Controls.Add(this.tb_CA, 2, 11);
            this.tableLayoutPanel1.Controls.Add(this.textBox_LSL, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.textBox_USL, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.textBox_Target, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.label18, 1, 10);
            this.tableLayoutPanel1.Controls.Add(this.textBoxDefect, 2, 10);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 25;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(229, 487);
            this.tableLayoutPanel1.TabIndex = 51;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(33, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 27);
            this.label1.TabIndex = 0;
            this.label1.Text = "规格下限LSL:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dateTimePicker_End
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.dateTimePicker_End, 2);
            this.dateTimePicker_End.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dateTimePicker_End.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dateTimePicker_End.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker_End.Location = new System.Drawing.Point(33, 455);
            this.dateTimePicker_End.Name = "dateTimePicker_End";
            this.dateTimePicker_End.Size = new System.Drawing.Size(182, 21);
            this.dateTimePicker_End.TabIndex = 46;
            this.dateTimePicker_End.Value = new System.DateTime(2010, 4, 1, 0, 0, 0, 0);
            this.dateTimePicker_End.ValueChanged += new System.EventHandler(this.ParameterChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label17.Location = new System.Drawing.Point(3, 452);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(24, 27);
            this.label17.TabIndex = 44;
            this.label17.Text = "到:";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dateTimePicker_Start
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.dateTimePicker_Start, 2);
            this.dateTimePicker_Start.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dateTimePicker_Start.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dateTimePicker_Start.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker_Start.Location = new System.Drawing.Point(33, 428);
            this.dateTimePicker_Start.Name = "dateTimePicker_Start";
            this.dateTimePicker_Start.Size = new System.Drawing.Size(182, 21);
            this.dateTimePicker_Start.TabIndex = 45;
            this.dateTimePicker_Start.Value = new System.DateTime(2010, 4, 1, 0, 0, 0, 0);
            this.dateTimePicker_Start.ValueChanged += new System.EventHandler(this.ParameterChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label16.Location = new System.Drawing.Point(3, 425);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(24, 27);
            this.label16.TabIndex = 43;
            this.label16.Text = "从:";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(33, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 27);
            this.label2.TabIndex = 2;
            this.label2.Text = "规格上限USL:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Location = new System.Drawing.Point(33, 54);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(104, 27);
            this.label11.TabIndex = 20;
            this.label11.Text = "目标值:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label12.Location = new System.Drawing.Point(33, 81);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(104, 27);
            this.label12.TabIndex = 22;
            this.label12.Text = "最大值:";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tb_Max
            // 
            this.tb_Max.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb_Max.Location = new System.Drawing.Point(143, 84);
            this.tb_Max.Name = "tb_Max";
            this.tb_Max.ReadOnly = true;
            this.tb_Max.Size = new System.Drawing.Size(72, 21);
            this.tb_Max.TabIndex = 23;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label13.Location = new System.Drawing.Point(33, 108);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(104, 27);
            this.label13.TabIndex = 24;
            this.label13.Text = "最小值:";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tb_Min
            // 
            this.tb_Min.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb_Min.Location = new System.Drawing.Point(143, 111);
            this.tb_Min.Name = "tb_Min";
            this.tb_Min.ReadOnly = true;
            this.tb_Min.Size = new System.Drawing.Size(72, 21);
            this.tb_Min.TabIndex = 25;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label14.Location = new System.Drawing.Point(33, 135);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(104, 27);
            this.label14.TabIndex = 26;
            this.label14.Text = "样本量:";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tb_TotalCount
            // 
            this.tb_TotalCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb_TotalCount.Location = new System.Drawing.Point(143, 138);
            this.tb_TotalCount.Name = "tb_TotalCount";
            this.tb_TotalCount.ReadOnly = true;
            this.tb_TotalCount.Size = new System.Drawing.Size(72, 21);
            this.tb_TotalCount.TabIndex = 27;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label15.Location = new System.Drawing.Point(33, 162);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(104, 27);
            this.label15.TabIndex = 28;
            this.label15.Text = "标准差:";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tb_CPK
            // 
            this.tb_CPK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb_CPK.Location = new System.Drawing.Point(143, 401);
            this.tb_CPK.Name = "tb_CPK";
            this.tb_CPK.ReadOnly = true;
            this.tb_CPK.Size = new System.Drawing.Size(72, 21);
            this.tb_CPK.TabIndex = 19;
            // 
            // tb_Sigma
            // 
            this.tb_Sigma.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb_Sigma.Location = new System.Drawing.Point(143, 165);
            this.tb_Sigma.Name = "tb_Sigma";
            this.tb_Sigma.ReadOnly = true;
            this.tb_Sigma.Size = new System.Drawing.Size(72, 21);
            this.tb_Sigma.TabIndex = 29;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Location = new System.Drawing.Point(33, 398);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(104, 27);
            this.label10.TabIndex = 18;
            this.label10.Text = "实际能力指数CPK:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(33, 189);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 27);
            this.label3.TabIndex = 4;
            this.label3.Text = "平均值：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tb_CP
            // 
            this.tb_CP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb_CP.Location = new System.Drawing.Point(143, 374);
            this.tb_CP.Name = "tb_CP";
            this.tb_CP.ReadOnly = true;
            this.tb_CP.Size = new System.Drawing.Size(72, 21);
            this.tb_CP.TabIndex = 17;
            // 
            // tb_Average
            // 
            this.tb_Average.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb_Average.Location = new System.Drawing.Point(143, 192);
            this.tb_Average.Name = "tb_Average";
            this.tb_Average.ReadOnly = true;
            this.tb_Average.Size = new System.Drawing.Size(72, 21);
            this.tb_Average.TabIndex = 5;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Location = new System.Drawing.Point(33, 371);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(104, 27);
            this.label9.TabIndex = 16;
            this.label9.Text = "潜在能力指数CP:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(33, 216);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 27);
            this.label4.TabIndex = 6;
            this.label4.Text = "上限过程能力CPU:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tb_M3Sigma
            // 
            this.tb_M3Sigma.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb_M3Sigma.Location = new System.Drawing.Point(143, 347);
            this.tb_M3Sigma.Name = "tb_M3Sigma";
            this.tb_M3Sigma.ReadOnly = true;
            this.tb_M3Sigma.Size = new System.Drawing.Size(72, 21);
            this.tb_M3Sigma.TabIndex = 15;
            // 
            // tb_CPU
            // 
            this.tb_CPU.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb_CPU.Location = new System.Drawing.Point(143, 219);
            this.tb_CPU.Name = "tb_CPU";
            this.tb_CPU.ReadOnly = true;
            this.tb_CPU.Size = new System.Drawing.Size(72, 21);
            this.tb_CPU.TabIndex = 7;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Location = new System.Drawing.Point(33, 344);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(104, 27);
            this.label8.TabIndex = 14;
            this.label8.Text = "下管制线-3σ";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(33, 243);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 27);
            this.label5.TabIndex = 8;
            this.label5.Text = "下限过程能力CPL:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tb_P3Sigma
            // 
            this.tb_P3Sigma.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb_P3Sigma.Location = new System.Drawing.Point(143, 320);
            this.tb_P3Sigma.Name = "tb_P3Sigma";
            this.tb_P3Sigma.ReadOnly = true;
            this.tb_P3Sigma.Size = new System.Drawing.Size(72, 21);
            this.tb_P3Sigma.TabIndex = 13;
            // 
            // tb_CPL
            // 
            this.tb_CPL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb_CPL.Location = new System.Drawing.Point(143, 246);
            this.tb_CPL.Name = "tb_CPL";
            this.tb_CPL.ReadOnly = true;
            this.tb_CPL.Size = new System.Drawing.Size(72, 21);
            this.tb_CPL.TabIndex = 9;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Location = new System.Drawing.Point(33, 317);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(104, 27);
            this.label7.TabIndex = 12;
            this.label7.Text = "上管制线+3σ";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Location = new System.Drawing.Point(33, 290);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(104, 27);
            this.label6.TabIndex = 10;
            this.label6.Text = "平均偏离度CA:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tb_CA
            // 
            this.tb_CA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb_CA.Location = new System.Drawing.Point(143, 293);
            this.tb_CA.Name = "tb_CA";
            this.tb_CA.ReadOnly = true;
            this.tb_CA.Size = new System.Drawing.Size(72, 21);
            this.tb_CA.TabIndex = 11;
            // 
            // textBox_LSL
            // 
            this.textBox_LSL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_LSL.Location = new System.Drawing.Point(143, 3);
            this.textBox_LSL.Name = "textBox_LSL";
            this.textBox_LSL.Size = new System.Drawing.Size(72, 21);
            this.textBox_LSL.TabIndex = 51;
            this.textBox_LSL.TextChanged += new System.EventHandler(this.ParameterChanged);
            // 
            // textBox_USL
            // 
            this.textBox_USL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_USL.Location = new System.Drawing.Point(143, 30);
            this.textBox_USL.Name = "textBox_USL";
            this.textBox_USL.Size = new System.Drawing.Size(72, 21);
            this.textBox_USL.TabIndex = 52;
            this.textBox_USL.TextChanged += new System.EventHandler(this.ParameterChanged);
            // 
            // textBox_Target
            // 
            this.textBox_Target.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_Target.Location = new System.Drawing.Point(143, 57);
            this.textBox_Target.Name = "textBox_Target";
            this.textBox_Target.Size = new System.Drawing.Size(72, 21);
            this.textBox_Target.TabIndex = 53;
            this.textBox_Target.TextChanged += new System.EventHandler(this.ParameterChanged);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label18.Location = new System.Drawing.Point(33, 270);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(104, 20);
            this.label18.TabIndex = 54;
            this.label18.Text = "预估不良率";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxDefect
            // 
            this.textBoxDefect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxDefect.Location = new System.Drawing.Point(143, 273);
            this.textBoxDefect.Name = "textBoxDefect";
            this.textBoxDefect.ReadOnly = true;
            this.textBoxDefect.Size = new System.Drawing.Size(72, 21);
            this.textBoxDefect.TabIndex = 55;
            // 
            // toolStripToolbar
            // 
            this.toolStripToolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbSave,
            this.toolStripDropDownButton1,
            this.toolStripComboBox_ItemSel});
            this.toolStripToolbar.Location = new System.Drawing.Point(0, 0);
            this.toolStripToolbar.Name = "toolStripToolbar";
            this.toolStripToolbar.Size = new System.Drawing.Size(704, 25);
            this.toolStripToolbar.TabIndex = 12;
            this.toolStripToolbar.Text = "toolStrip1";
            // 
            // tsbSave
            // 
            this.tsbSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSave.Image = global::AnaControl.Properties.Resources.Save;
            this.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSave.Name = "tsbSave";
            this.tsbSave.Size = new System.Drawing.Size(23, 22);
            this.tsbSave.Click += new System.EventHandler(this.tsbSave_Click);
            // 
            // toolStripComboBox_ItemSel
            // 
            this.toolStripComboBox_ItemSel.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripComboBox_ItemSel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.toolStripComboBox_ItemSel.Name = "toolStripComboBox_ItemSel";
            this.toolStripComboBox_ItemSel.Size = new System.Drawing.Size(450, 25);
            this.toolStripComboBox_ItemSel.TextChanged += new System.EventHandler(this.toolStripComboBox_ItemSel_TextChanged);
            // 
            // chart1
            // 
            this.chart1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            chartArea1.AlignmentOrientation = ((System.Windows.Forms.DataVisualization.Charting.AreaAlignmentOrientations)((System.Windows.Forms.DataVisualization.Charting.AreaAlignmentOrientations.Vertical | System.Windows.Forms.DataVisualization.Charting.AreaAlignmentOrientations.Horizontal)));
            chartArea1.AxisX.MajorTickMark.TickMarkStyle = System.Windows.Forms.DataVisualization.Charting.TickMarkStyle.InsideArea;
            chartArea1.AxisX.ScaleBreakStyle.BreakLineStyle = System.Windows.Forms.DataVisualization.Charting.BreakLineStyle.Straight;
            chartArea1.AxisX.ScaleBreakStyle.Enabled = true;
            chartArea1.AxisX.ScaleBreakStyle.StartFromZero = System.Windows.Forms.DataVisualization.Charting.StartFromZero.Yes;
            chartArea1.AxisX2.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.False;
            chartArea1.AxisX2.MajorGrid.Enabled = false;
            chartArea1.AxisX2.MajorTickMark.Enabled = false;
            chartArea1.AxisY.MajorGrid.Enabled = false;
            chartArea1.AxisY2.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.False;
            chartArea1.AxisY2.MajorGrid.Enabled = false;
            chartArea1.AxisY2.MajorTickMark.Enabled = false;
            chartArea1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            chartArea1.InnerPlotPosition.Auto = false;
            chartArea1.InnerPlotPosition.Height = 94.35864F;
            chartArea1.InnerPlotPosition.Width = 97.19969F;
            chartArea1.InnerPlotPosition.X = 2.2418F;
            chartArea1.Name = "ChartArea1";
            chartArea1.Position.Auto = false;
            chartArea1.Position.Height = 94F;
            chartArea1.Position.Width = 94F;
            chartArea1.Position.X = 3F;
            chartArea1.Position.Y = 3F;
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.ContextMenuStrip = this.contextMenuStrip_msChart;
            this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chart1.Location = new System.Drawing.Point(0, 0);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.CustomProperties = "PointWidth=1";
            series1.Name = "Series3";
            dataPoint1.AxisLabel = "122";
            dataPoint1.Label = "123";
            dataPoint2.AxisLabel = "333";
            dataPoint3.AxisLabel = "444";
            dataPoint4.AxisLabel = "555";
            dataPoint5.AxisLabel = "666";
            series1.Points.Add(dataPoint1);
            series1.Points.Add(dataPoint2);
            series1.Points.Add(dataPoint3);
            series1.Points.Add(dataPoint4);
            series1.Points.Add(dataPoint5);
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series2.Legend = "Legend1";
            series2.Name = "Series2";
            series2.Points.Add(dataPoint6);
            series2.Points.Add(dataPoint7);
            series2.Points.Add(dataPoint8);
            series2.Points.Add(dataPoint9);
            series2.Points.Add(dataPoint10);
            series2.Points.Add(dataPoint11);
            series2.Points.Add(dataPoint12);
            series2.Points.Add(dataPoint13);
            series2.Points.Add(dataPoint14);
            series2.Points.Add(dataPoint15);
            series2.Points.Add(dataPoint16);
            series2.Points.Add(dataPoint17);
            series2.Points.Add(dataPoint18);
            series2.Points.Add(dataPoint19);
            series2.Points.Add(dataPoint20);
            series2.Points.Add(dataPoint21);
            series2.Points.Add(dataPoint22);
            series2.Points.Add(dataPoint23);
            series2.Points.Add(dataPoint24);
            series2.Points.Add(dataPoint25);
            series2.Points.Add(dataPoint26);
            series2.XAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.ErrorBar;
            series3.Legend = "Legend1";
            series3.Name = "Series4";
            dataPoint27.AxisLabel = "123";
            dataPoint27.CustomProperties = "ErrorBarCenterMarkerStyle=None";
            dataPoint27.IsVisibleInLegend = true;
            dataPoint27.Label = "123";
            dataPoint27.LegendText = "";
            dataPoint28.Label = "567";
            series3.Points.Add(dataPoint27);
            series3.Points.Add(dataPoint28);
            series3.YValuesPerPoint = 3;
            this.chart1.Series.Add(series1);
            this.chart1.Series.Add(series2);
            this.chart1.Series.Add(series3);
            this.chart1.Size = new System.Drawing.Size(467, 487);
            this.chart1.TabIndex = 0;
            // 
            // contextMenuStrip_msChart
            // 
            this.contextMenuStrip_msChart.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_Save,
            this.ToolStripMenuItem_CopyBmp,
            this.ToolStripMenuItem_CopyData});
            this.contextMenuStrip_msChart.Name = "contextMenuStrip_msChart";
            this.contextMenuStrip_msChart.Size = new System.Drawing.Size(125, 70);
            this.contextMenuStrip_msChart.Text = "Save";
            // 
            // toolStripMenuItem_Save
            // 
            this.toolStripMenuItem_Save.Name = "toolStripMenuItem_Save";
            this.toolStripMenuItem_Save.Size = new System.Drawing.Size(124, 22);
            this.toolStripMenuItem_Save.Text = "保存数据";
            this.toolStripMenuItem_Save.Click += new System.EventHandler(this.toolStripMenuItem_Save_Click);
            // 
            // ToolStripMenuItem_CopyBmp
            // 
            this.ToolStripMenuItem_CopyBmp.Name = "ToolStripMenuItem_CopyBmp";
            this.ToolStripMenuItem_CopyBmp.Size = new System.Drawing.Size(124, 22);
            this.ToolStripMenuItem_CopyBmp.Text = "拷贝图片";
            this.ToolStripMenuItem_CopyBmp.Click += new System.EventHandler(this.ToolStripMenuItem_CopyBmp_Click);
            // 
            // ToolStripMenuItem_CopyData
            // 
            this.ToolStripMenuItem_CopyData.Name = "ToolStripMenuItem_CopyData";
            this.ToolStripMenuItem_CopyData.Size = new System.Drawing.Size(124, 22);
            this.ToolStripMenuItem_CopyData.Text = "拷贝数据";
            this.ToolStripMenuItem_CopyData.Click += new System.EventHandler(this.ToolStripMenuItem_CopyData_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.splitContainer1.Panel1.Controls.Add(this.chart1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AutoScroll = true;
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel1);
            this.splitContainer1.Size = new System.Drawing.Size(704, 489);
            this.splitContainer1.SplitterDistance = 469;
            this.splitContainer1.TabIndex = 13;
            // 
            // checkBoxAutoWildcard
            // 
            this.checkBoxAutoWildcard.AutoSize = true;
            this.checkBoxAutoWildcard.Checked = true;
            this.checkBoxAutoWildcard.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAutoWildcard.Location = new System.Drawing.Point(565, 4);
            this.checkBoxAutoWildcard.Name = "checkBoxAutoWildcard";
            this.checkBoxAutoWildcard.Size = new System.Drawing.Size(108, 16);
            this.checkBoxAutoWildcard.TabIndex = 14;
            this.checkBoxAutoWildcard.Text = "自动添加通配符";
            this.checkBoxAutoWildcard.UseVisualStyleBackColor = true;
            this.checkBoxAutoWildcard.CheckedChanged += new System.EventHandler(this.ParameterChanged);
            // 
            // NormDist
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.checkBoxAutoWildcard);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStripToolbar);
            this.Name = "NormDist";
            this.Size = new System.Drawing.Size(704, 514);
            this.Load += new System.EventHandler(this.NormDist_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.toolStripToolbar.ResumeLayout(false);
            this.toolStripToolbar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.contextMenuStrip_msChart.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripButton tsbSave;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_RemoveFail;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_RemoveRepeats;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_RemovePass;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePicker_End;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.DateTimePicker dateTimePicker_Start;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tb_Max;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox tb_Min;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox tb_TotalCount;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox tb_CPK;
        private System.Windows.Forms.TextBox tb_Sigma;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_CP;
        private System.Windows.Forms.TextBox tb_Average;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tb_M3Sigma;
        private System.Windows.Forms.TextBox tb_CPU;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tb_P3Sigma;
        private System.Windows.Forms.TextBox tb_CPL;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tb_CA;
        private System.Windows.Forms.TextBox textBox_LSL;
        private System.Windows.Forms.TextBox textBox_USL;
        private System.Windows.Forms.TextBox textBox_Target;
        private System.Windows.Forms.ToolStrip toolStripToolbar;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox_ItemSel;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_msChart;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Save;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox textBoxDefect;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_CopyBmp;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_CopyData;
        private System.Windows.Forms.ToolStripMenuItem 去除异常数据ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 产品类型选择ToolStripMenuItem;
        private System.Windows.Forms.CheckBox checkBoxAutoWildcard;
    }
}
