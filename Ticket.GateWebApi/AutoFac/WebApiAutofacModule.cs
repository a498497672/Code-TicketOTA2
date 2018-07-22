using System.Reflection;
using Autofac;
using Autofac.Integration.WebApi;
using Module = Autofac.Module;

namespace Ticket.OtaWebApi.AutoFac
{
    public class WebApiAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
        }
    }
}