//using _3121410469_PhamNguyenPhuocThien_BT3.Data;
//using _3121410469_PhamNguyenPhuocThien_BT3.Models;
using tiemsach.Data;
using tiemsach.Models;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
//using Sql.Data.MySqlClient;
//using ZstdSharp.Unsafe;

//using System.Data.Entity;



//Customer table, in fact, is a address-detail table
//Customer table is a child table of User table
//There are several address for each customer so there's also several similar user id in this table
namespace tiemsach.Repositories
{
    public interface IKhachHangRepository
    {
        Task<IEnumerable<Khachhang>> GetAllAsync();
        Task<IEnumerable<Khachhang>?> GetCustomerById(int? id);
        //Task InsertAsync(KhachHang ctm);
        //Task UpdateAsync(KhachHang ctm);
        Task DeleteAddressAsync(int id);
        Task DeleteAsync(int? id);
        Task SaveAsync();
    }
    public class KhachHangRepository : IKhachHangRepository
    {
        private readonly TiemsachContext _context;
        //private readonly DiaChiRepository repoAddr;

        public KhachHangRepository(TiemsachContext context)
        {
            _context = context;
            //repoAddr = new DiaChiRepository(context);
        }

        public async Task<IEnumerable<Nguoidung>> GetAllCustomerUser()
        {
            return await _context.Nguoidungs.Where(user => user.Vaitro == false && user.Tinhtrang == true).ToListAsync();
        }

        public async Task<IEnumerable<Khachhang>> GetAllAsync()
        {
            return await _context.Khachhangs.Where(nv => nv.Tinhtrang == true).ToListAsync();
        }

        public async Task<IEnumerable<Khachhang>?> GetCustomerById(int? id)
        {
            return await _context.Khachhangs.Where(nv => nv.IdNavigation.Id == id && nv.Tinhtrang == true).ToListAsync();
        }

        //Delete one of user's address
        public async Task DeleteAddressAsync(int id)
        {
            var sql = @"
                UPDATE khachhang
                SET deleted_at = @DeletedAt, tinhtrang = 0
                WHERE nguoidung_id = @NguoiDung_id";

            var parameters = new[]
            {
                new SqlParameter("@DeletedAt", DateTime.UtcNow),
                new SqlParameter("@NguoiDung_id", id),
            };

            await _context.Database.ExecuteSqlRawAsync(sql, parameters);
        }

        //Delete user
        public async Task DeleteAsync(int? id)
        {
            var list = await GetCustomerById(id);
            foreach (Khachhang ctm in list)
                await DeleteAddressAsync((int)ctm.Id);
            var customer = await _context.Nguoidungs.FirstOrDefaultAsync(ctm => ctm.Id == id && ctm.Tinhtrang == true);
            if (customer != null)
            {
                customer.Tinhtrang = false;
                _context.Nguoidungs.Update(customer);
            }
            await _context.SaveChangesAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        //Find by name/id/role
        public async Task<IEnumerable<Nguoidung>> Search(string key)
        {
            return await _context.Nguoidungs.Where(ctm => (ctm.Id.ToString().Contains(key) || ctm.Hoten.Contains(key)) && ctm.Tinhtrang == true).ToListAsync();
        }
    }
}