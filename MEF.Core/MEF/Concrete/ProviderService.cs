using MEF.Core.MEF.Abstract;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Composition;
using System.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace MEF.Core.MEF.Concrete
{
    /// <summary>
    /// Provider Service
    /// </summary>
    /// <typeparam name="T">MetaData Parameter : Example - Lazy<ILogger,LoggerData></typeparam>
    /// <typeparam name="I">Interface Parameter : Example - ILogger </typeparam>
    public abstract class ProviderService<T,I> : IProviderService<T>
    {
        [ImportMany]
        public abstract IEnumerable<T> Services { get; set; }

        public ProviderService()
        {
            Compose();
        }

        public void Compose()
        {
            var assemblies = new List<Assembly>() { System.Reflection.Assembly.GetExecutingAssembly() };
            
            var pluginAssemblies = Directory.GetFiles(@"D:\berkay.bilgin\projects\dotnet-core\MEF.Samples\MEF.Web\Extensions\netstandard2.0", "*.dll",
                SearchOption.TopDirectoryOnly)
                .Select(AssemblyLoadContext.Default.LoadFromAssemblyPath)
                .Where(s => s.GetTypes().Where(p => typeof(I).IsAssignableFrom(p)).Any());

            assemblies.AddRange(pluginAssemblies);

            var configuration = new ContainerConfiguration()
                .WithAssemblies(assemblies);

            using (CompositionHost container = configuration.CreateContainer())
            {
                Services = container.GetExports<T>();
            }
        }
    }
}
