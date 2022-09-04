using TestBackEndApi.Helpers;
using TestBackEndApi.Models;
using TestBackEndApi.Repository;
using TestBackEndApi.Models.ViewModels;
using TestBackEndApi.Models.ViewModels.CompanyViewModel;
using TestBackEndApi.Models.ViewModels.RepositoryViewModel;

namespace TestBackEndApi.Factory
{
    public interface CompanyFactoryImp
    {
        public Task<IEnumerable<ListCompanyViewModel>> GetCompaniesAsync();
        public Task<ResultViewModel> GetCompanyByIdAsync(Guid id);
        public Task<IEnumerable<ListCompanyViewModel>> GetCompaniesProviderAsync(Guid id);
        public Task<ResultViewModel> CreateCompanyAsync(EditCompanyViewModel model);
        public Task<ResultViewModel> UpdateCompanyAsync(EditCompanyViewModel model);
        public Task<ResultViewModel> DeleteCompanyAsync(Guid id);
    }
}
