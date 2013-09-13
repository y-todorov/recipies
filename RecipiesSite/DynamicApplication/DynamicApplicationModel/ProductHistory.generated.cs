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


namespace RecipiesModelNS	
{
	public partial class ProductHistory
	{
		private int _productHistoryId;
		public virtual int ProductHistoryId 
		{ 
		    get
		    {
		        return this._productHistoryId;
		    }
		    set
		    {
		        this._productHistoryId = value;
		    }
		}
		
		private int? _productId;
		public virtual int? ProductId 
		{ 
		    get
		    {
		        return this._productId;
		    }
		    set
		    {
		        this._productId = value;
		    }
		}
		
		private int? _unitMeasureId;
		public virtual int? UnitMeasureId 
		{ 
		    get
		    {
		        return this._unitMeasureId;
		    }
		    set
		    {
		        this._unitMeasureId = value;
		    }
		}
		
		private int? _categoryId;
		public virtual int? CategoryId 
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
		
		private string _code;
		public virtual string Code 
		{ 
		    get
		    {
		        return this._code;
		    }
		    set
		    {
		        this._code = value;
		    }
		}
		
		private decimal? _unitPrice;
		public virtual decimal? UnitPrice 
		{ 
		    get
		    {
		        return this._unitPrice;
		    }
		    set
		    {
		        this._unitPrice = value;
		    }
		}
		
		private int? _unitsInStock;
		public virtual int? UnitsInStock 
		{ 
		    get
		    {
		        return this._unitsInStock;
		    }
		    set
		    {
		        this._unitsInStock = value;
		    }
		}
		
		private int? _unitsOnOrder;
		public virtual int? UnitsOnOrder 
		{ 
		    get
		    {
		        return this._unitsOnOrder;
		    }
		    set
		    {
		        this._unitsOnOrder = value;
		    }
		}
		
		private string _store;
		public virtual string Store 
		{ 
		    get
		    {
		        return this._store;
		    }
		    set
		    {
		        this._store = value;
		    }
		}
		
		private int? _reorderLevel;
		public virtual int? ReorderLevel 
		{ 
		    get
		    {
		        return this._reorderLevel;
		    }
		    set
		    {
		        this._reorderLevel = value;
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
		
	}
}
