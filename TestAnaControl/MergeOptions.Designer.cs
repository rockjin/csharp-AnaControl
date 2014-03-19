namespace TestAnaControl
{
    partial class MergeOptions
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonOk = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxTestResults = new System.Windows.Forms.CheckBox();
            this.checkBoxTestItemValues = new System.Windows.Forms.CheckBox();
            this.checkBoxBind = new System.Windows.Forms.CheckBox();
            this.checkBoxFailCodeTable = new System.Windows.Forms.CheckBox();
            this.checkBoxTestTime = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonOk.Location = new System.Drawing.Point(0, 149);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(319, 23);
            this.buttonOk.TabIndex = 0;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBoxTestTime);
            this.groupBox1.Controls.Add(this.checkBoxFailCodeTable);
            this.groupBox1.Controls.Add(this.checkBoxBind);
            this.groupBox1.Controls.Add(this.checkBoxTestItemValues);
            this.groupBox1.Controls.Add(this.checkBoxTestResults);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(319, 149);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "选择要合并的表";
            // 
            // checkBoxTestResults
            // 
            this.checkBoxTestResults.AutoSize = true;
            this.checkBoxTestResults.Checked = true;
            this.checkBoxTestResults.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxTestResults.Enabled = false;
            this.checkBoxTestResults.Location = new System.Drawing.Point(82, 20);
            this.checkBoxTestResults.Name = "checkBoxTestResults";
            this.checkBoxTestResults.Size = new System.Drawing.Size(96, 16);
            this.checkBoxTestResults.TabIndex = 0;
            this.checkBoxTestResults.Text = "TEST RESULTS";
            this.checkBoxTestResults.UseVisualStyleBackColor = true;
            // 
            // checkBoxTestItemValues
            // 
            this.checkBoxTestItemValues.AutoSize = true;
            this.checkBoxTestItemValues.Checked = true;
            this.checkBoxTestItemValues.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxTestItemValues.Enabled = false;
            this.checkBoxTestItemValues.Location = new System.Drawing.Point(82, 43);
            this.checkBoxTestItemValues.Name = "checkBoxTestItemValues";
            this.checkBoxTestItemValues.Size = new System.Drawing.Size(120, 16);
            this.checkBoxTestItemValues.TabIndex = 1;
            this.checkBoxTestItemValues.Text = "TEST ITEM VALUES";
            this.checkBoxTestItemValues.UseVisualStyleBackColor = true;
            // 
            // checkBoxBind
            // 
            this.checkBoxBind.AutoSize = true;
            this.checkBoxBind.Location = new System.Drawing.Point(82, 66);
            this.checkBoxBind.Name = "checkBoxBind";
            this.checkBoxBind.Size = new System.Drawing.Size(48, 16);
            this.checkBoxBind.TabIndex = 2;
            this.checkBoxBind.Text = "BIND";
            this.checkBoxBind.UseVisualStyleBackColor = true;
            // 
            // checkBoxFailCodeTable
            // 
            this.checkBoxFailCodeTable.AutoSize = true;
            this.checkBoxFailCodeTable.Location = new System.Drawing.Point(82, 89);
            this.checkBoxFailCodeTable.Name = "checkBoxFailCodeTable";
            this.checkBoxFailCodeTable.Size = new System.Drawing.Size(78, 16);
            this.checkBoxFailCodeTable.TabIndex = 3;
            this.checkBoxFailCodeTable.Text = "FAIL CODE";
            this.checkBoxFailCodeTable.UseVisualStyleBackColor = true;
            // 
            // checkBoxTestTime
            // 
            this.checkBoxTestTime.AutoSize = true;
            this.checkBoxTestTime.Location = new System.Drawing.Point(82, 112);
            this.checkBoxTestTime.Name = "checkBoxTestTime";
            this.checkBoxTestTime.Size = new System.Drawing.Size(78, 16);
            this.checkBoxTestTime.TabIndex = 4;
            this.checkBoxTestTime.Text = "TEST TIME";
            this.checkBoxTestTime.UseVisualStyleBackColor = true;
            // 
            // MergeOptions
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(319, 172);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MergeOptions";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Merge  Options";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBoxTestTime;
        private System.Windows.Forms.CheckBox checkBoxFailCodeTable;
        private System.Windows.Forms.CheckBox checkBoxBind;
        private System.Windows.Forms.CheckBox checkBoxTestItemValues;
        private System.Windows.Forms.CheckBox checkBoxTestResults;
    }
}