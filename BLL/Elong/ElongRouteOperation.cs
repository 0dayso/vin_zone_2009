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
        /// ��ȡ������ĸ���˾����
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
        /// ����������Ϣ
        /// </summary>
        /// <param name="strContent">��������</param>
        /// <param name="regexInstance">����ʵ��</param>
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
        /// ��ȡ�����URL
        /// </summary>
        /// <param name="strDeparture">������</param>
        /// <param name="strArrival">�����</param>
        /// <param name="departureTime">����ʱ��</param>
        /// <returns></returns>
        public string GetRequestUrl(string strDeparture, string strArrival, DateTime? departureTime)
        {
            return string.Empty;
        }

        /// <summary>
        /// ��ȡ���߼۸���Դ
        /// </summary>
        /// <returns></returns>
        public int GetSourceType()
        {
            return 0;
        }

        /// <summary>
        /// ����HtmlԴ�ļ�
        /// </summary>
        /// <param name="Url">�����ַ</param>
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
        /// ��ȡ����������Ϣ
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

            //�������С��������
            valueList = RegexOperation.GetValuesByRegex(etripRegex.GetCityRegex(), strContent);
            if (valueList != null && valueList.Count == 2)
            {
                elongrouteInformation.OriginalAirport = valueList[0];
                elongrouteInformation.DestinationAirport = valueList[1];
            }


            //��������
            //elongrouteInformation.OriginalAirport = RegexOperation.GetValuesByRegex(etripRegex.GetDepartureCityRegex(), strContent).ToString();
            //�������
           // elongrouteInformation.DestinationAirport = RegexOperation.GetValuesByRegex(etripRegex.GetArrivalCityRegex(), strContent).ToString();
            //���չ�˾
            elongrouteInformation.AirLine = RegexOperation.GetValueByRegex(etripRegex.GetAirLineRegex(), strTable).ToString();
            //�����
            elongrouteInformation.FlightNO = RegexOperation.GetValueByRegex(etripRegex.GetFlightNORegex(), strContent).ToString();
            //��λ
            elongrouteInformation.Cabin = RegexOperation.GetValueByRegex(etripRegex.GetCabinRegex(), strContent).ToString();
            //����
            elongrouteInformation.FlightType = RegexOperation.GetValueByRegex(etripRegex.GetFlightTypeRegex(), strRule).ToString();
            //�˸�
            elongrouteInformation.ChangeRule = RegexOperation.GetValueByRegex(etripRegex.GetChangeRuleRegex(), strContent).ToString();
            //�ۿ�
            elongrouteInformation.Ediscount = RegexOperation.GetValueByRegex(etripRegex.GetDiscountRegex(), strContent).ToString();

            //˰��
            string eAirportFuel = RegexOperation.GetValueByRegex(etripRegex.GetEAirportFuelRegex(), strTable).ToString();
            if (!string.IsNullOrEmpty(eAirportFuel))
            {
                elongrouteInformation.Eairportfuel = double.Parse(eAirportFuel);
            }
            //����
            string airPort = RegexOperation.GetValueByRegex(etripRegex.GetAirportRegex(), strContent).ToString();
            if (!string.IsNullOrEmpty(airPort))
            {
                elongrouteInformation.AirportTax = double.Parse(airPort);
            }
            //ȼ��
            string fuel = RegexOperation.GetValueByRegex(etripRegex.GetFuelRegex(), strContent).ToString();
            if (!string.IsNullOrEmpty(fuel))
            {
                elongrouteInformation.FuelTax = double.Parse(fuel);
            }
            //Ʊ��
            string ticketPrice = RegexOperation.GetValueByRegex(etripRegex.GetTicketPriceRegex(), strContent).ToString();
            if (!string.IsNullOrEmpty(ticketPrice))
            {
                elongrouteInformation.TicketPrice = double.Parse(ticketPrice);
            }

            string yearmonthday = RegexOperation.GetValueByRegex(etripRegex.GetYearMonthDay(), strRule).ToString();

            //����ʱ��
            string departureTime = RegexOperation.GetValueByRegex(etripRegex.GetDepartureTimeRegex(), strContent).ToString();
            string yd = yearmonthday+" "+departureTime;
           
            if (!string.IsNullOrEmpty(departureTime))
            {
                elongrouteInformation.DepartureTime = DateTime.Parse(yd);
            }
            //����ʱ��
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
