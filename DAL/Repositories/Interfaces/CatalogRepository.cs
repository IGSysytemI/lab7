using System;
using System.Collections.Generic;
using System.Text;
using Catalog.DAL.Entities;

namespace Catalog.DAL.Repositories.Interfaces
{
    public interface CatalogRepository
        : IRepository<Entities.Catalog>
    {
    }
}
