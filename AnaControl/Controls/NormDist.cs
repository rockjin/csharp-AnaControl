using AnaControl.Utils;
using DbDriver;
using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Point = System.Drawing.Point;
using AnaControl.Dlgs;

namespace AnaControl.Controls
{
    public partial class NormDist : UserControl
    {
        #region log informations
        private void LogFunc(object sender, EventArgs e)
        {
            Debug.WriteLine(e.ToString());
        }
        public EventHandler OnLog = null;
        private void InvokeOnLog(MsgEventArgs e)
        {
            if (OnLog == null) return;
            OnLog(this, e);
        }
        #endregion
        private bool isInitializing;
        private double[] _data;

        private DbProc _db = new DbProc();
        public DbProc Db
        {
            get { return _db; }
            set { _db = value; }
        }
        public NormDist()
        {
            OnLog = this.LogFunc;
            InitializeComponent();
            InvokeOnLog("Initialized sucess.");
        }
        private static Bitmap CreateBitmap(Control trl)
        {
            Bitmap bmpDrawed = new Bitmap(trl.Width, trl.Height);
            Graphics gDest = Graphics.FromImage(bmpDrawed);
            gDest.CopyFromScreen(trl.PointToScreen(new Point(0, 0)), new Point(0, 0), trl.Size);
            return bmpDrawed;
        }
        private void tsbSave_Click(object sender, System.EventArgs e)
        {
            try
            {
                this.Refresh();
                Bitmap bmp = CreateBitmap(this.splitContainer1);
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "png|*.png";
                sfd.FileName = "temp.png";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    bmp.Save(sfd.FileName,System.Drawing.Imaging.ImageFormat.Png);
                    this.InvokeOnLog(string.Format("save bitmap file :\n{0}\nSuccess!", sfd.FileName));
                }
            }
            catch (SystemException exp)
            {
                this.InvokeOnLog(exp.Message);
                return;
            }
        }

        private void NormDist_Load(object sender, EventArgs e)
        {
            try
            {
                this.dateTimePicker_Start.Value = Properties.Settings.Default.DefaultDateTimeStart;
                this.dateTimePicker_End.Value = Properties.Settings.Default.DefaultDateTimeEnd;
                //var strCmd = string.Format("Select max(test_time) from TEST_ITEM_VALUES");
                //DataTable dt = _db.GetDataTable(strCmd);
                //dt.Clear();
                //strCmd = string.Format("SELECT DISTINCT TEST_ITEM_NAME FROM TEST_ITEM_VALUES");
                //dt = _db.GetDataTable(strCmd);
                isInitializing = true;
            }
            catch (Exception exp)
            {
                this.InvokeOnLog(exp.Message);
            }
        }
        string _max, _min, _usl, _lsl, _totalCount, _average, _sigma, _target, _m3Sigma, _p3Sigma;

