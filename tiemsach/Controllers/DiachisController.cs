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
    public class DiachisController : Controller
    {
        private readonly TiemsachContext _context;

        public DiachisController(TiemsachContext context)
        {
            _context = context;
        }

        // GET: Diachis
        public async Task<IActionResult> Index()
        {
            return View(await _context.Diachis.ToListAsync());
        }

        // GET: Diachis/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diachi = await _context.Diachis
                .FirstOrDefaultAsync(m => m.Id == id);
            if (diachi == null)
            {
                return NotFound();
            }

            return View(diachi);
        }

        // GET: Diachis/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Diachis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ten,Tinhtrang")] Diachi diachi)
        {
            if (ModelState.IsValid)
            {
                _context.Add(diachi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(diachi);
        }

        // GET: Diachis/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diachi = await _context.Diachis.FindAsync(id);
            if (diachi == null)
            {
                return NotFound();
            }
            return View(diachi);
        }

        // POST: Diachis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Ten,Tinhtrang")] Diachi diachi)
        {
            if (id != diachi.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(diachi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiachiExists(diachi.Id))
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
            return View(diachi);
        }

        // GET: Diachis/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diachi = await _context.Diachis
                .FirstOrDefaultAsync(m => m.Id == id);
            if (diachi == null)
            {
                return NotFound();
            }

            return View(diachi);
        }

        // POST: Diachis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var diachi = await _context.Diachis.FindAsync(id);
            if (diachi != null)
            {
                //_context.Diachis.Remove(diachi);
                diachi.Tinhtrang = false;
                _context.Update(diachi);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiachiExists(long id)
        {
            return _context.Diachis.Any(e => e.Id == id);
        }

        public async Task<IActionResult> Search(string key)
        {
            var list = await _context.Diachis.Where(e => e.Id == int.Parse(key)||e.Ten.Equals(key)).ToListAsync();
            return View("index", list); 
        }
    }
}
