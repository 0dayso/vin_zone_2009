using System;
using System.Collections.Generic;
using System.Text;

namespace MyFramework.BusinessLogic.Common
{
    public class EnumDef
    {
        public const string EWebOrder = "网站";
        public const string EAgentOrder = "代理商";
        public const string EHBEGetXml = "20000";     //从航信获取XML文档结果
        public const int EAdminUserID = 1; //系统管理员ID
        public const string EHubsGetXml = "success"; //从汇通天下获取XML文档结果

        #region 系统公用
        public enum EListFillType { 所有 = 1, 空项 = 2, 正常 = 3 };
        //包含添加，修改两个值（ToModify,ToInsert）
        public enum EBaseOperation { modify, insert, delete, none };

        public enum EWeek : int { 星期一 = 1, 星期二 = 2, 星期三 = 3, 星期四 = 4, 星期五 = 5, 星期六 = 6, 星期日 = 0 };

        public enum EYesNo : int { 是 = 1, 否 = 0 };

        /// <summary>
        /// 生日类型
        /// </summary>
        public enum ECanlendarType : int { 阳历 = 1, 阴历 = 2, 不接受短信生日祝福 = 3 };

        /// <summary>
        ///货币类型
        /// </summary>
        public enum ECurrencyType : int { 人民币 = 1, 美元 = 2, 欧元 = 3, 英镑 = 8, 港币 = 9, 日元 = 10, 澳门元 = 11, 新加坡元 = 14 };
        /// <summary>
        /// 系统字典_方位
        /// </summary>
        public enum EDirectionType : int { 未知 = 9 };

        #region 日志操作

        #region 操作类型Log枚举
        /// <summary>
        /// 操作类型Log枚举
        /// </summary>
        public enum EOperationLogType : int
        {
            /// <summary>
            /// 其它操作
            /// </summary>
            OtherOperation = 0,

            /// <summary>
            /// 网银在线信用卡操作
            /// </summary>
            WangYinOperation = 1

        }
        #endregion

        #region 日志配置
        /// <summary>
        /// 日志配置,1表示Open,0表示Close
        /// </summary>
        public enum EOpenFlag : int
        {
            /// <summary>
            /// 打开日志记录
            /// </summary>
            Close = 0,

            /// <summary>
            /// 关闭日志记录
            /// </summary>
            Open = 1

        }
        #endregion

        #region 记录方式配置
        /// <summary>
        /// 日志配置,1表示异步,0表示同步
        /// </summary>
        public enum ERecordModeFlag : int
        {
            /// <summary>
            /// 同步
            /// </summary>
            Synchronize = 0,

            /// <summary>
            /// 异步
            /// </summary>
            Asynchronize = 1
        }
        #endregion

        #endregion

        #endregion

        #region 订单公用

        /// <summary>
        /// 订单来源
        /// </summary>
        public enum ESOrderSourceType : int { 电话 = 1, 网上 = 2, 代理商 = 3, 手机 = 4 };

        /// <summary>
        /// 订单来源 外网使用
        /// </summary>
        public enum ESOrderSourceTypeWeb : int { 电话预订 = 1, 网上预订 = 2 };

        /// <summary>
        /// 手机网站酒店订单状态
        /// </summary>
        public enum ESMobileOrderStatus : int { 预订 = 0, 处理中 = 1, 已完成 = 2, 作废 = 3 };

        #region 订单支付
        /// <summary>
        /// 支付记录与订单对应关系类型
        /// </summary>
        /// 2007-9-19 zhb
        public enum ESPOrderPayType : int { 退款 = 0, 收款 = 1 };

        /// <summary>
        /// 支付记录与订单对应关系状态
        /// </summary>
        /// 2007-9-19 zhb
        public enum ESPOrderPayStatus : int { 正常 = 1, 取消 = 0 };

        /// <summary>
        /// 支付记录对应的订单的类型
        /// </summary>
        /// 2007-9-19 zhb
        public enum ESPOrderType : int { 酒店订单 = 1, 机票订单 = 2, 餐饮订单 = 3, 高尔夫订单 = 4, 国际机票 = 5, 机加酒订单 = 11, 会员回访 = 15 };

        /// <summary>
        /// /// 支付记录的支付方式
        /// </summary>
        /// 2007-9-19 zhb
        /// 添加 分红返款 2008/11/21 cl
        public enum ESPPayMeansType : int { 储值卡 = 1, 直接支付 = 2, 分红返款 = 3 };

        /// <summary>
        /// 支付记录与订单对应关系类型
        /// </summary>
        /// 2007-9-19 zhb
        public enum ESPPayAmountType : int { 订单金额 = 1, 支付手续费 = 2, 退订手续费 = 3, 改订手续费 = 4, 其它 = 0, 会员退款 = 5 };

        /// <summary>
        /// 支付记录的创建类型
        /// </summary>
        /// 2007-9-19 zhb
        public enum ESPRecordCreateType : int { 系统自动 = 0, 人工添加 = 1 };

        /// <summary>
        /// 支付记录的处理状态
        /// </summary>
        /// 2007-9-19 zhb
        public enum ESPRecordStatus : int { 锁定 = 1, 活动 = 2 };

        /// <summary>
        /// 支付记录的退款标识
        /// </summary>
        /// 2007-9-19 zhb
        public enum ESPRecordRefundFlag : int { 待退款 = 1, 无需退款 = 0, 已退款 = 2 };


        public enum ESPTicketSerarchType : int { 国际机票接口 = 11, 国际机票外网 = 13, 国际机票手机 = 15, 国际机票CRM = 17, 国际机票通用 = 19 };
        #endregion

        #region 订单任务

        /// <summary>
        /// 订单任务完成类型
        /// </summary>
        public enum EOTIssueType : int { 完成 = 1, 转移 = 2, 延时 = 3, 异常终止 = 4, 保持 = 5, 停止 = 6 };

        /// <summary>
        /// 订单任务锁定状态
        /// </summary>
        public enum EOTLockStatus : int { 锁定 = 1, 等待 = 0 };

        #endregion

        #region 传真短信相关

        /// <summary>
        /// 传真接收使用方式(3已删除)
        /// </summary>
        public enum EOFRecvFaxIssueType : int { 待对应 = 0, 已对应 = 2, 接收 = 3, 接收可续用 = 1, 作废 = 4 };

        /// <summary>
        /// 传真短信收发类型
        /// </summary>
        /// created by：李明鸽　 2007-09-29
        public enum EOFSSendRecvType : int { 发出 = 1, 接收 = 2 };

        /// <summary>
        /// 订单通知传真情况
        /// </summary>
        public enum EOFSendStatus : int
        {
            无 = 0,
            待发送 = 1,
            发送成功 = 2,
            发送失败 = 3
        }

        /// <summary>
        /// 订单回传传真处理情况
        /// </summary>
        public enum EOFReceiveDealedFlag : int
        {
            未处理 = 0,
            已处理 = 1
        }
        /// <summary>
        /// 订单回传传真情况
        /// </summary>
        public enum EOFReceiveStatus : int
        {
            无回传 = 0,
            有回传 = 1
        }

        /// <summary>
        /// 传真短信处理状态
        /// created by：李明鸽　2007-09-29
        /// </summary>
        public enum EOFSIssueStatus : int
        {
            未实际发送 = 5,
            待发送 = 11,
            发送成功 = 12,
            发送失败 = 13,
            待处理 = 21,
            已处理 = 22,
            作废 = 90
        };

        /// <summary>
        /// 酒店结算传真状态
        /// created by：wuweijun　2011-03-01
        /// </summary>
        public enum EOBSendStatus : int
        {
            发送失败 = 0,
            发送成功 = 1,

            无 = 20,
            待发送 = 21
        }

        /// <summary>
        /// 酒店订单邮件状态
        /// </summary>
        public enum EOESendStatus : int
        {
            发送失败 = 0,
            发送成功 = 1,
        }
        #endregion

        #endregion

        #region 酒店产品字典
        //酒店状态
        public enum EHHotelStatus : int { 上网 = 1, 挂起 = 2, 黑名单 = 3, 下网 = 4, 非会员 = 5 };
        //预订状态
        public enum EHHotelBookStatus { 可预订 = 1, 可协调 = 2, 不可预订 = 3 };
        //推荐等级
        public enum EHHotelCommendLevel { 特推 = 1, 一级 = 2, 二级 = 3, 三级 = 4, 四级 = 5, 五级 = 6, 无主推 = 7 };

        /// <summary>
        /// 酒店星级
        /// create by zhb 2007-10-7
        /// </summary>
        public enum EHHotelGradeType : int { 五星级 = 1, 四星级 = 2, 三星级 = 3, 二星级 = 4, 公寓酒店 = 5, 青年旅社 = 6 };

        /// <summary>
        /// 酒店星级类型
        /// create by yanght 2008-5-31
        /// </summary>
        public enum EHotelGradeType : int { 标准星级 = 0, 准星级 = 1, 其它 = 2 };

        //酒店担保规则Ns赔偿
        public enum EHNSClaimType : int { 固定 = 1, 首晚 = 2, 全额 = 3 };

        //酒店预留房计算规则 
        public enum EHPreOccuptRoomUseRule { 只扣除首日间夜 = 1, 扣除全部在店间夜 = 2 };
        //接机类型
        public enum EHPickUpType { 不接机 = 0, 免费接机 = 1, 收费接机 = 3 };

        //价格类型
        public enum EHPriceType : int { 会员价 = 1, 代收代付价 = 2, 夜间特惠价 = 3, 团队价 = 4, 多天打包价 = 6, 协商价 = 90, 网上销售价 = 99, };
        //团队定义类型
        public enum EHGroupType : int { 房 = 1, 间夜 = 2, 人 = 3 };
        //返佣方式
        public enum EReCommisionType : int { 固定返佣 = 1, 差额返佣 = 2, 销售价百分比 = 3, 底价百分比 = 4, 净价百分比 = 5 };
        //酒店价格有效性
        public enum EHPriceValidity : int { 正常 = 1, 需实时协商 = 2, 不可预订 = 3 };
        //宾客类型
        public enum EHGuestType : int { 内宾 = 1, 外宾 = 2 };
        public enum EHALLGuestType : int { 内宾 = 1, 外宾 = 2, 内外宾 = 5 };

        //打包原则
        public enum EHPackagePriceUseRule : int { 要求入住期间全包含入价格期间 = 1, 仅要求入住首日全包含入价格期间 = 2 };

        //酒店房态更新方式
        public enum EHRoomStatusType : int { 买房 = 1, 占房 = 2, 预留房 = 3, 满房 = 4 };

        //酒店房态
        public enum EHRoomStatusDetail : int { 无房 = 0, 部分无房 = 1, 有房 = 2 };

        /// <summary>
        /// 酒店联系人业务性质
        /// </summary>
        /// create by zhb 2007-9-25
        public enum EHContactType : int { 前厅 = 1, 预订部 = 2, 商务中心 = 3, 订房中心 = 4, 财务 = 5, 销售 = 6, };

        public enum EHFaxNoType : int { 座机 = 1, 手机 = 2, 传真 = 3 };

        public enum EHFaxNoUsage : int { 其它 = 1, 确认 = 2, 夜审 = 3, 结算 = 4 };

        /// <summary>
        /// 酒店地标使用性质
        /// </summary>
        /// create by zhb 2007-9-28
        public enum EHMarkUseType : int { 公有 = 1, 私有 = 0 };

        /// <summary>
        /// 酒店确认规则的日期类型
        /// </summary>
        /// create by yanght 2007-10-18
        public enum EHConfirmRuleDateType : int { 平日 = 1, 周末 = 2 };

        /// <summary>
        /// 酒店结算规则的间夜归属
        /// </summary>
        /// create by Wusx 2007-11-26
        public enum EHFinanceRuleRmDy : int { 预订 = 1, 入住 = 2, 离店 = 3 };

        /// <summary>
        /// 酒店结算规则的对帐方式
        /// </summary>
        /// create by Wusx 2007-11-26
        public enum EHFinanceCheckType : int { 传真 = 1, 互联网 = 2 };

        /// <summary>
        /// 酒店结算规则的结算方式
        /// </summary>
        /// create by Wusx 2007-11-26
        public enum EHFinanceRulePeriodType : int { 按月结算 = 1, 按佣金结算 = 2, 按间夜结算 = 3 };

        /// <summary>
        /// 酒店结算规则的阶梯核算类型是否自动计算
        /// </summary>
        /// create by Wusx 2007-11-26
        public enum EHFinanceRuleIfIssueReward : int { 自动计算 = 1, 不自动计算 = 2 };

        /// <summary>
        /// 酒店结算规则的汇费计算方式
        /// </summary>
        /// create by Wusx 2007-11-26
        public enum EHFinanceRuleRemitChargeType : int { 固定收费 = 1, 按比例收费 = 2 };

        /// <summary>
        /// 酒店结算规则的发票要求
        /// </summary>
        /// create by Wusx 2007-11-26
        public enum EHFinanceRuleReceiptType : int { 先开发票 = 1, 后开发票 = 2, 开收据 = 3, 无票据 = 4 };

        /// <summary>
        /// 酒店预定代收代付规则
        /// </summary>
        /// create by yanght 2008-1-26
        public enum EHOPaymentType : int { 不支持 = 1, 可佣金冲抵房费 = 2, 需公司付款 = 3, 需提前付款 = 4 };


        /// <summary>
        /// 酒店阶梯政策累计规则 
        /// </summary>
        /// create by yanght 2008-1-26
        public enum EHOComdefAddRule : int { 期间每结算月 = 1, 期间累计 = 2, 季度累计 = 3 };


        /// <summary>
        /// 酒店阶梯政策警示规则 
        /// </summary>
        /// create by yanght 2008-1-26
        public enum EHOWarnType : int { 高于 = 1, 低于 = 2 };

        /// <summary>
        /// 酒店房型状态
        /// </summary>
        public enum EHRoomTypeStatus : int { 正常 = 0, 作废 = 1 };

        /// <summary>
        /// 酒店归属
        /// </summary>
        public enum EHotelBelong : int
        {
            金色世纪 = 0,
            航信 = 1,
            汇通天下 = 2,
            汇通天下_汇通天下自签酒店 = 3,
            旅行社 = 4,
            艺龙 = 5
        };
        #endregion

        #region 酒店订单字典

        public enum EHOAssignType : int { 确认处理 = 1, 无房找房 = 2, };

        //酒店确认员在线状态
        public enum EHOConfirmerStatues : int { 上线 = 1, 置忙 = 2, 外出 = 3, 下线 = 10 };

        //酒店订单产品类型
        public enum EHOProductType : int { 正常 = 1, 预留房 = 2, 买房 = 3, 免费房 = 4, TravelHub = 11 };

        /// <summary>
        /// 酒店订单产品使用情况核算类型
        /// </summary>
        public enum EHOCalprtyType : int { 新使用 = 1, 改变使用 = 2 };

        /// <summary>
        /// 酒店订单使用预留房的情况
        /// </summary>
        public enum EHOPreOccuptUseType : int { 未使用预留房 = 1, 部分使用预留房 = 2, 全部使用预留房 = 3 };

        /// <summary>
        /// 酒店订单占房标识
        /// </summary>
        public enum EHOoccupt_Type : int { 普通 = 1, 占房 = 2, 占房已使用 = 3 };

        /// <summary>
        /// 酒店订单付款方式
        /// </summary>
        public enum EHOPayType : int { 前台现付 = 1, 会员直接支付 = 2, 储值卡支付 = 3 };

        /// <summary>
        /// 酒店订单付款状况
        /// </summary>
        public enum EHOPayStatus : int { 已支付 = 1, 未支付 = 2 };

