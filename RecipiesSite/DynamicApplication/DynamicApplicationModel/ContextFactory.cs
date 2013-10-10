﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace RecipiesModelNS
{
    public class ContextFactory
    {
        private static readonly string contextKey = typeof(RecipiesEntities).FullName;

        public static RecipiesEntities GetContextPerRequest()
        {
            HttpContext httpContext = HttpContext.Current;
            if (httpContext == null)
            {
                return new RecipiesEntities();
            }
            else
            {
                RecipiesEntities context = httpContext.Items[contextKey] as RecipiesEntities;

                if (context == null)
                {
                    context = new RecipiesEntities();
                    httpContext.Items[contextKey] = context;
                }

                return context;
            }
        }

        public static void Dispose()
        {
            HttpContext httpContext = HttpContext.Current;

            if (httpContext != null)
            {
                RecipiesEntities context = httpContext.Items[contextKey] as RecipiesEntities;

                if (context != null)
                {
                    context.Dispose();
                    httpContext.Items[contextKey] = null;
                }
            }
        }
    }
}

