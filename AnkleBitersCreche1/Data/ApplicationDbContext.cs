using System;
using System.Collections.Generic;
using System.Text;
using AnkleBitersCreche.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AnkleBitersCreche.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<OurService> OurService { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<ServiceShoppingCart> ServiceShoppingCart { get; set; }
        public DbSet<ServiceHeader> ServiceHeader { get; set; }
        public DbSet<ServiceDetails> ServiceDetails { get; set; }
    }
}
