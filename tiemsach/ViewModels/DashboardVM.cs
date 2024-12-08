using tiemsach.Data;

namespace tiemsach.ViewModels
{


    public class DashboardVM
    {
        public List<Phieunhap> Phieunhaps { get; set; }
        public List<Phieuxuat> Phieuxuats { get; set; }

        public List<Chitietphieuxuat> Chitietphieuxuats { get; set; }

        public List<Khachhang> Khachhangs { get; set; }

        public List<Nhanvien> Nhanviens { get; set; }
        public int TotalNV { get; set; }
        public int TotalKH { get; set; }

        public int TotalSach { get; set; }
   
        public double TotalProfit { get; set; }


        public List<string> LineChartLabels { get; set; }
        public List<int> LineChartData { get; set; }

    }
}
