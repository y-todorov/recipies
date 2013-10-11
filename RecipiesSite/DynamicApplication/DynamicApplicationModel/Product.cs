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
    
    public partial class Product
    {
        public Product()
        {
            this.Inventories = new HashSet<Inventory>();
            this.ProductVendors = new HashSet<ProductVendor>();
            this.ProductWastes = new HashSet<ProductWaste>();
            this.PurchaseOrderDetails = new HashSet<PurchaseOrderDetail>();
            this.RecipeIngredients = new HashSet<RecipeIngredient>();
        }
    
        public int ProductId { get; set; }
        public Nullable<int> UnitMeasureId { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public Nullable<int> StoreId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public Nullable<double> UnitsInStock { get; set; }
        public Nullable<double> UnitsOnOrder { get; set; }
        public Nullable<double> ReorderLevel { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ModifiedByUser { get; set; }
        public double StockValue { get; set; }
    
        public virtual ICollection<Inventory> Inventories { get; set; }
        public virtual Store Store { get; set; }
        public virtual ICollection<ProductVendor> ProductVendors { get; set; }
        public virtual ICollection<ProductWaste> ProductWastes { get; set; }
        public virtual ICollection<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }
        public virtual ICollection<RecipeIngredient> RecipeIngredients { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }
        public virtual UnitMeasure UnitMeasure { get; set; }
    }
}