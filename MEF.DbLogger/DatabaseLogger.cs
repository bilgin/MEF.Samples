using MEF.Contracts;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Text;

namespace MEF.DbLogger
{
    [Export(typeof(ILogger))]
    [ExportMetadata("Status", "Active")]
    public class DatabaseLogger : ILogger
    {
        public string Log(string message)
        {
            return $"{message} Database Logger";
        }
    }
}