        /// <summary>
        /// 酒店订单通知酒店方式
        /// </summary>
        public enum EHOHotelConfirmType : int { 传真 = 1, Ebooking = 3, TravelHUB = 5 };

        /// <summary>
        /// 酒店订单是否直接确认
        /// </summary>
        public enum EHOHotelAutoConfirmFlag : int { 非直接确认 = 0, 直接确认 = 1, 书面确认 = 2, 口头确认 = 3 };

        /// <summary>
        /// 酒店订单通知客人方式
        /// </summary>
        public enum EHOInformType { 电话通知 = 1, 短信通知 = 2, 传真通知 = 3, Email通知 = 4, 代理商接口通知 = 5, 不用通知 = 20, };

        /// <summary>
        /// 酒店订单通知客人语言
        /// </summary>
        public enum EHOGuestInformLanguage { 中文 = 1, 英文 = 2 };

        /// <summary>
        /// 酒店反取消类型
        /// </summary>
        public enum EHOReCancelType : int { 反取消 = 1, 反审计 = 2 };

        /// <summary>
        /// 酒店担保原则的取消类型
        /// </summary>
        public enum EHAssureCancelType : int { 规定日期前取消 = 1, 规定提前几天几点前取消 = 2, 不允许取消 = 3 };

        /// <summary>
        /// 订单相关传真业务类型
        /// </summary>
        /// 创建人：李明鸽　 2007-09-29
        public enum EHOFaxType : int
        {
            其它 = 0,
            酒店预订通知单 = 111,
            酒店修改预订通知单 = 112,
            酒店取消预订通知单 = 113,
            酒店入住变更通知单 = 114,
            酒店审核入住通知单 = 121,
            酒店结算通知单 = 131,
            酒店预订公司担保函 = 181,
            酒店担保预订通知单 = 182,
            酒店预订公司转担保函 = 185,

            酒店入住客人通知单 = 211,
            酒店取消预订客人通知单 = 212,
            酒店预订预付担保通知函 = 221,
            酒店预订信用卡资料担保通知函 = 222,
            酒店预订确认回传单 = 311,
            酒店取消确认回传单 = 312,
            酒店审核入住回传单 = 321,
            酒店结算回传单 = 331,
            机票预订出票通知单 = 411,
            机票预订退票通知单 = 412,


        };

        /// <summary>
        /// 订单相关Email业务类型
        /// </summary>
        /// 创建人：李明鸽　 2007-09-29
        public enum EHOEmailType : int
        {
            其它 = 0,
            入住通知单 = 11,
            复核入住通知单 = 12,
            客人取消通知单 = 13
        };

        /// <summary>
        /// 订单大状态
        /// </summary>
        public enum EHOOrderStatus : int
        {
            未知 = 0,

            待确认 = 1,
            确认 = 100,
            通知 = 300,
            审核 = 500,
            完成 = 700,
            NoShow = 760,
            取消 = 800,
            作废 = 900
        };

        /// <summary>
        /// 订单大状态  外网使用 注：通知状态在外网不可见，归为确认状态
        /// </summary>
        public enum EHOOrderStatusWeb : int
        {
            待确认 = 1,
            确认 = 100,
            审核 = 500,
            完成 = 700,
            取消 = 800,
            作废 = 900
        };

        /// <summary>
        /// 酒店订单流程状态
        /// </summary>
        public enum EHOOrderFlowStatus : int
        {
            未知 = 0,

            待确认 = 1,
            预留单确认 = 50,
            真伪鉴别 = 100,
            协商价格 = 110,
            担保处理 = 120,
            担保确认 = 130,
            占房待使用 = 140,
            代收代付处理 = 150,
            失败处理 = 160,
            问题订单 = 170,
            无房找房 = 180,
            催单处理 = 190,
            延迟确认 = 200,
            通知客人 = 300,

            待审核 = 500,
            审核中 = 501,
            待复核 = 510,

            正常离店 = 700,
            客人LS = 710,
            酒店LS = 720,
            客人NS = 761,
            酒店NS = 762,

            待取消确认 = 800,
            取消 = 810,
            作废 = 900
        };


        /// <summary>
        /// 酒店订单担保方式
        /// </summary>
        public enum EHOGuarantyType : int
        {
            无担保 = 1,
            个人信用卡号担保 = 2,
            个人信用卡资料担保 = 3,
            个人预付款担保 = 4,
            个人向公司提供信用卡担保 = 5,
            公司直接为个人提供担保 = 6,
            公司转担保 = 10,
        };

        /// <summary>
        /// 酒店订单操作类型
        /// </summary>
        public enum EHOOperationType : int
        {
            真伪鉴别 = 1,

            生成订单 = 2,

            生成占房单 = 3,

            使用占房单 = 4,

            修改占房单 = 6,

            协商价格 = 100,

            预留单处理 = 180,
            确认处理 = 200,
            Ebooking确认 = 201,

            航信确认处理 = 203,
            重新确认处理 = 205,


            汇通接口确认处理 = 206,
            汇通接口重新确认处理 = 207,
            汇通接口担保处理 = 208,

            占房确认处理 = 210,

            取消确认处理 = 220,
            Ebooking取消确认 = 221,

            担保处理 = 231,
            担保确认 = 232,

            代收代付处理 = 240,
            代付处理 = 245,

            问题订单处理 = 251,
            无房找房处理 = 252,
            失败处理 = 253,
            延迟确认 = 254,

            航信无房找房处理 = 255,

            异常回复处理 = 270,
            催单处理 = 299,

            通知客人 = 300,
            跟单处理 = 310,

            审核入住 = 400,
            复核入住 = 401,
            入住修改 = 402,

            修改订单 = 600,

            取消订单 = 700,

            反取消 = 710,
            反审计 = 720,

            完成结算 = 800

        };

        /// <summary>
        /// 酒店订单任务类型
        /// </summary>
        ///创建人： wsx 2007-9-27 11:38
        public enum EHOTaskType : int
        {
            真伪鉴别 = 1,

            协商价格 = 100,

            预留单处理 = 180,
            确认处理 = 200,
            Ebooking确认 = 201,

            航信确认处理 = 203,
            重新确认处理 = 205,


            汇通接口确认处理 = 206,
            汇通接口重新确认处理 = 207,
            汇通接口担保处理 = 208,

            占房确认处理 = 210,

            取消确认处理 = 220,
            Ebooking取消确认 = 221,


            担保处理 = 231,
            担保确认 = 232,

            代收代付处理 = 240,
            代付处理 = 245,

            问题订单处理 = 251,
            无房找房处理 = 252,
            失败处理 = 253,
            延迟确认 = 254,

            航信无房找房处理 = 255,

            催单处理 = 299,

            通知客人 = 300,
            跟单处理 = 310,

            电话审核 = 401,
            EBooking审核 = 402,
            传真审核 = 403,
            EBooking复核 = 404,
            复核 = 405,
            电话复核 = 407,
            航信夜审 = 410,
            汇通夜审 = 412,
            航信复核 = 415,
            汇通复核 = 416,
            代理商复核 = 417,
        };


        /// <summary>
        /// 酒店订单任务类型是否需要独立操作
        /// </summary>
        ///创建人： wsx 2007-9-27 11:38
        public enum EHOTaskLockType : int
        {
            真伪鉴别 = 1,

            协商价格 = 1,

            预留单处理 = 1,
            确认处理 = 1,
            Ebooking确认 = 1,

            航信确认处理 = 1,
            重新确认处理 = 1,


            汇通接口确认处理 = 1,
            汇通接口重新确认处理 = 1,
            汇通接口担保处理 = 1,

            航信无房找房处理 = 1,

            占房确认处理 = 1,

            取消确认处理 = 1,
            Ebooking取消确认 = 1,

            担保处理 = 1,
            担保确认 = 1,

            代收代付处理 = 1,
            代付处理 = 1,

            问题订单处理 = 1,
            无房找房处理 = 1,
            失败处理 = 1,

            延迟确认 = 1,

            催单处理 = 0,

            通知客人 = 0,
            跟单处理 = 0,

            电话审核 = 1,
            EBooking审核 = 1,
            传真审核 = 1,
            EBooking复核 = 1,
            复核 = 1,
            电话复核 = 1,
            航信夜审 = 1,
            航信复核 = 1,
            汇通夜审 = 1,
            汇通复核 = 1,
            代理商复核 = 1
        };


        /// <summary>
        /// 酒店订单任务系统默认完成时间
        /// </summary>
        ///创建人： 李明鸽 2007-9-27 11:38
        public enum EHOTaskDefaultTime : int
        {
            真伪鉴别 = 5,

            协商价格 = 20,

            预留单处理 = 15,
            确认处理 = 15,
            Ebooking确认 = 15,

            航信确认处理 = 15,
            重新确认处理 = 15,

            汇通接口确认处理 = 15,
            汇通接口重新确认处理 = 15,
            汇通接口担保处理 = 15,

            航信无房找房处理 = 15,

            占房确认处理 = 15,

            取消确认处理 = 15,
            Ebooking取消确认 = 15,

            担保处理 = 20,
            担保确认 = 15,

            代收代付处理 = 15,

            代付处理 = 15,

            问题订单处理 = 15,
            无房找房处理 = 15,
            失败处理 = 15,

            催单处理 = 15,

            通知客人 = 15,
            跟单处理 = 15,

            延迟确认 = 15,

            电话审核 = 600,
            EBooking审核 = 600,
            传真审核 = 600,
            EBooking复核 = 600,
            复核 = 600,
            电话复核 = 600,
            航信夜审 = 600,
            航信复核 = 600,
            汇通夜审 = 600,
            汇通复核 = 600,
            代理商复核 = 600
        };

        /// <summary>
        /// 酒店订单任务最长锁定时间
        /// </summary>
        ///创建人： 李明鸽 2007-9-27 11:38
        public enum EHOTaskLockTime : int
        {
            真伪鉴别 = 20,

            协商价格 = 20,

            确认处理 = 20,
            Ebooking确认 = 20,

            航信确认处理 = 20,
            重新确认处理 = 20,


            汇通接口确认处理 = 20,
            汇通接口重新确认处理 = 20,

            航信无房找房处理 = 20,

            占房确认处理 = 20,

            取消确认处理 = 20,
            Ebooking取消确认 = 20,

            担保处理 = 20,
            担保确认 = 20,

            代收代付处理 = 20,
            代付处理 = 20,

            问题订单处理 = 20,
            无房找房处理 = 20,
            失败处理 = 20,

            催单处理 = 20,

            通知客人 = 20,
            跟单处理 = 20,

            电话审核 = 20,
            EBooking审核 = 20,
            传真审核 = 20,
            EBooking复核 = 20,
            复核 = 20,
            电话复核 = 20,
            航信夜审 = 20,
            航信复核 = 20,
            汇通夜审 = 20,
            汇通复核 = 20,
            代理商复核 = 20
        };

        /// <summary>
        /// 酒店订单任务最长延时时间
        /// </summary>
        ///创建人： 李明鸽 2007-9-27 11:38
        public enum EHOTaskDelayTime : int
        {
            真伪鉴别 = 5,

            协商价格 = 5,

            预留单处理 = 5,
            确认处理 = 5,
            Ebooking确认 = 5,

            航信确认处理 = 5,
            重新确认处理 = 5,

            汇通接口确认处理 = 5,
            汇通接口重新确认处理 = 5,
            汇通接口担保处理 = 5,

            航信无房找房处理 = 5,

            占房确认处理 = 5,

            取消确认处理 = 5,
            Ebooking取消确认 = 5,

            担保处理 = 5,
            担保确认 = 5,

            代收代付处理 = 5,

            代付处理 = 5,

            问题订单处理 = 5,
            无房找房处理 = 5,
            失败处理 = 5,

            催单处理 = 5,

            延迟确认 = 5,

            通知客人 = 5,
            跟单处理 = 5,

            电话审核 = 10,
            EBooking审核 = 10,
            传真审核 = 10,
            EBooking复核 = 10,
            复核 = 10,
            电话复核 = 10,
            航信夜审 = 10,
            航信复核 = 10,
            汇通夜审 = 10,
            汇通复核 = 10,
            代理商复核 = 10
        };

        /// <summary>
        /// 酒店订单任务锁定类型
        /// </summary>
        public enum EHOTaskLockStatus : int
        {
            正常 = 0,
            锁定 = 1,
            延时 = 2
        }

        /// <summary>
        /// 酒店订单占房使用规则
        /// </summary>
        /// 创建人：ZHB 2007-9-17 13:30
        public enum EHOOccupyUseRule : int { 普通单 = 0, 占房可修改 = 1, 占房不可修改 = 2, 占房已使用 = 3 };



        /// <summary>
        /// 订单类型规则
        /// </summary>
        /// 创建人:YHT 2011-03-25
        public enum EHOOrderTypeRule : int
        {
            普通单 = 0,
            占房单 = 1,
            航信单 = 2,
            汇通单 = 3,
            非会员酒店普通单 = 5,
            非会员酒店占房单 = 6,
            普通预留单 = 10,            //报表筛选使用固定值，不能更改（10，11）
            非会员酒店预留单 = 11,      //报表筛选使用固定值，不能更改（10，11）
            普通预留单已使用 = 20,
            非会员酒店预留单已使用 = 21,
        };

        /// <summary>
        /// 入住类型
        /// </summary>
        /// 创建人：ZHB 2007-9-17
        public enum EHOStayType : int { 正常 = 1, 续住 = 2, 换入 = 3, 加房 = 4, 换出 = 5 };

        /// <summary>
        /// 房间组类型
        /// </summary>
        /// 创建人：yanght 2007-10-7
        public enum EHORoomGroupType : int { 预订 = 1, 续住 = 2, 换房 = 3, 加房 = 4 };

        /// <summary>
        /// 入住结果
        /// </summary>
        /// 创建人：ZHB 2007-9-17 14：22
        public enum EHOStayResult : int { 未入住 = 0, 入住 = 1 };

        /// <summary>
        /// 酒店订单买房产品返佣类型
        /// </summary>
        public enum EHOBuyRoomCommType : int { 正常返佣 = 0, 不返佣 = 1 };

        /// <summary>
        /// 酒店对帐单对账标识
        /// </summary>
        /// 创建人：ZHB 2007-9-17
        public enum EHOCheckPaymentFlag : int { 未对帐 = 0, 已对帐 = 1 };

        /// <summary>
        /// 酒店分区类型
        /// </summary>
        /// zhb 2007-9-21
        public enum EHORegionType : int { 确认分区 = 1, 夜审分区 = 2, 无房找房分区 = 3, 酒店分区 = 4, 失败处理分区 = 5 };

        /// <summary>
        /// 行政区域级别
        /// </summary>
        /// zhb 2007-9-21
        /// wsx 修改2007-9-27
        public enum EHOCityLevel : int { 省市 = 1, 地级市 = 2, 县市 = 3, 乡镇 = 4 };

        /// <summary>
        /// 酒店确认规则分类
        /// </summary>
        /// zhb 2007-9-24
        public enum EHOHotelRuleType : int { 确认 = 1, 夜审 = 2 };

        /// <summary>
        /// 电话预订步骤
        /// </summary>
        /// yanght 2007-10-24
        public enum EHOReservFlow : int
        {
            酒店查询 = 1,
            列举酒店 = 2,
            选定酒店 = 3,
            有预留订房型 = 4,
            使用占房单 = 5,
            提交 = 6
        };
        /// <summary>
        /// 酒店取消原因
        /// </summary>
        /// yanght 2007-12-11
        public enum EHOCancelReason : int
        {
            占房取消 = 14,
            占房未使用 = 26,
            改订酒店 = 100,
            其它原因 = 200

        };

