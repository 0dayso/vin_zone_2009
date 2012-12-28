using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Common.Interface
{
    public interface IRouteFactory
    {
        /// <summary>
        /// ��������ʵ��
        /// </summary>
        /// <returns></returns>
        IRouteOperation CreateRouteInstance();

        /// <summary>
        /// ��������������ʽ
        /// </summary>
        /// <returns></returns>
        IRegexExpression CreateRouteRegex();
    }
}
