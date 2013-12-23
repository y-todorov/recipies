using System;
using System.Collections.Generic;
using System.Linq;

namespace RecipiesModelNS
{
    public partial class PurchaseOrderHeader : YordanBaseEntity
    {
        //public bool UpdateProductsFromStatus(PurchaseOrderStatusEnum oldStatus, PurchaseOrderStatusEnum newStatus)
        //{
        //    bool isValidStatusTransition = false;
        //    int lastNumberOfDays = 14;
        //    List<PurchaseOrderDetail> purchaseOrderDetails =
        //        ContextFactory.GetContextPerRequest()
        //            .PurchaseOrderDetails.Where(po => po.PurchaseOrderId == PurchaseOrderId)
        //            .ToList();
        //    if (oldStatus == PurchaseOrderStatusEnum.Pending && newStatus == PurchaseOrderStatusEnum.Approved ||
        //        oldStatus == PurchaseOrderStatusEnum.Rejected && newStatus == PurchaseOrderStatusEnum.Approved)
        //    {
        //        isValidStatusTransition = true;
        //        foreach (PurchaseOrderDetail pod in purchaseOrderDetails)
        //        {
        //            pod.Product.UnitsOnOrder += pod.Product.GetBaseUnitMeasureQuantityForProduct(pod.OrderQuantity,
        //                pod.UnitMeasure);
        //        }
        //    }
        //    if (oldStatus == PurchaseOrderStatusEnum.Approved && newStatus == PurchaseOrderStatusEnum.Pending ||
        //        oldStatus == PurchaseOrderStatusEnum.Approved && newStatus == PurchaseOrderStatusEnum.Rejected)
        //    {
        //        isValidStatusTransition = true;
        //        foreach (PurchaseOrderDetail pod in purchaseOrderDetails)
        //        {
        //            pod.Product.UnitsOnOrder -= pod.Product.GetBaseUnitMeasureQuantityForProduct(pod.OrderQuantity,
        //                pod.UnitMeasure);
        //        }
        //    }
        //    if (oldStatus == PurchaseOrderStatusEnum.Approved && newStatus == PurchaseOrderStatusEnum.Completed)
        //    {
        //        isValidStatusTransition = true;
        //        foreach (PurchaseOrderDetail pod in purchaseOrderDetails)
        //        {
        //            pod.Product.UnitsInStock += pod.Product.GetBaseUnitMeasureQuantityForProduct(pod.StockedQuantity,
        //                pod.UnitMeasure);
        //            pod.Product.UnitsOnOrder -= pod.Product.GetBaseUnitMeasureQuantityForProduct(pod.OrderQuantity,
        //                pod.UnitMeasure);
        //        }
        //    }
        //    if (oldStatus == PurchaseOrderStatusEnum.Completed && newStatus == PurchaseOrderStatusEnum.Approved)
        //    {
        //        isValidStatusTransition = true;
        //        foreach (PurchaseOrderDetail pod in purchaseOrderDetails)
        //        {
        //            pod.Product.UnitsInStock -= pod.Product.GetBaseUnitMeasureQuantityForProduct(pod.StockedQuantity,
        //                pod.UnitMeasure);
        //            pod.Product.UnitsOnOrder += pod.Product.GetBaseUnitMeasureQuantityForProduct(pod.StockedQuantity,
        //                pod.UnitMeasure);
        //        }
        //    }

        //    if (isValidStatusTransition)
        //    {
        //        ContextFactory.GetContextPerRequest().SaveChanges();
        //    }
        //    List<PurchaseOrderDetail> pods =
        //        ContextFactory.GetContextPerRequest()
        //            .PurchaseOrderDetails.Where(pod => pod.PurchaseOrderId == PurchaseOrderId)
        //            .ToList();
        //    foreach (PurchaseOrderDetail pod in pods)
        //    {
        //        pod.Product.UnitPrice = (decimal) pod.Product.GetAveragePriceLastDays(14);
        //    }
        //    ContextFactory.GetContextPerRequest().SaveChanges();
        //    return isValidStatusTransition;
        //}

