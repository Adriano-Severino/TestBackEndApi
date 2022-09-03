using TestBackEndApi.Helpers;
using TestBackEndApi.ViewModels.CompanyViewModel;
using TestBackEndApi.ViewModels.RepositoryViewModel;
using TestBackEndApi.ViewModels;
using TestBackEndApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace TestBackEndApi.Services.Repository
{
    public interface CompanyRepositoryImp
    {
        public IEnumerable<ListCompanyViewModel> GetCompanies();
        public Company GetCompanyById(Guid id);
        public IEnumerable<ListCompanyViewModel> GetCompaniesProvider(Guid id);
        public void Save(Company company);
        public void UpdateCompany(Company company);
        public Company DeleleCompany(Company company);
    }
}
