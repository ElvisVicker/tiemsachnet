using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using tiemsach.Data;

namespace tiemsach.Controllers
{
    [ServiceFilter(typeof(AdminRoleAttribute))]
    public class LoaisachesController : Controller
    {
        private readonly TiemsachContext _context;

        public LoaisachesController(TiemsachContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Layout"] = "_LayoutAdmin";
            return View(await _context.Loaisaches.ToListAsync());
        }

        public async Task<IActionResult> Details(long? id)
        {
            ViewData["Layout"] = "_LayoutAdmin";
            if (id == null)
            {
                return NotFound();
            }

            var loaisach = await _context.Loaisaches
                .FirstOrDefaultAsync(m => m.Id == id);
            if (loaisach == null)
            {
                return NotFound();
            }

            return View(loaisach);
        }

        public IActionResult Create()
        {
            ViewData["Layout"] = "_LayoutAdmin";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ten")] Loaisach loaisach)
        {
            ViewData["Layout"] = "_LayoutAdmin";
            if (ModelState.IsValid)
            {
                loaisach.Tinhtrang = true;
                loaisach.CreatedAt = DateTime.Now;
                _context.Add(loaisach);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Loại sách đã được tạo thành công!";
                return RedirectToAction(nameof(Index));
            }
            return View(loaisach);
        }

        public async Task<IActionResult> Edit(long? id)
        {
            ViewData["Layout"] = "_LayoutAdmin";
            if (id == null)
            {
                return NotFound();
            }

            var loaisach = await _context.Loaisaches.FindAsync(id);
            if (loaisach == null)
            {
                return NotFound();
            }
            return View(loaisach);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Ten,Tinhtrang")] Loaisach loaisach)
        {
            ViewData["Layout"] = "_LayoutAdmin";
            if (id != loaisach.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    loaisach.UpdatedAt = DateTime.Now;
                    _context.Update(loaisach);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Loại sách đã được cập nhật thành công!";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoaisachExists(loaisach.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(loaisach);
        }

        public async Task<IActionResult> Delete(long? id)
        {
            ViewData["Layout"] = "_LayoutAdmin";
            if (id == null)
            {
                return NotFound();
            }

            var loaisach = await _context.Loaisaches
                .FirstOrDefaultAsync(m => m.Id == id);
            if (loaisach == null)
            {
                return NotFound();
            }

            return View(loaisach);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            ViewData["Layout"] = "_LayoutAdmin";
            var loaisach = await _context.Loaisaches.FindAsync(id);
            if (loaisach != null)
            {
                try
                {
                    loaisach.Tinhtrang = false;
                    loaisach.DeletedAt = DateTime.Now;

                    _context.Update(loaisach);
                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = "Loại sách đã được ẩn thành công!";
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Không thể xóa dữ liệu. Lỗi: " + ex.Message);
                }
            }
            return RedirectToAction(nameof(Index));
        }

        private bool LoaisachExists(long id)
        {
            return _context.Loaisaches.Any(e => e.Id == id);
        }
    }
}
