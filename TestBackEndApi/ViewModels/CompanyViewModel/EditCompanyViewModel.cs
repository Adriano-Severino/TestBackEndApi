using Flunt.Extensions.Br.Validations;
using Flunt.Notifications;
using Flunt.Validations;
using TestBackEndApi.Helpers;

namespace TestBackEndApi.ViewModels.CompanyViewModel
{
    public class EditCompanyViewModel : Notifiable<Notification>
    {
        public Guid Id { get; set; }
        public string FantasyName { get; set; }

        public string Cnpj { get; set; }
        public string Uf { get; set; }
        public void Validate()
        {
            AddNotifications(new Contract<Notification>()
                .IsCnpj(Cnpj, "CNPJ", "CNPJ inválido")
               .IsGreaterThan(FantasyName, 3, "FantasyName", "Nome Fantasia deve ter no minimo 3 caracteres")
               .IsLowerThan(FantasyName, 64, "FantasyName", "Nome Fantasia deve ser menor que 64 caracteres")
               .IsNotNullOrEmpty(FantasyName, "Nome Fantasia é obrigatório")
               .IsNotNullOrEmpty(Cnpj, "Cnpj é obrigatório")
               .IsNotNullOrEmpty(Uf, "Uf é obrigatório")
               );
        }
    }
}
