using SlothEnterprise.External;
using SlothEnterprise.ProductApplication.Models.Applications;
using System;

namespace SlothEnterprise.ProductApplication.Clients
{
    public abstract class ServiceClientBase
    {
        /// <summary>
        /// Validate <see cref="ISellerApplication"/> and throw exception when not valid.
        /// </summary>
        /// <typeparam name="TProduct"></typeparam>
        /// <param name="application"></param>
        /// <returns>Product casted to <typeparamref name="TProduct"/></returns>
        protected TProduct ValidateArgument<TProduct>(ISellerApplication application)
        {
            if (application is null)
            {
                throw new ArgumentNullException(nameof(application));
            }
            if (application.CompanyData is null)
            {
                throw new ArgumentException("Application company data is required in order to submit an application.");
            }
            if (!(application.Product is TProduct product))
            {
                throw new ArgumentException($"Provided application product is not supported by '{GetType().Name}'.");
            }
            return product;
        }

        /// <summary>
        /// Get result from <see cref="IApplicationResult"/>.
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        protected int GetResult(IApplicationResult result)
        {
            if (result is null)
            {
                return -1;
            }
            return result.Success? result.ApplicationId ?? -1 : -1;
        }
    }
}
