using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
 
using System.Data.OracleClient;
using System.Text.RegularExpressions;

namespace MyFramework.BusinessLogic.Common
{
   #region "DBUtil"
   /// <summary>数据库访问时候的一点帮助函数</summary>
   public class DBUtil
   {
       public enum StringType { Echar = 1, Enchar = 2, Evarchar = 3, Envarchar = 4, Eclob = 5, Enclob = 6 };
      
      #region SafeGetInt32
      /// <summary>从结果集中获取索引为 i 的字段的整形值，缺省为 defVal</summary>
      /// <param name="rs">结果集</param>
      /// <param name="i">字段索引</param>
      /// <param name="defVal">缺省值</param>
      /// <returns></returns>
      public static int SafeGetInt32(IDataReader rs, int i, int defVal)
      {
         if( rs.IsDBNull(i) )
            return defVal; 
          return Convert.ToInt32(rs[i]);
      }

      /// <summary>从结果集中获取索引为 i 的字段的整形值，缺省为 0</summary>
      /// <param name="rs">结果集</param>
      /// <param name="i">字段索引</param>
      public static int SafeGetInt32(IDataReader rs, int i)
      {
         return SafeGetInt32(rs, i, 0);
      }

      /// <summary>从结果集中获取名称为 field 的字段的整形值，缺省为 defVal</summary>
      /// <param name="rs">结果集</param>
      /// <param name="field">字段名称</param>
      /// <param name="defVal">缺省值</param>
      public static int SafeGetInt32(IDataReader rs, string field, int defVal)
      {
         return SafeGetInt32(rs, rs.GetOrdinal(field), defVal);
      }

      /// <summary>从结果集中获取名称为 field 的字段的整形值，缺省为 0</summary>
      /// <param name="rs">结果集</param>
      /// <param name="field">字段名称</param>
      public static int SafeGetInt32(IDataReader rs, string field)
      {
         return SafeGetInt32(rs, rs.GetOrdinal(field), 0);
      }

      #endregion

      #region SafeGetString
      /// <summary>从结果集中获取索引为 i 的字段的字符串，缺省为 defVal</summary>
      /// <param name="rs">结果集</param>
      /// <param name="i">字段索引</param>
      /// <param name="defVal">缺省值</param>
      public static string SafeGetString(IDataReader rs, int i, string defVal)
      {
         if( rs.IsDBNull(i) )
            return defVal;
         return Convert.ToString(rs[i]);
      }

      /// <summary>从结果集中获取索引为 i 的字段的字符串，缺省为 string.Empty</summary>
      /// <param name="rs">结果集</param>
      /// <param name="i">字段索引</param>
      public static string SafeGetString(IDataReader rs, int i)
      {
         return SafeGetString(rs, i, string.Empty);
      }

      /// <summary>从结果集中获取名称为 field 的字段的字符串，缺省为 defVal</summary>
      /// <param name="rs">结果集</param>
      /// <param name="field">字段名称</param>
      /// <param name="defVal">缺省值</param>
      public static string SafeGetString(IDataReader rs, string field, string defVal)
      {
         return SafeGetString(rs, rs.GetOrdinal(field), defVal);
      }

      /// <summary>从结果集中获取名称为 field 的字段的字符串，缺省为 string.Empty</summary>
      /// <param name="rs">结果集</param>
      /// <param name="field">字段名称</param>
      public static string SafeGetString(IDataReader rs, string field)
      {
         return SafeGetString(rs, rs.GetOrdinal(field), string.Empty);
      }
      #endregion

      #region "SafeGetDateTime"
      /// <summary>从结果集中获取索引为 i 的字段的日期值，缺省为 defVal</summary>
      /// <param name="rs">结果集</param>
      /// <param name="i">字段索引</param>
      /// <param name="defVal">缺省值</param>
      /// <returns></returns>
      public static System.DateTime SafeGetDateTime(IDataReader rs, int i, System.DateTime defVal)
      {
         try
         {
            if( rs.IsDBNull(i) )
               return defVal;
            return Convert.ToDateTime(rs[i]);
         }
         catch(InvalidCastException) { return defVal; }
         catch(FormatException) { return defVal; }
      }

      /// <summary>从结果集中获取索引为 i 的字段的日期值，缺省为 DateTime(0)</summary>
      /// <param name="rs">结果集</param>
      /// <param name="i">字段索引</param>
      public static System.DateTime SafeGetDateTime(IDataReader rs, int i)
      {
         return SafeGetDateTime(rs, i, new System.DateTime(0));
      }

      /// <summary>从结果集中获取名称为 field 的字段的日期值，缺省为 defVal</summary>
      /// <param name="rs">结果集</param>
      /// <param name="field">字段名称</param>
      /// <param name="defVal">缺省值</param>
      public static System.DateTime SafeGetDateTime(IDataReader rs, string field, System.DateTime defVal)
      {
         return SafeGetDateTime(rs, rs.GetOrdinal(field), defVal);
      }

      /// <summary>从结果集中获取名称为 field 的字段的日期值，缺省为 defVal</summary>
      /// <param name="rs">结果集</param>
      /// <param name="field">字段名称</param>
      public static System.DateTime SafeGetDateTime(IDataReader rs, string field)
      {
         return SafeGetDateTime(rs, rs.GetOrdinal(field), new System.DateTime(0));
      }
      #endregion

      #region "SafeGetDouble"
      /// <summary>从结果集中获取名称为 field 的字段的双精度浮点值，缺省为 defVal</summary>
      /// <param name="rs">结果集</param>
      /// <param name="field">字段名称</param>
      /// <param name="defVal">缺省值</param>
      /// <returns></returns>
      public static double SafeGetDouble(IDataReader rs, int field, double defVal)
      {
         try { if( rs.IsDBNull(field) ) return defVal; else return Convert.ToDouble(rs.GetValue(field)); }
         catch(System.InvalidCastException) { return defVal; }
         catch(System.FormatException) { return defVal; }
      }

      /// <summary>从结果集中获取名称为 field 的字段的双精度浮点值，缺省为 0</summary>
      /// <param name="rs">结果集</param>
      /// <param name="field">字段名称</param>
      public static double SafeGetDouble(IDataReader rs, string field)
      {
         return SafeGetDouble(rs, rs.GetOrdinal(field), 0);
      }

      /// <summary>从结果集中获取索引为 i 的字段的双精度浮点值，缺省为 0</summary>
      /// <param name="rs">结果集</param>
      /// <param name="i">字段索引</param>
      public static double SafeGetDouble(IDataReader rs, int i)
      {
         return SafeGetDouble(rs, i, 0);
      }

      /// <summary>从结果集中获取索引为 i 的字段的双精度浮点值，缺省为 defVal</summary>
      /// <param name="rs">结果集</param>
      /// <param name="i">字段索引</param>
      /// <param name="defVal">缺省值</param>
      public static double SafeGetDouble(IDataReader rs, string i, double defVal)
      {
         return SafeGetDouble(rs, rs.GetOrdinal(i), defVal);
      }
      #endregion



