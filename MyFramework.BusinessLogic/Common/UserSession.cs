using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web;
using System.Web.SessionState;
using System.Data;
using MyFramework.BusinessLogic.Common.SystemFrame;


namespace MyFramework.BusinessLogic.Common
{
    /// <summary>
    /// SessionInit 的摘要说明。
    /// </summary>
    public class UserSession
    {
        /// <summary>
        /// 此类用Session相关处理的操作
        /// </summary>
        UserInfo loUserInfo = null;
        System.Web.SessionState.HttpSessionState moSession = null;


        public UserSession()
        {
        }
        public UserSession(System.Web.SessionState.HttpSessionState toSession)
        {
            this.moSession = toSession;
        }
        public UserInfo UserInfo
        {
            get { return this.loUserInfo; }
        }
        public string Login(string tsUserAccount, string tsPassword, ref string tsUserHash, bool lsBool)
        {
            string lsMessage = "";

            string lsSql = "Select * from t_S_Employee where Employee_Login_ID='" + tsUserAccount + "'";

            DataTable lodtEmployee = new DataTable();

            DAL.DBA.FillDataTable(lodtEmployee, lsSql);
            if (lodtEmployee.Rows.Count >= 1)
            {

                try
                {
                    this.loUserInfo = new UserInfo();
                    loUserInfo.UserId = Convert.ToInt32(lodtEmployee.Rows[0]["Employee_ID"].ToString());
                    loUserInfo.UserName = lodtEmployee.Rows[0]["Employee_Name"].ToString();
                    loUserInfo.DeptId = Convert.ToInt32(lodtEmployee.Rows[0]["Dept_ID"].ToString());
                    loUserInfo.Is_Employee_Pwd_Reset = Convert.ToBoolean(IntUtil.SafeCInt(lodtEmployee.Rows[0]["Is_Employee_Pwd_Reset"]));
                    loUserInfo.Employee_Status = (EnumDef.EStatus)IntUtil.SafeCInt(lodtEmployee.Rows[0]["Employee_Status"].ToString());
                    loUserInfo.Login_ID = lodtEmployee.Rows[0]["Employee_Login_ID"].ToString();
                    loUserInfo.Is_Admin = Convert.ToBoolean(IntUtil.SafeCInt(lodtEmployee.Rows[0]["Is_Admin"]));
                    loUserInfo.Password = lodtEmployee.Rows[0]["employee_login_pwd"].ToString();
                    loUserInfo.PositionID = IntUtil.SafeCIntNullable(lodtEmployee.Rows[0]["Position_ID"]);
                    if (IntUtil.SafeCInt(lodtEmployee.Rows[0]["Employee_Status"]) != 1)
                    {
                        lsMessage = "此用户处于未激活状态！";
                        return lsMessage;
                    }
                    //密码不正确
                    if (loUserInfo.Password != CommonFunction.GetCode(tsPassword))
                    {
                        lsMessage = "登录密码错误，请重新输入！";
                        return lsMessage;
                    }
                    if (!lsBool)
                    {
                        if (StrUtil.SafeCString(lodtEmployee.Rows[0]["Is_Out_Accesss"]) != "1")
                        {
                            lsMessage = "此用户无权登录！";
                            return lsMessage;
                        }
                    }
                    #region 添加在线用户表记录
                    string lsSQL = "select HashCode from t_S_OnLine_Employee where Employee_ID=" + loUserInfo.UserId + " and rownum =1";
                    object loUserHash = DAL.DBA.ExecuteScalar(lsSQL);
                    if (loUserHash != null && loUserHash != DBNull.Value)
                        tsUserHash = loUserHash.ToString();
                    else
                    {
                        tsUserHash = CommonFunction.GetCode(loUserInfo.Login_ID + loUserInfo.Password);
                        int lnOnlineID = Convert.ToInt32(CommonDBFunction.GenerateSEQIdentity("t_S_OnLine_Employee"));
                        lsSQL = @"insert into t_S_OnLine_Employee(OnLine_Employee_id,Employee_ID,Status,HashCode,LoginTime) values(" + lnOnlineID.ToString() + "," + loUserInfo.UserId.ToString() + ",1,'" + tsUserHash + "',to_date('" + DateTime.Now + "','yyyy-MM-dd HH24:MI:SS'))";
                        DAL.DBA.ExecSQL("userlogin", lsSQL, ref lsMessage);
                    }
                    #endregion

                    this.moSession["UserSession"] = this;
                    this.InitMenu();


                    return lsMessage;
                }
                catch (Exception ex)
                {
                    lsMessage = "登陆失败！\r\n" + ex.Message;
                    return lsMessage;
                }

            }
            else
            {
                lsMessage = "用户“" + tsUserAccount + "”不存在！";
                return lsMessage;
            }


        }

