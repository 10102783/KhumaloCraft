using KhumaloCraft.Data;
using Microsoft.EntityFrameworkCore;

namespace KhumaloCraft.Repository
{
    public class ProductRepo
    {
        public readonly ProductContext _context;

        public ProductRepo(ProductContext context)
        {
            _context = context;
        }
        public void AddProduct()
        {
            //Define the SQL query or adding product

            var sqlQuery = @"
                INSERT INTO Product (name, price, category, pic_url, availability)
                VALUES
                        ('Product 1','R100.00','Bag','Available')";

                //Execute the Sql query
                _context.Database.ExecuteSqlRaw(sqlQuery);
        }
    }
}