      #region "AddParameter"
       //add by yanght 2007/09/25 begin
       //添加对可空值的控制
       public static System.Data.IDataParameter AddParameter(OracleCommand sqlcmd, string paramName, object toValue, OracleType dbType)
       {
           if (toValue == null)
           {
               toValue = DBNull.Value;
           }
           System.Data.OracleClient.OracleParameter para = sqlcmd.CreateParameter();
           para.ParameterName = paramName;
           para.OracleType = dbType;
           para.Direction = ParameterDirection.Input;
           para.Value = toValue;
           sqlcmd.Parameters.Add(para);
           return para;
       }
       //int
       public static System.Data.IDbDataParameter AddParameter(OracleCommand sqlcmd, string paramName, int? val)
       {
            if (val == null)
                return AddParameter(sqlcmd, paramName, OracleType.Number);
            else
                return AddParameter(sqlcmd, paramName, val, OracleType.Number, ParameterDirection.Input);
       }
       //double
       public static System.Data.IDbDataParameter AddParameter(OracleCommand sqlcmd, string paramName, double? val)
       {
           if (val == null)
               return AddParameter(sqlcmd, paramName, OracleType.Double);
           else
                return AddParameter(sqlcmd, paramName, val, OracleType.Double, ParameterDirection.Input);
       }
       //datetime
       public static System.Data.IDbDataParameter AddParameter(OracleCommand sqlcmd, string paramName, System.DateTime? val)
       {
           if (val == null)
               return AddParameter(sqlcmd, paramName, OracleType.DateTime);
           else
                return AddParameter(sqlcmd, paramName, val, OracleType.DateTime, ParameterDirection.Input);
       }
       //add end



      /// <summary>给一个 IDbCommand 对象添加参数</summary>
      /// <param name="sqlcmd">IDbCommand 对象</param>
      /// <param name="paramName">参数名称</param>
      /// <param name="val">整型(int)参数值</param>
       public static System.Data.IDbDataParameter AddParameter(OracleCommand sqlcmd, string paramName, OracleType val)
      {
          return AddParameter(sqlcmd, paramName, DBNull.Value, val, ParameterDirection.Input);
      }
      /// <summary>给一个 IDbCommand 对象添加参数</summary>
      /// <param name="sqlcmd">IDbCommand 对象</param>
      /// <param name="paramName">参数名称</param>
      /// <param name="val">整型(int)参数值</param>
      public static System.Data.IDbDataParameter AddParameter(OracleCommand sqlcmd, string paramName, int val)
      {

          return AddParameter(sqlcmd, paramName, val, OracleType.Number, ParameterDirection.Input);
      }
      /// <summary>给一个 IDbCommand 对象添加参数</summary>
      /// <param name="sqlcmd">IDbCommand 对象</param>
      /// <param name="paramName">参数名称</param>
      /// <param name="val">Int64参数值</param>
      public static System.Data.IDbDataParameter AddParameter(OracleCommand sqlcmd, string paramName, Int64 val)
      {

          return AddParameter(sqlcmd, paramName, val, OracleType.Double, ParameterDirection.Input);
      }

      /// <summary>给一个 IDbCommand 对象添加参数</summary>
      /// <param name="sqlcmd">IDbCommand 对象</param>
      /// <param name="paramName">参数名称</param>
      /// <param name="val">双精度浮点型(double)参数值</param>
       public static System.Data.IDbDataParameter AddParameter(OracleCommand sqlcmd, string paramName, double val)
      {

          return AddParameter(sqlcmd, paramName, val, OracleType.Number, ParameterDirection.Input);
      }

      /// <summary>给一个 IDbCommand 对象添加参数</summary>
      /// <param name="sqlcmd">IDbCommand 对象</param>
      /// <param name="paramName">参数名称</param>
      /// <param name="val">日期型(DateTime)参数值</param>
       public static System.Data.IDbDataParameter AddParameter(OracleCommand sqlcmd, string paramName, System.DateTime val)
      {
        
           return AddParameter(sqlcmd, paramName, val, OracleType.DateTime, ParameterDirection.Input);
      }

      /// <summary>给一个 IDbCommand 对象添加参数</summary>
      /// <param name="sqlcmd">IDbCommand 对象</param>
      /// <param name="paramName">参数名称</param>
      /// <param name="val">字符串型(string)参数值</param>
       public static System.Data.IDbDataParameter AddParameter(OracleCommand sqlcmd, string paramName, string val)
      {
          if (val == null)
                return AddParameter(sqlcmd, paramName, OracleType.VarChar);
          return AddParameter(sqlcmd, paramName, val, OracleType.VarChar, ParameterDirection.Input);
      }

    
      /// <summary>给一个 IDbCommand 对象添加参数</summary>
      /// <param name="sqlcmd">IDbCommand 对象</param>
      /// <param name="paramName">参数名称</param>
      /// <param name="val">字符串型(string)参数值</param>
       public static System.Data.IDbDataParameter AddParameter(OracleCommand sqlcmd, string paramName, string val, StringType type)
      {
          switch (type)
          {
              case StringType.Echar:
                  return AddParameter(sqlcmd, paramName, val, OracleType.Char, ParameterDirection.Input);
               case StringType.Enchar:
                  return AddParameter(sqlcmd, paramName, val, OracleType.NChar, ParameterDirection.Input);
                 
              case StringType.Evarchar:
                  return AddParameter(sqlcmd, paramName, val, OracleType.VarChar, ParameterDirection.Input);
                  
              case StringType.Envarchar:
                  return AddParameter(sqlcmd, paramName, val, OracleType.NVarChar, ParameterDirection.Input);
                  
              case StringType.Eclob:
                  return AddParameter(sqlcmd, paramName, val, OracleType.NClob, ParameterDirection.Input);
                  
              case StringType.Enclob:
                  return AddParameter(sqlcmd, paramName, val, OracleType.NClob, ParameterDirection.Input);
              default:
                  return AddParameter(sqlcmd, paramName, val, OracleType.VarChar, ParameterDirection.Input);
                  
          }
      }
    
      /// <summary>给一个 IDbCommand 对象添加参数</summary>
      /// <param name="sqlcmd">IDbCommand 对象</param>
      /// <param name="paramName">参数名称</param>
      /// <param name="val">参数值</param>
      /// <param name="dbtype">参数类型</param>
      /// <param name="direction">参数方向</param>
       public static System.Data.IDbDataParameter AddParameter(OracleCommand sqlcmd, string paramName, object val, System.Data.OracleClient.OracleType dbtype, System.Data.ParameterDirection direction)
      {
        
         System.Data.OracleClient.OracleParameter para = sqlcmd.CreateParameter();
         para.ParameterName = paramName;
         para.OracleType = dbtype;
         para.Direction = direction;
         para.Value = val;
         sqlcmd.Parameters.Add(para);
         return para;
      }

       /// <summary>
       /// 把Oracle参数组赋给 OraCommand
       /// </summary>
       /// <param name="cmd"></param>
       /// <param name="cmdText"></param>
       /// <param name="cmdParms"></param>
       public static void PrepareCommand(OracleCommand cmd, string cmdText, OracleParameter[] cmdParms)
       {
           cmd.CommandType = CommandType.Text;//cmdType;
           cmd.CommandText = cmdText;
           cmd.Parameters.Clear();
           if (cmdParms != null)
           {
               foreach (OracleParameter parm in cmdParms)
               {
                   if (parm.Value == null)
                       parm.Value = DBNull.Value;
                   cmd.Parameters.Add(parm);

               }
           }
       }
      #endregion

