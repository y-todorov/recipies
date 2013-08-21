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
	public partial class ProductVendor
	{
		private int _productVendorId;
		public virtual int ProductVendorId 
		{ 
		    get
		    {
		        return this._productVendorId;
		    }
		    set
		    {
		        this._productVendorId = value;
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
		
		private int? _vendorId;
		public virtual int? VendorId 
		{ 
		    get
		    {
		        return this._vendorId;
		    }
		    set
		    {
		        this._vendorId = value;
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
		
		private int? _averageLeadTime;
		public virtual int? AverageLeadTime 
		{ 
		    get
		    {
		        return this._averageLeadTime;
		    }
		    set
		    {
		        this._averageLeadTime = value;
		    }
		}
		
		private decimal? _standardPrice;
		public virtual decimal? StandardPrice 
		{ 
		    get
		    {
		        return this._standardPrice;
		    }
		    set
		    {
		        this._standardPrice = value;
		    }
		}
		
		private decimal? _lastReceiptCost;
		public virtual decimal? LastReceiptCost 
		{ 
		    get
		    {
		        return this._lastReceiptCost;
		    }
		    set
		    {
		        this._lastReceiptCost = value;
		    }
		}
		
		private DateTime? _lastReceiptDate;
		public virtual DateTime? LastReceiptDate 
		{ 
		    get
		    {
		        return this._lastReceiptDate;
		    }
		    set
		    {
		        this._lastReceiptDate = value;
		    }
		}
		
		private int? _minOrderQuantity;
		public virtual int? MinOrderQuantity 
		{ 
		    get
		    {
		        return this._minOrderQuantity;
		    }
		    set
		    {
		        this._minOrderQuantity = value;
		    }
		}
		
		private int? _maxOrderQuantity;
		public virtual int? MaxOrderQuantity 
		{ 
		    get
		    {
		        return this._maxOrderQuantity;
		    }
		    set
		    {
		        this._maxOrderQuantity = value;
		    }
		}
		
		private int? _onOrderQuantity;
		public virtual int? OnOrderQuantity 
		{ 
		    get
		    {
		        return this._onOrderQuantity;
		    }
		    set
		    {
		        this._onOrderQuantity = value;
		    }
		}
		
		private Vendor _vendor;
		public virtual Vendor Vendor 
		{ 
		    get
		    {
		        return this._vendor;
		    }
		    set
		    {
		        this._vendor = value;
		    }
		}
		
		private UnitMeasure _unitMeasure;
		public virtual UnitMeasure UnitMeasure 
		{ 
		    get
		    {
		        return this._unitMeasure;
		    }
		    set
		    {
		        this._unitMeasure = value;
		    }
		}
		
		private Product _product;
		public virtual Product Product 
		{ 
		    get
		    {
		        return this._product;
		    }
		    set
		    {
		        this._product = value;
		    }
		}
		
	}
}