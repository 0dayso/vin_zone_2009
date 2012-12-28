using System;
using System.Collections.Generic;
using System.Text;

using Models;
using CRM.Buzlogic.Common;
using System.Data.OracleClient;
using System.Data;
namespace BLL.Elong
{
    public class AirLineOperation
    {
        #region 添加价格比较数据
        /// <summary>
        /// 添加价格比较数据
        /// </summary>
        /// <param name="priceCompare"></param>
        /// <returns></returns>
        public bool AddPriceCompare(FlightPriceCompare priceCompare)
        {
            //创建命令对象
            OracleCommand oracleCommand = DSCRM.DBA.GetOraCommand();

            //构建sql插入语句
            StringBuilder sbInsertSql = new StringBuilder();
            sbInsertSql.Append(" insert into T_TP_PRICE_COMPARE( COMPAREID,OTHERPRICEID,LOWEST_PRICE_ID,AUTORESULT,HANDRESULT,MEMO) ");
            sbInsertSql.Append(" values (:COMPAREID,:OTHERPRICEID,:LOWEST_PRICE_ID,:AUTORESULT,:HANDRESULT,:MEMO) ");

            oracleCommand.CommandText = sbInsertSql.ToString();

            // 装载参数
            DBUtil.AddParameter(oracleCommand, "COMPAREID", (Int32)CommonDBFunction.GenerateIdentity("T_TP_PRICE_COMPARE"));
            DBUtil.AddParameter(oracleCommand, "OTHERPRICEID", priceCompare.Otherpriceid);
            DBUtil.AddParameter(oracleCommand, "LOWEST_PRICE_ID", priceCompare.LowestPriceId);
            DBUtil.AddParameter(oracleCommand, "AUTORESULT", priceCompare.Autoresult);
            DBUtil.AddParameter(oracleCommand, "HANDRESULT", priceCompare.Handresult);
            DBUtil.AddParameter(oracleCommand, "MEMO", priceCompare.Memo);

            int count = 0;

            //捕获异常
            try
            {
                count = DSCRM.DBA.ExecuteNonQuery(oracleCommand);
            }
            catch
            {
            }

            return count > 0;
        }
        #endregion

        #region 添加其它价格

        /// <summary>
        /// 添加其它价格并返回主键ID
        /// </summary>
        public int AddOtherPrice(FlightOtherPrice otherPrice)
        {
            //创建命令对象
            OracleCommand oracleCommand = DSCRM.DBA.GetOraCommand();

            //构建sql插入语句
            StringBuilder sbInsertSql = new StringBuilder();
            sbInsertSql.Append(" insert into T_TP_OTHER_PRICE( OTHERPRICEID,SOURCETYPE,DEPARTURE,ARRIVAL,CREATE_DATE,LOWEST_PRICE,AIRLINER,FLIGHT,CABIN) ");
            sbInsertSql.Append(" values (:OTHERPRICEID,:SOURCETYPE,:DEPARTURE,:ARRIVAL,:CREATE_DATE,:LOWEST_PRICE,:AIRLINER,:FLIGHT,:CABIN) ");

            oracleCommand.CommandText = sbInsertSql.ToString();

            int recordIndex = (Int32)CommonDBFunction.GenerateIdentity("T_TP_OTHER_PRICE");

            // 装载参数
            DBUtil.AddParameter(oracleCommand, "OTHERPRICEID", recordIndex);
            DBUtil.AddParameter(oracleCommand, "SOURCETYPE", otherPrice.Sourcetype);
            DBUtil.AddParameter(oracleCommand, "DEPARTURE", otherPrice.Departure);
            DBUtil.AddParameter(oracleCommand, "ARRIVAL", otherPrice.Arrival);
            DBUtil.AddParameter(oracleCommand, "CREATE_DATE", otherPrice.CreateDate);
            DBUtil.AddParameter(oracleCommand, "LOWEST_PRICE", otherPrice.LowestPrice);
            DBUtil.AddParameter(oracleCommand, "AIRLINER", otherPrice.Airliner);
            DBUtil.AddParameter(oracleCommand, "FLIGHT", otherPrice.Flight);
            DBUtil.AddParameter(oracleCommand, "CABIN", otherPrice.Cabin);



            //捕获异常
            try
            {
                DSCRM.DBA.ExecuteNonQuery(oracleCommand);

                return recordIndex;
            }
            catch
            {
                return 0;
            }

        }
        #endregion

        #region 添加最低价格

