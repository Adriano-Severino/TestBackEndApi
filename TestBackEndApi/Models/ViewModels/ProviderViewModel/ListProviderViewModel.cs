﻿using System.ComponentModel.DataAnnotations;
using TestBackEndApi.Domain;

namespace TestBackEndApi.Models.ViewModels.ProviderViewModel
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
        public Company Company { get; set; }
        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Registered { get; set; }
    }
}
