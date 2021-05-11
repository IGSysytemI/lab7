using Catalog.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.DAL.EF
{
    public class CatalogContext
        : DbContext
    {
        public DbSet<Entities.Catalog> Catalogs { get; set; }
        public DbSet<Task> Tasks { get; set; }

        public CatalogContext(DbContextOptions options)
            : base(options)
        {
        }

    }
}
