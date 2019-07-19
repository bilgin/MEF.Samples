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
            // Catalogs does not exists in Dotnet Core, so you need to manage your own.
            var assemblies = new List<Assembly>() { typeof(Program).GetTypeInfo().Assembly };
            var pluginAssemblies = Directory.GetFiles(@"D:\berkay.bilgin\projects\dotnet-core\MEF.Samples\MEF.Web\Extensions\netstandard2.0", "*.dll", SearchOption.TopDirectoryOnly)
                .Select(AssemblyLoadContext.Default.LoadFromAssemblyPath)
                // Ensure that the assembly contains an implementation for the given type.
                .Where(s => s.GetTypes().Where(p => typeof(ILogger).IsAssignableFrom(p)).Any());

            assemblies.AddRange(pluginAssemblies);

            var configuration = new ContainerConfiguration()
                .WithAssemblies(assemblies);

            using (var container = configuration.CreateContainer())
            {
                Services = container.GetExports<ILogger>();
            }
        }

        public IEnumerable<(string Id, string Value)> GetAllLoggers()
        {
            return Services.Select(f => new { Id = f.GetType().ToString(), Value = f.Log() })
             .AsEnumerable()
             .Select(c => (c.Id, c.Value))
             .ToList();
        }
    }
}
