using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace MyFramework.BusinessLogic.Common.SystemFrame
{
    public class TopMenu : System.Web.UI.WebControls.WebControl
    {
        protected string msAppDir = ""; 
		protected Table moTopTable = null;
		protected TableCell moContentCell = null;
        protected System.Web.UI.WebControls.Literal moMenuLiteral;
        //
        protected System.Web.UI.WebControls.Table tabLeftMenu;
        protected MenuStructure moMenuStructure;
        protected string msTopMenuID;
        protected string msLeftMenuID;
        protected string msMenuPrefix = "";
        private bool _IsExpand = false;
        private bool _IsTree = false;
        private string _cssItem = "bmsLeftMenuitem";
        private string _cssSubitem = "bmsLeftMenuSubitem";
        protected System.Web.UI.WebControls.Table tabLeftMenuTable;
        private string _cssSubitemSel = "bmsFocusedLeftMenuSubitem";
        private int _MenuLevel = 3;
        private string _SubImage = "";
        private string _MasterImage = "";
        //
        public TopMenu()
		{
			this.EnableViewState = false;
            _SubImage = "/" + AppDir + "/SystemFrame/images/arrow_b2.gif";
            _MasterImage = "/" + AppDir + "/SystemFrame/images/arrow_b.gif";
            tabLeftMenu = new Table();
		}
        public string MenuPrefix
        {
            get
            {
                return this.msMenuPrefix;
            }
            set
            {
                this.msMenuPrefix = value;
            }
        }

        public string SubImage
        {
            get
            {
                return _SubImage;
            }
            set
            {
                _SubImage = value;
            }
        }
        public int MenuLevel
        {
            get
            {
                return _MenuLevel;
            }
            set
            {
                _MenuLevel = value;
            }
        }
        public string cssItem
        {
            set
            {
                this._cssItem = value;
            }
        }

        public string cssSubitem
        {
            set
            {
                this._cssSubitem = value;
            }
        }

        public string cssSubitemSel
        {
            set
            {
                this._cssSubitemSel = value;
            }
        }

        public bool IsExpand
        {
            get
            {
                return _IsExpand;
            }
            set
            {
                _IsExpand = value;
            }
        }

        public bool IsTree
        {
            set
            {
                _IsTree = value;
            }
        }
		protected string AppDir
		{
			get
			{
				if(this.msAppDir=="") this.msAppDir=System.Configuration.ConfigurationSettings.AppSettings["AppDir"];
				return msAppDir;
			}
		}
		/// <summary>
		/// 根据每个菜单的信息产生相应的菜单表格
		/// </summary>
		protected void GenTopTable()
		{
			Table loTable;
			TableRow loRow;
			TableCell loCell;

			loTable = new System.Web.UI.WebControls.Table();
			this.Controls.Add(loTable);
			loTable.CellPadding=0;
			loTable.CellSpacing=0;
			loTable.Width = new Unit(100,UnitType.Percentage);
			loTable.Height = new Unit(100,UnitType.Percentage);
			loTable.BorderWidth =0;
			loTable.HorizontalAlign = HorizontalAlign.Center;

			loRow = new TableRow();
			loCell = new TableCell();

			loCell.VerticalAlign = VerticalAlign.Top;
			loTable.Rows.Add(loRow);
			loRow.Cells.Add(loCell);
			this.moContentCell = loCell;
			this.moTopTable = loTable;
		}	

		protected override void CreateChildControls()
		{
			this.GenTopTable();
            this.moMenuLiteral = new Literal();
            this.moTopTable.CssClass = "";
            this.moContentCell.VerticalAlign = VerticalAlign.Middle;
            this.moContentCell.Controls.Add(this.moMenuLiteral);

            this.moContentCell.Controls.Add(new Table());
		}
        protected override void Render(HtmlTextWriter output)
        {
            this.GenMenus();
            ///////
            this.moMenuStructure = (MenuStructure)Page.Session["MenuStructure"];
            if (this.moMenuStructure != null)
            {


                GenerateCommonMenu();

            }
            base.Render(output);
        }

        protected void GenMenus()
        {
            MenuStructure loMenuStructure = (MenuStructure)Page.Session["MenuStructure"];
            if (loMenuStructure == null) return;

            string lsText;

            lsText = "<TABLE>";
            lsText += "<TR>";
            lsText += "<TD class=\"bmsMainMenuitem\">";
            lsText += "<IMG src=\"/" + AppDir + "/SystemFrame/Images/ocd_black.gif\" border=\"0\" style=\"height:10px;width:1px;\"/>";
            lsText += "<IMG src=\"/" + AppDir + "/SystemFrame/Images/ocd_blank.gif\" border=\"0\" style=\"height:8px;width:4px;\" />";
            lsText += "<a href=\"/" + AppDir + "/ test \"><Span class=\"\">test</span></a>";
            lsText += "<IMG src=\"/" + AppDir + "/SystemFrame/Images/ocd_blank.gif\" border=\"0\" style=\"height:1px;width:9px;\" />";
            lsText += "</TD>";
            foreach (Menuitem loItem in loMenuStructure.TopMenuitems)
            {
                lsText += "<TD class=\"bmsMainMenuitem\">";
                lsText += "<IMG src=\"/" + AppDir + "/SystemFrame/Images/ocd_black.gif\" border=\"0\" style=\"height:10px;width:1px;\"/>";
                lsText += "<IMG src=\"/" + AppDir + "/SystemFrame/Images/ocd_blank.gif\" border=\"0\" style=\"height:8px;width:4px;\" />";
                lsText += "<a href=\"/" + AppDir + "/" + BasePage.AppendQueryString(loItem.HyperLink,"") + "\"><Span class=\"\">" + loItem.Caption + "</span></a>";
                lsText += "<IMG src=\"/" + AppDir + "/SystemFrame/Images/ocd_blank.gif\" border=\"0\" style=\"height:1px;width:9px;\" />";
                lsText += "</TD>";
            }
            lsText += "</TR>";
            lsText += "</TABLE>";

            moMenuLiteral.Text = lsText;
        }
		
        //subItems
        public void GenerateCommonMenu()
        {
            foreach (Menuitem loTopMenu in this.moMenuStructure.TopMenuitems)
            {
                if (loTopMenu == null)
                    return;
                foreach (Menuitem loItem in loTopMenu.Subitems)
                {
                    if (this.IsRelatedItem(loItem)) tabLeftMenu.Rows.Add(this.GenerateCommonMenu(loItem, 2));
                    if (this._MenuLevel == 3)
                        foreach (Menuitem loSubitem in loItem.Subitems)
                            if (this.IsRelatedItem(loItem)) tabLeftMenu.Rows.Add(this.GenerateCommonMenu(loSubitem, 3));

                }

            }


        }
        private bool IsRelatedItem(Menuitem loItem)
        {
            return loItem.MenuID.IndexOf(this.MenuPrefix) == 0;
        }

        private TableRow GenerateCommonMenu(Menuitem toMenuitem, int tnCurLevel)
        {
            TableRow loRow;
            TableCell loCell;
            HyperLink loHyper;
            Literal loText;
            System.Web.UI.WebControls.Image loImage;

            loRow = new TableRow();
            loCell = new TableCell();
            loCell.VerticalAlign = VerticalAlign.Top;
            loCell.HorizontalAlign = HorizontalAlign.Left;
            loHyper = new HyperLink();
            loText = new Literal();

            if (toMenuitem.HyperLink == "")
            {
                loText.Text = toMenuitem.Caption;
                loCell.Controls.Add(loText);
                loCell.CssClass = this._cssItem;
                loCell.ColumnSpan = 2;
            }
            else
            {
                loCell.VerticalAlign = VerticalAlign.Top;
                loCell.ColumnSpan = 1;
                loImage = new System.Web.UI.WebControls.Image();
                if (tnCurLevel == 2)
                {
                    loImage.ImageUrl = this._MasterImage;
                    loImage.Width = 8;
                }
                else
                {
                    loImage.Width = 16;
                    loImage.ImageUrl = this._SubImage;
                }
                loImage.Height = 15;
                loImage.BorderWidth = 0;
                loCell.Controls.Add(loImage);

                loHyper.Text = toMenuitem.Caption;
                loHyper.NavigateUrl = "/" + AppDir + "/" + BasePage.AppendQueryString(toMenuitem.HyperLink,"");
                loHyper.CssClass = this._cssSubitem;
                loCell.Controls.Add(loHyper);
            }
            loRow.Cells.Add(loCell);
            return loRow;
        }


        protected void GenerateTreeMenu(HtmlTextWriter toWriter)
        {
            //Menuitem loItem;
            //bool lbSelected = false;

            //toWriter.WriteBeginTag("TABLE");
            //toWriter.WriteAttribute("id", "leftNavTable");
            //toWriter.WriteAttribute("onkeyup", "leftnav_keyup();");
            //toWriter.AddStyleAttribute(HtmlTextWriterStyle.BackgroundColor, "#ffffff");
            //toWriter.AddStyleAttribute(HtmlTextWriterStyle.FontStyle, "MARGIN-TOP");
            //toWriter.AddStyleAttribute(HtmlTextWriterStyle.FontSize, "5px");
            //toWriter.AddStyleAttribute(HtmlTextWriterStyle.BorderWidth, "0");
            //toWriter.WriteAttribute("cellSpacing", "2");
            //toWriter.WriteAttribute("cellPadding", "2");
            //toWriter.Write(HtmlTextWriter.TagRightChar);
            //toWriter.WriteLine();
            //toWriter.WriteFullBeginTag("TBODY");
            //toWriter.WriteLine();
            //loItem = moMenuStructure.FindMenuitem(this.moMenuStructure.TopMenuitems, this.msTopMenuID);
            //if (loItem != null)
            //{
            //    foreach (Menuitem loSubItem in loItem.Subitems)
            //    {
            //        string lsName = "m" + loSubItem.MenuID;
            //        string lsBtnName = lsName + "Btn";
            //        string lsItemsName = lsName + "Items";
            //        toWriter.WriteFullBeginTag("TR");
            //        toWriter.WriteLine();

            //        toWriter.WriteBeginTag("TD");
            //        toWriter.WriteAttribute("vAlign", "top");
            //        toWriter.WriteAttribute("align", "middle");
            //        toWriter.Write(HtmlTextWriter.TagRightChar);
            //        toWriter.WriteLine();
            //        //<LABEL for=\"mSub\">
            //        toWriter.WriteBeginTag("LABEL");
            //        toWriter.WriteAttribute("for", lsName);
            //        toWriter.Write(HtmlTextWriter.TagRightChar);
            //        //<IMG class=\"clsImgButton\" id=\"mSubBtn\" onclick=\"ToggleDisplay(mSubBtn, mSubItems);\"height=\"9\" alt=\"\" src=\"default_files/plus.gif\" width=\"9\" align=\"right\" border=\"0\">
            //        toWriter.WriteBeginTag("IMG");
            //        toWriter.WriteAttribute("class", "clsImgButton");
            //        toWriter.WriteAttribute("id", lsBtnName);//mSubBtn
            //        toWriter.WriteAttribute("onclick", "ToggleDisplay(" + lsBtnName + "," + lsItemsName + ");");//mSubBtn, mSubItems
            //        toWriter.WriteAttribute("height", "9");
            //        toWriter.WriteAttribute("alt", "");
            //        toWriter.WriteAttribute("src", "Images/plus.gif");
            //        toWriter.WriteAttribute("width", "9");
            //        toWriter.WriteAttribute("align", "right");
            //        toWriter.WriteAttribute("border", "0");
            //        toWriter.Write(HtmlTextWriter.TagRightChar);
            //        toWriter.WriteEndTag("LABEL");
            //        toWriter.WriteEndTag("TD");
            //        toWriter.WriteLine();
            //        //<TD width="100%">
            //        toWriter.WriteBeginTag("TD");
            //        toWriter.WriteAttribute("width", "100%");
            //        toWriter.Write(HtmlTextWriter.TagRightChar);
            //        toWriter.WriteLine();
            //        //<A class="clsTocHead" id="mSub" onclick="return ToggleDisplay(mSubBtn, mSubItems);"href="javascript:void();">开发工具产品系列</A>
            //        toWriter.WriteBeginTag("A");
            //        toWriter.WriteAttribute("class", this._cssItem);
            //        toWriter.WriteAttribute("id", lsName);
            //        toWriter.WriteAttribute("onclick", "return ToggleDisplay(" + lsBtnName + "," + lsItemsName + ");");
            //        if (loSubItem.HyperLink == "")
            //            toWriter.WriteAttribute("href", "javascript:void();");
            //        else
            //            toWriter.WriteAttribute("href", loSubItem.HyperLink);
            //        toWriter.Write(HtmlTextWriter.TagRightChar);
            //        toWriter.Write(loSubItem.Caption);
            //        toWriter.WriteEndTag("A");
            //        toWriter.WriteLine();
            //        toWriter.WriteLine();
            //        //<DIV class="clsTocItem" id="mSubItems">
            //        toWriter.WriteBeginTag("DIV");
            //        toWriter.WriteAttribute("class", this._cssSubitem);
            //        toWriter.WriteAttribute("id", lsItemsName);
            //        toWriter.Write(HtmlTextWriter.TagRightChar);
            //        toWriter.WriteLine();
            //        foreach (Menuitem loSubMenu in loSubItem.Subitems)
            //        {
            //            toWriter.WriteFullBeginTag("DIV");
            //            toWriter.WriteBeginTag("A");
            //            toWriter.WriteAttribute("href", BasePage.AppendQueryString(loSubMenu.HyperLink, "TopMenuID=" + this.msTopMenuID + "&LeftMenuID=" + loSubMenu.MenuID));
            //            toWriter.Write(HtmlTextWriter.TagRightChar);
            //            toWriter.Write(loSubMenu.Caption);
            //            toWriter.WriteEndTag("A");
            //            toWriter.WriteEndTag("DIV");
            //            toWriter.WriteLine();
            //        }
            //        //<SCRIPT>mSubItems.style.display='none';</SCRIPT>
            //        toWriter.WriteFullBeginTag("SCRIPT");
            //        if (lbSelected || this._IsExpand)
            //        {
            //            toWriter.WriteAttribute(lsItemsName + ".style.display", "block");
            //            lbSelected = false;
            //        }
            //        else
            //            toWriter.WriteAttribute(lsItemsName + ".style.display", "none");

            //        toWriter.Write(HtmlTextWriter.SemicolonChar);
            //        toWriter.WriteEndTag("SCRIPT");
            //        toWriter.WriteLine();
            //        toWriter.WriteEndTag("DIV");
            //        toWriter.WriteEndTag("TD");
            //        toWriter.WriteEndTag("TR");
            //    }
            //}
            //toWriter.WriteEndTag("TBODY");
            //toWriter.WriteEndTag("TABlE");

        }
    }
 
}
