using System;
using Xunit;
using Catalog.DAL.Repositories.Impl;
using Catalog.DAL.EF;
using Microsoft.EntityFrameworkCore;
using Catalog.DAL.Entities;
using Catalog.DAL.Repositories.Interfaces;
using System.Linq;
using Moq;

namespace DAL.Tests
{
    class TestTaskRepository
        : BaseRepository<Task>
    {
        public TestTaskRepository(DbContext context) 
            : base(context)
        {
        }
    }

    public class BaseRepositoryUnitTests
    {

        [Fact]
        public void Create_InputTaskInstance_CalledAddMethodOfDBSetWithTaskInstance()
        {
            // Arrange
            DbContextOptions opt = new DbContextOptionsBuilder<CatalogContext>()
                .Options;
            var mockContext = new Mock<CatalogContext>(opt);
            var mockDbSet = new Mock<DbSet<Task>>();
            mockContext
                .Setup(context => 
                    context.Set<Task>(
                        ))
                .Returns(mockDbSet.Object);
            //EFUnitOfWork uow = new EFUnitOfWork(mockContext.Object);
            var repository = new TestTaskRepository(mockContext.Object);

            Task expectedTask = new Mock<Task>().Object;

            //Act
            repository.Create(expectedTask);

            // Assert
            mockDbSet.Verify(
                dbSet => dbSet.Add(
                    expectedTask
                    ), Times.Once());
        }

        [Fact]
        public void Delete_InputId_CalledFindAndRemoveMethodsOfDBSetWithCorrectArg()
        {
            // Arrange
            DbContextOptions opt = new DbContextOptionsBuilder<CatalogContext>()
                .Options;
            var mockContext = new Mock<CatalogContext>(opt);
            var mockDbSet = new Mock<DbSet<Task>>();
            mockContext
                .Setup(context =>
                    context.Set<Task>(
                        ))
                .Returns(mockDbSet.Object);
            //EFUnitOfWork uow = new EFUnitOfWork(mockContext.Object);
            //ITaskRepository repository = uow.Tasks;
            var repository = new TestTaskRepository(mockContext.Object);

            Task expectedTask = new Task() { TaskID = 1};
            mockDbSet.Setup(mock => mock.Find(expectedTask.TaskID)).Returns(expectedTask);

            //Act
            repository.Delete(expectedTask.TaskID);

            // Assert
            mockDbSet.Verify(
                dbSet => dbSet.Find(
                    expectedTask.TaskID
                    ), Times.Once());
            mockDbSet.Verify(
                dbSet => dbSet.Remove(
                    expectedTask
                    ), Times.Once());
        }

        [Fact]
        public void Get_InputId_CalledFindMethodOfDBSetWithCorrectId()
        {
            // Arrange
            DbContextOptions opt = new DbContextOptionsBuilder<CatalogContext>()
                .Options;
            var mockContext = new Mock<CatalogContext>(opt);
            var mockDbSet = new Mock<DbSet<Task>>();
            mockContext
                .Setup(context =>
                    context.Set<Task>(
                        ))
                .Returns(mockDbSet.Object);

            Task expectedTask = new Task() { TaskID = 1 };
            mockDbSet.Setup(mock => mock.Find(expectedTask.TaskID))
                    .Returns(expectedTask);
            var repository = new TestTaskRepository(mockContext.Object);

            //Act
            var actualTask = repository.Get(expectedTask.TaskID);

            // Assert
            mockDbSet.Verify(
                dbSet => dbSet.Find(
                    expectedTask.TaskID
                    ), Times.Once());
            Assert.Equal(expectedTask, actualTask);
        }

      
    }
}