      #region "SafeGetScalarInt32/SafeGetScalarString"
      /// <summary>执行 IDbCommand 的 ExcecuteScalar 并将结果转换为 int 型, 缺省值为 0</summary>
      /// <param name="sqlcmd">IDbCommand</param>
      /// <returns></returns>
      public static int SafeGetScalarInt32(IDbCommand sqlcmd) { return SafeGetScalarInt32(sqlcmd, 0); }

      /// <summary>执行 IDbCommand 的 ExcecuteScalar 并将结果转换为 int 型, 缺省值为 defvalue</summary>
      /// <param name="sqlcmd">IDbCommand</param>
      /// <param name="defvalue">缺省值</param>
      /// <returns></returns>
      public static int SafeGetScalarInt32(IDbCommand sqlcmd, int defvalue)
      {
         object o = sqlcmd.ExecuteScalar();
         if( o == null || o == Convert.DBNull )
            return defvalue;
         return Convert.ToInt32(o);
      }

      /// <summary>执行 IDbCommand 的 ExcecuteScalar 并将结果转换为 string 型, 缺省值为 string.Empty</summary>
      /// <param name="sqlcmd">IDbCommand</param>
      /// <returns></returns>
      public static string SafeGetScalarString(IDbCommand sqlcmd)
      {
         return SafeGetScalarString(sqlcmd, string.Empty);
      }

      /// <summary>执行 IDbCommand 的 ExcecuteScalar 并将结果转换为 string 型, 缺省值为 defvalue</summary>
      /// <param name="sqlcmd">IDbCommand</param>
      /// <param name="defvalue">缺省值</param>
      public static string SafeGetScalarString(IDbCommand sqlcmd, string defvalue)
      {
         try
         {
            object o = sqlcmd.ExecuteScalar();
            if( o == null || o == Convert.DBNull )
               return defvalue;
            return Convert.ToString(o);
         }
         catch(InvalidCastException) { }
         catch(FormatException) { }
         return defvalue;
      }
      #endregion

      #region "FillDataTable - 填充 DataTable"
      /// <summary>使用指定的数据读取器填充数据表 (V 0.1)</summary>
      /// <param name="rs">数据读取器</param>
      /// <param name="dt">数据表</param>
      public static void FillDataTable(IDataReader rs, System.Data.DataTable dt)
      {
         FillDataTable(rs, dt, 0, -1);
      }

      /// <summary>使用指定的数据读取器填充数据表 (V 0.1)</summary>
      /// <param name="rs">数据读取器</param>
      /// <param name="dt">数据表</param>
      /// <param name="start">开始位置, = 0 表示从第一条记录开始填充</param>
      /// <param name="count">填充数量, = -1 表示填充直到所有数据读取出来</param>
      public static void FillDataTable(IDataReader rs, System.Data.DataTable dt, int start, int count)
      {
         if( rs == null )
            throw new ArgumentNullException("rs", "需要一个数据读取器");
         if( dt == null )
            throw new ArgumentNullException("dt", "需要给出要填充的数据表对象");

         // 1. Get Schema Table and build dt
         
         int i, fieldCount = rs.FieldCount;
         if( fieldCount <= 0 )
            throw new ApplicationException("数据读取器中没有任何字段");
         
         for( i = 0 ; i < fieldCount ; i++ )
            dt.Columns.Add(new System.Data.DataColumn(rs.GetName(i), rs.GetFieldType(i)));
         
         // 2. skip to start
         if( start > 0 )
         {
            while( rs.Read() && (start > 0) )
               start--;
            if( start == 0 )
               return;
         }

         // 3. fill the table
         if( count < 0 ) count = int.MaxValue;
         dt.BeginLoadData();
         for( i = 0 ; i < count ; i++ )
         {
            if( rs.Read() == false )
               break;
            DataRow row = dt.NewRow();
            for( int j = 0 ; j < fieldCount ; j++ )
               row[j] = rs.GetValue(j);
            dt.Rows.Add(row);
         }
         dt.EndLoadData();
      }

      #endregion

   }
   #endregion

   #region "StrUtil"
   /// <summary>VB 字符串处理的一些简单帮助函数</summary>
    public class StrUtil
    {
        #region "Left/Right/Mid"
        private StrUtil() { }
        /// <summary>
        /// 获取一个字符串的左边指定长度的部分，如果字符串长度不够，则返回全部
        /// </summary>
        /// <param name="str"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string Left(string str, int len)
        {
            if (len < 0)
                throw new ArgumentException("Argument <= 0", "Length");
            if (len == 0 || str == null)
                return string.Empty;
            if (len >= str.Length)
                return str;
            return str.Substring(0, len);
        }

        /// <summary>
        /// 获取一个字符串的右边指定长度的部分，如果字符串长度不够，则返回全部
        /// </summary>
        /// <param name="str"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string Right(string str, int len)
        {
            int local0;

            if (len < 0)
                throw new ArgumentException("Argument <= 0", "Length");
            if (len == 0 || str == null)
                return string.Empty;
            local0 = str.Length;
            if (len >= local0)
                return str;
            return str.Substring(local0 - len, len);
        }

        public static string Mid(string str, int start)
        {
            if (str == null)
                return null;
            return Mid(str, start, str.Length);
        }

        /// <summary>
        /// Visual Basic 的 Mid 实现。注意： start 是从 1 开始的。
        /// </summary>
        /// <param name="str"></param>
        /// <param name="start"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string Mid(string str, int start, int len)
        {
            int local0;

            if (start <= 0)
                throw new ArgumentException("Argument <= 0", "start");
            if (len < 0)
                throw new ArgumentException("Argument < 0", "len");
            if (len == 0 || str == null)
                return string.Empty;
            local0 = str.Length;
            if (start > local0)
                return string.Empty;
            if (start + len > local0)
                return str.Substring(start - 1);
            return str.Substring(start - 1, len);
        }
        #endregion

        #region "SqlString/SafeToString/SafeJString"
        /// <summary>将字符串中的 单引号 替换为两个单引号，以提供给 SQL Server 做为查询用</summary>
        /// <param name="str">要处理的字符串</param>
        /// <returns></returns>
        public static string SqlString(string str)
        {
            if (str == null || str == string.Empty)
                return string.Empty;
            if (str.IndexOf('\'') < 0)
                return str;
            return str.Replace("'", "''");
        }

        /// <summary>安全的将一个对象转换为字符串</summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static string SafeToString(object v)
        {
            if (v == null || v == Convert.DBNull) return string.Empty;
            return Convert.ToString(v).Trim();
        }

        public static string SafeCString(object v)
        {
            if (v == null || v == Convert.DBNull || v.ToString().Equals(""))
                return string.Empty;
            else
                return Convert.ToString(v).Trim();
        }

        /// <summary>
        /// 为前端使用的 JavaScript 构造字符串, 这些字符串中 \ \r\n ' 等需要替换为相应的字符.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string SafeJString(string str)
        {
            if (str == null || str == string.Empty) return string.Empty;
            if (str.IndexOf('\\') >= 0)
                str = str.Replace("\\", "\\\\");
            if (str.IndexOf('\'') >= 0)
                str = str.Replace("'", "\\'");
            if (str.IndexOf('\r') >= 0)
                str = str.Replace("\r", "\\r");
            if (str.IndexOf('\n') >= 0)
                str = str.Replace("\n", "\\n");
            return str;
        }
        #endregion

