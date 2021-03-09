﻿using DesktopClient.Interfaces;
using DesktopClient.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DesktopClient.Services
{
    public class ProductRepository : IProductRepository
    {
        private static readonly HttpClient _client = new HttpClient();
        private static readonly string uri = "https://localhost:5001/";
        public async Task<ICollection<Product>> GetProductsAsync(string product)
        {
            var products = (await _client.GetFromJsonAsync(uri + $"getproductsbykeyword?keyword={product}", typeof(ICollection<Product>))) as ICollection<Product>;

            return products;
        }

        public async Task<string> SubscribeProductAsync(Product product)
        {

            
            var response = string.Empty;

            var jsonString = JsonSerializer.Serialize<Product>(product);

            HttpContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");
           

            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"{uri}subscribe"),
                Content = content
            };

            HttpResponseMessage result = await _client.SendAsync(request);
            if (result.IsSuccessStatusCode)
            {
                response = result.StatusCode.ToString();
            }

            return response;
        }
        
    }
}