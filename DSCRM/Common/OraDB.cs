using System;
using System.Collections.Generic;
using System.Text;
using System.Data;              
//using Oracle.DataAccess.Client; // ODP.NET Oracle managed provider
using System.Data.OracleClient;

namespace DSCRM
{
    public class OraDB
    {
        public string ConnectionString;
        private OracleConnection conn;
        private OracleTransaction myTrans;
        private int mnOraConnIndex;//连接池中的序号
        public OraDB()
        {
          
        }

        #region Open() 建立连接
        /// <summary>
        /// 建立连接
        /// </summary>
        public void Open()
        {

            conn = DBAdmin.GetConnection("DB");

        }
        #endregion

        #region Open() 建立连接
        /// <summary>
        /// 建立连接
        /// </summary>
        public void Open(String dbStr)
        {

            conn = DBAdmin.GetConnection(dbStr);

        }
        #endregion

        #region Close 关闭连接
        /// <summary>
        /// 关闭连接
        /// </summary>
        public void Close()
        {
            DBAdmin.FreeConnect(conn);
        }
        #endregion

        #region SelectDataToDataSet 根据sql文查询并放入DataSet
        /// <summary>
        /// 根据sql文查询并放入DataSet
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="sql"></param>
        /// <param name="tablename"></param>
        /// <returns></returns>
        public int SelectDataToDataSet(DataSet ds, string sql, ref string ErrorInfo)
        {
            int iRet = 1;
            try
            {
                
                OracleDataAdapter da = new OracleDataAdapter(sql, conn);
                da.Fill(ds);
            }
            catch (OracleException ex) // catches only Oracle errors
            {
                iRet = -1;
                switch (ex.Code)
                {
                    case 1:
                        ErrorInfo="Error attempting to insert duplicate data.";
                        return iRet;
                    case 12545:
                        ErrorInfo = "The database is unavailable.";
                        return iRet;
                    default:
                        ErrorInfo ="Database error:" + ex.Message.ToString();
                        return iRet;
                }
            }
            catch (Exception e)
            {
                iRet = -1;
                ErrorInfo = e.Message;

            }
            return iRet;
        }
        #endregion

        #region ExecSQL 执行SQL文
        /// <summary>
        /// 执行SQL文
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int ExecSQL(string sql, ref string ErrorInfo)
        {
            int ret=1;
            OracleCommand cmd = new OracleCommand(sql, conn);

            try
            {
                ret = cmd.ExecuteNonQuery();
            }
            catch (OracleException ex) // catches only Oracle errors
            {
                ret = -1;
                switch (ex.Code)
                {
                    case 1:
                        ErrorInfo = "Error attempting to insert duplicate data.";
                        return ret;
                    case 12545:
                        ErrorInfo = "The database is unavailable.";
                        return ret;
                    default:
                        ErrorInfo = "Database error:" + ex.Message.ToString();
                        return ret;
                }
            }
            catch (Exception e)
            {
                ret = -1;
                ErrorInfo = e.Message;
            }

            return ret;
        }
        #endregion
 

        #region BeginTransaction 开始事务

        /// <summary>
        /// 开始事务

        /// </summary>
        public void BeginTransaction()
        {
            myTrans = conn.BeginTransaction();
        }
        #endregion

        #region ExecSqlTrans 执行事务
        /// <summary>
        /// 执行事务
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int ExecSqlTrans(string sql)
        {
            int ret = 1;
            OracleCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            try
            {
                ret = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                ret = -1;
            }

            return ret;
        } 
        #endregion

        #region 创建Command对象
        /// <summary>
        /// 返回一个OracleCommand对象
        /// </summary>
        /// <returns></returns>
        public OracleCommand GetOraCommand()
        {
            OracleCommand loOraComm = new OracleCommand();
            loOraComm.Connection = conn;
            return loOraComm;
        }
        #endregion


        #region Commit 提交事务
        /// <summary>
        /// 提交事务
        /// </summary>
        public void Commit()
        {
            myTrans.Commit();
        }
        #endregion

        #region Rollback 回滚事务
        /// <summary>
        /// 回滚事务
        /// </summary>
        public void Rollback()
        {
            myTrans.Rollback();
        }
        #endregion

        #region 执行存储过程--取表的序号
        //执行存储过程--取表的序号
        public int ExecSP(String SPName)
        {
            int ret;
            OracleCommand cmd = new OracleCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandText = "GENERATEIDENTITY";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@PropertyName", OracleType.NVarChar, 50);
                cmd.Parameters.Add("@NewIdentityValue", OracleType.Number);
                ret = cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                ret = -1;
            }
            return ret;

        } 
        #endregion

    }
}
