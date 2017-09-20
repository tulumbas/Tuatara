using System;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Tuatara.Models.Services;
using Tuatara.Data.Repositories;
using Tuatara.Data.DB;
using System.Web.Configuration;

namespace Tuatara.App_Start
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your types here
            // container.RegisterType<IProductRepository, ProductRepository>();

            var automapperConfig = AutomapperConfig.Configure();
            container.RegisterInstance(automapperConfig.CreateMapper());
            
            // services
            container.RegisterType<AssignmentService>();
            container.RegisterType<CalendarService>();
            container.RegisterType<PlaybookService>();
            container.RegisterType<ProjectClientService>();
            container.RegisterType<ResourceService>();
            container.RegisterType<UserService>();

            // EF repositories
            if (WebConfigurationManager.AppSettings["mockupData"] == "true")
            {

            }
            else
            {
                container.RegisterType<IAssignmentRepository, AssignmentRepository>();
                container.RegisterType<ICalendarItemRepository, CalendarItemRepository>();
                container.RegisterType<IProjectClientRepository, ProjectClientRepository>();
                container.RegisterType<IResourceRepository, ResourceRepository>();
                container.RegisterType<IUserRepository, UserRepository>();
            }
        }
    }
}
