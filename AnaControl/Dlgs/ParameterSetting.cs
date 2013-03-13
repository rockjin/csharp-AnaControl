using DbDriver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AnaControl.Dlgs;

namespace AnaControl
{
    public partial class ParameterSetting : Form
    {
        public ParameterSetting()
        {
            InitializeComponent();
        }

        private DbProc _db = new DbProc();
        public DbProc Db
        {
            get { return _db; }
            set { _db = value; }
        }

        private string _produceType = "%";
        public string ProduceType{get { return _produceType; }}
        private void buttonConfirm_Click(object sender, EventArgs e)
        {

        }
    }
}
