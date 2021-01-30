using Moq;
using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Models.Applications;
using SlothEnterprise.ProductApplication.Clients;
using SlothEnterprise.ProductApplication.Models.Products;
using System;
using Xunit;

namespace SlothEnterprise.ProductApplication.Tests.Clients
{
    public class SelectInvoiceServiceClientTests
    {
        private readonly Mock<ISelectInvoiceService> _selectInvoiceServiceMock;

        public SelectInvoiceServiceClientTests()
        {
            _selectInvoiceServiceMock = new Mock<ISelectInvoiceService>();
        }

        [Fact]
        public void Submit_WhenCalledWithNull_ShouldThrowException()
        {
            // Arrange
            var client = new SelectInvoiceServiceClient(_selectInvoiceServiceMock.Object);

            // Assert
            Assert.Throws<ArgumentNullException>(() => client.SubmitApplication(null));
        }

        [Fact]
        public void Submit_WhenCalledWithUnsupportedProduct_ShouldThrowException()
        {
            // Arrange
            var application = new SellerApplication
            {
                Product = new BusinessLoans(),
            };
            var client = new SelectInvoiceServiceClient(_selectInvoiceServiceMock.Object);

            // Assert
            Assert.Throws<ArgumentException>(() => client.SubmitApplication(application));
        }

        [Fact]
        public void Submit_WhenCalledWithMissingCompanyData_ShouldThrowException()
        {
            // Arrange
            var application = new SellerApplication
            {
                Product = new SelectiveInvoiceDiscount(),
            };
            var client = new SelectInvoiceServiceClient(_selectInvoiceServiceMock.Object);

            // Assert
            Assert.Throws<ArgumentException>(() => client.SubmitApplication(application));
        }

        [Fact]
        public void Submit_ShouldCallService_And_ReturnCorrectResult()
        {
            // Arrange
            var product = new SelectiveInvoiceDiscount
            {
                AdvancePercentage = 6.99m,
                InvoiceAmount = 1500m,
            };
            var companyData = new SellerCompanyData
            {
                 Number = 123,
            };
            var serviceResult = 3;

            var client = new SelectInvoiceServiceClient(_selectInvoiceServiceMock.Object);

            _selectInvoiceServiceMock.Setup(m => m.SubmitApplicationFor("123", product.InvoiceAmount, product.AdvancePercentage))
                .Returns(serviceResult);

            // Act
            var result = client.SubmitApplication(new SellerApplication
            {
                Product = product,
                CompanyData = companyData,
            });

            // Assert
            Assert.Equal(serviceResult, result);
        }
    }
}
