using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    /// <summary>
    /// Repository class for handling operations related to the Product entity in the data layer.
    /// </summary>
    public class ProductRepository : IProductRepository
    {
        private readonly DefaultContext _context;
        /// <summary>
        /// Initialize a new instance of <see cref="ProductRepository"/>.
        /// </summary>
        /// <param name="context"></param>
        public ProductRepository(DefaultContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Creates a new product in database.
        /// </summary>
        /// <param name="product"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Product> CreateAsync(Product product, CancellationToken cancellationToken = default)
        {
            await _context.Products.AddAsync(product, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return product;
        }

        /// <summary>
        /// Permanently deletes a product by its identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var product = await GetByIdAsync(id, cancellationToken);
            if (product == null)
                return false;

            _context.Products.Remove(product);
            _context.SaveChanges();
            return true;
        }

        /// <summary>
        /// Retrieves a paginated list of products with total count.
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<List<Product>> GetAllPaginatedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
          return await _context.Products                
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
           
        }

        /// <summary>
        /// Retrieves a paginated list of products by category with total count.
        /// </summary>
        /// <param name="category"></param>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <param name="order"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(List<Product>, int totalCount)> GetByCategoryPaginatedAsync(string category, int page, int size, string? order, CancellationToken cancellationToken)
        {
            var query = _context.Products.AsQueryable();

            var totalCount = await query.CountAsync();

            var products = await query
                .Where(x => x.Category == category)
                .OrderBy(x => x.Name)
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync(cancellationToken);

            return(products, totalCount);
        }

        /// <summary>
        /// Retrieves a product by its unique identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Products.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        /// <summary>
        /// Retrieves a product by its name.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Product?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _context.Products.FirstOrDefaultAsync(x => x.Name == name, cancellationToken);
        }

        /// <summary>
        /// Retrieves a list of product categories.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<string>> GetCategoriesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Products
               .Select(p => p.Category)
               .Distinct()
               .OrderBy(c => c)
               .ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Updates an existing product in the database.
        /// </summary>
        /// <param name="product"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Product> UpdateAsync(Product product, CancellationToken cancellationToken = default)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync(cancellationToken);
            return product;
        }
    }
}
