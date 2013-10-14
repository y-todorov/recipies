using System;
using System.Collections.Generic;
using System.Linq;

namespace RecipiesModelNS
{
    public partial class SalesOrderHeader
    {
        public static List<SalesOrderHeader> GetSalesOrderHeadersInPeriod(DateTime fromDate, DateTime toDate,
            SalesOrderStatusEnum status)
        {
            DateTime defaultDate = new DateTime(2000, 1, 1);
            List<SalesOrderHeader> result =
                ContextFactory.GetContextPerRequest().SalesOrderHeaders.Where(pof => pof.ShippedDate >= fromDate.Date &&
                                                                                     pof.ShippedDate <= toDate.Date &&
                                                                                     pof.StatusId == (int) status)
                    .ToList();
            return result;
        }
    }
}