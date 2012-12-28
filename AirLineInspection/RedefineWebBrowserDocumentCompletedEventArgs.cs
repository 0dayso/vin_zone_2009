using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Models;
namespace AirLineInspection
{
    public class RedefineWebBrowserDocumentCompletedEventArgs : WebBrowserDocumentCompletedEventArgs
    {

        //public RedefineWebBrowserDocumentCompletedEventArgs(FlightLowestPrice lowestPrice)
        //{
        //    this.RedefineWebBrowserDocumentCompletedEventArgs(lowestPrice, base.Url);
        //}

        //public RedefineWebBrowserDocumentCompletedEventArgs()
        //{
           
        //}

        public RedefineWebBrowserDocumentCompletedEventArgs(FlightLowestPrice lowestPrice, Uri url)
            : base(url)
        {
            this.lowestPrice = lowestPrice;
        }


        #region 属性
        private FlightLowestPrice lowestPrice;
        /// <summary>
        /// 系统最低价
        /// </summary>
        public FlightLowestPrice LowestPrice
        {
            get
            {
                return lowestPrice;
            }
        }
        #endregion
    }
}
