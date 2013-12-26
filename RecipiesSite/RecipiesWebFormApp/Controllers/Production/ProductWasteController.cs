using InventoryManagementMVC.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using RecipiesModelNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagementMVC.Controllers
{
    public class ProductWasteController : ControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            List<ProductWasteViewModel> viewModels = ContextFactory.Current.Wastes.OfType<ProductWaste>().ToList().Select
                (c => ProductWasteViewModel.ConvertFromProductWasteEntity(c, new ProductWasteViewModel())).ToList();
            return Json(viewModels.ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<ProductWasteViewModel> wastes)
        {
            if (wastes != null && ModelState.IsValid)
            {
                foreach (ProductWasteViewModel waste in wastes)
                {
                    ProductWaste newWasteEntity = ProductWasteViewModel.ConvertToProductWasteEntity(waste,
                        new ProductWaste());
                    ContextFactory.Current.Wastes.Add(newWasteEntity);
                    ContextFactory.Current.SaveChanges();
                    newWasteEntity = ContextFactory.Current.Wastes.OfType<ProductWaste>().First(w => w.WasteId == newWasteEntity.WasteId);
                    ProductWasteViewModel.ConvertFromProductWasteEntity(newWasteEntity, waste);
                }
            }

            return Json(wastes.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<ProductWasteViewModel> wastes)
        {
            if (wastes != null && ModelState.IsValid)
            {
                foreach (ProductWasteViewModel waste in wastes)
                {
                    ProductWaste wasteEntity =
                        ContextFactory.Current.Wastes.OfType<ProductWaste>().FirstOrDefault(c => c.WasteId == waste.WasteId);

                    ProductWasteViewModel.ConvertToProductWasteEntity(waste, wasteEntity);

                    ContextFactory.Current.SaveChanges();

                    ProductWasteViewModel.ConvertFromProductWasteEntity(wasteEntity, waste);
                }
            }

            return Json(wastes.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Destroy([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<ProductWasteViewModel> wastes)
        {
            if (wastes.Any())
            {
                foreach (ProductWasteViewModel waste in wastes)
                {
                    ProductWaste wasteEntity = ContextFactory.Current.Wastes.OfType<ProductWaste>().FirstOrDefault(c => c.WasteId == waste.WasteId);
                    ContextFactory.Current.Wastes.Remove(wasteEntity);

                    ContextFactory.Current.SaveChanges();
                }
            }

            return Json(wastes.ToDataSourceResult(request, ModelState));
        }
    }
}