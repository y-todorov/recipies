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
    public class ProductInventoryViewModel : InventoryViewModel
    {
        //[Key]
        //public int InventoryId { get; set; }

        [Editable(false)]
        [HiddenInput(DisplayValue = false)]
        public int? ProductInventoryHeaderId { get; set; }

        [Relation(EntityType = typeof (Product), DataFieldValue = "ProductId", DataFieldText = "Name")]
        [Display(Name = "Product")]
        public int? ProductId { get; set; }

        [Editable(false)]
        public string UnitMeasure { get; set; }

        [Editable(false)]
        public string Category { get; set; }

        public static ProductInventoryViewModel ConvertFromProductInventoryEntity(
            ProductInventory entity,
            ProductInventoryViewModel model)
        {
            InventoryViewModel.ConvertFromInventoryEntity(entity, model);
            model.ProductId = entity.ProductId;
            model.ProductInventoryHeaderId = entity.ProductInventoryHeaderId;
            if (entity.Product != null && entity.Product.ProductCategory != null)
            {
                model.Category = entity.Product.ProductCategory.Name;
            }
            if (entity.Product != null && entity.Product.UnitMeasure != null)
            {
                model.UnitMeasure = entity.Product.UnitMeasure.Name;
            }

            if (entity.ProductInventoryHeader != null)
            {
                model.InventoryHeaderForDate = entity.ProductInventoryHeader.ForDate;
            }

            return model;
        }

        public static ProductInventory ConvertToProductInventoryEntity(
            ProductInventoryViewModel model, ProductInventory entity)
        {
            InventoryViewModel.ConvertToInventoryEntity(model, entity);
            entity.ProductId = model.ProductId;
            entity.ProductInventoryHeaderId = model.ProductInventoryHeaderId;

            return entity;
        }
    }
}