# Entity Framework Core migrations — Infrastructure

This document explains how to create, run and revert EF Core migrations for the Infrastructure project.

Prerequisites
- .NET SDK installed (matching target: net10.0)
- dotnet-ef CLI tool installed globally or available as a local tool:
  - Install global: `dotnet tool install --global dotnet-ef`
  - Or restore local tools if a tool manifest exists: `dotnet tool restore`
- The Infrastructure project must reference the EF Core provider (for SQL Server this project already uses `Microsoft.EntityFrameworkCore.SqlServer`).

General notes
- Project layout in this solution:
  - Data (DbContext + models): `Infrastructure` project
  - Startup / host: `CMS/CMS` (startup project used by EF tooling)
- When running dotnet-ef commands, specify the target project (`-p`) that contains the migrations/DbContext, and the startup project (`-s`) that provides configuration (IHost). Example flags are used below.
- If you have multiple DbContext types, pass `--context ApplicationDbContext` (or the concrete context type name) to target the correct one.

1) Create a new migration

Example (creates a migration named `InitialCreate`):

dotnet ef migrations add InitialCreate -p Infrastructure -s CMS/CMS --context ApplicationDbContext -o Migrations

- `-p Infrastructure`: the project that contains the DbContext and where migrations will be placed.
- `-s CMS/CMS`: the startup project (used to read configuration & DI).
- `--context ApplicationDbContext`: optional if you have one context or need to be explicit.
- `-o Migrations`: optional output directory for migrations inside the Infrastructure project.

2) Apply migrations to the database

Apply all pending migrations:

dotnet ef database update -p Infrastructure -s CMS/CMS --context ApplicationDbContext

Apply up to a specific migration (use migration name listed by `migrations list`):

dotnet ef database update MigrationName -p Infrastructure -s CMS/CMS --context ApplicationDbContext

3) Remove the last migration (not yet applied)

If you created a migration but have not applied it to the database, remove it from the project with:

dotnet ef migrations remove -p Infrastructure -s CMS/CMS --context ApplicationDbContext

This deletes the last migration files. If it has been applied to the database, use the revert steps below first.

4) Revert an applied migration (downgrade)

To rollback the database to a previous migration state use the migration name or `0` to revert all migrations (drops tables created by migrations):

dotnet ef database update PreviousMigrationName -p Infrastructure -s CMS/CMS --context ApplicationDbContext

Examples:
- Revert the last applied migration (if the previous migration is `BeforeLast`):
  dotnet ef database update BeforeLast -p Infrastructure -s CMS/CMS --context ApplicationDbContext
- Revert all migrations (be careful — this will remove schema created by migrations):
  dotnet ef database update 0 -p Infrastructure -s CMS/CMS --context ApplicationDbContext

Important: if you are reverting in production or on a database that contains important data, always take a backup before downgrading. The EF downgrade may drop columns/tables and data will be lost.

5) List migrations

dotnet ef migrations list -p Infrastructure -s CMS/CMS --context ApplicationDbContext

6) Generate SQL script

Create a SQL script for a migration or for all pending migrations (useful for DBAs):

dotnet ef migrations script -p Infrastructure -s CMS/CMS --context ApplicationDbContext -o migration.sql

Or to script from a specific migration to another:

dotnet ef migrations script FromMigration ToMigration -p Infrastructure -s CMS/CMS --context ApplicationDbContext -o migration_range.sql

7) Visual Studio Package Manager Console (alternative)

If you prefer PMC inside Visual Studio, use these commands (set Default project to `Infrastructure` and StartUp Project to `CMS`):

- Add migration:
  Add-Migration InitialCreate -Project Infrastructure -StartupProject CMS -Context ApplicationDbContext
- Update database:
  Update-Database -Project Infrastructure -StartupProject CMS -Context ApplicationDbContext
- Remove migration (last, not applied):
  Remove-Migration -Project Infrastructure -StartupProject CMS -Context ApplicationDbContext

8) Apply migrations at runtime (optional)

You can apply migrations automatically at app startup (not recommended for all production scenarios). In your Program.cs / startup code, use:

using (var scope = app.Services.CreateScope())
{
	var db = scope.ServiceProvider.GetRequiredService<Infrastructure.Persistence.ApplicationDbContext>();
	db.Database.Migrate();
}

Note: the project currently uses a DatabaseSeeder that calls `EnsureCreatedAsync()` for initial seeding. If you adopt migrations, you should remove `EnsureCreatedAsync()` and call `Database.Migrate()` instead so EF migrations are applied.

9) Troubleshooting
- If EF tools cannot find your DbContext, verify the `-p` and `-s` arguments and the `--context` name. The startup project must build and be able to provide configuration.
- If migrations are generated with unexpected namespace or folder, pass `-o` and `--context` to control placement.
- If you get permission/connection errors, check the connection string in `CMS/appsettings.json` or provide environment variables before running CLI.

Safety checklist before applying to production
- Backup database (full backup) before applying schema changes.
- Test migrations in a staging environment with a copy of production data.
- Review generated SQL (use `dotnet ef migrations script`) and verify destructive operations.

If you want, I can:
- Add a `Migrations` folder to the Infrastructure project and create an initial migration locally (I can scaffold the migration files, but applying them to your DB requires the `dotnet ef` CLI to run on your machine or CI).
- Replace `EnsureCreatedAsync()` with `Database.Migrate()` and adjust the seeder to run after migrations.
