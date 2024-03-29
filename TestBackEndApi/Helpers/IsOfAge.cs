﻿using TestBackEndApi.Domain;
using TestBackEndApi.Helpers.Extension;
using TestBackEndApi.Models.ViewModels;
using TestBackEndApi.Models.ViewModels.ProviderViewModel;

namespace TestBackEndApi.Helpers
{
    public static class IsOfAge
    {
        public static bool Result(EditProviderViewModel model, Company company)
        {
            if (CalculateOfAge.IsOfAge(model.BirthDate) && company.Uf.ToLower().Contains("sp") && model.PhysicalPerson)
            {
               return true;
            }
            return false;
        }
    }
}
