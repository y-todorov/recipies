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
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using Telerik.OpenAccess;
using Telerik.OpenAccess.Metadata;
using Telerik.OpenAccess.Data.Common;
using Telerik.OpenAccess.Metadata.Fluent;
using Telerik.OpenAccess.Metadata.Fluent.Advanced;
using RecipiesModelNS;


namespace RecipiesModelNS	
{
	public partial class Inventory
	{
		private int _inventoryId;
		public virtual int InventoryId 
		{ 
		    get
		    {
		        return this._inventoryId;
		    }
		    set
		    {
		        this._inventoryId = value;
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
		
		private DateTime? _forDate;
		public virtual DateTime? ForDate 
		{ 
		    get
		    {
		        return this._forDate;
		    }
		    set
		    {
		        this._forDate = value;
		    }
		}
		
		private decimal? _averageUnitPrice;
		public virtual decimal? AverageUnitPrice 
		{ 
		    get
		    {
		        return this._averageUnitPrice;
		    }
		    set
		    {
		        this._averageUnitPrice = value;
		    }
		}
		
		private double? _quantityByDocuments;
		public virtual double? QuantityByDocuments 
		{ 
		    get
		    {
		        return this._quantityByDocuments;
		    }
		    set
		    {
		        this._quantityByDocuments = value;
		    }
		}
		
		private double? _valueByDocuments;
		public virtual double? ValueByDocuments 
		{ 
		    get
		    {
		        return this._valueByDocuments;
		    }
		}
		
		private double? _stocktakeQuantity;
		public virtual double? StocktakeQuantity 
		{ 
		    get
		    {
		        return this._stocktakeQuantity;
		    }
		}
		
		private double? _stocktakeValue;
		public virtual double? StocktakeValue 
		{ 
		    get
		    {
		        return this._stocktakeValue;
		    }
		}
		
		private double? _deficiencyQuantity;
		public virtual double? DeficiencyQuantity 
		{ 
		    get
		    {
		        return this._deficiencyQuantity;
		    }
		}
		
		private double? _deficiencyValue;
		public virtual double? DeficiencyValue 
		{ 
		    get
		    {
		        return this._deficiencyValue;
		    }
		}
		
		private double? _surplusQuantity;
		public virtual double? SurplusQuantity 
		{ 
		    get
		    {
		        return this._surplusQuantity;
		    }
		}
		
		private double? _surplusValue;
		public virtual double? SurplusValue 
		{ 
		    get
		    {
		        return this._surplusValue;
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
