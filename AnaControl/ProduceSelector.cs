using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AnaControl
{
    public partial class ProduceSelector : Form
    {
        public ProduceSelector()
        {
            InitializeComponent();
            DbProc proc =new DbProc();
            proc.Connect();
            try
            {

            }
            catch (Exception exp)
            {
                MessageBox.Show("无法查询到数据 " + exp.Message);
                this.Close();
            }
            proc.DisConnect();
        }

        private string _produceType = "%";
        public string ProduceType{get { return _produceType; }}
        private void buttonConfirm_Click(object sender, EventArgs e)
        {

        }
    }
}
