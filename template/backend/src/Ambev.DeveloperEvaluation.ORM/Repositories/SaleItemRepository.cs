using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    /// <summary>
    /// Repository class for handling operations related to the SaleItem entity in the data layer.
    /// </summary>
    public class SaleItemRepository : ISaleItemRepository
    {
        private readonly DefaultContext _context;
        /// <summary>
        /// Initialize a new instance of <see cref="SaleItemRepository"/>.
        /// </summary>
        /// <param name="context"></param>
        public SaleItemRepository(DefaultContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates a new sale item in database.
        /// </summary>
        /// <param name="saleItem"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<SaleItem> CreateAsync(SaleItem saleItem, CancellationToken cancellationToken = default)
        {
            await _context.SaleItems.AddAsync(saleItem, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return saleItem;
        }

        /// <summary>
        /// Permanently deletes a sale item by its identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var saleItem = await GetByIdAsync(id, cancellationToken);
            if (saleItem == null)
                return false;

            _context.SaleItems.Remove(saleItem);
            _context.SaveChanges();
            return true;
        }

        /// <summary>
        /// Retrieves a paginated list of sale items with total count.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<SaleItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.SaleItems
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        /// <summary>
        /// Retrieves a list of sale items by their sale identifier.
        /// </summary>
        /// <param name="saleId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<List<SaleItem>> GetBySaleIdAsync(Guid saleId, CancellationToken cancellationToken = default)
        {
            return await _context.SaleItems
                .Where(x => x.SaleId == saleId)
                .ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Updates an existing sale item in the database.
        /// </summary>
        /// <param name="saleItem"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<SaleItem> UpdateAsync(SaleItem saleItem, CancellationToken cancellationToken = default)
        {
            _context.SaleItems.Update(saleItem);
            await _context.SaveChangesAsync(cancellationToken);
            return saleItem;
        }
    }
}
