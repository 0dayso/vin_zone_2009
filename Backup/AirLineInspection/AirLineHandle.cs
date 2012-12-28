using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using BLL.Ctrip;
using Models;
using System.Configuration;
using System.Threading;
using CRM.Buzlogic.Common;

using BLL.Common.Operation;
using BLL.Common.Interface;
using System.IO;
namespace AirLineInspection
{
    public class AirLineHandle
    {
        //线程是否启用
        private static readonly string AIRLINETHREADING = ConfigurationManager.AppSettings["AIRLINETHREADING"].ToString();
       
        private static readonly int TIMEINTERVAL = string.IsNullOrEmpty(ConfigurationManager.AppSettings["TIMEINTERVAL"].ToString())
                                                ? 1000 : Convert.ToInt32(ConfigurationManager.AppSettings["TIMEINTERVAL"]);
       
        private static FlightLowestPrice lowestPrice;

        /// <summary>
        /// 处理最低度价航线
        /// </summary>
        public static void HandleAirLine()
        {
            while (true)
            {
                //线程是否启动
                if ("YES" == AIRLINETHREADING)
                {
                    try
                    {
                        lowestPrice = GetFlightLowestPrice();

                        if (!IsExistLowestPrice(lowestPrice.LowestPriceId))
                        {
                            CreateQuest(lowestPrice);
                        }
                    }
                    catch(Exception ex)
                    {
                        //WriteLog(ex,lowestPrice);
                    }
                }

                Thread.Sleep(TIMEINTERVAL);
            }
        }



        /// <summary>
        /// 获取最低价航线
        /// </summary>
        /// <returns></returns>
        private static FlightLowestPrice GetFlightLowestPrice()
        {
            AirLineOperation operation = new AirLineOperation();

            return operation.QueryLowestPrice();
        }

        /// <summary>
        /// 系统最低价是否存在
        /// </summary>
        /// <param name="lowestPriceId"></param>
        /// <returns></returns>
        private static  bool IsExistLowestPrice(int lowestPriceId)
        {
            AirLineOperation operation = new AirLineOperation();

            return operation.IsExistLowestPrice(lowestPriceId);
        }

        /// <summary>
        /// 获取解析各公司的地址
        /// </summary>
        /// <returns></returns>
        private static string GetWebAddress()
        {
            FlightLowestPrice CElowerstPrice = new FlightLowestPrice();
            string strDeparture = lowestPrice.Departure;
            if (strDeparture.ToUpper().Equals("PEK") || strDeparture.ToUpper().Equals("NAY"))
                strDeparture = "BJS";

            string strArrival = lowestPrice.Arrival;
            if (strArrival.ToUpper().Equals("PEK") || strArrival.ToUpper().Equals("NAY"))
                strArrival = "BJS";
            if (Constant.CCTRIPPATH == "CTRIPPATH")
            {
                string URL = "http://flights.ctrip.com/Domestic/ShowFareFirst.aspx?DCity1=" + strDeparture + "&ACity1=" + strArrival + "&DCityName1=&ACityName1=&DDate1=" + lowestPrice.DepartureTime.Value.ToShortDateString() + "&ClassType=&PassengerQuantity=1&SendTicketCity=&Airline=&PassengerType=ADU&FlightSearchType=S&";
                return URL;
            }
            else if (Constant.CCTRIPPATH == "ETRIPPATH")
            {
                string url = "http://flight.elong.com/cn_list_" + strDeparture + "_" + strArrival + "_" +
                CElowerstPrice.DepartureTime.Value.ToString("yyyy-MM-dd") + "_Y.html";
                return url;
            }
        }

