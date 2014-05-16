using System;
namespace AnaControl.Controls
{
    public interface IAbstrctAnalyzer
    {
        DbDriver.DbProc Db { get; set; }
        event EventHandler OnLog;
    }
}
