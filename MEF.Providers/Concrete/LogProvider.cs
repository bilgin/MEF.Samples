using MEF.Contracts;
using MEF.Core.MEF.Concrete;
using MEF.Providers;
using MEF.Providers.Abstract;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Composition;
using System.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace MEF.Providers.Concrete
{
    public class LogProvider : ProviderService<Lazy<ILogger, LoggerData>, ILogger>, ILogProvider
    {
        public override IEnumerable<Lazy<ILogger, LoggerData>> Services { get; set; }

        public IEnumerable<(string Id, string Value)> LogControl()
        {
            List<(string Id, string Value)> loggers = new List<(string Id, string Value)>();

            foreach (Lazy<ILogger, LoggerData> item in Services)
            {
                if (item.Metadata.Status.Equals("Passive"))
                {
                    loggers.Add((item.Value.GetType().ToString(), "Passive log module"));
                }
                else if (item.Metadata.Status.Equals("Active"))
                {
                    loggers.Add((item.Value.GetType().ToString(), "Active log module"));
                }
            }

            return loggers;

        }

        public IEnumerable<string> LogAll(string message)
        {
            List<string> loggerResults = new List<string>();

            foreach (Lazy<ILogger,LoggerData> item in Services.Where(x => x.Metadata.Status.Equals("Active")))
            {
                loggerResults.Add(item.Value.Log(message));
            };

            return loggerResults;
        }
    }
}
