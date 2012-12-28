using System;
using System.Collections.Generic;
using System.Text;

using System.Configuration;
using Models;
namespace BLL.Common.Operation
{
    public class CommonOperation
    {
        #region ��ȡ�����ļ�
        /// <summary>
        /// ��ȡ�����ļ�
        /// </summary>
        /// <param name="strKey">��</param>
        /// <returns></returns>
        public static string GetConfigValueByKey(string strKey)
        {
            string strValue = ConfigurationManager.AppSettings[strKey];

            if (string.IsNullOrEmpty(strValue))
                return string.Empty;
            else
                return strValue;
        }
        #endregion

        #region ��ȡxml������Ϣ
        /// <summary>
        /// ��ȡxml������Ϣ
        /// </summary>
        /// <param name="flightInforamtion">������Ϣ</param>
        /// <returns></returns>
        public static string GetXmlOfFlightInformation(IList<RouteInformation> routeInformationList)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append("<?xml version=\"1.0\" encoding=\"gb2312\" ?>");
            strBuilder.Append("<travelskyInfo><errorCode>0</errorCode><errorInfo>OK</errorInfo>");
            
            if (routeInformationList != null && routeInformationList.Count > 0)
            {
                strBuilder.Append("<flightDate>" + routeInformationList[0].AirDate.ToString("yyyy-MM-dd") + "</flightDate>");
                strBuilder.Append("<airResults>");

                foreach (RouteInformation routeInformation in routeInformationList)
                {
                    strBuilder.Append("<airResult>");
                    strBuilder.Append("<airDate>" + routeInformation.AirDate.ToString("yyyy-MM-dd") + "</airDate>");
                    strBuilder.Append("<originalAirport>" + routeInformation.OriginalAirport + "</originalAirport>");
                    strBuilder.Append("<destinationAirport>" + routeInformation.DestinationAirport + "</destinationAirport>");
                    strBuilder.Append("<airLine>" + routeInformation.AirLine + "</airLine>");
                    strBuilder.Append("<flightNO>" + routeInformation.FlightNO + "</flightNO>");
                    strBuilder.Append("<departureTime>" + routeInformation.DepartureTime.ToString("HHmm") + "</departureTime>");
                    strBuilder.Append("<arriveTime>" + routeInformation.ArriveTime.ToString("HHmm") + "</arriveTime>");
                    strBuilder.Append("<flightType>" + routeInformation.FlightType + "</flightType>");
                    strBuilder.Append("<ASR></ASR>");
                    strBuilder.Append("<meal>" + routeInformation.Meal + "</meal>");
                    strBuilder.Append("<link></link>");
                    strBuilder.Append("<FuelTax>" + routeInformation.FuelTax + "</FuelTax>");
                    strBuilder.Append("<AirportTax>" + routeInformation.AirportTax + "</AirportTax>");
                    strBuilder.Append("<Etkt>E</Etkt>");
                    strBuilder.Append("<stops>" + routeInformation.Stops.ToString() + "</stops>");
                    strBuilder.Append("<Yprice>" + routeInformation.Yprice + "</Yprice>");

                    //��λ
                    strBuilder.Append(GetXmlOfSeat(routeInformation.SeatList));
                    strBuilder.Append("</airResult>");
                }

                strBuilder.Append("</airResults>");
            }

            strBuilder.Append("</travelskyInfo>");

            return strBuilder.ToString();
        }

        /// <summary>
        /// ��ȡxml��λ�ַ���
        /// </summary>
        /// <param name="seatList"></param>
        /// <returns></returns>
        private static string GetXmlOfSeat(IList<Seat> seatList)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append("<seats>");

            if (seatList != null)
            {
                foreach (Seat seat in seatList)
                {
                    strBuilder.Append("<seat>");
                    strBuilder.Append("<cabin>" + seat.Cabin + "</cabin>");
                    strBuilder.Append("<count>" + seat.Count.ToString() + "</count>");
                    strBuilder.Append("<price>" + seat.Price.ToString() + "</price>");
                    strBuilder.Append("<subcabin>" + seat.SubCanbin + "</subcabin>");
                    strBuilder.Append("</seat>");
                }
            }

            strBuilder.Append("</seats>");

            return strBuilder.ToString();
        }
        #endregion
    }
}
