using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    /// <summary>
    /// Repository interface for handling operations related to the Product entity in the data layer.
    /// </summary>
    public interface IProductRepository
    {
        /// <summary>
        /// Creates a new product record asynchronously.
        /// </summary>
        /// <param name="product"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Product> CreateAsync(Product product, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves a product by its unique identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves a product by its name.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Product?> GetByNameAsync(string name, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves a product by its name and active status.
        /// </summary>
        /// <param name="product"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Product> UpdateAsync(Product product, CancellationToken cancellationToken = default);

        /// <summary>
        /// Permanently deletes a product by its identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves a paginated list of products with total count.
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<(List<Product>, int totalCount)> GetAllPaginatedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default);
       
        /// <summary>
        /// Retrieves a list of all product categories.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<string>> GetCategoriesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves a paginated list of products by category.
        /// </summary>
        /// <param name="category"></param>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <param name="order"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<(List<Product>, int totalCount)> GetByCategoryPaginatedAsync(string category, int page, int size, string? order, CancellationToken cancellationToken);
    }
}
