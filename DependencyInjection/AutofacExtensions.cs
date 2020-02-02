using System;
using System.Reflection;
using Autofac;
using Autofac.Builder;
using Autofac.Features.Scanning;
using FluentValidation;

namespace DependencyInjection
{
    /// <summary>
    /// Extensions to assist with type registrations
    /// </summary>
    public static class AutofacExtensions
    {
        /// <summary>
        /// Registers an instance of the given type as the supplied type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TServiceType"></typeparam>
        /// <param name="builder"></param>
        /// <param name="lifetimeScope"></param>
        /// <returns></returns>
        public static IRegistrationBuilder<T, ConcreteReflectionActivatorData, SingleRegistrationStyle>
            RegisterInstance<T, TServiceType>(this ContainerBuilder builder, object lifetimeScope)
        {
            return builder.RegisterType<T>()
                .As<TServiceType>()
                .InstancePerMatchingLifetimeScope(lifetimeScope);
        }

        /// <summary>
        /// Registers an instance of the supplied type with autowired properties
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="builder"></param>
        /// <param name="lifetimeScope"></param>
        /// <returns></returns>
        public static IRegistrationBuilder<T, ConcreteReflectionActivatorData, SingleRegistrationStyle>
            RegisterAutowiredInstance<T>(this ContainerBuilder builder, object lifetimeScope)
        {
            return builder.RegisterType<T>()
                .PropertiesAutowired()
                .InstancePerMatchingLifetimeScope(lifetimeScope);
        }

        /// <summary>
        /// Registers an instance of the supplied to type as another type with autowired properties
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="builder"></param>
        /// <param name="lifetimeScope"></param>
        /// <returns></returns>
        public static IRegistrationBuilder<T1, ConcreteReflectionActivatorData, SingleRegistrationStyle>
            RegisterAutowiredInstance<T1, T2>(this ContainerBuilder builder, object lifetimeScope)
        {
            return builder.RegisterType<T1>()
                .As<T2>()
                .PropertiesAutowired()
                .InstancePerMatchingLifetimeScope(lifetimeScope);
        }

        public static void RegisterAutowiredInstances(this ContainerBuilder builder, object lifetimeScope, params Type[] types)
        {
            foreach (var type in types)
            {
                builder.RegisterType(type)
                    .AsImplementedInterfaces()
                    .PropertiesAutowired()
                    .InstancePerMatchingLifetimeScope(lifetimeScope);
            }
        }

        public static void RegisterAutowiredInstances(this ContainerBuilder builder, params Type[] types)
        {
            foreach (var type in types)
            {
                builder.RegisterType(type)
                    .AsImplementedInterfaces()
                    .PropertiesAutowired()
                    .InstancePerLifetimeScope();
            }
        }

        /// <summary>
        /// Registers FluentValidator models with autowired properties
        /// </summary>
        /// <typeparam name="TValidator">Validator type</typeparam>
        /// <typeparam name="TModel">The model to be validated</typeparam>
        /// <param name="builder">Autofac builder</param>
        /// <param name="lifetimeScope">Scope of the registration</param>
        /// <returns>Builder</returns>
        public static IRegistrationBuilder<TValidator, ConcreteReflectionActivatorData, SingleRegistrationStyle>
            RegisterValidator<TValidator, TModel>(this ContainerBuilder builder, object lifetimeScope)
        {
            return builder.RegisterType<TValidator>()
                .As<IValidator<TModel>>()
                .PropertiesAutowired()
                .InstancePerMatchingLifetimeScope(lifetimeScope);
        }

        /// <summary>
        /// Register all validators in the supplied assemblies with autowired properties
        /// </summary>
        /// <param name="builder">AutoFac container</param>
        /// <param name="assemblies">Names of the assemblies to register validators</param>
        public static void RegisterAssemblyValidators(this ContainerBuilder builder, params string[] assemblies)
        {
            foreach (var assembly in assemblies)
            {
                builder.RegisterAssemblyTypes(Assembly.Load(assembly))
                    .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
                    .AsImplementedInterfaces()
                    .PropertiesAutowired()
                    .InstancePerLifetimeScope();
            }
        }

        /// <summary>
        /// Registers an instance of a generic type
        /// </summary>
        /// <param name="builder">Autofac builder</param>
        /// <param name="type">The concrete type</param>
        /// <param name="serviceType">The type we are registering as, e.g. an interface type</param>
        /// <returns>Builder</returns>
        public static IRegistrationBuilder<object, ReflectionActivatorData, DynamicRegistrationStyle>
            RegisterGenericInstance(this ContainerBuilder builder, Type type, Type serviceType)
        {
            return builder.RegisterGeneric(type)
                .As(serviceType)
                .InstancePerLifetimeScope();
        }

        /// <summary>
        /// Registers an autowired instance of a generic type
        /// </summary>
        /// <param name="builder">Autofac builder</param>
        /// <param name="type">The concrete type</param>
        /// <param name="serviceType">The type we are registering as, e.g. an interface type</param>
        /// <param name="lifetimeScope">Scope of the registration</param>
        /// <returns>Builder</returns>
        public static IRegistrationBuilder<object, ReflectionActivatorData, DynamicRegistrationStyle>
            RegisterAutowiredGenericInstance(this ContainerBuilder builder, Type type, Type serviceType, object lifetimeScope)
        {
            return builder.RegisterGeneric(type)
                .As(serviceType)
                .PropertiesAutowired()
                .InstancePerMatchingLifetimeScope(lifetimeScope);
        }

        /// <summary>
        /// Registers all interface types in an assembly as per their implementations with properties autowired
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle>
            RegisterAutowiredAssemblyInterfaces(this ContainerBuilder builder, Assembly assembly)
        {
            return builder.RegisterAssemblyTypes(assembly)
                .AsImplementedInterfaces()
                .PropertiesAutowired()
                .InstancePerLifetimeScope();
        }

        /// <summary>
        /// Registers all types in an assembly with properties autowired
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle>
            RegisterAutowiredAssemblyTypes(this ContainerBuilder builder, Assembly assembly)
        {
            return builder.RegisterAssemblyTypes(assembly)
                .PropertiesAutowired()
                .InstancePerLifetimeScope();
        }

        /// <summary>
        /// Registers all interfaces in an assembly as per their implementations
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle>
            RegisterAssemblyInterfaces(this ContainerBuilder builder, Assembly assembly)
        {
            return builder.RegisterAssemblyTypes(assembly)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}