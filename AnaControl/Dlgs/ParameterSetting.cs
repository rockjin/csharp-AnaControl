using DbDriver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AnaControl.Controls;
using System.Windows.Forms.Integration;

namespace AnaControl.Dlgs
{
    public partial class ParameterSetting : Form
    {
        public ParameterSettingsWpf wpf;
        public ParameterSetting(DbProc db)
        {
            InitializeComponent();
            wpf = new ParameterSettingsWpf(db);
            ElementHost host = new ElementHost();
            host.Child = wpf;
            host.Dock = DockStyle.Fill;
            this.groupBox1.Controls.Add(host);
        }

        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            double.TryParse(this.wpf.tbMaxValue.Text, out this.wpf.MaxValue);
            double.TryParse(this.wpf.tbMinValue.Text, out this.wpf.MinValue);
            this.wpf.RemoveExceptData = (bool)this.wpf.ckRemoveSpecialData.IsChecked;
            this.wpf.RemoveFailData = (bool)this.wpf.ckRemoveFailData.IsChecked;
            this.wpf.RemovePassData = (bool)this.wpf.ckRemovePassData.IsChecked;
            this.wpf.RemoveRepeatData = (bool)this.wpf.ckRemoveRepeatData.IsChecked;
            this.wpf.AddWildcard = (bool)this.wpf.ckAddWildcard.IsChecked;
            this.wpf.StartTime = this.wpf.dtStartTime.Value;
            this.wpf.EndTime = this.wpf.dtEndTime.Value;
            this.wpf.ProductType = this.wpf.cbProductType.Text == "Any" ? "%" : this.wpf.cbProductType.Text;
            this.wpf.TestBench = this.wpf.cbTestBench.Text == "Any" ? "%" : this.wpf.cbTestBench.Text;
            this.wpf.TestItem = this.wpf.cbTestItem.Text == "Any" ? "%" : this.wpf.cbTestItem.Text;

            double val = Properties.Settings.Default.DefaultMaxTestTime;
            double.TryParse(this.wpf.tbMaxTestTime.Text, out val);
            Properties.Settings.Default.DefaultMaxTestTime = val;
            val = Properties.Settings.Default.DefaultMinTestTime;
            double.TryParse(this.wpf.tbMinTestTime.Text, out val);
            Properties.Settings.Default.DefaultMinTestTime = val;

            int ival = Properties.Settings.Default.DefaultShowNum;
            int.TryParse(this.wpf.tbStatisticsCycle.Text, out ival);
            Properties.Settings.Default.DefaultShowNum = ival;


            Properties.Settings.Default.AutoWildcard = wpf.AddWildcard;
            Properties.Settings.Default.DefaultRemoveRepeat = wpf.RemoveRepeatData;
            Properties.Settings.Default.DefaultRemovePassData = wpf.RemovePassData;
            Properties.Settings.Default.DefaultRemoveFailData = wpf.RemoveFailData;
            Properties.Settings.Default.DefaultRemoveSpecialData = wpf.RemoveExceptData;
            Properties.Settings.Default.DefaultDateTimeStart = wpf.StartTime;
            Properties.Settings.Default.DefaultDateTimeEnd = wpf.EndTime;
            Properties.Settings.Default.AbnormalUpData = wpf.MaxValue;
            Properties.Settings.Default.AbnormalLowData = wpf.MinValue;
            Properties.Settings.Default.ProductType = wpf.ProductType;
            Properties.Settings.Default.DefaultTestBench = wpf.TestBench;
            Properties.Settings.Default.DefaultTestItem = wpf.TestItem;
            if (int.TryParse(wpf.textBoxUpLimit.Text, out ival))
            {
                Properties.Settings.Default.DefaultTestDataUpLimit = ival;
            }            
            Properties.Settings.Default.Save();

            DialogResult = DialogResult.OK;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

    }
}
