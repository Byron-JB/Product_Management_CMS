using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Interface
{
    public interface IProductTypeRepository
    {
        Task<IEnumerable<ProductType>> GetAllAsync();
        Task<ProductType?> GetByIdAsync(int id);
        Task<ProductType> CreateAsync(ProductType productType);
        Task UpdateAsync(ProductType productType);
        Task DeleteAsync(int id);
    }
}
