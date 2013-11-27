using System;
using System.ComponentModel.DataAnnotations;
using RecipiesModelNS;
using InventoryManagementMVC.DataAnnotations;
using System.ComponentModel;

namespace InventoryManagementMVC.Models
{
    public class RecipeViewModel
    {
        [Key]
        public int RecipeId { get; set; }

        [Relation(EntityType = typeof(ProductCategory), DataFieldValue = "CategoryId", DataFieldText = "Name")]
        [Display(Name = "Category")]
        public int? CategoryId { get; set; }

        [Required(ErrorMessage = "Please enter a name for the recipie!")]
        public string Name { get; set; }

        public string Description { get; set; }

        [ReadOnly(true)]
        public decimal? ProductionValuePerPortion { get; set; }

        [Range(0.01, int.MaxValue, ErrorMessage = "{0} must be positive!")]
        [Display(Name = "Sell Value Per Portion")]
        public decimal? SellValuePerPortion { get; set; }
                
        [ReadOnly(true)]
        [DisplayFormat(DataFormatString="{0:P2}")]
        public decimal? GrossProfit { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string ModifiedByUser { get; set; }

        public static RecipeViewModel ConvertFromRecipeEntity(Recipe entity,
            RecipeViewModel model)
        {

            model.RecipeId = entity.RecipeId;
            model.CategoryId = entity.CategoryId;
            model.Name = entity.Name;
            model.Description = entity.Description;
            model.ProductionValuePerPortion = entity.ProductionValuePerPortion;
            model.SellValuePerPortion = entity.SellValuePerPortion;
            model.GrossProfit = entity.GrossProfit;
            model.ModifiedDate = entity.ModifiedDate;
            model.ModifiedByUser = entity.ModifiedByUser;

            return model;
        }

        public static Recipe ConvertToRecipeEntity(RecipeViewModel model,
            Recipe entity)
        {
            entity.RecipeId = model.RecipeId;
            entity.CategoryId = model.CategoryId;
            entity.Name = model.Name;
            entity.Description = model.Description;
            entity.ProductionValuePerPortion = model.ProductionValuePerPortion;
            entity.SellValuePerPortion = model.SellValuePerPortion;
            entity.GrossProfit = model.GrossProfit;
            entity.ModifiedDate = model.ModifiedDate;
            entity.ModifiedByUser = model.ModifiedByUser;
            
            return entity;
        }
    }
}