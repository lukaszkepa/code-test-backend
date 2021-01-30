using SlothEnterprise.ProductApplication.Models.Products;

namespace SlothEnterprise.ProductApplication.Clients
{
    public interface IServiceClientFactory
    {
        IServiceClient GetService(IProduct product);
    }
}
