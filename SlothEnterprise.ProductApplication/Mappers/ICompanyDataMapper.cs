using SlothEnterprise.External;
using SlothEnterprise.ProductApplication.Models.Applications;

namespace SlothEnterprise.ProductApplication.Mappers
{
    public interface ICompanyDataMapper
    {
        CompanyDataRequest MapToRequest(ISellerCompanyData companyData);
    }
}
