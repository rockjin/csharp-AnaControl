using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;

namespace AnaControl.Scripts
{
    public class Filter
    {
        public static void Exec(ref List<Series> series)
        {
            Series se = series.Find((_se) =>
                {
                    if (_se.Name == "TotalTime")
                    {
                        return true;
                    }
                    return false;
                });
            List<int> indexs = new List<int>();
            int count = 0;
            foreach (DataPoint dp in se.Points)
            {
                if (dp.YValues[0] < Properties.Settings.Default.DefaultMinTestTime)
                {
                    indexs.Insert(0, count);
                }
                if (dp.YValues[0] > Properties.Settings.Default.DefaultMaxTestTime)
                {
                    indexs.Insert(0, count);
                }
                count++;
            }
            foreach (Series _se in series)
            {
                foreach (int i in indexs)
                {
                    _se.Points.RemoveAt(i);
                }
            }
        }
    }
}
