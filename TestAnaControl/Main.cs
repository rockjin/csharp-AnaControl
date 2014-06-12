using AnaControl;
using AnaControl.Controls;
using AnaControl.Controls.Capacitys;
using AnaControl.Utils;
using DbDriver;
using System;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using TestAnaControl.Utils;

namespace TestAnaControl
{
    public partial class MainForm : Form
    {
        private bool Stop;
        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }
        public MainForm()
        {
            InitializeComponent();
            this.Text = AssemblyTitle;
            string[] types = AnaFactory.GetAnalyzers();
            ToolStripMenuItem tm = new ToolStripMenuItem();
            tm.Text = "指标分析";
            tm.Click += normDist_Click;
            this.tsMenuSelector.DropDownItems.Add(tm);
            foreach (string s in types)
            {
                tm = new ToolStripMenuItem(s);
                tm.Click += tm_Click;
                this.tsMenuSelector.DropDownItems.Add(tm);
            }
        }

        private void normDist_Click(object sender, EventArgs e)
        {
            this.panel1.Controls.Clear();
            NormDist ctl = new NormDist();
            ctl.Db.Conn = new OleDbConnection(ConnectionBuilder.Instance.Conn);
            ctl.Parent = this.panel1;
            ctl.Dock = DockStyle.Fill;
            ctl.Show();
            ctl.OnLog += OnMessageLog;
        }

        void tm_Click(object sender, EventArgs e)
        {
            this.panel1.Controls.Clear();
            ToolStripMenuItem mi = sender as ToolStripMenuItem;
            if(mi == null) return;
            IAbstrctAnalyzer dlg = AnaFactory.CreateAna(mi.Text);
            if (dlg == null) return;
            dlg.Db.Conn = new OleDbConnection(ConnectionBuilder.Instance.Conn);
            UserControl uc = (UserControl)dlg;
            uc.Parent = this.panel1;
            uc.Dock = DockStyle.Fill;
            uc.Show();
            dlg.OnLog += OnMessageLog;
        }

        private void MainForm_Load(object sender, System.EventArgs e)
        {
            NormDist normDist = new NormDist();
            normDist.Db.Conn = new OleDbConnection(ConnectionBuilder.Instance.Conn);
            normDist.Parent=this.panel1;
            normDist.Dock = DockStyle.Fill;
            normDist.Refresh();
            normDist.Show();
            normDist.OnLog += OnMessageLog;
        }
        private void OnMessageLog(object sender,EventArgs e)
        {
            OutputLog(e.ToString());
        }
        private void OpenFile_Click(object sender, EventArgs e)
        {
            ConnectionBuilder.Instance.ShowDialog(this);
            if (this.panel1.Controls.Count>0)
            {
                Control ctl = this.panel1.Controls[0];
                if (ctl is NormDist)
                {
                    ((NormDist)ctl).Db.Conn = new OleDbConnection(ConnectionBuilder.Instance.Conn);
                }
                if (ctl is AbstrctAnalyzer)
                {
                    ((AbstrctAnalyzer)ctl).Db.Conn = new OleDbConnection(ConnectionBuilder.Instance.Conn);
                }
            }
        }
        private void OutputLog(string str)
        {
            if (this.richTextBox1.InvokeRequired)
            {
                this.Invoke(new Action<string>(OutputLog), str);
                return;
            }
            this.richTextBox1.AppendText(DateTime.Now.ToString() + "-->" + str);
            if (!str.EndsWith("\n"))
            {
                this.richTextBox1.AppendText("\n");
            }
            this.richTextBox1.ScrollToCaret();
        }
        private void mprintf(string format,params object[] objects)
        {
            string temp = string.Format(format, objects);
            OutputLog(temp);
        }
        private void MergeFile_Click(object sender, EventArgs e)
        {
            DlgWaiting dlg = new DlgWaiting();
            dlg.OnAction += dlg_OnAction;
            dlg.ShowDialog(this);
        }

