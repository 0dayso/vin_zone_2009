using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using MyFramework.DAL;

namespace MyFramework.BusinessLogic.Common
{
    /// <summary>
    /// 主要封装了和数据库有关的操作
    /// </summary>
    public class CommonDBFunction
    {
        public static Boolean DBDelete(String tsSQL)
        {

            try
            {
                String lsError = "";
                DataSet ds = new DataSet();

                if (DBA.SelectSQL("", tsSQL, ref ds, ref lsError) > 0)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {

                return false;
            }
            return false;
        }
        //生成每个数据表的最大Id
        public static long GenerateIdentity(String tsName)
        {

            OracleCommand loOraComm = DBA.GetOraCommand();
            try
            {
                if (tsName == "")
                    return -1;
                loOraComm.CommandText = "GenerateIdentity";
                loOraComm.CommandType = CommandType.StoredProcedure;
                OracleParameter parameter = loOraComm.CreateParameter();
                parameter.ParameterName = "PropertyName";
                parameter.OracleType = OracleType.NVarChar;
                parameter.Size = 50;
                parameter.SourceColumn = "Name";
                parameter.Value = tsName;
                loOraComm.Parameters.Add(parameter);
                parameter = loOraComm.CreateParameter();
                parameter.ParameterName = "NewIdentityValue";
                parameter.OracleType = OracleType.Number;
                parameter.Direction = ParameterDirection.Output;
                loOraComm.Parameters.Add(parameter);
                loOraComm.ExecuteNonQuery();

                if (Convert.ToInt64(parameter.Value) == 0)
                {
                    throw new Exception("此表未初始化");
                }
                else
                    return Convert.ToInt64(parameter.Value);
            }
            catch (Exception ex)
            {
                throw new Exception("取表主键出错！", ex);
            }
            finally
            {
                DBA.RealseOraCommand(loOraComm);
            }

        }
        /// <summary>
        /// 根据数据库的序列查找最大ID
        /// </summary>
        /// <param name="tsName">序列名</param>
        /// <returns></returns>
        public static long GenerateSEQIdentity(String tsName)
        {

            OracleCommand loOraComm = DBA.GetOraCommand();
            try
            {
                if (tsName == "")
                    return -1;
                loOraComm.CommandText = "GenerateIdentity2";
                loOraComm.CommandType = CommandType.StoredProcedure;
                OracleParameter parameter = loOraComm.CreateParameter();
                parameter.ParameterName = "PropertyName";
                parameter.OracleType = OracleType.NVarChar;
                parameter.Size = 50;
                parameter.SourceColumn = "Name";
                parameter.Value = tsName;
                loOraComm.Parameters.Add(parameter);
                parameter = loOraComm.CreateParameter();
                parameter.ParameterName = "NewIdentityValue";
                parameter.OracleType = OracleType.Number;
                parameter.Direction = ParameterDirection.Output;
                loOraComm.Parameters.Add(parameter);
                loOraComm.ExecuteNonQuery();

                if (Convert.ToInt64(parameter.Value) == 0)
                {
                    throw new Exception("此表没有相应的序列");
                }
                else
                    return Convert.ToInt64(parameter.Value);
            }
            catch (Exception ex)
            {
                throw new Exception("根据序列取表主键出错！", ex);
            }
            finally
            {
                DBA.RealseOraCommand(loOraComm);
            }

        }
        //更新房态基本信息表
        public static void UpdateHotelStatus(EnumDef.EHRoomStatusType tnUpdateStyle, int tnUpdateNum, DateTime tsUpdateDate, String tsHBaseInfoId, int tnEmployeeID)
        {
            String lsSQL = "SELECT * FROM HOTELSTATUS WHERE HOTELINFOID = " + tsHBaseInfoId.Trim() + " AND USEDATE = to_date('" + tsUpdateDate + "','yyyy-mm-dd hh24:mi:ss')";
            int lnRoomBuyNum = 0;
            int lnRoomHold = 0;

            DataSet ds = new DataSet();
            String lsError = "";
            if (DBA.SelectSQL("", lsSQL, ref ds, ref lsError) > 0)
            {

                switch (tnUpdateStyle)
                {
                    case EnumDef.EHRoomStatusType.买房:
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            lnRoomBuyNum += IntUtil.SafeCInt(ds.Tables[0].Rows[0]["BUYHOUSENUM"]) + tnUpdateNum;
                        }

                        break;
                    case EnumDef.EHRoomStatusType.占房:
                        break;
                    case EnumDef.EHRoomStatusType.预留房:
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            lnRoomHold += IntUtil.SafeCInt(ds.Tables[0].Rows[0]["OBLIGATEHOUSENUM"]) + tnUpdateNum;
                        }
                        break;
                    case EnumDef.EHRoomStatusType.满房:
                        break;
                }



                String lsSQLUpdate = @"update HOTELSTATUS set  BuyHouseNum = :BuyHouseNum,OBLIGATEHOUSENUM = :OBLIGATEHOUSENUM
                                        where HOUSESTATETYPEID = :HOUSESTATETYPEID ";

                OracleConnection loOraConn = DBA.GetOraConnection("CRMDB");
                try
                {
                    OracleCommand loOraComm = new OracleCommand(lsSQLUpdate, loOraConn);

                    DBUtil.AddParameter(loOraComm, "BuyHouseNum", lnRoomBuyNum);
                    DBUtil.AddParameter(loOraComm, "OBLIGATEHOUSENUM", lnRoomHold);
                    DBUtil.AddParameter(loOraComm, "HOUSESTATETYPEID", IntUtil.SafeCInt(ds.Tables[0].Rows[0]["HOUSESTATETYPEID"]));

                    loOraComm.ExecuteNonQuery();
                }
                finally
                {
                    DBA.CloseOraConnection(loOraConn);

                }
            }
            else
            {
                switch (tnUpdateStyle)
                {
                    case EnumDef.EHRoomStatusType.买房:
                        lnRoomBuyNum += tnUpdateNum;

                        break;
                    case EnumDef.EHRoomStatusType.占房:
                        break;
                    case EnumDef.EHRoomStatusType.预留房:
                        lnRoomHold += tnUpdateNum;
                        break;
                    case EnumDef.EHRoomStatusType.满房:
                        break;
                }

                String lsSQLInsert = @"insert into  HOTELSTATUS (HOUSESTATETYPEID,HOTELINFOID,USEDATE,BUYHOUSENUM,OBLIGATEHOUSENUM,OPERATETIME,EmployeeInfoID)
                                          Values(:HOUSESTATETYPEID,:HOTELINFOID,:USEDATE,:BUYHOUSENUM,:OBLIGATEHOUSENUM,:OPERATETIME,:EmployeeInfoID)";


                OracleConnection loOraConn = DBA.GetOraConnection("CRMDB");
                try
                {
                    OracleCommand loOraComm = new OracleCommand(lsSQLInsert, loOraConn);

                    DBUtil.AddParameter(loOraComm, "HOUSESTATETYPEID", IntUtil.SafeCInt(CommonDBFunction.GenerateIdentity("HOTELSTATUS")));
                    DBUtil.AddParameter(loOraComm, "HOTELINFOID", IntUtil.SafeCInt(tsHBaseInfoId));
                    DBUtil.AddParameter(loOraComm, "USEDATE", Convert.ToDateTime(tsUpdateDate));
                    DBUtil.AddParameter(loOraComm, "BUYHOUSENUM", lnRoomBuyNum);
                    DBUtil.AddParameter(loOraComm, "OBLIGATEHOUSENUM", lnRoomHold);
                    DBUtil.AddParameter(loOraComm, "OPERATETIME", DateUitl.GetDateTime());
                    DBUtil.AddParameter(loOraComm, "EmployeeInfoID", tnEmployeeID);

                    loOraComm.ExecuteNonQuery();
                }
                finally
                {
                    DBA.CloseOraConnection(loOraConn);

                }
            }


        }


