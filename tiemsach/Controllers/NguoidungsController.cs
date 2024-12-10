using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using tiemsach.Data;
using tiemsach.ViewModels;

namespace tiemsach.Controllers
{
    public class NguoidungsController : Controller
    {
        private readonly TiemsachContext _context;
        private readonly ILogger<NguoidungsController> _logger;
        public NguoidungsController(TiemsachContext context, ILogger<NguoidungsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Nguoidungs
        public async Task<IActionResult> Index()
        {
            ViewData["Layout"] = "_LayoutAdmin";
            var tiemsachContext = _context.Nguoidungs.Include(n => n.Quyen).Where(q => q.Id != 22);
            return View(await tiemsachContext.ToListAsync());
        }

        // GET: Nguoidungs/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewData["Layout"] = "_LayoutAdmin";
            var nguoidung = await _context.Nguoidungs
                .Include(n => n.Quyen)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nguoidung == null)
            {
                return NotFound();
            }

            return View(nguoidung);
        }

        // GET: Nguoidungs/Create
        public IActionResult Create()
        {
            ViewData["QuyenId"] = new SelectList(_context.Quyens, "Id", "Id");
            return View();
        }

        // POST: Nguoidungs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,QuyenId,Hoten,Gioitinh,Vaitro,Sodienthoai,Diachi,Tinhtrang,Image,Email,Password,CreatedAt,UpdatedAt,DeletedAt")] Nguoidung nguoidung)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nguoidung);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["QuyenId"] = new SelectList(_context.Quyens, "Id", "Id", nguoidung.QuyenId);
            return View(nguoidung);
        }

        // GET: Nguoidungs/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            Console.WriteLine($"Received id: {id}");
            if (id == null)
            {
           
                return NotFound();
            }
     
            ViewData["Layout"] = "_LayoutAdmin";
            var nguoidung = await _context.Nguoidungs.FindAsync(id);
            if (nguoidung == null)
            {
       
                return NotFound();
            }
            ViewData["QuyenId"] = new SelectList(_context.Quyens.Where(q => q.Id != 5), "Id", "Ten");
            return View(nguoidung);
        }

        // POST: Nguoidungs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken] 
        public async Task<IActionResult> Edit(long id, [Bind("Id,QuyenId,Hoten,Gioitinh,Vaitro,Sodienthoai,Diachi,Tinhtrang,Image,Email,Password,CreatedAt,UpdatedAt,DeletedAt")] Nguoidung nguoidung)
        {
            ViewData["Layout"] = "_LayoutAdmin";
            if (ModelState.IsValid)
            { 

                var updateNguoiDung = await _context.Nguoidungs.FirstOrDefaultAsync(p => p.Id == id);

            if (updateNguoiDung == null)
            {
                return NotFound();
            }

            updateNguoiDung.UpdatedAt = DateTime.Now;
            updateNguoiDung.Hoten = nguoidung.Hoten;
                updateNguoiDung.Gioitinh = nguoidung.Gioitinh;
            updateNguoiDung.Sodienthoai = nguoidung.Sodienthoai;
            updateNguoiDung.Tinhtrang = nguoidung.Tinhtrang;
            await _context.SaveChangesAsync();
            }

            return View(nguoidung);







            //if (id != nguoidung.Id)
            //{
            //    return NotFound();
            //}
            //if (!ModelState.IsValid)
            //{
            //    foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            //    {
            //        Console.WriteLine(error.ErrorMessage);
            //    }
            //    return View(nguoidung);
            //}
            //if (ModelState.IsValid)
            //{
            //    try
            //    {

            //        _context.Update(nguoidung);
            //        await _context.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!NguoidungExists(nguoidung.Id))
            //        {

            //            return NotFound();
            //        }
            //        else
            //        {

            //            throw;
            //        }
            //    }
            //    return RedirectToAction(nameof(Index));
            //}


            //ViewData["QuyenId"] = new SelectList(_context.Quyens.Where(q => q.Id != 5), "Id", "Ten");
            ////ViewData["QuyenId"] = new SelectList(_context.Quyens, "Id", "Id", nguoidung.QuyenId);
            //return View(nguoidung);
        }
        
        // GET: Nguoidungs/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nguoidung = await _context.Nguoidungs
                .Include(n => n.Quyen)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nguoidung == null)
            {
                return NotFound();
            }

            return View(nguoidung);
        }

        // POST: Nguoidungs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var nguoidung = await _context.Nguoidungs.FindAsync(id);
            if (nguoidung != null)
            {
                _context.Nguoidungs.Remove(nguoidung);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NguoidungExists(long id)
        {
            return _context.Nguoidungs.Any(e => e.Id == id);
        }









        [HttpGet]
        public IActionResult CreateKhachHang()
        {
            ViewData["Layout"] = "_LayoutAdmin";
            ViewData["DiaChiId"] = new SelectList(_context.Diachis, "Id", "Ten");
            return View("/Views/NguoiDungs/CreateKhachHang.cshtml");

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateKH(RegisterVM registerVM, long diaChiId)
        {
            ViewData["Layout"] = "_LayoutAdmin";

            // Check if the email already exists
            var existingUser = await _context.Nguoidungs
                .FirstOrDefaultAsync(u => u.Email == registerVM.Email);

            if (existingUser != null)
            {
                ModelState.AddModelError("Email", "Email is already in use.");
            }




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




                return RedirectToAction(nameof(Index));
     
             
            }

            ViewData["DiaChiId"] = new SelectList(_context.Diachis, "Id", "Ten");
            return CreateKhachHang();
        }

















        [HttpGet]
        public IActionResult CreateNhanVien()
        {
            ViewData["Layout"] = "_LayoutAdmin";
            ViewData["QuyenId"] = new SelectList(_context.Quyens.Where(q => q.Id != 5), "Id", "Ten");
            ViewData["DiaChiId"] = new SelectList(_context.Diachis, "Id", "Ten");
            return View("/Views/NguoiDungs/CreateNhanVien.cshtml");

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateNV(RegisterVM registerVM, long QuyenId, long diaChiId)
        {
            ViewData["Layout"] = "_LayoutAdmin";

    
            var existingUser = await _context.Nguoidungs
                .FirstOrDefaultAsync(u => u.Email == registerVM.Email);

            if (existingUser != null)
            {
                ModelState.AddModelError("Email", "Email is already in use.");
            }




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
                    QuyenId = QuyenId,
                    Diachi = diaChi.Ten,
                    Vaitro = true,
                    Tinhtrang = true,
                    CreatedAt = DateTime.Now
                };

                _context.Add(nguoidung);
                await _context.SaveChangesAsync();

                // Create Khachhang
                var nhanvien = new Nhanvien
                {
                    Id = nguoidung.Id,

                    Tinhtrang = true,
                    Vitri = (QuyenId != 5 ? "Nhân viên" : ""),
                    CreatedAt = DateTime.Now
                };

                _context.Nhanviens.Add(nhanvien);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }


            ViewData["QuyenId"] = new SelectList(_context.Quyens.Where(q => q.Id != 5), "Id", "Ten");
            ViewData["DiaChiId"] = new SelectList(_context.Diachis, "Id", "Ten");
            return CreateKhachHang();
        }







        //[HttpPost, ActionName("SoftDelete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> SoftDelete(long id)
        //{
        //    ViewData["Layout"] = "_LayoutAdmin";
        //    var nguoiDung = await _context.Nguoidungs.FindAsync(id);
        //    if (nguoiDung != null)
        //    {
        //        nguoiDung.Tinhtrang = false;
        //        nguoiDung.DeletedAt = DateTime.UtcNow;
        //        nguoiDung.UpdatedAt = DateTime.UtcNow;
        //        _context.Update(nguoiDung);
        //        await _context.SaveChangesAsync();
        //    }

        //    return RedirectToAction(nameof(Index));
        //}


        //[HttpPost, ActionName("Restore")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Restore(long id)
        //{
        //    ViewData["Layout"] = "_LayoutAdmin";
        //    var nguoiDung = await _context.Nguoidungs.FindAsync(id);
        //    if (nguoiDung != null)
        //    {
        //        nguoiDung.Tinhtrang = true;
        //        nguoiDung.DeletedAt = null;
        //        nguoiDung.UpdatedAt = DateTime.UtcNow;
        //        _context.Update(nguoiDung);
        //        await _context.SaveChangesAsync();
        //    }

        //    return RedirectToAction(nameof(Index));
        //}


    }
}
