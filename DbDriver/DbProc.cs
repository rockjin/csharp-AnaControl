// *******************************************************************************
// *  COPYRIGHT DaTang Mobile Communications Equipment CO.,LTD                   *
// *******************************************************************************
// Functions:   
// Note:        
// 
// History:
// Date         Ahthor      Description
// ------------------------------------------------------------------------------
// 2010-03-15   jinfangxiao      Create this file
// *******************************************************************************/
using System;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;

namespace DbDriver
{
    public class DbProc : IDisposable
    {
        #region private propertes

        private OleDbConnection conn;
        private bool _RemoveRepeat = true;
        public static DateTime _PassCheckStartTime = DateTime.Now;
        public DateTime time_start;
        public DateTime time_end;
        public string ProductType = "";
        public string TestBench = "";


        #endregion private propertes


        public bool RemoveRepeat
        {
            set { _RemoveRepeat = value; }
        }
        public OleDbConnection Conn
        {
            get { return conn; }
            set {
                try
                {
                    if (conn != null && conn.State == ConnectionState.Open)
                        conn.Close();
                    conn = value;
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                    conn.Open();
                }
                catch { }
            }
        }        
        public DbProc()
        {
            //this._ConnectionStr = this.CreateSqlConnection(ip, uid, pwd);
            time_start = new DateTime(2005, 1, 1, 0, 0, 0);
            time_end = DateTime.Now;
        }
        public DbProc(OleDbConnection oconn)
        {
            time_start = new DateTime(2005, 1, 1, 0, 0, 0);
            time_end = DateTime.Now;
            this.Conn = oconn;
        }
        public DbProc(string connStr)
        {
            time_start = new DateTime(2005, 1, 1, 0, 0, 0);
            time_end = DateTime.Now;
            this.Conn = new OleDbConnection(connStr);
        }
 