        #region toSQLString

        public static string toSQLString(object str)
        {
            if (str == null)
                return "NULL";
            else
                return str.ToString();
        }
        #endregion

        #region IIf
        /// <summary>
        /// 根据条件返回 真 部分的值或 假 部分的值，结果为一个字符串。
        /// </summary>
        /// <param name="expr"></param>
        /// <param name="truepart"></param>
        /// <param name="falsepart"></param>
        /// <returns></returns>
        public static string IIf(object expr, object truepart, object falsepart)
        {
            if (expr == null) return SafeToString(falsepart);
            if (SafeConvert.SafeBoolean(expr) == false) return SafeToString(falsepart);
            return SafeToString(truepart);
        }
        #endregion

        #region "Repeat"
        /// <summary>
        /// 将一个字符串重复指定次数，返回结果
        /// </summary>
        /// <param name="v">模板字符串</param>
        /// <param name="n">重复次数</param>
        /// <returns></returns>
        public static string Repeat(string v, int n)
        {
            if (v == null || v == string.Empty || n <= 0) return string.Empty;
            if (n == 1) return v;
            if (n == 2) return v + v;
            if (n == 3) return string.Concat(v, v, v);

            System.Text.StringBuilder strbuf = new System.Text.StringBuilder(v.Length * n + 2);
            for (int i = 0; i < n; i++)
                strbuf.Append(v);
            return strbuf.ToString();
        }
        #endregion

        #region "IsGuid"
        /// <summary>
        /// 判断给定的一个字符串是否是一个有效 Guid 格式
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsGuid(string str)
        {
            if (str == null || str == string.Empty) return false;
            try
            {
                System.Guid t = new System.Guid(str);
                return true;
            }
            catch (System.FormatException) { return false; }
            catch (System.InvalidCastException) { return false; }
        }
        #endregion

        #region "XMLEncode - 字符串替换为脚本输出"
        public static string XMLEncode(object var)
        {
            string strTmp = "";

            if ((var == null) || (var.ToString() == string.Empty))
                return "";
            if (var.GetType() == typeof(Boolean))
            {
                if (Convert.ToBoolean(var))
                    strTmp = "1";
                else
                    strTmp = "0";
            }
            else
            {
                strTmp = var.ToString();
                strTmp = strTmp.Replace("&", "&amp;");
                strTmp = strTmp.Replace("<", "&lt;");
                strTmp = strTmp.Replace(">", "&gt;");
                strTmp = strTmp.Replace("\"", "&quot;");
                strTmp = strTmp.Replace("'", "&apos;");
                strTmp = strTmp.Replace("{", "(");
                strTmp = strTmp.Replace("\n", "");
                strTmp = strTmp.Replace("\r", "");
                strTmp = strTmp.Replace("}", ")");
            }
            return strTmp;

        }
        #endregion
    }

   #endregion

   #region "ParamUtil"
   /// <summary>
   /// 从 Http Request 中获取参数的帮助类
   /// </summary>
   public class ParamUtil
   {
      public static int SafeGetInt32(string key, int def)
      {
         string val = System.Web.HttpContext.Current.Request.Params[key];
         try { if( val == null || val == string.Empty ) return def; else return Convert.ToInt32(val); }
         catch(InvalidCastException) { return def; }
         catch(FormatException) { return def; }
      }
      public static int SafeGetInt32(string key)
      {
         return SafeGetInt32(key, 0);
      }
      public static string SafeGetString(string key, string def)
      {
         System.Web.HttpContext ctx = System.Web.HttpContext.Current;
         string val = ctx.Request.QueryString[key];
         if( val == null )
         {
            val = ctx.Request.Form[key];
            if( val == null )
               return def;
         }
         return val;
      }
      public static string SafeGetString(string key)
      {
         return SafeGetString(key, string.Empty);
      }
      public static string SafeGetCookie(string key)
      {
         System.Web.HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies[key];
         if( cookie == null )
            return string.Empty;
         return cookie.Value;
      }
      public static string SafeGetCookie(string key1, string key2)
      {
         System.Web.HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies[key1];
         if( cookie == null )
            return string.Empty;
         return cookie[key2];
      }
      public static void SafeSetCookie(string key, string val)
      {
         System.Web.HttpCookie cookie = new System.Web.HttpCookie(key, val);
         HttpContext.Current.Response.Cookies.Set(cookie);
      }
      public static void SafeSetCookie(string key1, string key2, string val)
      {
         HttpContext ctx = HttpContext.Current;
         System.Web.HttpCookie cookie = ctx.Request.Cookies[key1];
         if( cookie == null )
         {
            cookie = new System.Web.HttpCookie(key1);
            cookie.Values.Add(key2, val);
         }
         else
         {
            cookie.Values[key2] = val;
         }
         ctx.Response.Cookies.Set(cookie);
      }
      public static void SafeRemoveCookie(string key)
      {
         // ASP.NET 中竟然不能完全删除一个 Cookie ，只能用恶劣的方法如下
         HttpContext ctx = HttpContext.Current;
         HttpCookie cookie = ctx.Request.Cookies[key];
         if( cookie == null ) return;     // 不要多事去设置一个否则反而会多出来！
         cookie = new HttpCookie(key, null);
         cookie.Expires = System.DateTime.Now.AddYears(-10);
         ctx.Response.Cookies.Remove(key);
         ctx.Response.Cookies.Set(cookie);
      }
      public static void SafeRemoveCookie(string key1, string key2)
      {
         // 在第一个键里面删除某一个更加麻烦
         HttpContext ctx = HttpContext.Current;
         HttpCookie oldCookie = ctx.Request.Cookies[key1];
         if( oldCookie == null ) return;        // 谢天谢地，没有这个 Cookie

         if( oldCookie.HasKeys == false ) return;  // 谢天谢地，没有子键
         if( oldCookie.Values[key2] == null ) return; // 谢天谢地，没有要的键

         // 克隆一个过来？
         HttpCookie newCookie = new HttpCookie(oldCookie.Name);
         newCookie.Domain = oldCookie.Domain;
         newCookie.Expires = oldCookie.Expires;
         newCookie.Path = oldCookie.Path;
         newCookie.Secure = oldCookie.Secure;
         for( int i = 0 ; i < oldCookie.Values.Count ; i ++ )
         {
            if( oldCookie.Values.Keys[i] != key2 )
               newCookie.Values.Add(oldCookie.Values.Keys[i], oldCookie.Values[i]);
         }
         ctx.Response.Cookies.Set(newCookie);
      }
   }
   #endregion

   #region "UrlUtil"
   public class UrlUtil
   {
      // 输入格式如 http(s)://www.chinaedustar.com:888/vdir/page.aspx
      // 我们要计算出 http(s)://www.chinaedustar.com:888 出来
      public static string GetServerUrl(string url)
      {
         if( url == null || url == string.Empty )
            throw new ArgumentNullException("url", "非法的 url 参数");
         // 1. 跳过 http:// 部分
         int i = url.IndexOf("//");
         if( i < 0 ) throw new ArgumentException("非法的 url 值", "url");
         int j = url.IndexOf("/", i + 2);
         if( j < 0 ) return url;             // 认为本身就是 ServerUrl
         return url.Substring(0, j);
      }


