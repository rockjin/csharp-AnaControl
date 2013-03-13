using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DbDriver;

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
        private DbProc _db = new DbProc();
        public DbProc Db
        {
            get { return _db; }
            set { _db = value; }
        }
        private void comboBoxFunction_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(this.comboBoxFunction.Text=="总数")
            {
                this.richTextBoxSqlCmd.Text = _db.GetTotalCountSql();
            }
            else if (this.comboBoxFunction.Text == "PASS数量")
            {
                this.richTextBoxSqlCmd.Text = _db.GetPassCountSql();
            }
            else if (this.comboBoxFunction.Text == "FAIL数量")
            {
                this.richTextBoxSqlCmd.Text = _db.GetFailCountSql();
            }
            else
            {
                
            }
        }

        private void buttonExecute_Click(object sender, EventArgs e)
        {
            try
            {
                bingSource.DataSource = _db.GetDataTable(this.richTextBoxSqlCmd.Text);
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
