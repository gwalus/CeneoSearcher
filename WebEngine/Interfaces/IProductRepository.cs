using System.Collections.Generic;

namespace WebEngine
{
    public interface IProductRepository
    {
        ICollection<Product> GetAll();
    }
}
