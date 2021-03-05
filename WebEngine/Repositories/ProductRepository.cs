using System;
using System.Collections.Generic;
using WebEngine.Entities;
using WebEngine.Interfaces;

namespace WebEngine.Repositories
{
    public class ProductRepository : IProductRepository
    {
        public ICollection<Product> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
