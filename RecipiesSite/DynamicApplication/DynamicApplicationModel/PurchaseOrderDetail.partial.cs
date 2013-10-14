using System.Data.Entity.Infrastructure;
using System.Linq;

namespace RecipiesModelNS
{
    public partial class PurchaseOrderDetail : YordanBaseEntity
    {

        private static int? purchaseOrderHeaderId;

        public override void Added(DbEntityEntry e = null)
        {
            UpdatePurchaseOrderHeaderFromPurchaseOrderDetailChange(PurchaseOrderId);
            base.Added(e);
        }

        public override void Changed(System.Data.Entity.Infrastructure.DbEntityEntry e = null)
        {
            UpdatePurchaseOrderHeaderFromPurchaseOrderDetailChange(PurchaseOrderId);
            base.Changed(e);
        }

        public override void Removing(System.Data.Entity.Infrastructure.DbEntityEntry e = null)
        {
            purchaseOrderHeaderId = PurchaseOrderId;
            base.Removing(e);
        }

        public override void Removed(System.Data.Entity.Infrastructure.DbEntityEntry e = null)
        {
            UpdatePurchaseOrderHeaderFromPurchaseOrderDetailChange(purchaseOrderHeaderId);
            base.Removed(e);
        }


        private void UpdatePurchaseOrderHeaderFromPurchaseOrderDetailChange(int? purchaseOrderHeaderId)
        {
            if (purchaseOrderHeaderId.HasValue)
            {
                PurchaseOrderHeader poh = ContextFactory.GetContextPerRequest().PurchaseOrderHeaders.FirstOrDefault(po => po.PurchaseOrderId == purchaseOrderHeaderId.Value);
                if (poh != null)
                {
                    decimal? subTotal = 0;
                    foreach (PurchaseOrderDetail pod in poh.PurchaseOrderDetails)
                    {
                        subTotal += (decimal?)pod.LineTotal;
                    }
                    poh.SubTotal = subTotal;
                    ContextFactory.GetContextPerRequest().SaveChanges();
                }
            }

        }
    }
}
