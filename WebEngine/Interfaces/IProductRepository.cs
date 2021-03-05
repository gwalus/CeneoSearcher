using System.Collections.Generic;
using WebEngine.Entities;

namespace WebEngine.Interfaces
{
    public interface IProductRepository
    {
        ICollection<Product> GetAll();
    }
}