        public static Boolean DBCheckBoxCtr(String tsDeleteSQL, DataTable tDtCheckBoxCtr)
        {
            tsDeleteSQL += " " + DatatableUpdate.GenerateInsertSql(tDtCheckBoxCtr);
            try
            {
                String lsError = "";
                DataSet ds = new DataSet();
                if (DBA.SelectSQL("", tsDeleteSQL, ref ds, ref lsError) > 0)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {

                return false;
            }
            return false;
        }


        #region 获得DropDownList数据源
        /// <summary>
        /// 获得DropDownList数据源
        /// </summary>
        /// <param name="tsTableName"></param>
        /// <returns></returns>
        /// create by zhb 2007-9-28
        public static DataTable getDropDownListDataSource(String tsTableName)
        {
            String lsSQL = "select * from " + tsTableName;

            String lsError = "";
            DataSet ds = new DataSet();
            if (DBA.SelectSQL("", lsSQL, ref ds, ref lsError) > 0)
            {
                return ds.Tables[0];
            }
            return null;
        }
        #endregion

        #region 执行SQL文列表
        /// <summary>
        /// 执行SQL文列表
        /// create by zhb 2007-10-19
        /// </summary>
        /// <param name="tsSQLs"></param>
        /// <returns></returns>
        public static Boolean ExecuteSQL(List<String> tsSQLs)
        {
            if (tsSQLs.Count == 0)
                return false;

            OracleConnection loOraConn = DAL.DBA.GetOraConnection("CRMDB");
            OracleTransaction loOraTran = loOraConn.BeginTransaction();
            try
            {

                OracleCommand loOraComm = loOraConn.CreateCommand();
                loOraComm.Transaction = loOraTran;

                foreach (String lsSQL in tsSQLs)
                {
                    loOraComm.CommandText = lsSQL;
                    loOraComm.ExecuteNonQuery();
                }

                loOraTran.Commit();

                return true;
            }
            catch (Exception ex)
            {
                loOraTran.Rollback();
                throw new Exception("执行语句出错！", ex);
                return false;
            }
            finally
            {
                DAL.DBA.CloseOraConnection(loOraConn);
            }
        }
        #endregion



        #region 执行SQL文列表
        /// <summary>
        /// 执行SQL文列表
        /// </summary>
        /// <param name="tsSQLs"></param>
        /// <returns></returns>
        public static Boolean ExecuteSQL(List<String> tsSQLs, OracleCommand loOraComm)
        {
            try
            {
                foreach (String lsSQL in tsSQLs)
                {
                    loOraComm.CommandText = lsSQL;
                    loOraComm.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("执行语句出错！", ex);
                return false;
            }
        }
        #endregion

        /// <summary>
        /// 根据货币Id查询汇率
        /// </summary>
        /// <param name="tscurrency_id"></param>
        /// <returns></returns>
        public static String getCurrencyRate(String tsCurrency_id)
        {
            String lsSQL = "select currency_rate from t_sd_currency where currency_id =" + tsCurrency_id;
            return DBA.ExecuteScalar(lsSQL).ToString();
        }
        public static double getCurrencyRate(int tnCurrency_id, string tsMonth)
        {
            object loCurrencyRate = DBA.ExecuteScalar("select f_HO_getCurrencyRate(" + tnCurrency_id + ",'" + tsMonth.Trim() + "') from t_SD_Currency_Rate ");
            if (loCurrencyRate == null || loCurrencyRate == DBNull.Value)
                return 1.0;
            else
                return DouUtil.SafeCDou(loCurrencyRate);
        }
        //判断是否有某个页面的权限
        public static Boolean ValidatePageRight(int tnEmployeeId, int tnPageId)
        {
            string lsSql = @" select c.Page_Id from t_S_Employee_Role a 
                     inner join t_S_Role b on a.ROLE_ID=b.Role_ID
                     inner join t_S_Role_Page c on b.Role_ID=c.Role_ID
                     where a.Valid_Date<Sysdate and a.Invalid_Date>Sysdate and a.Employee_ID=" + tnEmployeeId + @" and Page_Id=" + tnPageId + @"
                      union 
                     select Page_ID from t_S_Employee_Module_Add d
                     where d.Valid_Date<Sysdate and d.Invalid_Date>Sysdate and d.Employee_ID=" + tnEmployeeId + @" and Page_Id=" + tnPageId;

            DataTable lodtpage = new DataTable();

            int lnPageid = IntUtil.SafeCInt(DAL.DBA.ExecuteScalar(lsSql));
            if (lnPageid == tnPageId)
                return true;
            else
                return false;
        }

        #region 获得天气信息 alter by wshs | time 08-5-28
        /// <summary>
        /// 获得天气信息
        /// </summary>
        /// <param name="cityname">城市名称*模糊</param>
        /// <param name="report_time">预报时间/2008-5-8/</param>
        /// <returns>DataTable</returns>
        public static DataTable GetWeatherReport(string cityname, string report_time)
        {
            /*格式  WEATHER_REPORT_ID(编号)	REPORT_TIME(预报时间)	CITY(城市名称) WEATHER(例:多云转雷阵雨)    TEMPERATURE(例:7℃ / 19℃)	
              WIND_DIRECTION(例:东南风5-6级转东北风5-6级)	WIND_POWER(例:4-5 or "")	GIF(例:duoyun_small.gif|leizhenyu_small.gif)--以'|'分割 */

            /*注:　 每城市预报三天内的天气数据　更新时间：　每天早上9：00　晚上　10：00*/

            string sql = @" select t.* from t_s_weather_report t where (1=1) ";

            if (!string.IsNullOrEmpty(cityname))
                sql += " and t.city like '%" + cityname + "%'";

            if (!string.IsNullOrEmpty(report_time))
                sql += " and t.report_time = to_date('" + report_time + "','yyyy-mm-dd')";

            DataSet ds = new DataSet();
            string error = string.Empty;
            if (DAL.DBA.SelectSQL("", sql, ref ds, ref error) > 0)
            {
                return ds.Tables[0];
            }

            return new DataTable();
        }
        #endregion
    }
}
