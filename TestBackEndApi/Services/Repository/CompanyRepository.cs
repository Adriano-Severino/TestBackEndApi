using Microsoft.EntityFrameworkCore;
using TestBackEndApi.Data;
using TestBackEndApi.Domain;
using TestBackEndApi.Services.Repository;
using TestBackEndApi.Models.ViewModels.RepositoryViewModel;

namespace TestBackEndApi.Repository
{
    public class CompanyRepository : CompanyRepositoryImp
    {
        private readonly ApplicationDbContext _context;

        public CompanyRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ListCompanyViewModel>> GetCompaniesAsync()
        {
            return await _context.Companies
                .Select(x => new ListCompanyViewModel
                {
                    Id = x.Id,
                    Cnpj = x.Cnpj,
                    FantasyName = x.FantasyName,
                    Uf = x.Uf,
                    Providers = x.Providers
                })
                .AsNoTracking()
                .ToListAsync();

        }
        public async Task<Company> GetCompanyByIdAsync(Guid id)
        {
            return await Task.FromResult(_context.Companies.AsNoTracking().Where(x => x.Id == id).FirstOrDefault());

        }
        public async Task<IEnumerable<ListCompanyViewModel>> GetCompaniesProviderAsync(Guid id)
        {
            return await _context.Companies
                .Where(x => x.Id == id)
                .Select(x => new ListCompanyViewModel
                {
                    Id = x.Id,
                    Cnpj = x.Cnpj,
                    FantasyName = x.FantasyName,
                    Uf = x.Uf,
                    Providers = x.Providers
                })
                .AsNoTracking()
                .ToListAsync();

        }
        public async Task<bool> SaveAsync(Company company)
        {
            _context.Companies.Add(company);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateCompanyAsync(Company company)
        {
            _context.Entry<Company>(company).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<Company> DeleleCompanyAsync(Company company)
        {
            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();

            return company;
        }

    }
}
