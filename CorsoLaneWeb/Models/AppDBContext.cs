// Add these using statements at the top
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CorsoLaneWeb.Models
{

    public class AppDBContext : IdentityDbContext<user>
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }


        public DbSet<Order> Orders { get; set; }
        public DbSet<products_entity> products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<CategorySubCategory> CategorySubCategories { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Composite key for join table
            modelBuilder.Entity<CategorySubCategory>()
                .HasKey(cs => new { cs.CategoryId, cs.SubCategoryId });

            // Configure many-to-many relationships
            modelBuilder.Entity<CategorySubCategory>()
                .HasOne(cs => cs.Category)
                .WithMany(c => c.CategorySubCategories)
                .HasForeignKey(cs => cs.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CategorySubCategory>()
                .HasOne(cs => cs.SubCategory)
                .WithMany(sc => sc.CategorySubCategories)
                .HasForeignKey(cs => cs.SubCategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }



    }
}