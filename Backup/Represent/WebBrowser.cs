using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using mshtml;
using BLL;
using CRM.Buzlogic.Ticket.TicketOrder;
using System.Xml;
using CRM.Buzlogic.Ticket.TicketSearch;
using CRM.Buzlogic.Common;
using CRM.Buzlogic.Member;
using CRM.Buzlogic.Card;
using CRM.Buzlogic.Finance.UnionPay;
using CRM.Buzlogic.Finance;
using System.Collections;
using CRM.Buzlogic.Ticket;

namespace Represent
{
    public partial class WebBrowwer : Form
    {
        public WebBrowwer()
        {
            InitializeComponent();

            action.Response += new Represent.Action.ResponseEventHandler(Action_Response);
        }
        private void WebBrowwer_Load(object sender, EventArgs e)
        {
            webBrowser1.ScriptErrorsSuppressed = false;
            webBrowser1.Navigated += new WebBrowserNavigatedEventHandler(webBrowser1_Navigated); //����confirm,alert 
            webBrowser1.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(webBrowser1_DocumentCompleted);

            this.txtLoginId.Text = "lt";
            this.txtPwd.Text = "lt";
            txtPrefixe.Text = "http://127.0.0.1:8000/";
            txtOrderId.Text = "2981890";
        }

        private ProcessControl process = BLL.ProcessControl.��½;
        private FillData fillData = BLL.FillData.��д��Ա����;
        static bool isTrue = false;

        #region webBrowser1

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (webBrowser1.ReadyState == WebBrowserReadyState.Complete)
            {
                WebBrowser webB = sender as WebBrowser;
                webB.Document.Window.Error += new HtmlElementErrorEventHandler(webBrowser1_Window_Error);
                switch (process)
                {
                    case BLL.ProcessControl.��½:
                        Login(webB);
                        break;
                    case BLL.ProcessControl.���˵�:
                        ParentMenu(webB);
                        break;
                    case BLL.ProcessControl.���Ӳ˵�:
                        Navigate(webB);
                        break;
                    case BLL.ProcessControl.�������:
                        {
                            switch (fillData)
                            {
                                case BLL.FillData.��д��Ա����:
                                    LoadCustomer(webB);
                                    break;
                                case BLL.FillData.��дPNR:
                                    LoadPNR(webB);
                                    break;
                            }
                        }
                        break;
                    case BLL.ProcessControl.�ύ:
                        FillData(webB);
                        SubmitOrder(webB);
                        break;
                }
            }
        }