        private bool FailCodeCheck(int fail_code, string product_name)
        {
            string cmdStr;
            OleDbDataAdapter oda;
            DataTable dt;
            try
            {
                cmdStr="Select * From FAIL_CODE_TABLE where PRODUCT_NAME=\'"
                    +product_name
                    +"\' and FAIL_CODE="
                    +fail_code.ToString();
                oda = new OleDbDataAdapter(cmdStr, this.conn);
                new OleDbCommandBuilder(oda);
                dt = new DataTable();
                oda.Fill(dt);
                if (dt.Rows.Count == 0)
                {
                    DataRow row = dt.NewRow();
                    row["PRODUCT_NAME"] = product_name;
                    row["FAIL_CODE"] = fail_code;
                    row["DESCRIPTION"] = "未定义";
                    dt.Rows.Add(row);
                }
                oda.Update(dt);
                return true;
            }
            catch (OleDbException e)
            {
                Debug.WriteLine(e.Message);
            }     
            return false;
        }
        public bool GetFailCodeFromDesc(string product_type, string errDescription, out int errCode)
        {
            string cmdStr;
            DataTable dt;
            OleDbDataAdapter oda;
            cmdStr = @"Select * From FAIL_CODE_TABLE where PRODUCT_NAME='"
                + product_type
                + @"' and DESCRIPTION='"
                + errDescription
                + @"'";
            dt = GetDataTable(cmdStr);
            if (dt.Rows.Count != 0)
            {
                errCode = int.Parse(dt.Rows[0]["FAIL_CODE"].ToString());
                return true;
            }
            dt.Clear();
            if (errDescription == "OK")
            {
                errCode = 0;
                cmdStr = @"Select * From FAIL_CODE_TABLE where PRODUCT_NAME='"
                    + product_type
                    + @"' and FAIL_CODE=0";
                oda = new OleDbDataAdapter(cmdStr, this.conn);
                OleDbCommandBuilder odc = new OleDbCommandBuilder(oda);
                oda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    dt.Rows[0]["DESCRIPTION"] = errDescription;
                }
                else
                {
                    DataRow row = dt.NewRow();
                    row["PRODUCT_NAME"] = product_type;
                    row["FAIL_CODE"] = errCode;
                    row["DESCRIPTION"] = errDescription;
                    dt.Rows.Add(row);
                }
            }
            else
            {
                cmdStr = @"Select * From FAIL_CODE_TABLE where PRODUCT_NAME='"
                    + product_type
                    + @"' order by FAIL_CODE";
                oda = new OleDbDataAdapter(cmdStr, this.conn);
                OleDbCommandBuilder odc = new OleDbCommandBuilder(oda);
                oda.Fill(dt);
                DataRow row = dt.NewRow();
                if (dt.Rows.Count == 0)
                {
                    errCode = -1;
                }
                else
                {
                    errCode = int.Parse(dt.Rows[0]["FAIL_CODE"].ToString()) - 1;
                }
                row["PRODUCT_NAME"] = product_type;
                row["FAIL_CODE"] = errCode;
                row["DESCRIPTION"] = errDescription;
                dt.Rows.Add(row);
            }
            oda.Update(dt);
            return true;
        }
        private bool GetDescFromFailCode(string product_type, int errCode, out string errDescription)
        {
            string cmdStr;
            DataTable dt;
            cmdStr = @"Select * From FAIL_CODE_TABLE where PRODUCT_NAME='"
                + product_type
                + @"' and FAIL_CODE="
                + errCode.ToString()
                + @"";
            dt = this.GetDataTable(cmdStr);
            if (dt.Rows.Count != 0)
            {
                errDescription = dt.Rows[0]["DESCRIPTION"].ToString();
                return true;
            }
            errDescription = "";
            return false;
        }
        public bool GetUnitState(string serial,string station_name,ref int itemCount,out int maxCount,out string results)
        {
            bool state = true;
            string cmdStr;
            OleDbDataAdapter oda;
            int item_count=0;
            maxCount = 0;
            results = "";
            StringBuilder sb = new StringBuilder();
            try
            {
                DataTable dt = new DataTable();
                if (itemCount == 0)
                {
                    cmdStr = "Select max(num1) as maxnum "
                        + "from "
                        + "("
                        + "Select count(t1.product_sn) as num1 "
                        + "from test_item_values t1,test_results t2 "
                        + "where t1.test_time=t2.test_time "
                        + "and t1.product_sn=t2.product_sn "
                        + "and STATION='" + station_name + "' "
                        + "and t2.test_time >=#" + _PassCheckStartTime.ToString("yyyy-MM-dd HH:mm:ss") + "# "
                        + "group by t1.product_sn,t1.test_time"
                        + ")";
                    oda = new OleDbDataAdapter(cmdStr, this.conn);
                    new OleDbCommandBuilder(oda);
                    oda.Fill(dt);
                    if (dt.Rows.Count > 0 && dt.Rows[0]["maxnum"] != System.DBNull.Value)
                    {
                        item_count = (int)dt.Rows[0]["maxnum"];
                    }
                    dt.Clear();
                    oda.Dispose();
                }
                else
                {
                    item_count = itemCount;
                }
                maxCount = item_count;
                cmdStr = "Select count(tab2.product_sn) as num "
                    + "from "
                    + "("
                    + "Select a2.product_sn,a2.test_item_name,max(a2.test_time) as ttime "
                    + "from test_item_values a2 "
                    + "where a2.product_sn = '"
                    + serial
                    + "' "
                    + " and a2.test_time>=#" + _PassCheckStartTime.ToString("yyyy-MM-dd HH:mm:ss") + "# "
                    + "Group by a2.product_sn,a2.test_item_name "
                    + ")"
                    + "tab1,test_item_values tab2,test_results tab3 "
                    + "where tab1.product_sn=tab2.product_sn "
                    + "and tab1.test_item_name=tab2.test_item_name "
                    + "and tab1.ttime=tab2.test_time "
                    + "and tab1.product_sn=tab3.product_sn "
                    + "and tab1.ttime=tab3.test_time "
                    + "and tab3.station='" + station_name + "' "
                    + "and tab2.PASS_STATE = 1 ";
                oda = new OleDbDataAdapter(cmdStr, conn);
                oda.Fill(dt);
                if (dt.Rows.Count < item_count)
                {
                    itemCount=(int)dt.Rows[0]["num"];
                }
                dt.Clear();
                oda.Dispose();

                cmdStr = "Select tab2.*"
                    + "from "
                    + "("
                    + "Select a2.product_sn,a2.test_item_name,max(a2.test_time) as ttime "
                    + "from test_item_values a2 "
                    + "where a2.product_sn = '"
                    + serial
                    + "' "
                    + " and a2.test_time>=#" + _PassCheckStartTime.ToString("yyyy-MM-dd HH:mm:ss") + "# "
                    + "Group by a2.product_sn,a2.test_item_name "
                    + ")"
                    + "tab1,test_item_values tab2,test_results tab3 "
                    + "where tab1.product_sn=tab2.product_sn "
                    + "and tab1.test_item_name=tab2.test_item_name "
                    + "and tab1.ttime=tab2.test_time "
                    + "and tab1.product_sn=tab3.product_sn "
                    + "and tab1.ttime=tab3.test_time "
                    + "and tab3.station='" + station_name + "' ";
                oda = new OleDbDataAdapter(cmdStr, conn);
                oda.Fill(dt);
                int record_count = 0;
                StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "items.xls",false,Encoding.UTF8);
                sw.WriteLine("{0}\t{1}\t{2}\t{3}", "PRODUCT_SN", "TEST_TIME", "TEST_ITEM_NAME", "PASS_STATE");
                foreach (DataRow row in dt.Rows)
                {
                    sw.WriteLine("{0}\t{1}\t{2}\t{3}", row["PRODUCT_SN"], row["TEST_TIME"]
                        , row["TEST_ITEM_NAME"], row["PASS_STATE"]);
                    if (int.Parse(row["PASS_STATE"].ToString()) != 1)
                    {
                        state = false;
                        sb.AppendLine("未通过的测试项:  " + row["test_item_name"].ToString());
                        continue;
                    }
                    record_count++;
                }
                sw.Close();
                if (itemCount < item_count)
                {
                    sb.AppendLine("当前测试的单元测试项不全,如果测试项内容有删减，请更新该用例的起始统计时间。");
                    state = false;
                }
                dt.Dispose();
                oda.Dispose();
            }
            catch (OleDbException e)
            {
                sb.Append(e.Message);
                state = false;
            }
            results = sb.ToString();
            return state;
        }

        public bool GetTotalCount(out int count)
        {
            count = 0;
            string cmdStr;
            if (this._RemoveRepeat)
            {
                cmdStr = "Select count(*) as NUM From TEST_RESULTS T1"
                    + " where T1.TEST_TIME IN("
                    + " SELECT MAX(T2.TEST_TIME) FROM TEST_RESULTS T2 "
                    + " where (t2.test_time > "
                    + "#" + this.time_start.ToString("yyyy-MM-dd HH:mm:ss") + "#"
                    + " AND t2.test_time<="
                    + "#" + this.time_end.ToString("yyyy-MM-dd HH:mm:ss") + "#"
                    + ")"
                    + " GROUP BY T2.PRODUCT_SN"
                    + ") "
                    + "and T1.product_name like '%" + ProductType + "%' "
                    + "and T1.station like '%" + TestBench + "%' ";
            }
            else
            {
                cmdStr = "Select count(*) as NUM From TEST_RESULTS T1"
                    + " where (test_time > "
                    + "#" + this.time_start.ToString("yyyy-MM-dd HH:mm:ss") + "#"
                    + " AND test_time <="
                    + "#" + this.time_end.ToString("yyyy-MM-dd HH:mm:ss") + "#"
                    + ") "
                    + "and T1.product_name like '%" + ProductType + "%' "
                    + "and T1.station like '%" + TestBench + "%' ";
            }
            DataTable dt = this.GetDataTable(cmdStr);
            if (dt.Rows.Count > 0)
            {
                count = int.Parse(dt.Rows[0]["NUM"].ToString());
            }
            else
            {
                count = 0;
            }
            return true;
        }

        public bool GetPassCount(out int count)
        {
            count = 0;
            string cmdStr;
            if (this._RemoveRepeat)
            {
                cmdStr = "Select count(*) as NUM From TEST_RESULTS T1"
                    + " where T1.TEST_TIME IN("
                    + " SELECT MAX(T2.TEST_TIME) FROM TEST_RESULTS T2 "
                    + " where (T2.test_time > "
                    + "#" + this.time_start.ToString("yyyy-MM-dd HH:mm:ss") + "#"
                    + " AND T2.test_time <="
                    + "#" + this.time_end.ToString("yyyy-MM-dd HH:mm:ss") + "#"
                    + ")"
                    + " GROUP BY T2.PRODUCT_SN"
                    + ") "
                    + " AND FAIL_CODE = 0 "
                    + "and T1.product_name like '%" + ProductType + "%' "
                    + "and T1.station like '%" + TestBench + "%' ";
            }
            else
            {
                cmdStr = "Select count(*) as NUM From TEST_RESULTS T1 where FAIL_CODE = 0"
                    + "AND (test_time > "
                    + "#" + this.time_start.ToString("yyyy-MM-dd HH:mm:ss") + "#"
                    + " AND test_time <="
                    + "#" + this.time_end.ToString("yyyy-MM-dd HH:mm:ss") + "#"
                    + ") "
                    + "and T1.product_name like '%" + ProductType + "%' "
                    + "and T1.station like '%" + TestBench + "%' ";
            }
            DataTable dt = this.GetDataTable(cmdStr);
            count = int.Parse(dt.Rows[0]["NUM"].ToString());
            return true;
        }


        public bool GetFailCount(out int count)
        {
            count = 0;
            string cmdStr;
            if (this._RemoveRepeat)
            {
                cmdStr = "Select count(*) as NUM From TEST_RESULTS T1"
                    + " where T1.TEST_TIME IN("
                    + " SELECT MAX(T2.TEST_TIME) FROM TEST_RESULTS T2 "
                    + " where (T2.test_time > "
                    + "#" + this.time_start.ToString("yyyy-MM-dd HH:mm:ss") + "#"
                    + " AND T2.test_time <="
                    + "#" + this.time_end.ToString("yyyy-MM-dd HH:mm:ss") + "#"
                    + ")"
                    + " GROUP BY T2.PRODUCT_SN"
                    + ") "
                    + " AND FAIL_CODE <> 0 "
                    + "and T1.product_name like '%" + ProductType + "%' "
                    + "and T1.station like '%" + TestBench + "%' ";
            }
            else
            {
                cmdStr = "Select count(*) as NUM From TEST_RESULTS T1 where FAIL_CODE <> 0"
                     + " AND (T1.test_time > "
                    + "#" + this.time_start.ToString("yyyy-MM-dd HH:mm:ss") + "#"
                    + " AND T1.test_time <="
                    + "#" + this.time_end.ToString("yyyy-MM-dd HH:mm:ss") + "#"
                    + ") "
                    + "and T1.product_name like '%" + ProductType + "%' "
                    + "and T1.station like '%" + TestBench + "%' ";
            }
            DataTable dt = this.GetDataTable(cmdStr);
            count = int.Parse(dt.Rows[0]["NUM"].ToString());
            return true;
        }
        public bool GetFPYCount(out int count)
        {
            count = 0;
            string cmdStr = "select count(*) as num from "
                + "( "
                + "select product_sn,min(test_time) as num1 "
                + "from test_results "
                + "group by product_sn "
                + ")tab1,test_results tab2 "
                + "where tab1.num1=tab2.test_time "
                + "and tab1.product_sn=tab2.product_sn "
                + "and tab2.fail_code=0 "
                + "and tab2.test_time>=#"
                + this.time_start.ToString("yyyy-MM-dd HH:mm:ss")
                + "#"
                + "and tab2.test_time<=#"
                + this.time_end.ToString("yyyy-MM-dd HH:mm:ss")
                + "#"
                + "and tab2.product_name like '%" + ProductType + "%' "
                + "and tab2.station like '%" + TestBench + "%' ";
            DataTable dt = this.GetDataTable(cmdStr);
            count = int.Parse(dt.Rows[0]["NUM"].ToString());
            return true;
        }
        public bool GetTopFail(string product_type,string station,
            out int[] top_count, out string[] top_item_name)
        {
            string cmdStr;
            if (this._RemoveRepeat)
            {
                cmdStr = "select * from("
                    + "Select count(fail_code) as NUM,fail_code From TEST_RESULTS t1 "
                    + "where t1.FAIL_CODE <> 0 "
                    + "and t1.test_time in "
                    + "("
                    + "select min(t2.test_time) "
                    + "from test_results t2 "
                    + "where (T2.test_time > "
                    + "#" + this.time_start.ToString("yyyy-MM-dd HH:mm:ss") + "#"
                    + " AND T2.test_time <="
                    + "#" + this.time_end.ToString("yyyy-MM-dd HH:mm:ss") + "#"
                    + ") "
                    + @"and product_name like '%"+product_type+@"%' "
                    + @"and station like '%" + station + @"%' "
                    + "group by t2.product_sn"
                    + ") "
                    + "group by t1.fail_code "
                    + ")"
                    + "order by num desc";
            }
            else
            {
                cmdStr = "select * from("
                    + "Select count(fail_code),fail_code as NUM From TEST_RESULTS "
                    + "where FAIL_CODE <> 0 "
                    + "AND (test_time > "
                    + "#" + this.time_start.ToString("yyyy-MM-dd HH:mm:ss") + "#"
                    + " AND test_time <="
                    + "#" + this.time_end.ToString("yyyy-MM-dd HH:mm:ss") + "#"
                    + ") "
                    + @"and product_name like '%" + product_type + @"%' "
                    + @"and station like '%" + station + @"%' "
                    + "group by fail_code "
                    + ")"
                    + "order by num desc";
            }

            DataTable dt = this.GetDataTable(cmdStr);
            top_count = new int[dt.Rows.Count];
            top_item_name = new string[dt.Rows.Count];
            for(int i=0;i<dt.Rows.Count;i++)
            {
                top_count[i] = int.Parse(dt.Rows[i]["NUM"].ToString());
                this.GetDescFromFailCode(product_type, int.Parse(dt.Rows[i]["fail_code"].ToString()), out top_item_name[i]);
            }
            return true;
        }
        public string GetErrorCodeDescription(string product_name, int error_code)
        {
            string cmdStr = "select * from FAIL_CODE_TABLE "
                         + "where PRODUCT_NAME=\"" 
                         + product_name 
                         + "\" and FAIL_CODE=" 
                         + error_code.ToString();
            DataTable dt = this.GetDataTable(cmdStr);
            return dt.Rows[0]["DESCRIPTION"].ToString();

        }
        public DataTable GetDataTable(string sql,params object[] pars)
        {
            string sqlCmd;
            if (this.conn.Provider != "Microsoft.Jet.OLEDB.4.0"
                && conn.Provider != "Microsoft.ACE.OLEDB.12.0")
                sqlCmd = sql.Replace('#', '\'');
            else
                sqlCmd = sql;
            using (OleDbCommand odc = new OleDbCommand(sql,conn))
            {
                int count = 1;
                foreach(object obj in pars)
                {
                    odc.Parameters.Add(new OleDbParameter("@p" + count, obj));
                    count++;
                }
                using (OleDbDataAdapter oda = new OleDbDataAdapter(odc))
                {
                    DataTable dt = new DataTable("Unknow");
                    oda.Fill(dt);
                    oda.Dispose();
                    return dt;
                }
            }
        }
        public DataTable GetDataTable(string sql, out OleDbDataAdapter oda)
        {
            string sqlCmd;
            if (this.conn.Provider != "Microsoft.Jet.OLEDB.4.0"
                && conn.Provider != "Microsoft.ACE.OLEDB.12.0")
                sqlCmd = sql.Replace('#', '\'');
            else
                sqlCmd = sql;
            oda = new OleDbDataAdapter(sqlCmd, conn);
            new OleDbCommandBuilder(oda);
            DataTable dt = new DataTable("Unknow");
            oda.Fill(dt);
            return dt;
        }

        public string[] GetProduceTypes()
        {
            string sqlCmd = "select distinct(PRODUCT_NAME) from TEST_RESULTS";
            DataTable dt = this.GetDataTable(sqlCmd);
            List<string> list = new List<string>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(row["PRODUCT_NAME"].ToString());
            }
            return list.ToArray();

        }

        #region IDisposable Members

        public void Dispose() 
        {
            try
            {
                if (this.conn != null && conn.State == ConnectionState.Open)
                {
                    this.conn.Close();
                }
            }
            catch (System.Exception)
            {
            }
            this.conn = null;
        }

        #endregion
    }
}
