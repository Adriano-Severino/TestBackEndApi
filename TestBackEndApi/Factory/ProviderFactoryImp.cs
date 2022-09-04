using TestBackEndApi.Models;
using TestBackEndApi.Models.ViewModels;
using TestBackEndApi.Models.ViewModels.ProviderViewModel;

namespace TestBackEndApi.Factory
{
    public interface ProviderFactoryImp
    {
        public Task<IEnumerable<ListProviderViewModel>> GetProvidersAsync();
        public Task<ResultViewModel> GetProviderByIdAsync(Guid id);
        public Task<ResultViewModel> SearchProviderAsync(string? Name = null, string? cpfCnpj = null, DateTime? date = null);
        public Task<IEnumerable<ListProviderViewModel>> GetCompanyProvidersAsync(Guid id);
        public Task<ResultViewModel> CreateProviderAsync(EditProviderViewModel model);
        public Task<ResultViewModel> UpdateProviderAsync(EditProviderViewModel model);
        public Task<ResultViewModel> DeleteProviderAsync(Guid id);
    }
}
