using DesktopClient.Interfaces;
using Shared.Dtos;
using Shared.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;

namespace DesktopClient.Services
{
    public class ProductRepository : IProductRepository
    {
        private static readonly HttpClient _client = new HttpClient();
        private static readonly string uri = "https://localhost:5001/";
        public async Task<ICollection<ProductDto>> GetProductsAsync(string product)
        {
            var Link = HttpUtility.UrlEncode(product);
            var products = (await _client.GetFromJsonAsync(uri + $"getproductsbykeyword?keyword={Link}", typeof(ICollection<ProductDto>))) as ICollection<ProductDto>;
            return products;
        }

        public async Task<string> SubscribeProductAsync(Product product)
        {
            var message = await SendProductRequestAsync(product, $"{uri}subscribe");
            return message;
        }

        public async Task<string> SendProductRequestAsync(Product product, string url)
        {
            var response = string.Empty;
            var jsonString = JsonSerializer.Serialize<Product>(product);

            HttpContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(url),
                Content = content
            };

            HttpResponseMessage result = await _client.SendAsync(request);
            if (result.IsSuccessStatusCode)
            {
                response = result.StatusCode.ToString();
            }

            return response;
        }

        public async Task<string> SendProductRequestAsync(string url)
        {
            var response = string.Empty;

            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(url)
            };

            HttpResponseMessage result = await _client.SendAsync(request);
            if (result.IsSuccessStatusCode)
            {
                response = result.StatusCode.ToString();
            }

            return response;
        }

        public async Task<ICollection<Product>> GetSubscribeProductsAsync()
        {
            var products = (await _client.GetFromJsonAsync(uri + $"getsubscribedproducts", typeof(ICollection<Product>))) as ICollection<Product>;
            return products;
        }

        public async Task<string> UnSubscribeProductsAsync(string link)
        {
            var Link = HttpUtility.UrlEncode(link);
            var message = await SendProductRequestAsync($"{uri}unsubscribe?Link={Link}");
            return message;
        }
    }
}