        private void ReadDataFromDatabase()
        {
            DataTable dt = new DataTable();
            string matchString = "";
            if (Properties.Settings.Default.AutoWildcard)
            {
                matchString = "%" + Properties.Settings.Default.DefaultTestItem + "%";
            }
            else
            {
                matchString = Properties.Settings.Default.DefaultTestItem;
            }
            string sqlCmd = "SELECT avg(t1.item_value) as avgVal,stdev(t1.item_value) as stdVal,"
                            + "min(t1.item_value) as minVal,max(t1.item_value) as maxVal,count(t1.item_value) as totalVal,"
                            + "min(t1.Low_Limit) as lowLimit,max(t1.up_limit) as upLimit "
                            + "from TEST_ITEM_VALUES t1, TEST_RESULTS t2 "
                            + "where t2.test_time>=#" +
                            Properties.Settings.Default.DefaultDateTimeStart.ToString("yyyy-MM-dd HH:mm:ss") + "# "
                            + "and t2.test_time<=#" +
                            Properties.Settings.Default.DefaultDateTimeEnd.ToString("yyyy-MM-dd HH:mm:ss") + "# "
                            + "and t1.TEST_ITEM_NAME like '" + matchString + "' "
                            + "and t1.PRODUCT_SN = t2.PRODUCT_SN and t1.TEST_TIME=t2.TEST_TIME "
                            + "and t2.STATION like '%" + Properties.Settings.Default.DefaultTestBench + "%' "
                            + "and t2.PRODUCT_NAME like '%" + Properties.Settings.Default.ProductType + "%' ";
            InvokeOnLog("create sql cmd...\n");
            if (Properties.Settings.Default.DefaultRemoveRepeat)
            {
                InvokeOnLog("remove repeats\n");
                sqlCmd += " and t1.test_time in("
                          + " select max(test_time) "
                          + " from test_results "
                          + "where test_time>=#" +
                           Properties.Settings.Default.DefaultDateTimeStart.ToString("yyyy-MM-dd HH:mm:ss") + "# "
                          + "and test_time<=#" +
                           Properties.Settings.Default.DefaultDateTimeEnd.ToString("yyyy-MM-dd HH:mm:ss") + "# "
                          + " group by product_sn)";
            }
            if (Properties.Settings.Default.DefaultRemovePassData)
            {
                InvokeOnLog("remove pass data\n");
                sqlCmd += " and t1.pass_state <> 0 and t2.fail_code <> 0";
            }
            if (Properties.Settings.Default.DefaultRemoveFailData)
            {
                InvokeOnLog("remove fail data\n");
                sqlCmd += " and t1.pass_state = 0 and t2.fail_code = 0";
            }
            //if (this.toolStripMenuItem_RemoveRepeats.Checked)
            //{
            //    sqlCmd += " group by product_sn)";
            //}
            if (Properties.Settings.Default.DefaultRemoveSpecialData)
            {
                sqlCmd += " and t1.item_value>" + Properties.Settings.Default.AbnormalLowData
                          + " and t1.item_value<" + Properties.Settings.Default.AbnormalUpData;

            }
            dt = _db.GetDataTable(sqlCmd);
            InvokeOnLog("query sucess\n");
            _max = dt.Rows[0]["maxVal"].ToString();
            _min = dt.Rows[0]["minVal"].ToString();
            _lsl = double.IsInfinity(double.Parse(dt.Rows[0]["lowLimit"].ToString())) ? "-100" : dt.Rows[0]["lowLimit"].ToString();
            _usl = double.IsInfinity(double.Parse(dt.Rows[0]["upLimit"].ToString())) ? "100" : dt.Rows[0]["upLimit"].ToString();
            _totalCount = dt.Rows[0]["totalVal"].ToString();
            _average = dt.Rows[0]["avgVal"].ToString();
            _sigma = dt.Rows[0]["stdVal"].ToString();
            _target = _average;

            {
                double a, s;
                if (!double.TryParse(_average, out a))
                {
                    return;
                }
                if (!double.TryParse(_sigma, out s))
                {
                    return;
                }
                _p3Sigma = (a + s * 3).ToString();
                _m3Sigma = (a - s * 3).ToString();
            }


            sqlCmd = "SELECT t1.item_value "
                        + "from TEST_ITEM_VALUES t1, TEST_RESULTS t2 "
                        + "where t2.test_time>=#" +
                        Properties.Settings.Default.DefaultDateTimeStart.ToString("yyyy-MM-dd HH:mm:ss") + "# "
                        + "and t2.test_time<=#" +
                        Properties.Settings.Default.DefaultDateTimeEnd.ToString("yyyy-MM-dd HH:mm:ss") + "# "
                        + "and t1.TEST_ITEM_NAME like '" + matchString + "' "
                        + "and t1.PRODUCT_SN = t2.PRODUCT_SN and t1.TEST_TIME=t2.TEST_TIME "
                        + "and t2.STATION like '%" + Properties.Settings.Default.DefaultTestBench + "%' "
                        + "and t2.PRODUCT_NAME like '%" + Properties.Settings.Default.ProductType + "%' ";
            InvokeOnLog("create sql command...\n");
            if (Properties.Settings.Default.DefaultRemoveRepeat)
            {
                InvokeOnLog("remove repeats\n");
                sqlCmd += " and t1.test_time in("
                          + " select max(test_time) "
                          + " from test_results "
                          + "where test_time>=#" +
                           Properties.Settings.Default.DefaultDateTimeStart.ToString("yyyy-MM-dd HH:mm:ss") + "# "
                          + "and test_time<=#" +
                           Properties.Settings.Default.DefaultDateTimeEnd.ToString("yyyy-MM-dd HH:mm:ss") + "# "
                          + " group by product_sn)";
            }
            if (Properties.Settings.Default.DefaultRemovePassData)
            {
                InvokeOnLog("remove pass data\n");
                sqlCmd += " and t1.pass_state <> 0 and t2.fail_code <> 0";
            }
            if (Properties.Settings.Default.DefaultRemoveFailData)
            {
                InvokeOnLog("remove fail data\n");
                sqlCmd += " and t1.pass_state = 0 and t2.fail_code = 0";
            }
            //if (this.toolStripMenuItem_RemoveRepeats.Checked)
            //{
            //    sqlCmd += " group by product_sn)";
            //}
            if (Properties.Settings.Default.DefaultRemoveSpecialData)
            {
                sqlCmd += " and t1.item_value>" + Properties.Settings.Default.AbnormalLowData
                          + " and t1.item_value<" + Properties.Settings.Default.AbnormalUpData;

            }
            dt.Clear();
            dt = _db.GetDataTable(sqlCmd);
            this._data = new double[dt.Rows.Count];
            for (int i = 0; i < _data.Length; i++)
            {
                this._data[i] = double.Parse(dt.Rows[i]["item_value"].ToString());
            }

        }

