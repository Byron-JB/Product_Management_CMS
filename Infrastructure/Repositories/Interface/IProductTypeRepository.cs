using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Interface
{
    public interface IProductTypeRepository
    {
        Task<IEnumerable<ProductType>> GetAllAsync(CancellationToken cancellationToken);
        Task<ProductType?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<ProductType> CreateAsync(ProductType productType, CancellationToken cancellationToken);
        Task UpdateAsync(ProductType productType, CancellationToken cancellationToken);
        Task DeleteAsync(int id, CancellationToken cancellationToken);
        Task<Domain.Common.PagedResult<ProductType>> GetPagedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
    }
}
