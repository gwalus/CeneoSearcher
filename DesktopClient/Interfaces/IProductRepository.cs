using Shared.Dtos;
using Shared.Model;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace DesktopClient.Interfaces
{
    /// <summary>
    /// Interface dla serwisu api.
    /// </summary>
    public interface IProductRepository
    {
        Task<string> SendProductRequestAsync(Product product, string url);
        Task<ICollection<ProductDto>> GetProductsAsync(string product);
        Task<string> SubscribeProductAsync(Product product);
        Task<ICollection<Product>> GetSubscribeProductsAsync();
        Task<string> UnSubscribeProductsAsync(string link);
        Task<ICollection<Product>> UpdateProductsAsync();
    }
}
