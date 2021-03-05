using System;
using System.Collections.Generic;
using WebEngine.Data;
using WebEngine.Entities;
using WebEngine.Interfaces;

namespace WebEngine.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private ProductContext _context;

        public ProductRepository(ProductContext context)
        {
            _context = context;
        }

        public ICollection<Product> GetAll()
        {
            return _context.Products.ToList();
        }
    }
}
