using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
//using Oracle.DataAccess.Client; // ODP.NET Oracle managed provider
using System.Data.OracleClient;
using System.Collections;
using System.Xml;
using System.Data.OleDb;


/// <summary>

/// </summary>
namespace DSCRM
{
    public class DBA
    {
        #region 执行sql文返回结果到DataSet
        /// <summary>
        /// 执行SQL文到DataSet:例子DataSet loginDS=sysConfig.exeusesql("Login.aspx.cs123",strsql,"DefaultSQL","dt");
        /// </summary>
        /// <param name="strFileName"></param>
        /// <param name="sql"></param>
        /// <param name="ds"></param>
        /// <param name="ErrorInfo"></param>
        /// <returns></returns>
        public static int SelectSQL(string strFileName, string sql, ref  DataSet ds, ref string ErrorInfo)
        {
            int iResult = -1;
            string strMyError = "";
            DataSet tempds = new DataSet();
            OraDB conspy = new OraDB();
            conspy.Open();
            try
            {
                iResult = conspy.SelectDataToDataSet(tempds, sql, ref strMyError);
                ds = tempds.Copy();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conspy.Close();
            }
            return iResult;
        }

        #endregion

        #region  执行SQL文
        /// <summary>
        /// 执行SQL文

        /// </summary>
        /// <param name="sql">SQL文</param>
        /// <param name="connectID">连接取"DefaultSQL","Oracle","Sybase"等值</param>
        /// <returns>返回结果，错误返回-1</returns>
        public static int ExecSQL(string strFileName, string sql, ref string ErrorInfo)
        {
            
            OraDB myol = new OraDB();
            myol.Open();
            int i = myol.ExecSQL(sql,ref  ErrorInfo);
            myol.Close();
            return i;
        }

        #endregion

        #region 执行存储过程
        public static int ExecSP(String SPName)
        {
            OraDB oledb = new OraDB();
            oledb.Open();
            int i = oledb.ExecSP(SPName);
            oledb.Close();
            return i;
        }
        #endregion

        #region 带事务执行sql文
        /// <summary>
        /// 带事务执行SQL文:例子DataSet loginDS=sysConfig.exeusesql("Login.aspx.cs123",strsql,"DefaultSQL","dt");
        /// </summary>
        /// <param name="sql">SQL文</param>
        /// <param name="connectID">连接取"DefaultSQL","Oracle","Sybase"等值</param>
        /// <returns></returns>
        public static bool ExeuseSQL(string[] sql)
        {
            bool result = true;
            try
            {
                OraDB conspy = new OraDB();
                conspy.Open();
                conspy.BeginTransaction();
                try
                {
                    for (int i = 0; i < sql.Length; i++)
                    {
                        conspy.ExecSqlTrans(sql[i]);
                    }
                    conspy.Commit();

                }
                catch (Exception e)
                {
                    String lsError = e.Message;
                    conspy.Rollback();
                    result = false;
                }
                finally
                {
                    conspy.Close();
                }

            }
            catch (Exception e)
            {
                String lsError = e.Message;
                result = false;
            }
            return result;

        }
        #endregion
        
        #region 返回Command 自己执行带参数的sql
        /// <summary>
        /// 返回Command 自己执行带参数的sql
        /// </summary>
        /// <param name="tsConnectId"></param>
        /// <returns></returns>
        public static OracleCommand GetOraCommand()
        {
            OraDB loOraDB = new OraDB();
            loOraDB.Open();
            return loOraDB.GetOraCommand();
           
          
        }

        /// <summary>
        /// 返回Command 自己执行带参数的sql
        /// </summary>
        /// <param name="tsConnectId"></param>
        /// <returns></returns>
        public static OracleCommand GetOraCommand(String dbStr)
        {
            OraDB loOraDB = new OraDB();
            loOraDB.Open(dbStr);
            return loOraDB.GetOraCommand();


        }
        
