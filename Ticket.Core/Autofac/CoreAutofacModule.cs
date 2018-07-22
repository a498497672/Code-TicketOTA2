using Autofac;
using Ticket.SqlSugar.Models;

namespace Ticket.Core.Autofac
{
    public class CoreAutofacModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterType<TicketDBEntities>().AsSelf().InstancePerLifetimeScope();
        }
    }
}
