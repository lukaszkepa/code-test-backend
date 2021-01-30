using FluentAssertions;
using Moq;
using SlothEnterprise.ProductApplication.Models.Applications;
using SlothEnterprise.ProductApplication.Clients;
using SlothEnterprise.ProductApplication.Models.Products;
using System;
using Xunit;

namespace SlothEnterprise.ProductApplication.Tests
{
    public class ProductApplicationTests
    {
        private readonly Mock<IServiceClientFactory> _serviceClientFactoryMock;

        public ProductApplicationTests()
        {
            _serviceClientFactoryMock = new Mock<IServiceClientFactory>();
        }

        [Fact]
        public void SubmitApplicationFor_WhenCalledWithNull_ShouldThrowException()
        {
            // Arrange
            var service = new ProductApplicationService(_serviceClientFactoryMock.Object);

            // Assert
            Assert.Throws<ArgumentNullException>(() => service.SubmitApplicationFor(null));
        }

        [Fact]
        public void SubmitApplicationFor_WhenCalledWithoutProduct_WhouldReturnMinusOne()
        {
            // Arrange
            var application = new SellerApplication
            {
                CompanyData = new SellerCompanyData(),
            };
            var service = new ProductApplicationService(_serviceClientFactoryMock.Object);

            // Act
            var result = service.SubmitApplicationFor(application);

            // Assert
            result.Should().Be(-1);
        }

        [Fact]
        public void SubmitApplicationFor_WhenCalledWithoutCompanyData_WhouldReturnMinusOne()
        {
            // Arrange
            var application = new SellerApplication
            {
                Product = new BusinessLoans(),
            };
            var service = new ProductApplicationService(_serviceClientFactoryMock.Object);

            // Act
            var result = service.SubmitApplicationFor(application);

            // Assert
            result.Should().Be(-1);
        }

        [Fact]
        public void SubmitApplicationFor_ShouldCallServiceClient()
        {
            // Arrange
            var application = new SellerApplication
            {
                Product = new BusinessLoans(),
                CompanyData = new SellerCompanyData(),
            };
            var service = new ProductApplicationService(_serviceClientFactoryMock.Object);

            var serviceClientMock = new Mock<IServiceClient>();
            serviceClientMock.Setup(m => m.SubmitApplication(application)).Returns(1);
            _serviceClientFactoryMock.Setup(m => m.GetService(application.Product)).Returns(serviceClientMock.Object);

            // Act
            var result = service.SubmitApplicationFor(application);

            // Assert
            result.Should().Be(1);
        }
    }
}