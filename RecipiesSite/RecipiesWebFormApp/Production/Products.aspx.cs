using RecipiesModelNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RecipiesWebFormApp.Production
{
    public partial class xProducts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
        }

        protected void rbUpdateUnitPrice_Click(object sender, EventArgs e)
        {
            List<Product> products = ContextFactory.GetContextPerRequest().Products.ToList();
            foreach (Product product in products)
            {
                product.UnitPrice = (decimal?)product.GetAveragePriceLastDays(14);
            }
            ContextFactory.GetContextPerRequest().SaveChanges();
            rgProducts.Rebind();
        }

        protected void rbUpdateUnitsInStock_Click(object sender, EventArgs e)
        {
            List<Product> allProducts = ContextFactory.GetContextPerRequest().Products.ToList();
            List<PurchaseOrderDetail> allCompletedPurchaseOrderDetals = 
                ContextFactory.GetContextPerRequest().PurchaseOrderDetails.Where(pod => pod.PurchaseOrderHeader.StatusId == (int)PurchaseOrderStatusEnum.Completed).ToList();

            double unitsInStock = 0;
            foreach (Product product in allProducts)
            {
                foreach (PurchaseOrderDetail pod in allCompletedPurchaseOrderDetals)
                {
                    if (pod.ProductId == product.ProductId)
                    {
                        unitsInStock += product.GetBaseUnitMeasureQuantityForProduct(pod.StockedQuantity, pod.UnitMeasure);
                    }
                }
                product.UnitsInStock = unitsInStock;
            }
            ContextFactory.GetContextPerRequest().SaveChanges();
            rgProducts.Rebind();
        }

        protected void rbUpdateUnitsOnOrder_Click(object sender, EventArgs e)
        {
            List<Product> allProducts = ContextFactory.GetContextPerRequest().Products.ToList();
            List<PurchaseOrderDetail> allCompletedPurchaseOrderDetals =
                ContextFactory.GetContextPerRequest().PurchaseOrderDetails.Where(pod => pod.PurchaseOrderHeader.StatusId == (int)PurchaseOrderStatusEnum.Approved).ToList();

            double unitsOnOrderk = 0;
            foreach (Product product in allProducts)
            {
                foreach (PurchaseOrderDetail pod in allCompletedPurchaseOrderDetals)
                {
                    if (pod.ProductId == product.ProductId)
                    {
                        unitsOnOrderk += product.GetBaseUnitMeasureQuantityForProduct(pod.OrderQuantity, pod.UnitMeasure);
                    }
                }
                product.UnitsOnOrder = unitsOnOrderk;
            }
            ContextFactory.GetContextPerRequest().SaveChanges();
            rgProducts.Rebind();
        }

        public decimal GetStocktackeValue(object productId)
        {
            Product product = ContextFactory.GetContextPerRequest().Products.FirstOrDefault(p => p.ProductId.ToString() == (string)productId);
            if (product != null)
            {
                decimal result = product.UnitPrice.GetValueOrDefault() * (decimal)product.UnitsInStock.GetValueOrDefault();
                return result;
            }
            return 0m;
        }
           
    }
}