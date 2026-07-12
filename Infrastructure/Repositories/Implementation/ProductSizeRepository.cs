using Domain.Entities;
using Infrastructure.Persistence;
using Infrastructure.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Implementation
{
    public class ProductSizeRepository : IProductSizeRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductSizeRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ProductSize> CreateAsync(ProductSize productSize, System.Threading.CancellationToken cancellationToken)
        {
            _db.ProductSizes.Add(productSize);
            await _db.SaveChangesAsync(cancellationToken);
            return productSize;
        }

        public async Task DeleteAsync(int id, System.Threading.CancellationToken cancellationToken)
        {
            var entity = await _db.ProductSizes.FirstOrDefaultAsync(ps => ps.iId == id, cancellationToken);
            if (entity != null)
            {
                _db.ProductSizes.Remove(entity);
                await _db.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<IEnumerable<ProductSize>> GetAllAsync(System.Threading.CancellationToken cancellationToken)
        {
            return await _db.ProductSizes.ToListAsync(cancellationToken);
        }

        public async Task<Domain.Common.PagedResult<ProductSize>> GetPagedAsync(int pageNumber, int pageSize, System.Threading.CancellationToken cancellationToken)
        {
            var query = _db.ProductSizes.AsQueryable();
            var total = await query.CountAsync(cancellationToken);
            var items = await query.OrderBy(ps => ps.iId).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
            return new Domain.Common.PagedResult<ProductSize>
            {
                Items = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = total
            };
        }

        public async Task<ProductSize?> GetByIdAsync(int id, System.Threading.CancellationToken cancellationToken)
        {
            return await _db.ProductSizes.FirstOrDefaultAsync(ps => ps.iId == id, cancellationToken);
        }

        public async Task UpdateAsync(ProductSize productSize, System.Threading.CancellationToken cancellationToken)
        {
            _db.ProductSizes.Update(productSize);
            await _db.SaveChangesAsync(cancellationToken);
        }
    }
}
