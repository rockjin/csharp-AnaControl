﻿using AnaControl;
using AnaControl.Controls;
using AnaControl.Controls.Capacitys;
using AnaControl.Utils;
using DbDriver;
using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;
using TestAnaControl.Utils;

namespace TestAnaControl
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
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
            AbstrctAnalyzer dlg = AnaFactory.CreateAna(mi.Text);
            if (dlg == null) return;
            dlg.Db.Conn = new OleDbConnection(ConnectionBuilder.Instance.Conn);
            dlg.Parent = this.panel1;
            dlg.Dock = DockStyle.Fill;
            dlg.Show();
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
            try
            {
                OleDbDataAdapter oda;
                mprintf("正在拷贝fail_code_table...");
                DataTable table = proc1.GetDataTable("select * from FAIL_CODE_TABLE");
                DataTable table2 = proc2.GetDataTable("select * from FAIL_CODE_TABLE", out oda);
                DataColumn[] primKeys = new DataColumn[2];
                primKeys[0] = table2.Columns["product_name"];
                primKeys[1] = table2.Columns["fail_code"];
                table2.PrimaryKey = primKeys;
                foreach (DataRow row in table.Rows)
                {
                    object[] keys = new object[2];
                    keys[0] = row["product_name"];
                    keys[1] = row["fail_code"];
                    if (!table2.Rows.Contains(keys))
                    {
                        table2.Rows.Add(row["product_name"]
                                        , row["fail_code"]
                                        , row["description"]
                                        , row["upload_state"]);
                    }
                }
                oda.Update(table2);
                mprintf(string.Format("拷贝完成,共计{0}条数据", table2.Rows.Count));
                mprintf("正在拷贝test_results...");
                table = proc1.GetDataTable("select * from test_results");
                table2 = proc2.GetDataTable("select * from test_results", out oda);
                primKeys = new DataColumn[2];
                primKeys[0] = table2.Columns["product_sn"];
                primKeys[1] = table2.Columns["test_time"];
                table2.PrimaryKey = primKeys;
                foreach (DataRow row in table.Rows)
                {
                    object[] keys = new object[2];
                    keys[0] = row["product_sn"];
                    keys[1] = row["test_time"];
                    if (!table2.Rows.Contains(keys))
                    {
                        table2.Rows.Add(row["PRODUCT_SN"]
                                        , row["TEST_TIME"]
                                        , row["TESTER"]
                                        , row["STATION"]
                                        , row["PRODUCT_NAME"]
                                        , row["FAIL_CODE"]
                                        , row["RESULTS_FILE"]
                                        , row["UPLOAD_STATE"]);
                    }
                }
                oda.Update(table2);
                mprintf(string.Format("拷贝完成,共计{0}条数据", table2.Rows.Count));
                mprintf("正在拷贝test_item_values...");
                table = proc1.GetDataTable("select * from test_item_values");
                table2 = proc2.GetDataTable("select * from test_item_values", out oda);
                primKeys = new DataColumn[3];
                primKeys[0] = table2.Columns["product_sn"];
                primKeys[1] = table2.Columns["test_time"];
                primKeys[2] = table2.Columns["test_item_name"];
                table2.PrimaryKey = primKeys;
                foreach (DataRow row in table.Rows)
                {
                    object[] keys = new object[3];
                    keys[0] = row["product_sn"];
                    keys[1] = row["test_time"];
                    keys[2] = row["test_item_name"];
                    if (!table2.Rows.Contains(keys))
                    {
                        table2.Rows.Add(row["PRODUCT_SN"]
                                        , row["TEST_TIME"]
                                        , row["TEST_ITEM_NAME"]
                                        , row["ITEM_VALUE"]
                                        , row["LOW_LIMIT"]
                                        , row["UP_LIMIT"]
                                        , row["PASS_STATE"]
                                        , row["UPLOAD_STATE"]);
                    }
                }
                oda.Update(table2);
                mprintf(string.Format("拷贝完成,共计{0}条数据", table2.Rows.Count));
                mprintf("正在拷贝bind...");
                table = proc1.GetDataTable("select * from bind");
                table2 = proc2.GetDataTable("select * from bind", out oda);
                primKeys = new DataColumn[2];
                primKeys[0] = table2.Columns["product_sn"];
                primKeys[1] = table2.Columns["sub_board_name"];
                table2.PrimaryKey = primKeys;
                foreach (DataRow row in table.Rows)
                {
                    object[] keys = new object[2];
                    keys[0] = row["product_sn"];
                    keys[1] = row["sub_board_name"];
                    if (!table2.Rows.Contains(keys))
                    {
                        table2.Rows.Add(row["PRODUCT_SN"]
                                        , row["UPDATE_TIME"]
                                        , row["SUB_BOARD_NAME"]
                                        , row["SUB_BOARD_SN"]
                                        , row["UPLOAD_STATE"]);
                    }
                }
                oda.Update(table2);
                mprintf("拷贝完成");
            }
            catch (Exception exp)
            {
                mprintf("拷贝失败-->{0}", exp.Message);
            }
            ConnectionBuilder.Instance.Conn = proc2.Conn.ConnectionString;
        }

        //private void 指标分析ToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    this.panel1.Controls.Clear();
        //    NormDist dlg = new NormDist();
        //    dlg.Db.Conn = new OleDbConnection(ConnectionBuilder.Instance.Conn);
        //    dlg.Parent = this.panel1;
        //    dlg.Dock = DockStyle.Fill;
        //    dlg.Refresh();
        //    dlg.Show();
        //    dlg.OnLog += OnMessageLog;
        //}

        //private void 测试通过率分析ToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    this.panel1.Controls.Clear();
        //    Capacity dlg = new Capacity();
        //    dlg.Db.Conn = new OleDbConnection(ConnectionBuilder.Instance.Conn);
        //    dlg.Parent = this.panel1;
        //    dlg.Dock = DockStyle.Fill;
        //    dlg.Refresh();
        //    dlg.Show();
        //    dlg.OnLog += OnMessageLog;
        //}

        //private void 详细数据ToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    this.panel1.Controls.Clear();
        //    Detail dlg = new Detail();
        //    dlg.Db.Conn = new OleDbConnection(ConnectionBuilder.Instance.Conn);
        //    dlg.Parent = this.panel1;
        //    dlg.Dock = DockStyle.Fill;
        //    dlg.Show();
        //    dlg.OnLog += OnMessageLog;
        //}
    }
}
