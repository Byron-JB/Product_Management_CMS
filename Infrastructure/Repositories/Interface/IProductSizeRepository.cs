using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Interface
{
    public interface IProductSizeRepository
    {
        Task<IEnumerable<ProductSize>> GetAllAsync(CancellationToken cancellationToken);
        Task<ProductSize?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<ProductSize> CreateAsync(ProductSize productSize, CancellationToken cancellationToken);
        Task UpdateAsync(ProductSize productSize, CancellationToken cancellationToken);
        Task DeleteAsync(int id, CancellationToken cancellationToken);
        Task<Domain.Common.PagedResult<ProductSize>> GetPagedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
    }
}
