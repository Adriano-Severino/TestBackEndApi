using TestBackEndApi.Domain;
using TestBackEndApi.Models.ViewModels.ListUserViewModel;

namespace TestBackEndApi.Services.Repository
{
    public interface UserServicesRepositoryImp
    {
        public Task<User> AuthenticateAsync(string username, string password);
        public Task<IEnumerable<ListUserViewModel>> GetUsersAsync();
    }
}
