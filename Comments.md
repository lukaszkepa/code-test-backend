## Main areas of concern

1. Whole logic was implemented in a single service (`ProductApplicationService`) and all services dependencies were injected into it. From now on, for every external service there are single client which can created by `ServiceClientFactory`.
2. There was no error handling and no null checks at all. Those were added.
3. Some structural inconsistency, e.g. some interaces were placed in the same file as actual implementation, one interface was placed in a file named differently then interface itself. Those were fixed and all models are placed under `*.Models` namespace.
4. Code duplication when mapping from `CompanyData` to `CompanyDataRequest`.
5. Unit test in `ProductApplicationTests` didn't test actual implementation of `IProductApplicationTests` because this service was mocked.