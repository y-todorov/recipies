using InventoryManagementMVC.DataAnnotations;
using RecipiesModelNS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InventoryManagementMVC.Models
{
    public class SalesOrderHeaderViewModel
    {
        [Key]
        public int SalesOrderHeaderId { get; set; }

        [Relation(EntityType = typeof(Customer), DataFieldValue = "CustomerID",
            DataFieldText = "ContactName")]
        [Display(Name = "Customer")]
        public int? CustomerId { get; set; }

        [Relation(EntityType = typeof(SalesOrderStatu), DataFieldValue = "SalesOrderStatusId",
           DataFieldText = "Name")]
        [Display(Name = "Status")]
        public int? StatusId { get; set; }

        [Relation(EntityType = typeof (Employee), DataFieldValue = "EmployeeId", DataFieldText = "FirstName")]
        [Display(Name = "Employee")]
        public int? EmployeeId { get; set; }

        [Relation(EntityType = typeof (PaymentType), DataFieldValue = "PaymentTypeId", DataFieldText = "Name")]
        [Display(Name = "Payment Type")]
        public int? PaymentTypeId { get; set; }
         
        public string AccountName { get; set; }
        [Required]
        public DateTime? OrderDate { get; set; }
        
        [Required]
        public DateTime? RequiredDate { get; set; }

        [Required]
        public DateTime? ShippedDate { get; set; }

        public string ShipName { get; set; }

        public string ShipAddress { get; set; } 

        public DateTime? ModifiedDate { get; set; }

        public string ModifiedByUser { get; set; }

        public List<PurchaseOrderDetailViewModel> PurchaseOrderDetailViewModels { get; set; }


        public static SalesOrderHeaderViewModel ConvertFromSalesOrderHeaderEntity(SalesOrderHeader entity,
            SalesOrderHeaderViewModel model)
        {
            model.AccountName = entity.AccountName;
            model.CustomerId = entity.CustomerId;
            model.EmployeeId = entity.EmployeeId;
            model.ModifiedByUser = entity.ModifiedByUser;
            model.ModifiedDate = entity.ModifiedDate;
            model.OrderDate = entity.OrderDate;
            model.PaymentTypeId = entity.PaymentTypeId;
            model.RequiredDate = entity.RequiredDate;
            model.SalesOrderHeaderId = entity.SalesOrderHeaderId;
            model.ShipAddress = entity.ShipAddress;
            model.ShipName = entity.ShipName;
            model.ShippedDate = entity.ShippedDate;
            model.StatusId = entity.StatusId;
                     
            return model;
        }

        public static SalesOrderHeader ConvertToSalesOrderHeaderEntity(SalesOrderHeaderViewModel model,
            SalesOrderHeader entity)
        {
            entity.AccountName = model.AccountName;
            entity.CustomerId = model.CustomerId;
            entity.EmployeeId = model.EmployeeId;
            entity.ModifiedByUser = model.ModifiedByUser;
            entity.ModifiedDate = model.ModifiedDate;
            entity.OrderDate = model.OrderDate;
            entity.PaymentTypeId = model.PaymentTypeId;
            entity.RequiredDate = model.RequiredDate;
            entity.SalesOrderHeaderId = model.SalesOrderHeaderId;
            entity.ShipAddress = model.ShipAddress;
            entity.ShipName = model.ShipName;
            entity.ShippedDate = model.ShippedDate;
            entity.StatusId = model.StatusId;

            return entity;
        }
    }
}