        #region added by chj
        public string LoginWithoutPwd(string tsUserAccount, string tsPassword, ref string tsUserHash, bool lsBool, ref bool isShowPwd)
        {
            string lsMessage = "";

            string lsSql = "Select * from t_S_Employee where Employee_Login_ID='" + tsUserAccount + "'";

            DataTable lodtEmployee = new DataTable();

            DAL.DBA.FillDataTable(lodtEmployee, lsSql);
            if (lodtEmployee.Rows.Count >= 1)
            {

                try
                {
                    this.loUserInfo = new UserInfo();
                    loUserInfo.UserId = Convert.ToInt32(lodtEmployee.Rows[0]["Employee_ID"].ToString());
                    loUserInfo.UserName = lodtEmployee.Rows[0]["Employee_Name"].ToString();
                    loUserInfo.DeptId = Convert.ToInt32(lodtEmployee.Rows[0]["Dept_ID"].ToString());
                    loUserInfo.Is_Employee_Pwd_Reset = Convert.ToBoolean(IntUtil.SafeCInt(lodtEmployee.Rows[0]["Is_Employee_Pwd_Reset"]));
                    loUserInfo.Employee_Status = (EnumDef.EStatus)IntUtil.SafeCInt(lodtEmployee.Rows[0]["Employee_Status"].ToString());
                    loUserInfo.Login_ID = lodtEmployee.Rows[0]["Employee_Login_ID"].ToString();
                    loUserInfo.Is_Admin = Convert.ToBoolean(IntUtil.SafeCInt(lodtEmployee.Rows[0]["Is_Admin"]));
                    loUserInfo.Password = lodtEmployee.Rows[0]["employee_login_pwd"].ToString();
                    loUserInfo.PositionID = IntUtil.SafeCIntNullable(lodtEmployee.Rows[0]["Position_ID"]);
                    if (IntUtil.SafeCInt(lodtEmployee.Rows[0]["Employee_Status"]) != 1)
                    {
                        lsMessage = "此用户处于未激活状态！";
                        return lsMessage;
                    }

                    //第一次登录
                    if (IntUtil.SafeCInt(lodtEmployee.Rows[0]["IS_FIRST_LOGIN"]) != 1)
                    {
                        if (string.IsNullOrEmpty(tsPassword))
                        {
                            lsMessage = "第一次登录，请输入密码！";
                            isShowPwd = true;
                            return lsMessage;
                        }
                        else
                        {
                            //密码不正确
                            if (loUserInfo.Password != CommonFunction.GetCode(tsPassword))
                            {
                                lsMessage = "登录密码错误，请重新输入！";
                                isShowPwd = true;
                                return lsMessage;
                            }
                        }
                    }

                    if (!lsBool)
                    {
                        if (StrUtil.SafeCString(lodtEmployee.Rows[0]["Is_Out_Accesss"]) != "1")
                        {
                            lsMessage = "此用户无权登录！";
                            return lsMessage;
                        }
                    }
                    #region 添加在线用户表记录
                    string lsSQL = "select HashCode from t_S_OnLine_Employee where Employee_ID=" + loUserInfo.UserId + " and rownum =1";
                    object loUserHash = DAL.DBA.ExecuteScalar(lsSQL);
                    if (loUserHash != null && loUserHash != DBNull.Value)
                        tsUserHash = loUserHash.ToString();
                    else
                    {
                        tsUserHash = CommonFunction.GetCode(loUserInfo.Login_ID + loUserInfo.Password);
                        int lnOnlineID = Convert.ToInt32(CommonDBFunction.GenerateSEQIdentity("t_S_OnLine_Employee"));
                        lsSQL = @"insert into t_S_OnLine_Employee(OnLine_Employee_id,Employee_ID,Status,HashCode,LoginTime) values(" + lnOnlineID.ToString() + "," + loUserInfo.UserId.ToString() + ",1,'" + tsUserHash + "',to_date('" + DateTime.Now + "','yyyy-MM-dd HH24:MI:SS'))";
                        DAL.DBA.ExecSQL("userlogin", lsSQL, ref lsMessage);
                    }
                    #endregion

                    this.moSession["UserSession"] = this;
                    this.InitMenu();


                    return lsMessage;
                }
                catch (Exception ex)
                {
                    lsMessage = "登陆失败！\r\n" + ex.Message;
                    return lsMessage;
                }

            }
            else
            {
                lsMessage = "用户“" + tsUserAccount + "”不存在！";
                return lsMessage;
            }


        }
        #endregion

