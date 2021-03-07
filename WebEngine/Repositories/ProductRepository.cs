using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        public async Task<bool> AddProduct(Product productToAdd)
        {
            await _context.AddAsync(productToAdd);
            if (await _context.SaveChangesAsync() > 0)
                return true;
            return false;
        }

        public async Task<ICollection<Product>> GetSubscibedProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }
    }
}
