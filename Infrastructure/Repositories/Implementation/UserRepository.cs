using Domain.Entities;
using Infrastructure.Persistence;
using Infrastructure.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;

        public UserRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<User> CreateAsync(User user, System.Threading.CancellationToken cancellationToken)
        {
            _db.Users.Add(user);
            await _db.SaveChangesAsync(cancellationToken);
            return user;
        }

        public async Task DeleteAsync(int id, System.Threading.CancellationToken cancellationToken)
        {
            var entity = await _db.Users.FirstOrDefaultAsync(u => u.iId == id, cancellationToken);
            if (entity != null)
            {
                _db.Users.Remove(entity);
                await _db.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<IEnumerable<User>> GetAllAsync(System.Threading.CancellationToken cancellationToken)
        {
            return await _db.Users.ToListAsync(cancellationToken);
        }

        public async Task<Domain.Common.PagedResult<User>> GetPagedAsync(int pageNumber, int pageSize, System.Threading.CancellationToken cancellationToken)
        {
            var query = _db.Users.AsQueryable();
            var total = await query.CountAsync(cancellationToken);
            var items = await query.OrderBy(u => u.iId).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
            return new Domain.Common.PagedResult<User>
            {
                Items = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = total
            };
        }

        public async Task<User?> GetByIdAsync(int id, System.Threading.CancellationToken cancellationToken)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.iId == id, cancellationToken);
        }

        public async Task UpdateAsync(User user, System.Threading.CancellationToken cancellationToken)
        {
            _db.Users.Update(user);
            await _db.SaveChangesAsync(cancellationToken);
        }
    }
}
