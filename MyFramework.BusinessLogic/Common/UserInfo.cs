using System;
using System.Collections.Generic;
using System.Text;

namespace MyFramework.BusinessLogic.Common
{
    public class UserInfo
    {
       // private List<String> moRights;
        private int mnUserId =1;
        private String msUserName = "p1";
        private int mnDeptId=1001;
        private Boolean msIs_Employee_Pwd_Reset;
        private EnumDef.EStatus meEmployee_Status;
        private string msEmployee_Login_ID;
        private Boolean msIs_Admin;
        private string msPassword;
        private int? msPositionID;
       /* public List<String> Rights
        {
            get { return moRights; }
            set { moRights = value; }
        }*/
        public int UserId
        {
            get { return mnUserId; }
            set { mnUserId = value; }
        }
        public String UserName
        {
            get { return msUserName; }
            set { msUserName = value; }
        }
        public int DeptId
        {
            get { return mnDeptId; }
            set { mnDeptId = value; }
        }
        public int? PositionID
        {
            get { return msPositionID; }
            set { msPositionID = value; }
        }
        public Boolean Is_Employee_Pwd_Reset
        {
            get { return msIs_Employee_Pwd_Reset; }
            set { msIs_Employee_Pwd_Reset = value; }
        }

        public EnumDef.EStatus Employee_Status
        {
            get { return meEmployee_Status; }
            set { meEmployee_Status = value; }
        }

        public string Login_ID
        {
            get { return msEmployee_Login_ID; }
            set { msEmployee_Login_ID = value; }
        }
        public Boolean Is_Admin
        {
            get { return msIs_Admin;  }
            set { msIs_Admin = value; }
        }
        public string Password
        {
            get { return msPassword; }
            set { msPassword = value; }
        }
    }
}
