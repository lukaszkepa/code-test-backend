using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Mappers;
using SlothEnterprise.ProductApplication.Models.Products;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace SlothEnterprise.ProductApplication.Clients
{
    public class ServiceClientFactory : IServiceClientFactory
    {
        private readonly IDictionary<Type, Func<IServiceClient>> _clients;

        public ServiceClientFactory(IServiceProvider serviceProvider)
        {
            _clients = new Dictionary<Type, Func<IServiceClient>>
            {
                [typeof(BusinessLoans)] = () => new BusinessLoansServiceClient(serviceProvider.GetRequiredService<IBusinessLoansService>(),
                    serviceProvider.GetRequiredService<ICompanyDataMapper>()),

                [typeof(ConfidentialInvoiceDiscount)] = () => new ConfidentialInvoiceServiceClient(serviceProvider.GetRequiredService<IConfidentialInvoiceService>(),
                    serviceProvider.GetRequiredService<ICompanyDataMapper>()),

                [typeof(SelectiveInvoiceDiscount)] = () => new SelectInvoiceServiceClient(serviceProvider.GetRequiredService<ISelectInvoiceService>()),
            };
        }

        public IServiceClient GetService(IProduct product)
        {
            if (product is null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            var productType = product.GetType();
            if (!_clients.TryGetValue(productType, out Func<IServiceClient> clientCreator))
            {
                throw new Exception($"Provided product of type '{productType.Name}' is not supported.");
            }
            return clientCreator();
        }
    }
}
