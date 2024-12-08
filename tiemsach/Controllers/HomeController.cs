using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using tiemsach.Models;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Security.Claims;
using System.Security.Cryptography;
using tiemsach.Data;
using tiemsach.ViewModels;


namespace tiemsach.Controllers
{
    public class HomeController : Controller
    {



        private readonly TiemsachContext _context;

        public HomeController(TiemsachContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Layout"] = "_LayoutCustomer";
            var books = await _context.Saches.Include(s => s.Loaisach).Include(s => s.Tacgia).ToListAsync();

            ViewData["categories"] = await _context.Loaisaches
                .Select(nxb => new SelectListItem
                {
                    Value = nxb.Id.ToString(),
                    Text = nxb.Ten
                })
                .ToListAsync();

            return View("/Views/Customer/Home.cshtml", books);
        }

        public async Task<IActionResult> Detail(long id)
        {
            ViewData["Layout"] = "_LayoutCustomer";
            var book = await _context.Saches
                .Include(s => s.Tacgia)
                .Include(s => s.Loaisach)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            return View("Views/Customer/Detail.cshtml", book);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



        [HttpGet]
        public IActionResult Register()
        {
            ViewData["Layout"] = "_LayoutCustomer";
            ViewData["DiaChiId"] = new SelectList(_context.Diachis, "Id", "Ten");
            return View("/Views/Shared/_Register.cshtml");

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DangKy(RegisterVM registerVM, long diaChiId)
        {
            ViewData["Layout"] = "_LayoutCustomer";


            var diaChi = await _context.Diachis.FindAsync(diaChiId);


            if (ModelState.IsValid)
            {
                // Create Nguoidung from RegisterVM
                var nguoidung = new Nguoidung
                {
                    Hoten = registerVM.Hoten,
                    Gioitinh = registerVM.Gioitinh,
                    Sodienthoai = registerVM.Sodienthoai,
                    Email = registerVM.Email,
                    Password = registerVM.Password, // Consider hashing the password before saving
                    QuyenId = 5,
                    Diachi = diaChi.Ten,
                    Vaitro = false,
                    Tinhtrang = true,
                    CreatedAt = DateTime.Now
                };

                _context.Add(nguoidung);
                await _context.SaveChangesAsync();

                // Create Khachhang
                var khachhang = new Khachhang
                {
                    Id = nguoidung.Id,
                    DiachiId = diaChiId,
                    Tinhtrang = true,
                    CreatedAt = DateTime.Now
                };

                _context.Khachhangs.Add(khachhang);
                await _context.SaveChangesAsync();

                return View("/Views/Shared/_Login.cshtml");
            }

            ViewData["DiaChiId"] = new SelectList(_context.Diachis, "Id", "Ten");
            return Register();
        }

        [HttpGet]
        public IActionResult Login(string? ReturnUrl)
        {
            ViewData["Layout"] = "_LayoutCustomer";
            ViewBag.ReturnUrl = ReturnUrl;
            return View("/Views/Shared/_Login.cshtml");
        }



        [HttpPost]
        public async Task<IActionResult> DangNhap(LoginVM model, string? ReturnUrl)
        {
            ViewData["Layout"] = "_LayoutCustomer";
            ViewBag.ReturnUrl = ReturnUrl;
            if (ModelState.IsValid)
            {
                var nguoiDung = _context.Nguoidungs.SingleOrDefault(kh => kh.Email == model.Email);
                if (nguoiDung == null)
                {
                    ModelState.AddModelError("loi", "Khong tim thay tai khoan");
                }
                else
                {
                    if (!nguoiDung.Tinhtrang)
                    {
                        ModelState.AddModelError("loi", "Tai khoan da bi khoa");
                    }
                    else
                    {
                        if (nguoiDung.Password != model.Password)
                        {
                            ModelState.AddModelError("loi", "Sai mat khau");
                        }
                        else
                        {
                            var claims = new List<Claim>
                            {
                
                                new Claim(ClaimTypes.Email, nguoiDung.Email),
                                new Claim(ClaimTypes.Name, nguoiDung.Hoten),
                                new Claim("Vaitro", nguoiDung.Vaitro.ToString()),
                                new Claim("ID", nguoiDung.Id.ToString()),
                                new Claim(ClaimTypes.Role, nguoiDung.Vaitro ? "true" : "false")
                            };






                            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                            await HttpContext.SignInAsync(claimsPrincipal);




                            if (nguoiDung.Vaitro == false)
                            {
                                // Set the layout for Customer
                                //ViewData["Layout"] = "~/Views/Shared/_LayoutCustomer.cshtml";
                                ViewData["Layout"] = "_LayoutCustomer";
                                return RedirectToAction("Index", "Home");
                            }




                            else if (nguoiDung.Vaitro == true)
                            {
                                // Set the layout for Admin
                                //ViewData["Layout"] = "~/Views/Shared/_LayoutAdmin.cshtml";

                                ViewData["Layout"] = "_LayoutAdmin";

                                return RedirectToAction("Index", "Dashboard");
                            }

                        }
                    }
                }

            }
            return View("/Views/Shared/_Login.cshtml");
        }

        [Authorize]
        public async Task<IActionResult> DangXuat()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }



    }

}
