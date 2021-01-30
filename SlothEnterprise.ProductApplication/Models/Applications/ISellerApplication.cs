using SlothEnterprise.ProductApplication.Models.Products;

namespace SlothEnterprise.ProductApplication.Models.Applications
{
    public interface ISellerApplication
    {
        IProduct Product { get; set; }
        ISellerCompanyData CompanyData { get; set; }
    }
}
