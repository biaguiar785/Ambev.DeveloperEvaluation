using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    /// <summary>
    /// Repository interface for handling operations related to the Sale entity in the data layer.
    /// </summary>
    public interface ISaleRepository
    {
        /// <summary>
        /// Creates a new sale record asynchronously.
        /// </summary>
        /// <param name="sale">The sale entity to create.</param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        /// <returns>The created sale.</returns>
        Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves a sale by its unique identifier.
        /// </summary>
        /// <param name="id">The sale's unique identifier.</param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        /// <returns>The sale if found; otherwise, null.</returns>
        Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves a sale by its sale number.
        /// </summary>
        /// <param name="saleNumber">The unique sale number.</param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        /// <returns>The sale if found; otherwise, null.</returns>
        Task<Sale?> GetBySaleNumberAsync(string saleNumber, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates an existing sale record.
        /// </summary>
        /// <param name="sale">The updated sale entity.</param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        /// <returns>The updated sale.</returns>
        Task<Sale> UpdateAsync(Sale sale, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves a paginated list of sales with total count.
        /// </summary>
        /// <param name="page">The page number to retrieve.</param>
        /// <param name="pageSize">The number of records per page.</param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        /// <returns>A tuple containing the list of sales and the total count.</returns>
        Task<(List<Sale> Sales, int TotalCount)> GetAllPaginatedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default);

        /// <summary>
        /// Cancels a sale by marking it as cancelled.
        /// </summary>
        /// <param name="id">The unique identifier of the sale to cancel.</param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        /// <returns>True if the operation was successful; otherwise, false.</returns>
        Task<bool> CancelAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Permanently deletes a sale by its identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the sale to delete.</param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        /// <returns>True if deletion was successful; otherwise, false.</returns>
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
