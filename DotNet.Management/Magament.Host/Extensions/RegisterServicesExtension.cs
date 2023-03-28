﻿using Management.Domain;
using System.Collections.Generic;
using System.Reflection;

namespace Management.Host
{
    public static class RegisterServicesExtension
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            string path = AppDomain.CurrentDomain.RelativeSearchPath ?? AppDomain.CurrentDomain.BaseDirectory;
            var assemblies = Directory.GetFiles(path, "*.dll").Select(Assembly.LoadFrom).ToArray();

            var allTypes = assemblies.SelectMany(a => a.GetTypes());
            var interFaceTypes = allTypes.Where(t => t.IsAssignableFrom(typeof(IDependency)) && t.IsClass && !t.IsAbstract && !t.IsGenericType);
            foreach (var type in interFaceTypes)
            {
                Type[] interfaceTypes = type.GetInterfaces();

                var types = interfaceTypes.Where(t => t != typeof(IDependency) 
                    && t != typeof(ISingletonDependency) 
                    && t != typeof(IScopedDependency) 
                    && t != typeof(ITransientDependency)).ToArray();

                if (interfaceTypes.Contains(typeof(IScopedDependency)))
                {
                    AddScoped(services,type, types);
                }
                else if (interfaceTypes.Contains(typeof(ISingletonDependency)))
                {
                    AddSingleton(services, type, types);
                }
                else
                {
                    AddTransient(services, type, types);
                }
            }
        }

        public static void AddScoped(IServiceCollection services, Type classType, Type[]? iTypes)
        {
            if (iTypes == null || !iTypes.Any())
            {
                services.AddScoped(classType);
                return;
            }
            foreach (var type in iTypes)
            {
                services.AddScoped(type, classType);
            }
        }

        public static void AddSingleton(IServiceCollection services, Type classType, Type[]? iTypes)
        {
            if (iTypes == null || !iTypes.Any())
            {
                services.AddSingleton(classType);
                return;
            }
            foreach (var type in iTypes)
            {
                services.AddSingleton(type, classType);
            }

        }

        public static void AddTransient(IServiceCollection services, Type classType, Type[]? iTypes)
        {
            if (iTypes == null || !iTypes.Any())
            {
                services.AddTransient(classType);
                return;
            }
            foreach (var type in iTypes)
            {
                services.AddTransient(type, classType);
            }

        }
    }
}
