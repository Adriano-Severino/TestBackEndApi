using Microsoft.EntityFrameworkCore;
using TestBackEndApi.Data;
using TestBackEndApi.Domain;
using TestBackEndApi.ViewModels.ProviderViewModel;

namespace TestBackEndApi.Services.Repository
{
    public class ProviderRepository : ProviderRepositoryImp
    {
        private readonly ApplicationDbContext _context;
        public ProviderRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<ListProviderViewModel> GetProviders()
        {
            return _context.Providers
                .Select(x => new ListProviderViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    BirthDate = x.BirthDate,
                    CompanyId = x.Company.Id,
                    CompanyName = x.CompanyName,
                    Registered = x.Registered,
                    CpfCnpj = x.CpfCnpj,
                    Rg = x.Rg,
                })
                .AsNoTracking()
            .ToList();

        }
        public Provider GetProviderById(Guid id)
        {
            return _context.Providers.AsNoTracking()
            .Where(x => x.Id == id).FirstOrDefault();

        }
        public IEnumerable<Provider> SearchProvider(string? Name = null, string? cpfCnpj = null, DateTime? date = null)
        {
            return _context.Providers.Where(x => x.Name == Name || x.CpfCnpj == cpfCnpj || x.BirthDate == date || x.Registered == date).AsNoTracking().ToList();
        }
        public IEnumerable<ListProviderViewModel> GetCompanyProviders(Guid id)
        {
            return _context.Providers
                .Where(x => x.Id == id)
                .Select(x => new ListProviderViewModel
                {
                    Id = x.Id,
                   Name = x.Name,
                   BirthDate = x.BirthDate,
                   CompanyId = x.Company.Id,
                   CompanyName = x.CompanyName,
                   Registered = x.Registered,
                   CpfCnpj = x.CpfCnpj,
                   Rg =x.Rg,
                   Company = x.Company
                   
                })
                .AsNoTracking()
            .ToList();

        }
        public bool Save(Provider provider)
        {
            _context.Providers.Add(provider);
            _context.SaveChanges();
            return true;
        }
        public bool UpdateProvider(Provider provider)
        {
            _context.Entry<Provider>(provider).State = EntityState.Modified;
            _context.SaveChanges();
            return true;
        }
        public Provider DeleleProvider(Provider provider)
        {
            _context.Providers.Remove(provider);
            _context.SaveChanges();

            return provider;
        }
    }

}
