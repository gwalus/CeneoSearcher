using System.Collections.Generic;
using System.Linq;
using WebEngine.Data;
using WebEngine.Interfaces;
using WebEngine.Model;

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
