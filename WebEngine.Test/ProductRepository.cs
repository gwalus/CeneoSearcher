using System.Collections.Generic;
using System.Linq;

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
            return _context.Products.ToList();
        }
    }
}
