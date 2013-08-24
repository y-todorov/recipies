﻿using DynamicApplicationModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Reporting;

namespace RecipiesWebFormApp.Purchasing
{
    public partial class PurchaseOrderReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var instanceReportSource = new Telerik.Reporting.InstanceReportSource();
            instanceReportSource.ReportDocument = new RecipiesReports.Report2();
            this.ReportViewer1.ReportSource = instanceReportSource;
        }
    }
}