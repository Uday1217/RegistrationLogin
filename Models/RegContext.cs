using Microsoft.EntityFrameworkCore;

namespace RegistrationLogin.Mvc.Models
{
    public class RegContext : DbContext
    {
        public RegContext(DbContextOptions<RegContext>options) : base(options)
        {

        }
        public DbSet<Register> Registers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductSubCategory> ProductSubCategories { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
               .Property(p => p.Image)
               .HasColumnType("Varbinary(Max)");

            /*modelBuilder.Entity<ProductSubCategory>()
                .HasOne(s => s.Category)
                .WithMany()
                .HasForeignKey(s => s.Category)
                .OnDelete(DeleteBehavior.Restrict);*/

        }
    }
}
