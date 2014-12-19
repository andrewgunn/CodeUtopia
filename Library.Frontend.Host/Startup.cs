using Autofac.Integration.SignalR;
using Library.Frontend.Host;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace Library.Frontend.Host
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var container = Container.Instance;

            GlobalHost.DependencyResolver = new AutofacDependencyResolver(container);
            app.UseAutofacMiddleware(container);

            app.MapSignalR();
        }
    }
}