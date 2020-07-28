using ApiCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiCore.Context
{
    public class AppDBContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public int? UserId { get; set; }

        public AppDBContext(DbContextOptions<AppDBContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        public AppDBContext(IConfiguration configuration)
        {
            Database.GetDbConnection().ConnectionString = configuration.GetConnectionString("DBConnection");
        }

        public override int SaveChanges()
        {
            SetDefaultValues();

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            SetDefaultValues();

            return base.SaveChangesAsync();
        }

        private void SetDefaultValues()
        {
            var currentUserId = GetCurrentUserId();

            System.Collections.Generic.IEnumerable<Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry> entities = ChangeTracker.Entries().Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    if (((BaseEntity)entity.Entity).CreatedAt == DateTime.MinValue)
                        ((BaseEntity)entity.Entity).CreatedAt = DateTime.UtcNow;

                    if (((BaseEntity)entity.Entity).CreatedById < 1 && currentUserId > 0)
                        ((BaseEntity)entity.Entity).CreatedById = currentUserId;
                }
                else
                {
                    if (!((BaseEntity)entity.Entity).UpdatedAt.HasValue)
                        ((BaseEntity)entity.Entity).UpdatedAt = DateTime.UtcNow;

                    if (!((BaseEntity)entity.Entity).UpdatedById.HasValue && currentUserId > 0)
                        ((BaseEntity)entity.Entity).UpdatedById = currentUserId;
                }
            }
        }

        public int GetCurrentUserId()
        {
            return UserId ?? 0;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            CreateModelMapping(modelBuilder);
            modelBuilder.HasDefaultSchema("public");

            base.OnModelCreating(modelBuilder);
        }

        private void CreateModelMapping(ModelBuilder modelBuilder)
        {
            //User
            modelBuilder.Entity<User>().HasIndex(x => x.Email);
            modelBuilder.Entity<User>().HasIndex(x => x.FullName);
            modelBuilder.Entity<User>().HasIndex(x => x.CreatedAt);
            modelBuilder.Entity<User>().HasIndex(x => x.UpdatedAt);
            modelBuilder.Entity<User>().HasIndex(x => x.Deleted);

            modelBuilder.Entity<Profile>()
                .HasMany(x => x.Users)
                .WithOne(x => x.Profile)
                .HasForeignKey(x => x.ProfileId);

            modelBuilder.Entity<Profile>()
                .HasOne(x => x.CreatedBy)
                .WithMany()
                .HasForeignKey(x => x.CreatedById);


            modelBuilder.Entity<Role>()
                .HasMany(x => x.Users)
                .WithOne(x => x.Role)
                .HasForeignKey(x => x.RoleId);

            BuildIndexes(modelBuilder);
        }

        public void BuildIndexes(ModelBuilder modelBuilder)
        {
            //User
            modelBuilder.Entity<User>().HasIndex(x => x.Email);
            modelBuilder.Entity<User>().HasIndex(x => x.FullName);
            modelBuilder.Entity<User>().HasIndex(x => x.CreatedAt);
            modelBuilder.Entity<User>().HasIndex(x => x.UpdatedAt);
            modelBuilder.Entity<User>().HasIndex(x => x.Deleted);
            modelBuilder.Entity<User>().HasIndex(x => new { x.Email }).IsUnique();
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Profile> Profiles { get; set; }

       // public DbSet<WeatherForecast> WeatherForecasts { get; set; }
    }
}
