using System;
using System.Xml;
using System.Data;

namespace MyFramework.Component
{
	/// <summary>
	/// XmlArray 的摘要说明。
	/// </summary>
	public class XmlArray
	{

		XmlDocument moXmlDocument = new XmlDocument();
		XmlNode moCurrentItem = null;
		string msArrayName = "Item";

		public XmlArray()
		{
			moXmlDocument.InnerXml = "<XmlData></XmlData>";
		}

		public string InnerXml
		{
			get
			{
				return this.moXmlDocument.InnerXml;
			}
			set
			{
				this.moXmlDocument.InnerXml = value;
			}
		}

		private XmlNode RootNode
		{
			get
			{
				return this.moXmlDocument.ChildNodes[0];
			}
		}
	
		public string ContentXml
		{
			get
			{
				return this.RootNode.InnerXml;
			}
			set
			{
				this.RootNode.InnerXml = value;
			}
		}

		public int Count
		{
			get
			{
				return this.RootNode.ChildNodes.Count;
			}
		}

		public int CurrentIndex
		{
			set
			{
				this.moCurrentItem = this.RootNode.ChildNodes[value];
			}
		}

		public XmlNode CurrentItem
		{
			get
			{
				return this.moCurrentItem;
			}
			set
			{
				this.moCurrentItem = value;
			}
		}

		public void AddItem()
		{
			this.moCurrentItem = this.moXmlDocument.CreateElement(this.msArrayName);
			this.RootNode.AppendChild(this.moCurrentItem);
		}

		public void RemoveItem()
		{
			if(this.moCurrentItem!=null)
			{
				this.RootNode.RemoveChild(this.moCurrentItem);
			}
		}

		public void RemoveItem(XmlNode toItem)
		{
			this.RootNode.RemoveChild(toItem);
		}

		public string GetAttribute(string tsAttributeName)
		{
			if(this.moCurrentItem==null)
			{
				throw new Exception("No current item is available.", null);
			}
			else
			{
				XmlAttribute loAttribute = this.moCurrentItem.Attributes[tsAttributeName];
				if(loAttribute==null) return null;
				else return loAttribute.Value;
			}
		}

		public void SetAttribute(string tsAttributeName, string tsValue)
		{
			if(this.moCurrentItem==null)
			{
				throw new Exception("No current item is available.", null);
			}
			else
			{
				XmlAttribute loAttribute = this.moCurrentItem.Attributes[tsAttributeName];
				if(loAttribute==null)
				{
					loAttribute = this.moXmlDocument.CreateAttribute(tsAttributeName);
					this.moCurrentItem.Attributes.Append(loAttribute);
				}
				loAttribute.Value = tsValue;
			}
		}
		public void Clear()
		{
			this.RootNode.InnerXml = "";
		}

		/// <summary>
		/// Load the data table into the XMLArray.
		/// </summary>
		/// <param name="toTable"></param>
		/// <param name="tsColumnNames"></param>
		public void Load(DataTable toTable, string[] tsColumnNames)
		{
			DataColumn[] loColumns = GetColumns(toTable, tsColumnNames, true);
			foreach(DataRow loRow in toTable.Rows)
			{
				this.AddItem();
				foreach(DataColumn loColumn in loColumns)
				{
					this.SetAttribute(loColumn.ColumnName, loRow[loColumn].ToString());
				}
			}
		}

        public static DataColumn[] GetColumns(DataTable toDataTable, string[] tsColumnNames, bool tbValidate)
        {
            DataColumn[] loColumns = null;
            if (tsColumnNames == null)
            {
                loColumns = new DataColumn[toDataTable.Columns.Count];
                for (int lnIndex = 0; lnIndex < toDataTable.Columns.Count; lnIndex++)
                    loColumns[lnIndex] = toDataTable.Columns[lnIndex];
            }
            else
            {
                loColumns = new DataColumn[tsColumnNames.Length];
                for (int lnIndex = 0; lnIndex < tsColumnNames.Length; lnIndex++)
                {
                    loColumns[lnIndex] = toDataTable.Columns[tsColumnNames[lnIndex]];
                    if (loColumns[lnIndex] == null && tbValidate)
                        throw new Exception("列 - " + tsColumnNames[lnIndex] + " 没有找到.", null);
                }
            }
            return loColumns;
        }
        //public void Fill(DataTable toTable, string[] tsColumnNames)
        //{
        //    DataColumn[] loColumns = DataSetUtility.GetColumns(toTable, tsColumnNames, true);
        //    for (int lnIndex = 0; lnIndex < this.Count; lnIndex++)
        //    {
        //        this.CurrentIndex = lnIndex;
        //        DataRow loRow = toTable.NewRow();
        //        foreach (DataColumn loColumn in loColumns)
        //        {
        //            loRow[loColumn] = DataConvertor.ToValueWithType(this.GetAttribute(loColumn.ColumnName), loColumn.DataType);
        //        }
        //        toTable.Rows.Add(loRow);
        //    }
        //}
	}
}
