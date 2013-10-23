using System;
using System.Collections.Generic;
using System.Linq;

namespace RecipiesModelNS
{
    public partial class SalesOrderHeader : YordanBaseEntity
    {
        public static List<SalesOrderHeader> GetSalesOrderHeadersInPeriod(DateTime fromDate, DateTime toDate,
            SalesOrderStatusEnum status)
        {
            DateTime defaultDate = new DateTime(2000, 1, 1);
            List<SalesOrderHeader> result =
                ContextFactory.GetContextPerRequest().SalesOrderHeaders.Where(pof => pof.ShippedDate >= fromDate.Date &&
                                                                                     pof.ShippedDate <= toDate.Date &&
                                                                                     pof.StatusId == (int)status)
                    .ToList();
            return result;
        }

        public override void Added(System.Data.Entity.Infrastructure.DbEntityEntry e = null)
        {
            List<Recipe> recipies = ContextFactory.GetContextPerRequest().Recipes.ToList();
            foreach (Recipe recipie in recipies)
            {
                SalesOrderDetail detail = new SalesOrderDetail()
                {
                    SalesOrderHeaderId = SalesOrderHeaderId,
                    RecipeId = recipie.RecipeId,
                    UnitPrice = 0,
                    UnitPriceDiscount = 0,
                    OrderQuantity = 0,
                };
                ContextFactory.GetContextPerRequest().SalesOrderDetails.Add(detail);
            }

            ContextFactory.GetContextPerRequest().SaveChanges();
            base.Added(e);
        }

        public override void Removed(System.Data.Entity.Infrastructure.DbEntityEntry e = null)
        {
           

            base.Removed(e);
            
            //SalesOrderHeader pod = this;
            //List<SalesOrderDetail> detailsToDelete =
            //    ContextFactory.GetContextPerRequest()
            //        .SalesOrderDetails.Where(d => !d.SalesOrderHeaderId.HasValue).ToList();
            //foreach (SalesOrderDetail salesOrderDetail in detailsToDelete)
            //{
            //    ContextFactory.GetContextPerRequest().SalesOrderDetails.Remove(salesOrderDetail);
            //}

            //ContextFactory.GetContextPerRequest().SaveChanges();
        }

        public override void Removing(System.Data.Entity.Infrastructure.DbEntityEntry e = null)
        { 
            
            base.Removing(e);
          
        }
    }
}