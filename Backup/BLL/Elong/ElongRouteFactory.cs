using System;
using System.Collections.Generic;
using System.Text;
using BLL.Common.Interface;
namespace BLL.Elong
{
    public class ElongRouteFactory : IRouteFactory
    {
        /// <summary>
        /// ��������������ʽ
        /// </summary>
        /// <returns></returns>
        public IRegexExpression CreateRouteRegex()
        {
            return new ElongRegexEpression();
        }

        /// <summary>
        /// ��������ʵ��
        /// </summary>
        /// <returns></returns>
        public IRouteOperation CreateRouteInstance()
        {
            return new ElongRouteOperation();
        }
    }
}
