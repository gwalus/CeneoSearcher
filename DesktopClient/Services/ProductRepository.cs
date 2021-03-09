using DesktopClient.Interfaces;
using DesktopClient.Model;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace DesktopClient.Services
{
    public class ProductRepository : IProductRepository
    {
        private static readonly HttpClient _client = new HttpClient();
        private static readonly string uri = "https://localhost:5001/getproductsbykeyword";
        public async Task<ICollection<Product>> GetProductsAsync(string product)
        {
            var products = (await _client.GetFromJsonAsync(uri + $"?keyword={product}", typeof(ICollection<Product>))) as ICollection<Product>;

            return products;
        }

    }
}
