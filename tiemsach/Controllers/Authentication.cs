using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tiemsach.Data;

namespace tiemsach.Controllers
{
    public class Authentication : Controller
    {



        public IActionResult AdminView()
        {
            return View("_LayoutAdmin");
        }


        public IActionResult CustomerView()
        {
            return View("_LayoutCustomer");
        }
























    }
}
