﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project56_new.Models;

namespace Project56_new.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<Project56_new.Models.Itms> Itms { get; set; }
        public DbSet<Project56_new.Models.ApplicationUser> UsersDb { get; set; }
        public DbSet<Project56_new.Models.ItmCategories> ItmCategories { get; set; }
        public DbSet<Project56_new.Models.OrdHistory> OrdHistory { get; set; }
        public DbSet<Project56_new.Models.OrdLines> OrdLines { get; set; }
        public DbSet<Project56_new.Models.OrdMains> OrdMains { get; set; }
        public DbSet<Project56_new.Models.OrdStatus> OrdStatus { get; set; }
        public DbSet<Project56_new.Models.WishMains> WishMains { get; set; }
        public DbSet<Project56_new.Models.WishLines> WishLines { get; set; }
        public DbSet<Project56_new.Models.WishlistModel> WishlistModel { get; set; }

    }
}
