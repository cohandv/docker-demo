using System;
using Autofac.Core;
using Autofac;
using webapi.Configuration;
using Microsoft.Extensions.Configuration;

namespace webapi
{
    internal class AutofacModule : Module
    {
        private IConfigurationRoot _configuration { get; set; }

        public AutofacModule(IConfigurationRoot config)
        {
            _configuration = config;
        }
        protected override void Load(ContainerBuilder builder)
        {
            var dd = _configuration["consul_refresh_interval_in_seconds"];
            // The generic ILogger<TCategoryName> service was added to the ServiceCollection by ASP.NET Core.
            // It was then registered with Autofac using the Populate method in ConfigureServices.
            builder.Register(c => new ConsulConfiguratorService(Convert.ToInt32(_configuration["consul_refresh_interval_in_seconds"])))
                .As<webapi.Configuration.IConfigurationService>()
                .InstancePerLifetimeScope();
        }
    }
}