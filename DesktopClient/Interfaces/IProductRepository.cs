using Shared.Dtos;
using Shared.Model;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace DesktopClient.Interfaces
{
    public interface IProductRepository
    {
        Task<string> SendProductRequestAsync(Product product, string url);
        Task<ICollection<ProductDto>> GetProductsAsync(string product);
        Task<string> SubscribeProductAsync(Product product);
        Task<ICollection<Product>> GetSubscribeProductsAsync();
        Task<string> UnSubscribeProductsAsync(string link);
    }
}
