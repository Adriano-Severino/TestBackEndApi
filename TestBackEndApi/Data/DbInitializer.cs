using Newtonsoft.Json.Linq;
using TestBackEndApi.Domain;
using TestBackEndApi.Services;

namespace TestBackEndApi.Data
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            ArgumentNullException.ThrowIfNull(context, nameof(context));
            context.Database.EnsureCreated();

            if (context.Companies.Any() && context.Providers.Any() && context.Users.Any())
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
           

            var providers = new Provider[]
            {
                new Provider {  Name = "Fornecedor 2" , CompanyId = Guid.NewGuid(), CpfCnpj = "81.922.342/0001-60", Telephone = "11234567892", CompanyName = "Empresa Brasileira Ltda" , BirthDate = null, Registered = DateTime.Now, Rg = null, Id = Guid.NewGuid(), Company = new Company { Cnpj="76.389.639/0001-72", FantasyName = "Nova Empresa Ltda",Id = Guid.NewGuid(), Uf = "PR" } },
               
            };

            foreach (Provider p in providers)
            {
                context.Providers.Add(p);
            }
           


            var users = new User[]
            {
                new User { Username = "usuário 1" , Password = Encrypt.EncryptPassword("123"), Role = "Admin" , Token = ""},
                new User { Username = "usuário 2" , Password = Encrypt.EncryptPassword("123"), Role = "User" , Token = ""}
            };

            foreach (User user in users)
            {
                context.Users.Add(user);
            }
            context.SaveChanges();
        }
    }
}
