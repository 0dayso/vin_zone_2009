using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.ComponentModel;


namespace MyFramework.Component
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:BaseCheckBoxList runat=server></{0}:BaseCheckBoxList>")]
    public class BaseCheckBoxList : CheckBoxList  
    {
        protected override object SaveViewState()
        {

            object[] objs = new object[2];
            objs[0] = base.SaveViewState();
            System.Collections.ArrayList list = new ArrayList();
            objs[1] = list;
            foreach (ListItem item in this.Items)
            {
                System.Collections.Hashtable hash = new Hashtable();
                foreach (Object key in item.Attributes.Keys)
                {
                    hash.Add(key, item.Attributes[key.ToString()]);
                }
                list.Add(hash);
            }
            return objs;
        }
        protected override void LoadViewState(object savedState)
        {
            object[] objs = (Object[])savedState;
            base.LoadViewState(objs[0]);
            System.Collections.ArrayList list = (System.Collections.ArrayList)objs[1];
            for (int i = 0; i < list.Count; i++)
            {
                System.Collections.Hashtable hash = (System.Collections.Hashtable)list[i];
                foreach (object key in hash.Keys)
                {
                    Items[i].Attributes.Add(key.ToString(), hash[key].ToString());
                }
            }
        }   
    }
}
