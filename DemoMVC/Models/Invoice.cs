namespace DemoMVC.Models
{
    public class Invoice
    {
        public int Quantity { get; set; } // Số lượng
        public decimal UnitPrice { get; set; } // Đơn giá
        public decimal TotalPrice { get; set; } // Tổng tiền
    }
}