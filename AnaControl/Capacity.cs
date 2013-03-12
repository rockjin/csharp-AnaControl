// *******************************************************************************
// *  COPYRIGHT DaTang Mobile Communications Equipment CO.,LTD                   *
// *******************************************************************************
// Functions:   
// Note:        
// 
// History:
// Date         Ahthor      Description
// ------------------------------------------------------------------------------
// 2010-03-15   jinfangxiao      Create this file
// *******************************************************************************/
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.Globalization;
using System.Data;
using System.Drawing.Imaging;
using System.Windows.Forms.DataVisualization.Charting;
using DbDriver;

namespace AnaControl
{
    public partial class Capacity : UserControl
    {
        private static int _rotateColor = (int)KnownColor.Red;
        private DateTime _lastestRecordTime = Properties.Settings.Default.DefaultDateTimeEnd;

        #region log informations
        private void LogFunc(object sender, EventArgs e)
        {
            Debug.WriteLine(e.ToString());
        }
        public EventHandler OnLog = null;
        private void InvokeOnLog(EventArgs e)
        {
            if (OnLog == null) return;
            OnLog(this, e);
        }
        #endregion
        public Capacity()
        {
            InitializeComponent();
        }
        private void tsbSave_Click(object sender, EventArgs e)
        {
            try
            {
                this.Refresh();
                Bitmap bmp = this.CreateBitmap(this);
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "bmp|*.bmp";
                sfd.FileName = "temp.bmp";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    //chart1.SaveImage(sfd.FileName, ChartImageFormat.Jpeg);
                    bmp.Save(sfd.FileName);
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
                return;
            }
        }
        /// <summary>
        /// 绘制整个控件位BitMap
        /// </summary>
        /// <param name="Control">要绘制的控件</param>
        /// <returns></returns>
        private Bitmap CreateBitmap(Control trl)
        {
            Graphics gDest;
            //IntPtr hdcDest;
            //int hdcSrc;
            //int hWnd = trl.Handle.ToInt32();
            Bitmap bmpDrawed = new Bitmap(trl.Width, trl.Height);
            gDest = Graphics.FromImage(bmpDrawed);
            //hdcSrc = Win32.GetWindowDC(hWnd);
            // hdcDest = gDest.GetHdc();
            gDest.CopyFromScreen(trl.PointToScreen(new Point(0, 0)), new Point(0, 0), trl.Size);

            //Win32.BitBlt(hdcDest.ToInt32(), 0, 0, trl.Width, trl.Height, hdcSrc, 0, 0, Win32.SRCCOPY);
            //gDest.ReleaseHdc(hdcDest);
            // Win32.ReleaseDC(hWnd, hdcSrc);
            return bmpDrawed;
        }

        private void toolStripMenuItem_RemoveRepeats_Click(object sender, EventArgs e)
        {
            this.toolStripMenuItem_RemoveRepeats.Checked = !this.toolStripMenuItem_RemoveRepeats.Checked;
            Properties.Settings.Default.DefaultRemoveRepeat = this.toolStripMenuItem_RemoveRepeats.Checked;
            Properties.Settings.Default.Save();
            this.DrawChart(new DbProc(), Properties.Settings.Default.DefaultDataType);
        }

