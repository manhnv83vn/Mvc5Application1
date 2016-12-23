using Microsoft.Practices.Unity;
using MvcSiteMapProvider;
using MvcSiteMapProvider.Builder;
using MvcSiteMapProvider.Caching;
using MvcSiteMapProvider.Security;
using MvcSiteMapProvider.Visitor;
using MvcSiteMapProvider.Web.Compilation;
using MvcSiteMapProvider.Web.Mvc;
using MvcSiteMapProvider.Web.UrlResolver;
using MvcSiteMapProvider.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web.Hosting;
using System.Web.Mvc;

namespace Mvc5Application1.VisibilityProvider
{
    public class Mvc5Application1ContainerExtension
    {
        public static void Initialize(IUnityContainer container)
        {
            bool enableLocalization = true;
            string absoluteFileName = HostingEnvironment.MapPath("~/Mvc.sitemap");
            TimeSpan absoluteCacheExpiration = TimeSpan.FromMinutes(5);
            bool visibilityAffectsDescendants = true;
            bool useTitleIfDescriptionNotProvided = true;
            bool securityTrimmingEnabled = false;
            string[] includeAssembliesForScan = { "Mvc5Application1.Web" };

            var currentAssembly = container.GetType().Assembly;
            var siteMapProviderAssembly = typeof(SiteMaps).Assembly;
            var allAssemblies = new[] { currentAssembly, siteMapProviderAssembly, typeof(RoleBasedVisibilityProvider).Assembly };
            var excludeTypes = new Type[] {
                // Use this array to add types you wish to explicitly exclude from convention-based
                // auto-registration. By default all types that either match I[TypeName] = [TypeName] or
                // I[TypeName] = [TypeName]Adapter will be automatically wired up as long as they don't
                // have the [ExcludeFromAutoRegistrationAttribute].
                //
                // If you want to override a type that follows the convention, you should add the name
                // of either the implementation name or the interface that it inherits to this list and
                // add your manual registration code below. This will prevent duplicate registrations
                // of the types from occurring.

                // Example:
                // typeof(SiteMap),
                // typeof(SiteMapNodeVisibilityProviderStrategy)
            };
            var multipleImplementationTypes = new[]  {
                typeof(ISiteMapNodeUrlResolver),
                typeof(ISiteMapNodeVisibilityProvider),
                typeof(IDynamicNodeProvider)
            };

            // Matching type name (I[TypeName] = [TypeName]) or matching type name + suffix Adapter (I[TypeName] = [TypeName]Adapter)
            // and not decorated with the [ExcludeFromAutoRegistrationAttribute].
            CommonConventions.RegisterDefaultConventions(
                (interfaceType, implementationType) => container.RegisterType(interfaceType, implementationType, new ContainerControlledLifetimeManager()),
                new[] { siteMapProviderAssembly },
                allAssemblies,
                excludeTypes,
                string.Empty);

            // Multiple implementations of strategy based extension points (and not decorated with [ExcludeFromAutoRegistrationAttribute]).
            CommonConventions.RegisterAllImplementationsOfInterface(
                (interfaceType, implementationType) => container.RegisterType(interfaceType, implementationType, implementationType.Name, new ContainerControlledLifetimeManager()),
                multipleImplementationTypes,
                allAssemblies,
                excludeTypes,
                string.Empty);

            // TODO: Find a better way to inject an array constructor

            // Url Resolvers
            container.RegisterType<ISiteMapNodeUrlResolverStrategy, SiteMapNodeUrlResolverStrategy>(new InjectionConstructor(
                new ResolvedArrayParameter<ISiteMapNodeUrlResolver>(container.ResolveAll<ISiteMapNodeUrlResolver>().ToArray())
                ));

            // Visibility Providers
            //container.RegisterType<ISiteMapNodeVisibilityProviderStrategy, SiteMapNodeVisibilityProviderStrategy>(new InjectionConstructor(
            //    new ResolvedArrayParameter<ISiteMapNodeVisibilityProvider>(container.ResolveAll<ISiteMapNodeVisibilityProvider>().ToArray()),
            //    new InjectionParameter<string>(string.Empty)
            //    ));

            container.RegisterType<ISiteMapNodeVisibilityProviderStrategy, SiteMapNodeVisibilityProviderStrategy>(new InjectionConstructor(
            new ResolvedArrayParameter<ISiteMapNodeVisibilityProvider>(container.ResolveAll<ISiteMapNodeVisibilityProvider>().ToArray()),
            new InjectionParameter<string>("Mvc5Application1.VisibilityProvider.RoleBasedVisibilityProvider, Mvc5Application1.Web")
            ));

            // Dynamic Node Providers
            container.RegisterType<IDynamicNodeProviderStrategy, DynamicNodeProviderStrategy>(new InjectionConstructor(
                new ResolvedArrayParameter<IDynamicNodeProvider>(container.ResolveAll<IDynamicNodeProvider>().ToArray())
                ));

            // Pass in the global controllerBuilder reference
            container.RegisterInstance(ControllerBuilder.Current);

            container.RegisterType<IControllerTypeResolverFactory, ControllerTypeResolverFactory>(new InjectionConstructor(
                new List<string>(),
                new ResolvedParameter<IControllerBuilder>(),
                new ResolvedParameter<IBuildManager>()));

            // Configure Security

            // IMPORTANT: Must give arrays of object a name in Unity in order for it to resolve them.
            container.RegisterType<IAclModule, AuthorizeAttributeAclModule>("authorizeAttribute");
            container.RegisterType<IAclModule, XmlRolesAclModule>("xmlRoles");
            container.RegisterType<IAclModule, CompositeAclModule>(new InjectionConstructor(new ResolvedArrayParameter<IAclModule>(
                new ResolvedParameter<IAclModule>("authorizeAttribute"),
                new ResolvedParameter<IAclModule>("xmlRoles"))));

            container.RegisterInstance<ObjectCache>(MemoryCache.Default);
            container.RegisterType(typeof(ICacheProvider<>), typeof(RuntimeCacheProvider<>));
            container.RegisterType<ICacheDependency, RuntimeFileCacheDependency>(
                "cacheDependency", new InjectionConstructor(absoluteFileName));

            container.RegisterType<ICacheDetails, CacheDetails>("cacheDetails",
                new InjectionConstructor(absoluteCacheExpiration, TimeSpan.MinValue, new ResolvedParameter<ICacheDependency>("cacheDependency")));

            // Configure the visitors
            container.RegisterType<ISiteMapNodeVisitor, UrlResolvingSiteMapNodeVisitor>();

            // Prepare for the sitemap node providers
            container.RegisterType<IXmlSource, FileXmlSource>("file1XmlSource", new InjectionConstructor(absoluteFileName));
            container.RegisterType<IReservedAttributeNameProvider, ReservedAttributeNameProvider>(new InjectionConstructor(new List<string>()));

            // IMPORTANT: Must give arrays of object a name in Unity in order for it to resolve them.
            // Register the sitemap node providers
            container.RegisterInstance<ISiteMapNodeProvider>("xmlSiteMapNodeProvider1",
                container.Resolve<XmlSiteMapNodeProviderFactory>().Create(container.Resolve<IXmlSource>("file1XmlSource")), new ContainerControlledLifetimeManager());
            container.RegisterInstance<ISiteMapNodeProvider>("reflectionSiteMapNodeProvider1",
                container.Resolve<ReflectionSiteMapNodeProviderFactory>().Create(includeAssembliesForScan), new ContainerControlledLifetimeManager());
            container.RegisterType<ISiteMapNodeProvider, CompositeSiteMapNodeProvider>(new InjectionConstructor(new ResolvedArrayParameter<ISiteMapNodeProvider>(
                new ResolvedParameter<ISiteMapNodeProvider>("xmlSiteMapNodeProvider1"),
                new ResolvedParameter<ISiteMapNodeProvider>("reflectionSiteMapNodeProvider1"))));

            // Configure the builders
            container.RegisterType<ISiteMapBuilder, SiteMapBuilder>();

            // Configure the builder sets
            container.RegisterType<ISiteMapBuilderSet, SiteMapBuilderSet>("builderSet1",
                new InjectionConstructor(
                    "default",
                    securityTrimmingEnabled,
                    enableLocalization,
                    visibilityAffectsDescendants,
                    useTitleIfDescriptionNotProvided,
                    new ResolvedParameter<ISiteMapBuilder>(),
                    new ResolvedParameter<ICacheDetails>("cacheDetails")));

            container.RegisterType<ISiteMapBuilderSetStrategy, SiteMapBuilderSetStrategy>(new InjectionConstructor(
                new ResolvedArrayParameter<ISiteMapBuilderSet>(new ResolvedParameter<ISiteMapBuilderSet>("builderSet1"))));
        }
    }
}