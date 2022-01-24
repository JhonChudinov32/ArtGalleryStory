using System.Web.Mvc;
using System.Web.Routing;
using ArtGalleryStore.Domain.Entities;
using ArtGalleryStore.WebUI.Infrastructure.Binders;

namespace ArtGalleryStore.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            ModelBinders.Binders.Add(typeof(Cart), new CartModelBinder());
            
        }
    }
}
