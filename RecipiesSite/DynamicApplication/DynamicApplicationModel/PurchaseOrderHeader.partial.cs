using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipiesModelNS
{
    public partial class PurchaseOrderHeader
    {

        public void UpdateProductsFromStatus(PurchaseOrderStatusEnum oldStatus, PurchaseOrderStatusEnum newStatus)
        {
            List<PurchaseOrderDetail> purchaseOrderDetails = ContextFactory.GetContextPerRequest().PurchaseOrderDetails.Where(po => po.PurchaseOrderId == PurchaseOrderId).ToList();
            if (oldStatus == PurchaseOrderStatusEnum.Pending && newStatus == PurchaseOrderStatusEnum.Approved)
            {
                foreach (PurchaseOrderDetail pod in purchaseOrderDetails)
                {
                    pod.Product.UnitsOnOrder += GetBaseUnitMeasureQuantityForProduct(pod.Product, pod.OrderQuantity, pod.UnitMeasure);
                }
            }
            if (oldStatus == PurchaseOrderStatusEnum.Approved && newStatus == PurchaseOrderStatusEnum.Pending)
            {
                foreach (PurchaseOrderDetail pod in purchaseOrderDetails)
                {
                    pod.Product.UnitsOnOrder -=  GetBaseUnitMeasureQuantityForProduct(pod.Product, pod.OrderQuantity, pod.UnitMeasure);
                }
            }
            if (oldStatus == PurchaseOrderStatusEnum.Approved && newStatus == PurchaseOrderStatusEnum.Completed)
            {
                foreach (PurchaseOrderDetail pod in purchaseOrderDetails)
                {
                    pod.Product.UnitsInStock += GetBaseUnitMeasureQuantityForProduct(pod.Product, pod.StockedQuantity, pod.UnitMeasure);
                    pod.Product.UnitsOnOrder -= GetBaseUnitMeasureQuantityForProduct(pod.Product, pod.OrderQuantity, pod.UnitMeasure); 
                }
            }
            if (oldStatus == PurchaseOrderStatusEnum.Completed && newStatus == PurchaseOrderStatusEnum.Approved)
            {
                foreach (PurchaseOrderDetail pod in purchaseOrderDetails)
                {
                    pod.Product.UnitsInStock -= GetBaseUnitMeasureQuantityForProduct(pod.Product, pod.StockedQuantity, pod.UnitMeasure);
                    pod.Product.UnitsOnOrder += GetBaseUnitMeasureQuantityForProduct(pod.Product, pod.StockedQuantity, pod.UnitMeasure); 
                }
            }

            ContextFactory.GetContextPerRequest().SaveChanges();
        }

        public void UpdateProductsFromStatus(int? oldStatusId, int? newStatusId)
        {
            if (oldStatusId.HasValue && newStatusId.HasValue)
            {
                PurchaseOrderStatusEnum oldStatus = (PurchaseOrderStatusEnum)oldStatusId.Value;
                PurchaseOrderStatusEnum newStatus = (PurchaseOrderStatusEnum)newStatusId.Value;

                UpdateProductsFromStatus(oldStatus, newStatus);
            }
        }

        private int GetBaseUnitMeasureQuantityForProduct(Product product, int? quantity, UnitMeasure quantityUnitMeasure)
        {
            if (quantityUnitMeasure.BaseUnitId == product.UnitMeasureId)
            {
                UnitMeasure baseUnitMeasure = product.UnitMeasure;
                if (quantityUnitMeasure.BaseUnitFactor.HasValue)
                {
                    int result = quantity.Value * (int)quantityUnitMeasure.BaseUnitFactor.Value;
                    return result;// Math.Round(result, 2);
                }
                else
                {
                    throw new ApplicationException("UnitMeasure mismatch in method GetBaseUnitMeasureQuantityForProduct! quantityUnitMeasure does no have");
                }
            }
            else if (quantityUnitMeasure.UnitMeasureId == product.UnitMeasureId)
            {
                int result = quantity.Value;
                return result; //Math.Round(result, 2);
            }
            else
            {
                throw new ApplicationException("UnitMeasure mismatch in method GetBaseUnitMeasureQuantityForProduct!");
            }
        }



    }
}
