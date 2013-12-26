using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using InventoryManagementMVC.DataAnnotations;
using RecipiesModelNS;

namespace InventoryManagementMVC.Models
{
    public class WasteViewModel
    {
        [Key]
        public int WasteId { get; set; }

        [Relation(EntityType = typeof(UnitMeasure), DataFieldValue = "UnitMeasureId", DataFieldText = "Name")]
        [Display(Name = "Unit Measure")]
        [ReadOnly(true)]
        public int? UnitMeasureId { get; set; } // Check this

        public double? Quantity { get; set; }

        [ReadOnly(true)]
        public decimal WasteValue { get; set; }

        public decimal? UnitPrice { get; set; }
        
        public DateTime? ModifiedDate { get; set; }

        public string ModifiedByUser { get; set; }

        public static WasteViewModel ConvertFromWasteEntity(Waste entity,
            WasteViewModel model)
        {
            model.Quantity = entity.Quantity;
            model.UnitMeasureId = entity.UnitMeasureId;
            model.UnitPrice = entity.UnitPrice;
            model.WasteId = entity.WasteId;
            model.WasteValue = (decimal)entity.WasteValue;
            model.ModifiedDate = entity.ModifiedDate;
            model.ModifiedByUser = entity.ModifiedByUser;

            return model;
        }

        public static Waste ConvertToWasteEntity(WasteViewModel model,
            Waste entity)
        {
            entity.Quantity = model.Quantity;
            entity.UnitMeasureId = model.UnitMeasureId;
            entity.UnitPrice = model.UnitPrice;
            entity.WasteId = model.WasteId;
            entity.WasteValue = (double)model.WasteValue;
            entity.ModifiedDate = model.ModifiedDate;
            entity.ModifiedByUser = model.ModifiedByUser;

            return entity;
        }
    }
}