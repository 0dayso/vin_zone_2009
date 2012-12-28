using System;
using System.Collections.Generic;
using System.Text;

using BLL.Common.Interface;
namespace BLL.VeryTrip
{
    public class VeryTripRouteFactory : IRouteFactory
    {

        /// <summary>
        /// ��������ʵ��
        /// </summary>
        /// <returns></returns>
        public IRouteOperation CreateRouteInstance()
        {
            return new VeryTripRouteOperation();
        }

        /// <summary>
        /// ��������������ʽ
        /// </summary>
        /// <returns></returns>
        public IRegexExpression CreateRouteRegex()
        {
            return new VeryTripRegexExpression();
        }

    }
}
