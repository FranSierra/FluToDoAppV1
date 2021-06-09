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
        public string UrlBase { get; set; }
        public string ApiUrl { get; set; }

        public ConfigurationApp()
        {
            var embeddedResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("FluToDoApp.Configuration.config.json");
            if (embeddedResourceStream == null)
                return;

            using (var streamReader = new StreamReader(embeddedResourceStream))
            {
                var jsonString = streamReader.ReadToEnd();
                var configuration = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonString);

                if (configuration == null)
                    return;

                UrlBase = configuration["UrlBase"];
                ApiUrl = configuration["ApiUrl"];
            }
        }

    }
}
