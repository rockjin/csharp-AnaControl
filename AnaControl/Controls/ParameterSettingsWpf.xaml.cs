using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AnaControl.Controls
{
    /// <summary>
    /// ParameterSettingsWpf.xaml 的交互逻辑
    /// </summary>
    public partial class ParameterSettingsWpf : UserControl
    {
        public string ProductType;
        public string TestBench;
        public string TestItem;
        public bool RemoveRepeatData;
        public bool RemovePassData;
        public bool RemoveFailData;
        public bool RemoveExceptData;
        public double MinValue;
        public double MaxValue;
        public DateTime StartTime;
        public DateTime EndTime;
        public ParameterSettingsWpf()
        {
            ProductType = Properties.Settings.Default.ProductType;
            TestBench = Properties.Settings.Default.DefaultTestItem;
            TestItem = Properties.Settings.Default.DefaultTestItem;
            RemoveRepeatData = Properties.Settings.Default.DefaultRemoveRepeat;
            RemoveFailData = Properties.Settings.Default.DefaultRemoveFailData;
            RemovePassData = Properties.Settings.Default.DefaultRemovePassData;
            MinValue = Properties.Settings.Default.AbnormalLowData;
            MaxValue = Properties.Settings.Default.AbnormalUpData;
            StartTime = Properties.Settings.Default.DefaultDateTimeStart;
            EndTime = Properties.Settings.Default.DefaultDateTimeEnd;

            cbProductType.Text = ProductType;
            cbTestBench.Text = TestBench;
            cbTestItem.Text = TestItem;
            ckRemoveRepeatData.IsChecked = RemoveRepeatData;
            ckRemovePassData.IsChecked = RemovePassData;
            ckRemoveFailData.IsChecked = RemoveFailData;
            ckRemoveSpecialData.IsChecked = RemoveExceptData;


            InitializeComponent();
        }
    }
}
