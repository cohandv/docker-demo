using Consul;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace webapi.Configuration
{
    public class ConsulConfiguratorService : IConfigurationService
    {
        private static string _lock = "does not care";
        private static int _refreshTimeInMilliseconds { get; set; }
        private static Dictionary<string, object> _configurations { get; set; }

        private static string _consulServer { get; set; }

        private static string _consulKey {get;set;}


        public ConsulConfiguratorService(string consulServer, string consulKey, int refreshTimeInMilliseconds= 1000)
        {
            _consulServer = consulServer;
            _consulKey = consulKey;
            _refreshTimeInMilliseconds = refreshTimeInMilliseconds;
            Build();
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
                Build();

                Thread.Sleep(_refreshTimeInMilliseconds);
            } while (true);
        }

        private static void Build()
        {
            var _tmpConfigurations = new Dictionary<string, object>();
            var consulCfg = new ConsulClientConfiguration();
            consulCfg.Address = new Uri(_consulServer);
            using (var client = new ConsulClient(consulCfg))
            {
                var getPair = client.KV.Get(_consulKey).GetAwaiter().GetResult();
                var data = Encoding.UTF8.GetString(getPair.Response.Value, 0, getPair.Response.Value.Length);
                _tmpConfigurations = JsonConvert.DeserializeObject<Dictionary<string, object>>(data);
            }

            lock (_lock)
            {
                _configurations = _tmpConfigurations;
            }
        }
    }
}
