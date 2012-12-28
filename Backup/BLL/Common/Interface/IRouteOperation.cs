using System;
using System.Collections.Generic;
using System.Text;

using Models;

namespace BLL.Common.Interface
{
    public interface IRouteOperation
    {
        /// <summary>
        /// 获取需解析的各公司内容
        /// </summary>
        /// <param name="strUrl"></param>
        string GetHtmlContent(string strUrl);

        /// <summary>
        /// 解析航线信息
        /// </summary>
        /// <param name="strContent">航线内容</param>
        /// <param name="regexInstance">正则实例</param>
        /// <returns></returns>
        IList<RouteInformation> ParseHtmlCode(string strContent,IRegexExpression regexInstance);

        /// <summary>
        /// 获取请求的URL
        /// </summary>
        /// <param name="strDeparture">出发地</param>
        /// <param name="strArrival">到达地</param>
        /// <param name="departureTime">出发时间</param>
        /// <returns></returns>
        string GetRequestUrl(string strDeparture, string strArrival, DateTime? departureTime);

        /// <summary>
        /// 获取航线价格来源
        /// </summary>
        /// <returns></returns>
        int GetSourceType();
    }
}
