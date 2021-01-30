using SlothEnterprise.ProductApplication.Models.Applications;
using SlothEnterprise.ProductApplication.Mappers;
using System;
using Xunit;

namespace SlothEnterprise.ProductApplication.Tests.Mappers
{
    public class CompanyDataMapperTests
    {
        [Fact]
        public void MapToRequest_WhenCallWithNull_ShouldThrowException()
        {
            // Arrange
            var mapper = new CompanyDataMapper();

            // Assert
            Assert.Throws<ArgumentNullException>(() => mapper.MapToRequest(null));
        }

        [Fact]
        public void MapToRequest_ShouldMapCorrectly()
        {
            // Arrange
            var data = new SellerCompanyData
            {
                DirectorName = "Director #1",
                Founded = DateTime.Today,
                Name = "Name #1",
                Number = 1,
            };
            var mapper = new CompanyDataMapper();

            // Act
            var result = mapper.MapToRequest(data);

            // Assert
            Assert.Equal(data.DirectorName, result.DirectorName);
            Assert.Equal(data.Founded, result.CompanyFounded);
            Assert.Equal(data.Name, result.CompanyName);
            Assert.Equal(data.Number, result.CompanyNumber);
        }
    }
}
