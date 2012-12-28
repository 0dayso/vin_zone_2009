using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
//using Oracle.DataAccess.Client; // ODP.NET Oracle managed provider
using System.Data.OracleClient;

namespace MyFramework.DAL
{
    public class DBPool
    {
        private const int mnMaxActive = 2;//连接池容量
        private static OracleConnection[] moOraConns;
        private static Boolean[] olindex;//连接池状态
        //添加LOG
        public static void LogAdd(String tsName,String tsMessage)
        {

        }
        private DBPool()
        {
            //moOraConns = new OracleConnection[mnMaxActive];
        } 
        /// <summary>
        /// 获得一个连接和连接在连接池中的序号
        /// </summary>
        /// <param name="tsConnnectID"></param>
        /// <param name="?"></param>
        /// <returns></returns>
        public static OracleConnection GetConnection(String tsConnnectID, ref int tnOraConnIndex)
        {
            if (moOraConns == null)
            {
                moOraConns = new OracleConnection[mnMaxActive];
                olindex = new Boolean[mnMaxActive];
                for (int indexI = 0; indexI < mnMaxActive; indexI++)
                {
                    //moOraConns[indexI] = new OracleConnection();
                    //moOraConns[indexI].ConnectionString = GetConnectionString(tsConnnectID);
                    //moOraConns[indexI].Open();
                    olindex[indexI] = true;
                }
            }
            int lnTryIndex = 0;//重试次数
           try 
	       {	        
		     while (lnTryIndex < 4)
                {
                for (int indexI = 0; indexI < mnMaxActive; indexI++)
                {
                    if (moOraConns[indexI] != null)
                    {
                        if (moOraConns[indexI].State == ConnectionState.Open && olindex[indexI] == false)
                        {
                            continue;
                        }
                        else if (moOraConns[indexI].State == ConnectionState.Open)
                        {
                            OracleConnection loConn = new OracleConnection();
                            loConn.ConnectionString = GetConnectionString(tsConnnectID);
                            loConn.Open();
                            olindex[indexI] = false;
                            tnOraConnIndex = indexI;
                            moOraConns[indexI].Close();
                            moOraConns[indexI].Dispose();
                            moOraConns[indexI] = null;
                            moOraConns[indexI] = loConn;

                            return moOraConns[indexI];
                        }
                        else if (moOraConns[indexI].State == ConnectionState.Closed)
                        {
                            OracleConnection loConn = new OracleConnection();
                            loConn.ConnectionString = GetConnectionString(tsConnnectID);
                            loConn.Open();
                            olindex[indexI] = false;
                            tnOraConnIndex = indexI;
                            moOraConns[indexI].Dispose();
                            moOraConns[indexI] = null;
                            moOraConns[indexI] = loConn;

                            return moOraConns[indexI];
                        }
                        else
                        {

                            OracleConnection loConn = new OracleConnection();
                            loConn.ConnectionString = GetConnectionString(tsConnnectID);
                            loConn.Open();
                            olindex[indexI] = false;
                            tnOraConnIndex = indexI;
                            moOraConns[indexI] = null;
                            moOraConns[indexI] = loConn;

                            LogAdd("resetOLEDB", "pool" + indexI.ToString() + "reset成功!");
                            return moOraConns[indexI];
                        }
                    }
                    else
                    {
                        OracleConnection loConn = new OracleConnection();
                        loConn.ConnectionString = GetConnectionString(tsConnnectID);
                        loConn.Open();
                        olindex[indexI] = false;
                        tnOraConnIndex = indexI;
                        moOraConns[indexI] = null;
                        moOraConns[indexI] = loConn;

                        LogAdd("resetOLEDB", "pool" + indexI.ToString() + "reset成功!");
                        return moOraConns[indexI];
                    }
                   
                }
                   
                
                //等待0.5秒
                System.Threading.Thread.Sleep(500);
                LogAdd("SearchDB","Try Num:"+lnTryIndex.ToString());
                lnTryIndex++;
            }
	}
	catch (Exception ex)
	{
        LogAdd("SearchDB","Err:"+ex.Message);
        return null;
	}
            return null;
        }
        /// <summary>
        /// 释放连接
        /// </summary>
        /// <param name="tnOraConnIndex"></param>
        public static void FreeConnect(int tnOraConnIndex)
        {
            olindex[tnOraConnIndex] = true;
            OracleConnection conn = moOraConns[tnOraConnIndex];
            if (conn != null && conn.State == ConnectionState.Open)
                conn.Close();
        }
        /// <summary>
        /// 获得连接字符串
        /// </summary>
        /// <param name="tsConnectID"></param>
        /// <returns></returns>
        private static String GetConnectionString(String tsConnectID)
        {
            String lsConnectionString = System.Configuration.ConfigurationSettings.AppSettings[tsConnectID];
            //string lsConnectionString = 
            return lsConnectionString;
        }
        

    }
}
