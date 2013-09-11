using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipiesModelNS
{
    public partial class SalesOrderHeader
    {
        public void UpdateProductsFromStatus(SalesOrderStatusEnum oldStatus, SalesOrderStatusEnum newStatus)
        {
            List<SalesOrderDetail> salesOrderDetails = ContextFactory.GetContextPerRequest().SalesOrderDetails.Where(po => po.SalesOrderId == OrderID).ToList();
            if (oldStatus == SalesOrderStatusEnum.Approved && newStatus == SalesOrderStatusEnum.Canceled)
            {
                foreach (SalesOrderDetail sod in salesOrderDetails)
                {
                    foreach (RecipeIngredient ri in sod.Recipe.RecipeIngredients)
                    {
                        if (ri.QuantityPerPortion.HasValue)
                        {
                            ri.Product.UnitsInStock -= (int)ri.QuantityPerPortion.Value; // TEST MUST BE FIXED
                        }
                    }
                }
            }
            if (oldStatus == SalesOrderStatusEnum.Canceled && newStatus == SalesOrderStatusEnum.Approved)
            {
                foreach (SalesOrderDetail sod in salesOrderDetails)
                {
                    foreach (RecipeIngredient ri in sod.Recipe.RecipeIngredients)
                    {
                        if (ri.QuantityPerPortion.HasValue)
                        {
                            ri.Product.UnitsInStock += (int)ri.QuantityPerPortion.Value; // TEST MUST BE FIXED
                        }
                    }
                }
            }           

            ContextFactory.GetContextPerRequest().SaveChanges();
        }

        public void UpdateProductsFromStatus(int? oldStatusId, int? newStatusId)
        {
            if (oldStatusId.HasValue && newStatusId.HasValue)
            {
                SalesOrderStatusEnum oldStatus = (SalesOrderStatusEnum)oldStatusId.Value;
                SalesOrderStatusEnum newStatus = (SalesOrderStatusEnum)newStatusId.Value;

                UpdateProductsFromStatus(oldStatus, newStatus);
            }
        }
    }
}
