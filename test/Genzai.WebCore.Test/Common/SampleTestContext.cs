using Genzai.WebCore.Interfaces;
using Genzai.WebCore.Test.Data;
using Genzai.WebCore.Test.Mock.Application.AutoMapper;
using Genzai.WebCore.Test.Mock.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Security.Claims;

namespace Genzai.WebCore.Test.Common
{

    public class SampleTestContext : IDisposable
    {
        private bool _disposed;
        public SampleContext SampleContext
        {
            get;
        }
        public IMediator Mediator
        {
            get;
        }
        public IMapper Mapper
        {
            get;
        }
        public ILoggerFactory LoggerFactory
        {
            get;
        }


        public SampleTestContext()
        {
            var mockMapper = new MapperConfiguration(cfg => cfg.AddProfile(new SampleMapping()));
            this.Mapper = mockMapper.CreateMapper();

            Mediator = new Mock<IMediator>().Object;

            string databaseName = $"Database{ Guid.NewGuid()}";
            DbContextOptions<SampleContext> _contextOptions = new DbContextOptionsBuilder<SampleContext>()
                .UseInMemoryDatabase(databaseName)
                .Options;

            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new("emails", "mock@email.fake"),
                new(ClaimTypes.Name, "anynameazuread@prosegur.com")
            }, "mock"));

            this.SampleContext = new SampleContext(_contextOptions, this.Mediator, claimsPrincipal);


            //Se rellenan los datos
            foreach (object o in SampleTestingDataset.ListDefaultDataset())
            {
                this.SampleContext.Add(o);
            }

            int i = this.SampleContext.SaveChanges();
            Console.WriteLine($"Se han insertado {i} datos en la bbdd {databaseName}");



            Mock<ILogger> mockLog = new Mock<ILogger>();
            Mock<ILoggerFactory> mockLogFactory = new Mock<ILoggerFactory>();
            mockLogFactory.Setup(f => f.CreateLogger(It.IsAny<string>())).Returns(mockLog.Object);
            LoggerFactory = mockLogFactory.Object;
            Mock<ICacheService> mockCache = new Mock<ICacheService>();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this._disposed)
            {
                return;
            }

            this._disposed = true;
            if (this.SampleContext != null)
            {
                this.SampleContext.Database.EnsureDeleted();
                this.SampleContext.Dispose();
            }

        }
    }
}
