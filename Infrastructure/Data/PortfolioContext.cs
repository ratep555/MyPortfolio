using System.Reflection;
using Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class PortfolioContext : IdentityDbContext
    {
        public PortfolioContext(DbContextOptions<PortfolioContext> options) : base(options)
        {
        }

        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Modality> Modalities { get; set; }
        public DbSet<Segment> Segments { get; set; }
        public DbSet<TypeOfStock> TypesOfStock { get; set; }
        public DbSet<Surtax> Surtaxes { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<StockTransaction> StockTransactions {get; set;}
        public DbSet<AnnualProfitOrLoss> AnnualProfitsOrLosses {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}