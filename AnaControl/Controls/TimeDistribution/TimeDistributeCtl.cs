﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Data.OleDb;


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
            List<DateTime> types = new List<DateTime>();
            string sqlCmd = "select distinct(t1.TEST_TIME) from TEST_TIME_DISTRIBUTION t1,"
                + "test_results t2 "
                + "where t1.test_time >= "
                + "#" + Properties.Settings.Default.DefaultDateTimeStart.ToString("yyyy-MM-dd HH:mm:ss") + "#"
                + "and t1.test_time < "
                + "#" + Properties.Settings.Default.DefaultDateTimeEnd.ToString("yyyy-MM-dd HH:mm:ss") + "#"
                + "and t1.product_sn = t2.product_sn "
                + "and t1.test_time = t2.test_time "
                + "and t2.STATION like '%" + Properties.Settings.Default.DefaultTestBench + "%' "
                + "and t2.PRODUCT_NAME like '%" + Properties.Settings.Default.ProductType + "%' ";
            DataTable dt = _db.GetDataTable(sqlCmd);
            foreach (DataRow row in dt.Rows)
            {
                if (row["TEST_TIME"] is DateTime)
                {
                    types.Add((DateTime)row["TEST_TIME"]);
                }
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

            this.Invoke(new Action(delegate()
            {
                this.chart1.Series.Add("TotalTime");
                this.chart1.Series["TotalTime"].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), Properties.Settings.Default.DefaultChartType);
            }));

            sqlCmd = "select distinct(ITEM_NAME) from TEST_TIME_DISTRIBUTION t1,"
                + "test_results t2 "
                + "where t1.test_time >= "
                + "#" + Properties.Settings.Default.DefaultDateTimeStart.ToString("yyyy-MM-dd HH:mm:ss") + "#"
                + "and t1.test_time < "
                + "#" + Properties.Settings.Default.DefaultDateTimeEnd.ToString("yyyy-MM-dd HH:mm:ss") + "#"
                + "and t1.product_sn = t2.product_sn "
                + "and t1.test_time = t2.test_time "
                + "and t2.STATION like '%" + Properties.Settings.Default.DefaultTestBench + "%' "
                + "and t2.PRODUCT_NAME like '%" + Properties.Settings.Default.ProductType + "%' "
                + "and t1.item_name <> 'TotalTime' ";
            dt = _db.GetDataTable(sqlCmd);
            foreach (DataRow row in dt.Rows)
            {
                this.Invoke(new Action(delegate()
                {
                    this.chart1.Series.Add(row[0].ToString().Trim());
                    this.chart1.Series[row[0].ToString().Trim()].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), Properties.Settings.Default.DefaultChartType);
                }));
            }
            dt.Dispose();

            foreach (DateTime tt in types)
            {
                sqlCmd = "select t1.* from TEST_TIME_DISTRIBUTION t1,"
                    + "test_results t2 "
                    + "where t1.test_time = ? "
                    + "and t1.product_sn = t2.product_sn "
                    + "and t1.test_time = t2.test_time "
                    + "and t2.STATION like '%" + Properties.Settings.Default.DefaultTestBench + "%' "
                    + "and t2.PRODUCT_NAME like '%" + Properties.Settings.Default.ProductType + "%' ";
                using (OleDbCommand odc = new OleDbCommand(sqlCmd, _db.Conn))
                {
                    odc.Parameters.Add("@1", OleDbType.Date);
                    odc.Parameters[0].Value = tt;
                    OleDbDataAdapter oda = new OleDbDataAdapter(odc);
                    dt = new DataTable("DISTRIBUTION");
                    oda.Fill(dt);
                    oda.Dispose();
                    this.Invoke(new Action(delegate()
                        {
                            foreach (Series se in this.chart1.Series)
                            {
                                se.Points.Add(0);
                            }
                        }));
                    foreach (DataRow row in dt.Rows)
                    {
                        string typeName = row["ITEM_NAME"].ToString().Trim();
                        this.Invoke(new Action(delegate()
                            {
                                this.chart1.Series[typeName].Points[
                                    this.chart1.Series[typeName].Points.Count - 1].SetValueY((double)row["USED_TIME"]);
                                this.chart1.Series[typeName].Points[
                                    this.chart1.Series[typeName].Points.Count - 1].BorderWidth = 5;

                            }));
                    }
                }
            }
        }
    }
}
