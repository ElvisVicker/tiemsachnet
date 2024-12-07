using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Security.Claims;
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

        public IActionResult Index()
        {
            ViewData["Layout"] = "_LayoutCustomer";
            return View("/Views/Customer/Home.cshtml");
        }



		//public IActionResult Login()
		//{
		//	return View("/Views/Shared/_Login.cshtml");
		//}


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
            ViewBag.ReturnUrl = ReturnUrl;
            if (ModelState.IsValid) {
                var nguoiDung = _context.Nguoidungs.SingleOrDefault(kh => kh.Email == model.Email);
                if (nguoiDung == null) {
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
                        if(nguoiDung.Password != model.Password)
                        {
                            ModelState.AddModelError("loi", "Sai mat khau");
                        }
                        else
                        {
                            var claims = new List<Claim>
                            {
                                new Claim(ClaimTypes.Email, nguoiDung.Email),
                                new Claim(ClaimTypes.Name, nguoiDung.Hoten),

                                  new Claim("ID", nguoiDung.Id.ToString()),
                                new Claim(ClaimTypes.Role, "Customer")
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
            return View("/Views/Customer/Home.cshtml");
        }

    }
}
