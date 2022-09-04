using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestBackEndApi.Factory;
using TestBackEndApi.Models.ViewModels;
using TestBackEndApi.Models.ViewModels.CompanyViewModel;
using TestBackEndApi.Models.ViewModels.RepositoryViewModel;

namespace TestBackEndApi.Controllers
{
    [ApiController]
    [Route("api/v1/")]
    public class CompanyController : ControllerBase
    {
        private readonly CompanyFactoryImp _companyFactory;

        public CompanyController(CompanyFactoryImp companyFactory)
        {
            _companyFactory = companyFactory;
        }

        [Route("companies")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<ListCompanyViewModel>> GetCompanies()
        {
            return await _companyFactory.GetCompaniesAsync();
        }

        [Route("company/{id}")]
        [Authorize("Admin")]
        [HttpGet]
        public async Task<ResultViewModel> GetCompanyByIdAsync(Guid id)
        {
            return await _companyFactory.GetCompanyByIdAsync(id);
        }

        [Route("company/{id}/Provider")]
        [Authorize("Admin")]
        [HttpGet]
        public async Task<IEnumerable<ListCompanyViewModel>> GetCompaniesProviderAsync(Guid id)
        {
            return await _companyFactory.GetCompaniesProviderAsync(id);
        }

        [Route("company")]
        [Authorize("Admin")]
        [HttpPost]
        public async Task<ResultViewModel> CreateCompanyAsync([Bind("Username,Password")][FromBody] EditCompanyViewModel model)
        {
            return await _companyFactory.CreateCompanyAsync(model);
        }

        [Route("company")]
        [Authorize("Admin")]
        [HttpPut]
        public async Task<ResultViewModel> UpdateCompanyAsync([Bind("Id,FantasyName,Cnpj,Uf")][FromBody] EditCompanyViewModel model)
        {
            return await _companyFactory.UpdateCompanyAsync(model);
        }

        [Route("company/{id}")]
        [Authorize("Admin")]
        [HttpDelete]
        public async Task<ResultViewModel> DeleteCompanyAsync(Guid id)
        {
            return await _companyFactory.DeleteCompanyAsync(id);
        }
    }
}
