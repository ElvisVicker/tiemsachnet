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
    [ServiceFilter(typeof(AdminRoleAttribute))]
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
            //var tiemsachContext = _context.Nguoidungs.Include(n => n.Quyen);
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
            var nguoidungVM = new NguoiDungVM
            {
                Id = nguoidung.Id,
                QuyenId = nguoidung.QuyenId,
                Hoten = nguoidung.Hoten,
                Gioitinh = nguoidung.Gioitinh,
                Vaitro = nguoidung.Vaitro,
                Sodienthoai = nguoidung.Sodienthoai,
                Diachi = nguoidung.Diachi,
                Tinhtrang = nguoidung.Tinhtrang,
                Image = nguoidung.Image,
                Email = nguoidung.Email,
                Password = nguoidung.Password,
                CreatedAt = nguoidung.CreatedAt,
                UpdatedAt = DateTime.Now,
                DeletedAt = nguoidung.DeletedAt,
            };
            Console.WriteLine("4");
            //ViewData["QuyenId"] = new SelectList(_context.Quyens, "Id", "Id", nguoidung.QuyenId);
            Console.WriteLine("QuyenId: " + nguoidung.QuyenId);


            ViewData["QuyenId"] = new SelectList(_context.Quyens.Where(q => q.Id != 5), "Id", "Ten");
            return View(nguoidungVM);
        }

        // POST: Nguoidungs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(long id, [Bind("Id,QuyenId,Hoten,Gioitinh,Vaitro,Sodienthoai,Diachi,Tinhtrang,Image,Email,Password,CreatedAt,UpdatedAt,DeletedAt")] Nguoidung nguoidung)
        public async Task<IActionResult> Edit(long id, NguoiDungVM nguoidungVM)
        {
            ViewData["Layout"] = "_LayoutAdmin";
            Console.WriteLine(nguoidungVM.QuyenId);
            ModelState.Clear();
            if (nguoidungVM == null)
            {
                return NotFound();
            }
            var nguoidung = await _context.Nguoidungs.FindAsync(id);
            if (id != nguoidungVM.Id)
            {
                
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                  
                    string filePath = null;

                    if (nguoidungVM.ImageFile != null)
                    {
                        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Customer/images");
                        if (!Directory.Exists(uploadsFolder))
                        {
                            Directory.CreateDirectory(uploadsFolder);
                        }

                        var uniqueFileName = Guid.NewGuid().ToString() + "_" + nguoidungVM.ImageFile.FileName;
                        filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await nguoidungVM.ImageFile.CopyToAsync(fileStream);
                        }
                        nguoidung.Image = Path.GetFileName(filePath);
                    }


                 

                    bool setVaiTro;
                    long quyenChung;
                    if (nguoidungVM.Vaitro == false)
                    {
                        quyenChung = 5;
                        setVaiTro = false;
                    }
                    else
                    {
                        quyenChung = nguoidungVM.QuyenId;
                        setVaiTro = true;
                    }



                    nguoidung.Id = nguoidungVM.Id;
                    nguoidung.QuyenId = quyenChung;
                    nguoidung.Hoten = nguoidungVM.Hoten;
                    nguoidung.Gioitinh = nguoidungVM.Gioitinh;
                    nguoidung.Vaitro = setVaiTro;
                    nguoidung.Sodienthoai = nguoidungVM.Sodienthoai;
                    nguoidung.Diachi = nguoidungVM.Diachi;
                    nguoidung.Tinhtrang = nguoidungVM.Tinhtrang;
                    nguoidung.Email = nguoidungVM.Email;
                    nguoidung.Password = nguoidungVM.Password;
                    nguoidung.CreatedAt = nguoidungVM.CreatedAt;
                    nguoidung.UpdatedAt = nguoidungVM.UpdatedAt;
                    nguoidung.DeletedAt = nguoidungVM.DeletedAt;
                    _context.Update(nguoidung);
                    await _context.SaveChangesAsync();
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NguoidungExists(nguoidung.Id))
                    {
                        Console.WriteLine("7");
                        return NotFound();
                    }
                    else
                    {
                        Console.WriteLine("8");
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            Console.WriteLine("9");
            ViewData["QuyenId"] = new SelectList(_context.Quyens, "Id", "Id", nguoidung.QuyenId);
            return View(nguoidung);
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











    }
}
