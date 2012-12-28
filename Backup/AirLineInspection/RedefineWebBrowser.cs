using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace AirLineInspection
{
    public class RedefineWebBrowser : WebBrowser
    {
        public RedefineWebBrowser()
        { 
        }

        protected override void OnDocumentCompleted(WebBrowserDocumentCompletedEventArgs e)
        {
          
            base.OnDocumentCompleted(e);
        }

    }
}
