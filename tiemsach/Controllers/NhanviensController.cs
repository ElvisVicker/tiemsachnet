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
//    public class NhanviensController : Controller
//    {
//        private readonly TiemsachContext _context;
//        private readonly 

//        public NhanviensController(TiemsachContext context)
//        {
//            _context = context;
//        }

//        // GET: Nhanviens
//        public async Task<IActionResult> Index()
//        {
//            //var tiemsachContext = _context.Nhanviens.ToListAsync();
//            var list = 
//            return View(await _context.Nhanviens.ToListAsync());
//        }

//        // GET: Nhanviens/Details/5
//        public async Task<IActionResult> Details(long? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var nhanvien = await _context.Nhanviens
//                .Include(n => n.IdNavigation)
//                .FirstOrDefaultAsync(m => m.Id == id);
//            if (nhanvien == null)
//            {
//                return NotFound();
//            }

//            return View(nhanvien);
//        }

//        // GET: Nhanviens/Create
//        public IActionResult Create()
//        {
//            ViewData["Id"] = new SelectList(_context.Nguoidungs, "Id", "Id");
//            return View();
//        }

//        // POST: Nhanviens/Create
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create([Bind("Id,Tinhtrang,Vitri,DeletedAt,CreatedAt,UpdatedAt")] Nhanvien nhanvien)
//        {
//            if (ModelState.IsValid)
//            {
//                _context.Add(nhanvien);
//                await _context.SaveChangesAsync();
//                return RedirectToAction(nameof(Index));
//            }
//            ViewData["Id"] = new SelectList(_context.Nguoidungs, "Id", "Id", nhanvien.Id);
//            return View(nhanvien);
//        }

//        // GET: Nhanviens/Edit/5
//        public async Task<IActionResult> Edit(long? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var nhanvien = await _context.Nhanviens.FindAsync(id);
//            if (nhanvien == null)
//            {
//                return NotFound();
//            }
//            ViewData["Id"] = new SelectList(_context.Nguoidungs, "Id", "Id", nhanvien.Id);
//            return View(nhanvien);
//        }

//        // POST: Nhanviens/Edit/5
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(long id, [Bind("Id,Tinhtrang,Vitri,DeletedAt,CreatedAt,UpdatedAt")] Nhanvien nhanvien)
//        {
//            if (id != nhanvien.Id)
//            {
//                return NotFound();
//            }

//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    _context.Update(nhanvien);
//                    await _context.SaveChangesAsync();
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!NhanvienExists(nhanvien.Id))
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
//            ViewData["Id"] = new SelectList(_context.Nguoidungs, "Id", "Id", nhanvien.Id);
//            return View(nhanvien);
//        }

//        // GET: Nhanviens/Delete/5
//        public async Task<IActionResult> Delete(long? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var nhanvien = await _context.Nhanviens
//                .Include(n => n.IdNavigation)
//                .FirstOrDefaultAsync(m => m.Id == id);
//            if (nhanvien == null)
//            {
//                return NotFound();
//            }

//            return View(nhanvien);
//        }

//        // POST: Nhanviens/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        //softDelete
//        public async Task<IActionResult> DeleteConfirmed(long id)
//        {
//            var nhanvien = await _context.Nhanviens.FindAsync(id);
//            if (nhanvien != null)
//            {
//                //_context.Nhanviens.Remove(nhanvien);
//                nhanvien.DeletedAt = DateTime.UtcNow;
//                _context.Update(nhanvien);
//            }

//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));
//        }

//        private bool NhanvienExists(long id)
//        {
//            return _context.Nhanviens.Any(e => e.Id == id);
//        }

//        //Search by position/id
//        public async Task<IActionResult> Search(string key)
//        {
//            var result = await _context.Nhanviens.Where(
//                nv => nv.Vitri.Equals(key) || nv.Id == int.Parse(key)
//                ).ToListAsync();
//            return View("Index", result); 
//        }
//    }
//}

using tiemsach.Data;
//using tiemsach.Repositories;
using tiemsach.Repositories.tiemsach.Repositories;
using tiemsach.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System;
using System.IO;
using System.Diagnostics.Contracts;


namespace tiemsach.Controllers
{
    public class NhanviensController : Controller
    {
        private readonly TiemsachContext _context;
        private readonly NhanVienRepository repo;
        private readonly NguoidungsController userCtrl;
        //private readonly NguoiDungRepository userRepo;
        private readonly QuyensController roleCtrl;

