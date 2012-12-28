using System;
using System.Collections.Generic;
using System.Text;
using BLL.Common.Interface;
namespace BLL.Elong
{
    public class ElongRouteFactory : IRouteFactory
    {
        /// <summary>
        /// 创建航线正则表达式
        /// </summary>
        /// <returns></returns>
        public IRegexExpression CreateRouteRegex()
        {
            return new ElongRegexEpression();
        }

        /// <summary>
        /// 创建航线实例
        /// </summary>
        /// <returns></returns>
        public IRouteOperation CreateRouteInstance()
        {
            return new ElongRouteOperation();
        }
    }
}