      /// <summary>创建目录， 如果目录不存在，则创建，创建过程发生的异常还是会抛出</summary>
      /// <param name="dir">创建目录的全路径</param>
      public static void SafeCreateDirectory(string dir)
      {
         if( System.IO.Directory.Exists(dir) == false )
            System.IO.Directory.CreateDirectory(dir);
      }


      /// <summary>从路径中计算出最后一个文件夹的名字</summary>
      /// <param name="path"></param>
      /// <returns></returns>
      public static string GetFolderName(string path)
      {
         if( path == null || path == string.Empty ) throw new ArgumentNullException("path", "非法的路径参数");
         if(path.EndsWith("\\"))
            path = path.Substring(0, path.Length - 1);
         
         int i = path.LastIndexOf('\\');
         if( i < 0 ) return path;
         return path.Substring(i+1, path.Length-i-1);
      }


      /// <summary>获得文件后缀</summary>
      /// <param name="fileName"></param>
      /// <returns>文件后缀，不带 '.' 符号。如果文件没有后缀，则返回 string.Empty</returns>
      public static string GetFileExtension(string fileName)
      {
         fileName = fileName.Replace('/', '\\');
         int pos = fileName.LastIndexOf('.');
         if( pos < 0 ) return string.Empty;
         int pos2 = fileName.LastIndexOf('\\');
         if( pos < pos2 ) return string.Empty;
         return fileName.Substring(pos + 1);
      }

      /// <summary>
      /// 判断给定的字符串是否是一个有效的 Url
      /// </summary>
      /// <param name="url"></param>
      /// <returns></returns>
      public static bool IsValidUrl(string url)
      {
         try
         {
            System.Uri u = new Uri(url);
            if( false == System.Uri.CheckSchemeName(u.Scheme) ) return false;
            if( System.Uri.CheckHostName(u.Host) == System.UriHostNameType.Unknown ) return false;
            return true;
         }
         catch(System.Exception)
         {
            return false;
         }
      }

      /// <summary>
      /// 合成服务地址， hostUrl + svcPath，去掉可能的多余 /
      /// </summary>
      /// <param name="hostUrl"></param>
      /// <param name="svcPath"></param>
      /// <returns></returns>
      public static string BuildServiceUrl(string hostUrl, string svcPath)
      {
         if( hostUrl.EndsWith("/") == false )
            hostUrl += "/";
         if( svcPath.StartsWith("/") )
            svcPath = svcPath.Substring(1);

         return hostUrl + svcPath;
      }
   }
   #endregion

   #region "IntUtil"
   public class IntUtil
   {
      static public int SafeCInt(object v)
      {
         if( v == null || Convert.DBNull.Equals(v) ) return 0;
         if( v.GetType() == typeof(string) && string.Empty.Equals(v) ) return 0;
         try { return Convert.ToInt32(v); }
         catch(InvalidCastException) { }
         catch(FormatException) { }
         return 0;
      }

       static public int? SafeCIntNullable(object v)
       {
           if (v == null || Convert.DBNull.Equals(v)) return null;
           if (v.GetType() == typeof(string) && string.Empty.Equals(v)) return null;
           try { return Convert.ToInt32(v); }
           catch (InvalidCastException) { }
           catch (FormatException) { }
           return null;
       }

       static public UInt16? SafeCUInt16Nullable(object v)
       {
           if (v == null || Convert.DBNull.Equals(v)) return null;
           if (v.GetType() == typeof(string) && string.Empty.Equals(v)) return null;
           try { return Convert.ToUInt16(v); }
           catch (InvalidCastException) { }
           catch (FormatException) { }
           return null;
       }

       static public UInt32? SafeCUInt32Nullable(object v)
       {
           if (v == null || Convert.DBNull.Equals(v)) return null;
           if (v.GetType() == typeof(string) && string.Empty.Equals(v)) return null;
           try { return Convert.ToUInt32(v); }
           catch (InvalidCastException) { }
           catch (FormatException) { }
           return null;
       }

       static public UInt64? SafeCUInt64Nullable(object v)
       {
           if (v == null || Convert.DBNull.Equals(v)) return null;
           if (v.GetType() == typeof(string) && string.Empty.Equals(v)) return null;
           try { return Convert.ToUInt64(v); }
           catch (InvalidCastException) { }
           catch (FormatException) { }
           return null;
       }

      /// <summary>判定一个字符串是否是一个整数</summary>
      /// <param name="str"></param>
      /// <returns></returns>
      public static bool IsInteger(string str)
      {
         if( str == null || str == string.Empty ) return false;
         try { int i = int.Parse(str); return true; }			//whether should use Int32.Parse(..), writen by dust
         catch(InvalidCastException) { return false; }
         catch(FormatException) {return false;}
      }

      /// <summary>将一个多标识连接字符串解析成为标识数组</summary>
      /// <param name="idstr"></param>
      /// <returns></returns>
      static public int[] IdlistToArray(string idstr)
      {
         string[] idstrs = idstr.Split(',');
         int i, count = 0;
         for( i = 0 ; i < idstrs.Length ; i++ )
            if( IntUtil.IsInteger(idstrs[i]) )
               count++;

         int[] ids = new int[count];
         count = 0;
         for( i = 0 ; i < idstrs.Length ; i++ )
            if( IntUtil.IsInteger(idstrs[i]) )
               ids[count++] = IntUtil.SafeCInt(idstrs[i]);

         return ids;
      }

      /// <summary>检测从外部传递过来的 idlist 类型的参数是否合法</summary>
      /// <param name="idlist"></param>
      /// <returns></returns>
      public static bool IsValidIdlist(string idlist)
      {
         if( idlist == string.Empty ) return false;
         try
         {
            string[] tmp = idlist.Split(',');
            for( int i = 0 ; i < tmp.Length ; i++ )
            {
               int j = System.Int32.Parse(tmp[i]);
            }
            return true;
         }
         catch(FormatException)
         {
         }
         catch(InvalidCastException)
         {
         }
         return false;
      }

      /// <summary>将一组标识转换为字符串，中间以 ',' 号分隔</summary>
      /// <param name="ids"></param>
      /// <returns></returns>
      public static string GetIdlistString(int[] ids)
      {
         if( ids == null || ids.Length == 0 ) throw new ArgumentNullException("ids", "非法参数，要求非空且至少包含一个元素。");
         if( ids.Length == 0 ) return ids[0].ToString();
         string idstr = ids[0].ToString();
         for( int i = 1 ; i < ids.Length ; i++ )
            idstr += "," + ids[i].ToString();
         return idstr;
      }

   }

   #endregion

   #region "SafeConvert"
   public class SafeConvert
   {
      #region "SafeBoolean"
      public static bool SafeBoolean(object o)
      {
         if( o == null || o == Convert.DBNull ) return false;
         System.Type ot = o.GetType();
         if( ot == typeof(string) || ot == typeof(char) )
            return SafeBoolean(o.ToString());
         if( ot == typeof(int) || ot == typeof(System.Int16) )
            return SafeBoolean(Convert.ToInt32(o));
         if( ot == typeof(System.Int64) )
            return !o.Equals((System.Int64)0);
		 if (ot==typeof(System.Boolean))
			return Convert.ToBoolean(o);
         // 其它类型我们不识别, 从而返回 false
         return false;
      }

