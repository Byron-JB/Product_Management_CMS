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

        public async Task<ProductType> CreateAsync(ProductType productType)
        {
            _db.ProductTypes.Add(productType);
            await _db.SaveChangesAsync();
            return productType;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _db.ProductTypes.FindAsync(id);
            if (entity != null)
            {
                _db.ProductTypes.Remove(entity);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<ProductType>> GetAllAsync()
        {
            return await _db.ProductTypes.ToListAsync();
        }

        public async Task<ProductType?> GetByIdAsync(int id)
        {
            return await _db.ProductTypes.FirstOrDefaultAsync(pt => pt.iId == id);
        }

        public async Task UpdateAsync(ProductType productType)
        {
            _db.ProductTypes.Update(productType);
            await _db.SaveChangesAsync();
        }
    }
}
