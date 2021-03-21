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
    /// <summary>
    /// The service class of the queries to the api.
    /// </summary>
    public class ProductRepository : IProductRepository
    {
        private static readonly HttpClient _client = new HttpClient();
        private static string uri;

        /// <summary>
        /// Constructor that puts a uri server into the uri variable..
        /// </summary>
        /// <param name="serwerUri">uri as string</param>
        public ProductRepository(string serwerUri = "https://localhost:5001/")
        {
            uri = serwerUri;
        }

        /// <summary>
        /// Query for products with a given name.
        /// </summary>
        /// <param name="product">Name of product as string</param>
        /// <returns>Kolekcja produktów</returns>
        public async Task<ICollection<ProductDto>> GetProductsAsync(string product)
        {
            var Link = HttpUtility.UrlEncode(product);
            var products = (await _client.GetFromJsonAsync(uri + $"api/Ceneo/products/search/{Link}", typeof(ICollection<ProductDto>))) as ICollection<ProductDto>;
            return products;
        }

        /// <summary>
        /// Query for item subscribing.
        /// </summary>
        /// <param name="product">Product</param>
        /// <returns>Feedback from server</returns>
        public async Task<string> SubscribeProductAsync(Product product)
        {
            var message = await SendProductRequestAsync(product, $"{uri}api/Ceneo/product/subscribe");
            return message;
        }

        /// <summary>
        /// Sending a query to the given url with product json.
        /// </summary>
        /// <param name="product">Product</param>
        /// <param name="url">url as string</param>
        /// <returns>Feedback from the server</returns>
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

        /// <summary>
        /// Url request
        /// </summary>
        /// <param name="url">Url for string queries</param>
        /// <returns>Feedback from the server</returns>
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

        /// <summary>
        /// Request for subscribed products.
        /// </summary>
        /// <returns>Collection of subscribed products</returns>
        public async Task<ICollection<Product>> GetSubscribeProductsAsync()
        {
            var products = (await _client.GetFromJsonAsync(uri + $"api/Ceneo/products", typeof(ICollection<Product>))) as ICollection<Product>;
            return products;
        }

        /// <summary>
        /// Unsubscribing product
        /// </summary>
        /// <param name="link">Id of product as string</param>
        /// <returns>Feedback from the server</returns>
        public async Task<string> UnSubscribeProductsAsync(string link)
        {
            var Link = HttpUtility.UrlEncode(link);
            var message = await SendProductRequestAsync($"{uri}api/Ceneo/product/unsubscribe?Link={Link}");
            return message;
        }

        /// <summary>
        /// Check price 
        /// </summary>
        /// <returns>Collection of subscribed items</returns>
        public async Task<ICollection<Product>> UpdateProductsAsync()
        {
            //var message = await SendProductRequestAsync($"{uri}api/Ceneo/product/checkprice");
            try
            {
                var products = (await _client.GetFromJsonAsync($"{uri}api/Ceneo/product/checkprice", typeof(ICollection<Product>))) as ICollection<Product>;
                return products;
            }
            catch (Exception)
            {
                return null;
                throw;
            }  
        }
    }
}
