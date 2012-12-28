using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
namespace BSCRM.Component
{
    public class HotelTypeListController
    {
        public HotelTypeListController()
        {
        }
        public static void FillItems(ListItemCollection toItems, ListMode teListMode)
        {
            HotelTypeListController.FillItems(toItems, teListMode, "");
        }
        public static void FillItems(ListItemCollection toItems, ListMode teListMode, string tsWhereClause)
        {
            BaseListController.FillItems(toItems, teListMode, CRM.Buzlogic.Common.DataUtility.AppendString("select Hotel_Type_ID,Hotel_Type_Name from t_HD_Hotel_Type ", " WHERE ", tsWhereClause));
        }
    }
}
