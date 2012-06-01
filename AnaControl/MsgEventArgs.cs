using System;
using System.Collections.Generic;
using System.Text;

namespace AnaControl
{
    internal class MsgEventArgs : EventArgs
    {
        private string msg = "";
        public MsgEventArgs(string str)
        {
            msg = str;
        }
        public override string ToString()
        {
            return msg;
        }
        public static implicit operator MsgEventArgs(string val)
        {
            MsgEventArgs msgEventArgs=new MsgEventArgs(val);
            return msgEventArgs;
        }
    }
}
