using System.ComponentModel.DataAnnotations;

namespace KhumaloCraft.Models
{
    public enum OrderStatus
    {
        Pending,
        Processed,
        Shipped,
        Delivered,
        Cancelled
    
    
    }




    public class Order
    {
        public int OrderId { get; set; }
        public string UserId { get; set; } // Foreign key to identify the user
        public AppUser User { get; set; } // Navigation property
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; } //status of the order
        public List<OrderItem> OrderItems { get; set; }
        // Add additional properties as needed
    }
}
