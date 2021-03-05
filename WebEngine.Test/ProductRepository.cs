using System;
using System.Collections.Generic;

namespace WebEngine
{
    class ProductRepository : IProductRepository
    {
        private ProductContext _context;

        public ProductRepository(ProductContext context)
        {
            _context = context;
        }

        public ICollection<Product> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
