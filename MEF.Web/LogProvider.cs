using MEF.Contracts;
using MEF.Providers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Composition;
using System.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace MEF.Web
{
    public class LogProvider : ILogProvider
    {
        public LogProvider()
        {
            ComposeLoggers();
        }

        [ImportMany]
        public IEnumerable<Lazy<ILogger, LoggerData>> Services { get; private set; }

        private void ComposeLoggers()
        {
            var assemblies = new List<Assembly>() { System.Reflection.Assembly.GetExecutingAssembly() };

            var pluginAssemblies = Directory.GetFiles(@"D:\berkay.bilgin\projects\dotnet-core\MEF.Samples\MEF.Web\Extensions\netstandard2.0", "*.dll",
                SearchOption.TopDirectoryOnly)
                .Select(AssemblyLoadContext.Default.LoadFromAssemblyPath)
                .Where(s => s.GetTypes().Where(p => typeof(ILogger).IsAssignableFrom(p) && p.HasMetadataToken()).Any());

            assemblies.AddRange(pluginAssemblies);

            var configuration = new ContainerConfiguration()
                .WithAssemblies(assemblies);

            using (var container = configuration.CreateContainer())
            {
                Services = container.GetExports<Lazy<ILogger, LoggerData>>();
            }
        }

        public IEnumerable<(string Id, string Value)> Log(string message)
        {
            List<(string Id, string Value)> loggers = new List<(string Id, string Value)>();

            foreach (Lazy<ILogger,LoggerData> item in Services)
            {
                if (item.Metadata.MetaValue.Equals("Unknown"))
                {
                    loggers.Add((item.Value.GetType().ToString(), item.Value.Log("Geçersiz log")));
                }
                else if (item.Metadata.MetaValue.Equals("Success"))
                {
                    loggers.Add((item.Value.GetType().ToString(), item.Value.Log("Başarılı log kaydı yapıldı    : ")));
                }
            }

            return loggers;

        }
    }
}
