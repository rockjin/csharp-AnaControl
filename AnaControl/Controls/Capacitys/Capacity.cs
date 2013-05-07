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
using AnaControl.Dlgs;
using AnaControl.Utils;
using System.ComponentModel;

namespace AnaControl.Controls.Capacitys
{
    [DisplayName("产能分析")]
    public partial class Capacity : AbstrctAnalyzer
    {
        public Capacity()
        {
            InitializeComponent();
        }

        private void DrawChartCapacity()
        {
            _db.RemoveRepeat = Properties.Settings.Default.DefaultRemoveRepeat;
            CultureInfo ci = new CultureInfo("en-us");
            _rotateColor = (int)KnownColor.Red;
            this.Invoke(new Action(delegate(){
                this.chart1.ChartAreas.Clear();
                this.chart1.Legends.Clear();
                this.chart1.ChartAreas.Add("capacity");
                this.chart1.ChartAreas[0].BackColor = SystemColors.AppWorkspace;
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
            }));

            TimeSpan ts = _lastestRecordTime - Properties.Settings.Default.DefaultDateTimeStart;
            TimeSpan tsStep = new TimeSpan(ts.Ticks / (long)Properties.Settings.Default.DefaultShowNum);
            for (DateTime s = Properties.Settings.Default.DefaultDateTimeStart; s < _lastestRecordTime; s += tsStep)
            {
                s = s > _lastestRecordTime ? _lastestRecordTime : s;
                _db.time_start = s;
                _db.time_end = (s + tsStep) < _lastestRecordTime ? (s + tsStep) : _lastestRecordTime;
                int count;
                int val = this.chart1.Series["Total"].Points.Count;
                _db.GetTotalCount(out count);
                this.Invoke(new Action(delegate()
                {
                    this.chart1.Series["Total"].Points.Add(count);
                    this.chart1.Series["Total"].Points[val].Label = count.ToString();
                    this.chart1.Series["Total"].Points[val].AxisLabel = s.ToString() + "\n" + _db.time_end.ToString();
                }));

                _db.GetPassCount(out count);
                this.Invoke(new Action(delegate()
                {
                    this.chart1.Series["Pass"].Points.Add(count);
                    this.chart1.Series["Pass"].Points[val].Label = count.ToString();
                    this.chart1.Series["Pass"].Points[val].AxisLabel = s.ToString() + "\n" + _db.time_end.ToString();
                }));

                _db.GetFailCount(out count);
                this.Invoke(new Action(delegate()
                {
                    this.chart1.Series["Fail"].Points.Add(count);
                    this.chart1.Series["Fail"].Points[val].Label = count.ToString();
                    this.chart1.Series["Fail"].Points[val].AxisLabel = s.ToString() + "\n" + _db.time_end.ToString();
                    this.chart1.Series["Fail"].Points[val].ToolTip = this.chart1.Series["Fail"].Points[val].Label;
                }));
            }
        }

