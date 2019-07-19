using System;
using System.Collections.Generic;
using System.Text;

namespace MEF.Providers
{
    public interface ILogProvider
    {
        IEnumerable<(string Id, string Value)> GetAllLoggers();
    }
}
