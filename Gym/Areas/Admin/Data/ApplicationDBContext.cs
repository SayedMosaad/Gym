using Gym.Areas.Admin.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gym.Areas.Admin.DTO;
using Microsoft.AspNetCore.Identity;

namespace Gym.Areas.Admin.Data
{
    public class ApplicationDBContext:IdentityDbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options): base(options)
        {
        }

        

        public DbSet<Biography> Profiles { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Certificate> Certificates { get; set; }
        public DbSet<Request> Requests { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Slider>().Property(s => s.Title).HasMaxLength(250);
            builder.Entity<Slider>().Property(s => s.Subtitle).HasMaxLength(250);
            builder.Entity<Slider>().Property(s => s.Description).HasMaxLength(500);

            builder.Entity<Doctor>().Property(x => x.Name).HasMaxLength(250);
            builder.Entity<Doctor>().Property(x => x.Image).HasMaxLength(250);
            builder.Entity<Doctor>().Property(x => x.Bio).HasMaxLength(250);

            builder.Entity<Blog>().Property(x => x.Title).HasMaxLength(250);
            builder.Entity<Blog>().Property(x => x.Image).HasMaxLength(250);
            builder.Entity<Blog>().Property(x => x.Description).HasMaxLength(250);
            builder.Entity<Blog>().Property(x => x.Body).HasMaxLength(1000);

            builder.Entity<Group>().Property(x => x.Name).HasMaxLength(250);



            builder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "2c5e174e-3b0e-446f-86af-483d56fd7210", Name = "Admin", NormalizedName = "ADMIN".ToUpper() });


            var hasher = new PasswordHasher<ApplicationUser>();


            builder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {
                    Id = "8e445865-a24d-4543-a6c6-9443d048cdb9", // primary key
                    UserName = "Admin",
                    NormalizedUserName = "ADMIN",
                    Email = "admin@gmail.com",
                    PasswordHash = hasher.HashPassword(null, "Pass@123")
                }
            );


            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "2c5e174e-3b0e-446f-86af-483d56fd7210",
                    UserId = "8e445865-a24d-4543-a6c6-9443d048cdb9"
                }
            );

        }
    }
}
