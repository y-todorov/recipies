﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RecipiesWebFormApp
{
    public partial class Error : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Exception ex = Server.GetLastError();
                if (ex != null)
                {
                    string details = string.Empty;
                    if (ex.InnerException != null)
                    {
                        details = ex.InnerException.Message;
                    }
                    lblError.Text = string.Format("Exception: {0}! Details: {1}!", ex.Message, details);
                }
            }
            catch (Exception exc)
            {
                lblError.Text = exc.Message + " " + exc.StackTrace;
            }

        }
    }
}