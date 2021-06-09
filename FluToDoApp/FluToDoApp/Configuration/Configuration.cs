using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace FluToDoApp.Configuration
{
    public class ConfigurationApp : IConfiguration
    {
        [JsonConstructor]
        public ConfigurationApp()
        {
            
        }

        public string ApiUrlBase { get; set; }
    }
}
