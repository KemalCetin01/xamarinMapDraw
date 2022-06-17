using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
//using DynamicAPI.Core.CrossCuttingConcerns.Caching;
//using DynamicAPI.Core.CrossCuttingConcerns.Caching.Microsoft;
using DynamicAPI.Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;

namespace DynamicAPI.Core.DependencyResolvers
{
    public class CoreModule:ICoreModule
    {
        public void Load(IServiceCollection services)
        {
            services.AddMemoryCache();
            //services.AddSingleton<ICacheManager, MemoryCacheManager>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<Stopwatch>();
        }
    }
}
