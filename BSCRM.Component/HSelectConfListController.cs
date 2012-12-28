using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
namespace BSCRM.Component
{
    public class HSelectConfListController
    {
        public HSelectConfListController()
		{
		}
        public static void FillItems(ListItemCollection toItems, ListMode teListMode)
		{
            HSelectConfListController.FillItems(toItems, teListMode, "");
		}
        public static void FillItems(ListItemCollection toItems, ListMode teListMode, string tsWhereClause)
        {
            BaseListController.FillItems(toItems, teListMode, CRM.Buzlogic.Common.DataUtility.AppendString("select AUX_TYPE_ID,AUX_TYPE_NAME from T_HD_AUX_TYPE", " WHERE ", tsWhereClause));
        }
        
    }
}
