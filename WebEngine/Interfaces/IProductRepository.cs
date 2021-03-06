using System.Collections.Generic;
using WebEngine.Model;

namespace WebEngine.Interfaces
{
    public interface IProductRepository
    {
        ICollection<Product> GetAll();
    }
}
