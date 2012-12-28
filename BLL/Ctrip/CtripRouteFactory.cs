using System;
using System.Collections.Generic;
using System.Text;

using BLL.Common.Interface;
namespace BLL.Ctrip
{
    public class CtripRouteFactory : IRouteFactory
    {
       /// <summary>
        /// ��������ʵ��
        /// </summary>
        /// <returns></returns>
        public IRouteOperation CreateRouteInstance()
        {
            return new CtripRouteOperation();
        }

         /// <summary>
        /// ��������������ʽ
        /// </summary>
        /// <returns></returns>
        public IRegexExpression CreateRouteRegex()
        {
            return new CtripRegexExpression() ;
        }
    }
}
