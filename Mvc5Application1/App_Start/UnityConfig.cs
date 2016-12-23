using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Mvc5Application1.Business;
using Mvc5Application1.Business.Administration;
using Mvc5Application1.Business.Contracts;
using Mvc5Application1.Business.Contracts.Administration;
using Mvc5Application1.Business.Contracts.RefData;
using Mvc5Application1.Business.Contracts.Security;
using Mvc5Application1.Business.RefData;
using Mvc5Application1.Business.Security;
using Mvc5Application1.Data;
using Mvc5Application1.Data.Contracts;
using Mvc5Application1.Data.Model;
using Mvc5Application1.Data.Repository;
using Mvc5Application1.Framework.Caching;
using Mvc5Application1.Framework.Logging;
using Mvc5Application1.Framework.Security.Authorization;
using System;

namespace Mvc5Application1.App_Start
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container

        private static readonly Lazy<IUnityContainer> Container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            container.LoadConfiguration();
            RegisterTypes(container);
            Mvc5Application1SiteMapProviderConfig.Register(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return Container.Value;
        }

        #endregion Unity Container

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<AuthorizationManager, AuthorizationManager>();

            container.RegisterType<IAdministrationBusiness, AdministrationBusiness>(new PerRequestLifetimeManager());
            container.RegisterType<IAuthorizationBusiness, AuthorizationBusiness>(new PerRequestLifetimeManager());
            container.RegisterType<ISettingsBusiness, SettingsBusiness>(new PerRequestLifetimeManager());

            container.RegisterType<IUserProfileBusiness, UserProfileBusiness>(new PerRequestLifetimeManager());
            container.RegisterType<IProjectUserRoleBusiness, ProjectUserRoleBusiness>(new PerRequestLifetimeManager());
            container.RegisterType<IRefDataBusiness, RefDataBusiness>(new PerRequestLifetimeManager());

            container.RegisterType<IUnitOfWork, TestDbContext>(new PerRequestLifetimeManager());
            container.RegisterType(typeof(IRepository<>), typeof(Repository<>), new PerRequestLifetimeManager());

            container.RegisterInstance(typeof(ICacheManager), new MemoryCacheManager());
            container.RegisterInstance(typeof(ILogger), new EaLogger());

            container.RegisterType(typeof(IRepository<UserProfile>), typeof(Repository<UserProfile>), new PerRequestLifetimeManager());
            container.RegisterType(typeof(IRepository<SpecicalPermission>), typeof(Repository<SpecicalPermission>), new PerRequestLifetimeManager());
            container.RegisterType(typeof(IRepository<>), typeof(Repository<>), new PerRequestLifetimeManager());

            container.RegisterType<IRefDataRepository, RefDataRepository>(new PerRequestLifetimeManager());
            container.RegisterType<IUserProfileRepository, UserProfileRepository>(new PerRequestLifetimeManager());
            container.RegisterType<ISecurityMatrixRepository, SecurityMatrixRepository>(new PerRequestLifetimeManager());
        }
    }
}