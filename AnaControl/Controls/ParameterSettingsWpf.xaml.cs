using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AnaControl.Controls
{
    /// <summary>
    /// ParameterSettingsWpf.xaml 的交互逻辑
    /// </summary>
    public partial class ParameterSettingsWpf : System.Windows.Controls.UserControl
    {
        public string ProductType;
        public string TestBench;
        public string TestItem;
        public bool RemoveRepeatData;
        public bool RemovePassData;
        public bool RemoveFailData;
        public bool RemoveExceptData;
        public bool AddWildcard;
        public double MinValue;
        public double MaxValue;
        public DateTime StartTime;
        public DateTime EndTime;
        public DateTimePicker dtStartTime;
        public DateTimePicker dtEndTime;
        public DbDriver.DbProc db;

        public ParameterSettingsWpf(DbDriver.DbProc db)
        {
            this.db = db;
            ProductType = Properties.Settings.Default.ProductType;
            TestBench = Properties.Settings.Default.DefaultTestBench;
            TestItem = Properties.Settings.Default.DefaultTestItem;
            RemoveRepeatData = Properties.Settings.Default.DefaultRemoveRepeat;
            RemoveFailData = Properties.Settings.Default.DefaultRemoveFailData;
            RemovePassData = Properties.Settings.Default.DefaultRemovePassData;
            RemoveExceptData = Properties.Settings.Default.DefaultRemoveSpecialData;
            AddWildcard = Properties.Settings.Default.AutoWildcard;
            MinValue = Properties.Settings.Default.AbnormalLowData;
            MaxValue = Properties.Settings.Default.AbnormalUpData;
            StartTime = Properties.Settings.Default.DefaultDateTimeStart;
            EndTime = Properties.Settings.Default.DefaultDateTimeEnd;

            InitializeComponent();

            cbProductType.Items.Add("Any");
            cbTestBench.Items.Add("Any");
            cbTestItem.Items.Add("Any");
            cbProductType.Text = ProductType=="%"?"Any":ProductType;
            cbTestBench.Text = TestBench == "%"?"Any":TestBench;
            cbTestItem.Text = TestItem == "%" ? "Any" : TestItem;
            ckRemoveRepeatData.IsChecked = RemoveRepeatData;
            ckRemovePassData.IsChecked = RemovePassData;
            ckRemoveFailData.IsChecked = RemoveFailData;
            ckRemoveSpecialData.IsChecked = RemoveExceptData;
            ckAddWildcard.IsChecked = AddWildcard;

            tbMaxValue.Text = MaxValue.ToString();
            tbMinValue.Text = MinValue.ToString();

            dtStartTime = new DateTimePicker();
            dtEndTime = new DateTimePicker();
            dtStartTime.Dock = DockStyle.Fill;
            dtEndTime.Dock = DockStyle.Fill;
            dtStartTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            dtEndTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            dtEndTime.Format = DateTimePickerFormat.Custom;
            dtStartTime.Format = DateTimePickerFormat.Custom;
            form1.Child = dtStartTime;
            form2.Child = dtEndTime;
            dtStartTime.Value = StartTime;
            dtEndTime.Value = EndTime;


            string sql = "select distinct(PRODUCT_NAME) from test_results";
            DataTable dt = db.GetDataTable(sql);
            foreach (DataRow row in dt.Rows)
            {
                cbProductType.Items.Add(row["PRODUCT_NAME"].ToString().Trim());
            }

            string ptype = cbProductType.Text == "Any" ? "" : cbProductType.Text;
            string btype = cbTestBench.Text == "Any" ? "" : cbTestBench.Text;

            sql = "select distinct(STATION) from test_results where "
                + "product_name like '%" + ptype + "%'";
            dt = db.GetDataTable(sql);
            foreach (DataRow row in dt.Rows)
            {
                this.cbTestBench.Items.Add(row["STATION"].ToString().Trim());
            }

            sql = "select distinct(TEST_ITEM_NAME) from test_item_values t1,test_results t2 "
                + "where t1.product_sn = t2.product_sn and t1.test_time = t2.test_time "
                + "and t2.product_name like '%" + ptype + "%' "
                + "and t2.STATION like '%" + btype + "%'";
            dt = db.GetDataTable(sql);
            foreach (DataRow row in dt.Rows)
            {
                this.cbTestItem.Items.Add(row["TEST_ITEM_NAME"].ToString().Trim());
            }
        }

        private void cbProductType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string sql;
            string ptype = cbProductType.Text == "Any" ? "" : cbProductType.Text;
            string btype = cbTestBench.Text == "Any" ? "" : cbTestBench.Text;

            sql = "select distinct(STATION) from test_results where "
                + "product_name like '%" + ptype + "%'";
            DataTable dt = db.GetDataTable(sql);
            foreach (DataRow row in dt.Rows)
            {
                this.cbTestBench.Items.Add(row["STATION"].ToString().Trim());
            }

            sql = "select distinct(TEST_ITEM_NAME) from test_item_values t1,test_results t2 "
                + "where t1.product_sn = t2.product_sn and t1.test_time = t2.test_time "
                + "and t2.product_name like '%" + ptype + "%' "
                + "and t2.STATION like '%" + btype + "%'";
            dt = db.GetDataTable(sql);
            foreach (DataRow row in dt.Rows)
            {
                this.cbTestItem.Items.Add(row["TEST_ITEM_NAME"].ToString().Trim());
            }
        }

        private void cbTestBench_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string sql;
            DataTable dt;
            string ptype = cbProductType.Text == "Any" ? "" : cbProductType.Text;
            string btype = cbTestBench.Text == "Any" ? "" : cbTestBench.Text;

            sql = "select distinct(TEST_ITEM_NAME) from test_item_values t1,test_results t2 "
                + "where t1.product_sn = t2.product_sn and t1.test_time = t2.test_time "
                + "and t2.product_name like '%" + ptype + "%' "
                + "and t2.STATION like '%" + btype + "%'";
            dt = db.GetDataTable(sql);
            foreach (DataRow row in dt.Rows)
            {
                this.cbTestItem.Items.Add(row["TEST_ITEM_NAME"].ToString().Trim());
            }
        }
    }
}
