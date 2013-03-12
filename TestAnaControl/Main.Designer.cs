namespace TestAnaControl
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.总体通过率分析ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.指标正态分布ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件FToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.合并文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.选项ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.指标分析ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.测试通过率分析ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.详细数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.DetectUrls = false;
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.richTextBox1.Location = new System.Drawing.Point(0, 624);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(784, 116);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(784, 599);
            this.panel1.TabIndex = 3;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 740);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(784, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // 总体通过率分析ToolStripMenuItem
            // 
            this.总体通过率分析ToolStripMenuItem.Name = "总体通过率分析ToolStripMenuItem";
            this.总体通过率分析ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.总体通过率分析ToolStripMenuItem.Text = "总体通过率分析";
            // 
            // 指标正态分布ToolStripMenuItem
            // 
            this.指标正态分布ToolStripMenuItem.Name = "指标正态分布ToolStripMenuItem";
            this.指标正态分布ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.指标正态分布ToolStripMenuItem.Text = "指标正态分布";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件FToolStripMenuItem,
            this.选项ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(784, 25);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件FToolStripMenuItem
            // 
            this.文件FToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.打开文件ToolStripMenuItem,
            this.合并文件ToolStripMenuItem});
            this.文件FToolStripMenuItem.Name = "文件FToolStripMenuItem";
            this.文件FToolStripMenuItem.Size = new System.Drawing.Size(58, 21);
            this.文件FToolStripMenuItem.Text = "文件(&F)";
            // 
            // 打开文件ToolStripMenuItem
            // 
            this.打开文件ToolStripMenuItem.Name = "打开文件ToolStripMenuItem";
            this.打开文件ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.打开文件ToolStripMenuItem.Text = "打开文件";
            this.打开文件ToolStripMenuItem.Click += new System.EventHandler(this.打开文件ToolStripMenuItem_Click);
            // 
            // 合并文件ToolStripMenuItem
            // 
            this.合并文件ToolStripMenuItem.Name = "合并文件ToolStripMenuItem";
            this.合并文件ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.合并文件ToolStripMenuItem.Text = "合并文件";
            this.合并文件ToolStripMenuItem.Click += new System.EventHandler(this.合并文件ToolStripMenuItem_Click);
            // 
            // 选项ToolStripMenuItem
            // 
            this.选项ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.指标分析ToolStripMenuItem,
            this.测试通过率分析ToolStripMenuItem,
            this.详细数据ToolStripMenuItem});
            this.选项ToolStripMenuItem.Name = "选项ToolStripMenuItem";
            this.选项ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.选项ToolStripMenuItem.Text = "选项";
            // 
            // 指标分析ToolStripMenuItem
            // 
            this.指标分析ToolStripMenuItem.Name = "指标分析ToolStripMenuItem";
            this.指标分析ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.指标分析ToolStripMenuItem.Text = "指标分析";
            this.指标分析ToolStripMenuItem.Click += new System.EventHandler(this.指标分析ToolStripMenuItem_Click);
            // 
            // 测试通过率分析ToolStripMenuItem
            // 
            this.测试通过率分析ToolStripMenuItem.Name = "测试通过率分析ToolStripMenuItem";
            this.测试通过率分析ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.测试通过率分析ToolStripMenuItem.Text = "测试通过率分析";
            this.测试通过率分析ToolStripMenuItem.Click += new System.EventHandler(this.测试通过率分析ToolStripMenuItem_Click);
            // 
            // 详细数据ToolStripMenuItem
            // 
            this.详细数据ToolStripMenuItem.Name = "详细数据ToolStripMenuItem";
            this.详细数据ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.详细数据ToolStripMenuItem.Text = "详细数据";
            this.详细数据ToolStripMenuItem.Click += new System.EventHandler(this.详细数据ToolStripMenuItem_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 762);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TestResultAnalyzer";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripMenuItem 总体通过率分析ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 指标正态分布ToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 文件FToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打开文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 合并文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 选项ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 指标分析ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 测试通过率分析ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 详细数据ToolStripMenuItem;







    }
}

