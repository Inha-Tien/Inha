using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Core;

namespace Inha.Commons.Log.Extensions
{
    public class SerilogHelper
    {
        private static Logger _logger;

        private SerilogHelper()
        {
        }

        public static Logger GetInstance(IConfiguration configuration)
        {
            if (_logger == null)
            {
                _logger = new LoggerConfiguration().ReadFrom.Configuration(configuration)
                                                   .CreateLogger();
            }

            return _logger;
        }

        public static Logger GetLogger()
        {
            return _logger;
        }
    }
}
