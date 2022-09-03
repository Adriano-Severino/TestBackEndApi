﻿using TestBackEndApi.Domain;

namespace TestBackEndApi.Data
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            ArgumentNullException.ThrowIfNull(context, nameof(context));
            context.Database.EnsureCreated();

            if (context.Companies.Any() && context.Providers.Any())
            {
                return;   // DB has been seeded
            }
            
            var companies = new Company[]
            {
              new Company { Cnpj="97.191.863/0001-25", FantasyName = "Empresa America Latina Ltda",Id = new Guid("5b9e9b24-9e06-4a0e-b460-62d750223abb"), Uf = "SP" , Providers = new List<Provider> { new Provider { Name = "Fornecedor 1" , CompanyId = new Guid("5b9e9b24-9e06-4a0e-b460-62d750223abb"), CpfCnpj = "32.298.799/0001-30", Telephone = "11234567892", CompanyName = "Empresa America Latina Ltda" , BirthDate = null, Registered = DateTime.Now, Rg = null, Id = Guid.NewGuid() } },
            }};

            foreach (Company c in companies)
            {
                context.Companies.Add(c);
            }
            context.SaveChanges();
            
            var providers = new Provider[]
            {
                new Provider { Name = "Fornecedor 1" , CompanyId = new Guid("5b9e9b24-9e06-4a0e-b460-62d750223abb"), CpfCnpj = "32.298.799/0001-30", Telephone = "11234567892", CompanyName = "Empresa America Latina Ltda" , BirthDate = null, Registered = DateTime.Now, Rg = null, Id = Guid.NewGuid() },
                new Provider { Name = "Fornecedor 2" , CompanyId = Guid.NewGuid(), CpfCnpj = "81.922.342/0001-60", Telephone = "11234567892", CompanyName = "Empresa Brasileira Ltda" , BirthDate = null, Registered = DateTime.Now, Rg = null, Id = Guid.NewGuid() },
                new Provider { Name = "Fornecedor 3" , CompanyId = Guid.NewGuid(), CpfCnpj = "66.593.323/0001-44", Telephone = "11234567892", CompanyName = "Empresa Sp Ltda" , BirthDate = null, Registered = DateTime.Now, Rg = null, Id = Guid.NewGuid() },
            };

            foreach (Provider p in providers)
            {
                context.Providers.Add(p);
            }
            context.SaveChanges();
        }
    }
}
