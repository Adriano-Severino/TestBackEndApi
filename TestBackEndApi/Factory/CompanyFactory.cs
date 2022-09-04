using TestBackEndApi.Domain;
using TestBackEndApi.Helpers;
using TestBackEndApi.Services.Repository;
using TestBackEndApi.Models.ViewModels;
using TestBackEndApi.Models.ViewModels.CompanyViewModel;
using TestBackEndApi.Models.ViewModels.RepositoryViewModel;


namespace TestBackEndApi.Factory
{
    public class CompanyFactory : CompanyFactoryImp
    {
        private readonly CompanyRepositoryImp _companyRepository;
        public CompanyFactory(CompanyRepositoryImp companyRepository)
        {
            _companyRepository = companyRepository;
        }
        public async Task<IEnumerable<ListCompanyViewModel>> GetCompaniesAsync()
        {
            return await _companyRepository.GetCompaniesAsync();
        }
        public async Task<ResultViewModel> GetCompanyByIdAsync(Guid id)
        {
            var company = await _companyRepository.GetCompanyByIdAsync(id);
            return ResultCustom.Result(company);
        }
        public async Task<IEnumerable<ListCompanyViewModel>> GetCompaniesProviderAsync(Guid id)
        {
            return await _companyRepository.GetCompaniesProviderAsync(id);
        }
        public async Task<ResultViewModel> CreateCompanyAsync(EditCompanyViewModel model)
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

                await _companyRepository.SaveAsync(company);

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
        public async Task<ResultViewModel> UpdateCompanyAsync(EditCompanyViewModel model)
        {
            var cnpj = model.Cnpj;
            model.Cnpj = CustomFormatAttribute.RemoveCharacterString(model.Cnpj);

            var company = await _companyRepository.GetCompanyByIdAsync(model.Id);

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

                await _companyRepository.UpdateCompanyAsync(company);

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
        public async Task<ResultViewModel> DeleteCompanyAsync(Guid id)
        {
            try

            {
                var company = await _companyRepository.GetCompanyByIdAsync(id);

                if (company == null)
                {
                    return ResultCustom.Result(company);
                }

                await _companyRepository.DeleleCompanyAsync(company);

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
