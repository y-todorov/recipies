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
                    pod.Product.UnitsOnOrder += pod.OrderQuantity;
                }
            }
            if (oldStatus == PurchaseOrderStatusEnum.Approved && newStatus == PurchaseOrderStatusEnum.Pending)
            {
                foreach (PurchaseOrderDetail pod in purchaseOrderDetails)
                {
                    pod.Product.UnitsOnOrder -= pod.OrderQuantity;
                }
            }
            if (oldStatus == PurchaseOrderStatusEnum.Approved && newStatus == PurchaseOrderStatusEnum.Completed)
            {
                foreach (PurchaseOrderDetail pod in purchaseOrderDetails)
                {
                    pod.Product.UnitsInStock += pod.StockedQuantity;
                    pod.Product.UnitsOnOrder -= pod.OrderQuantity;
                }
            }
            if (oldStatus == PurchaseOrderStatusEnum.Completed && newStatus == PurchaseOrderStatusEnum.Approved)
            {
                foreach (PurchaseOrderDetail pod in purchaseOrderDetails)
                {
                    pod.Product.UnitsInStock -= pod.StockedQuantity;
                    pod.Product.UnitsOnOrder += pod.OrderQuantity;
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
           
        

    }
}
