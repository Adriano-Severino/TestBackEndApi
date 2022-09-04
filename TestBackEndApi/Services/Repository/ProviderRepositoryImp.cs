using Microsoft.EntityFrameworkCore;
using TestBackEndApi.Domain;
using TestBackEndApi.ViewModels.ProviderViewModel;

namespace TestBackEndApi.Services.Repository
{
    public interface ProviderRepositoryImp
    {
        public IEnumerable<ListProviderViewModel> GetProviders();
        public Provider GetProviderById(Guid id);
        public IEnumerable<Provider> SearchProvider(string? Name = null, string? cpfCnpj = null, DateTime? date = null);
        public IEnumerable<ListProviderViewModel> GetCompanyProviders(Guid id);
        public bool Save(Provider provider);
        public bool UpdateProvider(Provider provider);
        public Provider DeleleProvider(Provider provider);
        
    }
}
