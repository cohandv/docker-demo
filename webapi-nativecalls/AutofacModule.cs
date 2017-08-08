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
            var refreshInterval = Convert.ToInt32(_configuration["consul_refresh_interval_in_seconds"]);
            var consulServer = _configuration["consulServer"];
            var consulKey = _configuration["consulKey"];

            builder.Register(c => new ConsulConfiguratorService(consulServer, consulKey, refreshInterval))
                .As<webapi.Configuration.IConfigurationService>()
                .InstancePerLifetimeScope();
        }
    }
}