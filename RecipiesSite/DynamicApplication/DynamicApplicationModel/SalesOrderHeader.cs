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
    
    public partial class SalesOrderHeader
    {
        public SalesOrderHeader()
        {
            this.SalesOrderDetails = new HashSet<SalesOrderDetail>();
        }
    
        public int SalesOrderHeaderId { get; set; }
        public Nullable<int> CustomerId { get; set; }
        public Nullable<int> EmployeeId { get; set; }
        public Nullable<int> StatusId { get; set; }
        public Nullable<int> PaymentTypeId { get; set; }
        public string AccountName { get; set; }
        public Nullable<System.DateTime> OrderDate { get; set; }
        public Nullable<System.DateTime> RequiredDate { get; set; }
        public Nullable<System.DateTime> ShippedDate { get; set; }
        public string ShipName { get; set; }
        public string ShipAddress { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ModifiedByUser { get; set; }
    
        public virtual Employee Employee { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual PaymentType PaymentType { get; set; }
        public virtual ICollection<SalesOrderDetail> SalesOrderDetails { get; set; }
        public virtual SalesOrderStatu SalesOrderStatu { get; set; }
    }
}
