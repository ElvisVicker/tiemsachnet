namespace tiemsach.Repositories
{
    using global::tiemsach.Data;
    using global::tiemsach.Models;
    //using System.Data.SqlClient;
    using Microsoft.EntityFrameworkCore;
    using MySql.Data.MySqlClient;
    using System.Data.SqlClient;

    //using System.Data.Entity;

    namespace tiemsach.Repositories
    {
        public interface INhanVienRepository
        {
            Task<IEnumerable<Nhanvien>> GetAllAsync();
            Task<Nhanvien?> GetEmployeeById(int? id);
            Task InsertAsync(Nhanvien employee);
            Task UpdateAsync(Nhanvien employee);
            Task DeleteAsync(int id);
            Task SaveAsync();
        }
        public class NhanVienRepository : INhanVienRepository
        {
            private readonly TiemsachContext _context;

            public NhanVienRepository(TiemsachContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<Nguoidung>> GetAllEmployeeUser()
            {
                return await _context.Nguoidungs.Where(user => user.Vaitro == true && user.Tinhtrang == true).ToListAsync();
            }

            public async Task<IEnumerable<Nhanvien>> GetAllAsync()
            {
                return await _context.Nhanviens.Where(nv => nv.Tinhtrang == true).ToListAsync();
            }

            public async Task<Nhanvien?> GetEmployeeById(int? id)
            {
                return await _context.Nhanviens.FirstOrDefaultAsync(nv => nv.Id == id && nv.Tinhtrang == true);
            }

            public async Task InsertAsync(Nhanvien employee)
            {
                var sql = @"insert into nhanvien (NguoiDung_id, vitri, tinhtrang, created_at, updated_at, deleted_at) 
                values (@NguoiDung_id, @vitri, 1, @created_at, null, null)";
                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@NguoiDung_id", employee.Id),
                    new SqlParameter("@vitri", employee.Vitri),
                    new SqlParameter("@created_at", employee.CreatedAt)
                };
                await _context.Database.ExecuteSqlRawAsync(sql, parameters);
            }

            public async Task UpdateAsync(Nhanvien employee)
            {
                var sql = @"
                    UPDATE nhanvien
                    SET vitri = @Vitri, updated_at = @UpdatedAt
                    WHERE nguoidung_id = @NguoiDung_id";

                var parameters = new[]
                {
                    new MySqlParameter("@NguoiDung_id", employee.Id),
                    new MySqlParameter("@Vitri", employee.Vitri),
                    new MySqlParameter("@UpdatedAt", employee.UpdatedAt ?? (object)DBNull.Value)
                };

                await _context.Database.ExecuteSqlRawAsync(sql, parameters);
            }

            public async Task DeleteAsync(int id)
            {
                var sql = @"
                    UPDATE nhanvien
                    SET deleted_at = @DeletedAt, tinhtrang = 0
                    WHERE nguoidung_id = @NguoiDung_id";

                var parameters = new[]
                {
                    new MySqlParameter("@DeletedAt", DateTime.UtcNow),
                    new MySqlParameter("@NguoiDung_id", id),
                };

                await _context.Database.ExecuteSqlRawAsync(sql, parameters);
            }

            public async Task SaveAsync()
            {
                await _context.SaveChangesAsync();
            }

            //Find by name/id/role
            public async Task<IEnumerable<Nhanvien>> Find(string key)
            {
                var list = await _context.Nhanviens.Where(nv =>
                    (nv.Id.ToString().Contains(key) ||
                    nv.Vitri.Contains(key)) && nv.Tinhtrang == true
                    ).ToListAsync();
                var list2 = await _context.Nguoidungs.Where(u =>
                    u.Tinhtrang == true &&
                    u.Hoten.Contains(key)).ToListAsync();
                foreach (Nguoidung u in list2)
                {
                    var nv = GetEmployeeById((int)u.Id).Result;
                    if (!list.Contains(nv)) list.Add(nv);
                }
                return list;
            }
        }
    }
}
