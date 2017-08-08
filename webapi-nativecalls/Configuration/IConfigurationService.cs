using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webapi.Configuration
{
    public interface IConfigurationService
    {
        object GetValue(string key);
    }
}
