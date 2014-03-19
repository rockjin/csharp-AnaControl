using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestAnaControl
{
    public partial class MergeOptions : Form
    {
        private Dictionary<string, bool> options;
        public const string TestResults = "Test results";
        public const string TestItemValues = "Test item values";
        public const string Bind = "Bind";
        public const string FailCodeTable = "Fail code table";
        public const string TestTime = "Test time distribution";
        public MergeOptions()
        {
            InitializeComponent();
            options = new Dictionary<string, bool>();
            options.Add(TestResults, true);
            options.Add(TestItemValues, true);
            options.Add(Bind, false);
            options.Add(FailCodeTable, false);
            options.Add(TestTime, false);            
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            options[Bind] = checkBoxBind.Checked;
            options[FailCodeTable] = checkBoxFailCodeTable.Checked;
            options[TestTime] = checkBoxTestTime.Checked;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public Dictionary<string,bool> Options
        {
            get { return options; }
        }
    }
}
