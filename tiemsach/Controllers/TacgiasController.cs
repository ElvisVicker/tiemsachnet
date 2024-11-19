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
    public class TacgiasController : Controller
    {
        private readonly TiemsachContext _context;

        public TacgiasController(TiemsachContext context)
        {
            _context = context;
        }

        // GET: Tacgias
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tacgia.ToListAsync());
        }

        // GET: Tacgias/Details/5
        public async Task<IActionResult> Details(long? id)
        {
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

        // GET: Tacgias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tacgias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ten,Tinhtrang,DeletedAt,CreatedAt,UpdatedAt,Namsinh")] Tacgia tacgia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tacgia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tacgia);
        }

        // GET: Tacgias/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
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

        // POST: Tacgias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Ten,Tinhtrang,DeletedAt,CreatedAt,UpdatedAt,Namsinh")] Tacgia tacgia)
        {
            if (id != tacgia.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tacgia);
                    await _context.SaveChangesAsync();
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
                return RedirectToAction(nameof(Index));
            }
            return View(tacgia);
        }

        // GET: Tacgias/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
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

        // POST: Tacgias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var tacgia = await _context.Tacgia.FindAsync(id);
            if (tacgia != null)
            {
                _context.Tacgia.Remove(tacgia);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TacgiaExists(long id)
        {
            return _context.Tacgia.Any(e => e.Id == id);
        }
    }
}
