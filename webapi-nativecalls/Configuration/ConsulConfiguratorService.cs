using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace webapi.Configuration
{
    public class ConsulConfiguratorService : IConfigurationService
    {
        private const string _consulServer = "consul:8500";

        private static string _lock="does not care";

        private static int _refreshTimeInMilliseconds { get; set; }
        private static Dictionary<string,object> _configurations { get; set; }

        public ConsulConfiguratorService(int refreshTimeInMilliseconds=1000)
        {
            _refreshTimeInMilliseconds = refreshTimeInMilliseconds;
            new Thread(new ThreadStart(RefreshConfigs)).Start();
        }

        public object GetValue(string key)
        {
            lock (_lock)
            {
                var val = _configurations == null ? string.Empty : _configurations[key];
                return val;
            }
        }

        private static void RefreshConfigs()
        {
            do
            {
                var _tmpConfigurations = new Dictionary<string, object>();
                _tmpConfigurations.Add("david", "static"+new Random().Next());

                lock (_lock)
                {
                    _configurations = _tmpConfigurations;
                }

                Thread.Sleep(_refreshTimeInMilliseconds);
            } while (true);
        }
    }
}
