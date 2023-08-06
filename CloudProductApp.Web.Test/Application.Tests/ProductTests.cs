using CloudProductApp.Web.Application.Products.Commands;
using CloudProductApp.Web.Application.Products.Queries;
using CloudProductApp.Web.Domain.Entities;
using CloudProductApp.Web.Infrastructure.API.Interfaces;
using Microsoft.Extensions.Options;
using Moq;

namespace CloudProductApp.Web.Test.Application.Tests
{
    namespace Application.Tests
    {
        public class ProductTests
        {
            [Fact]
            public async Task GetAllProductsQueryHandler_ShouldReturnProductDtos()
            {
                // Arrange
                var products = new List<Product>
                {
                    new Product { Name = "Product 1", BasePrice = 100 },
                    new Product { Name = "Product 2", BasePrice = 150 }
                };

                var apikey = "test";
                var appSettingsMock = new Mock<IOptions<Startup.AppSettings>>();
                appSettingsMock.Setup(s => s.Value)
                               .Returns(new Startup.AppSettings { ApiKey = apikey });
                var cloudApiClientMock = new Mock<ICloudApiClient>();
                cloudApiClientMock.Setup(client => client.GetAllProductsAsync(apikey))
                                 .ReturnsAsync(products);

                var handler = new GetProductsQueryHandler(cloudApiClientMock.Object, appSettingsMock.Object);

                // Act
                var query = new GetProductsQuery();
                var result = await handler.Handle(query, CancellationToken.None);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(products.Count, result.Count);
            }

            [Fact]
            public async Task GetAllProductsQueryHandler_ShouldReturnEmptyProductList()
            {
                // Arrange
                var emptyProductsList = new List<Product>();

                var apikey = "API-2ZKC1OWQ3TZQG3C";

                var cloudApiClientMock = new Mock<ICloudApiClient>();
                cloudApiClientMock.Setup(client => client.GetAllProductsAsync(apikey))
                                 .ReturnsAsync(emptyProductsList);

                var appSettingsMock = new Mock<IOptions<Startup.AppSettings>>();
                appSettingsMock.Setup(s => s.Value)
                               .Returns(new Startup.AppSettings { ApiKey = apikey });

                var handler = new GetProductsQueryHandler(cloudApiClientMock.Object, appSettingsMock.Object);

                // Act
                var query = new GetProductsQuery();
                var result = await handler.Handle(query, CancellationToken.None);

                // Assert
                Assert.NotNull(result);
                Assert.Empty(result);
            }



        }
    }

}
