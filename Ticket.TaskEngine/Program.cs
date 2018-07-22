using Autofac;
using Autofac.Extras.Quartz;
using Autofac.Features.ResolveAnything;
using System.Configuration;
using Ticket.Core.Autofac;
using Ticket.TaskEngine.Services;
using Topshelf;

namespace Ticket.TaskEngine
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var container = ConfigureAutofac();
            ConfigService(container);
        }

        private static IContainer ConfigureAutofac()
        {
            var builder = new ContainerBuilder();
            builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());
            builder.RegisterModule(new QuartzAutofacFactoryModule());
            builder.RegisterModule(new QuartzAutofacJobsModule(typeof(Program).Assembly));
            builder.RegisterModule(new CoreAutofacModule());
            var container = builder.Build();
            return container;
        }

        private static void ConfigService(IContainer container)
        {
            var serviceName = ConfigurationManager.AppSettings["taskService:ServiceName"];
            var serviceDisplayName = ConfigurationManager.AppSettings["taskService:DisplayName"];
            var serviceDescription = ConfigurationManager.AppSettings["taskService:Description"];
            HostFactory.Run(hostConfigurator =>
            {
                hostConfigurator.Service<TaskService>(serviceConfigurator =>
                {
                    serviceConfigurator.ConstructUsing(name => container.Resolve<TaskService>());
                    serviceConfigurator.WhenStarted(taskService => taskService.Start());
                    serviceConfigurator.WhenStopped(taskService => taskService.Stop());
                });
                hostConfigurator.RunAsLocalSystem();
                hostConfigurator.SetServiceName(serviceName);//服务名称
                hostConfigurator.SetDisplayName(serviceDisplayName);//显示名称
                hostConfigurator.SetDescription(serviceDescription);//安装服务后，服务的描述
                hostConfigurator.StartAutomaticallyDelayed();
                hostConfigurator.EnableServiceRecovery(action => action.RestartService(1));
            });
        }
    }
}
