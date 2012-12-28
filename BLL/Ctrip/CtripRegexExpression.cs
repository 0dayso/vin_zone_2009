using System;
using System.Collections.Generic;
using System.Text;

using BLL.Common.Interface;
using BLL.Common.Operation;
using System.Xml.XPath;
namespace BLL.Ctrip
{
    public class CtripRegexExpression : IRegexExpression
    {
        private XPathNodeIterator nodeIterator;

        public CtripRegexExpression()
        {
            XPathNavigator navigator = RegexOperation.GetXPathNavigatorByPath(CommonOperation.GetConfigValueByKey(Constant.CCTRIPPATH));

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
        /// 获取Tbody元素中Data属性的值
        /// </summary>
        public string GetTbodyDataRegex()
        {
            return RegexOperation.GetElementNodeValue(nodeIterator, Constant.CTBODYDATA);
        }

        /// <summary>
        /// 获取城市对值
        /// </summary>
        public string GetCityRegex()
        {
            return RegexOperation.GetElementNodeValue(nodeIterator, Constant.CCITY);
        }

        /// <summary>
        /// 获取日期对值
        /// </summary>
        public string GetDateRegex()
        {
            return RegexOperation.GetElementNodeValue(nodeIterator, Constant.CDATE);
        }

        /// <summary>
        /// 获取餐食
        /// </summary>
        public string GetMealRegex()
        {
            return RegexOperation.GetElementNodeValue(nodeIterator, Constant.CMEAL);
        }

        /// <summary>
        /// 获取机建燃油表达式
        /// </summary>
        /// <returns></returns>
        public  string GetAirportFuelRegex()
        {
            return RegexOperation.GetElementNodeValue(nodeIterator, Constant.CAIRPORTFUEL); 
        }

        /// <summary>
        /// 获取飞行时间表达式
        /// </summary>
        /// <returns></returns>
        public string GetFlightIntervalRegex()
        {
            return RegexOperation.GetElementNodeValue(nodeIterator, Constant.CFLIGHTINTERVAL); 
        }

        /// <summary>
        /// 获取某个航班的所有舱位信息
        /// </summary>
        /// <returns></returns>
        public string GetAllCabinInfomation()
        {
            return RegexOperation.GetElementNodeValue(nodeIterator, Constant.CALLCABININFOMATION);
        }

        /// <summary>
        /// 获取某个航班的舱位
        /// </summary>
        /// <returns></returns>
        public string GetOtherCanbin()
        {
            return RegexOperation.GetElementNodeValue(nodeIterator, Constant.COTHERCANBIN);
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
