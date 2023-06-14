﻿using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
    public class NZWalksDbContext : DbContext
    {
        public NZWalksDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }
        public DbSet<Difficulty> difficulty { get; set; }
        public DbSet<Region> regions { get; set; }
        public DbSet<Walk> walks { get; set; }
    }
}
