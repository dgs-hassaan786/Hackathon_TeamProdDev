using Foundation.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foundation.Extensions
{
    public class LoggingAttribute: Attribute
    {
        private ILog _logger;
        public LoggingAttribute(ILog logger)
        {
            _logger = logger;
        }
    }
}
