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

        public static List<PurchaseOrderHeader> GetPurchaseOrderHeadersInPeriod(DateTime fromDate, DateTime toDate,
            PurchaseOrderStatusEnum status)
        {
            DateTime defaultDate = new DateTime(2000, 1, 1);
            List<PurchaseOrderHeader> result =
                ContextFactory.GetContextPerRequest().PurchaseOrderHeaders.Where(pof => pof.ShipDate >= fromDate.Date &&
                                                                                        pof.ShipDate <= toDate.Date &&
                                                                                        pof.StatusId == (int) status)
                    .ToList();
            return result;
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