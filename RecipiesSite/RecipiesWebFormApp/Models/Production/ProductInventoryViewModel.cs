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

        public ProductInventoryViewModel ConvertFromEntity(ProductInventory entity)
        {
            base.ConvertFromEntity(entity);
            //InventoryViewModel.ConvertFromInventoryEntity(entity, model);
            ProductId = entity.ProductId;
            ProductInventoryHeaderId = entity.ProductInventoryHeaderId;
            if (entity.Product != null && entity.Product.ProductCategory != null)
            {
                Category = entity.Product.ProductCategory.Name;
            }
            if (entity.Product != null && entity.Product.UnitMeasure != null)
            {
                UnitMeasure = entity.Product.UnitMeasure.Name;
            }

            if (entity.ProductInventoryHeader != null)
            {
                InventoryHeaderForDate = entity.ProductInventoryHeader.ForDate;
            }

            return this;
        }

        public ProductInventory ConvertToEntity(ProductInventory entity)
        {
            base.ConvertToEntity(entity);
            //InventoryViewModel.ConvertToInventoryEntity(model, entity);
            entity.ProductId = ProductId;
            entity.ProductInventoryHeaderId = ProductInventoryHeaderId;

            return entity;
        }
    }
}