﻿using System;
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

        public static void AddDefaultRecipiesInSalesOrderHeader(int? salesOrderHeaderId)
        {
            if (salesOrderHeaderId.HasValue)
            {
                SalesOrderHeader salesOrderHeader = ContextFactory.GetContextPerRequest()
                    .SalesOrderHeaders.FirstOrDefault(soh => soh.SalesOrderHeaderId == salesOrderHeaderId);
                if (salesOrderHeader != null)
                {
                    List<Recipe> recipies = ContextFactory.GetContextPerRequest().Recipes.ToList();
                    foreach (Recipe recipie in recipies)
                    {
                        SalesOrderDetail detail = new SalesOrderDetail()
                        {
                            SalesOrderHeaderId = salesOrderHeader.SalesOrderHeaderId,
                            RecipeId = recipie.RecipeId,
                            UnitPrice = 0,
                            UnitPriceDiscount = 0,
                            OrderQuantity = 0,
                        };
                        ContextFactory.GetContextPerRequest().SalesOrderDetails.Add(detail);
                    }

                    ContextFactory.GetContextPerRequest().SaveChanges();
                }
            }
        }
           
        public static void UpdateProductsUnitsInStock(int? salesOrderHeaderId)
        {
            List<SalesOrderDetail> details =
                ContextFactory.GetContextPerRequest()
                    .SalesOrderDetails.Where(sod => sod.SalesOrderHeaderId == salesOrderHeaderId).ToList();
            foreach (SalesOrderDetail salesOrderDetail in details)
            {
                if (salesOrderDetail.Recipe != null)
                {
                    foreach (ProductIngredient recipeIngredient in salesOrderDetail.Recipe.ProductIngredients)
                    {
                        Product.UpdateUnitsInStock(recipeIngredient.ProductId);
                    }
                }
            }
        }

        public override void Added(System.Data.Entity.Infrastructure.DbEntityEntry e = null)
        { 
            AddDefaultRecipiesInSalesOrderHeader(SalesOrderHeaderId);
            UpdateProductsUnitsInStock(SalesOrderHeaderId);
           
            base.Added(e);
        }
     
        public override void Changed(System.Data.Entity.Infrastructure.DbEntityEntry e = null)
        {
            UpdateProductsUnitsInStock(SalesOrderHeaderId);
            base.Changed(e);
        }

        private static int? salesOrderHeaderId = 0;

        public override void Removing(System.Data.Entity.Infrastructure.DbEntityEntry e = null)
        {
            salesOrderHeaderId = SalesOrderHeaderId;
            base.Removing(e);
        }

        public override void Removed(System.Data.Entity.Infrastructure.DbEntityEntry e = null)
        {
            UpdateProductsUnitsInStock(salesOrderHeaderId);
            base.Removed(e);
        }
    }
}