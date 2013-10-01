using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.OpenAccess;
using Telerik.OpenAccess.Web;

namespace RecipiesModelNS
{
    public partial class PurchaseOrderDetail : YordanBaseEntity
    {
        
        public override void OaldsInserted(object sender, OpenAccessLinqDataSourceStatusEventArgs e)
        {          
            PurchaseOrderDetail insertedPurchaseOrderDetail = e.Result as PurchaseOrderDetail;
            PurchaseOrderHeader purchaseOrder = ContextFactory.GetContextPerRequest().PurchaseOrderHeaders.FirstOrDefault(p => p.PurchaseOrderId == PurchaseOrderId);
            Product selectedProduct = ContextFactory.GetContextPerRequest().Products.FirstOrDefault(p => p.ProductId == insertedPurchaseOrderDetail.ProductId);
            UnitMeasure selectedUnitMeasure = ContextFactory.GetContextPerRequest().UnitMeasures.FirstOrDefault(um => um.UnitMeasureId == insertedPurchaseOrderDetail.UnitMeasureId);

            if (purchaseOrder.StatusId == (int)PurchaseOrderStatusEnum.Approved)
            {
                selectedProduct.UnitsOnOrder += selectedProduct.GetBaseUnitMeasureQuantityForProduct(insertedPurchaseOrderDetail.OrderQuantity, selectedUnitMeasure);
            }
            else if (purchaseOrder.StatusId == (int)PurchaseOrderStatusEnum.Completed)
            {
                selectedProduct.UnitsInStock += selectedProduct.GetBaseUnitMeasureQuantityForProduct(insertedPurchaseOrderDetail.StockedQuantity, selectedUnitMeasure);
            }
            selectedProduct.UnitPrice = (decimal)selectedProduct.GetAveragePriceLastDays(14);          

            UpdatePurchaseOrderSubTotalFromPurchaseOrderDetails(PurchaseOrderId);

            base.OaldsInserted(sender, e);
        }

        public override void OaldsUpdating(object sender, OpenAccessLinqDataSourceUpdateEventArgs e)
        {
            PurchaseOrderDetail oldPurchaseOrderDetail = e.OriginalObject as PurchaseOrderDetail;
            PurchaseOrderDetail newPurchaseOrderDetail = e.NewObject as PurchaseOrderDetail;
            double differenceOrderQuantity = newPurchaseOrderDetail.OrderQuantity.GetValueOrDefault() - oldPurchaseOrderDetail.OrderQuantity.GetValueOrDefault();
            // newPurchaseOrderDetail.StockedQuantity THIS WILL BE CALCULATED AFTER SAVE. IT WILL BE 0 NOW
            double newStockQuantity = newPurchaseOrderDetail.ReceivedQuantity.GetValueOrDefault() - newPurchaseOrderDetail.ReturnedQuantity.GetValueOrDefault();
            double differenceStockedQuantity = newStockQuantity - oldPurchaseOrderDetail.StockedQuantity;
            //double differenceStockedQuantity = newPurchaseOrderDetail.StockedQuantity - oldPurchaseOrderDetail.StockedQuantity;

            Product selectedProduct = ContextFactory.GetContextPerRequest().Products.FirstOrDefault(p => p.ProductId == newPurchaseOrderDetail.ProductId);
            UnitMeasure selectedUnitMeasure = ContextFactory.GetContextPerRequest().UnitMeasures.FirstOrDefault(um => um.UnitMeasureId == newPurchaseOrderDetail.UnitMeasureId);


            PurchaseOrderHeader purchaseOrder = ContextFactory.GetContextPerRequest().PurchaseOrderHeaders.FirstOrDefault(p => p.PurchaseOrderId == PurchaseOrderId);
            if (purchaseOrder.StatusId == (int)PurchaseOrderStatusEnum.Approved)
            {
                selectedProduct.UnitsOnOrder += selectedProduct.GetBaseUnitMeasureQuantityForProduct(differenceOrderQuantity, selectedUnitMeasure);
            }
            else if (purchaseOrder.StatusId == (int)PurchaseOrderStatusEnum.Completed)
            {
                selectedProduct.UnitsInStock += selectedProduct.GetBaseUnitMeasureQuantityForProduct(differenceStockedQuantity, selectedUnitMeasure);
            }
            
            //selectedProduct.UnitPrice = (decimal)selectedProduct.GetAveragePriceLastDays(14);
         
            base.OaldsUpdating(sender, e);
        }