        public Boolean ReLogin(string tsHashCode)
        {
            if (tsHashCode == "") return false;
            string lsSql = @"select b.* from t_S_OnLine_Employee a inner join t_S_Employee b on a.Employee_Id=b.Employee_Id
                           where a.HashCode='" + tsHashCode + "'";

            DataTable lodtEmployee = new DataTable();

            DAL.DBA.FillDataTable(lodtEmployee, lsSql);

            if (lodtEmployee.Rows.Count == 1)
            {

                this.loUserInfo = new UserInfo();
                loUserInfo.UserId = Convert.ToInt32(lodtEmployee.Rows[0]["Employee_ID"].ToString());
                loUserInfo.UserName = lodtEmployee.Rows[0]["Employee_Name"].ToString();
                loUserInfo.DeptId = Convert.ToInt32(lodtEmployee.Rows[0]["Dept_ID"].ToString());
                loUserInfo.Is_Employee_Pwd_Reset = Convert.ToBoolean(IntUtil.SafeCInt(lodtEmployee.Rows[0]["Is_Employee_Pwd_Reset"]));
                loUserInfo.Employee_Status = (EnumDef.EStatus)IntUtil.SafeCInt(lodtEmployee.Rows[0]["Employee_Status"].ToString());
                loUserInfo.Login_ID = lodtEmployee.Rows[0]["Employee_Login_ID"].ToString();
                loUserInfo.Is_Admin = Convert.ToBoolean(IntUtil.SafeCInt(lodtEmployee.Rows[0]["Is_Admin"]));
                loUserInfo.Password = lodtEmployee.Rows[0]["employee_login_pwd"].ToString();
                loUserInfo.PositionID = IntUtil.SafeCIntNullable(lodtEmployee.Rows[0]["Position_ID"]);
                this.moSession["UserSession"] = this;
                this.InitMenu();
                return true;
            }
            else
                return false;

        }
        private void GetUserAvailablePage(int tnUserId)
        {
            
        }
        private List<Int32> GetUserFunctionIds()
        {
            // this.moSession["UserPage"]; 
            string lsSql = @"select distinct e.Menu_ID from t_S_Page e inner join 
                    (select a.Employee_ID,c.Page_Id from t_S_Employee_Role a 
                     inner join t_S_Role b on a.ROLE_ID=b.Role_ID
                     inner join t_S_Role_Page c on b.Role_ID=c.Role_ID
                     where a.Valid_Date<Sysdate and a.Invalid_Date>Sysdate and a.Employee_ID=" + this.loUserInfo.UserId
                      + @" union 
                     select Employee_ID,Page_ID from t_S_Employee_Module_Add d
                     where d.Valid_Date<Sysdate and d.Invalid_Date>Sysdate and d.Employee_ID=" + this.loUserInfo.UserId + ") f on e.Page_ID=f.Page_ID";

            //获取此用户所有有权限的菜单
            DataTable lodtMenu = new DataTable();

            DAL.DBA.FillDataTable(lodtMenu, lsSql);

            //获取用户有权限的
            List<Int32> loFunctions = new List<int>();
            foreach (DataRow drMenu in lodtMenu.Rows)
            {
                loFunctions.Add(Convert.ToInt32(drMenu["Menu_ID"].ToString()));    
            }
            return loFunctions;

        }


