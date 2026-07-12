using Domain.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class DatabaseSeeder
    {
        private readonly Persistence.ApplicationDbContext _db;
        private readonly ILogger<DatabaseSeeder> _logger;

        public DatabaseSeeder(Persistence.ApplicationDbContext db, ILogger<DatabaseSeeder> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task EnsureSeedAsync()
        {
            // Ensure database exists
            await _db.Database.EnsureCreatedAsync();

            // Seed admin if no users
            if (await _db.Users.AnyAsync() == false)
            {
                _logger.LogInformation("Seeding initial admin user");
                var admin = new User
                {
                    strFirstName = "Admin",
                    strLastName = "User",
                    strEmail = "admin@local",
                    strPasswordHash = "changeme",
                    dtCreatedDate = DateTime.UtcNow,
                    dtModifiedDate = DateTime.UtcNow,
                    iAddedBy = null,
                    iModifiedBy = null
                };

                _db.Users.Add(admin);
                await _db.SaveChangesAsync();
            }
        }
    }
}
