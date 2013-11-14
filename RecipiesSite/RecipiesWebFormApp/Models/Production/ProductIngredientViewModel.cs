using System;
using System.ComponentModel.DataAnnotations;
using RecipiesModelNS;
using InventoryManagementMVC.DataAnnotations;

namespace InventoryManagementMVC.Models
{
    public class ProductIngredientViewModel
    {
        [Key]
        public int ProductIngredientId { get; set; }

        [Relation(EntityType = typeof(Recipe), DataFieldValue = "RecipeId", DataFieldText = "Name")]
        [Display(Name = "Recipe")]
        public int? RecipeId { get; set; }


        [Relation(EntityType = typeof(Product), DataFieldValue = "ProductId", DataFieldText = "Name")]
        [Display(Name = "Product")]
        public int? ProductId { get; set; }

        public double? QuantityPerPortion { get; set; }

        public decimal? Cost { get; set; }

        public decimal? TotalValue { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string ModifiedByUser { get; set; }

        public static ProductIngredientViewModel ConvertFromProductIngredientEntity(ProductIngredient entity,
            ProductIngredientViewModel model)
        {            
            model.ProductIngredientId = entity.ProductIngredientId;
            model.RecipeId = entity.RecipeId;
            model.ProductId = entity.ProductId;
            model.QuantityPerPortion = entity.QuantityPerPortion;
            model.Cost = entity.Cost;
            model.TotalValue = (decimal?)entity.TotalValue;

            model.ModifiedDate = entity.ModifiedDate;
            model.ModifiedByUser = entity.ModifiedByUser;

            return model;
        }

        public static ProductIngredient ConvertToProductIngredientEntity(ProductIngredientViewModel model,
            ProductIngredient entity)
        {

            entity.ProductIngredientId = model.ProductIngredientId;
            entity.RecipeId = model.RecipeId;
            entity.ProductId = model.ProductId;
            entity.QuantityPerPortion = model.QuantityPerPortion;
            entity.Cost = model.Cost;
            entity.TotalValue = (double)model.TotalValue.GetValueOrDefault();

            entity.ModifiedDate = model.ModifiedDate;
            entity.ModifiedByUser = model.ModifiedByUser;
            
            return entity;
        }
    }
}