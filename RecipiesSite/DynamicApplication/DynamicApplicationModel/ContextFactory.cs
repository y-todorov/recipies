using System.Web;

namespace RecipiesModelNS
{
    public class ContextFactory
    {
        private static readonly string contextKey = typeof (RecipiesEntities).FullName;

        public static RecipiesEntities GetContextPerRequest()
        {
            HttpContext httpContext = HttpContext.Current;
            if (httpContext == null)
            {
                return new RecipiesEntities(false);
            }
            else
            {
                RecipiesEntities context = httpContext.Items[contextKey] as RecipiesEntities;
                //RecipiesEntities context = httpContext.Application[contextKey] as RecipiesEntities; // This doesn't work 
                

                if (context == null)
                {
                    context = new RecipiesEntities(false);
                    httpContext.Items[contextKey] = context;
                    //httpContext.Application[contextKey] = context; // This doesn't work 
                }

                return context;
            }
        }

        public static RecipiesEntities Current
        {
            get
            {
                RecipiesEntities current = GetContextPerRequest();
                return current;
            }
        }

        public static void RemoveFromCache()
        {
             HttpContext httpContext = HttpContext.Current;
             if (httpContext != null)
             {
                 RecipiesEntities context = httpContext.Items[contextKey] as RecipiesEntities;

                 if (context != null)
                 {
                     httpContext.Items[contextKey] = null;
                 }
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

        public static RecipiesEntities CreateNewContext()
        {
            return new RecipiesEntities();
        }
    }
}