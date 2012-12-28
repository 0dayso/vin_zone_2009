using System;
using System.Collections.Generic;
using System.Text;

using Models;

namespace BLL.Common.Interface
{
    public interface IRouteOperation
    {
        /// <summary>
        /// ��ȡ������ĸ���˾����
        /// </summary>
        /// <param name="strUrl"></param>
        string GetHtmlContent(string strUrl);

        /// <summary>
        /// ����������Ϣ
        /// </summary>
        /// <param name="strContent">��������</param>
        /// <param name="regexInstance">����ʵ��</param>
        /// <returns></returns>
        IList<RouteInformation> ParseHtmlCode(string strContent,IRegexExpression regexInstance);

        /// <summary>
        /// ��ȡ�����URL
        /// </summary>
        /// <param name="strDeparture">������</param>
        /// <param name="strArrival">�����</param>
        /// <param name="departureTime">����ʱ��</param>
        /// <returns></returns>
        string GetRequestUrl(string strDeparture, string strArrival, DateTime? departureTime);

        /// <summary>
        /// ��ȡ���߼۸���Դ
        /// </summary>
        /// <returns></returns>
        int GetSourceType();
    }
}
