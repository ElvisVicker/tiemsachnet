//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using tiemsach.Data;

//namespace tiemsach.Controllers
//{
//    public class KhachhangsController : Controller
//    {
//        private readonly TiemsachContext _context;


//        public KhachhangsController(TiemsachContext context)
//        {
//            _context = context;
//        }

//        // GET: Khachhangs
//        public async Task<IActionResult> Index()
//        {
//            var tiemsachContext = _context.Khachhangs.Include(k => k.Diachi).Include(k => k.IdNavigation);
//            return View(await tiemsachContext.ToListAsync());
//        }

//        // GET: Khachhangs/Details/5
//        public async Task<IActionResult> Details(long? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var khachhang = await _context.Khachhangs
//                .Include(k => k.Diachi)
//                .Include(k => k.IdNavigation)
//                .FirstOrDefaultAsync(m => m.Id == id);
//            if (khachhang == null)
//            {
//                return NotFound();
//            }

//            return View(khachhang);
//        }

//        // GET: Khachhangs/Create
//        public IActionResult Create()
//        {
//            ViewData["DiachiId"] = new SelectList(_context.Diachis, "Id", "Id");
//            ViewData["Id"] = new SelectList(_context.Nguoidungs, "Id", "Id");
//            return View();
//        }

//        // POST: Khachhangs/Create
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create([Bind("Id,DiachiId,Tinhtrang,DeletedAt,CreatedAt,UpdatedAt")] Khachhang khachhang)
//        {
//            if (ModelState.IsValid)
//            {
//                _context.Add(khachhang);
//                await _context.SaveChangesAsync();
//                return RedirectToAction(nameof(Index));
//            }
//            ViewData["DiachiId"] = new SelectList(_context.Diachis, "Id", "Id", khachhang.DiachiId);
//            ViewData["Id"] = new SelectList(_context.Nguoidungs, "Id", "Id", khachhang.Id);
//            return View(khachhang);
//        }

//        // GET: Khachhangs/Edit/5
//        public async Task<IActionResult> Edit(long? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var khachhang = await _context.Khachhangs.FindAsync(id);
//            if (khachhang == null)
//            {
//                return NotFound();
//            }
//            ViewData["DiachiId"] = new SelectList(_context.Diachis, "Id", "Id", khachhang.DiachiId);
//            ViewData["Id"] = new SelectList(_context.Nguoidungs, "Id", "Id", khachhang.Id);
//            return View(khachhang);
//        }

//        // POST: Khachhangs/Edit/5
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(long id, [Bind("Id,DiachiId,Tinhtrang,DeletedAt,CreatedAt,UpdatedAt")] Khachhang khachhang)
//        {
//            if (id != khachhang.Id)
//            {
//                return NotFound();
//            }

//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    _context.Update(khachhang);
//                    await _context.SaveChangesAsync();
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!KhachhangExists(khachhang.Id))
//                    {
//                        return NotFound();
//                    }
//                    else
//                    {
//                        throw;
//                    }
//                }
//                return RedirectToAction(nameof(Index));
//            }
//            ViewData["DiachiId"] = new SelectList(_context.Diachis, "Id", "Id", khachhang.DiachiId);
//            ViewData["Id"] = new SelectList(_context.Nguoidungs, "Id", "Id", khachhang.Id);
//            return View(khachhang);
//        }

//        // GET: Khachhangs/Delete/5
//        public async Task<IActionResult> Delete(long? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var khachhang = await _context.Khachhangs
//                .Include(k => k.Diachi)
//                .Include(k => k.IdNavigation)
//                .FirstOrDefaultAsync(m => m.Id == id);
//            if (khachhang == null)
//            {
//                return NotFound();
//            }

//            return View(khachhang);
//        }

//        // POST: Khachhangs/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(long id)
//        {
//            var khachhang = await _context.Khachhangs.FindAsync(id);
//            if (khachhang != null)
//            {
//                //_context.Khachhangs.Remove(khachhang);
//                khachhang.DeletedAt = DateTime.UtcNow;
//                khachhang.Tinhtrang = false;
//                _context.Update(khachhang);
//            }

//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));
//        }

//        private bool KhachhangExists(long id)
//        {
//            return _context.Khachhangs.Any(e => e.Id == id);
//        }

//        public async Task<List<Diachi>> GetAllDiachi(long idKhachHang)
//        {
//            if (!KhachhangExists(idKhachHang)) return null;
//            var chitietdiachi = await _context.Khachhangs.Where(kh => kh.Id == idKhachHang).ToListAsync();
//            List<Diachi> diachiList = new List<Diachi>();
//            foreach (var khachhang in chitietdiachi)
//                diachiList.Add(await _context.Diachis.FindAsync(khachhang.DiachiId));
//            return diachiList; 
//        }
//    }
//}
using tiemsach.Data;
using tiemsach.Models;
using tiemsach.Repositories;
using tiemsach.Repositories.tiemsach.Repositories;
using tiemsach.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace tiemsach.Controllers
{
    public class KhachhangsController : Controller
    {
        private TiemsachContext _context;
        private KhachHangRepository repo;
        //private NguoiDungsController userCtrl;
        private DiaChiRepository addrRepo;

        public KhachhangsController(TiemsachContext context)
        {
            _context = context;
            repo = new KhachHangRepository(_context);
            //userCtrl = new NguoiDungsController(_context);
            addrRepo = new DiaChiRepository(_context);
        }

        private async Task<Nguoidung?> GetUserById(long id)
        {
            return await _context.Nguoidungs.FirstOrDefaultAsync(u => u.Id == id);
        }

        private async Task<IEnumerable<KhachHang_ViewModel>> ViewModelConverter(IEnumerable<Nguoidung> khachHangs)
        {
            var khach_vm = new List<KhachHang_ViewModel>();
            foreach (Nguoidung ctm in khachHangs)
            {
                var user = await GetUserById(ctm.Id);
                var addr = await addrRepo.GetCustomerAddress((int)user.Id);
                var item = new KhachHang_ViewModel(user, addr, ctm.CreatedAt, ctm.UpdatedAt);
                khach_vm.Add(item);
            }

            return khach_vm;
        }

        public async Task<IActionResult> Index()
        {
            var list = await repo.GetAllCustomerUser();
            return View(await ViewModelConverter(list));
        }

        public async Task<IActionResult> Search(string key)
        {
            var list = repo.Search(key).Result;
            return View("index", await ViewModelConverter(list));
        }

        //DETAILS
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khachHang = await repo.GetCustomerById(id);
            if (khachHang == null)
            {
                return NotFound();
            }

            return View(khachHang);
        }
    }
}


