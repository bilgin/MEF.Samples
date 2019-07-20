using MEF.Contracts;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Text;

namespace MEF.FileLogger
{
    [Export(typeof(ILogger))]
    public class JsonLogger : ILogger
    {
        public string Log(string message)
        {
            return $"{message} File Logger";
        }
    }
}
