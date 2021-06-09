using Autofac;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Module = Autofac.Module;

namespace FluToDoApp.Configuration
{
    public class ConfigurationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Get and deserialize config.json file from Configuration folder.
            var embeddedResourceStream = Assembly.GetAssembly(typeof(IConfiguration)).GetManifestResourceStream("FluToDoApp.Configuration.config.json");
            if (embeddedResourceStream == null)
                return;

            using (var streamReader = new StreamReader(embeddedResourceStream))
            {
                var jsonString = streamReader.ReadToEnd();
                var configuration = JsonConvert.DeserializeObject<ConfigurationApp>(jsonString);

                if (configuration == null)
                    return;

                builder.Register<IConfiguration>(c => configuration).SingleInstance();
            }
        }
    }
}
