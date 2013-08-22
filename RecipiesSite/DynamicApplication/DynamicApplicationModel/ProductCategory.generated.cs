#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
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
	public partial class ProductCategory
	{
		private int _categoryId;
		public virtual int CategoryId 
		{ 
		    get
		    {
		        return this._categoryId;
		    }
		    set
		    {
		        this._categoryId = value;
		    }
		}
		
		private string _name;
		public virtual string Name 
		{ 
		    get
		    {
		        return this._name;
		    }
		    set
		    {
		        this._name = value;
		    }
		}
		
		private DateTime? _modifiedDate;
		public virtual DateTime? ModifiedDate 
		{ 
		    get
		    {
		        return this._modifiedDate;
		    }
		    set
		    {
		        this._modifiedDate = value;
		    }
		}
		
		private string _modifiedByUser;
		public virtual string ModifiedByUser 
		{ 
		    get
		    {
		        return this._modifiedByUser;
		    }
		    set
		    {
		        this._modifiedByUser = value;
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
