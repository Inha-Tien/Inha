using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RestEase;
using Inha.Commons.Utils;
using System;
using System.Linq;
using System.Net.Http;
using Inha.Commons.Configurations;

namespace Inha.Commons.RestEase
{
    public static class Extensions
    {
        public static void RegisterServiceForwarder<T>(this IServiceCollection services, string serviceName)
            where T : class
        {
            var clientName = typeof(T).ToString();
            var options = ConfigureOptions(services);
            switch (options.LoadBalancer?.ToLowerInvariant())
            {
                case "consul":
                    ConfigureConsulClient(services, clientName, serviceName);
                    break;
                case "fabio":
                    ConfigureFabioClient(services, clientName, serviceName);
                    break;
                default:
                    ConfigureDefaultClient(services, clientName, serviceName, options);
                    break;
            }

            ConfigureForwarder<T>(services, clientName);
        }

        private static RestEaseOptions ConfigureOptions(IServiceCollection services)
        {
            IConfiguration configuration;
            using (var serviceProvider = services.BuildServiceProvider())
            {
                configuration = serviceProvider.GetService<IConfiguration>();
            }

            services.Configure<RestEaseOptions>(configuration.GetSection("restEase"));

            var restEaseOptions = configuration.GetOptions<RestEaseOptions>("restEase");

            restEaseOptions.LoadBalancer = configuration.GetEx("restEase:loadBalancer");
            foreach (var service in restEaseOptions.Services)
            {
                service.Host = configuration.GetEx(service.Host, "restEase:services:host");
                service.Name = configuration.GetEx(service.Name, "restEase:services:name");
                service.Scheme = configuration.GetEx(service.Scheme, "restEase:services:scheme");
                service.Port = configuration.GetEx(service.Port, "restEase:services:port");
            }
            return restEaseOptions;
        }

        private static void ConfigureConsulClient(IServiceCollection services, string clientName,
            string serviceName)
        {
            //services.AddHttpClient(clientName)
            //    .AddHttpMessageHandler(c =>
            //        new ConsulServiceDiscoveryMessageHandler(c.GetService<IConsulServicesRegistry>(),
            //            c.GetService<IOptions<ConsulOptions>>(), serviceName, overrideRequestUri: true));
        }

        private static void ConfigureFabioClient(IServiceCollection services, string clientName,
            string serviceName)
        {
            //services.AddHttpClient(clientName)
            //    .AddHttpMessageHandler(c =>
            //        new FabioMessageHandler(c.GetService<IOptions<FabioOptions>>(), serviceName));
        }

        private static void ConfigureDefaultClient(IServiceCollection services, string clientName,
            string serviceName, RestEaseOptions options)
        {
            services.AddHttpClient(clientName, client =>
            {
                var service = options.Services.SingleOrDefault(s => s.Name.Equals(serviceName,
                    StringComparison.InvariantCultureIgnoreCase));
                if (service == null)
                {
                    throw new RestEaseServiceNotFoundException($"RestEase service: '{serviceName}' was not found.",
                        serviceName);
                }

                client.BaseAddress = new UriBuilder
                {
                    Scheme = service.Scheme,
                    Host = service.Host,
                    Port = Convert.ToInt32(service.Port)
                }.Uri;
            });
        }

        private static void ConfigureForwarder<T>(IServiceCollection services, string clientName) where T : class
        {
            services.AddTransient<T>(c => new RestClient(c.GetService<IHttpClientFactory>().CreateClient(clientName))
            {
                RequestQueryParamSerializer = new QueryParamSerializer()
            }.For<T>());
        }
    }
}