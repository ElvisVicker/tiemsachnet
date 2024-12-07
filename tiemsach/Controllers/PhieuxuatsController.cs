using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
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

            ViewData["Layout"] = "_LayoutAdmin";
            var phieuxuats = _context.Phieuxuats.Include(p => p.Khachhang).ThenInclude(kh => kh.IdNavigation)
                .Include(p => p.Nhanvien).ThenInclude(nv => nv.IdNavigation);
            return View(await phieuxuats.ToListAsync());

        }






        //// GET: Phieuxuats/Create
        //public IActionResult Create()
        //{
        //    ViewData["KhachhangId"] = new SelectList(_context.Khachhangs, "Id", "Id");
        //    ViewData["NhanvienId"] = new SelectList(_context.Nhanviens, "Id", "Id");
        //    return View();
        //}

        //// POST: Phieuxuats/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,KhachhangId,Tendiachi,Tinhtrang,DeletedAt,CreatedAt,UpdatedAt,NhanvienId")] Phieuxuat phieuxuat)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(phieuxuat);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["KhachhangId"] = new SelectList(_context.Khachhangs, "Id", "Id", phieuxuat.KhachhangId);
        //    ViewData["NhanvienId"] = new SelectList(_context.Nhanviens, "Id", "Id", phieuxuat.NhanvienId);
        //    return View(phieuxuat);
        //}


        // GET: Phieuxuats/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {

            ViewData["Layout"] = "_LayoutAdmin";
            if (id == null)
			{
				return NotFound();
			}


            var phieuxuat = await _context.Phieuxuats.Include(p => p.Khachhang).ThenInclude(kh => kh.IdNavigation)
             .Include(p => p.Nhanvien).ThenInclude(nv => nv.IdNavigation)
				.Include(p => p.Chitietphieuxuat)
				.FirstOrDefaultAsync(m => m.Id == id);

			if (phieuxuat == null)
			{
				return NotFound();
			}


            ViewData["CTPXList"] = await _context.Chitietphieuxuats
           .Where(c => c.PhieuxuatId == id)
           .Include(c => c.Sach)
           .ToListAsync();


            ViewData["KhachhangId"] = new SelectList(_context.Khachhangs, "Id", "Id", phieuxuat.KhachhangId);
			ViewData["NhanvienId"] = new SelectList(_context.Nhanviens, "Id", "Id", phieuxuat.NhanvienId);

			return View(phieuxuat);
		}



        [HttpPost]
        [ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(long id, Phieuxuat phieuxuat, List<Chitietphieuxuat> chitietphieuxuats)
		{

            ViewData["Layout"] = "_LayoutAdmin";
            var updatePhieuXuat = await _context.Phieuxuats.FirstOrDefaultAsync(p => p.Id == id);

			if (updatePhieuXuat == null)
			{
				return NotFound();
			}

			updatePhieuXuat.UpdatedAt = DateTime.Now;
			updatePhieuXuat.Tinhtrang = phieuxuat.Tinhtrang;






			await _context.SaveChangesAsync();



            

           ViewData["CTPXList"] = await _context.Chitietphieuxuats
                .Where(c => c.PhieuxuatId == id)
                .Include(c => c.Sach)
                .ToListAsync();


           var danhSachCTPX = await _context.Chitietphieuxuats
               .Where(c => c.PhieuxuatId == id)
               .Include(c => c.Sach)
               .ToListAsync();


            if (phieuxuat.Tinhtrang)
            {
                foreach (var item in danhSachCTPX)
                {
                    item.Sach.Soluong -= item.Soluong;
                    await _context.SaveChangesAsync();
                }
            }

            

            return View(phieuxuat);


		}
















		//// POST: Phieuxuats/Edit/5
		//// To protect from overposting attacks, enable the specific properties you want to bind to.
		//// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		//[HttpPost]
		//      [ValidateAntiForgeryToken]
		//      public async Task<IActionResult> Edit(long id, [Bind("Id,KhachhangId,Tendiachi,Tinhtrang,DeletedAt,CreatedAt,UpdatedAt,NhanvienId")] Phieuxuat phieuxuat)
		//      {
		//          if (id != phieuxuat.Id)
		//          {
		//              return NotFound();
		//          }

		//          if (ModelState.IsValid)
		//          {
		//              try
		//              {
		//                  _context.Update(phieuxuat);
		//                  await _context.SaveChangesAsync();
		//              }
		//              catch (DbUpdateConcurrencyException)
		//              {
		//                  if (!PhieuxuatExists(phieuxuat.Id))
		//                  {
		//                      return NotFound();
		//                  }
		//                  else
		//                  {
		//                      throw;
		//                  }
		//              }
		//              return RedirectToAction(nameof(Index));
		//          }
		//          ViewData["KhachhangId"] = new SelectList(_context.Khachhangs, "Id", "Id", phieuxuat.KhachhangId);
		//          ViewData["NhanvienId"] = new SelectList(_context.Nhanviens, "Id", "Id", phieuxuat.NhanvienId);
		//          return View(phieuxuat);
		//      }









		//Phieuxuats/Delete/5
		[HttpPost, ActionName("SoftDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SoftDelete(long id)
        {
            ViewData["Layout"] = "_LayoutAdmin";
            var phieuxuat = await _context.Phieuxuats.FindAsync(id);
            if (phieuxuat != null)
            {
                phieuxuat.Tinhtrang = false; 
                phieuxuat.DeletedAt = DateTime.UtcNow;
                phieuxuat.UpdatedAt = DateTime.UtcNow;
                _context.Update(phieuxuat);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }


        [HttpPost, ActionName("Restore")]
        [ValidateAntiForgeryToken]
        //[HttpPost, ActionName("SoftDelete")]
        public async Task<IActionResult> Restore(long id)
        {
            ViewData["Layout"] = "_LayoutAdmin";
            var phieuxuat = await _context.Phieuxuats.FindAsync(id);
            if (phieuxuat != null)
            {
                phieuxuat.Tinhtrang = true;
                phieuxuat.DeletedAt = null;
                phieuxuat.UpdatedAt = DateTime.UtcNow;
                _context.Update(phieuxuat);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }



        private bool PhieuxuatExists(long id)
        {
            ViewData["Layout"] = "_LayoutAdmin";
            return _context.Phieuxuats.Any(e => e.Id == id);
        }
    }
}
