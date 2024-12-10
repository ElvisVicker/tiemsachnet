using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using tiemsach.Data;
using tiemsach.ViewModels;

namespace tiemsach.Controllers
{
    [ServiceFilter(typeof(AdminRoleAttribute))]
    public class SachesController : Controller
    {
        private readonly TiemsachContext _context;

        public SachesController(TiemsachContext context)
        {
            _context = context;
        }

        // GET: Saches
        public async Task<IActionResult> Index()
        {
            ViewData["Layout"] = "_LayoutAdmin";
            var tiemsachContext = _context.Saches.Include(s => s.Loaisach).Include(s => s.Tacgia);
            return View(await tiemsachContext.ToListAsync());
        }

        // GET: Saches/Create
        public IActionResult Create()
        {
            ViewData["Layout"] = "_LayoutAdmin";
            //ViewData["LoaisachId"] = new SelectList(_context.Loaisaches, "Id", "Ten");
            //ViewData["TacgiaId"] = new SelectList(_context.Tacgia, "Id", "Ten");


            ViewData["LoaisachId"] = new SelectList(_context.Loaisaches.Where(ls => ls.Tinhtrang == true).ToList(), "Id", "Ten");
            ViewData["TacgiaId"] = new SelectList(_context.Tacgia.Where(tg => tg.Tinhtrang == true), "Id", "Ten");





            return View();
        }

        // POST: Saches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SachVM sachModel)
        {
            ViewData["Layout"] = "_LayoutAdmin";

            if (sachModel.Image == null || sachModel.Image.Length == 0)
            {
                ModelState.AddModelError("Image", "Bắt buộc phải có ảnh");
            }

            if (ModelState.IsValid)
            {
                string filePath = null;

                if (sachModel.Image != null)
                {
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Customer/images");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + sachModel.Image.FileName;
                    filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await sachModel.Image.CopyToAsync(fileStream);
                    }
                }
                var sach = new Sach
                {
                    Ten = sachModel.Ten,
                    Image = Path.GetFileName(filePath),
                    TacgiaId = sachModel.TacgiaId,
                    LoaisachId = sachModel.LoaisachId,
                    Mota = sachModel.Mota,
                    Tinhtrang = false,
                    Gianhap = 0,
                    Giaxuat = 0
                };

                _context.Saches.Add(sach);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Sách đã được tạo thành công!";
                return RedirectToAction(nameof(Index));
            }
            

            ViewData["LoaisachId"] = new SelectList(_context.Loaisaches.Where(ls => ls.Tinhtrang == true).ToList(), "Id", "Ten");
            ViewData["TacgiaId"] = new SelectList(_context.Tacgia.Where(tg => tg.Tinhtrang == true), "Id", "Ten");
            return View(sachModel);
        }


        // GET: Saches/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            ViewData["Layout"] = "_LayoutAdmin";
            if (id == null)
            {
                return NotFound();
            }

            var sach = await _context.Saches.FindAsync(id);
            if (sach == null)
            {
                return NotFound();
            }

            var sachVM = new EditSachVM
            {
                Ten = sach.Ten,
                Mota = sach.Mota,
                Gianhap = sach.Gianhap,
                Giaxuat = sach.Giaxuat,
                TacgiaId = sach.TacgiaId,
                LoaisachId = sach.LoaisachId,
                Tinhtrang = sach.Tinhtrang,
                ImageName = sach.Image,
                Soluong = sach.Soluong
            };
            ViewData["LoaisachId"] = new SelectList(_context.Loaisaches.Where(ls => ls.Tinhtrang == true).ToList(), "Id", "Ten");
            ViewData["TacgiaId"] = new SelectList(_context.Tacgia.Where(tg => tg.Tinhtrang == true), "Id", "Ten");
            return View(sachVM);
        }

        // POST: Saches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, EditSachVM sachModel)
        {
            ViewData["Layout"] = "_LayoutAdmin";
            Console.WriteLine("hello");
            ModelState.Clear();
            Console.WriteLine(id);
            var sach = await _context.Saches.FindAsync(id);
            if (sach == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string filePath = null;

                    if (sachModel.Image != null)
                    {
                        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Customer/images");
                        if (!Directory.Exists(uploadsFolder))
                        {
                            Directory.CreateDirectory(uploadsFolder);
                        }

                        var uniqueFileName = Guid.NewGuid().ToString() + "_" + sachModel.Image.FileName;
                        filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await sachModel.Image.CopyToAsync(fileStream);
                        }
                        sach.Image = Path.GetFileName(filePath);
                    }
                    sach.Ten = sachModel.Ten;
                    sach.Mota = sachModel.Mota;
                    sach.Gianhap = sachModel.Gianhap;
                    sach.Giaxuat = sachModel.Giaxuat;
                    sach.TacgiaId = sachModel.TacgiaId;
                    sach.LoaisachId = sachModel.LoaisachId;
                    sach.Tinhtrang = sachModel.Tinhtrang;
                    sach.Soluong = sachModel.Soluong;

                    _context.Update(sach); // Thêm đối tượng vào context
                    await _context.SaveChangesAsync(); // Lưu vào cơ sở dữ liệu
                    return RedirectToAction(nameof(Index)); // Chuyển hướng về trang danh sách
                }
                catch (Exception ex)
                {
                    // Xử lý lỗi lưu trữ nếu có
                    ModelState.AddModelError("", "Không thể lưu dữ liệu. Lỗi: " + ex.Message);
                }
            }
            ViewData["LoaisachId"] = new SelectList(_context.Loaisaches.Where(ls => ls.Tinhtrang == true).ToList(), "Id", "Ten");
            ViewData["TacgiaId"] = new SelectList(_context.Tacgia.Where(tg => tg.Tinhtrang == true), "Id", "Ten");
            return View(sach);
        }

        // GET: Saches/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            ViewData["Layout"] = "_LayoutAdmin";
            if (id == null)
            {
                return NotFound();
            }

            var sach = await _context.Saches
                .Include(s => s.Loaisach)
                .Include(s => s.Tacgia)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sach == null)
            {
                return NotFound();
            }

            return View(sach);
        }

        // POST: Saches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            ViewData["Layout"] = "_LayoutAdmin";
            var sach = await _context.Saches.FindAsync(id);
            if (sach != null)
            {
                try
                {
                    sach.Tinhtrang = false;
                    _context.Update(sach); // Thêm đối tượng vào context
                    await _context.SaveChangesAsync(); // Lưu vào cơ sở dữ liệu
                    return RedirectToAction(nameof(Index)); // Chuyển hướng về trang danh sách
                }
                catch (Exception ex)
                {
                    // Xử lý lỗi lưu trữ nếu có
                    ModelState.AddModelError("", "Không thể lưu dữ liệu. Lỗi: " + ex.Message);
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SachExists(long id)
        {
            return _context.Saches.Any(e => e.Id == id);
        }
    }
}
