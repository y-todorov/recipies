using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecipiesModelNS
{
    public partial class ProductInventoryHeader : YordanBaseEntity
    {
        private static int? productInventoryHeaderId = 0;
        public override void Removing(System.Data.Entity.Infrastructure.DbEntityEntry e = null)
        {
            productInventoryHeaderId = ProductInventoryHeaderId;
            base.Removing(e);
        }

        public override void Removed(System.Data.Entity.Infrastructure.DbEntityEntry e = null)
        {
            List<ProductInventory> pis = ContextFactory.Current.Inventories.OfType<ProductInventory>()
                .Where(pi => pi.ProductInventoryHeaderId == null).ToList();
            //pi => pi.ProductInventoryHeaderId == productInventoryHeaderId ||
            foreach (ProductInventory pi in pis)
            {
                ContextFactory.Current.Inventories.Remove(pi);
            }
            ContextFactory.Current.SaveChanges();
            base.Removed(e);
        }
    }
}