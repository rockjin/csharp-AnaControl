using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AnaControl.Controls.TimeDistribution
{
    public partial class TimeDistributeSettings : Form
    {
        public TimeDistributeSettings()
        {
            InitializeComponent();
            this.dtStart.Value = Properties.Settings.Default.DefaultDateTimeStart;
            this.dtEnd.Value = Properties.Settings.Default.DefaultDateTimeEnd;
            this.textBoxMinTestTime.Text = Properties.Settings.Default.DefaultMinTestTime.ToString();
            this.textBoxMaxTesttime.Text = Properties.Settings.Default.DefaultMaxTestTime.ToString();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.DefaultDateTimeStart = this.dtStart.Value;
            Properties.Settings.Default.DefaultDateTimeEnd = this.dtEnd.Value;
            double val = Properties.Settings.Default.DefaultMaxTestTime;
            double.TryParse(this.textBoxMaxTesttime.Text, out val);
            Properties.Settings.Default.DefaultMaxTestTime = val;
            val = Properties.Settings.Default.DefaultMinTestTime;
            double.TryParse(this.textBoxMinTestTime.Text, out val);
            Properties.Settings.Default.DefaultMinTestTime = val;

            Properties.Settings.Default.Save();
            DialogResult = DialogResult.OK;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
