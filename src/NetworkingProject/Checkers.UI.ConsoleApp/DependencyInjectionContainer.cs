using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Checkers.Domain.Interfaces;
using Checkers.Infrastructure.HttpClient;
using Checkers.Infrastructure.JsonDeserializer;
using Checkers.Services.HttpDeserializerService;
using Checkers.Services.Interfaces;
using Checkers.Infrastructure.Data;
using Unity;
using Unity.Injection;

namespace Checkers.UI.ConsoleApp
{
    static class DependencyInjectionContainer
    {
        private readonly static Lazy<IUnityContainer> _container = new Lazy<IUnityContainer>(
            () =>
            {
                var container = new UnityContainer();
                RegisterTypes(container);
                return container;
            });

        private static IUnityContainer GetConfiguredContainer()
        {
            return _container.Value;
        }

        public static T Get<T>()
        {
            return (T)GetConfiguredContainer().Resolve(typeof(T));
        }

        private static void RegisterTypes(IUnityContainer container)
        {
            string baseUrl = ConfigurationManager.AppSettings["BaseAPIUrl"];
            container.RegisterType<IHttpClient, HttpClient>();
            container.RegisterType<IDeserializer, JsonDeserializer>();
            container.RegisterType<IHttpDeserializerService, HttpDeserializer>(
                    new InjectionConstructor(
                        container.Resolve<IHttpClient>(),
                        container.Resolve<IDeserializer>()
                        )
                );
            container.RegisterType<ICheckersRepository, CheckersRepository>(
                    new InjectionConstructor(
                        baseUrl,
                        container.Resolve<IHttpDeserializerService>(),
                        container.Resolve<IHttpClient>()
                        )
                );
        }
    }
}