        /// <summary>
        /// 酒店帐单操作类型
        /// </summary>
        /// zhb 2007-9-24
        public enum EHOBillOperateType : int { 生成结算单 = 1, 重新生成 = 2, 对账确认 = 3, 反对账 = 4, 催帐 = 5, 核销 = 10, 坏账处理 = 20 };

        /// <summary>
        /// 结算单坏账
        /// </summary>
        public enum EHOBillBadType : int { 正常 = 0, 坏账 = 1 };

        //发送短信类型
        public enum EHOSMSType : int
        {
            网上订单受理通知 = 26,
            普通订单确认短信 = 27,
            提前预付或担保房确认短信 = 28,
            续住预订成功通知 = 29,
            担保处理的通知 = 30,
            通知预付 = 31,
            夜审续住价格变更通知 = 32,
            取消预订成功 = 33,
            NO_SHOW复核 = 35,
            LESS_SHOW复核 = 36,
            问题订单确认 = 38,
            无房通知 = 39,
            积分分红通知 = 40,
            提示短信114 = 251,
            跟单停止短信 = 260,
            核实名字 = 500,
            预订信息 = 501,
            预留不出票 = 502,
            出票短信 = 503,
            改期短信 = 504



        }


        //发送短信类型
        public enum EHOrderRelationType : int
        {
            改订酒店 = 1,
            转占房 = 2,
            使用占房 = 3
        }
        #endregion

        #region 酒店订单页面操作选项字典

        /// <summary>
        ///酒店订单真伪鉴别处理选项
        /// </summary>
        /// 2007-9-20 zhb
        public enum EHOVerifyResult : int { 正常 = 0, 完成 = 1, 延时 = 2, 正常并修改订单 = 3 };

        /// <summary>
        /// 酒店订单确认处理选项
        /// </summary>
        public enum EHOConfirmOption : int
        {
            确认订单可正常入住 = 1,
            转担保处理 = 2,
            确认无房 = 3,
            酒店无法满足客人特殊要求 = 4,
            延时 = 5,
            取消确认成功 = 6,
            延迟确认 = 7
        };

        /// <summary>
        /// 酒店订单确认方式
        /// </summary>
        public enum EHOConfirmMeans : int { 书面确认 = 1, 口头确认 = 2 };

        public enum EHONoRoomDealOption : int { 转无房找房 = 1, 转失败处理 = 2, 转航信无房找房 = 5 };

        public enum EHONotMatchDealOption : int { 转问题订单 = 1, 转无房找房 = 2, 转失败处理 = 3 };

        /// <summary>
        /// 酒店订单催单处理选项
        /// </summary>
        public enum EHOPushConfirmResultType : int { 确认回传 = 1, 停止催回传 = 2, 转问题订单 = 4, 延时 = 6, };

        /// <summary>
        /// 问题订单处理结果
        /// </summary>
        /// 2007-09-19 liulichuan
        public enum EHOQuestionIssueResultType : int { 已经通知客人直接确认 = 1, 转无房找房 = 2, 转担保处理 = 3, 延时 = 10 };

        //延迟确认处理结果
        public enum EHODelyConfirmType : int { 已经通知客人直接确认 = 1, 转无房找房 = 2, 转担保处理 = 3, 延时 = 10 };

        /// <summary>
        /// 酒店订单通知客人处理选项
        /// </summary>
        ///  2007-09-18 19:22 liulichuan
        public enum EHOGuestInformResultType : int { 成功通知 = 1, 停止通知 = 2, 延时 = 3 };

        /// <summary>
        /// 酒店订单跟单处理选项
        /// </summary>
        ///  2007-09-18 17:22 liulichuan
        public enum EHOTrackResultType : int { 停止跟单 = 1, 继续跟单 = 2, 延时 = 3 };

        /// <summary>
        /// 酒店订单确认选项
        /// </summary>
        ///  2007-10-16 16:00 liulichuan
        public enum EHOConfirmType : int
        {
            确认订单可正常入住 = 1,
            转担保处理 = 2,
            确认无房 = 3,
            酒店无法满足客人特殊要求 = 4,
            延时处理 = 5
        };

        /// <summary>
        /// 传真操作选项
        /// </summary>
        public enum EHOFaxOpreationType : int { 接收 = 1, 接收可续用 = 2, 作废 = 3, 返还 = 4 }

        /// <summary>
        /// 酒店阶梯政策日期类型
        /// </summary>
        /// 2007-11-20 yanght
        public enum EHCommDefDayType : int { 整期间 = 0, 年 = 1, 月 = 2, 周 = 3, 日 = 5 }
        /// <summary>
        /// 酒店阶梯政策数量类型
        /// </summary>
        /// 2007-11-20 yanght
        public enum EHCommDefNumType : int { 入住间夜数 = 1, 预订包数 = 2, 预订房费 = 3, 预订佣金 = 4 }

        public enum EHOBillStatus : int { 待发送 = 1, 待确认 = 2, 待回款 = 3, 已完成 = 4 };

        public enum EHOBillCheckStatus : int { 无回款 = 1, 累计中 = 2, 待回款 = 3, 部分回款 = 4, 全部回款 = 5 };

        public enum EHOOrderType : int { 系统订单 = 1, 航信订单 = 2 };

        /// <summary>
        /// 取消订单是否改定酒店
        /// </summary>
        /// 2011-02-24 wuweijun
        public enum EHOOrderIsReformulate : int { 已改 = 1, 未改 = 0 };

        public enum EHotelMailState : int { 待打印 = 1, 累积邮寄 = 2, 已打印 = 3, 确认寄出 = 4, 不开发票 = 5 };
        public enum EHotelMailOperType : int { 待打印 = 1, 累积邮寄 = 2, 已打印 = 3, 确认寄出 = 4, 不开发票 = 5, 邮寄导入 = 10, 手动录入 = 20, };

        public enum EHotelMailMode : int { 快递 = 1, 挂号 = 2 };
        #endregion

        #region 酒店夜审

        /// <summary>
        /// 酒店夜审操作选项
        /// </summary>
        /// create by : llc |2007/09/22 |
        public enum EHOCheckRoomDayResultType : int
        {

            在店 = 200,


            变更入住 = 301,
            LS待复核 = 302,
            NS = 303,

            酒店确认LS = 401,
            客人确认LS = 402,

            正常离店 = 600

        };

        /// <summary>
        /// 夜审类型
        /// </summary>
        /// create by :llc|2007/09/22|
        public enum EHOCheckRoomDayType : int { 初审 = 0, 复核 = 1 };

        /// <summary>
        /// 夜审模块
        /// </summary>
        public enum EHOCheckRoomDayMode : int { 电话审核 = 1, 酒店审核 = 2, 传真审核 = 3, Ebooking审核 = 4, 复核 = 5, 电话复核 = 6, 航信夜审 = 7, 航信复核 = 8, 代理商复核 = 9, 汇通夜审 = 11, 汇通复核 = 12 };

        /// <summary>
        /// 审核的方式
        /// </summary>
        /// create by :llc|2007/09/22|
        public enum EHOCheckRoomDayMeansType : int { 传真 = 1, 电话 = 2, Ebooking = 3, 其它 = 4 };

        /// <summary>
        /// 酒店订单房间审核状态
        /// </summary>
        ///创建人：李明鸽 2007-9-27 11:38
        public enum EHORoomStayType : int
        {

            待审 = 100,

            在店 = 200,

            LessShow待复核 = 302,
            NoShow待复核 = 303,

            正常离店待复核 = 304,
            变更入住 = 305,

            酒店确认LessShow = 401,
            客人确认LessShow = 402,

            酒店确认NoShow = 501,
            客人确认NoShow = 502,

            正常离店 = 600
        };

        /// <summary>
        /// 酒店订单间夜审核状态
        /// </summary>
        ///创建人：李明鸽 2007-9-27 11:38
        public enum EHORoomDayStayType : int
        {
            待审 = 1,

            入住 = 2,

            未入住 = 3
        };

        public enum EHOCheckRoomDayResult : int { 在店 = 1, NS = 2, 离店 = 3, 变更 = 4 };

        public enum EHOFaxCheckRoomDayResult : int
        {
            回夜审 = 0,
            在店 = 1,
            酒店确认LessShow = 2,
            客人确认LessShow = 3,
            酒店确认NoShow = 4,
            客人确认NoShow = 5,
            LessShow待复核 = 7,
            NoShow待复核 = 8,
            正常离店 = 10
        };

        /// <summary>
        /// 航信订单入住状态
        /// NNN：待审结；入住状态尚未确认
        /// INN：在入住；旅客入住中
        /// NML：入住正常；预订间夜数＝实际入住间夜数
        /// LES：LESSSHOW；实际入住间夜数 小于 预订间夜数
        /// MOR：延住；实际入住间夜数 大于 预订间夜数
        /// CAN：代理在入住日期之后才取消订单，如订单已进入结算系统后才取消订单
        /// NSH：NOSHOW
        /// </summary>
        public enum EHBE_CheckinInfo : int { NNN = -1, INN = 1, LES = 2, NSH = 4, MOR = 7, CAN = 8, NML = 10 };

        /// <summary>
        /// 航信订单NOSHOW状态，仅在Status为NSH时有意义
        /// 无法确认(NCC)；待追踪(WNN)；代理追踪(ANN)；未审核(NCK)；确认NS(CNS)；待确认(WCC)
        /// </summary>
        public enum EHBE_NoShowStatus : int { NCC = 0, WNN = 1, ANN = 2, NCK = 3, CNS = 4, WCC = 5 };

        #endregion

        #region 酒店第三方接口

        /// <summary>
        /// 接口主动通知类型
        /// </summary>
        ///
        public enum EHIOInformationType : int
        {
            酒店上下网状态通知 = 10,
            订单状态通知 = 20,
            房型修改通知 = 30,
            价格修改通知 = 40,
            房态通知 = 50
        };

        #endregion

        #region 酒店Ebooking

        #region Ebook登陆状态
        public enum EbookingLogType : int
        {
            登录 = 0,
            在线 = 1
        }
        #endregion

        #endregion

        #region 会员管理
        /// <summary>
        /// 支付帐号类型
        /// </summary>
        public enum ECPymtAccountType : int
        {
            信用卡 = 1,
            借记卡 = 2,
            paypal = 3,

        }
        /// <summary>
        /// 婚姻状况
        /// </summary>

        public enum ECMarriageStyle : int { 已婚 = 1, 未婚 = 2, 离异 = 3, 未知 = 0 };

        public enum ECustomerType : int { 网络会员 = 1, 分销商会员 = 2 };

        /// <summary>
        /// 会员联系方式类型
        /// </summary>          
        public enum ECContactType : int { 传真 = 4, 电子邮件 = 3, 手机 = 1, 座机 = 2 };
        /// <summary>
        /// 性别
        /// </summary>
        public enum ECSex : int { 女 = 1, 男 = 2, 未知 = 0 };

        /// <summary>
        /// 存留款原因类型
        /// </summary>
        public enum ECDepositType : int
        {
            使用存留款支付 = 1,
            订单退款 = 2,
            分红转存 = 3,
            欠服务费 = 4
        };

        /// <summary>
        /// 会员名字类别
        /// </summary>
        public enum ECNameType : int { 现用名 = 1, 曾用名 = 2 };

        /// <summary>
        /// 会员联系方式优先级
        /// create by zhb
        /// </summary>
        public enum ECContactMeansPri : int { 常用 = 1, 不常用 = 2 };

        /// <summary>
        /// 是否购买保险
        /// </summary>
        public enum ECIsInsurance : int { 是 = 1, 否 = 0 };

        /// <summary>
        /// 是否用于担保
        /// create by zhb
        /// </summary>
        public enum ECIsUsedInsurance : int { 是 = 1, 否 = 0 };
        /// <summary>
        /// 会员履历类别
        /// create by zhb
        /// </summary>
        public enum ECExprType : int { 受教育 = 1, 职业 = 2 };

        /// <summary>
        /// 担保渠道
        /// </summary>
        public enum ECInsChannel : int { 金色世纪 = 1, 其他 = 2 };
        //是否会员
        public enum ECIsMember : int { 是会员 = 1, 非会员 = 2 };

        /// <summary>
        /// 保单状态
        /// </summary>
        public enum ECGuaranteeStatus : int { 初始 = 1, 已售出 = 2, 已使用 = 3 };
        /// <summary>
        /// 保单录入类型
        /// </summary>
        public enum ECGuaranteeInputType : int { 单个录入 = 1, 批量录入 = 2 };

        /// <summary>
        /// 模板类型
        /// </summary>
        public enum ECTemplateType : int { 回访 = 1, 销售 = 2 };

        #region 预留款

        /// <summary>
        /// 常用账户类型
        /// </summary>
        public enum EDepositAccountType : int
        {
            分红金额 = 1,
            退票款 = 2,
            公司赔偿 = 3,
            会员预存 = 4,

        }

        /// <summary>
        /// 预留款变动类型
        /// </summary>
        public enum EDepositAlterationType : int
        {
            分红导入 = 1,
            订单退废 = 2,
            直接预存 = 3,
            流水帐户转入 = 4,
            公司赔偿 = 5,
            订单取消 = 6,
            预定机票 = 7,
            预定酒店 = 8,
            支付欠款 = 9,
            返现金 = 10,
            人工调整 = 11,
            销售回访 = 12
        }
        /// <summary>
        /// 支付记录中的支付状态
        /// </summary>
        public enum EDepositPayStatus : int
        {
            已支付 = 1,
            支付作废 = 2,
            支付退款 = 3,
            其他 = 4
        }
        /// <summary>
        /// 预留款业务 订单类型
        /// </summary>
        public enum EDepositBusinessOrderType : int
        {
            酒店 = 1,
            机票 = 2
        }

        /// <summary>
        /// 预留款业务类型
        /// </summary>
        public enum EDepositBusinessType : int
        {
            酒店 = 1,
            机票 = 2,
            返现 = 3,
            分红 = 4,
            其他 = 5,
            销售 = 6
        }

        /// <summary>
        /// 预留款业务审核状态 
        /// </summary>
        public enum EDepositBusinessState : int
        {
            未审核 = 1,
            己审核 = 2,
            审核失败 = 3
        }

        /// <summary>
        /// 锁定状态
        /// </summary>
        public enum EDepositLockState : int
        {
            锁定 = 1,
            解锁 = 2
        }

        /// <summary>
        /// 返现标记
        /// </summary>
        public enum EDepositCashFLag : int
        {
            可以返现 = 1,
            不能返现 = 2
        }

        /// <summary>
        /// 返现状态
        /// </summary>
        public enum EDepositCashState : int
        {
            未返现 = 1,
            己返现 = 2,
            取消返现 = 3
        }

        /// <summary>
        /// 返现审核状态
        /// </summary>
        public enum EDepositCashAuditState : int
        {
            申请 = 1,
            待返现 = 2,
            完成 = 3,
            失败 = 4,
            取消 = 5
        }

        /// <summary>
        /// 返现审核状态
        /// </summary>
        public enum EDepositAdjustStatus : int
        {
            待调整 = 1,
            已调整 = 2,
            不用调整 = 3
        }


        /// <summary>
        /// 明细变动类型
        /// </summary>
        public enum EDepositDetailChangeType : int
        {
            增加 = 1,
            减少 = 2
        }
        /// <summary>
        /// 预存方式
        /// </summary>
        public enum EDepositAdvanceChargeType : int
        {
            汇款 = 1,
            现金 = 2,
            招行POS机 = 3,
            交行POS机 = 4,
            移动POS = 5,
            快钱 = 6,
            网银在线 = 7,
            汇付天下 = 8,
            环讯 = 9,
            易宝 = 10,
            财富通 = 11,
            银联手机支付 = 12,
            网上支付 = 13,
            支票 = 14
        }

