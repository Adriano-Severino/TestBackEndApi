using Microsoft.EntityFrameworkCore;
using TestBackEndApi.Domain;
using TestBackEndApi.Models.ViewModels.ProviderViewModel;

namespace TestBackEndApi.Services.Repository
{
    public interface ProviderRepositoryImp
    {
        public Task<IEnumerable<ListProviderViewModel>> GetProvidersAsync();
        public Task<Provider> GetProviderByIdAsync(Guid id);
        public Task<IEnumerable<Provider>> SearchProviderAsync(string? Name = null, string? cpfCnpj = null, DateTime? date = null);
        public Task<IEnumerable<ListProviderViewModel>> GetCompanyProvidersAsync(Guid id);
        public Task<bool> SaveAsync(Provider provider);
        public Task<bool> UpdateProviderAsync(Provider provider);
        public Task<Provider> DeleleProviderAsync(Provider provider);

    }
}
