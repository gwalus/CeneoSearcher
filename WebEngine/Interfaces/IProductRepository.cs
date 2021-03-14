using System.Collections.Generic;
using System.Threading.Tasks;
using WebEngine.Model;

namespace WebEngine.Interfaces
{
    public interface IProductRepository
    {
        Task<ICollection<Product>> GetSubscibedProductsAsync();
        Task<bool> AddProduct(Product productToAdd);
        Task<bool> DeleteProduct(string link);
        Task<bool> IfProductExists(string link);
        Task<bool> UpdateProduct(Product productToUpdate);
    }
}
