using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public static void UpdateUnitsInStock(int? productId)
        {
            if (productId.HasValue)
            {
                Product product =
                    ContextFactory.GetContextPerRequest().Products.FirstOrDefault(p => p.ProductId == productId);
                if (product != null)
                {
                    double quantityByDocumentsForDate =  product.GetQuantityByDocumentsForDate(DateTime.Now);
                    if (product.UnitsInStock != quantityByDocumentsForDate)
                    {
                        product.UnitsInStock = quantityByDocumentsForDate;
                        ContextFactory.GetContextPerRequest().SaveChanges();
                    }                                        
                }
            }
        }

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
            List<PurchaseOrderDetail> pods =
                context.PurchaseOrderDetails.Where(
                    pod => pod.ProductId == ProductId && pod.PurchaseOrderHeader.ShipDate.HasValue &&
                        pod.OrderQuantity != 0 && // this is important
                           pod.PurchaseOrderHeader.ShipDate >= fromDate && pod.PurchaseOrderHeader.ShipDate <= toDate &&
                           pod.PurchaseOrderHeader.StatusId == (int) PurchaseOrderStatusEnum.Completed)
                    .OrderByDescending(p => p.PurchaseOrderHeader.ShipDate)
                    .ToList();


            StringBuilder result = new StringBuilder();

            decimal totalPrice = 0;
            double totalQuantity = 0;
            double baseUnitsQuantity = 0;
            if (pods.Count > 0)
            {
                foreach (PurchaseOrderDetail pod in pods)
                {
                    double tempBaseUnitsQuantity = pod.Product.GetBaseUnitMeasureQuantityForProduct(
                        pod.StockedQuantity, pod.UnitMeasure, pod);
                    baseUnitsQuantity += tempBaseUnitsQuantity;
                    totalPrice += (decimal) pod.StockedQuantity*pod.UnitPrice.GetValueOrDefault();
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
                    result.AppendLine(
                        string.Format(
                            "Purchase order id: {0}, ship date: {1:dd/MM/yyyy}, base unit quantity: {2} {5}, stocked quantity: {3} {6}, unitPrice of vendor measure: {4}",
                            pod.PurchaseOrderHeader.PurchaseOrderId, pod.PurchaseOrderHeader.ShipDate,
                            tempBaseUnitsQuantity, pod.StockedQuantity, pod.UnitPrice, productMeasureName,
                            podUnitMeasureName));
                }
            }
            else
            {
                DateTime nowDate = DateTime.Now.Date;
                PurchaseOrderDetail lastPod = context.PurchaseOrderDetails.Where(pod => pod.ProductId == ProductId &&
                    pod.OrderQuantity != 0 && // this is important
                                                                                        pod.PurchaseOrderHeader.ShipDate <=
                                                                                        nowDate &&
                                                                                        pod.PurchaseOrderHeader.StatusId ==
                                                                                        (int)
                                                                                            PurchaseOrderStatusEnum
                                                                                                .Completed)
                    .OrderByDescending(pod => pod.PurchaseOrderHeader.ShipDate)
                    .FirstOrDefault();
                if (lastPod != null)
                {
                    double tempBaseUnitsQuantity =
                        lastPod.Product.GetBaseUnitMeasureQuantityForProduct(lastPod.StockedQuantity,
                            lastPod.UnitMeasure, lastPod);
                    baseUnitsQuantity += tempBaseUnitsQuantity;
                    totalPrice += (decimal) lastPod.StockedQuantity*lastPod.UnitPrice.GetValueOrDefault();

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
                    result.AppendLine(
                        string.Format(
                            "Purchase order id: {0}, ship date: {1:dd/MM/yyyy}, base unit quantity: {2} {5}, stocked quantity: {3} {6}, unitPrice of vendor measure: {4}",
                            lastPod.PurchaseOrderHeader.PurchaseOrderId, lastPod.PurchaseOrderHeader.ShipDate,
                            tempBaseUnitsQuantity, lastPod.StockedQuantity, lastPod.UnitPrice,
                            productMeasureName, podUnitMeasureName));
                }
                else
                {
                    // няма никакви поръчки -> значи вземаме цената от последната инвентаризация;

                    ProductInventory productInventory =
                        ContextFactory.Current.Inventories.OfType<ProductInventory>()
                            .FirstOrDefault(pi => pi.ProductId == ProductId && pi.ForDate <= DateTime.Now);
                    if (productInventory != null)
                    {
                        totalPrice = productInventory.AverageUnitPrice.GetValueOrDefault();
                        totalQuantity = 1;
                        baseUnitsQuantity = 1;
                        result.AppendLine(
                            string.Format(
                                "Total price from last inventory : {0}",
                                totalPrice));
                    }
                }
            }

            double averagePrice = Math.Round((double) totalPrice/baseUnitsQuantity, 3);

            if (double.IsNaN(averagePrice) || double.IsInfinity(averagePrice))
            {
                averagePrice = 0;
            }
            result.AppendLine(
                string.Format(
                    "Total price: {0}, Total quantity in vendor units: {1}, Total quantity in product base units: {2}",
                    totalPrice, totalQuantity, baseUnitsQuantity));
            result.AppendLine(string.Format("averagePrice: {0}", averagePrice));

            text = result.ToString();
            return averagePrice;
        }

        public ProductInventory GetLastInventoryForDate(DateTime forDate)
        {
            ProductInventory lastInventory =
                ContextFactory.GetContextPerRequest()
                    .Inventories.OfType<ProductInventory>()
                    .Where(inv => inv.ProductId == ProductId && inv.ProductInventoryHeader.ForDate <= forDate.Date)
                    .OrderByDescending(inv => inv.ForDate).FirstOrDefault();
            return lastInventory;
        }

        public double GetQuantityByDocumentsForDate(DateTime forDate)
        {
            Inventory inventory = GetLastInventoryForDate(forDate);

            double purchases = 0;
            double sales = 0;

            double quantityByDocuments = 0;


            if (inventory != null)
            {
                purchases = GetPurchaseOrderStockedQuantity(inventory.ForDate.GetValueOrDefault(), forDate.Date);
                sales = GetSalesOrderQuantity(inventory.ForDate.GetValueOrDefault(), forDate.Date);

                quantityByDocuments = inventory.StocktakeQuantity.GetValueOrDefault() + purchases - sales;
            }
            else
            {
                purchases = GetPurchaseOrderStockedQuantity(DateTime.Now.AddYears(-100), forDate.Date);
                sales = GetSalesOrderQuantity(DateTime.Now.AddYears(-100), forDate.Date);

                quantityByDocuments = purchases - sales;
            }
            return quantityByDocuments;
        }

        public double GetPurchaseOrderStockedQuantity(DateTime fromDate, DateTime toDate)
        {
            List<PurchaseOrderHeader> purchaseOrderHeaders =
                ContextFactory.GetContextPerRequest().PurchaseOrderHeaders.Where(pu => pu.ShipDate.HasValue &&
                                                                                       pu.ShipDate >= fromDate.Date &&
                                                                                       pu.ShipDate <= toDate.Date &&
                                                                                       pu.StatusId ==
                                                                                       (int)
                                                                                           PurchaseOrderStatusEnum
                                                                                               .Completed &&
                                                                                       pu.PurchaseOrderDetails.Any(
                                                                                           pod =>
                                                                                               pod.ProductId ==
                                                                                               ProductId)).ToList();
            double stockedQuantityForPeriod = 0;
            foreach (PurchaseOrderHeader poh in purchaseOrderHeaders)
            {
                foreach (PurchaseOrderDetail pod in poh.PurchaseOrderDetails)
                {
                    if (pod.ProductId == ProductId)
                    {
                        stockedQuantityForPeriod += this.GetBaseUnitMeasureQuantityForProduct(pod.StockedQuantity,
                            pod.UnitMeasure);
                    }
                }
            }
            return stockedQuantityForPeriod;
        }

        public double GetSalesOrderQuantity(DateTime fromDate, DateTime toDate)
        {
            List<SalesOrderHeader> salesOrderHeaders =
                ContextFactory.GetContextPerRequest().SalesOrderHeaders.Where(soh => soh.ShippedDate.HasValue &&
                                                                                     soh.ShippedDate >= fromDate.Date &&
                                                                                     soh.ShippedDate <= toDate.Date &&
                                                                                     soh.SalesOrderDetails.Any(
                                                                                         sod =>
                                                                                             sod.Recipe
                                                                                                 .ProductIngredients.Any(
                                                                                                     ri =>
                                                                                                         ri.ProductId ==
                                                                                                         ProductId)))
                    .ToList();

            return 0;
        }

        public double GetBaseUnitMeasureQuantityForProduct(double? quantity, UnitMeasure quantityUnitMeasure,
            PurchaseOrderDetail pod = null)
        {
            if (quantityUnitMeasure.BaseUnitId == this.UnitMeasureId)
            {
                UnitMeasure baseUnitMeasure = this.UnitMeasure;
                if (quantityUnitMeasure.BaseUnitFactor.HasValue)
                {
                    double result = quantity.Value*quantityUnitMeasure.BaseUnitFactor.Value;
                    return Math.Round(result, 3);
                }
                else
                {
                    throw new ApplicationException(
                        "UnitMeasure mismatch in method GetBaseUnitMeasureQuantityForProduct! quantityUnitMeasure does no have");
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
                    podMessage = string.Format("Purchase Order Id: {0}, purchase order detail Id: {1}",
                        pod.PurchaseOrderId, pod.PurchaseOrderDetailId);
                }
                throw new ApplicationException(
                    string.Format(
                        "UnitMeasure mismatch in method GetBaseUnitMeasureQuantityForProduct! Product id: {0}, Product name: {4}, Product unit measure: {1}, Quantity unit measure: {2}, More info: {3}",
                        ProductId, UnitMeasure.Name, quantityUnitMeasure.Name, podMessage, Name));
            }
        }

        public static void UpdateUnitPriceOfAllProducts()
        {
            List<Product> products = ContextFactory.GetContextPerRequest().Products.ToList();
            foreach (Product product in products)
            {
                //if (product.ProductId == 6983)
                {
                    decimal? averagePriceLastDays = (decimal?)product.GetAveragePriceLastDays(14);
                    if (averagePriceLastDays != product.UnitPrice)
                    {
                        product.UnitPrice = averagePriceLastDays;
                    }
                }
            }
            ContextFactory.GetContextPerRequest().SaveChanges();
        }

        public static void UpdateUnitsInStockOfAllProducts()
        {
            List<Product> allProducts = ContextFactory.GetContextPerRequest().Products.ToList();
            List<PurchaseOrderDetail> allCompletedPurchaseOrderDetals =
                ContextFactory.GetContextPerRequest()
                    .PurchaseOrderDetails.Where(
                        pod => pod.PurchaseOrderHeader.StatusId == (int) PurchaseOrderStatusEnum.Completed).ToList();

            foreach (Product product in allProducts)
            {
                ProductInventory pi = product.GetLastInventoryForDate(DateTime.Now);
                //if (product.ProductId == 23)
                {
                    double unitsInStock = 0;
                    if (pi != null)
                    {
                        unitsInStock = pi.StocktakeQuantity.GetValueOrDefault();
                    }
                    foreach (PurchaseOrderDetail pod in allCompletedPurchaseOrderDetals)
                    {
                        if (pod.ProductId == product.ProductId)
                        {

                            if (pi != null)
                            {
                                if (pod.PurchaseOrderHeader.ShipDate.GetValueOrDefault().Date >= pi.ForDate.GetValueOrDefault().Date)
                                {
                                    unitsInStock += product.GetBaseUnitMeasureQuantityForProduct(pod.StockedQuantity,
                                        pod.UnitMeasure);
                                }
                            }
                            else
                            {
                                unitsInStock += product.GetBaseUnitMeasureQuantityForProduct(pod.StockedQuantity,
                                       pod.UnitMeasure);
                            }
                        }
                    }
                    if (product.UnitsInStock != unitsInStock)
                    {
                        product.UnitsInStock = unitsInStock;
                    }
                }
            }
            ContextFactory.GetContextPerRequest().SaveChanges();
        }

        public static void UpdateUnitsOnOrderOfAllProducts()
        {
            List<Product> allProducts = ContextFactory.GetContextPerRequest().Products.ToList();
            List<PurchaseOrderDetail> allCompletedPurchaseOrderDetals =
                ContextFactory.GetContextPerRequest()
                    .PurchaseOrderDetails.Where(
                        pod => pod.PurchaseOrderHeader.StatusId == (int) PurchaseOrderStatusEnum.Approved).ToList();

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