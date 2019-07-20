using System;
using System.Collections.Generic;
using System.Text;

namespace MEF.Core.MEF.Abstract
{
    public interface IProviderService<T>
    {
        IEnumerable<T> Services { get; set; }
        void Compose();
    }
}
