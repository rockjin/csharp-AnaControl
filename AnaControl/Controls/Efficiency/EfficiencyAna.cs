using AnaControl.Dlgs;
using AnaControl.Scripts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace AnaControl.Controls.Efficiency
{
    [DisplayName("效率分析")]
    internal class EfficiencyAna : AbstrctAnalyzer
    {
        protected override void RefreshChart()
        {
            List<long> types = new List<long>();
            Dictionary<string, double> timeDistribute = new Dictionary<string, double>();
            string sqlCmd = "select distinct(t1.TEST_ID) from TEST_TIME_DISTRIBUTION t1,"
                + " test_results t2 "
                + " where t2.test_time >= "
                + " #" + Properties.Settings.Default.DefaultDateTimeStart.ToString("yyyy-MM-dd HH:mm:ss") + "# "
                + " and t2.test_time < "
                + " #" + Properties.Settings.Default.DefaultDateTimeEnd.ToString("yyyy-MM-dd HH:mm:ss") + "# "
                + " and t1.test_id = t2.test_id "
                + " and t2.STATION like '%" + Properties.Settings.Default.DefaultTestBench + "%' "
                + " and t2.PRODUCT_NAME like '%" + Properties.Settings.Default.ProductType + "%' "
                + " and (t2.test_id in( "
                + " select test_id from ("
                + " select test_id,count(test_id) as num from test_item_values "
                + " group by test_id"
                + " ) t1_1,"
                + " ("
                + " select max(num) as maxNum from "
                + " ("
                + " select count(st1.test_id) as num "
                + " from test_item_values st1, test_results st2 "
                + " where st2.test_time >= "
                + " #" + Properties.Settings.Default.DefaultDateTimeStart.ToString("yyyy-MM-dd HH:mm:ss") + "# "
                + " and st2.test_time < "
                + " #" + Properties.Settings.Default.DefaultDateTimeEnd.ToString("yyyy-MM-dd HH:mm:ss") + "# "
                + " and st2.STATION like '%" + Properties.Settings.Default.DefaultTestBench + "%' "
                + " and st2.PRODUCT_NAME like '%" + Properties.Settings.Default.ProductType + "%' "
                + " and st1.test_id = st2.test_id "
                + " group by st1.test_id"
                + ") as tab1) as t2_1"
                + " where t1_1.num = t2_1.maxNum "
                + " ))";
            DataTable dt = _db.GetDataTable(sqlCmd);
            int TotalNumber = Properties.Settings.Default.DefaultTestDataUpLimit;

            foreach (DataRow row in dt.Rows)
            {
                types.Add(long.Parse(row["TEST_ID"].ToString()));
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
                this.chart1.ChartAreas[0].BackColor = SystemColors.AppWorkspace;
                this.chart1.Titles.Add("测试项时间分布");
                this.chart1.Titles[0].Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            }));
            
            timeDistribute.Add("TimeSpan", 0);
            timeDistribute.Add("TotalTime", 0);
            timeDistribute.Add("频谱使用时长", 0);
            timeDistribute.Add("信号源使用时长", 0);
            timeDistribute.Add("频谱仪等待时长", 0);
            timeDistribute.Add("信号源等待时长", 0);

            DateTime timeValidFirst, timeValidLast;

            sqlCmd = "select * from TEST_TIME_DISTRIBUTION t1,"
                + "test_results t2 "
                + "where t2.test_time >= "
                + "#" + Properties.Settings.Default.DefaultDateTimeStart.ToString("yyyy-MM-dd HH:mm:ss") + "#"
                + "and t2.test_time < "
                + "#" + Properties.Settings.Default.DefaultDateTimeEnd.ToString("yyyy-MM-dd HH:mm:ss") + "#"
                + "and t1.test_id = t2.test_id "
                + "and t2.STATION like '%" + Properties.Settings.Default.DefaultTestBench + "%' "
                + "and t2.PRODUCT_NAME like '%" + Properties.Settings.Default.ProductType + "%' "
                + "order by t2.test_time";
            dt = _db.GetDataTable(sqlCmd);
            timeValidFirst = (DateTime)dt.Rows[0]["TEST_TIME"];
            timeValidLast = (DateTime)dt.Rows[dt.Rows.Count - 1]["TEST_TIME"];
            double lastTimeLong = (double)dt.Rows[dt.Rows.Count - 1]["USED_TIME"];
            TimeSpan lastTimeSpan = new TimeSpan((long)(lastTimeLong * 60 * 60 * 1000 * 1000 * 10));
            timeValidLast = timeValidLast + lastTimeSpan;

            this.Invoke(new Action(() =>
            {
                this.chart1.Titles[0].Text += string.Format("   有效时长({0}  --  {1})  ({2}小时)",
                    timeValidFirst.ToString("yyyy-MM-dd HH:mm:ss"),
                    timeValidLast.ToString("yyyy-MM-dd HH:mm:ss"),
                    (timeValidLast - timeValidFirst).TotalHours.ToString("0.0000"));
            }));

            timeDistribute["TimeSpan"] = (timeValidLast - timeValidFirst).TotalHours;

            this.Invoke(new Action(delegate()
            {
                this.chart1.Series.Add("TimeDistribute");
            }));

            foreach (int tt in types)
            {
                sqlCmd = "select t1.* from TEST_TIME_DISTRIBUTION t1,"
                    + "test_results t2 "
                    + "where t1.test_id = ? "
                    + "and t1.test_id = t2.test_id "
                    + "and t2.STATION like '%" + Properties.Settings.Default.DefaultTestBench + "%' "
                    + "and t2.PRODUCT_NAME like '%" + Properties.Settings.Default.ProductType + "%' ";
                using (OleDbCommand odc = new OleDbCommand(sqlCmd, _db.Conn))
                {
                    odc.Parameters.Add("@1", OleDbType.Integer);
                    odc.Parameters[0].Value = tt;
                    OleDbDataAdapter oda = new OleDbDataAdapter(odc);
                    dt = new DataTable("DISTRIBUTION");
                    oda.Fill(dt);
                    oda.Dispose();
                    var totalRow = dt.AsEnumerable().First((row) =>
                    {
                        if (row["ITEM_NAME"].ToString().Trim() == "TotalTime") return true;
                        return false;
                    });
                    double totalTestTime = double.Parse(totalRow["USED_TIME"].ToString());
                    if (totalTestTime < Properties.Settings.Default.DefaultMinTestTime
                        || totalTestTime > Properties.Settings.Default.DefaultMaxTestTime)
                        continue;

                    foreach (DataRow row in dt.Rows)
                    {
                        string typeName = row["ITEM_NAME"].ToString().Trim();
                        if (!timeDistribute.ContainsKey(typeName.Trim())) continue;
                        timeDistribute[typeName.Trim()] += double.Parse(row["USED_TIME"].ToString()) / totalTestTime;
                    }
                }
            }
            
            foreach (string typeName in timeDistribute.Keys)
            {
                this.Invoke(new Action(delegate()
                {
                    this.chart1.Series[0].Points.Add(timeDistribute[typeName]);
                    TimeSpan ts = new TimeSpan((long)(timeDistribute[typeName] * 3600 * 1000 * 1000 * 10));
                    string label = string.Format("{0}：{1}\n占总时长的{2}%", 
                        typeName, 
                        ts.ToString(@"d\-h\:m\:s"), 
                        (timeDistribute[typeName]/timeDistribute["TimeSpan"]*100).ToString("0.00"));
                    this.chart1.Series[0].Points[this.chart1.Series[0].Points.Count - 1].Label = label;
                }));
            }
        }
        private void AddMenuItems()
        {
            this._contentMenuEx.Clear();
            ToolStripMenuItem mi = new ToolStripMenuItem("曲线类型");
            mi.Name = mi.Text;
            this._contentMenuEx.Add(mi);
            ToolStripMenuItem allItem = new ToolStripMenuItem("全选");
            allItem.Checked = true;
            allItem.Click += (sender, args) =>
            {
                allItem.Checked = !allItem.Checked;
                if (!allItem.Checked)
                {
                    foreach (Series se in chart1.Series)
                    {
                        if (!_bkSeries.Contains(se))
                        {
                            _bkSeries.Add(se);
                        }
                    }
                    chart1.Series.Clear();
                    foreach (ToolStripMenuItem tmp in mi.DropDownItems)
                    {
                        tmp.Checked = false;
                    }
                }
                else
                {
                    foreach (Series se in _bkSeries)
                    {
                        if (!chart1.Series.Contains(se))
                        {
                            chart1.Series.Add(se);
                        }
                    }
                    foreach (ToolStripMenuItem tmp in mi.DropDownItems)
                    {
                        tmp.Checked = true;
                    }
                }
            };
            mi.DropDownItems.Insert(0, allItem);
            foreach (Series si in chart1.Series)
            {
                ToolStripMenuItem tsb = new ToolStripMenuItem(si.Name);
                tsb.Checked = true;
                tsb.Click += (obj, args) =>
                {
                    ToolStripMenuItem tmi = obj as ToolStripMenuItem;
                    tmi.Checked = !tmi.Checked;
                    if (tmi == null) return;
                    if (tmi.Checked)
                    {
                        Series se = _bkSeries.Find(s => s.Name == tmi.Text);
                        if (se != null)
                        {
                            if (!chart1.Series.Contains(se))
                            {
                                chart1.Series.Add(se);
                            }
                        }
                    }
                    else
                    {
                        Series se = chart1.Series.FindByName(tmi.Text);
                        if (se != null)
                        {
                            if (!_bkSeries.Contains(se))
                            {
                                _bkSeries.Add(se);
                            }
                        }
                        chart1.Series.Remove(se);
                    }
                };

                mi.DropDownItems.Add(tsb);
            }
        }

        protected override bool Setting()
        {
            base.Setting();
            ParameterSetting dlg = new ParameterSetting(_db);
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