        public static void RealseOraCommand(OracleCommand loCommand)
        {
            OracleConnection conn = loCommand.Connection;
            if (conn != null && conn.State == ConnectionState.Open)
                conn.Close();
        }

        #endregion

        #region 执行一个command，并将此连接关闭
        /// <summary>
        /// 执行一个command，并将此连接关闭
        /// </summary>
        /// <param name="loCommand"></param>
        public static int ExecuteNonQuery(OracleCommand loCommand)
        {
            int iReturn = -1;
            try
            {
                iReturn = loCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                RealseOraCommand(loCommand);
            }
            return iReturn;
        } 
        #endregion


        #region 执行一段代码(ExecuteNonQuery)
        /// <summary>
        /// 执行一段代码(ExecuteNonQuery)
        /// </summary>
        /// <param name="tsSQL">语句</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string tsSQL)
        {
            OracleCommand loOraCommand = DBA.GetOraCommand();
            loOraCommand.CommandText = tsSQL;
            int lnReturnValue = -1;
            try
            {
                lnReturnValue = loOraCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                RealseOraCommand(loOraCommand);
            }
            return lnReturnValue;

        }
        #endregion

        #region 获得一个连接
        /// <summary>
        /// 获得一个连接
        /// </summary>
        /// <param name="tsConnectId"></param>
        /// <param name="tnOraConnIndex"></param>
        /// <returns></returns>
        public static OracleConnection GetOraConnection(String tsConnectId, ref int tnOraConnIndex)
        {
            return DBPool.GetConnection(tsConnectId,ref tnOraConnIndex);
        }

        /// <summary>
        /// 获得一个连接
        /// </summary>
        /// <param name="tsConnectId"></param>
        /// <param name="tnOraConnIndex"></param>
        /// <returns></returns>
        public static OracleConnection GetOraConnection(String tsConnectId)
        {
            return DBAdmin.GetConnection(tsConnectId);
        }
        #endregion

        #region 释放一个连接
        /// <summary>
        /// 释放一个连接
        /// </summary>
        /// <param name="tnOraConnIndex"></param>
        public static void CloseOraConnection(int tnOraConnIndex)
        {
            DBPool.FreeConnect(tnOraConnIndex);
        }

        /// <summary>
        /// 释放一个连接
        /// </summary>
        /// <param name="tnOraConn"></param>
        public static void CloseOraConnection(OracleConnection tnOraConn)
        {
            DBAdmin.FreeConnect(tnOraConn);
        }
        #endregion

        #region 填充数据表

        public static DataTable GetDataTable(string tsSelectCommandText)
        {
            DataTable dt = new DataTable();
            DBA.FillDataTable(dt, tsSelectCommandText);
            return dt;
        }

        public static void FillDataTable(DataTable toTable, string tsSelectCommandText, string tsFields)
        {
            DBA.FillDataTable(toTable, tsSelectCommandText,null,tsFields, null, true, -1, -1);
        }

        public static void FillDataTable(DataTable toTable, string tsSelectCommandText)
        {
            DBA.FillDataTable(toTable, tsSelectCommandText, null, null, null, true, -1, -1);
        }

        public static void FillDataTable(DataTable toTable, string tsSelectCommandText,OracleCommand toOracleCommand)
        {
            DBA.FillDataTable(toTable, tsSelectCommandText, toOracleCommand, null, null, true, -1, -1);
        }

        public static void FillDataTable(DataTable toTable, string tsSelectCommandText, int tnStartIndex, int tnAffectedRowCount)
        {
            DBA.FillDataTable(toTable, tsSelectCommandText,null, null, null, false, tnStartIndex, tnAffectedRowCount);
        }

