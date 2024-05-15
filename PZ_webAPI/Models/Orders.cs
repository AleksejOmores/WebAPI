namespace PZ_webAPI.Models
{
    public class Orders
    {
        public int OrderID { get; set; }
        public Users UserID { get; set; }
        public DateOnly OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
