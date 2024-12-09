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




            var phieuxuatData = _context.Phieuxuats.ToList();
            var chartLabels = new List<string>();
            var chartData = new List<int>();

            // Group by year or month and count
            var groupedData = phieuxuatData.GroupBy(p => p.CreatedAt) // Adjust this based on your date property
                                            .Select(g => new
                                            {
                                                Month = g.Key,
                                                Count = g.Count()
                                            })
                                            .OrderBy(g => g.Month);

            foreach (var item in groupedData)
            {
                chartLabels.Add(item.Month.ToString());
                chartData.Add(item.Count);
            }


            var viewModel = new DashboardVM
            {
                TotalKH = TotalKH,
                TotalNV = TotalNV,
                TotalSach = TotalSach,
                TotalProfit = TotalProfit,
                LineChartLabels = chartLabels,
                LineChartData = chartData,
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