        /// <summary>
        /// 会员退钱方式
        /// </summary>
        public enum EDepositCashType : int
        {
            订单退款申请 = 1,
            预留款返现 = 2
        }

        /// <summary>
        /// 合作商自确认退款入预留款状态 
        /// added by yanwei in the2011-6-24 for DepositAmount   
        /// </summary>
        public enum EDepositCheckStatus : int
        {
            待处理 = 1,
            确认 = 2,
            有问题 = 3,
            取消入预留款 = 4
        }
        /// <summary>
        /// 结算单状态
        /// </summary>
        public enum EDepositBillStatus : int
        {
            初始化 = 0, 确认 = 1, 取消 = 2
        }

        /// <summary>
        /// 预留款返现失败原因
        /// </summary>
        public enum EDepositRefundCashReason : int
        {
            姓名有误 = 1,
            银行账号有误 = 2,
            开户行有误 = 3,
            金额有误 = 4
        }

        /// <summary>
        /// 生成返现单状态
        /// </summary>
        public enum EDepositProductState : int
        {
            待处理 = 1,
            完成 = 2
        }

        /// <summary>
        /// 返现单提交次数
        /// </summary>
        public enum EDepositProductSubmitType : int
        {
            第一次提交 = 1,
            再次提交 = 2
        }

        /// <summary>
        /// 操作类型
        /// </summary>
        public enum EDepositRefundCashOperationType : int
        {
            申请 = 1,
            待返现 = 2,
            再次提交 = 3,
            完成 = 4,
            失败 = 5,
            取消 = 6
        }
        #endregion
        #endregion

        #region 卡管理
        /// <summary>
        /// 会员卡功能类型
        /// 创建:李明鸽　2007-10-07
        /// </summary>
        public enum EMCPurposeType : int { 储值卡 = 1, 功能卡 = 2 };

        /// <summary>
        /// 会员卡是否可以使用卡内金额
        /// 创建:李明鸽　2007-10-07
        /// </summary>
        public enum EMCSelfPayFlag : int { 可以使用卡内金额 = 1, 不可以使用卡内金额 = 2 };
        /// <summary>
        ///发行方式
        /// </summary>
        public enum EFPIssueType : int { 公司发行 = 0, 联名公司发行 = 1, 其它公司发行 = 2 }

        /// <summary>
        /// 会员卡号结尾方式
        /// </summary>
        public enum EMCCardNoEndType : int { 固定 = 1, 随机 = 2, 校验 = 3, 无结尾 = 4 };

        /// <summary>
        /// 会员卡号生成方式
        /// </summary>
        public enum EMCCardNoGenerationType : int { 规则生成 = 1, 文件生成 = 2 };

        /// <summary>
        /// 会员卡密码生成方式
        /// </summary>
        public enum EMCCardPWDGenerationType : int { 固定 = 1, 随机 = 2 };
        /// <summary>
        /// 会员卡状态
        /// </summary>
        public enum EMCCardStatus : int { 待制卡 = 1, 已制卡 = 2, 已出库 = 3, 已售出 = 4, 作废 = 5 };

        /// <summary>
        /// 会员卡使用状态
        /// </summary>
        public enum EMCCardUsedStatus : int { 售出 = 0, 激活 = 1, 正常使用 = 2 };

        //会员卡过期时间类型
        public enum EMCInvalidTimeType : int { 年 = 1, 月 = 2, 日 = 3 };
        //卡增值服务类型
        public enum EMCAddServiceTypeName : int { 三百万保险 = 1, 汽车救援 = 2, 贵宾厅服务 = 3, 代金券 = 4, 健康挂号 = 5, 心理咨询 = 6, 累计310万交通意外 = 7, 累计400万交通意外险 = 8, 免费法律咨询 = 9, 法律无忧 = 10 };

        //卡预订服务类型
        public enum EMCGCServiceDesc : int { 酒店预订 = 1, 机票预订 = 2, 餐饮预订 = 3, 高尔夫预订 = 4, 旅游度假预订 = 5, 汽车保险预订 = 6 };
        /// <summary>
        /// 领卡方式
        /// </summary>
        public enum EMCCardOutType : int { 先交款后提卡 = 1, 先提卡后交款 = 2, 赠卡 = 3 };
        /// <summary>
        /// 开卡规则
        /// </summary>
        public enum EMCInitRule : int { 有预订 = 1, 提交资料 = 2, 售出 = 3 };

        /// <summary>
        /// 卡关联关系
        /// </summary>
        public enum EMCCardRalationType : int { 管辖卡 = 0, 售出卡 = 1 };

        /// <summary>
        /// 卡充值方式
        /// </summary>
        public enum EMCCardRechargeType : int { 现金 = 1, 走帐 = 2 };

        /// <summary>
        ///  是否是旅通卡
        /// </summary>
        public enum EMCCardTypeFlag : int { 旅通卡 = 1052 };

        #endregion

        #region 员工管理

        /// <summary>
        /// 在职状态
        /// </summary>
        public enum EStatus : int { 在职 = 1, 离职 = 2, 未知 = 0 };

        #endregion

        #region 机票产品
        /// <summary>
        /// 航空公司类型
        /// </summary>
        /// Created By:sunbin 2007-10-12 
        /// 
        public enum ETAirLinerType : int { 国外 = 1, 国内 = 0 };

        /// <summary>
        /// 是否电子客票
        /// Created By:sunbin 2007-10-12 
        /// </summary> 
        public enum ETETicketType : int { 是 = 1, 否 = 0 };

        /// <summary>
        /// 是否启用
        /// Created By:sunbin 2007-10-12 
        /// </summary>
        public enum ETIsUsed : int { 是 = 1, 否 = 0 };

        /// <summary>
        /// 是否特价
        /// Created By:sunbin 2007-10-12 
        /// </summary>
        public enum ETCabinType : int { 特价舱 = 1, 正常舱 = 0 };

        /// <summary>
        /// 舱位等级
        /// Created By:sunbin 2007-10-12 
        /// </summary>
        public enum ETCabinGrade : int { 头等舱 = 1, 公务舱 = 2, 经济舱 = 3, 高特F = 4, 高特C = 5, 超值经济舱 = 6, 特价经济舱 = 7 };

        /// <summary>
        /// 送票状态
        /// Created By:sunbin 2007-10-12 
        /// </summary>
        public enum EPStatus : int { 在职 = 0, 离职 = 1 };

        /// <summary>
        /// 送票交通工具
        /// Created By:sunbin 2007-10-12 
        /// </summary>
        public enum EPVehicle : int { 摩托车 = 0, 公交车 = 1, 自行车 = 2, 电动车 = 3 };

        /// <summary>
        /// 合作商等级
        /// Created By:sunbin 2007-10-23 
        /// </summary>
        public enum ETAgentClass : int { 公司内部 = 1, 一级代理商 = 2, 二级代理商 = 3, 三级代理商 = 4, 四级代理商 = 5, 五级代理商 = 6 };

        /// <summary>
        /// 配票等级 added by tianlei 2009-2-23
        /// </summary>
        public enum ETParnterAssignClass : int { 一级 = 1, 二级 = 2, 三级 = 3 };

        public enum ETAgentStatus : int { 启用 = 1, 停止 = 0 };

        /// <summary>
        /// 是否可以打票
        /// Created By:sunbin 2007-10-23 
        /// </summary>
        public enum ETIsSupportIssue : int { 可以打票 = 1, 不能打票 = 0 };

        /// <summary>
        /// 出票途径
        /// Created By:sunbin 2007-10-23 
        /// </summary>
        public enum EIssuePath : int { BSP = 1, B2B = 2, 外购 = 3 };

        /// <summary>
        /// Pat状态
        /// Created By:liyao 2008-11-23 
        /// </summary>
        public enum EPatStatus : int { 有效 = 2, 无效 = 3, 不确定 = 4 };

        #region add by yanwei
        /// <summary>
        ///政策类型  add by yanwei 2010-09-25
        /// </summary>
        public enum EPloicyType { 比例 = 1, 金额 = 2 }

        /// <summary>
        /// 合作商联系电话用途 add by yanwei 2011-3-9 
        /// </summary>
        public enum EPartnerPhoneFucntionType { 挂起联系电话 = 1, 解挂联系电话 = 2, 跟单联系电话 = 3, 全业务联系电话 = 4 }

        /// <summary>
        /// 权限分配的功能类型  add by yanwei 2011-3-9
        /// </summary>
        public enum EPartnerAssignFucntionType { /*合作商任务 = 1, , 跟单任务 = 3*/挂起监控任务 = 2, 特价申请订单任务 = 4 }
        #endregion

        /// <summary>
        /// 机票的最低价核对__核实结果
        /// Created by:ZhangJiatong 2011-01-10
        /// </summary>
        public enum EFlightCompare : int { 未核实 = 0, 产品维护错误 = 1, 系统计算错误 = 2, 产品差异 = 3, 产品优势 = 4, 系统AV错误 = 5, 三方AV错误 = 6, 无法重现 = 7, 其他 = 8 }

        /// <summary>
        /// 机票的最低价核对__自动核实结果
        /// Created by:ZhangJiatong 2011-01-13
        /// </summary>
        public enum EFlightAutoCompare : int { 有差异 = 0, 无差异 = 1 }


        /// <summary>
        /// 机票的最低价核对__价格来源
        /// Created by:ZhangJiatong 2011-01-10
        /// </summary>
        public enum ETicketPriceOrigin : int { 携程 = 1, 艺龙 = 2, 非常旅行 = 3 }

        /// <summary>
        /// 机票的最低价核对_舱位选择
        /// Created by:ZhangJiatong 2011-01-20
        /// </summary>
        public enum EFlightCabin : int { 相同舱位 = 0, 不同舱位 = 1 }

        #region 航信查询日志
        #region 执行状态
        /// <summary>
        /// 执行状态
        /// </summary>
        public enum EExcuteState : int
        {
            成功 = 1,
            失败 = 2,
            异常 = 3
        }
        #endregion

        #region 操作类型
        /// <summary>
        /// 操作类型
        /// </summary>
        public enum EOperationType : int
        {
            ABE = 1,
            IBE = 2,
            Cache = 3,
            其它 = 4
        }
        #endregion
        #endregion

        #endregion

        #region 机票订单
        /// <summary>
        /// added by liyao 2007-10-23
        /// 机票订单状态
        /// </summary>
        public enum ETicketOrderStatus : int { 待订 = 0, 预订 = 1, 自出票 = 2, 外出票 = 3, 转领班 = 4, 派单 = 5, 完成 = 6, 取消 = 7 }

        /// <summary>
        /// added by sunbin 2008-2-18  外网使用
        /// 机票订单状态
        /// </summary>
        public enum ETicketOrderStatusWeb : int { 待订 = 0, 预订 = 1, 待出票 = 2, 完成 = 6, 取消 = 7 }

        /// <summary>
        /// added by liyao
        /// 机票订单乘客类型
        /// </summary>
        public enum ETicketOrderPassengerType : int { 成人 = 0, 成人特价 = 1, 儿童 = 2, 婴儿 = 3 }
        /// <summary>
        /// 配送方式
        /// </summary>
        public enum ETicketSendType : int { 机场取票 = 1, 市区送票 = 2, 邮寄客票 = 3, 无须送票 = 4 }

        /// <summary>
        /// added by liyao 2007-10-23
        /// 退改签类型
        /// </summary>
        public enum ETicketChangeType : int { 退票 = 1, 废票 = 2, 改签 = 3 }

        /// <summary>
        /// added by liyao 2008-07-04
        /// 退改签审核状态
        /// </summary>
        public enum ETicketBackStatu : int { 待配送 = 1, 待黑屏 = 2, 完成 = 3 }

        /// <summary>
        /// added by liyao 2007-10-24
        /// 机票订单操作记录类型
        /// </summary>
        public enum ETicketOrderOperationTye : int
        {
            预订 = 1, 内出票 = 2, 外出票 = 3, 分配出票 = 4, 转领班 = 5, 退 = 6, 废 = 7, 改签 = 8, 取消 = 9, 修改订单 = 10,
            领班分配机票 = 11, 退票完成 = 12, 废票完成 = 13, 取消退废 = 14, 修改退废 = 15, 订单支付 = 16, 订单收银 = 17,
            待出票 = 18, 退废配送 = 19, 退废执行 = 20, 支付方式修改 = 21, 出票点修改 = 22, 修改pnr = 23, 修改配送方式 = 24,
            修改期望出票时间 = 25, 出保 = 26, 挂起 = 27, 解挂 = 28, 现金退款 = 29, 修改代金券 = 30, 待担保 = 31, 收回行程单 = 32,
            保留行程单 = 33
        }

        /// <summary>
        /// added by liyao 2007-10-29
        /// 机票订单收银状态
        /// </summary>
        public enum ETicketOrderPaymentStatus : int { 未支付 = 1, 已支付 = 2 }

        /// <summary>
        /// added by liyao 2007-10-29
        /// 机票订单任务类型
        /// </summary>
        public enum ETicketOrderTaskType : int { 待出订单 = 1, 自出票 = 2, 外出票 = 3, 领班管理 = 4, 待配票 = 5, 总部收款 = 6, 出票点自收 = 7, 出票点代收 = 8 }

        /// <summary>
        /// added by liyao 2007-10-30
        /// 机票订单支付类型
        /// </summary>
        public enum ETicketOrderPayType : int
        {
            未知 = 0, 公司会员存款支付 = 1, 公司储值卡支付 = 2, 公司信用卡支付 = 3, 公司网上支付 = 4, 公司邮局汇款 = 5,
            公司现金 = 6, 公司手机支付 = 7, 公司汇至银行卡 = 8, 公司电话支付 = 9, 公司汇至对公账户 = 10,
            出票点自收现金 = 11, 出票点自收汇款 = 12, 出票点代收现金 = 13, 出票点代收汇款 = 14
        }

        /// <summary>
        /// added by liyao 2007-10-30
        /// 机票订单支付任务类型
        /// </summary>
        public enum ETicketOrderPayTaskType : int { 总部收款 = 1, 出票点自收 = 2, 出票点代收 = 3 }

        /// <summary>
        /// added by liyao 2008-1-14
        /// 机票订单支付 汇款类型
        /// </summary>
        public enum ETicketOrderJSJRemitType : int { 邮局汇款 = 1, 汇至银行卡 = 2, 汇至对公帐户 = 3 }

        /// <summary>
        /// added by liyao 2008-1-14
        /// 机票订单支付金额变化原因
        /// </summary>
        public enum ETicketOrderPayChangeType : int { 生成订单 = 1, 取消订单 = 2, 机票退废 = 3, 转欠款 = 4, 修改订单平账 = 5, 修改预留款平账 = 6 }

        /// <summary>
        /// added by liyao 2008-1-17
        /// 代收款结算单状态
        /// </summary>
        public enum ETicketConsignBillStatu : int { 初始化 = 1, 确认 = 2, 取消 = 3 }

        /// <summary>
        /// added by liyao 2008-2-2
        /// 关联定单类型
        /// </summary>
        public enum ETicketRelationType : int { 往返 = 1, 联程 = 2 }

        /// <summary>
        /// added by liyao 2008-2-3
        /// 航程类型
        /// </summary>
        public enum ETicketTripType : int { 单程 = 1, 往返 = 2, 中转联程 = 3, 联程 = 4 }

        /// <summary>
        /// added by liyao 2008-2-18
        /// 网上订单支付方式
        /// </summary>
        public enum ETicketNetPayType : int { 票到付款 = 1, 网络支付 = 2 }

        /// <summary>
        /// added by liyao 2008-2-18
        /// 网上订单支付结果
        /// </summary>
        public enum ETicketNetPayResult : int { 未知 = 0, 失败 = 1, 成功 = 2 }

