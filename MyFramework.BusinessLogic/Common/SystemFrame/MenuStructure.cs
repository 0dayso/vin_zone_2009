using System;
using System.Collections;
using System.Collections.Generic;
namespace MyFramework.BusinessLogic.Common.SystemFrame
{
	/// <summary>
	/// 本类包含菜单的相关信息.
	/// </summary>
	public class MenuStructure
	{
        public List<Menuitem> TopMenuitems = new List<Menuitem>();
        public List<Menuitem> AvailableItems = new List<Menuitem>();
		public MenuStructure()
		{
		}

		/// <summary>
		/// 把一个菜单项加入到菜单集合中.
		/// </summary>
		/// <param name="toMenuitem">加入一个菜单项</param>
		public void AddMenuitem(Menuitem toMenuitem)
		{
			Menuitem loMenuitem;

            if (toMenuitem.ParentMenuID == null || toMenuitem.ParentMenuID=="")
				this.TopMenuitems.Add(toMenuitem);
			else
			{
				loMenuitem = this.FindMenuitem(this.TopMenuitems,toMenuitem.ParentMenuID);
				if (loMenuitem != null)
					loMenuitem.Subitems.Add(toMenuitem);
			}

		}

		public Menuitem FindTopMenuitem(string tsMenuId)
		{
			foreach (Menuitem loTopMenuitem in this.TopMenuitems)
			{
				if(loTopMenuitem.MenuID == tsMenuId)
					return loTopMenuitem;				
			}
			return null;
		}

        public Menuitem FindMenuitem(List<Menuitem> toMenus, string tsMenuID)
		{
			Menuitem loRetMenu;
			if (toMenus == null)
				return null;
			foreach (Menuitem loItem in toMenus)
			{
				if (loItem.MenuID == tsMenuID)
					loRetMenu = loItem;
				else
					loRetMenu = FindMenuitem(loItem.Subitems, tsMenuID);
				if (loRetMenu != null)
					return loRetMenu;
			}
			return null;
		}

		public string GetMenuURL(string tsMenuID)
		{
			Menuitem loMenuitem = this.FindMenuitem(this.TopMenuitems,tsMenuID);
			if (loMenuitem == null) 
				return "";
			else
                return MyFramework.BusinessLogic.Common.BasePage.AppendQueryString(loMenuitem.HyperLink, "TopMenuId=" + loMenuitem.ParentMenuID + "&LeftMenuId=" + loMenuitem.MenuID);
                //return loMenuitem.HyperLink;
        }
        public void EnavailableMenus(List<Int32> toFunctions)
        {
            CheckVisibleMenus(this.TopMenuitems, toFunctions);
            SetAvailableMenus(this.TopMenuitems);
            this.TopMenuitems = new List<Menuitem>();
            this.TopMenuitems = this.AvailableItems;
        }
        private Boolean CheckVisibleMenus(List<Menuitem> toMenuitems, List<Int32> toAvailableMenuIds)
        {
            Boolean lbVisible = false;
	        for(int lnIndex = 0; lnIndex<toMenuitems.Count; lnIndex++)
	         {
		        Menuitem loItem=(Menuitem)toMenuitems[lnIndex];
                if (toAvailableMenuIds.Contains(Convert.ToInt32(loItem.MenuID)) && loItem.Subitems.Count == 0) loItem.Visible = true;
		        else
		        {
			        if(loItem.Subitems.Count==0) loItem.Visible = false;
			        else loItem.Visible = CheckVisibleMenus(loItem.Subitems, toAvailableMenuIds);
		         }
                 if (loItem.Visible) lbVisible = true;
		      }
              return lbVisible;
	     }
        private void SetAvailableMenus(List<Menuitem> toMenuitems)
        {
	        for(int lnIndex = 0; lnIndex<toMenuitems.Count; lnIndex++)
	        {
                Menuitem loItem = (Menuitem)toMenuitems[lnIndex];
		        if(loItem.Visible)
		        {
                    PAddMenuitem(this.AvailableItems, new Menuitem(loItem.MenuID, loItem.ParentMenuID, loItem.Caption, loItem.HyperLink, loItem.Level));
			        if(loItem.Subitems.Count>0) this.SetAvailableMenus(loItem.Subitems);
		        }
	        }

        }

        private void PAddMenuitem(List<Menuitem> toMenuitems, Menuitem toMenuitem)
        {
            if (toMenuitem.ParentMenuID==null || toMenuitem.ParentMenuID == "")
            {
                toMenuitems.Add(toMenuitem);
            }
            else
            {
                Menuitem loItem = PFindMenuitem(toMenuitems, toMenuitem.ParentMenuID);
                if (loItem != null)
                {
                    loItem.Subitems.Add(toMenuitem);
                }
            }
        }

        private Menuitem PFindMenuitem(List<Menuitem> toMenuitems, string tsMenuId)
        {
	        Menuitem loTopItem;
	        for(int lnIndex = 0; lnIndex<toMenuitems.Count; lnIndex++)
	        {
		        if(((Menuitem)toMenuitems[lnIndex]).MenuID == tsMenuId)
			        return (Menuitem)toMenuitems[lnIndex];
                loTopItem = RecusiveFindMenuitem((Menuitem)toMenuitems[lnIndex], tsMenuId);
                if (loTopItem != null)
                    return loTopItem;
	        }
	        return null;
        }
        private Menuitem RecusiveFindMenuitem(Menuitem toCurrentItem, string tsMenuId)
        {
            Menuitem loTempItem;
	        for(int lnIndex = 0; lnIndex<toCurrentItem.Subitems.Count;lnIndex++)
	        {
                if (((Menuitem)toCurrentItem.Subitems[lnIndex]).MenuID == tsMenuId)
                    return (Menuitem)toCurrentItem.Subitems[lnIndex];
                loTempItem = RecusiveFindMenuitem((Menuitem)toCurrentItem.Subitems[lnIndex], tsMenuId);
		        if(loTempItem != null)
			        return loTempItem;
		
	        }
	        return null;
        }
    }
}