        public NhanviensController(TiemsachContext context)
        {
            _context = context;
            repo = new NhanVienRepository(context);
            userCtrl = new NguoidungsController(context);
            //userRepo = new NguoiDungRepository(context);
            roleCtrl = new QuyensController(context);
        }

        private async Task<IEnumerable<NhanVien_ViewModel>> ViewModelConverter(IEnumerable<Nhanvien> nhanViens)
        {
            var nhanVien_VM = new List<NhanVien_ViewModel>();
            foreach (Nhanvien nv in nhanViens)
            {
                //var user = await userCtrl.(nv.NguoiDung_id);
                var user = await _context.Nguoidungs.FirstOrDefaultAsync(u => u.Id == nv.Id);
                var item = new NhanVien_ViewModel(user, nv.Vitri, nv.CreatedAt, nv.UpdatedAt);
                nhanVien_VM.Add(item);
            }

            return nhanVien_VM;
        }

        //GET: NhanVien/Index
        public async Task<IActionResult> Index()
        {
            var nhanViens = await repo.GetAllAsync();
            var nhanVien_vm = await ViewModelConverter(nhanViens);
            return View(nhanVien_vm);
        }

        //CREATE: NhanVien/Create
        public IActionResult Create()
        {
            long id = _context.Nguoidungs.Max(x => x.Id) + 1;
            var user = new Nguoidung();
            user.Id = id;
            NhanVien_ViewModel vm = new NhanVien_ViewModel
            {
                id = id,
                created_at = DateTime.Now,
                Quyens = _context.Quyens.ToListAsync().Result.ToList()
            };
            return View(vm);
        }

        //CREATE: NhanVien/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NhanVien_ViewModel user)
        {
            if (ModelState.IsValid)
            {
                var user_ = new Nguoidung
                {
                    Id = user.id,
                    QuyenId = user.quyen_id,
                    Hoten = user.hoten,
                    Gioitinh = user.gioitinh,
                    Vaitro = true,
                    Sodienthoai = user.sodienthoai,
                    Diachi = user.diachi,
                    Tinhtrang = true,
                    Image = user.image,
                    Email = user.email,
                    Password = user.password,
                    CreatedAt = user.created_at,
                    UpdatedAt = null,
                    DeletedAt = null
                };

                //if (userRepo.GetUserByEmail(user.email).Result != null)
                if (_context.Nguoidungs.Where(u => u.Email.Equals(user.email)) != null)
                {
                    HttpContext.Session.SetString("Error", "Email already exist.");
                    user.Quyens = _context.Quyens.ToListAsync().Result.ToList();
                    user.id = _context.Nguoidungs.Max(x => x.Id) + 1;
                    return View(user);
                }

                //await userRepo.InsertAsync(user_);
                await _context.Nguoidungs.AddAsync(user_);
                await _context.SaveChangesAsync();

                var nhanVien = new Nhanvien
                {
                    Id = user_.Id,
                    Vitri = "NhanVien",
                    CreatedAt = user_.CreatedAt,
                    UpdatedAt = user_.UpdatedAt
                };
                await repo.InsertAsync(nhanVien);
                return RedirectToAction(nameof(Index));
            }

            user.Quyens = _context.Quyens.ToListAsync().Result.ToList();
            user.id = _context.Nguoidungs.Max(x => x.Id) + 1;
            return View(user);
        }

        public async Task<IActionResult> Dashboard()
        {
            return Redirect("/Dashboard");
        }
        private async Task<Nguoidung?> GetUserById(long id)
        {
            return await _context.Nguoidungs.FirstOrDefaultAsync(u => u.Id == id);
        }

        //EDIT: NhanVien/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhanVien = await repo.GetEmployeeById(id);
            if (nhanVien == null)
            {
                return NotFound();
            }

            //var user = await userCtrl.GetUserById(nhanVien.NguoiDung_id);
            var user = GetUserById(nhanVien.Id).Result;
            var nhanVien_vm = new NhanVien_ViewModel
            {
                id = nhanVien.Id,
                hoten = user.Hoten,
                email = user.Email,
                password = user.Password,
                confirm_password = user.Password,
                diachi = user.Diachi,
                sodienthoai = user.Sodienthoai,
                gioitinh = user.Gioitinh,
                image = user.Image,
                vitri = nhanVien.Vitri,
            };
            return View(nhanVien_vm);
        }

