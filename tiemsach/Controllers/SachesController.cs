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
            var tiemsachContext = _context.Saches.Include(s => s.Loaisach).Include(s => s.Tacgia);
            return View(await tiemsachContext.ToListAsync());
        }

        // GET: Saches/Details/5
        public async Task<IActionResult> Details(long? id)
        {
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

        // GET: Saches/Create
        public IActionResult Create()
        {
            ViewData["LoaisachId"] = new SelectList(_context.Loaisaches, "Id", "Ten");
            ViewData["TacgiaId"] = new SelectList(_context.Tacgia, "Id", "Id");
            return View();
        }

        // POST: Saches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ten,Image,Gianhap,Giaxuat,Mota,Soluong,Tinhtrang,TacgiaId,LoaisachId")] Sach sach)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sach);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LoaisachId"] = new SelectList(_context.Loaisaches, "Id", "Ten", sach.LoaisachId);
            ViewData["TacgiaId"] = new SelectList(_context.Tacgia, "Id", "Id", sach.TacgiaId);
            return View(sach);
        }

        // GET: Saches/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sach = await _context.Saches.FindAsync(id);
            if (sach == null)
            {
                return NotFound();
            }
            ViewData["LoaisachId"] = new SelectList(_context.Loaisaches, "Id", "Ten", sach.LoaisachId);
            ViewData["TacgiaId"] = new SelectList(_context.Tacgia, "Id", "Id", sach.TacgiaId);
            return View(sach);
        }

        // POST: Saches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Ten,Image,Gianhap,Giaxuat,Mota,Soluong,Tinhtrang,TacgiaId,LoaisachId")] Sach sach)
        {
            if (id != sach.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sach);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SachExists(sach.Id))
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
            ViewData["LoaisachId"] = new SelectList(_context.Loaisaches, "Id", "Ten", sach.LoaisachId);
            ViewData["TacgiaId"] = new SelectList(_context.Tacgia, "Id", "Id", sach.TacgiaId);
            return View(sach);
        }

        // GET: Saches/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
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
            var sach = await _context.Saches.FindAsync(id);
            if (sach != null)
            {
                _context.Saches.Remove(sach);
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
