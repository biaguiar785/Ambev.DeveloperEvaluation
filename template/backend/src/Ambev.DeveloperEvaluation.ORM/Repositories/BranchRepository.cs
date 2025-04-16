using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    /// <summary>
    /// Repository class for handling operations related to the Branch entity in the data layer.
    /// </summary>
    public class BranchRepository : IBranchRepository
    {
        private readonly DefaultContext _context;
        /// <summary>
        /// Initialize a new instance of <see cref="BranchRepository"/>.
        /// </summary>
        /// <param name="context"></param>
        public BranchRepository(DefaultContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates a new branch in database.
        /// </summary>
        /// <param name="branch"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Branch> CreateAsync(Branch branch, CancellationToken cancellationToken = default)
        {
            await _context.Branches.AddAsync(branch, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return branch;
        }

        /// <summary>
        /// Permanently deletes a branch by its identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var branch = await GetByIdAsync(id, cancellationToken);
            if (branch == null)
                return false;

            _context.Branches.Remove(branch);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        /// <summary>
        /// Retrieves a paginated list of branches with total count.
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<(List<Branch>, int totalCount)> GetAllPaginatedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
            var query =  _context.Branches.AsQueryable();

            var totalCount = await query.CountAsync(cancellationToken);

            var branches = await query
                .OrderBy(x => x.Name)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return(branches, totalCount);
        }

        /// <summary>
        /// Retrieves a branch by its unique identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Branch?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var branch = await _context.Branches.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            return branch;
        }

        /// <summary>
        /// Retrieves a branch by its name.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Branch?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            var branch = await _context.Branches.FirstOrDefaultAsync(x => x.Name == name, cancellationToken);
            return branch;
        }
    }
}
