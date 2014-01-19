using Quartz;
using RecipiesModelNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecipiesWebFormApp.Quartz.Jobs
{
    public class CalculateProductsUnitPriceJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            Product.UpdateUnitPriceOfAllProducts();
        }
    }
}