        private string GetProfileImageFolderPath()
        {
            string DotnetVersion = "net8.0-windows"; // Adjust this if needed
            string DebugPath = Path.Combine("bin", "Debug", DotnetVersion);
            string basePath = Environment.CurrentDirectory.Replace(DebugPath, "");
            return Path.Combine(basePath, "Assets", "UserProfile");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, NhanVien_ViewModel nhanVien_vm, IFormFile imageFile)
        {
            if (id != nhanVien_vm.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var user = GetUserById(nhanVien_vm.id).Result;

                    user.Hoten = nhanVien_vm.hoten;
                    user.Sodienthoai = nhanVien_vm.sodienthoai;
                    user.Gioitinh = nhanVien_vm.gioitinh;
                    user.Diachi = nhanVien_vm.diachi;
                    user.UpdatedAt = nhanVien_vm.updated_at;

                    if (imageFile != null && imageFile.Length > 0)
                    {
                        var uploadsFolder = GetProfileImageFolderPath();
                        if (!Directory.Exists(uploadsFolder))
                        {
                            Directory.CreateDirectory(uploadsFolder);
                        }

                        string newFileName = $"{nhanVien_vm.id}{Path.GetExtension(imageFile.FileName)}";
                        var filePath = Path.Combine(uploadsFolder, newFileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await imageFile.CopyToAsync(stream);
                        }

                        user.Image = "/" + newFileName;
                    }

                    var nhanVien = new Nhanvien
                    {
                        Id = nhanVien_vm.id,
                        Vitri = nhanVien_vm.vitri,
                        CreatedAt = nhanVien_vm.created_at,
                        UpdatedAt = nhanVien_vm.updated_at
                    };

                    // Save changes to the database
                    _context.Nguoidungs.Update(user);
                    await _context.SaveChangesAsync();

                    await repo.UpdateAsync(nhanVien);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NhanVienExists((int)nhanVien_vm.id))
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
            nhanVien_vm.Quyens = _context.Quyens.ToListAsync().Result.ToList();
            return View(nhanVien_vm);
        }

        //DETAILS: NhanVien/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhanVien = await repo.GetEmployeeById(id);
            if (nhanVien == null)
            {
                return NotFound();
            }

            var user = GetUserById(nhanVien.Id).Result;
            var nhanVien_vm = new NhanVien_ViewModel
            {
                id = nhanVien.Id,
                hoten = user.Hoten,
                email = user.Email,
                password = user.Password,
                confirm_password = user.Password,
                diachi = user.Diachi,
                sodienthoai = user.Sodienthoai,
                gioitinh = user.Gioitinh,
                image = user.Image,
                vitri = nhanVien.Vitri,
            };
            return View(nhanVien_vm);
        }

        //DELETE: NhanVien/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhanVien = await repo.GetEmployeeById(id);
            if (nhanVien == null)
            {
                return NotFound();
            }

            var user = GetUserById(nhanVien.Id).Result;
            var nhanVien_vm = new NhanVien_ViewModel
            (
                user, nhanVien.Vitri, nhanVien.CreatedAt, nhanVien.UpdatedAt
            );
            nhanVien_vm.email = user.Email;
            nhanVien_vm.password = user.Password;
            return View(nhanVien_vm);
        }

        //DELETE: NhanVien/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nhanVien = await repo.GetEmployeeById(id);
            var user = GetUserById(nhanVien.Id).Result;
            if (nhanVien != null && user != null)
            {
                await repo.DeleteAsync((int)nhanVien.Id);
                //await userRepo.DeleteAsync(user.id);
                await DeleteUserAsync(user.Id);
            }
            return RedirectToAction(nameof(Index));
        }

        //SEARCH: NhanVien/key=PhamNguyenPhuocThien
        public async Task<IActionResult> Search(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return RedirectToAction("Index");
            }
            var nhanViens = await repo.Find(key);
            var nhanVien_vm = await ViewModelConverter(nhanViens);
            return View("Index", nhanVien_vm);
        }

        private bool NhanVienExists(int id)
        {
            return _context.Nhanviens.Any(e => e.Id == id);
        }

        private bool CheckAccountUnique(string email)
        {
            //var user = userRepo.GetUserByEmail(email).Result;
            var user = _context.Nguoidungs.FirstOrDefaultAsync(u => u.Email == email).Result;
            return user == null;
        }

        private async Task DeleteUserAsync(long id)
        {
            var user = await GetUserById(id);
            if (user != null)
            {
                user.Tinhtrang = false;
                user.DeletedAt = DateTime.UtcNow;
                _context.Nguoidungs.Update(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}