        public static void FillDataTable(DataTable toTable, string tsSelectCommandText, string tsKeyFields, bool tbFillSynchronize)
        {
            DBA.FillDataTable(toTable, tsSelectCommandText,null, null, tsKeyFields, false, -1, -1);
        }
        /// <summary>
        /// 根据查询语句填允数据表.
        /// </summary>
        /// <param name="toTable">数据表</param>
        /// <param name="tsSelectCommandText">查询语句</param>
        /// <param name="tsFields"> 
        /// </param>
        private static void FillDataTable(DataTable toTable, string tsSelectCommandText, OracleCommand toOracleCommand, string tsFields, string tsKeyFields, bool tbFillSynchronize, int tnStartIndex, int tnAffectedRowCount)
        {
            OracleCommand loOraCommand =null;
            bool isUsedOtherCmd = false;//是否使用外部的Command
            if (toOracleCommand != null)
            {
                loOraCommand = toOracleCommand;
                isUsedOtherCmd = true;
            }else
                loOraCommand = DBA.GetOraCommand();
           

            loOraCommand.CommandText = tsSelectCommandText;
           try
            {
             OracleDataReader loReader = loOraCommand.ExecuteReader();
           
                // 初始化要填允的字段
                string[] lsFields = null;
                System.Type[] loFieldTypes = null;

                if (tsFields != "" && tsFields != null)
                {
                    lsFields = tsFields.Split(new char[] { ',' });
                }
                else
                {
                    lsFields = new string[loReader.FieldCount];
                    loFieldTypes = new Type[loReader.FieldCount];

                    for (int lnIndex = 0; lnIndex < loReader.FieldCount; lnIndex++)
                    {
                        lsFields[lnIndex] = loReader.GetName(lnIndex);
                        loFieldTypes[lnIndex] = loReader.GetFieldType(lnIndex);
                    }
                }

                // 找到主键.
                string[] lsKeyFields = null;
                if (tsKeyFields != null && tsKeyFields != "")
                {
                    lsKeyFields = tsKeyFields.Split(new char[] { ',' });
                }


                DataColumn[] loDataColumns = new DataColumn[lsFields.Length];
                DataColumn loColumn = null;
                for (int lnIndex = 0; lnIndex < lsFields.Length; lnIndex++)
                {
                    loColumn = toTable.Columns[lsFields[lnIndex]];
                    if (loColumn == null)
                    {
                        loColumn = new DataColumn(lsFields[lnIndex], loFieldTypes[lnIndex]);
                        toTable.Columns.Add(loColumn);
                    }
                    loDataColumns[lnIndex] = loColumn;
                }



                DataRow loRow = null;
                int lnRowIndex = 0;
                while (loReader.Read())
                {
                    if (tnStartIndex < 0 ||
                        (tnStartIndex >= 0 && lnRowIndex >= tnStartIndex && lnRowIndex < tnStartIndex + tnAffectedRowCount))
                    {
                        if (lsKeyFields == null)
                            loRow = toTable.NewRow();
                        else
                        {

                            object[] loKeyValues = new object[lsKeyFields.Length];
                            for (int lnIndex = 0; lnIndex < lsKeyFields.Length; lnIndex++)
                                loKeyValues[lnIndex] = loReader[lsKeyFields[lnIndex]];

                            Array loFoundRows = FindRows(toTable.Rows, lsKeyFields, loKeyValues, true);
                            if (loFoundRows.Length > 0) loRow = (DataRow)loFoundRows.GetValue(0);
                            else
                            {
                                if (tbFillSynchronize)
                                {
                                    loReader.Close();
                                    throw new Exception("不能找到要装载数据", null);
                                }
                            }
                        }

                        if (loRow != null)
                        {
                            for (int lnIndex = 0; lnIndex < loDataColumns.Length; lnIndex++)
                            {
                                loRow[loDataColumns[lnIndex]] = loReader[lnIndex];
                            }
                            if (lsKeyFields == null) toTable.Rows.Add(loRow);
                        }
                    }
                    else if (tnStartIndex >= 0 && lnRowIndex > tnStartIndex + tnAffectedRowCount)
                        break;
                    lnRowIndex++;
                }
               loReader.Close();
            }
            finally
            {
               
                if(!isUsedOtherCmd)
                    DBA.RealseOraCommand(loOraCommand);
            }
            toTable.AcceptChanges();
        }

