using System;
using System.Collections.Generic;
using System.Text;

using BLL.Common.Interface;
using System.Reflection;
namespace BLL.Common.Operation
{
    public class FactoryContribution
    {
        /// <summary>
        /// 创建工厂
        /// </summary>
        /// <returns></returns>
        public IRouteFactory CreateFactory()
        {
            Assembly assembly = Assembly.Load(Constant.CBLL);

            string strFactoryName = CommonOperation.GetConfigValueByKey(Constant.CFACTORYNAME);
            if (string.IsNullOrEmpty(strFactoryName))
                return null;

            return (IRouteFactory)assembly.CreateInstance(strFactoryName);
        }
    }
}
