using Application.Features.Products.Commands;
using Application.Features.Products.Handlers;
using Application.Features.Products.Queries;
using Domain.Entities;
using Infrastructure.Repositories.Interface;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.Tests
{
    public class ProductHandlersTests
    {
        [Fact]
        public async Task GetAllProductsHandler_ReturnsAllProducts()
        {
            var mockRepo = new Mock<IProductRepository>();
            var products = new List<Product>
            {
                new Product { iId = 1, strName = "A" },
                new Product { iId = 2, strName = "B" }
            };

            mockRepo.Setup(r => r.GetAllAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(products);

            var handler = new GetAllProductsHandler(mockRepo.Object);

            var result = (await handler.Handle(new GetAllProductsQuery(), CancellationToken.None)).ToList();

            Assert.Equal(2, result.Count);
            Assert.Contains(result, p => p.strName == "A");
            mockRepo.Verify(r => r.GetAllAsync(It.IsAny<System.Threading.CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task GetProductByIdHandler_ReturnsProduct_WhenFound()
        {
            var mockRepo = new Mock<IProductRepository>();
            var product = new Product { iId = 5, strName = "Found" };
            mockRepo.Setup(r => r.GetByIdAsync(5, It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(product);

            var handler = new GetProductByIdHandler(mockRepo.Object);

            var result = await handler.Handle(new GetProductByIdQuery(5), CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(5, result!.iId);
            mockRepo.Verify(r => r.GetByIdAsync(5, It.IsAny<System.Threading.CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task CreateProductHandler_CallsRepositoryCreate()
        {
            var mockRepo = new Mock<IProductRepository>();
            var product = new Product { strName = "New" };
            mockRepo.Setup(r => r.CreateAsync(It.IsAny<Product>(), It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync((Product p, System.Threading.CancellationToken ct) =>
            {
                p.iId = 99;
                return p;
            });

            var handler = new CreateProductHandler(mockRepo.Object);

            var created = await handler.Handle(new CreateProductCommand(product), CancellationToken.None);

            Assert.Equal(99, created.iId);
            mockRepo.Verify(r => r.CreateAsync(It.Is<Product>(x => x.strName == "New"), It.IsAny<System.Threading.CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task UpdateProductHandler_CallsRepositoryUpdate()
        {
            var mockRepo = new Mock<IProductRepository>();
            var product = new Product { iId = 3, strName = "Up" };

            mockRepo.Setup(r => r.UpdateAsync(product, It.IsAny<System.Threading.CancellationToken>())).Returns(Task.CompletedTask).Verifiable();

            var handler = new UpdateProductHandler(mockRepo.Object);

            await handler.Handle(new UpdateProductCommand(product), CancellationToken.None);

            mockRepo.Verify(r => r.UpdateAsync(product, It.IsAny<System.Threading.CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task DeleteProductHandler_CallsRepositoryDelete()
        {
            var mockRepo = new Mock<IProductRepository>();
            mockRepo.Setup(r => r.DeleteAsync(7, It.IsAny<System.Threading.CancellationToken>())).Returns(Task.CompletedTask).Verifiable();

            var handler = new DeleteProductHandler(mockRepo.Object);

            await handler.Handle(new DeleteProductCommand(7), CancellationToken.None);

            mockRepo.Verify(r => r.DeleteAsync(7, It.IsAny<System.Threading.CancellationToken>()), Times.Once);
        }
    }
}
