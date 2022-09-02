using TestBackEndApi.Models;

namespace TestBackEndApi.ViewModels.ProviderViewModel
{
    public class ListProviderViewModel
    {
        public Guid Id { get; set; }
        public string FantasyName { get; set; }
        public string Cnpj { get; set; }
        public string Uf { get; set; }
    }
}
