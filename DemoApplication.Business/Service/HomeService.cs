using DemoApplication.Business.Interface;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApplication.Business.Service
{
    public class HomeService : IHomeService
    {
        private readonly ILogger<HomeService> _logger;
        public HomeService(ILogger<HomeService> logger) {
            _logger = logger;
        }

        public void PrintLog()
        {
            _logger.LogInformation("Print Log Called");
            _logger.LogCritical("Log: When Some Error Occured");
            _logger.LogError("Log: Error");
        }
    }
}
