﻿using Microsoft.Owin;
using Owin;
using RegBot.RestApi.App_Start;
using System.Configuration;

[assembly: OwinStartup(typeof(RegBot.RestApi.Startup))]
[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace RegBot.RestApi
{
    /// <summary>
    /// Represents the entry point into an application.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Specifies how the ASP.NET application will respond to individual HTTP request.
        /// </summary>
        /// <param name="app">Instance of <see cref="IAppBuilder"/>.</param>
        public void Configuration(IAppBuilder app)
        {
            new ApiConfig(app)
                .ConfigureCorsMiddleware(ConfigurationManager.AppSettings["cors"])
                .ConfigureAufacMiddleware()
                .ConfigureFormatters()
                .ConfigureRoutes()
                .ConfigureExceptionHandling()
                .ConfigureSwagger()
                .UseWebApi();
        }
    }
}