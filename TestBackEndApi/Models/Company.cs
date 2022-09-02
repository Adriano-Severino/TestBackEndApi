using System.ComponentModel.DataAnnotations;

namespace TestBackEndApi.Models
{
    public class Company
    {
        public Guid Id { get; set; }
        public string FantasyName { get; set; }
        public string Cnpj { get; set; }
        public string Uf { get; set; }
        public Guid ProviderId { get; set; }
        public ICollection<Provider> Providers { get; set; }

    }
}
