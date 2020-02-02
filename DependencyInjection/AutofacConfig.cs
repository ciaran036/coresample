using System.Reflection;
using Autofac;
using Data.Repositories;

namespace DependencyInjection
{
    public static class AutofacConfig
    {
        #region Assembly names

        public const string Entities = "Entities";
        public const string Web = "Web";
        public const string Data = "Data";
        public const string JobRunner = "JobRunner";
        public const string Reporting = "Reporting";
        public const string Services = "Services";

        #endregion

        public static void RegisterWebServices(this ContainerBuilder builder)
        {
            WebRegistration(builder);
            ServiceRegistration(builder);
            DatabaseRegistration(builder);
            builder.RegisterAssemblyValidators(Entities, Services, Web);
        }

        private static void WebRegistration(ContainerBuilder builder)
        {
            builder.RegisterAutowiredAssemblyInterfaces(Assembly.Load(Web));
            builder.RegisterAutowiredAssemblyTypes(Assembly.Load(Web));
        }

        private static void DatabaseRegistration(ContainerBuilder builder)
        {
            builder.RegisterGenericInstance(typeof(GenericRepository<>), typeof(IGenericRepository<>));
            //builder.RegisterGenericInstance(typeof(GuidEntityBaseRepository<>), typeof(IGuidEntityBaseRepository<>), lifetimeScope);

            builder.RegisterAssemblyInterfaces(Assembly.Load(Data));
        }

        private static void ServiceRegistration(ContainerBuilder builder)
        {
            builder.RegisterAutowiredAssemblyInterfaces(Assembly.Load(Services));
        }
    }
}