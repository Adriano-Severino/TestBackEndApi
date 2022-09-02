using Microsoft.AspNetCore.Mvc;
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
        private readonly CompanyRepository _Repositorio;

        public CompanyController(CompanyRepository repositorio)
        {
            _Repositorio = repositorio;
        }

        [Route("companies")]
        [HttpGet]
        public IEnumerable<ListCompanyViewModel> GetCompany()
        {
            return _Repositorio.GetCompany();
        }

        [Route("Company/{id}")]
        [HttpGet]
        public Company GetCompanyById(Guid id)
        {
            return _Repositorio.GetCompanyById(id);
        }

        [Route("Company/{id}/teste")]
        [HttpGet]
        public IEnumerable<ListCompanyViewModel> GetProvidersTeste(Guid id)
        {
            return _Repositorio.GetCompanyProviders(id);
        }

        [Route("Company")]
        [HttpPost]
        public ResultViewModel CreateCompany([Bind("Id,FantasyName,Cnpj,Uf")][FromBody] EditCompanyViewModel model)
        {
            var cnpj = model.Cnpj;
            model.Cnpj =  CustomFormatAttribute.RemoveCharacterString(model.Cnpj);
            
            model.Validate();
            if (!model.IsValid)
                return new ResultViewModel
                {
                    Success = false,
                    Message = "Nao foi possivel adicionar a empresa",
                    Data = model.Notifications
                };
            try
            {
                var company = new Company();
                company.FantasyName = model.FantasyName;
                company.Cnpj = cnpj;

                _Repositorio.Save(company);

                return new ResultViewModel
                {
                    Success = true,
                    Message = "Empresa adicionado com Sucesso!",
                    Data = company
                };
            }
            catch (Exception)
            {
                return new ResultViewModel
                {
                    Success = false,
                    Message = "Erro ao criar cadastrar a empresa!",
                    Data = model.Notifications
                };
            }

        }

        [Route("company")]
        [HttpPut]
        [ValidateAntiForgeryToken]
        public ResultViewModel UpdateCompany([Bind("Id,FantasyName,Cnpj,Uf")][FromBody] EditCompanyViewModel model)
        {
            var company = _Repositorio.GetCompanyById(model.Id);

            if (company == null)
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
                    Message = "Nao foi possivel atualizar a empresa",
                    Data = model.Notifications
                };
            try
            {
                company.Cnpj = model.Cnpj;
                company.FantasyName = model.FantasyName;
                company.Uf = company.Uf;

                _Repositorio.UpdateCompany(company);

                return new ResultViewModel
                {
                    Success = true,
                    Message = "Empresa atualizado Com Sucesso!",
                    Data = company
                };
            }
            catch (Exception)
            {
                return new ResultViewModel
                {
                    Success = false,
                    Message = "Erro ao atualizar a empresa",
                    Data = model.Notifications
                };
            }
        }

        [Route("Company/{id}")]
        [HttpDelete]
        public ResultViewModel DeleteCompany(Guid id)
        {
            try

            {
                var company = _Repositorio.GetCompanyById(id);

                if (company == null)
                {
                    return new ResultViewModel()
                    {
                        Success = false,
                        Message = "Não encontrado",
                    };
                }

                _Repositorio.DeleleCompany(company);

                return new ResultViewModel
                {
                    Success = true,
                    Message = "Empresa Deletado com Sucesso!"
                };
            }
            catch (Exception)
            {
                return new ResultViewModel
                {
                    Success = false,
                    Message = "Erro ao deletado a empresa!"
                };
            }

        }
    }
}
