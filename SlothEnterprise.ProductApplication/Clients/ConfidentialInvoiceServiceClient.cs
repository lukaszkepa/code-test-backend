using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Models.Applications;
using SlothEnterprise.ProductApplication.Mappers;
using SlothEnterprise.ProductApplication.Models.Products;
using System;

namespace SlothEnterprise.ProductApplication.Clients
{
    public class ConfidentialInvoiceServiceClient : ServiceClientBase, IServiceClient
    {
        private readonly IConfidentialInvoiceService _confidentialInvoiceService;
        private readonly ICompanyDataMapper _companyDataMapper;

        public ConfidentialInvoiceServiceClient(IConfidentialInvoiceService confidentialInvoiceService, ICompanyDataMapper companyDataMapper)
        {
            _confidentialInvoiceService = confidentialInvoiceService;
            _companyDataMapper = companyDataMapper;
        }

        public int SubmitApplication(ISellerApplication application)
        {
            var cid = ValidateArgument<ConfidentialInvoiceDiscount>(application);

            var result = _confidentialInvoiceService.SubmitApplicationFor(_companyDataMapper.MapToRequest(application.CompanyData),
                cid.TotalLedgerNetworth, cid.AdvancePercentage, cid.VatRate);

            return GetResult(result);
        }
    }
}
