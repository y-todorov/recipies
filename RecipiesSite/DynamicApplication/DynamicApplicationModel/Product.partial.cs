using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.OpenAccess;

namespace RecipiesModelNS
{
    public partial class Product : YordanBaseEntity
    {
        public override void Added(RecipiesModel context, AddEventArgs e)
        {
            base.Added(context, e);
        }

        public double GetAveragePriceLastDays(int lastDays)
        {
            RecipiesModel context = ContextFactory.GetContextPerRequest();

            List<PurchaseOrderDetail> pods = context.PurchaseOrderDetails.Where(pod => pod.ProductId == ProductId && pod.PurchaseOrderHeader.ShipDate.HasValue &&
                pod.PurchaseOrderHeader.ShipDate.Value.Date > DateTime.Now.AddDays(-lastDays).Date && pod.PurchaseOrderHeader.ShipDate.Value.Date <= DateTime.Now.Date &&
                pod.PurchaseOrderHeader.StatusId == (int)PurchaseOrderStatusEnum.Completed).ToList();

            decimal totalPrice = 0;
            double totalQuantity = 0;
            if (pods.Count > 0)
            {
                foreach (PurchaseOrderDetail pod in pods)
                {
                    if (pod.UnitPrice.HasValue)
                    {
                        totalPrice += (decimal)pod.StockedQuantity * pod.UnitPrice.Value;
                    }
                    totalQuantity += pod.StockedQuantity;
                }
            }
            else
            {
                PurchaseOrderDetail lastPod = context.PurchaseOrderDetails.Where(pod => pod.ProductId == ProductId && pod.PurchaseOrderHeader.ShipDate.HasValue &&
                pod.PurchaseOrderHeader.ShipDate.Value.Date <= DateTime.Now.Date &&
                pod.PurchaseOrderHeader.StatusId == (int)PurchaseOrderStatusEnum.Completed).OrderByDescending(pod => pod.PurchaseOrderHeader.ShipDate).FirstOrDefault();
                if (lastPod != null)
                {
                    if (lastPod.UnitPrice.HasValue)
                    {
                        totalPrice += (decimal)lastPod.StockedQuantity * lastPod.UnitPrice.Value;
                    }
                    totalQuantity += lastPod.StockedQuantity;
                }
            }

            double averagePrice = Math.Round((double)totalPrice / totalQuantity, 2);
            if (double.IsNaN(averagePrice) || double.IsInfinity(averagePrice))
            {
                averagePrice = 0;
            }
            return averagePrice;
        }

        public Inventory GetLastInventory()
        {
            Inventory lastInventory = ContextFactory.GetContextPerRequest().Inventories.Where(inv => inv.ProductId == ProductId).OrderByDescending(inv => inv.ForDate).FirstOrDefault();
            return lastInventory;
        }

        public double GetQuantityByDocuments()
        {
            Inventory inventory = GetLastInventory();

            // test
            double result = GetPurchaseOrderStockedQuantity(DateTime.Now.AddYears(-1), DateTime.Now.AddYears(1));
            double result2 = GetSalesOrderQuantity(DateTime.Now.AddYears(-1), DateTime.Now.AddYears(1));
            


            if (inventory != null)
            {
                if (inventory.StocktakeQuantity.HasValue)
                {

                }
            }
            return 0;
        }

        public double GetPurchaseOrderStockedQuantity(DateTime fromDate, DateTime toDate)
        {
            List<PurchaseOrderHeader> purchaseOrderHeaders = ContextFactory.GetContextPerRequest().PurchaseOrderHeaders.Where(pu => pu.ShipDate.HasValue &&
                pu.ShipDate.Value.Date >= fromDate.Date &&
                pu.ShipDate.Value.Date <= toDate.Date && pu.PurchaseOrderDetails.Any(pod => pod.ProductId == ProductId)).ToList();
            double stockedQuantityForPeriod = 0;
            foreach (PurchaseOrderHeader poh in purchaseOrderHeaders)
            {
                foreach (PurchaseOrderDetail pod in poh.PurchaseOrderDetails)
                {
                    stockedQuantityForPeriod += pod.StockedQuantity;
                }
            }
            return stockedQuantityForPeriod;
        }

        public double GetSalesOrderQuantity(DateTime fromDate, DateTime toDate)
        {
            List<SalesOrderHeader> salesOrderHeaders = ContextFactory.GetContextPerRequest().SalesOrderHeaders.Where(soh => soh.ShippedDate.HasValue &&
                soh.ShippedDate.Value.Date >= fromDate.Date &&
                soh.ShippedDate.Value.Date <= toDate.Date &&
                soh.SalesOrderDetails.Any(sod => sod.Recipe.RecipeIngredients.Any(ri => ri.ProductId == ProductId))).ToList();

            return 0;
        }

        public double GetBaseUnitMeasureQuantityForProduct(double? quantity, UnitMeasure quantityUnitMeasure)
        {
            if (quantityUnitMeasure.BaseUnitId == this.UnitMeasureId)
            {
                UnitMeasure baseUnitMeasure = this.UnitMeasure;
                if (quantityUnitMeasure.BaseUnitFactor.HasValue)
                {
                    double result = quantity.Value * quantityUnitMeasure.BaseUnitFactor.Value;
                    return Math.Round(result, 3);
                }
                else
                {
                    throw new ApplicationException("UnitMeasure mismatch in method GetBaseUnitMeasureQuantityForProduct! quantityUnitMeasure does no have");
                }
            }
            else if (quantityUnitMeasure.UnitMeasureId == this.UnitMeasureId)
            {
                double result = quantity.Value;
                return Math.Round(result, 3);
            }
            else
            {
                throw new ApplicationException("UnitMeasure mismatch in method GetBaseUnitMeasureQuantityForProduct!");
            }
        }
    }
}
