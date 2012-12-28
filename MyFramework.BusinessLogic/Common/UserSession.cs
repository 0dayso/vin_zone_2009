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
    /// SessionInit ��ժҪ˵����
    /// </summary>
    public class UserSession
    {
        /// <summary>
        /// ������Session��ش���Ĳ���
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
                        lsMessage = "���û�����δ����״̬��";
                        return lsMessage;
                    }
                    //���벻��ȷ
                    if (loUserInfo.Password != CommonFunction.GetCode(tsPassword))
                    {
                        lsMessage = "��¼����������������룡";
                        return lsMessage;
                    }
                    if (!lsBool)
                    {
                        if (StrUtil.SafeCString(lodtEmployee.Rows[0]["Is_Out_Accesss"]) != "1")
                        {
                            lsMessage = "���û���Ȩ��¼��";
                            return lsMessage;
                        }
                    }
                    #region ��������û����¼
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
                    lsMessage = "��½ʧ�ܣ�\r\n" + ex.Message;
                    return lsMessage;
                }

            }
            else
            {
                lsMessage = "�û���" + tsUserAccount + "�������ڣ�";
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
                        lsMessage = "���û�����δ����״̬��";
                        return lsMessage;
                    }

                    //��һ�ε�¼
                    if (IntUtil.SafeCInt(lodtEmployee.Rows[0]["IS_FIRST_LOGIN"]) != 1)
                    {
                        if (string.IsNullOrEmpty(tsPassword))
                        {
                            lsMessage = "��һ�ε�¼�����������룡";
                            isShowPwd = true;
                            return lsMessage;
                        }
                        else
                        {
                            //���벻��ȷ
                            if (loUserInfo.Password != CommonFunction.GetCode(tsPassword))
                            {
                                lsMessage = "��¼����������������룡";
                                isShowPwd = true;
                                return lsMessage;
                            }
                        }
                    }

                    if (!lsBool)
                    {
                        if (StrUtil.SafeCString(lodtEmployee.Rows[0]["Is_Out_Accesss"]) != "1")
                        {
                            lsMessage = "���û���Ȩ��¼��";
                            return lsMessage;
                        }
                    }
                    #region ��������û����¼
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
                    lsMessage = "��½ʧ�ܣ�\r\n" + ex.Message;
                    return lsMessage;
                }

            }
            else
            {
                lsMessage = "�û���" + tsUserAccount + "�������ڣ�";
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

            //��ȡ���û�������Ȩ�޵Ĳ˵�
            DataTable lodtMenu = new DataTable();

            DAL.DBA.FillDataTable(lodtMenu, lsSql);

            //��ȡ�û���Ȩ�޵�
            List<Int32> loFunctions = new List<int>();
            foreach (DataRow drMenu in lodtMenu.Rows)
            {
                loFunctions.Add(Convert.ToInt32(drMenu["Menu_ID"].ToString()));    
            }
            return loFunctions;

        }


        /// <summary>
        /// ��ʼ���˵�����
        /// </summary>
        public void InitMenu()
        {
            #region "��ʼ���˵�"
            //��ʼ���˵�
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

            #region "�Ժ�Ҫȥ��"

            //MenuStructure loMenuStructure = new MenuStructure();
            //loMenuStructure.AddMenuitem(new Menuitem("1", null, "ϵͳ����", "/MyFramework/Default.aspx", "0"));
            //loMenuStructure.AddMenuitem(new Menuitem("11", "1", "Ա������", "/MyFramework/Default.aspx", "1"));
            //loMenuStructure.AddMenuitem(new Menuitem("111", "11", "���Ź���", "/MyFramework/Employee/DepartmentAdmin/DepartmentList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("112", "11", "Ա������", "/MyFramework/Employee/EmployeeAdmin/EmpoyeeList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("113", "11", "��ɫ����", "/MyFramework/Employee/RoleAdmin/RoleList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("12", "1", "ϵͳ�ֵ�", "/MyFramework/Default.aspx", "1"));
            //loMenuStructure.AddMenuitem(new Menuitem("121", "12", "�ر����", "/MyFramework/Admin/LandMark/LandMarkTypeList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("122", "12", "�ر�", "/MyFramework/Admin/MarkType/MarkTypeList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("123", "12", "�ؼ���", "/MyFramework/Admin/CountyMark/CountyList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("124", "12", "����", "/MyFramework/Admin/CityMark/CityList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("125", "12", "����", "/MyFramework/Admin/TownMark/TownList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("126", "12", "����", "/MyFramework/Admin/Currency/CurrencyList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("127", "12", "���ÿ�����", "/MyFramework/Admin/CreditCard/CareditCardList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("128", "12", "ͼƬ����", "/MyFramework/Admin/PixType/PixTypeList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("129", "12", "��������", "/MyFramework/Admin/InsuranceType/InsuranceList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("130", "12", "����ȼ�", "/MyFramework/Admin/LncomeLevelType/LncomeLevelTypeList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("131", "12", "��������", "/MyFramework/PublicCase/CaseType/CaseTypeList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("133", "12", "����С��", "/MyFramework/Admin/CaseSubType/CaseSubType.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("134", "12", "Ա��ְλ", "/MyFramework/Admin/EmplyeePosition/EmplyeePositionList.aspx", "2"));


            //loMenuStructure.AddMenuitem(new Menuitem("2", null, "�Ƶ�ҵ��", "/MyFramework/Default.aspx", "0"));
            //loMenuStructure.AddMenuitem(new Menuitem("21", "2", "�Ƶ�Ԥ��", "/MyFramework/Default.aspx", "1"));
            //loMenuStructure.AddMenuitem(new Menuitem("211", "21", "���ɶ���", "/MyFramework/Hotel/HotelOrder/HotelOrderQuery.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("212", "21", "Ԥ�����", "/MyFramework/Hotel/HotelOrder/HotelOrderQuery.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("213", "21", "��α����", "/MyFramework/Hotel/HotelOrder/HOVerifyList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("214", "21", "ȷ�ϴ���", "/MyFramework/Hotel/HotelOrder/HotelConfirmList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("215", "21", "��������", "/MyFramework/Hotel/HotelOrder/HotelAssureList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("216", "21", "����ȷ��", "/MyFramework/Hotel/HotelOrder/HOAssureConfirmList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("217", "21", "ʧ�ܴ���", "/MyFramework/Hotel/HotelOrder/HotelFailList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("218", "21", "���ⶩ��", "/MyFramework/Hotel/HotelOrder/HotelQuestionList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("219", "21", "���մ�������", "/MyFramework/Hotel/HotelOrder/HOPaymentList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("220", "21", "�ߵ�", "/MyFramework/Hotel/HotelOrder/HotelUrgeList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("221", "21", "����", "/MyFramework/Hotel/HotelOrder/HotelFollowList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("222", "21", "����ռ����", "/MyFramework/Hotel/HotelOrder/HOORderOccupt/HOORderOccuptQuery.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("223", "21", "Э�̴���", "/MyFramework/Hotel/HotelOrder/HOOrderNego/HOOrderNegoList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("224", "21", "�޷��ҷ�", "/MyFramework/Hotel/HotelOrder/HONoRommList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("228", "21", "֪ͨ����", "/MyFramework/Hotel/HotelOrder/HotelNotifyCustomList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("229", "21", "������ѯ", "/MyFramework/Hotel/HotelOrder/OrderyQuery.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("22", "2", "�Ƶ��Ʒ", "/Default.asp", "1"));
            //loMenuStructure.AddMenuitem(new Menuitem("225", "22", "�Ƶ����", "/MyFramework/Hotel/HotelAdmin/HotelQuery.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("226", "22", "��̬����", "/MyFramework/Hotel/HotelAdmin/HotelRoomStatusQuery.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("227", "22", "��ѷ�����", "/MyFramework/Hotel/HotelAdmin/HotelHouseRoom/HFreeRoomList.aspx", "2"));

            //loMenuStructure.AddMenuitem(new Menuitem("23", "2", "�Ƶ����", "/Default.asp", "1"));
            //loMenuStructure.AddMenuitem(new Menuitem("231", "23", "��˷���", "/MyFramework/Hotel/HotelOrder/HOAreaList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("232", "23", "�绰��˷���", "/MyFramework/Hotel/HotelOrder/HOChkRmDy.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("233", "23", "���ʹ���", "/MyFramework/Hotel/HotelAdmin/HotelQuery.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("235", "23", "�绰���", "/MyFramework/Hotel/HotelOrder/HAuditing/HPhoneAuditingList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("236", "23", "��˾���", "/MyFramework/Hotel/HotelOrder/HAuditing/HOtherAuditingList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("237", "23", "��˼��", "/MyFramework/Hotel/HotelAdmin/HotelQuery.aspx", "2"));

            //loMenuStructure.AddMenuitem(new Menuitem("24", "2", "�Ƶ����", "/Default.asp", "1"));
            //loMenuStructure.AddMenuitem(new Menuitem("241", "24", "�����˵�", "/MyFramework/Hotel/HotelSettlement/HotelSettlementQuery.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("242", "24", "����ȷ��", "/MyFramework/Hotel/HotelSettlement/HotelSettlementQuery.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("243", "24", "���˼�¼��ѯ", "/MyFramework/Hotel/HotelSettlement/HotelSettlementQuery.aspx", "2"));
            ////loMenuStructure.AddMenuitem(new Menuitem("244", "24", "���������ʲ�ѯ", "/MyFramework/Finance/FPPymtQuery.aspx", "3"));
            //loMenuStructure.AddMenuitem(new Menuitem("245", "24", "����", "/MyFramework/Hotel/HotelSettlement/HotelSettlementBillConfirm.aspx", "2"));
            ////  loMenuStructure.AddMenuitem(new Menuitem("246", "24", "Ӧ�տ��ѯ", "/MyFramework/Hotel/HotelSettlement/HotelSettleShouldPayQuery.aspx", "3"));

            //loMenuStructure.AddMenuitem(new Menuitem("25", "2", "�Ƶ����", "/Default.asp", "1"));
            //loMenuStructure.AddMenuitem(new Menuitem("251", "25", "�տ�Ǽ�", "/MyFramework/Finance/FPPymtQuery.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("252", "25", "�˿����", "/MyFramework/Hotel/HotelSettlement/HFDispose.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("253", "25", "Ӧ�տ��ѯ", "/MyFramework/Hotel/HotelSettlement/HotelSettleShouldPayQuery.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("254", "25", "������¼��ѯ", "/MyFramework/Hotel/HotelSettlement/HotelSettlementCheckRecord.aspx", "2"));


            //loMenuStructure.AddMenuitem(new Menuitem("26", "2", "�Ƶ��ֵ�", "/Default.asp", "1"));
            //loMenuStructure.AddMenuitem(new Menuitem("261", "26", "�Ƶ�����", "/MyFramework/Hotel/HotelDictionary/HotelProperty/HotelPropertyList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("262", "26", "�Ƶ����", "/MyFramework/Hotel/HotelDictionary/HotelType/HotelTypeList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("263", "26", "�Ƶ����ͷ���", "/MyFramework/Hotel/HotelDictionary/ServiceType/ServiceTypeList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("264", "26", "�Ƶ��Ǽ�", "/MyFramework/Hotel/HotelDictionary/HotelGradeType/HotelGradeTypeList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("265", "26", "�Ƶ�װ�޼���", "/MyFramework/Hotel/HotelDictionary/HotelDecType/HotelDecTypeList.aspx", "2"));


            //loMenuStructure.AddMenuitem(new Menuitem("3", null, "��Ʊҵ��", "/MyFramework/Ticket/Default.aspx", "0"));
            //loMenuStructure.AddMenuitem(new Menuitem("31", "3", "��Ʊҵ��", "-", "1"));
            //loMenuStructure.AddMenuitem(new Menuitem("311", "31", "���ڻ�Ʊ", "/MyFramework/Ticket/Ticket/TicketOrderChina_Search.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("302", "31", "��������", "/MyFramework/Ticket/Ticket/TicketOrderChina_Order.aspx?IsImport=1", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("301", "31", "��ƱԤ��", "/MyFramework/Ticket/Ticket/TicketOrderChina_Order.aspx?User=liyao&Argument=B|A|1530.00|90|-1|-1|0||&CommandName=50|80|NAY|CAN|FM2275|1650|1930|20071010|1700.00&SrcCity=BJ%b1%b1%be%a9&Grade=0", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("312", "31", "��������", "/MyFramework/Ticket/Ticket/OrderDetailList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("313", "31", "������", "/MyFramework/Ticket/Ticket/LeaderManagerList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("314", "31", "�˸�ǩ", "/MyFramework/Ticket/Ticket/TicketOrderChangeList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("315", "31", "���Ʊ", "/MyFramework/Ticket/Ticket/PrintOutOrder.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("316", "31", "�Գ�Ʊ", "/MyFramework/Ticket/Ticket/PrintInnerOrder.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("317", "31", "�����Ʊ", "/MyFramework/Ticket/Ticket/OrderDispense.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("318", "31", "����", "/MyFramework/Ticket/Ticket/BalanceOrder.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("319", "31", "�˸�ǩ���", "/MyFramework/Ticket/Ticket/TicketBackConfirmList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("320", "31", "����֧������", "/MyFramework/Ticket/TicketFinance/TicketPayTaskList.aspx", "2"));

            //loMenuStructure.AddMenuitem(new Menuitem("32", "3", "��Ʊ��Ʒ", "-", "1"));
            //loMenuStructure.AddMenuitem(new Menuitem("321", "32", "���չ�˾", "/MyFramework/Ticket/TicketProduct/AirlineManagePage.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("322", "32", "����", "/MyFramework/Ticket/TicketProduct/FlightManagePage.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("323", "32", "����", "/MyFramework/Ticket/TicketProduct/FlightTypeManagePage.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("324", "32", "����", "/MyFramework/Ticket/TicketProduct/AirPortManagePage.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("325", "32", "��λ", "/MyFramework/Ticket/TicketProduct/CabinManagePage.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("326", "32", "������", "/MyFramework/Ticket/PartnerManageForm.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("327", "32", "��ƱԱ", "/MyFramework/Ticket/EmployeeDeliverManageForm.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("328", "32", "��������", "/MyFramework/Ticket/TicketPolicy/IssueBill.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("329", "32", "�ؼ۽�������", "/MyFramework/Ticket/TicketPolicy/SpecialIssueBill.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("330", "32", "�������û���ϵ", "/MyFramework/Ticket/TicketProduct/PartnerEmployeeList.aspx", "2"));

            //loMenuStructure.AddMenuitem(new Menuitem("33", "3", "��Ʊ����", "-", "1"));
            //loMenuStructure.AddMenuitem(new Menuitem("331", "33", "���ɽ��㵥", "/MyFramework/Ticket/TicketFinance/TicketParnterBill.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("332", "33", "���㵥�б�", "/MyFramework/Ticket/TicketFinance/TicketBillList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("333", "33", "֧����¼¼��", "/MyFramework/Finance/FPPaymentRecordInput.aspx?CustomerID=1", "2"));

            //loMenuStructure.AddMenuitem(new Menuitem("34", "3", "��Ʊ����", "-", "1"));
            //loMenuStructure.AddMenuitem(new Menuitem("341", "34", "�տ�Ǽ�", "/MyFramework/Finance/FPPymtQuery.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("342", "34", "�˿����", "/MyFramework/Ticket/TicketFinance/TicketDispose.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("343", "34", "Ӧ�տ��ѯ", "/MyFramework/Ticket/TicketFinance/TicketShouldPayQuery.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("344", "34", "������¼��ѯ", "/MyFramework/Ticket/TicketFinance/TicketSettlementCheckRecord.aspx", "2"));


            //loMenuStructure.AddMenuitem(new Menuitem("5", null, "��Ա����", "/MyFramework/Default.aspx", "0"));
            //loMenuStructure.AddMenuitem(new Menuitem("51", "5", "�������", "/MyFramework/Default.aspx", "1"));
            //loMenuStructure.AddMenuitem(new Menuitem("511", "51", "�����������", "/MyFramework/MemberService/MemberVote/MemberVoteTaskList.aspx", "2"));
            ////loMenuStructure.AddMenuitem(new Menuitem("512", "51", "����������Ŀ", "/MyFramework/MemberService/MemberItem/MemberVoteItemDef.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("52", "5", "��Ա�ֵ�", "/MyFramework/Default.aspx", "1"));
            //loMenuStructure.AddMenuitem(new Menuitem("527", "52", "��Ա����", "/MyFramework/Admin/CustomerClass/CustomerClassList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("528", "52", "�����������", "/MyFramework/Admin/Bank/BankTypeList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("521", "52", "�ط���������", "/MyFramework/MemberService/MemberDictionary/CBKTaskType/CBKTaskTypeList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("522", "52", "Ͷ��ԭ��", "/MyFramework/MemberService/Complaint/ComplaintReasonList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("523", "52", "Ͷ�߷�ʽ", "/MyFramework/MemberService/Complaint/ComplaintMeansList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("524", "52", "Ͷ�߽��", "/MyFramework/MemberService/Complaint/ComplaintReaultList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("525", "52", "�⳥����", "/MyFramework/MemberService/Complaint/ReimbTypeList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("526", "52", "֧���ʺ����� ", "/MyFramework/MemberService/Complaint/PymtAccountTypeList.aspx", "2"));

            //loMenuStructure.AddMenuitem(new Menuitem("54", "5", "�طù���", "/MyFramework/Default.aspx", "1"));
            //loMenuStructure.AddMenuitem(new Menuitem("541", "54", "�ط�ģ�����", "/MyFramework/MemberService/MemberTemplate/CBKTemplateList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("542", "54", "�½��ط�����", "/MyFramework/MemberService/CBKManagement/CBKManagementNewTask.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("544", "54", "�ط��������", "/MyFramework/MemberService/CBKManagement/CBKManagementTaskManage.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("545", "54", "��չ�طù���", "/MyFramework/MemberService/CBKManagement/ImplementCBKTask.aspx", "2"));

            //loMenuStructure.AddMenuitem(new Menuitem("55", "5", "��Ա����", "/MyFramework/Default.aspx", "1"));
            //loMenuStructure.AddMenuitem(new Menuitem("551", "55", "��Ա��ѯ", "/MyFramework/Member/MemberQuery.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("552", "55", "���չ���", "/MyFramework/AutomobileInsurance/CustomerQuery.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("553", "55", "��Ա�ֺ��¼", "/MyFramework/Member/DividendRecord.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("554", "55", "���չ���", "/MyFramework/Member/InsuranceRecord.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("555", "55", "��Ա¼��", "/MyFramework/Member/MCCard_Customer.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("556", "55", "��������", "/MyFramework/Member/MemberGuaranteeQuery.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("557", "5 5", "�����ʺ�", "/MyFramework/Member/ReturnAccountList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("558", "55", "���տͻ�¼��", "/MyFramework/AutomobileInsurance/CVICustomerInfo.aspx", "2"));

            //loMenuStructure.AddMenuitem(new Menuitem("56", "5", "��������", "/MyFramework/Default.aspx", "1"));
            //loMenuStructure.AddMenuitem(new Menuitem("561", "56", "��Ʒ��Ϣ����", "/MyFramework/MemberService/MemberPoint/ProductsManage.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("562", "56", "��Ա��������", "/MyFramework/MemberService/MemberEncourage/MemberApplyEncourage.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("563", "56", "�����������", "/MyFramework/MemberService/MemberEncourage/MemberApplyManage.aspx", "2"));

            //loMenuStructure.AddMenuitem(new Menuitem("564", "56", "�ֽ�һ�����", "/MyFramework/MemberService/MemberCashExchange/MemberApplyCashExchange.aspx", "2"));

            //loMenuStructure.AddMenuitem(new Menuitem("565", "56", "���ɷֺ챨��", "/MyFramework/MemberService/MemberCashExchange/ExportDvdList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("566", "56", "�ѵ����ֺ��¼", "/MyFramework/MemberService/MemberCashExchange/ExportedRecordsList.aspx", "2"));

            //loMenuStructure.AddMenuitem(new Menuitem("6", null, "��������", "/MyFramework/PublicCase/Default.aspx", "0"));
            //loMenuStructure.AddMenuitem(new Menuitem("61", "6", "ϵͳ����", "/MyFramework/PublicCase/Default.aspx", "1"));
            //loMenuStructure.AddMenuitem(new Menuitem("611", "61", "�½���������", "/MyFramework/PublicCase/AddNewCase.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("613", "61", "�ҵ�����", "/MyFramework/PublicCase/PublicCaseMyTask.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("614", "61", "��������", "/MyFramework/PublicCase/PublicCaseSystemTask.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("615", "61", "�����Ͷ���", "/MyFramework/SMS/AddNewSmsPage.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("616", "61", "��������Ϣ", "/MyFramework/Message/AddNewMessagePage.aspx", "2"));

            //loMenuStructure.AddMenuitem(new Menuitem("7", null, "�г�Ӫ��", "/MyFramework/Default.aspx", "0"));
            //loMenuStructure.AddMenuitem(new Menuitem("71", "7", "�����̹���", "/MyFramework/Default.aspx", "1"));
            //loMenuStructure.AddMenuitem(new Menuitem("711", "71", "������ά��", "/MyFramework/Agent/AgentList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("712", "71", "������Ԥ������", "/MyFramework/Agent/AgentManager/ReservRuleList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("713", "71", "�������ۿ�ҵ�����", "/MyFramework/Agent/AgentManager/CardSaleRuleList.aspx", "2"));

            //loMenuStructure.AddMenuitem(new Menuitem("72", "7", "�����̽���", "/MyFramework/Default.aspx", "1"));
            //loMenuStructure.AddMenuitem(new Menuitem("721", "72", "����������ά��", "/MyFramework/Agent/AgentManager/AgentReservCommRuleList.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("722", "72", "�����̽���", "/MyFramework/Agent/AgentBill/AgentBill.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("723", "72", "�����б�", "/MyFramework/Agent/AgentBill/AgentBillList.aspx", "2"));


            //loMenuStructure.AddMenuitem(new Menuitem("8", null, "�����", "/MyFramework/Default.aspx", "0"));
            //loMenuStructure.AddMenuitem(new Menuitem("81", "8", "��Ա�Ӵ�", "/MyFramework/Default.aspx", "1"));
            //loMenuStructure.AddMenuitem(new Menuitem("811", "81", "������ʾ", "/MyFramework/VIPLounge/VFlightRemind.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("812", "81", "�ۿ�����", "/MyFramework/VIPLounge/VCardSale.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("813", "81", "�˿�����", "/MyFramework/VIPLounge/VCardSaleRecordQuery.aspx", "2"));

            //loMenuStructure.AddMenuitem(new Menuitem("82", "8", "���ù���", "/MyFramework/Default.aspx", "1"));
            //loMenuStructure.AddMenuitem(new Menuitem("821", "82", "������Ϣ", "/MyFramework/VIPLounge/FeeInfoView.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("822", "82", "��ͬ��Ϣ", "/MyFramework/VIPLounge/ContactInfoView.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("823", "82", "Ա��������Ϣ", "/MyFramework/VIPLounge/EmployeeRentInfoView.aspx", "2"));


            //loMenuStructure.AddMenuitem(new Menuitem("83", "8", "������ֵ�", "/MyFramework/Default.aspx", "1"));
            //loMenuStructure.AddMenuitem(new Menuitem("831", "83", "��Ʒ���", "/MyFramework/VIPLounge/ProductTypeView.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("832", "83", "��Ʒ��Ϣ", "/MyFramework/VIPLounge/ProductInfoView.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("833", "83", "��Ӧ�̹���", "/MyFramework/VIPLounge/VendorView.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("834", "83", "֧����Ŀ����", "/MyFramework/VIPLounge/PaymentItemTypeView.aspx", "2"));

            //loMenuStructure.AddMenuitem(new Menuitem("9", null, "���̹���", "/MyFramework/Default.aspx", "0"));
            //loMenuStructure.AddMenuitem(new Menuitem("91", "9", "����Ԥ��", "/MyFramework/Default.aspx", "1"));
            //loMenuStructure.AddMenuitem(new Menuitem("911", "91", "����Ԥ��", "/MyFramework/ContractVendor/CVOOrderVendorQuery.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("912", "91", "����������ѯ", "/MyFramework/ContractVendor/CVOOrderQuery.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("92", "9", "�߶���Ԥ��", "/MyFramework/Default.aspx", "1"));
            //loMenuStructure.AddMenuitem(new Menuitem("921", "92", "�߶���Ԥ��", "/MyFramework/Default.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("922", "92", "�߶��򶩵���ѯ", "/MyFramework/Default.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("93", "9", "�̻�����", "/MyFramework/Default.aspx", "1"));
            //loMenuStructure.AddMenuitem(new Menuitem("931", "93", "�̻�ά��", "/MyFramework/ContractVendor/CVVendorInfoQuery.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("932", "93", "�̻���ѯ", "/MyFramework/Default.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("94", "9", "�����ֵ�", "/MyFramework/Default.aspx", "1"));
            //loMenuStructure.AddMenuitem(new Menuitem("941", "94", "�̻����", "/MyFramework/ContractVendor/CVTypeView.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("942", "94", "�̻������ֵ�", "/MyFramework/ContractVendor/AttributeDctionaryView.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("943", "94", "�̻���ɫ", "/MyFramework/ContractVendor/VendorSpecialView.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("944", "94", "�ͻ�����", "/MyFramework/ContractVendor/CustomerVolumeView.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("945", "94", "�˾�����", "/MyFramework/ContractVendor/AvgConsumptionView.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("946", "94", "��Ӫ��ʽ", "/MyFramework/ContractVendor/OperationModeView.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("947", "94", "�̻�ͼƬ����", "/MyFramework/ContractVendor/ImageStyleView.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("948", "94", "��ζ", "/MyFramework/ContractVendor/TasteTypeView.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("949", "94", "��������", "/MyFramework/ContractVendor/EnvironmentView.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("950", "94", "�͹���ɫ", "/MyFramework/ContractVendor/SpecialServiceView.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("951", "94", "������Ŀ", "/MyFramework/ContractVendor/CVDServiceView.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("952", "94", "�˵����", "/MyFramework/ContractVendor/MenuTypeView.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("953", "94", "�ܱ߻���", "/MyFramework/ContractVendor/SDEnvironmentView.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("95", "9", "���̻����", "/MyFramework/Default.aspx", "1"));
            //loMenuStructure.AddMenuitem(new Menuitem("954", "95", "�����", "/MyFramework/ContractVendor/CVPromoteSaleView.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("955", "95", "���¶�̬", "/MyFramework/ContractVendor/TopActiveView.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("956", "95", "�����Ƽ�", "/MyFramework/ContractVendor/PopRecommendView.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("957", "95", "��Ʒ�Ƽ�", "/MyFramework/ContractVendor/ElaborateRecommendView.aspx", "2"));
            ////loMenuStructure.AddMenuitem(new Menuitem("821", "82", "�߶���", "/MyFramework/Default.aspx", "2"));


            //loMenuStructure.AddMenuitem(new Menuitem("10", null, "������", "/MyFramework/Default.aspx", "0"));
            //loMenuStructure.AddMenuitem(new Menuitem("101", "10", "������", "/MyFramework/Card/MCCardNoSessionQuery.aspx", "1"));
            //loMenuStructure.AddMenuitem(new Menuitem("1011", "101", "���Ŷι���", "/MyFramework/Card/MCCardNoSessionQuery.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("1012", "101", "������", "/MyFramework/Card/CardTypeDef.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("1013", "101", "���ɿ���", "/MyFramework/Card/MCCardNoMark.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("1014", "101", "�ƿ�", "/MyFramework/Card/MCCardMake.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("1015", "101", "�쿨", "/MyFramework/Card/MCCardOut.aspx", "2"));


            //loMenuStructure.AddMenuitem(new Menuitem("18", null, "�������", "/MyFramework/Default.aspx", "0"));
            //loMenuStructure.AddMenuitem(new Menuitem("181", "18", "�Ƶ����", "/Default.asp", "1"));
            //loMenuStructure.AddMenuitem(new Menuitem("1811", "181", "�տ�Ǽ�", "/MyFramework/Finance/FPPymtQuery.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("1812", "181", "�˿����", "/MyFramework/Hotel/HotelSettlement/HFDispose.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("1813", "181", "Ӧ�տ��ѯ", "/MyFramework/Hotel/HotelSettlement/HotelSettleShouldPayQuery.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("1814", "181", "������¼��ѯ", "/MyFramework/Hotel/HotelSettlement/HotelSettlementCheckRecord.aspx", "2"));

            //loMenuStructure.AddMenuitem(new Menuitem("19", null, "����֧��", "/MyFramework/Default.aspx", "0"));
            //loMenuStructure.AddMenuitem(new Menuitem("191", "19", "Ԥ������", "/MyFramework/Default.aspx", "1"));
            //loMenuStructure.AddMenuitem(new Menuitem("1911", "191", "�Ƶ궩��", "/MyFramework/Default.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("1912", "191", "��Ʊ����", "/MyFramework/Default.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("1913", "191", "��������", "/MyFramework/Default.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("1914", "191", "�����ۺϷ���", "/MyFramework/Default.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("192", "19", "�������", "/MyFramework/Default.aspx", "1"));
            //loMenuStructure.AddMenuitem(new Menuitem("1921", "192", "��Ա����", "/MyFramework/Default.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("1922", "192", "Ͷ�߷���", "/MyFramework/Default.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("1923", "192", "����ģʽ����", "/MyFramework/Default.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("193", "19", "�г�����", "/MyFramework/Default.aspx", "1"));
            //loMenuStructure.AddMenuitem(new Menuitem("1931", "193", "�����̷���", "/MyFramework/Default.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("1932", "193", "Ӫ�����Է���", "/MyFramework/Default.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("1933", "193", "Ͷ�ʻر�����", "/MyFramework/Default.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("194", "19", "�����̷���", "/MyFramework/Default.aspx", "1"));
            //loMenuStructure.AddMenuitem(new Menuitem("1941", "194", "�Ƶ��Ʒ����", "/MyFramework/Default.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("1942", "194", "��Ʊ��Ʒ����", "/MyFramework/Default.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("1943", "194", "����Ч�����", "/MyFramework/Default.aspx", "2"));

            //loMenuStructure.AddMenuitem(new Menuitem("195", "19", "���۷���", "/MyFramework/Default.aspx", "1"));
            //loMenuStructure.AddMenuitem(new Menuitem("1951", "195", "�Ƶ��Ʒ����", "/MyFramework/Default.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("1952", "195", "��Ʊ��Ʒ����", "/MyFramework/Default.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("1953", "195", "����Ч�����", "/MyFramework/Default.aspx", "2"));

            //loMenuStructure.AddMenuitem(new Menuitem("196", "19", "���������", "/MyFramework/Default.aspx", "1"));
            //loMenuStructure.AddMenuitem(new Menuitem("1961", "196", "��Ʒ���۷���", "/MyFramework/Default.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("1962", "196", "��Ա�Ӵ�����", "/MyFramework/Default.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("1963", "196", "�ͻ�����", "/MyFramework/Default.aspx", "2"));

            //loMenuStructure.AddMenuitem(new Menuitem("197", "19", "��Ʒ����", "/MyFramework/Default.aspx", "1"));
            //loMenuStructure.AddMenuitem(new Menuitem("1971", "197", "�����۷���", "/MyFramework/Default.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("1972", "197", "�������۷���", "/MyFramework/Default.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("1973", "197", "�������ַ���", "/MyFramework/Default.aspx", "2"));

            //loMenuStructure.AddMenuitem(new Menuitem("198", "19", "������Դ����", "/MyFramework/Default.aspx", "1"));
            //loMenuStructure.AddMenuitem(new Menuitem("1981", "198", "Ա������", "/MyFramework/Default.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("1982", "198", "��Ч����", "/MyFramework/Default.aspx", "2"));
            //loMenuStructure.AddMenuitem(new Menuitem("1982", "198", "��ֵ����", "/MyFramework/Default.aspx", "2"));
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
