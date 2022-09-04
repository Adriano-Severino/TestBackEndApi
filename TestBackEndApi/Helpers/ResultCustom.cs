using TestBackEndApi.ViewModels;

namespace TestBackEndApi.Helpers
{
    public static class ResultCustom
    {
        public static ResultViewModel Result(object data)
        {
            if (data == null)
            {
                return new ResultViewModel
                {
                    Success = false,
                    Message = "Não encontrado",
                    Data = data
                };
            }

            var name = data.GetType().GetProperty("Name")?.GetValue(data, null);
            var fantasyName = data.GetType().GetProperty("FantasyName")?.GetValue(data, null);
            
            var providerName =  data.GetType().GetProperty("Name");
            var companyName = data.GetType().GetProperty("FantasyName");

            var dataName = name != null ? name : fantasyName;
            var dataKey = providerName != null ? providerName.Name : companyName?.Name;

            dataKey = dataKey == "Name" ? "Fornecedor" : "Empresa";
            
            return new ResultViewModel
            {
                Success = true,
                Message = $"{dataKey}: {dataName}",
                Data = data
            };
        }
    }
}
