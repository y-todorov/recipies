using RecipiesModelNS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InventoryManagementMVC.Models
{
    public class ProductInventoryHeaderViewModel
    {
        [Key]
        public int ProductInventoryHeaderId { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ForDate { get; set; }

        public DateTime? ModifiedDate { get; set; }
        
        public string ModifiedByUser { get; set; }

        public static ProductInventoryHeaderViewModel ConvertFromProductInventoryHeaderEntity(
           ProductInventoryHeader entity,
           ProductInventoryHeaderViewModel model)
        {
            model.ProductInventoryHeaderId = entity.ProductInventoryHeaderId;
            model.ForDate = entity.ForDate;

            model.ModifiedDate = entity.ModifiedDate;
            model.ModifiedByUser = entity.ModifiedByUser;


            return model;
        }

        public static ProductInventoryHeader ConvertToProductInventoryHeaderEntity(
            ProductInventoryHeaderViewModel model, ProductInventoryHeader entity)
        {
            entity.ProductInventoryHeaderId = model.ProductInventoryHeaderId;
            entity.ForDate = model.ForDate;

            entity.ModifiedDate = model.ModifiedDate;
            entity.ModifiedByUser = model.ModifiedByUser;
            
            return entity;
        }

    }
}