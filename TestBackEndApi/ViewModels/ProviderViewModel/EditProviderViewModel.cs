using Flunt.Extensions.Br.Validations;
using Flunt.Notifications;
using Flunt.Validations;
using System.ComponentModel.DataAnnotations;
using TestBackEndApi.Helpers.Extension;

namespace TestBackEndApi.ViewModels.ProviderViewModel
{
    public class EditProviderViewModel : Notifiable<Notification>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string CpfCnpj { get; set; }
        public string Telephone { get; set; }
        public bool PhysicalPerson { get; set; }

        [DataType(DataType.Date)]
        public DateTime Registration { get; set; }
        public void Validate()
        {
            AddNotifications(new Contract<Notification>()
               .IsCpf(CpfCnpj, "CPF", "CPF inválido")
               .IsCnpj(CpfCnpj, "CNPJ", "CNPJ inválido")
               .IsGreaterThan(Name, 3, "Nome", "Nome deve ter no minimo 3 caracteres")
               .IsLowerThan(Name, 34, "Nome", "Nome deve ter no minimo 34 caracteres")
               .IsGreaterThan(CompanyName, 3, "Nome Fantasia", "Nome Fantasia deve ter no minimo 3 caracteres")
               .IsLowerThan(CompanyName, 64, "Nome Fantasia", "Nome Fantasia deve ter no minimo 3 caracteres")
               .IsGreaterThan(Registration.CalculateOfAgeProvider(), 17, "Fornecedor", "O fornecedor deve ser maior de 18 anos")
               .IsLowerThan(Telephone, 64, "Telefone", "Telefone deve ser menor que 54 caracteres")
               .IsNotNullOrEmpty(CompanyName, "Nome Fantasia é obrigatorio")
               .IsNotNullOrEmpty(CpfCnpj, "Nome Cpf/Cnpj é obrigatorio")
               .IsNotNullOrEmpty(Name, "Nome é obrigatorio")
               );

        }
    }
}
