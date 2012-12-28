using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;

namespace BSCRM.Component
{
    public class HBloodTypeLst
    {

        public HBloodTypeLst()
        { }

        /// <summary>
        /// ÑªÐÍ
        /// </summary>
        /// <param name="toItems"></param>
        /// <param name="teListMode"></param>
        public static void FillItems(ListItemCollection toItems, ListMode teListMode)
        {
            BaseListController.FillItems(toItems, teListMode, "select mbloodtypeid, mbloodtype from mbloodtype");
        }
    }
}
