using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Common.Operation
{
    public class Constant
    {
        /// <summary>
        /// 根元素
        /// </summary>
        public const string CROOTELEMENT = "fltListContent";

        /// <summary>
        /// 空元素
        /// </summary>
        public const string CBLANKURL = "about:blank";

        /// <summary>
        /// 获取创建工厂的键值
        /// </summary>
        public const string CFACTORYNAME = "FACTORYNAME";
        /// <summary>
        /// 艺龙 根元素
        /// </summary>
        public const string EROOTELEMENT = "flightlist";
        /// <summary>
        /// 程序集名称
        /// </summary>
        public const string CBLL = "BLL";

        /// <summary>
        /// 记录日志目录
        /// </summary>
        public const string CDIRECTORY = "DIRECTORY";

        #region 获取Xml文件路径的键值
        /// <summary>
        /// 获取携程正则表达式文件的键值或获取艺龙正则表达式文件的键值
        /// </summary>
        public const string CCTRIPPATH = "CTRIPPATH";
        public const string CELONGPATH = "ELONGPATH";
        /// <summary>
        /// 非常旅行
        /// </summary>
        public const string CVERYTRIPPATH = "VERYTRIPPATH";
        #endregion

        #region 获取请求URL的键值
        /// <summary>
        /// 携程URL键值
        /// </summary>
        public const string CCTRIPURL = "CTRIPURL";
        /// <summary>
        /// 携程子舱位URL键值
        /// </summary>
        public const string CCTRIPSUBURL = "CTRIPSUBURL";

        /// <summary>
        /// 非常旅行查询URL键值
        /// </summary>
        public const string CVERYTRIPURL = "VERYTRIPURL";
        /// <summary>
        /// 非常旅行等待URL键值
        /// </summary>
        public const string CVERYWAITINGTRIPURL = "VERYWAITINGTRIPURL";
        #endregion

        #region 正则表达式常量
        /// <summary>
        /// 正则表达式根节点
        /// </summary>
        public const string CREGEXEXPRESSION = "RegexExpression";

        /// <summary>
        /// 航空公司
        /// </summary>
        public const string CAIRLINE = "AirLine";

        /// <summary>
        /// 机建
        /// </summary>
        public const string CAIRPORT = "Airport";

        /// <summary>
        /// 到达城市
        /// </summary>
        public const string CARRIVALCITY = "ArrivalCity";

        /// <summary>
        /// 到达时间
        /// </summary>
        public const string CARRIVALTIME = "ArrivalTime";

        /// <summary>
        /// 舱位
        /// </summary>
        public const string CCABIN = "Cabin";

        /// <summary>
        /// 退改签
        /// </summary>
        public const string CCHANGERULE = "ChangeRule";

        /// <summary>
        /// 出发城市
        /// </summary>
        public const string CDEPARTURECITY = "DepartureCity";

        /// <summary>
        /// 出发时间
        /// </summary>
        public const string CDEPARTURETIME = "DepartureTime";

        /// <summary>
        /// 折扣
        /// </summary>
        public const string CDISCOUNT = "Discount";

        /// <summary>
        /// 航班号
        /// </summary>
        public const string CFLIGHTNO = "FlightNO";

        /// <summary>
        /// 机型
        /// </summary>
        public const string CFLIGHTTYPE = "FlightType";

        /// <summary>
        /// 燃油
        /// </summary>
        public const string CFUEL = "Fuel";

        /// <summary>
        /// 某行数据
        /// </summary>
        public const string CSINGLEROW = "SingleRow";

        /// <summary>
        /// 票价
        /// </summary>
        public const string CTICKETPRICE = "TicketPrice";

        /// <summary>
        /// Y舱价格
        /// </summary>
        public const string CYPRICE = "Yprice";

        /// <summary>
        /// Tbody元素中Data属性
        /// </summary>
        public const string CTBODYDATA = "TbodyData";

        /// <summary>
        /// 城市对值
        /// </summary>
        public const string CCITY = "City";

        /// <summary>
        /// 日期对值
        /// </summary>
        public const string CDATE = "Date";

        /// <summary>
        /// 餐食
        /// </summary>
        public const string CMEAL = "Meal";

        /// <summary>
        /// 机建燃油
        /// </summary>
        public const string CAIRPORTFUEL = "AirportFuel";

        /// <summary>
        /// 飞行时长
        /// </summary>
        public const string CFLIGHTINTERVAL = "FlightInterval";
        /// <summary>
        /// 税费
        /// </summary>
        public const string EAIPORTFUEL = "EAirportFuel";


        /// <summary>
        /// rule中的数据
        /// </summary>
        public const string RULEDATA = "RuleData";

        /// <summary>
        /// rule中的年月日
        /// </summary>
        public const string YEARMONTHDAY = "YearMonthDay";

        /// <summary>
        /// 获取某个航班的所有舱位信息
        /// </summary>
        public const string CALLCABININFOMATION = "AllCabinInfomation";

        /// <summary>
        /// 获取某个航班的舱位
        /// </summary>
        public const string COTHERCANBIN = "OtherCanbin";

        /// <summary>
        /// 获取某个航班舱位的价格
        /// </summary>
        public const string COTHERCANBINPRICE = "OtherCanbinPrice";

        /// <summary>
        /// 获取正常航班中的html代码段
        /// </summary>
        public const string CNORMALFRAGMENT = "NormalFragment";

        /// <summary>
        /// 获取某个航班中第一部分信息
        /// </summary>
        public const string CSINGLEROWFIRST = "SingleRowFirst";

        /// <summary>
        /// 获取某个航班第二部分信息
        /// </summary>
        public const string CSINGLEROWSECOND = "SingleRowSecond";


        /// <summary>
        /// 获取分页html代码段
        /// </summary>
        public const string CPAGEFRAGMENT = "PageFragment";

        /// <summary>
        /// 获取页面分页链接
        /// </summary>
        public const string CPAGELINK = "PageLink";

        /// <summary>
        /// 经停
        /// </summary>
        public const string CSTOPS = "Stops";

        /// <summary>
        /// 子舱位
        /// </summary>
        public const string CSUBCANBIN = "SubCanbin";
        #endregion

        #region 出发到达三字码
        public const string CPEK = "PEK";
        public const string CBJS = "BJS";
        public const string CNAY = "NAY";
        public const string CSIA = "SIA";
        public const string CXIY = "XIY";
        public const string CSHA = "SHA";
        public const string CPVG = "PVG";
        #endregion

        #region 整合航信
        /// <summary>
        /// 正则表达式键值
        /// </summary>
        public const string CTRAVELSKY = "TRAVELSKY";

        /// <summary>
        /// 出发地
        /// </summary>
        public const string CORGCITY = "orgcity";

        /// <summary>
        /// 到达地
        /// </summary>
        public const string CDSTCITY = "dstcity";

        /// <summary>
        /// 出发时间
        /// </summary>
        public const string CFLYDATE = "flydate";
        #endregion

        #region 非常旅行
        /// <summary>
        /// ?问号
        /// </summary>
        public const char CQuestionMark = '?';

        /// <summary>
        /// 获取等待后缀
        /// </summary>
        public const string CWAITINGSUFFIX = "WaitingSuffix";

        /// <summary>
        /// 正常航段匹配标记
        /// </summary>
        public const string CMATCHSIGN = "<div class=\"route-list clear\">";

        /// <summary>
        /// 编码
        /// </summary>
        public const string CCODESIGN = "gb2312";


        #endregion
    }
}
