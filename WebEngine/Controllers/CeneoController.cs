using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;
using Shared.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebEngine.Interfaces;

namespace WebEngine.Controllers
{
    /// <summary>
    /// Ceneo controller class. Contains endpoints to work with ceneo.
    /// </summary>
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

        /// <summary>
        /// Endpoint returns products from ceneo.
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns>List of productsdto.</returns>
        [HttpGet("products/search/{keyword}")]
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

        /// <summary>
        /// Endpoint returns subscribed products.
        /// </summary>
        /// <returns>List of products.</returns>
        [HttpGet("products")]
        public async Task<ActionResult<ICollection<Product>>> GetProductsAsync()
        {
            var products = await _productRepository.GetSubscibedProductsAsync();

            if (products != null)
                return Ok(products);

            return BadRequest("Data cannot be retrieved");
        }

        /// <summary>
        /// Endpoint to add product to be subscribed.
        /// </summary>
        /// <param name="productToAdd"></param>
        /// <returns>ActionResult.</returns>
        [HttpPost("product/subscribe")]
        public async Task<ActionResult> SubscribeProduct(Product productToAdd)
        {
            if (await _productRepository.IfProductExists(productToAdd.Link))
                return BadRequest("Product is already subscribed");

            if (await _productRepository.AddProduct(productToAdd))
                return Ok("Product has been subscribed");

            return BadRequest();
        }

        /// <summary>
        /// Endpoint to unsubscribe a product.
        /// </summary>
        /// <param name="link"></param>
        /// <returns>ActionResult.</returns>
        [HttpPost("product/unsubscribe")]
        public async Task<ActionResult> UnsubscribeProduct(string link)
        {
            if (await _productRepository.DeleteProduct(link))
                return Ok("Product has been unsubscribed");

            return BadRequest("Something went wrong");
        }

        /// <summary>
        /// Endpoint to check actual product price.
        /// </summary>
        /// <returns>ActionResult or subscribed products when price was changed.</returns>
        [HttpGet("product/checkprice")]
        public async Task<ActionResult<IList<Product>>> CheckProductsPrice()
        {
            var products = await _productRepository.GetSubscibedProductsAsync();

            if (products == null) 
                return BadRequest("Data not found");

            int updatedProducts = 0;

            foreach (var product in products)
            {
                double priceFromCeneo = _ceneoWebScraper.GetProductPrice(product.Link);
                double currentPrice = double.Parse(product.Price);

                if (priceFromCeneo != currentPrice && priceFromCeneo != 0)
                {
                    product.Price = priceFromCeneo.ToString();
                    if (await _productRepository.UpdateProduct(product)) updatedProducts++;
                }
            }

            if(updatedProducts > 0)
                return Ok(await _productRepository.GetSubscibedProductsAsync());

            return Ok("Product prices are up-to-date");
        }
    }
}
