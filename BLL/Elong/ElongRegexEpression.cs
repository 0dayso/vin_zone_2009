using System;
using System.Collections.Generic;
using System.Text;
using BLL.Common.Interface;
using BLL.Common.Operation;
using System.Xml.XPath;

namespace BLL.Elong
{
    public class ElongRegexEpression : IRegexExpression
    {
           private XPathNodeIterator nodeIterator;

        public ElongRegexEpression()
        {
            XPathNavigator navigator = RegexOperation.GetXPathNavigatorByPath(CommonOperation.GetConfigValueByKey(Constant.CCTRIPPATH));

            nodeIterator = navigator.Select(Constant.CREGEXEXPRESSION);

            nodeIterator.MoveNext();
        }

        /// <summary>
        /// ��ȡĳtable���ݵ�������ʽ
        /// </summary>
        public string GetSingleRowRegex()
        {
            return RegexOperation.GetElementNodeValue(nodeIterator, Constant.CSINGLEROW);
        }

        /// <summary>
        /// ��ȡ���չ�˾��������ʽ
        /// </summary>
        public string GetAirLineRegex()
        {
            return RegexOperation.GetElementNodeValue(nodeIterator, Constant.CAIRLINE);
		}

        /// <summary>
        /// ��ȡ��������ѵ�������ʽ
        /// </summary>
        public string GetAirportRegex()
        {
            return RegexOperation.GetElementNodeValue(nodeIterator, Constant.CAIRPORT);
        }

        /// <summary>
        /// ��ȡ������е�������ʽ
        /// </summary>
        public string GetArrivalCityRegex()
        {
            return RegexOperation.GetElementNodeValue(nodeIterator, Constant.CARRIVALCITY);
        }
        /// <summary>
        /// ��ȡ���ж�ֵ
        /// </summary>
        public string GetCityRegex()
        {
            return RegexOperation.GetElementNodeValue(nodeIterator, Constant.CCITY);
        }

        /// <summary>
        /// ��ȡ����ʱ���������ʽ
        /// </summary>
        public string GetArrivalTimeRegex()
        {
            return RegexOperation.GetElementNodeValue(nodeIterator, Constant.CARRIVALTIME);
        }

        /// <summary>
        /// ��ȡ��λ��������ʽ
        /// </summary>
        public string GetCabinRegex()
        {
            return RegexOperation.GetElementNodeValue(nodeIterator, Constant.CCABIN);
        }

        /// <summary>
        /// ��ȡ�˸�ǩ��������ʽ
        /// </summary>
        public string GetChangeRuleRegex()
        {
            return RegexOperation.GetElementNodeValue(nodeIterator, Constant.CCHANGERULE);
        }

        /// <summary>
        /// ��ȡ�������е�������ʽ
        /// </summary>
        public string GetDepartureCityRegex()
        {
            return RegexOperation.GetElementNodeValue(nodeIterator, Constant.CDEPARTURECITY);
        }

        /// <summary>
        /// ��ȡ����ʱ���������ʽ
        /// </summary>
        public string GetDepartureTimeRegex()
        {
            return RegexOperation.GetElementNodeValue(nodeIterator, Constant.CDEPARTURETIME);
        }

        /// <summary>
        /// ��ȡ�ۿ۵�������ʽ
        /// </summary>
        public string GetDiscountRegex()
        {
            return RegexOperation.GetElementNodeValue(nodeIterator, Constant.CDISCOUNT);
        }

        /// <summary>
        /// ��ȡ����ŵ�������ʽ
        /// </summary>
        public string GetFlightNORegex()
        {
            return RegexOperation.GetElementNodeValue(nodeIterator, Constant.CFLIGHTNO);
        }

        /// <summary>
        /// ��ȡ���͵�������ʽ
        /// </summary>
        public string GetFlightTypeRegex()
        {
            return RegexOperation.GetElementNodeValue(nodeIterator, Constant.CFLIGHTTYPE);
        }

        /// <summary>
        /// ��ȡȼ�͵�������ʽ
        /// </summary>
        public string GetFuelRegex()
        {
            return RegexOperation.GetElementNodeValue(nodeIterator, Constant.CFUEL);
        }

        /// <summary>
        /// ��ȡƱ�۵�������ʽ
        /// </summary>
        public string GetTicketPriceRegex()
        {
            return RegexOperation.GetElementNodeValue(nodeIterator, Constant.CTICKETPRICE);
        }

        /// <summary>
        /// ��ȡ˰�ѱ��ʽ
        /// </summary>
        /// <returns></returns>
        public  string GetEAirportFuelRegex()
        {
            return RegexOperation.GetElementNodeValue(nodeIterator, Constant.EAIPORTFUEL); 
        }

        /// <summary>
        /// ��ȡY�ռ۸��������ʽ
        /// </summary>
        public string GetYpriceRegex()
        {
            return RegexOperation.GetElementNodeValue(nodeIterator, Constant.CYPRICE);
        }

        /// <summary>
        /// ��ȡrule�е�����
        /// </summary>
        /// <returns></returns>
        public string GetRuleData()
        {
            return RegexOperation.GetElementNodeValue(nodeIterator,Constant.RULEDATA);
        }

        /// <summary>
        /// ��ȡrule�е�������
        /// </summary>
        /// <returns></returns>
        public string GetYearMonthDay()
        {
            return RegexOperation.GetElementNodeValue(nodeIterator, Constant.YEARMONTHDAY);
        }
    }
}
