using Beast.Characters;
using Beast.Security;
using Microsoft.EntityFrameworkCore;

namespace Beast.Sqlite
{
    public class SqliteContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Character> Characters { get; set; }
        //public DbSet<Item> Items { get; set; }
        //public DbSet<Place> Places { get; set; }

        public SqliteContext(DbContextOptions<SqliteContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .ToTable("Users")
                .HasKey(t => t.Id);
            modelBuilder.Entity<User>()
                .HasIndex(t => t.Name)
                .IsUnique()
                .ForSqliteHasName("IX_User_Name");

            modelBuilder.Entity<Character>()
                .ToTable("Characters")
                .HasKey(t => t.Id);
            modelBuilder.Entity<Character>()
                .HasIndex(t => t.Name)
                .IsUnique()
                .ForSqliteHasName("IX_Character_Name");

            base.OnModelCreating(modelBuilder);
        }
    }
}
