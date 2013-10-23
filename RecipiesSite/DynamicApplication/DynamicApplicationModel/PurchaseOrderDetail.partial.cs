using System.Data.Entity.Infrastructure;
using System.Linq;

namespace RecipiesModelNS
{
    public partial class PurchaseOrderDetail : YordanBaseEntity
    {
        private static int? _purchaseOrderHeaderId;

        public override void Added(DbEntityEntry e = null)
        {
            UpdatePurchaseOrderHeaderSubTotalFromPurchaseOrderDetailChange(PurchaseOrderId);
            PurchaseOrderHeader.UpdateProductsUnitsInStock(PurchaseOrderId);
            base.Added(e);
        }

        public override void Changed(DbEntityEntry e = null)
        {
            UpdatePurchaseOrderHeaderSubTotalFromPurchaseOrderDetailChange(PurchaseOrderId);
            PurchaseOrderHeader.UpdateProductsUnitsInStock(PurchaseOrderId);
            base.Changed(e);
        }

        public override void Removing(DbEntityEntry e = null)
        {
            _purchaseOrderHeaderId = PurchaseOrderId;
            base.Removing(e);
        }

        public override void Removed(DbEntityEntry e = null)
        {
            UpdatePurchaseOrderHeaderSubTotalFromPurchaseOrderDetailChange(_purchaseOrderHeaderId);
            PurchaseOrderHeader.UpdateProductsUnitsInStock(_purchaseOrderHeaderId);
            base.Removed(e);
        }


        private void UpdatePurchaseOrderHeaderSubTotalFromPurchaseOrderDetailChange(int? purchaseOrderHeaderId)
        {
            if (purchaseOrderHeaderId.HasValue)
            {
                PurchaseOrderHeader poh =
                    ContextFactory.GetContextPerRequest()
                        .PurchaseOrderHeaders.FirstOrDefault(po => po.PurchaseOrderId == purchaseOrderHeaderId.Value);
                if (poh != null)
                {
                    decimal? subTotal = 0;
                    foreach (PurchaseOrderDetail pod in poh.PurchaseOrderDetails)
                    {
                        subTotal += (decimal?) pod.LineTotal;
                    }
                    poh.SubTotal = subTotal;
                    ContextFactory.GetContextPerRequest().SaveChanges();
                }
            }
        }
    }
}