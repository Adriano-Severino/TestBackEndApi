using TestBackEndApi.Domain;
using TestBackEndApi.Helpers;
using TestBackEndApi.Models.ViewModels;
using TestBackEndApi.Models.ViewModels.ProviderViewModel;
using TestBackEndApi.Services.Repository;

namespace TestBackEndApi.Factory
{
    public class ProviderFactory : ProviderFactoryImp
    {
        private readonly ProviderRepositoryImp _providerRepository;
        private readonly CompanyRepositoryImp _companyRepository;
        public ProviderFactory(ProviderRepositoryImp providerRepository, CompanyRepositoryImp companyRepository)
        {
            _providerRepository = providerRepository;
            _companyRepository = companyRepository;
        }

        public async Task<IEnumerable<ListProviderViewModel>> GetProvidersAsync()
        {
            return await _providerRepository.GetProvidersAsync();
        }
        public async Task<ResultViewModel> GetProviderByIdAsync(Guid id)
        {
            var provider = await _providerRepository.GetProviderByIdAsync(id);
            return ResultCustom.Result(provider);
        }
        public async Task<ResultViewModel> SearchProviderAsync(string? Name = null, string? cpfCnpj = null, DateTime? date = null)
        {
            var provider = await _providerRepository.SearchProviderAsync(Name, cpfCnpj, date);
            return ResultCustom.Result(provider);
        }
        public async Task<IEnumerable<ListProviderViewModel>> GetCompanyProvidersAsync(Guid id)
        {
            return await _providerRepository.GetCompanyProvidersAsync(id);
        }
        public async Task<ResultViewModel> CreateProviderAsync(EditProviderViewModel model)
        {
            var cpfCnpj = model.CpfCnpj;
            model.CpfCnpj = CustomFormatAttribute.RemoveCharacterString(model.CpfCnpj);
            model.PhysicalPerson = false;
            model.Validate();
            if (!model.IsValid)
            {
                var error = ValidateModel.isNotValid(model);
                if (error.Data != "CNPJ") return error;
            }

            var company = await _companyRepository.GetCompanyByIdAsync(model.CompanyId);
            if (company == null)
                return new ResultViewModel
                {
                    Success = false,
                    Message = "Empresa não encontrada",
                    Data = $"Id:{model.CompanyId} veja se a empresa esta cadastrada no sistema"
                };

            var ofAge = IsOfAge.Result(model, company);

            if (ofAge)
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
                provider.CompanyName = company.FantasyName;
                provider.CompanyId = model.CompanyId;
                provider.Telephone = model.Telephone;

                await _providerRepository.SaveAsync(provider);

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
        public async Task<ResultViewModel> UpdateProviderAsync(EditProviderViewModel model)
        {
            var cpfCnpj = model.CpfCnpj;
            model.CpfCnpj = CustomFormatAttribute.RemoveCharacterString(model.CpfCnpj);
            model.PhysicalPerson = false;

            var provider = await _providerRepository.GetProviderByIdAsync(model.Id);

            if (provider == null)
            {
                return ResultCustom.Result(provider);
            }

            model.Validate();
            if (!model.IsValid)
                ValidateModel.isNotValid(model);

            var company = await _companyRepository.GetCompanyByIdAsync(model.CompanyId);
            if (company == null)
                return new ResultViewModel
                {
                    Success = false,
                    Message = "Empresa não encontrada",
                    Data = $"Id:{model.CompanyId} veja se a empresa esta cadastrada no sistema"
                };

            var ofAge = IsOfAge.Result(model, company);

            if (ofAge)
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
                provider.Name = model.Name;
                provider.CpfCnpj = cpfCnpj;
                provider.Registered = DateTime.Now;
                provider.Rg = model.PhysicalPerson ? model.Rg : null;
                provider.BirthDate = model.PhysicalPerson ? model.BirthDate : null;
                provider.CompanyName = company.FantasyName;
                provider.CompanyId = model.CompanyId;
                provider.Telephone = model.Telephone;

                await _providerRepository.UpdateProviderAsync(provider);

                return new ResultViewModel
                {
                    Success = true,
                    Message = "Fornecedor atualizado som sucesso!",
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
        public async Task<ResultViewModel> DeleteProviderAsync(Guid id)
        {
            try

            {
                var provider = await _providerRepository.GetProviderByIdAsync(id);

                if (provider == null)
                {
                    return ResultCustom.Result(provider);
                }

                await _providerRepository.DeleleProviderAsync(provider);

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
