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

        public async Task<ProductSize> CreateAsync(ProductSize productSize)
        {
            _db.ProductSizes.Add(productSize);
            await _db.SaveChangesAsync();
            return productSize;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _db.ProductSizes.FindAsync(id);
            if (entity != null)
            {
                _db.ProductSizes.Remove(entity);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<ProductSize>> GetAllAsync()
        {
            return await _db.ProductSizes.ToListAsync();
        }

        public async Task<ProductSize?> GetByIdAsync(int id)
        {
            return await _db.ProductSizes.FirstOrDefaultAsync(ps => ps.iId == id);
        }

        public async Task UpdateAsync(ProductSize productSize)
        {
            _db.ProductSizes.Update(productSize);
            await _db.SaveChangesAsync();
        }
    }
}
