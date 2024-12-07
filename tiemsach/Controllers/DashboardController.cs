using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using tiemsach.Models;

namespace tiemsach.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ILogger<DashboardController> _logger;
      
        public DashboardController(ILogger<DashboardController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {

            ViewData["Layout"] = "_LayoutAdmin";
            return View();
        }

        public IActionResult Privacy()
        {

            ViewData["Layout"] = "_LayoutAdmin";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {

            ViewData["Layout"] = "_LayoutAdmin";
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
