using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tiemsach.Data;

namespace tiemsach.Controllers
{
    [ServiceFilter(typeof(AdminRoleAttribute))]
    public class TacgiasController : Controller
    {
        private readonly TiemsachContext _context;

        public TacgiasController(TiemsachContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Layout"] = "_LayoutAdmin";
            return View(await _context.Tacgia.ToListAsync());
        }

        public async Task<IActionResult> Details(long? id)
        {
            ViewData["Layout"] = "_LayoutAdmin";
            if (id == null)
            {
                return NotFound();
            }

            var tacgia = await _context.Tacgia
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tacgia == null)
            {
                return NotFound();
            }

            return View(tacgia);
        }

        public IActionResult Create()
        {
            ViewData["Layout"] = "_LayoutAdmin";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ten,Namsinh")] Tacgia tacgia)
        {
            ViewData["Layout"] = "_LayoutAdmin";
            if (ModelState.IsValid)
            {
                tacgia.Tinhtrang = true;
                tacgia.CreatedAt = DateTime.Now;
                tacgia.UpdatedAt = DateTime.Now;
                _context.Add(tacgia);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Tác giả đã được tạo thành công!";
                return RedirectToAction(nameof(Index));
            }
            return View(tacgia);
        }

        public async Task<IActionResult> Edit(long? id)
        {
            ViewData["Layout"] = "_LayoutAdmin";
            if (id == null)
            {
                return NotFound();
            }

            var tacgia = await _context.Tacgia.FindAsync(id);
            if (tacgia == null)
            {
                return NotFound();
            }
            return View(tacgia);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Ten,Tinhtrang,Namsinh")] Tacgia tacgia)
        {
            ViewData["Layout"] = "_LayoutAdmin";
            if (id != tacgia.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    tacgia.CreatedAt = tacgia.CreatedAt;
                    tacgia.UpdatedAt = DateTime.Now;
                    _context.Update(tacgia);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Tác giả đã được cập nhật thành công!";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TacgiaExists(tacgia.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(tacgia);
        }

        public async Task<IActionResult> Delete(long? id)
        {
            ViewData["Layout"] = "_LayoutAdmin";
            if (id == null)
            {
                return NotFound();
            }

            var tacgia = await _context.Tacgia
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tacgia == null)
            {
                return NotFound();
            }

            return View(tacgia);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            ViewData["Layout"] = "_LayoutAdmin";
            var tacgia = await _context.Tacgia.FindAsync(id);
            if (tacgia != null)
            {
                try
                {
                    tacgia.Tinhtrang = false;
                    tacgia.DeletedAt = DateTime.Now;

                    _context.Update(tacgia);
                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = "Tác giả đã được ẩn thành công!";
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Không thể xóa dữ liệu. Lỗi: " + ex.Message);
                }
            }
            return RedirectToAction(nameof(Index));
        }

        private bool TacgiaExists(long id)
        {
            return _context.Tacgia.Any(e => e.Id == id);
        }
    }
}