        private void DrawChartCapacity(DbProc db)
        {
            db.RemoveRepeat = Properties.Settings.Default.DefaultRemoveRepeat;
            CultureInfo ci = new CultureInfo("en-us");
            _rotateColor = (int)KnownColor.Red;
            this.chart1.ChartAreas[0].AxisX.Title = "产能分析";
            this.chart1.Series.Clear();
            SeriesChartType sct =
                (SeriesChartType)Enum.Parse(typeof(SeriesChartType), Properties.Settings.Default.DefaultChartType);
            this.chart1.Series.Add(new Series("Total"));
            this.chart1.Series["Total"].ChartType = sct;
            this.chart1.Series.Add(new Series("Pass"));
            this.chart1.Series["Pass"].ChartType = sct;
            this.chart1.Series.Add(new Series("Fail"));
            this.chart1.Series["Fail"].ChartType = sct;
            this.chart1.Series["Total"].Points.Clear();
            this.chart1.Series["Pass"].Points.Clear();
            this.chart1.Series["Fail"].Points.Clear();

            this.chart1.Series[0].Color = Color.Brown;
            this.chart1.Series[1].Color = Color.Green;
            this.chart1.Series[2].Color = Color.Red;

            TimeSpan ts = _lastestRecordTime - Properties.Settings.Default.DefaultDateTimeStart;
            TimeSpan tsStep = new TimeSpan(ts.Ticks / (long)Properties.Settings.Default.DefaultShowNum);
            for (DateTime s = Properties.Settings.Default.DefaultDateTimeStart; s < _lastestRecordTime; s += tsStep)
            {
                s = s > _lastestRecordTime ? _lastestRecordTime : s;
                db.time_start = s;
                db.time_end = (s + tsStep) < _lastestRecordTime ? (s + tsStep) : _lastestRecordTime;
                int count;
                int val = this.chart1.Series["Total"].Points.Count;
                db.GetTotalCount(out count);
                this.chart1.Series["Total"].Points.Add(count);
                this.chart1.Series["Total"].Points[val].Label = count.ToString();
                this.chart1.Series["Total"].Points[val].AxisLabel = s.ToString() + "\n" + db.time_end.ToString();

                db.GetPassCount(out count);
                this.chart1.Series["Pass"].Points.Add(count);
                this.chart1.Series["Pass"].Points[val].Label = count.ToString();
                this.chart1.Series["Pass"].Points[val].AxisLabel = s.ToString() + "\n" + db.time_end.ToString();

                db.GetFailCount(out count);
                this.chart1.Series["Fail"].Points.Add(count);
                this.chart1.Series["Fail"].Points[val].Label = count.ToString();
                this.chart1.Series["Fail"].Points[val].AxisLabel = s.ToString() + "\n" + db.time_end.ToString();
                this.chart1.Series["Fail"].Points[val].ToolTip = this.chart1.Series["Fail"].Points[val].Label;
            }
        }

        private void DrawChartCapacity2(DbProc db)
        {
            db.RemoveRepeat = Properties.Settings.Default.DefaultRemoveRepeat;
            CultureInfo ci = new CultureInfo("en-us");
            _rotateColor = (int)KnownColor.Red;
            this.chart1.ChartAreas[0].AxisX.Title = "产能分析";
            this.chart1.Series.Clear();
            this.chart1.Series.Add(new Series("Total"));
            SeriesChartType sct =
                (SeriesChartType) Enum.Parse(typeof (SeriesChartType), Properties.Settings.Default.DefaultChartType);
            this.chart1.Series["Total"].ChartType = sct;
            this.chart1.Series["Total"].Points.Clear();

            TimeSpan ts = _lastestRecordTime - Properties.Settings.Default.DefaultDateTimeStart;
            TimeSpan tsStep = new TimeSpan(ts.Ticks / (long)Properties.Settings.Default.DefaultShowNum);
            for (DateTime s = Properties.Settings.Default.DefaultDateTimeStart; s < _lastestRecordTime; s += tsStep)
            {
                s = s > _lastestRecordTime ? _lastestRecordTime : s;
                db.time_start = s;
                db.time_end = (s + tsStep) < _lastestRecordTime ? (s + tsStep) : _lastestRecordTime;
                int count, sum;
                int val = this.chart1.Series["Total"].Points.Count;
                db.GetTotalCount(out count);
                this.chart1.Series["Total"].Points.Add(count);
                sum = count;
                this.chart1.Series["Total"].Points[val].Label = "总数(" + count + ") " + "100%";
                this.chart1.Series["Total"].Points[val].AxisLabel = s.ToString() + "\n" + db.time_end.ToString();
                this.chart1.Series["Total"].Points[val].Color = Color.Brown;
                val = this.chart1.Series["Total"].Points.Count;
                db.GetPassCount(out count);
                this.chart1.Series["Total"].Points.Add(count);
                this.chart1.Series["Total"].Points[val].Label = "PASS(" + count + ") " + ((double)count/sum).ToString("P");
                this.chart1.Series["Total"].Points[val].AxisLabel = s.ToString() + "\n" + db.time_end.ToString();
                this.chart1.Series["Total"].Points[val].Color = Color.Green;
                val = this.chart1.Series["Total"].Points.Count;
                db.GetFailCount(out count);
                this.chart1.Series["Total"].Points.Add(count);
                this.chart1.Series["Total"].Points[val].Label = "FAIL(" + count + ") " + ((double)count / sum).ToString("P");
                this.chart1.Series["Total"].Points[val].AxisLabel = s.ToString() + "\n" + db.time_end.ToString();
                this.chart1.Series["Total"].Points[val].Color = Color.Red;
            }
        }

