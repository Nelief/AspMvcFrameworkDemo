using System.Web;
using System.Web.Mvc;

namespace WebAppMVCFramework
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

            //global authorization filter
            filters.Add(new AuthorizeAttribute());

            //Filtro per disattivare HTTP classico
            filters.Add(new RequireHttpsAttribute());
        }
    }
}
