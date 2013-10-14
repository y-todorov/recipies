﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace RecipiesModelNS
{
    public partial class PurchaseOrderHeader : YordanBaseEntity
    {
        //public override void Removed(RecipiesModel context, RemoveEventArgs e)
        //{
        //    base.Removed(context, e);
        //}

        public bool UpdateProductsFromStatus(PurchaseOrderStatusEnum oldStatus, PurchaseOrderStatusEnum newStatus)
        {
            bool isValidStatusTransition = false;
            int lastNumberOfDays = 14;
            List<PurchaseOrderDetail> purchaseOrderDetails =
                ContextFactory.GetContextPerRequest()
                    .PurchaseOrderDetails.Where(po => po.PurchaseOrderId == PurchaseOrderId)
                    .ToList();
            if (oldStatus == PurchaseOrderStatusEnum.Pending && newStatus == PurchaseOrderStatusEnum.Approved ||
                oldStatus == PurchaseOrderStatusEnum.Rejected && newStatus == PurchaseOrderStatusEnum.Approved)
            {
                isValidStatusTransition = true;
                foreach (PurchaseOrderDetail pod in purchaseOrderDetails)
                {
                    pod.Product.UnitsOnOrder += pod.Product.GetBaseUnitMeasureQuantityForProduct(pod.OrderQuantity,
                        pod.UnitMeasure);
                }
            }
            if (oldStatus == PurchaseOrderStatusEnum.Approved && newStatus == PurchaseOrderStatusEnum.Pending ||
                oldStatus == PurchaseOrderStatusEnum.Approved && newStatus == PurchaseOrderStatusEnum.Rejected)
            {
                isValidStatusTransition = true;
                foreach (PurchaseOrderDetail pod in purchaseOrderDetails)
                {
                    pod.Product.UnitsOnOrder -= pod.Product.GetBaseUnitMeasureQuantityForProduct(pod.OrderQuantity,
                        pod.UnitMeasure);
                }
            }
            if (oldStatus == PurchaseOrderStatusEnum.Approved && newStatus == PurchaseOrderStatusEnum.Completed)
            {
                isValidStatusTransition = true;
                foreach (PurchaseOrderDetail pod in purchaseOrderDetails)
                {
                    pod.Product.UnitsInStock += pod.Product.GetBaseUnitMeasureQuantityForProduct(pod.StockedQuantity,
                        pod.UnitMeasure);
                    pod.Product.UnitsOnOrder -= pod.Product.GetBaseUnitMeasureQuantityForProduct(pod.OrderQuantity,
                        pod.UnitMeasure);
                }
            }
            if (oldStatus == PurchaseOrderStatusEnum.Completed && newStatus == PurchaseOrderStatusEnum.Approved)
            {
                isValidStatusTransition = true;
                foreach (PurchaseOrderDetail pod in purchaseOrderDetails)
                {
                    pod.Product.UnitsInStock -= pod.Product.GetBaseUnitMeasureQuantityForProduct(pod.StockedQuantity,
                        pod.UnitMeasure);
                    pod.Product.UnitsOnOrder += pod.Product.GetBaseUnitMeasureQuantityForProduct(pod.StockedQuantity,
                        pod.UnitMeasure);
                }
            }

            if (isValidStatusTransition)
            {
                ContextFactory.GetContextPerRequest().SaveChanges();
            }
            List<PurchaseOrderDetail> pods =
                ContextFactory.GetContextPerRequest()
                    .PurchaseOrderDetails.Where(pod => pod.PurchaseOrderId == PurchaseOrderId)
                    .ToList();
            foreach (PurchaseOrderDetail pod in pods)
            {
                pod.Product.UnitPrice = (decimal) pod.Product.GetAveragePriceLastDays(14);
            }
            ContextFactory.GetContextPerRequest().SaveChanges();
            return isValidStatusTransition;
        }

        public bool UpdateProductsFromStatus(int? oldStatusId, int? newStatusId)
        {
            if (oldStatusId.HasValue && newStatusId.HasValue)
            {
                PurchaseOrderStatusEnum oldStatus = (PurchaseOrderStatusEnum) oldStatusId.Value;
                PurchaseOrderStatusEnum newStatus = (PurchaseOrderStatusEnum) newStatusId.Value;

                bool isValidStatusTransition = UpdateProductsFromStatus(oldStatus, newStatus);
                return isValidStatusTransition;
            }
            return false;
        }

        //public override void OaldsUpdating(DbEntityEntry e)
        //{
        //PurchaseOrderHeader oldPurchaseOrderHeader = e.OriginalObject as PurchaseOrderHeader;
        //PurchaseOrderHeader newPurchaseOrderHeader = e.NewObject as PurchaseOrderHeader;


        //bool isValidStatusTransition = newPurchaseOrderHeader.UpdateProductsFromStatus(oldPurchaseOrderHeader.StatusId, newPurchaseOrderHeader.StatusId);

        //base.OaldsUpdating(e);
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
    }
}