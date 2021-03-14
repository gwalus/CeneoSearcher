using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebEngine.Interfaces;
using WebEngine.Model;

namespace WebEngine.Controllers
{
    public class CeneoController : BaseApiController
    {
        private readonly IProductRepository _productRepository;
        private readonly IWebScraper _ceneoWebScraper;
        private readonly IMapper _mapper;
        private readonly string _baseUrl = "https://www.ceneo.pl/;szukaj-";

        public CeneoController(IProductRepository productRepository, IWebScraper ceneoWebScraper, IMapper mapper)
        {
            _productRepository = productRepository;
            _ceneoWebScraper = ceneoWebScraper;
            _mapper = mapper;
        }

        [HttpGet("/getproductsbykeyword")]
        public async Task<ActionResult<IList<ProductDto>>> GetProductsFromCeneo(string keyword)
        {
            var returnedProducts = new List<ProductDto>();

            var ceneoProducts = _ceneoWebScraper.GetListOfProducts($"{_baseUrl}{keyword}");

            if (ceneoProducts != null)
            {
                foreach (var product in ceneoProducts)
                {
                    var mappedProduct = _mapper.Map<Product, ProductDto>(product);

                    mappedProduct.IsSubscribed = await _productRepository.IfProductExists(product.Link);

                    returnedProducts.Add(mappedProduct);
                }

                return Ok(returnedProducts);
            }

            return NotFound("No products were found for the keyword");
        }

        [HttpGet("/getsubscribedproducts")]
        public async Task<ICollection<Product>> GetProductsAsync()
        {
            return await _productRepository.GetSubscibedProductsAsync();
        }

        [HttpPost("/subscribe")]
        public async Task<ActionResult> SubscribeProduct(Product productToAdd)
        {
            if (await _productRepository.AddProduct(productToAdd)) return Ok("Product has been subscribed");

            return BadRequest();
        }


        [HttpPost("/unsubscribe")]
        public async Task<ActionResult> UnsubscribeProduct(string link)
        {
            if (await _productRepository.DeleteProduct(link)) return Ok("Product has been unsubscribed");

            return BadRequest("Something went wrong");
        }
    }
}