      public static bool SafeBoolean(string str)
      {
         if( str == null || str == string.Empty ) return false;
         str = str.ToLower();
         if( str == "0" || str == "f" || str == "false" || str == "off" ) return false;
         return true;
      }
      public static bool SafeBoolean(int i)
      {
         return i != 0;
      }
      #endregion

      #region "SafeCInt"
      public static int SafeCInt(object o) { return IntUtil.SafeCInt(o); }
      #endregion

      #region "SafeCString"
      public static string SafeCString(object o) { return StrUtil.SafeToString(o); }
      #endregion

   }
   #endregion

   #region "Counter"
   public class Counter
   {
      private Counter() { }
      
      // BOOL QueryPerformanceCounter(LARGE_INTEGER *lpPerformanceCount);
      [System.Runtime.InteropServices.DllImport("Kernel32.dll")]
      static private extern int QueryPerformanceCounter(out long count);

      // BOOL QueryPerformanceFrequency(LARGE_INTEGER *lpFrequency);
      [System.Runtime.InteropServices.DllImport("Kernel32.dll")]
      static private extern int QueryPerformanceFrequency(out long freq);

      public static long Count { get { long i; QueryPerformanceCounter(out i); return i; } }
      public static long Frequency { get { long i; QueryPerformanceFrequency(out i); return i; } }
   }

   #endregion

   #region "DateTimeWrapper"
   /// <summary>
   /// 对 DateTime 类型进行封装的类
   /// </summary>
   public class DateTimeWrapper
   {
      private System.DateTime _v;
      public DateTimeWrapper() 
      {
         _v = System.DateTime.Now;
      }
      public DateTimeWrapper(System.DateTime v)
      {
         _v = v;
      }

      public System.DateTime Value { get { return _v; } set { _v = value; } }
   }

   #endregion

   #region "IntWrapper"
   /// <summary>
   /// 对一个整数进行封装的类
   /// </summary>
   public class IntWrapper
   {
      private int _v;
      public IntWrapper() { }
      public IntWrapper(int v) { _v = v; }

      public int Value { get { return _v; } set { _v = value; } }

      public static implicit operator int(IntWrapper iw) { return iw._v; }
   }
   #endregion

   #region "HrefUtil"
   public class HrefUtil
   {
      #region "GetParentHref/IsParentHref/IsAncestorHref/IsValidHref/SplitCategoryHref"
      /// <summary>获得 hrefChild 的父分类的分类路径</summary>
      /// <param name="hrefChild">子分类的分类路径</param>
      /// <returns>父分类的分类路径。根分类的父分类路径将返回 NULL。</returns>
      public static string GetParentHref(string hrefChild)
      {
         if( hrefChild == null ) throw new ArgumentNullException("hrefChild", "非法参数。");
         if( false == IsValidHref(hrefChild) ) throw new ArgumentException("不是一个合法的 Href 参数。", "hrefChild");

         if( hrefChild == "/" ) return null;           // 根分类没有父分类，所以我们返回 NULL

         hrefChild = hrefChild.Substring(0, hrefChild.Length-1);
         int pos = hrefChild.LastIndexOf('/');
         System.Diagnostics.Debug.Assert(pos >= 0);
         return hrefChild.Substring(0, pos + 1);
      }

      /// <summary>判断 hrefChild 是否是 hrefParent 的直接子分类</summary>
      /// <param name="hrefChild">子分类路径</param>
      /// <param name="hrefParent">父分类路径</param>
      /// <returns>true 表示 hrefChild 是直接子分类，false 表示不是。</returns>
      public static bool IsParentHref(string hrefChild, string hrefParent)
      {
         if( null == hrefChild ) throw new ArgumentNullException("hrefChild", "参数为空。");
         if( null == hrefParent ) throw new ArgumentNullException("hrefParent", "参数为空。");
         if( false == IsValidHref(hrefChild) ) throw new ArgumentException("不是一个合法的 Href 参数。", "hrefChild");
         if( false == IsValidHref(hrefParent) ) throw new ArgumentException("不是一个合法的 Href 参数。", "hrefParent");

         // 如果是子节点，则 hrefChild 的长度必定大于 hrefParent 的长度
         if( hrefChild.Length <= hrefParent.Length ) return false;

         // 前面部分必须完全相同 (TEXT 比较)
         if( string.Compare(hrefParent, hrefChild.Substring(0, hrefParent.Length), true) != 0 ) return false;

         string lastPart = hrefChild.Substring(hrefParent.Length);
         // 剩下的这一部分必须只有一个 / 且在最后一位
         if( lastPart.IndexOf('/') != lastPart.Length-1 ) return false;
         return true;
      }

      /// <summary>判断是否是一个合法的 Href</summary>
      /// <param name="href">分类的 Href 路径</param>
      /// <returns>true 表示合法，false 表示不合法</returns>
      public static bool IsValidHref(string href)
      {
         if( href == null || href == string.Empty ) return false;
         if( href[0] != '/' ) return false;
         if( href[href.Length-1] != '/' ) return false;
         if( href.IndexOf("//") >= 0 ) return false;
         return true;
      }
      /// <summary>判断 hrefAncestor 是否是 hrefChild 的祖先分类</summary>
      /// <param name="hrefChild">孙子分类路径</param>
      /// <param name="hrefAncestor">祖先分类路径</param>
      /// <returns>true 表示是一个祖先分类；false 表示不是一个祖先分类。</returns>
      public static bool IsAncestorHref(string hrefChild, string hrefAncestor)
      {
         if( null == hrefChild ) throw new ArgumentNullException("hrefChild", "参数为空。");
         if( null == hrefAncestor ) throw new ArgumentNullException("hrefAncestor", "参数为空。");
         if( false == IsValidHref(hrefChild) ) throw new ArgumentException("不是一个合法的 Href 参数。", "hrefChild");
         if( false == IsValidHref(hrefAncestor) ) throw new ArgumentException("不是一个合法的 Href 参数。", "hrefAncestor");

         // 如果是子节点，则 hrefChild 的长度必定大于 hrefParent 的长度
         if( hrefChild.Length <= hrefAncestor.Length ) return false;

         // 前面部分完全相同 (TEXT 比较) 则是祖先分类
         if( string.Compare(hrefAncestor, hrefChild.Substring(0, hrefAncestor.Length), true) != 0 ) return false;

         return true;
      }

