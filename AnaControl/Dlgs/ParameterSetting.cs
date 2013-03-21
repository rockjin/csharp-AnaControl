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


            DialogResult = DialogResult.OK;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

    }
}
