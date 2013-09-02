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
using RecipiesModelNS;


namespace RecipiesModelNS	
{
	public partial class PurchaseOrderDetail
	{
		private int _purchaseOrderDetailId;
		public virtual int PurchaseOrderDetailId 
		{ 
		    get
		    {
		        return this._purchaseOrderDetailId;
		    }
		    set
		    {
		        this._purchaseOrderDetailId = value;
		    }
		}
		
		private int? _purchaseOrderId;
		public virtual int? PurchaseOrderId 
		{ 
		    get
		    {
		        return this._purchaseOrderId;
		    }
		    set
		    {
		        this._purchaseOrderId = value;
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
		
		private int? _orderQuantity;
		public virtual int? OrderQuantity 
		{ 
		    get
		    {
		        return this._orderQuantity;
		    }
		    set
		    {
		        this._orderQuantity = value;
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
		
		private decimal _lineTotal;
		public virtual decimal LineTotal 
		{ 
		    get
		    {
		        return this._lineTotal;
		    }
		}
		
		private int? _receivedQuantity;
		public virtual int? ReceivedQuantity 
		{ 
		    get
		    {
		        return this._receivedQuantity;
		    }
		    set
		    {
		        this._receivedQuantity = value;
		    }
		}
		
		private int _stockedQuantity;
		public virtual int StockedQuantity 
		{ 
		    get
		    {
		        return this._stockedQuantity;
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
		
		private int? _returnedQuantity;
		public virtual int? ReturnedQuantity 
		{ 
		    get
		    {
		        return this._returnedQuantity;
		    }
		    set
		    {
		        this._returnedQuantity = value;
		    }
		}
		
		private PurchaseOrderHeader _purchaseOrderHeader;
		public virtual PurchaseOrderHeader PurchaseOrderHeader 
		{ 
		    get
		    {
		        return this._purchaseOrderHeader;
		    }
		    set
		    {
		        this._purchaseOrderHeader = value;
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
