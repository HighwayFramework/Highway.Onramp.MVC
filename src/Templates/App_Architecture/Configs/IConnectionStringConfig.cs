using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Templates.App_Architecture.Configs
{
    public interface IConnectionStringConfig
    {
        string ConnectionString { get; set; }
    }
}