        /// <summary>
        /// added by liyao 2008-3-24
        /// 配送结果
        /// </summary>
        public enum ETicketDeliverResult : int { 待配送 = 0, 配送中 = 1, 已送达 = 2, 变更地址 = 3, 变更时间 = 4, 机场自取 = 5, 联系不到 = 6, 改邮寄 = 7 }

        /// <summary>
        /// added by rxh 2009-11-4
        /// 配送状态
        /// </summary>
        public enum ETicketDeliverAssignState : int
        {
            待配送 = 0,
            配送中 = 1,
            完成 = 2,
            未送达 = 3
        }

        /// <summary>
        /// added by liyao 2008-3-24
        /// 退改签类型
        /// </summary>
        public enum ETicketOrderType : int { 正常订单 = 1, 退废票 = 2 }

        /// <summary>
        /// added by liyao 2008-6-18
        /// 合作商自收款金额调整
        /// </summary>
        public enum ETicketPriceAdjustType : int { 代金券调整 = 1, 票面价调整 = 2, 机建燃油调整 = 3, 其他 = 4 }

        /// <summary>
        /// 混合支付状态
        /// added by liyao 2009-02-18
        /// </summary>
        public enum ETicketMixPayStatus : int { 待处理 = 1, 完成 = 2, 无 = 0 }

        /// <summary>
        /// added by liyao 2009-03-30
        /// 订单是否是银联借记卡
        /// </summary>
        public enum ETicketOrderUnionPay : int { 否 = 0, 是 = 1 }

        /// <summary>
        /// 订单挂起类型
        /// </summary>
        public enum ETicketOrderHangupType : int
        {
            合作商挂起 = 1,
            解挂 = 2,
            公司挂起 = 3
        }

        /// <summary>
        /// 保险出保
        /// </summary>
        public enum ETicketInsuranceState : int
        {
            未出保 = 0,
            出保 = 1
        }

        #region add by yanwei 2010-8-26
        /// <summary>
        /// 跟单方式
        /// </summary>
        public enum TicketTraceType
        {
            /// <summary>
            /// 系统跟单
            /// </summary>
            TraceSystem = 1,
            /// <summary>
            /// 手工一次跟单
            /// </summary>
            TraceHand = 2
        }
        /// <summary>
        /// 手工跟单类型
        /// </summary>
        public enum TicketTraceHand
        {
            /// <summary>
            /// 默认
            /// </summary>
            TraceDefault = 0,
            /// <summary>
            /// 二次跟单
            /// </summary>
            TraceSecond = 1,
            /// <summary>
            /// 待后台处理
            /// </summary>
            TraceBackstage = 2
        }

        /// <summary>
        /// 跟单结果
        /// </summary>
        public enum TicketTraceResult
        {
            /// <summary>
            /// 跟单成功
            /// </summary>
            Success = 1,
            /// <summary>
            /// 跟单失败
            /// </summary>
            Failure = 2
        }


        /// <summary>
        /// 锁的类型
        /// </summary>
        public enum ETicketLockType { 出票锁定 = 1, 挂起锁定 = 2, 解挂锁定 = 3 }
        /// <summary>
        /// 完成状态
        /// </summary>
        public enum ETicketLockStatus { 未完成 = 1, 完成 = 2 }
        /// <summary>
        /// 操作类型
        /// </summary>
        public enum ETicketLockOperType { 解锁 = 1, 强行解锁 = 2, 过期解锁 = 3 }


        #endregion

        #region added by rxh 2011-03-15 (待处理订单类型)
        /// <summary>
        /// 待处理订单类型
        /// </summary>
        public enum EPrecedingRemainType : int
        {
            折扣待定 = 1,
            时间待定 = 2,
            行程待定 = 3,
            证件待定 = 4,
            姓名待定 = 5,
            不用联系客人等通知 = 6,
            其它 = 7
        }
        #endregion


        #region 特价机票申请 add by liutong 2011-10-12
        /// <summary>
        /// 出票渠道
        /// </summary>
        public enum ESpecilIssuePath
        {
            黑屏明折明扣 = 1,
            航空K位 = 2,
            航空官网 = 3,
            包机 = 4,
            东航畅行卡 = 5,
            国航商旅卡 = 6
        }

        /// <summary>
        /// 政策类型
        /// </summary>
        public enum ECommdefType
        {
            普通政策 = 1,
            特价政策 = 2,
            K位政策 = 3,
            特价参考政策 = 4,
            无特价参考申请政策 = 5
        }

        /// <summary>
        /// 外购付款方式
        /// </summary>
        public enum EOutPaymentType
        {
            招行一网通 = 1,
            国航商旅卡 = 2,
            东航畅行E卡 = 3
        }

        /// <summary>
        /// K位类型	
        /// </summary>
        public enum EKseatType
        {
            自动K位 = 1,
            人工K位 = 2
        }

        /// <summary>
        /// add by whn 2011-10-11
        /// 特价申请订单申请状态   
        /// </summary>
        public enum ETicketSpecialApplyStatus : int
        {
            待申请 = 1,
            出票成功 = 2,
            申请成功未出票 = 3,
            未申请成功 = 4
        }
        #endregion

        #endregion

        #region 机票自动出票
        /// <summary>
        /// 出票途径
        /// added by yanwei 2010-10-26
        /// </summary>
        public enum TicketAutoInterFaceType : int { 汇付天下 = 1, 支付宝 = 2, 百拓 = 4, 黑屏 = 5, 财付通 = 6, 其他 = 10, }

        /// <summary>
        /// 自动出票状态
        /// added by liyao 2010-10-27
        /// </summary>
        public enum TicketAutoStatus : int { 初始化 = 1, 出票中 = 2, 失败 = 3, 完成 = 4 }

        /// <summary>
        /// 自动出票模式
        /// added by liyao 2010-10-27
        /// </summary>
        public enum TicketAutoModel : int { 自动 = 1, 手工 = 2 }
        /// <summary>
        ///机票统计日期类型  add by yanwei 2010-10-09
        /// </summary>
        public enum TicketCountDateType : int { 出票日期 = 1, 乘机日期 = 2 }

        /// <summary>
        /// 机票出票支付方式 add by zhangjiatong 2011-03-11
        /// </summary>
        public enum TicketPayMent : int { 信用支付 = 1, 现金支付 = 2 }

        #endregion

        #region 机票自动出保
        /// <summary>
        /// 出保使用状态
        /// add by ZhangJiatong 2011-04-01
        /// </summary>
        public enum TicketAutoInsuranceUseState : int { 已使用 = 1, 空闲 = 2 }

        /// <summary>
        /// 自动出保结算单状态
        /// add by ZhangJiatong 2011-04-12
        /// </summary>
        public enum TicketAutoInsuranceAccountState : int { 初始化 = 0, 确认 = 1, 取消 = 2 }

        /// <summary>
        /// 自动出保对数量的操作
        /// add by ZhangJiatong 2011-04-13
        /// </summary>
        public enum TicketAutoInsureCountAddReduce : int { 增加 = 1, 减少 = 2 }
        /// <summary>
        /// 出保模式
        /// </summary>
        public enum TicketAutoInsuranceMode : int
        {
            手工 = 1,
            自动 = 2,
            无需 = 3
        }

        /// <summary>
        /// 出保结果
        /// </summary>
        public enum TicketAutoInsuranceResult : int
        {
            未知 = 0,
            成功 = 1,
            失败 = 2
        }

        /// <summary>
        /// 出保状态
        /// </summary>
        public enum TicketAutoInsuranceState : int
        {
            待出保 = 1,
            出保中 = 2,
            已出保 = 3,
            失败 = 4,
            等待二次 = 5
        }

        /// <summary>
        /// 失败原因
        /// </summary>
        public enum TicketAutoInsuranceReason : int
        {
            接口异常 = 1,
            身份证解析失败 = 2,
            年龄过保 = 3,
            退废票 = 4,
            信息不全 = 5,
            多航段 = 6,
            在有效期内 = 7,
            其它 = 8
        }
        #endregion

        #region 机票现金管理
        /// <summary>
        /// 机票收款业务类型
        /// Created By:liyao 2009-10-26 
        /// </summary> 
        public enum ETTicketCashBusinessType : int { 收款 = 1, 退款 = 2 };

        /// <summary>
        /// 机票收款类型
        /// Created By:liyao 2009-10-26
        /// </summary> 
        public enum ETTicketCashType : int { 现金 = 1, 移动pos = 2, 支票 = 3 };

        /// <summary>
        /// 机票退款申请状态
        /// Created By:liyao 2009-10-26
        /// </summary> 
        public enum ETTicketBackCashRequestStatu : int { 申请 = 1, 完成 = 2, 取消 = 3 };

        /// <summary>
        /// 机票退款请款申请单状态
        /// Created By:liyao 2009-10-26
        /// </summary> 
        public enum ETTicketRefundRequestStatu : int { 申请 = 1, 确定 = 2, 完成 = 3, 取消 = 4 };

        /// <summary>
        /// 机票差异状态
        /// Created By:rxh 2009-12-2
        /// </summary> 
        public enum ETTicketCollectDifferentStatu : int { 申请 = 1, 确定 = 2, 完成 = 3, 取消 = 4 };

        /// <summary>
        /// 缴款单状态
        /// Created By:liyao 2009-10-26
        /// </summary> 
        public enum ETTicketPaycashBillStatu : int { 申请 = 1, 部分核销 = 2, 完全核销 = 3, 撤销 = 4 };

        /// <summary>
        /// 缴款单明细状态
        /// Created By:liyao 2009-10-26
        /// </summary> 
        public enum ETTicketPaycashStatu : int { 待审核 = 1, 已审核 = 2, 审核失败 = 3, 再次审核 = 4 };

        /// <summary>
        /// 差异说明类型
        /// Created By:liyao 2009-11-20
        /// </summary> 
        public enum ETTicketCashDiffType : int { 记录客人欠款 = 1, 公司损失 = 2, 进会员预留款 = 3, 其他 = 4 };

        /// <summary>
        /// 缴存状态名称
        /// created by:rxh 2009-12-10
        /// </summary>
        public enum ETicketCashDepositedStatu : int { 未缴 = 0, 待审核 = 1, 已审核 = 2, 审核失败 = 3, 再次审核 = 4 }

        /// <summary>
        /// 票款缴存监控类型
        /// created by:rxh 2009-12-10
        /// </summary>
        public enum ETicketCashCaptureWatchType : int
        {
            已配送未收款 = 1,
            已收款未缴 = 2,
            缴款失败 = 3,
            已收款已缴 = 4,
            应收未收 = 5,
            未配送未收款 = 6,
            已缴未审核 = 7,
            已缴已审核 = 8
        }

        #endregion

        #region 财务结算
        /// <summary>
        /// 财务处理款项类型
        /// </summary>
        public enum EFPaymentType : int { 收款 = 1, 付款 = 2 };

        /// <summary>
        /// 开票状态
        /// </summary>
        public enum EFRcptStatusType : int { 无需开票 = 1, 待开发票 = 2, 已开发票 = 3, 已开收据 = 4 };

        /// <summary>
        /// 发票状态
        /// </summary>
        public enum EFRpirStatusType : int { 正常 = 0, 作废 = 1, 重复 = 2 };

        /// <summary>
        /// 款项状态
        /// </summary>
        public enum EFPymtStatusType : int { 未核销 = 1, 部分核销 = 2, 完全核销 = 3 };

        /// <summary>
        /// 合作商类型
        /// </summary>
        public enum EFPartnerType : int { 酒店合作商 = 1, 机票合作商 = 2, 餐饮合作商 = 3 };

        /// <summary>
        /// 佣金核销调整类型
        /// create by zhb
        /// </summary>
        public enum EHFCommAdjustType : int { 盈余 = 1, 损失 = 2, 冲佣 = 3, 坏账 = 4 };

        /// <summary>
        /// added by wushuxian
        /// 机票订单支付类型
        /// </summary>
        public enum EMemberPayType : int
        {
            储值卡支付 = 1, 网银在线 = 2, 招行POS机 = 3,
            银联POS机 = 4, 农行POS机 = 5, 中行POS机 = 6, 民生POS机 = 7, 交行POS机 = 8, 李董农行卡 = 9, 汇付天下 = 10, 快钱 = 11, 现金 = 12,
            汇付天下_信用卡支付 = 13, 银联卡_电话支付 = 14, 财富通 = 15, 快钱_信用卡 = 16, 网银在线_信用卡 = 17, 网银在线_网上支付 = 18,
            环讯_信用卡 = 19, 易宝_信用卡 = 20

        };

        /// <summary>
        /// 借记卡支付状态
        /// added by liyao 2009-03-30
        /// </summary>
        public enum EUnionPayStatus : int { 初始化 = 1, 待发送 = 2, 发送中 = 3, 成功 = 4, 失败 = 5, 异常 = 6, 取消 = 7 };

        /// <summary>
        /// 借记卡支付订单类型
        /// added by liyao 2009-03-31
        /// </summary>
        public enum EUnionPayOrderType : int { 机票 = 1, 酒店 = 2, 其他 = 3 };

        #endregion

        #region 信用卡类型

        /// <summary>
        ///是否可公司刷卡
        /// </summary>
        public enum EFPAcceptable : int { 是 = 1, 否 = 0 };

        #endregion

        #region 会员调查任务
        /// <summary>
        ///调查状态
        /// </summary>
        public enum EFPVoteStatus : int { 待发布 = 1, 发布 = 2, 完成 = 3 }

        #endregion

        #region 会员调查任务项目
        /// <summary>
        ///项目类型
        /// </summary>
        public enum EFPVoteItem : int { 调查条目 = 1, 归类条目 = 0 }

        #endregion

        #region 会员回访相关
        /// <summary>
        ///项目类型
        /// </summary>
        public enum EFPTemplateItemType : int { 回访条目 = 1, 归类条目 = 0 }

        /// <summary>
        /// 回访任务状态
        /// </summary>
        public enum ECBKTaskStatus : int { 待分配 = 0, 分配中 = 1, 分配完成 = 2, 进行中 = 3, 结束 = 4 }

        /// <summary>
        /// 回访电话状态
        /// </summary>
        public enum ECBKTelStatus : int { 待回访 = 0, 未接 = 1, 占线 = 2, 空号 = 3, 号码错误 = 4, 接通 = 5, 停机 = 6, 无法接通 = 7, 关机 = 8, 稍后联系 = 9, 秘书服务 = 10 }

        /// <summary>
        /// 回访结果分类
        /// </summary>
        public enum ECBKStatus : int { 待回访 = 0, 接受回访 = 1, 拒绝回访 = 2, 失败回访 = 3 }

        /// <summary>
        /// 回访完成类型
        /// </summary>
        public enum ECBKFinishStatus : int { 未结束 = 0, 完成回访 = 1, 终止回访 = 2 }
        #endregion

        #region 会员销售相关
        /// <summary>
        /// 销售回访电话状态
        /// </summary>
        public enum ETSTTelStatus : int { 待回访 = 0, 接通 = 1, 号码错误 = 2, 停机 = 3, 空号 = 4, 无法接通 = 5, 关机 = 6, 无人接听 = 7, 占线 = 8, 稍后联系 = 9, 秘书服务 = 10 }

        /// <summary>
        /// 销售回访会员状态
        /// </summary>
        public enum ETSTStatus : int { 待回访 = 1, 接受回访 = 2, 拒绝回访 = 3, 失败回访 = 4, 放弃回访 = 5 }

        /// <summary>
        /// 销售回访任务状态
        /// </summary>
        public enum ETSTTaskStatus : int { 待分配 = 1, 分配中 = 2, 完成分配 = 3, 任务终止 = 4, 任务完成 = 5 }

