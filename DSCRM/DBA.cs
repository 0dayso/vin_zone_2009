using System;
using System.Data;
using System.Configuration;
using Oracle.DataAccess.Client; // ODP.NET Oracle managed provider



/// <summary>
/// sysConfig 的摘要说明

/// </summary>
namespace DSCRM
{
    public class DBA
    {
        #region 执行sql文返回结果到DataSet
        /// <summary>
        /// 执行SQL文到DataSet:例子DataSet loginDS=sysConfig.exeusesql("Login.aspx.cs123",strsql,"DefaultSQL","dt");
        /// </summary>
        /// <param name="sql">SQL文</param>
        /// <param name="connectID">连接取"DefaultSQL","Oracle","Sybase"等值</param>
        /// <returns></returns>
        public static int SelectSQL(string strFileName, string sql, ref  DataSet ds, ref string ErrorInfo)
        {
            int iResult = -1;
            string strMyError = "";
            DataSet tempds = new DataSet();
            try
            {
                //查看日志设置级别
                //if (sql.ToLower().StartsWith("select"))
                //{

                //}
                //else
                //{
                //    if (strFileName.StartsWith("UserONline.aspx"))
                //    {

                //    }
                //    else
                //    {
                //        DBlog(strFileName, connectID, sql);//打log
                //    }
                //}

                OraDB conspy = new OraDB();
                conspy.Open();
                iResult = conspy.SelectDataToDataSet(tempds, sql, ref strMyError);
                ds = tempds.Copy();
                conspy.Close();

            }
            catch (Exception e)
            {
                //重新开始执行3次，如果失败就放弃


                //for (int i = 0; i < 3; i++)
                //{
                //    try
                //    {
                //        OLEDB  conspy = new OLEDB(connectID);
                //        conspy.Open();
                //        iResult = conspy.SelectDataToDataSet(tempds, sql, ref strMyError);
                //        conspy.Close();
                //        ds = tempds.Copy();
                //        break;
                //    }
                //    catch (Exception f)
                //    {
                //        ErrorInfo = f.Message;//设置错误信息
                //    }
                //}

                //DBErrorlog(strFileName, connectID, sql);//打log
                //DBErrorlog(strFileName, connectID, e.Message);
                //DBErrorlog(strFileName, "", "============================================================================================================");
            }
            return iResult;
        }

        #endregion

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

        public static int ExecSP(String SPName)
        {
            OraDB oledb = new OraDB();
            oledb.Open();
            int i = oledb.ExecSP(SPName);
            oledb.Close();
            return i;
        }

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
                conspy.Close();

            }
            catch (Exception e)
            {
                String lsError = e.Message;
                result = false;
            }
            return result;

        }

        #endregion
        public static OracleCommand GetOraCommand(String tsConnectId)
        {
            OraDB loOraDB = new OraDB();
            loOraDB.Open();
            loOraDB.BeginTransaction();
            return loOraDB.GetOraCommand();
        }
    } 
}
