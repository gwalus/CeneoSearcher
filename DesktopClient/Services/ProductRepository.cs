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
    /// Klasa serwisu zapytań do api.
    /// </summary>
    public class ProductRepository : IProductRepository
    {
        private static readonly HttpClient _client = new HttpClient();
        private static string uri;

        /// <summary>
        /// Konstruktor wpisujący serwer uri do zmienej uri.
        /// </summary>
        /// <param name="serwerUri">uri typu string</param>
        public ProductRepository(string serwerUri = "https://localhost:5001/")
        {
            uri = serwerUri;
        }

        /// <summary>
        /// Zapytanie o produkty o podanej nazwie.
        /// </summary>
        /// <param name="product">Nazwa produktu typu string</param>
        /// <returns>Kolekcja produktów</returns>
        public async Task<ICollection<ProductDto>> GetProductsAsync(string product)
        {
            var Link = HttpUtility.UrlEncode(product);
            var products = (await _client.GetFromJsonAsync(uri + $"api/Ceneo/products/search/{Link}", typeof(ICollection<ProductDto>))) as ICollection<ProductDto>;
            return products;
        }

        /// <summary>
        /// Zapytania aby subskrybować produkt.
        /// </summary>
        /// <param name="product">Produkt typu Product</param>
        /// <returns>Odpowiedz z serwera typu wiadomość</returns>
        public async Task<string> SubscribeProductAsync(Product product)
        {
            var message = await SendProductRequestAsync(product, $"{uri}api/Ceneo/product/subscribe");
            return message;
        }

        /// <summary>
        /// Wysyłanie zapytania pod podany url z jsonem produktu.
        /// </summary>
        /// <param name="product">Produkt typu Product</param>
        /// <param name="url">url typu string</param>
        /// <returns>Odpowiedz z serwera typu wiadomość</returns>
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
        /// Wysyłanie prostego zapytania url.
        /// </summary>
        /// <param name="url">Url zapytania typu string</param>
        /// <returns>Odpowiedz z serwera typu wiadomość</returns>
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
        /// Zapytanie o subskrybowane produkty.
        /// </summary>
        /// <returns>Kolekcja subskrybowanych produktów</returns>
        public async Task<ICollection<Product>> GetSubscribeProductsAsync()
        {
            var products = (await _client.GetFromJsonAsync(uri + $"api/Ceneo/products", typeof(ICollection<Product>))) as ICollection<Product>;
            return products;
        }

        /// <summary>
        /// Zapytania aby znieść subskrybcję produktu.
        /// </summary>
        /// <param name="link">Id produktu typu string</param>
        /// <returns>Odpowiedz z serwera typu wiadomość</returns>
        public async Task<string> UnSubscribeProductsAsync(string link)
        {
            var Link = HttpUtility.UrlEncode(link);
            var message = await SendProductRequestAsync($"{uri}api/Ceneo/product/unsubscribe?Link={Link}");
            return message;
        }

        /// <summary>
        /// Zapytanie sprawdzające cene produktów.
        /// </summary>
        /// <returns>Kolekcja subskrybowanych produktów</returns>
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
