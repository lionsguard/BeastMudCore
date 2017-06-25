using Beast.Objects.Items;
using Beast.Objects.Mobiles;
using Beast.Objects.Places;
using Beast.Security;
using Microsoft.EntityFrameworkCore;

namespace Beast.Sqlite
{
    public class SqliteContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Mobile> Mobiles { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<Exit> Exits { get; set; }

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


            modelBuilder.Entity<Mobile>()
                .ToTable("Mobiles")
                .HasKey(t => t.Id);
            modelBuilder.Entity<Mobile>()
                .HasIndex(t => t.Name)
                .ForSqliteHasName("IX_Mobile_Name");


            modelBuilder.Entity<Item>()
                .ToTable("Items")
                .HasKey(t => t.Id);
            modelBuilder.Entity<Item>()
                .HasIndex(t => t.Name)
                .ForSqliteHasName("IX_Item_Name");


            modelBuilder.Entity<Place>()
                .ToTable("Places")
                .HasKey(t => t.Id);
            modelBuilder.Entity<Place>()
                .HasIndex(t => t.Name)
                .ForSqliteHasName("IX_Place_Name");


            modelBuilder.Entity<Exit>()
                .ToTable("Exits")
                .HasKey(t => new { t.PlaceId, t.DestinationId });
            modelBuilder.Entity<Exit>()
                .HasOne(t => t.Place)
                .WithMany()
                .HasForeignKey(t => t.PlaceId);
            modelBuilder.Entity<Exit>()
                .HasOne(t => t.Destination)
                .WithMany()
                .HasForeignKey(t => t.DestinationId);


            base.OnModelCreating(modelBuilder);
        }
    }
}
