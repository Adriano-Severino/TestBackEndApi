using TestBackEndApi.Models;
using TestBackEndApi.ViewModels;
using TestBackEndApi.ViewModels.ProviderViewModel;

namespace TestBackEndApi.Factory
{
    public interface ProviderFactoryImp
    {
        public IEnumerable<ListProviderViewModel> GetProviders();
        public Provider GetProviderById(Guid id);
        public Provider SearchProvider(string? Name = null, string? cpfCnpj = null, DateTime? date = null);
        public IEnumerable<ListProviderViewModel> GetCompanyProviders(Guid id);
        public ResultViewModel CreateProvider(EditProviderViewModel model);
        public ResultViewModel UpdateProvider(EditProviderViewModel model);
        public ResultViewModel DeleteProvider(Guid id);
    }
}
