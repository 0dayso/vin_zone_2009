using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Web.UI;
using System.Web.UI.Design;
using System.Web.UI.WebControls;
using ProgStudios.WebControls;

namespace ProgStudios.WebControls.Design {
	/// <summary>
	/// Summary description for ComboBoxDesigner.
	/// </summary>
	public class ComboBoxDesigner: ControlDesigner, IDataSourceProvider {
		public ComboBoxDesigner() {
			//
			// TODO: Add constructor logic here
			//
		}
		public override string GetDesignTimeHtml() {
			ComboBox component = (ComboBox) base.Component;
			return @"<table cellspacing=0 cellpadding=0 border=0>
				<tr>
					<td style='background-color:white;border:ridge 1px buttonface;' ><input style='border:none;margin-right:1px;margin-left:1px;font-size:9pt;' size='"+component.Size+"' value='" + component.Value + @"'></td>
					<td style='background-color:buttonface;border-top:ridge 1px buttonface;border-bottom:ridge 1px buttonface;border-right:ridge 1px buttonface;'> .. </td>
				</tr>
			</table>";
		}
		
		
		#region Proxies of the properties that are involved in DataBinding
		/// <summary>
		/// This is a proxy for the DataMember field that is required to attach the DataMemberConverter to.
		/// </summary>
		public string DataMember {
			get {
				return ((ComboBox)base.Component).DataMember;
			}
			set {
				((ComboBox)base.Component).DataMember = value;
			}
		}

		/// <summary>
		/// This is a proxy for the DataTextField field that is required to attach the DataFieldConverter to.
		/// </summary>
		public string DataTextField {
			get {
				return ((ComboBox)base.Component).DataTextField;
			}
			set {
				((ComboBox)base.Component).DataTextField = value;
			}
		}

		/// <summary>
		/// This is a proxy for the DataValueField field that is required to attach the DataFieldConverter to.
		/// </summary>
		public string DataValueField {
			get {

				return ((ComboBox)base.Component).DataValueField;
			}
			set {
				((ComboBox)base.Component).DataValueField = value;
			}
		}

		/// <summary>
		/// This is a proxy for the DataSource field that is required to attach the DataSourceConverter to.
		/// This is especially required as it allows us to represent the DataSource property as a string
		/// rather then as an object.
		/// </summary>
		public string DataSource {
			get {
				DataBinding binding = DataBindings["DataSource"];
				if (binding != null) 
					return binding.Expression;
				return string.Empty;
			}
			set {				
				if ((value == null) || (value.Length == 0)) 
					base.DataBindings.Remove("DataSource");
				else {
					DataBinding binding = DataBindings["DataSource"];
					if (binding == null) 
						binding = new DataBinding("DataSource", 
							typeof(IEnumerable), value);
					else 
						binding.Expression = value;
					DataBindings.Add(binding);
				}

				OnBindingsCollectionChanged("DataSource");
			}
		}
		#endregion

		#region Overrides
		/// <summary>
		/// Set to false so that the control can't be resized on the form.
		/// </summary>
		public override bool AllowResize {
			get {
				return false;
			}
		}

		
		/// <summary>
		/// Used to modify the Attributes of the 'Data' related fields such that
		/// the correct TypeConverters are added to the Attributes. For some reason
		/// adding the attributes directly doesn't work.
		/// </summary>
		/// <param name="properties">The dictionary</param>
		protected override void PreFilterProperties(IDictionary properties) {
			base.PreFilterProperties(properties);
			PropertyDescriptor prop = (PropertyDescriptor)properties["DataSource"];
			if(prop!=null) {
				System.ComponentModel.AttributeCollection runtimeAttributes = prop.Attributes;
				// make a copy of the original attributes but make room for one extra attribute ie the TypeConverter attribute
				Attribute[] attrs = new Attribute[runtimeAttributes.Count + 1];
				runtimeAttributes.CopyTo(attrs, 0);
				attrs[runtimeAttributes.Count] = new TypeConverterAttribute(typeof(DataSourceConverter));
				prop = TypeDescriptor.CreateProperty(this.GetType(), "DataSource", typeof(string),attrs);
				properties["DataSource"] = prop;
			}			

			prop = (PropertyDescriptor)properties["DataMember"];
			if(prop!=null) {
				System.ComponentModel.AttributeCollection runtimeAttributes = prop.Attributes;
				Attribute[] attrs = new Attribute[runtimeAttributes.Count + 1];
				// make a copy of the original attributes but make room for one extra attribute ie the TypeConverter attribute
				runtimeAttributes.CopyTo(attrs, 0);
				attrs[runtimeAttributes.Count] = new TypeConverterAttribute(typeof(DataMemberConverter));
				prop = TypeDescriptor.CreateProperty(this.GetType(), "DataMember", typeof(string),attrs);
				properties["DataMember"] = prop;
			}			

			prop = (PropertyDescriptor)properties["DataValueField"];
			if(prop!=null) {
				System.ComponentModel.AttributeCollection runtimeAttributes = prop.Attributes;
				Attribute[] attrs = new Attribute[runtimeAttributes.Count + 1];
				// make a copy of the original attributes but make room for one extra attribute ie the TypeConverter attribute
				runtimeAttributes.CopyTo(attrs, 0);
				attrs[runtimeAttributes.Count] = new TypeConverterAttribute(typeof(DataFieldConverter));
				prop = TypeDescriptor.CreateProperty(this.GetType(), "DataValueField", typeof(string),attrs);
				properties["DataValueField"] = prop;
			}			
			
			prop = (PropertyDescriptor)properties["DataTextField"];
			if(prop!=null) {
				System.ComponentModel.AttributeCollection runtimeAttributes = prop.Attributes;
				Attribute[] attrs = new Attribute[runtimeAttributes.Count + 1];
				// make a copy of the original attributes but make room for one extra attribute ie the TypeConverter attribute
				runtimeAttributes.CopyTo(attrs, 0);
				attrs[runtimeAttributes.Count] = new TypeConverterAttribute(typeof(DataFieldConverter));
				prop = TypeDescriptor.CreateProperty(this.GetType(), "DataTextField", typeof(string),attrs);
				properties["DataTextField"] = prop;
			}			
		}
		#endregion
				
		#region IDataSourceProvider methods
		/// <summary>
		/// Used by the DataFieldConverter to resolve the DataSource and DataMember combination
		/// so that it can populate a dropdown with a list of available fields.
		/// </summary>
		IEnumerable IDataSourceProvider.GetResolvedSelectedDataSource() {
			DataBinding binding;
			binding = this.DataBindings["DataSource"];
			if (binding != null)
				return DesignTimeData.GetSelectedDataSource(this.Component, binding.Expression, this.DataMember);
			return null;
		}

		/// <summary>
		/// Used by the DataMemberConverter to resolve the DataSource which it can then use
		/// to populate a drop down box containing a list of available tables.
		/// </summary>
		/// <returns>The object that is our DataSource</returns>
		object IDataSourceProvider.GetSelectedDataSource() {
			DataBinding binding;

			binding = this.DataBindings["DataSource"];
			if (binding != null)
				return DesignTimeData.GetSelectedDataSource(this.Component, binding.Expression);
			return null;
		}
		#endregion
	}
}
