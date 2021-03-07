using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebEngine.Interfaces;
using WebEngine.Model;

namespace WebEngine.Controllers
{
    public class CeneoController : BaseApiController
    {
        private readonly IProductRepository _productRepository;
        private readonly IWebScraper _ceneoWebScraper;

        private readonly string _baseUrl = "https://www.ceneo.pl/;szukaj-";

        public CeneoController(IProductRepository productRepository, IWebScraper ceneoWebScraper)
        {
            _productRepository = productRepository;
            _ceneoWebScraper = ceneoWebScraper;
        }

        [HttpGet("/getproductsbykeyword")]
        public ActionResult<IList<Product>> GetProductsFromCeneo(string keyword)
        {
            return _ceneoWebScraper.GetListOfProducts(_baseUrl + keyword);
        }

        [HttpGet("/getsubscribedproducts")]
        public async Task<ICollection<Product>> GetProductsAsync()
        {
            return await _productRepository.GetSubscibedProductsAsync();
        }
    }
}
