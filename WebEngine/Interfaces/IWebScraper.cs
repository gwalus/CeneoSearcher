using Shared.Model;
using System.Collections.Generic;

namespace WebEngine.Interfaces
{
    public interface IWebScraper
    {
        /// <summary>
        /// Scraps items from the page
        /// </summary>
        /// <param name="html">Link to list of products</param>
        /// <returns>List of Products</returns>
        List<Product> GetListOfProducts(string html);
        /// <summary>
        /// Gets item the lowest price
        /// </summary>
        /// <param name="link">Link to the specified product</param>
        /// <returns>Single product price</returns>
        double GetProductPrice(string link);
    }
}
