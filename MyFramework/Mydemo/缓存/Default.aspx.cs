using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyFramework.BusinessLogic.Common;

public partial class Mydemo_缓存_Default : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.PageCaption = "this is page caption";
    }
    protected void Button1_Click(object sender, EventArgs e)
    {         
        string cacheKey = "test";
        object objCache = CacheHelper.GetCache(cacheKey);
        if (objCache == null)
        {
            this.LitMessage.Text = "<span style='color:Red'>" + " is : " + this.TextBox1.Text + "---无缓存，显示输入内容" + "</span>";
            CacheHelper.SetCache(cacheKey, this.TextBox1.Text);
        }
        else
        {
            this.LitMessage.Text = "<span style='color:Red'>" + "is : " + Convert.ToString(objCache) + "---有缓存，显示缓存内容" + "</span>";
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        CacheHelper.RemoveAllCache();
    }
}