        /// <summary>
        /// 添加最低价格
        /// </summary>
        public bool AddLowestPrice(FlightLowestPrice lowestPrice)
        {
            //创建命令对象
            OracleCommand oracleCommand = DSCRM.DBA.GetOraCommand();

            //构建sql插入语句
            StringBuilder sbInsertSql = new StringBuilder();
            sbInsertSql.Append(" insert into T_TP_FLIGHT_LOWEST_PRICE( LOWEST_PRICE_ID,DEPARTURE,ARRIVAL,CREATE_DATE,LOWEST_PRICE,DEPARTURE_TIME,ARRIVAL_TIME,AIRLINER,FLIGHT,CABIN) ");
            sbInsertSql.Append(" values (:LOWEST_PRICE_ID,:DEPARTURE,:ARRIVAL,:CREATE_DATE,:LOWEST_PRICE,:DEPARTURE_TIME,:ARRIVAL_TIME,:AIRLINER,:FLIGHT,:CABIN) ");

            oracleCommand.CommandText = sbInsertSql.ToString();

            // 装载参数
            DBUtil.AddParameter(oracleCommand, "LOWEST_PRICE_ID", (Int32)CommonDBFunction.GenerateIdentity("T_TP_FLIGHT_LOWEST_PRICE"));
            DBUtil.AddParameter(oracleCommand, "DEPARTURE", lowestPrice.Departure);
            DBUtil.AddParameter(oracleCommand, "ARRIVAL", lowestPrice.Arrival);
            DBUtil.AddParameter(oracleCommand, "CREATE_DATE", lowestPrice.CreateDate);
            DBUtil.AddParameter(oracleCommand, "LOWEST_PRICE", lowestPrice.LowestPrice);
            DBUtil.AddParameter(oracleCommand, "DEPARTURE_TIME", lowestPrice.DepartureTime);
            DBUtil.AddParameter(oracleCommand, "ARRIVAL_TIME", lowestPrice.ArrivalTime);
            DBUtil.AddParameter(oracleCommand, "AIRLINER", lowestPrice.Airliner);
            DBUtil.AddParameter(oracleCommand, "FLIGHT", lowestPrice.Flight);
            DBUtil.AddParameter(oracleCommand, "CABIN", lowestPrice.Cabin);

            int count = 0;

            //捕获异常
            try
            {
                count = DSCRM.DBA.ExecuteNonQuery(oracleCommand);
            }
            catch
            {
            }

            return count > 0;
        }
        #endregion

        #region 查询最低价
        /// <summary>
        /// 查询最低价
        /// </summary>
        /// <returns></returns>
        public FlightLowestPrice QueryLowestPrice()
        {
            string strSql = "select * from t_TP_Flight_Lowest_Price where rownum < 2 order by lowest_price_id desc ";

            FlightLowestPrice lowestPrice = new FlightLowestPrice();

            //创建命令对象
            OracleCommand oracleCommand = DSCRM.DBA.GetOraCommand();
            oracleCommand.CommandText = strSql;

            //执行完关闭连接
            using (OracleDataReader oracleDataReader = oracleCommand.ExecuteReader(CommandBehavior.CloseConnection))
            {
                //读取数据成功返回true
                if (oracleDataReader.Read())
                {

                    lowestPrice.LowestPriceId = Convert.ToInt32(oracleDataReader["LOWEST_PRICE_ID"]);

                    if (oracleDataReader["DEPARTURE"] != DBNull.Value)
                    {
                        lowestPrice.Departure = Convert.ToString(oracleDataReader["DEPARTURE"]);
                    }

                    if (oracleDataReader["ARRIVAL"] != DBNull.Value)
                    {
                        lowestPrice.Arrival = Convert.ToString(oracleDataReader["ARRIVAL"]);
                    }

                    if (oracleDataReader["CREATE_DATE"] != DBNull.Value)
                    {
                        lowestPrice.CreateDate = Convert.ToDateTime(oracleDataReader["CREATE_DATE"]);
                    }

                    if (oracleDataReader["LOWEST_PRICE"] != DBNull.Value)
                    {
                        lowestPrice.LowestPrice = Convert.ToDouble(oracleDataReader["LOWEST_PRICE"]);
                    }

                    if (oracleDataReader["DEPARTURE_TIME"] != DBNull.Value)
                    {
                        lowestPrice.DepartureTime = Convert.ToDateTime(oracleDataReader["DEPARTURE_TIME"]);
                    }

                    if (oracleDataReader["ARRIVAL_TIME"] != DBNull.Value)
                    {
                        lowestPrice.ArrivalTime = Convert.ToDateTime(oracleDataReader["ARRIVAL_TIME"]);
                    }

                    if (oracleDataReader["AIRLINER"] != DBNull.Value)
                    {
                        lowestPrice.Airliner = Convert.ToString(oracleDataReader["AIRLINER"]);
                    }

                    if (oracleDataReader["FLIGHT"] != DBNull.Value)
                    {
                        lowestPrice.Flight = Convert.ToString(oracleDataReader["FLIGHT"]);
                    }

                    if (oracleDataReader["CABIN"] != DBNull.Value)
                    {
                        lowestPrice.Cabin = Convert.ToString(oracleDataReader["CABIN"]);
                    }

                }

            }

            return lowestPrice;
        }

        #endregion

        #region 系统最低价是否存在
        /// <summary>
        /// 系统最低价是否存在
        /// </summary>
        /// <param name="lowestPriceId">最低价ID</param>
        /// <returns></returns>
        public bool IsExistLowestPrice(int lowestPriceId)
        {
            string strSql = " select 1 from T_TP_PRICE_COMPARE where LOWEST_PRICE_ID = " + lowestPriceId;

            object objValue = DSCRM.DBA.ExecuteScalar(strSql);

            if (objValue == null)
                return false;

            return (objValue.ToString() == "1");
        }
        #endregion
    }
}
