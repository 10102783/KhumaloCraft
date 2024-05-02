using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KhumaloCraft.Models
{
    public class Transactions
    {
        [Key]
        public int TransactionID { get; set; }
        
      
        [ForeignKey("Product")]
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public int TotalAmount { get; set; }

        [DataType(DataType.Date)]
        public DateTime TransactionDate { get; set; }

        [DataType(DataType.Time)]
        public DateTime TransactionTime { get; set; }




      
        public Product Product { get; set; }

    }
}
