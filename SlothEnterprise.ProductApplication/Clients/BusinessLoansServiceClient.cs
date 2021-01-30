using SlothEnterprise.External;
using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Models.Applications;
using SlothEnterprise.ProductApplication.Mappers;
using SlothEnterprise.ProductApplication.Models.Products;

namespace SlothEnterprise.ProductApplication.Clients
{
    public class BusinessLoansServiceClient : ServiceClientBase, IServiceClient
    {
        private readonly IBusinessLoansService _businessLoansService;
        private readonly ICompanyDataMapper _companyDataMapper;

        public BusinessLoansServiceClient(IBusinessLoansService businessLoansService, ICompanyDataMapper companyDataMapper)
        {
            _businessLoansService = businessLoansService;
            _companyDataMapper = companyDataMapper;
        }

        public int SubmitApplication(ISellerApplication application)
        {
            var loans = ValidateArgument<BusinessLoans>(application);

            var result = _businessLoansService.SubmitApplicationFor(_companyDataMapper.MapToRequest(application.CompanyData),
                new LoansRequest
                {
                    InterestRatePerAnnum = loans.InterestRatePerAnnum,
                    LoanAmount = loans.LoanAmount
                });
            return GetResult(result);
        }
    }
}
