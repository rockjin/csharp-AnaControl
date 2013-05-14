using AnaControl.Dlgs;
using AnaControl.Scripts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;


namespace AnaControl.Controls.TimeDistribution
{
    [DisplayName("测试时间分析")]
    public partial class TimeDistributeCtl : AbstrctAnalyzer
    {
        public TimeDistributeCtl()
        {
            InitializeComponent();
        }

        protected override bool Setting()
        {
            ParameterSetting dlg = new ParameterSetting(_db);
            if (dlg.ShowDialog(this) == DialogResult.Cancel)
                return false;
            _bkSeries.Clear();
            return true;
        }

        protected override void RefreshChart()
        {
            List<long> types = new List<long>();
            string sqlCmd = "select distinct(t1.TEST_ID) from TEST_TIME_DISTRIBUTION t1,"
                + "test_results t2 "
                + "where t2.test_time >= "
                + "#" + Properties.Settings.Default.DefaultDateTimeStart.ToString("yyyy-MM-dd HH:mm:ss") + "#"
                + "and t2.test_time < "
                + "#" + Properties.Settings.Default.DefaultDateTimeEnd.ToString("yyyy-MM-dd HH:mm:ss") + "#"
                + "and t1.test_id = t2.test_id "
                + "and t2.STATION like '%" + Properties.Settings.Default.DefaultTestBench + "%' "
                + "and t2.PRODUCT_NAME like '%" + Properties.Settings.Default.ProductType + "%' ";
            DataTable dt = _db.GetDataTable(sqlCmd);
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
                + "where t2.test_time >= "
                + "#" + Properties.Settings.Default.DefaultDateTimeStart.ToString("yyyy-MM-dd HH:mm:ss") + "#"
                + "and t2.test_time < "
                + "#" + Properties.Settings.Default.DefaultDateTimeEnd.ToString("yyyy-MM-dd HH:mm:ss") + "#"
                + "and t1.test_id = t2.test_id "
                + "and t2.STATION like '%" + Properties.Settings.Default.DefaultTestBench + "%' "
                + "and t2.PRODUCT_NAME like '%" + Properties.Settings.Default.ProductType + "%' "
                + "and t1.item_name <> 'TotalTime' ";
            dt = _db.GetDataTable(sqlCmd);
            Dictionary<string, double> testValues = new Dictionary<string, double>();
            testValues.Add("TotalTime", 0);
            foreach (DataRow row in dt.Rows)
            {
                this.Invoke(new Action(delegate()
                {
                    testValues.Add(row[0].ToString().Trim(), 0);
                    this.chart1.Series.Add(row[0].ToString().Trim());
                    this.chart1.Series[row[0].ToString().Trim()].Color = NextColor();
                    this.chart1.Series[row[0].ToString().Trim()].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), Properties.Settings.Default.DefaultChartType);
                }));
            }
            dt.Dispose();

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
                                    this.chart1.Series[typeName].Points.Count - 1].SetValueY(double.Parse(row["USED_TIME"].ToString()));
                                this.chart1.Series[typeName].Points[
                                    this.chart1.Series[typeName].Points.Count - 1].BorderWidth = 5;
                                testValues[typeName] += double.Parse(row["USED_TIME"].ToString()) / totalTestTime;
                                if (Properties.Settings.Default.DefaultShowValues)
                                {
                                    this.chart1.Series[typeName].Points[
                                       this.chart1.Series[typeName].Points.Count - 1].Label = row["USED_TIME"].ToString();
                                }

                            }));
                    }
                    foreach (Series se in chart1.Series)
                    {
                        this.Invoke(new Action(() =>
                            {
                                se.LegendText = string.Format("{0} ({1}%)"
                                    , se.Name
                                    , (testValues[se.Name] / se.Points.Count() * 100).ToString("0.00"));

                            }));
                    }
                }
            }
            #region 按照时间的长短对图表进行排序
            this.Invoke(new Action(() =>
            {
                var newSe = chart1.Series.OrderByDescending((se) =>
                {
                    int sPos, nPos;
                    sPos = se.LegendText.LastIndexOf('(') + 1;
                    nPos = se.LegendText.LastIndexOf('%') - sPos;
                    double val = 0;
                    if (sPos <= 0 || nPos <= 0) return val;
                    double.TryParse(se.LegendText.Substring(sPos, nPos), out val);
                    return val;
                });
                foreach (var s in newSe.ToArray())
                {
                    _bkSeries.Add(s);
                }
                chart1.Series.Clear();
                foreach (var s in _bkSeries)
                {
                    chart1.Series.Add(s);
                }
            }));
            #endregion 按照时间的长短对图表进行排序

            #region 过滤图表选项
            try
            {
                this.Invoke(new Action(() =>
                    {
                        Filter.Exec(ref _bkSeries);
                        chart1.Series.Clear();
                        foreach (var s in _bkSeries)
                        {
                            chart1.Series.Add(s);
                        }
                    }));
            }
            catch (Exception exp)
            {
                this.Invoke(new Action(() =>
                    {
                        MessageBox.Show(this, exp.ToString());
                    }));
            }
            #endregion 过滤图标选项

            #region 添加图例时间信息显示
            this.Invoke(new Action(() =>
                {
                    foreach (Series se in chart1.Series)
                    {
                        double avg = 0;
                        foreach (DataPoint pt in se.Points)
                        {
                            avg += pt.YValues[0] / se.Points.Count();
                        }
                        se.LegendText += "[" + avg.ToString("0.0000") + "小时]";
                    }
                }));
            #endregion 添加图例时间信息显示
            AddMenuItems();
        }//End RefreshChart

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
    }
}
