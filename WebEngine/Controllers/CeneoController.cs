using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebEngine.Interfaces;
using WebEngine.Model;

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
