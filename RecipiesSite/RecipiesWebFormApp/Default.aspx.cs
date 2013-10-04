﻿using RecipiesModelNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.OpenAccess;

namespace RecipiesWebFormApp
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string s = string.Empty;
                rhcLast10ModifiedProducts.DataSource = ContextFactory.GetContextPerRequest().Products.OrderByDescending(pr => pr.ModifiedDate).Take(10);

                rhcProductsCountByCategory.DataSource = ContextFactory.GetContextPerRequest().ProductCategories.Select(cat => new { CategoryName = cat.Name, ProductCount = cat.Products.Count }).OrderByDescending(res => res.ProductCount);

                rhcProductsForReorder.DataSource = ContextFactory.GetContextPerRequest().Products.Where(product => product.UnitsInStock <= product.ReorderLevel).OrderByDescending(product => product.ReorderLevel).Take(10);

                rhcMostExpensiveProducts.DataSource = ContextFactory.GetContextPerRequest().Products.OrderByDescending(product => product.UnitPrice).Take(10);
                
                List<PurchaseOrderDetail> pods = ContextFactory.GetContextPerRequest().PurchaseOrderDetails.Where(pod => pod.PurchaseOrderHeader.StatusId == (int)PurchaseOrderStatusEnum.Completed &&
                    pod.PurchaseOrderHeader.OrderDate <= DateTime.Now.Date && pod.PurchaseOrderHeader.OrderDate > DateTime.Now.Date.AddDays(-7)).ToList();
                var groupiong = pods.GroupBy(pod => pod.PurchaseOrderHeader.Vendor);

                rhcVendorsLastWeek.DataSource = groupiong.Select(g => new { VendorName = g.Key.Name, Price = g.Sum(pod => pod.LineTotal) });
            }
        }
    }
}