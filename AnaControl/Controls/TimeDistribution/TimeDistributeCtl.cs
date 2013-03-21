using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace AnaControl.Controls.TimeDistribution
{
    [DisplayName("测试时间分析")]
    public partial class TimeDistributeCtl : AbstrctAnalyzer
    {
        public TimeDistributeCtl()
        {
            InitializeComponent();
        }

        protected override void Setting()
        {
            TimeDistributeSettings dlg = new TimeDistributeSettings();
            if (dlg.ShowDialog(this) == DialogResult.Cancel)
                return;
        }

        protected override void RefreshChart()
        {
            List<string> types = new List<string>();
            string sqlCmd = "select distinct(ITEM_NAME) from TEST_TIME_DISTRIBUTION "
                + "where test_time >= "
                + "#" + Properties.Settings.Default.DefaultDateTimeStart.ToString("yyyy-MM-dd HH:mm:ss") + "#"
                + "and test_time < "
                + "#" + Properties.Settings.Default.DefaultDateTimeEnd.ToString("yyyy-MM-dd HH:mm:ss") + "#";
            DataTable dt = _db.GetDataTable(sqlCmd);
            foreach (DataRow row in dt.Rows)
            {
                types.Add(row["ITEM_NAME"].ToString().Trim());
            }
            dt.Dispose();

            this.Invoke(new Action(delegate()
                {
                    this.chart1.Annotations.Clear();
                    this.chart1.Titles.Clear();
                    this.chart1.ChartAreas.Clear();
                    this.chart1.Series.Clear();
                    this.chart1.Legends.Clear();

                    this.chart1.ChartAreas.Add("测试项时间分布");
                    this.chart1.Legends.Add("测试项时间分布");
                    this.chart1.Legends[0].BackColor = SystemColors.AppWorkspace;
                    this.chart1.ChartAreas[0].BackColor = SystemColors.AppWorkspace;
                    this.chart1.Titles.Add("测试项时间分布");
                    this.chart1.Titles[0].Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
                }));
            sqlCmd = "select avg(USED_TIME) average from TEST_TIME_DISTRIBUTION "
                    + "where test_time >= "
                    + "#" + Properties.Settings.Default.DefaultDateTimeStart.ToString("yyyy-MM-dd HH:mm:ss") + "# "
                    + " and test_time < "
                    + "#" + Properties.Settings.Default.DefaultDateTimeEnd.ToString("yyyy-MM-dd HH:mm:ss") + "# "
                    + " and ITEM_NAME = '"
                    + "TotalTime"
                    + "'";
            double totalTime = 0;
            dt = _db.GetDataTable(sqlCmd);
            totalTime = (double)dt.Rows[0]["average"];
            foreach (string tt in types)
            {
                sqlCmd = "select USED_TIME from TEST_TIME_DISTRIBUTION "
                    + "where test_time >= "
                    + "#" + Properties.Settings.Default.DefaultDateTimeStart.ToString("yyyy-MM-dd HH:mm:ss") + "# "
                    + " and test_time < "
                    + "#" + Properties.Settings.Default.DefaultDateTimeEnd.ToString("yyyy-MM-dd HH:mm:ss") + "# "
                    + " and ITEM_NAME = '"
                    + tt
                    + "'";
                dt = _db.GetDataTable(sqlCmd);
                this.Invoke(new Action(delegate()
                    {
                        this.chart1.Series.Add(tt);
                        this.chart1.Series[tt].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                        foreach (DataRow row in dt.Rows)
                        {
                            this.chart1.Series[tt].Points.Add((float)row["USED_TIME"]);
                        }
                    }));
                sqlCmd = "select avg(USED_TIME) average from TEST_TIME_DISTRIBUTION "
                    + "where test_time >= "
                    + "#" + Properties.Settings.Default.DefaultDateTimeStart.ToString("yyyy-MM-dd HH:mm:ss") + "# "
                    + " and test_time < "
                    + "#" + Properties.Settings.Default.DefaultDateTimeEnd.ToString("yyyy-MM-dd HH:mm:ss") + "# "
                    + " and ITEM_NAME = '"
                    + tt
                    + "'";
                dt = _db.GetDataTable(sqlCmd);
                this.Invoke(new Action(delegate()
                    {
                        this.chart1.Series[tt].LegendText = tt + "占总时间比为: " + (double)dt.Rows[0]["average"] / totalTime * 100 + "%";
                    }));
            }
        }
    }
}
