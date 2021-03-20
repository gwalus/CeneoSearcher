using Shared.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebEngine.Interfaces
{
    /// <summary>
    /// Product repository interface contains methods to operation of products.
    /// </summary>
    public interface IProductRepository
    {
        /// <summary>
        /// Returns subscribed products from database.
        /// </summary>
        /// <returns>List of products</returns>
        Task<ICollection<Product>> GetSubscibedProductsAsync();

        /// <summary>
        /// Add product to database.
        /// </summary>
        /// <param name="productToAdd"></param>
        /// <returns>Bool</returns>
        Task<bool> AddProduct(Product productToAdd);

        /// <summary>
        /// Delete product from database.
        /// </summary>
        /// <param name="link"></param>
        /// <returns>Bool</returns>
        Task<bool> DeleteProduct(string link);

        /// <summary>
        /// Check if product already exists in database.
        /// </summary>
        /// <param name="link"></param>
        /// <returns>Bool</returns>
        Task<bool> IfProductExists(string link);

        /// <summary>
        /// Update product in database.
        /// </summary>
        /// <param name="productToUpdate"></param>
        /// <returns>Bool</returns>
        Task<bool> UpdateProduct(Product productToUpdate);
    }
}
