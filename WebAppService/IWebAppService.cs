using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;



namespace MyFramework.WebAppService
{
    [ServiceContract]//服务契约
    public interface IWebAppService
    {
        [OperationContract]//操作合约
        bool Login(string username, string password);
    }
}