        /// <summary>
        /// 销售回访操作类型
        /// </summary>
        public enum ETSTOperation : int { 新建 = 1, 分配 = 2, 删除 = 3, 终止 = 4, 恢复 = 5, 回收 = 6, 查看 = 7, 监控 = 8, 导入 = 9 }

        /// <summary>
        /// 销售支付类型
        /// </summary>
        public enum ETSTPmytType : int { 信用卡 = 1, 借记卡 = 2, 汇款 = 3, 分红 = 4, 现金 = 5, 预留款 = 10 }

        /// <summary>
        /// 销售客户类型
        /// </summary>
        public enum ETSTCustomerType : int { 已购买 = 1, 同意购买尚未付款 = 2, 重点客户 = 3, 待跟进 = 4, 主观放弃 = 5, 彻底放弃 = 10 }
        #endregion

        #region 系统工单
        /// <summary>
        /// 系统工单状态
        /// </summary>
        public enum ECCaseType : int { 待处理 = 0, 处理完 = 1, 废弃 = 2 }

        /// <summary>
        /// 工单操作类型
        /// </summary>
        public enum ECaseOperationType : int { 转移 = 0, 完成 = 1, 废弃 = 2 }

        /// <summary>
        /// 工单优先级
        /// </summary>
        public enum ECasePri : int { 普通 = 0, 紧急 = 1 }
        /// <summary>

        /// 工单类型
        /// </summary>
        public enum ECaseClassType : int { 会员投诉 = 1, 酒店预订 = 2, 机票预定 = 3, 餐饮服务 = 4, 咨询 = 5 }
        #endregion

        #region 代理商管理
        /// <summary>
        /// 代理返佣方式
        /// added by liyao 
        /// </summary>
        public enum EACommType : int { 固定值 = 1, 消费百分比 = 2, 公司利润百分比 = 3 }

        /// <summary>
        /// 卡预订服务类型
        /// added by liyao 
        /// </summary>
        public enum EMCGCAgentBillOrderType : int { 酒店预订 = 1, 机票预订 = 2, 机票退废 = 3, 餐饮预订 = 4, 高尔夫预订 = 5 };

        /// <summary>
        /// 网上预订方式
        /// added by liyao
        /// </summary>
        public enum EMANetOrderType : int { 无 = 0, XML = 1, 嵌入式 = 2, 链接 = 3 };

        /// <summary>
        /// 电话预订方式
        /// added by liyao
        /// </summary>
        public enum EMATelOrderType : int { 无 = 0, 普通 = 1, 主叫识别 = 2, 被叫识别 = 3 };

        /// <summary>
        /// 服务名义
        /// added by liyao
        /// </summary>
        public enum EMAServiceType : int { 公司名义 = 0, 代理商名义 = 1 };

        /// <summary>
        /// 确认对象
        /// added by liyao
        /// </summary>
        public enum EMAConfirmToType : int { 与客人确认 = 0, 与代理商确认 = 1, 接口确认 = 2 };

        /// <summary>
        /// 系统分类
        /// added by liyao
        /// </summary>
        public enum EMASystemType : int { 统一处理 = 0, 分开处理 = 1 };

        #endregion

        #region 贵宾厅
        /// <summary>
        /// 贵宾厅_航班提醒任务_到贵宾厅状态
        /// OS为已经来，NS为没有来
        /// </summary>
        public enum EVFlightRemindTaskStatus : int { OS = 1, NS = 2 };
        /// <summary>
        /// 贵宾厅_合同租金类型
        /// </summary>
        public enum EVRentType : int { 整年 = 1, 半年 = 2, 季度 = 3, 月 = 4 };
        /// <summary>
        /// 贵宾厅_质量监查项
        /// </summary>
        public enum EVQualityInspectItems : int { 服务 = 1, 设施 = 2, 环境 = 3, 商品 = 4, 其他 = 5 }
        public enum EVQualityIsSatisfy : int { 满意 = 1, 不满意 = 2 }
        #endregion

        #region 会员关系
        /// <summary>
        /// 会员关系
        /// </summary>
        public enum EVFMemRelationshipID : int { 父亲 = 1, 母亲 = 2, 子女 = 3, 妻子 = 4, 同事 = 5, 朋友 = 6 };
        #endregion

        #region 费用审核状态
        /// <summary>
        /// 费用审核状态
        /// </summary>
        public enum EVFeeStatus : int { 待审核 = 1, 审核通过 = 2, 取消 = 3 };

        #endregion

        #region 积分换奖品申请来源
        /// <summary>
        /// 积分换奖品申请来源
        /// </summary>
        /// Created By: SunBin | 2007-11-22
        public enum EApplySource : int { 信件邮寄 = 1, 上门领取 = 2, 邮寄他人 = 3, 上门代领 = 4, 奖励客房 = 5 }
        #endregion

        #region 积分换奖品发放方式
        /// <summary>
        /// 积分换奖品发放方式
        /// </summary>
        /// Created by: SunBin | 2007-11-22
        public enum EApplySendMethod : int { 电话申请 = 1, 上门申请 = 2, 网上申请 = 3, 短信申请 = 4 }
        #endregion

        #region "短信模板"

        /// <summary>
        /// 发送方式
        /// </summary>
        public enum ESMSSendMeans : int { 强制发送 = 1, 人工干预 = 2 }

        /// <summary>
        /// 短信模板类型
        /// </summary>
        public enum ESMSTemplateType : int { 固定模板 = 1, 定制模板 = 2 }

        /// <summary>
        /// 短信级别
        /// </summary>
        public enum ESMSLevel : int { 加急 = 1, 急 = 2, 正常 = 3, 不急 = 4 }

        /// <summary>
        /// 接收短信处理状态
        /// </summary>
        public enum EVIssueFlag : int { 未处理 = 0, 已处理 = 2 };
        #endregion

        #region 现金申请状态
        /// <summary>
        /// 现金申请状态
        /// </summary>
        /// Created By: SunBin | 2007-12-03
        public enum ECashApplyStatus : int { 提交 = 1, 导出 = 2, 返还 = 3, 取消 = 4 }
        #endregion

        #region 特商
        /// <summary>
        /// 订单状态
        /// </summary>
        public enum ECVOrderStatus : int { 新订单 = 1, 处理中 = 2, 处理完 = 3, 作废 = 4 };
        /// <summary>
        /// 付款状态
        /// </summary>
        public enum ECVPayStatus : int { 未付 = 1, 已付 = 2 };
        /// <summary>
        /// 菜单状态
        /// </summary>
        public enum ECVMenuStatus : int { 作废 = 1, 可用 = 2 };
        /// <summary>
        /// 就餐场地
        /// </summary>
        public enum ECVDinnerPlace : int { 包间 = 1, 散座 = 2 };
        #endregion

        #region 积分原因类型
        /// <summary>
        /// 积分原因类型
        /// </summary>
        public enum EMSourceTypeID : int { 酒店预订 = 1, 手工赠送 = 2, 礼物 = 3, 机票预订 = 4, 餐饮预订 = 5, 贵宾厅 = 6, 其他 = 7, 国际机票 = 8, 信用担保欠款抵销 = 9 };
        #endregion

        #region 分红原因类型
        public enum EMDvDSourceTypeID : int { 酒店预订 = 11, 手工赠送 = 12, 礼物 = 13, 现金 = 14, 其他 = 15, 信用担保欠款抵销 = 9 };
        #endregion

        #region 积分或分红方向
        /// <summary>
        /// 积分或分红方向
        /// </summary>
        public enum EMDirection : int { 增加 = 1, 减少 = 2 };
        #endregion

        #region 积分来源
        /// <summary>
        /// 积分来源
        /// </summary>
        public enum EMOrderSource : int { 外部导入 = 1, 公司积累 = 2 };
        #endregion

        #region 奖品申请状态
        /// <summary>
        /// 奖品申请状态
        /// </summary>
        public enum EMProductApplyStatus : int { 待审核 = 1, 通过审核 = 2, 发放奖品 = 3, 取消 = 4 };
        #endregion

        #region 发送消息类型
        /// <summary>
        /// 发送消息类型
        /// </summary>
        public enum ECMessageType : int { 手工通知 = 1, 订单通知 = 2, 工单通知 = 3 };
        #endregion

        #region 弹屏操作类型
        /// <summary>
        /// 弹屏操作类型
        /// added by liyao 2007-12-14
        /// </summary>
        public enum ECTIOperateType : int { 酒店预订 = 1, 国内机票 = 2, 国际机票 = 3, 度假及旅游 = 4, 法律服务 = 5, 会员服务 = 6, 车险办理 = 7, 英文服务 = 8, 手机预订 = 9 };

        /// <summary>
        /// 弹屏操作结果
        /// added by liyao 2007-12-14
        /// </summary>
        public enum ECTIOperateResult : int { 成功 = 1, 失败 = 2 };
        #endregion

        #region 保险类型
        public enum EInsurranceType : int { 三百万万平安保险 = 1 };
        #endregion

        #region 会员申请退款记录状态
        public enum ERefundmentStatus : int { 初始 = 1, 已申请 = 2, 完成 = 3, 取消 = 4, 已审核 = 5, 入预留款 = 6 };
        #endregion

        #region 会员退款类型
        public enum ERefundmentType : int { 公司预留款 = 2, 公司汇款给会员 = 4 };
        #endregion

        #region 担保单状态
        public enum EAssureStatus : int { 待收款 = 1, 已付款 = 2, 取消 = 3, 退票支付退票费 = 4, 退票下次支付退票费 = 5, 退票拒付退票费 = 6, 废票 = 7 };
        #endregion

        #region 卡号段级别
        /// <summary>
        /// 卡号段级别
        /// added by liuxianyin 2008-06-11
        /// </summary>
        public enum EMCardLevelType : int { 至尊卡 = 1, 贵宾金卡 = 2, 贵宾卡 = 3, 置换卡 = 4, 免费注册卡 = 5 };
        #endregion

        #region 担保原因
        public enum EAssureReason : int { 超出送票范围 = 1, 超出送票时间 = 2, 客人不便支付 = 3, 其它 = 4 };
        #endregion

        #region 担保损失原因
        public enum EAssureExpense : int { 客人恶意拒付 = 1, 预订信息错误 = 2, 未找到取票点 = 3, 其它 = 4, 无损失 = 5 };
        #endregion

        #region 酒店对账结算月统计类型
        public enum EATotalQuery : int { 各地区审计完成间夜利润统计 = 1, 各地区对账确认间夜利润统计 = 2, 各地区已回款间夜利润统计 = 3, 各地区应对账酒店数量统计 = 4, 各地区结算流失统计 = 5 };
        #endregion

        #region 旅通卡订单类型
        public enum TCOrderType : int { 酒店订单 = 1, 机票订单 = 2, 旅游度假 = 3, 高尔夫 = 4, 贵宾厅 = 5, 三百万保险 = 6, 汽车道路救援 = 7, 奢侈品订单 = 8 };
        #endregion

        #region 短信发送状态
        public enum SMSStatus : int { 发送失败 = 0, 发送成功 = 1, 内容中有禁词 = 2, 号码不正确 = 3, 号码被禁止 = 4, 号码在黑名单中 = 5, 号码不在白名单中 = 6, 企业欠费 = 7, 通讯异常 = 8, 系统错误 = 101, 内容无法到达手机 = 102 };
        #endregion


        #region 分红发放

        #region 分红方式
        /// <summary>
        /// 分红方式
        /// </summary>
        public enum BonusType : int { 预留款分红 = 1, 现金分红 = 2 };
        #endregion

        #region 分红作废状态

        public enum EDvdAbolishStatus : int { 未完成 = 1, 已完成 = 2 };

        #endregion
        #region 分红返还状态

        public enum EDvdReturnStatus : int { 未完成 = 1, 未返还 = 2, 已返还 = 3, 现金审核中 = 4, 分红取消 = 5 };

        #endregion

        #region 分红记录状态/*add by zpy 2010-12-22*/
        public enum EDvdRecordStatus : int
        {
            累积中 = 1,
            正常作废 = 2,
            相抵作废 = 3,
            分红待返 = 4,
            已返分红 = 5,
            分红相抵 = 6,
            信用担保欠款抵消 = 7,
            分红入预留款 = 8
        };
        #endregion

        #region 每月分红作废状态

        public enum EDvdMonthAbolishStatus : int { 未完成 = 1, 处理中 = 2, 已完成 = 3 };

        #endregion


        #region 每月分红返还状态

        public enum EDvdMonthReturnStatus : int { 未完成 = 1, 处理中 = 2, 已完成 = 3 };

        #endregion

        #region 分红返还取消原因

        public enum EDvdReturnCancelReason : int { 信息不合格取消 = 1, 分红不成功取消 = 2 };

        #endregion

        #region 返分红方式类型
        public enum EDvdBillType : int { 单会员 = 1, 多会员 = 2 };

        #endregion
        #endregion

        #region 国际机票

        #region 国际机票航线级别
        /// <summary>
        /// 国际机票航线级别
        /// </summary>
        public enum LineLevel : int
        {
            Level1 = 1,
            Level2 = 2
        }
        #endregion

        #region 合作商级别
        /// <summary>
        /// 合作商级别
        /// </summary>
        public enum PartnerLevel : int
        {
            Level1 = 1,
            Level2 = 2,
            Level3 = 3,
            Level4 = 4
        }
        #endregion

        #region 乘客类型
        /// <summary>
        /// 乘客类型
        /// </summary>
        public enum EITPCustomerType : int
        {
            普客 = 1,
            学生 = 2,
            青年 = 3,
            劳务 = 4,
            海员 = 5,
            新移民 = 6

        }
        #endregion
        #region 乘客类型
        /// <summary>
        /// 乘客类型
        /// </summary>
        public enum EITGuestType : int
        {

            ADT = 1,
            STU = 2,
            YTH = 3,
            LBR = 4,
            SEA = 5,
            CHD = 7,
            STUYTH = 8,
            GV2 = 9,
            GV4 = 10

        }
        #endregion

        #region 行程类型
        public enum TripType : int
        {
            单程 = 1,
            往返 = 2
        }
        #endregion

        #region 团队类型
        /// <summary>
        /// 团队类型
        /// </summary>
        public enum TeamType : int
        {
            散客 = 1,
            二至五人小团 = 2,
            十人以上团队 = 3
        }
        #endregion

        #region 合作商等级
        /// <summary>
        /// 合作商等级
        /// </summary>
        public enum EITAgentClass : int { 公司内部 = 1, 一级代理商 = 2, 二级代理商 = 3, 三级代理商 = 4, 四级代理商 = 5, 五级代理商 = 6 };

        #endregion

        #region 国际机票状态
        /// <summary>
        /// 国际机票状态
        /// </summary>
        public enum EITicketOrderStatus : int
        {

            待订 = 1,
            预订 = 10,
            真伪鉴别 = 8,
            派单 = 20,
            转领班 = 30,
            出票 = 40,

            配送 = 80,

            完成 = 100,
            取消 = 120,
            作废 = 200
        }
        #endregion

        #region 国际机票任务类型
        /// <summary>
        /// 国际机票任务类型
        /// </summary>
        public enum EITicketTaskType : int
        {
            待出订单 = 10,
            自出票 = 20,
            退票 = 30,
            废票 = 40,
            改签 = 50,
            配单 = 60,
            订单收银 = 60,
        }
        #endregion

        #region 国际机票的配送方式
        /// <summary>
        /// 国际机票的配送方式
        /// </summary>
        public enum EITDeliverAssign : int { 邮寄行程单 = 1, 票保留 = 2, 机场取票 = 3, 市区送票 = 4, 市区取票 = 5, 无需送票 = 6 };
        #endregion

