using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DSCRM
{
    /// <summary>
    /// 此类主要用于实现对datatable的更新
    /// </summary>
    public class DatatableUpdate
    {
       //将datatable中的数据生成可执行的sql语句
       public static String[] GenerateInsertSql(DataTable toDataTable)
        {
           if(toDataTable.Rows.Count==0)
               return null;
           string[] lsSql = new String[toDataTable.Rows.Count];
           int indexRow = 0;
            foreach (DataRow dr in toDataTable.Rows)
            {
                
                lsSql[indexRow] = "INSERT INTO "+toDataTable.TableName.Trim() +" ( ";
                for (int indexI = 0; indexI < toDataTable.Columns.Count; indexI++)
                {
                    if (indexI != toDataTable.Columns.Count - 1)
                    {
                        lsSql[indexRow] += toDataTable.Columns[indexI].ColumnName + ",";
                    }
                    else
                    {
                        lsSql[indexRow] += toDataTable.Columns[indexI].ColumnName + ") VALUES (";
                    }
                }
                for (int indexJ = 0; indexJ < toDataTable.Columns.Count; indexJ++)
                {
                    if (indexJ != toDataTable.Columns.Count - 1)
                    {
                        if (dr[indexJ] is DateTime)
                        {
                            lsSql[indexRow] += " to_date('" + dr[indexJ].ToString() + "','yyyy-mm-dd hh24:mi:ss')" + ",";
                        }
                        else
                        {
                            lsSql[indexRow] += "'" + dr[indexJ].ToString() + "'" + ",";
                        }
                    }
                    else
                    {
                        if (dr[indexJ]  is DateTime)
                        {
                            lsSql[indexRow] += " to_date('" + dr[indexJ].ToString() + "','yyyy-mm-dd hh24:mi:ss'))";
                        }
                        else
                        {
                            lsSql[indexRow] += "'" + dr[indexJ].ToString() + "'" + ")";
                        }

                        
                    }
                } 
                indexRow++;
            }
            return lsSql;
        }

        //将datatable中的数据生成可执行的sql语句
       public static String[] GenerateUpdateSql(DataTable toDataTable)
        {
           if(toDataTable.Rows.Count ==0)
               return null;
            string[] lsSql = new String[toDataTable.Rows.Count];
           int indexRow = 0;
            foreach (DataRow dr in toDataTable.Rows)
            {
                lsSql[indexRow] += " UPDATE " + toDataTable.TableName.Trim() + " SET ";
                Boolean lbFieldisNull = false;
                for (int indexI = 0; indexI < toDataTable.Columns.Count; indexI++)
                {
                    if (lbFieldisNull == false)
                    {
                        if (dr[indexI].ToString() != "")
                        {
                            if (dr[indexI] is DateTime)
                            {
                                lsSql[indexRow] += toDataTable.Columns[indexI].ColumnName + " = to_date('" + dr[indexI].ToString().Trim() + "','yyyy-mm-dd hh24:mi:ss')";
                                lbFieldisNull = true;
                            }
                            else
                            {
                                lsSql[indexRow] += toDataTable.Columns[indexI].ColumnName + " = '" + dr[indexI].ToString().Trim() + "'";
                                lbFieldisNull = true;
                            }
                        }
                    }
                    else
                    {
                        if (dr[indexI].ToString() != "")
                        {
                            if (dr[indexI] is DateTime)
                            {
                                lsSql[indexRow] += "," + toDataTable.Columns[indexI].ColumnName + " = to_date('" + dr[indexI].ToString().Trim() + "','yyyy-mm-dd hh24:mi:ss')";
                            }
                            else
                            {
                                lsSql[indexRow] += "," + toDataTable.Columns[indexI].ColumnName + " = '" + dr[indexI].ToString().Trim() + "'";
                            }
                        }

                    }
                }
                lsSql[indexRow] += " WHERE " + toDataTable.Columns[0].ColumnName.Trim() + " = '" + dr[0].ToString().Trim() +"'";
                indexRow++;
            }
            return lsSql;
        }
        
       /// <summary>
       /// 插入
       /// </summary>
       /// <param name="toDataTable"></param>
       /// <returns></returns>
       public static Boolean DataTableInsert(DataTable toDataTable)
        {
           String[] lsSQL = GenerateInsertSql(toDataTable);
           if(lsSQL==null )
               return false;
            try
            {
                return DBA.ExeuseSQL(lsSQL);
                
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        //更新
        public static Boolean DataTableUpdate(DataTable toDataTable)
        {
             String[] lsSQL = GenerateUpdateSql(toDataTable);
           if(lsSQL==null )
               return false;
            try
            {
                return DBA.ExeuseSQL(lsSQL);
                
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    
        //#region 更新酒店状态
        ///// <summary>
        ///// 更新酒店状态
        ///// </summary>
        ///// <param name="tDtHBaseInfo">酒店状态更改表_插入</param>
        ///// <param name="toDataTable">酒店状态更改表_更新</param>
        ///// <returns></returns>
        //public static Boolean UpdateHotelState(DataTable tDtHBaseInfo, DataTable toDataTable)
        //{
        //    //数据检测
        //    if (tDtHBaseInfo == null || tDtHBaseInfo.Rows.Count == 0)
        //        return false;
        //    if (toDataTable == null || toDataTable.Rows.Count == 0)
        //        return false;
        //    String[] lsSQLs = new String[2];
        //    lsSQLs[0] = GenerateUpdateSql(tDtHBaseInfo);
        //    lsSQLs[1] = GenerateInsertSql(toDataTable);

        //    return sysConfig.ExeuseSQL("", lsSQLs, "CRMDB");

        //} 
        //#endregion

        public static Boolean DatasetOperator(DataTable tDtDelete, DataTable tDtInsert, DataTable tDtUpdate)
        {
            return false;
        }
        public static DataTable DataTableSelect(String sql)
        {
            try
            {
                DataSet ds = new DataSet();
                String lsError = "";
                if (DBA.SelectSQL("", sql,ref ds,ref lsError) > 0)
                    return ds.Tables[0];
                else
                    return null;
                    
            }
            catch(Exception e)
            {
                return null;
            }
        }
    }
}
