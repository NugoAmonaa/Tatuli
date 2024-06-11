using Final.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Final.database

{

    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Post> Posts { get; set; }



        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
         
            optionsBuilder.UseSqlServer(ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            modelbuilder.Entity<User>().HasMany(x => x.Posts).WithOne(x => x.Creator).HasForeignKey(x => x.Id).OnDelete(DeleteBehavior.Restrict);

            modelbuilder.Entity<User>().HasMany(x => x.Comments).WithOne(x => x.User).HasForeignKey(x => x.Id).OnDelete(DeleteBehavior.Restrict);

            modelbuilder.Entity<Post>().HasMany(x => x.Comments).WithOne(x=>x.Post).HasForeignKey(x => x.PostID).OnDelete(DeleteBehavior.Cascade);
            modelbuilder.Entity<Comment>().HasKey(x => x.Id);
            modelbuilder.Entity<Comment>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelbuilder.Entity<Post>().HasKey(x => x.Id);
            modelbuilder.Entity<Post>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelbuilder.Entity<User>().HasKey(x => x.Id);
            modelbuilder.Entity<User>().Property(x => x.Id).ValueGeneratedOnAdd();



        }
        public static string ConnectionString { get; } = "Server=DESKTOP-0INB2UD\\SQLEXPRESS;Database=ForumDatabase2;Trusted_Connection=True;TrustServerCertificate=True";


    }
}
