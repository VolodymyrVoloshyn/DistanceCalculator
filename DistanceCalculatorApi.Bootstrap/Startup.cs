using System;
using System.Configuration;
using System.Threading.Tasks;
using System.Web.Http;
using DistanceCalculator;
using Microsoft.Owin;
using Owin;
using StationProvider;
using Unity.WebApi;
using Microsoft.Practices.Unity;

[assembly: OwinStartup(typeof(WebApplication3.Startup))]

namespace WebApplication3
{
    public class Startup
    {
        private Lazy<IUnityContainer> _container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();


            RegisterTypes(container);
            return container;
        });

        private static void RegisterTypes(UnityContainer container)
        {
            container.RegisterType<IDistanceCalculator, KmDistanceCalculator>()
                .RegisterType<IStationDistanceCalculator, KmStationDistanceCalculator>()
                .RegisterType<IStationParcer<string>, StringStationParcer>()
					 .RegisterType<IStationDataSource, TxtStationDataSource>(
						  new InjectionConstructor(ConfigurationManager.AppSettings["stationFilePath"], typeof(IStationParcer<string>)))
					 .RegisterType<IStationProvider, TxtStationProvider>();
        }

        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            var container = _container.Value;

            config.DependencyResolver = new UnityDependencyResolver(container);

            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );


            app.UseWebApi(config);
        }
    }
}
