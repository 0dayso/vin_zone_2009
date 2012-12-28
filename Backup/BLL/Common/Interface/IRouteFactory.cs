using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Common.Interface
{
    public interface IRouteFactory
    {
        /// <summary>
        /// 创建航线实例
        /// </summary>
        /// <returns></returns>
        IRouteOperation CreateRouteInstance();

        /// <summary>
        /// 创建航线正则表达式
        /// </summary>
        /// <returns></returns>
        IRegexExpression CreateRouteRegex();
    }
}
