using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.ConnectionUI;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace TestAnaControl.Utils
{
    class ConnectionBuilder
    {
        //"Provider=Microsoft.Jet.OLEDB.4.0;Data Source="
        //        + file
        //        + ";Persist Security Info=True;Jet OLEDB:Database Password="
        //        + pwd;

        //private string _conn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=att2000.mdb;Persist Security Info=True";
        private string _conn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source="
                + "results.mdb"
                + ";Persist Security Info=True;Jet OLEDB:Database Password="
                + "datang";
        private static ConnectionBuilder _instance = null;
        private static object objLock = new object();
        private const string xmlFile = "Connection.xml";
        XmlDocument _xmlDoc;
        private void Save()
        {
            XmlNode root = _xmlDoc.SelectSingleNode("Settings");
            if (null == root)
            {
                root = _xmlDoc.CreateElement("Settings");
                _xmlDoc.AppendChild(root);
            }
            XmlNode element = root.SelectSingleNode("ConnectionString");
            if (null == element)
            {
                element = _xmlDoc.CreateElement("ConnectionString");
                root.AppendChild(element);
                element.InnerText = _conn;
            }
            else
            {
                element.InnerText = _conn;
            }
            _xmlDoc.Save(xmlFile);
        }

        private void Load()
        {
            if (File.Exists(xmlFile))
            {
                _xmlDoc.Load(xmlFile);
            }
            else
            {
                XmlDeclaration dec = _xmlDoc.CreateXmlDeclaration("1.0", "GB2312", null);
                _xmlDoc.AppendChild(dec);
            }
            XmlNode root = _xmlDoc.SelectSingleNode("Settings");
            if (null == root)
            {
                root = _xmlDoc.CreateElement("Settings");
                _xmlDoc.AppendChild(root);
            }
            XmlNode element = root.SelectSingleNode("ConnectionString");
            if (null == element)
            { 
                element = _xmlDoc.CreateElement("ConnectionString");
                root.AppendChild(element);
                element.InnerText = _conn;
            }
            else
            {
                _conn = element.InnerText;
            }
        }

        public string Conn
        {
            get
            {
                return _conn;
            }
            set
            {
                _conn = value;
                Save();
            }
        }
        private ConnectionBuilder() 
        {
            _xmlDoc = new XmlDocument();            
            Load();
        }

        public static ConnectionBuilder Instance
        {
            get
            {
                lock (objLock)
                {
                    if (_instance == null)
                    {
                        _instance = new ConnectionBuilder();
                    }
                    return _instance;
                }
            }
        }
        public DialogResult ShowDialog(IWin32Window owner)
        {
            DataConnectionDialog dialog = new DataConnectionDialog();
            dialog.DataSources.Add(DataSource.AccessDataSource);
            dialog.DataSources.Add(DataSource.OdbcDataSource);
            dialog.DataSources.Add(DataSource.OracleDataSource);
            dialog.DataSources.Add(DataSource.SqlDataSource);
            dialog.DataSources.Add(DataSource.SqlFileDataSource);

            dialog.SelectedDataSource = DataSource.AccessDataSource;
            dialog.SelectedDataProvider = DataProvider.OleDBDataProvider;
            //dialog.ConnectionString = _conn;

            if (DataConnectionDialog.Show(dialog, owner) == DialogResult.OK)
            {
                _conn = dialog.ConnectionString;
                Save();
                return DialogResult.OK;
            }
            else
            {
                return DialogResult.Cancel;
            }
        }
    }
}