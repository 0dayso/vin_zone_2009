using System;
using System.Collections.Generic;
using System.Text;

using System.Net;
using System.Threading;
using Models;
using BLL.Common.Operation;
using BLL.Common.Interface;
using System.IO;
namespace AirLineInspection
{
    public class HttpListenerOperation
    {
        private HttpListener httpListener;
        private Thread thread;
        private bool isListening;
        private IList<string> prefixList;

        /// <summary>
        /// Prefix前缀
        /// </summary>
        public IList<string> PrefixList
        {
            get
            {
                return prefixList;
            }

            set
            {
                prefixList = value;
            }
        }

        private void AddPrefixs()
        {
            if(prefixList == null || prefixList.Count == 0)
               return;

            if (httpListener == null)
                httpListener = new HttpListener();

            string strResult;
            foreach(string strPrefix in prefixList)
            {
                 strResult = strPrefix;

                 if(!strPrefix.EndsWith("/"))
                     strResult = strPrefix + "/";

                 if(!httpListener.Prefixes.Contains(strResult))
                     httpListener.Prefixes.Add(strResult);
            }

        }

        public void Start()
        {
            if (!HttpListener.IsSupported)
                return;

            try
            {
                if (!isListening)
                {
                    AddPrefixs();

                    httpListener.Start();

                    //IAsyncResult result = httpListener.BeginGetContext(new AsyncCallback(Process), httpListener);
                    // Applications can do some work here while waiting for the 
                    // request. If no work can be done until you have processed a request,
                    // use a wait handle to prevent this thread from terminating
                    // while the asynchronous operation completes.
                    //Console.WriteLine("Waiting for request to be processed asyncronously.");
                    //result.AsyncWaitHandle.WaitOne();
                    //Console.WriteLine("Request processed asyncronously.");

                    //isListening = true;

                    thread = new Thread(Process);
                    thread.SetApartmentState(ApartmentState.STA);

                    thread.Start();
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex,string.Empty);
            }
        }

        public void ThreadProcess()
        {
            //httpListener.BeginGetContext(new AsyncCallback(Process), httpListener);
        }
        public static void ListenerCallback(IAsyncResult result)
        {
           
            HttpListener listener = (HttpListener)result.AsyncState;
            listener.BeginGetContext(new AsyncCallback(ListenerCallback), listener);
            // Call EndGetContext to complete the asynchronous operation.
            HttpListenerContext context = listener.EndGetContext(result);
            HttpListenerRequest request = context.Request;
            // Obtain a response object.
            HttpListenerResponse response = context.Response;
            // Construct a response.
            string responseString = "<HTML><BODY> Hello world!</BODY></HTML>";
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
            // Get a response stream and write the response to it.
            response.ContentLength64 = buffer.Length;
            System.IO.Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            // You must close the output stream.
            output.Close();
        }


        public void Stop()
        {
            if (isListening)
            {
                httpListener.Stop();
                thread.Abort();

                isListening = false;
            }
        }

        public void Process()
        {
            //while (isListening)
            //{

            HttpListenerContext context = httpListener.GetContext();

            try
            {
                HttpListenerResponse response = context.Response;
                response.ContentType = "text/xml";
                //response.ContentEncoding = Encoding.UTF8;
                string strQuery = context.Request.Url.Query;
                //WriteLog(null, strQuery);

                string strContent = ControlResponse(strQuery);
               // WriteLog(null, strContent);

                byte[] byteContent = System.Text.Encoding.UTF8.GetBytes(strContent);

                response.ContentLength64 = byteContent.Length;

                response.OutputStream.Write(byteContent, 0, byteContent.Length);

                response.OutputStream.Close();

            }
            catch(Exception ex)
            {
                WriteLog(ex,string.Empty);
            }

            Process();
            //}
        }


        private string ControlResponse(string strUrl)
        {
            if (string.IsNullOrEmpty(strUrl))
                return string.Empty;

            string strRegexExpression = CommonOperation.GetConfigValueByKey(Constant.CTRAVELSKY);

            string strDeparture = RegexOperation.GetValueByRegex(string.Format(strRegexExpression, Constant.CORGCITY), strUrl);
            string strArrival = RegexOperation.GetValueByRegex(string.Format(strRegexExpression, Constant.CDSTCITY), strUrl);
            string strDepartureTime = RegexOperation.GetValueByRegex(string.Format(strRegexExpression, Constant.CFLYDATE), strUrl);

            DateTime? departureTime = Convert.ToDateTime(strDepartureTime.Substring(0,4) 
                                                        + "-" + strDepartureTime.Substring(4,2) 
                                                        + "-" + strDepartureTime.Substring(6,2));

            IList<RouteInformation> routeInformationList = RequestRoute(strDeparture, strArrival, departureTime);
            if (routeInformationList == null)
                return string.Empty;

            return CommonOperation.GetXmlOfFlightInformation(routeInformationList);
           
        }

