using System;
using System.Collections.Generic;
using System.Text;

using BLL.Common.Interface;
using Models;
using System.Threading;
using System.Windows.Forms;
using BLL.Common.Operation;
using System.IO;
namespace BLL.VeryTrip
{
    public class VeryTripRouteOperation : IRouteOperation
    {
        private HttpClient client;
        private DateTime? flightDate;

        public VeryTripRouteOperation()
        {
            client = new HttpClient();
        }

        /// <summary>
        /// 获取需解析的各公司内容
        /// </summary>
        /// <param name="strUrl"></param>
        public string GetHtmlContent(string strUrl)
        {
            return GetHtmlSource(strUrl);
        }

        /// <summary>
        /// 解析航线信息
        /// </summary>
        /// <param name="strContent">航线内容</param>
        /// <param name="regexInstance">正则实例</param>
        /// <returns></returns>
        public IList<RouteInformation> ParseHtmlCode(string strContent, IRegexExpression regexInstance)
        {
            if (string.IsNullOrEmpty(strContent) || regexInstance == null)
                return null;

            VeryTripRegexExpression veryTripRegex = (VeryTripRegexExpression)regexInstance;

            string strUrl = GetRealQuestUrl(strContent, veryTripRegex);

            return CombineAllRoute(GetHtmlSource(strUrl), veryTripRegex);
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
            string strUrl = CommonOperation.GetConfigValueByKey(Constant.CVERYWAITINGTRIPURL);

            SetFlightDate(departureTime);

            return string.Format(strUrl, strDeparture, strArrival, departureTime.Value.ToShortDateString());
        }

        /// <summary>
        /// 获取航线价格来源
        /// </summary>
        /// <returns></returns>
        public int GetSourceType()
        {
            return (int)CRM.Buzlogic.Common.EnumDef.ETicketPriceOrigin.非常旅行;
        }


        /// <summary>
        /// 下载Html源文件
        /// </summary>
        /// <param name="Url">请求地址</param>
        /// <returns></returns>
        private string DownHtmlSource(string Url)
        {
            //string strHtmlContent = string.Empty;

            //try
            //{
            //    System.Net.WebClient wc = new System.Net.WebClient();

            //    strHtmlContent = wc.DownloadString(Url);
            //}
            //catch
            //{
            //}

            //return strHtmlContent;

            string strMessage = string.Empty;

            if (client == null)
                client = new HttpClient();

            return client.GetSrc(Url, Constant.CCODESIGN, out strMessage); 
        }

        private RouteInformation GetRouteInformation(string strContent, VeryTripRegexExpression veryTripRegex)
        {
            if (string.IsNullOrEmpty(strContent))
                return null;

            string strFirstContent = RegexOperation.GetValueByRegex(veryTripRegex.GetSingleRowFirstRegex(), strContent);
            if (string.IsNullOrEmpty(strFirstContent))
                return null;

            RouteInformation routeInformation = new RouteInformation();

            routeInformation.OriginalAirport = RegexOperation.GetValueByRegex(veryTripRegex.GetDepartureCityRegex(), strFirstContent);
            routeInformation.DestinationAirport = RegexOperation.GetValueByRegex(veryTripRegex.GetArrivalCityRegex(), strFirstContent);

            if (GetFlightDate().HasValue)
            {
                string strStartTime = RegexOperation.GetValueByRegex(veryTripRegex.GetDepartureTimeRegex(), strFirstContent);
                string strEndTime = RegexOperation.GetValueByRegex(veryTripRegex.GetArrivalTimeRegex(), strFirstContent);
                
                routeInformation.DepartureTime = DateTime.Parse(GetFlightDate().Value.ToString("yyyy-MM-dd") + " " + strStartTime + ":00");
                routeInformation.ArriveTime = DateTime.Parse(GetFlightDate().Value.ToString("yyyy-MM-dd") + " " + strEndTime + ":00");

                routeInformation.AirDate = GetFlightDate().Value;
            }

            //routeInformation.Discount = double.Parse(strDiscount);

            //string strTicketPrice = RegexOperation.GetValueByRegex(veryTripRegex.GetTicketPriceRegex(), strTbodyData);
            //if (!string.IsNullOrEmpty(strTicketPrice))
            //    routeInformation.TicketPrice = double.Parse(strTicketPrice);

            //routeInformation.Meal = RegexOperation.GetValueByRegex(veryTripRegex.GetMealRegex(), strTbodyData);
            //routeInformation.AirLine = RegexOperation.GetValueByRegex(veryTripRegex.GetAirLineRegex(), strTbodyData);

            //routeInformation.ChangeRule = RegexOperation.GetValueByRegex(veryTripRegex.GetChangeRuleRegex(), strContent);
            //routeInformation.FlightInterval = RegexOperation.GetValueByRegex(veryTripRegex.GetFlightIntervalRegex(), strContent);
            routeInformation.FlightNO = RegexOperation.GetValueByRegex(veryTripRegex.GetFlightNORegex(), strFirstContent);
            routeInformation.AirLine = routeInformation.FlightNO.Substring(0, 2);
            routeInformation.FlightType = RegexOperation.GetValueByRegex(veryTripRegex.GetFlightTypeRegex(), strFirstContent);
            
            string strStops = RegexOperation.GetValueByRegex(veryTripRegex.GetStops(), strFirstContent);
            if (!string.IsNullOrEmpty(strStops))
                routeInformation.Stops = int.Parse(strStops);

            //routeInformation.Cabin = RegexOperation.GetValueByRegex(veryTripRegex.GetCabinRegex(), strContent);


            string strYprice = RegexOperation.GetValueByRegex(veryTripRegex.GetYpriceRegex(), strFirstContent);
            if (!string.IsNullOrEmpty(strYprice))
                routeInformation.Yprice = double.Parse(strYprice);

            //string strAirportFuelTax = RegexOperation.GetValueByRegex(veryTripRegex.GetAirportFuelRegex(), strContent);
            //if (!string.IsNullOrEmpty(strAirportFuelTax))
            //{
            //    string[] strDatas = strAirportFuelTax.Split('＋');

            //    if (!string.IsNullOrEmpty(strDatas[0]))
            //        routeInformation.AirportTax = double.Parse(strDatas[0]);

            //    if (!string.IsNullOrEmpty(strDatas[1]))
            //        routeInformation.FuelTax = double.Parse(strDatas[1]);
            //}

            return routeInformation;
        }

