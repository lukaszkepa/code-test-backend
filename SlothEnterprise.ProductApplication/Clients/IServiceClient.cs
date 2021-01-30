using SlothEnterprise.ProductApplication.Models.Applications;

namespace SlothEnterprise.ProductApplication.Clients
{
    public interface IServiceClient
    {
        int SubmitApplication(ISellerApplication application);
    }
}