        #region 国际机票的付款方式
        /// <summary>
        /// 国际机票的付款方式
        /// </summary>
        public enum EITPayType : int { 现金 = 1, 信用卡 = 2, 支票 = 3, 汇款 = 4, 其它 = 5 };
        #endregion

        #region 国际机票订单任务锁定类型
        /// <summary>
        /// 国际机票订单任务锁定类型
        /// </summary>
        public enum EITTaskLockStatus : int
        {
            正常 = 0,
            锁定 = 1,
            延时 = 2
        }
        #endregion

        #region 国际机自出票操作
        /// <summary>
        /// 国际机自出票操作
        /// </summary>
        public enum EITInnerOrderOperation : int
        {
            添票号 = 0,
            延时 = 2
        }
        #endregion

        #region 国际机票订单任务最长延时时间
        /// <summary>
        /// 国际机票订单任务最长延时时间
        /// </summary>
        public enum EITTaskDelayTime : int
        {
            确保时间 = 5
        }
        #endregion

        #region 退改签类型
        /// <summary>
        /// 退改签类型
        /// </summary>
        public enum EITTicketChangeType : int { 退票 = 1, 废票 = 2, 改签 = 3 }
        #endregion

        #region 订单任务完成类型
        /// <summary>
        /// 订单任务完成类型
        /// </summary>
        public enum EIOTIssueType : int { 完成 = 1, 转移 = 2, 延时 = 3, 异常终止 = 4, 保持 = 5, 停止 = 6 };

        #endregion

        #region 订单任务锁定状态
        /// <summary>
        /// 订单任务锁定状态
        /// </summary>
        public enum EIOTLockStatus : int { 锁定 = 1, 等待 = 0 }
        #endregion

        #region 操作类型
        /// <summary>
        /// 操作类型
        /// </summary>
        public enum EITOperationType : int
        {
            生成订单 = 10,
            真伪鉴别 = 8,
            修改订单 = 15,
            订单收银 = 20,
            财务收款确认 = 25,
            待出票处理 = 30,
            接口订单出票 = 32,

            合作商出票 = 38,
            出票 = 40,

            废票出票 = 43,
            退票出票 = 45,

            合作商拒单 = 46,

            生成退废改签 = 48,
            退票 = 50,
            合作商退款 = 51,
            客人退款 = 52,

            废票 = 53,
            退废票审核 = 54,
            改签 = 57,
            订单取消 = 60,
            自出票 = 70,
            发票打印 = 71,
            发票处理 = 73,
            发票废换处理 = 74,
            添加票号 = 75,
            邮寄行程单配送 = 80,
            机场取票配送 = 90,
            市区送票配送 = 100,
            市区自取 = 110,
            保留行程单 = 120,

            添加高额发票 = 200,
            修改净价 = 250,
            拆分价格 = 300,
            回退退票处理 = 301,
            回退废票处理 = 302,
            回退改签处理 = 303,

            订单出票修改 = 305,

        }

        #endregion

        #region 任务类型
        /// <summary>
        /// 任务类型
        /// </summary>
        public enum EITOTaskType : int
        {
            自出票 = 10,
            真伪鉴别 = 8,
            订单收银 = 20,
            财务收款确认 = 25,
            待出订单 = 30,
            接口待出订单 = 32,
            出票 = 40,

            合作商出票 = 42,

            退票出票 = 45,
            废票出票 = 43,
            退票 = 50,
            合作商退款 = 51,
            客人退款 = 52,
            废票 = 53,
            退废票审核 = 54,

            改签 = 57,



            订单取消 = 60,

            合作商拒单 = 65,

            发票打印 = 70,
            发票处理 = 75,
            邮寄行程单配送 = 80,
            机场取票配送 = 90,
            市区送票配送 = 100,
            市区自取 = 110,
            保留行程单 = 120


        }
        #endregion

        #region 配送 处理类型
        /// <summary>
        /// 处理类型
        /// </summary>
        public enum EITDeliverStatus : int
        {
            处理中 = 1,
            已完成 = 2,
        }
        #endregion

        #region 配送未送达原因
        /// <summary>
        /// 未送达原因
        /// </summary>
        public enum EITNonDeliveryReason : int
        {
            变更时间 = 1,
            变更地点 = 2,
            变更方式 = 3,
            取消预订 = 4,
        }
        #endregion

        #region 退.废.改签 类型
        /// <summary>
        /// 退.废.改签 类型
        /// </summary>
        public enum EITicketChangeType : int { 退票 = 1, 废票 = 2, 改签 = 3 }
        #endregion

        #region 机票状态
        /// <summary>
        /// 机票状态 
        /// </summary>
        public enum EITicketStatus : int { 正常 = 0, 退票 = 1, 废票 = 2, 改签 = 3, 取消 = 4 }
        #endregion

        #region 航舱是否启用
        /// <summary>
        /// 航舱是否启用
        /// </summary>
        public enum EITicketValid : int { 是 = 1, 否 = 0 }
        #endregion

        #region 航班是否经停
        /// <summary>
        ///  航班是否经停
        /// </summary>
        public enum EITicketStop : int { 是 = 1, 否 = 0 }

        #endregion

        #region 航班是否启用
        /// <summary>
        /// 航班是否启用
        /// </summary>
        public enum EITicketIsUsed : int { 是 = 1, 否 = 0 }
        #endregion
        #region 国际机票需求单状态
        public enum EITisBooktable_Status : int
        {
            已提交 = 1,
            已处理 = 2

        }
        #endregion
        #region 价格政策原因
        /// <summary>
        /// 价格政策原因
        /// </summary>
        public enum EITPricePolicylsReason : int { 手动更新 = 1, 自动更新 = 2, 删除 = 0 }
        #endregion

        #region 散团
        public enum EITPricePolicyTeamType : int { 散客 = 1, 团体 = 20 }
        #endregion

        #region 送达状态
        /// <summary>
        /// 送达状态
        /// </summary>
        public enum EITDeliverFinishStatus : int { 已送达 = 1, 未送达 = 2 }
        #endregion

        #region 启用状态
        /// <summary>
        /// 启用状态
        /// </summary>
        public enum EITIsUsedStatus : int { 启用 = 1, 禁用 = 0 }
        #endregion

        #region 国际机票政策年月日
        /// <summary>
        /// 国际机票政策年月日
        /// </summary>
        public enum EITTicketValidityType : int
        {
            D = 1,
            M = 2

        }
        #endregion
        #region 国际机票真伪鉴别状态
        /// <summary>
        /// 国际机票真伪鉴别状态
        /// </summary>
        public enum EITVerifyStatus : int { 否 = 1, 是 = 2 }
        #endregion
        #region 国际机票代付结算方式
        /// <summary>
        /// 国际机票代付结算方式
        /// </summary>
        public enum EITPymtType : int { 全价结算 = 1, 净价结算 = 2 }
        #endregion
        #region 国际机票收款状态
        public enum EITOrderPymtStatus : int { 未收款 = 0, 操作员收款 = 1, 财务收款 = 2 }
        #endregion

        #region 国际机票合作商退款状态
        public enum EITPartnerReFeeStatus : int { 待退 = 0, 已退 = 1 }
        #endregion
        #region 国际机票客人退款状态
        public enum EITGuestReFeeStatus : int { 待退 = 0, 已退 = 1 }
        #endregion

        #region 国际机票出票状态
        public enum EITChangeStatus : int { 待配送 = 0, 待出票 = 1, 待审核 = 2, 审核完成 = 3 }
        #endregion


        #region 国际机票收款类型
        public enum EITOPayPal_Type : int { 收现 = 1, 代收代付 = 2 };
        #endregion


        #region 国际机票代付结算单状态
        public enum EITOColPaymentStatus : int { 待支付 = 1, 已退回 = 2, 已支付 = 3, 已取消 = 4 };
        #endregion


        #region 国际机票代付结算单状态
        public enum EITOTicketBillStatus : int { 待确认 = 1, 已确认 = 2, 已回款 = 3 };
        #endregion

        #region 国际机票出票操作类型
        public enum EITOTicketIssueWay : int { 公司操作 = 0, 合作商自操作 = 2 };

        #endregion

        #region TravelFusion相关

        #region 查询类型
        /// <summary>
        /// 国际机票代码查询方式（是按机场代码查询，还是按城市代码查询）
        /// </summary>
        public enum EITTicketQueryType : int
        {
            机场代码 = 1,
            城市代码 = 2
        };

        public enum EITTOrder_Type : int
        {
            金色世纪 = 0,
            TravelFusion = 1
        };

        #endregion

        #endregion

        #endregion

        #region 信用卡交易
        /// <summary>
        /// 信用卡交易状态
        /// </summary>
        public enum CreditCardTransStat : int { 交易初始化 = 1, 交易失败 = 2, 交易中 = 3, 交易成功 = 4, 交易异常 = 5 }
        #endregion

        # region alter by wangjunpeng 2009-3-12　信用积分

        /// <summary>
        /// 会员等级
        /// </summary>
        public enum EMemberStar : int { 一级 = 1, 二级 = 2, 三级 = 3, 四级 = 4, 五级 = 5, 高级 = 6, 特级 = 7 }
        /// <summary>
        /// 信用积分增减类型
        /// </summary>
        public enum EPointType : int { 成功支付 = 1, 成功支付损失费用 = 5, 没有损失 = 0, 损失小于等于五十 = -5, 损失大于五十小于等于一百 = -10, 损失大于一百小于等于二百 = -30, 损失大于两百 = -50 }

        /// <summary>
        /// 客人欠款类型
        /// </summary>
        public enum CustomerOweType : int
        {
            担保退票欠款 = 1, 代收代付单因退票自动产生欠款 = 2, 差价票款 = 3, 客人原因造成退票欠款 = 4
        }
        # endregion

        #region alter by wangjunpeng 2009-12-31　信用积分

        /// <summary>
        /// 会员信用等级 Assure
        /// </summary>
        public enum ECMemberStar : int { 无担保 = 1, 三星 = 2, 四星 = 3, 五星 = 4, 高级 = 5, 特级 = 6 }
        /// <summary>
        /// 是否担保 
        /// </summary>
        public enum ECIsAssure : int { 无担保 = 1, 担保 = 2 }
        /// <summary>
        /// 会员信用支付状态 
        /// </summary>
        public enum ECPayType : int { 成功支付 = 1, 未成功支付 = 2, 成功支付损失费用 = 3 }
        /// <summary>
        /// 会员信用积分增减方向 
        /// </summary>
        public enum ECDirection : int { 正向 = 1, 负向 = 2 }

        /// <summary>
        /// 会员是否是欠款
        /// </summary>
        public enum ECIsRePayment : int { 欠款 = 1, 还款 = 2 }
        #endregion

        #region 第三方支付类型
        /// <summary>
        /// 第三方支付类型
        /// </summary>
        public enum ThirdPartyType : int
        {
            网络支付 = 1, 信用卡支付 = 2, 银联卡支付 = 3
        }
        #endregion

        #region 银联支付log类型
        /// <summary>
        /// 银联支付log类型
        /// </summary>
        public enum UnionPayLogType : int
        {
            发送 = 1, 接收 = 2
        }
        #endregion

        #region 会员评分时是否显示该项
        /// <summary>
        /// 会员评分时是否显示该项
        /// </summary>
        public enum UnionCommentShowType : int
        {
            是 = 1, 否 = 2
        }

        #endregion

        #region 会员评分时是否酒店是否有此项服务
        /// <summary>
        /// 会员评分时是否酒店是否有此项服务
        /// </summary>
        public enum UnionCommentHasType : int
        {
            是 = 1, 否 = 2
        }
        #endregion

        #region 酒店评分类型
        /// <summary>
        /// 酒店评分类型
        /// </summary>
        public enum Comment_Type_ID : int
        {
            卫生 = 1,
            服务 = 2,
            周边环境 = 3,
            性价比 = 4,
            舒适度 = 5,
            餐厅 = 6,
            健康设施 = 7
        };
        #endregion

        #region 航信酒店接口相关

        /// <summary>
        /// 航信酒店同步默认操作员ID
        /// </summary>
        public enum EHBE_User_ID : int { 系统管理员 = 1 };

        /// <summary>
        /// 更新类型
        /// </summary>
        public enum EHBE_Syn_Type : int
        {
            酒店导入 = 0,
            酒店更新 = 10,
            酒店预定 = 20,
            订单更新 = 30,
            取消订单 = 40,
            WebSev酒店服务 = 50,
            WebSev订单服务 = 60,

            HUBWS房态变化 = 231,
            HUBWS价格变化 = 232,
            HUBWS订单状态变化 = 233,
            HUBWS佣金政策变化 = 234,

        };

        /// <summary>
        /// 航信酒店订单同步更新类型
        /// </summary>
        public enum EHBE_OrderSyn_Type : int { 新建订单 = 0, 修改订单 = 1, 订单确认 = 2, 自动确认 = 3, 航信夜审 = 4, 自动审核 = 5, 取消订单 = 10 };

        /// <summary>
        /// 航信酒店导入状态
        /// </summary>
        public enum EHBE_Import_Status : int { 导入失败 = -30, 查询失败 = -20, 更新失败 = -10, 未导入 = 0, 导入成功 = 10, 更新成功 = 20, 数据一致 = 30, 已存在 = 40 };

        /// <summary>
        /// 航信酒店查询分页方式
        /// </summary>
        public enum EHBE_Import_displayReq : int { 不分页查询 = 50, 全部查询 = 40, 分页查询 = 30 };

        /// <summary>
        /// 航信服务类型 0:餐饮设施,1:会议设施,2:娱乐健身,3:服务项目
        /// </summary>
        public enum EHBE_Service : int { 餐饮设施 = 0, 会议设施 = 1, 娱乐健身 = 2, 服务项目 = 3, 客房设施 = 6000, 其它设施 = 6005 };

        /// <summary>
        /// CRM服务设施 1:餐饮设施,2:会议设施,3:娱乐设施,4:服务设施
        /// </summary>
        public enum ECRM_Service : int { 餐饮设施 = 1, 会议设施 = 2, 娱乐设施 = 3, 服务设施 = 4, 客房设施 = 6000, 其它设施 = 6005 };

        /// <summary>
        /// 酒店服务项目是否收费
        /// </summary>
        public enum EHBE_IsCharge : int { 收费 = 1, 免费 = 0 };

        /// <summary>
        /// 酒店房型是否允许网站销售
        /// </summary>
        public enum ECRM_IfWebSale : int { 允许 = 1, 不允许 = 0 };

        /// <summary>
        /// 酒店房型是否允许代理商销售
        /// </summary>
        public enum ECRM_IfAgentSale : int { 允许 = 1, 不允许 = 0 };

        /// <summary>
        /// 酒店房型是否可以加床
        /// </summary>
        public enum ECRM_IfAddBed : int { 可以 = 1, 不可以 = 0 };

        /// <summary>
        /// 提供早餐类型：0-无早；1－单早；2－双早
        /// </summary>
        public enum EHBE_Meal : int { 有早 = -1, 无早 = 0, 单早 = 1, 双早 = 2 };

        /// <summary>
        /// 是否基本房价
        /// </summary>
        public enum EHBE_IsBase : int { 否 = 0, 是 = 1 };

        /// <summary>
        /// 是否航信酒店
        /// </summary>
        public enum EHBE_IsHBEHotel : int { 否 = 0, 是 = 1 };

        /// <summary>
        /// 航信酒店上下线状态
        /// </summary>
        public enum EHBE_Status : int { 上线 = 0, 下线 = 1 };

        /// <summary>
        /// 酒店预定规则提前预定时间
        /// </summary>
        public enum EHBE_PreBookTime : int { 提前预定时间 = 0 };

