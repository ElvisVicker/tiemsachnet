using System.ComponentModel.DataAnnotations;
using tiemsach.Data;

namespace tiemsach.ViewModels
{
    public class CartItem
    {
        public int Id { get; set; }
        public int SoLuong { get; set; }
        public string TenSach { get; set; }
        public double GiaXuat { get; set; }
    }
}
