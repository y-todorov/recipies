//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RecipiesModelNS
{
    using System;
    using System.Collections.Generic;
    
    public partial class Recipe
    {
        public Recipe()
        {
            this.SalesOrderDetails = new HashSet<SalesOrderDetail>();
            this.RecipeIngredients = new HashSet<RecipeIngredient>();
            this.RecipeInventories = new HashSet<RecipeInventory>();
            this.RecipeWastes = new HashSet<RecipeWaste>();
        }
    
        public int RecipeId { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Nullable<decimal> ProductionValuePerPortion { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ModifiedByUser { get; set; }
        public Nullable<decimal> SellValuePerPortion { get; set; }
        public decimal GrossProfit { get; set; }
    
        public virtual ProductCategory ProductCategory { get; set; }
        public virtual ICollection<SalesOrderDetail> SalesOrderDetails { get; set; }
        public virtual ICollection<RecipeIngredient> RecipeIngredients { get; set; }
        public virtual ICollection<RecipeInventory> RecipeInventories { get; set; }
        public virtual ICollection<RecipeWaste> RecipeWastes { get; set; }
    }
}
