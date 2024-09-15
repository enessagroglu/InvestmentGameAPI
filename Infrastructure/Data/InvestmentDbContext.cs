using Microsoft.EntityFrameworkCore;
using InvestmentGameAPI.Models;

namespace InvestmentGameAPI.Infrastructure.Data
{
    public class InvestmentDbContext : DbContext
    {
        public InvestmentDbContext(DbContextOptions<InvestmentDbContext> options) : base(options) { }

        // Veritabanı tabloları
        public DbSet<User> Users { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<UserCompanyOwnership> UserCompanyOwnerships { get; set; }
        public DbSet<InGameItem> InGameItems { get; set; }
        public DbSet<UserItemOwnership> UserItemOwnerships { get; set; }

        // Model yapılandırma
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User - Company (Many-to-Many) ilişkisinin tanımlanması
            modelBuilder.Entity<UserCompanyOwnership>()
                .HasKey(uc => new { uc.UserId, uc.CompanyId });

            modelBuilder.Entity<UserCompanyOwnership>()
                .HasOne(uc => uc.User)
                .WithMany(u => u.UserCompanyOwnerships)
                .HasForeignKey(uc => uc.UserId);

            modelBuilder.Entity<UserCompanyOwnership>()
                .HasOne(uc => uc.Company)
                .WithMany(c => c.UserCompanyOwnerships)
                .HasForeignKey(uc => uc.CompanyId);

            // User - InGameItem (Many-to-Many) ilişkisinin tanımlanması
            modelBuilder.Entity<UserItemOwnership>()
                .HasKey(ui => new { ui.UserId, ui.ItemId });

            modelBuilder.Entity<UserItemOwnership>()
                .HasOne(ui => ui.User)
                .WithMany(u => u.UserItemOwnerships)
                .HasForeignKey(ui => ui.UserId);

            modelBuilder.Entity<UserItemOwnership>()
                .HasOne(ui => ui.InGameItem)
                .WithMany(i => i.UserItemOwnerships)
                .HasForeignKey(ui => ui.ItemId);

            // İndeks ekleme işlemleri (Fluent API ile)
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();  // Username alanını benzersiz yapıyoruz

            modelBuilder.Entity<UserCompanyOwnership>()
                .HasIndex(uc => uc.UserId);  // UserId için indeks
            modelBuilder.Entity<UserCompanyOwnership>()
                .HasIndex(uc => uc.CompanyId);  // CompanyId için indeks

            modelBuilder.Entity<UserItemOwnership>()
                .HasIndex(ui => ui.UserId);  // UserId için indeks
            modelBuilder.Entity<UserItemOwnership>()
                .HasIndex(ui => ui.ItemId);  // ItemId için indeks

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Company>().HasData(
                new Company { Id = 1, Name = "Company A", CurrentStockPrice = 100.5m, TotalShares = 1000, MarketCap = 100500m },
                new Company { Id = 2, Name = "Company B", CurrentStockPrice = 200.0m, TotalShares = 2000, MarketCap = 400000m },
                new Company { Id = 3, Name = "Company C", CurrentStockPrice = 300.0m, TotalShares = 1500, MarketCap = 450000m }
            );
        }
    }
}
