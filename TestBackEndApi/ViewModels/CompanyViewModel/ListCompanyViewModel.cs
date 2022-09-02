﻿using System.ComponentModel.DataAnnotations;
using TestBackEndApi.Models;

namespace TestBackEndApi.ViewModels.RepositoryViewModel
{
    public class ListCompanyViewModel
    {
        public Guid Id { get; set; }
        public string FantasyName { get; set; }
        public string Cnpj { get; set; }
        public string Uf { get; set; }
        public ICollection<Provider> Providers { get; set; }
    }
}