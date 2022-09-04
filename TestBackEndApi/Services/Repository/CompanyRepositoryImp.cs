using TestBackEndApi.Domain;
using TestBackEndApi.Models.ViewModels.RepositoryViewModel;

namespace TestBackEndApi.Services.Repository
{
    public interface CompanyRepositoryImp
    {
        public Task<IEnumerable<ListCompanyViewModel>> GetCompaniesAsync();
        public Task<Company> GetCompanyByIdAsync(Guid id);
        public Task<IEnumerable<ListCompanyViewModel>> GetCompaniesProviderAsync(Guid id);
        public Task<bool> SaveAsync(Company company);
        public Task<bool> UpdateCompanyAsync(Company company);
        public Task<Company> DeleleCompanyAsync(Company company);
    }
}
