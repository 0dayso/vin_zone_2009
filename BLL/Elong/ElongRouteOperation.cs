using System;
using System.Collections.Generic;
using System.Text;

using BLL.Common.Interface;
using Models;
using System.Threading;
using System.Windows.Forms;
using BLL.Common.Operation;
using System.IO;
namespace BLL.Elong
{
    public class ElongRouteOperation:IRouteOperation
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


            return webBrower.Document.GetElementById(Constant.EROOTELEMENT).InnerHtml ?? string.Empty;
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

            ElongRegexEpression etripRegex = (ElongRegexEpression)regexInstance;
            //StringReader sr = new StringReader(strContent);
            IList<string> valueList = RegexOperation.GetValuesByRegex(etripRegex.GetSingleRowRegex(), strContent);

            IList<RouteInformation> ElongrouteInformationList = new List<RouteInformation>();
            RouteInformation elongrouteInformation;
                foreach (string strValue in valueList)
                {
                    elongrouteInformation = GetElongRouteInformation(strValue.ToString(), etripRegex);

                    if (elongrouteInformation != null)
                        ElongrouteInformationList.Add(elongrouteInformation);
                }

            return ElongrouteInformationList;
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
            return string.Empty;
        }

        /// <summary>
        /// 获取航线价格来源
        /// </summary>
        /// <returns></returns>
        public int GetSourceType()
        {
            return 0;
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
               //System.Net.WebClient wc = new System.Net.WebClient();
              
               // strHtmlContent=wc.DownloadString(Url);





                //string PageUrl = UrlText.Text;
                System.Net.WebClient wc = new System.Net.WebClient();
                wc.Credentials = System.Net.CredentialCache.DefaultCredentials;
                Byte[] pageData = wc.DownloadData(Url);
                strHtmlContent = System.Text.Encoding.UTF8.GetString(pageData);

                Stream resStream = wc.OpenRead(Url);
                StreamReader sr = new StreamReader(resStream, System.Text.Encoding.UTF8);
                strHtmlContent = sr.ReadToEnd();
                resStream.Close();
            }
            catch
            {
            }

            return strHtmlContent;
        }

        /// <summary>
        /// 获取艺龙航线信息
        /// </summary>
        /// <param name="strContent"></param>
        /// <param name="etripRegex"></param>
        /// <returns></returns>
        private RouteInformation GetElongRouteInformation(string strContent, ElongRegexEpression etripRegex)
        {
            if (string.IsNullOrEmpty(strContent))
                return null;
            string strRule = RegexOperation.GetValueByRegex(etripRegex.GetRuleData(),strContent);
            string strTable = RegexOperation.GetValueByRegex(etripRegex.GetSingleRowRegex(), strContent);
            if (string.IsNullOrEmpty(strTable))
                return null;
            string airline = RegexOperation.GetValueByRegex(etripRegex.GetAirLineRegex(), strContent);
            if (string.IsNullOrEmpty(airline))
                return null;

            RouteInformation elongrouteInformation = new RouteInformation();
            IList<string> valueList;

            //出发城市、到大城市
            valueList = RegexOperation.GetValuesByRegex(etripRegex.GetCityRegex(), strContent);
            if (valueList != null && valueList.Count == 2)
            {
                elongrouteInformation.OriginalAirport = valueList[0];
                elongrouteInformation.DestinationAirport = valueList[1];
            }


            //出发城市
            //elongrouteInformation.OriginalAirport = RegexOperation.GetValuesByRegex(etripRegex.GetDepartureCityRegex(), strContent).ToString();
            //到达城市
           // elongrouteInformation.DestinationAirport = RegexOperation.GetValuesByRegex(etripRegex.GetArrivalCityRegex(), strContent).ToString();
            //航空公司
            elongrouteInformation.AirLine = RegexOperation.GetValueByRegex(etripRegex.GetAirLineRegex(), strTable).ToString();
            //航班号
            elongrouteInformation.FlightNO = RegexOperation.GetValueByRegex(etripRegex.GetFlightNORegex(), strContent).ToString();
            //舱位
            elongrouteInformation.Cabin = RegexOperation.GetValueByRegex(etripRegex.GetCabinRegex(), strContent).ToString();
            //机型
            elongrouteInformation.FlightType = RegexOperation.GetValueByRegex(etripRegex.GetFlightTypeRegex(), strRule).ToString();
            //退改
            elongrouteInformation.ChangeRule = RegexOperation.GetValueByRegex(etripRegex.GetChangeRuleRegex(), strContent).ToString();
            //折扣
            elongrouteInformation.Ediscount = RegexOperation.GetValueByRegex(etripRegex.GetDiscountRegex(), strContent).ToString();

            //税费
            string eAirportFuel = RegexOperation.GetValueByRegex(etripRegex.GetEAirportFuelRegex(), strTable).ToString();
            if (!string.IsNullOrEmpty(eAirportFuel))
            {
                elongrouteInformation.Eairportfuel = double.Parse(eAirportFuel);
            }
            //机建
            string airPort = RegexOperation.GetValueByRegex(etripRegex.GetAirportRegex(), strContent).ToString();
            if (!string.IsNullOrEmpty(airPort))
            {
                elongrouteInformation.AirportTax = double.Parse(airPort);
            }
            //燃油
            string fuel = RegexOperation.GetValueByRegex(etripRegex.GetFuelRegex(), strContent).ToString();
            if (!string.IsNullOrEmpty(fuel))
            {
                elongrouteInformation.FuelTax = double.Parse(fuel);
            }
            //票价
            string ticketPrice = RegexOperation.GetValueByRegex(etripRegex.GetTicketPriceRegex(), strContent).ToString();
            if (!string.IsNullOrEmpty(ticketPrice))
            {
                elongrouteInformation.TicketPrice = double.Parse(ticketPrice);
            }

            string yearmonthday = RegexOperation.GetValueByRegex(etripRegex.GetYearMonthDay(), strRule).ToString();

            //出发时间
            string departureTime = RegexOperation.GetValueByRegex(etripRegex.GetDepartureTimeRegex(), strContent).ToString();
            string yd = yearmonthday+" "+departureTime;
           
            if (!string.IsNullOrEmpty(departureTime))
            {
                elongrouteInformation.DepartureTime = DateTime.Parse(yd);
            }
            //到达时间
            string arrivalTime=RegexOperation.GetValueByRegex(etripRegex.GetArrivalTimeRegex(), strContent).ToString();
            string ya = yearmonthday+" "+arrivalTime;
            if (!string.IsNullOrEmpty(arrivalTime))
            {
                elongrouteInformation.ArriveTime = DateTime.Parse(ya);
            }
          
            return elongrouteInformation;

        }
    }
}
