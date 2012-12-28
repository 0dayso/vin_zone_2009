using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Oracle.DataAccess.Client; // ODP.NET Oracle managed provider

namespace DSCRM
{
    public  class DBPool
    {
        private const int mnMaxActive = 30;
        private static  OracleConnection[] moOraConns;
        //private static Boolean 
        private DBPool()
        {
            //moOraConns = new OracleConnection[mnMaxActive];
        }
        public static OracleConnection GetConnection(String tnConnnectID)
        {
            if (moOraConns == null)
            {
                moOraConns = new OracleConnection[mnMaxActive];
                foreach (OracleConnection conn in moOraConns)
                {
 
                }
            }
            for (int indexI = 0; indexI < mnMaxActive; indexI++)
            {
                //moOraConns[indexI].State = ConnectionState.
            }
            return null;
        }
    }
}