        //public bool UpdateProductsFromStatus(int? oldStatusId, int? newStatusId)
        //{
        //    if (oldStatusId.HasValue && newStatusId.HasValue)
        //    {
        //        PurchaseOrderStatusEnum oldStatus = (PurchaseOrderStatusEnum) oldStatusId.Value;
        //        PurchaseOrderStatusEnum newStatus = (PurchaseOrderStatusEnum) newStatusId.Value;

        //        bool isValidStatusTransition = UpdateProductsFromStatus(oldStatus, newStatus);
        //        return isValidStatusTransition;
        //    }
        //    return false;
        //}

        public static List<PurchaseOrderDetail> GetPurchaseOrderDetailsInPeriod(DateTime fromDate, DateTime toDate,
            PurchaseOrderStatusEnum status, List<ProductCategory> categoriesToExclude = null)
        {
            DateTime defaultDate = new DateTime(2000, 1, 1);
            DateTime endDateForLinq = toDate.Date.AddDays(1);
            List<PurchaseOrderHeader> purchaseOrderHeaders =
                ContextFactory.GetContextPerRequest().PurchaseOrderHeaders.Where(pof => pof.ShipDate >= fromDate.Date &&
                                                                                        pof.ShipDate < endDateForLinq &&
                                                                                        pof.StatusId == (int) status)
                    .ToList();

            List<PurchaseOrderDetail> pods = purchaseOrderHeaders.SelectMany(poh => poh.PurchaseOrderDetails.Where(pod => categoriesToExclude != null && !categoriesToExclude.Any(pc => (pod.Product != null && pc.CategoryId == pod.Product.CategoryId)))).ToList();
            return pods;
            //return purchaseOrderHeaders;
        }

        public static void UpdateProductsUnitsInStock(int? purchaseOrderHeaderId)
        {
            if (purchaseOrderHeaderId.HasValue)
            {
                PurchaseOrderHeader poh =
                    ContextFactory.GetContextPerRequest()
                        .PurchaseOrderHeaders.FirstOrDefault(p => p.PurchaseOrderId == purchaseOrderHeaderId);

                if (poh != null)
                {
                    foreach (PurchaseOrderDetail pod in poh.PurchaseOrderDetails)
                    {
                        Product.UpdateUnitsInStock(pod.ProductId);
                    }
                }
            }
        }

        public static void UpdatePurchaseOrderHeaderSubTotalFromPurchaseOrderDetails(int? purchaseOrderHeaderId)
        {
            if (purchaseOrderHeaderId.HasValue)
            {
                PurchaseOrderHeader poh =
                    ContextFactory.GetContextPerRequest()
                        .PurchaseOrderHeaders.FirstOrDefault(po => po.PurchaseOrderId == purchaseOrderHeaderId.Value);
                if (poh != null)
                {
                    decimal? subTotal = 0;
                    foreach (PurchaseOrderDetail pod in poh.PurchaseOrderDetails)
                    {
                        subTotal += (decimal?)pod.LineTotal;
                    }
                    poh.SubTotal = subTotal;
                    ContextFactory.GetContextPerRequest().SaveChanges();
                }
            }
        }

        public override void Added(System.Data.Entity.Infrastructure.DbEntityEntry e = null)
        {
            UpdateProductsUnitsInStock(PurchaseOrderId);
            base.Added(e);
        }

        public override void Changed(System.Data.Entity.Infrastructure.DbEntityEntry e = null)
        {
            UpdateProductsUnitsInStock(PurchaseOrderId);
            base.Changed(e);
        }

        private static int? _purchaseOrderId = 0;

        public override void Removing(System.Data.Entity.Infrastructure.DbEntityEntry e = null)
        {
            _purchaseOrderId = PurchaseOrderId;
            base.Removing(e);
        }

        public override void Removed(System.Data.Entity.Infrastructure.DbEntityEntry e = null)
        {
            UpdateProductsUnitsInStock(_purchaseOrderId);
            base.Removed(e);
        }
    }
}