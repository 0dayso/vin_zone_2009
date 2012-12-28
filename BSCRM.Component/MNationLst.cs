using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;

namespace BSCRM.Component
{
    public  class HNationLst
    {
        public HNationLst()
        { }

        /// <summary>
        /// Ãñ×å
        /// </summary>
        /// <param name="toItems"></param>
        /// <param name="teListMode"></param>
        public static void FillItems(ListItemCollection toItems, ListMode teListMode)
        {
            BaseListController.FillItems(toItems, teListMode, "select nationid, nation from snation");
        }
    }
}
