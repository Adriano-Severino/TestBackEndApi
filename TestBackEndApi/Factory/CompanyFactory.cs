using TestBackEndApi.Domain;
using TestBackEndApi.Helpers;
using TestBackEndApi.Services.Repository;
using TestBackEndApi.ViewModels;
using TestBackEndApi.ViewModels.CompanyViewModel;
using TestBackEndApi.ViewModels.RepositoryViewModel;


namespace TestBackEndApi.Factory
{
    public class CompanyFactory : CompanyFactoryImp
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
        public ResultViewModel GetCompanyById(Guid id)
        {
            var company = _companyRepository.GetCompanyById(id);
            return ResultCustom.Result(company);
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
                var company = new Company();
                company.FantasyName = model.FantasyName;
                company.Cnpj = cnpj;
                company.Uf = model.Uf;

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
            var cnpj = model.Cnpj;
            model.Cnpj = CustomFormatAttribute.RemoveCharacterString(model.Cnpj);

            var company = _companyRepository.GetCompanyById(model.Id);

            if (company == null)
            {
               return ResultCustom.Result(company);
            }
            model.Validate();
            if (!model.IsValid)
                return new ResultViewModel
                {
                    Success = false,
                    Message = "Nao foi possivel atualizar a empresa",
                    Data = model.Notifications
                };
            try
            {
                company.Cnpj = cnpj;
                company.FantasyName = model.FantasyName;
                company.Uf = model.Uf;

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
                    return ResultCustom.Result(company);
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