        private static void CreateQuest(FlightLowestPrice lowestPrice)
        {
            if (lowestPrice == null)
                return;

            if (!lowestPrice.DepartureTime.HasValue)
                return;

            #region 原来
            //string strDeparture = lowestPrice.Departure;
            //if (strDeparture.ToUpper().Equals("PEK") || strDeparture.ToUpper().Equals("NAY"))
            //    strDeparture = "BJS";

            //string strArrival = lowestPrice.Arrival;
            //if (strArrival.ToUpper().Equals("PEK") || strArrival.ToUpper().Equals("NAY"))
            //    strArrival = "BJS";

            //string URL = "http://flights.ctrip.com/Domestic/ShowFareFirst.aspx?DCity1=" + strDeparture + "&ACity1=" + strArrival + "&DCityName1=&ACityName1=&DDate1=" + lowestPrice.DepartureTime.Value.ToShortDateString() + "&ClassType=&PassengerQuantity=1&SendTicketCity=&Airline=&PassengerType=ADU&FlightSearchType=S&";
            #endregion

            #region 张
            /*
            if (Constant.CCTRIPPATH == "CTRIPPATH")
            {
                CtripInvoke(GetWebAddress());
            }
            else if (Constant.CCTRIPPATH == "ETRIPPATH")
            {
                ElongInvoke(GetWebAddress());
            }
             * */
            #endregion

            #region 现在
            RequestInvoke(lowestPrice.Departure, lowestPrice.Arrival, lowestPrice.DepartureTime);
            #endregion
        }

        #region 请求
        /// <summary>
        /// 执行相应请求
        /// </summary>
        /// <param name="strDeparture">出发地</param>
        /// <param name="strArrival">到达地</param>
        /// <param name="departureTime">出发时间</param>
        private static void RequestInvoke(string strDeparture, string strArrival, DateTime? departureTime)
        {
            FactoryContribution factoryContribution = new FactoryContribution();

            IRouteFactory routeFactory = factoryContribution.CreateFactory();
            if (routeFactory == null)
                return;

            IRouteOperation routeOperation = routeFactory.CreateRouteInstance();

            IRegexExpression regexExpression = routeFactory.CreateRouteRegex();

            string strUrl = routeOperation.GetRequestUrl(strDeparture, strArrival, departureTime);
            if (string.IsNullOrEmpty(strUrl))
                return;

            string strContent = routeOperation.GetHtmlContent(strUrl);

            IList<RouteInformation> routeInformationList = routeOperation.ParseHtmlCode(strContent, regexExpression);

            CompareCtripLowestPrice(routeInformationList,routeOperation.GetSourceType());
        }

        private static void CompareCtripLowestPrice(IList<RouteInformation> routeInformationList, int sourceType)
        {
            if (lowestPrice == null || routeInformationList == null)
                return;

            List<RouteInformation> routeInformationListConverted = (List<RouteInformation>)routeInformationList;
            RouteInformation routeInformationFinded = routeInformationListConverted.Find(delegate(RouteInformation routeInformation)
            {
                return (routeInformation.FlightNO.Trim() == lowestPrice.Flight.Trim());
            });

            AirLineOperation operation = new AirLineOperation();
            int otherPriceId = 0;
            int autoCompare = (int)EnumDef.EFlightAutoCompare.有差异;
            if (routeInformationFinded != null)
            {
                FlightOtherPrice otherPrice = CreateCtripOtherPrice(routeInformationFinded);
                otherPriceId = operation.AddOtherPrice(otherPrice);
                if (otherPriceId > 0)
                {
                    otherPrice.Otherpriceid = otherPriceId;

                    if (routeInformationFinded.TicketPrice.Equals(lowestPrice.LowestPrice))
                    {
                        autoCompare = (int)EnumDef.EFlightAutoCompare.无差异;
                    }
                }
            }

            FlightPriceCompare priceCompare = new FlightPriceCompare();
            priceCompare.LowestPriceId = lowestPrice.LowestPriceId;
            priceCompare.Otherpriceid = otherPriceId;
            priceCompare.Autoresult = autoCompare;
            priceCompare.Handresult = (int)EnumDef.EFlightCompare.未核实;
            operation.AddPriceCompare(priceCompare);
        }

        private static FlightOtherPrice CreateCtripOtherPrice(RouteInformation routeInformation, int sourceType)
        {
            FlightOtherPrice otherPrice = new FlightOtherPrice();

            otherPrice.Airliner = routeInformation.AirLine;
            otherPrice.Arrival = routeInformation.DestinationAirport;
            otherPrice.Cabin = routeInformation.Cabin;
            otherPrice.CreateDate = DateTime.Now;
            otherPrice.Departure = routeInformation.OriginalAirport;
            otherPrice.Flight = routeInformation.FlightNO;
            otherPrice.LowestPrice = routeInformation.TicketPrice;
            otherPrice.Sourcetype = sourceType;

            return otherPrice;
        }
        #endregion

