using System;
using System.Collections;
using System.Collections.Generic;
namespace MyFramework.BusinessLogic.Common.SystemFrame
{
	public class Menuitem
	{

        /// <summary>
        /// 此类用于菜单项的建立
        /// </summary>
		private string lnCaption;
		private string lnHyperLink;

		public Menuitem()
		{
	
		}
		public string MenuID;
		public string ParentMenuID;
		
		public string Caption
		{
			get
			{
				return lnCaption;
			}
			set
			{
				lnCaption = value;
			}
		}

		public string HyperLink
		{
			get
			{
				return lnHyperLink;
			}
			set
			{
				lnHyperLink = value;
			}
		}
        private Boolean visible=true;
        public Boolean Visible
		{
			get
			{
                return visible;
			}
			set
			{
                visible = value;
			}
		}
        private string level;
        public string Level
        {
            get
            {
                return level;
            }
            set
            {
                level = value;
            }
        }
       	public List<Menuitem> Subitems=new List<Menuitem>();

		public Menuitem(string tsMenuID,string tsParentMenuID,string tsCaption,string tsHyperLink,string tslevel)
		{
			MenuID = tsMenuID;
			ParentMenuID = tsParentMenuID;
			Caption = tsCaption;
			HyperLink = tsHyperLink;
            Level = tslevel;
		//	Subitems = new ArrayList();
		}		
	}
}
