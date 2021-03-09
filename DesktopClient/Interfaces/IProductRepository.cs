using DesktopClient.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DesktopClient.Interfaces
{
    public interface IProductRepository
    {
        Task<ICollection<Product>> GetProductsAsync(string product);
    }
}
