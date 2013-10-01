using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Telerik.OpenAccess;
using Telerik.OpenAccess.Web;

namespace RecipiesModelNS
{
    public class YordanBaseEntity
    {
        public virtual void Adding(RecipiesModel context, AddEventArgs e)
        {

        }

        public virtual void Added(RecipiesModel context, AddEventArgs e)
        {
            SetModifiedDateAndModifiedByUserFields();
        }

        public virtual void Changing(RecipiesModel context, ChangeEventArgs e)
        {

        }

        public virtual void Changed(RecipiesModel context, ChangeEventArgs e)
        {
            SetModifiedDateAndModifiedByUserFields();
        }

        public virtual void Removing(RecipiesModel context, RemoveEventArgs e)
        {

        }

        public virtual void Removed(RecipiesModel context, RemoveEventArgs e)
        {

        }

        public virtual void BeforeInsert(RecipiesModel context)
        {

        }

        public virtual void AfterInsert(RecipiesModel context)
        {

        }
       
        public virtual void BeforeUpdate(RecipiesModel context)
        {

        } 
        
        public virtual void AfterUpdate(RecipiesModel context)
        {

        }

        public virtual void BeforeDelete(RecipiesModel context)
        {

        }

        public virtual void AfterDelete(RecipiesModel context)
        {

        }

        public virtual void OaldsInserting(object sender, OpenAccessLinqDataSourceInsertEventArgs e)
        {
            if (ContextFactory.GetContextPerRequest().HasChanges)
            {
                ContextFactory.GetContextPerRequest().SaveChanges();
            }
        }

        public virtual void OaldsInserted(object sender, OpenAccessLinqDataSourceStatusEventArgs e)
        {
            if (ContextFactory.GetContextPerRequest().HasChanges)
            {
                ContextFactory.GetContextPerRequest().SaveChanges();
            }
        }

        public virtual void OaldsUpdating(object sender, OpenAccessLinqDataSourceUpdateEventArgs e)
        {
            if (ContextFactory.GetContextPerRequest().HasChanges)
            {
                ContextFactory.GetContextPerRequest().SaveChanges();
            }
        }

        public virtual void OaldsUpdated(object sender, OpenAccessLinqDataSourceStatusEventArgs e)
        {
            if (ContextFactory.GetContextPerRequest().HasChanges)
            {
                ContextFactory.GetContextPerRequest().SaveChanges();
            }
        }

        public virtual void OaldsDeleting(object sender, OpenAccessLinqDataSourceDeleteEventArgs e)
        {
            if (ContextFactory.GetContextPerRequest().HasChanges)
            {
                ContextFactory.GetContextPerRequest().SaveChanges();
            }
        }

        public virtual void OaldsDeleted(object sender, OpenAccessLinqDataSourceStatusEventArgs e)
        {
            if (ContextFactory.GetContextPerRequest().HasChanges)
            {
                ContextFactory.GetContextPerRequest().SaveChanges();
            }
        }

        private void SetModifiedDateAndModifiedByUserFields()
        {
            string userName = null;
            if (HttpContext.Current != null && HttpContext.Current.User != null && HttpContext.Current.User.Identity != null)
            {
                userName = HttpContext.Current.User.Identity.Name;
            }
            Type type = this.GetType();
            if (type.GetProperties().Any(p => p.Name.Equals("ModifiedDate")))
            {
                DateTime modifiedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time"));
                this.SetFieldValue<DateTime>("ModifiedDate", modifiedDate);
                this.SetFieldValue<string>("ModifiedByUser", userName);
            }
        }

               
    }
}
