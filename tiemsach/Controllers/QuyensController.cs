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
    public class QuyensController : Controller
    {
        private readonly TiemsachContext _context;

        public QuyensController(TiemsachContext context)
        {
            _context = context;
        }

        // GET: Quyens
        public async Task<IActionResult> Index()
        {
            ViewData["Layout"] = "_LayoutAdmin";
            return View(await _context.Quyens.ToListAsync());
        }

        // GET: Quyens/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quyen = await _context.Quyens
                .FirstOrDefaultAsync(m => m.Id == id);
            if (quyen == null)
            {
                return NotFound();
            }

            return View(quyen);
        }

        // GET: Quyens/Create
        public IActionResult Create()
        {
            ViewData["Layout"] = "_LayoutAdmin";
            return View();
        }

        // POST: Quyens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ten,Tinhtrang,DeletedAt,CreatedAt,UpdatedAt,Cnnguoidung,Cntacgia,Cnloaisach,Cnsach,Cnquyen,Cnnhap,Cnxuat,Cnnxb")] Quyen quyen)
        {
            ViewData["Layout"] = "_LayoutAdmin";
            if (ModelState.IsValid)
            {




                _context.Add(quyen);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(quyen);
        }

        // GET: Quyens/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            ViewData["Layout"] = "_LayoutAdmin";
            if (id == null)
            {
                return NotFound();
            }

            var quyen = await _context.Quyens.FindAsync(id);
            if (quyen == null)
            {
                return NotFound();
            }
            return View(quyen);
        }

        // POST: Quyens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Ten,Tinhtrang,DeletedAt,CreatedAt,UpdatedAt,Cnnguoidung,Cntacgia,Cnloaisach,Cnsach,Cnquyen,Cnnhap,Cnxuat,Cnnxb")] Quyen quyen)
        {
            ViewData["Layout"] = "_LayoutAdmin";
            if (id != quyen.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(quyen);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuyenExists(quyen.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(quyen);
        }

        // GET: Quyens/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quyen = await _context.Quyens
                .FirstOrDefaultAsync(m => m.Id == id);
            if (quyen == null)
            {
                return NotFound();
            }

            return View(quyen);
        }

        // POST: Quyens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var quyen = await _context.Quyens.FindAsync(id);
            if (quyen != null)
            {
                _context.Quyens.Remove(quyen);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuyenExists(long id)
        {
            return _context.Quyens.Any(e => e.Id == id);
        }
    }
}
