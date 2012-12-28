using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Common.Interface
{
    /// <summary>
    /// 获取各公司航线最低价的正则表达式接口
    /// </summary>
    public interface IRegexExpression
    {

        /// <summary>
        /// 获取某行数据的正则表达式
        /// </summary>
        string GetSingleRowRegex();

        /// <summary>
        /// 获取航空公司的正则表达式
        /// </summary>
        string GetAirLineRegex();

        /// <summary>
        /// 获取机场建设费的正则表达式
        /// </summary>
        string GetAirportRegex();

        /// <summary>
        /// 获取到达城市的正则表达式
        /// </summary>
        string GetArrivalCityRegex();

        /// <summary>
        /// 获取到达时间的正则表达式
        /// </summary>
        string GetArrivalTimeRegex();

        /// <summary>
        /// 获取舱位的正则表达式
        /// </summary>
        string GetCabinRegex();

        /// <summary>
        /// 获取退改签的正则表达式
        /// </summary>
        string GetChangeRuleRegex();

        /// <summary>
        /// 获取出发城市的正则表达式
        /// </summary>
        string GetDepartureCityRegex();

        /// <summary>
        /// 获取出发时间的正则表达式
        /// </summary>
        string GetDepartureTimeRegex();

        /// <summary>
        /// 获取折扣的正则表达式
        /// </summary>
        string GetDiscountRegex();

        /// <summary>
        /// 获取航班号的正则表达式
        /// </summary>
        string GetFlightNORegex();

        /// <summary>
        /// 获取机型的正则表达式
        /// </summary>
        string GetFlightTypeRegex();

        /// <summary>
        /// 获取燃油的正则表达式
        /// </summary>
        string GetFuelRegex();

        /// <summary>
        /// 获取Y舱价格的正则表达式
        /// </summary>
        string GetYpriceRegex();

        /// <summary>
        /// 获取票价的正则表达式
        /// </summary>
        string GetTicketPriceRegex();

    }
}
