using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.XPath;
using System.IO;
using System.Text.RegularExpressions;

namespace BLL.Common.Operation
{
    public class RegexOperation
    {
        #region ��ȡxml�ĵ������α�
        /// <summary>
        /// ��ȡxml�ĵ������α�
        /// </summary>
        /// <param name="strDocument">xml�ĵ�·��</param>
        /// <returns></returns>
        public static XPathNavigator GetXPathNavigatorByPath(string strPath)
        {
            //�Ƿ�Ϊ��
            if (!File.Exists(strPath))
                return null;

            //����·���ĵ�
            XPathDocument document = new XPathDocument(strPath);

            //�����α�
            XPathNavigator navigator = document.CreateNavigator();

            return navigator;
        }
        #endregion

        #region ��ȡĳ��Ԫ�ؽڵ�ֵ
        /// <summary>
        /// ��ȡĳ��Ԫ�ؽڵ�ֵ
        /// </summary>
        /// <param name="nodeIterator">������</param>
        /// <param name="strElementName">Ԫ������</param>
        /// <returns>���ؽڵ�ֵ</returns>
        public static string GetElementNodeValue(XPathNodeIterator nodeIterator, string strElementName)
        {
            //�Ƿ�Ϊ��
            if (nodeIterator == null || string.IsNullOrEmpty(strElementName))
                return string.Empty;

            //��ȡ�ض�Ԫ���α�
            XPathNavigator subNavigator = nodeIterator.Current.SelectSingleNode(strElementName);

            if (subNavigator == null)
                return string.Empty;

            return subNavigator.Value;
        }
        #endregion

        #region ͨ��������ʽ��ȡֵ����
        /// <summary>
        /// ͨ��������ʽ��ȡֵ����
        /// </summary>
        /// <param name="strRegex">������ʽ</param>
        /// <param name="strContent">����</param>
        /// <returns></returns>
        public static IList<string> GetValuesByRegex(string strRegex, string strContent)
        {
            IList<string> valueList = new List<string>();

            if (string.IsNullOrEmpty(strRegex) || string.IsNullOrEmpty(strContent))
                return valueList;

            MatchCollection matchCollection = null;

            Regex regex = new Regex(strRegex, RegexOptions.IgnoreCase);

            try
            {
                matchCollection = regex.Matches(strContent);
            }
            catch
            {

            }

            if (matchCollection == null || matchCollection.Count == 0)
                return valueList;

            foreach (Match match in matchCollection)
            {
                if(match != null)
                   valueList.Add(match.Value);
            }

            return valueList;
        }

        #endregion

        #region ͨ��������ʽ��ȡֵ
        /// <summary>
        /// ͨ��������ʽ��ȡֵ
        /// </summary>
        /// <param name="strRegex">������ʽ</param>
        /// <param name="strContent">����</param>
        /// <returns></returns>
        public static string GetValueByRegex(string strRegex, string strContent)
        {
            IList<string> valueList = GetValuesByRegex(strRegex, strContent);
            if (valueList.Count == 0)
                return string.Empty;

            return valueList[0];
        }

        #endregion
    }
}
