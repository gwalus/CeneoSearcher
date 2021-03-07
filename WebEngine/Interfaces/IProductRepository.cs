﻿using System.Collections.Generic;
using System.Threading.Tasks;
using WebEngine.Model;

namespace WebEngine.Interfaces
{
    public interface IProductRepository
    {
        Task<ICollection<Product>> GetSubscibedProductsAsync();
    }
}
