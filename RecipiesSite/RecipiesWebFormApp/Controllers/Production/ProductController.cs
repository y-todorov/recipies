using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using InventoryManagementMVC.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using RecipiesModelNS;
using RecipiesWebFormApp;
using System.Web;
using System.Web.Caching;
using RecipiesWebFormApp.Caching;
using DevTrends.MvcDonutCaching;

namespace InventoryManagementMVC.Controllers
{
    public class ProductController : ControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            List<Product> allProducts = ContextFactory.Current.Products.ToList();
            List<ProductViewModel> productViewModels =
                allProducts.Select(p => ProductViewModel.ConvertFromProductEntity(p, new ProductViewModel())).ToList();
            return Json(productViewModels.ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<ProductViewModel> products)
        {
            //List<ProductViewModel> results = new List<ProductViewModel>();

            if (products != null && ModelState.IsValid)
            {
                foreach (ProductViewModel productViewModel in products)
                {
                    Product newProduct = ProductViewModel.ConvertToProductEntity(productViewModel, new Product());
                    ContextFactory.Current.Products.Add(newProduct);
                    ContextFactory.Current.SaveChanges();

                    ProductViewModel.ConvertFromProductEntity(newProduct, productViewModel);
                    //results.Add(productViewModel);
                }
            }

            return Json(products.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<ProductViewModel> products)
        {
            if (products != null && ModelState.IsValid)
            {
                foreach (ProductViewModel productViewModel in products)
                {
                    Product product =
                        ContextFactory.Current.Products.FirstOrDefault(p => p.ProductId == productViewModel.ProductId);
                    ProductViewModel.ConvertToProductEntity(productViewModel, product);
                    ContextFactory.Current.SaveChanges();

                    ProductViewModel.ConvertFromProductEntity(product, productViewModel);
                }
            }

            return Json(products.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Destroy([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<ProductViewModel> products)
        {
            if (products.Any())
            {
                foreach (ProductViewModel productViewModel in products)
                {
                    Product product =
                        ContextFactory.Current.Products.FirstOrDefault(p => p.ProductId == productViewModel.ProductId);
                    ContextFactory.Current.Products.Remove(product);

                    ContextFactory.Current.SaveChanges();
                }
            }

            return Json(products.ToDataSourceResult(request, ModelState));
        }

        public ActionResult ReadProductRecipies(int? productId, [DataSourceRequest] DataSourceRequest request)
        {
            List<Recipe> allRecipes = ContextFactory.Current.Recipes//.ToList();
                .Where(r => r.ProductIngredients.Any(pi => pi.ProductId == productId)).ToList();

            //List<Recipe> newRecipies = new List<Recipe>();

            // This is too slow!!!

            //foreach (Recipe recipe in allRecipes)
            //{
            //    Dictionary<int, double> dic = new Dictionary<int, double>();
            //    Recipe.GetProductsWithQuantities(recipe.RecipeId, dic);
            //    if (dic.ContainsKey(productId.GetValueOrDefault()))
            //    {
            //        newRecipies.Add(recipe);
            //    }
            //}

            List<RecipeViewModel> recipeModels =
                allRecipes.Select(r => RecipeViewModel.ConvertFromRecipeEntity(r, new RecipeViewModel())).ToList();

            return Json(recipeModels.ToDataSourceResult(request));
                // This is important not to be just  Json(recipeModels) !!!!
        }

        public ActionResult ReadProductInventories(int? productId, [DataSourceRequest] DataSourceRequest request)
        {
            List<ProductInventory> productInventories =
                ContextFactory.Current.Inventories.OfType<ProductInventory>().Where
                    (pi => pi.ProductId == productId)
                    .OrderByDescending(de => de.ProductInventoryHeader.ForDate)
                    .ToList();
            List<ProductInventoryViewModel> productInventoriesModels = productInventories.Select
                (r => ProductInventoryViewModel.ConvertFromProductInventoryEntity(r, new ProductInventoryViewModel()))
                .ToList();

            return Json(productInventoriesModels.ToDataSourceResult(request));
        }

        public ActionResult ReadProductPurchaseOrders(int? productId, [DataSourceRequest] DataSourceRequest request)
        {
            List<PurchaseOrderDetail> productPurchaseOrders =
                ContextFactory.Current.PurchaseOrderDetails.Where
                    (pi => pi.ProductId == productId)
                    .OrderByDescending(de => de.PurchaseOrderHeader.OrderDate)
                    .ToList();
            List<PurchaseOrderDetailViewModel> productPurchaseOrdersModels = productPurchaseOrders.Select
                (r => PurchaseOrderDetailViewModel.ConvertFromPurchaseOrderDetailEntity(r, new PurchaseOrderDetailViewModel()))
                .ToList();

            return Json(productPurchaseOrdersModels.ToDataSourceResult(request));
        }

        
    }
}