using Moq;
using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Clients;
using SlothEnterprise.ProductApplication.Mappers;
using SlothEnterprise.ProductApplication.Models.Products;
using System;
using Xunit;

namespace SlothEnterprise.ProductApplication.Tests.Clients
{
    public class ServiceClientFactoryTests
    {
        private readonly Mock<IServiceProvider> _serviceProviderMock;

        public ServiceClientFactoryTests()
        {
            _serviceProviderMock = new Mock<IServiceProvider>();
            RegisterMockService<IBusinessLoansService>();
            RegisterMockService<IConfidentialInvoiceService>();
            RegisterMockService<ISelectInvoiceService>();
            RegisterMockService<ICompanyDataMapper>();
        }

        [Fact]
        public void CreateService_BusinessLoans_ShouldReturnCorrectService() =>
            ShouldReturnCorrectService<BusinessLoansServiceClient>(new BusinessLoans());

        [Fact]
        public void CreateService_ConfidentialInvoiceDiscount_ShouldReturnCorrectService() =>
            ShouldReturnCorrectService<ConfidentialInvoiceServiceClient>(new ConfidentialInvoiceDiscount());

        [Fact]
        public void CreateService_SelectiveInvoiceDiscount_ShouldReturnCorrectService() =>
            ShouldReturnCorrectService<SelectInvoiceServiceClient>(new SelectiveInvoiceDiscount());

        private void ShouldReturnCorrectService<TClient>(IProduct product)
        {
            // Arrange
            var factory = new ServiceClientFactory(_serviceProviderMock.Object);

            // Act
            var client = factory.GetService(product);

            // Assert
            Assert.NotNull(client);
            Assert.IsType<TClient>(client);
        }

        private void RegisterMockService<TService>() where TService: class =>
            _serviceProviderMock.Setup(m => m.GetService(typeof(TService))).Returns(Mock.Of<TService>());
    }
}
