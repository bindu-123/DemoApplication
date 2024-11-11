using DemoApplication.Business.Interface;
using DemoApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Core;

namespace DemoApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomeService _homeService;

        public HomeController(ILogger<HomeController> logger, IHomeService homeService)
        {
            _logger = logger;
            _homeService = homeService;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("Home Controller Index Method called");
            _homeService.PrintLog();
            _logger.LogInformation("After Home Service Print Log called");
            return View();
        }

        // for displaing error from Custom middleware
        // raised Divide By Zero Exception
        public IActionResult Privacy()
        {
            try
            {
                int y = 10;
                int x = y / 0;
                return View();
            } catch(Exception ex)
            {
                throw ex;
            }
          
        }
    }
}
