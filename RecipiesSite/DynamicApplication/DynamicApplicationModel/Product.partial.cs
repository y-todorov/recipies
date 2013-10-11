using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipiesModelNS
{
    public partial class Product : YordanBaseEntity
    {
        //public override void Added(RecipiesModel context, AddEventArgs e)
        //{
        //    base.Added(context, e);
        //}
        //[Required]
        //[DisplayName("Unit price")]
        //[DataType(DataType.Currency)]
        //[Range(0, int.MaxValue)]
        //public Nullable<decimal> UnitPrice { get; set; }

        public double GetAveragePriceLastDays(int lastDays)
        {
            //return 0;
            string help;
            return GetAveragePriceLastDays(lastDays, out help);
        }

        public double GetAveragePriceLastDays(int lastDays, out string text)
        {
            RecipiesEntities context = ContextFactory.GetContextPerRequest();

            DateTime fromDate = DateTime.Now.AddDays(-lastDays).Date;
            DateTime toDate = DateTime.Now.Date;
            List<PurchaseOrderDetail> pods = context.PurchaseOrderDetails.Where(pod => pod.ProductId == ProductId && pod.PurchaseOrderHeader.ShipDate.HasValue &&
                pod.PurchaseOrderHeader.ShipDate >= fromDate && pod.PurchaseOrderHeader.ShipDate <= toDate &&
                pod.PurchaseOrderHeader.StatusId == (int)PurchaseOrderStatusEnum.Completed).OrderByDescending(p => p.PurchaseOrderHeader.ShipDate).ToList();

            StringBuilder result = new StringBuilder();

            decimal totalPrice = 0;
            double totalQuantity = 0;
            double baseUnitsQuantity = 0;
            if (pods.Count > 0)
            {

                foreach (PurchaseOrderDetail pod in pods)
                {
                    double tempBaseUnitsQuantity = pod.Product.GetBaseUnitMeasureQuantityForProduct(pod.StockedQuantity, pod.UnitMeasure, pod);
                    baseUnitsQuantity += tempBaseUnitsQuantity;
                    totalPrice += (decimal)pod.StockedQuantity * pod.UnitPrice.GetValueOrDefault();
                    totalQuantity += pod.StockedQuantity;

                    string productMeasureName = string.Empty;
                    if (pod.Product != null && pod.Product.UnitMeasure != null)
                    {
                        productMeasureName = pod.Product.UnitMeasure.Name;
                    }
                    string podUnitMeasureName = string.Empty;
                    if (pod.UnitMeasure != null)
                    {
                        podUnitMeasureName = pod.UnitMeasure.Name;
                    }
                    result.AppendLine(string.Format("Purchase order id: {0}, ship date: {1:dd/MM/yyyy}, base unit quantity: {2} {5}, stocked quantity: {3} {6}, unitPrice of vendor measure: {4}",
                        pod.PurchaseOrderHeader.PurchaseOrderId, pod.PurchaseOrderHeader.ShipDate, tempBaseUnitsQuantity, pod.StockedQuantity, pod.UnitPrice, productMeasureName, podUnitMeasureName));
                }
            }
            else
            {
                DateTime nowDate = DateTime.Now.Date;
                PurchaseOrderDetail lastPod = context.PurchaseOrderDetails.Where(pod => pod.ProductId == ProductId &&
                pod.PurchaseOrderHeader.ShipDate <= nowDate &&
                pod.PurchaseOrderHeader.StatusId == (int)PurchaseOrderStatusEnum.Completed).OrderByDescending(pod => pod.PurchaseOrderHeader.ShipDate).FirstOrDefault();
                if (lastPod != null)
                {

                    double tempBaseUnitsQuantity = lastPod.Product.GetBaseUnitMeasureQuantityForProduct(lastPod.StockedQuantity, lastPod.UnitMeasure, lastPod);
                    baseUnitsQuantity += tempBaseUnitsQuantity;
                    totalPrice += (decimal)lastPod.StockedQuantity * lastPod.UnitPrice.GetValueOrDefault();

                    totalQuantity += lastPod.StockedQuantity;
                    string productMeasureName = string.Empty;
                    if (lastPod.Product != null && lastPod.Product.UnitMeasure != null)
                    {
                        productMeasureName = lastPod.Product.UnitMeasure.Name;
                    }
                    string podUnitMeasureName = string.Empty;
                    if (lastPod.UnitMeasure != null)
                    {
                        podUnitMeasureName = lastPod.UnitMeasure.Name;
                    }
                    result.AppendLine(string.Format("Purchase order id: {0}, ship date: {1:dd/MM/yyyy}, base unit quantity: {2} {5}, stocked quantity: {3} {6}, unitPrice of vendor measure: {4}",
                       lastPod.PurchaseOrderHeader.PurchaseOrderId, lastPod.PurchaseOrderHeader.ShipDate, tempBaseUnitsQuantity, lastPod.StockedQuantity, lastPod.UnitPrice,
                       productMeasureName, podUnitMeasureName));
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

        public Inventory GetLastInventoryForDate(DateTime forDate)
        {
            Inventory lastInventory = ContextFactory.GetContextPerRequest().Inventories.Where(inv => inv.ProductId == ProductId && inv.ForDate <= forDate.Date)
                .OrderByDescending(inv => inv.ForDate).FirstOrDefault();
            return lastInventory;
        }

        public double GetQuantityByDocumentsForDate(DateTime forDate)
        {
            Inventory inventory = GetLastInventoryForDate(forDate);

            if (inventory != null)
            {
                double purchases = GetPurchaseOrderStockedQuantity(inventory.ForDate.GetValueOrDefault(), forDate.Date);
                double sales = GetSalesOrderQuantity(inventory.ForDate.GetValueOrDefault(), forDate.Date);

                double quantityByDocuments = inventory.StocktakeQuantity.GetValueOrDefault() + purchases - sales;
                return quantityByDocuments;
            }
            return 0;
        }

        public double GetPurchaseOrderStockedQuantity(DateTime fromDate, DateTime toDate)
        {
            List<PurchaseOrderHeader> purchaseOrderHeaders = ContextFactory.GetContextPerRequest().PurchaseOrderHeaders.Where(pu => pu.ShipDate.HasValue &&
                pu.ShipDate >= fromDate.Date &&
                pu.ShipDate <= toDate.Date && pu.PurchaseOrderDetails.Any(pod => pod.ProductId == ProductId)).ToList();
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
                soh.ShippedDate >= fromDate.Date &&
                soh.ShippedDate <= toDate.Date &&
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

        public static void UpdateUnitPriceOfAllProducts()
        {
            List<Product> products = ContextFactory.GetContextPerRequest().Products.ToList();
            foreach (Product product in products)
            {
                decimal? averagePriceLastDays = (decimal?)product.GetAveragePriceLastDays(14);
                if (averagePriceLastDays != product.UnitPrice)
                {
                    product.UnitPrice = averagePriceLastDays;
                }
            }
            ContextFactory.GetContextPerRequest().SaveChanges();
        }

        public static void UpdateUnitsInStockOfAllProducts()
        {
            List<Product> allProducts = ContextFactory.GetContextPerRequest().Products.ToList();
            List<PurchaseOrderDetail> allCompletedPurchaseOrderDetals =
                ContextFactory.GetContextPerRequest()
                .PurchaseOrderDetails.Where(pod => pod.PurchaseOrderHeader.StatusId == (int)PurchaseOrderStatusEnum.Completed).ToList();

            foreach (Product product in allProducts)
            {
                double unitsInStock = 0;
                foreach (PurchaseOrderDetail pod in allCompletedPurchaseOrderDetals)
                {
                    if (pod.ProductId == product.ProductId)
                    {
                        unitsInStock += product.GetBaseUnitMeasureQuantityForProduct(pod.StockedQuantity, pod.UnitMeasure);
                    }
                }
                if (product.UnitsInStock != unitsInStock)
                {
                    product.UnitsInStock = unitsInStock;
                }
            }
            ContextFactory.GetContextPerRequest().SaveChanges();
        }

        public static void UpdateUnitsOnOrderOfAllProducts()
        {
            List<Product> allProducts = ContextFactory.GetContextPerRequest().Products.ToList();
            List<PurchaseOrderDetail> allCompletedPurchaseOrderDetals =
                ContextFactory.GetContextPerRequest()
                .PurchaseOrderDetails.Where(pod => pod.PurchaseOrderHeader.StatusId == (int)PurchaseOrderStatusEnum.Approved).ToList();

            foreach (Product product in allProducts)
            {
                double unitsOnOrderk = 0;
                foreach (PurchaseOrderDetail pod in allCompletedPurchaseOrderDetals)
                {
                    if (pod.ProductId == product.ProductId)
                    {
                        unitsOnOrderk += product.GetBaseUnitMeasureQuantityForProduct(pod.OrderQuantity, pod.UnitMeasure);
                    }
                }
                if (product.UnitsOnOrder != unitsOnOrderk)
                {
                    product.UnitsOnOrder = unitsOnOrderk;
                }
            }
            ContextFactory.GetContextPerRequest().SaveChanges();
        }
    }
}
