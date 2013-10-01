using RecipiesModelNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.OpenAccess.Web;

namespace YordanCustomControls
{
    public class YordanCustomOpenAccessLinqDataSource : OpenAccessLinqDataSource
    {
        public YordanCustomOpenAccessLinqDataSource()
        {
            // These events will fire first. Next will fire the events in custom pages that this control resides.
            Inserting += YordanCustomOpenAccessLinqDataSource_Inserting;
            Inserted += YordanCustomOpenAccessLinqDataSource_Inserted;

            Updating += YordanCustomOpenAccessLinqDataSource_Updating;
            Updated += YordanCustomOpenAccessLinqDataSource_Updated;

            Deleting += YordanCustomOpenAccessLinqDataSource_Deleting;
            Deleted += YordanCustomOpenAccessLinqDataSource_Deleted;
        }

        void YordanCustomOpenAccessLinqDataSource_Inserting(object sender, OpenAccessLinqDataSourceInsertEventArgs e)
        {
            YordanBaseEntity ybe = e.NewObject as YordanBaseEntity;
            if (ybe != null)
            {
                ybe.OaldsInserting(sender, e);
            }
        } 
        
        void YordanCustomOpenAccessLinqDataSource_Inserted(object sender, OpenAccessLinqDataSourceStatusEventArgs e)
        {
            YordanBaseEntity ybe = e.Result as YordanBaseEntity;
            if (ybe != null)
            {
                ybe.OaldsInserted(sender, e);
            }
        } 
        
        void YordanCustomOpenAccessLinqDataSource_Updating(object sender, OpenAccessLinqDataSourceUpdateEventArgs e)
        {
            YordanBaseEntity ybe = e.OriginalObject as YordanBaseEntity;
            if (ybe != null)
            {
                ybe.OaldsUpdating(sender, e);
            }
        }

        void YordanCustomOpenAccessLinqDataSource_Updated(object sender, OpenAccessLinqDataSourceStatusEventArgs e)
        {
            YordanBaseEntity ybe = e.Result as YordanBaseEntity;
            if (ybe != null)
            {
                ybe.OaldsUpdated(sender, e);
            }
        }  
        
        void YordanCustomOpenAccessLinqDataSource_Deleting(object sender, OpenAccessLinqDataSourceDeleteEventArgs e)
        {
            YordanBaseEntity ybe = e.OriginalObject as YordanBaseEntity;
            if (ybe != null)
            {
                ybe.OaldsDeleting(sender, e);
            }
        }
        
        void YordanCustomOpenAccessLinqDataSource_Deleted(object sender, OpenAccessLinqDataSourceStatusEventArgs e)
        {
            YordanBaseEntity ybe = e.Result as YordanBaseEntity;
            if (ybe != null)
            {
                ybe.OaldsDeleted(sender, e);
            }
        }

        protected override bool OnBubbleEvent(object source, EventArgs args)
        {
            return base.OnBubbleEvent(source, args);
        }
    }
}
