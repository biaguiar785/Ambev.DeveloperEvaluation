using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    /// <summary>
    /// Repository interface for handling operations related to the Branch entity in the data layer.
    /// </summary>
    public interface IBranchRepository
    {
        /// <summary>
        /// Creates a new branch record asynchronously.
        /// </summary>
        /// <param name="branch"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Branch> CreateAsync(Branch branch, CancellationToken cancellationToken = default);
       
        /// <summary>
        /// Retrieves a branch by its unique identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Branch?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// Retrieves a branch by its name.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Branch?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// Retrieves a branch by its name and active status.
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<(List<Branch>, int totalCount)> GetAllPaginatedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// Permanently deletes a branch by its identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        
    }
}
