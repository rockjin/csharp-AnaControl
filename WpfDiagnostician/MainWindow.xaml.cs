using System;
using System.Collections.Generic;
using System.Data;
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

namespace WpfDiagnostician
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataTable table = new DataTable("Solution_Table");
            table.Columns.Add("PRODUCT_NAME");
            table.Columns.Add("SN");
            table.Columns.Add("FAIL_CODE");

            for (int i = 0; i < 10; i++)
            {
                DataRow row = table.NewRow();
                row["PRODUCT_NAME"] = "TDRU331FAE";
                row["SN"] = "99130122159" + i.ToString("000");
                row["FAIL_CODE"] = 100 - i;
                table.Rows.Add(row);
            }
            this.listBox1.ItemsSource = table.AsDataView();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
