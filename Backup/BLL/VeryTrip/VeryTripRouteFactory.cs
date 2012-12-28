using System;
using System.Collections.Generic;
using System.Text;

using BLL.Common.Interface;
namespace BLL.VeryTrip
{
    public class VeryTripRouteFactory : IRouteFactory
    {

        /// <summary>
        /// 创建航线实例
        /// </summary>
        /// <returns></returns>
        public IRouteOperation CreateRouteInstance()
        {
            return new VeryTripRouteOperation();
        }

        /// <summary>
        /// 创建航线正则表达式
        /// </summary>
        /// <returns></returns>
        public IRegexExpression CreateRouteRegex()
        {
            return new VeryTripRegexExpression();
        }

    }
}
