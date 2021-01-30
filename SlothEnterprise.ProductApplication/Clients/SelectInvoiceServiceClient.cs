using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Models.Applications;
using SlothEnterprise.ProductApplication.Models.Products;

namespace SlothEnterprise.ProductApplication.Clients
{
    public class SelectInvoiceServiceClient : ServiceClientBase, IServiceClient
    {
        private readonly ISelectInvoiceService _selectInvoiceService;

        public SelectInvoiceServiceClient(ISelectInvoiceService selectInvoiceService)
        {
            _selectInvoiceService = selectInvoiceService;
        }

        public int SubmitApplication(ISellerApplication application)
        {
            var sid = ValidateArgument<SelectiveInvoiceDiscount>(application);

            return _selectInvoiceService.SubmitApplicationFor(application.CompanyData.Number.ToString(),
                sid.InvoiceAmount, sid.AdvancePercentage);
        }
    }
}
