using System;
using System.Collections.Generic;
using System.Text;

using BLL.Common.Interface;
using Models;
using System.Threading;
using System.Windows.Forms;
using BLL.Common.Operation;

namespace BLL.Ctrip
{
    public class CtripRouteOperation : IRouteOperation
    {
       
       /// <summary>
        /// 获取需解析的各公司内容
        /// </summary>
        /// <param name="strUrl"></param>
        public string GetHtmlContent(string strUrl)
        {
            WebBrowser webBrower = new WebBrowser();

            webBrower.ScriptErrorsSuppressed = true;

            webBrower.Navigate(Constant.CBLANKURL);

            webBrower.Document.Write(DownHtmlSource(strUrl));

            string strHtmlContent = webBrower.DocumentText;
            if (webBrower.Document.GetElementById(Constant.CROOTELEMENT) != null)
                strHtmlContent = webBrower.Document.GetElementById(Constant.CROOTELEMENT).InnerHtml;

            webBrower.Dispose();

            return strHtmlContent;
        }

        /// <summary>
        /// 解析航线信息
        /// </summary>
        /// <param name="strContent">航线内容</param>
        /// <param name="regexInstance">正则实例</param>
        /// <returns></returns>
        public IList<RouteInformation> ParseHtmlCode(string strContent,IRegexExpression regexInstance)
        {
            if (string.IsNullOrEmpty(strContent) || regexInstance == null)
                return null;

            CtripRegexExpression ctripRegex = (CtripRegexExpression)regexInstance;

            IList<string> valueList = RegexOperation.GetValuesByRegex(ctripRegex.GetSingleRowRegex(), strContent);

            IList<RouteInformation> routeInformationList = new List<RouteInformation>();
            RouteInformation routeInformation;

            foreach (string strValue in valueList)
            {
                routeInformation = GetRouteInformation(strValue,ctripRegex);

                if (routeInformation != null)
                {
                    routeInformation.SeatList = GetSeatInformation(routeInformation,ctripRegex);

                    routeInformationList.Add(routeInformation);
                }
            }

            return routeInformationList;
        }

        /// <summary>
        /// 获取请求的URL
        /// </summary>
        /// <param name="strDeparture">出发地</param>
        /// <param name="strArrival">到达地</param>
        /// <param name="departureTime">出发时间</param>
        /// <returns></returns>
        public string GetRequestUrl(string strDeparture, string strArrival, DateTime? departureTime)
        {
            return GetUrl(strDeparture, strArrival, departureTime,string.Empty);
        }

        /// <summary>
        /// 获取航线价格来源
        /// </summary>
        /// <returns></returns>
        public int GetSourceType()
        {
            return (int)CRM.Buzlogic.Common.EnumDef.ETicketPriceOrigin.携程;
        }


        /// <summary>
        /// 下载Html源文件
        /// </summary>
        /// <param name="Url">请求地址</param>
        /// <returns></returns>
        private string DownHtmlSource(string Url)
        {
            string strHtmlContent = string.Empty;

            try
            {
                System.Net.WebClient wc = new System.Net.WebClient();

                strHtmlContent = wc.DownloadString(Url);
            }
            catch
            {
            }

            return strHtmlContent;
        }

