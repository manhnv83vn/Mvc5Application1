using Microsoft.Practices.Unity;
using Mvc5Application1.VisibilityProvider;
using MvcSiteMapProvider;
using MvcSiteMapProvider.Loader;
using MvcSiteMapProvider.Web.Mvc;
using MvcSiteMapProvider.Xml;
using System.Web.Hosting;
using System.Web.Routing;

namespace Mvc5Application1
{
    internal class Mvc5Application1SiteMapProviderConfig
    {
        public static void Register(IUnityContainer container)
        {
            Mvc5Application1ContainerExtension.Initialize(container);

            // Setup global sitemap loader
            SiteMaps.Loader = container.Resolve<ISiteMapLoader>();

            // Check all configured .sitemap files to ensure they follow the XSD for MvcSiteMapProvider
            var validator = container.Resolve<ISiteMapXmlValidator>();
            validator.ValidateXml(HostingEnvironment.MapPath("~/Mvc.sitemap"));

            // Register the Sitemaps routes for search engines
            XmlSiteMapController.RegisterRoutes(RouteTable.Routes);
        }
    }
}