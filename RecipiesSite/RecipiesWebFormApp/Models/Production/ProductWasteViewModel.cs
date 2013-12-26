using System;
using System.ComponentModel.DataAnnotations;
using InventoryManagementMVC.DataAnnotations;
using RecipiesModelNS;

namespace InventoryManagementMVC.Models
{
    public class ProductWasteViewModel : WasteViewModel
    {
        [Relation(EntityType = typeof(Product), DataFieldValue = "ProductId", DataFieldText = "Name")]
        [Display(Name = "Product")]
        public int? ProductId { get; set; }

        public static ProductWasteViewModel ConvertFromProductWasteEntity(ProductWaste entity,
            ProductWasteViewModel model)
        {
            ConvertFromWasteEntity(entity, model);
            model.ProductId = entity.ProductId;
            if (entity.Product != null && entity.Product.UnitMeasure != null)
            {
                model.UnitMeasureId = entity.Product.UnitMeasure.UnitMeasureId;
            }
            if (entity.Product != null)
            {
                model.UnitPrice = entity.Product.UnitPrice;
            }
            return model;
        }

        public static ProductWaste ConvertToProductWasteEntity(ProductWasteViewModel model,
            ProductWaste entity)
        {
            ConvertToWasteEntity(model, entity);
            entity.ProductId = model.ProductId;

            return entity;
        }
    }
}