using Domain.Entities;
using Domain.Common;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Infrastructure.Repositories.Interface
{
    public interface IActivityRepository
    {
        Task<Activity> CreateAsync(Activity activity, CancellationToken cancellationToken);
        Task<Activity?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<Activity>> GetAllAsync(CancellationToken cancellationToken);
        Task<PagedResult<Activity>> GetPagedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
        Task DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
