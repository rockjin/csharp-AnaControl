using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Diagnostician.Forms
{
    public partial class ConditionInput : Form
    {
        public ConditionInput()
        {
            InitializeComponent();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {

        }

        public string Condition
        {
            get { return _condition; }
        }

        private string _condition = null;
    }
}