        private void ParameterChanged(object sender, EventArgs e)
        {
            if (isInitializing)
            {
                try
                {
                    this.DrawChart();
                }
                catch (Exception exp)
                {
                    InvokeOnLog(exp.Message);
                }
            }
        }
        private void DrawChart()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(DrawChart));
                return;
            }
            #region 局部变量
            double yMax = 0;
            #endregion 局部变量
            #region 计算中间值
            double usl = double.NegativeInfinity, lsl = double.NegativeInfinity;
            if (!double.TryParse(this.textBox_LSL.Text, out lsl)) return;
            if (!double.TryParse(this.textBox_USL.Text, out usl)) return;
            double average;
            if (!double.TryParse(this._average, out average)) return;
            double sigma;
            if (!double.TryParse(this._sigma, out sigma)) return;
            double cp = (usl - lsl) / (6 * sigma);
            double cpu = (usl - average) / (3 * sigma);
            double cpl = (average - lsl) / (3 * sigma);
            double cpk = Math.Min(cpu, cpl);
            double ca = (average - (usl + lsl) / 2) / ((usl - lsl) / 2);
            double p3Sigma = average + 3 * sigma;
            double m3Sigma = average - 3 * sigma;
            double uStandardize = (usl - average) / sigma;
            double dStandardize = (lsl - average) / sigma;
            double uNormsdist = NormSDist(uStandardize);
            double dNormsdist = NormSDist(dStandardize);
            double defect = Math.Round(1 - uNormsdist + dNormsdist, 9) * 100;

            this.tb_CP.Text = cp.ToString();
            this.tb_CPU.Text = cpu.ToString();
            this.tb_CPL.Text = cpl.ToString();
            this.tb_CPK.Text = cpk.ToString();
            this.tb_CA.Text = (ca * 100).ToString("f2") + @"%";
            this.tb_Average.Text = this._average;
            this.tb_Sigma.Text = this._sigma;
            this.tb_P3Sigma.Text = p3Sigma.ToString();
            this.tb_M3Sigma.Text = m3Sigma.ToString();
            this.textBoxDefect.Text = defect.ToString("f7") + @"%";
            #endregion 计算中间值
            #region 创建图表区域
            this.chart1.Series.Clear();
            this.chart1.ChartAreas.Clear();
            this.chart1.ChartAreas.Add("NormDist");
            this.chart1.ChartAreas["NormDist"].BackColor = SystemColors.AppWorkspace;
            this.chart1.ChartAreas["NormDist"].AxisX2.Enabled = AxisEnabled.False;
            this.chart1.ChartAreas["NormDist"].AxisY2.Enabled = AxisEnabled.False;
            this.chart1.ChartAreas["NormDist"].AxisX.MajorGrid.Enabled = false;
            this.chart1.ChartAreas["NormDist"].AxisY.MajorGrid.Enabled = false;
            //this.chart1.ChartAreas["NormDist"].AxisX.Interval = 1;
            //this.chart1.ChartAreas["NormDist"].AxisX.MajorTickMark.TickMarkStyle = TickMarkStyle.InsideArea;
            //this.chart1.ChartAreas["NormDist"].AxisY.MajorTickMark.TickMarkStyle = TickMarkStyle.InsideArea;
            //this.chart1.ChartAreas["NormDist"].AxisX2.IsMarginVisible = false;
            //this.chart1.ChartAreas["NormDist"].AxisX.IsMarginVisible = false;
            this.chart1.ChartAreas["NormDist"].AxisX.MajorTickMark.Enabled = false;
            #endregion 创建图表区域

            #region 添加标题
            this.chart1.Titles.Clear();
            this.chart1.Titles.Add(Properties.Settings.Default.DefaultTestItem + " 正态分布");
            #endregion
            #region 添加曲线
            this.chart1.Series.Add("Column");
            this.chart1.Series.Add("limit");
            this.chart1.Series.Add("Sigma");
            this.chart1.Series.Add("Target");
            this.chart1.Series.Add("正态曲线");
            this.chart1.Series["正态曲线"].ChartType = SeriesChartType.Spline;
            this.chart1.Series["正态曲线"].Color = Color.DeepPink;
            //this.chart1.Series["正态曲线"].XAxisType = AxisType.Secondary;

            this.chart1.Series["limit"].ChartType = SeriesChartType.ErrorBar;
            this.chart1.Series["limit"].Color = Color.Red;

            this.chart1.Series["Sigma"].ChartType = SeriesChartType.ErrorBar;
            this.chart1.Series["Sigma"].Color = Color.Green;


            this.chart1.Series["Target"].ChartType = SeriesChartType.ErrorBar;
            this.chart1.Series["Target"].Color = Color.DarkBlue;
            this.chart1.Series["Target"].BorderWidth = 2;

            this.chart1.Series["Column"].ChartType = SeriesChartType.Column;
            this.chart1.Series["Column"].BorderColor = Color.Black;
            this.chart1.Series["Column"].Color = Color.DodgerBlue;
            this.chart1.Series["Column"].SetCustomProperty("PointWidth", "1");
            //this.chart1.Series["Column"].XAxisType = AxisType.Primary;
            #endregion 添加曲线
            #region 正态分布图

            double total = double.Parse(this._totalCount);
            this.tb_TotalCount.Text = _totalCount;

            int precision = (int)(1 - Math.Round(Math.Log10(sigma), 0));
            double center = Math.Round(average * Math.Pow(10, precision)) / Math.Pow(10, precision);
            double xStep =
                Math.Floor
                (Math.Floor(sigma * 2 / Math.Pow(10, Math.Floor(Math.Log10(sigma)))) * Math.Pow(10, Math.Floor(Math.Log10(sigma))) * 5 /
                 Math.Pow(10, Math.Round(Math.Log10(sigma)))) * Math.Pow(10, Math.Round(Math.Log10(sigma))) / 10;
            double xmin = Math.Min(lsl, center - xStep * 5);
            double xmax = Math.Max(usl, center + xStep * 5);
            double xStart = average + Math.Round((xmin - center) / xStep, 0) * xStep;
            double xEnd = average + Math.Round((xmax - center) / xStep, 0) * xStep;

            for (double i = xStart; i < xEnd; i += (xEnd - xStart) / 50)
            {
                double yVal = 1 / (Math.Sqrt(2 * Math.PI) * sigma) * Math.Pow(Math.E, -(i - average) * (i - average) / (2 * sigma * sigma));
                chart1.Series["正态曲线"].Points.AddXY(i, yVal * total * xStep);
                yMax = yMax > yVal * total * xStep ? yMax : yVal * total * xStep;
            }
            #endregion 正态分布图
            #region 直方图

            this.chart1.ChartAreas["NormDist"].AxisX.Interval = xStep;
            double[] itemVals = new double[(int)((xEnd - xStart) / xStep) + 2];
            int count = 0;
            for (int i = 0; i < _data.Length; i++)
            {
                count = 0;
                for (double x = xStart; x <= xEnd; x += xStep)
                {
                    if (x - xStep / 2 < _data[i] && x + xStep / 2 >= _data[i])
                    {
                        itemVals[count]++;
                        break;
                    }
                    count++;
                }
            }
            count = 0;
            for (double x = xStart; x <= xEnd; x += xStep)
            {
                yMax = yMax > itemVals[count] ? yMax : itemVals[count];
                this.chart1.Series["Column"].Points.AddXY(x, itemVals[count]);
                //this.chart1.Series["Column"].Points[this.chart1.Series["Column"].Points.Count - 1].AxisLabel = x.ToString("f1");
                count++;
            }
            #endregion 直方图
            #region 标准线

            this.chart1.Series["limit"].Points.AddXY(usl, 0, 0, yMax * 0.99);
            this.chart1.Series["limit"].Points.AddXY(lsl, 0, 0, yMax * 0.99);
            this.chart1.Series["limit"].Points[0].Label = usl.ToString("f2");
            this.chart1.Series["limit"].Points[1].Label = lsl.ToString("f2");

            this.chart1.Series["Sigma"].Points.AddXY(double.Parse(this.tb_P3Sigma.Text), 0, 0, yMax * 0.7);
            this.chart1.Series["Sigma"].Points.AddXY(double.Parse(this.tb_M3Sigma.Text), 0, 0, yMax * 0.7);
            this.chart1.Series["Sigma"].Points[0].Label = "+3σ";
            this.chart1.Series["Sigma"].Points[1].Label = "-3σ";

            this.chart1.Series["Target"].Points.AddXY(double.Parse(this.textBox_Target.Text), 0, 0, yMax * 1.1);
            this.chart1.Series["Target"].Points[0].Label = this.textBox_Target.Text;
            #endregion 标准线
        }

        private void toolStripMenuItem_Common_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem)
            {
                try
                {
                    ToolStripMenuItem tmi = sender as ToolStripMenuItem;
                    tmi.Checked = !tmi.Checked;
                }
                catch (Exception exp)
                {
                    this.InvokeOnLog(new MsgEventArgs(exp.Message));
                }
            }
        }
        private double NormSDist(double x)
        {
            // this guards against overflow
            const double gamma = 0.231641900,
                          a1 = 0.319381530,
                          a2 = -0.356563782,
                          a3 = 1.781477973,
                          a4 = -1.821255978,
                          a5 = 1.330274429;
            double k = 1.0 / (1 + Math.Abs(x) * gamma);
            double n = k * (a1 + k * (a2 + k * (a3 + k * (a4 + k * a5))));
            n = 1 - Normal(x) * n;
            if (x < 0)
                return 1.0 - n;
            return n;
        }
        private double Normal(double x)
        {
            return 1 / Math.Sqrt(2 * Math.PI) * Math.Exp(-x * x / 2);
        }

        private void toolStripMenuItem_Save_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "xls|*.xls";
                sfd.FileName = "temp.xls";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    StreamWriter fs = new StreamWriter(sfd.FileName);
                    foreach(double d in _data)
                    {
                        fs.WriteLine(d);
                    }
                    fs.Flush();
                    fs.Close();
                }
            }
            catch (Exception exp)
            {
                InvokeOnLog(new MsgEventArgs(exp.Message));
            }
        }

        private void ToolStripMenuItem_CopyBmp_Click(object sender, EventArgs e)
        {
            this.Refresh();
            Bitmap bmp = CreateBitmap(this.splitContainer1);
            Clipboard.SetImage(bmp);
        }

        private void ToolStripMenuItem_CopyData_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            foreach (double d in _data)
            {
                sb.Append(d + "\n");
            }
            Clipboard.SetText(sb.ToString());
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            ParameterSetting dlg = new ParameterSetting(_db);
            if (dlg.ShowDialog(this) == DialogResult.Cancel)
                return;
            Properties.Settings.Default.AutoWildcard = dlg.wpf.AddWildcard;
            Properties.Settings.Default.DefaultRemoveRepeat = dlg.wpf.RemoveRepeatData;
            Properties.Settings.Default.DefaultRemovePassData = dlg.wpf.RemovePassData;
            Properties.Settings.Default.DefaultRemoveFailData = dlg.wpf.RemoveFailData;
            Properties.Settings.Default.DefaultRemoveSpecialData = dlg.wpf.RemoveExceptData;
            Properties.Settings.Default.DefaultDateTimeStart = dlg.wpf.StartTime;
            Properties.Settings.Default.DefaultDateTimeEnd = dlg.wpf.EndTime;
            Properties.Settings.Default.AbnormalUpData = dlg.wpf.MaxValue;
            Properties.Settings.Default.AbnormalLowData = dlg.wpf.MinValue;
            Properties.Settings.Default.ProductType = dlg.wpf.ProductType;
            Properties.Settings.Default.DefaultTestBench = dlg.wpf.TestBench;
            Properties.Settings.Default.DefaultTestItem = dlg.wpf.TestItem;
            this.dateTimePicker_Start.Value = dlg.wpf.StartTime;
            this.dateTimePicker_End.Value = dlg.wpf.EndTime;
            Properties.Settings.Default.Save();

            DlgWaiting wait = new DlgWaiting();
            wait.OnAction += dlg_OnAction;
            wait.ShowDialog();

            //this.textBox_LSL.Text = this._lsl;
            //this.textBox_USL.Text = this._usl;
            //this.textBox_Target.Text = this._target;
            //this.tb_Sigma.Text = this._sigma;
            //this.tb_M3Sigma.Text = this._m3Sigma;
            //this.tb_P3Sigma.Text = this._p3Sigma;
            //this.tb_Average.Text = this._average;
        }

        void dlg_OnAction(object sender, EventArgs e)
        {
            try
            {
                this.ReadDataFromDatabase();
                UpdateControl();
                this.DrawChart();
            }
            catch (Exception exp)
            {
                this.Invoke(new Action(delegate()
                    {
                        InvokeOnLog(new MsgEventArgs(exp.Message));
                    }));
            }
        }

        private void UpdateControl()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(UpdateControl));
                return;
            }
            this.textBox_Target.Text = _target;
            this.textBox_USL.Text = _usl;
            this.textBox_LSL.Text = _lsl;
            this.tb_Max.Text = _max;
            this.tb_Min.Text = _min;
        }

        private void ParameterSetting_Click(object sender, EventArgs e)
        {
            RefreshButton_Click(sender, e);
        }

    }
}