        private void DrawChart(DbProc db, string DataType)
        {
            CultureInfo ci = new CultureInfo("en-us");
            _rotateColor = (int)KnownColor.Red;
            try
            {
                db.Connect();
                #region ////////////////////////////////产能分析////////////////////////////////////////

                var st = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), Properties.Settings.Default.DefaultChartType);
                switch (st)
                {
                    case SeriesChartType.Column:
                    case SeriesChartType.Bar:
                        this.DrawChartCapacity(db);
                        break;
                    default:
                        this.DrawChartCapacity2(db);
                        break;
                }
                #endregion
                
            }
            catch (System.Exception e)
            {
                InvokeOnLog((MsgEventArgs)e.Message);
            }
            db.DisConnect();
            this.chart1.Refresh();
            //////////////////End Draw Pie/////////////////////////
        }

        private void ChartTypeButton_Click(object sender, EventArgs e)
        {
            try
            {
                SeriesChartType t = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), sender.ToString());
                Properties.Settings.Default.DefaultChartType = t.ToString();
                foreach (ToolStripMenuItem item in this.toolStripDropDownButton_ChartType.DropDownItems)
                {
                    item.Checked = false;
                }
                (sender as ToolStripMenuItem).Checked = true;
                this.toolStripDropDownButton_ChartType.Text = t.ToString();
                RefreshChart();
            }
            catch (System.Exception ee)
            {
                this.chart1 = new Chart();
                InvokeOnLog((MsgEventArgs)ee.Message);
            }
            return;
        }

        public static string[] GetChartTypes
        {
            get { return Enum.GetNames(typeof(SeriesChartType)); }
        }
        public void RefreshChart()
        {
            string strCmd;
            DbProc dbproc = new DbProc();
            try
            {
                dbproc.Connect();
            }
            catch (System.Exception e)
            {
                InvokeOnLog((MsgEventArgs)e.Message);
                return;
            }

            strCmd = string.Format("Select max(test_time) from TEST_ITEM_VALUES");
            DataTable dt = dbproc.GetDataTable(strCmd);
            if (dt.Rows.Count > 0 && dt.Rows[0][0] != System.DBNull.Value)
            {
                _lastestRecordTime = DateTime.Parse(dt.Rows[0][0].ToString()) + new TimeSpan(1, 0, 0, 0);
                _lastestRecordTime = Properties.Settings.Default.DefaultDateTimeEnd > _lastestRecordTime ? _lastestRecordTime : Properties.Settings.Default.DefaultDateTimeEnd;
            }
            dt.Clear();
            DrawChart(dbproc, Properties.Settings.Default.DefaultDataType);
            dbproc.DisConnect();
        }

        private Color NextColor()
        {
            if (_rotateColor + 1 > Enum.GetValues(typeof(KnownColor)).Length)
                _rotateColor = (int)KnownColor.Red;
            Color c = Color.FromKnownColor((KnownColor)_rotateColor++);
            #region //屏蔽较白颜色
            byte min = c.R;
            min = min < c.G ? min : c.G;
            min = min < c.B ? min : c.B;
            if (min > 200)
            {
                return NextColor();
            }
            #endregion
            return c;
        }

        private void Capacity_Load(object sender, EventArgs e)
        {
            this.OnLog = this.LogFunc;
            foreach (string name in Enum.GetNames(typeof(SeriesChartType)))
            {
                this.toolStripDropDownButton_ChartType.DropDownItems.Add(name);
                toolStripDropDownButton_ChartType.DropDownItems[toolStripDropDownButton_ChartType.DropDownItems.Count - 1].Click += new EventHandler(ChartTypeButton_Click);
            }
            this.toolStripDropDownButton_ChartType.Text = Properties.Settings.Default.DefaultChartType;

            RefreshChart();
        }

        private void toolStripMenuItemFirstPassYieldRate_Click(object sender, EventArgs e)
        {

        }

    }
}