      /// <summary>
      /// ' 1. 将 /A/B/C/ 变换为 '/', '/A/', '/A/B/', '/A/B/C/' 的函数
      /// ' 2. /A's/  变换为 '/', '/A''s/',
      /// ' 3. 将 /A/B/C 变换为 '', '/A', '/A/B', '/A/B/C' 的函数
      /// 用户在 Category 表格中查找该分类及其所有父辈分类用
      /// </summary>
      /// <param name="strHref">所给的路径</param>
      /// <returns></returns>
      public static string SplitCategoryHref(string strHref)
      {
         if( strHref == null || strHref == string.Empty )
            throw new ArgumentNullException("strHref", "您需要给出一个合法的分类路径");
         int style = (strHref[strHref.Length - 1] == '/') ? 1 : 2;
         string[] aHrefs = strHref.Split('/');

         int i;
         System.Text.StringBuilder strHrefs = new System.Text.StringBuilder();
         string strTemp = string.Empty;

         if( style == 1 )     // 模式一处理 /a/b/c/
         {
            for( i = 0 ; i < aHrefs.Length-1 ; i++ )
            {
               strTemp = string.Concat(strTemp, aHrefs[i].Replace("'", "''"), "/");
               if( i > 0 )
                  strHrefs.Append(',');
               strHrefs.Append('\'');
               strHrefs.Append(strTemp);
               strHrefs.Append('\'');
            }
         }
         else        // 模式二处理 /a/b/c
         {
            // 处理根
            strHrefs.Append("''");
            for( i = 0 + 1 ; i < aHrefs.Length ; i++ )
            {
               //      strTemp = strTemp  & "/" & aHrefs(i)
               //      strHrefs = strHrefs & ",'" & Replace(strTemp,"'","''") & "'"

               strTemp += "/" + aHrefs[i].Replace("'", "''");
               strHrefs.Append(",'");
               strHrefs.Append(strTemp);
               strHrefs.Append("'");
            }
         }

         return strHrefs.ToString();
      }


      #endregion
   }
   #endregion

   #region DateUitl
   public class DateUitl
   {
       public static DateTime getAuditingTime()
       {
           DateTime ldAudingDate = DateTime.Now.Hour <= 8 ? DateTime.Now.AddDays(-1) : DateTime.Now;
           return ldAudingDate;
       }
       public static DateTime GetDateTime()
       {
           return DateTime.Now;
       }
       public static DateTime? SafeToDateTimeNullable(object toObject)
       {
           if (toObject == DBNull.Value || toObject == null || toObject.ToString().Trim() == "")
               return null;
           else
               return Convert.ToDateTime(toObject);
       }
       public static string getDateString(object toValue)
       {
           if (toValue == DBNull.Value || toValue == null)
               return "";
           return Convert.ToDateTime(toValue).ToString("yyyy-MM-dd");
       }
       public static int getDateDiff(DateTime tdStartDate, DateTime tdEndDate)
       {
           TimeSpan loSpan = tdEndDate.Date.Subtract(tdStartDate.Date);
           return loSpan.Days;
       }
       /// <summary>
       /// 找出两个时间段的交集
       /// </summary>
       /// <param name="tdAStartDate"></param>
       /// <param name="tdAEndDate"></param>
       /// <param name="tdBStartDate"></param>
       /// <param name="tdBEndDate"></param>
       /// <returns></returns>
       public static DateTime[] getDateIntersect(DateTime tdAStartDate, DateTime tdAEndDate, DateTime tdBStartDate, DateTime tdBEndDate)
       {
           DateTime ldAStartDate = tdAStartDate;
           DateTime ldAEndDate = tdAEndDate;
           DateTime ldBStartDate = tdBStartDate;
           DateTime ldBEndDate = tdBEndDate;

           if (tdAEndDate > tdBEndDate)
           {
               ldAStartDate = tdBStartDate;
               ldAEndDate = tdBEndDate;
               ldBStartDate = tdAStartDate;
               ldBEndDate = tdAEndDate;
           }
           DateTime[] ldDateIntersects = new DateTime[2];
           TimeSpan loTimeSpan = ldAEndDate.Subtract(ldBStartDate);
           if (loTimeSpan.TotalMilliseconds >= 0)
           {
               if (ldAStartDate > ldBStartDate)
                   ldDateIntersects[0] = ldAStartDate;
               else
                   ldDateIntersects[0] = ldBStartDate;

               ldDateIntersects[1] = ldAEndDate;
               return ldDateIntersects;
           }
           return null;
       }

       public static void genHoldTime(DropDownList toList, DateTime tdArrivalDate)
       {
           DateTime ldSelectHoldTime = DateTime.MinValue;
           if (!String.IsNullOrEmpty(toList.SelectedValue))
               ldSelectHoldTime = Convert.ToDateTime(toList.SelectedValue);
           toList.Items.Clear();

           DateTime ldTempDate = tdArrivalDate;
           DateTime ldNextDate = Convert.ToDateTime(tdArrivalDate.AddDays(1).ToShortDateString());
           int lnTotalCount = 1; //最少8个小时
           while (ldTempDate <= ldNextDate || lnTotalCount<=16)
           {
               ListItem loItem = new ListItem();
               loItem.Value = ldTempDate.ToString("yyyy-MM-dd HH:mm");
               loItem.Text = ldTempDate.ToString("yyyy-MM-dd HH:mm");
               if (ldTempDate == tdArrivalDate.AddHours(2))
               {
                   if(toList.SelectedItem != null)
                   toList.SelectedItem.Selected = false;
                   loItem.Selected = true;
               }
               if (ldSelectHoldTime == ldTempDate)
               {
                   if (toList.SelectedItem != null)
                       toList.SelectedItem.Selected = false;
                   loItem.Selected = true;
               }
               toList.Items.Add(loItem);
               ldTempDate = ldTempDate.AddMinutes(30);
               lnTotalCount++;
           }
       }
       //验证字符串是否为日期
       //格式yyyy-MM-dd
       public static bool isDateString(string strDate)
       {
           string strRegex = @"((^((1[8-9]\d{2})|([2-9]\d{3}))([-\/\._])(10|12|0?[13578])([-\/\._])(3[01]|[12][0-9]|0?[1-9])$)|(^((1[8-9]\d{2})|([2-9]\d{3}))([-\/\._])(11|0?[469])([-\/\._])(30|[12][0-9]|0?[1-9])$)|(^((1[8-9]\d{2})|([2-9]\d{3}))([-\/\._])(0?2)([-\/\._])(2[0-8]|1[0-9]|0?[1-9])$)|(^([2468][048]00)([-\/\._])(0?2)([-\/\._])(29)$)|(^([3579][26]00)([-\/\._])(0?2)([-\/\._])(29)$)|(^([1][89][0][48])([-\/\._])(0?2)([-\/\._])(29)$)|(^([2-9][0-9][0][48])([-\/\._])(0?2)([-\/\._])(29)$)|(^([1][89][2468][048])([-\/\._])(0?2)([-\/\._])(29)$)|(^([2-9][0-9][2468][048])([-\/\._])(0?2)([-\/\._])(29)$)|(^([1][89][13579][26])([-\/\._])(0?2)([-\/\._])(29)$)|(^([2-9][0-9][13579][26])([-\/\._])(0?2)([-\/\._])(29)$))";

           Regex re = new Regex(strRegex);
           if (re.IsMatch(strDate))
               return (true);
           else
               return (false);
       }

       public static DateTime getDateFromYYYYYMMDD(string tsDate)
       {
           if (tsDate.Length != 8)
               return DateTime.MinValue;
           else
           {
               return new DateTime(int.Parse(tsDate.Substring(0, 4)), int.Parse(tsDate.Substring(4, 2)), int.Parse(tsDate.Substring(6, 2)));
           }

       }

   }
   #endregion

