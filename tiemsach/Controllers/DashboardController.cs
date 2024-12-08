using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using tiemsach.Data;
using tiemsach.Models;
using tiemsach.ViewModels;
using static NuGet.Packaging.PackagingConstants;


namespace tiemsach.Controllers
{
    public class DashboardController : Controller
    {
        //private readonly ILogger<DashboardController> _logger;
      
        //public DashboardController(ILogger<DashboardController> logger)
        //{
        //    _logger = logger;
        //}







        private readonly TiemsachContext _context;
        public DashboardController(TiemsachContext context)
        {
            _context = context;

        }





        public IActionResult Index()
        {


            ViewData["Layout"] = "_LayoutAdmin";





            var kh = _context.Khachhangs.ToList();
            var nv = _context.Nhanviens.ToList();
            var s = _context.Saches.ToList();
            var ctpx = _context.Chitietphieuxuats.ToList();
    
            
      


            var TotalProfit = ctpx.Sum(ct => ct.Soluong * ct.Giaxuat) == null ? 0.0 : ctpx.Sum(ct => ct.Soluong * ct.Giaxuat);
          
      


          
            var TotalKH = kh.Count;
            var TotalNV = nv.Count;
            var TotalSach = s.Count;


            var viewModel = new DashboardVM
            {
                TotalKH= TotalKH,
                TotalNV = TotalNV,
                TotalSach = TotalSach,
                TotalProfit = TotalProfit
            };




            return View(viewModel);
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
