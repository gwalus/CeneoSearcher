using System.Collections.Generic;
using WebEngine.Entities;

namespace WebEngine.Interfaces
{
    public interface IWebScraper
    {
        List<Product> GetListOfProducts(string html);
    }
}
