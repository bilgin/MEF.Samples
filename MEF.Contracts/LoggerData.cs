using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MEF.Contracts
{
   public class LoggerData
    {
        [DefaultValue("Unknown")]
        public string MetaValue { get; set; }
    }
}
