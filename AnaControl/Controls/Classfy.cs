using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AnaControl.Dlgs;
using DbDriver;
using AnaControl.Controls;
using System.Windows.Forms.DataVisualization.Charting;
using System.IO;

namespace AnaControl.Controls
{
    [DisplayName("测试项分类")]
    public partial class Classfy : UserControl,IAbstrctAnalyzer
    {
        DataTable table = new DataTable("Results");
        TypeSelector selDlg;
        ParameterSetting settings;
        private DataGridView gridView;
        
        public Classfy()
        {
            InitializeComponent();            
            gridView = new DataGridView();
            gridView.Dock = DockStyle.Fill;
            this.Controls.Add(gridView);
            gridView.SendToBack();
            gridView.ContextMenuStrip = this.contextMenuStripGridView;
        }
        private DbProc _db = new DbProc();
        public DbProc Db
        {
            get
            {
                return _db;
            }
            set
            {
                selDlg = new TypeSelector(value);
                _db = value;
            }
        }

        public event EventHandler OnLog;

        private void Classfy_Load(object sender, EventArgs e)
        {
            selDlg = new TypeSelector(_db);
            foreach(string item in selDlg.Items)
            {
                this.listView1.Items.Add(item.Trim());
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            while (this.listView1.SelectedItems.Count > 0)
            {
                string text = this.listView1.SelectedItems[0].Text;
                if(!this.listView2.Items.ContainsKey(text))
                {
                    this.listView2.Items.Add(text);
                }
                this.listView1.Items.Remove(this.listView1.SelectedItems[0]);
            }
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            while (this.listView2.SelectedItems.Count > 0)
            {
                string text = this.listView2.SelectedItems[0].Text;
                if (!this.listView1.Items.ContainsKey(text))
                {
                    this.listView1.Items.Add(text);
                }
                this.listView2.Items.Remove(this.listView2.SelectedItems[0]);
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if(this.buttonOk.Text == "Next->")
            {
                this.gridView.BringToFront();
                ReadData();
                this.buttonOk.Text = "<-Back";
            }
            else if(this.buttonOk.Text == "<-Back")
            {
                this.gridView.SendToBack();
                this.buttonOk.Text = "Next->";
            }
        }
        private void InvokeOnLog(MsgEventArgs e)
        {
            if (OnLog == null) return;
            OnLog(this, e);
        }
        private void ReadData()
        {
            DataTable table = new DataTable("results");
            List<DataColumn> columns = new List<DataColumn>();
            if (this.checkBoxAddTestTime.Checked)
            {
                table.Columns.Add("TestTime", typeof(DateTime));
                columns.Add(table.Columns["TestTime"]);
            }
            table.Columns.Add("Sn");
            columns.Add(table.Columns["Sn"]);
            if (this.checkBoxAddTestItemName.Checked)
            {
                table.Columns.Add("TestItemName");
                columns.Add(table.Columns["TestItemName"]);
            }
            table.PrimaryKey = columns.ToArray();
            foreach(ListViewItem item in listView2.Items)
            {
                table.Columns.Add(item.Text);
            }
            List<DataTable> dts = new List<DataTable>();
            int maxLine = 0;
            foreach(ListViewItem item in listView2.Items)
            {
                string sqlCmd = string.Format("select t1.test_time,t1.product_sn,t1.test_item_name,t1.item_value from TEST_ITEM_VALUES t1, TEST_RESULTS t2 ");
                InvokeOnLog("create sql cmd...\n");
                string matchString = "";
                if (Properties.Settings.Default.AutoWildcard)
                {
                    matchString = "%" + item.Text + "%";
                }
                else
                {
                    matchString = item.Text;
                }
                if (Properties.Settings.Default.DefaultRemoveRepeat)
                {
                    InvokeOnLog("remove repeats\n");
                    sqlCmd += " ,("
                              + " select max(tt1.test_time) as test_time2,tt1.product_sn,tt1.test_item_name "
                              + " from TEST_ITEM_VALUES tt1,test_results tt2"
                              + " where tt1.test_item_name like '" + matchString + "' "
                              + " and tt1.test_id = tt2.test_id "
                              + " and tt2.STATION like '%" + Properties.Settings.Default.DefaultTestBench + "%' "
                              + " and tt2.PRODUCT_NAME like '%" + Properties.Settings.Default.ProductType + "%' "
                              + " group by tt1.product_sn,tt1.test_item_name "
                              + ") t3"
                              + " where t2.test_time = t3.test_time2 and t1.test_item_name = t3.test_item_name and ";
                }
                else
                {
                    sqlCmd += " where ";
                }
                sqlCmd+= " t2.test_time>=#" +
                        Properties.Settings.Default.DefaultDateTimeStart.ToString("yyyy-MM-dd HH:mm:ss") + "# "
                        + "and t2.test_time<=#" +
                        Properties.Settings.Default.DefaultDateTimeEnd.ToString("yyyy-MM-dd HH:mm:ss") + "# "
                        + "and t1.TEST_ITEM_NAME like '" + matchString + "' "
                        + "and t1.TEST_ID = t2.TEST_ID "
                        + "and t2.STATION like '%" + Properties.Settings.Default.DefaultTestBench + "%' "
                        + "and t2.PRODUCT_NAME like '%" + Properties.Settings.Default.ProductType + "%' ";

                if (Properties.Settings.Default.DefaultRemovePassData)
                {
                    InvokeOnLog("remove pass data\n");
                    sqlCmd += " and t1.pass_state <> 0";
                }
                if (Properties.Settings.Default.DefaultRemoveFailData)
                {
                    InvokeOnLog("remove fail data\n");
                    sqlCmd += " and t1.pass_state = 0";
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

                DataTable dt = _db.GetDataTable(sqlCmd);
                dts.Add(dt);
                if (maxLine < dt.Rows.Count) maxLine = dt.Rows.Count;
            }
            for(int i=0;i<maxLine;i++)
            {
                //table.Rows.Add(table.NewRow());
                for(int j=0;j<dts.Count;j++)
                {
                    if (dts[j].Rows.Count <= i) break;
                    DataRow row;
                    List<object> keys = new List<object>();
                    int offset = 1;
                    if(checkBoxAddTestTime.Checked)
                    {
                        keys.Add(dts[j].Rows[i]["test_time"]);
                        offset++;
                    }
                    keys.Add(dts[j].Rows[i]["product_sn"].ToString().Trim());
                    if(checkBoxAddTestItemName.Checked)
                    {
                        keys.Add(dts[j].Rows[i]["test_item_name"].ToString().Trim());
                        offset++;
                    }
                    row = table.Rows.Find(keys.ToArray());
                    if (row != null)
                    {
                        if (this.checkBoxAddTestItemName.Checked)
                        {
                            row[j + offset] = dts[j].Rows[i]["item_value"];
                        }
                        else
                        {
                            row[j + offset] = dts[j].Rows[i]["item_value"];
                        }
                    }
                    else
                    {
                        row = table.NewRow();                        
                        if(checkBoxAddTestTime.Checked)
                        {
                            row["TestTime"] = dts[j].Rows[i]["test_time"];
                        }
                        row["Sn"] = row[1] = dts[j].Rows[i]["product_sn"].ToString().Trim();
                        if(checkBoxAddTestItemName.Checked)
                        {
                            row["TestItemName"] = dts[j].Rows[i]["test_item_name"].ToString().Trim();
                        }
                        row[j + offset] = dts[j].Rows[i]["item_value"];                        
                        table.Rows.Add(row);
                    }
                }
            }
            this.gridView.DataSource = null;
            this.gridView.Rows.Clear();
            this.gridView.Columns.Clear();
            this.gridView.DataSource = table;
            this.gridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.gridView.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 12, FontStyle.Bold);
            this.gridView.AutoResizeColumnHeadersHeight();
            this.gridView.AutoResizeColumns();
        }
        private class RowComparer : System.Collections.IComparer
        {
            private static int sortOrderModifier = 1;

            public RowComparer(SortOrder sortOrder)
            {
                if (sortOrder == SortOrder.Descending)
                {
                    sortOrderModifier = -1;
                }
                else if (sortOrder == SortOrder.Ascending)
                {
                    sortOrderModifier = 1;
                }
            }

            public int Compare(object x, object y)
            {
                DataGridViewRow DataGridViewRow1 = (DataGridViewRow)x;
                DataGridViewRow DataGridViewRow2 = (DataGridViewRow)y;

                // Try to sort based on the Last Name column.
                int CompareResult = System.String.Compare(
                    DataGridViewRow1.Cells[0].Value.ToString(),
                    DataGridViewRow2.Cells[0].Value.ToString());

                // If the Last Names are equal, sort based on the First Name.
                if (CompareResult == 0)
                {
                    CompareResult = System.String.Compare(
                        DataGridViewRow1.Cells[1].Value.ToString(),
                        DataGridViewRow2.Cells[1].Value.ToString());
                }
                return CompareResult * sortOrderModifier;
            }
        }
        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(SelectedDataToString());
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = "temp.xls";
            sfd.DefaultExt = "xls";
            sfd.Filter = "Excel(*.xls)|*.xls|All(*.*)|*.*";
            if (sfd.ShowDialog(this) != DialogResult.OK)
                return;
            StreamWriter sw = new StreamWriter(sfd.FileName, false, Encoding.Unicode);
            sw.Write(SelectedDataToString());
            sw.Flush();
            sw.Close();
        }

        private string SelectedDataToString()
        {
            StringBuilder temp = new StringBuilder();
            Dictionary<int, Dictionary<int, object>> data = new Dictionary<int, Dictionary<int, object>>();
            int maxLine = 0;
            foreach(DataGridViewCell cell in gridView.SelectedCells)
            {
                object value;
                if (cell.Value == null) continue;
                //if (!double.TryParse(cell.Value.ToString(), out value)) continue;
                value = cell.Value;
                if(data.ContainsKey(cell.ColumnIndex))
                {
                    data[cell.ColumnIndex].Add(cell.RowIndex, value);
                }
                else
                {
                    data.Add(cell.ColumnIndex, new Dictionary<int, object>());
                    data[cell.ColumnIndex].Add(cell.RowIndex, value); 
                }
                if(maxLine<data[cell.ColumnIndex].Count)
                {
                    maxLine = data[cell.ColumnIndex].Count;
                }
            }
            var orderData = data.OrderBy((d) => { return d.Key; });
            foreach (var pareVal in orderData)
            {
                int col = pareVal.Key;
                temp.Append(gridView.Columns[col].HeaderText + "\t");
            }
            if (temp.Length > 0)
            {
                temp.Replace('\t', '\n', temp.Length - 1, 1);
            }
            for (int i = 0; i < maxLine;i++ )
            {
                foreach (var pareVal in orderData)
                {
                    int col = pareVal.Key;
                    if (pareVal.Value.Count <= i) continue;
                    var tempVals = pareVal.Value.OrderBy((d) => d.Key);
                    temp.Append(tempVals.ElementAt(i).Value.ToString() + "\t");
                }
                if (temp.Length > 0)
                {
                    temp.Replace('\t', '\n', temp.Length - 1, 1);
                }
            }            
            return temp.ToString();
        }

        private void listView2_DoubleClick(object sender, EventArgs e)
        {
            this.listView2.FocusedItem.BeginEdit();
        }

        private void buttonConditions_Click(object sender, EventArgs e)
        {
            settings = new ParameterSetting(_db);
            settings.ShowDialog();
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            this.listView2.Items.Add(this.listView1.FocusedItem.Text);
            this.listView1.Items.Remove(this.listView1.FocusedItem);
        }
 
        private void ListView_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                ListView lv = sender as ListView;
                if (lv == null) return;
                while(lv.SelectedItems.Count>0)
                {
                    lv.Items.Remove(lv.SelectedItems[0]);
                }
            }
        }
    }

    class TypeSelector : Form
    {
        private PropertyGrid grid;
        private Panel panel;
        private Button btnOk, btnCancel;
        private ItemType itemType;
        private DbProc _proc;
        public TypeSelector(DbProc db)
        {
            _proc = db;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Size = new System.Drawing.Size(600, 400);
            panel = new Panel();
            panel.Dock = DockStyle.Bottom;
            panel.Padding = new System.Windows.Forms.Padding(10);
            panel.Height = 60;
            this.Controls.Add(panel);
            grid = new PropertyGrid();
            grid.Dock = DockStyle.Fill;
            this.Controls.Add(grid);
            btnOk = new Button();
            btnOk.Text = "Ok";
            btnOk.Dock = DockStyle.Right;
            btnOk.Margin = new System.Windows.Forms.Padding(20, 0, 0, 20);
            panel.Controls.Add(btnOk);
            btnCancel = new Button();
            btnCancel.Text = "Cancel";
            btnCancel.Dock = DockStyle.Right;
            btnCancel.Margin = new System.Windows.Forms.Padding(20, 0, 0, 20);
            panel.Controls.Add(btnCancel);
            this.AcceptButton = btnOk;
            this.CancelButton = btnCancel;
            itemType = new ItemType();
            grid.SelectedObject = itemType;
            db.time_start = Properties.Settings.Default.DefaultDateTimeStart;
            db.time_end = Properties.Settings.Default.DefaultDateTimeEnd;
            string sql = string.Format("select distinct(test_item_name) from test_item_values"
                + " where test_time between #{0}# and #{1}#", db.time_start, db.time_end);
            DataTable dt = db.GetDataTable(sql);
            List<string> ls = new List<string>();
            foreach(DataRow row in dt.Rows)
            {
                ls.Add((string) row["test_item_name"]);
            }
            itemType.Items = ls.ToArray();
            grid.ExpandAllGridItems();
        }

        public string[] Items
        {
            get { return itemType.Items; }
        }
    }

    class ItemType
    {
        private string[] _items = new string[0];
        [DisplayName("测试项"),
        Category("测试项分类")]
        public string[] Items
        {
            get { return _items; }
            set { _items = value; }
        }
    }
}
