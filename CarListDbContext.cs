using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarListApp.Api
{
    public class CarListDbContext : IdentityDbContext
    {
        public CarListDbContext(DbContextOptions<CarListDbContext> options) : base(options)
        {

        }
        public DbSet<Car> Cars { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Car>().HasData(
                new Car
                {
                    Id= 1,
                    Make="HONDA",
                    Model="Civic",
                    Vin="1234"
                },
                new Car
                {
                    Id= 2,
                    Make="TATA",
                    Model="Neon",
                    Vin="2345"
                },
                new Car
                {
                    Id = 3,
                    Make = "BMW",
                    Model = "A6",
                    Vin = "5678"
                },
                new Car
                {
                    Id = 4,
                    Make = "MAHINDRA",
                    Model = "THAR",
                    Vin = "0987"
                },
                new Car
                {
                    Id = 5,
                    Make = "TOYOTA",
                    Model = "Fortuner",
                    Vin = "6542"
                });
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id= "3c34c713-d0d7-484d-9ec7-ed1471536072",
                    Name="Administrator",
                    NormalizedName="ADMINISTRATOR"
                },
                new IdentityRole
                {
                    Id = "138a30c7-362e-4f90-b240-f7d5bd8ba424",
                    Name = "User",
                    NormalizedName = "USER"
                }
              );
            var hasher = new PasswordHasher<IdentityUser>();
            modelBuilder.Entity<IdentityUser>().HasData(
                new IdentityUser
                {
                    Id= "36721706-1151-48f9-8983-95ab58ccb376",
                    Email="admin@localhost.com",
                    NormalizedEmail="ADMIN@LOCALHOST.COM",
                    NormalizedUserName="ADMIN@LOCALHOST.COM",
                    UserName="admin@localhost.com",
                    PasswordHash=hasher.HashPassword(null,"P@ssword1"),
                    EmailConfirmed=true
                },
                 new IdentityUser
                 {
                     Id= "78f7e47e-e6e4-48bf-adba-a60efc5fdb29",
                     Email = "user@localhost.com",
                     NormalizedEmail = "USER@LOCALHOST.COM",
                     NormalizedUserName = "USER@LOCALHOST.COM",
                     UserName = "user@localhost.com",
                     PasswordHash = hasher.HashPassword(null, "P@ssword1"),
                     EmailConfirmed = true
                 }
                );
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId= "3c34c713-d0d7-484d-9ec7-ed1471536072",
                    UserId= "36721706-1151-48f9-8983-95ab58ccb376"
                },
                new IdentityUserRole<string>
                {
                    RoleId= "138a30c7-362e-4f90-b240-f7d5bd8ba424",
                    UserId= "78f7e47e-e6e4-48bf-adba-a60efc5fdb29"
                }
                );
        }
    }
}
