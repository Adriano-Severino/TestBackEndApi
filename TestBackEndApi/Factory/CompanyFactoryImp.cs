using TestBackEndApi.Helpers;
using TestBackEndApi.Models;
using TestBackEndApi.Repository;
using TestBackEndApi.ViewModels;
using TestBackEndApi.ViewModels.CompanyViewModel;
using TestBackEndApi.ViewModels.RepositoryViewModel;

namespace TestBackEndApi.Factory
{
    public interface CompanyFactoryImp
    {
        public IEnumerable<ListCompanyViewModel> GetCompanies();
        public ResultViewModel GetCompanyById(Guid id);
        public IEnumerable<ListCompanyViewModel> GetCompaniesProvider(Guid id);
        public ResultViewModel CreateCompany(EditCompanyViewModel model);
        public ResultViewModel UpdateCompany(EditCompanyViewModel model);
        public ResultViewModel DeleteCompany(Guid id);
    }
}
