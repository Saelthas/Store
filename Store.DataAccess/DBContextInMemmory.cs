using Microsoft.EntityFrameworkCore;
using Store.Models.Product;
using System;

namespace Store.DataAccess
{
    public class DBContextInMemmory : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DBContextInMemmory(DbContextOptions options) : base(options)
        {
        }

        private void loadAccounts()
        {

        }
    }
}
