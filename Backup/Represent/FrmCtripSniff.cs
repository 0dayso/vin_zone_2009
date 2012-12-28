using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using BLL.Elong;
using Models;
namespace Represent
{
    public partial class FrmCtripSniff : Form
    {
        

        public FrmCtripSniff()
        {
            InitializeComponent();
        }

        private void btnParse_Click(object sender, EventArgs e)
        {
              //DataTransfer dataTrans = new DataTransfer ();

             // IList<FlightInformation> flightInformationList = dataTrans.ParseHtmlCode(txtContent.Text);

              //txtResult.Text = ConvertXml(flightInformationList);
        }

        private string ConvertXml(IList<RouteInformation> flightInformationList)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append("<FlightInformations>");

            if (flightInformationList != null && flightInformationList.Count > 0)
            {
                foreach (RouteInformation flightInformation in flightInformationList)
                {
                    strBuilder.Append("<FlightInformation>");
                    strBuilder.Append("<AirDate>" + flightInformation.AirDate.ToString() + "</AirDate>");
                    strBuilder.Append("<OriginalAirport>" + flightInformation.OriginalAirport + "</OriginalAirport>");
                    strBuilder.Append("<DestinationAirport>" + flightInformation.DestinationAirport + "</DestinationAirport>");
                    strBuilder.Append("<AirLine>" + flightInformation.AirLine + "</AirLine>");
                    strBuilder.Append("<FlightNO>" + flightInformation.FlightNO + "</FlightNO>");
                    strBuilder.Append("<DepartureTime>" + flightInformation.DepartureTime.ToString() + "</DepartureTime>");
                    strBuilder.Append("<ArriveTime>" + flightInformation.ArriveTime.ToString() + "</ArriveTime>");
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

            return strBuilder.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //WebBrowser webCtrip = new WebBrowser();
            //webCtrip.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(webCtrip_DocumentCompleted);

            //string URL = "http://flights.ctrip.com/Domestic/ShowFareFirst.aspx?DCity1=" + txtStartAirport.Text.Trim() + "&ACity1=" + txtEndAirport.Text.Trim() + "&DCityName1=&ACityName1=&DDate1=" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "&ClassType=&PassengerQuantity=1&SendTicketCity=&Airline=&PassengerType=ADU&FlightSearchType=S&";

            //webCtrip.Navigate(URL);





            string URL = "http://flight.elong.com/cn_list_" + txtStartAirport.Text.Trim() + "_" + txtEndAirport.Text.Trim() + "_" +

dateTimePicker1.Value.ToString("yyyy-MM-dd") + "_Y.html";

            WebBrowser webBrower = new WebBrowser();

            webBrower.ScriptErrorsSuppressed = true;
            webBrower.Navigate(URL);
            webBrower.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(webCtrip_DocumentCompleted);




            
        }        

        private void webCtrip_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            WebBrowser webCtrip = (WebBrowser)sender;
            string strContent = string.Empty;
            if (webCtrip.ReadyState == WebBrowserReadyState.Complete)
            {
                textBox1.Text = webCtrip.Document.GetElementById("flightlist").InnerHtml;
            }
            //WebBrowser webCtrip = (WebBrowser)sender;
            //if (webCtrip.ReadyState == WebBrowserReadyState.Complete)
            //{

            //    DataTransfer dataTrans = new DataTransfer();
            //    IList<FlightInformation> flightInformationList = dataTrans.ParseHtmlCode(webCtrip.Document.GetElementById("fltListContent").InnerHtml);
            //    dvgFlightResult.DataSource = flightInformationList;
            //}
        }

       
    }
}