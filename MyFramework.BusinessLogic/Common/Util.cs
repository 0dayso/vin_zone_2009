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
   /// <summary>���ݿ����ʱ���һ���������</summary>
   public class DBUtil
   {
       public enum StringType { Echar = 1, Enchar = 2, Evarchar = 3, Envarchar = 4, Eclob = 5, Enclob = 6 };
      
      #region SafeGetInt32
      /// <summary>�ӽ�����л�ȡ����Ϊ i ���ֶε�����ֵ��ȱʡΪ defVal</summary>
      /// <param name="rs">�����</param>
      /// <param name="i">�ֶ�����</param>
      /// <param name="defVal">ȱʡֵ</param>
      /// <returns></returns>
      public static int SafeGetInt32(IDataReader rs, int i, int defVal)
      {
         if( rs.IsDBNull(i) )
            return defVal; 
          return Convert.ToInt32(rs[i]);
      }

      /// <summary>�ӽ�����л�ȡ����Ϊ i ���ֶε�����ֵ��ȱʡΪ 0</summary>
      /// <param name="rs">�����</param>
      /// <param name="i">�ֶ�����</param>
      public static int SafeGetInt32(IDataReader rs, int i)
      {
         return SafeGetInt32(rs, i, 0);
      }

      /// <summary>�ӽ�����л�ȡ����Ϊ field ���ֶε�����ֵ��ȱʡΪ defVal</summary>
      /// <param name="rs">�����</param>
      /// <param name="field">�ֶ�����</param>
      /// <param name="defVal">ȱʡֵ</param>
      public static int SafeGetInt32(IDataReader rs, string field, int defVal)
      {
         return SafeGetInt32(rs, rs.GetOrdinal(field), defVal);
      }

      /// <summary>�ӽ�����л�ȡ����Ϊ field ���ֶε�����ֵ��ȱʡΪ 0</summary>
      /// <param name="rs">�����</param>
      /// <param name="field">�ֶ�����</param>
      public static int SafeGetInt32(IDataReader rs, string field)
      {
         return SafeGetInt32(rs, rs.GetOrdinal(field), 0);
      }

      #endregion

      #region SafeGetString
      /// <summary>�ӽ�����л�ȡ����Ϊ i ���ֶε��ַ�����ȱʡΪ defVal</summary>
      /// <param name="rs">�����</param>
      /// <param name="i">�ֶ�����</param>
      /// <param name="defVal">ȱʡֵ</param>
      public static string SafeGetString(IDataReader rs, int i, string defVal)
      {
         if( rs.IsDBNull(i) )
            return defVal;
         return Convert.ToString(rs[i]);
      }

      /// <summary>�ӽ�����л�ȡ����Ϊ i ���ֶε��ַ�����ȱʡΪ string.Empty</summary>
      /// <param name="rs">�����</param>
      /// <param name="i">�ֶ�����</param>
      public static string SafeGetString(IDataReader rs, int i)
      {
         return SafeGetString(rs, i, string.Empty);
      }

      /// <summary>�ӽ�����л�ȡ����Ϊ field ���ֶε��ַ�����ȱʡΪ defVal</summary>
      /// <param name="rs">�����</param>
      /// <param name="field">�ֶ�����</param>
      /// <param name="defVal">ȱʡֵ</param>
      public static string SafeGetString(IDataReader rs, string field, string defVal)
      {
         return SafeGetString(rs, rs.GetOrdinal(field), defVal);
      }

      /// <summary>�ӽ�����л�ȡ����Ϊ field ���ֶε��ַ�����ȱʡΪ string.Empty</summary>
      /// <param name="rs">�����</param>
      /// <param name="field">�ֶ�����</param>
      public static string SafeGetString(IDataReader rs, string field)
      {
         return SafeGetString(rs, rs.GetOrdinal(field), string.Empty);
      }
      #endregion

      #region "SafeGetDateTime"
      /// <summary>�ӽ�����л�ȡ����Ϊ i ���ֶε�����ֵ��ȱʡΪ defVal</summary>
      /// <param name="rs">�����</param>
      /// <param name="i">�ֶ�����</param>
      /// <param name="defVal">ȱʡֵ</param>
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

      /// <summary>�ӽ�����л�ȡ����Ϊ i ���ֶε�����ֵ��ȱʡΪ DateTime(0)</summary>
      /// <param name="rs">�����</param>
      /// <param name="i">�ֶ�����</param>
      public static System.DateTime SafeGetDateTime(IDataReader rs, int i)
      {
         return SafeGetDateTime(rs, i, new System.DateTime(0));
      }

      /// <summary>�ӽ�����л�ȡ����Ϊ field ���ֶε�����ֵ��ȱʡΪ defVal</summary>
      /// <param name="rs">�����</param>
      /// <param name="field">�ֶ�����</param>
      /// <param name="defVal">ȱʡֵ</param>
      public static System.DateTime SafeGetDateTime(IDataReader rs, string field, System.DateTime defVal)
      {
         return SafeGetDateTime(rs, rs.GetOrdinal(field), defVal);
      }

      /// <summary>�ӽ�����л�ȡ����Ϊ field ���ֶε�����ֵ��ȱʡΪ defVal</summary>
      /// <param name="rs">�����</param>
      /// <param name="field">�ֶ�����</param>
      public static System.DateTime SafeGetDateTime(IDataReader rs, string field)
      {
         return SafeGetDateTime(rs, rs.GetOrdinal(field), new System.DateTime(0));
      }
      #endregion

      #region "SafeGetDouble"
      /// <summary>�ӽ�����л�ȡ����Ϊ field ���ֶε�˫���ȸ���ֵ��ȱʡΪ defVal</summary>
      /// <param name="rs">�����</param>
      /// <param name="field">�ֶ�����</param>
      /// <param name="defVal">ȱʡֵ</param>
      /// <returns></returns>
      public static double SafeGetDouble(IDataReader rs, int field, double defVal)
      {
         try { if( rs.IsDBNull(field) ) return defVal; else return Convert.ToDouble(rs.GetValue(field)); }
         catch(System.InvalidCastException) { return defVal; }
         catch(System.FormatException) { return defVal; }
      }

      /// <summary>�ӽ�����л�ȡ����Ϊ field ���ֶε�˫���ȸ���ֵ��ȱʡΪ 0</summary>
      /// <param name="rs">�����</param>
      /// <param name="field">�ֶ�����</param>
      public static double SafeGetDouble(IDataReader rs, string field)
      {
         return SafeGetDouble(rs, rs.GetOrdinal(field), 0);
      }

      /// <summary>�ӽ�����л�ȡ����Ϊ i ���ֶε�˫���ȸ���ֵ��ȱʡΪ 0</summary>
      /// <param name="rs">�����</param>
      /// <param name="i">�ֶ�����</param>
      public static double SafeGetDouble(IDataReader rs, int i)
      {
         return SafeGetDouble(rs, i, 0);
      }

      /// <summary>�ӽ�����л�ȡ����Ϊ i ���ֶε�˫���ȸ���ֵ��ȱʡΪ defVal</summary>
      /// <param name="rs">�����</param>
      /// <param name="i">�ֶ�����</param>
      /// <param name="defVal">ȱʡֵ</param>
      public static double SafeGetDouble(IDataReader rs, string i, double defVal)
      {
         return SafeGetDouble(rs, rs.GetOrdinal(i), defVal);
      }
      #endregion



      #region "AddParameter"
       //add by yanght 2007/09/25 begin
       //��ӶԿɿ�ֵ�Ŀ���
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



      /// <summary>��һ�� IDbCommand ������Ӳ���</summary>
      /// <param name="sqlcmd">IDbCommand ����</param>
      /// <param name="paramName">��������</param>
      /// <param name="val">����(int)����ֵ</param>
       public static System.Data.IDbDataParameter AddParameter(OracleCommand sqlcmd, string paramName, OracleType val)
      {
          return AddParameter(sqlcmd, paramName, DBNull.Value, val, ParameterDirection.Input);
      }
      /// <summary>��һ�� IDbCommand ������Ӳ���</summary>
      /// <param name="sqlcmd">IDbCommand ����</param>
      /// <param name="paramName">��������</param>
      /// <param name="val">����(int)����ֵ</param>
      public static System.Data.IDbDataParameter AddParameter(OracleCommand sqlcmd, string paramName, int val)
      {

          return AddParameter(sqlcmd, paramName, val, OracleType.Number, ParameterDirection.Input);
      }
      /// <summary>��һ�� IDbCommand ������Ӳ���</summary>
      /// <param name="sqlcmd">IDbCommand ����</param>
      /// <param name="paramName">��������</param>
      /// <param name="val">Int64����ֵ</param>
      public static System.Data.IDbDataParameter AddParameter(OracleCommand sqlcmd, string paramName, Int64 val)
      {

          return AddParameter(sqlcmd, paramName, val, OracleType.Double, ParameterDirection.Input);
      }

      /// <summary>��һ�� IDbCommand ������Ӳ���</summary>
      /// <param name="sqlcmd">IDbCommand ����</param>
      /// <param name="paramName">��������</param>
      /// <param name="val">˫���ȸ�����(double)����ֵ</param>
       public static System.Data.IDbDataParameter AddParameter(OracleCommand sqlcmd, string paramName, double val)
      {

          return AddParameter(sqlcmd, paramName, val, OracleType.Number, ParameterDirection.Input);
      }

      /// <summary>��һ�� IDbCommand ������Ӳ���</summary>
      /// <param name="sqlcmd">IDbCommand ����</param>
      /// <param name="paramName">��������</param>
      /// <param name="val">������(DateTime)����ֵ</param>
       public static System.Data.IDbDataParameter AddParameter(OracleCommand sqlcmd, string paramName, System.DateTime val)
      {
        
           return AddParameter(sqlcmd, paramName, val, OracleType.DateTime, ParameterDirection.Input);
      }

      /// <summary>��һ�� IDbCommand ������Ӳ���</summary>
      /// <param name="sqlcmd">IDbCommand ����</param>
      /// <param name="paramName">��������</param>
      /// <param name="val">�ַ�����(string)����ֵ</param>
       public static System.Data.IDbDataParameter AddParameter(OracleCommand sqlcmd, string paramName, string val)
      {
          if (val == null)
                return AddParameter(sqlcmd, paramName, OracleType.VarChar);
          return AddParameter(sqlcmd, paramName, val, OracleType.VarChar, ParameterDirection.Input);
      }

    
      /// <summary>��һ�� IDbCommand ������Ӳ���</summary>
      /// <param name="sqlcmd">IDbCommand ����</param>
      /// <param name="paramName">��������</param>
      /// <param name="val">�ַ�����(string)����ֵ</param>
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
    
      /// <summary>��һ�� IDbCommand ������Ӳ���</summary>
      /// <param name="sqlcmd">IDbCommand ����</param>
      /// <param name="paramName">��������</param>
      /// <param name="val">����ֵ</param>
      /// <param name="dbtype">��������</param>
      /// <param name="direction">��������</param>
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
       /// ��Oracle�����鸳�� OraCommand
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
      /// <summary>ִ�� IDbCommand �� ExcecuteScalar �������ת��Ϊ int ��, ȱʡֵΪ 0</summary>
      /// <param name="sqlcmd">IDbCommand</param>
      /// <returns></returns>
      public static int SafeGetScalarInt32(IDbCommand sqlcmd) { return SafeGetScalarInt32(sqlcmd, 0); }

      /// <summary>ִ�� IDbCommand �� ExcecuteScalar �������ת��Ϊ int ��, ȱʡֵΪ defvalue</summary>
      /// <param name="sqlcmd">IDbCommand</param>
      /// <param name="defvalue">ȱʡֵ</param>
      /// <returns></returns>
      public static int SafeGetScalarInt32(IDbCommand sqlcmd, int defvalue)
      {
         object o = sqlcmd.ExecuteScalar();
         if( o == null || o == Convert.DBNull )
            return defvalue;
         return Convert.ToInt32(o);
      }

      /// <summary>ִ�� IDbCommand �� ExcecuteScalar �������ת��Ϊ string ��, ȱʡֵΪ string.Empty</summary>
      /// <param name="sqlcmd">IDbCommand</param>
      /// <returns></returns>
      public static string SafeGetScalarString(IDbCommand sqlcmd)
      {
         return SafeGetScalarString(sqlcmd, string.Empty);
      }

      /// <summary>ִ�� IDbCommand �� ExcecuteScalar �������ת��Ϊ string ��, ȱʡֵΪ defvalue</summary>
      /// <param name="sqlcmd">IDbCommand</param>
      /// <param name="defvalue">ȱʡֵ</param>
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

      #region "FillDataTable - ��� DataTable"
      /// <summary>ʹ��ָ�������ݶ�ȡ��������ݱ� (V 0.1)</summary>
      /// <param name="rs">���ݶ�ȡ��</param>
      /// <param name="dt">���ݱ�</param>
      public static void FillDataTable(IDataReader rs, System.Data.DataTable dt)
      {
         FillDataTable(rs, dt, 0, -1);
      }

      /// <summary>ʹ��ָ�������ݶ�ȡ��������ݱ� (V 0.1)</summary>
      /// <param name="rs">���ݶ�ȡ��</param>
      /// <param name="dt">���ݱ�</param>
      /// <param name="start">��ʼλ��, = 0 ��ʾ�ӵ�һ����¼��ʼ���</param>
      /// <param name="count">�������, = -1 ��ʾ���ֱ���������ݶ�ȡ����</param>
      public static void FillDataTable(IDataReader rs, System.Data.DataTable dt, int start, int count)
      {
         if( rs == null )
            throw new ArgumentNullException("rs", "��Ҫһ�����ݶ�ȡ��");
         if( dt == null )
            throw new ArgumentNullException("dt", "��Ҫ����Ҫ�������ݱ����");

         // 1. Get Schema Table and build dt
         
         int i, fieldCount = rs.FieldCount;
         if( fieldCount <= 0 )
            throw new ApplicationException("���ݶ�ȡ����û���κ��ֶ�");
         
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
   /// <summary>VB �ַ��������һЩ�򵥰�������</summary>
    public class StrUtil
    {
        #region "Left/Right/Mid"
        private StrUtil() { }
        /// <summary>
        /// ��ȡһ���ַ��������ָ�����ȵĲ��֣�����ַ������Ȳ������򷵻�ȫ��
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
        /// ��ȡһ���ַ������ұ�ָ�����ȵĲ��֣�����ַ������Ȳ������򷵻�ȫ��
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
        /// Visual Basic �� Mid ʵ�֡�ע�⣺ start �Ǵ� 1 ��ʼ�ġ�
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
        /// <summary>���ַ����е� ������ �滻Ϊ���������ţ����ṩ�� SQL Server ��Ϊ��ѯ��</summary>
        /// <param name="str">Ҫ������ַ���</param>
        /// <returns></returns>
        public static string SqlString(string str)
        {
            if (str == null || str == string.Empty)
                return string.Empty;
            if (str.IndexOf('\'') < 0)
                return str;
            return str.Replace("'", "''");
        }

        /// <summary>��ȫ�Ľ�һ������ת��Ϊ�ַ���</summary>
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
        /// Ϊǰ��ʹ�õ� JavaScript �����ַ���, ��Щ�ַ����� \ \r\n ' ����Ҫ�滻Ϊ��Ӧ���ַ�.
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
        /// ������������ �� ���ֵ�ֵ�� �� ���ֵ�ֵ�����Ϊһ���ַ�����
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
        /// ��һ���ַ����ظ�ָ�����������ؽ��
        /// </summary>
        /// <param name="v">ģ���ַ���</param>
        /// <param name="n">�ظ�����</param>
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
        /// �жϸ�����һ���ַ����Ƿ���һ����Ч Guid ��ʽ
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

        #region "XMLEncode - �ַ����滻Ϊ�ű����"
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
   /// �� Http Request �л�ȡ�����İ�����
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
         // ASP.NET �о�Ȼ������ȫɾ��һ�� Cookie ��ֻ���ö��ӵķ�������
         HttpContext ctx = HttpContext.Current;
         HttpCookie cookie = ctx.Request.Cookies[key];
         if( cookie == null ) return;     // ��Ҫ����ȥ����һ�����򷴶���������
         cookie = new HttpCookie(key, null);
         cookie.Expires = System.DateTime.Now.AddYears(-10);
         ctx.Response.Cookies.Remove(key);
         ctx.Response.Cookies.Set(cookie);
      }
      public static void SafeRemoveCookie(string key1, string key2)
      {
         // �ڵ�һ��������ɾ��ĳһ�������鷳
         HttpContext ctx = HttpContext.Current;
         HttpCookie oldCookie = ctx.Request.Cookies[key1];
         if( oldCookie == null ) return;        // л��л�أ�û����� Cookie

         if( oldCookie.HasKeys == false ) return;  // л��л�أ�û���Ӽ�
         if( oldCookie.Values[key2] == null ) return; // л��л�أ�û��Ҫ�ļ�

         // ��¡һ��������
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
      // �����ʽ�� http(s)://www.chinaedustar.com:888/vdir/page.aspx
      // ����Ҫ����� http(s)://www.chinaedustar.com:888 ����
      public static string GetServerUrl(string url)
      {
         if( url == null || url == string.Empty )
            throw new ArgumentNullException("url", "�Ƿ��� url ����");
         // 1. ���� http:// ����
         int i = url.IndexOf("//");
         if( i < 0 ) throw new ArgumentException("�Ƿ��� url ֵ", "url");
         int j = url.IndexOf("/", i + 2);
         if( j < 0 ) return url;             // ��Ϊ������� ServerUrl
         return url.Substring(0, j);
      }


      /// <summary>����Ŀ¼�� ���Ŀ¼�����ڣ��򴴽����������̷������쳣���ǻ��׳�</summary>
      /// <param name="dir">����Ŀ¼��ȫ·��</param>
      public static void SafeCreateDirectory(string dir)
      {
         if( System.IO.Directory.Exists(dir) == false )
            System.IO.Directory.CreateDirectory(dir);
      }


      /// <summary>��·���м�������һ���ļ��е�����</summary>
      /// <param name="path"></param>
      /// <returns></returns>
      public static string GetFolderName(string path)
      {
         if( path == null || path == string.Empty ) throw new ArgumentNullException("path", "�Ƿ���·������");
         if(path.EndsWith("\\"))
            path = path.Substring(0, path.Length - 1);
         
         int i = path.LastIndexOf('\\');
         if( i < 0 ) return path;
         return path.Substring(i+1, path.Length-i-1);
      }


      /// <summary>����ļ���׺</summary>
      /// <param name="fileName"></param>
      /// <returns>�ļ���׺������ '.' ���š�����ļ�û�к�׺���򷵻� string.Empty</returns>
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
      /// �жϸ������ַ����Ƿ���һ����Ч�� Url
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
      /// �ϳɷ����ַ�� hostUrl + svcPath��ȥ�����ܵĶ��� /
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

      /// <summary>�ж�һ���ַ����Ƿ���һ������</summary>
      /// <param name="str"></param>
      /// <returns></returns>
      public static bool IsInteger(string str)
      {
         if( str == null || str == string.Empty ) return false;
         try { int i = int.Parse(str); return true; }			//whether should use Int32.Parse(..), writen by dust
         catch(InvalidCastException) { return false; }
         catch(FormatException) {return false;}
      }

      /// <summary>��һ�����ʶ�����ַ���������Ϊ��ʶ����</summary>
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

      /// <summary>�����ⲿ���ݹ����� idlist ���͵Ĳ����Ƿ�Ϸ�</summary>
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

      /// <summary>��һ���ʶת��Ϊ�ַ������м��� ',' �ŷָ�</summary>
      /// <param name="ids"></param>
      /// <returns></returns>
      public static string GetIdlistString(int[] ids)
      {
         if( ids == null || ids.Length == 0 ) throw new ArgumentNullException("ids", "�Ƿ�������Ҫ��ǿ������ٰ���һ��Ԫ�ء�");
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
         // �����������ǲ�ʶ��, �Ӷ����� false
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
   /// �� DateTime ���ͽ��з�װ����
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
   /// ��һ���������з�װ����
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
      /// <summary>��� hrefChild �ĸ�����ķ���·��</summary>
      /// <param name="hrefChild">�ӷ���ķ���·��</param>
      /// <returns>������ķ���·����������ĸ�����·�������� NULL��</returns>
      public static string GetParentHref(string hrefChild)
      {
         if( hrefChild == null ) throw new ArgumentNullException("hrefChild", "�Ƿ�������");
         if( false == IsValidHref(hrefChild) ) throw new ArgumentException("����һ���Ϸ��� Href ������", "hrefChild");

         if( hrefChild == "/" ) return null;           // ������û�и����࣬�������Ƿ��� NULL

         hrefChild = hrefChild.Substring(0, hrefChild.Length-1);
         int pos = hrefChild.LastIndexOf('/');
         System.Diagnostics.Debug.Assert(pos >= 0);
         return hrefChild.Substring(0, pos + 1);
      }

      /// <summary>�ж� hrefChild �Ƿ��� hrefParent ��ֱ���ӷ���</summary>
      /// <param name="hrefChild">�ӷ���·��</param>
      /// <param name="hrefParent">������·��</param>
      /// <returns>true ��ʾ hrefChild ��ֱ���ӷ��࣬false ��ʾ���ǡ�</returns>
      public static bool IsParentHref(string hrefChild, string hrefParent)
      {
         if( null == hrefChild ) throw new ArgumentNullException("hrefChild", "����Ϊ�ա�");
         if( null == hrefParent ) throw new ArgumentNullException("hrefParent", "����Ϊ�ա�");
         if( false == IsValidHref(hrefChild) ) throw new ArgumentException("����һ���Ϸ��� Href ������", "hrefChild");
         if( false == IsValidHref(hrefParent) ) throw new ArgumentException("����һ���Ϸ��� Href ������", "hrefParent");

         // ������ӽڵ㣬�� hrefChild �ĳ��ȱض����� hrefParent �ĳ���
         if( hrefChild.Length <= hrefParent.Length ) return false;

         // ǰ�沿�ֱ�����ȫ��ͬ (TEXT �Ƚ�)
         if( string.Compare(hrefParent, hrefChild.Substring(0, hrefParent.Length), true) != 0 ) return false;

         string lastPart = hrefChild.Substring(hrefParent.Length);
         // ʣ�µ���һ���ֱ���ֻ��һ�� / �������һλ
         if( lastPart.IndexOf('/') != lastPart.Length-1 ) return false;
         return true;
      }

      /// <summary>�ж��Ƿ���һ���Ϸ��� Href</summary>
      /// <param name="href">����� Href ·��</param>
      /// <returns>true ��ʾ�Ϸ���false ��ʾ���Ϸ�</returns>
      public static bool IsValidHref(string href)
      {
         if( href == null || href == string.Empty ) return false;
         if( href[0] != '/' ) return false;
         if( href[href.Length-1] != '/' ) return false;
         if( href.IndexOf("//") >= 0 ) return false;
         return true;
      }
      /// <summary>�ж� hrefAncestor �Ƿ��� hrefChild �����ȷ���</summary>
      /// <param name="hrefChild">���ӷ���·��</param>
      /// <param name="hrefAncestor">���ȷ���·��</param>
      /// <returns>true ��ʾ��һ�����ȷ��ࣻfalse ��ʾ����һ�����ȷ��ࡣ</returns>
      public static bool IsAncestorHref(string hrefChild, string hrefAncestor)
      {
         if( null == hrefChild ) throw new ArgumentNullException("hrefChild", "����Ϊ�ա�");
         if( null == hrefAncestor ) throw new ArgumentNullException("hrefAncestor", "����Ϊ�ա�");
         if( false == IsValidHref(hrefChild) ) throw new ArgumentException("����һ���Ϸ��� Href ������", "hrefChild");
         if( false == IsValidHref(hrefAncestor) ) throw new ArgumentException("����һ���Ϸ��� Href ������", "hrefAncestor");

         // ������ӽڵ㣬�� hrefChild �ĳ��ȱض����� hrefParent �ĳ���
         if( hrefChild.Length <= hrefAncestor.Length ) return false;

         // ǰ�沿����ȫ��ͬ (TEXT �Ƚ�) �������ȷ���
         if( string.Compare(hrefAncestor, hrefChild.Substring(0, hrefAncestor.Length), true) != 0 ) return false;

         return true;
      }

      /// <summary>
      /// ' 1. �� /A/B/C/ �任Ϊ '/', '/A/', '/A/B/', '/A/B/C/' �ĺ���
      /// ' 2. /A's/  �任Ϊ '/', '/A''s/',
      /// ' 3. �� /A/B/C �任Ϊ '', '/A', '/A/B', '/A/B/C' �ĺ���
      /// �û��� Category ����в��Ҹ÷��༰�����и���������
      /// </summary>
      /// <param name="strHref">������·��</param>
      /// <returns></returns>
      public static string SplitCategoryHref(string strHref)
      {
         if( strHref == null || strHref == string.Empty )
            throw new ArgumentNullException("strHref", "����Ҫ����һ���Ϸ��ķ���·��");
         int style = (strHref[strHref.Length - 1] == '/') ? 1 : 2;
         string[] aHrefs = strHref.Split('/');

         int i;
         System.Text.StringBuilder strHrefs = new System.Text.StringBuilder();
         string strTemp = string.Empty;

         if( style == 1 )     // ģʽһ���� /a/b/c/
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
         else        // ģʽ������ /a/b/c
         {
            // �����
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
       /// �ҳ�����ʱ��εĽ���
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
           int lnTotalCount = 1; //����8��Сʱ
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
       //��֤�ַ����Ƿ�Ϊ����
       //��ʽyyyy-MM-dd
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
        /// �Ը���List���ϣ��Ѽ���������ѡ�е���ֵ�Ը����ķָ�����ɷ���
        /// </summary>
        /// <param name="toItems">List����</param>
        /// <param name="tsPer">�ָ���</param>
        /// <returns>�Էָ�����ɷ���</returns>
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
        /// ���ݸ������ַ���ֵ���ո����ķָ����List������ѡ��
        /// </summary>
        /// <param name="toItems">List����</param>
        /// <param name="tsPer">�ָ���</param>
        /// <param name="tsValues">�ַ���ֵ</param>
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
            //�����ļ�������һ����ʱ��+�������ɵ�һ���µ��ļ���
            string newfilename = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString()
            + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString()
            + DateTime.Now.Second.ToString() + DateTime.Now.Minute.ToString()
            + DateTime.Now.Millisecond.ToString()
                //+ rand.Next(1000).ToString() 
            + FileName.Substring(FileName.LastIndexOf("."), FileName.Length - FileName.LastIndexOf("."));
            return newfilename;
        }
        /// <summary>
        /// ���ڸ�����DataTable �����ֶζ�DataTable ���з��顣
        /// </summary>
        /// <param name="toDt">�����ı�</param>
        /// <param name="tsGroupFields">������ֶ�</param>
        /// <param name="tsSortFields">��Ҫ������ֶ�</param>
        /// <returns>�ѷֺ����Arraylist</returns>
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
        /// ���ڸ�����DataRow[],�����ֶζ�DataRow[]���з��顣
        /// </summary>
        /// <param name="toDt">�������м���</param>
        /// <param name="tsGroupFields">������ֶ�</param>
        /// <returns>�ѷֺ����Arraylist</returns>
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
