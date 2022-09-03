using Microsoft.EntityFrameworkCore;
using TestBackEndApi.Data;
using TestBackEndApi.Domain;
using TestBackEndApi.Services.Repository;
using TestBackEndApi.ViewModels.RepositoryViewModel;

namespace TestBackEndApi.Repository
{
    public class CompanyRepository : CompanyRepositoryImp
    {
        private readonly ApplicationDbContext _context;

        public CompanyRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<ListCompanyViewModel> GetCompanies()
        {
            return _context.Companies
                //.Include(x => x.Providers)
                .Select(x => new ListCompanyViewModel
                {
                    Id = x.Id,
                    Cnpj = x.Cnpj,
                    FantasyName = x.FantasyName,
                    Uf = x.Uf,
                    Providers = x.Providers
                })
                .AsNoTracking()
                .ToList();

        }
        public Company GetCompanyById(Guid id)
        {
            return _context.Companies.AsNoTracking()
            //.Include(x => x.Providers)
            .Where(x => x.Id == id).FirstOrDefault();

        }
        public IEnumerable<ListCompanyViewModel> GetCompaniesProvider(Guid id)
        {
            return _context.Companies
                .Where(x => x.Id == id)
                //.Include(x => x.Providers)
                .Select(x => new ListCompanyViewModel
                {
                    Id = x.Id,
                    Cnpj = x.Cnpj,
                    FantasyName = x.FantasyName,
                    Uf = x.Uf,
                    Providers = x.Providers
                })
                .AsNoTracking()
                .ToList();

        }
        public void Save(Company company)
        {
            _context.Companies.Add(company);
            _context.SaveChanges();
        }
        public void UpdateCompany(Company company)
        {
            _context.Entry<Company>(company).State = EntityState.Modified;
            _context.SaveChanges();
        }
        public Company DeleleCompany(Company company)
        {
            _context.Companies.Remove(company);
            _context.SaveChanges();

            return company;
        }

    }
}
