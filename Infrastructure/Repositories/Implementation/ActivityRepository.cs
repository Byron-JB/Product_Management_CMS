using Domain.Common;
using Domain.Entities;
using Infrastructure.Persistence;
using Infrastructure.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Implementation
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly ApplicationDbContext _db;

        public ActivityRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Activity> CreateAsync(Activity activity, CancellationToken cancellationToken)
        {
            _db.Activities.Add(activity);
            await _db.SaveChangesAsync(cancellationToken);
            return activity;
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var entity = await _db.Activities.FirstOrDefaultAsync(a => a.iId == id, cancellationToken);
            if (entity != null)
            {
                _db.Activities.Remove(entity);
                await _db.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<IEnumerable<Activity>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _db.Activities.ToListAsync(cancellationToken);
        }

        public async Task<Activity?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _db.Activities.FirstOrDefaultAsync(a => a.iId == id, cancellationToken);
        }

        public async Task<PagedResult<Activity>> GetPagedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var query = _db.Activities.AsQueryable();
            var total = await query.CountAsync(cancellationToken);
            var items = await query.OrderByDescending(a => a.dtTimestamp).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
            return new PagedResult<Activity>
            {
                Items = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = total
            };
        }
    }
}