        private void DrawChartCapacity2()
        {
            _db.RemoveRepeat = Properties.Settings.Default.DefaultRemoveRepeat;
            CultureInfo ci = new CultureInfo("en-us");
            _rotateColor = (int)KnownColor.Red;
            

            TimeSpan ts = _lastestRecordTime - Properties.Settings.Default.DefaultDateTimeStart;
            TimeSpan tsStep = new TimeSpan(ts.Ticks / (long)Properties.Settings.Default.DefaultShowNum);
            int i = 0;
            this.Invoke(new Action(delegate()
                {
                    this.chart1.ChartAreas.Clear();
                    this.chart1.Series.Clear();
                    this.chart1.Legends.Clear();
                    this.chart1.Titles.Clear();
                    this.chart1.Annotations.Clear();
                }));
            for (DateTime s = Properties.Settings.Default.DefaultDateTimeStart; s < _lastestRecordTime; s += tsStep)
            {
                this.Invoke(new Action(delegate()
                {
                    this.chart1.ChartAreas.Add("c" + i.ToString());
                    this.chart1.ChartAreas[i].BackColor = SystemColors.AppWorkspace;
                    this.chart1.ChartAreas[i].AxisX.Title = "产能分析";
                    SeriesChartType sct =
                        (SeriesChartType)Enum.Parse(typeof(SeriesChartType), Properties.Settings.Default.DefaultChartType);
                    this.chart1.Series.Add(new Series("Total" + i.ToString()));
                    this.chart1.Series["Total" + i.ToString()].ChartArea = this.chart1.ChartAreas[i].Name;
                    this.chart1.Series["Total" + i.ToString()].ChartType = sct;
                }));
                //this.chart1.Series.Add(new Series("Pass" + i.ToString()));
                //this.chart1.Series["Pass" + i.ToString()].ChartArea = this.chart1.ChartAreas[i].Name;
                //this.chart1.Series["Pass" + i.ToString()].ChartType = sct;
                //this.chart1.Series.Add(new Series("Fail" + i.ToString()));
                //this.chart1.Series["Fail" + i.ToString()].ChartArea = this.chart1.ChartAreas[i].Name;
                //this.chart1.Series["Fail" + i.ToString()].ChartType = sct;
                //this.chart1.Series["Total" + i.ToString()].Points.Clear();
                //this.chart1.Series["Pass" + i.ToString()].Points.Clear();
                //this.chart1.Series["Fail" + i.ToString()].Points.Clear();

                //this.chart1.Series["Total" + i.ToString()].Color = Color.Brown;
                //this.chart1.Series["Pass" + i.ToString()].Color = Color.Green;
                //this.chart1.Series["Fail" + i.ToString()].Color = Color.Red;


                s = s > _lastestRecordTime ? _lastestRecordTime : s;
                _db.time_start = s;
                _db.time_end = (s + tsStep) < _lastestRecordTime ? (s + tsStep) : _lastestRecordTime;
                int count;
                int val = this.chart1.Series["Total" + i.ToString()].Points.Count;
                _db.GetTotalCount(out count);
                this.Invoke(new Action(delegate()
                {
                    this.chart1.Series["Total" + i.ToString()].Points.Add(count);
                    this.chart1.Series["Total" + i.ToString()].Points[val].Color = Color.Brown;
                    this.chart1.Series["Total" + i.ToString()].Points[val].Label = count.ToString();
                    this.chart1.Series["Total" + i.ToString()].Points[val].AxisLabel = s.ToString() + "\n" + _db.time_end.ToString();
                }));

                _db.GetPassCount(out count);
                this.Invoke(new Action(delegate()
                {
                    val = this.chart1.Series["Total" + i.ToString()].Points.Count;
                    this.chart1.Series["Total" + i.ToString()].Points.Add(count);
                    this.chart1.Series["Total" + i.ToString()].Points[val].Color = Color.Green;
                    this.chart1.Series["Total" + i.ToString()].Points[val].Label = count.ToString();
                    this.chart1.Series["Total" + i.ToString()].Points[val].AxisLabel = s.ToString() + "\n" + _db.time_end.ToString();
                }));

                _db.GetFailCount(out count);
                this.Invoke(new Action(delegate()
                {
                    val = this.chart1.Series["Total" + i.ToString()].Points.Count;
                    this.chart1.Series["Total" + i.ToString()].Points.Add(count);
                    this.chart1.Series["Total" + i.ToString()].Points[val].Color = Color.Red;
                    this.chart1.Series["Total" + i.ToString()].Points[val].Label = count.ToString();
                    this.chart1.Series["Total" + i.ToString()].Points[val].AxisLabel = s.ToString() + "\n" + _db.time_end.ToString();
                }));
                //this.chart1.Series["Total" + i.ToString()].Points[val].ToolTip = this.chart1.Series["Total" + i.ToString()].Points[val].Label;
                i++;
            }
        }

        private void DrawChart(string DataType)
        {
            CultureInfo ci = new CultureInfo("en-us");
            _rotateColor = (int)KnownColor.Red;
            try
            {
                #region ////////////////////////////////产能分析////////////////////////////////////////

                var st = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), Properties.Settings.Default.DefaultChartType);
                switch (st)
                {
                    case SeriesChartType.Pie:
                        this.DrawChartCapacity2();
                        break;
                    default:
                        this.DrawChartCapacity();
                        break;
                }
                #endregion
                
            }
            catch (System.Exception e)
            {
                InvokeOnLog((MsgEventArgs)e.Message);
            }
            this.Invoke(new Action(this.chart1.Refresh));
            //////////////////End Draw Pie/////////////////////////
        }

        protected override void RefreshChart()
        {
            string strCmd;
            strCmd = string.Format("Select max(test_time), min(test_time) from TEST_RESULTS");
            DataTable dt = _db.GetDataTable(strCmd);
            if (dt.Rows.Count > 0)
            {
                _lastestRecordTime = DateTime.Parse(dt.Rows[0][0].ToString()) + new TimeSpan(1, 0, 0, 0);
                _firstRecordTime = DateTime.Parse(dt.Rows[0][1].ToString());
                _lastestRecordTime = Properties.Settings.Default.DefaultDateTimeEnd > _lastestRecordTime ? _lastestRecordTime : Properties.Settings.Default.DefaultDateTimeEnd;
                _firstRecordTime = Properties.Settings.Default.DefaultDateTimeStart < _firstRecordTime ? _firstRecordTime : Properties.Settings.Default.DefaultDateTimeStart;
            }
            dt.Clear();
            DrawChart(Properties.Settings.Default.DefaultDataType);
        }

        protected override bool Setting()
        {
            base.Setting();
            CapacitySettings dlg = new CapacitySettings();
            if (dlg.ShowDialog(this) == DialogResult.Cancel) return false;
            if (this._db != null)
            {
                _db.time_start = Properties.Settings.Default.DefaultDateTimeStart;
                _db.time_end = Properties.Settings.Default.DefaultDateTimeEnd;
                _db.TestBench = Properties.Settings.Default.DefaultTestBench;
                _db.ProductType = Properties.Settings.Default.ProductType;
            }
            return true;
        }
    }
}
