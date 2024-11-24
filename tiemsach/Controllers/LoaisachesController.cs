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
    public class LoaisachesController : Controller
    {
        private readonly TiemsachContext _context;

        public LoaisachesController(TiemsachContext context)
        {
            _context = context;
        }

        // GET: Loaisaches
        public async Task<IActionResult> Index()
        {
            return View(await _context.Loaisaches.ToListAsync());
        }

        // GET: Loaisaches/Details/5
        public async Task<IActionResult> Details(long? id)
        {
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

        // GET: Loaisaches/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Loaisaches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ten,Tinhtrang,DeletedAt,CreatedAt,UpdatedAt")] Loaisach loaisach)
        {
            if (ModelState.IsValid)
            {
                _context.Add(loaisach);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(loaisach);
        }

        // GET: Loaisaches/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
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

        // POST: Loaisaches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Ten,Tinhtrang,DeletedAt,CreatedAt,UpdatedAt")] Loaisach loaisach)
        {
            if (id != loaisach.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loaisach);
                    await _context.SaveChangesAsync();
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
                return RedirectToAction(nameof(Index));
            }
            return View(loaisach);
        }

        // GET: Loaisaches/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
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

        // POST: Loaisaches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var loaisach = await _context.Loaisaches.FindAsync(id);
            if (loaisach != null)
            {
                _context.Loaisaches.Remove(loaisach);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoaisachExists(long id)
        {
            return _context.Loaisaches.Any(e => e.Id == id);
        }
    }
}
