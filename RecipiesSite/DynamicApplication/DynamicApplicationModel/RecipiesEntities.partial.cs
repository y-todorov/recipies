using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace RecipiesModelNS
{
    public partial class RecipiesEntities : DbContext
    {
        protected override System.Data.Entity.Validation.DbEntityValidationResult ValidateEntity(System.Data.Entity.Infrastructure.DbEntityEntry entityEntry, IDictionary<object, object> items)
        {
            return base.ValidateEntity(entityEntry, items);
        }

        protected override bool ShouldValidateEntity(System.Data.Entity.Infrastructure.DbEntityEntry entityEntry)
        {
            return base.ShouldValidateEntity(entityEntry);
        }

        public override int SaveChanges()
        {
            IEnumerable<DbEntityEntry> entries = ChangeTracker.Entries();
            foreach (DbEntityEntry entry in entries)
            {
                if (entry.State == System.Data.EntityState.Added)
                {
                    YordanBaseEntity ybe = entry.Entity as YordanBaseEntity;
                    if (ybe != null)
                    {
                        ybe.Adding(entry);
                    }
                }
            }
            int result = base.SaveChanges();
            foreach (DbEntityEntry entry in entries)
            {
            }


            return result;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