        public static Array FindRows(IEnumerable toRows, string[] tsFieldNames, object[] toValues, bool tbFindFirst)
        {
            ArrayList loRows = new ArrayList();
            DataRow loRow;
            int lnIndex;
            bool lbIsEqual;

            foreach (object loObject in toRows)
            {
                if (loObject is DataRow)
                {
                    loRow = (DataRow)loObject;
                }
                else if (loObject is DataRowView)
                {
                    loRow = ((DataRowView)loObject).Row;
                }
                else
                {
                    loRow = null;
                }
                if (loRow != null && loRow.RowState != DataRowState.Deleted)
                {
                    lbIsEqual = true;
                    for (lnIndex = 0; lnIndex < tsFieldNames.Length; lnIndex++)
                    {
                        if (!loRow[tsFieldNames[lnIndex]].Equals(toValues[lnIndex]))
                        {
                            lbIsEqual = false;
                            break;
                        }
                    }
                    if (lbIsEqual)
                    {
                        loRows.Add(loRow);
                        if (tbFindFirst) break;
                    }
                }
            }
            return loRows.ToArray();
        }
        #endregion

        #region 填允XML
        public string GenXML(string tsSelectCommandText)
        {
            string msTableName = "Item";
            string msXmlScript = "";
            OracleCommand loOraCommand = DBA.GetOraCommand();
            loOraCommand.CommandText = tsSelectCommandText;
            try
            {
            OracleDataReader loDataReader = loOraCommand.ExecuteReader();
            
                object[] loValues = new object[loDataReader.FieldCount];
                int lnColIndex;

                System.IO.StringWriter loStringWriter = new System.IO.StringWriter();
                XmlTextWriter loWriter = new XmlTextWriter(loStringWriter);

                while (loDataReader.Read())
                {
                    loDataReader.GetValues(loValues);
                    loWriter.WriteStartElement(msTableName);
                    for (lnColIndex = 0; lnColIndex < loDataReader.FieldCount; lnColIndex++)
                    {
                        loWriter.WriteAttributeString(loDataReader.GetName(lnColIndex), loValues[lnColIndex].ToString());
                    }
                    loWriter.WriteEndElement();
                }
                msXmlScript += loStringWriter.ToString();
                loWriter.Close();
               loDataReader.Close();
            }
            finally
            {
                
                DBA.RealseOraCommand(loOraCommand);
            }
            return msXmlScript;
        }

        #endregion

        #region 通过查询语句返回一个值
        /// <summary>
        /// 通过查询语句返回一个值
        /// </summary>
        /// <param name="tsSql">查询语句</param>
        /// <returns>查询结果</returns>
        public static object ExecuteScalar(string tsSql)
        {
            OracleCommand loOraCommand = DBA.GetOraCommand();
            loOraCommand.CommandText = tsSql;
            try
            {
                object loValue = loOraCommand.ExecuteScalar();
                return loValue;
            }
            finally
            {
                DBA.RealseOraCommand(loOraCommand);
            }
        }
        #endregion

        #region 通过查询语句返回一个值
        /// <summary>
        /// 通过查询语句返回一个值
        /// </summary>
        /// <param name="tsSql">查询语句</param>
        /// <returns>查询结果</returns>
        public static object ExecuteScalar(string tsSql,String dbStr)
        {
            OracleCommand loOraCommand = DBA.GetOraCommand(dbStr);
            loOraCommand.CommandText = tsSql;
            try
            {
                object loValue = loOraCommand.ExecuteScalar();
                return loValue;
            }
            finally
            {
                DBA.RealseOraCommand(loOraCommand);
            }
        }
        #endregion

