using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Vidly
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //to avoid access to any controller with out login
            filters.Add(new AuthorizeAttribute());
        }
    }
}
