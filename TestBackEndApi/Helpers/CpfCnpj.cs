using Flunt.Extensions.Br.Validations;
using Flunt.Notifications;
using Flunt.Validations;
using System.Xml.Linq;
using TestBackEndApi.ViewModels.ProviderViewModel;
using DocumentValidator;

namespace TestBackEndApi.Helpers
{
    public static class CpfCnpj 
    {
        public static string IsInvalid(EditProviderViewModel model)
        {
            var cpf = model.Notifications.FirstOrDefault(x => x.Key.Contains("CPF"));
            if (cpf != null)
                return "CPF/CNPJ inválido";

            model.PhysicalPerson = true;
            if (model.Rg != null)
            {
                if (!RGValidation.Validate(model.Rg))
                {
                    return "É obrigatório cadastrar um Rg válido para pessoas física";
                } 
            }
            var error = model.Notifications.FirstOrDefault(x => x.Key != "CNPJ");
            if (error != null)
            {
                return $"{error.Key}: {error.Message}";
            }
            
            return $"CNPJ";
        }
    }
}
