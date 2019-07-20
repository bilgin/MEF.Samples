using MEF.Contracts;
using MEF.Providers;
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
        public IEnumerable<ILogger> Services { get; private set; }

        private void ComposeLoggers()
        {
            var assemblies = new List<Assembly>() { typeof(Program).GetTypeInfo().Assembly };
            var pluginAssemblies = Directory.GetFiles(@"..\Extensions\netstandard2.0", "*.dll", SearchOption.TopDirectoryOnly)
                .Select(AssemblyLoadContext.Default.LoadFromAssemblyPath)
                .Where(s => s.GetTypes().Where(p => typeof(ILogger).IsAssignableFrom(p)).Any());

            assemblies.AddRange(pluginAssemblies);

            var configuration = new ContainerConfiguration()
                .WithAssemblies(assemblies);

            using (var container = configuration.CreateContainer())
            {
                Services = container.GetExports<ILogger>();
            }
        }

        public IEnumerable<(string Id, string Value)> Log(string message)
        {
            return Services.Select(f => 
            new { Id = f.GetType().ToString(), Value = f.Log(message) }
            )
             .AsEnumerable()
             .Select(c => (c.Id, c.Value))
             .ToList();
        }
    }
}
