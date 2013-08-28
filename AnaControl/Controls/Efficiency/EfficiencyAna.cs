using AnaControl.Dlgs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AnaControl.Controls.Efficiency
{
    [DisplayName("效率分析")]
    public class EfficiencyAna : AbstrctAnalyzer
    {
        protected override void RefreshChart()
        {
            string sqlCmd = string.Format("select distinct(t1.ITEM_NAME) "
                + " from test_time_distribution as t1 inner join "
                + " test_result as t2 on t1.test_id = t2.test_id "
                + " where t2.test_time >= "
                + " #" + Properties.Settings.Default.DefaultDateTimeStart.ToString("yyyy-MM-dd HH:mm:ss") + "# "
                + " and t2.test_time < "
                + " #" + Properties.Settings.Default.DefaultDateTimeEnd.ToString("yyyy-MM-dd HH:mm:ss") + "# "
                + " and t2.STATION like '%" + Properties.Settings.Default.DefaultTestBench + "%' "
                + " and t2.PRODUCT_NAME like '%" + Properties.Settings.Default.ProductType + "%' ");
            DataTable table = _db.GetDataTable(sqlCmd);
        }
        protected override bool Setting()
        {
            base.Setting();
            ParameterSetting dlg = new ParameterSetting(_db);
            if (dlg.ShowDialog(this) == DialogResult.Cancel) return false;
            if (this._db != null)
            {
                _db.time_start = Properties.Settings.Default.DefaultDateTimeStart;
                _db.time_end = Properties.Settings.Default.DefaultDateTimeEnd;
                _db.TestBench = Properties.Settings.Default.DefaultTestBench;
                _db.ProductType = Properties.Settings.Default.ProductType;
            }
            return true;
        }
    }
}
