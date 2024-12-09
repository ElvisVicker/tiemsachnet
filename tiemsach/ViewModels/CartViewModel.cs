namespace tiemsach.ViewModels
{
    public class CartViewModel
    {
        public List<CartItem> Items { get; set; } = new List<CartItem>();
        public double TotalPrice { get; set; }

        public double giaXuat { get; set; }

    }
}
