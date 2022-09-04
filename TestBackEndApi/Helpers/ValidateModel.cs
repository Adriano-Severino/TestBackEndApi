using TestBackEndApi.Models.ViewModels;
using TestBackEndApi.Models.ViewModels.ProviderViewModel;

namespace TestBackEndApi.Helpers
{
    public static class ValidateModel
    {
        public static ResultViewModel isNotValid(EditProviderViewModel model)
        {
            if (model.Notifications.Any(x => x.Key.Contains("CNPJ")))
            {
                return new ResultViewModel
                {
                    Success = false,
                    Message = "Não foi possivel cadastrar o fornecedor",
                    Data = CpfCnpj.IsInvalid(model)
                };
            }
            else
            {
                return new ResultViewModel
                {
                    Success = false,
                    Message = "Não foi possivel cadastrar o fornecedor",
                    Data = model
                };
            }
        }
    }
}
