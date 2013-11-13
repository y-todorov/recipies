using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using InventoryManagementMVC.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using RecipiesModelNS;

namespace InventoryManagementMVC.Controllers
{
    public class RecipeController : ControllerBase
    {
        public ActionResult Index()
        {
            List<RecipeViewModel> recipeViewModels = ContextFactory.Current.Recipes.ToList().Select
                (c => RecipeViewModel.ConvertFromRecipeEntity(c, new RecipeViewModel())).ToList();
            return View(recipeViewModels);
        }

        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            List<RecipeViewModel> recipeViewModels = ContextFactory.Current.Recipes.ToList().Select
                (c => RecipeViewModel.ConvertFromRecipeEntity(c, new RecipeViewModel())).ToList();
            return Json(recipeViewModels.ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<RecipeViewModel> recipies)
        {
            if (recipies != null && ModelState.IsValid)
            {
                foreach (RecipeViewModel recipe in recipies)
                {
                    Recipe newRecipe = RecipeViewModel.ConvertToRecipeEntity(recipe,
                        new Recipe());
                    ContextFactory.Current.Recipes.Add(newRecipe);
                    ContextFactory.Current.SaveChanges();
                    RecipeViewModel.ConvertFromRecipeEntity(newRecipe, recipe);
                }
            }

            return Json(recipies.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<RecipeViewModel> recipies)
        {
            if (recipies != null && ModelState.IsValid)
            {
                foreach (RecipeViewModel recipe in recipies)
                {
                    Recipe recipeEntity =
                        ContextFactory.Current.Recipes.FirstOrDefault(
                            r => r.RecipeId == recipe.RecipeId);

                    RecipeViewModel.ConvertToRecipeEntity(recipe, recipeEntity);

                    ContextFactory.Current.SaveChanges();

                    RecipeViewModel.ConvertFromRecipeEntity(recipeEntity, recipe);
                }
            }

            return Json(recipies.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Destroy([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<RecipeViewModel> recipies)
        {
            if (recipies.Any())
            {
                foreach (RecipeViewModel recipe in recipies)
                {
                    Recipe recipeEntity =
                        ContextFactory.Current.Recipes.FirstOrDefault(r => r.RecipeId == recipe.RecipeId);
                    ContextFactory.Current.Recipes.Remove(recipeEntity);

                    ContextFactory.Current.SaveChanges();
                }
            }

            return Json(recipies.ToDataSourceResult(request, ModelState));
        }
    }
}