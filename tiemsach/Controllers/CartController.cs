using Microsoft.AspNetCore.Mvc;
using tiemsach.Data;
using tiemsach.ViewModels;
using tiemsach.Helper;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace tiemsach.Controllers
{
    public class CartController : Controller
    {
        private readonly TiemsachContext db;


        public CartController(TiemsachContext context) {

            db = context;
        
        }
     

        public IActionResult AddToCart(int id, int quantity = 1)
        {
			ViewData["Layout"] = "_LayoutCustomer";

            if (!User.Identity.IsAuthenticated)
            {
                TempData["ErrorMessage"] = "Login before add to cart";
                //return View("/Views/Shared/_Login.cshtml");

                return RedirectToAction("Login", "Home");



            }
            
            // Retrieve the cart from the cookie
            var cart = GetCart();

            // Check if the item already exists in the cart
            var cartItem = cart.FirstOrDefault(c => c.Id == id);
            if (cartItem != null)
            {
                // Update the quantity if it already exists
                cartItem.SoLuong += quantity;
            }
            else
            {
                // Add new item to the cart
                cart.Add(new CartItem { Id = id, SoLuong = quantity });
            }

            // Save the updated cart back to the cookie
            SaveCart(cart);

            return RedirectToAction("Index", "Home");
        }


        private List<CartItem> GetCart()
        {
			ViewData["Layout"] = "_LayoutCustomer";
			var cartJson = Request.Cookies["Cart"];
            if (string.IsNullOrEmpty(cartJson))
            {
                return new List<CartItem>();
            }

            return JsonConvert.DeserializeObject<List<CartItem>>(cartJson);
        }

        private void SaveCart(List<CartItem> cart)
        {
			ViewData["Layout"] = "_LayoutCustomer";
			var cartJson = JsonConvert.SerializeObject(cart);
            Response.Cookies.Append("Cart", cartJson, new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddDays(30) // Set expiration as needed
            });
        }


        public IActionResult ViewCart()
        {
       
            ViewData["Layout"] = "_LayoutCustomer";
            var cart = GetCart();
            var cartViewModel = new CartViewModel
            {
                Items = new List<CartItem>(), // Initialize the list of items
                TotalPrice = 0 // Initialize total price
            };

            // Fetch the prices for each item in the cart
            foreach (var item in cart)
            {
                var book = db.Saches.FirstOrDefault(s => s.Id == item.Id);
                if (book != null)
                {
                    // Create a new CartItem with the price
                    var cartItem = new CartItem
                    {
                        Id = Convert.ToInt32(book.Id),

                        TenSach= book.Ten,

                        SoLuong = item.SoLuong,
                        GiaXuat = book.Giaxuat // Get the selling price
                    };

                    // Add to the cart items
                    cartViewModel.Items.Add(cartItem);

                    // Calculate total price for this item
                    cartViewModel.TotalPrice += cartItem.GiaXuat * cartItem.SoLuong;
                }
            }
            return View("Views/Customer/Cart.cshtml", cartViewModel);
		
        }


        [HttpPost]
        public async Task<IActionResult> CreatePhieuXuat(string KhachhangId)
        {
            ViewData["Layout"] = "_LayoutCustomer";
            if (!User.Identity.IsAuthenticated)
            {
                TempData["ErrorMessage"] = "Login before checkout";
                return RedirectToAction("Login", "Home");
            }

            var cart = GetCart();
            if (cart.Count == 0)
            {
                // Optionally, you can add a message to inform the user
                TempData["ErrorMessage"] = "Your cart is empty. Please add items to your cart before checking out.";
                return RedirectToAction("ViewCart"); // Redirect to the cart view
            }

            // Step 1: Create a new PhieuXuat
            var phieuXuat = new Phieuxuat
            {
                KhachhangId = Convert.ToInt64(KhachhangId),
                NhanvienId = 22,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Tinhtrang = false 
            };

            db.Phieuxuats.Add(phieuXuat);
            await db.SaveChangesAsync(); // Save to get the ID




            foreach (var item in cart)
            {
                var book = await db.Saches.FindAsync(Convert.ToInt64(item.Id));
                if (book != null)
                {
                    // Step 3: Create Chitietphieuxuat entries
                    var chitietphieuxuat = new Chitietphieuxuat
                    {
                        PhieuxuatId = phieuXuat.Id,
                        SachId = book.Id,
                        Soluong = item.SoLuong,
                        Tinhtrang = true,
                        Giaxuat = book.Giaxuat
                    };

                    db.Chitietphieuxuats.Add(chitietphieuxuat);              
                    book.Soluong -= item.SoLuong;
                }
            }

            await db.SaveChangesAsync(); 

       
            SaveCart(new List<CartItem>()); 

            return RedirectToAction("Index", "Home");
        }




            public IActionResult ClearCart()
            {
                // Clear the cart in the cookie
                Response.Cookies.Delete("Cart");

                return RedirectToAction("ViewCart"); // Redirect to the cart view or any other page
            }
 

    }
}
