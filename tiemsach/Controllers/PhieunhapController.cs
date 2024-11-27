
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using tiemsach.Data;

namespace tiemsach.Controllers
{
    public class PhieunhapController : Controller
    {
        private readonly TiemsachContext _context;
        public PhieunhapController(TiemsachContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Phieunhaps
                .Include(pn => pn.Nxb)
                .Include(pn => pn.Nhanvien)
                    .ThenInclude(nv => nv.IdNavigation)
                .ToListAsync()
            );
        }

        public async Task<IActionResult> Create()
        {
            ViewData["NxbList"] = await _context.Nxbs
                .Select(nxb => new SelectListItem
                {
                    Value = nxb.Id.ToString(),
                    Text = nxb.Ten
                })
                .ToListAsync();

            ViewData["NhanvienList"] = await _context.Nhanviens
                .Select(n => new SelectListItem
                {
                    Value = n.Id.ToString(),
                    Text = n.IdNavigation.Hoten
                })
                .ToListAsync();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Tinhtrang,NhanvienId,NxbId")] Phieunhap pn)
        {
            if (pn.NhanvienId != null && pn.NxbId != null)
            {
                pn.CreatedAt = DateTime.Now;
                _context.Phieunhaps.Add(pn);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["NxbList"] = await _context.Nxbs
                .Select(nxb => new SelectListItem
                {
                    Value = nxb.Id.ToString(),
                    Text = nxb.Ten
                })
                .ToListAsync();

            ViewData["NhanvienList"] = await _context.Nhanviens
                .Select(n => new SelectListItem
                {
                    Value = n.Id.ToString(),
                    Text = n.IdNavigation.Hoten
                })
                .ToListAsync();

            return View();
        }

        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pn = await _context.Phieunhaps.FindAsync(id);
            if (pn == null)
            {
                return NotFound();
            }

            ViewData["NxbList"] = await _context.Nxbs
                .Select(nxb => new SelectListItem
                {
                    Value = nxb.Id.ToString(),
                    Text = nxb.Ten
                })
                .ToListAsync();

            ViewData["NhanvienList"] = await _context.Nhanviens
                .Select(n => new SelectListItem
                {
                    Value = n.Id.ToString(),
                    Text = n.IdNavigation.Hoten
                })
                .ToListAsync();

            ViewData["CtpnList"] = await _context.Chitietphieunhaps
                .Where(c => c.PhieunhapId == id)
                .Include(c => c.Sach)
                .ToListAsync();

            return View(pn);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id, Tinhtrang, UpdatedAt, NhanvienId, NxbId")] Phieunhap pn)
        {
            if (id == null)
            {
                return NotFound();
            }

            var existingPhieunhap = await _context.Phieunhaps.FirstOrDefaultAsync(p => p.Id == id);

            if (existingPhieunhap == null)
            {
                return NotFound();
            }

            existingPhieunhap.UpdatedAt = DateTime.Now;
            existingPhieunhap.Tinhtrang = pn.Tinhtrang;
            existingPhieunhap.NhanvienId = pn.NhanvienId;
            existingPhieunhap.NxbId = pn.NxbId;

            await _context.SaveChangesAsync();

            ViewData["NxbList"] = await _context.Nxbs
                .Select(nxb => new SelectListItem
                {
                    Value = nxb.Id.ToString(),
                    Text = nxb.Ten
                })
                .ToListAsync();

            ViewData["NhanvienList"] = await _context.Nhanviens
                .Select(n => new SelectListItem
                {
                    Value = n.Id.ToString(),
                    Text = n.IdNavigation.Hoten
                })
                .ToListAsync();

            ViewData["CtpnList"] = await _context.Chitietphieunhaps
                .Where(c => c.PhieunhapId == id)
                .Include(c => c.Sach)
                .ToListAsync();

            return View(pn);
        }

        public async Task<IActionResult> CreateCTPN(long? id)
        {
            ViewData["SachList"] = await _context.Saches
                .Select(nxb => new SelectListItem
                {
                    Value = nxb.Id.ToString(),
                    Text = nxb.Ten
                })
                .ToListAsync();

            ViewData["PhieunhapId"] = id;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCTPN([Bind("PhieunhapId, Gianhap, Soluong, Tinhtrang, DeletedAt, CreatedAt, UpdatedAt, SachId")] Chitietphieunhap ctpn)
        {
            if (!ModelState.IsValid)
            {
                ctpn.CreatedAt = DateTime.Now;
                _context.Chitietphieunhaps.Add(ctpn);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(ctpn);
        }

        public async Task<IActionResult> EditCTPN(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ctpn = await _context.Chitietphieunhaps.FindAsync(id);
            if (ctpn == null)
            {
                return NotFound();
            }

            ViewData["SachList"] = await _context.Saches
                .Select(nxb => new SelectListItem
                {
                    Value = nxb.Id.ToString(),
                    Text = nxb.Ten
                })
                .ToListAsync();

            return View(ctpn);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCTPN(long id, [Bind("Id, Soluong, Gianhap, Tinhtrang, SachId")] Chitietphieunhap ctpn)
        {
            if (id == null)
            {
                return NotFound();
            }

            var existingCTPN = await _context.Chitietphieunhaps.FirstOrDefaultAsync(p => p.Id == id);

            if (existingCTPN == null)
            {
                return NotFound();
            }

            if (ctpn.Soluong != null && ctpn.Gianhap != null)
            {
                existingCTPN.UpdatedAt = DateTime.Now;
                existingCTPN.Tinhtrang = ctpn.Tinhtrang;
                existingCTPN.Soluong = ctpn.Soluong;
                existingCTPN.Gianhap = ctpn.Gianhap;
                existingCTPN.SachId = ctpn.SachId;

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }


            ViewData["NxbList"] = await _context.Nxbs
                .Select(nxb => new SelectListItem
                {
                    Value = nxb.Id.ToString(),
                    Text = nxb.Ten
                })
                .ToListAsync();

            ViewData["NhanvienList"] = await _context.Nhanviens
                .Select(n => new SelectListItem
                {
                    Value = n.Id.ToString(),
                    Text = n.IdNavigation.Hoten
                })
                .ToListAsync();

            ViewData["CtpnList"] = await _context.Chitietphieunhaps
                .Where(c => c.PhieunhapId == id)
                .Include(c => c.Sach)
                .ToListAsync();

            return View(ctpn);
        }


        public async Task<IActionResult> DeleteCTPN(long id)
        {
            var ctpn = await _context.Chitietphieunhaps.FindAsync(id);
            if (ctpn != null)
            {
                _context.Chitietphieunhaps.Remove(ctpn);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(long id)
        {
            var phieunhap = await _context.Phieunhaps.FindAsync(id);

            if (phieunhap == null)
            {
                return NotFound();
            }

            var chitietPhieunhaps = _context.Chitietphieunhaps
                .Where(ctpn => ctpn.PhieunhapId == id);

            _context.Chitietphieunhaps.RemoveRange(chitietPhieunhaps);

            _context.Phieunhaps.Remove(phieunhap);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool PhieunhapExists(long id)
        {
            return _context.Phieunhaps.Any(e => e.Id == id);
        }
    }
}