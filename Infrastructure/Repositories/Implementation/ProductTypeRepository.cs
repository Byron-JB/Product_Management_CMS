using Domain.Entities;
using Infrastructure.Persistence;
using Infrastructure.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Implementation
{
    public class ProductTypeRepository : IProductTypeRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductTypeRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ProductType> CreateAsync(ProductType productType, System.Threading.CancellationToken cancellationToken)
        {
            _db.ProductTypes.Add(productType);
            await _db.SaveChangesAsync(cancellationToken);
            return productType;
        }

        public async Task DeleteAsync(int id, System.Threading.CancellationToken cancellationToken)
        {
            var entity = await _db.ProductTypes.FirstOrDefaultAsync(pt => pt.iId == id, cancellationToken);
            if (entity != null)
            {
                _db.ProductTypes.Remove(entity);
                await _db.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<IEnumerable<ProductType>> GetAllAsync(System.Threading.CancellationToken cancellationToken)
        {
            return await _db.ProductTypes.ToListAsync(cancellationToken);
        }

        public async Task<Domain.Common.PagedResult<ProductType>> GetPagedAsync(int pageNumber, int pageSize, System.Threading.CancellationToken cancellationToken)
        {
            var query = _db.ProductTypes.AsQueryable();
            var total = await query.CountAsync(cancellationToken);
            var items = await query.OrderBy(pt => pt.iId).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
            return new Domain.Common.PagedResult<ProductType>
            {
                Items = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = total
            };
        }

        public async Task<ProductType?> GetByIdAsync(int id, System.Threading.CancellationToken cancellationToken)
        {
            return await _db.ProductTypes.FirstOrDefaultAsync(pt => pt.iId == id, cancellationToken);
        }

        public async Task UpdateAsync(ProductType productType, System.Threading.CancellationToken cancellationToken)
        {
            _db.ProductTypes.Update(productType);
            await _db.SaveChangesAsync(cancellationToken);
        }
    }
}
