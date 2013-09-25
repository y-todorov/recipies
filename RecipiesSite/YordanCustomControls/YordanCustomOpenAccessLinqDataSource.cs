using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.OpenAccess.Web;

namespace YordanCustomControls
{
    class YordanCustomOpenAccessLinqDataSource : OpenAccessLinqDataSource
    {
        public YordanCustomOpenAccessLinqDataSource()
        {
           
        }
       

        protected override bool OnBubbleEvent(object source, EventArgs args)
        {
            return base.OnBubbleEvent(source, args);
        }
    }
}
