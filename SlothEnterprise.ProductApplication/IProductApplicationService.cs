﻿using SlothEnterprise.ProductApplication.Models.Applications;

namespace SlothEnterprise.ProductApplication
{
    public interface IProductApplicationService
    {
        int SubmitApplicationFor(ISellerApplication application);
    }
}