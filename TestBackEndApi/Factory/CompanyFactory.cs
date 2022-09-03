using Microsoft.AspNetCore.Mvc;
using TestBackEndApi.Domain;
using TestBackEndApi.Helpers;
using TestBackEndApi.Services.Repository;
using TestBackEndApi.ViewModels;
using TestBackEndApi.ViewModels.CompanyViewModel;
using TestBackEndApi.ViewModels.RepositoryViewModel;


namespace TestBackEndApi.Factory
{
    public class CompanyFactory 
    {
        private readonly CompanyRepositoryImp _companyRepository;
        public CompanyFactory(CompanyRepositoryImp companyRepository)
        {
            _companyRepository = companyRepository;
        }
        public IEnumerable<ListCompanyViewModel> GetCompanies()
        {
            return _companyRepository.GetCompanies();
        }
        public Company GetCompanyById(Guid id)
        {
            return _companyRepository.GetCompanyById(id);
        }
        public IEnumerable<ListCompanyViewModel> GetCompaniesProvider(Guid id)
        {
            return _companyRepository.GetCompaniesProvider(id);
        }
        public ResultViewModel CreateCompany(EditCompanyViewModel model)
        {
            var cnpj = model.Cnpj;
            model.Cnpj = CustomFormatAttribute.RemoveCharacterString(model.Cnpj);

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
                var company = new TestBackEndApi.Domain.Company();
                company.FantasyName = model.FantasyName;
                company.Cnpj = cnpj;

                _companyRepository.Save(company);

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
        public ResultViewModel UpdateCompany(EditCompanyViewModel model)
        {
            var company = _companyRepository.GetCompanyById(model.Id);

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

                _companyRepository.UpdateCompany(company);

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
        public ResultViewModel DeleteCompany(Guid id)
        {
            try

            {
                var company = _companyRepository.GetCompanyById(id);

                if (company == null)
                {
                    return new ResultViewModel()
                    {
                        Success = false,
                        Message = "Não encontrado",
                    };
                }

                _companyRepository.DeleleCompany(company);

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