        #region 携程
        private static void CtripInvoke(string strUrl)
        {
            FactoryContribution factoryContribution = new FactoryContribution();

            IRouteFactory routeFactory = factoryContribution.CreateFactory();
            if (routeFactory == null)
                return;

            IRouteOperation routeOperation = routeFactory.CreateRouteInstance();

            IRegexExpression regexExpression = routeFactory.CreateRouteRegex();

            string strContent = routeOperation.GetHtmlContent(strUrl);

            IList<RouteInformation> routeInformationList = routeOperation.ParseHtmlCode(strContent, regexExpression);

            CompareCtripLowestPrice(routeInformationList);
        }

        private static void CompareCtripLowestPrice(IList<RouteInformation> routeInformationList)
        {
            if (lowestPrice == null || routeInformationList == null)
                return;

            List<RouteInformation> routeInformationListConverted = (List<RouteInformation>)routeInformationList;
            RouteInformation routeInformationFinded = routeInformationListConverted.Find(delegate(RouteInformation routeInformation)
            {
                return (routeInformation.FlightNO.Trim() == lowestPrice.Flight.Trim());
            });

            AirLineOperation operation = new AirLineOperation();
            int otherPriceId = 0;
            int autoCompare = (int)EnumDef.EFlightAutoCompare.有差异;
            if (routeInformationFinded != null)
            {
                FlightOtherPrice otherPrice = CreateCtripOtherPrice(routeInformationFinded);
                otherPriceId = operation.AddOtherPrice(otherPrice);
                if (otherPriceId > 0)
                {
                    otherPrice.Otherpriceid = otherPriceId;

                    if (routeInformationFinded.TicketPrice.Equals(lowestPrice.LowestPrice))
                    {
                        autoCompare = (int)EnumDef.EFlightAutoCompare.无差异;
                    }
                }
            }

            FlightPriceCompare priceCompare = new FlightPriceCompare();
            priceCompare.LowestPriceId = lowestPrice.LowestPriceId;
            priceCompare.Otherpriceid = otherPriceId;
            priceCompare.Autoresult = autoCompare;
            priceCompare.Handresult = (int)EnumDef.EFlightCompare.未核实;
            operation.AddPriceCompare(priceCompare);
        }

        private static FlightOtherPrice CreateCtripOtherPrice(RouteInformation routeInformation)
        {
            FlightOtherPrice otherPrice = new FlightOtherPrice();

            otherPrice.Airliner = routeInformation.AirLine;
            otherPrice.Arrival = routeInformation.DestinationAirport;
            otherPrice.Cabin = routeInformation.Cabin;
            otherPrice.CreateDate = DateTime.Now;
            otherPrice.Departure = routeInformation.OriginalAirport;
            otherPrice.Flight = routeInformation.FlightNO;
            otherPrice.LowestPrice = routeInformation.TicketPrice;
            otherPrice.Sourcetype = (int)EnumDef.ETicketPriceOrigin.携程;

            return otherPrice;
        }
        #endregion





        #region 艺龙
        private static void ElongInvoke(string strUrl)
        {
            FactoryContribution factoryContribution = new FactoryContribution();

            IRouteFactory routeFactory = factoryContribution.CreateFactory();
            if (routeFactory == null)
                return;

            IRouteOperation routeOperation = routeFactory.CreateRouteInstance();

            IRegexExpression regexExpression = routeFactory.CreateRouteRegex();

            string strContent = routeOperation.GetHtmlContent(strUrl);

            IList<RouteInformation> routeInformationList = routeOperation.ParseHtmlCode(strContent, regexExpression);

            CompareElongLowestPrice(routeInformationList);
        }

