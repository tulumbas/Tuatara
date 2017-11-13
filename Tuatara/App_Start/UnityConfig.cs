using AutoMapper;
using System;
using Tuatara.Data.DB;
using Tuatara.Data.Repositories;
using Tuatara.Services;
using Tuatara.Services.BL;
using Unity;

namespace Tuatara
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your type's mappings here.
            // container.RegisterType<IProductRepository, ProductRepository>();
            //var automapperConfig = AutomapperConfig.Configure();
            var automapperConfig = new MapperConfiguration(cfg => {
                cfg.AddProfile<TuataraDataProfile>(); // add profiles for all assemblies
            });
            container.RegisterInstance(automapperConfig.CreateMapper());

            // DB
            container.RegisterType<IDbContext, TuataraContext>();
            container.RegisterType<IUnitOfWork, UnitOfWork>();

            // services
            container.RegisterType<AssignmentService>();
            container.RegisterType<CalendarService>();
            container.RegisterType<ProjectClientService>();
            container.RegisterType<ResourceService>();
            container.RegisterType<UserService>();
            container.RegisterType<StatusService>();

        }
    }
}