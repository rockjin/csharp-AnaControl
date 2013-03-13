using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AnaControl
{
    public partial class AbnormalData : Form
    {
        public AbnormalData()
        {
            InitializeComponent();
            this.textBoxLowLimit.Text = Properties.Settings.Default.AbnormalLowData.ToString();
            this.textBoxUpLimit.Text =  Properties.Settings.Default.AbnormalUpData.ToString();

        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            double val = 0;
            double.TryParse(this.textBoxLowLimit.Text, out val);
            Properties.Settings.Default.AbnormalLowData = val;
            double.TryParse(this.textBoxUpLimit.Text, out val);
            Properties.Settings.Default.AbnormalUpData = val;
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
