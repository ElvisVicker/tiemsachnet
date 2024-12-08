using tiemsach.Data;
using tiemsach.Models;
using tiemsach.Repositories.tiemsach.Repositories;

//using System.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace tiemsach.Repositories
{
    public interface IDiaChiRepository
    {
        Task<IEnumerable<Diachi>> GetAllAsync();
        Task<Diachi?> GetAddressById(int id);
        Task<IEnumerable<Diachi>> GetCustomerAddress(int? id);
    }
    public class DiaChiRepository : IDiaChiRepository
    {
        private TiemsachContext _context;
        private KhachHangRepository repoCtm;

        public DiaChiRepository(TiemsachContext context)
        {
            _context = context;
            repoCtm = new KhachHangRepository(_context);
        }

        public async Task<IEnumerable<Diachi>> GetAllAsync()
        {
            return await _context.Diachis.Where(adr => adr.Tinhtrang == true).ToListAsync();
        }

        public async Task<Diachi?> GetAddressById(int id)
        {
            return await _context.Diachis.FirstOrDefaultAsync(adr => adr.Id == id && adr.Tinhtrang == true);
        }

        public async Task<IEnumerable<Diachi>?> GetCustomerAddress(int? userId)
        {
            var Detail_address = await repoCtm.GetCustomerById(userId);
            var list = new List<Diachi>();
            foreach (Khachhang ctm in Detail_address)
                list.Add(await GetAddressById((int)ctm.DiachiId));
            return list;
        }
    }
}
