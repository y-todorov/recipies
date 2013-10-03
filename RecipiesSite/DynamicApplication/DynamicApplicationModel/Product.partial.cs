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
            string help;
            return GetAveragePriceLastDays(lastDays, out help);
        }

        public double GetAveragePriceLastDays(int lastDays, out string text)
        {
            RecipiesModel context = ContextFactory.GetContextPerRequest();

            List<PurchaseOrderDetail> pods = context.PurchaseOrderDetails.Where(pod => pod.ProductId == ProductId && pod.PurchaseOrderHeader.ShipDate.HasValue &&
                pod.PurchaseOrderHeader.ShipDate.Value.Date > DateTime.Now.AddDays(-lastDays).Date && pod.PurchaseOrderHeader.ShipDate.Value.Date <= DateTime.Now.Date &&
                pod.PurchaseOrderHeader.StatusId == (int)PurchaseOrderStatusEnum.Completed).OrderByDescending(p => p.PurchaseOrderHeader.ShipDate).ToList();

            StringBuilder result = new StringBuilder();

            decimal totalPrice = 0;
            double totalQuantity = 0;
            double baseUnitsQuantity = 0;
            if (pods.Count > 0)
            {

                foreach (PurchaseOrderDetail pod in pods)
                {
                    double  tempBaseUnitsQuantity = pod.Product.GetBaseUnitMeasureQuantityForProduct(pod.StockedQuantity, pod.UnitMeasure, pod);
                    baseUnitsQuantity += tempBaseUnitsQuantity;
                    totalPrice += (decimal)pod.StockedQuantity * pod.UnitPrice.GetValueOrDefault();
                    totalQuantity += pod.StockedQuantity;

                    result.AppendLine(string.Format("Purchase order id: {0}, ship date: {1:dd/MM/yyyy}, base unit quantity: {2}, stocked quantity: {3}, unitPrice of vendor measure: {4}",
                        pod.PurchaseOrderHeader.PurchaseOrderId, pod.PurchaseOrderHeader.ShipDate, tempBaseUnitsQuantity, pod.StockedQuantity, pod.UnitPrice));
                }
            }
            else
            {
                PurchaseOrderDetail lastPod = context.PurchaseOrderDetails.Where(pod => pod.ProductId == ProductId && pod.PurchaseOrderHeader.ShipDate.HasValue &&
                pod.PurchaseOrderHeader.ShipDate.Value.Date <= DateTime.Now.Date &&
                pod.PurchaseOrderHeader.StatusId == (int)PurchaseOrderStatusEnum.Completed).OrderByDescending(pod => pod.PurchaseOrderHeader.ShipDate).FirstOrDefault();
                if (lastPod != null)
                {
                    
                        double tempBaseUnitsQuantity = lastPod.Product.GetBaseUnitMeasureQuantityForProduct(lastPod.StockedQuantity, lastPod.UnitMeasure, lastPod);
                        baseUnitsQuantity += tempBaseUnitsQuantity;
                        totalPrice += (decimal)lastPod.StockedQuantity * lastPod.UnitPrice.GetValueOrDefault();
                    
                    totalQuantity += lastPod.StockedQuantity;
                    result.AppendLine(string.Format("Purchase order id: {0}, ship date: {1:dd/MM/yyyy}, base unit quantity: {2}, stocked quantity: {3}, unitPrice of vendor measure: {4}",
                       lastPod.PurchaseOrderHeader.PurchaseOrderId, lastPod.PurchaseOrderHeader.ShipDate, tempBaseUnitsQuantity, lastPod.StockedQuantity, lastPod.UnitPrice));
                }
            }
          
            double averagePrice = Math.Round((double)totalPrice / baseUnitsQuantity, 3);

            if (double.IsNaN(averagePrice) || double.IsInfinity(averagePrice))
            {
                averagePrice = 0;
            }
            result.AppendLine(string.Format("Total price: {0}, Total quantity in vendor units: {1}, Total quantity in product base units: {2}", totalPrice, totalQuantity, baseUnitsQuantity));
            result.AppendLine(string.Format("averagePrice: {0}", averagePrice));

            text = result.ToString();
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

        public double GetBaseUnitMeasureQuantityForProduct(double? quantity, UnitMeasure quantityUnitMeasure, PurchaseOrderDetail pod = null)
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
                string podMessage = "None.";
                if (pod != null)
                {
                    podMessage = string.Format("Purchase Order Id: {0}, purchase order detail Id: {1}", pod.PurchaseOrderId, pod.PurchaseOrderDetailId);
                }
                throw new ApplicationException(string.Format("UnitMeasure mismatch in method GetBaseUnitMeasureQuantityForProduct! Product id: {0}, product unit measure: {1}, quantity unit measure: {2}, More info: {3}",
                    ProductId, UnitMeasure.Name, quantityUnitMeasure.Name, podMessage));
            }
        }
    }
}
