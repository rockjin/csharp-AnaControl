using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace AnaControl.Utils
{
    public partial class DlgWaiting : Form
    {
        public DlgWaiting()
        {
            InitializeComponent();
        }

        public event EventHandler OnAction;
        private bool isComplete = false;

        private void DlgWaiting_Load(object sender, EventArgs e)
        {
            bw = new Thread(bw_DoWork);
            bw.IsBackground = true;
            bw.Start();

            Thread t = new Thread(timer1_Tick);
            t.IsBackground = true;
            t.Start();
        }

        void bw_DoWork()
        {
            if (OnAction != null)
            {
                OnAction.Invoke(null, null);
            }
        }

        private int count = 0;

        private void timer1_Tick()
        {
            while (bw.IsAlive)
            {
                count++;
                count %= 6;
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < count; i++)
                {
                    sb.Append("。");
                }
                msg = "请耐心等待" + sb.ToString();
                this.SafeRefresh();
                Thread.Sleep(100);
            }
            this.SafeClose();
        }

        private void SafeClose()
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action(Close));
                return;
            }
            this.Close();
        }
        private void SafeRefresh()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(SafeRefresh));
                return;
            }
            this.Refresh();
        }
        string msg = "";
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Font font = new Font("方正舒体", 24.0f);
            e.Graphics.DrawString(msg, font, Brushes.Black, new PointF(10, (Height - font.Height) / 2f));
        }

        private Thread bw;
    }
}
