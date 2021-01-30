using SlothEnterprise.External;
using SlothEnterprise.ProductApplication.Models.Applications;
using System;

namespace SlothEnterprise.ProductApplication.Mappers
{
    public class CompanyDataMapper : ICompanyDataMapper
    {
        public CompanyDataRequest MapToRequest(ISellerCompanyData companyData)
        {
            if (companyData is null)
            {
                throw new ArgumentNullException(nameof(companyData));
            }

            return new CompanyDataRequest
            {
                CompanyFounded = companyData.Founded,
                CompanyNumber = companyData.Number,
                CompanyName = companyData.Name,
                DirectorName = companyData.DirectorName
            };
        }
    }
}
