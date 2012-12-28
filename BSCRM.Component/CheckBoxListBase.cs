using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

namespace MyFramework.Component
{

    [DefaultProperty("Text")]
    [ToolboxData("<{0}:CheckBoxListBase runat=server></{0}:CheckBoxListBase>")]
    public class CheckBoxListBase : CheckBoxList
    {
        private String[] msSelectValues;
        private string msSelectValuesStr;
        /// <summary>
        /// ��ȡ�������б�
        /// </summary>
        public String[] SelectValues
        {
            get
            {
                ArrayList loSelectValues = new ArrayList();
                for (int indexI = 0; indexI < Items.Count; indexI++)
                {
                    if (Items[indexI].Selected == true)
                        loSelectValues.Add(Items[indexI].Value);
                }
                msSelectValues = new String[loSelectValues.Count];
                loSelectValues.CopyTo(msSelectValues);
                return msSelectValues;
            }
            set
            {
                msSelectValues = value;
                //����
                for (int indexI = 0; indexI < Items.Count; indexI++)
                {
                    Items[indexI].Selected = false;
                }
                //��ֵ

                if (msSelectValues != null)
                {
                    for (int indexI = 0; indexI < msSelectValues.Length; indexI++)
                    {
                        ListItem loItem = Items.FindByValue(msSelectValues[indexI]);
                        if(loItem != null)
                            Items.FindByValue(msSelectValues[indexI]).Selected = true;
                    }
                }
            }
        }
        //ѡ�е�values���ַ����ŷָ���ַ���ʾ
        [Description("ѡ�е�values���ַ����ŷָ���ַ���ʾ")]
        public String SelectValuesStr
        {
            get
            {
                msSelectValuesStr = "";
                for (int indexI = 0; indexI < Items.Count; indexI++)
                {
                    if (Items[indexI].Selected == true)
                        msSelectValuesStr += Items[indexI].Value + ",";
                }
                msSelectValuesStr = msSelectValuesStr.TrimEnd(',');
                return msSelectValuesStr;
            }
            set
            {
                msSelectValuesStr = value;
                //����
                for (int indexI = 0; indexI < Items.Count; indexI++)
                {
                    Items[indexI].Selected = false;
                }
                //��ֵ
                
                for (int indexI = 0; indexI < msSelectValues.Length; indexI++)
                {
                    Items.FindByValue((msSelectValuesStr.Split(','))[indexI]).Selected = true;
                }
            }
        }
    }
}
