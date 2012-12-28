using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace CRM.Buzlogic.Common
{
    /// <summary>
    /// 主要封装了和数据库有关的操作
    /// </summary>
    public class CommonDBFunction
    {
        public static Boolean  DBDelete(String tsSQL)
        {

            try
            {
                String lsError = "";
                DataSet ds = new DataSet();
                if (sysConfig.SelectSQL("", tsSQL, "CRMDB", ref ds, ref lsError) > 0)
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
            CRM.Buzlogic.OLEDB tes = new OLEDB("CRMDB");
            string lsSQL = "DECLARE	@NewIdentityValue int;  exec GENERATEIDENTITY  @PropertyName = N'" + tsName + "',@NewIdentityValue = @NewIdentityValue  output";
            try
            {
                return tes.ExecSp("GENERATEIDENTITY", tsName);
                
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public static Boolean DBCheckBoxCtr(String tsDeleteSQL,DataTable tDtCheckBoxCtr)
        {
            tsDeleteSQL += " " + DatatableUpdate.GenerateInsertSql(tDtCheckBoxCtr);
            try
            {
                String lsError = "";
                DataSet ds = new DataSet();
                if (sysConfig.SelectSQL("", tsDeleteSQL, "CRMDB", ref ds, ref lsError) > 0)
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
    }
}
