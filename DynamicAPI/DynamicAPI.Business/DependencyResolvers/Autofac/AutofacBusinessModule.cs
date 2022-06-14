using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using DynamicAPI.Business.Concrete;
using DynamicAPI.Business.Service;
using DynamicAPI.Core.Utilities.Interceptors;
using DynamicAPI.DataAccess.Concrete.EntityFramework;
using DynamicAPI.DataAccess.Service;

namespace DynamicAPI.Business.DependencyResolvers.Autofac
{
   public class AutofacBusinessModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<KonumManager>().As<IKonumService>();
            builder.RegisterType<UserManager>().As<IUserService>();
            
            builder.RegisterType<EfKonumDal>().As<IKonumDal>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();



            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
          .EnableInterfaceInterceptors(new ProxyGenerationOptions()
          {
              Selector = new AspectInterceptorSelector()
          }).SingleInstance();
        }
}
}
