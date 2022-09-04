using System.ComponentModel.DataAnnotations;
using TestBackEndApi.Models;

namespace TestBackEndApi.Models.ViewModels.RepositoryViewModel
{
    public class ListCompanyViewModel
    {
        public Guid Id { get; set; }
        public string FantasyName { get; set; }
        public string Cnpj { get; set; }
        public string Uf { get; set; }
        public object Providers { get; set; }
    }
}
