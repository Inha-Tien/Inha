using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.DynamicProxy;
using AutoMapper;
using Castle.DynamicProxy;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Inha.Commons.BackgroudServices;
using Inha.Commons.Configurations;
using Inha.Commons.Dapper;
using Inha.Commons.Dispatchers;
using Inha.Commons.ErrorFilterWrapper;
using Inha.Commons.ErrorFilterWrapper.Models;
using Inha.Commons.ExceptionFilter;
using Inha.Commons.Kafka;
using Inha.Commons.Kafka.Models.Configs;
using Inha.Commons.Log.AutoWriteLog;
using Inha.Commons.RestEase;
using Inha.Commons.Swaggers;
using Inha.Commons.Swaggers.Extensions;
using Inha.Commons.Types;

namespace Inha.Services.Demo
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "OpsWebOrigins";

        public Startup( /*IConfiguration configuration*/ IHostingEnvironment env)
        {
            //Configuration = configuration;

            var builder = new ConfigurationBuilder();
            builder.SetBasePath(env.ContentRootPath)
                   .AddEnvironmentVariables();
            builder.AddJsonFile($"appsettings.{env.EnvironmentName}.json", false, true);
            Configuration = builder.Build();
        }

        public IContainer Container { get; private set; }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {


            var domains = Configuration.GetEx("AllowDomains")
                    .Split(";");
            services.AddCors(options =>
                             {
                                 options.AddPolicy(MyAllowSpecificOrigins,
                                                   builder1 =>
                                                   {
                                                       builder1.WithOrigins(domains);
                                                       builder1.AllowAnyHeader();
                                                       builder1.AllowAnyMethod();
                                                   });
                             });

            services.Configure<CookiePolicyOptions>(options =>
                                                    {
                                                        // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                                                        options.CheckConsentNeeded = context => true;
                                                        options.MinimumSameSitePolicy = SameSiteMode.None;
                                                    });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


            services.AddMvc()
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddMetricsTrackingMiddleware();
            services.AddMvcCore()
                    .AddJsonFormatters();

            services.Configure<ApiBehaviorOptions>(options =>
                                                   {
                                                       options.SuppressModelStateInvalidFilter = true;
                                                   });
            services.AddSignalR();

            #region Swagger

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine("App_Data", xmlFile);

            var scopes = new Dictionary<string, string>();

            services.AddSwagger(Configuration.ConfigApiVersions(), scopes, xmlPath);

            #endregion


            services.AddSingleton(Configuration);
            //services.AddScheduler((sender, args) =>
            //{
            //    Console.Write(args.Exception.Message);
            //    args.SetObserved();
            //});

            //services.RegisterServiceForwarder<IHandheldsService>("handheld-service");
            // register by autofac
            var builder = new ContainerBuilder();

            builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly())
                   .AsImplementedInterfaces()
                   .EnableInterfaceInterceptors()
                   .InterceptedBy("log-calls");

            builder.Register(c => new DynamicProxyLog(new DynamicProxyAsyncLog(Configuration)))
                   .Named<IInterceptor>("log-calls");

            //// Đăng ký tự động các Services Kafka trong project common
            //builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(BusPublisher)))
            //       .AsImplementedInterfaces();

            //builder.RegisterType<ApplyWorkInstructionPending>().Keyed<IApplyWorkInstructionReport>(EventStoreNames.WORKINSTRUCTION_TRAILER_PENDING);
            //builder.RegisterType<ApplyWorkInstructionInprogress>().Keyed<IApplyWorkInstructionReport>(EventStoreNames.WORKINSTRUCTION_TRAILER_INPROGRESS);
            //builder.RegisterType<ApplyWorkInstructionDone>().Keyed<IApplyWorkInstructionReport>(EventStoreNames.WORKINSTRUCTION_TRAILER_DONE);

            builder.Populate(services);
            builder.AddDispatchers();

            Container = builder.Build();


            return new AutofacServiceProvider(Container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
                              IHostingEnvironment env)
        {
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();

            #region Use Swagger

            app.UseSwaggerExtension(string.Empty, string.Empty, Configuration.ConfigDocumentSwagger());

            #endregion

            app.UseCors(MyAllowSpecificOrigins);
            app.UseMvc(routes =>
                       {
                           routes.MapRoute(
                                           name: "default",
                                           template: "{controller=Home}/{action=Index}/{id?}");
                       });
        }
    }
}
