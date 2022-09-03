using Microsoft.AspNetCore.Mvc;
using TestBackEndApi.Helpers.Extension;
using TestBackEndApi.Helpers;
using TestBackEndApi.Services.Repository;
using TestBackEndApi.ViewModels.ProviderViewModel;
using TestBackEndApi.ViewModels;
using TestBackEndApi.Models;

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

        public IEnumerable<ListProviderViewModel> GetProviders()
        {
            return _providerRepository.GetProviders();
        }
        public Provider GetProviderById(Guid id)
        {
            return _providerRepository.GetProviderById(id);
        }
        public Provider SearchProvider(string? Name = null, string? cpfCnpj = null, DateTime? date = null)
        {
            return _providerRepository.SearchProvider(Name, cpfCnpj, date);
        }
        public IEnumerable<ListProviderViewModel> GetCompanyProviders(Guid id)
        {
            return _providerRepository.GetCompanyProviders(id);
        }
        public ResultViewModel CreateProvider(EditProviderViewModel model)
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

            var company = _companyRepository.GetCompanyById(model.CompanyId);
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
                _providerRepository.Save(provider);

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
        public ResultViewModel UpdateProvider(EditProviderViewModel model)
        {
            var provider = _providerRepository.GetProviderById(model.Id);

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

                _providerRepository.UpdateProvider(provider);

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
        public ResultViewModel DeleteProvider(Guid id)
        {
            try

            {
                var provider = _providerRepository.GetProviderById(id);

                if (provider == null)
                {
                    return new ResultViewModel()
                    {
                        Success = false,
                        Message = "Não encontrado",
                    };
                }

                _providerRepository.DeleleProvider(provider);

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
