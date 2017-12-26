using System;
using System.Configuration;
using System.Threading.Tasks;
using System.Web.Http;
using DistanceCalculator;
using Microsoft.Owin;
using Owin;
using StationProvider;
using Unity.WebApi;
//using Microsoft.Practices.Unity;
using Unity;
using Unity.Injection;
using Unity.Lifetime;
using System.IO;

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

			#region Register station data source
				/*
				 * Register station data source
				 * Choose only one 
				*/

				/*
					// register station data source with file path
					.RegisterType<IStationDataSource, TxtStationDataSource>(
						new InjectionConstructor(ConfigurationManager.AppSettings["stationFilePath"], typeof(IStationParcer<string>)))
				*/

				/*
				// register station data source with TextReader 
				// and StreamReader as implementation TextReader + file path for it
				.RegisterType<TextReader, StreamReader>(
					new InjectionConstructor(ConfigurationManager.AppSettings["stationFilePath"]))
				.RegisterType<IStationDataSource, TxtTextReaderStationDataSource>()
				*/

				// register station data source with factory which returns TextReader
				// and StreamReader as implementation TextReader + file path for it
				// This case allows to read data multiple times from the same instance of data source
				.RegisterType<TextReader, StreamReader>(
					new InjectionConstructor(ConfigurationManager.AppSettings["stationFilePath"]))
				.RegisterType<IStationDataSource, TxtTextReaderStationFactoryDataSource>()

			#endregion

				.RegisterType<IStationProvider, TxtStationProvider>(new ContainerControlledLifetimeManager());
		}

		public void Configuration(IAppBuilder app)
		{
			var config = new HttpConfiguration();

			var container = _container.Value;

			config.DependencyResolver = new UnityDependencyResolver(container);

			config.MapHttpAttributeRoutes();
			//config.Routes.MapHttpRoute(
			//    name: "DefaultApi",
			//    routeTemplate: "api/{controller}/{action}/{id}",
			//    defaults: new { id = RouteParameter.Optional }
			//);


			app.UseWebApi(config);
		}
	}
}
