using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace WebEngine.Controllers
{
    public class CeneoController : BaseApiController
    {
        private readonly IProductRepository _productRepository;

        public CeneoController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public ActionResult<IList<Product>> GetProductsAsync()
        {
            return _productRepository.GetAll().ToList();
        }
    }
}
