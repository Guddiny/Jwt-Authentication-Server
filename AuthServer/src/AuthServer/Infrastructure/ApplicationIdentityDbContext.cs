using System;
using System.Threading.Tasks;
using AuthServer.Core.Domains;
using Common.Authentication;
using Common.EF;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace AuthServer.Infrastructure
{
    public class ApplicationIdentityDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>, IDataContext
    {
        private IDbContextTransaction _transaction;

        public ApplicationIdentityDbContext(DbContextOptions<ApplicationIdentityDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RefreshToken<Guid>>()
                .ToTable("RefreshTokens");

            modelBuilder.Entity<RefreshToken<Guid>>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<RefreshToken<Guid>>().Property(x => x.UserId);

            modelBuilder.Entity<RefreshToken<Guid>>()
                .Property(s => s.Id)
                .HasDefaultValueSql("NEWID()")
                .IsRequired();

            modelBuilder.Entity<RefreshToken<Guid>>()
                .Property(s => s.Token)
                .IsRequired();

            modelBuilder.Entity<RefreshToken<Guid>>()
                .Property(s => s.RevokedAt)
                .IsRequired(false);

            modelBuilder.Entity<RefreshToken<Guid>>()
                .Property(s => s.CreatedAt)
                .IsRequired()
                .HasDefaultValue(DateTime.Now);

            modelBuilder.Entity<RefreshToken<Guid>>()
                .Property(s => s.Expires)
                .IsRequired();
        }

        public DbSet<RefreshToken<Guid>> RefreshTokens { get; set; }

        public void BeginTransaction()
        {
            _transaction = Database.BeginTransaction();
        }

        public int Commit()
        {
            try
            {
                var saveChanges = SaveChanges();
                _transaction.Commit();
                return saveChanges;
            }
            finally
            {
                _transaction.Dispose();
            }
        }

        public void Rollback()
        {
            _transaction.Rollback();
            _transaction.Dispose();
        }

        public Task<int> CommitAsync()
        {
            try
            {
                var saveChangesAsync = SaveChangesAsync();
                _transaction.Commit();
                return saveChangesAsync;
            }
            finally
            {
                _transaction.Dispose();
            }
        }
    }
}