        public override void OaldsUpdated(object sender, OpenAccessLinqDataSourceStatusEventArgs e)
        {
            Product selectedProduct = ContextFactory.GetContextPerRequest().Products.FirstOrDefault(p => p.ProductId == ProductId);
            if (selectedProduct != null)
            {
                selectedProduct.UnitPrice = (decimal)selectedProduct.GetAveragePriceLastDays(14);
            }
            UpdatePurchaseOrderSubTotalFromPurchaseOrderDetails(PurchaseOrderId);

            base.OaldsUpdated(sender, e);
        }
        

        public override void OaldsDeleting(object sender, OpenAccessLinqDataSourceDeleteEventArgs e)
        {
            PurchaseOrderDetail deletedPurchaseOrderDetail = e.OriginalObject as PurchaseOrderDetail;
            PurchaseOrderHeader purchaseOrder = ContextFactory.GetContextPerRequest().PurchaseOrderHeaders.FirstOrDefault(p => p.PurchaseOrderId == PurchaseOrderId);
            Product selectedProduct = ContextFactory.GetContextPerRequest().Products.FirstOrDefault(p => p.ProductId == deletedPurchaseOrderDetail.ProductId);
            UnitMeasure selectedUnitMeasure = ContextFactory.GetContextPerRequest().UnitMeasures.FirstOrDefault(um => um.UnitMeasureId == deletedPurchaseOrderDetail.UnitMeasureId);

            if (purchaseOrder.StatusId == (int)PurchaseOrderStatusEnum.Approved)
            {
                selectedProduct.UnitsOnOrder -= selectedProduct.GetBaseUnitMeasureQuantityForProduct(deletedPurchaseOrderDetail.OrderQuantity, selectedUnitMeasure);
            }
            else if (purchaseOrder.StatusId == (int)PurchaseOrderStatusEnum.Completed)
            {
                selectedProduct.UnitsInStock -= selectedProduct.GetBaseUnitMeasureQuantityForProduct(deletedPurchaseOrderDetail.StockedQuantity, selectedUnitMeasure);
            }

            //deletedPurchaseOrderDetail.Product.UnitPrice = (decimal)deletedPurchaseOrderDetail.Product.GetAveragePriceLastDays(14);
            deletedProduct = selectedProduct;
            deletedPurchaseOrderHeaderId = PurchaseOrderId;

            base.OaldsDeleting(sender, e);
        }

        private static int? deletedPurchaseOrderHeaderId;
        private static Product deletedProduct;

        public override void OaldsDeleted(object sender, OpenAccessLinqDataSourceStatusEventArgs e)
        {
            if (deletedProduct != null)
            {
                deletedProduct.UnitPrice = (decimal)deletedProduct.GetAveragePriceLastDays(14);
            }
            UpdatePurchaseOrderSubTotalFromPurchaseOrderDetails(deletedPurchaseOrderHeaderId);
            base.OaldsDeleted(sender, e);
        }

        private void UpdatePurchaseOrderSubTotalFromPurchaseOrderDetails(int? purchaseOrderHeaderId)
        {
            if (purchaseOrderHeaderId.HasValue)
            {
                PurchaseOrderHeader poh = ContextFactory.GetContextPerRequest().PurchaseOrderHeaders.FirstOrDefault(po => po.PurchaseOrderId == purchaseOrderHeaderId.Value);
                if (poh != null)
                {
                    decimal? subTotal = 0;
                    foreach (PurchaseOrderDetail pod in poh.PurchaseOrderDetails)
                    {
                        subTotal += (decimal)pod.LineTotal;
                    }
                    poh.SubTotal = subTotal;
                }
            }

        }
    }
}
