using Autofac;
using Autofac.Features.ResolveAnything;
using Autofac.Integration.WebApi;
using Microsoft.Owin.Cors;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Owin;
using Swashbuckle.Application;
using System;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using Ticket.SaleWebApi.Application;
using Ticket.SaleWebApi.AutoFac;
using Ticket.Utility.MessageHandlers;
using Ticket.Utility.Services;

namespace Ticket.SaleWebApi
{
    /// <summary>
    /// 程序入口
    /// </summary>
    public class Startup
    {
        private readonly HttpConfiguration _httpConfig;

        /// <summary>
        /// 
        /// </summary>
        public Startup()
        {
            _httpConfig = new HttpConfiguration();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        public void Configuration(IAppBuilder app)
        {
            ConfigureAutofac(app);
            ConfigureWebApi(app);
            ConfigureSwagger();
        }

        private void ConfigureWebApi(IAppBuilder app)
        {
            _httpConfig.MapHttpAttributeRoutes();
            _httpConfig.Services.Add(typeof(IExceptionLogger), new UnhandledExceptionLogger());
            _httpConfig.Services.Replace(typeof(IExceptionHandler), new UnhandledExceptionHandler());
            _httpConfig.MessageHandlers.Add(new ETagHandler());
            var jsonFormatter = _httpConfig.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            jsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            app.UseCors(CorsOptions.AllowAll);
            app.UseWebApi(_httpConfig);
        }

        private void ConfigureAutofac(IAppBuilder app)
        {
            var builder = new ContainerBuilder();
            builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());
            builder.RegisterModule(new WebApiAutofacModule());
            var container = builder.Build();
            _httpConfig.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            app.UseAutofacMiddleware(container);
            app.UseAutofacWebApi(_httpConfig);
        }

        private void ConfigureSwagger()
        {
            _httpConfig.EnableSwagger(c =>
            {
                c.SingleApiVersion("v1", "Ticket.SaleWebApi");
                c.IncludeXmlComments(GetControllerXmlCommentsPath());
                c.UseFullTypeNameInSchemaIds();
            }).EnableSwaggerUi("docs/{*assetPath}", c => { c.DocExpansion(DocExpansion.List); });
        }

        private static string GetControllerXmlCommentsPath()
        {
            return $@"{AppDomain.CurrentDomain.BaseDirectory}\bin\Ticket.SaleWebApi.xml";
        }
    }
}