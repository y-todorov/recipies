#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by the ClassGenerator.ttinclude code generation file.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Common;
using System.Collections.Generic;
using Telerik.OpenAccess;
using Telerik.OpenAccess.Metadata;
using Telerik.OpenAccess.Data.Common;
using Telerik.OpenAccess.Metadata.Fluent;
using Telerik.OpenAccess.Metadata.Fluent.Advanced;
using DynamicApplicationModel;

namespace DynamicApplicationModel	
{
	public partial class Category
	{
		private int _categoryID;
		public virtual int CategoryID
		{
			get
			{
				return this._categoryID;
			}
			set
			{
				this._categoryID = value;
			}
		}
		
		private string _categoryName;
		public virtual string CategoryName
		{
			get
			{
				return this._categoryName;
			}
			set
			{
				this._categoryName = value;
			}
		}
		
		private string _description;
		public virtual string Description
		{
			get
			{
				return this._description;
			}
			set
			{
				this._description = value;
			}
		}
		
		private byte[] _picture;
		public virtual byte[] Picture
		{
			get
			{
				return this._picture;
			}
			set
			{
				this._picture = value;
			}
		}
		
		private IList<Product> _products = new List<Product>();
		public virtual IList<Product> Products
		{
			get
			{
				return this._products;
			}
		}
		
	}
}
#pragma warning restore 1591
