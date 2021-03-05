using System;
using System.Collections.Generic;

namespace WebEngine
{
    class ProductRepository : IProductRepository
    {
        public ICollection<Product> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
