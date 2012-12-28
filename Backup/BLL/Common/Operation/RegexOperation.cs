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
        #region 获取xml文档解析游标
        /// <summary>
        /// 获取xml文档解析游标
        /// </summary>
        /// <param name="strDocument">xml文档路径</param>
        /// <returns></returns>
        public static XPathNavigator GetXPathNavigatorByPath(string strPath)
        {
            //是否为空
            if (!File.Exists(strPath))
                return null;

            //创建路径文档
            XPathDocument document = new XPathDocument(strPath);

            //创建游标
            XPathNavigator navigator = document.CreateNavigator();

            return navigator;
        }
        #endregion

        #region 获取某个元素节点值
        /// <summary>
        /// 获取某个元素节点值
        /// </summary>
        /// <param name="nodeIterator">迭代器</param>
        /// <param name="strElementName">元素名称</param>
        /// <returns>返回节点值</returns>
        public static string GetElementNodeValue(XPathNodeIterator nodeIterator, string strElementName)
        {
            //是否为空
            if (nodeIterator == null || string.IsNullOrEmpty(strElementName))
                return string.Empty;

            //获取特定元素游标
            XPathNavigator subNavigator = nodeIterator.Current.SelectSingleNode(strElementName);

            if (subNavigator == null)
                return string.Empty;

            return subNavigator.Value;
        }
        #endregion

        #region 通过正则表达式获取值集合
        /// <summary>
        /// 通过正则表达式获取值集合
        /// </summary>
        /// <param name="strRegex">正则表达式</param>
        /// <param name="strContent">内容</param>
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

        #region 通过正则表达式获取值
        /// <summary>
        /// 通过正则表达式获取值
        /// </summary>
        /// <param name="strRegex">正则表达式</param>
        /// <param name="strContent">内容</param>
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
