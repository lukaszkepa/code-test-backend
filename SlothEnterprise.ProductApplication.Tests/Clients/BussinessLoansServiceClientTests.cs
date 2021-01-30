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
    public class BussinessLoansServiceClientTests
    {
        private readonly Mock<ICompanyDataMapper> _companyDataMapperMock;
        private readonly Mock<IBusinessLoansService> _businessLoansServiceMock;

        public BussinessLoansServiceClientTests()
        {
            _companyDataMapperMock = new Mock<ICompanyDataMapper>();
            _businessLoansServiceMock = new Mock<IBusinessLoansService>();
        }

        [Fact]
        public void Submit_WhenCalledWithNull_ShouldThrowException()
        {
            // Arrange
            var client = new BusinessLoansServiceClient(_businessLoansServiceMock.Object, _companyDataMapperMock.Object);

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
            var client = new BusinessLoansServiceClient(_businessLoansServiceMock.Object, _companyDataMapperMock.Object);

            // Assert
            Assert.Throws<ArgumentException>(() => client.SubmitApplication(application));
        }

        [Fact]
        public void Submit_ShouldCallService_And_ReturnCorrectResult()
        {
            // Arrange
            var application = new SellerApplication
            {
                Product = new BusinessLoans(),
                CompanyData = new SellerCompanyData(),
            };
            var serviceResult = Mock.Of<IApplicationResult>(m => m.Success == true && m.ApplicationId == 1);

            var client = new BusinessLoansServiceClient(_businessLoansServiceMock.Object, _companyDataMapperMock.Object);

            _businessLoansServiceMock.Setup(m => m.SubmitApplicationFor(It.IsAny<CompanyDataRequest>(), It.IsAny<LoansRequest>()))
                .Returns(serviceResult);

            // Act
            var result = client.SubmitApplication(application);

            // Assert
            Assert.Equal(serviceResult.ApplicationId, result);
        }
    }
}
