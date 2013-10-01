using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipiesModelNS
{
    public partial class ProductCategory : YordanBaseEntity
    {
        public override void Inserted(RecipiesModel context)
        {
            ProductCategory inserted = this;
            base.Inserted(context);
        }

        public override void Updated(RecipiesModel context)
        {
            ProductCategory updated = this;
            base.Updated(context);
        }

        public override void Deleted(RecipiesModel context)
        {
            ProductCategory deleted = this;
            base.Deleted(context);
        }
    }
}
