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
	public partial class Recipe
	{
		private int _recipeId;
		public virtual int RecipeId 
		{ 
		    get
		    {
		        return this._recipeId;
		    }
		    set
		    {
		        this._recipeId = value;
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
		
		private decimal? _sellValuePerPortion;
		public virtual decimal? SellValuePerPortion 
		{ 
		    get
		    {
		        return this._sellValuePerPortion;
		    }
		    set
		    {
		        this._sellValuePerPortion = value;
		    }
		}
		
		private decimal? _productionValuePerPortion;
		public virtual decimal? ProductionValuePerPortion 
		{ 
		    get
		    {
		        return this._productionValuePerPortion;
		    }
		    set
		    {
		        this._productionValuePerPortion = value;
		    }
		}
		
		private decimal _grossProfit;
		public virtual decimal GrossProfit 
		{ 
		    get
		    {
		        return this._grossProfit;
		    }
		}
		
		private ProductCategory _productCategory;
		public virtual ProductCategory ProductCategory 
		{ 
		    get
		    {
		        return this._productCategory;
		    }
		    set
		    {
		        this._productCategory = value;
		    }
		}
		
		private IList<SalesOrderDetail> _salesOrderDetails = new List<SalesOrderDetail>();
		public virtual IList<SalesOrderDetail> SalesOrderDetails 
		{ 
		    get
		    {
		        return this._salesOrderDetails;
		    }
		}
		
		private IList<RecipeIngredient> _recipeIngredients = new List<RecipeIngredient>();
		public virtual IList<RecipeIngredient> RecipeIngredients 
		{ 
		    get
		    {
		        return this._recipeIngredients;
		    }
		}
		
		private IList<RecipeWaste> _recipeWastes = new List<RecipeWaste>();
		public virtual IList<RecipeWaste> RecipeWastes 
		{ 
		    get
		    {
		        return this._recipeWastes;
		    }
		}
		
	}
}
