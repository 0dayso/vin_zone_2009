using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OracleClient;
using System.Data;
namespace MyFramework.DAL
{
    class DBAdmin
    {
        private DBAdmin()
        { }

        public static OracleConnection GetConnection(String tsConnnectID)
        {
           try{

                OracleConnection loConn = new OracleConnection();
                loConn.ConnectionString = GetConnectionString(tsConnnectID);
                loConn.Open();
                return loConn;

                          
            }
            catch (Exception ex)
            {
                throw new Exception("��ȡ����ʱ����" + ex.Message);
            }
           
        }
       
        /// <summary>
        /// �ͷ�����
        /// </summary>
        /// <param name="tnOraConnIndex"></param>
        public static void FreeConnect(OracleConnection tnOraConn)
        {
            if (tnOraConn != null && tnOraConn.State == ConnectionState.Open)
            {
                tnOraConn.Close();
                tnOraConn.Dispose();
                tnOraConn = null; 
            }
        }
        /// <summary>
        /// ��������ַ���
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