        /// <summary>
        /// 初始化菜单操作
        /// </summary>
        public void InitMenu()
        {
            #region "初始化菜单"
            //初始化菜单
            string lsSql = @"select a.Menu_ID,a.Parent_Menu_ID,a.Menu_Name,a.Menu_Level,a.Menu_Desc,b.PAGE_URL from t_S_Menu a
                                         left join t_S_Page b on a.Menu_ID=b.Menu_ID and b.PAGE_TYPE=1 order by a.Menu_Level,a.Menu_Pri,a.Parent_Menu_ID,a.Menu_ID";

            DataTable lodtMenu = new DataTable();

            DAL.DBA.FillDataTable(lodtMenu, lsSql);
            MenuStructure loMenuStructure = new MenuStructure();
            foreach (DataRow drMenu in lodtMenu.Rows)
            {
                Menuitem loMenuitem = new Menuitem();
                loMenuitem.MenuID = drMenu["Menu_ID"].ToString();
                loMenuitem.ParentMenuID = drMenu["Parent_Menu_ID"].ToString();
                loMenuitem.Caption = drMenu["Menu_Name"].ToString();
                loMenuitem.HyperLink = drMenu["PAGE_URL"].ToString();
                if (loMenuitem.HyperLink == "")
                    loMenuitem.HyperLink = "/MyFramework/Default.aspx";
                loMenuitem.Level = drMenu["Menu_Level"].ToString();
                loMenuStructure.AddMenuitem(loMenuitem);

            }
            if (!loUserInfo.Is_Admin)
            {
                List<Int32> loUserFunctionIds = this.GetUserFunctionIds();
                loMenuStructure.EnavailableMenus(loUserFunctionIds);
            }
            this.moSession["MenuStructure"] = loMenuStructure;
            #endregion

            #region "以后要去掉"

            //MenuStructure loMenuStructure = new MenuStructure();
            //loMenuStructure.AddMenuitem(new Menuitem("1", null, "系统管理", "/MyFramework/Default.aspx", "0"));
            //loMenuStructure.AddMenuitem(new Menuitem("11", "1", "员工管理", "/MyFramework/Default.aspx", "1"));
            //loMenuStructure.AddMenuitem(new Menuitem("111", "11", "部门管理", "/MyFramework/Employee/DepartmentAdmin/DepartmentList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("112", "11", "员工管理", "/MyFramework/Employee/EmployeeAdmin/EmpoyeeList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("113", "11", "角色管理", "/MyFramework/Employee/RoleAdmin/RoleList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("12", "1", "系统字典", "/MyFramework/Default.aspx", "1"));
            //loMenuStructure.AddMenuitem(new Menuitem("121", "12", "地标类别", "/MyFramework/Admin/LandMark/LandMarkTypeList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("122", "12", "地标", "/MyFramework/Admin/MarkType/MarkTypeList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("123", "12", "地级市", "/MyFramework/Admin/CountyMark/CountyList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("124", "12", "县市", "/MyFramework/Admin/CityMark/CityList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("125", "12", "乡镇", "/MyFramework/Admin/TownMark/TownList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("126", "12", "货币", "/MyFramework/Admin/Currency/CurrencyList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("127", "12", "信用卡类型", "/MyFramework/Admin/CreditCard/CareditCardList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("128", "12", "图片类型", "/MyFramework/Admin/PixType/PixTypeList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("129", "12", "保险类型", "/MyFramework/Admin/InsuranceType/InsuranceList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("130", "12", "收入等级", "/MyFramework/Admin/LncomeLevelType/LncomeLevelTypeList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("131", "12", "工单类型", "/MyFramework/PublicCase/CaseType/CaseTypeList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("133", "12", "工单小类", "/MyFramework/Admin/CaseSubType/CaseSubType.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("134", "12", "员工职位", "/MyFramework/Admin/EmplyeePosition/EmplyeePositionList.aspx", "2"));


            //loMenuStructure.AddMenuitem(new Menuitem("2", null, "酒店业务", "/MyFramework/Default.aspx", "0"));
            //loMenuStructure.AddMenuitem(new Menuitem("21", "2", "酒店预订", "/MyFramework/Default.aspx", "1"));
            //loMenuStructure.AddMenuitem(new Menuitem("211", "21", "生成订单", "/MyFramework/Hotel/HotelOrder/HotelOrderQuery.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("212", "21", "预订监控", "/MyFramework/Hotel/HotelOrder/HotelOrderQuery.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("213", "21", "真伪鉴别", "/MyFramework/Hotel/HotelOrder/HOVerifyList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("214", "21", "确认处理", "/MyFramework/Hotel/HotelOrder/HotelConfirmList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("215", "21", "担保处理", "/MyFramework/Hotel/HotelOrder/HotelAssureList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("216", "21", "担保确认", "/MyFramework/Hotel/HotelOrder/HOAssureConfirmList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("217", "21", "失败处理", "/MyFramework/Hotel/HotelOrder/HotelFailList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("218", "21", "问题订单", "/MyFramework/Hotel/HotelOrder/HotelQuestionList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("219", "21", "代收代付处理", "/MyFramework/Hotel/HotelOrder/HOPaymentList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("220", "21", "催单", "/MyFramework/Hotel/HotelOrder/HotelUrgeList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("221", "21", "跟单", "/MyFramework/Hotel/HotelOrder/HotelFollowList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("222", "21", "生成占房单", "/MyFramework/Hotel/HotelOrder/HOORderOccupt/HOORderOccuptQuery.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("223", "21", "协商处理", "/MyFramework/Hotel/HotelOrder/HOOrderNego/HOOrderNegoList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("224", "21", "无房找房", "/MyFramework/Hotel/HotelOrder/HONoRommList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("228", "21", "通知客人", "/MyFramework/Hotel/HotelOrder/HotelNotifyCustomList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("229", "21", "订单查询", "/MyFramework/Hotel/HotelOrder/OrderyQuery.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("22", "2", "酒店产品", "/Default.asp", "1"));
            //loMenuStructure.AddMenuitem(new Menuitem("225", "22", "酒店管理", "/MyFramework/Hotel/HotelAdmin/HotelQuery.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("226", "22", "房态管理", "/MyFramework/Hotel/HotelAdmin/HotelRoomStatusQuery.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("227", "22", "免费房管理", "/MyFramework/Hotel/HotelAdmin/HotelHouseRoom/HFreeRoomList.aspx", "2"));

            //loMenuStructure.AddMenuitem(new Menuitem("23", "2", "酒店审核", "/Default.asp", "1"));
            //loMenuStructure.AddMenuitem(new Menuitem("231", "23", "审核分区", "/MyFramework/Hotel/HotelOrder/HOAreaList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("232", "23", "电话审核分配", "/MyFramework/Hotel/HotelOrder/HOChkRmDy.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("233", "23", "发送传真", "/MyFramework/Hotel/HotelAdmin/HotelQuery.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("235", "23", "电话审核", "/MyFramework/Hotel/HotelOrder/HAuditing/HPhoneAuditingList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("236", "23", "公司审核", "/MyFramework/Hotel/HotelOrder/HAuditing/HOtherAuditingList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("237", "23", "审核监控", "/MyFramework/Hotel/HotelAdmin/HotelQuery.aspx", "2"));

            //loMenuStructure.AddMenuitem(new Menuitem("24", "2", "酒店结算", "/Default.asp", "1"));
            //loMenuStructure.AddMenuitem(new Menuitem("241", "24", "发对账单", "/MyFramework/Hotel/HotelSettlement/HotelSettlementQuery.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("242", "24", "对账确认", "/MyFramework/Hotel/HotelSettlement/HotelSettlementQuery.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("243", "24", "对账记录查询", "/MyFramework/Hotel/HotelSettlement/HotelSettlementQuery.aspx", "2"));
            ////loMenuStructure.AddMenuitem(new Menuitem("244", "24", "合作方到帐查询", "/MyFramework/Finance/FPPymtQuery.aspx", "3"));
            //loMenuStructure.AddMenuitem(new Menuitem("245", "24", "催帐", "/MyFramework/Hotel/HotelSettlement/HotelSettlementBillConfirm.aspx", "2"));
            ////  loMenuStructure.AddMenuitem(new Menuitem("246", "24", "应收款查询", "/MyFramework/Hotel/HotelSettlement/HotelSettleShouldPayQuery.aspx", "3"));

            //loMenuStructure.AddMenuitem(new Menuitem("25", "2", "酒店核销", "/Default.asp", "1"));
            //loMenuStructure.AddMenuitem(new Menuitem("251", "25", "收款登记", "/MyFramework/Finance/FPPymtQuery.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("252", "25", "账款核销", "/MyFramework/Hotel/HotelSettlement/HFDispose.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("253", "25", "应收款查询", "/MyFramework/Hotel/HotelSettlement/HotelSettleShouldPayQuery.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("254", "25", "核销记录查询", "/MyFramework/Hotel/HotelSettlement/HotelSettlementCheckRecord.aspx", "2"));


            //loMenuStructure.AddMenuitem(new Menuitem("26", "2", "酒店字典", "/Default.asp", "1"));
            //loMenuStructure.AddMenuitem(new Menuitem("261", "26", "酒店性质", "/MyFramework/Hotel/HotelDictionary/HotelProperty/HotelPropertyList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("262", "26", "酒店类别", "/MyFramework/Hotel/HotelDictionary/HotelType/HotelTypeList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("263", "26", "酒店类型服务", "/MyFramework/Hotel/HotelDictionary/ServiceType/ServiceTypeList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("264", "26", "酒店星级", "/MyFramework/Hotel/HotelDictionary/HotelGradeType/HotelGradeTypeList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("265", "26", "酒店装修级别", "/MyFramework/Hotel/HotelDictionary/HotelDecType/HotelDecTypeList.aspx", "2"));


            //loMenuStructure.AddMenuitem(new Menuitem("3", null, "机票业务", "/MyFramework/Ticket/Default.aspx", "0"));
            //loMenuStructure.AddMenuitem(new Menuitem("31", "3", "机票业务", "-", "1"));
            //loMenuStructure.AddMenuitem(new Menuitem("311", "31", "国内机票", "/MyFramework/Ticket/Ticket/TicketOrderChina_Search.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("302", "31", "订单导入", "/MyFramework/Ticket/Ticket/TicketOrderChina_Order.aspx?IsImport=1", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("301", "31", "机票预订", "/MyFramework/Ticket/Ticket/TicketOrderChina_Order.aspx?User=liyao&Argument=B|A|1530.00|90|-1|-1|0||&CommandName=50|80|NAY|CAN|FM2275|1650|1930|20071010|1700.00&SrcCity=BJ%b1%b1%be%a9&Grade=0", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("312", "31", "待出订单", "/MyFramework/Ticket/Ticket/OrderDetailList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("313", "31", "领班管理", "/MyFramework/Ticket/Ticket/LeaderManagerList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("314", "31", "退改签", "/MyFramework/Ticket/Ticket/TicketOrderChangeList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("315", "31", "外出票", "/MyFramework/Ticket/Ticket/PrintOutOrder.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("316", "31", "自出票", "/MyFramework/Ticket/Ticket/PrintInnerOrder.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("317", "31", "分配机票", "/MyFramework/Ticket/Ticket/OrderDispense.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("318", "31", "收银", "/MyFramework/Ticket/Ticket/BalanceOrder.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("319", "31", "退改签审核", "/MyFramework/Ticket/Ticket/TicketBackConfirmList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("320", "31", "订单支付处理", "/MyFramework/Ticket/TicketFinance/TicketPayTaskList.aspx", "2"));

            //loMenuStructure.AddMenuitem(new Menuitem("32", "3", "机票产品", "-", "1"));
            //loMenuStructure.AddMenuitem(new Menuitem("321", "32", "航空公司", "/MyFramework/Ticket/TicketProduct/AirlineManagePage.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("322", "32", "航班", "/MyFramework/Ticket/TicketProduct/FlightManagePage.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("323", "32", "机型", "/MyFramework/Ticket/TicketProduct/FlightTypeManagePage.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("324", "32", "机场", "/MyFramework/Ticket/TicketProduct/AirPortManagePage.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("325", "32", "舱位", "/MyFramework/Ticket/TicketProduct/CabinManagePage.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("326", "32", "合作商", "/MyFramework/Ticket/PartnerManageForm.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("327", "32", "送票员", "/MyFramework/Ticket/EmployeeDeliverManageForm.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("328", "32", "结算政策", "/MyFramework/Ticket/TicketPolicy/IssueBill.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("329", "32", "特价结算政策", "/MyFramework/Ticket/TicketPolicy/SpecialIssueBill.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("330", "32", "合作商用户关系", "/MyFramework/Ticket/TicketProduct/PartnerEmployeeList.aspx", "2"));

            //loMenuStructure.AddMenuitem(new Menuitem("33", "3", "机票结算", "-", "1"));
            //loMenuStructure.AddMenuitem(new Menuitem("331", "33", "生成结算单", "/MyFramework/Ticket/TicketFinance/TicketParnterBill.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("332", "33", "结算单列表", "/MyFramework/Ticket/TicketFinance/TicketBillList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("333", "33", "支付记录录入", "/MyFramework/Finance/FPPaymentRecordInput.aspx?CustomerID=1", "2"));

            //loMenuStructure.AddMenuitem(new Menuitem("34", "3", "机票核销", "-", "1"));
            //loMenuStructure.AddMenuitem(new Menuitem("341", "34", "收款登记", "/MyFramework/Finance/FPPymtQuery.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("342", "34", "账款核销", "/MyFramework/Ticket/TicketFinance/TicketDispose.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("343", "34", "应收款查询", "/MyFramework/Ticket/TicketFinance/TicketShouldPayQuery.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("344", "34", "核销记录查询", "/MyFramework/Ticket/TicketFinance/TicketSettlementCheckRecord.aspx", "2"));


            //loMenuStructure.AddMenuitem(new Menuitem("5", null, "会员服务", "/MyFramework/Default.aspx", "0"));
            //loMenuStructure.AddMenuitem(new Menuitem("51", "5", "调查管理", "/MyFramework/Default.aspx", "1"));
            //loMenuStructure.AddMenuitem(new Menuitem("511", "51", "调查任务管理", "/MyFramework/MemberService/MemberVote/MemberVoteTaskList.aspx", "2"));
            ////loMenuStructure.AddMenuitem(new Menuitem("512", "51", "调查任务项目", "/MyFramework/MemberService/MemberItem/MemberVoteItemDef.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("52", "5", "会员字典", "/MyFramework/Default.aspx", "1"));
            //loMenuStructure.AddMenuitem(new Menuitem("527", "52", "会员类型", "/MyFramework/Admin/CustomerClass/CustomerClassList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("528", "52", "返款银行类别", "/MyFramework/Admin/Bank/BankTypeList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("521", "52", "回访任务类型", "/MyFramework/MemberService/MemberDictionary/CBKTaskType/CBKTaskTypeList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("522", "52", "投诉原因", "/MyFramework/MemberService/Complaint/ComplaintReasonList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("523", "52", "投诉方式", "/MyFramework/MemberService/Complaint/ComplaintMeansList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("524", "52", "投诉结果", "/MyFramework/MemberService/Complaint/ComplaintReaultList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("525", "52", "赔偿类型", "/MyFramework/MemberService/Complaint/ReimbTypeList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("526", "52", "支付帐号类型 ", "/MyFramework/MemberService/Complaint/PymtAccountTypeList.aspx", "2"));

            //loMenuStructure.AddMenuitem(new Menuitem("54", "5", "回访管理", "/MyFramework/Default.aspx", "1"));
            //loMenuStructure.AddMenuitem(new Menuitem("541", "54", "回访模板管理", "/MyFramework/MemberService/MemberTemplate/CBKTemplateList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("542", "54", "新建回访任务", "/MyFramework/MemberService/CBKManagement/CBKManagementNewTask.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("544", "54", "回访任务管理", "/MyFramework/MemberService/CBKManagement/CBKManagementTaskManage.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("545", "54", "开展回访工作", "/MyFramework/MemberService/CBKManagement/ImplementCBKTask.aspx", "2"));

            //loMenuStructure.AddMenuitem(new Menuitem("55", "5", "会员管理", "/MyFramework/Default.aspx", "1"));
            //loMenuStructure.AddMenuitem(new Menuitem("551", "55", "会员查询", "/MyFramework/Member/MemberQuery.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("552", "55", "车险管理", "/MyFramework/AutomobileInsurance/CustomerQuery.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("553", "55", "会员分红记录", "/MyFramework/Member/DividendRecord.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("554", "55", "保险管理", "/MyFramework/Member/InsuranceRecord.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("555", "55", "会员录入", "/MyFramework/Member/MCCard_Customer.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("556", "55", "保单管理", "/MyFramework/Member/MemberGuaranteeQuery.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("557", "5 5", "返款帐号", "/MyFramework/Member/ReturnAccountList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("558", "55", "车险客户录入", "/MyFramework/AutomobileInsurance/CVICustomerInfo.aspx", "2"));

            //loMenuStructure.AddMenuitem(new Menuitem("56", "5", "奖励管理", "/MyFramework/Default.aspx", "1"));
            //loMenuStructure.AddMenuitem(new Menuitem("561", "56", "商品信息管理", "/MyFramework/MemberService/MemberPoint/ProductsManage.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("562", "56", "会员奖励申请", "/MyFramework/MemberService/MemberEncourage/MemberApplyEncourage.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("563", "56", "奖励申请管理", "/MyFramework/MemberService/MemberEncourage/MemberApplyManage.aspx", "2"));

            //loMenuStructure.AddMenuitem(new Menuitem("564", "56", "现金兑换申请", "/MyFramework/MemberService/MemberCashExchange/MemberApplyCashExchange.aspx", "2"));

            //loMenuStructure.AddMenuitem(new Menuitem("565", "56", "生成分红报表", "/MyFramework/MemberService/MemberCashExchange/ExportDvdList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("566", "56", "已导出分红记录", "/MyFramework/MemberService/MemberCashExchange/ExportedRecordsList.aspx", "2"));

            //loMenuStructure.AddMenuitem(new Menuitem("6", null, "公共任务", "/MyFramework/PublicCase/Default.aspx", "0"));
            //loMenuStructure.AddMenuitem(new Menuitem("61", "6", "系统任务", "/MyFramework/PublicCase/Default.aspx", "1"));
            //loMenuStructure.AddMenuitem(new Menuitem("611", "61", "新建工单任务", "/MyFramework/PublicCase/AddNewCase.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("613", "61", "我的任务", "/MyFramework/PublicCase/PublicCaseMyTask.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("614", "61", "部门任务", "/MyFramework/PublicCase/PublicCaseSystemTask.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("615", "61", "待发送短信", "/MyFramework/SMS/AddNewSmsPage.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("616", "61", "待发送消息", "/MyFramework/Message/AddNewMessagePage.aspx", "2"));

            //loMenuStructure.AddMenuitem(new Menuitem("7", null, "市场营销", "/MyFramework/Default.aspx", "0"));
            //loMenuStructure.AddMenuitem(new Menuitem("71", "7", "代理商管理", "/MyFramework/Default.aspx", "1"));
            //loMenuStructure.AddMenuitem(new Menuitem("711", "71", "代理商维护", "/MyFramework/Agent/AgentList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("712", "71", "代理商预订规则", "/MyFramework/Agent/AgentManager/ReservRuleList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("713", "71", "代理商售卡业务规则", "/MyFramework/Agent/AgentManager/CardSaleRuleList.aspx", "2"));

            //loMenuStructure.AddMenuitem(new Menuitem("72", "7", "代理商结算", "/MyFramework/Default.aspx", "1"));
            //loMenuStructure.AddMenuitem(new Menuitem("721", "72", "代理商政策维护", "/MyFramework/Agent/AgentManager/AgentReservCommRuleList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("722", "72", "代理商结算", "/MyFramework/Agent/AgentBill/AgentBill.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("723", "72", "结算列表", "/MyFramework/Agent/AgentBill/AgentBillList.aspx", "2"));


            //loMenuStructure.AddMenuitem(new Menuitem("8", null, "贵宾厅", "/MyFramework/Default.aspx", "0"));
            //loMenuStructure.AddMenuitem(new Menuitem("81", "8", "会员接待", "/MyFramework/Default.aspx", "1"));
            //loMenuStructure.AddMenuitem(new Menuitem("811", "81", "航班提示", "/MyFramework/VIPLounge/VFlightRemind.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("812", "81", "售卡管理", "/MyFramework/VIPLounge/VCardSale.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("813", "81", "退卡管理", "/MyFramework/VIPLounge/VCardSaleRecordQuery.aspx", "2"));

            //loMenuStructure.AddMenuitem(new Menuitem("82", "8", "费用管理", "/MyFramework/Default.aspx", "1"));
            //loMenuStructure.AddMenuitem(new Menuitem("821", "82", "费用信息", "/MyFramework/VIPLounge/FeeInfoView.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("822", "82", "合同信息", "/MyFramework/VIPLounge/ContactInfoView.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("823", "82", "员工房租信息", "/MyFramework/VIPLounge/EmployeeRentInfoView.aspx", "2"));


            //loMenuStructure.AddMenuitem(new Menuitem("83", "8", "贵宾厅字典", "/MyFramework/Default.aspx", "1"));
            //loMenuStructure.AddMenuitem(new Menuitem("831", "83", "商品类别", "/MyFramework/VIPLounge/ProductTypeView.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("832", "83", "商品信息", "/MyFramework/VIPLounge/ProductInfoView.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("833", "83", "供应商管理", "/MyFramework/VIPLounge/VendorView.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("834", "83", "支出项目类型", "/MyFramework/VIPLounge/PaymentItemTypeView.aspx", "2"));

            //loMenuStructure.AddMenuitem(new Menuitem("9", null, "特商管理", "/MyFramework/Default.aspx", "0"));
            //loMenuStructure.AddMenuitem(new Menuitem("91", "9", "餐饮预订", "/MyFramework/Default.aspx", "1"));
            //loMenuStructure.AddMenuitem(new Menuitem("911", "91", "餐饮预订", "/MyFramework/ContractVendor/CVOOrderVendorQuery.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("912", "91", "餐饮订单查询", "/MyFramework/ContractVendor/CVOOrderQuery.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("92", "9", "高尔夫预订", "/MyFramework/Default.aspx", "1"));
            //loMenuStructure.AddMenuitem(new Menuitem("921", "92", "高尔夫预订", "/MyFramework/Default.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("922", "92", "高尔夫订单查询", "/MyFramework/Default.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("93", "9", "商户管理", "/MyFramework/Default.aspx", "1"));
            //loMenuStructure.AddMenuitem(new Menuitem("931", "93", "商户维护", "/MyFramework/ContractVendor/CVVendorInfoQuery.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("932", "93", "商户查询", "/MyFramework/Default.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("94", "9", "特商字典", "/MyFramework/Default.aspx", "1"));
            //loMenuStructure.AddMenuitem(new Menuitem("941", "94", "商户类别", "/MyFramework/ContractVendor/CVTypeView.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("942", "94", "商户属性字典", "/MyFramework/ContractVendor/AttributeDctionaryView.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("943", "94", "商户特色", "/MyFramework/ContractVendor/VendorSpecialView.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("944", "94", "客户流量", "/MyFramework/ContractVendor/CustomerVolumeView.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("945", "94", "人均消费", "/MyFramework/ContractVendor/AvgConsumptionView.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("946", "94", "经营方式", "/MyFramework/ContractVendor/OperationModeView.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("947", "94", "商户图片类型", "/MyFramework/ContractVendor/ImageStyleView.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("948", "94", "口味", "/MyFramework/ContractVendor/TasteTypeView.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("949", "94", "环境气氛", "/MyFramework/ContractVendor/EnvironmentView.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("950", "94", "餐馆特色", "/MyFramework/ContractVendor/SpecialServiceView.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("951", "94", "服务项目", "/MyFramework/ContractVendor/CVDServiceView.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("952", "94", "菜单类别", "/MyFramework/ContractVendor/MenuTypeView.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("953", "94", "周边环境", "/MyFramework/ContractVendor/SDEnvironmentView.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("95", "9", "特商活动管理", "/MyFramework/Default.aspx", "1"));
            //loMenuStructure.AddMenuitem(new Menuitem("954", "95", "促销活动", "/MyFramework/ContractVendor/CVPromoteSaleView.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("955", "95", "最新动态", "/MyFramework/ContractVendor/TopActiveView.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("956", "95", "热门推荐", "/MyFramework/ContractVendor/PopRecommendView.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("957", "95", "精品推荐", "/MyFramework/ContractVendor/ElaborateRecommendView.aspx", "2"));
            ////loMenuStructure.AddMenuitem(new Menuitem("821", "82", "高尔夫", "/MyFramework/Default.aspx", "2"));


            //loMenuStructure.AddMenuitem(new Menuitem("10", null, "卡管理", "/MyFramework/Default.aspx", "0"));
            //loMenuStructure.AddMenuitem(new Menuitem("101", "10", "卡管理", "/MyFramework/Card/MCCardNoSessionQuery.aspx", "1"));
            //loMenuStructure.AddMenuitem(new Menuitem("1011", "101", "卡号段管理", "/MyFramework/Card/MCCardNoSessionQuery.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("1012", "101", "卡类型", "/MyFramework/Card/CardTypeDef.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("1013", "101", "生成卡号", "/MyFramework/Card/MCCardNoMark.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("1014", "101", "制卡", "/MyFramework/Card/MCCardMake.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("1015", "101", "领卡", "/MyFramework/Card/MCCardOut.aspx", "2"));


            //loMenuStructure.AddMenuitem(new Menuitem("18", null, "财务管理", "/MyFramework/Default.aspx", "0"));
            //loMenuStructure.AddMenuitem(new Menuitem("181", "18", "酒店核销", "/Default.asp", "1"));
            //loMenuStructure.AddMenuitem(new Menuitem("1811", "181", "收款登记", "/MyFramework/Finance/FPPymtQuery.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("1812", "181", "账款核销", "/MyFramework/Hotel/HotelSettlement/HFDispose.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("1813", "181", "应收款查询", "/MyFramework/Hotel/HotelSettlement/HotelSettleShouldPayQuery.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("1814", "181", "核销记录查询", "/MyFramework/Hotel/HotelSettlement/HotelSettlementCheckRecord.aspx", "2"));

            //loMenuStructure.AddMenuitem(new Menuitem("19", null, "决策支持", "/MyFramework/Default.aspx", "0"));
            //loMenuStructure.AddMenuitem(new Menuitem("191", "19", "预订分析", "/MyFramework/Default.aspx", "1"));
            //loMenuStructure.AddMenuitem(new Menuitem("1911", "191", "酒店订单", "/MyFramework/Default.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("1912", "191", "机票订单", "/MyFramework/Default.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("1913", "191", "餐饮订单", "/MyFramework/Default.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("1914", "191", "订单综合分析", "/MyFramework/Default.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("192", "19", "会服分析", "/MyFramework/Default.aspx", "1"));
            //loMenuStructure.AddMenuitem(new Menuitem("1921", "192", "会员分析", "/MyFramework/Default.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("1922", "192", "投诉分析", "/MyFramework/Default.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("1923", "192", "消费模式分析", "/MyFramework/Default.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("193", "19", "市场分析", "/MyFramework/Default.aspx", "1"));
            //loMenuStructure.AddMenuitem(new Menuitem("1931", "193", "代理商分析", "/MyFramework/Default.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("1932", "193", "营销策略分析", "/MyFramework/Default.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("1933", "193", "投资回报分析", "/MyFramework/Default.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("194", "19", "合作商分析", "/MyFramework/Default.aspx", "1"));
            //loMenuStructure.AddMenuitem(new Menuitem("1941", "194", "酒店产品分析", "/MyFramework/Default.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("1942", "194", "机票产品分析", "/MyFramework/Default.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("1943", "194", "合作效益分析", "/MyFramework/Default.aspx", "2"));

            //loMenuStructure.AddMenuitem(new Menuitem("195", "19", "销售分析", "/MyFramework/Default.aspx", "1"));
            //loMenuStructure.AddMenuitem(new Menuitem("1951", "195", "酒店产品分析", "/MyFramework/Default.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("1952", "195", "机票产品分析", "/MyFramework/Default.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("1953", "195", "合作效益分析", "/MyFramework/Default.aspx", "2"));

            //loMenuStructure.AddMenuitem(new Menuitem("196", "19", "贵宾厅分析", "/MyFramework/Default.aspx", "1"));
            //loMenuStructure.AddMenuitem(new Menuitem("1961", "196", "商品销售分析", "/MyFramework/Default.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("1962", "196", "会员接待分析", "/MyFramework/Default.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("1963", "196", "客户分析", "/MyFramework/Default.aspx", "2"));

            //loMenuStructure.AddMenuitem(new Menuitem("197", "19", "产品分析", "/MyFramework/Default.aspx", "1"));
            //loMenuStructure.AddMenuitem(new Menuitem("1971", "197", "卡销售分析", "/MyFramework/Default.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("1972", "197", "保险销售分析", "/MyFramework/Default.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("1973", "197", "餐饮娱乐分析", "/MyFramework/Default.aspx", "2"));

            //loMenuStructure.AddMenuitem(new Menuitem("198", "19", "人力资源分析", "/MyFramework/Default.aspx", "1"));
            //loMenuStructure.AddMenuitem(new Menuitem("1981", "198", "员工分析", "/MyFramework/Default.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("1982", "198", "绩效分析", "/MyFramework/Default.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("1982", "198", "价值分析", "/MyFramework/Default.aspx", "2"));
            //if (!loUserInfo.Is_Admin)
            //{
            //    List<Int32> loUserFunctionIds = this.GetUserFunctionIds();
            //    loMenuStructure.EnavailableMenus(loUserFunctionIds);
            //}
            //this.moSession["MenuStructure"] = loMenuStructure;
            #endregion


        }


        private bool HasChildMenu(int ParenMenuId, DataTable toMenuDt)
        {
            DataRow[] loFindRows = toMenuDt.Select("PreMenuID=" + ParenMenuId);
            if (loFindRows.Length > 0)
                return true;
            else
                return false;
        }
    }
}
