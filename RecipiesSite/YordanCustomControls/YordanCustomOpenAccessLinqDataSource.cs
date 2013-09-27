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

        }

        void YordanCustomOpenAccessLinqDataSource_Inserted(object sender, OpenAccessLinqDataSourceStatusEventArgs e)
        {
            //throw new NotImplementedException();
        }

        void YordanCustomOpenAccessLinqDataSource_Inserting(object sender, OpenAccessLinqDataSourceInsertEventArgs e)
        {
            //throw new NotImplementedException();
        }       

        protected override bool OnBubbleEvent(object source, EventArgs args)
        {
            return base.OnBubbleEvent(source, args);
        }
    }
}