        /// <summary>
        /// 预留房来源类型
        /// </summary>
        public enum ECRM_HotelSouce : int { 增加 = 0, 合同 = 1 };

        /// <summary>
        /// 预留房确认方式
        /// </summary>
        public enum ECRM_IfAutoConfirm : int { 不可直接确认 = 0, 可直接确认 = 1 };

        /// <summary>
        /// 酒店性质
        /// </summary>
        public enum ECRM_HotelProperty : int { 中外合资 = 1, 旅游局 = 2, 独资 = 3, 国有企业 = 4, 私营企业 = 6000 };

        /// <summary>
        /// 酒店类型
        /// </summary>
        public enum ECRM_HotelType : int { 观光型 = 1, 综合型 = 2, 商务型 = 3, 会议型 = 4, 经济型 = 5, 公寓型 = 6, 个性化 = 7, 青年旅馆 = 6000 };

        /// <summary>
        /// 装修级别
        /// </summary>
        public enum ECRM_FilmentClass : int { 装修级别1 = 1, 装修级别2 = 2, 装修级别3 = 3, 装修级别4 = 4 };

        /// <summary>
        /// 床型要求：‘0’默认，无特别要求；‘1’尽量大床；‘2’务必大床；‘3’务必双床；‘4’尽量双床
        /// </summary>
        public enum EHBE_RoomTypeReq : int { 无特别要求 = 0, 尽量大床 = 1, 务必大床 = 2, 务必双床 = 3, 尽量双床 = 4 };

        /// <summary>
        /// 无烟要求：‘0’默认，无特别要求；‘1’尽量无烟层房间；‘2’务必无烟层房间；‘3’请无烟处理
        /// </summary>
        public enum EHBE_SmokeReq : int { 无特别要求 = 0, 尽量无烟层房间 = 1, 务必无烟层房间 = 2, 请无烟处理 = 3 };

        /// <summary>
        /// 早餐要求：‘0’默认，无特别要求；‘1’每天每间加一早；‘2’每天每间加两早
        /// </summary>
        public enum EHBE_BreakfeastReq : int { 无特别要求 = 0, 每天每间加一早 = 1, 每天每间加两早 = 2 };

        /// <summary>
        /// 接机要求：‘0’默认，无特别要求；‘1’接机
        /// </summary>
        public enum EHBE_WelcomeReq : int { 无特别要求 = 0, 接机 = 1 };

        /// <summary>
        /// 加床要求：‘0’默认，无特别要求；‘1’加床
        /// </summary>
        public enum EHBE_AddBedReq : int { 无特别要求 = 0, 加床 = 1 };

        #endregion

        #region 搜索查询完成情况 create by wangjunpeng 2009-06-02

        /// <summary>
        /// 搜索查询完成情况
        /// </summary>
        public enum FinishStatus : int { 完成 = 1, 未完成 = 0 }
        #endregion

        #region 搜索查询类型 create by yanghongtao 2009-08-18
        /// <summary>
        /// 搜索查询类型
        /// </summary>
        public enum ESearchType : int { 国内机票查询 = 0, 国际机票查询 = 1 };
        #endregion


        #region 查询舱位
        /// <summary>
        /// 查询舱位
        /// </summary>
        public enum ETCabinSearch : int { 去程舱位 = 1, 返程舱位 = 2 }
        #endregion

        #region 航班预订步骤
        /// <summary>
        /// 航班预订步骤
        /// </summary>
        public enum ETReservationStep : int { 预订去程 = 1, 预订返程 = 2 }

        #endregion

        #region 舱位选择
        /// <summary>
        /// 舱位选择
        /// </summary>
        public enum ETCabinChoose : int { 最低价舱位 = 1, 所有舱位 = 2 }

        #endregion

        #region 手机网站支付
        /// <summary>
        /// 手机网站支付
        /// </summary>
        public enum ETMobliePay : int { 借记卡支付 = 1, 信用卡支付 = 2 }
        #endregion

        #region 发送短信条件（乘客类型和航段）
        /// <summary>
        /// 发送短信条件（乘客类型和航段）
        /// added by rxh 2009-7-16
        /// </summary>
        public enum ETMessageCondition : int
        {
            成人单程 = 1,
            成人多程 = 2,
            混合单程 = 3,
            混合多程 = 4
        }
        #endregion



        #region 呼入呼出类型
        /// <summary>
        /// 呼入呼出类型
        /// </summary>
        public enum ECallType : int { 呼入 = 1, 呼出 = 2, 呼入呼出 = 3 }
        #endregion


        #region eHi租车
        /// <summary>
        /// 订单状态
        /// </summary>
        public enum eHiOrderStatus : int
        {
            预约中 = 0, 租赁中 = 30, 已完成 = 60, 取消待定 = 90, 已取消 = 120
        }

        /// <summary>
        /// GPS状态
        /// </summary>
        public enum eHiGPSStatus : int
        {
            Y = 0, N = 1
        }

        /// <summary>
        /// 租车时间分段
        /// </summary>
        public enum eHiDayType : int
        {
            平时 = 1, 周末 = 2, 节假日 = 3
        }
        #endregion

        #region 手机机票查询排序类型
        /// <summary>
        /// 手机机票查询排序类型
        /// </summary>
        public enum TMobileFlightInfoSortType : int
        {
            时间升序 = 1, 时间降序 = 2, 价格升序 = 3, 价格降序 = 4, 航空公司升序 = 5, 航空公司降序 = 6
        }

        #endregion

        #region 出港到港类型
        /// <summary>
        /// 出港到港类型
        /// </summary>
        public enum ESpecialDirectionType : int
        {
            出港城市 = 0,
            到港城市 = 1
        }
        #endregion

        #region 外网用舱位等级
        /// <summary>
        /// 外网用舱位等级
        /// </summary>
        public enum ETCabinGradeWeb : int { 头等舱 = 1, 公务舱 = 2, 经济舱 = 3, 全价舱 = 4, 最低价舱 = 5, 特价经济舱 = 6, 直航优惠 = 7 };
        #endregion

        #region 外网机票支付方式
        /// <summary>
        /// 外网机票支付方式
        /// </summary>
        public enum ETJSJPayType : int { 信用卡 = 1, 借记卡 = 2, 网上支付 = 3, 票到付款 = 4 };
        #endregion

        #region 外网报销凭证
        /// <summary>
        /// 外网报销凭证
        /// </summary>
        public enum ETJSJReceiptType : int { 不需要 = 1, 送票上门 = 2, 到指定地点自取 = 3, 邮寄行程单 = 4 };
        #endregion


        #region 外网页面访问类型
        /// <summary>
        /// 外网页面访问类型
        /// </summary>
        public enum ETJSJAccessType : int
        {
            //酒店信息
            酒店查询 = 101,
            酒店信息查看 = 102,
            酒店预订 = 103,
            生成酒店订单 = 104,

            //国际机票
            国际机票查询 = 301,
            国际机票信息查看 = 302,
            国际机票预订 = 303,
            生成国际机票订单 = 304
        };
        #endregion

        #region 传递消息类型
        /// <summary>
        /// 传递消息类型
        /// </summary>
        public enum EWTInfoType : int { 通用 = 1, 机票订单修改 = 2, 机票订单取消 = 3, 机票退改签 = 4 };
        #endregion

        #region 传递消息处理状态
        /// <summary>
        /// 传递消息处理状态
        /// </summary>
        public enum EWTInfoStatus : int { 未处理 = 1, 已处理 = 2 };
        #endregion
        #region webservice 提供机票订单状态
        /// <summary>
        /// added by wjp 2009-11-06
        /// webservice 提供机票订单状态
        /// </summary>
        public enum EJSJWSTicketOrderStatus : int { 待订 = 0, 预订 = 1, 出票 = 2, 派单 = 4, 完成 = 5, 取消 = 6 }
        #endregion

        #region 代理商订单类型
        /// <summary>
        /// added by wjp 2009-11-30
        ///  针对代理商订单类型
        /// </summary>
        public enum EAgentOrderType : int { 酒店订单 = 0, 国内机票 = 1, 国际机票 = 2, 其它 = 3 }

        #endregion

        #region 代理商订单是否加密
        /// <summary>
        /// added by wjp 2009-11-30
        ///  针对代理商订单类型
        /// </summary>
        public enum EIsEncrypt : int { 不加密 = 0, 加密 = 1 }

        #endregion

        #region 代理商工单生成类型
        /// <summary>
        /// added by wjp 2009-11-30
        ///  代理商工单生成类型
        /// </summary>
        public enum ECaseType : int { 主动 = 0, 被动 = 1 }

        #endregion

        #region 代理商工单状态
        /// <summary>
        /// added by wjp 2009-11-30
        ///  代理商工单状态
        /// </summary>
        public enum ECaseStatus : int { 初始化 = 1, 已接收 = 2, 待处理 = 3, 已处理 = 4, 取消 = 5 }

        #endregion

        #region 外部酒店来源
        public enum EHPRefHotelSource : int { 航信TravelHUB = 4, 到到网 = 11 };
        #endregion

        #region 外部酒店返佣类型
        public enum RefHotelCommType : int { 按金额返佣 = 0, 按比例返佣 = 1 }
        #endregion


        #region TravelHUB

        /// <summary>
        /// 房型的可申请状态
        /// </summary>
        public enum EHUBAvailabilityStatus : int { 可申请 = 1, 可确认 = 2, 不可用 = 3 };

        public enum EHUBOrderStatus : int
        {
            CON = 1,    //确认单
            RES = 10,   //申请单
            MOD = 20,   //修改单
            CAN = 30,   //取消单
            HAC = 40,   //拒绝单
            TST = 50,   //测试
            XXX = 60,   //客户取消
            RCM = 70,   //房间确认
        };

        #endregion

        #region  会员注册渠道 -------------------xiongxin  2010-05-18
        /// <summary>
        /// 会员注册渠道 
        /// </summary>
        public enum CustomerChannel : int { 金色世纪 = 0, 去哪儿 = 1, 到到网 = 2 }
        #endregion

        #region 航信HBE by wuweijun 2010-07-14
        public enum HbeTransactionName : int
        {
            HotelSearch = 1,
            SingleHotelSearch = 2,
            SingleHotelBaseInfoSearch = 3,
            SingleHotelRoomTypeSearch = 4,
            BookingHotel = 5,
            EditHotelOrder = 6,
            CancelOrder = 7,
            OrderSearch = 8,
            CheckinInfoSearch = 9,
            GuaranteeTypeAndTimeSearch = 10,

            OrderDetailSearch = 11,
            CreateExtendOrder = 12,
            OrderCreateProcess = 13,
            OrderGuestConfirm = 14,
            OrderGuestConfirmSearch = 15,

            PropertyCache = 16,


            //通知部分
            HotelStatusNotifyRS = 31,     //酒店上下线通知消息
            HotelPriceNotifyRS = 32,     //酒店房价早餐通知
            HotelAvailNotifyRS = 33,     //酒店房态通知
            HotelRoomStatusNotifyRS = 34,    //酒店房型上下线通知
            MessageTypeOrder = 40   //订单状态通知  


        }

        public enum HbeGuestStatus : int
        {
            未处理 = 1,
            处理中 = 2,
            已处理 = 3
        }
        #endregion

        #region 汇通HUBS by wuweijun 2010-11-25
        /// <summary>汇通天下API方法</summary>
        public enum HubsMsgType : int
        {
            getproplist = 100,
            getProperty = 101,
            getdesc = 102,
            getroomobj = 103,
            getrateobj = 104,
            getplanobj = 105,
            gettemplate = 106,
            getcratemap = 107,
            getonlineratemap = 108,
            getimage = 109,

            getpropresv = 110,
            newresv = 111,
            modresv = 112,
            remodresv = 113,
            cancelresv = 114,
            getresvaudit = 115
        };

        /// <summary>酒店API类型</summary>
        public enum EHotelApiType : int
        {
            航信 = 1,
            汇通天下 = 2,
            艺龙 = 3,
        };

        /// <summary>汇通天下价格状态</summary>
        public enum HubsRateState : int
        {
            A = 1,
            I = 2,
        };

        public enum EHotelBelongCommValue : int
        {
            固定佣金 = 1,
            销售价百分比 = 2,
        };

        public enum EHotelPlanPriceState : int
        {
            无房 = 0,
            限制 = 1,
            有房 = 2
        };

        public enum EHotelHubsIsassure : int
        {
            现付 = 0,
            担保 = 1,
            预付 = 2,
            公司担保 = 3,
        };

        public enum EHotelIsGuarantee : int
        {
            非担保 = 0,
            担保 = 1,
        };

        #endregion

        #region 艺龙Elong by wuweijun 2011-09-29
        public enum ElongMsgType : int
        {
            HotelListSearch = 100,
            HotelIdSearch = 101,
            HotelGeoSearch = 102,
            HotelBaseInfoCode = 103,
            HotelBrandSearch = 104,
        };

        public enum ElongGen : int
        {
            城市 = 1,
            行政区 = 2,
            商业区 = 3,
            城市标志物 = 4
        };

        public enum ElongGuestType : int
        {
            统一价 = 1,
            内宾 = 2,
            外宾 = 3,
            港澳台 = 4,
            日本 = 5
        };


        #endregion

        #region 机加酒
        //------------机加酒开始------------------//
        public enum ETHOperationType : int
        {
            生成订单 = 10,
            订单处理 = 20,//以后要详细分
        }


        public enum ETHOrderFlow : int
        {

            预订 = 10,
            处理中 = 20,
            已确认 = 30,
            已支付 = 40,
            完成 = 50,
            取消 = 60,
            作废 = 70,
            退票 = 80,

        }
        public enum EITicketAndHotelRoomType : int
        {
            单人床 = 1,
            大床 = 2
        }

        public enum EITicketAndHotelInternetType : int
        {
            拨号 = 1,
            无线 = 2,
            有线 = 3
        }
        //------------机加酒结束------------------//


        /// <summary>
        /// 电子代金券的状态  add by yanwei 2010-9-9
        /// </summary>
        #endregion

        public enum EVoucherStatus
        {
            已作废 = 1,
            正常使用 = 2
        }

        /// <summary>
        /// 退票支付处理相关类型 add by zpy 2010-12-21
        /// </summary>
        public enum TicketLossType
        {
            Pay = 1, NextPay = 2, RejectPay = 3, Redo = 4, NoLoss = 5
        }

        /// <summary>
        /// 国际机票-发票信息 by wuweijun 2010-12-30
        /// </summary>
        public enum EItInvoice
        {
            不要开发票 = 0,
            要开发票 = 1,
        }
        /// <summary>
        /// 国际机票-发票状态 by wangliuqin 2011-5-4
        /// </summary>
        public enum EIntInvoiceType
        {
            待开发票 = 1,
            发票已打印 = 2,
            发票已发送 = 3,
            发票已作废 = 4,
            发票已换开 = 5
        }
        /// <summary>
        /// 酒店归属-结算类型  by wanghuanan 2011-06-13
        /// </summary>
        public enum EHHotelBelongFinanceType : int { 按每个酒店单独生成结算单 = 1, 同一归属只生成一个结算单 = 2 };

        #region 会员特殊需求
        /// <summary>
        /// 会员特殊需求提醒方式
        /// </summary>
        public enum ECSRemainType : int { 页面 = 1, 工单 = 2 };

        /// <summary>
        /// 会员特殊需求模板类型
        /// </summary>
        public enum ECSTemplateType : int { 机票订单内部注意事项 = 1, 会员信息备注 = 2, 酒店订单备注 = 3 };

        /// <summary>
        /// 是否基于特定订单 是否需要根据合作商发工单
        /// </summary>
        public enum ECSISBasedOnOrder : int { 是 = 1, 否 = 0 }

        #endregion


    }
}
