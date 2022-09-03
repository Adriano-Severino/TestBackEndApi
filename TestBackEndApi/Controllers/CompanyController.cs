using Microsoft.AspNetCore.Mvc;
using TestBackEndApi.Factory;
using TestBackEndApi.Helpers;
using TestBackEndApi.Models;
using TestBackEndApi.Repository;
using TestBackEndApi.ViewModels;
using TestBackEndApi.ViewModels.CompanyViewModel;
using TestBackEndApi.ViewModels.RepositoryViewModel;

namespace TestBackEndApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly CompanyFactoryImp _companyFactory;

        public CompanyController(CompanyFactoryImp companyFactory)
        {
            _companyFactory = companyFactory;
        }

        [Route("companies")]
        [HttpGet]
        public IEnumerable<ListCompanyViewModel> GetCompanies()
        {
            return _companyFactory.GetCompanies();
        }

        [Route("company/{id}")]
        [HttpGet]
        public ResultViewModel GetCompanyById(Guid id)
        {
            return _companyFactory.GetCompanyById(id);
        }

        [Route("company/{id}/teste")]
        [HttpGet]
        public IEnumerable<ListCompanyViewModel> GetCompaniesProvider(Guid id)
        {
            return _companyFactory.GetCompaniesProvider(id);
        }

        [Route("company")]
        [HttpPost]
        public ResultViewModel CreateCompany([Bind("Id,FantasyName,Cnpj,Uf")][FromBody] EditCompanyViewModel model)
        {
            return _companyFactory.CreateCompany(model);
        }

        [Route("company")]
        [HttpPut]
        [ValidateAntiForgeryToken]
        public ResultViewModel UpdateCompany([Bind("Id,FantasyName,Cnpj,Uf")][FromBody] EditCompanyViewModel model)
        {
            return _companyFactory.UpdateCompany(model);
        }

        [Route("company/{id}")]
        [HttpDelete]
        public ResultViewModel DeleteCompany(Guid id)
        {
            return  _companyFactory.DeleteCompany(id);
        }
    }
}
