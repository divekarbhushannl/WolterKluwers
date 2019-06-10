using System.Web;
using System.Web.Mvc;

namespace WolterKluwer.POS.Terminal.API
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
