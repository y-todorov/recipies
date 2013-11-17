using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using InventoryManagementMVC.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using RecipiesModelNS;

namespace InventoryManagementMVC.Controllers
{
    public class RecipeIngredientController : ControllerBase
    {
        public ActionResult Read(int? recipeId, [DataSourceRequest] DataSourceRequest request)
        {
            List<RecipeIngredientViewModel> productIngredients = ContextFactory.Current.RecipeIngredients
                .Where(
                    pod => recipeId.HasValue ? pod.ParentRecipeId == recipeId.Value : true)
                .ToList().Select
                (c => RecipeIngredientViewModel.ConvertFromProductIngredientEntity(c, new RecipeIngredientViewModel())).ToList();
            return Json(productIngredients.ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(int? recipeId, [DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<RecipeIngredientViewModel> productIngredients)
        {
            if (productIngredients != null && ModelState.IsValid)
            {
                foreach (RecipeIngredientViewModel pi in productIngredients)
                {
                    RecipeIngredient newProductIngredient = RecipeIngredientViewModel.ConvertToProductIngredientEntity(pi,
                        new RecipeIngredient());
                    newProductIngredient.ParentRecipeId = recipeId;
                    ContextFactory.Current.RecipeIngredients.Add(newProductIngredient);
                    ContextFactory.Current.SaveChanges();
                    RecipeIngredientViewModel.ConvertFromProductIngredientEntity(newProductIngredient, pi);
                }
            }

            return Json(productIngredients.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<RecipeIngredientViewModel> productIngredients)
        {
            if (productIngredients != null && ModelState.IsValid)
            {
                foreach (RecipeIngredientViewModel productIngredient in productIngredients)
                {
                    RecipeIngredient piEntity =
                        ContextFactory.Current.RecipeIngredients.FirstOrDefault(
                            r => r.RecipeIngredientId == productIngredient.RecipeIngredientId);

                    RecipeIngredientViewModel.ConvertToProductIngredientEntity(productIngredient, piEntity);

                    ContextFactory.Current.SaveChanges();

                    RecipeIngredientViewModel.ConvertFromProductIngredientEntity(piEntity, productIngredient);
                }
            }

            return Json(productIngredients.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Destroy([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<RecipeIngredientViewModel> productIngredients)
        {
            if (productIngredients.Any())
            {
                foreach (RecipeIngredientViewModel pi in productIngredients)
                {
                    RecipeIngredient piEntity =
                        ContextFactory.Current.RecipeIngredients.FirstOrDefault(r => r.RecipeIngredientId  == pi.RecipeIngredientId);
                    ContextFactory.Current.RecipeIngredients.Remove(piEntity);

                    ContextFactory.Current.SaveChanges();
                }
            }

            return Json(productIngredients.ToDataSourceResult(request, ModelState));
        }
    }
}