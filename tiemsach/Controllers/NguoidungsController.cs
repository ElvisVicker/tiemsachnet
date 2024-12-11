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
    public class NguoidungsController : Controller
    {
        private readonly TiemsachContext _context;
        private readonly ILogger<NguoidungsController> _logger;
        public NguoidungsController(TiemsachContext context, ILogger<NguoidungsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Nguoidungs
        public async Task<IActionResult> Index()
        {
            ViewData["Layout"] = "_LayoutAdmin";
            var tiemsachContext = _context.Nguoidungs.Include(n => n.Quyen);
            return View(await tiemsachContext.ToListAsync());
        }

        // GET: Nguoidungs/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewData["Layout"] = "_LayoutAdmin";
            var nguoidung = await _context.Nguoidungs
                .Include(n => n.Quyen)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nguoidung == null)
            {
                return NotFound();
            }

            return View(nguoidung);
        }

        // GET: Nguoidungs/Create
        public IActionResult Create()
        {
            ViewData["QuyenId"] = new SelectList(_context.Quyens, "Id", "Id");
            return View();
        }

        // POST: Nguoidungs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,QuyenId,Hoten,Gioitinh,Vaitro,Sodienthoai,Diachi,Tinhtrang,Image,Email,Password,CreatedAt,UpdatedAt,DeletedAt")] Nguoidung nguoidung)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nguoidung);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["QuyenId"] = new SelectList(_context.Quyens, "Id", "Id", nguoidung.QuyenId);
            return View(nguoidung);
        }

        // GET: Nguoidungs/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            Console.WriteLine($"Received id: {id}");
            if (id == null)
            {
                Console.WriteLine("1");
                return NotFound();
            }
            Console.WriteLine("2");

            ViewData["Layout"] = "_LayoutAdmin";
            var nguoidung = await _context.Nguoidungs.FindAsync(id);
            if (nguoidung == null)
            {
                Console.WriteLine("3");
                return NotFound();
            }
            Console.WriteLine("4");
            ViewData["QuyenId"] = new SelectList(_context.Quyens, "Id", "Id", nguoidung.QuyenId);
            Console.WriteLine("QuyenId: " + nguoidung.QuyenId);

            return View(nguoidung);
        }

        // POST: Nguoidungs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,QuyenId,Hoten,Gioitinh,Vaitro,Sodienthoai,Diachi,Tinhtrang,Image,Email,Password,CreatedAt,UpdatedAt,DeletedAt")] Nguoidung nguoidung)
        {
            ViewData["Layout"] = "_LayoutAdmin";
            Console.WriteLine(nguoidung.QuyenId);
            if (id != nguoidung.Id)
            {
                Console.WriteLine("5");
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }
                return View(nguoidung);
            }
            if (ModelState.IsValid)
            {
                try
                {
                    Console.WriteLine("6");
                    _context.Update(nguoidung);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NguoidungExists(nguoidung.Id))
                    {
                        Console.WriteLine("7");
                        return NotFound();
                    }
                    else
                    {
                        Console.WriteLine("8");
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            Console.WriteLine("9");
            ViewData["QuyenId"] = new SelectList(_context.Quyens, "Id", "Id", nguoidung.QuyenId);
            return View(nguoidung);
        }
        
        // GET: Nguoidungs/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nguoidung = await _context.Nguoidungs
                .Include(n => n.Quyen)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nguoidung == null)
            {
                return NotFound();
            }

            return View(nguoidung);
        }

        // POST: Nguoidungs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var nguoidung = await _context.Nguoidungs.FindAsync(id);
            if (nguoidung != null)
            {
                _context.Nguoidungs.Remove(nguoidung);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NguoidungExists(long id)
        {
            return _context.Nguoidungs.Any(e => e.Id == id);
        }
    }
}
