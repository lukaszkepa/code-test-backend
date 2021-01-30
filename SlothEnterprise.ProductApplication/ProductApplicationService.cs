using SlothEnterprise.ProductApplication.Models.Applications;
using SlothEnterprise.ProductApplication.Clients;
using System;

namespace SlothEnterprise.ProductApplication
{
    public class ProductApplicationService : IProductApplicationService
    {
        private readonly IServiceClientFactory _serviceClientFactory;

        public ProductApplicationService(IServiceClientFactory serviceClientFactory)
        {
            _serviceClientFactory = serviceClientFactory;
        }

        public int SubmitApplicationFor(ISellerApplication application)
        {
            if (application is null)
            {
                throw new ArgumentNullException(nameof(application));
            }
            if (application.CompanyData is null || application.Product is null)
            {
                return -1;
            }

            var client = _serviceClientFactory.GetService(application.Product);
            return client.SubmitApplication(application);
        }
    }
}
