using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Diagnostician
{
    class Program
    {
        static int Main(string[] args)
        {
            //Debug.WriteLine(args);
            FormMain form = new FormMain();
            form.ShowDialog();
            return 0;
        }
    }
}
