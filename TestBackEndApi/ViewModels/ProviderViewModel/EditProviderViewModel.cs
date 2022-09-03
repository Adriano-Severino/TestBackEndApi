using Flunt.Extensions.Br.Validations;
using Flunt.Notifications;
using Flunt.Validations;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using TestBackEndApi.Helpers.Extension;

namespace TestBackEndApi.ViewModels.ProviderViewModel
{
    public class EditProviderViewModel : Notifiable<Notification>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public Guid CompanyId { get; set; }
        public string CpfCnpj { get; set; }
        public string? Rg { get; set; }
        public string Telephone { get; set; }
        public bool PhysicalPerson { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        public DateTime Registered { get; set; }
        public void Validate()
        {
            AddNotifications(new Contract<Notification>()
               .IsCnpj(CpfCnpj, "CNPJ", "CPF/CNPJ inválido"));

            if (this.Notifications.Count >= 1)
            {
                AddNotifications(new Contract<Notification>()
               .IsCpf(CpfCnpj, "CPF", "CPF/CNPJ inválido"));
            }
            AddNotifications(new Contract<Notification>()
                .IsGreaterThan(Name, 3, "Nome", "Nome deve ter no minimo 3 caracteres")
               .IsLowerThan(Name, 34, "Nome", "Nome deve ter no minimo 34 caracteres")
               .IsGreaterThan(CompanyName, 3, "Nome", "Nome deve ter no minimo 3 caracteres")
               .IsLowerThan(CompanyName, 64, "Nome", "Nome deve ter no minimo 3 caracteres")
               .IsLowerThan(Telephone, 64, "Telefone", "Telefone deve ser menor que 54 caracteres")
               );

            

        }
    }
}
