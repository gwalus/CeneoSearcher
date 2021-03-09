using System.Collections.Generic;
using WebEngine.Model;

namespace WebEngine.Interfaces
{
    public interface IWebScraper
    {
        List<Product> GetListOfProducts(string html);
        double UpdatePrice(string link);
    }
}