        void dlg_OnAction(object sender, EventArgs e)
        {
            DbProc proc1 = new DbProc();
            DbProc proc2 = new DbProc();
            proc2.Conn = new OleDbConnection(ConnectionBuilder.Instance.Conn);
            bool isCancel = false;
            this.Invoke(new Action(delegate()
                {
                    if (ConnectionBuilder.Instance.ShowDialog(this) == System.Windows.Forms.DialogResult.Cancel)
                        isCancel = true;
                }));
            if (isCancel) return;
            proc1.Conn = new OleDbConnection(ConnectionBuilder.Instance.Conn);
            ConnectionBuilder.Instance.Conn = proc2.Conn.ConnectionString;

            MergeOptions options = new MergeOptions();
            this.Invoke(new Action(() =>
                {
                    options.ShowDialog(this);
                }));
            try
            {
                Stop = false;
                string sqlCmd;
                DataTable table, table2;
                //if (options.Options[MergeOptions.FailCodeTable])
                {
                    mprintf("正在拷贝fail_code_table...");
                    table = proc1.GetDataTable("select * from FAIL_CODE_TABLE");
                    //DataColumn[] primKeys = new DataColumn[2];
                    //primKeys[0] = table2.Columns["product_name"];
                    //primKeys[1] = table2.Columns["fail_code"];
                    //table2.PrimaryKey = primKeys;
                    foreach (DataRow row in table.Rows)
                    {
                        if (Stop) break;
                        //object[] keys = new object[2];
                        //keys[0] = row["product_name"];
                        //keys[1] = row["fail_code"];
                        table2 = proc2.GetDataTable(string.Format("select * from FAIL_CODE_TABLE where product_name='{0}' and fail_code={1}",
                            row["product_name"], row["fail_code"]));
                        if (table2.Rows.Count == 0)
                        {
                            sqlCmd = "insert into FAIL_CODE_TABLE(PRODUCT_NAME,FAIL_CODE,DESCRIPTION,UPLOAD_STATE) "
                                + "values(?,?,?,?)";
                            using (OleDbCommand odc = new OleDbCommand(sqlCmd, proc2.Conn))
                            {
                                odc.Parameters.Add(new OleDbParameter("@p1", OleDbType.Char)).Value = row["product_name"];
                                odc.Parameters.Add(new OleDbParameter("@p2", OleDbType.Integer)).Value = row["fail_code"];
                                odc.Parameters.Add(new OleDbParameter("@p3", OleDbType.VarChar)).Value = row["description"];
                                odc.Parameters.Add(new OleDbParameter("@p4", OleDbType.TinyInt)).Value = row["upload_state"];
                                if (odc.ExecuteNonQuery() == 1)
                                {
                                    mprintf("合并数据 FailCode {0}{1}成功!", row["product_name"], row["fail_code"]);
                                }
                                else
                                {
                                    mprintf("合并数据 FailCode {0}{1}失败!", row["product_name"], row["fail_code"]);
                                }
                            }
                        }
                    }
                }

                //mprintf(string.Format("拷贝完成,共计{0}条数据", table2.Rows.Count));
                mprintf("正在拷贝test_results...");
                table = proc1.GetDataTable("select * from test_results");
                
                foreach (DataRow proc1_row in table.Rows)
                {
                    if (Stop) break;
                    sqlCmd = "select * from test_results where product_sn = ? and test_time = ?";
                    using (OleDbCommand odc = new OleDbCommand(sqlCmd, proc2.Conn))
                    {
                        OleDbParameter pa = new OleDbParameter("@1", proc1_row["PRODUCT_SN"]);
                        odc.Parameters.Add(pa);
                        pa = new OleDbParameter("@2", OleDbType.Date);
                        pa.Value = proc1_row["TEST_TIME"];
                        odc.Parameters.Add(pa);

                        var reader = odc.ExecuteReader();
                        if (reader.Read())
                        {
                            reader.Close();
                            reader.Dispose();   
                            continue;
                        }
                        reader.Close();
                        reader.Dispose();                        
                    }
                    sqlCmd = "insert into test_results(PRODUCT_SN,TEST_TIME,TESTER,STATION,PRODUCT_NAME,FAIL_CODE) "
                            + "values(?,?,?,?,?,?)";
                    using (OleDbCommand odc = new OleDbCommand(sqlCmd, proc2.Conn))
                    {
                        odc.Parameters.Add(new OleDbParameter("@p1", proc1_row["PRODUCT_SN"]));
                        OleDbParameter pa = new OleDbParameter("@P2", OleDbType.Date);
                        pa.Value = proc1_row["TEST_TIME"];
                        odc.Parameters.Add(pa);
                        odc.Parameters.Add(new OleDbParameter("@p3", proc1_row["TESTER"]));
                        odc.Parameters.Add(new OleDbParameter("@p4", proc1_row["STATION"]));
                        odc.Parameters.Add(new OleDbParameter("@p5", proc1_row["PRODUCT_NAME"]));
                        odc.Parameters.Add(new OleDbParameter("@p6", proc1_row["FAIL_CODE"]));
                        if (odc.ExecuteNonQuery() == 1)
                        {
                            mprintf("合并数据 TestResults {0}{1}成功!", proc1_row["PRODUCT_SN"], proc1_row["TEST_TIME"]);
                        }
                        else
                        {
                            mprintf("合并数据 TestResults {0}{1}失败!", proc1_row["PRODUCT_SN"], proc1_row["TEST_TIME"]);
                        }
                    }

                    var proc1_table2 = proc1.GetDataTable(string.Format("select * from test_item_values where test_id = {0}", proc1_row["TEST_ID"]));
                    int lastRecordId = 0;
                    sqlCmd = "select @@identity";
                    using (OleDbCommand odc = new OleDbCommand(sqlCmd, proc2.Conn))
                    {
                        lastRecordId = int.Parse(odc.ExecuteScalar().ToString());
                    }
                    #region 拷贝test_item_values

                    sqlCmd = "insert into test_item_values(test_id,PRODUCT_SN,TEST_TIME,TEST_ITEM_NAME,ITEM_VALUE,LOW_LIMIT,UP_LIMIT) "
                            + "values(?,?,?,?,?,?,?)";
                    if (!proc2.Conn.Provider.Contains("Jet.OLEDB"))
                    {
                        foreach (DataRow row in proc1_table2.Rows)
                        {
                            using (OleDbCommand odc = new OleDbCommand(sqlCmd, proc2.Conn))
                            {
                                odc.Parameters.Add(new OleDbParameter("@p1", OleDbType.BigInt)).Value = lastRecordId;
                                odc.Parameters.Add(new OleDbParameter("@p2", OleDbType.Char)).Value = row["PRODUCT_SN"];
                                odc.Parameters.Add(new OleDbParameter("@p3", OleDbType.Date)).Value = row["TEST_TIME"];
                                odc.Parameters.Add(new OleDbParameter("@p4", OleDbType.Char)).Value = row["TEST_ITEM_NAME"];
                                if (double.IsInfinity((double)row["ITEM_VALUE"]))
                                {
                                    odc.Parameters.Add(new OleDbParameter("@p5", OleDbType.Double)).Value = double.MaxValue;
                                }
                                else
                                {
                                    odc.Parameters.Add(new OleDbParameter("@p5", OleDbType.Double)).Value = row["ITEM_VALUE"];
                                }
                                odc.Parameters.Add(new OleDbParameter("@p6", OleDbType.Double)).Value = row["LOW_LIMIT"];
                                odc.Parameters.Add(new OleDbParameter("@p7", OleDbType.Double)).Value = row["UP_LIMIT"];
                                if (odc.ExecuteNonQuery() == 1)
                                {
                                    mprintf("合并数据 Test_Item_Values {0}{1}成功!", row["PRODUCT_SN"], row["TEST_TIME"]);
                                }
                                else
                                {
                                    mprintf("合并数据 Test_Item_Values {0}{1}失败!", row["PRODUCT_SN"], row["TEST_TIME"]);
                                }
                            }
                        }
                    }
                    else
                    {

                        sqlCmd = "select * from test_item_values where test_id=" + lastRecordId;
                        using (OleDbDataAdapter oda = new OleDbDataAdapter(sqlCmd, proc2.Conn))
                        {
                            using (OleDbCommandBuilder odb = new OleDbCommandBuilder(oda))
                            {
                                DataSet ds = new DataSet();
                                oda.Fill(ds, "1");
                                foreach (DataRow row in proc1_table2.Rows)
                                {
                                    DataRow nr = ds.Tables[0].NewRow();
                                    nr["test_id"] = lastRecordId;
                                    nr["PRODUCT_SN"] = row["PRODUCT_SN"];
                                    nr["TEST_TIME"] = row["TEST_TIME"];
                                    nr["TEST_ITEM_NAME"] = row["TEST_ITEM_NAME"];
                                    nr["PASS_STATE"] = row["PASS_STATE"];
                                    if (double.IsInfinity((double)row["ITEM_VALUE"]))
                                    {
                                        if (double.IsNegativeInfinity((double)row["ITEM_VALUE"]))
                                        {
                                            nr["ITEM_VALUE"] = double.MinValue;
                                        }
                                        else
                                        {
                                            nr["ITEM_VALUE"] = double.MaxValue;
                                        }
                                    }
                                    else
                                    {
                                        nr["ITEM_VALUE"] = row["ITEM_VALUE"];
                                    }
                                    nr["LOW_LIMIT"] = row["LOW_LIMIT"];
                                    nr["UP_LIMIT"] = row["UP_LIMIT"];
                                    ds.Tables[0].Rows.Add(nr);
                                }
                                oda.Update(ds, "1");
                                ds.Dispose();
                            }
                        }
                    }

                    #endregion 拷贝test_item_values
                    if (options.Options[MergeOptions.TestTime])
                    {
                        proc1_table2 = proc1.GetDataTable(string.Format("select * from TEST_TIME_DISTRIBUTION where test_id = {0}", proc1_row["TEST_ID"]));
                        sqlCmd = "insert into TEST_TIME_DISTRIBUTION(test_id,ITEM_NAME,USED_TIME) "
                                + "values(?,?,?)";
                        foreach (DataRow row in proc1_table2.Rows)
                        {
                            using (OleDbCommand odc = new OleDbCommand(sqlCmd, proc2.Conn))
                            {
                                odc.Parameters.Add(new OleDbParameter("@p1", OleDbType.BigInt)).Value = lastRecordId;
                                odc.Parameters.Add(new OleDbParameter("@p2", OleDbType.Char)).Value = row["ITEM_NAME"];
                                odc.Parameters.Add(new OleDbParameter("@p3", OleDbType.Double)).Value = row["USED_TIME"];
                                if (odc.ExecuteNonQuery() == 1)
                                {
                                    mprintf("合并数据 TEST_TIME_DISTRIBUTION {0}{1}成功!", row["ITEM_NAME"], row["USED_TIME"]);
                                }
                                else
                                {
                                    mprintf("合并数据 TEST_TIME_DISTRIBUTION {0}{1}失败!", row["ITEM_NAME"], row["USED_TIME"]);
                                }
                            }
                        }
                    }
                }
                mprintf("拷贝完成");
            }
            catch (Exception exp)
            {
                mprintf("拷贝失败-->{0}", exp.Message);
            }
        }

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Stop = true;
            }
        }

        private void ToolStripMenuItemAbout_Click(object sender, EventArgs e)
        {
            AboutBox dlg = new AboutBox();
            dlg.ShowDialog(this);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if(File.Exists("SnapShot.exe"))
            {
                File.Delete("SnapShot.exe");
            }
            File.WriteAllBytes("SnapShot.exe", Properties.Resources.SnapShot);
            Process proc = new Process();
            proc.StartInfo.FileName = "SnapShot.exe";
            proc.Start();
            proc.WaitForExit();
        }

    }
}
