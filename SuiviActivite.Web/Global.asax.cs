using SuiviActivite.Web.Custom.ModelBinders;
using SuiviActivite.Web.Models.Schedules.AddSchedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SuiviActivite.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            InitilalizeAutoMappings();
            //InitializeCustomModelBinders();

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
        }

        private void InitilalizeAutoMappings()
        {
            Business.ObjectsMapping.InitializeObjectMaps.InitializeMaps();
        }

        private void InitializeCustomModelBinders()
        {
            ModelBinders.Binders.Add(typeof(AddSchedule), new AddScheduleBinder());
        }
    }
}