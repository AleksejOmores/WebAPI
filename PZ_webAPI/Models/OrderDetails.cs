using Microsoft.OpenApi.Models;

namespace PZ_webAPI.Models
{
    public class OrderDetails
    {
        public Orders OrderID { get; set; }
        public Products ProductID { get; set; }
        public int Quantity { get; set; }
    }
}
