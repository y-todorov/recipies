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
    
    public partial class ProductWaste
    {
        public int ProductWasteId { get; set; }
        public Nullable<int> ProductId { get; set; }
        public Nullable<int> UnitMeasureId { get; set; }
        public Nullable<double> Quantity { get; set; }
        public double WasteValue { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ModifiedByUser { get; set; }
    
        public virtual Product Product { get; set; }
        public virtual UnitMeasure UnitMeasure { get; set; }
    }
}