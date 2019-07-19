using MEF.Contracts;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Text;

namespace MEF.DbLogger
{
    [Export(typeof(ILogger))]
    public class DatabaseLogger : ILogger
    {
        public string Log()
        {
            string newMessage ="Database Log";
            Console.WriteLine(newMessage);
            return newMessage;
        }
    }
}
