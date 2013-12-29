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

        public PaymentTypeViewModel ConvertFromEntity(PaymentType entity)
        {
            this.PaymentTypeId = entity.PaymentTypeId;
            this.Name = entity.Name;
            this.ModifiedByUser = entity.ModifiedByUser;
            this.ModifiedDate = entity.ModifiedDate;

            return this;
        }
     
        public PaymentType ConvertToEntity(PaymentType entity)
        {
            entity.PaymentTypeId = this.PaymentTypeId;
            entity.Name = this.Name;
            entity.ModifiedByUser = this.ModifiedByUser;
            entity.ModifiedDate = this.ModifiedDate;

            return entity;
        }
    }
}