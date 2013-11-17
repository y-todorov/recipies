using System;
using System.ComponentModel.DataAnnotations;
using RecipiesModelNS;
using InventoryManagementMVC.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;

namespace InventoryManagementMVC.Models
{
    public class RecipeIngredientViewModel
    {
        [Key]
        public int RecipeIngredientId { get; set; }

        [ReadOnly(true)]
        //[Relation(EntityType = typeof(Recipe), DataFieldValue = "RecipeId", DataFieldText = "Name")]
        [Display(Name = "Recipe")]
        [HiddenInput(DisplayValue = false)]
        public int? ParentRecipeId { get; set; }

        [Relation(EntityType = typeof(Recipe), DataFieldValue = "RecipeId", DataFieldText = "Name")]
        [Display(Name = "Recipe")]
        public int? IngredientRecipeId { get; set; }

        public double? QuantityPerPortion { get; set; }

        public decimal? Cost { get; set; }

        [ReadOnly(true)]
        public decimal? TotalValue { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string ModifiedByUser { get; set; }

        public static RecipeIngredientViewModel ConvertFromProductIngredientEntity(RecipeIngredient entity,
            RecipeIngredientViewModel model)
        {            
            model.RecipeIngredientId = entity.RecipeIngredientId;
            model.ParentRecipeId = entity.ParentRecipeId;
            model.IngredientRecipeId = entity.IngredientRecipeId;
            model.QuantityPerPortion = entity.QuantityPerPortion;
            model.Cost = entity.Cost;
            model.TotalValue = (decimal?)entity.TotalValue;

            model.ModifiedDate = entity.ModifiedDate;
            model.ModifiedByUser = entity.ModifiedByUser;

            

            return model;
        }

        public static RecipeIngredient ConvertToProductIngredientEntity(RecipeIngredientViewModel model,
            RecipeIngredient entity)
        {

            entity.RecipeIngredientId = model.RecipeIngredientId;
            entity.ParentRecipeId = model.ParentRecipeId;
            entity.IngredientRecipeId = model.IngredientRecipeId;
            entity.QuantityPerPortion = model.QuantityPerPortion;
            entity.Cost = model.Cost;
            entity.TotalValue = (double)model.TotalValue.GetValueOrDefault();

            entity.ModifiedDate = model.ModifiedDate;
            entity.ModifiedByUser = model.ModifiedByUser;

            
            return entity;
        }
    }
}