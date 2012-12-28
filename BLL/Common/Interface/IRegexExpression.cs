using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Common.Interface
{
    /// <summary>
    /// ��ȡ����˾������ͼ۵�������ʽ�ӿ�
    /// </summary>
    public interface IRegexExpression
    {

        /// <summary>
        /// ��ȡĳ�����ݵ�������ʽ
        /// </summary>
        string GetSingleRowRegex();

        /// <summary>
        /// ��ȡ���չ�˾��������ʽ
        /// </summary>
        string GetAirLineRegex();

        /// <summary>
        /// ��ȡ��������ѵ�������ʽ
        /// </summary>
        string GetAirportRegex();

        /// <summary>
        /// ��ȡ������е�������ʽ
        /// </summary>
        string GetArrivalCityRegex();

        /// <summary>
        /// ��ȡ����ʱ���������ʽ
        /// </summary>
        string GetArrivalTimeRegex();

        /// <summary>
        /// ��ȡ��λ��������ʽ
        /// </summary>
        string GetCabinRegex();

        /// <summary>
        /// ��ȡ�˸�ǩ��������ʽ
        /// </summary>
        string GetChangeRuleRegex();

        /// <summary>
        /// ��ȡ�������е�������ʽ
        /// </summary>
        string GetDepartureCityRegex();

        /// <summary>
        /// ��ȡ����ʱ���������ʽ
        /// </summary>
        string GetDepartureTimeRegex();

        /// <summary>
        /// ��ȡ�ۿ۵�������ʽ
        /// </summary>
        string GetDiscountRegex();

        /// <summary>
        /// ��ȡ����ŵ�������ʽ
        /// </summary>
        string GetFlightNORegex();

        /// <summary>
        /// ��ȡ���͵�������ʽ
        /// </summary>
        string GetFlightTypeRegex();

        /// <summary>
        /// ��ȡȼ�͵�������ʽ
        /// </summary>
        string GetFuelRegex();

        /// <summary>
        /// ��ȡY�ռ۸��������ʽ
        /// </summary>
        string GetYpriceRegex();

        /// <summary>
        /// ��ȡƱ�۵�������ʽ
        /// </summary>
        string GetTicketPriceRegex();

    }
}
