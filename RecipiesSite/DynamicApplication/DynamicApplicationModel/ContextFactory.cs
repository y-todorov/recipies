using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace RecipiesModelNS
{
    public class ContextFactory
    {
        private static readonly string contextKey = typeof(RecipiesModel).FullName;

        public static RecipiesModel GetContextPerRequest()
        {
            HttpContext httpContext = HttpContext.Current;
            if (httpContext == null)
            {
                return new RecipiesModel();
            }
            else
            {
                RecipiesModel context = httpContext.Items[contextKey] as RecipiesModel;

                if (context == null)
                {
                    context = new RecipiesModel();
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
                RecipiesModel context = httpContext.Items[contextKey] as RecipiesModel;

                if (context != null)
                {
                    context.Dispose();
                    httpContext.Items[contextKey] = null;
                }
            }
        }
    }
}

