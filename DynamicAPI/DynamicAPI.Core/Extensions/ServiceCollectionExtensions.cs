using System;
using System.Collections.Generic;
using System.Text;
using DynamicAPI.Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace DynamicAPI.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDependencyResolvers(this IServiceCollection services,
            ICoreModule[] modules)
        {
            foreach (var module in modules)
            {
               module.Load(services); 
            }

            return ServiceTool.Create(services);
        }
    }
}