   #region "DouUtil"
   public class DouUtil
    {
        static public double SafeCDou(object v)
        {
            if (v == null || Convert.DBNull.Equals(v)) return 0;
            if (v.GetType() == typeof(string) && string.Empty.Equals(v)) return 0;
            try { return Convert.ToDouble(v); }
            catch (InvalidCastException) { }
            catch (FormatException) { }
            return 0;
        }

        static public double? SafeCDouNullable(object v)
        {
            if (v == null || Convert.DBNull.Equals(v)) return null;
            if (v.GetType() == typeof(string) && string.Empty.Equals(v)) return null;
            try { return Convert.ToDouble(v); }
            catch (InvalidCastException) { }
            catch (FormatException) { }
            return null;
        }
    }
     #endregion
	
   #region "DataUtility"

    public class DataUtility
    {
        public static string AppendString(string tsTarget, string tsSeperator, string tsSource)
        {
            if (tsTarget == "") return tsSource;
            else
            {
                if (tsSource == "") return tsTarget;
                else
                {
                    return tsTarget + tsSeperator + tsSource;
                }
            }
        }
        /// <summary>
        /// 对给定List集合，把集合内所有选中的项值以给定的分隔符组成符串
        /// </summary>
        /// <param name="toItems">List集合</param>
        /// <param name="tsPer">分隔符</param>
        /// <returns>以分隔符组成符串</returns>
        /// create by yanght 2007-10-24
        public static string GetSelectedValues(System.Web.UI.WebControls.ListItemCollection toItems, string tsPer)
        {
            string lsValues = "";
            foreach (System.Web.UI.WebControls.ListItem loItem in toItems)
            {
                if (loItem.Selected)
                {
                    if (lsValues != "") lsValues += tsPer;
                    lsValues += loItem.Value;
                }
            }
            return lsValues;
        }
        /// <summary>
        /// 根据给定的字符串值按照给定的分割符在List集合中选中
        /// </summary>
        /// <param name="toItems">List集合</param>
        /// <param name="tsPer">分隔符</param>
        /// <param name="tsValues">字符串值</param>
        /// create by yanght 2007-10-24
        public static void SetItemSelected(System.Web.UI.WebControls.ListItemCollection toItems, string tsPer,string tsValues)
        {
            string[] lsValues = tsValues.Split(tsPer.ToCharArray());
            foreach (string lsValue in lsValues)
            {
                System.Web.UI.WebControls.ListItem loItem = toItems.FindByValue(lsValue);
                if (loItem != null)
                    loItem.Selected = true;
            }
        }
        public static string GetNewFileName(string FileName)
        {   
            //跟据文件名产生一个由时间+随机数组成的一个新的文件名
            string newfilename = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString()
            + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString()
            + DateTime.Now.Second.ToString() + DateTime.Now.Minute.ToString()
            + DateTime.Now.Millisecond.ToString()
                //+ rand.Next(1000).ToString() 
            + FileName.Substring(FileName.LastIndexOf("."), FileName.Length - FileName.LastIndexOf("."));
            return newfilename;
        }
        /// <summary>
        /// 对于给定的DataTable 分组字段对DataTable 进行分组。
        /// </summary>
        /// <param name="toDt">需分组的表</param>
        /// <param name="tsGroupFields">分组的字段</param>
        /// <param name="tsSortFields">需要排序的字段</param>
        /// <returns>已分好组的Arraylist</returns>
        public static ArrayList getGroupRow(DataTable toDt,string[] tsGroupFields,string[] tsSortFields)
        {
            ArrayList loHaveGroupRows = new ArrayList();
            string lsSortStr = "";
            if (tsSortFields != null)
            {
                foreach (string lsString in tsSortFields)
                {
                    if (lsSortStr != "") lsSortStr += ",";
                    lsSortStr += lsString;
                }
            }
            if (toDt.Rows.Count > 0)
            {
                DataView loDataView = toDt.DefaultView;
                if (lsSortStr != null)
                    loDataView.Sort = lsSortStr;
                List<DataRow> loGroupRows = new List<DataRow>();
                foreach(DataRowView loRowView in loDataView)
                {
                    loGroupRows.Add(loRowView.Row);
                }
                loHaveGroupRows = getGroupRow(loGroupRows.ToArray(), tsGroupFields);
            }
            return loHaveGroupRows;
        }
        /// <summary>
        /// 对于给定的DataRow[],分组字段对DataRow[]进行分组。
        /// </summary>
        /// <param name="toDt">需分组的行集合</param>
        /// <param name="tsGroupFields">分组的字段</param>
        /// <returns>已分好组的Arraylist</returns>
        public static ArrayList getGroupRow(DataRow[] toRows, string[] tsGroupFields)
        {
            ArrayList loHaveGroupRows = new ArrayList();
            if (toRows.Length> 0)
            {
                List<DataRow> loGroupRows = new List<DataRow>();
                Object[] loValues = new Object[tsGroupFields.Length];
                for (int lnIndex = 0; lnIndex < tsGroupFields.Length; lnIndex++)
                    loValues[lnIndex] = toRows[0][tsGroupFields[lnIndex]];
                for (int lnIndex = 0; lnIndex < toRows.Length; lnIndex++)
                {
                    bool lbIsSame = true;
                    for (int i = 0; i < loValues.Length; i++)
                    {
                        if (!toRows[lnIndex][tsGroupFields[i]].Equals(loValues[i]))
                        {
                            lbIsSame = false;
                            break;
                        }

                    }
                    if (lbIsSame)
                    {
                        loGroupRows.Add(toRows[lnIndex]);
                    }
                    if ((!lbIsSame) && lnIndex != toRows.Length - 1)
                    {
                        loValues = new Object[tsGroupFields.Length];
                        for (int i = 0; i < tsGroupFields.Length; i++)
                            loValues[i] = toRows[lnIndex][tsGroupFields[i]];
                        loHaveGroupRows.Add(loGroupRows);
                        loGroupRows = new List<DataRow>();
                        loGroupRows.Add(toRows[lnIndex]);
                    }
                    if (lnIndex == toRows.Length - 1)
                    {
                        if (!lbIsSame)
                        {
                            loHaveGroupRows.Add(loGroupRows);
                            loGroupRows = new List<DataRow>();
                            loGroupRows.Add(toRows[lnIndex]);
                        }
                        loHaveGroupRows.Add(loGroupRows);
                    }
                }
            }
            return loHaveGroupRows;
        }
        public static ArrayList GGTest(int lnRoomDay)
        {

            ArrayList loArray = new ArrayList();
            string[] pp = null;
            int[] lnPageCount = new int[4] { 5, 4, 3, 2 };
            for (int i = 0; i < lnPageCount.Length - 1; i++)
            {
                for (int j = i + 1; j < lnPageCount.Length - 1; j++)
                {
                    for (int m = 0; m < lnRoomDay / lnPageCount[i]; m++)
                    {
                        int n = (lnRoomDay - m * (lnPageCount[i])) / lnPageCount[j];
                        int t = lnRoomDay - lnPageCount[i] * m - lnPageCount[j] * n;
                        pp = new string[5];
                        pp[0] = lnPageCount[i].ToString();
                        pp[1] = m.ToString();
                        pp[2] = lnPageCount[j].ToString();
                        pp[3] = n.ToString();
                        pp[4] = t.ToString();
                        loArray.Add(pp);
                    }
                }
            }
            return loArray;
        }

       
    }
   #endregion
}
