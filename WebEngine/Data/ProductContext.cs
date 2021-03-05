﻿using Microsoft.EntityFrameworkCore;
using WebEngine.Model;

namespace WebEngine.Data
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}
