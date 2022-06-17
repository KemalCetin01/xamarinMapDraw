using Autofac;

using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using DynamicAPI.Core.Utilities.Interceptors;
using DynamicAPI.Core.Utilities.Security.Jwt;
using DynamicAPI.Business.Concrete;
using DynamicAPI.Business.Service;
using DynamicAPI.DataAccess.Concrete.EntityFramework;
using DynamicAPI.DataAccess.Service;

namespace DynamicAPI.Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<KonumManager>().As<IKonumService>();
            builder.RegisterType<UserManager>().As<IUserService>();

            builder.RegisterType<EfKonumDal>().As<IKonumDal>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();

            builder.RegisterType<AuthManager>().As<IAuthService>();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();




            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
          .EnableInterfaceInterceptors(new ProxyGenerationOptions()
          {
              Selector = new AspectInterceptorSelector()
          }).SingleInstance();
        }
    }
}
