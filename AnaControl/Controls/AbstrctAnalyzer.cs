using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Diagnostics;
using DbDriver;
using System.Windows.Forms.DataVisualization.Charting;
using AnaControl.Utils;

namespace AnaControl.Controls
{
    public partial class AbstrctAnalyzer : UserControl
    {

        protected static int _rotateColor = (int)KnownColor.Red;
        protected DateTime _lastestRecordTime = Properties.Settings.Default.DefaultDateTimeEnd;
        protected DateTime _firstRecordTime = Properties.Settings.Default.DefaultDateTimeStart;

        public AbstrctAnalyzer()
        {
            InitializeComponent();
        }
        protected DbProc _db = new DbProc();
        public DbProc Db
        {
            get { return _db; }
            set { _db = value; }
        }

        private void tsbDrawChart_Click(object sender, EventArgs e)
        {
            Setting();
            DlgWaiting wait = new DlgWaiting();
            wait.OnAction += dlg_OnAction;
            wait.ShowDialog();
        }

        private void dlg_OnAction(object sender, EventArgs e)
        {
            try
            {
                RefreshChart();
            }
            catch (Exception exp)
            {
                this.InvokeOnLog(new EventArgsWithMsg(exp.Message));
            }
        }

        private void tsbSaveChart_Click(object sender, EventArgs e)
        {
            try
            {
                this.Refresh();
                Bitmap bmp = this.CreateBitmap(this);
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "bmp|*.bmp";
                sfd.FileName = "temp.bmp";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    //chart1.SaveImage(sfd.FileName, ChartImageFormat.Jpeg);
                    bmp.Save(sfd.FileName);
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
                return;
            }
        }

        protected virtual void Setting() { }
        protected virtual void RefreshChart() { }

        public event EventHandler OnLog;

        private void LogFunc(object sender, EventArgs e)
        {
            Debug.WriteLine(e.ToString());
        }

        protected void InvokeOnLog(EventArgs e)
        {
            if (OnLog == null) return;
            OnLog(this, e);
        }

        private Bitmap CreateBitmap(Control trl)
        {
            Graphics gDest;
            //IntPtr hdcDest;
            //int hdcSrc;
            Bitmap bmpDrawed = new Bitmap(trl.Width, trl.Height);
            //int hWnd = trl.Handle.ToInt32();
            gDest = Graphics.FromImage(bmpDrawed);
            //hdcSrc = Win32.GetWindowDC(hWnd);
            // hdcDest = gDest.GetHdc();
            gDest.CopyFromScreen(trl.PointToScreen(new Point(0, 0)), new Point(0, 0), trl.Size);

            //Win32.BitBlt(hdcDest.ToInt32(), 0, 0, trl.Width, trl.Height, hdcSrc, 0, 0, Win32.SRCCOPY);
            //gDest.ReleaseHdc(hdcDest);
            // Win32.ReleaseDC(hWnd, hdcSrc);
            return bmpDrawed;
        }

        private void toolStripMenuItem_Save_Click(object sender, EventArgs e)
        {
            tsbSaveChart_Click(null, null);
        }

        private void ToolStripMenuItem_3d_Click(object sender, EventArgs e)
        {
            this.ToolStripMenuItem_3d.Checked = !this.ToolStripMenuItem_3d.Checked;
            RefreshChart();
        }

        private void AbstrctAnalyzer_Load(object sender, EventArgs e)
        {
            this.OnLog += this.LogFunc;
            foreach (string name in Enum.GetNames(typeof(SeriesChartType)))
            {
                ToolStripMenuItem mi = new ToolStripMenuItem(name);
                this.menuItemChartTypes.DropDownItems.Add(mi);
                mi.Click += new EventHandler(ChartTypeButton_Click);
                if (name == Properties.Settings.Default.DefaultChartType)
                {
                    mi.Checked = true;
                }
            }
        }

        public static string[] GetChartTypes
        {
            get { return Enum.GetNames(typeof(SeriesChartType)); }
        }

        protected Color NextColor()
        {
            if (_rotateColor + 1 > Enum.GetValues(typeof(KnownColor)).Length)
                _rotateColor = (int)KnownColor.Red;
            Color c = Color.FromKnownColor((KnownColor)_rotateColor++);
            #region //屏蔽较白颜色
            byte min = c.R;
            min = min < c.G ? min : c.G;
            min = min < c.B ? min : c.B;
            if (min > 200)
            {
                return NextColor();
            }
            #endregion
            return c;
        }

        private void ChartTypeButton_Click(object sender, EventArgs e)
        {
            try
            {
                SeriesChartType t = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), sender.ToString());
                Properties.Settings.Default.DefaultChartType = t.ToString();
                foreach (ToolStripMenuItem item in this.menuItemChartTypes.DropDownItems)
                {
                    item.Checked = false;
                }
                (sender as ToolStripMenuItem).Checked = true;
                DlgWaiting wait = new DlgWaiting();
                wait.OnAction += dlg_OnAction;
                wait.ShowDialog();
            }
            catch (System.Exception ee)
            {
                this.chart1 = new Chart();
                InvokeOnLog((MsgEventArgs)ee.Message);
            }
            return;
        }
    }

    public static class AnaFactory
    {
        public static AbstrctAnalyzer CreateAna(string name)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            foreach (Type type in asm.GetExportedTypes())
            {
                if (!type.IsInterface
                    && !type.IsAbstract
                    && type.IsSubclassOf(typeof(AbstrctAnalyzer)))
                {
                    object[] attr = type.GetCustomAttributes(typeof(DisplayNameAttribute), false);
                    if (attr.Length == 0)
                    {
                        continue;
                    }
                    if ((attr[0] as DisplayNameAttribute).DisplayName != name)
                    {
                        continue;
                    }
                    var obj = asm.CreateInstance(type.FullName);
                    if (obj != null)
                    {
                        return obj as AbstrctAnalyzer;
                    }
                }
            }
            return null;
        }

        public static string[] GetAnalyzers()
        {
            List<string> types = new List<string>();
            Assembly asm = Assembly.GetExecutingAssembly();
            foreach (Type type in asm.GetExportedTypes())
            {
                if (!type.IsInterface
                    && !type.IsAbstract
                    && type.IsSubclassOf(typeof(AbstrctAnalyzer)))
                {
                    object[] attr = type.GetCustomAttributes(typeof(DisplayNameAttribute), false);
                    if (attr.Length != 0)
                    {
                        types.Add((attr[0] as DisplayNameAttribute).DisplayName);
                    }
                }
            }
            return types.ToArray();
        }
    }

    public class EventArgsWithMsg : EventArgs
    {
        private string _msg;
        public EventArgsWithMsg(string msg)
        {
            _msg = msg;
        }
        public override string ToString()
        {
            return _msg;
        }
    }
}