        private void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            HtmlWindowCollection coll = (sender as WebBrowser).Document.Window.Frames;
            string s = "function alert(){}; function confirm(){ return true;};";//function open(){};function window.showModalDialog(){};";
            IHTMLWindow2 win1 = null;
            foreach (HtmlWindow win in coll)
            {
                win1 = (IHTMLWindow2)win.DomWindow;
                win1.execScript(s, "javascript");
            }
        }

        private void webBrowser1_Window_Error(object sender, HtmlElementErrorEventArgs e)
        {
            e.Handled = true;
        }

        #endregion

        #region ��ȡ����

        private void FillData(WebBrowser browser)
        {
            try
            {
                HtmlWindowCollection coll = browser.Document.Window.Frames;
                foreach (HtmlWindow win in coll)
                {
                    HtmlDocument doc = win.Document;
                    HtmlElement element = doc.GetElementById("tSendContactName");
                    if (element != null)
                    {
                        System.Web.Caching.Cache cache = System.Web.HttpRuntime.Cache;
                        object obj = cache.Get("TicketOrderInfo");
                        TicketOrder ticketOrder = null;
                        if (obj == null)
                        {
                            ticketOrder = new TicketOrder();
                            ticketOrder.LoadTicketOrder(txtOrderId.Text.Trim());
                        }
                        else
                        {
                            ticketOrder = obj as TicketOrder;
                        }

                        ChangeData(doc, ticketOrder);
                        ChangeTicket(doc, ticketOrder);
                    }
                }
            }
            catch { }
        }

        private void SubmitOrder(WebBrowser browser)
        {
            try
            {
                HtmlWindowCollection coll = browser.Document.Window.Frames;
                foreach (HtmlWindow win in coll)
                {
                    HtmlElement element = win.Document.GetElementById("bOrderSave");
                    if (element != null)
                    {
                        element.InvokeMember("Click");
                        isTrue = true;
                        break;
                    }
                }
            }
            catch { }

        }

        #region ����PNR  ��  ��Ա��Ϣ

        private void LoadPNR(WebBrowser webBrowser1)
        {
            try
            {
                HtmlWindowCollection coll = webBrowser1.Document.Window.Frames;
                foreach (HtmlWindow win in coll)
                {
                    HtmlElement ele = win.Document.GetElementById("tPNR");
                    if (ele != null)
                    {
                        win.Document.GetElementById("bA").InvokeMember("Click");

                        process = BLL.ProcessControl.�ύ;
                        fillData = BLL.FillData.��ʼ��;

                        break;
                    }

                }
            }
            catch { }
        }

        private void LoadCustomer(WebBrowser webBrowser1)
        {
            try
            {
                HtmlWindowCollection coll = webBrowser1.Document.Window.Frames;
                foreach (HtmlWindow win in coll)
                {
                    HtmlElement ele = win.Document.GetElementById("tMemCardID");
                    if (ele != null)
                    {
                        TicketOrder ticketOrder = new TicketOrder();
                        if (ticketOrder.LoadTicketOrder(txtOrderId.Text.Trim()))
                        {
                            win.Document.GetElementById("tPNR").SetAttribute("value", ticketOrder.PNR);

                            System.Web.Caching.Cache cache = System.Web.HttpRuntime.Cache;
                            cache.Add("TicketOrderInfo", ticketOrder, null, DateTime.MaxValue, new TimeSpan(0, 5, 0), System.Web.Caching.CacheItemPriority.Normal, null);
                        }

                        ele.SetAttribute("value", ticketOrder.CardNum);
                        win.Document.GetElementById("tMemName").SetAttribute("value", ticketOrder.MemberName);

                        ele = win.Document.GetElementById("bMemInfo");
                        ele.InvokeMember("Click");
                        fillData = BLL.FillData.��дPNR;
                        break;
                    }
                }
            }
            catch { }
        }

        #endregion

        #region  menu
        private void Login(WebBrowser browser)
        {
            try
            {
                browser.Document.GetElementById("txtUserName").InnerText = txtLoginId.Text.Trim();
                browser.Document.GetElementById("txtPassword").InnerText = txtPwd.Text.Trim();
                browser.Document.GetElementById("btnLogIn").InvokeMember("Click");
                process = BLL.ProcessControl.���˵�;
            }
            catch { }
        }

        private void ParentMenu(WebBrowser browser)
        {
            try
            {
                HtmlElementCollection eleCollection = browser.Document.GetElementsByTagName("a");
                eleCollection[2].InvokeMember("Click");
                process = BLL.ProcessControl.���Ӳ˵�;
            }
            catch { }
        }

        private void ChildMenu(WebBrowser browser)
        {
            try
            {
                HtmlWindowCollection coll = browser.Document.Window.Frames;
                foreach (HtmlWindow win in coll)
                {
                    HtmlElement ele = win.Document.GetElementById("LeftMenuTable");
                    if (ele != null)
                    {
                        HtmlElementCollection eleCollection = ele.All;
                        foreach (HtmlElement el in eleCollection)
                        {
                            if (el.InnerText == "��ƱԤ��")
                            {
                                el.Parent.Parent.InvokeMember("Click");
                                process = BLL.ProcessControl.�򿪲˵�;
                                break;
                            }

                        }
                    }
                }
            }
            catch { }
        }

        private void Navigate(WebBrowser browser)
        {
            try
            {
                HtmlWindowCollection coll = browser.Document.Window.Frames;
                foreach (HtmlWindow win in coll)
                {
                    HtmlElement ele = win.Document.GetElementById("LeftMenuTable");
                    if (ele != null)
                    {
                        HtmlElementCollection eleCollection = ele.All;
                        foreach (HtmlElement el in eleCollection)
                        {
                            if (el.InnerText == "��������")
                            {
                                el.GetElementsByTagName("a")[0].InvokeMember("Click");
                                process = BLL.ProcessControl.�������;
                                fillData = BLL.FillData.��д��Ա����;
                                break;
                            }
                        }
                    }
                }

            }
            catch { }
        }

        #endregion

        #region ��½

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtLoginId.Text.Trim() == "" || txtPwd.Text.Trim() == "" || txtOrderId.Text.Trim() == "")
            {
                MessageBox.Show("�����붩���ţ��û��������룡");
            }
            else
            {
                this.webBrowser1.Url = new Uri(System.Configuration.ConfigurationManager.AppSettings["BSCRMUrl"]);
                process = BLL.ProcessControl.��½;
                System.Web.Caching.Cache cache = System.Web.HttpRuntime.Cache;
                cache.Remove("TicketOrderInfo");
            }
        }

        #endregion

        #region ����Ʊ�� ���ۿ�

        private double RoundFloat1(double tnValue, double tnPrecise)
        {
            if (tnPrecise == 0)
                tnPrecise = 6;
            double lnNumber = Math.Pow(10, tnPrecise);
            return Math.Round(tnValue * lnNumber) / lnNumber;
        }

        private double GetInfFlueTax(double FueFee)
        {
            double iReturn = FueFee / 2;
            if (FueFee != 0)
            {
                if ((iReturn % 10) >= 5)
                {
                    iReturn = (iReturn - (iReturn % 10)) + 10;
                }

            }
            return iReturn;
        }

        private void ChangeTicket(HtmlDocument doc, TicketOrder ticketOrder)
        {
            HtmlElement element = doc.GetElementById("tTicketPrice");
            if (element != null)
            {
                string sql = "select * from t_to_order_tickets where order_id=" + txtOrderId.Text + " and rownum<2";
                DataTable dt = DSCRM.DBA.GetDataTable(sql);

                double YPrice = Convert.ToInt32(dt.Rows[0]["FULL_PRICE"].ToString());
                doc.GetElementById("tTicketPrice100").SetAttribute("value", YPrice.ToString());//ȫ��

                //Ʊ��
                double enPrice = 10 * RoundFloat1(YPrice * 0.05, 0);
                double babyPrice = 10 * RoundFloat1(YPrice * 0.01, 0);
                element.SetAttribute("value", (ticketOrder.TicketPassengers[0].TicketPrice + "|" + enPrice + "|" + babyPrice));

                //�ۿ� 
                doc.GetElementById("tRate").SetAttribute("value", ticketOrder.TicketPassengers[0].Rate.ToString() + "|50|10");
                //���� 
                doc.GetElementById("tAirportTax").SetAttribute("value", ticketOrder.TicketPassengers[0].AirPortTax.ToString() + "|0|0");
                //ȼ�� 
                double enFuelTax = GetInfFlueTax(ticketOrder.TicketPassengers[0].FuelTax);
                doc.GetElementById("tFuelTax").SetAttribute("value", ticketOrder.TicketPassengers[0].FuelTax.ToString() + "|" + enFuelTax + "|0");

                //Ԥ������
                element = doc.GetElementById("hidBookingParam");

                string hidStr = "<xml><Arguments><CommDefID>-1</CommDefID><CommisionRate>-1</CommisionRate><Discount>" + dt.Rows[0]["DISCOUNT"]
                     + "</Discount><TicketPrice>" + YPrice + "</TicketPrice><AirPortTax>"
                     + ticketOrder.TicketPassengers[0].AirPortTax + "</AirPortTax><FuelTax>"
                     + ticketOrder.TicketPassengers[0].FuelTax + "</FuelTax><IsSpecial>0</IsSpecial></Arguments></xml>";

                //����˿�
                string[] Passengers = doc.GetElementById("t_PassengerList").GetAttribute("value").Split(',');

                string NewPassengerList = "";

                for (int i = 0; i < Passengers.Length - 1; i++)
                {
                    string[] subGuest = Passengers[i].Split('|');
                    NewPassengerList += subGuest[0] + "|";
                    NewPassengerList += subGuest[1] + "|";
                    NewPassengerList += subGuest[2] + "|";
                    NewPassengerList += subGuest[3] + "|";
                    if (subGuest[2] == " ���� ")
                    {
                        NewPassengerList += ticketOrder.TicketPassengers[0].TicketPrice + "|";
                        NewPassengerList += ticketOrder.TicketPassengers[0].AirPortTax + "|";
                        NewPassengerList += ticketOrder.TicketPassengers[0].FuelTax + ",";
                    }
                    else if (subGuest[2] == " ��ͯ ")
                    {
                        NewPassengerList += enPrice + "|";
                        NewPassengerList += "0|";
                        NewPassengerList += enFuelTax + ",";
                    }
                    else if (subGuest[2] == " Ӥ�� ")
                    {
                        NewPassengerList += babyPrice + "|";
                        NewPassengerList += "0|";
                        NewPassengerList += "0,";
                    }
                }

                doc.GetElementById("t_PassengerList").SetAttribute("value", NewPassengerList);

                (doc.DomDocument as IHTMLDocument2).parentWindow.execScript("AutoPrice();", "javascript");
            }
        }

        private void ChangeData(HtmlDocument doc, TicketOrder ticketOrder)
        {
            #region ��������Ϣ
            doc.GetElementById("tSendContactName").SetAttribute("value", ticketOrder.DeliverRecord.Contact_Name == null ? "" : ticketOrder.DeliverRecord.Contact_Name); //��ϵ��
            doc.GetElementById("tSendTel").SetAttribute("value", ticketOrder.DeliverRecord.Contact_Phone == null ? "" : ticketOrder.DeliverRecord.Contact_Phone); //��ϵ�˵绰
            doc.GetElementById("tSendMobile").SetAttribute("value", ticketOrder.DeliverRecord.Contact_Mobile == null ? "" : ticketOrder.DeliverRecord.Contact_Mobile); //��ϵ���ֻ�
            doc.GetElementById("tSendEmail").SetAttribute("value", ticketOrder.DeliverRecord.Contact_Email == null ? "" : ticketOrder.DeliverRecord.Contact_Email); //��ϵ���ʼ�
            doc.GetElementById("tSendAddress").SetAttribute("value", ticketOrder.DeliverRecord.Contact_Address == null ? "" : ticketOrder.DeliverRecord.Contact_Address); //��ϵ��ַ
            doc.GetElementById("tCustomerRemark").SetAttribute("value", ticketOrder.CustomerRemark == null ? "" : ticketOrder.CustomerRemark); //�ͻ�����Ҫ��
            doc.GetElementById("tCompanyRemark").SetAttribute("value", ticketOrder.CompanyRemark == null ? "" : ticketOrder.CompanyRemark); //��˾������ע 

            doc.GetElementById("tSentDate").SetAttribute("value", ticketOrder.GetTicketTime == null ? "" : ticketOrder.GetTicketTime); //��Ʊʱ��  
            doc.GetElementById("ddlOperDstSite").SetAttribute("value", ticketOrder.DstSite); //��Ʊ�� 
            string sIssueCityCode = TicketOrderAdmin.GetCityCodebyPartnerID(ticketOrder.DstSite);
            doc.GetElementById("ddlOperDstCity").SetAttribute("value", sIssueCityCode);  //������id
            //doc.GetElementById("ddlSendType").SetAttribute("value", ticketOrder.DeliverRecord.Deliver_Type_Id.ToString());//���ͷ�ʽ

            HtmlElementCollection elements = doc.GetElementById("ddlSendType").GetElementsByTagName("Option");
            foreach (HtmlElement el in elements)
            {
                if (el.GetAttribute("value") == ticketOrder.DeliverRecord.Deliver_Type_Id.ToString())
                {
                    el.SetAttribute("selected", "selected");
                }
            }


            if (ticketOrder.IsRemitSMS)
                doc.GetElementById("ckbRemitSMS").InvokeMember("click"); //�Ƿ������  
            if (ticketOrder.IsNotShouldIssue)
                doc.GetElementById("ckbNotIssue").InvokeMember("click"); //�Ƿ���Ҫ��Ʊ  
            if (ticketOrder.SendIndex == 0)
                doc.GetElementById("cbSendIndex").InvokeMember("click"); //�Ƿ���Ҫ�Ӽ� 

            doc.GetElementById("txtUserFixMoney").SetAttribute("value", ticketOrder.MemberBalance.ToString() == null ? "" : ticketOrder.MemberBalance.ToString()); //�������տ� 
            doc.GetElementById("txtArrearage").SetAttribute("value", ticketOrder.PayOweAmount.ToString() == null ? "" : ticketOrder.PayOweAmount.ToString()); //�������տ�  

            doc.GetElementById("txtPayAmount").SetAttribute("value", ticketOrder.PayAmount.ToString());//֧����� 
            doc.GetElementById("txtArrearage").SetAttribute("value", ticketOrder.PayOweAmount.ToString()); //֧��Ƿ��

            if (ticketOrder.Pay_Type == EnumDef.ETicketOrderPayType.��˾����֧��)
            {
                doc.GetElementById("rblPayType_0").InvokeMember("click");
                doc.GetElementById("ddlJSJPayType").SetAttribute("value", "4");
            }
            else if (ticketOrder.Pay_Type == EnumDef.ETicketOrderPayType.��˾�������п�)
            {
                doc.GetElementById("rblPayType_0").InvokeMember("click");
                doc.GetElementById("ddlJSJPayType").SetAttribute("value", "8");
                doc.GetElementById("ddlUnionPay").SetAttribute("value", (ticketOrder.IsUnionCard == EnumDef.ETicketOrderUnionPay.�� ? "1" : "2"));
            }
            else if (ticketOrder.Pay_Type == EnumDef.ETicketOrderPayType.��Ʊ�������ֽ�)
            {
                doc.GetElementById("rblPayType_1").InvokeMember("click");
                doc.GetElementById("ddlSelfPayType").SetAttribute("value", "1");
            }
            else if (ticketOrder.Pay_Type == EnumDef.ETicketOrderPayType.��Ʊ������ֽ�)
            {
                doc.GetElementById("rblPayType_2").InvokeMember("click");
                doc.GetElementById("ddlInsteadPayType").SetAttribute("value", "1");

                if (ticketOrder.TicketPayInfo != null)
                {
                    doc.GetElementById("ddlInsteadPartner").SetAttribute("value", ticketOrder.TicketPayInfo.GatherPartnerId.ToString());
                }

            }

            F_Debit_Payment_Record mobilePayRecord = new F_Debit_Payment_Record();
            if (mobilePayRecord.Load(txtOrderId.Text.Trim(), EnumDef.ESPOrderType.��Ʊ����))
            {
                doc.GetElementById("rblPayType_0").InvokeMember("click");
                if (mobilePayRecord.PaymentTypeId == EnumDef.ETMobliePay.��ǿ�֧��)
                {
                    doc.GetElementById("ddlJSJPayType").SetAttribute("value", "8");
                    doc.GetElementById("ddlUnionBank").SetAttribute("value", mobilePayRecord.BankcardTypeId.Value.ToString());
                    doc.GetElementById("txtUnionCardNO").SetAttribute("value", mobilePayRecord.CashcardNo.Trim());
                    doc.GetElementById("txtUnionPhone").SetAttribute("value", mobilePayRecord.CashcardMobi.Trim());
                }
                else if (mobilePayRecord.PaymentTypeId == EnumDef.ETMobliePay.���ÿ�֧��)
                {
                    doc.GetElementById("ddlJSJPayType").SetAttribute("value", "3");
                    doc.GetElementById("ddlCreditCardType").SetAttribute("value", mobilePayRecord.CreditcardType.Value.ToString());
                    doc.GetElementById("txtCreditCardNum").SetAttribute("value", mobilePayRecord.CreditcardNo.Trim());
                    doc.GetElementById("txtCreditCardDate").SetAttribute("value", mobilePayRecord.CreditcardAvail.Value.ToString("yyyy-MM-dd"));
                    doc.GetElementById("txtHandName").SetAttribute("value", mobilePayRecord.CreditcardOwner);
                    if (mobilePayRecord.CreditcardCvv2 != null && mobilePayRecord.CreditcardCvv2.Trim() != "")
                    {
                        HtmlElement element = doc.GetElementById("tCompanyRemark");
                        element.SetAttribute("value", element.GetAttribute("value") + "CVV" + mobilePayRecord.CreditcardCvv2);
                    }
                }
            }


            #endregion

        }

        #endregion

        #endregion

        Action.HttplistenerHandler action = new Represent.Action.HttplistenerHandler();

        System.Threading.Thread thread = null;
        private void btnStart_Click(object sender, EventArgs e)
        {
            //if (thread == null)
            //{
            //    thread = new System.Threading.Thread(Action.HttplistenerHandler.Start);
            //    thread.Start();
            //}
            action.Start();
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            //if (thread != null)
            //{
            //    thread.Abort();
            //    thread = null;
            //}
            action.Stop();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add(txtPrefixe.Text.Trim());
            action.prefixes.Add(txtPrefixe.Text.Trim());
            txtPrefixe.Text = null;
        }

        private string Action_Response(object sender, EventArgs e)
        {
            txtLoginId.Text = sender.ToString();
            this.webBrowser1.Url = new Uri(System.Configuration.ConfigurationManager.AppSettings["BSCRMUrl"]);
            process = BLL.ProcessControl.��½;
            System.Web.Caching.Cache cache = System.Web.HttpRuntime.Cache;
            cache.Remove("TicketOrderInfo");

            string str = "";
            if (isTrue)
            {

                str = webBrowser1.Document.Body.InnerHtml;
            }
            return str;
        }

    }


}