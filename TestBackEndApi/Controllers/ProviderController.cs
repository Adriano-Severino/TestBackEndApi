using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestBackEndApi.Factory;
using TestBackEndApi.Models.ViewModels;
using TestBackEndApi.Models.ViewModels.ProviderViewModel;
using TestBackEndApi.Repository;

namespace TestBackEndApi.Controllers
{
    [ApiController]
    [Route("api/v1/")]
    public class ProviderController : ControllerBase
    {
        private readonly ProviderFactoryImp _providerFactory;
        private readonly CompanyRepository _RepositorioCompanyRepository;
        public ProviderController(ProviderFactoryImp providerFactory, CompanyRepository repositorioCompanyRepository = null)
        {
            _providerFactory = providerFactory;
            _RepositorioCompanyRepository = repositorioCompanyRepository;
        }

        [Route("providers")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<ListProviderViewModel>> GetProvidersAsync()
        {
            return await _providerFactory.GetProvidersAsync();
        }

        [Route("provider/{id}")]
        [Authorize("Admin")]
        [HttpGet]
        public async Task<ResultViewModel> GetProviderByIdAsync(Guid id)
        {
            return await _providerFactory.GetProviderByIdAsync(id);
        }

        [Route("provider/search")]
        [Authorize("Admin")]
        [HttpGet]
        public async Task<ResultViewModel> SearchProviderAsync(string? Name = null, string? cpfCnpj = null, DateTime? date = null)
        {
            return await _providerFactory.SearchProviderAsync(Name, cpfCnpj, date);
        }

        [Route("provider/{id}/company")]
        [Authorize("Admin")]
        [HttpGet]
        public async Task<IEnumerable<ListProviderViewModel>> GetCompanyProvidersAsync(Guid id)
        {
            return await _providerFactory.GetCompanyProvidersAsync(id);
        }

        [Route("provider")]
        [Authorize("Admin")]
        [HttpPost]
        public async Task<ResultViewModel> CreateProviderAsync([Bind("Name,CpfCnpj,Rg,Telephone,BirthDate,Registered")][FromBody] EditProviderViewModel model)
        {
            return await _providerFactory.CreateProviderAsync(model);
        }

        [Route("provider")]
        [Authorize("Admin")]
        [HttpPut]
        public async Task<ResultViewModel> UpdateProviderAsync([Bind("Id,Name,CpfCnpj,Rg,Telephone,BirthDate,Registered")][FromBody] EditProviderViewModel model)
        {
            return await _providerFactory.UpdateProviderAsync(model);
        }

        [Route("provider/{id}")]
        [Authorize("Admin")]
        [HttpDelete]
        public async Task<ResultViewModel> DeleteProviderAsync(Guid id)
        {
            return await _providerFactory.DeleteProviderAsync(id);
        }
    }
}
