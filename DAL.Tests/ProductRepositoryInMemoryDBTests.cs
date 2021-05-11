using System;
using Xunit;
using Catalog.DAL.Repositories.Impl;
using Catalog.DAL.EF;
using Microsoft.EntityFrameworkCore;
using Catalog.DAL.Entities;
using Catalog.DAL.Repositories.Interfaces;
using System.Linq;

namespace DAL.Tests
{
    public class TaskRepositoryInMemoryDBTests
    {
        public CatalogContext Context => SqlLiteInMemoryContext();

        private CatalogContext SqlLiteInMemoryContext()
        {

            var options = new DbContextOptionsBuilder<CatalogContext>()
                .UseSqlite("DataSource=:memory:")
                .Options;

            var context = new CatalogContext(options);
            context.Database.OpenConnection();
            context.Database.EnsureCreated();
            return context;
        }

        [Fact]
        public void Create_InputTaskWithId0_SetTaskId1()
        {
            // Arrange
            int expectedListCount = 1;
            var context = SqlLiteInMemoryContext();
            EFUnitOfWork uow = new EFUnitOfWork(context);
            Catalog.DAL.Repositories.Interfaces.TaskRepository repository = uow.Tasks;

            Task Task = new Task()
            {
                CatalogID = 17,
                Name = "test",
                Description = "testD",
                Catalog = new Catalog.DAL.Entities.Catalog() { CatalogID = 17}
            };

            //Act
            repository.Create(Task);
            uow.Save();
            var factListCount = context.Tasks.Count();

            // Assert
            Assert.Equal(expectedListCount, factListCount);
        }

        [Fact]
        public void Delete_InputExistTaskId_Removed()
        {
            // Arrange
            int expectedListCount = 0;
            var context = SqlLiteInMemoryContext();
            EFUnitOfWork uow = new EFUnitOfWork(context);
            Catalog.DAL.Repositories.Interfaces.TaskRepository repository = uow.Tasks;
            Task Task = new Task()
            {
                //TaskId = 1,
                CatalogID = 17,
                Name = "test",
                Description = "testD",
                Catalog = new Catalog.DAL.Entities.Catalog() { CatalogID = 17 }
            };
            context.Tasks.Add(Task);
            context.SaveChanges();

            //Act
            repository.Delete(Task.TaskID);
            uow.Save();
            var factTaskCount = context.Tasks.Count();

            // Assert
            Assert.Equal(expectedListCount, factTaskCount);
        }

        [Fact]
        public void Get_InputExistTaskId_ReturnTask()
        {
            // Arrange
            var context = SqlLiteInMemoryContext();
            EFUnitOfWork uow = new EFUnitOfWork(context);
            Catalog.DAL.Repositories.Interfaces.TaskRepository repository = uow.Tasks;
            Task expectedTask = new Task()
            {
                //TaskId = 1,
                CatalogID = 17,
                Name = "test",
                Description = "testD",
                Catalog = new Catalog.DAL.Entities.Catalog() { CatalogID = 17 }
            };
            context.Tasks.Add(expectedTask);
            context.SaveChanges();

            //Act
            var factTask = repository.Get(expectedTask.TaskID);

            // Assert
            Assert.Equal(expectedTask, factTask);
        }
    }
}
