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
        /// 获取某table数据的正则表达式
        /// </summary>
        public string GetSingleRowRegex()
        {
            return RegexOperation.GetElementNodeValue(nodeIterator, Constant.CSINGLEROW);
        }

        /// <summary>
        /// 获取航空公司的正则表达式
        /// </summary>
        public string GetAirLineRegex()
        {
            return RegexOperation.GetElementNodeValue(nodeIterator, Constant.CAIRLINE);
		}

        /// <summary>
        /// 获取机场建设费的正则表达式
        /// </summary>
        public string GetAirportRegex()
        {
            return RegexOperation.GetElementNodeValue(nodeIterator, Constant.CAIRPORT);
        }

        /// <summary>
        /// 获取到达城市的正则表达式
        /// </summary>
        public string GetArrivalCityRegex()
        {
            return RegexOperation.GetElementNodeValue(nodeIterator, Constant.CARRIVALCITY);
        }
        /// <summary>
        /// 获取城市对值
        /// </summary>
        public string GetCityRegex()
        {
            return RegexOperation.GetElementNodeValue(nodeIterator, Constant.CCITY);
        }

        /// <summary>
        /// 获取到达时间的正则表达式
        /// </summary>
        public string GetArrivalTimeRegex()
        {
            return RegexOperation.GetElementNodeValue(nodeIterator, Constant.CARRIVALTIME);
        }

        /// <summary>
        /// 获取舱位的正则表达式
        /// </summary>
        public string GetCabinRegex()
        {
            return RegexOperation.GetElementNodeValue(nodeIterator, Constant.CCABIN);
        }

        /// <summary>
        /// 获取退改签的正则表达式
        /// </summary>
        public string GetChangeRuleRegex()
        {
            return RegexOperation.GetElementNodeValue(nodeIterator, Constant.CCHANGERULE);
        }

        /// <summary>
        /// 获取出发城市的正则表达式
        /// </summary>
        public string GetDepartureCityRegex()
        {
            return RegexOperation.GetElementNodeValue(nodeIterator, Constant.CDEPARTURECITY);
        }

        /// <summary>
        /// 获取出发时间的正则表达式
        /// </summary>
        public string GetDepartureTimeRegex()
        {
            return RegexOperation.GetElementNodeValue(nodeIterator, Constant.CDEPARTURETIME);
        }

        /// <summary>
        /// 获取折扣的正则表达式
        /// </summary>
        public string GetDiscountRegex()
        {
            return RegexOperation.GetElementNodeValue(nodeIterator, Constant.CDISCOUNT);
        }

        /// <summary>
        /// 获取航班号的正则表达式
        /// </summary>
        public string GetFlightNORegex()
        {
            return RegexOperation.GetElementNodeValue(nodeIterator, Constant.CFLIGHTNO);
        }

        /// <summary>
        /// 获取机型的正则表达式
        /// </summary>
        public string GetFlightTypeRegex()
        {
            return RegexOperation.GetElementNodeValue(nodeIterator, Constant.CFLIGHTTYPE);
        }

        /// <summary>
        /// 获取燃油的正则表达式
        /// </summary>
        public string GetFuelRegex()
        {
            return RegexOperation.GetElementNodeValue(nodeIterator, Constant.CFUEL);
        }

        /// <summary>
        /// 获取票价的正则表达式
        /// </summary>
        public string GetTicketPriceRegex()
        {
            return RegexOperation.GetElementNodeValue(nodeIterator, Constant.CTICKETPRICE);
        }

        /// <summary>
        /// 获取税费表达式
        /// </summary>
        /// <returns></returns>
        public  string GetEAirportFuelRegex()
        {
            return RegexOperation.GetElementNodeValue(nodeIterator, Constant.EAIPORTFUEL); 
        }

        /// <summary>
        /// 获取Y舱价格的正则表达式
        /// </summary>
        public string GetYpriceRegex()
        {
            return RegexOperation.GetElementNodeValue(nodeIterator, Constant.CYPRICE);
        }

        /// <summary>
        /// 获取rule中的数据
        /// </summary>
        /// <returns></returns>
        public string GetRuleData()
        {
            return RegexOperation.GetElementNodeValue(nodeIterator,Constant.RULEDATA);
        }

        /// <summary>
        /// 获取rule中的年月日
        /// </summary>
        /// <returns></returns>
        public string GetYearMonthDay()
        {
            return RegexOperation.GetElementNodeValue(nodeIterator, Constant.YEARMONTHDAY);
        }
    }
}
