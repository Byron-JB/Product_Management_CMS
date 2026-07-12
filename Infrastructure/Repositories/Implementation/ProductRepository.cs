using Domain.Entities;
using Infrastructure.Persistence;
using Infrastructure.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Implementation
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Product> CreateAsync(Product product, System.Threading.CancellationToken cancellationToken)
        {
            _db.Products.Add(product);
            await _db.SaveChangesAsync(cancellationToken);
            return product;
        }

        public async Task DeleteAsync(int id, System.Threading.CancellationToken cancellationToken)
        {
            var entity = await _db.Products.FirstOrDefaultAsync(p => p.iId == id, cancellationToken);
            if (entity != null)
            {
                _db.Products.Remove(entity);
                await _db.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<IEnumerable<Product>> GetAllAsync(System.Threading.CancellationToken cancellationToken)
        {
            return await _db.Products.Include(p => p.ProductType).Include(p => p.ProductSize).ToListAsync(cancellationToken);
        }

        public async Task<Domain.Common.PagedResult<Product>> GetPagedAsync(int pageNumber, int pageSize, System.Threading.CancellationToken cancellationToken)
        {
            var query = _db.Products.Include(p => p.ProductType).Include(p => p.ProductSize).AsQueryable();
            var total = await query.CountAsync(cancellationToken);
            var items = await query.OrderBy(p => p.iId).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
            return new Domain.Common.PagedResult<Product>
            {
                Items = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = total
            };
        }

        public async Task<Product?> GetByIdAsync(int id, System.Threading.CancellationToken cancellationToken)
        {
            return await _db.Products.Include(p => p.ProductType).Include(p => p.ProductSize).FirstOrDefaultAsync(p => p.iId == id, cancellationToken);
        }

        public async Task UpdateAsync(Product product, System.Threading.CancellationToken cancellationToken)
        {
            _db.Products.Update(product);
            await _db.SaveChangesAsync(cancellationToken);
        }
    }
}
