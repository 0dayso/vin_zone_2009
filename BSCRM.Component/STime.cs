using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

namespace MyFramework.Component
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:STime runat=server></{0}:STime>")]
    public class STime : WebControl
    {
       

        protected override void CreateChildControls()
        {
                base.CreateChildControls();
                DropDownList LstHour = new DropDownList();
                //LstHour.AutoPostBack = true;
                for (int i = 0; i < 24; i++)
                {
                    LstHour.Items.Add(i.ToString().PadLeft(2, '0'));
                }
                LstHour.SelectedIndexChanged += new EventHandler(LstHour_SelectedIndexChanged);
                LstHour.AutoPostBack = true;
                LstHour.Attributes.Add("onclick", "ListChange()");
                Controls.Add(LstHour);
                

                DropDownList LstMin = new DropDownList();
                //LstMin.AutoPostBack = true;
                for (int i = 0; i < 60; i++)
                {
                    LstMin.Items.Add(i.ToString().PadLeft(2, '0'));
                }
                Controls.Add(LstMin);
            
        }

        protected  void LstHour_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
        
       
        protected override void RenderContents(HtmlTextWriter output)
        {
            Controls[0].RenderControl(output);
            output.Write("时");
            Controls[1].RenderControl(output);
            output.Write("分");
            output.Write(GenerateScript());
        }
        
        public String Time
        {
            get 
            {
                return (((DropDownList)this.Controls[0]).SelectedValue + ":" + ((DropDownList)this.Controls[1]).SelectedValue);
            }
            set 
            {
                String lsTime = value;
                Regex regex = new Regex("^(([01]\\d)|(2[0-3])):[0-5]\\d$");
                if (regex.IsMatch(lsTime)==true)
                {
                    String[] lsTimes = lsTime.Split(':');
                    if (lsTimes.Length == 2)
                    {
                        ((DropDownList)this.Controls[0]).SelectedIndex = -1;
                        ((DropDownList)this.Controls[0]).SelectedValue = lsTimes[0];
                        ((DropDownList)this.Controls[1]).SelectedIndex = -1;
                        ((DropDownList)this.Controls[1]).SelectedValue = lsTimes[1];
                    }
                }
                else
                {
                    
                    //lblError.Text = "时间格式不正确！";
                    //lblError.ForeColor = System.Drawing.Color.Red;
                }
                
            }
        }

        public String GenerateScript()
        {
            String lsScript = "";
            lsScript += "<script language=\"JavaScript\" runat=\"server\" >\n";
            lsScript += @"function ListChange(control)
                         {
                            alert(control.value);
                         }";
            lsScript += "</script>";
            return lsScript;
        }
    }
}
