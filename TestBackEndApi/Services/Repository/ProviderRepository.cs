using Microsoft.EntityFrameworkCore;
using TestBackEndApi.Data;
using TestBackEndApi.Domain;
using TestBackEndApi.Models.ViewModels.ProviderViewModel;

namespace TestBackEndApi.Services.Repository
{
    public class ProviderRepository : ProviderRepositoryImp
    {
        private readonly ApplicationDbContext _context;
        public ProviderRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ListProviderViewModel>> GetProvidersAsync()
        {
            return await _context.Providers
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
            .ToListAsync();

        }
        public async Task<Provider> GetProviderByIdAsync(Guid id)
        {
            return await Task.FromResult(_context.Providers.AsNoTracking().Where(x => x.Id == id).FirstOrDefault());

        }
        public async Task<IEnumerable<Provider>> SearchProviderAsync(string? Name = null, string? cpfCnpj = null, DateTime? date = null)
        {
            return await _context.Providers.Where(x => x.Name.Contains(Name) || x.CpfCnpj == cpfCnpj || x.BirthDate == date || x.Registered == date).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<ListProviderViewModel>> GetCompanyProvidersAsync(Guid id)
        {
            return await _context.Providers
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
                    Rg = x.Rg,
                    Company = x.Company

                })
                .AsNoTracking()
            .ToListAsync();

        }
        public async Task<bool> SaveAsync(Provider provider)
        {
            _context.Providers.Add(provider);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateProviderAsync(Provider provider)
        {
            _context.Entry<Provider>(provider).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<Provider> DeleleProviderAsync(Provider provider)
        {
            _context.Providers.Remove(provider);
            await _context.SaveChangesAsync();

            return provider;
        }
    }

}
