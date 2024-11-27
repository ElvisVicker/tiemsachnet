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
    public class PhieuxuatsController : Controller
    {
        private readonly TiemsachContext _context;

        public PhieuxuatsController(TiemsachContext context)
        {
            _context = context;
        }

        // GET: Phieuxuats
        public async Task<IActionResult> Index()
        {
            var tiemsachContext = _context.Phieuxuats.Include(p => p.Khachhang).Include(p => p.Nhanvien);
            return View(await tiemsachContext.ToListAsync());
        }

        // GET: Phieuxuats/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phieuxuat = await _context.Phieuxuats
                .Include(p => p.Khachhang)
                .Include(p => p.Nhanvien)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (phieuxuat == null)
            {
                return NotFound();
            }

            return View(phieuxuat);
        }

        // GET: Phieuxuats/Create
        public IActionResult Create()
        {
            ViewData["KhachhangId"] = new SelectList(_context.Khachhangs, "Id", "Id");
            ViewData["NhanvienId"] = new SelectList(_context.Nhanviens, "Id", "Id");
            return View();
        }

        // POST: Phieuxuats/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,KhachhangId,Tendiachi,Tinhtrang,DeletedAt,CreatedAt,UpdatedAt,NhanvienId")] Phieuxuat phieuxuat)
        {
            if (ModelState.IsValid)
            {
                _context.Add(phieuxuat);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KhachhangId"] = new SelectList(_context.Khachhangs, "Id", "Id", phieuxuat.KhachhangId);
            ViewData["NhanvienId"] = new SelectList(_context.Nhanviens, "Id", "Id", phieuxuat.NhanvienId);
            return View(phieuxuat);
        }

        // GET: Phieuxuats/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phieuxuat = await _context.Phieuxuats.FindAsync(id);
            if (phieuxuat == null)
            {
                return NotFound();
            }
            ViewData["KhachhangId"] = new SelectList(_context.Khachhangs, "Id", "Id", phieuxuat.KhachhangId);
            ViewData["NhanvienId"] = new SelectList(_context.Nhanviens, "Id", "Id", phieuxuat.NhanvienId);
            return View(phieuxuat);
        }

        // POST: Phieuxuats/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,KhachhangId,Tendiachi,Tinhtrang,DeletedAt,CreatedAt,UpdatedAt,NhanvienId")] Phieuxuat phieuxuat)
        {
            if (id != phieuxuat.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(phieuxuat);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhieuxuatExists(phieuxuat.Id))
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
            ViewData["KhachhangId"] = new SelectList(_context.Khachhangs, "Id", "Id", phieuxuat.KhachhangId);
            ViewData["NhanvienId"] = new SelectList(_context.Nhanviens, "Id", "Id", phieuxuat.NhanvienId);
            return View(phieuxuat);
        }

        // GET: Phieuxuats/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phieuxuat = await _context.Phieuxuats
                .Include(p => p.Khachhang)
                .Include(p => p.Nhanvien)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (phieuxuat == null)
            {
                return NotFound();
            }

            return View(phieuxuat);
        }

        // POST: Phieuxuats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var phieuxuat = await _context.Phieuxuats.FindAsync(id);
            if (phieuxuat != null)
            {
                _context.Phieuxuats.Remove(phieuxuat);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PhieuxuatExists(long id)
        {
            return _context.Phieuxuats.Any(e => e.Id == id);
        }
    }
}
