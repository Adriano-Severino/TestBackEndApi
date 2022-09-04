using Microsoft.AspNetCore.Mvc;
using TestBackEndApi.Helpers;
using TestBackEndApi.ViewModels.CompanyViewModel;
using TestBackEndApi.ViewModels.RepositoryViewModel;
using TestBackEndApi.ViewModels;
using TestBackEndApi.Repository;
using TestBackEndApi.Services.Repository;
using TestBackEndApi.ViewModels.ProviderViewModel;
using TestBackEndApi.Models;
using TestBackEndApi.Helpers.Extension;
using TestBackEndApi.Factory;
using System.ComponentModel.DataAnnotations;

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
        [HttpGet]
        public IEnumerable<ListProviderViewModel> GetProviders()
        {
            return _providerFactory.GetProviders();
        }

        [Route("provider/{id}")]
        [HttpGet]
        public ResultViewModel GetProviderById(Guid id)
        {
            return _providerFactory.GetProviderById(id);
        }

        [Route("provider/search")]
        [HttpGet]
        public ResultViewModel SearchProvider(string? Name = null, string? cpfCnpj = null, DateTime? date = null)
        {
            return _providerFactory.SearchProvider(Name, cpfCnpj, date);
        }

        [Route("provider/{id}/company")]
        [HttpGet]
        public IEnumerable<ListProviderViewModel> GetCompanyProviders(Guid id)
        {
            return _providerFactory.GetCompanyProviders(id);
        }

        [Route("provider")]
        [HttpPost]
        public ResultViewModel CreateProvider([Bind("Name,CpfCnpj,Rg,Telephone,BirthDate,Registered")][FromBody] EditProviderViewModel model)
        {
            return _providerFactory.CreateProvider(model);
        }

        [Route("provider")]
        [HttpPut]
        public ResultViewModel UpdateProvider([Bind("Id,Name,CpfCnpj,Rg,Telephone,BirthDate,Registered")][FromBody] EditProviderViewModel model)
        {
            return _providerFactory.UpdateProvider(model);
        }

        [Route("provider/{id}")]
        [HttpDelete]
        public ResultViewModel DeleteProvider(Guid id)
        {
            return _providerFactory.DeleteProvider(id);
        }
    }
}
