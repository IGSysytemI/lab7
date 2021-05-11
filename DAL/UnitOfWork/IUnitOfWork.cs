using Catalog.DAL.Entities;
using Catalog.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        CatalogRepository Catalogs { get; }
        TaskRepository Tasks { get; }
        void Save();
        //CatalogRepository OSBBs { get; }
        //ProductRepository Streets { get; }
        //void Save();
    }
}