        private RouteInformation GetRouteInformation(string strContent, CtripRegexExpression ctripRegex)
        {
            if (string.IsNullOrEmpty(strContent))
                return null;

            string strTbodyData = RegexOperation.GetValueByRegex(ctripRegex.GetTbodyDataRegex(), strContent);
            if (string.IsNullOrEmpty(strTbodyData))
                return null;

            RouteInformation routeInformation = new RouteInformation();
            IList<string> valueList;

            valueList = RegexOperation.GetValuesByRegex(ctripRegex.GetCityRegex(), strContent);
            if (valueList != null && valueList.Count == 2)
            {
                routeInformation.OriginalAirport = valueList[0];
                routeInformation.DestinationAirport = valueList[1];
            }

            valueList = RegexOperation.GetValuesByRegex(ctripRegex.GetDateRegex(), strContent);
            if (valueList != null && valueList.Count == 2)
            {
                if (!string.IsNullOrEmpty(valueList[0]))
                    routeInformation.DepartureTime = DateTime.Parse(valueList[0]);

                if (!string.IsNullOrEmpty(valueList[1]))
                    routeInformation.ArriveTime = DateTime.Parse(valueList[1]);
            }

            routeInformation.AirDate = routeInformation.DepartureTime;

            string strDiscount = RegexOperation.GetValueByRegex(ctripRegex.GetDiscountRegex(), strTbodyData);
            if (!string.IsNullOrEmpty(strDiscount))
                routeInformation.Discount = double.Parse(strDiscount);

            string strTicketPrice = RegexOperation.GetValueByRegex(ctripRegex.GetTicketPriceRegex(), strTbodyData);
            if (!string.IsNullOrEmpty(strTicketPrice))
                routeInformation.TicketPrice = double.Parse(strTicketPrice);

            routeInformation.Meal = RegexOperation.GetValueByRegex(ctripRegex.GetMealRegex(), strTbodyData);
            routeInformation.AirLine = RegexOperation.GetValueByRegex(ctripRegex.GetAirLineRegex(), strTbodyData);

            routeInformation.ChangeRule = RegexOperation.GetValueByRegex(ctripRegex.GetChangeRuleRegex(), strContent);
            routeInformation.FlightInterval = RegexOperation.GetValueByRegex(ctripRegex.GetFlightIntervalRegex(), strContent);
            routeInformation.FlightNO = RegexOperation.GetValueByRegex(ctripRegex.GetFlightNORegex(), strContent);
            routeInformation.FlightType = RegexOperation.GetValueByRegex(ctripRegex.GetFlightTypeRegex(), strContent);
            routeInformation.ChangeRule = RegexOperation.GetValueByRegex(ctripRegex.GetChangeRuleRegex(), strContent);
            routeInformation.Cabin = RegexOperation.GetValueByRegex(ctripRegex.GetCabinRegex(), strContent);

            
            string strYprice = RegexOperation.GetValueByRegex(ctripRegex.GetYpriceRegex(), strContent);
            if (!string.IsNullOrEmpty(strYprice))
                routeInformation.Yprice = double.Parse(strYprice);

            string strAirportFuelTax = RegexOperation.GetValueByRegex(ctripRegex.GetAirportFuelRegex(), strContent);
            if (!string.IsNullOrEmpty(strAirportFuelTax))
            {
                string[] strDatas = strAirportFuelTax.Split('＋');

                if (!string.IsNullOrEmpty(strDatas[0]))
                    routeInformation.AirportTax = double.Parse(strDatas[0]);

                if (!string.IsNullOrEmpty(strDatas[1]))
                    routeInformation.FuelTax = double.Parse(strDatas[1]);
            }

            return routeInformation;
        }

        private IList<Seat> GetSeatInformation(RouteInformation routeInformation, CtripRegexExpression ctripRegex)
        {
            IList<Seat> seatList = new List<Seat>();
            Seat seat;

            string strUrl = GetUrl(routeInformation.OriginalAirport, routeInformation.DestinationAirport, routeInformation.DepartureTime, routeInformation.FlightNO);
            
            IList<string> resultList = RegexOperation.GetValuesByRegex(string.Format(ctripRegex.GetAllCabinInfomation(), routeInformation.FlightNO.Trim()),DownHtmlSource(strUrl));
            if (resultList == null)
            {
                return seatList;
            }

            string strPrice = string.Empty;
            foreach (string strResult in resultList)
            {
                seat = new Seat();
                seat.Cabin = RegexOperation.GetValueByRegex(ctripRegex.GetOtherCanbin(),strResult);
               
                strPrice =  RegexOperation.GetValueByRegex(ctripRegex.GetOtherCanbinPrice(), strResult);
                if (!string.IsNullOrEmpty(strPrice))
                   seat.Price = double.Parse(strPrice);

                seat.Count = 9;

                seatList.Add(seat);
            }

            return seatList;
        }


        private string GetUrl(string strDeparture, string strArrival, DateTime? departureTime,string strFlightNO)
        {
            if (strDeparture.ToUpper().Equals(Constant.CPEK))
                strDeparture = Constant.CBJS;

            if (strDeparture.ToUpper().Equals(Constant.CNAY))
                strDeparture = Constant.CBJS + "," + Constant.CNAY;

            if (strDeparture.ToUpper().Equals(Constant.CPVG))
                strDeparture = Constant.CSHA + "," + Constant.CPVG;

            if (strArrival.ToUpper().Equals(Constant.CPEK))
                strArrival = Constant.CBJS;

            if (strArrival.ToUpper().Equals(Constant.CNAY))
                strArrival = Constant.CBJS + "," + Constant.CNAY;

            if (strArrival.ToUpper().Equals(Constant.CPVG))
                strArrival = Constant.CSHA + "," + Constant.CPVG;

            string strUrl = string.Empty;
            if (string.IsNullOrEmpty(strFlightNO))
                strUrl = CommonOperation.GetConfigValueByKey(Constant.CCTRIPURL);
            else
                strUrl = CommonOperation.GetConfigValueByKey(Constant.CCTRIPSUBURL);

            return string.Format(strUrl, strDeparture, strArrival, departureTime.Value.ToShortDateString(),strFlightNO);
        }
    }
}
