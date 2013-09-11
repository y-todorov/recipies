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
                            ri.Product.UnitsInStock += (int)ri.QuantityPerPortion.Value; // TEST MUST BE FIXED NO CASTING
                        }
                    }
                }
            }
            if (oldStatus == SalesOrderStatusEnum.Canceled && newStatus == SalesOrderStatusEnum.Approved &&
                oldStatus == SalesOrderStatusEnum.None || newStatus == SalesOrderStatusEnum.Approved)
            {
                foreach (SalesOrderDetail sod in salesOrderDetails)
                {
                    foreach (RecipeIngredient ri in sod.Recipe.RecipeIngredients)
                    {
                        if (ri.QuantityPerPortion.HasValue)
                        {
                            ri.Product.UnitsInStock -= (int)ri.QuantityPerPortion.Value; // TEST MUST BE FIXED NO CASTING
                        }
                    }
                }
            }           

            ContextFactory.GetContextPerRequest().SaveChanges();
        }

        public void UpdateProductsFromStatus(int? oldStatusId, int? newStatusId)
        {
            SalesOrderStatusEnum oldStatus = SalesOrderStatusEnum.None;
            SalesOrderStatusEnum newStatus = SalesOrderStatusEnum.None;
           
            if (newStatusId.HasValue)
            {
                newStatus = (SalesOrderStatusEnum)newStatusId.Value;               
            } 
            if (oldStatusId.HasValue)
            {
                oldStatus = (SalesOrderStatusEnum)oldStatusId.Value;               
            }
            UpdateProductsFromStatus(oldStatus, newStatus);

        }
    }
}