        private IList<Seat> GetSeatInformation(string strContent, VeryTripRegexExpression veryTripRegex)
        {
            IList<Seat> seatList = new List<Seat>();
            Seat seat;

            IList<string> secondContentList = RegexOperation.GetValuesByRegex(veryTripRegex.GetSingleRowSecondRegex(), strContent);
            if (secondContentList == null)
            {
                return seatList;
            }

            string strPrice = string.Empty;
            foreach (string strResult in secondContentList)
            {
                seat = new Seat();
                seat.Cabin = RegexOperation.GetValueByRegex(veryTripRegex.GetCabinRegex(), strResult);

                strPrice = RegexOperation.GetValueByRegex(veryTripRegex.GetTicketPriceRegex(), strResult);
                if (!string.IsNullOrEmpty(strPrice))
                    seat.Price = double.Parse(strPrice);

                seat.Count = 9;

                seat.SubCanbin = RegexOperation.GetValueByRegex(veryTripRegex.GetSubCanbin(), strResult);

                seatList.Add(seat);
            }

            return seatList;
        }

        private string GetHtmlSource(string strUrl)
        {
            WebBrowser webBrower = new WebBrowser();

            webBrower.ScriptErrorsSuppressed = true;

            webBrower.Navigate(Constant.CBLANKURL);
           
            webBrower.Document.Write(DownHtmlSource(strUrl));

            string strHtmlContent = webBrower.DocumentText;

            //using (StreamReader reader = new StreamReader(webBrower.DocumentStream, Encoding.GetEncoding(webBrower.Document.Encoding)))
            //{
            //    strHtmlContent = reader.ReadToEnd();
            //}

            webBrower.Dispose();

            return strHtmlContent;
        }

        private string GetPrefixUrl()
        {
            return CommonOperation.GetConfigValueByKey(Constant.CVERYTRIPURL);
        }

        private string GetRealQuestUrl(string strContent, VeryTripRegexExpression veryTripRegex)
        {
           return  GetPrefixUrl() + RegexOperation.GetValueByRegex(veryTripRegex.GetWaitingSuffixRegex(), strContent);
        }

        private IList<RouteInformation> CombineAllRoute(string strContent, VeryTripRegexExpression veryRegexInstance)
        {
            string strNomalRouteContent = GetNomalFragment(strContent, veryRegexInstance);

            List<RouteInformation> routeInformationList = GetCurrentPageRoute(strNomalRouteContent, veryRegexInstance);

            IList<string> pageUrlList = GetPageUrl(strContent, veryRegexInstance);
            foreach (string strPageUrl in pageUrlList)
            {
                strNomalRouteContent = GetNomalFragment(GetHtmlSource(strPageUrl), veryRegexInstance);

                routeInformationList.AddRange(GetCurrentPageRoute(strNomalRouteContent, veryRegexInstance));
            }

            return routeInformationList;
        }

        private List<RouteInformation> GetCurrentPageRoute(string strContent, VeryTripRegexExpression veryTripRegex)
        {

            IList<string> valueList = RegexOperation.GetValuesByRegex(veryTripRegex.GetSingleRowRegex(), strContent);

            List<RouteInformation> routeInformationList = new List<RouteInformation>();
            RouteInformation routeInformation;

            foreach (string strValue in valueList)
            {
                routeInformation = GetRouteInformation(strValue, veryTripRegex);

                if (routeInformation != null)
                {
                    routeInformation.SeatList = GetSeatInformation(strValue, veryTripRegex);

                    routeInformationList.Add(routeInformation);
                }
            }

            return routeInformationList;
        }

        private string GetNomalFragment(string strContent, VeryTripRegexExpression veryRegexInstance)
        {
           string strNomalContent = RegexOperation.GetValueByRegex(veryRegexInstance.GetNormalFragmentRegex(), strContent);
           
            if (!string.IsNullOrEmpty(strNomalContent))
               strNomalContent += Constant.CMATCHSIGN;

           return strNomalContent;
        }

        private IList<string> GetPageUrl(string strContent, VeryTripRegexExpression veryRegexInstance)
        {
            IList<string> pageUrlList = new List<string>();

            string strPages = RegexOperation.GetValueByRegex(veryRegexInstance.GetPageFragmentRegex(), strContent);
            if (string.IsNullOrEmpty(strPages))
                return pageUrlList;

            
            IList<string> suffixUrlList = RegexOperation.GetValuesByRegex(veryRegexInstance.GetPageLinkRegex(), strPages);
            foreach(string strSuffixUrl in suffixUrlList)
            {
                pageUrlList.Add(GetPrefixUrl() + strSuffixUrl);
            }

            return pageUrlList;
        }

        private void SetFlightDate(DateTime? flightDate)
        {
            this.flightDate = flightDate;
        }

        private DateTime? GetFlightDate()
        {
            return this.flightDate;
        }
    }
}
