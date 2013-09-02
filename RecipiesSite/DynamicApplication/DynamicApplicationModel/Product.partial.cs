using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipiesModelNS
{
    public partial class Product
    {
        public double GetAveragePriceLastSevenDays()
        {
            RecipiesModel context = ContextFactory.GetContextPerRequest();

            List<PurchaseOrderDetail> pods = context.PurchaseOrderDetails.Where(pod => pod.PurchaseOrderHeader.ShipDate.HasValue &&
                pod.PurchaseOrderHeader.ShipDate.Value.Date > DateTime.Now.AddDays(-7).Date && pod.PurchaseOrderHeader.ShipDate.Value.Date <= DateTime.Now.Date &&
                pod.PurchaseOrderHeader.StatusId == (int)PurchaseOrderStatusEnum.Completed).ToList();

            decimal totalPrice = 0;
            double totalQuantity = 0;

            foreach (PurchaseOrderDetail pod in pods)
            {
                if (pod.UnitPrice.HasValue)
                {
                    totalPrice += pod.StockedQuantity * pod.UnitPrice.Value;
                }
                totalQuantity += pod.StockedQuantity;
            }

            double averagePrice = Math.Round((double)totalPrice / totalQuantity, 2);
            return averagePrice;                
        }
    }
}
