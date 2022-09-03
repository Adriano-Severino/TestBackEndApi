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

namespace TestBackEndApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProviderController : ControllerBase
    {
        private readonly ProviderRepository _Repositorio;
        private readonly CompanyRepository _RepositorioCompanyRepository;
        public ProviderController(ProviderRepository repositorio, CompanyRepository repositorioCompanyRepository = null)
        {
            _Repositorio = repositorio;
            _RepositorioCompanyRepository = repositorioCompanyRepository;
        }

        [Route("providers")]
        [HttpGet]
        public IEnumerable<ListProviderViewModel> GetProviders()
        {
            return _Repositorio.GetProviders();
        }

        [Route("provider/{id}")]
        [HttpGet]
        public TestBackEndApi.Domain.Provider GetProviderById(Guid id)
        {
            return _Repositorio.GetProviderById(id);
        }

        [Route("provider/{Search}")]
        [HttpGet]
        public Provider SearchProvider(string? Name = null, string? cpfCnpj = null, DateTime? date = null)
        {
            return _Repositorio.SearchProvider(Name, cpfCnpj, date);
        }

        [Route("provider/{id}/teste")]
        [HttpGet]
        public IEnumerable<ListProviderViewModel> GetCompanyProviders(Guid id)
        {
            return _Repositorio.GetCompanyProviders(id);
        }

        [Route("provider")]
        [HttpPost]
        public ResultViewModel CreateProvider([Bind("Id,Name,CompanyName,CpfCnpj,Telephone,CustomData")][FromBody] EditProviderViewModel model)
        {
            var cpfCnpj = model.CpfCnpj;
            model.CpfCnpj = CustomFormatAttribute.RemoveCharacterString(model.CpfCnpj);
            model.PhysicalPerson = false;
            model.Validate();
            if (!model.IsValid)
                if (model.Notifications.Any(x => x.Key.Contains("CNPJ")))
                {
                    return new ResultViewModel
                    {
                        Success = false,
                        Message = "Não foi possivel adicionar o forncedor",
                        Data = CpfCnpj.IsInvalid(model)
                    };
                }
                else
                {
                    return new ResultViewModel
                    {
                        Success = false,
                        Message = "Não foi possivel adicionar o forncedor",
                        Data = model
                    };
                }

            var company = _RepositorioCompanyRepository.GetCompanyById(model.CompanyId);
            if (company == null)
                return new ResultViewModel
                {
                    Success = false,
                    Message = "Empresa não encontrada",
                    Data = $"Id:{model.CompanyId} veja se a empresa esta cadastrada no sistema"
                };
            
            if (CalculateOfAge.IsOfAge(model.BirthDate) && company.Uf.ToLower().Contains("sp") && model.PhysicalPerson)
            {
                return new ResultViewModel
                {
                    Success = false,
                    Message = "Nào foi possivel cadastrar o fornecedor",
                    Data = "O fornecedor deve ser maior de 18 anos"
                };
            }

            try
            {
                var provider = new Provider();
                provider.Name = model.Name;
                provider.CpfCnpj = cpfCnpj;
                provider.Registered = DateTime.Now;
                provider.Rg = model.PhysicalPerson ? model.Rg : null;
                provider.BirthDate = model.PhysicalPerson ? model.BirthDate : null;
                provider.CompanyName = model.CompanyName;
                provider.CompanyId = model.CompanyId;
                _Repositorio.Save(provider);

                return new ResultViewModel
                {
                    Success = true,
                    Message = "Fornecedor adicionado com Sucesso!",
                    Data = provider
                };
            }
            catch (Exception)
            {
                return new ResultViewModel
                {
                    Success = false,
                    Message = "Erro ao criar cadastrar fornecedor!",
                    Data = model.Notifications
                };
            }

        }

        [Route("provider")]
        [HttpPut]
        [ValidateAntiForgeryToken]
        public ResultViewModel UpdateProvider([Bind("Id,Name,CompanyName,CpfCnpj,Telephone,CustomData")][FromBody] EditProviderViewModel model)
        {
            var provider = _Repositorio.GetProviderById(model.Id);

            if (provider == null)
            {
                return new ResultViewModel
                {
                    Success = false,
                    Message = "Não encontrado",
                    Data = model.Notifications
                };
            }
            model.Validate();
            if (model.IsValid)
                return new ResultViewModel
                {
                    Success = false,
                    Message = "Nao foi possivel atualizar o fornecedor",
                    Data = model.Notifications
                };
            try
            {
                provider.Name = model.Name;
                provider.CompanyName = model.CompanyName;
                provider.CpfCnpj = provider.CpfCnpj;

                _Repositorio.UpdateProvider(provider);

                return new ResultViewModel
                {
                    Success = true,
                    Message = "Fornecedor atualizado Com Sucesso!",
                    Data = provider
                };
            }
            catch (Exception)
            {
                return new ResultViewModel
                {
                    Success = false,
                    Message = "Erro ao atualizar o fornecedor",
                    Data = model.Notifications
                };
            }
        }

        [Route("provider/{id}")]
        [HttpDelete]
        public ResultViewModel DeleteProvider(Guid id)
        {
            try

            {
                var provider = _Repositorio.GetProviderById(id);

                if (provider == null)
                {
                    return new ResultViewModel()
                    {
                        Success = false,
                        Message = "Não encontrado",
                    };
                }

                _Repositorio.DeleleProvider(provider);

                return new ResultViewModel
                {
                    Success = true,
                    Message = "Fornecedor Deletado com Sucesso!"
                };
            }
            catch (Exception)
            {
                return new ResultViewModel
                {
                    Success = false,
                    Message = "Erro ao deletado o fornecedor!"
                };
            }

        }
    }
}