        private static void CompareElongLowestPrice(IList<RouteInformation> routeInformationList)
        {
            if (lowestPrice == null || routeInformationList == null)
                return;

            List<RouteInformation> routeInformationListConverted = (List<RouteInformation>)routeInformationList;
            RouteInformation routeInformationFinded = routeInformationListConverted.Find(delegate(RouteInformation routeInformation)
            {
                return (routeInformation.FlightNO.Trim() == lowestPrice.Flight.Trim());
            });

            AirLineOperation operation = new AirLineOperation();
            int otherPriceId = 0;
            int autoCompare = (int)EnumDef.EFlightAutoCompare.有差异;
            if (routeInformationFinded != null)
            {
                FlightOtherPrice otherPrice = CreateElongOtherPrice(routeInformationFinded);
                otherPriceId = operation.AddOtherPrice(otherPrice);
                if (otherPriceId > 0)
                {
                    otherPrice.Otherpriceid = otherPriceId;

                    if (routeInformationFinded.TicketPrice.Equals(lowestPrice.LowestPrice))
                    {
                        autoCompare = (int)EnumDef.EFlightAutoCompare.无差异;
                    }
                }
            }

            FlightPriceCompare priceCompare = new FlightPriceCompare();
            priceCompare.LowestPriceId = lowestPrice.LowestPriceId;
            priceCompare.Otherpriceid = otherPriceId;
            priceCompare.Autoresult = autoCompare;
            priceCompare.Handresult = (int)EnumDef.EFlightCompare.未核实;
            operation.AddPriceCompare(priceCompare);
        }

        private static FlightOtherPrice CreateElongOtherPrice(RouteInformation routeInformation)
        {
            FlightOtherPrice otherPrice = new FlightOtherPrice();

            otherPrice.Airliner = routeInformation.AirLine;
            otherPrice.Arrival = routeInformation.DestinationAirport;
            otherPrice.Cabin = routeInformation.Cabin;
            otherPrice.CreateDate = DateTime.Now;
            otherPrice.Departure = routeInformation.OriginalAirport;
            otherPrice.Flight = routeInformation.FlightNO;
            otherPrice.LowestPrice = routeInformation.TicketPrice;
            otherPrice.Sourcetype = (int)EnumDef.ETicketPriceOrigin.艺龙;

            return otherPrice;
        }
        #endregion




        #region 异常记录
        /// <summary>
        /// 异常记录
        /// </summary>
        /// <param name="ex">异常</param>
        /// <param name="lowestPrice">最低价实例</param>
        private static void WriteLog(Exception ex, FlightLowestPrice lowestPrice)
        {
            string strDirectory = CommonOperation.GetConfigValueByKey(Constant.CDIRECTORY);

            if (!Directory.Exists(strDirectory))
                Directory.CreateDirectory(strDirectory);

            if (!strDirectory.EndsWith("/"))
                strDirectory += "/";

            string strPath = strDirectory + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";

            StringBuilder sbContent = new StringBuilder();
            if (lowestPrice != null)
                sbContent.AppendLine("Id:" + lowestPrice.LowestPriceId
                                        + " 出发地:" + lowestPrice.Departure
                                        + " 到达地:" + lowestPrice.Arrival
                                        + " 起飞时间:" + lowestPrice.DepartureTime.Value.ToString());

            sbContent.AppendLine("源:" + ex.Source + " 消息:" + ex.Message + " 栈:" + ex.StackTrace);

            using (FileStream fileStream = new FileStream(strPath, FileMode.OpenOrCreate, FileAccess.Write))
            {
                using (StreamWriter streamWriter = new StreamWriter(fileStream))
                {
                    streamWriter.Write(sbContent.ToString());

                    streamWriter.Close();
                }

                fileStream.Close();
            }

        }
        #endregion

        #region 测试
        public string RouteTest(string strDeparture, string strArrival, DateTime? departureTime, string strFlightNo)
        {
            FactoryContribution factoryContribution = new FactoryContribution();

            IRouteFactory routeFactory = factoryContribution.CreateFactory();
            if (routeFactory == null)
                return string.Empty;

            IRouteOperation routeOperation = routeFactory.CreateRouteInstance();

            IRegexExpression regexExpression = routeFactory.CreateRouteRegex();

            if (strDeparture.ToUpper().Equals("PEK") || strDeparture.ToUpper().Equals("NAY"))
                strDeparture = "BJS";

            if (strArrival.ToUpper().Equals("PEK") || strArrival.ToUpper().Equals("NAY"))
                strArrival = "BJS";

            string strUrl = CommonOperation.GetConfigValueByKey(Constant.CCTRIPPATH);
            if(!string.IsNullOrEmpty(strFlightNo))
                strUrl = CommonOperation.GetConfigValueByKey(Constant.CCTRIPSUBURL);

            return routeOperation.GetHtmlContent(string.Format(strUrl,strDeparture,strArrival,departureTime.Value.ToString("yyyy-MM-dd"),strFlightNo));
        }
        #endregion

    }
}
