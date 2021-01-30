using Moq;
using SlothEnterprise.External;
using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Models.Applications;
using SlothEnterprise.ProductApplication.Clients;
using SlothEnterprise.ProductApplication.Mappers;
using SlothEnterprise.ProductApplication.Models.Products;
using System;
using Xunit;

namespace SlothEnterprise.ProductApplication.Tests.Clients
{
    public class ConfidentialInvoiceServiceClientTests
    {
        private readonly Mock<ICompanyDataMapper> _companyDataMapperMock;
        private readonly Mock<IConfidentialInvoiceService> _confidentialInvoiceServiceMock;

        public ConfidentialInvoiceServiceClientTests()
        {
            _companyDataMapperMock = new Mock<ICompanyDataMapper>();
            _confidentialInvoiceServiceMock = new Mock<IConfidentialInvoiceService>();
        }

        [Fact]
        public void Submit_WhenCalledWithNull_ShouldThrowException()
        {
            // Arrange
            var client = new ConfidentialInvoiceServiceClient(_confidentialInvoiceServiceMock.Object, _companyDataMapperMock.Object);

            // Assert
            Assert.Throws<ArgumentNullException>(() => client.SubmitApplication(null));
        }

        [Fact]
        public void Submit_WhenCalledWithUnsupportedProduct_ShouldThrowException()
        {
            // Arrange
            var application = new SellerApplication
            {
                Product = new SelectiveInvoiceDiscount(),
            };
            var client = new ConfidentialInvoiceServiceClient(_confidentialInvoiceServiceMock.Object, _companyDataMapperMock.Object);

            // Assert
            Assert.Throws<ArgumentException>(() => client.SubmitApplication(application));
        }

        [Fact]
        public void Submit_ShouldCallService_And_ReturnCorrectResult()
        {
            // Arrange
            var product = new ConfidentialInvoiceDiscount
            {
                TotalLedgerNetworth = 10m,
                AdvancePercentage = 5.99m,
            };
            var application = new SellerApplication
            {
                Product = product,
                CompanyData = new SellerCompanyData(),
            };
            var serviceResult = Mock.Of<IApplicationResult>(m => m.Success == true && m.ApplicationId == 2);

            var client = new ConfidentialInvoiceServiceClient(_confidentialInvoiceServiceMock.Object, _companyDataMapperMock.Object);

            _confidentialInvoiceServiceMock.Setup(m => m.SubmitApplicationFor(It.IsAny<CompanyDataRequest>(),
                product.TotalLedgerNetworth, product.AdvancePercentage, product.VatRate)).Returns(serviceResult);

            // Act
            var result = client.SubmitApplication(application);

            // Assert
            Assert.Equal(serviceResult.ApplicationId, result);
        }
    }
}
