using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace AnaControl
{
    public partial class Detail : UserControl
    {
        public Detail()
        {
            InitializeComponent();
        }
        public EventHandler OnLog = null;
        private void InvokeOnLog(EventArgs e)
        {
            if (OnLog == null) return;
            OnLog(this, e);
        }

        private void comboBoxFunction_SelectedIndexChanged(object sender, EventArgs e)
        {
            DbProc proc = new DbProc();
            if(this.comboBoxFunction.Text=="总数")
            {
                this.richTextBoxSqlCmd.Text = proc.GetTotalCountSql();
            }
            else if (this.comboBoxFunction.Text == "PASS数量")
            {
                this.richTextBoxSqlCmd.Text = proc.GetPassCountSql();
            }
            else if (this.comboBoxFunction.Text == "FAIL数量")
            {
                this.richTextBoxSqlCmd.Text = proc.GetFailCountSql();
            }
            else
            {
                
            }
        }

        private void buttonExecute_Click(object sender, EventArgs e)
        {
            DbProc proc = new DbProc();
            proc.Connect();
            try
            {
                bingSource.DataSource = proc.GetDataTable(this.richTextBoxSqlCmd.Text);
                proc.DisConnect();
                this.dataGridView1.DataSource = bingSource;
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
                return;
            }
            
        }

        private BindingSource bingSource=new BindingSource();
    }
}
