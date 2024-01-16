using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using System.Web.Http;
using AutoMapper;
using WebAppMVCFramework.App_Start;

namespace WebAppMVCFramework
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //inizializzazione mapper
            Mapper.Initialize(c => c.AddProfile<MappingProfile>());

            //configurazione WEB API importante che questa sia all' inizio della lista, altrimenti le API risultano irraggiungibili
            GlobalConfiguration.Configure(WebApiConfig.Register);

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

           
        }
    }
}
