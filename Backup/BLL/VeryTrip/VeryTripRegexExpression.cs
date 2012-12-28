using System;
using System.Collections.Generic;
using System.Text;

using BLL.Common.Interface;
using BLL.Common.Operation;
using System.Xml.XPath;

namespace BLL.VeryTrip
{
    public class VeryTripRegexExpression : IRegexExpression
    {
        private XPathNodeIterator nodeIterator;

        public VeryTripRegexExpression()
        {
            XPathNavigator navigator = RegexOperation.GetXPathNavigatorByPath(CommonOperation.GetConfigValueByKey(Constant.CVERYTRIPPATH));

            nodeIterator = navigator.Select(Constant.CREGEXEXPRESSION);

            nodeIterator.MoveNext();
        }

        /// <summary>
        /// ��ȡ���չ�˾��������ʽ
        /// </summary>
        public string GetAirLineRegex()
        {
            return RegexOperation.GetElementNodeValue(nodeIterator, Constant.CAIRLINE);
        }

        /// <summary>
        /// ��ȡ��������ѵ�������ʽ
        /// </summary>
        public string GetAirportRegex()
        {
            return RegexOperation.GetElementNodeValue(nodeIterator, Constant.CAIRPORT);
        }

        /// <summary>
        /// ��ȡ������е�������ʽ
        /// </summary>
        public string GetArrivalCityRegex()
        {
            return RegexOperation.GetElementNodeValue(nodeIterator, Constant.CARRIVALCITY);
        }

        /// <summary>
        /// ��ȡ����ʱ���������ʽ
        /// </summary>
        public string GetArrivalTimeRegex()
        {
            return RegexOperation.GetElementNodeValue(nodeIterator, Constant.CARRIVALTIME);
        }

        /// <summary>
        /// ��ȡ��λ��������ʽ
        /// </summary>
        public string GetCabinRegex()
        {
            return RegexOperation.GetElementNodeValue(nodeIterator, Constant.CCABIN);
        }

        /// <summary>
        /// ��ȡ�˸�ǩ��������ʽ
        /// </summary>
        public string GetChangeRuleRegex()
        {
            return RegexOperation.GetElementNodeValue(nodeIterator, Constant.CCHANGERULE);
        }

        /// <summary>
        /// ��ȡ�������е�������ʽ
        /// </summary>
        public string GetDepartureCityRegex()
        {
            return RegexOperation.GetElementNodeValue(nodeIterator, Constant.CDEPARTURECITY);
        }

        /// <summary>
        /// ��ȡ����ʱ���������ʽ
        /// </summary>
        public string GetDepartureTimeRegex()
        {
            return RegexOperation.GetElementNodeValue(nodeIterator, Constant.CDEPARTURETIME);
        }

        /// <summary>
        /// ��ȡ�ۿ۵�������ʽ
        /// </summary>
        public string GetDiscountRegex()
        {
            return RegexOperation.GetElementNodeValue(nodeIterator, Constant.CDISCOUNT);
        }

        /// <summary>
        /// ��ȡ����ŵ�������ʽ
        /// </summary>
        public string GetFlightNORegex()
        {
            return RegexOperation.GetElementNodeValue(nodeIterator, Constant.CFLIGHTNO);
        }

        /// <summary>
        /// ��ȡ���͵�������ʽ
        /// </summary>
        public string GetFlightTypeRegex()
        {
            return RegexOperation.GetElementNodeValue(nodeIterator, Constant.CFLIGHTTYPE);
        }

        /// <summary>
        /// ��ȡȼ�͵�������ʽ
        /// </summary>
        public string GetFuelRegex()
        {
            return RegexOperation.GetElementNodeValue(nodeIterator, Constant.CFUEL);
        }

        /// <summary>
        /// ��ȡĳ�����ݵ�������ʽ
        /// </summary>
        public string GetSingleRowRegex()
        {
            return RegexOperation.GetElementNodeValue(nodeIterator, Constant.CSINGLEROW);
        }

        /// <summary>
        /// ��ȡƱ�۵�������ʽ
        /// </summary>
        public string GetTicketPriceRegex()
        {
            return RegexOperation.GetElementNodeValue(nodeIterator, Constant.CTICKETPRICE);
        }

        /// <summary>
        /// ��ȡY�ռ۸��������ʽ
        /// </summary>
        public string GetYpriceRegex()
        {
            return RegexOperation.GetElementNodeValue(nodeIterator, Constant.CYPRICE);
        }

        /// <summary>
        /// ��ȡ���������е�html�����
        /// </summary>
        public string GetNormalFragmentRegex()
        {
            return RegexOperation.GetElementNodeValue(nodeIterator, Constant.CNORMALFRAGMENT);
        }

        /// <summary>
        /// ��ȡĳ�������е�һ������Ϣ
        /// </summary>
        public string GetSingleRowFirstRegex()
        {
            return RegexOperation.GetElementNodeValue(nodeIterator, Constant.CSINGLEROWFIRST);
        }

        /// <summary>
        /// ��ȡĳ������ڶ�������Ϣ
        /// </summary>
        public string GetSingleRowSecondRegex()
        {
            return RegexOperation.GetElementNodeValue(nodeIterator, Constant.CSINGLEROWSECOND);
        }

        /// <summary>
        /// ��ȡ��ҳhtml�����
        /// </summary>
        public string GetPageFragmentRegex()
        {
            return RegexOperation.GetElementNodeValue(nodeIterator, Constant.CPAGEFRAGMENT);
        }

        /// <summary>
        /// ��ȡҳ���ҳ����
        /// </summary>
        /// <returns></returns>
        public string GetPageLinkRegex()
        {
            return RegexOperation.GetElementNodeValue(nodeIterator, Constant.CPAGELINK);
        }

       

        /// <summary>
        /// ��ȡ�ȴ���׺
        /// </summary>
        /// <returns></returns>
        public string GetWaitingSuffixRegex()
        {
            return RegexOperation.GetElementNodeValue(nodeIterator, Constant.CWAITINGSUFFIX);
        }
        

        /// <summary>
        /// ��ȡĳ�����ྭͣ��Ϣ
        /// </summary>
        /// <returns></returns>
        public string GetStops()
        {
            return RegexOperation.GetElementNodeValue(nodeIterator, Constant.CSTOPS);
        }

        /// <summary>
        /// ��ȡĳ��������Ӳ�λ
        /// </summary>
        /// <returns></returns>
        public string GetSubCanbin()
        {
            return RegexOperation.GetElementNodeValue(nodeIterator, Constant.CSUBCANBIN);
        }

        /// <summary>
        /// ��ȡĳ�������λ�ļ۸�
        /// </summary>
        /// <returns></returns>
        public string GetOtherCanbinPrice()
        {
            return RegexOperation.GetElementNodeValue(nodeIterator, Constant.COTHERCANBINPRICE);
        }
    }
}
