using LoginService.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginService.Models
{
    public class DatabaseContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=SURESH\SQLEXPRESS;Initial Catalog=CoreLogicDb;User ID=sa;Password=1234;Integrated Security=True;MultipleActiveResultSets=true;");
        }

        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserDetail> UserDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LoginService.Models.Entity.UserDetail", b =>
            {
                b.Property<int>("UserId")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int")
                    .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<string>("Email")
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("Password")
                    .HasColumnType("nvarchar(max)");

                b.Property<int>("RoleId")
                    .HasColumnType("int");

                b.Property<string>("UserName")
                    .HasColumnType("nvarchar(max)");

                b.HasKey("UserId");

                b.ToTable("UserDetails");
            });

            modelBuilder.Entity("LoginService.Models.Entity.UserRole", b =>
            {
                b.Property<int>("RoleId")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int")
                    .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<string>("Role")
                    .HasColumnType("nvarchar(max)");

                b.HasKey("RoleId");

                b.ToTable("UserRoles");
            });

            modelBuilder.Entity("LoginService.Models.Entity.UserDetail", b =>
            {
                b.HasOne("LoginService.Models.Entity.UserRole", "UserRole")
                    .WithMany("UserDetails")
                    .HasForeignKey("RoleId");

                b.Navigation("UserRole");
            });

            modelBuilder.Entity("LoginService.Models.Entity.UserRole", b =>
            {
                b.Navigation("UserDetails");
            });
        }
    }
}
