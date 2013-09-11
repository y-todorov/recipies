using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipiesModelNS
{
    public partial class Product
    {
        public double GetAveragePriceLastDays(int lastDays)
        {
            RecipiesModel context = ContextFactory.GetContextPerRequest();

            List<PurchaseOrderDetail> pods = context.PurchaseOrderDetails.Where(pod => pod.ProductId == ProductId && pod.PurchaseOrderHeader.ShipDate.HasValue &&
                pod.PurchaseOrderHeader.ShipDate.Value.Date > DateTime.Now.AddDays(lastDays).Date && pod.PurchaseOrderHeader.ShipDate.Value.Date <= DateTime.Now.Date &&
                pod.PurchaseOrderHeader.StatusId == (int)PurchaseOrderStatusEnum.Completed).ToList();

            decimal totalPrice = 0;
            double totalQuantity = 0;
            if (pods.Count > 0)
            {
                foreach (PurchaseOrderDetail pod in pods)
                {
                    if (pod.UnitPrice.HasValue)
                    {
                        totalPrice += pod.StockedQuantity * pod.UnitPrice.Value;
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
                        totalPrice += lastPod.StockedQuantity * lastPod.UnitPrice.Value;
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
    }
}