        private IList<RouteInformation> RequestRoute(string strDeparture, string strArrival, DateTime? departureTime)
        {
            FactoryContribution factoryContribution = new FactoryContribution();

            IRouteFactory routeFactory = factoryContribution.CreateFactory();
            if (routeFactory == null)
                return null;

            IRouteOperation routeOperation = routeFactory.CreateRouteInstance();

            IRegexExpression regexExpression = routeFactory.CreateRouteRegex();

            string strUrl = routeOperation.GetRequestUrl(strDeparture, strArrival, departureTime);
            if (string.IsNullOrEmpty(strUrl))
                return null;

            string strContent = routeOperation.GetHtmlContent(strUrl);

            IList<RouteInformation> routeInformationList = routeOperation.ParseHtmlCode(strContent, regexExpression);

            return routeInformationList;
        }

        private string ConvertXml(IList<RouteInformation> flightInformationList)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");//encoding=\"utf-8\" 
            //strBuilder.Append("<![CDATA[\"");
            strBuilder.Append("<FlightInformations>");

            if (flightInformationList != null && flightInformationList.Count > 0)
            {
                foreach (RouteInformation flightInformation in flightInformationList)
                {
                    strBuilder.Append("<FlightInformation>");
                    strBuilder.Append("<AirDate>" + flightInformation.AirDate.ToString("yyyy-MM-dd") + "</AirDate>");
                    strBuilder.Append("<OriginalAirport>" + flightInformation.OriginalAirport + "</OriginalAirport>");
                    strBuilder.Append("<DestinationAirport>" + flightInformation.DestinationAirport + "</DestinationAirport>");
                    strBuilder.Append("<AirLine>" + flightInformation.AirLine + "</AirLine>");
                    strBuilder.Append("<FlightNO>" + flightInformation.FlightNO + "</FlightNO>");
                    strBuilder.Append("<DepartureTime>" + flightInformation.DepartureTime.ToString("HHmm") + "</DepartureTime>");
                    strBuilder.Append("<ArriveTime>" + flightInformation.ArriveTime.ToString("HHmm") + "</ArriveTime>");
                    strBuilder.Append("<FlightType>" + flightInformation.FlightType + "</FlightType>");
                    strBuilder.Append("<Meal>" + flightInformation.Meal + "</Meal>");
                    strBuilder.Append("<FuelTax>" + flightInformation.FuelTax.ToString("N") + "</FuelTax>");
                    strBuilder.Append("<AirportTax>" + flightInformation.AirportTax.ToString("N") + "</AirportTax>");
                    strBuilder.Append("<Yprice>" + flightInformation.Yprice + "</Yprice>");
                    strBuilder.Append("<ChangeRule>" + flightInformation.ChangeRule + "</ChangeRule>");
                    strBuilder.Append("<FlightInterval>" + flightInformation.FlightInterval + "</FlightInterval>");
                    strBuilder.Append("<Discount>" + flightInformation.Discount.ToString() + "</Discount>");
                    strBuilder.Append("<FlihghtPrice>" + flightInformation.TicketPrice.ToString("N") + "</FlihghtPrice>");
                    strBuilder.Append("</FlightInformation>");
                }
            }

            strBuilder.Append("</FlightInformations>");
            //strBuilder.Append("\"]]>");
            return strBuilder.ToString();
        }

        #region 异常记录
        /// <summary>
        /// 异常记录
        /// </summary>
        /// <param name="ex">异常</param>
        private static void WriteLog(Exception ex,string strContent)
        {
            string strDirectory = CommonOperation.GetConfigValueByKey(Constant.CDIRECTORY);

            if (!Directory.Exists(strDirectory))
                Directory.CreateDirectory(strDirectory);

            if (!strDirectory.EndsWith("/"))
                strDirectory += "/";

            string strPath = strDirectory + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";

            StringBuilder sbContent = new StringBuilder();
            if(ex != null)
              sbContent.AppendLine("源:" + ex.Source + " 消息:" + ex.Message + " 栈:" + ex.StackTrace);

            if (!string.IsNullOrEmpty(strContent))
            {
                sbContent.AppendLine("操作：" + strContent);
            }
            
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

    }
}
