using System;
using System.Collections.Generic;
using System.Text;

namespace MEF.Providers.Abstract
{
    public interface ILogProvider
    {
        IEnumerable<(string Id, string Value)> LogControl();
        IEnumerable<string> LogAll(string message);
    }
}
