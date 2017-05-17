using System.Web;
using System.Web.Mvc;

namespace TopProgs
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

            // Include line below for automatic authorisation check before controller calls.
            // So user must be logged in to get to any screen apart from the home screen.
            filters.Add(new AuthorizeAttribute());
        }
    }
}
