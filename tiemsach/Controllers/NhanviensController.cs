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
    public class NhanviensController : Controller
    {
        private readonly TiemsachContext _context;

        public NhanviensController(TiemsachContext context)
        {
            _context = context;
        }

        // GET: Nhanviens
        public async Task<IActionResult> Index()
        {
            //var tiemsachContext = _context.Nhanviens.ToListAsync();
            return View(await _context.Nhanviens.ToListAsync());
        }

        // GET: Nhanviens/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhanvien = await _context.Nhanviens
                .Include(n => n.IdNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nhanvien == null)
            {
                return NotFound();
            }

            return View(nhanvien);
        }

        // GET: Nhanviens/Create
        public IActionResult Create()
        {
            ViewData["Id"] = new SelectList(_context.Nguoidungs, "Id", "Id");
            return View();
        }

        // POST: Nhanviens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Tinhtrang,Vitri,DeletedAt,CreatedAt,UpdatedAt")] Nhanvien nhanvien)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nhanvien);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id"] = new SelectList(_context.Nguoidungs, "Id", "Id", nhanvien.Id);
            return View(nhanvien);
        }

        // GET: Nhanviens/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhanvien = await _context.Nhanviens.FindAsync(id);
            if (nhanvien == null)
            {
                return NotFound();
            }
            ViewData["Id"] = new SelectList(_context.Nguoidungs, "Id", "Id", nhanvien.Id);
            return View(nhanvien);
        }

        // POST: Nhanviens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Tinhtrang,Vitri,DeletedAt,CreatedAt,UpdatedAt")] Nhanvien nhanvien)
        {
            if (id != nhanvien.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nhanvien);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NhanvienExists(nhanvien.Id))
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
            ViewData["Id"] = new SelectList(_context.Nguoidungs, "Id", "Id", nhanvien.Id);
            return View(nhanvien);
        }

        // GET: Nhanviens/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhanvien = await _context.Nhanviens
                .Include(n => n.IdNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nhanvien == null)
            {
                return NotFound();
            }

            return View(nhanvien);
        }

        // POST: Nhanviens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        //softDelete
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var nhanvien = await _context.Nhanviens.FindAsync(id);
            if (nhanvien != null)
            {
                //_context.Nhanviens.Remove(nhanvien);
                nhanvien.DeletedAt = DateTime.UtcNow;
                _context.Update(nhanvien);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NhanvienExists(long id)
        {
            return _context.Nhanviens.Any(e => e.Id == id);
        }

        //Search by position/id
        public async Task<IActionResult> Search(string key)
        {
            var result = await _context.Nhanviens.Where(
                nv => nv.Vitri.Equals(key) || nv.Id == int.Parse(key)
                ).ToListAsync();
            return View("Index", result);
        }
    }
}