        #region ExeSqlsTrans事务处理sql语句集合
        public static bool ExeSqlsTrans(ArrayList sSqls)
        {
            bool bReturn = false;
           
            OracleConnection loOraConn = DSCRM.DBA.GetOraConnection("DB");
            OracleTransaction loOraTrans = loOraConn.BeginTransaction();

            OracleCommand loOraComm = loOraConn.CreateCommand();

            try
            {
                loOraComm.Transaction = loOraTrans;
                for (int i = 0; i < sSqls.Count; i++)
                {
                    loOraComm.CommandText = sSqls[i].ToString();
                    loOraComm.ExecuteNonQuery();
                }
                loOraTrans.Commit();
                bReturn = true;
            }
            catch (Exception ex)
            {
                loOraTrans.Rollback();
                throw ex;
            }
            finally
            {
                DSCRM.DBA.CloseOraConnection(loOraConn);
               
            }
            return bReturn;
        }
        #endregion

        #region 带事务处理sql语句集合
        public static void ExeSqlsTrans(List<string> tsSQLs)
        {
            
            OracleConnection loOraConn = DSCRM.DBA.GetOraConnection("DB");
            OracleTransaction loOraTrans = loOraConn.BeginTransaction();
            OracleCommand loOraComm = loOraConn.CreateCommand();
            try
            {
                loOraComm.Transaction = loOraTrans;
                foreach (string lsSQl in tsSQLs)
                {
                    loOraComm.CommandText = lsSQl;
                    loOraComm.ExecuteNonQuery();

                }
                loOraTrans.Commit();
            }
            catch (Exception ex)
            {
                loOraTrans.Rollback();
                throw ex;
            }
            finally
            {
                 DSCRM.DBA.CloseOraConnection(loOraConn);
               
            }
        }
        #endregion

        #region 带事务处理sql语句集合
        public static void ExeSqlsTrans(string[] tsSQLs)
        {
            
            OracleConnection loOraConn = DSCRM.DBA.GetOraConnection("DB");
            OracleTransaction loOraTrans = loOraConn.BeginTransaction();
            OracleCommand loOraComm = loOraConn.CreateCommand();
            try
            {
                loOraComm.Transaction = loOraTrans;
                foreach (string lsSQl in tsSQLs)
                {
                    if (!string.IsNullOrEmpty(lsSQl))
                    {
                        loOraComm.CommandText = lsSQl;
                        loOraComm.ExecuteNonQuery();
                    }

                }
                loOraTrans.Commit();
            }
            catch (Exception ex)
            {
                loOraTrans.Rollback();
                throw ex;
            }
            finally
            {
                 DSCRM.DBA.CloseOraConnection(loOraConn);
                
            }
        }
        #endregion

        #region 分页
        public static DataTable PageDataTable(int intCurrentPage, int intPageSize, string strSql)
        {
            OracleCommand loOraCommand = DBA.GetOraCommand();
            loOraCommand.CommandText = strSql;
            try
            {
                int StartIndex;
                StartIndex = intCurrentPage * intPageSize;
                DataSet ds = new DataSet();

                OracleDataAdapter MyAdapter = new OracleDataAdapter();
                MyAdapter.SelectCommand = loOraCommand;

                MyAdapter.Fill(ds, StartIndex, intPageSize, "Page");
                return ds.Tables["Page"];
            }
            finally
            {
                DBA.RealseOraCommand(loOraCommand);
            }
            
           
        }
        #endregion

        #region 分页
        public static DataTable PageDataTable(int intCurrentPage, int intPageSize, string strSql,String dbStr)
        {
            OracleCommand loOraCommand = DBA.GetOraCommand(dbStr);
            loOraCommand.CommandText = strSql;
            try
            {
                int StartIndex;
                StartIndex = intCurrentPage * intPageSize;
                DataSet ds = new DataSet();

                OracleDataAdapter MyAdapter = new OracleDataAdapter();
                MyAdapter.SelectCommand = loOraCommand;

                MyAdapter.Fill(ds, StartIndex, intPageSize, "Page");
                return ds.Tables["Page"];
            }
            finally
            {
                DBA.RealseOraCommand(loOraCommand);
            }


        }
        #endregion
    } 
}
