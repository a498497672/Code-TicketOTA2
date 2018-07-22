using Autofac;
using Autofac.Features.ResolveAnything;
using Autofac.Integration.Mvc;
using System.Reflection;
using System.Web.Mvc;

namespace Ticket.DistributorPlatform.AutoFac
{
    public class AutoFacInitialize
    {
        public static void AutofacIntitialy()
        {
            //创建autofac管理注册类的容器实例
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());
            //使用Autofac提供的RegisterControllers扩展方法来对程序集中所有的Controller一次性的完成注册
            //生成具体的实例(属性注入)
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            var container = builder.Build();
            //下面就是使用MVC的扩展 更改了MVC中的注入方式.
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}