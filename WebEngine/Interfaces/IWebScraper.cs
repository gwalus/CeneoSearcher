using Shared.Model;
using System.Collections.Generic;

namespace WebEngine.Interfaces
{
    public interface IWebScraper
    {
        List<Product> GetListOfProducts(string html);
        double GetProductPrice(string link);
    }
}
