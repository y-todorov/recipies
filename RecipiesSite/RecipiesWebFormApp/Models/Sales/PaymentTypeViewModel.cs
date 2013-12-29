using InventoryManagementMVC.DataAnnotations;
using RecipiesModelNS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagementMVC.Models
{
    public class PaymentTypeViewModel
    {
        [Key]
        public int PaymentTypeId { get; set; }

        [Required()]
        public string Name { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string ModifiedByUser { get; set; }

        public List<PurchaseOrderDetailViewModel> PurchaseOrderDetailViewModels { get; set; }


        public static PaymentTypeViewModel ConvertFromPaymentTypeEntity(PaymentType entity,
            PaymentTypeViewModel model)
        {
            model.PaymentTypeId = entity.PaymentTypeId;
            model.Name = entity.Name;
            model.ModifiedByUser = entity.ModifiedByUser;
            model.ModifiedDate = entity.ModifiedDate;

            return model;
        }

        public static PaymentType ConvertToPaymentTypeEntity(PaymentTypeViewModel model,
            PaymentType entity)
        {
            entity.PaymentTypeId = model.PaymentTypeId;
            entity.Name = model.Name;
            entity.ModifiedByUser = model.ModifiedByUser;
            entity.ModifiedDate = model.ModifiedDate;

            return entity;
        }
    }
}