using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Interface
{
    public interface IProductSizeRepository
    {
        Task<IEnumerable<ProductSize>> GetAllAsync();
        Task<ProductSize?> GetByIdAsync(int id);
        Task<ProductSize> CreateAsync(ProductSize productSize);
        Task UpdateAsync(ProductSize productSize);
        Task DeleteAsync(int id);
    }
}
