using System.ComponentModel.DataAnnotations;
using TestBackEndApi.Models;

namespace TestBackEndApi.ViewModels.ProviderViewModel
{
    public class ListProviderViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string CpfCnpj { get; set; }
        public string Rg { get; set; }
        public string Telephone { get; set; }
        public Guid CompanyId { get; set; }

        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Registered { get; set; }
    }
}
