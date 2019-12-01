using System.Web;
using System.Web.Mvc;

namespace mvcAuth2019
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //!PROTECTS WHOLE APP FROM ACCESS. ONLY ATHORIZED
            //filters.Add(new AuthorizeAttribute());
        }
    }
}
