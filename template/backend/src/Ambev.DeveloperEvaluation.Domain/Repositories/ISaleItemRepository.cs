using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    /// <summary>
    /// Repository interface for handling operations related to the SaleItem entity in the data layer.
    /// </summary>
    public interface ISaleItemRepository
    {
        /// <summary>
        /// Creates a new sale item record asynchronously.
        /// </summary>
        /// <param name="saleItem"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<SaleItem> CreateAsync(SaleItem saleItem, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// Retrieves a sale item by its unique identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<SaleItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves a sale item by its product identifier.
        /// </summary>
        /// <param name="saleItem"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<SaleItem> UpdateAsync(SaleItem saleItem, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves a paginated list of sale items with total count.
        /// </summary>
        /// <param name="saleId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<SaleItem>> GetBySaleIdAsync(Guid saleId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes a sale item by its unique identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    }
}
