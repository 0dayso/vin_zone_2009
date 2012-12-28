using System;
using System.Collections.Generic;
using System.Text;

using BLL.Common.Interface;
using BLL.Common.Operation;
using System.Xml.XPath;

namespace BLL.VeryTrip
{
    public class VeryTripRegexExpression : IRegexExpression
    {
        private XPathNodeIterator nodeIterator;

        public VeryTripRegexExpression()
        {
            XPathNavigator navigator = RegexOperation.GetXPathNavigatorByPath(CommonOperation.GetConfigValueByKey(Constant.CVERYTRIPPATH));

            nodeIterator = navigator.Select(Constant.CREGEXEXPRESSION);

            nodeIterator.MoveNext();
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
        /// 获取某行数据的正则表达式
        /// </summary>
        public string GetSingleRowRegex()
        {
            return RegexOperation.GetElementNodeValue(nodeIterator, Constant.CSINGLEROW);
        }

        /// <summary>
        /// 获取票价的正则表达式
        /// </summary>
        public string GetTicketPriceRegex()
        {
            return RegexOperation.GetElementNodeValue(nodeIterator, Constant.CTICKETPRICE);
        }

        /// <summary>
        /// 获取Y舱价格的正则表达式
        /// </summary>
        public string GetYpriceRegex()
        {
            return RegexOperation.GetElementNodeValue(nodeIterator, Constant.CYPRICE);
        }

        /// <summary>
        /// 获取正常航班中的html代码段
        /// </summary>
        public string GetNormalFragmentRegex()
        {
            return RegexOperation.GetElementNodeValue(nodeIterator, Constant.CNORMALFRAGMENT);
        }

        /// <summary>
        /// 获取某个航班中第一部分信息
        /// </summary>
        public string GetSingleRowFirstRegex()
        {
            return RegexOperation.GetElementNodeValue(nodeIterator, Constant.CSINGLEROWFIRST);
        }

        /// <summary>
        /// 获取某个航班第二部分信息
        /// </summary>
        public string GetSingleRowSecondRegex()
        {
            return RegexOperation.GetElementNodeValue(nodeIterator, Constant.CSINGLEROWSECOND);
        }

        /// <summary>
        /// 获取分页html代码段
        /// </summary>
        public string GetPageFragmentRegex()
        {
            return RegexOperation.GetElementNodeValue(nodeIterator, Constant.CPAGEFRAGMENT);
        }

        /// <summary>
        /// 获取页面分页链接
        /// </summary>
        /// <returns></returns>
        public string GetPageLinkRegex()
        {
            return RegexOperation.GetElementNodeValue(nodeIterator, Constant.CPAGELINK);
        }

       

        /// <summary>
        /// 获取等待后缀
        /// </summary>
        /// <returns></returns>
        public string GetWaitingSuffixRegex()
        {
            return RegexOperation.GetElementNodeValue(nodeIterator, Constant.CWAITINGSUFFIX);
        }
        

        /// <summary>
        /// 获取某个航班经停信息
        /// </summary>
        /// <returns></returns>
        public string GetStops()
        {
            return RegexOperation.GetElementNodeValue(nodeIterator, Constant.CSTOPS);
        }

        /// <summary>
        /// 获取某个航班的子舱位
        /// </summary>
        /// <returns></returns>
        public string GetSubCanbin()
        {
            return RegexOperation.GetElementNodeValue(nodeIterator, Constant.CSUBCANBIN);
        }

        /// <summary>
        /// 获取某个航班舱位的价格
        /// </summary>
        /// <returns></returns>
        public string GetOtherCanbinPrice()
        {
            return RegexOperation.GetElementNodeValue(nodeIterator, Constant.COTHERCANBINPRICE);
        }
    }
}
