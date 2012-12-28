using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyFramework.WebAppService
{
    public class WebAppServiceType : IWebAppService
    {
        public bool Login(string username, string password)
        {
            return true;
        }
    }
}
