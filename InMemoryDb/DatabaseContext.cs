using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace InMemoryDb
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Table>(table =>
            {
                table.HasKey(field => field.Field);
            });

            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<Table> Tables { get; set; }
    }
}
