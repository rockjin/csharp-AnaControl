using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AnaControl.Controls.Capacitys
{
    public partial class CapacitySettings : Form
    {
        public CapacitySettings()
        {
            InitializeComponent();
            this.cbRemoveRepeats.Checked = Properties.Settings.Default.DefaultRemoveRepeat;
            this.dtStart.Value = Properties.Settings.Default.DefaultDateTimeStart;
            this.dtEnd.Value = Properties.Settings.Default.DefaultDateTimeEnd;
            this.tbValueCycle.Text = Properties.Settings.Default.DefaultShowNum.ToString();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.DefaultRemoveRepeat = this.cbRemoveRepeats.Checked;
            Properties.Settings.Default.DefaultDateTimeStart = this.dtStart.Value;
            Properties.Settings.Default.DefaultDateTimeEnd = this.dtEnd.Value;
            int val=1;
            if (!int.TryParse(this.tbValueCycle.Text, out val))
            {
                val = 1;
            }
            Properties.Settings.Default.DefaultShowNum = val;

            Properties.Settings.Default.Save();
            DialogResult = DialogResult.OK;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
