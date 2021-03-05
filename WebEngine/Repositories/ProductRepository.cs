<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using WebEngine.Data;
using WebEngine.Entities;
using WebEngine.Interfaces;

namespace WebEngine.Repositories
=======
﻿using System.Collections.Generic;
using System.Linq;

namespace WebEngine
>>>>>>> gw/25/ceneoControllerTests
{
    public class ProductRepository : IProductRepository
    {
        private ProductContext _context;

        public ProductRepository(ProductContext context)
        {
            _context = context;
        }

        public ICollection<Product> GetAll()
        {
<<<<<<< HEAD
            throw new NotImplementedException();
=======
            return _context.Products.ToList();
>>>>>>> gw/25/ceneoControllerTests
        }
    }
}
