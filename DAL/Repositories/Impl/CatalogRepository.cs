using System;
using System.Collections.Generic;
using System.Text;
using Catalog.DAL.Entities;
using Catalog.DAL.Repositories.Interfaces;
using Catalog.DAL.EF;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Catalog.DAL.Repositories.Impl
{
    public class CatalogRepository
        : BaseRepository<Entities.Catalog>, Interfaces.CatalogRepository
    {

        internal CatalogRepository(CatalogContext context) 
            : base(context)
        {
        }
    }
}
