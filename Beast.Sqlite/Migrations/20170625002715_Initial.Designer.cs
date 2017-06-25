using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Beast.Sqlite;

namespace Beast.Sqlite.Migrations
{
    [DbContext(typeof(SqliteContext))]
    [Migration("20170625002715_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2");

            modelBuilder.Entity("Beast.Characters.Character", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<Guid?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasAnnotation("Sqlite:Name", "IX_Character_Name");

                    b.ToTable("Characters");
                });

            modelBuilder.Entity("Beast.Security.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Email");

                    b.Property<bool>("IsDeactivated");

                    b.Property<bool>("IsEmailVerified");

                    b.Property<bool>("IsLockedOut");

                    b.Property<DateTime>("LastLoginDate");

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.Property<string>("PasswordSalt");

                    b.Property<string>("RealName");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasAnnotation("Sqlite:Name", "IX_User_Name");

                    b.ToTable("Users");
                });
        }
    }
}
