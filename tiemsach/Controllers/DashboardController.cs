using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using tiemsach.Data;
using tiemsach.Models;
using tiemsach.ViewModels;
using static NuGet.Packaging.PackagingConstants;


namespace tiemsach.Controllers
{


    [ServiceFilter(typeof(AdminRoleAttribute))]
    public class DashboardController : Controller
    {
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



            var phieuxuatData = _context.Phieuxuats.Where(p => p.Tinhtrang == true)
                .GroupBy( p => new { p.CreatedAt.Value.Year, p.CreatedAt.Value.Month })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    Count = g.Count()
                })
                .OrderBy(g => g.Year)
                .ThenBy(g => g.Month)
                .ToList();

            var chartLabels = phieuxuatData.Select(g => $"{g.Month}/{g.Year}").ToList();
            var chartData = phieuxuatData.Select(g => g.Count).ToList();


           

            var topCustomer = _context.Phieuxuats.Where(p => p.Tinhtrang == true)
                .Include(p => p.Khachhang)
                .ThenInclude(kh => kh.IdNavigation)
                .GroupBy(p => p.Khachhang.IdNavigation)
                .OrderByDescending(g => g.Count())
                .Take(5)
                .Select(g => new TopCustomer
                {
                    Ten = g.Key.Hoten,
                    SoLanMua = g.Count()
                })
                .ToList();
            var viewModel = new DashboardVM
            {
                TotalKH = TotalKH,
                TotalNV = TotalNV,
                TotalSach = TotalSach,
                TotalProfit = TotalProfit,
                LineChartLabels = chartLabels,
                LineChartData = chartData,
                TopCustomers = topCustomer
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
