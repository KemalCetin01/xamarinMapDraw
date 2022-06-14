using DynamicAPI.Core.CrossCuttingConcerns.Logging.Log4Net;
using System;
using System.Collections.Generic;
using System.Text;

namespace DynamicAPI.Core.CrossCuttingConcerns.Logging.Log4Net.Loggers
{
    public class DatabaseLogger:LoggerServiceBase
    {
        public DatabaseLogger() : base("DatabaseLogger")
        {
        }
    }
}
